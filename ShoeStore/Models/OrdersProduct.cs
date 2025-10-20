using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class OrdersProduct
{
    public int IdOrder { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int CodeToReceive { get; set; }

    public int? IdPickUpPpoint { get; set; }

    public int? IdUser { get; set; }

    public int? IdStatus { get; set; }

    public virtual PickUpPpoint? IdPickUpPpointNavigation { get; set; }

    public virtual StatusOrder? IdStatusNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ProductsByOrder> ProductsByOrders { get; set; } = new List<ProductsByOrder>();
}
