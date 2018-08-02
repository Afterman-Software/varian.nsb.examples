
namespace Varian.Webhooks
{
    using Commons;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.DefaultVarianConfiguration();
        }
    }
}
