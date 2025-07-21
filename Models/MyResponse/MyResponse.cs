namespace SaleManagerWebAPI.Models.MyResponse
{
    public class MyResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
        public MyResponse(object data, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
        }
        public MyResponse(string message, List<string> errors = null)
        {
            Success = false;
            Message = message;
            Data = default;
            Errors = errors ?? new List<string>();
        }
    }
}
