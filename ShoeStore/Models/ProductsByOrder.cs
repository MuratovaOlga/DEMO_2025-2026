using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class ProductsByOrder
{
    public int IdProductsByOrder { get; set; }

    public int? IdProduct { get; set; }

    public int? IdOrder { get; set; }

    public virtual OrdersProduct? IdOrderNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
