
namespace Varian.Publisher
{
    using NServiceBus;
    using Varian.Commons;
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.DefaultVarianConfiguration();
        }
    }
}
