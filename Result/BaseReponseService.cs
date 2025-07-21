using SaleManagerWebAPI.Interface.Result;
using SaleManagerWebAPI.IResponsitories;
using SaleManagerWebAPI.Models.MyResponse;

namespace SaleManagerWebAPI.Result
{
    public class BaseReponseService : IBaseReponseService
    {
        public MyResponse CreateErrorResponse(string message, List<string> errors = null)
        {
            return new MyResponse(message, errors);
        }

        public MyResponse CreateSuccessResponse(object data, string message = "")
        {
            return new MyResponse(data, message);
        }
    }
}
