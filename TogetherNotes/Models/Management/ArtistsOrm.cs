using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace TogetherNotes.Models.Management
{
    public static class ArtistsOrm
    {
        public static List<artists> SelectAllArtist()
        {
            try
            {
                return Orm.db.artists.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<artists>();
        }


        public static bool InsertArtist(string name, string mail, string password, int genreId, int rating)
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
                    app_user_id = newUser.id,
                    genre_id = genreId
                };

                Orm.db.artists.Add(newArtist);
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

        public static bool UpdateArtist(int userId, string name, string mail, string password, int genreId, int rating)
        {
            try
            {
                var userToUpdate = Orm.db.app.FirstOrDefault(a => a.id == userId);
                var artistToUpdate = Orm.db.artists.FirstOrDefault(a => a.app_user_id == userId);

                if (userToUpdate == null || artistToUpdate == null)
                {
                    Console.WriteLine("Error: Artista o usuario de la aplicación no encontrado.");
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

                artistToUpdate.genre_id = genreId;

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



        public static bool DeleteArtist(int userId)
        {
            try
            {
                var artistToDelete = Orm.db.artists.FirstOrDefault(a => a.app_user_id == userId);
                var userToDelete = Orm.db.app.FirstOrDefault(a => a.id == userId);

                if (artistToDelete != null && userToDelete != null)
                {
                    Orm.db.artists.Remove(artistToDelete);
                    Orm.db.app.Remove(userToDelete);
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