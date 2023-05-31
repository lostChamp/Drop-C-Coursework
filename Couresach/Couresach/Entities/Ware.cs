using System.Collections.Generic;

namespace Couresach;

public class Ware
{
    public int Id { get; set; }
    public string? Item { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int? Quantity { get; set; }
    public string? Category { get; set; }
    public double? Price { get; set; }
}