using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;

namespace Board22
{
    public class BoardDac : IDisposable
    {
        public void Dispose()
        {
        }
        public DataSet CreateAppBaseInfo(string serial_no,
                                         string writer,
                                         string password,
                                         string title,
                                         string message,
                                         string ref_id,
                                         string inner_id,
                                         string depth,
                                         string read_count,
                                         string del_flag,
                                         string reg_date,
                                         string delete_chid,
                                         out int totalCount,
                                         int topSize, int pageNum, string keyword)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("serial_no", SqlDbType.Int, 9);
            arParams[1] = new SqlParameter("total_count", SqlDbType.Int);
            arParams[2] = new SqlParameter("top_size", SqlDbType.Int);
            arParams[3] = new SqlParameter("page_num", SqlDbType.Int);
            arParams[4] = new SqlParameter("keyword", SqlDbType.NVarChar);
            //arParams[1] = new SqlParameter("writer", SqlDbType.VarChar, 50);
            //arParams[2] = new SqlParameter("password", SqlDbType.VarChar, 50);
            //arParams[3] = new SqlParameter("title", SqlDbType.VarChar, 250);
            //arParams[4] = new SqlParameter("message", SqlDbType.VarChar, 4000);
            //arParams[5] = new SqlParameter("ref_id", SqlDbType.Int, 30);
            //arParams[6] = new SqlParameter("inner_id", SqlDbType.Int, 20);
            //arParams[7] = new SqlParameter("depth", SqlDbType.Int, 10);
            //arParams[8] = new SqlParameter("read_count", SqlDbType.Int, 50);
            //arParams[9] = new SqlParameter("del_flag", SqlDbType.Char, 100);
            //arParams[10] = new SqlParameter("reg_date", SqlDbType.DateTime, 400);
            //arParams[11] = new SqlParameter("delete_chid", SqlDbType.Int, 1);
            arParams[0].Value = serial_no;

            arParams[1].Direction = ParameterDirection.Output;
            arParams[2].Value = topSize;
            arParams[3].Value = pageNum;
            arParams[4].Value = keyword;
            //arParams[1].Value = writer;
            //arParams[2].Value = password;
            //arParams[3].Value = title;
            //arParams[4].Value = message;
            //arParams[5].Value = ref_id;
            //arParams[6].Value = inner_id;
            //arParams[7].Value = depth;
            //arParams[8].Value = read_count;
            //arParams[9].Value = del_flag;
            //arParams[10].Value = reg_date;
            //arParams[11].Value = delete_chid;
            DataSet ds;

            using (SqlConnection cn = new SqlConnection(DBConnection.Gwcommon))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string

                ds = DBHelper.ExecuteDataset(DBConnection.Gwcommon, CommandType.StoredProcedure, "gpp_AppBaseInfo_GetList1", arParams);
                totalCount = Convert.ToInt32(arParams[1].Value);
                //return ExecuteDataset(cn, CommandType.StoredProcedure, "gpp_AppBaseInfo_GetList1", arParams);
            }



            //return DBHelper.ExecuteDataset(DBConnection.Gwcommon, CommandType.StoredProcedure, "gpp_AppBaseInfo_GetList1", arParams);
            //if (ds.Tables[0].Rows.Count == 1)
            //    return ds.Tables[0].Rows[0];
            //else
            //    return null;
            return ds;
        }


        public DataSet CreateReadBoardData(string serial_no)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("serial_no", SqlDbType.Int, 9);
            arParams[0].Value = serial_no;
          
    
            using (SqlConnection cn = new SqlConnection(DBConnection.Gwcommon))
            {
                cn.Open();
                return ExecuteDataset(cn, CommandType.StoredProcedure, "gpp_AppBaseInfo_GetList2", arParams);
            }

        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the SqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }
    }
}