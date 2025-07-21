using Microsoft.AspNetCore.Mvc;
using SaleManagerWebAPI.Models.MyResponse;

namespace SaleManagerWebAPI.Interface.Result
{
    public interface IBaseReponseService
    {
        MyResponse CreateSuccessResponse(object data, string message = "");
        MyResponse CreateErrorResponse(string message, List<string> errors = null);
    }
}
