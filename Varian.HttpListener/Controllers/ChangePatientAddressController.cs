using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Varian.HttpListener.Controllers
{
    using System.Web.Http;
    using Contracts.Commands.PublisherEndpoint;
    using NServiceBus;
    using System.Threading.Tasks;

    public class ChangePatientAddressController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody] ChangePatientAddress changeAddressCommand)
        {
            //changeAddressCommand.Validate();
            await BusRegistration.EndpointInstance.Send(changeAddressCommand, new SendOptions());
            return Ok();
        }
    }
}