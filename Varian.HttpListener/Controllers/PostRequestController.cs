using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Varian.HttpListener.Controllers
{
    using System.Web.Http;
    using Contracts.Commands.SubscriberEndpoint;
    using NServiceBus;

    public class PostRequestController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }

        public IHttpActionResult Post()
        {
            var authToken = this.Request.Headers.Authorization.Parameter;
            var sendOptions = new SendOptions();
            sendOptions.SetHeader("authorization", authToken);
            BusRegistration.EndpointInstance
                .Send<PostRequest>(x =>
                {
                    x.RequestId = 1;
                }, sendOptions);

            return Ok();
        }
    }
}