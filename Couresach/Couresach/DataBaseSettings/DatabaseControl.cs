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

    public static User? EditUser(string email, string password, string full_name, string phone_number, string old_email)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            var user = ctx.User?.Where(p => p.Email==old_email).FirstOrDefault();
            if (user != null)
            {
                user.Email = email;
                user.Password = password;
                user.Full_name = full_name;
                user.Phone_number = phone_number;
                user.Date_reg = user.Date_reg;
                user.Role_id = user.Role_id;
                ctx.User?.Update(user);
            }
            ctx.SaveChanges();
            return user;
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

    public static void DeleteUser(User user)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.User?.Remove(user);
            ctx.SaveChanges();
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