using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class PickUpPpoint
{
    public int IdPickUpPpoint { get; set; }

    public string? AddressPickUpPpoint { get; set; }

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
