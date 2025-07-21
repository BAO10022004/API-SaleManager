using SaleManagerWebAPI.Data;
using SaleManagerWebAPI.Models.Entities;
using SaleManagerWebAPI.Responsitories;

namespace SaleManagerWebAPI.Services
{
    public class AuthServices
    {
        private readonly AuthResponsitories _responsitories;
        public AuthServices(SaleContext dbContext)
        {
            _responsitories = new AuthResponsitories(dbContext);
        }

        #region FindAccountWithEmailOrUsername
        public Account FindAccountWithEmailOrUsername(string emailOrUsername)
        {
            if(emailOrUsername == null)
                throw new ArgumentNullException("emailOrUsername is null");

            var account = _responsitories.FindAccountByEmail(emailOrUsername) 
                           ?? _responsitories.FindAccountByUsername(emailOrUsername);
            return account;
        }
        #endregion

        #region FindAccountWithEmail
        public Account FindAccountWithEmail(string email)
        {
            if (email == null)
                throw new ArgumentNullException("email is null");

            return _responsitories.FindAccountByEmail(email);
        }
        #endregion

        #region FindAccountWithUsername 
        public Account FindAccountWithUsername(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username is null");

            return _responsitories.FindAccountByUsername(username);
        }
        #endregion

        #region SignUp
        public Account SignUp(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account is null");

            _responsitories.AddAccount(account);
            return account;
        }
        #endregion

    }
}
