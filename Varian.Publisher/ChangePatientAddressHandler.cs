using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Publisher
{
    using Contracts.Commands.PublisherEndpoint;
    using Contracts.Events.PublisherEndpoint;
    using NServiceBus;
    using NServiceBus.Logging;

    public class ChangePatientAddressHandler : IHandleMessages<ChangePatientAddress>
    {
        private static ILog Log = LogManager.GetLogger(typeof(ChangePatientAddressHandler));

        public async Task Handle(ChangePatientAddress message, IMessageHandlerContext context)
        {
            message.Validate();
            
            Log.Info(
                $"got a change patient address command for patient {message.PatientId}; new address {message.NewAddress}");
            //TODO: call some web service (or database... or something)

            Log.Info($"raising event to say that address was changed.");

            await context.Publish<IChangedAPatientAddress>(x =>
            {
                x.NewAddress = message.NewAddress;
                x.PatientId = message.PatientId;
                x.OldAddress = "DUDE; I dont know.";
            });
            await Task.CompletedTask;
        }
    }
}
