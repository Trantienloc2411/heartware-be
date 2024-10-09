using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? UnitsInStock { get; set; }

    public int? CategoryId { get; set; }

    public int? ProductStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
