
namespace Varian.Subscriber
{
    using Commons;
    using Contracts.Commands.SubscriberEndpoint;
    using NServiceBus;
    using NServiceBus.Logging;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.DefaultVarianConfiguration();
        }

        
    }
}
