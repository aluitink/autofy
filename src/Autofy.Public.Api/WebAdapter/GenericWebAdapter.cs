using Autofy.Public.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autofy.Public.Api.WebAdapter
{
    public class GenericWebAdapter : IWebAdapter
    {
        private HttpClient _httpClient = new HttpClient();
        
        public int PerformRequest(string url)
        {
            return (int)_httpClient.GetAsync(url).Result.StatusCode;
        }
    }
}
