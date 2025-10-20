using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class StatusOrder
{
    public int IdStatusOrder { get; set; }

    public string StatusOrderName { get; set; } = null!;

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public override string ToString()
    {
        return StatusOrderName;
    }
}
