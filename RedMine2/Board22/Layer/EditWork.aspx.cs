using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.IO;


namespace Board22.Layer
{
    public partial class EditWork : System.Web.UI.Page
    {
        string fileName;

        public int WC_id;
        public string WC_State;
        public string WC_Priority;
        public string WC_Name;
        public string WC_Author;
        public string WC_Manager;
        public DateTime WC_date;
        public string WC_Version;
        public int WC_Progress;
        public int WC_Type;
        public int WC_Reply;
        public string WC_Contents;
        public string WC_NeedTime;
        public string WC_Completedate;
        public string WC_Files;
        public int WC_FileSize;
        public string WC_FileName;
        public string[] AttachFiles { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Convert.ToInt32(Request.QueryString["sn"]);
            BindingList(); //일감정보 바인딩
            if (!IsPostBack)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                fileName = Server.MapPath("~/Uploads") + @"\" + FileUpload1.FileName;
                FileUpload1.SaveAs(fileName);
                Label1.Text = "<br />파일명 : " + FileUpload1.FileName + "<br />";
                Label1.Text += "콘텐츠 타입 : " + FileUpload1.PostedFile.ContentType + "<br />";
                Label1.Text += "파일 크기 : " + FileUpload1.PostedFile.ContentLength + " Byte";
            }
            else
            {
                Label1.Text = "업로드할 파일을 선택하지 않았습니다.";
            }
        }
        protected void EditText(object sender, EventArgs e)
        {

            txtTitle.Text = Server.HtmlEncode(txtTitle.Text);

        }
            protected void lnbEdit_Click(object sender, EventArgs e)
        {

            //string insertString = "INSERT INTO WorkContents (WC_State,WC_Priority,WC_Name,WC_Author,WC_Manager,WC_date";
            //insertString += ",WC_Version,WC_Progress,WC_Type ,WC_Reply,WC_Contents,WC_NeedTime,WC_Completedate,WC_Files,WC_FileSize,WC_FileName)";
            //insertString += "VALUES(@WC_State, @WC_Priority, @WC_Name, @WC_Author, @WC_Manager,GETDATE(),@WC_Version,@WC_Progress,@WC_Type,@WC_Reply,@WC_Contents,@WC_NeedTime,@WC_Completedate,@WC_Files,@WC_FileSize,@WC_FileName)";

            string UpdateString = "update WorkContents Set WC_STATE=@WC_State,WC_Priority=@WC_Priority,WC_Name=@WC_Name,WC_Author=@WC_Author,WC_Manager=@WC_Manager,WC_date=@WC_date" +
                ",WC_Version=@WC_Version,WC_Progress=@WC_Progress,WC_Type=@WC_Type,WC_Reply=@WC_Reply,WC_Contents=@WC_Contents,WC_NeedTime=@WC_NeedTime,WC_Completedate=@WC_Completedate," +
                "WC_Files=@WC_Files,WC_FileSize=@WC_FileSize,WC_FileName=@WC_FileName where wc_id="+ WC_id.ToString();

            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(UpdateString, conn.GetConn());
            cmd.Parameters.AddWithValue("@WC_State", WC_State1.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Priority", WC_Priority1.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Name", txtTitle.Text);
            if(Session["UserName"]== null)
                cmd.Parameters.AddWithValue("@WC_Author", WC_Author);
            else
            cmd.Parameters.AddWithValue("@WC_Author", Session["UserName"]);
            cmd.Parameters.AddWithValue("@WC_Manager", WC_Manager1.Text);
            cmd.Parameters.AddWithValue("@WC_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@WC_Version", DL_Version.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Progress", WC_Progress1.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Type", DropDownList2.SelectedIndex);
            cmd.Parameters.AddWithValue("@WC_Reply", 1);


            cmd.Parameters.AddWithValue("@WC_Contents", this.biz_MakeWork.Value);


            if (FileUpload1.FileName != "")
                fileName = Server.MapPath("~/Uploads") + @"\" + FileUpload1.FileName;
            else
                fileName = Server.MapPath("~/Uploads") + @"\" + "708097.jpg";

            cmd.Parameters.AddWithValue("@WC_NeedTime", NeedTime.Text);
            cmd.Parameters.AddWithValue("@WC_Completedate", DateTime.Now);
            cmd.Parameters.AddWithValue("@WC_Files", fileName);
            cmd.Parameters.AddWithValue("@WC_FileSize", FileUpload1.PostedFile.ContentLength);
            if (FileUpload1.FileName != "")
                cmd.Parameters.AddWithValue("@WC_FileName", FileUpload1.FileName);
            else
                cmd.Parameters.AddWithValue("@WC_FileName", "708097.jpg");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Response.Write(error.ToString());
            }
            finally
            {
                conn.Close();

                string fileName = "";
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (fileName != "")
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                    Response.Write("File uploaded sucessfully.");
                }

            }
            Response.Redirect("Works.aspx");
        }

        private void BindingList()
        {
            //번호(sn)받은걸로 일감 정보들 갱신.
            WC_id = Convert.ToInt32(Request.QueryString["sn"]);


            string connectionString =
            WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlConnection conn2 = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText =
             "SELECT * FROM WorkContents WHERE WC_id='" + WC_id + "'";
            conn.Open();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dtList = null;

            da.Fill(ds);

            cmd.Parameters.Clear();


            dtList = ds.Tables[0];

            if (dtList != null)
            {
                DataRow[] rows = dtList.Select();

            
                if (WC_id != 0)
                {
                    WC_id = Convert.ToInt32(rows[0]["WC_id"]);
                    WC_State = rows[0]["WC_State"].ToString();
                    WC_Priority = rows[0]["WC_Priority"].ToString();
                    WC_Name = rows[0]["WC_Name"].ToString();
                    WC_Author = rows[0]["WC_Author"].ToString();
                    WC_Manager = rows[0]["WC_Manager"].ToString();
                    WC_date = Convert.ToDateTime(rows[0]["WC_date"].ToString());

                    WC_Version = rows[0]["WC_Version"].ToString();
                    WC_Progress = Convert.ToInt32(rows[0]["WC_Progress"].ToString());
                    WC_Type = Convert.ToInt32(rows[0]["WC_Type"]);
                    WC_Reply = Convert.ToInt32(rows[0]["WC_Reply"].ToString());
                    WC_Contents = rows[0]["WC_Contents"].ToString();
                    WC_NeedTime = rows[0]["WC_NeedTime"].ToString();
                    WC_Completedate = rows[0]["WC_Completedate"].ToString();
                    WC_Files = rows[0]["WC_Files"].ToString();
                    if (!DBNull.Value.Equals(rows[0]["WC_FileSize"]))
                        WC_FileSize = Convert.ToInt32(rows[0]["WC_FileSize"]);
                    WC_FileName = rows[0]["WC_FileName"].ToString();
                    AttachFiles = WC_Files.Split('|');

                    //각 항목들 넣기!
                    switch (WC_State)
                    {
                        case "버그": WC_State1.SelectedValue = "버그"; break;
                        case "업무": WC_State1.SelectedValue = "업무"; break;
                        case "건의": WC_State1.SelectedValue = "건의"; break;
                        case "기능": WC_State1.SelectedValue = "기능"; break;
                        case "지원": WC_State1.SelectedValue = "지원"; break;
                        case "잡일": WC_State1.SelectedValue = "잡일"; break;
                        case "운영관리": WC_State1.SelectedValue = "운영관리"; break;
                        case "변경점": WC_State1.SelectedValue = "변경점"; break;
                        case "자산": WC_State1.SelectedValue = "자산"; break;
                        case "크래시": WC_State1.SelectedValue = "크래시"; break;
                    }
                    //txtTitle.Text = WC_Name;  //서버에서 이렇게 뿌리지 말고 음...
                    switch (WC_Type)
                    {
                        case 1: DropDownList2.SelectedValue = "새 일감"; break;
                        case 2: DropDownList2.SelectedValue = "진행중"; break;
                        case 3: DropDownList2.SelectedValue = "확인 요망"; break;
                        case 4: DropDownList2.SelectedValue = "QA중"; break;
                        case 5: DropDownList2.SelectedValue = "완료"; break;
                        case 6: DropDownList2.SelectedValue = "재발생"; break;
                        case 7: DropDownList2.SelectedValue = "지속"; break;
                        case 8: DropDownList2.SelectedValue = "폐기"; break;
                        case 9: DropDownList2.SelectedValue = "보류"; break;
                    }
                    switch (WC_Priority)
                    {
                        case "낮음": WC_Priority1.SelectedValue = "낮음"; break;
                        case "보통": WC_Priority1.SelectedValue = "보통"; break;
                        case "중요": WC_Priority1.SelectedValue = "중요"; break;
                        case "높음": WC_Priority1.SelectedValue = "높음"; break;
                        case "긴급": WC_Priority1.SelectedValue = "긴급"; break;
                    }
                    WC_Manager1.Text = WC_Manager;
                    //switch (WC_Version)
                    //{
                    //    case 1: DL_Version.SelectedValue = "1"; break;
                    //    case 2: DL_Version.SelectedValue = "2"; break;
                    //}
                    NeedTime.Text = WC_NeedTime;
                    biz_start_dt.Text = WC_date.ToString();
                    biz_end_dt.Text = WC_Completedate;

                    switch (WC_Progress)
                    {
                        case 0: WC_Progress1.SelectedValue = "0";break;
                        case 10: WC_Progress1.SelectedValue = "10"; break;
                        case 20: WC_Progress1.SelectedValue = "20"; break;
                        case 30: WC_Progress1.SelectedValue = "30"; break;
                        case 40: WC_Progress1.SelectedValue = "40"; break;
                        case 50: WC_Progress1.SelectedValue = "50"; break;
                        case 60: WC_Progress1.SelectedValue = "60"; break;
                        case 70: WC_Progress1.SelectedValue = "70"; break;
                        case 80: WC_Progress1.SelectedValue = "80"; break;
                        case 90: WC_Progress1.SelectedValue = "90"; break;
                        case 100: WC_Progress1.SelectedValue = "100"; break;
                    }
                    //fileName = Server.MapPath("~/Uploads") + @"\" + WC_FileName;
                    //FileUpload1.SaveAs(WC_Files + WC_FileName);
                    //ir_MakeWork. WC_Contents
                    ir_MakeWork.Value = WC_Contents;

                }

            }
        }
    }
}