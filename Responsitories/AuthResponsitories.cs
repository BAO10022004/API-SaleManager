using SaleManagerWebAPI.Data;
using SaleManagerWebAPI.IResponsitories;
using SaleManagerWebAPI.Models.Entities;

namespace SaleManagerWebAPI.Responsitories
{
    public class AuthResponsitories : IAuthResponsitories
    {
        private readonly SaleContext _dbContext;
        public AuthResponsitories(SaleContext dbContext) { 
            _dbContext = dbContext;
        }
        public Account FindAccountByEmail(string email)
        {
            return _dbContext.Accounts.FirstOrDefault(account => account.Email == email);
        }

        public Account FindAccountByUsername(string username)
        {
            return _dbContext.Accounts.FirstOrDefault(account => account.Username == username);
        }
        public void AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");
            try
            {
                _dbContext.Accounts.Add(account);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                _dbContext.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception($"Failed to add account {ex.Message}");
            }
        }
        public Account UpdateAccount(Account accountUpdated)
        {
            try
            {

                 _dbContext.Accounts.Update(accountUpdated);
                _dbContext.SaveChanges();
                return accountUpdated;
            }catch(Exception ex){
                throw new Exception($"Failed to update account {ex.Message}");
            }
        }
    }
}
