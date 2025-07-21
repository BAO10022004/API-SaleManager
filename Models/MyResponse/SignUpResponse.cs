using SaleManagerWebAPI.Models.Entities;

namespace SaleManagerWebAPI.Models.MyResponse
{
    public class SignUpResponse
    {
        public Account Data { get; set; }
        public string StrengthScore { get; set; }
        public bool StrengthDescription { get; set; }
    }
}
