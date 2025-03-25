﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

        public static bool ValidateUser(string email, string password)
        {
            try
            {
                var user = Orm.db.admin
                    .Where(a => a.name == email && a.password == password)
                    .FirstOrDefault();

                return user != null;
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

        public static List<admin> SelectAllAdmins()
        {
            try
            {
                return Orm.db.admin.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<admin>();
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
    }
}