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
using System.Collections;


namespace Board22.Layer
{
    public partial class Work : System.Web.UI.Page
    {

        public int WC_id;
        public string WC_State;
        public string WC_Priority;
        public string WC_Name;
        public string WC_Author;
        public string WC_Manager;
        public string WC_date;
        public string WC_Version;
        public int WC_Progress;
        public int WC_Type;
        public string WC_Typestring;
        public int WC_Reply;
        public string WC_Contents;
        public string WC_NeedTime;
        public string WC_Completedate;
        public string WC_Files;
        public int WC_FileSize;
        public string WC_FileName;
        public string[] AttachFiles { get; set; }
        public List<string> WorkManagerlist =  new List<string>();
    protected void Page_Load(object sender, EventArgs e)
        {
            InitParameter();
            BindingList(); //일감정보 바인딩
      
            if (!IsPostBack)
            {
                GetAttachFile(); //첨부파일 추가
            }
        }
        private void InitParameter()
        {
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

                if (WC_id !=0)
                {
                    WC_id=  Convert.ToInt32(rows[0]["WC_id"]);
                    WC_State = rows[0]["WC_State"].ToString();
                    WC_Priority = rows[0]["WC_Priority"].ToString();
                    WC_Name = rows[0]["WC_Name"].ToString();
                    WC_Author = rows[0]["WC_Author"].ToString();
                    WC_Manager = rows[0]["WC_Manager"].ToString();
                    WC_date = rows[0]["WC_date"].ToString();
                    WC_Version = rows[0]["WC_Version"].ToString();
                    if (WC_Version == "1")
                        WC_Version = "GBOX18";
                    if (WC_Version == "2")
                        WC_Version = "IBOX 2018 3Q";
                    WC_Progress = Convert.ToInt32(rows[0]["WC_Progress"].ToString());
                    WC_Type = Convert.ToInt32(rows[0]["WC_Type"].ToString());

                    switch (WC_Type)
                    {
                        case 1: WC_Typestring = "새 일감"; break;
                        case 2: WC_Typestring = "진행중"; break;
                        case 3: WC_Typestring = "확인 요망"; break;
                        case 4: WC_Typestring = "QA중"; break;
                        case 5: WC_Typestring = "완료"; break;
                        case 6: WC_Typestring = "재발생"; break;
                        case 7: WC_Typestring = "지속"; break;
                        case 8: WC_Typestring = "폐기"; break;
                        case 9: WC_Typestring = "보류"; break;
                    }

                 			
                    WC_Reply = Convert.ToInt32(rows[0]["WC_Reply"].ToString());
                    WC_Contents =rows[0]["WC_Contents"].ToString();
                    WC_NeedTime = rows[0]["WC_NeedTime"].ToString();
                    WC_Completedate = rows[0]["WC_Completedate"].ToString();
             
                    if (!DBNull.Value.Equals(rows[0]["WC_FileSize"]))
                    {
                        WC_Files = rows[0]["WC_Files"].ToString();
                        WC_FileSize = Convert.ToInt32(rows[0]["WC_FileSize"]);
                        WC_FileName = rows[0]["WC_FileName"].ToString();
                    }
                


                    AttachFiles = WC_Files.Split('|');

                }

            }

       
            string UpdateString = "";
            UpdateString = "SELECT WM_id,WM_name  FROM WorkManager WHERE WM_id=" + WC_id.ToString();
            DBConn con = new DBConn();
            cmd = new SqlCommand(UpdateString, con.GetConn());
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
              
                while (reader.Read())
                {
                    WorkManagerlist.Add(reader.GetValue(1).ToString());
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
        protected string GetProcessGraph(string processPercent)
        {
            int percent = 0;
            string retValue = "";
            try
            {
                percent = Convert.ToInt32(processPercent);
                if (percent >= 10 && percent < 20)
                    retValue = "<font color='#951015'>■</font>□□□□□";
                if (percent >= 20 && percent < 40)
                    retValue = "<font color='#951015'>■</font><font color='#CA2704'>■</font>□□□□";
                if (percent >= 40 && percent < 60)
                    retValue = "<font color='#951015'>■</font><font color='#CA2704'>■</font><font color='#FB5A02'>■</font>□□□";
                if (percent >= 60 && percent < 80)
                    retValue = "<font color='#951015'>■</font><font color='#CA2704'>■</font><font color='#FB5A02'>■</font><font color='#FAA200'>■</font>□□";
                if (percent >= 80)
                    retValue = "<font color='#951015'>■</font><font color='#CA2704'>■</font><font color='#FB5A02'>■</font><font color='#FAA200'>■</font><font color='#FDDC4F'>■</font>□";
            }
            catch
            {
                retValue = "";
            }
            return retValue;
        }

        #region 첨부파일 셋팅
        private void GetAttachFile()
        {
            //if (this._workEntity.AttachFiles != null && this._workEntity.AttachFiles.Length > 0)
            if(WC_Files!="" && AttachFiles.Length>0)
            {
                string[] fileList = new string[AttachFiles.Length];
                //실제상대경로로 바꾼다
                for (int i = 0; i < AttachFiles.Length; i++)
                {
                    fileList[i] = (AttachFiles[i].Split(';'))[0];
                }


                //다운받을수 있게 링크 처리
                foreach (string file in fileList)
                {
                    //< img src = "/images/redminimg.jpg" alt = "Alternate Text" />
                    string fileImage = "<img src='/Images/{0}' border=0 width=16 height=16 align='absbottom'>";
                    string ext = file.Substring(file.LastIndexOf(".") + 1).ToLower();
                    if (ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png" || ext == "ico")
                        fileImage = string.Format(fileImage, "file_img.png");
                    else if (ext == "xls" || ext == "xlsx")
                        fileImage = string.Format(fileImage, "file_xls.png");
                    else if (ext == "ppt" || ext == "pptx")
                        fileImage = string.Format(fileImage, "file_ppt.png");
                    else if (ext == "doc" || ext == "docx")
                        fileImage = string.Format(fileImage, "file_doc.png");
                    else
                        fileImage = string.Format(fileImage, "file_none.png");


                    //암호화 하여 넘김
                    string key = (Path.GetFileName(file).Split('_'))[0];
                    if(key!="")
                    {
                        key = key.Substring(0, 8);
                        this.ltrAttachFile.Text += "<a href='Download.aspx"
                            + "?key=" + key
                            + "&path=" + Utility.Encrypt(key, file)
                            + "' target='page'>"
                            + fileImage + " "
                            + file.Substring(file.IndexOf("_") + 1) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                 
                }
            }
        }
        #endregion
    }
}