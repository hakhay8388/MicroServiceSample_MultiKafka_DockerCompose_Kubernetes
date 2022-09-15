using MicroServiceSample.nWebGraph;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceSample.nWebGraph
{
    [Route("[controller]")]
    public class WebApiController : cBaseController
    {
        
        public WebApiController(cEventGraph _EventGraph)
            :base(_EventGraph)
        {
        }

        [HttpPost("[action]")]
        public IActionResult WebApi()
        {
            Stream ___Request = Request.Body;
            if (___Request.CanSeek)
            {
                ___Request.Seek(0, System.IO.SeekOrigin.Begin);
            }
            string __JSON = new StreamReader(___Request).ReadToEnd();

            Events = JObject.Parse(__JSON);

            try
            {
                EventGraph.Interpret(this);
            }
            catch (Exception _Ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
                //Exception Handling
            }    

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
