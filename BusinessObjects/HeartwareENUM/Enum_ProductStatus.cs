using System.ComponentModel;

namespace BusinessObjects.HeartwareENUM;

public enum Enum_ProductStatus
{
    [Description("Product is available")]
    Active = 1,
    [Description("Product is out of stock")]
    OutofStock = 2,
    [Description("Product has been discontinued")]
    PreOrder = 3
    
}