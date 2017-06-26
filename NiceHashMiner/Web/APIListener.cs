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
        // Current mining statistics
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/status")]
        public IHttpContext Status(IHttpContext context) {
            var algID = GetAlgIDQuery(context);
            var devID = GetDevIDQuery(context);

            var apiStatus = GetCurrentMinerStatus(algID, devID);

            var ret = JsonConvert.SerializeObject(apiStatus);

            context.Response.SendResponse(ret);
            return context;
        }

        // Device info 
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/devices")]
        public IHttpContext Devices(IHttpContext context) {
            var devID = GetDevIDQuery(context);
        }

        //Default
        [RestRoute]
        public IHttpContext HelloWorld(IHttpContext context) {
            context.Response.SendResponse("Hello, world");
            return context;
        }

        private List<APIGroupMinerStatus> GetCurrentMinerStatus(AlgorithmType algorithm=AlgorithmType.NONE, int devID=-1) {
            var status = MinersManager.GetGroupMinerStatuses();
            if (algorithm != AlgorithmType.NONE) {
                status = status.FindAll((s) => s.APIData.AlgorithmID == algorithm || s.APIData.SecondaryAlgorithmID == algorithm);
            }

            var apiStatus = new List<APIGroupMinerStatus>();
            foreach (var stat in status) {
                apiStatus.Add(new APIGroupMinerStatus(stat.APIData, stat.CurrentRate));
            }

            // NHM does not collect individual devices, so return any miner status that includes the device
            if (devID >= 0) {
                apiStatus = apiStatus.FindAll((s) => s.DeviceString.Contains(devID.ToString()));
            }

            return apiStatus;
        }

        #region query_helpers
        private int GetIntFromQuery(string query, IHttpContext context, int defaultValue=-1) {
            var idQuery = context.Request.QueryString[query] ?? defaultValue.ToString();
            var id = defaultValue;
            int.TryParse(idQuery, out id);
            return id;
        }
        private int GetDevIDQuery(IHttpContext context) {
            var devID = GetIntFromQuery("device", context);
            return devID;
        }
        private AlgorithmType GetAlgIDQuery(IHttpContext context) {
            var alg = (AlgorithmType)GetIntFromQuery("algorithm", context);
            if (alg > AlgorithmType.Sia || alg < AlgorithmType.DaggerSia) {
                alg = AlgorithmType.NONE;
            }
            return alg;
        }
        #endregion
    }
}
