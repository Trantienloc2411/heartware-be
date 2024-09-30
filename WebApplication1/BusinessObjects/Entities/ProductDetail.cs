using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public string? ProductParam { get; set; }

    public string? ProductValue { get; set; }

    public Guid? ProductId { get; set; }
}
