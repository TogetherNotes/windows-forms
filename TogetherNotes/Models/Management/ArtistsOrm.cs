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

                // Asociar géneros al artista en la tabla `artist_genre`
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

                if (artist != null)
                {
                    // Rating
                    var ratings = Orm.db.rating.Where(r => r.artist_id == userId).ToList();
                    Orm.db.rating.RemoveRange(ratings);

                    // Matches
                    //var matches = db.matches.Where(m => m.artist_id == userId).ToList();
                    //db.matches.RemoveRange(matches);

                    //// Temp Match
                    //var tempMatches = db.temp_match.Where(t => t.artist_id == userId).ToList();
                    //db.temp_match.RemoveRange(tempMatches);

                    // Contracts
                    var contracts = Orm.db.contracts.Where(c => c.artist_id == userId).ToList();
                    Orm.db.contracts.RemoveRange(contracts);

                    // Messages (si el artista envió mensajes)
                    var messages = Orm.db.messages.Where(m => m.sender_id == userId).ToList();
                    Orm.db.messages.RemoveRange(messages);

                    // Chats (si el artista participó en chats)
                    var chats = Orm.db.chats.Where(c => c.user1_id == userId || c.user2_id == userId).ToList();
                    Orm.db.chats.RemoveRange(chats);

                    // Incidences (si el artista tuvo incidencias)
                    var incidences = Orm.db.incidences.Where(i => i.app_user_id == userId).ToList();
                    Orm.db.incidences.RemoveRange(incidences);


                    // Eliminar el artista de la tabla artists
                    Orm.db.artists.Remove(artist);

                    // Eliminar el usuario de la tabla app
                    var user = Orm.db.app.SingleOrDefault(u => u.id == userId);
                    if (user != null)
                    {
                        Orm.db.app.Remove(user);
                    }

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