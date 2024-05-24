using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class PhotoFileException : Exception
    {
        public string V { get; set; }
        public PhotoFileException(string v,string? message) : base(message)
        {
            V = v;
        }
    }
}
