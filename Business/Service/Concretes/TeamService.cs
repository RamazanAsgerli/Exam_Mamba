using Business.CustomExceptions;
using Business.Service.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public void AddTeam(Team team)
        {
            if (team == null) throw new EntityNullException("", "entity is null!!!");
            if (team.PhotoFile == null) throw new PhotoFileException("PhotoFile", "File is not");
            if (team.PhotoFile.Length > 2098152) throw new PhotoLengthException("PhotoFile", "Photo<2mb !!!");
            if (!team.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Duzgun format deyil");
            string path= "C:\\Users\\ll novbe\\Desktop\\EXAM_MVC15\\EXAM_MVC15\\wwwroot\\Upload\\Team\\" + team.PhotoFile.FileName;
           
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                team.PhotoFile.CopyTo(stream);
            }
            team.ImgUrl = team.PhotoFile.FileName;
            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void Delete(int id)
        {
            var team = _teamRepository.Get(x => x.Id == id);
            if (team == null) throw new EntityNullException("", "Entity is null");
            string path = "C:\\Users\\ll novbe\\Desktop\\EXAM_MVC15\\EXAM_MVC15\\wwwroot\\Upload\\Team\\" + team.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.Delete();
            _teamRepository.Delete(team);
            _teamRepository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void Update(int id, Team newteam)
        {
            var oldTeam= _teamRepository.Get(x => x.Id == id);
            if (oldTeam == null)throw new EntityNullException("", "Entity is null");
            if(newteam.PhotoFile != null)
            {
                if (newteam.PhotoFile.Length > 2098152) throw new PhotoLengthException("PhotoFile", "Photo<2mb !!!");
                if (!newteam.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "");
                string oldpath = "C:\\Users\\ll novbe\\Desktop\\EXAM_MVC15\\EXAM_MVC15\\wwwroot\\Upload\\Team\\" + oldTeam.ImgUrl;
                if (File.Exists(oldpath))
                {
                    FileInfo fileInfo = new FileInfo(oldpath);
                    fileInfo.Delete();
                }
                string newpath = "C:\\Users\\ll novbe\\Desktop\\EXAM_MVC15\\EXAM_MVC15\\wwwroot\\Upload\\Team\\" + newteam.PhotoFile.FileName;
                using(FileStream stream=new FileStream(newpath, FileMode.Create))
                {
                    newteam.PhotoFile.CopyTo(stream);
                }
                oldTeam.ImgUrl = newteam.PhotoFile.FileName;
            }
            oldTeam.Name = newteam.Name;
            oldTeam.Position = newteam.Position;
            _teamRepository.Commit();
        }
    }
}
