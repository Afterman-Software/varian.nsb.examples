using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.SyncEndpoint
{
    using NServiceBus;

    public class SyncPatientsSagaState : IContainSagaData
    {
        public virtual string Country { get; set; }
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }
}
