using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Persister 的摘要描述
/// </summary>
public class Persister
{
    public static string DefaultDataSource { get; private set; }
    public static string NLSCFBDataSource { get; private set; }

    static Persister()
    {
        DefaultDataSource = ConfigurationManager.ConnectionStrings["DefaultDataSource"].ConnectionString;
        NLSCFBDataSource = ConfigurationManager.ConnectionStrings["NLSCFBDataSource"].ConnectionString;
    }

    public static int ExecuteNonQuery(SqlCommand command)
    {
        return ExecuteNonQuery(command, DefaultDataSource);
    }

    public static int ExecuteFBNonQuery(SqlCommand command)
    {
        return ExecuteNonQuery(command, NLSCFBDataSource);
    }

    public static int ExecuteNonQuery(SqlCommand command, string connectionString)
    {
        int affect = 0;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            command.Connection = connection;

            connection.Open();
            affect = command.ExecuteNonQuery();
        }
        return affect;
    }

    public static DataTable Execute(SqlCommand command)
    {
        return Execute(command, DefaultDataSource);
    }

    public static DataTable ExecuteFB(SqlCommand command)
    {
        return Execute(command, NLSCFBDataSource);
    }

    public static DataTable Execute(SqlCommand command, string connectionString)
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            command.Connection = connection;

            connection.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                dt.BeginLoadData();
                adapter.Fill(dt);
                dt.EndLoadData();
            }
        }
        return dt;
    }
}