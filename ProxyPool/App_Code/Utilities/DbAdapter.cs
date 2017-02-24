using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace ProxyPool
{
    /// <summary>
    /// DbAdapter 的摘要说明
    /// </summary>
    public class DbAdapter
    {
        public DbAdapter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["proxy_pool"].ToString());
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }
        private SqlConnection conn;
        private SqlCommand cmd;

        public DataTable ExecQuery(string sql)
        {
            DataTable dt = new DataTable();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public Object ExecScalar(string sql)
        {
            object o = null;
            cmd.CommandText = sql;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            o = cmd.ExecuteScalar();
            conn.Close();
            return o;
        }
        public int ExecNonQuery(string sql)
        {
            int r = -1;
            cmd.CommandText = sql;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            r = cmd.ExecuteNonQuery();
            conn.Close();
            return r;
        }

        public bool Testconn()
        {
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch { return false; }
        }
    }
}