using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.SyncEndpoint
{
    using NServiceBus;
    public class PatientSatisfactionSagaState : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public bool PatientInitiatedReview { get; set; }

        public long AppointmentId { get; set; }

    }
}
