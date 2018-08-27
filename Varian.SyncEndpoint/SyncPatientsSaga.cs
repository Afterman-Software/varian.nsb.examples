using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.SyncEndpoint
{
    using Contracts.Commands.SyncEndpoint;
    using Contracts.Messages.SyncEndpoint;
    using NServiceBus;

    /// <summary>
    /// The purpose?
    /// maintain a running schedule for which to sync patients
    /// </summary>
    public class SyncPatientsSaga :
        Saga<SyncPatientsSagaState>
        , IAmStartedByMessages<StartSyncingPatients>
        , IHandleTimeouts<SyncPatients>

    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SyncPatientsSagaState> mapper)
        {
            mapper.ConfigureMapping<StartSyncingPatients>(x => x.Country)
                .ToSaga(s => s.Country);
        }


        public async Task Handle(StartSyncingPatients message, IMessageHandlerContext context)
        {
            await base.RequestTimeout<SyncPatients>(context, TimeSpan.FromSeconds(30));
            await Task.CompletedTask;
        }

        public async Task Timeout(SyncPatients state, IMessageHandlerContext context)
        {
            //actually do the sync work (query the database for the changes and start sending commands to the real endpoint)
            await base.RequestTimeout<SyncPatients>(context, TimeSpan.FromSeconds(30));

        }
    }
}
