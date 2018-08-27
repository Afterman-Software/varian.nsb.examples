using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Subscriber
{
    using Contracts.Commands;
    using Contracts.Commands.SubscriberEndpoint;
    using NServiceBus;
    using NServiceBus.Logging;

    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {

        private static readonly ILog Log =
            LogManager.GetLogger(typeof(Startup));
        public async Task Start(IMessageSession session)
        {
            Log.Info("starting up!");
            
            //session.ScheduleEvery(TimeSpan.FromDays(1) , x =>
            //{
                
            //})
            await Task.CompletedTask;
        }

        public async Task Stop(IMessageSession session)
        {
            Log.Info("stopping...");
            await Task.CompletedTask;
        }
    }
}
