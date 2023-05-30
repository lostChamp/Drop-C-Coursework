using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Couresach;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Full_name { get; set; }
    public string Phone_number { get; set; }
    public DateOnly Date_reg { get; set; }
    
    public int Role_id { get; set; }
    
    [ForeignKey("Role_id")]
    public Role? RolesEntity { get; set; }
}