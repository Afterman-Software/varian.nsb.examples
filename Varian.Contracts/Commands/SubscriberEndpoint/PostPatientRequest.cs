namespace Varian.Contracts.Commands.SubscriberEndpoint
{
    public class PostPatientRequest : PostRequest
    {
        public int PatientId { get; set; }
    }
}
