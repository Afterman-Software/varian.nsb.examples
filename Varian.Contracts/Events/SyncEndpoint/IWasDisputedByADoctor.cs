using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Contracts.Events.SyncEndpoint
{
    public interface IWasDisputedByADoctor
    {
        long AppointmentId { get; set; }
    }
}
