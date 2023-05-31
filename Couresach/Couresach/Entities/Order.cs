using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Couresach;

public class Order
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int User_id { get; set; }
    [ForeignKey("User_id")]
    public User? UsersEntity { get; set; }
}