using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;
using TogetherNotes.Utils;

namespace TogetherNotes.Models.Management
{
    public static class ArtistsOrm
    {
        public static List<User> SelectAllArtists()
        {
            try
            {
                var artists = Orm.db.artists
                    .Select(a => new User
                    {
                        Id = a.app_user_id,
                        Fullname = a.app.name,
                        Mail = a.app.mail,
                        Password = a.app.password,
                        Role = a.app.role,
                        Rating = a.app.rating,
                        Genre = Orm.db.artist_genres
                            .Where(ag => ag.artist_id == a.app_user_id)
                            .Select(ag => ag.genres.name)
                            .ToList()
                    })
                    .ToList();

                return artists;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
            }

            return new List<User>();
        }




        public static bool InsertArtist(string name, string mail, string password, List<string> genres, int rating)
        {
            try
            {
                var newUser = new app
                {
                    name = name,
                    mail = mail,
                    password = password,
                    role = "Artist",
                    rating = rating,
                    active = true
                };

                Orm.db.app.Add(newUser);
                Orm.db.SaveChanges();

                var newArtist = new artists
                {
                    app_user_id = newUser.id
                };

                Orm.db.artists.Add(newArtist);
                Orm.db.SaveChanges();

                foreach (var genreName in genres)
                {
                    var genre = Orm.db.genres.FirstOrDefault(g => g.name == genreName);
                    if (genre != null)
                    {
                        Orm.db.artist_genres.Add(new artist_genres
                        {
                            artist_id = newArtist.app_user_id,
                            genre_id = genre.id
                        });
                    }
                }

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


        public static bool UpdateArtist(int userId, string name, string mail, string password, List<string> genres, int rating)
        {
            try
            {
                var userToUpdate = Orm.db.app.FirstOrDefault(a => a.id == userId);

                if (userToUpdate == null)
                {
                    Console.WriteLine("Error: Artista o usuario no encontrado.");
                    return false;
                }

                if (rating < 1 || rating > 5)
                {
                    Console.WriteLine("Error: El rating debe estar entre 1 y 5.");
                    return false;
                }

                userToUpdate.name = name;
                userToUpdate.mail = mail;
                userToUpdate.password = password;
                userToUpdate.rating = rating;

                var existingGenres = Orm.db.artist_genres.Where(ag => ag.artist_id == userId).ToList();
                Orm.db.artist_genres.RemoveRange(existingGenres);


                foreach (var genreName in genres)
                {
                    var genre = Orm.db.genres.FirstOrDefault(g => g.name == genreName);
                    if (genre != null)
                    {
                        Orm.db.artist_genres.Add(new artist_genres
                        {
                            artist_id = userId,
                            genre_id = genre.id
                        });
                    }
                }

                Orm.db.SaveChanges();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
            }
            return false;
        }

        public static bool DeleteArtist(int userId)
        {
            try
            {
                var artist = Orm.db.artists.SingleOrDefault(a => a.app_user_id == userId);
                var user = Orm.db.app.SingleOrDefault(a => a.id == userId);

                if (artist != null && user != null)
                {
                    // Eliminar artist_genres (Relaciones con géneros)
                    var artistGenres = Orm.db.artist_genres.Where(a => a.artist_id == userId).ToList();
                    Orm.db.artist_genres.RemoveRange(artistGenres);

                    // Eliminar ratings
                    var ratings = Orm.db.rating.Where(r => r.artist_id == userId).ToList();
                    Orm.db.rating.RemoveRange(ratings);

                    // Eliminar matches
                    var matches = Orm.db.matches.Where(m => m.artist_id == userId).ToList();
                    Orm.db.matches.RemoveRange(matches);

                    // Eliminar temp_matches
                    var tempMatches = Orm.db.temp_match.Where(t => t.artist_id == userId).ToList();
                    Orm.db.temp_match.RemoveRange(tempMatches);

                    // Eliminar contratos
                    var contracts = Orm.db.contracts.Where(c => c.artist_id == userId).ToList();
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

                    // Eliminar artista de artists
                    Orm.db.artists.Remove(artist);

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
                Console.WriteLine("Error al eliminar artista: " + ex.Message);
                return false;
            }
        }
    }
}