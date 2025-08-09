namespace SaleManagerWebAPI.Interface.IResponsitories
{
    public interface IEmailResponsitoris
    {
        Task SendVerificationCodeAsync(string email, string code);
    }
}
