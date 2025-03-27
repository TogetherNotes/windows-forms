using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;
using TogetherNotes.Utils;

namespace TogetherNotes.Models.Management
{
    public static class SpacesOrm
    {
        public static List<User> SelectAllSpaces()
        {
            try
            {
                var spaces = Orm.db.spaces
                    .Select(a => new User
                    {
                        Id = a.app_user_id,
                        Fullname = a.app.name,
                        Mail = a.app.mail,
                        Password = a.app.password,
                        Role = a.app.role,
                        Rating = a.app.rating,
                        Capacity = a.capacity
                    })
                    .ToList();

                return spaces;
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

        public static bool InsertSpace(string name, string mail, string password, int capacity)
        {
            try
            {
                var newUser = new app
                {
                    name = name,
                    mail = mail,
                    password = password,
                    role = "Space",
                    rating = null, // Puede ser nulo
                    active = true
                };

                Orm.db.app.Add(newUser);
                Orm.db.SaveChanges();

                var newSpace = new spaces
                {
                    app_user_id = newUser.id,
                    capacity = capacity
                };

                Orm.db.spaces.Add(newSpace);
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

        public static bool UpdateSpace(int userId, string name, string mail, string password, int capacity)
        {
            try
            {
                var userToUpdate = Orm.db.app.FirstOrDefault(a => a.id == userId);
                var spaceToUpdate = Orm.db.spaces.FirstOrDefault(s => s.app_user_id == userId);

                if (userToUpdate == null || spaceToUpdate == null)
                {
                    Console.WriteLine("Error: Espacio o usuario de la aplicación no encontrado.");
                    return false;
                }

                if (capacity < 1)
                {
                    Console.WriteLine("Error: La capacidad debe ser mayor a 0.");
                    return false;
                }

                userToUpdate.name = name;
                userToUpdate.mail = mail;
                userToUpdate.password = password;

                spaceToUpdate.capacity = capacity;

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

        public static bool DeleteSpace(int userId)
        {
            try
            {
                var userToDelete = Orm.db.app.FirstOrDefault(a => a.id == userId);
                var spaceToDelete = Orm.db.spaces.FirstOrDefault(s => s.app_user_id == userId);

                if (userToDelete == null || spaceToDelete == null)
                {
                    Console.WriteLine("Error: Espacio o usuario de la aplicación no encontrado.");
                    return false;
                }

                Orm.db.spaces.Remove(spaceToDelete);
                Orm.db.app.Remove(userToDelete);
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