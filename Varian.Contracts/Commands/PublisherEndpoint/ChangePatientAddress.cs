using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Contracts.Commands.PublisherEndpoint
{
    public class ChangePatientAddress
    {
        public int PatientId { get; set; }

        public string NewAddress { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(NewAddress))
                throw new InvalidOperationException();
            if(PatientId < 1) 
                throw new InvalidOperationException();
        }
    }
}
