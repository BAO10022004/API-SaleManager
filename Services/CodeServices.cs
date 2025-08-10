using SaleManagerWebAPI.Interface.IResponsitories;
using SaleManagerWebAPI.Models.Entities;
using System.Net.Sockets;
using System.Net;
using SaleManagerWebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SaleManagerWebAPI.Services
{
    public class CodeServices : ICodeResponsitories
    {
        private readonly AuthServices _authServices;
        private readonly DeviceServices _deviceServices;
        private readonly SaleContext _dbContext;
        public CodeServices(SaleContext saleContext ) {
            _dbContext = saleContext;
            _authServices =new AuthServices(_dbContext);
            _deviceServices = new DeviceServices();
        }
        #region AddCode
        public void AddCode(string email, string code)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be null or empty.", nameof(code));

            var codeItem = new CodeVetify();
            var account = _authServices.FindAccountWithEmail(email);


            if (account == null)
                throw new ArgumentException("Account not found for the provided email.");

            DeactivateOldCodes(account.Id);

            codeItem.AccountId = account.Id;
            codeItem.Code = code;
            codeItem.CreatedAt = DateTime.UtcNow;
            codeItem.ExpiresAt = DateTime.UtcNow.AddMinutes(60);
            codeItem.IsActive = true;
            codeItem.DeviceInfo = _deviceServices.GetDeviceMAC();
            codeItem.IpAddress = _deviceServices.GetDeviceIP();
            codeItem.VerifiedAt = DateTime.MinValue;
            _dbContext.CodeVetifies.Add(codeItem);
            _dbContext.SaveChangesAsync();
        }
        #endregion

        #region DeactivateOldCodes
        public void DeactivateOldCodes(Guid accountId)
        {
            var oldCodes = _dbContext.CodeVetifies
               .Where(c => c.AccountId == accountId && c.IsActive)
               .ToList();

            foreach (var oldCode in oldCodes)
            {
                oldCode.IsActive = false;
            }
        }


        #endregion

        #region VerifyCodeAsync
        public async Task<bool> VerifyCodeAsync(string email, string code)
        {
            try
            {
                var account = _authServices.FindAccountWithEmail(email);
                if (account == null) return false;

                var codeItem = await _dbContext.CodeVetifies
                    .FirstOrDefaultAsync(c =>
                        c.AccountId == account.Id &&
                        c.Code == code &&
                        c.IsActive &&
                        c.ExpiresAt > DateTime.UtcNow);

                if (codeItem == null) return false;

                // Vô hiệu hóa code sau khi verify thành công
                codeItem.IsActive = false;
                codeItem.VerifiedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error verifying code", ex);
            }
        }
        #endregion
    }
}
