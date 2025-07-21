using Microsoft.AspNetCore.Mvc;
using SaleManagerWebAPI.Data;
using SaleManagerWebAPI.Models.Dtos;
using SaleManagerWebAPI.Models.Entities;
using SaleManagerWebAPI.Models.MyResponse;
using SaleManagerWebAPI.Result;
using SaleManagerWebAPI.Services;

namespace SaleManagerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly BaseReponseService _baseReponseService;
        private readonly AuthServices _authService;
        public AuthController(
            AuthServices authServices,
            BaseReponseService baseReponseService
        )
        {
            _baseReponseService = baseReponseService;
            _authService = authServices;
        }

        #region FindAccountWithEmailOrUsername
        [HttpGet("FindAccountByEmailOrUsername/{emailOrUsername}")]
        public ActionResult FindAccountByEmailOrUsername(string emailOrUsername)
        {
            if(string.IsNullOrEmpty(emailOrUsername))
                return BadRequest(_baseReponseService.CreateErrorResponse("Email or Username cannot be empty."));
            var account = _authService.FindAccountWithEmailOrUsername(emailOrUsername);
            if (account != null)
            {
                return Ok(_baseReponseService.CreateSuccessResponse(account, "Account found successfully."));
            }
            return NotFound(_baseReponseService.CreateErrorResponse("Account not found."));
        }
        #endregion

        #region Sign Up
        /////////  ---------- Sign Up ----------  /////////
        [HttpPost("SignUp")]
        public ActionResult SignUp([FromBody] AccountSignUpDTO account)
        {
            try
            {
                if (account == null)
                    return BadRequest(_baseReponseService.CreateErrorResponse("Account data cannot be null."));

                // Fix logic: cần ít nhất username HOẶC email
                if (string.IsNullOrEmpty(account.Username) && string.IsNullOrEmpty(account.Email))
                    return BadRequest(_baseReponseService.CreateErrorResponse("Username or Email is required."));

                if (string.IsNullOrEmpty(account.Password))
                    return BadRequest(_baseReponseService.CreateErrorResponse("Password is required."));

                PasswordValidationResult validationResult = new PasswordValidationResult();
                CheckPassword(account.Password, account.Username, account.Email, validationResult);

                if (validationResult.Errors.Any())
                {
                    return BadRequest(_baseReponseService.CreateErrorResponse("Password validation failed.", validationResult.Errors));
                }

                // Kiểm tra tồn tại account - xử lý null safety
                Account existingAccount = null;

                if (!string.IsNullOrEmpty(account.Email))
                {
                    existingAccount = _authService.FindAccountWithEmailOrUsername(account.Email);
                }

                if (existingAccount == null && !string.IsNullOrEmpty(account.Username))
                {
                    existingAccount = _authService.FindAccountWithUsername(account.Username);
                }

                if (existingAccount != null)
                {
                    return BadRequest(_baseReponseService.CreateErrorResponse("Username or Email already exists."));
                }

                var newAccount = new Account
                {
                    Username = account.Username,
                    Email = account.Email,
                    PasswordHash =HashPassword( account.Password) // Lưu ý: bạn nên hash password trước khi lưu
                };

                _authService.SignUp(newAccount);

                int strengthScore = CalculateStrengthScore(account.Password);
                string strengthDescription = GetStrengthDescription(strengthScore);

                return Ok(_baseReponseService.CreateSuccessResponse(new
                {
                    Account = newAccount,
                    StrengthScore = strengthScore,
                    StrengthDescription = strengthDescription
                }, "Account created successfully."));
            }
            catch (Exception ex)
            {
                // Log exception để debug
                // _logger.LogError(ex, "Error in SignUp method");
                return StatusCode(500, _baseReponseService.CreateErrorResponse("Internal server error occurred."));
            }
        }
        /////////  ---------- Hash Password ----------  /////////
        private string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 12);
            return hashedPassword;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        /////////  ---------- CalculateStrengthScore ----------  /////////
        private int CalculateStrengthScore(string password)
        {
            if (string.IsNullOrEmpty(password)) return 0;

            int score = 0;
            if (password.Length >= 8) score += 1;
            if (password.Length >= 12) score += 1;
            if (password.Any(char.IsUpper)) score += 1;
            if (password.Any(char.IsLower)) score += 1;
            if (password.Any(char.IsDigit)) score += 1;
            if (password.Any(c => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c))) score += 1;
            if (password.Length >= 16) score += 1;
            return score;
        }

        private string GetStrengthDescription(int score)
        {
            return score switch
            {
                <= 2 => "Rất yếu",
                <= 4 => "Yếu",
                <= 6 => "Trung bình",
                <= 7 => "Mạnh",
                _ => "Rất mạnh"
            };
        }

        ////////  ---------- ValidatePassword ----------  /////////
        private void CheckPassword(string password, string username, string email, PasswordValidationResult result)
        {
            if (string.IsNullOrEmpty(password))
            {
                result.Errors.Add("Mật khẩu không được để trống");
                return;
            }

            if (password.Length < 8)
                result.Errors.Add($"Mật khẩu phải có ít nhất 8 ký tự");

            if (password.Length > 128)
                result.Errors.Add($"Mật khẩu không được vượt quá 128 ký tự");

            if (!password.Any(char.IsUpper))
                result.Errors.Add("Mật khẩu phải chứa ít nhất 1 chữ cái viết hoa");

            if (!password.Any(char.IsLower))
                result.Errors.Add("Mật khẩu phải chứa ít nhất 1 chữ cái viết thường");

            if (!password.Any(char.IsDigit))
                result.Errors.Add("Mật khẩu phải chứa ít nhất 1 chữ số");

            var specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            if (!password.Any(c => specialChars.Contains(c)))
                result.Errors.Add("Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt (!@#$%^&*()_+-=[]{}|;:,.<>?)");

            if (password.Any(char.IsWhiteSpace))
                result.Errors.Add("Mật khẩu không được chứa khoảng trắng");

            // Kiểm tra username
            if (!string.IsNullOrEmpty(username) && password.ToLower().Contains(username.ToLower()))
                result.Errors.Add("Mật khẩu không được chứa tên đăng nhập");

            // Kiểm tra email - thêm validation
            if (!string.IsNullOrEmpty(email) && email.Contains('@'))
            {
                try
                {
                    var emailPrefix = email.Split('@')[0];
                    if (!string.IsNullOrEmpty(emailPrefix) && password.ToLower().Contains(emailPrefix.ToLower()))
                        result.Errors.Add("Mật khẩu không được chứa phần tên trong email");
                }
                catch
                {
                    // Ignore nếu email format không đúng
                }
            }
        }
        #endregion

    }
}
