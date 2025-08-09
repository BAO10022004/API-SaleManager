using MimeKit;
using SaleManagerWebAPI.Interface.IResponsitories;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace SaleManagerWebAPI.Services
{
    public class EmailServices : IEmailResponsitoris
    {
        public async Task SendVerificationCodeAsync(string toEmail, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App", "giabaoonutc2@gmail.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Mã xác thực";

            message.Body = new TextPart("html")
            {
                Text = CreateVerificationEmailTemplate(code)
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("giabaoonutc2@gmail.com", "zaxhlxwulmedygre");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        #region CreateVerificationEmailTemplate
        public static string CreateVerificationEmailTemplate(string verificationCode)
        {
                return $@"
                <!DOCTYPE html>
                <html lang='vi'>
                <head> 
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Mã xác thực</title>
                <style>
                    @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600;700&display=swap');
            
                    @keyframes slideInFromTop {{
                        0% {{ transform: translateY(-50px); opacity: 0; }}
                        100% {{ transform: translateY(0); opacity: 1; }}
                    }}
            
                    @keyframes bounceIn {{
                        0% {{ transform: scale(0.3); opacity: 0; }}
                        50% {{ transform: scale(1.05); }}
                        70% {{ transform: scale(0.9); }}
                        100% {{ transform: scale(1); opacity: 1; }}
                    }}
            
                    @keyframes pulse {{
                        0% {{ transform: scale(1); }}
                        50% {{ transform: scale(1.05); }}
                        100% {{ transform: scale(1); }}
                    }}
            
                    @keyframes shimmer {{
                        0% {{ background-position: -200% center; }}
                        100% {{ background-position: 200% center; }}
                    }}
            
                    @keyframes glow {{
                        0% {{ box-shadow: 0 0 20px rgba(102, 126, 234, 0.3); }}
                        50% {{ box-shadow: 0 0 30px rgba(102, 126, 234, 0.6), 0 0 40px rgba(118, 75, 162, 0.4); }}
                        100% {{ box-shadow: 0 0 20px rgba(102, 126, 234, 0.3); }}
                    }}
            
                    .animated-header {{
                        animation: slideInFromTop 0.8s ease-out;
                    }}
            
                    .code-container {{
                        animation: bounceIn 1.2s ease-out 0.3s both;
                    }}
            
                    .code-display {{
                        animation: glow 2s ease-in-out infinite;
                        background: linear-gradient(
                            45deg,
                            #ffffff,
                            #f8f9ff,
                            #ffffff,
                            #f0f4ff,
                            #ffffff
                        );
                        background-size: 200% 200%;
                        animation: glow 2s ease-in-out infinite, shimmer 3s ease-in-out infinite;
                    }}
            
                    .warning-box {{
                        animation: pulse 2s ease-in-out infinite;
                        background: linear-gradient(135deg, #fff3cd 0%, #ffeaa7 50%, #fff3cd 100%);
                        background-size: 200% 200%;
                        animation: pulse 2s ease-in-out infinite, shimmer 4s ease-in-out infinite;
                    }}
            
                    .gradient-bg {{
                        background: linear-gradient(
                            135deg,
                            #667eea 0%,
                            #764ba2 25%,
                            #f093fb 50%,
                            #f5576c 75%,
                            #4facfe 100%
                        );
                        background-size: 300% 300%;
                        animation: shimmer 6s ease-in-out infinite;
                    }}
            
                    .floating-icon {{
                        animation: pulse 2s ease-in-out infinite alternate;
                    }}
            
                    .fade-in {{
                        animation: slideInFromTop 1s ease-out 0.6s both;
                    }}
            
                    /* Responsive cho mobile */
                    @media only screen and (max-width: 600px) {{
                        .main-table {{ width: 100% !important; }}
                        .code-text {{ font-size: 28px !important; }}
                        .main-padding {{ padding: 20px 15px !important; }}
                    }}
                </style>
                </head>
                <body style='margin: 0; padding: 0; background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%); font-family: \""Poppins\"", Arial, sans-serif;'>
                <!-- Background với hiệu ứng -->
                    <div style='background: radial-gradient(circle at 30% 20%, rgba(102, 126, 234, 0.1) 0%, transparent 50%),
                                radial-gradient(circle at 70% 80%, rgba(245, 87, 108, 0.1) 0%, transparent 50%),
                                linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
                                min-height: 100vh; padding: 20px 0;'>
            
                        <!-- Main Email Container -->
                        <table class='main-table' style='width: 100%; max-width: 600px; margin: 0 auto; background-color: white; 
                                                      border-radius: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.1); overflow: hidden;'>
                            <tr>
                                <td class='main-padding' style='padding: 50px 40px;'>
                        
                                    <!-- Animated Header -->
                                    <div class='animated-header' style='text-align: center; margin-bottom: 40px;'>
                                        <div class='floating-icon' style='font-size: 48px; margin-bottom: 20px;'>🔐</div>
                                        <h1 style='color: #333; margin: 0; font-size: 28px; font-weight: 700; 
                                                  background: linear-gradient(135deg, #667eea, #764ba2);
                                                  -webkit-background-clip: text; -webkit-text-fill-color: transparent;
                                                  background-clip: text; text-shadow: 2px 2px 4px rgba(0,0,0,0.1);'>
                                            Mã Xác Thực Của Bạn
                                        </h1>
                                        <div style='width: 60px; height: 4px; background: linear-gradient(90deg, #667eea, #764ba2); 
                                                    margin: 15px auto; border-radius: 2px;'></div>
                                    </div>
                
                                    <!-- Main Code Container -->
                                    <div class='code-container gradient-bg' 
                                         style='padding: 40px 30px; border-radius: 20px; text-align: center; margin: 30px 0;
                                                box-shadow: 0 15px 35px rgba(102, 126, 234, 0.3);'>
                            
                                        <p style='color: white; font-size: 18px; margin: 0 0 25px 0; font-weight: 400;
                                                 text-shadow: 1px 1px 2px rgba(0,0,0,0.2);'>
                                            ✨ Mã xác thực của bạn là:
                                        </p>
                            
                                        <!-- Code Display with Advanced Effects -->
                                        <div class='code-display' 
                                             style='background-color: white; padding: 25px 20px; border-radius: 15px; 
                                                    display: inline-block; margin: 10px 0; position: relative; overflow: hidden;
                                                    box-shadow: 0 10px 25px rgba(0,0,0,0.15);'>
                                
                                            <!-- Decorative corners -->
                                            <div style='position: absolute; top: 5px; left: 5px; width: 20px; height: 20px;
                                                       border-top: 3px solid #667eea; border-left: 3px solid #667eea; border-radius: 3px 0 0 0;'></div>
                                            <div style='position: absolute; top: 5px; right: 5px; width: 20px; height: 20px;
                                                       border-top: 3px solid #764ba2; border-right: 3px solid #764ba2; border-radius: 0 3px 0 0;'></div>
                                            <div style='position: absolute; bottom: 5px; left: 5px; width: 20px; height: 20px;
                                                       border-bottom: 3px solid #667eea; border-left: 3px solid #667eea; border-radius: 0 0 0 3px;'></div>
                                            <div style='position: absolute; bottom: 5px; right: 5px; width: 20px; height: 20px;
                                                       border-bottom: 3px solid #764ba2; border-right: 3px solid #764ba2; border-radius: 0 0 3px 0;'></div>
                                
                                            <!-- Main Code -->
                                            <span class='code-text' style='font-size: 36px; font-weight: 700; color: #333; 
                                                       letter-spacing: 4px; font-family: \""Courier New\"", monospace;
                                                       background: linear-gradient(45deg, #667eea, #764ba2, #f093fb);
                                                       -webkit-background-clip: text; -webkit-text-fill-color: transparent;
                                                       background-clip: text; text-shadow: 2px 2px 4px rgba(0,0,0,0.1);'>
                                                {verificationCode}
                                            </span>
                                        </div>
                            
                                        <!-- Additional decorative elements -->
                                        <div style='margin-top: 20px;'>
                                            <span style='color: rgba(255,255,255,0.8); font-size: 14px; font-weight: 300;'>
                                                🛡️ Được bảo mật bởi mã hóa AES-256
                                            </span>
                                        </div>
                                    </div>
                
                                    <!-- Warning Box with Enhanced Effects -->
                                    <div class='warning-box fade-in' 
                                         style='border: 2px solid #ffeaa7; border-radius: 15px; padding: 25px; margin: 30px 0;
                                                position: relative; overflow: hidden;
                                                box-shadow: 0 8px 20px rgba(255, 234, 167, 0.3);'>
                            
                                        <!-- Animated border -->
                                        <div style='position: absolute; top: -2px; left: -2px; right: -2px; bottom: -2px;
                                                   background: linear-gradient(45deg, #ffeaa7, #f093fb, #ffeaa7, #667eea, #ffeaa7);
                                                   background-size: 300% 300%; border-radius: 15px; z-index: -1;
                                                   animation: shimmer 4s ease-in-out infinite;'></div>
                            
                                        <div style='text-align: center;'>
                                            <div style='font-size: 32px; margin-bottom: 15px; animation: pulse 2s ease-in-out infinite;'>⏱️</div>
                                            <p style='margin: 0; color: #856404; font-weight: 600; font-size: 16px;'>
                                                <strong style='color: #d63384;'>⚠️ QUAN TRỌNG:</strong> Mã này sẽ hết hạn sau 
                                                <span style='background: linear-gradient(45deg, #d63384, #f093fb); -webkit-background-clip: text; 
                                                           -webkit-text-fill-color: transparent; background-clip: text; font-weight: 700; font-size: 18px;'>
                                                    60 giây
                                                </span>
                                            </p>
                                            <div style='margin-top: 10px; font-size: 12px; color: #856404; font-style: italic;'>
                                                Vui lòng sử dụng ngay để tránh hết hạn 🚀
                                            </div>
                                        </div>
                                    </div>
                
                                   
                                    <!-- Footer -->
                                    <div class='fade-in' style='text-align: center; margin-top: 40px; padding-top: 30px; 
                                                               border-top: 2px solid #f0f0f0;'>
                                        <p style='color: #888; font-size: 13px; margin: 0; line-height: 1.5;'>
                                            <strong>💡 Lời khuyên:</strong> Luôn kiểm tra URL trang web trước khi nhập mã<br>
                                            <span style='font-size: 11px; color: #bbb;'>
                                                Email được gửi tự động • Không trả lời email này
                                            </span>
                                        </p>
                            
                                        <!-- Social proof icons -->
                                        <div style='margin-top: 20px;'>
                                            <span style='font-size: 20px; margin: 0 5px; opacity: 0.6;'>🛡️</span>
                                            <span style='font-size: 20px; margin: 0 5px; opacity: 0.6;'>🔐</span>
                                            <span style='font-size: 20px; margin: 0 5px; opacity: 0.6;'>✅</span>
                                        </div>
                                    </div>
                        
                                </td>
                            </tr>
                        </table>
            
                        <!-- Additional background decoration -->
                        <div style='position: fixed; top: 0; left: 0; width: 100%; height: 100%; pointer-events: none; z-index: -1;'>
                            <div style='position: absolute; top: 10%; left: 10%; width: 100px; height: 100px; 
                                       background: radial-gradient(circle, rgba(102, 126, 234, 0.1) 0%, transparent 70%);
                                       border-radius: 50%; animation: pulse 4s ease-in-out infinite;'></div>
                            <div style='position: absolute; bottom: 20%; right: 15%; width: 80px; height: 80px; 
                                       background: radial-gradient(circle, rgba(245, 87, 108, 0.1) 0%, transparent 70%);
                                       border-radius: 50%; animation: pulse 3s ease-in-out infinite reverse;'></div>
                        </div>
                    </div>
                </body>
                </html>
                ";
        }
        #endregion
    }
}
