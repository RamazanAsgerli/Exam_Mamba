using System.ComponentModel.DataAnnotations;

namespace EXAM_MVC15.DTOs.AccountDto
{
    public class LoginDto
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Confirmed { get; set; }
    }
}
