using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Interfaces
{
    public interface IPassword
    {
       public string Hash(string password);
       public bool Verify(string password, string hash);
    }
}
