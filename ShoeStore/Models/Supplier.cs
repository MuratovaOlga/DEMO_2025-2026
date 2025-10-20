using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Supplier
{
    public int IdSupplier { get; set; }

    public string NameSupplier { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    public override string ToString()
    {
        return NameSupplier;
    }
}
