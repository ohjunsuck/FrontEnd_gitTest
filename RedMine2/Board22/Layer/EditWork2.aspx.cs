using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Data;
using System.Collections;
using System.Configuration;


namespace Board22.Layer
{
    public partial class EditWork2 : System.Web.UI.Page
    {
        public int WC_id;
        public string WC_State2;
        public string WC_Priority2;
        public string WC_Name2;
        public string WC_Author;
        public string WC_Manager;
        public DateTime WC_date;
        public string WC_Version;
        public int WC_Progress2;
        public int WC_Type;
        public int WC_Reply;
        public string WC_Contents;
        public string WC_NeedTime;
        public string WC_Completedate;
        public string WC_Files;
        public int WC_FileSize;
        public string WC_FileName;
        public string[] AttachFiles { get; set; }

        ////
        public string fileName;
        public string sJSON;
        public ArrayList WorkManagerarray = new ArrayList();
        public static ArrayList AddWorkManagerarray = new ArrayList();
        public List<string> Versionlist = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitParameter();
            BindingList(); //매니저 리스트 바인딩
          

            if (!IsPostBack)
            {
                BindWorkContents(); //일감정보 바인딩

                AddVersion();
            }
        }
        private void InitParameter()
        {
            WC_id = Convert.ToInt32(Request.QueryString["sn"]);

        }

        private void AddVersion()
        {
            // 현재 가지고있는 버전정보 확인해서  DL_Version 드랍다운에 넣기
            SqlCommand cmd = new SqlCommand();
            string UpdateString = "";
            UpdateString = "SELECT VS_name  FROM Versions ";
            DBConn con = new DBConn();
            cmd = new SqlCommand(UpdateString, con.GetConn());
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Versionlist.Add(reader.GetValue(0).ToString());
                    DL_Version.Items.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
            }
            catch (Exception error)
            {
                Response.Write(error.ToString());
            }
            finally
            {
                con.Close();
            }

