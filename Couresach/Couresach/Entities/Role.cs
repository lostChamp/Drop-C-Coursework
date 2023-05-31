using System.Collections.Generic;

namespace Couresach;

public class Role
{
    public int Id { get; set; }
    public string? Value { get; set; }
    public double? Salary { get; set; }
    public List<User>? UsersEntity { get; set; }
}