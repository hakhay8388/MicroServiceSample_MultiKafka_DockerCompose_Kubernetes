using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace MicroServiceSample.nWebGraph
{
    public abstract class cBaseController : Controller
    {
        protected cEventGraph EventGraph { get; set; }
        public JObject Events { get; set; }
        public JObject Result { get; set; }

        public cBaseController(cEventGraph _EventGraph)
        {
            EventGraph = _EventGraph;
        }
    }
}
