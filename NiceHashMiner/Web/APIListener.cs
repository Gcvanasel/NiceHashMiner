using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;

using NiceHashMiner.Miners;

namespace NiceHashMiner.Web
{
    [RestResource]
    class APIListener
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/repeat")]
        public IHttpContext RepeatMe(IHttpContext context) {
            var word = context.Request.QueryString["word"] ?? "what?";
            MinersManager.
            context.Response.SendResponse(word);
            return context;
        }
        [RestRoute]
        public IHttpContext HelloWorld(IHttpContext context) {
            context.Response.SendResponse("Hello, world");
            return context;
        }
    }
}
