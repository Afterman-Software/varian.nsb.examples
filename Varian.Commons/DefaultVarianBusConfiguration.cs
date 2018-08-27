using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Commons
{
    using Contracts.Commands.SubscriberEndpoint;
    using NServiceBus;

    public static class DefaultVarianBusConfiguration
    {
        public static void DefaultVarianConfiguration(this EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseDirectRoutingTopology();
            var routing = transport.Routing();
            SetupRouting(routing);
            endpointConfiguration.PurgeOnStartup(System.Diagnostics.Debugger.IsAttached);
            endpointConfiguration.Conventions()
                .DefiningCommandsAs(x => x.Namespace != null && x.Namespace.Contains("Contracts.Commands"))
                .DefiningEventsAs(x => x.Namespace != null && x.Namespace.Contains("Contracts.Events"))
                .DefiningMessagesAs(x => x.Namespace != null && x.Namespace.Contains("Contracts.Messages"));


        }

        

        private static void SetupRouting(RoutingSettings<RabbitMQTransport> routing)
        {
            routing.RouteToEndpoint(typeof(PostRequest).Assembly
                , "Varian.Contracts.Commands.SubscriberEndpoint"
                , "Varian.Subscriber");
            routing.RouteToEndpoint(typeof(PostRequest).Assembly
                , "Varian.Contracts.Commands.PublisherEndpoint"
                , "Varian.Publisher");
        }
    }
}
