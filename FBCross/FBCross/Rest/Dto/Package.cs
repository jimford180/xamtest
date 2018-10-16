using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class Package
    {
        public int Id { get; set; }

        public Guid PackageGuid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfUses { get; set; }

        public bool UsesInMinutes { get; set; }

        public decimal Price { get; set; }

        public List<int> ServiceIds { get; set; }

        public int? NumberOfDaysToExpiry { get; set; }
    }
}
