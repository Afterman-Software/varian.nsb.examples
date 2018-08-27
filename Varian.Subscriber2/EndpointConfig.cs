
namespace Varian.Subscriber2
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
