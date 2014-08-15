using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface ITokenService : IBaseService<Token>
    {
        Token GetByCode(string code);

        IList<Token> GetByUser(int userId);

        IList<Token> GetByEmail(string emailAddress);

        Token Create(User user);

        bool IsCodeValid(string code, bool requriesAdmin = false);

        bool IsCodeValid(string code, out Token token, bool requriesAdmin = false);
    }
}