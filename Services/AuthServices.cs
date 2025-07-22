using SaleManagerWebAPI.Data;
using SaleManagerWebAPI.Models.Entities;
using SaleManagerWebAPI.Responsitories;
using SaleManagerWebAPI.Tools;

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
            if (AccountTooler.IsEmail(emailOrUsername))
                return _responsitories.FindAccountByEmail(emailOrUsername);
            return _responsitories.FindAccountByUsername(emailOrUsername);
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

        #region ChangePassword
        public Account ChangePassword(Account account, string newPassword)
        {
            if (account == null)
                throw new ArgumentNullException("account is null");
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentException("newPassword cannot be null or empty");

            account.PasswordHash = newPassword;
            return _responsitories.UpdateAccount(account);
        }
        #endregion
       
        #region ChangeEmail
        public Account ChangeEmail(Account account, string newEmail)
        {
            if (account == null)
                throw new ArgumentNullException("account is null");
            if (string.IsNullOrEmpty(newEmail))
                throw new ArgumentException("Email cannot be null or empty");

            account.Email = newEmail;
            return _responsitories.UpdateAccount(account);
        }
        #endregion
    }
}
