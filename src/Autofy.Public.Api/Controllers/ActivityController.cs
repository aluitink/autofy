using Autofy.Public.Api.DataProvider;
using Autofy.Public.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autofy.Public.Api.Controllers
{
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private IDataProvider _dataProvider;
        private IWebAdapter _webAdapter;
        public ActivityController(IDataProvider dataProvider, IWebAdapter webAdapter)
        {
            _dataProvider = dataProvider;
            _webAdapter = webAdapter;
        }

        [HttpPost]
        public string Post([FromBody]Activity value)
        {
            return _dataProvider.CreateActivity(value);
        }

        [HttpGet("{id}")]
        public bool Get(string id)
        {
            var activity = _dataProvider.RetrieveActivity(id);

            var parts = activity.WebHookUrl.Split(new string[] { "https://maker.ifttt.com/use/" }, StringSplitOptions.RemoveEmptyEntries);
            string iftttId = parts[0];

            bool result = true;
            foreach (var action in activity.Actions)
            {
                var requestUrl = string.Format("https://maker.ifttt.com/trigger/{0}/with/key/{1}", action, iftttId);
                result &= _webAdapter.PerformRequest(requestUrl) == 200;
            }

            return result;
        }
        
    }
}
