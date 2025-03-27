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

        public static bool InsertSpace(string name, string mail, string password, int capacity, int rating)
        {
            try
            {
                var newUser = new app
                {
                    name = name,
                    mail = mail,
                    password = password,
                    role = "Space",
                    rating = rating, 
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
                var space = Orm.db.spaces.SingleOrDefault(s => s.app_user_id == userId);
                var user = Orm.db.app.SingleOrDefault(a => a.id == userId);

                if (space != null && user != null)
                {
                    // Eliminar ratings del espacio
                    var ratings = Orm.db.rating.Where(r => r.space_id == userId).ToList();
                    Orm.db.rating.RemoveRange(ratings);

                    // Eliminar matches
                    var matches = Orm.db.matches.Where(m => m.space_id == userId).ToList();
                    Orm.db.matches.RemoveRange(matches);

                    // Eliminar temp_matches
                    var tempMatches = Orm.db.temp_match.Where(t => t.space_id == userId).ToList();
                    Orm.db.temp_match.RemoveRange(tempMatches);

                    // Eliminar contratos
                    var contracts = Orm.db.contracts.Where(c => c.space_id == userId).ToList();
                    Orm.db.contracts.RemoveRange(contracts);

                    // **ELIMINAR MENSAJES PRIMERO**
                    var messages = Orm.db.messages.Where(m => m.chat_id != null &&
                                                              (Orm.db.chats.Any(c => c.id == m.chat_id &&
                                                                                      (c.user1_id == userId || c.user2_id == userId)))).ToList();
                    Orm.db.messages.RemoveRange(messages);

                    // **AHORA ELIMINAR CHATS**
                    var chats = Orm.db.chats.Where(c => c.user1_id == userId || c.user2_id == userId).ToList();
                    Orm.db.chats.RemoveRange(chats);

                    // Eliminar incidencias
                    var incidences = Orm.db.incidences.Where(i => i.app_user_id == userId).ToList();
                    Orm.db.incidences.RemoveRange(incidences);

                    // Eliminar el espacio de la tabla spaces
                    Orm.db.spaces.Remove(space);

                    // Eliminar archivos asociados si existen
                    if (user.file_id != null)
                    {
                        var file = Orm.db.files.SingleOrDefault(f => f.id == user.file_id);
                        if (file != null) Orm.db.files.Remove(file);
                    }

                    // Eliminar notificaciones asociadas si existen
                    if (user.notification_id != null)
                    {
                        var notification = Orm.db.notifications.SingleOrDefault(n => n.id == user.notification_id);
                        if (notification != null) Orm.db.notifications.Remove(notification);
                    }

                    // Finalmente, eliminar el usuario de la tabla app
                    Orm.db.app.Remove(user);

                    // Guardar cambios
                    Orm.db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar espacio: " + ex.Message);
                return false;
            }
        }


    }
}