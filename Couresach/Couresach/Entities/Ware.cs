using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Couresach;

public class Ware
{
    public int Id { get; set; }
    public string? Item { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int? Quantity { get; set; }
    
    public int? Category_id { get; set; }
    [ForeignKey("Category_id")]
    public Category? CategoryEntity { get; set; }
    public double? Price { get; set; }
    
    public int? Manufacturer_id { get; set; }
    [ForeignKey("Manufacturer_id")]
    public Manufacturer? ManufacturerEntity { get; set; }
}