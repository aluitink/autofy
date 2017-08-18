using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofy.Public.Api.Interfaces
{
    public interface IWebAdapter
    {
        int PerformRequest(string url);
    }
}
