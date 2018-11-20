using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GW.Approval.Web.Utils.SmartEditor2
{
    public partial class Preview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Charset = "euc-kr";

            //넘어온 Form값을 받아서, 화면에 출력한다.
            foreach (string Key in Request.Form)
            {
                if (Key == "hddpreviewContents")
                {
                    lblContents.Text = HttpUtility.HtmlDecode(Request.Form[Key]);
                    break;
                }
            }
        }
    }
}