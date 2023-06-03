using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Couresach;

public class DatabaseControl
{
    
    //User
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

    //Role
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

    //Order
    public static void CreateOrder(Order order)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Order?.Add(order);
            ctx.SaveChanges();
        }
    }

    public static List<Order> GetAllOrders()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Order?.Include(p => p.UsersEntity).ToList();
        }
    }

    public static List<Order> GetAllOrdersByUserId(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Order?.Where(p => p.User_id == id).ToList();
        }
    }

    //Service
    public static List<Service> GetAllServices()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Service?.ToList();
        }
    }

    //Ware
    public static List<Ware> GetAllForProductsWare()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Where(p => p.Quantity>0).ToList();
        }
    }
    public static List<Ware> GetAllWare()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Include(p => p.CategoryEntity).ToList();
        }
    }
    
    public static void CreateNewWareItem(Ware item)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Ware?.Add(item);
            ctx.SaveChanges();
        }
    }
    
    public static List<Ware> GetWareByItem(string item)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Include(p => p.CategoryEntity).Where(p => EF.Functions.Like(p.Item, $"%{item}%")).ToList();
        }
    }
    
    public static List<Ware> GetUniqueValues()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Distinct().ToList();
        }
    }
    
    public static void DeleteWareItem(Ware item)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Ware?.Remove(item);
            ctx.SaveChanges();
        }
    }

    //Category
    public static Category GetCategoryByName(string name)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Category?.Where(p => p.Name == name).FirstOrDefault();
        }
    }
    
    public static List<Category> GetAllCategories()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Category?.ToList();
        }
    }
    
    //Manufacturer
    public static Manufacturer GetManByName(string name)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Manufacturer?.Where(p => p.Name == name).FirstOrDefault();
        }
    }
    
    public static List<Manufacturer> GetAllMans()
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Manufacturer?.ToList();
        }
    }
    
    //Order
    public static List<Order> GetOrderByUserName(string full_name)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Order?.Include(p => p.UsersEntity).Where(p => EF.Functions.Like(p.UsersEntity.Full_name, $"%{full_name}%")).ToList();
        }
    }

    public static void DeleteOrderItem(Order item)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Order?.Remove(item);
            ctx.SaveChanges();
        }
    }
}