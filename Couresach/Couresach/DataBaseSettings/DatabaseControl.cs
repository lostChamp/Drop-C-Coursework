using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Couresach;

public class DatabaseControl
{
    public static void RegistrationUser(User user)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.User?.Add(user);
            ctx.SaveChanges();
        }
    }

    public static User? GetUserByEmail(string email)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            User user = ctx.User?.Where(p => p.Email==email).FirstOrDefault();
            return user;
        }
    }

    public static Role? GetRoleByValue(string value)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            Role role = ctx.Role?.Where(p => p.Value == value).FirstOrDefault();
            return role;
        }
    }

    public static List<Role> GetAllRoles()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Role?.ToList();
        }
    }

    public static Role? GetRoleById(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            Role role = ctx.Role?.Find(id);
            return role;
        }
    }
}