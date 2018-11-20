using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace Board22
{
    public class DBConn
    {
        // 1. Database Connection 정보
        string connectionString =
            WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
        public SqlConnection dbConn;

        // 2. 생성자를 이용하여 생성과 동시에 데이터 접속
        public DBConn()
        {
            dbConn = new SqlConnection(connectionString);
            dbConn.Open();
        }

        // 3. DB 연결 닫기
        public void Close()
        {
            dbConn.Close();
        }

        // 4. 연결 객체 반환 메서드
        public SqlConnection GetConn()
        {
            return dbConn;
        }

        // 5. ExecuteNonQuery 실행
        // 매개 변수를 이용하지 않는 UPDATE, DELETE, INSERT 문에서 사용
        public void ExecuteNonQuery(string queryString)
        {
            SqlCommand cmd = new SqlCommand(queryString, dbConn);
            cmd.ExecuteNonQuery();
        }

        // 6. ExecuteReader 실행
        // 매개 변수를 이용하지 않는 SELECT 문에서 사용
        public SqlDataReader ExecuteReader(string queryString)
        {
            SqlCommand cmd = new SqlCommand(queryString, dbConn);
            return cmd.ExecuteReader();
        }

        // 7. ExecuteScalar 실행
        // 첫 행의 첫 열을 객체로 반환
        public object ExecuteScalar(string queryString)
        {
            SqlCommand cmd = new SqlCommand(queryString, dbConn);
            return cmd.ExecuteScalar();
        }
    }
}
