using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DataAccessLayer.DatabaseObjects;
using DataAccessLayer.DataModels.Interfaces;
using log4net;

namespace DataAccessLayer.DataModels
{
    /// <summary>
    /// Class which represents data model of Instance
    /// </summary>
    public class Instance : IInstanceOperations
    {
        private static readonly IConector Conector = new ConnectionProvider("ConnectionStringForUse");
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Id { get; set; }
        public string HostName { get; set; }
        public string InstanceName { get; set; }
        public int Status { get; set; }
        public string Version { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Modified { get; set; }
        public int? UserId { get; set; }

        public Instance(){}

        public Instance(int id,
                        string hostName,
                        string instanceName,
                        int status,
                        string version,
                        DateTime added,
                        DateTime? modified = null,
                        int? userId = null)
        {
            Id = id;
            HostName = hostName;
            InstanceName = instanceName;
            Status = status;
            Version = version;
            Added = added;
            Modified = modified;
            UserId = userId;
        }

        /// <summary>
        /// Returns total count of instances in database
        /// </summary>
        /// <returns>Number of instances in db</returns>
        public int Count()
        {
            int instancesCount = 0;
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.InstancesCount, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@RowsCount", SqlDbType.Int));
                    cmd.Parameters["@RowsCount"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    instancesCount = (int)cmd.Parameters["@RowsCount"].Value;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Can't get count of instances", ex);
                throw;
            }
            return instancesCount;
        }

        /// <summary>
        /// Сhecks whether there is an instance with this name in a database
        /// </summary>
        /// <param name="hostName">UserName of the specific host</param>
        /// <param name="instanceName">UserName of the specific instance</param>
        /// <returns>True - if this instance exists, false if not</returns>
        public bool Exists(string hostName, string instanceName)
        {
            bool result = false;
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.InstanceExists, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@HostName", SqlDbType.VarChar, 64)).Value = hostName;
                    cmd.Parameters.Add(new SqlParameter("@InstanceName", SqlDbType.VarChar, 50)).Value = instanceName;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        result = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Can't check is instance exists", ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Adds new instance to database
        /// </summary>
        /// <param name="instance">Instance which we you are going to add</param>
        public void Add(Instance instance)
        {
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.AddInstance, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = instance.Id;
                    cmd.Parameters.Add(new SqlParameter("@HostName", SqlDbType.VarChar, 64)).Value = instance.HostName;
                    cmd.Parameters.Add(new SqlParameter("@InstanceName", SqlDbType.VarChar, 50)).Value = instance.InstanceName;
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int)).Value = instance.Status;
                    cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.VarChar, 64)).Value = instance.Version;
                    cmd.Parameters.Add(new SqlParameter("@Added", SqlDbType.DateTime)).Value = instance.Added;
                    cmd.Parameters.Add(new SqlParameter("@Modified", SqlDbType.DateTime)).Value = instance.Modified;
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int)).Value = instance.UserId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Adding instance failed", ex);
                throw;
            }
        }

        /// <summary>
        /// Updates the specific instance in database
        /// </summary>
        /// <param name="instance">Updated instance which you are going to store into database</param>
        public void Update(Instance instance)
        {
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.UpdateInstance, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = instance.Id;
                    cmd.Parameters.Add(new SqlParameter("@HostName", SqlDbType.VarChar, 64)).Value = instance.HostName;
                    cmd.Parameters.Add(new SqlParameter("@InstanceName", SqlDbType.VarChar, 50)).Value = instance.InstanceName;
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int)).Value = instance.Status;
                    cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.VarChar, 64)).Value = instance.Version;
                    cmd.Parameters.Add(new SqlParameter("@Modified", SqlDbType.DateTime)).Value = instance.Modified;
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int)).Value = instance.UserId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to update instance", ex);
                throw;
            }
        }

        /// <summary>
        /// Returns all instances from database
        /// </summary>
        /// <returns>List of instances</returns>
        public List<Instance> GetAll()
        {
            List<Instance> instances = new List<Instance>();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetAllInstances, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        instances.Add(
                            new Instance
                            (
                            (int)reader["Id"],
                            reader["HostName"].ToString().Trim(' '),
                            reader["InstanceName"].ToString().Trim(' '),
                            (int)reader["Status"],
                            reader["Version"].ToString(),
                            (DateTime)reader["Added"],
                            (reader["Modified"] != DBNull.Value) ? (DateTime)reader["Modified"] : (DateTime?)null,
                            (reader["UserId"] != DBNull.Value) ? (int)reader["UserId"] : (int?)null
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Can't get all instances", ex);
                throw;
            }
            return instances;
        }

        /// <summary>
        /// Gets specific instance from database by instance Id
        /// </summary>
        /// <param name="id">Id of instance which you need</param>
        /// <returns>Instance which you need to get</returns>
        public Instance GetById(int id)
        {
            Instance resultInstance = new Instance();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetInstanceById, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultInstance = new Instance
                        (
                            id,
                            reader["HostName"].ToString().Trim(' '),
                            reader["InstanceName"].ToString().Trim(' '),
                            (int)reader["Status"],
                            reader["Version"].ToString(),
                            (DateTime)reader["Added"],
                            (reader["Modified"] != DBNull.Value) ? (DateTime)reader["Modified"] : (DateTime?)null,
                            (reader["UserId"] != DBNull.Value) ? (int)reader["UserId"] : (int?)null
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Getting instance by Id failed", ex);
                throw;
            }
            return resultInstance;
        }

        /// <summary>
        /// Seeks instances in table of instances by the search creteria
        /// </summary>
        /// <param name="searchString">Searhc criteria</param>
        /// <param name="pageNumber">Page number(for pagination)</param>
        /// <param name="instancesPerPage">Number of instances per page(for pagination)</param>
        /// <param name="orderBy">How to order records(Descending or Ascending)</param>
        /// <returns>List of seeked instances</returns>
        public List<Instance> SearchByInstanceName(string searchString, int pageNumber, int instancesPerPage, string orderBy)
        {
            List<Instance> instances = new List<Instance>();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.SearchInstancesByInstanceName, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@InstanceName", SqlDbType.VarChar, 50)).Value = searchString;
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int)).Value = pageNumber;
                    cmd.Parameters.Add(new SqlParameter("@InstancesPerPage", SqlDbType.Int)).Value = instancesPerPage;
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar, 4)).Value = orderBy;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        instances.Add(
                            new Instance
                            (
                            (int)reader["Id"],
                            reader["HostName"].ToString().Trim(' '),
                            reader["InstanceName"].ToString().Trim(' '),
                            (int)reader["Status"],
                            reader["Version"].ToString(),
                            (DateTime)reader["Added"],
                            (reader["Modified"] != DBNull.Value) ? (DateTime)reader["Modified"] : (DateTime?)null,
                            (reader["UserId"] != DBNull.Value) ? (int)reader["UserId"] : (int?)null
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Search instances by Instance name failed", ex);
                throw;
            }
            return instances;
        }

        /// <summary>
        /// Deletes the specific instance
        /// </summary>
        /// <param name="id">Id of instance which you are going to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.DeleteInstance, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to delete instance", ex);
                throw;
            }
        }

        public List<InstanceWithUserInfo> GetAllWithUserInfo()
        {
            List<InstanceWithUserInfo> result = new List<InstanceWithUserInfo>();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetAllInstancesWithUserInfo, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new InstanceWithUserInfo(
                            (int)reader["Id"],
                            (string)reader["HostName"],
                            (string)reader["InstanceName"],
                            (int)reader["Status"],
                            (string)reader["Version"],
                            ((DateTime)reader["Added"]).ToString("d/MM/yyyy"),
                            (reader["Modified"] != DBNull.Value) ? ((DateTime)reader["Modified"]).ToString("d/MM/yyyy") : null,
                            (reader["UserId"] != DBNull.Value) ? (int?)reader["UserId"] : null,
                            (reader["UserName"] != DBNull.Value) ? (string)reader["UserName"] : null,
                            (reader["AuthentificationMode"] != DBNull.Value) ? (int)reader["AuthentificationMode"] : 0));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to get all Instances with user info", ex);
                throw;
            }
            return result;
        }

        public List<InstanceWithUserInfo> GetWithUserInfo(SearchDataOut searchData)
        {
            List<InstanceWithUserInfo> result = new List<InstanceWithUserInfo>();
            try
            {
                using (SqlConnection connection = Conector.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedureNames.GetInstancesWithUserInfoPaginationAndSearch,
                        connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int)).Value = searchData.CurrentPage;
                    cmd.Parameters.Add(new SqlParameter("@RecordsPerPage", SqlDbType.Int)).Value = searchData.ItemsPerPage;
                    cmd.Parameters.Add(new SqlParameter("@HostNameSearch", SqlDbType.VarChar, 64)).Value =
                        searchData.SearchParameterByFields.HostName;
                    cmd.Parameters.Add(new SqlParameter("@InstanceNameSearch", SqlDbType.VarChar, 50)).Value =
                        searchData.SearchParameterByFields.InstanceName;
                    cmd.Parameters.Add(new SqlParameter("@StatusSearch", SqlDbType.Int)).Value =
                        searchData.SearchParameterByFields.Status;
                    cmd.Parameters.Add(new SqlParameter("@VersionSearch", SqlDbType.VarChar, 64)).Value =
                        searchData.SearchParameterByFields.Version;
                    cmd.Parameters.Add(new SqlParameter("@AddedSearch", SqlDbType.Date)).Value =
                            searchData.SearchParameterByFields.Added;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedSearch", SqlDbType.Date)).Value =
                        searchData.SearchParameterByFields.Modified;
                    cmd.Parameters.Add(new SqlParameter("@UserNameSearch", SqlDbType.VarChar, 255)).Value =
                        searchData.SearchParameterByFields.UserName;
                    cmd.Parameters.Add(new SqlParameter("@AuthentificationModeSearch", SqlDbType.Int)).Value =
                        searchData.SearchParameterByFields.AuthMode;
                    cmd.Parameters.Add(new SqlParameter("@OrderByField", SqlDbType.VarChar, 255)).Value =
                        searchData.OrderbyField;
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.Bit)).Value = true;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new InstanceWithUserInfo(
                            (int)reader["Id"],
                            (string)reader["HostName"],
                            (string)reader["InstanceName"],
                            (int)reader["Status"],
                            (string)reader["Version"],
                            ((DateTime)reader["Added"]).ToString("dd MM yyyy"),
                            (reader["Modified"] != DBNull.Value) ? ((DateTime)reader["Modified"]).ToString("dd MM yyyy") : null,
                            (reader["UserId"] != DBNull.Value) ? (int?)reader["UserId"] : null,
                            (reader["Name"] != DBNull.Value) ? (string)reader["Name"] : null,
                            (reader["AuthentificationMode"] != DBNull.Value) ? (int)reader["AuthentificationMode"] : 0));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to get all Instances with user info", ex);
                throw;
            }
            return result;
        }
    }
}
