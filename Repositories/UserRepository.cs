using Gradebook.Models;
using Gradebook.Repositories;
using Microsoft.Data.SqlClient;

namespace Gradebook.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public User GetById(int id)
        {
            //User to return
            User user = null;
            using (var connection = Connection)
            {
                //Open connection to SQL Database
                connection.Open();
                //Create the command to becomes a SQL query
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Select Id as UserId, [Name], Email, DateCreated, FirebaseId
                                        from [User]
                                        where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = UserBuilder(reader);
                        }
                    }
                }
            }
            return user;
        }

        public User GetByEmail(string email)
        {
            User user = null;
            using (var connection = Connection)
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Select Id as UserId, [Name], Email, DateCreated, FirebaseId
                                        from [User]
                                        where Email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = UserBuilder(reader);
                        }
                    }
                }
            }
            return user;
        }

        public void Add(User user)
        {
            using (var connection = Connection)
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Insert into [User] (DisplayName, Email, ProfilePictureUrl, DateCreated, FirebaseId, IsBanned, PackId)
                                        OUTPUT INSERTED.ID
                                        values (@UN, @email, @dc, @fbid)";

                    cmd.Parameters.AddWithValue("@UN", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@dc", DateTime.Now);
                    cmd.Parameters.AddWithValue("@fbid", user.FirebaseId);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(User user)
        {
            using (var connection = Connection)
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Update [User]
                                        SET [Name] = @UN,
                                        Where Id = @id";

                    cmd.Parameters.AddWithValue("@UN", user.Name);
                    //if (!String.IsNullOrWhiteSpace(user.ProfilePictureUrl))
                    //{
                    //    cmd.Parameters.AddWithValue("@pfp", user.ProfilePictureUrl);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@pfp", DBNull.Value);
                    //}
                    cmd.Parameters.AddWithValue("id", user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetByFirebaseId(string firebaseId)
        {
            User user = null;
            using (var connection = Connection)
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Select Id as UserId, [Name], Email, DateCreated, FirebaseId
                                        from [User]
                                        where FirebaseId = @fbid";

                    cmd.Parameters.AddWithValue("@fbid", firebaseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = UserBuilder(reader);
                        }
                    }
                }
            }
            return user;
        }

        public List<User> GetByDistrictId(int districtId)
        {
            List<User> usersInDistrict = new List<User>();
            //using (var connection = Connection)
            //{
            //    connection.Open();

            //    using(var cmd = connection.CreateCommand())
            //    {
            //        cmd.CommandText = @"";
            //        //cmd.Parameters.AddWithValue();

            //        using(SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while(reader.Read())
            //            {
            //                User user = UserBuilder(reader);
            //                usersInDistrict.Add(user);
            //            }
            //        }
            //    }
            //}
            return usersInDistrict;
        }






        private User UserBuilder(SqlDataReader reader)
        {
            User user = new()
            {
                Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                Name = reader.GetString(reader.GetOrdinal("DisplayName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
            };
            //if (!reader.IsDBNull(reader.GetOrdinal("ProfilePictureUrl")))
            //{
            //    user.ProfilePictureUrl = reader.GetString(reader.GetOrdinal("ProfilePictureUrl"));
            //}
            //else
            //{
            //    user.ProfilePictureUrl = null;
            //}
            return user;
        }
    }
}
