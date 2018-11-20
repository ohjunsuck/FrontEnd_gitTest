using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Web.Configuration;
using System.Data;

namespace Board22.Board
{
    public partial class Edit : System.Web.UI.Page
    {

        public static string sn;
        public string ViewData { get; set; }
        public BoardDac appDac;
        public static string writer { get; set; }
        public string time { get; set; }
        public string title { get; set; }
        public string contents { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewData = System.Web.HttpContext.Current.Request.Form["Submit1"];
            if(ViewData == "수정")
            {
                //바뀐 title, contents를 db에서 set한후 다시 List.aspx로 리다이렉팅

                EditBoardContents();
                Response.Redirect("List.aspx");
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

            DataTable dtList = null;

            using (appDac = new BoardDac())
            {

                DataSet ds = null;
                if (Request["sn"] != null)
                    ds = appDac.CreateReadBoardData(Request["sn"].ToString());
                else
                    ds = appDac.CreateReadBoardData("1");

                dtList = ds.Tables[0];
                sn = Request["sn"].ToString();
            }

            if (dtList != null)
            {
                DataRow[] rows = dtList.Select();

             
                if (Request["sn"] != null)
                {
                    writer = rows[0]["writer"].ToString();
                    time = rows[0]["reg_date"].ToString();
                    title = rows[0]["title"].ToString();
                    contents = rows[0]["message"].ToString();
                }
            }
        }


        private void EditBoardContents()
        {
          
            string updateString = "UPDATE board SET writer=@writer, ";
            updateString += "title=@title, message=@message ";
            updateString += "WHERE serial_no=" + sn;

            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(updateString, conn.GetConn());

            cmd.Parameters.AddWithValue("@writer", writer);
            cmd.Parameters.AddWithValue("@title", System.Web.HttpContext.Current.Request.Form["edittitle"].ToString());
            cmd.Parameters.AddWithValue("@message", System.Web.HttpContext.Current.Request.Form["txtMessage"].ToString());

            //이걸 runat="server"로 보내지못하니깐 새로쓴 내용을 서버에서 읽어와야한다!

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}