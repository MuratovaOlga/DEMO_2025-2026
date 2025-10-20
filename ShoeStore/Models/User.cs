using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Fiouser { get; set; } = null!;

    public string LoginUser { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public int? IdRole { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

}
