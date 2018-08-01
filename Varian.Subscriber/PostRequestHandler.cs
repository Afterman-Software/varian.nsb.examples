using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Subscriber
{
    using Contracts.Commands;
    using Contracts.Commands.SubscriberEndpoint;
    using NServiceBus;
    using NServiceBus.Logging;

    public class PostRequestHandler :
        IHandleMessages<PostRequest>
    {
        private static readonly ILog Log = 
            LogManager.GetLogger(typeof(PostRequestHandler));

        public async Task Handle(PostRequest message, IMessageHandlerContext context)
        {
            Log.Info($"handling message...");
            var authToken = context.MessageHeaders["authorization"];
            //call your wcf service here.
            await Task.CompletedTask;
        }

       
    }
}
