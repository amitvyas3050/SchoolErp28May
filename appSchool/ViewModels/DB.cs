using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Text;

namespace appSchool.ViewModels
{
    public class DB
    {
        #region METHOD DECLARED REGION
        public static string GetConnectionString()
        {
            string ConString = ConfigurationManager.ConnectionStrings["DataConnectionString"].ToString();
            return ConString;
        }
        public static string GetBioMetricConnectionString()
        {
            string ConString = ConfigurationManager.ConnectionStrings["BioMetricCons"].ToString();
            return ConString;
        }
        public static string GetCityDBConnectionString()
        {
            string ConStringDB = ConfigurationManager.ConnectionStrings["DataConnectionString"].ToString();
            return ConStringDB;
        }
        public static string GetConnectionStringEEEAdmin()
        {
            return "Data Source=SERVER;Initial Catalog=EEEAdmin;User ID=Admin3e;Password=transport3e;";
        }
        public static SqlConnection GetBioMetricActiveConnection()
        {
            SqlConnection mConn = null;
            try
            {
                //mConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                mConn = new SqlConnection(GetBioMetricConnectionString());
                mConn.Open();
            }
            catch (Exception ex)
            {
                //throw ex; 
                mConn = new SqlConnection(GetBioMetricConnectionString());
                mConn.Open();
            }
            return mConn;
        }
        public static SqlConnection GetCityDBConnection()
        {
            SqlConnection mConn = null;
            try
            {

                mConn = new SqlConnection(GetCityDBConnectionString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mConn;
        }
        public static SqlConnection GetConnection()
        {
            SqlConnection mConn = null;
            try
            {
                //mConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());    
                mConn = new SqlConnection(GetConnectionString());
                mConn.Open();
                mConn.Close();
            }
            catch (Exception ex)
            {
                
                //throw ex;
                mConn = new SqlConnection(GetConnectionString());
            }
            return mConn;
        }
        public static SqlConnection GetActiveConnection()
        {
            SqlConnection mConn = null;
            try
            {
                //mConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                mConn = new SqlConnection(GetConnectionString());
                mConn.Open();
            }
            catch(Exception ex)
            {
                //throw ex; 
                mConn = new SqlConnection(GetConnectionString());
                mConn.Open();
            }
            return mConn;
        }
        public static SqlConnection GetConnectionEEEAdmin()
        {
            SqlConnection mConn = null;
            try
            {
                //mConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());    
                mConn = new SqlConnection(GetConnectionStringEEEAdmin());
                mConn.Open();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mConn;
        }
        public static SqlCommand CreateCommand(string ProcName)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(ProcName, GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
               
            }
            catch(Exception ex)
            {
                throw ex; 
            }
            return cmd;
        }
        public static SqlCommand CreateCommand(string ProcName,bool Isadmin)
        {
            SqlCommand cmd = null;
           
            try
            {
                cmd = new SqlCommand(ProcName, GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }
        public static SqlCommand CreateQuery(string ProcName)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(ProcName, GetConnection());
                cmd.CommandType = CommandType.Text;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return cmd;
        }
        public static SqlCommand CreateQueryUniversalDB(string ProcName)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(ProcName, GetCityDBConnection());
                cmd.CommandType = CommandType.Text;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return cmd;
        }
        public static DataTable ExecuteCommand(SqlCommand cmd)
        {
            DataTable dt = null;
            try
            {
                cmd.Connection.Open();
                //SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(rdr);
                cmd.Connection.Close();

                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(cmd.Connection.State ==  ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return dt;
        }
        public static object ExecuteScalarCommand(SqlCommand cmd)
        {
            object o = null;
            try
            {
                cmd.Connection.Open();
                o = cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw ex; 
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return o;
        }
        public static int ExecuteScalarQuery(String mQuery)
        {
            int DefStateId=0;
            try
            {
                int.TryParse(DB.ExecuteScalarCommand(DB.CreateQuery(mQuery)).ToString(), out DefStateId);
            }
            catch(Exception ex)
            {
                return DefStateId; 
            }
            finally
            {
                
            }
            return DefStateId;  
            
        }

        public static int ExecuteQueryNoResult(string mQuery)
        {
            int i = -1;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(mQuery, GetActiveConnection());

                i = cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return i;

        }
        public static Double ExecuteScalarQueryDouble(String mQuery)
        {
            double DefStateId = 0;
            try
            {
                double.TryParse(DB.ExecuteScalarCommand(DB.CreateQuery(mQuery)).ToString(), out DefStateId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return DefStateId;

        }
        public static string GetListForGrid(String mQuery)
        {
            string ItemList = "|";
            SqlCommand cmd=null;
            SqlDataReader rdr;
            try
            {
                cmd = DB.CreateQuery(mQuery);
                cmd.Connection.Open();
                rdr =cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemList = ItemList + rdr[0].ToString() + "|";
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return ItemList;

        }
        public static int ExecuteScalarQueryOnUniversalDB(String mQuery)
        {
            int DefStateId = 0;

            try
            {
                int.TryParse(DB.ExecuteScalarCommand(DB.CreateQueryUniversalDB(mQuery)).ToString(), out DefStateId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return DefStateId;  

        }
        public static int ExecuteNoResult(SqlCommand cmd)
        {
            int IDX = -1;
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                IDX = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception EX)
            {
                throw EX;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return IDX;
        }
        public static DataRow ExecuteSingleRow(string mQuery)
        {
            SqlCommand cmd=null;
            DataTable dt = new DataTable();
            DataRow dr = null; 
            try
            {
                cmd = new SqlCommand("Execute_SQL", GetActiveConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dr= dt.Rows[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return dr;
        }
        //public static void SetAccountVoucherSetup()
        //{
        //    SqlConnection _mConn = GetActiveConnection();
        //    SqlTransaction _mTran;
        //    _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
        //    DateTime Dt = Program.StartDate;
        //    StringBuilder mQuery = new StringBuilder();
        //    mQuery.Append("Insert into dbo.AccountSetup (VouDate,BranchID,SessionID,CompID,GodownID) ");
        //    for (int i = 1; i <= 365; i++)
        //    {
        //        mQuery.Append(" select '" + Dt.Date.ToString("MM/dd/yyyy") + "'," + Program.BRANCH_ID.ToString() + "," + Program.SESSION_ID.ToString() + "," + Program.COMPANY_ID.ToString() + "," + Program.GODOWN_ID.ToString());
        //        Dt = Dt.AddDays(1);
        //        if (i < 365) mQuery.Append(" Union All ");
        //    }
        //    SqlCommand cmdDetail = new SqlCommand(mQuery.ToString(), _mConn);
        //    cmdDetail.Transaction = _mTran;
        //    cmdDetail.CommandType = CommandType.Text;
        //    int recs = cmdDetail.ExecuteNonQuery();
        //    _mTran.Commit();

        //}
        public static DataTable ExecuteQuery(string mQuery)
        {
            SqlCommand cmd = null;
            DataTable dt = null;
            try
            {
                cmd = new SqlCommand(mQuery, GetActiveConnection());
                
                SqlDataReader rdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(rdr);
                cmd.Connection.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return dt;
        
        }

        public static DataTable ExecuteBiometricQuery(string mQuery)
        {
            SqlCommand cmd = null;
            DataTable dt = null;
            try
            {
                cmd = new SqlCommand(mQuery, GetBioMetricActiveConnection());

                SqlDataReader rdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(rdr);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            return dt;

        }
        public static string GetListForGridWithId(String mQuery)
        {
            string ItemList = "|";
            SqlCommand cmd = null;
            SqlDataReader rdr;
            try
            {
                cmd = DB.CreateQuery(mQuery);
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemList = ItemList + rdr[0].ToString() + "\t" + rdr[1].ToString() + "|";
                }
            }
            catch
            {
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return ItemList;
        }
        public static string[] GetListArray(String mQuery)
        {
            string[] ItemList;
            SqlCommand cmd = null;
            SqlDataReader rdr;
            DataTable dt = new DataTable() ;
            try
            {
                cmd = DB.CreateQuery(mQuery);
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                
                ItemList = new string[dt.Rows.Count];
                for (int i = 0; i <= dt.Rows.Count-1;++i )
                    {
                        ItemList[i] = dt.Rows[i][0].ToString();
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return ItemList;

        }
        #endregion
    }
}
