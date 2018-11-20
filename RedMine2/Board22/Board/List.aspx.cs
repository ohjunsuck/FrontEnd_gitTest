using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Board22.Board
{
    public partial class List : System.Web.UI.Page
    {
        private string _searchKeyword;
        public int TotalCount { get; set; }
        private  int _TOPSIZE = 15;
        private const int _PAGESIZE = 10;
        private int _page;
        //
        public string serial_no;
        public string writer;
        public string password;
        public string title;
        public string message;
        public string ref_id;
        public string inner_id;
        public string depth;
        public string read_count;
        public string del_flag;
        public string reg_date;
        public string delete_chid;
        //
        public BoardDac appDac;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitParameter();
            BindingList(); //리스트 바인딩
            if (!IsPostBack)
            {

            }

        }

        protected string ShowDepth(int depth)
        {
            string returnString = "";
            for (int i = 0; i < depth; i++)
            {
                returnString += "&nbsp;&nbsp;&nbsp;";
            }
            return returnString;
        }

        protected string ShowReplyIcon(int innerId)
        {
            string returnString = "";

            if (innerId != 0)
            {
                returnString += "<img src='/images/reply_icon.gif' />";
            }
            return returnString;
        }
        protected string ShowWriter(string writer)
        {
            return writer;
        }
        protected string ShowReadCount(string readcount)
        {
            return readcount;
        }
        protected string ShowSerailNum(string num)
        {
            return num;
        }
        

        protected string ShowTitle(string serialNo, string title, string delFlag)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Board"].ConnectionString;
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn2;
            conn2.Open();
            string SearchadminSQL = "SELECT COUNT(serial_no) FROM boardreply  WHERE depth ='" + serialNo + "'";
            cmd2 = new SqlCommand(SearchadminSQL, conn2);
            int cntreply = 0;
            if (cmd2.ExecuteScalar() != null)
                cntreply = (int)cmd2.ExecuteScalar();


            string returnString = "";
            // del_flag 필드 값이 N인 경우, 즉 삭제되지 않은 게시물
            if (delFlag == "N")
            {
                Session["WriteName"] = Eval("del_flag").ToString();
                //returnString += "<a href='Read.aspx?sn=" + serialNo +"&loginid="+ Request.QueryString["loginid"]+"&loginPassword="+ Request.QueryString["loginPassword"];
                returnString += "<a href='Read.aspx?sn=" + serialNo;
                returnString += "' class='a01'>" + title + " " + "[" + cntreply + "]" + "</a>";
            }
            // del_flag 필드 값이 Y인 경우, 즉 삭제된 게시물
            else
            {
                returnString += "삭제된 게시물입니다.";
            }

            conn2.Close();
            return returnString;
        }
        protected string ShowDate(DateTime regDate)
        {
            return string.Format("{0:yyyy-MM-dd}", regDate);
        }

        protected void rptList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //    string processId = ((DataRowView)e.Item.DataItem)["process_id"].ToString();
            //    if (this._preProcessId == processId)
            //    {
            //        e.Item.Visible = false;
            //    }

            //    this._preProcessId = processId;
            //}
        }
        private void BindingList()
        {

            DataTable dtList = null;

            using (appDac = new BoardDac())
            {
            
                DataSet ds = null;
                ds = appDac.CreateAppBaseInfo("1", writer, password, title, message, ref_id,
                     inner_id, depth, read_count, del_flag, reg_date, delete_chid, out int totalCount,_TOPSIZE, this._page, this._searchKeyword);

                this.TotalCount = totalCount;
                dtList = ds.Tables[0];
            }

            if (dtList != null)
            {
                if (this.TotalCount == 0)
                {
                    this.ltrNone.Text = "<div style='text-align:center;margin:10px; padding-bottom:10px; border-bottom:1px solid #999;'>조회결과가 없습니다</div>";
                    this.rptList.Visible = false;
                }
                else
                {
                    this.rptList.DataSource = dtList;
                    this.rptList.DataBind();
                }

                //페이징 구성
                if (this.TotalCount > _TOPSIZE)
                    this.ltrPaging.Text = Utility.GetPagingLinkSting2(_PAGESIZE,
                                                                    ((this.TotalCount % _TOPSIZE) > 0) ? this.TotalCount / _TOPSIZE + 1 : this.TotalCount / _TOPSIZE,
                                                                    this._page,
                                                                    Request.RawUrl,
                                                                    "page",
                                                                    "", "", "", "", "", "", "", "", "");
                //Session["NowPageIndex"] = this._page;

            }
         }

        private void InitParameter()
        {
            //if (Session["NowPageIndex"] != null)
            //    this._page = (int)Session["NowPageIndex"];
            //else
            //    this._page = 1;
            this._page = Utility.GetQuery("page") == "" ? 1 : Convert.ToInt32(Utility.GetQuery("page"));
            // 
            Session["NowPageIndex"] = _page;

           

          
            this._searchKeyword = Utility.GetQuery("keyword");
            if (_searchKeyword != null)
                Session["keyword"] = _searchKeyword;
            else
                Session["keyword"] = "";
            //_page = 1;
            //this._readYn = Utility.GetQuery("read");
            //this._formType = Utility.GetQuery("formtype");
            //this._searchKey = Utility.GetQuery("key");
            //this._searchKeyword = Utility.GetQuery("keyword");

            if(System.Web.HttpContext.Current.Request.Form["countboard"]!=null)
            _TOPSIZE =  Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["countboard"]);



        }

    }
}