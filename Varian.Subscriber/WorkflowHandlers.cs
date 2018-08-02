using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.Subscriber
{
    using System.Data.SqlTypes;
    using Contracts.Commands.SubscriberEndpoint;
    using Contracts.Commands.Workflows;
    using NServiceBus;


    public interface IBuildNextWorkflowStepCommands
    {
        bool CanBuild(Type nextStepCommandType);

        bool Build(Type nextStepCommandType, int patientId);
    }

    public class CollectReferralBuilder : IBuildNextWorkflowStepCommands
    {
        public bool CanBuild(Type nextStepCommandType)
        {
            return nextStepCommandType == typeof(CollectReferral);
        }

        public bool Build(Type nextStepCommandType, int patientId)
        {
            throw new NotImplementedException();
        }
    }

    public class ScheduleConsultBuilder : IBuildNextWorkflowStepCommands
    {
        public bool CanBuild(Type nextStepCommandType)
        {
            return nextStepCommandType == typeof(ScheduleConsultBuilder);
        }

        public bool Build(Type nextStepCommandType, int patientId)
        {
            throw new NotImplementedException();
        }
    }


    public class WorkflowHandlers : IHandleMessages<CreatePatient>
        , IHandleMessages<CollectReferral>
        , IHandleMessages<ScheduleConsult>
    {
        private readonly IEnumerable<IBuildNextWorkflowStepCommands> _builders;
        public WorkflowHandlers(IEnumerable<IBuildNextWorkflowStepCommands> builders)
        {
            _builders = builders;
        }
        public async Task Handle(CreatePatient message, IMessageHandlerContext context)
        {
            //TODO: handle it.
            await Next(message, context);
        }

        private async Task Next(Object message, IMessageHandlerContext context)
        {
            //dynamically determine which step comes next

            /*
             * EX 1:
             * NEXTSTEPS - CollectReferral, ScheduleConsult
             * 
             * EX 2:
             * NEXTSTEPS - ScheduleConsult, CollectReferral
             * 
             * */

            var nextSteps = context.MessageHeaders["NextSteps"]
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            var nextStep = nextSteps.FirstOrDefault();
            var patientId = Int32.Parse(context.MessageHeaders["PatientId"]);
            if (null != nextStep)
            {
                nextSteps.Remove(nextStep);
                var sendOptions = new SendOptions();
                sendOptions.SetHeader("NextSteps", String.Join(",", nextSteps));
                //TODO: build whatever the next message actually looks like
                var nextMessage = _builders.First(x => x.CanBuild(Type.GetType(nextStep)))
                    .Build(Type.GetType(nextStep), patientId);
                await context.Send(nextMessage,sendOptions);
            }
                

            
        }

        public async Task Handle(CollectReferral message, IMessageHandlerContext context)
        {
            //TODO: handle it.
            await Next(message, context);
        }

        public async Task Handle(ScheduleConsult message, IMessageHandlerContext context)
        {
            //TODO: handle it.
            await Next(message, context);
        }
    }
}
