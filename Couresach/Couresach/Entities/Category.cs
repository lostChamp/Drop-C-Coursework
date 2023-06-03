using System.Collections.Generic;

namespace Couresach;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Ware>? WareEntity { get; set; }
}