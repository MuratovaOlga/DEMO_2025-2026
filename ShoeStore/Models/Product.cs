using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ArticleProduct { get; set; } = null!;

    public string NameProduct { get; set; } = null!;

    public string? UnitOfMeasurementProduct { get; set; }

    public decimal PriceProduct { get; set; }

    public int? CurrentDiscountProduct { get; set; }

    public int? QuantityInStockProduct { get; set; }

    public string? DescriptionProduct { get; set; }

    public string? PhotoProduct { get; set; }

    public int? IdSupplier { get; set; }

    public int? IdManufacturer { get; set; }

    public int? IdProductCategory { get; set; }

    public int? IdWarehouse { get; set; }

    public virtual Manufacturer? IdManufacturerNavigation { get; set; }

    public virtual ProductCategory? IdProductCategoryNavigation { get; set; }
    public virtual Supplier IdSupplierNavigation { get; set; }

    public virtual PickUpPpoint? IdWarehouseNavigation { get; set; }

    public virtual ICollection<ProductsByOrder> ProductsByOrders { get; set; } = new List<ProductsByOrder>();

    public void CleanField()
    {
        IdProductCategoryNavigation.CategoryName = IdProductCategoryNavigation.CategoryName.Replace("\r\n", "");
        IdManufacturerNavigation.Name = IdManufacturerNavigation.Name.Replace("\r\n", "");
        IdSupplierNavigation.NameSupplier = IdSupplierNavigation.NameSupplier.Replace("\r\n", "");
        NameProduct = NameProduct.Replace("\r\n", "");
        UnitOfMeasurementProduct = UnitOfMeasurementProduct.Replace("\r\n", "");
        DescriptionProduct = DescriptionProduct.Replace("\r\n", "");
        if (PhotoProduct == null) PhotoProduct = "C:\\Users\\Home\\Desktop\\VKI NSU\\проекты\\ShoeStore\\ShoeStore\\Images\\picture.png";
        PhotoProduct = PhotoProduct.Replace("\r\n", "");
    }
}
