﻿using NZWalks.Entities.Base;

namespace NZWalks.Entities
{
    public class Walk : IBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
        public ICollection<WalkCategory> WalkCategories { get; set; }
    }
}
