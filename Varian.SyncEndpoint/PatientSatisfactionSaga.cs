using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varian.SyncEndpoint
{
    using Contracts.Commands.SyncEndpoint;
    using Contracts.Events.SyncEndpoint;
    using Contracts.Messages.SyncEndpoint;
    using NServiceBus;
    /// <summary>
    /// after an appointment, a patient may "rate" their doctor and service
    ///     ICompletedAnAppointment
    /// if they do so, then the doctor may "refute" their rating for up to XX days
    ///     AppointmentDoctorDisputePeriodExpired
    ///     IReviewedAnAppointment
    /// //if the doctor does this, then an investigation starts, which is outside the scope of this workflow
    ///     IWasDisputedByADoctor
    ///     StartInvestigation
    /// //if the patient does not rate their doctor within YY days, then they lose the right to do so.
    ///     AppointmentReviewPeriodExpired
    /// 
    /// </summary>
    public class PatientSatisfactionSaga : Saga<PatientSatisfactionSagaState>
        , IAmStartedByMessages<ICompletedAnAppointment>
        , IHandleMessages<IReviewedAnAppointment>
        , IHandleMessages<IWasDisputedByADoctor>
        , IHandleTimeouts<AppointmentReviewPeriodExpired>
        , IHandleTimeouts<AppointmentDoctorDisputePeriodExpired>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PatientSatisfactionSagaState> mapper)
        {
            mapper.ConfigureMapping<ICompletedAnAppointment>(
                m => m.AppointmentId).ToSaga(s => s.AppointmentId);
            mapper.ConfigureMapping<IReviewedAnAppointment>(m => m.AppointmentId)
                .ToSaga(s => s.AppointmentId);
            mapper.ConfigureMapping<IWasDisputedByADoctor>(m => m.AppointmentId)
                .ToSaga(s => s.AppointmentId);
        }

        public async Task Handle(ICompletedAnAppointment message, IMessageHandlerContext context)
        {
            await base.RequestTimeout<AppointmentReviewPeriodExpired>(context, TimeSpan.FromDays(30));
        }

        public async Task Handle(IReviewedAnAppointment message, IMessageHandlerContext context)
        {
            this.Data.PatientInitiatedReview = true;
            await base.RequestTimeout<AppointmentDoctorDisputePeriodExpired>(context, TimeSpan.FromDays(30));

        }

        public async Task Handle(IWasDisputedByADoctor message, IMessageHandlerContext context)
        {
            await context.Send<StartInvestigation>(x =>
            {

            });
            this.MarkAsComplete();
        }

        public async Task Timeout(AppointmentReviewPeriodExpired state, IMessageHandlerContext context)
        {
            if(!this.Data.PatientInitiatedReview)
                this.MarkAsComplete();
            await Task.CompletedTask;
        }

        public async Task Timeout(AppointmentDoctorDisputePeriodExpired state, IMessageHandlerContext context)
        {
            this.MarkAsComplete();
        }
    }
}
