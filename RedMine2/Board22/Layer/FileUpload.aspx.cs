using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data;

namespace Board22.Layer
{
    public partial class FileUpload : System.Web.UI.Page
    {
        #region ################## Variable ##################        
        private string[] _AttachFile { get; set; }
        private string _brdSeq { get; set; }
        private string _returnId;
        //private string _realFileName;
        #endregion

        #region ################## Property ##################
        public string ReturnId { get { return this._returnId; } }
        //public string RealFileName { get { return this._realFileName; } }
        #endregion

        #region ################## Page Load #################
        protected void Page_Load(object sender, EventArgs e)
        {
            InitValiable();     //전역변수 초기화
            InitParameter();    //파라메터 초기화
            InitServerControl();//서버컨트롤 초기화            

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this._brdSeq))
                {
                    BindingData();
                }
            }

        }
        #endregion

        #region ################## Method ####################

        #region 전역변수 초기화
        private void InitValiable()
        {

        }
        #endregion

        #region 파라메터 초기화
        private void InitParameter()
        {
            this._brdSeq = Utility.GetQuery("brdid");
            this._returnId = Utility.GetQuery("returnid");
        }
        #endregion

        #region 서버컨트롤 초기화
        private void InitServerControl()
        {

        }
        #endregion

        #region
        /// <summary>
        ///  수정시, 첨부파일 리스트 가져오는 부분
        /// </summary>
        public void BindingData()
        {
           // BoardBiz boardBiz = null;
            DataRow dr = null;

           // using (boardBiz = new BoardBiz())
            {
               // dr = boardBiz.GetBrdInfoView(Convert.ToInt32(this._brdSeq));
            }

            this.hdfAttachFile.Value = dr["brd_attach"].ToString();
            int attachmentCount = this.hdfAttachFile.Value == "" ? 0 : this.hdfAttachFile.Value.Split('|').Length;

            if (attachmentCount > 0)
            {
                string[] FileInfo = this.hdfAttachFile.Value.Split('|');

                foreach (string file in FileInfo)
                {
                    string fileName = (file.Split(';'))[0];
                    decimal fileSize = Convert.ToDecimal((file.Split(';'))[1]);

                    this.lstAttachFile.Items.Add(
                    new ListItem(
                        fileName.Substring(fileName.LastIndexOf('/') + 1) + "(" + Utility.GetFileSize(fileSize) + ")",
                        fileSize.ToString())
                        );

                    this.spanFileCount.InnerText = this.lstAttachFile.Items.Count.ToString();
                    decimal size = 0;
                    foreach (ListItem li in this.lstAttachFile.Items)
                        size += Convert.ToDecimal(li.Value);

                    this.spanFileSize.InnerText = Utility.GetFileSize(size);
                    this.hdfAttachFile.Value = dr["brd_attach"].ToString();

                }
            }
        }
        #endregion

        #endregion

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

                        System.Web.HttpContext.Current.Response.Write(this.hdfAttachFile.Value);
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