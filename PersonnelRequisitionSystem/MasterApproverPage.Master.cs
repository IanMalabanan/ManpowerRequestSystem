using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonnelRequisitionSystem
{
    public partial class MasterApproverPage : System.Web.UI.MasterPage
    {
        public string lblUserFname
        {
            get
            {
                return lbluserfname.InnerText;
            }
            set
            {
                lbluserfname.InnerText = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}