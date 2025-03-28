using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TokenRepository
    {
        public void Add(TokenForgetPassword token)
        {
            PrnFinalProjectContext.Ins.TokenForgetPasswords.Add(token);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
    }
}
