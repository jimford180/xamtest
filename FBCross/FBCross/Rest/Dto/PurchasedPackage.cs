namespace FBCross.Rest.Dto
{
    public class PurchasedPackage
    {
        public int Id { get; set; }
        public int PurchasedId { get; set; }
        public string PackageName { get; set; }
        public int UsesPurchased { get; set; }
        public int UsesRemaining { get; set; }
        public bool UsesInMinutes { get; set; }
        public string CreatedAt { get; set; }
        public string ExpiryDate { get; set; }
        public bool RelocityBillable { get; internal set; }
    }
}