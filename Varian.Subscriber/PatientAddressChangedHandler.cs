using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Subscriber
{
    using Contracts.Events.PublisherEndpoint;
    using NServiceBus;
    using NServiceBus.Logging;

    public class PatientAddressChangedHandler : IHandleMessages<IChangedAPatientAddress>
    {
        private static ILog Log = LogManager.GetLogger(typeof(PatientAddressChangedHandler));

        public async Task Handle(IChangedAPatientAddress message, IMessageHandlerContext context)
        {
            Log.Info($"Handling the patient address changed event; we are in subscriber endpoint.");
            await Task.CompletedTask;
        }
    }
}
