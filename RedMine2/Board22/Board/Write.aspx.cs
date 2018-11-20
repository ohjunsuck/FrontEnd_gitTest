using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.Security;

namespace Board22.Board
{
    public partial class Write : System.Web.UI.Page
    {
        public static string sn;
        public string ViewData { get; set; }
        public BoardDac appDac;
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            //작성자 // 현재날짜 셋팅하고
            //제목, 내용 쓴거 submit하면 db에 저장하고 다시 게시판 뒤로가서 생성된거 뿌리기!


            ViewData = System.Web.HttpContext.Current.Request.Form["Submit1"];
            if (ViewData == "쓰기")
            {
                //바뀐 title, contents를 db에서 set한후 다시 List.aspx로 리다이렉팅

                WriteBoardContent();
                Response.Redirect("List.aspx");
                //Session["UserID"] = m_Password;
                //Session["UserName"] = m_ID;
                //Response.Redirect("List.aspx?loginid"+ Session["UserName"] + "&loginPassword=" + Session["UserID"]);
            }

            // sn즉 1012로 조회해서 넣기!
            // Request["sn"]; 로 넣고 디비에서 데이터 읽어와서 변수에다 넣으면 끝!
            BindingList();
            if (!IsPostBack)
            {

            }
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //form태그안에 runat=server 포함해야합니다의 에러문 해결법.
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        private void BindingList()
        {
            if(Session["UserName"]!=null)
            writer = Session["UserName"].ToString();
            else
            {
                //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert(" + "로그인을하시오" + ")</SCRIPT>");
                //Top form = new Form2();
                //form.Show();
                
                Response.Redirect("List.aspx");
            }
          
        }
        //private void ShowMessage(string Message, string Title)
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", Message, Title), true);
        //}

        private void WriteBoardContent()
        {

            string insertString = "INSERT INTO board (writer, password, title, message, ";
            insertString += "ref_id, inner_id, depth, read_count, del_flag, reg_date) ";
            insertString += "VALUES(@writer, @password, @title, @message, 0,0,0,0, ";
            insertString += "'N', GETDATE())";

            string updateString = "UPDATE board SET ref_id = serial_no WHERE ref_id = 0";

            //string hashedPassword =
            //    FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");

            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(insertString, conn.GetConn());
            cmd.Parameters.AddWithValue("@writer", System.Web.HttpContext.Current.Request.Form["editwriter"].ToString());
            cmd.Parameters.AddWithValue("@password", System.Web.HttpContext.Current.Request.Form["writepassword"].ToString());
            cmd.Parameters.AddWithValue("@title", System.Web.HttpContext.Current.Request.Form["edittitle"].ToString());
            cmd.Parameters.AddWithValue("@message", System.Web.HttpContext.Current.Request.Form["txtMessage"].ToString());


            try
            {
                cmd.ExecuteNonQuery();
                conn.ExecuteNonQuery(updateString);
            }
            catch (Exception error)
            {
                Response.Write(error.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}