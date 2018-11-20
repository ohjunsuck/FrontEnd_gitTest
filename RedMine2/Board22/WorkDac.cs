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
    public class WorkDac : IDisposable
    {
        public void Dispose()
        {
        }
        public DataSet CreateAppBaseInfo(string serial_no, out int totalCount,
                                         int topSize, int pageNum, string keyword)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("serial_no", SqlDbType.Int, 9);
            arParams[1] = new SqlParameter("total_count", SqlDbType.Int);
            arParams[2] = new SqlParameter("top_size", SqlDbType.Int);
            arParams[3] = new SqlParameter("page_num", SqlDbType.Int);
            arParams[4] = new SqlParameter("keyword", SqlDbType.NVarChar);
         
            arParams[0].Value = serial_no;

            arParams[1].Direction = ParameterDirection.Output;
            arParams[2].Value = topSize;
            arParams[3].Value = pageNum;
            arParams[4].Value = keyword;
       
            DataSet ds;

            using (SqlConnection cn = new SqlConnection(DBConnection.Gwcommon))
            {
                cn.Open();

                ds = DBHelper.ExecuteDataset(DBConnection.Gwcommon, CommandType.StoredProcedure, "WorkGetList", arParams);
                totalCount = Convert.ToInt32(arParams[1].Value);
             
            }


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