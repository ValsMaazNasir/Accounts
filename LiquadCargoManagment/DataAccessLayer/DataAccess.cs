using Bilty.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LiquadCargoManagment.DataAccessLayer
{
    public static class DataAccess
    {
        public static int ExecuteByQuery(string query)
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            int value = 0;
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                value = cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            return value;
        }
        public static Int64 GetInsertedIdExecuteByQuery(string query)
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            Int64 value = 0;
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            // conn.Open();
            try
            {
                using (SqlConnection con = new SqlConnection(conn.ConnectionString))
                {
                    conn.Open();
                    //Insert QUery with Scope_Identity
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        value = Convert.ToInt64(cmd.ExecuteScalar());

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            return value;
        }
        public static DataTable GetDataTableByQuery(string query)
        {
            DataTable dt = new DataTable();
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter adp; //= new SqlDataAdapter();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                adp = new SqlDataAdapter(cm);
                adp.Fill(dt);


            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }
        public static List<T> GetDateByQuey<T>(string query, SqlParameter[] parma)
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            List<T> RowList;
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                SqlDataReader rd = cm.ExecuteReader();
                if (rd.HasRows)
                {
                    RowList = DataReaderMapToList<T>(rd);
                    return RowList;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                AppLogEntry(query);
                AppLogEntry("GetDateByQuey:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            RowList = null;
            return RowList;
        }
        public static List<T> GetDateBySp<T>(string query)
        {
            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@Action_Type", SqlDbType.Int);


            paramsToStore[0].Value = 104;

            paramsToStore[1] = new SqlParameter("@p_Success", SqlDbType.Bit);
            paramsToStore[1].Direction = ParameterDirection.Output;
            paramsToStore[2] = new SqlParameter("@p_Error_Message", SqlDbType.VarChar);
            paramsToStore[2].Direction = ParameterDirection.Output;
            paramsToStore[2].Size = -1;
            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            List<T> RowList;
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandTimeout = 0;
                cm.CommandText = query;
                cm.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramsToStore.Length; i++)
                {
                    cm.Parameters.Add(paramsToStore[i]);
                }
                SqlDataReader rd = cm.ExecuteReader();
                if (rd.HasRows)
                {
                    RowList = DataReaderMapToList<T>(rd);
                    return RowList;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                AppLogEntry(query);
                AppLogEntry("GetDateByQuey:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            RowList = null;
            return RowList;
        }

        public static List<T> GetDataFromSpById<T>(string spName, string PrimaryName, Int64 PrimaryValue = 0)
        {
            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@Action_Type", SqlDbType.Int);
            paramsToStore[0].Value = 104;
            paramsToStore[1] = new SqlParameter("@p_Success", SqlDbType.Bit);
            paramsToStore[1].Direction = ParameterDirection.Output;
            paramsToStore[2] = new SqlParameter("@p_Error_Message", SqlDbType.VarChar);
            paramsToStore[2].Direction = ParameterDirection.Output;
            paramsToStore[2].Size = -1;
            paramsToStore[3] = new SqlParameter("@" + PrimaryName, SqlDbType.BigInt);
            paramsToStore[3].Value = PrimaryValue;
            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            List<T> RowList;
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = spName;
                cm.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramsToStore.Length; i++)
                {
                    cm.Parameters.Add(paramsToStore[i]);
                }
                SqlDataReader rd = cm.ExecuteReader();
                if (rd.HasRows)
                {
                    RowList = DataReaderMapToList<T>(rd);
                    return RowList;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                AppLogEntry(spName);
                AppLogEntry("GetDataFromSpById:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            RowList = null;
            return RowList;
        }

        public static List<T> GetDateBySp<T>(string query, SqlParameter[] paramsToStore)
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            List<T> RowList;
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = query;
                cm.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramsToStore.Length; i++)
                {
                    cm.Parameters.Add(paramsToStore[i]);
                }
                SqlDataReader rd = cm.ExecuteReader();
                if (rd.HasRows)
                {
                    RowList = DataReaderMapToList<T>(rd);
                    return RowList;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                AppLogEntry(query);
                AppLogEntry("GetDateByQuey:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            RowList = null;
            return RowList;
        }


        public static Response InsertOrUpdateOrDelete(string spName, SqlParameter[] paramsToStore)
        {
            Response retunValue = new Response();
            try
            {
                int value = ExecuteNonQuery(spName, paramsToStore);
                retunValue.ReturnValue = value;
                retunValue.Error = paramsToStore[2].Value.ToString();
                retunValue.Success = (bool)paramsToStore[1].Value;

            }
            catch (Exception ex)
            {
                AppLogEntry(spName);
                AppLogEntry("GetDateByQuey:-" + ex.Message.ToString());
            }
            finally
            {

            }
            return retunValue;
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public static bool AppLogEntry(string sLog)
        {
            DateTime dt;
            dt = DateTime.Now;
            string eventDate = DateTime.Now.ToString("MM-dd-yyyy");
            string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString() + "Log\\";
            try
            {
                if (!System.IO.Directory.Exists(LogPath))
                {
                    System.IO.Directory.CreateDirectory(LogPath);
                }

                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                File.AppendAllText(LogPath + eventDate + ".log", DateTime.Now.ToString());
                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                File.AppendAllText(LogPath + eventDate + ".log", sLog);
                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                //   StreamWriter sw = new StreamWriter(LogPath + "\\Log-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".log", true);
                // sw.WriteLine(" Log Entry: " + dt + Environment.NewLine + sLog + Environment.NewLine); sw.Flush(); sw.Close();
            }
            catch (Exception CurrentException)
            {
                throw;
            }
            return true;
        }
        public static Response InsertUpdateDeleteOrder(usp_InquiryAndOrders model, int Action_Type, long GroupID, long? CompanyID, long? DepartmentID, long UserLogin)
        {

            Response retunValue = new Response();
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[18];
                paramsToStore[0] = new SqlParameter("@Action_Type", SqlDbType.Int);
                if (Action_Type != 0)
                {
                    paramsToStore[0].Value = Action_Type;
                }
                else
                {


                    if (model.Order_ID == 0)
                        paramsToStore[0].Value = 101;
                    else if (model.Order_ID > 0)
                        paramsToStore[0].Value = 102;
                    else
                        paramsToStore[0].Value = Action_Type;
                }
                paramsToStore[1] = new SqlParameter("@p_Success", SqlDbType.Bit);
                paramsToStore[1].Direction = ParameterDirection.Output;
                paramsToStore[2] = new SqlParameter("@p_Error_Message", SqlDbType.VarChar);
                paramsToStore[2].Direction = ParameterDirection.Output;
                paramsToStore[2].Size = -1;
                paramsToStore[3] = new SqlParameter("@Order_ID", SqlDbType.BigInt);
                if (model.Order_ID == 0)
                {
                    paramsToStore[3].Direction = ParameterDirection.Output;
                }
                else
                {
                    paramsToStore[3].Direction = ParameterDirection.InputOutput;
                    paramsToStore[3].Value = model.Order_ID;
                }
                paramsToStore[4] = new SqlParameter("@OrderDate", SqlDbType.DateTime);
                paramsToStore[4].Value = DateTime.Now;
                paramsToStore[5] = new SqlParameter("@CustomerID", SqlDbType.BigInt);
                paramsToStore[5].Value = model.CustomerID;
                paramsToStore[6] = new SqlParameter("@IsForward", SqlDbType.Bit);
                paramsToStore[6].Value = model.IsForward;
                paramsToStore[7] = new SqlParameter("@Department", SqlDbType.Int);
                paramsToStore[7].Value = model.Department;
                paramsToStore[8] = new SqlParameter("@IsResponseBackToCustomer", SqlDbType.Bit);
                paramsToStore[8].Value = model.IsResponseBackToCustomer;
                paramsToStore[9] = new SqlParameter("@IsInquiryToOrder", SqlDbType.Bit);
                paramsToStore[9].Value = model.IsInquiryToOrder;
                paramsToStore[10] = new SqlParameter("@IsComplete", SqlDbType.Bit);
                paramsToStore[10].Value = model.IsComplete;
                if (model.Order_ID == 0)
                {
                    paramsToStore[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                    paramsToStore[13].Value = DateTime.Now;
                    paramsToStore[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt);
                    paramsToStore[11].Value = UserLogin;
                    paramsToStore[12] = new SqlParameter("@ModifiedBy", SqlDbType.BigInt);
                    paramsToStore[12].Value = DBNull.Value;
                    paramsToStore[14] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                    paramsToStore[14].Value = DBNull.Value;
                }
                else
                {
                    paramsToStore[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                    paramsToStore[13].Value = DBNull.Value;
                    paramsToStore[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt);
                    paramsToStore[11].Value = DBNull.Value;
                    paramsToStore[12] = new SqlParameter("@ModifiedBy", SqlDbType.BigInt);
                    paramsToStore[12].Value = UserLogin;

                    paramsToStore[14] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                    paramsToStore[14].Value = DateTime.Now;
                }
                paramsToStore[15] = new SqlParameter("@CompanyId", SqlDbType.Int);
                paramsToStore[15].Value = CompanyID;
                paramsToStore[16] = new SqlParameter("@GroupID", SqlDbType.Int);
                paramsToStore[16].Value = GroupID;
                paramsToStore[17] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                paramsToStore[17].Value = DepartmentID;
                int value = ExecuteNonQuery("usp_InquiryAndOrders", paramsToStore);
                retunValue.ReturnValue = value;
                retunValue.Error = paramsToStore[2].Value.ToString();
                retunValue.Success = (bool)paramsToStore[1].Value;
                retunValue.LastRowID = Convert.ToInt64(paramsToStore[3].Value.ToString());
            }
            catch (Exception ex)
            {
                AppLogEntry("usp_InquiryAndOrders");
                AppLogEntry("InsertUpdateDeleteOrder:-" + ex.Message.ToString());
            }
            finally
            {

            }
            return retunValue;
        }
        public static List<usp_InquiryAndOrders> GetOrder(usp_InquiryAndOrders model, int Action_Type, long GroupID, long? CompanyID, long? DepartmentID, long UserLogin)
        {
            List<usp_InquiryAndOrders> lstOrder;
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[19];
                paramsToStore[0] = new SqlParameter("@Action_Type", SqlDbType.Int);


                paramsToStore[0].Value = Action_Type;

                paramsToStore[1] = new SqlParameter("@p_Success", SqlDbType.Bit);
                paramsToStore[1].Direction = ParameterDirection.Output;
                paramsToStore[2] = new SqlParameter("@p_Error_Message", SqlDbType.VarChar);
                paramsToStore[2].Direction = ParameterDirection.Output;
                paramsToStore[2].Size = -1;


                paramsToStore[3] = new SqlParameter("@Order_ID", SqlDbType.BigInt);

                if (model.Order_ID > 0)
                {
                    paramsToStore[3].Direction = ParameterDirection.InputOutput;
                    paramsToStore[3].Value = model.Order_ID;
                }
                else
                {
                    paramsToStore[3].Direction = ParameterDirection.Output;

                }
                paramsToStore[4] = new SqlParameter("@OrderDate", SqlDbType.DateTime);
                paramsToStore[4].Value = DateTime.Now;
                paramsToStore[5] = new SqlParameter("@CustomerID", SqlDbType.BigInt);
                paramsToStore[5].Value = model.CustomerID;
                paramsToStore[6] = new SqlParameter("@IsForward", SqlDbType.Bit);
                if (model.IsForward == false)
                    paramsToStore[6].Value = DBNull.Value;
                else
                    paramsToStore[6].Value = model.IsForward;
                paramsToStore[7] = new SqlParameter("@Department", SqlDbType.Int);
                paramsToStore[7].Value = model.Department;
                paramsToStore[8] = new SqlParameter("@IsResponseBackToCustomer", SqlDbType.Bit);
                paramsToStore[8].Value = model.IsResponseBackToCustomer;
                paramsToStore[9] = new SqlParameter("@IsInquiryToOrder", SqlDbType.Bit);
                paramsToStore[9].Value = model.IsInquiryToOrder;
                paramsToStore[10] = new SqlParameter("@IsComplete", SqlDbType.Bit);
                paramsToStore[10].Value = model.IsComplete;
                if (model.Order_ID == 0)
                {
                    paramsToStore[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                    paramsToStore[13].Value = DateTime.Now;
                    paramsToStore[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt);
                    paramsToStore[11].Value = UserLogin;
                    paramsToStore[12] = new SqlParameter("@ModifiedBy", SqlDbType.BigInt);
                    paramsToStore[12].Value = DBNull.Value;
                    paramsToStore[14] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                    paramsToStore[14].Value = DBNull.Value;
                }
                else
                {
                    paramsToStore[4].Value = DBNull.Value;
                    paramsToStore[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                    paramsToStore[13].Value = DBNull.Value;
                    paramsToStore[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt);
                    paramsToStore[11].Value = DBNull.Value;
                    paramsToStore[12] = new SqlParameter("@ModifiedBy", SqlDbType.BigInt);
                    paramsToStore[12].Value = UserLogin;

                    paramsToStore[14] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                    paramsToStore[14].Value = DateTime.Now;
                }
                paramsToStore[15] = new SqlParameter("@CompanyId", SqlDbType.Int);
                paramsToStore[15].Value = CompanyID;
                paramsToStore[16] = new SqlParameter("@GroupID", SqlDbType.Int);
                paramsToStore[16].Value = GroupID;
                paramsToStore[17] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                paramsToStore[17].Value = DepartmentID;
                paramsToStore[18] = new SqlParameter("@Active", SqlDbType.Bit);
                paramsToStore[18].Value = model.Active;
                lstOrder = GetReaderData<usp_InquiryAndOrders>("usp_InquiryAndOrders", paramsToStore);
                return lstOrder;
            }
            catch (Exception excp)
            {

            }
            finally
            {

            }

            lstOrder = null;
            return lstOrder;
        }



        public static int ExecuteNonQuery(string spName, SqlParameter[] param)
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            int value = 0;
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = spName;
                cm.CommandType = CommandType.StoredProcedure;
                if (param.Length > 0)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cm.Parameters.Add(param[i]);
                    }
                }
                value = cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppLogEntry(spName);
                AppLogEntry("ExecuteNonQuery:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            return value;
        }

        public static List<T> GetReaderData<T>(string spName, SqlParameter[] param) where T : new()
        {

            SqlCommand cm = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["BiltySystem"].ToString();
            conn.Open();
            List<T> RowList;
            try
            {
                cm = new SqlCommand();
                cm.Connection = conn;
                cm.CommandText = spName;
                cm.CommandType = CommandType.StoredProcedure;
                if (param.Length > 0)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cm.Parameters.Add(param[i]);
                    }
                }
                SqlDataReader rd = cm.ExecuteReader();

                if (rd.HasRows)
                {
                    RowList = DataReaderMapToList<T>(rd);
                    return RowList;

                }

                rd.Close();
            }
            catch (Exception ex)
            {
                AppLogEntry(spName);
                AppLogEntry("GetReaderData:-" + ex.Message.ToString());
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            RowList = null;
            return RowList;
        }

    }
}