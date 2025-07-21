using SaleManagerWebAPI.Models.Entities;

namespace SaleManagerWebAPI.IResponsitories
{
    public interface IAuthResponsitories
    {
        Account FindAccountByEmail(string email);
        Account FindAccountByUsername(string username);
        void AddAccount(Account account);
    }
}
