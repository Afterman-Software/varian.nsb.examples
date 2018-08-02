using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Contracts.Events.PublisherEndpoint
{
    public interface IChangedAPatientAddress
    {
        int PatientId { get; set; }

        string OldAddress { get; set; }

        string NewAddress { get; set; }
    }
}
