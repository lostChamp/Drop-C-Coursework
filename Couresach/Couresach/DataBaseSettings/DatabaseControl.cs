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

    public static void EditOrderStatus(int userId, string status)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            Order item = ctx.Order?.Where(p => p.UsersEntity.Id == userId).FirstOrDefault();
            if (item != null)
            {
                item.Status = status;
                ctx.Order?.Update(item);
            }

            ctx.SaveChanges();
        }
    }
    
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

    public static List<Order> GetOrdersByWareItem(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Order?.Where(p => p.WareEntity.Id == id).ToList();
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

    public static Ware GetWareById(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Include(p => p.CategoryEntity).Include(p => p.ManufacturerEntity).Where(p => p.Id == id).FirstOrDefault();
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

    public static void EditWareItem(string itemName, string newItem, string newImage, string newDescr, int newQuantity, int caregotyId, double newPrice, int manId)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            Ware item = ctx.Ware?.Where(p => p.Item == itemName).FirstOrDefault();
            if (item != null)
            {
                item.Item = newItem;
                item.Image = newImage;
                item.Description = newDescr;
                item.Quantity = newQuantity;
                item.Category_id = caregotyId;
                item.Price = newPrice;
                item.Manufacturer_id = manId;
                ctx.Ware?.Update(item);
            }
            ctx.SaveChanges();
        }
    }

    public static void decQuantityOfItemById(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            Ware item = ctx.Ware?.Where(p => p.Id == id).FirstOrDefault();
            if (item != null)
            {
                item.Quantity--;
                ctx.Ware?.Update(item);
            }

            ctx.SaveChanges();
        }
    }

    public static List<Ware> GetAllByManWare(string man)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Where(p => p.ManufacturerEntity.Name == man).ToList();
        }
    }

    public static List<Ware> GetAllByTypeWare(string type)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Where(p => p.CategoryEntity.Name == type).ToList();
        }
    }
    
    public static List<Ware> GetAllByTypeAndManWare(string type, string man)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Ware?.Where(p => p.CategoryEntity.Name == type).Where(p => p.ManufacturerEntity.Name == man).ToList();
        }
    }

    //Category

    public static void CreateNewCategory(Category category)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Category?.Add(category);
            ctx.SaveChanges();
        }
    }
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

    public static void DeleteCategory(Category category)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Category?.Remove(category);
            ctx.SaveChanges();
        }
    }
    
    //Manufacturer

    public static void CreateNewMan(Manufacturer man)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Manufacturer?.Add(man);
            ctx.SaveChanges();
        }
    }
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

    public static Manufacturer GetManById(int id)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            return ctx.Manufacturer?.Where(p => p.Id == id).FirstOrDefault();
        }
    }

    public static void DeleteMan(Manufacturer item)
    {
        using (DbAppContext ctx = new DbAppContext())
        {
            ctx.Manufacturer?.Remove(item);
            ctx.SaveChanges();
        }
    }
}