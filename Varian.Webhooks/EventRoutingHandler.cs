using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Webhooks
{
    using Contracts.Commands.WebhooksEndpoint;
    using Contracts.Events;
    using NServiceBus;

    public class EventRoutingHandler : 
        IHandleMessages<IAmAnyEvent>
    {

        //look up who is interested in my event (e.g. ICreatedAPatient)
        //and then send a command to the bus to call their web service 
        //(one command sent for each registered web service listener)
        public async Task Handle(IAmAnyEvent message, IMessageHandlerContext context)
        {

            //TODO: look this up in the database, by event type, which can be found like: 
            //context.MessageHeaders[Headers.EnclosedMessageTypes] <-- contains the type name of the actual event raised (not the base type were listening to)
            var interestedWebServices = new List<object>();
            foreach (var interestedWebService in interestedWebServices)
            {
                await context.Send<CallWebhook>(x =>
                {
                    //x.Uri = "";
                    //x.etc... 
                });
            }
            await Task.CompletedTask;
        }
    }
}


/*
 * EVENTS MENU
 * 
 * INSRUCTIONS
 * For each event you'd like, provide teh following:
 *  1. URL
 *  2. security details
 * 
 * 
 * MENU
 * 1. Patient.Created
 *          EXAMPLE JSON
 *          {
 *              "id" : "1"
 *          }
 * 
 * 2. Patient.AddressChanged
 *          EXAMPLE JSON
 *          
 * 3. Procedure.Scheduled
 * etc etc.
 * 
 * 
 * Additional Options 
 * 
 *  - subscribe to a single event like this:
 *      Procedure.Scheduled
 *      
 *  - subscribe to a family of events like this:
 *      Procedure.*
 *      
 * */

