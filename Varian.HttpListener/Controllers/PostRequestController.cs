using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Varian.HttpListener.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Contracts.Commands.SubscriberEndpoint;
    using Contracts.Messages.SubscriberEndpoint;
    using NServiceBus;

    
    public class PostRequestController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }

        public async Task<IHttpActionResult> Post()
        {
            var authToken = this.Request.Headers.Authorization.Parameter;
            var sendOptions = new SendOptions();
            sendOptions.SetHeader("authorization", authToken);


            //CASE 2
            var response = await BusRegistration.EndpointInstance
                .Request<PostRequestReply>(new PostRequest(), sendOptions);
            var whatever = response.IsSuccessful;

            //CASE 1: reply to address, non-blocking
            await BusRegistration.EndpointInstance.Send<PostRequest>(x =>
            {
                
            }, sendOptions);

            return Ok();
        }

        
    }


    //CASE 1:
    public class ReplyHandler : IHandleMessages<PostRequestReply>
    {
        public async Task Handle(PostRequestReply message, IMessageHandlerContext context)
        {
            await Task.CompletedTask;
        }
    }

    
}