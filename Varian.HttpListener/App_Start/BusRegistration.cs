using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Varian.HttpListener
{
    using NServiceBus;
    using Varian.Commons;

    public static class BusRegistration
    {
        public static IEndpointInstance EndpointInstance { get; set; }
        public static void Startup()
        {
            var endpointConfiguration = new EndpointConfiguration(typeof(BusRegistration).Namespace);
            endpointConfiguration.DefaultVarianConfiguration();
            var runningEndpoint = Endpoint
                .Create(endpointConfiguration)
                .Result
                .Start()
                .Result;
            EndpointInstance = runningEndpoint;
        }
    }
}