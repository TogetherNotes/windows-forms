﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TogetherNotes.Utils;

namespace TogetherNotes.Models.Management
{
    public static class AdminOrm
    {
        public static int SelectTotalOfAdmin(int rol)
        {
            try
            {
                return Orm.db.admin
                    .Where(admin => admin.role_id == rol)
                    .Count();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return 0; 
        }

        public static int? ValidateUser(string email, string password)
        {
            try
            {
                var roleAdmin = Orm.db.admin
                    .Where(a => a.name == email && a.password == password)
                    .Select(a => a.role_id)
                    .FirstOrDefault();

                return roleAdmin;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return null;
        }

        public static List<User> SelectAllAdmins()
        {
            try
            {
                var admins = Orm.db.admin
                     .Select(a => new User
                     {
                         Id = a.id,
                         Fullname = a.name,
                         Mail = a.mail,
                         Password = a.password,
                         Role = a.roles.name
                     })
                     .ToList();

                return admins;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<User>();
        }

        public static bool UpdateAdmin(int id, string name, string mail, string password, int roleId)
        {
            try
            {
                var adminToUpdate = Orm.db.admin.FirstOrDefault(a => a.id == id);
                if (adminToUpdate == null)
                {
                    Console.WriteLine("El usuario con el ID proporcionado no existe.");
                    return false;
                }

                adminToUpdate.name = name;
                adminToUpdate.mail = mail;
                adminToUpdate.password = password;
                adminToUpdate.role_id = roleId;

                Orm.db.SaveChanges();  
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return false;
        }

        public static bool InsertAdmin(string name, string mail, string password, int roleId)
        {
            try
            {
                admin newAdmin = new admin
                {
                    name = name,
                    mail = mail,
                    password = password,
                    role_id = roleId
                };

                Orm.db.admin.Add(newAdmin);
                Orm.db.SaveChanges(); 
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return false;
        }

        public static bool DeleteAdmin(int adminId)
        {
            try
            {
                var adminToDelete = Orm.db.admin.FirstOrDefault(a => a.id == adminId);
                if (adminToDelete != null)
                {
                    Orm.db.admin.Remove(adminToDelete);
                    Orm.db.SaveChanges();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return false;
        }

    }
}