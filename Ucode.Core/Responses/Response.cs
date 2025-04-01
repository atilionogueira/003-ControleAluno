using System.Text.Json.Serialization;

namespace Ucode.Core.Responses
{
    public class Response<TData>
    {
       

        private readonly int _code;

        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCode;
        
        public Response(TData? data, int code = 200, string? message = null)
        {
            Data = data;
            _code = Configuration.DefaultStatusCode;
            Message = message;
        }
        public TData? Data { get; set; }
        public string? Message { get; set; }

        public bool isSucess => _code is >= 200 and <= 299;


    }
}
