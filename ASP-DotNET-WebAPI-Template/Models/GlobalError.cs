using Newtonsoft.Json;

namespace ASP_DotNET_WebAPI_Template.Models
{
    public class GlobalError
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}