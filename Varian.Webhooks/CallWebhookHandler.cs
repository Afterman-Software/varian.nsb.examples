using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Webhooks
{
    using Contracts.Commands.WebhooksEndpoint;
    using NServiceBus;
    public class CallWebhookHandler : IHandleMessages<CallWebhook>
    {
        public async Task Handle(CallWebhook message, IMessageHandlerContext context)
        {
            //call their web service (POST, whatever).
            await Task.CompletedTask;
        }
    }
}
