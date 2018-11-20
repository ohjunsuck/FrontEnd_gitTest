using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Board22.UserControl
{
  
    public partial class Top : System.Web.UI.UserControl
    {
        public static bool m_bisLogin { get; set; }

        public string m_sLoginID { get; set; }

        public string ViewData { get; set; }

        public string m_ID { get; set; }
        public string m_Password { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                m_ID = System.Web.HttpContext.Current.Request.Form["loginid"];
                m_Password = System.Web.HttpContext.Current.Request.Form["loginPassword"];

            }
            else
            {
                m_ID = Session["UserName"].ToString();
                m_Password = Session["UserID"].ToString();
            }

            if (!IsPostBack)
            {
                m_bisLogin = true;

            }

            string connectionString =
            WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlConnection conn2 = new SqlConnection(connectionString);
            // 1. 가입된 사용자인지를 확인
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText =
               "SELECT * FROM Account WHERE AC_Name='" + m_ID + "'";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            bool hasSubscribed = rd.Read();
            rd.Close();
            // 가입된 사용자가 아닐 경우 Exception을 발생시켜 catch 문으로 이동
            //if (!hasSubscribed)
            //    throw new System.InvalidOperationException("가입된 아이디가 아닙니다.");

            // 가입됬을경우 해당 ID의 비밀번호가 맞으면 로그인하기!
            // 파라미터를 사용 왜써야하는지 마음대로 db조회할수 있기 때문에 유저가 쓰면 안된다.
            string SearchPasswordSQL = "SELECT AC_Password FROM ACCOUNT WHERE AC_Name ='" + m_ID + "'";


            cmd = new SqlCommand(SearchPasswordSQL, conn);
            string tempstring = (string)cmd.ExecuteScalar();

            // 현재로그인한 계정이 admin계정인지 확인하기.
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.Connection = conn2;
            //conn2.Open();
            //string SearchadminSQL = "SELECT AC_bAdmin FROM Account WHERE AC_Name ='" + m_ID + "'";
            //cmd2 = new SqlCommand(SearchadminSQL, conn2);
            object adminbool = "false";
            //if (cmd2.ExecuteScalar() != null)
            //    adminbool = cmd2.ExecuteScalar();

           // if (adminbool.ToString() != "false")
                Session["ACCOUNTAddmin"] = adminbool;



           // if (!IsPostBack)
            {
                if (m_Password == tempstring && tempstring !=null)  //여기에  session 값이 있으면 무조건 와야하는데... 
                {

                    Session["UserID"] = m_Password;
                    Session["UserName"] = m_ID;
                    //쿠키사용
                    Response.Cookies["UserID"].Value = m_Password;
                    Response.Cookies["UserName"].Value = m_ID;
                    Response.Cookies["UserID"].Expires = DateTime.Now.AddHours(1);

                    m_sLoginID = m_ID + "님 반갑습니다!";
                    m_bisLogin = false;
                }
            }

            if (Session["UserName"] != null)
            {
                Session["UserID"] = m_Password;
                Session["UserName"] = m_ID;
                //쿠키사용
                Response.Cookies["UserID"].Value = m_Password;
                Response.Cookies["UserName"].Value = m_ID;
                Response.Cookies["UserID"].Expires = DateTime.Now.AddHours(1);

                m_sLoginID = m_ID + "님 반갑습니다!";
                m_bisLogin = false;
            }



            ViewData = System.Web.HttpContext.Current.Request.Form["Submit1"];

            if (ViewData == "로그아웃")
            {
                m_bisLogin = true;
                Session["UserID"] = "";
                Session["UserName"] = "";
                Session["UserName"] = null;
                m_ID = null;
            }


            conn.Close();
            conn2.Close();
        }
    }

}