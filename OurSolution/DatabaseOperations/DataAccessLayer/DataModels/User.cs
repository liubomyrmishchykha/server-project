using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DataAccessLayer.DatabaseObjects;
using DataAccessLayer.DataModels.Interfaces;
using log4net;
using log4net.Core;

namespace DataAccessLayer.DataModels
{
    public class User : IUserOperations
    {
        private static readonly IConector Conector = new ConnectionProvider("ConnectionStringForUse");
        private static readonly ILog Log = LogManager.GetLogger(typeof(User));
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int AuthentificationMode { get; set; }

        /// <summary>
        /// Use this constructor for dependency injection to DbDataProvider
        /// </summary>
        public User() { }

        /// <summary>
        /// Use this constructor when you need to create new user
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="pass">User password</param>
        /// <param name="authMode">Authentification mode</param>
        /// <remarks>0 - Unknown, 1 - Windows authentification, 2 - Mixed</remarks>
        /// <param name="id">User id</param>
        /// <remarks>When creating new user this is optional</remarks>
        public User(string name, string pass, int? id = null, int authMode = 0)
        {
            Id = id;
            Name = name;
            Password = pass;
            AuthentificationMode = authMode;
        }

        /// <summary>
        /// Adds new user to database
        /// </summary>
        /// <param name="user">User which we are going to add to database</param>
        public void Add(User user)
        {
            try
            {
                if (user.Equals(null))
                    throw new NullReferenceException("Passed user is null");
                if (user.Name == string.Empty)
                    throw new ArgumentException("User name can't be empty");
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.AddUser, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = user.Id;
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255)).Value = user.Name;
                    cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 64)).Value = user.Password;
                    cmd.Parameters.Add(new SqlParameter("@AuthentificationMode", SqlDbType.Int)).Value = user.AuthentificationMode;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to add user", ex);
                throw;
            }
        }


        /// <summary>
        /// Updates specific user in database
        /// </summary>
        /// <param name="user">Updated user which you are going to store in database</param>
        public void Update(User user)
        {
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.UpdateUser, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = user.Id;
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255)).Value = user.Name;
                    cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 64)).Value = user.Password;
                    cmd.Parameters.Add(new SqlParameter("@AuthentificationMode", SqlDbType.Int)).Value = user.AuthentificationMode;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to update user", ex);
                throw;
            }
        }

        /// <summary>
        /// Method returns list of all users from Users table
        /// </summary>
        /// <returns> List of users </returns>
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetAllUsers, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add
                        (
                            new User
                            (
                                reader["Name"].ToString(),
                                reader["Password"].ToString(),
                                (reader["Id"] != DBNull.Value) ? (int)reader["Id"] : (int?)null,
                                (int)reader["AuthentificationMode"]
                            )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to get all users", ex);
                throw;
            }
            return users;
        }

        /// <summary>
        /// Gets specific user from table by id
        /// </summary>
        /// <param name="id">Id of user which you need</param>
        /// <returns>User object, if user with this id was found</returns>
        public User GetById(int id)
        {
            User resultUser = null;
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetUserById, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultUser = new User
                        (

                                reader["Name"].ToString(),
                                reader["Password"].ToString(),
                                (reader["Id"] != DBNull.Value) ? (int)reader["Id"] : (int?)null,
                                (int)reader["AuthentificationMode"]


                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to get user by id", ex);
                throw;
            }
            return resultUser;
        }

        /// <summary>
        /// Deletes specific user from database
        /// </summary>
        /// <param name="id">Id of user which you are going to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.DeleteUser, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to delete user", ex);
                throw;
            }
        }


    }
}
