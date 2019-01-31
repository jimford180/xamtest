namespace FBCross.Rest.Dto
{
    public class WaitListStartLookingResponse : ResponseBase
    {
        public int NumberOfPartiesWaiting { get; set; }
        public int? NumberOfSlotsForFirstParty { get; set; }
    }
}
