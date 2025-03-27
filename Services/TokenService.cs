using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TokenService
    {
        private TokenRepository tokenRepository = new TokenRepository();
        public void AddToken(TokenForgetPassword token)
        {
            tokenRepository.Add(token);
        }
    }
}
