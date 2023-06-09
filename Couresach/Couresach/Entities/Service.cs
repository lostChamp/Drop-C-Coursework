﻿using System.Collections.Generic;

namespace Couresach;

public class Service
{
    public int Id { get; set; }
    public string? Name {get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public List<Order>? OrderEntity { get; set; }
}