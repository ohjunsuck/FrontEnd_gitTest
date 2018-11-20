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
    public partial class MakeWorks : System.Web.UI.Page
    {
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
                AddVersion();
            }
        }
        private void InitParameter()
        {
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


        protected void lnbSave_Click(object sender, EventArgs e)
        {

            string insertString = "INSERT INTO WorkContents (WC_State,WC_Priority,WC_Name,WC_Author,WC_Manager,WC_date";
            insertString += ",WC_Version,WC_Progress,WC_Type ,WC_Reply,WC_Contents,WC_NeedTime,WC_Completedate,WC_Files,WC_FileSize,WC_FileName)";
            insertString += "VALUES(@WC_State, @WC_Priority, @WC_Name, @WC_Author, @WC_Manager,@WC_date,@WC_Version,@WC_Progress,@WC_Type,@WC_Reply,@WC_Contents,@WC_NeedTime,@WC_Completedate,@WC_Files,@WC_FileSize,@WC_FileName)";


            DBConn conn = new DBConn();
            SqlCommand cmd = new SqlCommand(insertString, conn.GetConn());
      
            cmd.Parameters.AddWithValue("@WC_State", WC_State.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Priority", WC_Priority.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Name", txtTitle.Value);
            if (Session["UserName"] == null)
                cmd.Parameters.AddWithValue("@WC_Author", "없음");
            else
                cmd.Parameters.AddWithValue("@WC_Author", Session["UserName"]);
            cmd.Parameters.AddWithValue("@WC_Manager", DL_Manager.Text);
            cmd.Parameters.AddWithValue("@WC_date", biz_start_dt.Text);
            cmd.Parameters.AddWithValue("@WC_Version", DL_Version.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Progress", WC_Progress.SelectedValue);
            cmd.Parameters.AddWithValue("@WC_Type", DropDownList2.SelectedIndex); //이값을 이용해서 WorkContentsType에 이름을 넣자.
            cmd.Parameters.AddWithValue("@WC_Reply", 1);



            cmd.Parameters.AddWithValue("@WC_Contents", this.biz_MakeWork.Value);
            fileName = Server.MapPath("~/Uploads") + @"\" + fupAttachFile.FileName;


            cmd.Parameters.AddWithValue("@WC_NeedTime", NeedTime.Text);
            cmd.Parameters.AddWithValue("@WC_Completedate", biz_end_dt.Text);
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


            // WorkManager 테이블에 일감번호,누군지 기록!
            // AddWorkManagerarray 를돌면서 join문형태로 db에 등록!
            // WM_ID   WM_name   
            // 1       이봉수
            // 1       조기제
            // 2       류계정
            // 2       조규모

            //select ident_current('WorkContents')

            //현재일감번호 last_insert_id
            string connectionString =
            WebConfigurationManager.ConnectionStrings["Red"].ConnectionString;
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn2;
            conn2.Open();
            string SearchNowWorkId = "SELECT ident_current('WorkContents')";
            cmd2 = new SqlCommand(SearchNowWorkId, conn2);
            int nowWorkId =0;
            if (cmd2.ExecuteScalar() != null)
                nowWorkId = Convert.ToInt32(cmd2.ExecuteScalar());


            for (int i=0; i< AddWorkManagerarray.Count;++i)
            {
                insertString = "INSERT INTO WorkManager (WM_id,WM_name)";
                insertString += "VALUES(@WM_id, @WM_name)";

                conn = new DBConn();
                cmd = new SqlCommand(insertString, conn.GetConn());
                cmd.Parameters.AddWithValue("@WM_id", nowWorkId);
                cmd.Parameters.AddWithValue("@WM_name", AddWorkManagerarray[i].ToString()); //AddWorkManagerarray를돌면서 여러개 삽입
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

            }



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