            DL_Version.SelectedValue = WC_Version;
        }
        private void BindWorkContents()
        {
           

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
                    WC_State2 = rows[0]["WC_State"].ToString();
                    WC_Priority2 = rows[0]["WC_Priority"].ToString();
                    WC_Name2 = rows[0]["WC_Name"].ToString();
                    WC_Author = rows[0]["WC_Author"].ToString();
                    WC_Manager = rows[0]["WC_Manager"].ToString();
                    WC_date = Convert.ToDateTime(rows[0]["WC_date"].ToString());

                    WC_Version = rows[0]["WC_Version"].ToString();
                    WC_Progress2 = Convert.ToInt32(rows[0]["WC_Progress"].ToString());
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
                    switch (WC_State2)
                    {
                        case "버그": WC_State.SelectedValue = "버그"; break;
                        case "업무": WC_State.SelectedValue = "업무"; break;
                        case "건의": WC_State.SelectedValue = "건의"; break;
                        case "기능": WC_State.SelectedValue = "기능"; break;
                        case "지원": WC_State.SelectedValue = "지원"; break;
                        case "잡일": WC_State.SelectedValue = "잡일"; break;
                        case "운영관리": WC_State.SelectedValue = "운영관리"; break;
                        case "변경점": WC_State.SelectedValue = "변경점"; break;
                        case "자산": WC_State.SelectedValue = "자산"; break;
                        case "크래시": WC_State.SelectedValue = "크래시"; break;
                    }
                    txtTitle.Text = WC_Name2;  //서버에서 이렇게 뿌리지 말고 음...
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
                    switch (WC_Priority2)
                    {
                        case "낮음": WC_Priority.SelectedValue = "낮음"; break;
                        case "보통": WC_Priority.SelectedValue = "보통"; break;
                        case "중요": WC_Priority.SelectedValue = "중요"; break;
                        case "높음": WC_Priority.SelectedValue = "높음"; break;
                        case "긴급": WC_Priority.SelectedValue = "긴급"; break;
                    }
                    DL_Manager.Text = WC_Manager;

                    //switch (WC_Version)
                    //{
                    //    case 1: DL_Version.SelectedValue = "1"; break;
                    //    case 2: DL_Version.SelectedValue = "2"; break;
                    //}
                    NeedTime.Text = WC_NeedTime;
                    biz_start_dt.Text = WC_date.ToString();
                    biz_end_dt.Text = WC_Completedate;

                    switch (WC_Progress2)
                    {
                        case 0: WC_Progress.SelectedValue = "0"; break;
                        case 10: WC_Progress.SelectedValue = "10"; break;
                        case 20: WC_Progress.SelectedValue = "20"; break;
                        case 30: WC_Progress.SelectedValue = "30"; break;
                        case 40: WC_Progress.SelectedValue = "40"; break;
                        case 50: WC_Progress.SelectedValue = "50"; break;
                        case 60: WC_Progress.SelectedValue = "60"; break;
                        case 70: WC_Progress.SelectedValue = "70"; break;
                        case 80: WC_Progress.SelectedValue = "80"; break;
                        case 90: WC_Progress.SelectedValue = "90"; break;
                        case 100: WC_Progress.SelectedValue = "100"; break;
                    }
                    //fileName = Server.MapPath("~/Uploads") + @"\" + WC_FileName;
                    //FileUpload1.SaveAs(WC_Files + WC_FileName);
                    //ir_MakeWork. WC_Contents

                    // ir_MakeWork.Value = WC_Contents;
                    biz_start_dt.Text = WC_date.ToString();
                    biz_end_dt.Text = WC_Completedate.ToString();

                }

            }
        }
        private void BindingList()
        {
            string connectionString =
           WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText =
             "SELECT AC_HangulName FROM Account";
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
                for (int i = 0; i < rows.Length; ++i)
                    WorkManagerarray.Add(rows[i].ItemArray[0].ToString());

                System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
              new System.Web.Script.Serialization.JavaScriptSerializer();
                sJSON = oSerializer.Serialize(WorkManagerarray);
            }
        }


        protected void lnbEdit_Click(object sender, EventArgs e)
        {
            string insertString = "";
            //string insertString = "INSERT INTO WorkContents (WC_State,WC_Priority,WC_Name,WC_Author,WC_Manager,WC_date";
            //insertString += ",WC_Version,WC_Progress,WC_Type ,WC_Reply,WC_Contents,WC_NeedTime,WC_Completedate,WC_Files,WC_FileSize,WC_FileName)";
            //insertString += "VALUES(@WC_State, @WC_Priority, @WC_Name, @WC_Author, @WC_Manager,@WC_date,@WC_Version,@WC_Progress,@WC_Type,@WC_Reply,@WC_Contents,@WC_NeedTime,@WC_Completedate,@WC_Files,@WC_FileSize,@WC_FileName)";

            string UpdateString = "update WorkContents Set WC_STATE=@WC_State,WC_Priority=@WC_Priority,WC_Name=@WC_Name,WC_Author=@WC_Author,WC_Manager=@WC_Manager,WC_date=@WC_date" +
             ",WC_Version=@WC_Version,WC_Progress=@WC_Progress,WC_Type=@WC_Type,WC_Reply=@WC_Reply,WC_Contents=@WC_Contents,WC_NeedTime=@WC_NeedTime,WC_Completedate=@WC_Completedate," +
             "WC_Files=@WC_Files,WC_FileSize=@WC_FileSize,WC_FileName=@WC_FileName where wc_id=" + WC_id.ToString();

            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(UpdateString, conn.GetConn());

            cmd.Parameters.AddWithValue("@WC_State", WC_State.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Priority", WC_Priority.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Name", txtTitle.Text);
            if (Session["UserName"] == null)
                cmd.Parameters.AddWithValue("@WC_Author", "없음");
            else
                cmd.Parameters.AddWithValue("@WC_Author", Session["UserName"]);
            cmd.Parameters.AddWithValue("@WC_Manager", DL_Manager.Text);
            cmd.Parameters.AddWithValue("@WC_date", DateTime.Parse(biz_start_dt.Text));
            cmd.Parameters.AddWithValue("@WC_Version", DL_Version.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Progress", WC_Progress.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Type", DropDownList2.SelectedIndex); //이값을 이용해서 WorkContentsType에 이름을 넣자.
            cmd.Parameters.AddWithValue("@WC_Reply", 1);



            cmd.Parameters.AddWithValue("@WC_Contents", this.biz_MakeWork.Value);
            fileName = Server.MapPath("~/Uploads") + @"\" + fupAttachFile.FileName;


            cmd.Parameters.AddWithValue("@WC_NeedTime", NeedTime.Text);
            cmd.Parameters.AddWithValue("@WC_Completedate", DateTime.Parse(biz_end_dt.Text));
            cmd.Parameters.AddWithValue("@WC_Files", hdfAttachFile.Value);
            cmd.Parameters.AddWithValue("@WC_FileSize", fupAttachFile.PostedFile.ContentLength);
            cmd.Parameters.AddWithValue("@WC_FileName", fupAttachFile.FileName);

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

                //여기부터잡자
                string fileName = "";
                fileName = Path.GetFileName(fupAttachFile.PostedFile.FileName);
                if (fileName != "")
                {
                    fupAttachFile.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                    Response.Write("File uploaded sucessfully.");
                }
            }


            // 일감 관람자 테이블 삽입이 아니고 변경!
            //string connectionString =
            //WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
            //SqlConnection conn2 = new SqlConnection(connectionString);
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.Connection = conn2;
            //conn2.Open();
            //string SearchNowWorkId = "SELECT ident_current('WorkContents')";
            //cmd2 = new SqlCommand(SearchNowWorkId, conn2);
            //int nowWorkId = 0;
            //if (cmd2.ExecuteScalar() != null)
            //    nowWorkId = Convert.ToInt32(cmd2.ExecuteScalar());


            //for (int i = 0; i < AddWorkManagerarray.Count; ++i)
            //{
            //    insertString = "INSERT INTO WorkManager (WM_id,WM_name)";
            //    insertString += "VALUES(@WM_id, @WM_name)";

            //    conn = new DBConn();
            //    cmd = new SqlCommand(insertString, conn.GetConn());
            //    cmd.Parameters.AddWithValue("@WM_id", nowWorkId);
            //    cmd.Parameters.AddWithValue("@WM_name", AddWorkManagerarray[i].ToString()); //AddWorkManagerarray를돌면서 여러개 삽입
            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception error)
            //    {
            //        Response.Write(error.ToString());
            //    }
            //    finally
            //    {
            //        conn.Close();
            //    }

            //}

            Response.Redirect("Works.aspx");
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            HttpFileCollection hfc = Request.Files; for (int i = 0; i < hfc.Count; i++)
            { HttpPostedFile hpf = hfc[i]; if (hpf.ContentLength > 0) { hpf.SaveAs(@"C:\Users\Administrator\Downloads\FILES\" + hpf.FileName); } }

        }


        ////
        protected void addworkmanager(object sender, EventArgs e)
        {
            AddWorkManagerarray.Add(autocomplete.Value);
            // WorkManager 테이블에 일감번호,누군지 기록!
        }
        protected void MakeVersion(object sender, EventArgs e)
        {
            // Versions 에 insert하기.
            string insertString = "INSERT INTO Versions (VS_name,VS_Contents,VS_State,VS_wikiPage,VS_DateTime,VS_Groupping)";
            insertString += "VALUES(@VS_name, @VS_Contents, @VS_State, @VS_wikiPage, @VS_DateTime,@VS_Groupping)";

            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(insertString, conn.GetConn());

            cmd.Parameters.AddWithValue("@VS_name", versionname.Value.ToString());
            cmd.Parameters.AddWithValue("@VS_Contents", modalexplnation.Value.ToString());
            cmd.Parameters.AddWithValue("@VS_State", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@VS_wikiPage", modalwikipage.Value.ToString());
            cmd.Parameters.AddWithValue("@VS_DateTime", modaldatepicker.Text);
            cmd.Parameters.AddWithValue("@VS_Groupping", DropDownList3.SelectedValue);
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
            }
            Response.Redirect("MakeWorks.aspx");
        }
        #region 첨부파일 추가
        protected void lnbAddAttachFile_Click(object sender, EventArgs e)
        {
            if (fupAttachFile.HasFile)
            {
                string path = ConfigurationManager.AppSettings["UploadData"];
                DateTime today = DateTime.Now;
                string year = today.Year.ToString();
                string month = (today.Month < 10) ? "0" + today.Month.ToString() : today.Month.ToString();
                string day = (today.Day < 10) ? "0" + today.Day.ToString() : today.Day.ToString();
                string second = (today.Second < 10) ? "0" + today.Second.ToString() : today.Second.ToString();
                string dateSeq = year + month + day + second + today.Millisecond.ToString();
                string saveFileName = dateSeq + "_" + fupAttachFile.FileName.Replace(" ", "");

                try
                {
                    path += "Board/" + year + "/" + month + "/" + day + "/";
                    if (!Directory.Exists(Server.MapPath(path)))
                        Directory.CreateDirectory(Server.MapPath(path));

                    if (fupAttachFile.PostedFile.ContentLength >= 104857600) // 100 MB
                    {
                        Utility.ScriptEndExecAlert(this.Page, "100MB이상의 파일은 올릴 수 없습니다.");
                    }
                    else
                    {// Upload
                        fupAttachFile.SaveAs(Server.MapPath(path + saveFileName));

                        //리스트 추가
                        this.lstAttachFile.Items.Add(
                            new ListItem(
                                fupAttachFile.FileName.Replace(" ", "") + "(" + Utility.GetFileSize(fupAttachFile.PostedFile.ContentLength) + ")",
                                fupAttachFile.PostedFile.ContentLength.ToString())
                                );
                        this.spanFileCount.InnerText = this.lstAttachFile.Items.Count.ToString();

                        decimal size = 0;
                        foreach (ListItem li in this.lstAttachFile.Items)
                            size += Convert.ToDecimal(li.Value);

                        this.spanFileSize.InnerText = Utility.GetFileSize(size);

                        if (this.hdfAttachFile.Value == string.Empty)
                            this.hdfAttachFile.Value = path + saveFileName + ";" + size.ToString();
                        else
                            this.hdfAttachFile.Value += "|" + path + saveFileName + ";" + size.ToString();

                        //System.Web.HttpContext.Current.Response.Write(this.hdfAttachFile.Value);
                        hdfAttachFile.Value = this.hdfAttachFile.Value;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ScriptEndExecAlert(this.Page, ex.Message);

                }

            }
            else
            {

            }
        }
        #endregion

        #region 첨부파일 삭제
        protected void lnbDelAttachFile_Click(object sender, EventArgs e)
        {
            int selectedIdx = this.lstAttachFile.SelectedIndex;

            string orgFile = this.hdfAttachFile.Value;

            string[] fileList = orgFile.Split('|');

            for (int i = 0; i < fileList.Length; i++)
            {
                fileList[i] = (fileList[i].Split(';'))[0];
            }

            try
            {
                if (File.Exists(Server.MapPath(fileList[selectedIdx])))
                {
                    File.Delete(Server.MapPath(fileList[selectedIdx]));
                    orgFile = "";

                    for (int i = 0; i < fileList.Length; i++)
                    {
                        if (i != selectedIdx)
                        {
                            if (orgFile == string.Empty)
                                orgFile = fileList[i] + ";" + this.lstAttachFile.Items[i].Value;
                            else
                                orgFile += "|" + fileList[i] + ";" + this.lstAttachFile.Items[i].Value;
                        }
                    }

                    this.lstAttachFile.Items.RemoveAt(selectedIdx);
                    this.spanFileCount.InnerText = this.lstAttachFile.Items.Count.ToString();

                    decimal size = 0;
                    foreach (ListItem li in this.lstAttachFile.Items)
                        size += Convert.ToDecimal(li.Value);

                    this.spanFileSize.InnerText = Utility.GetFileSize(size);
                    this.hdfAttachFile.Value = orgFile;
                }
                else
                    Utility.ScriptEndExecAlert(this.Page, "파일이 없습니다.");


            }
            catch (Exception ex)
            {
                Utility.ScriptEndExecAlert(this.Page, ex.Message);
            }
        }
        #endregion
    }

}
