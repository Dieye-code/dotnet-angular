﻿namespace API.Domain.Entities
{
    public  class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
