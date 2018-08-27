
namespace Varian.SyncEndpoint
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
