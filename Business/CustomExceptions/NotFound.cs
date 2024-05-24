using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class NotFound : Exception
    {
        public string V { get; set; }
        public NotFound(string v,string? message) : base(message)
        {
            V = v;
        }
    }
}
