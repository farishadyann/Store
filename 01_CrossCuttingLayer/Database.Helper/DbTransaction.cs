using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Database.Helper
{
    public class DbTransaction
    {
        private string SqlConnectionString;

        public DbTransaction(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString;
        }

        #region DbToDataTable
        public DataTable DbToDataTable(string Query, bool IsStorageProcedure)
        {
            return ExecuteDataTable(IsStorageProcedure, Query, ObjetToSqlParameter(new { }));
        }
       
        public DataTable DbToDataTable(string Query, object Parameter, bool IsStorageProcedure)
        {
            return ExecuteDataTable(IsStorageProcedure, Query, ObjetToSqlParameter(Parameter));
        }

        private SqlParameter[] ObjetToSqlParameter(object dataObject)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (dataObject != null)
            {
                Type type = dataObject.GetType();
                PropertyInfo[] props = type.GetProperties();

                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].PropertyType.IsValueType || props[i].PropertyType.Name == "String" || props[i].PropertyType.Name == "Object")
                    {
                        object fieldValue = type.InvokeMember(props[i].Name, BindingFlags.GetProperty, null, dataObject, null);
                        SqlParameter sqlParameter = new SqlParameter("@" + props[i].Name, fieldValue);
                        paramList.Add(sqlParameter);
                    }
                }
            }
            return paramList.ToArray();
        }

        private DataTable ExecuteDataTable(bool IsStorageProcedure, string Query, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();

            // Open the connection
            using (SqlConnection sqlConnection = new SqlConnection(SqlConnectionString))
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                // Define the command
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    if (IsStorageProcedure)
                        cmd.CommandType = CommandType.StoredProcedure;
                    else
                        cmd.CommandType = CommandType.Text;

                    cmd.CommandText = Query;

                    //// Handle the parameters
                    if (arrParam != null)
                    {
                        foreach (SqlParameter param in arrParam)
                            cmd.Parameters.Add(param);
                    }

                    //cmd.Parameters.Add("MenuId", 1);

                    // Define the data adapter and fill the dataset
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        #endregion DbToDataTable

        #region DbExecute
        
        public void DbExecute(string Query, List<SqlParameter> Parameter, bool IsStorageProcedure, out SqlParameterCollection outParam)
        {
            using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(Query, sqlConnection))
                    {
                        if (IsStorageProcedure)
                            dbCommand.CommandType = CommandType.StoredProcedure;
                        else
                            dbCommand.CommandType = CommandType.Text;

                        foreach (SqlParameter param in Parameter)
                        {
                            dbCommand.Parameters.Add(param);
                        }

                        dbCommand.ExecuteNonQuery();

                        outParam = dbCommand.Parameters;
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }
        #endregion DbExecute
    }
}
