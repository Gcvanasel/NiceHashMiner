using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;
using Newtonsoft.Json;

using NiceHashMiner.Miners;
using NiceHashMiner.Enums;
using NiceHashMiner.Web.Models;

namespace NiceHashMiner.Web
{
    [RestResource]
    class APIListener
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/status")]
        public IHttpContext RepeatMe(IHttpContext context) {
            var devID = context.Request.QueryString["algorithm"] ?? "-1";
            var tmp = 0;
            int.TryParse(devID, out tmp);
            var ID = (AlgorithmType)tmp;

            var status = MinersManager.GetGroupMinerStatuses();
            if (ID != AlgorithmType.NONE) {
                status = status.FindAll((s) => s.APIData.AlgorithmID == ID || s.APIData.SecondaryAlgorithmID == ID);
            }

            var apiStatus = new List<APIGroupMinerStatus>();
            foreach (var stat in status) {
                apiStatus.Add(new APIGroupMinerStatus(stat.APIData, stat.CurrentRate));
            }

            var ret = JsonConvert.SerializeObject(apiStatus);

            context.Response.SendResponse(ret);
            return context;
        }
        [RestRoute]
        public IHttpContext HelloWorld(IHttpContext context) {
            context.Response.SendResponse("Hello, world");
            return context;
        }
    }
}
