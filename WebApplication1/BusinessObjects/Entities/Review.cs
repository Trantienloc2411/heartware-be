using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Rating { get; set; }

    public string? Content { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Product? Product { get; set; }
}
