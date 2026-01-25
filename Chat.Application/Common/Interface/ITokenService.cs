using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Interface
{
    public interface ITokenService
    {
        string createAccessToken(Guid userId, string email, IList<string> roles);
        string CreateRefreshToken();
    }
}
