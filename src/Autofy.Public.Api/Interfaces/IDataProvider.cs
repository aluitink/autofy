using Autofy.Public.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofy.Public.Api.Interfaces
{
    public interface IDataProvider
    {
        string CreateActivity(Activity activity);
        Activity RetrieveActivity(string id);
    }
}
