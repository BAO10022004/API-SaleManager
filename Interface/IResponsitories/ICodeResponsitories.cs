namespace SaleManagerWebAPI.Interface.IResponsitories
{
    public interface ICodeResponsitories
    {
        void DeactivateOldCodes(Guid accountId);
        void AddCode(string email, string code);
        public Task<bool> VerifyCodeAsync(string email, string code);
    }
}
