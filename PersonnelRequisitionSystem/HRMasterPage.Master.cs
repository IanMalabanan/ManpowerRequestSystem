using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClsLibBusiness;
using ClsLibConnection;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using Telerik.Web.UI;

namespace PersonnelRequisitionSystem
{
    public partial class HRMasterPage : System.Web.UI.MasterPage
    {
        BPersonnelRequisition _master = new BPersonnelRequisition();

        public static string mssg =string.Empty;

        private void Show(string msg)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "msg", "alert('" + msg + "');", true);
            }
        }
               
        public string ShowMessage()
        {
            string msg = string.Empty;

            msg = mssg;

            return msg;
        }

        private Boolean _VerifyFields()
        {
            if(tbconfirmpass.Text.Trim() != tbnewpass.Text.Trim())
            {
                AlertError.Visible = true;
                mssg = "Password did not match";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalChangePass();", true);
                return false;
            }

            if (string.IsNullOrEmpty(tbnewpass.Text.Trim()))
            {
                AlertError.Visible = true;
                mssg = "New Password is required";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalChangePass();", true);
                return false;
            }

            if (string.IsNullOrEmpty(tbconfirmpass.Text.Trim()))
            {
                AlertError.Visible = true;
                mssg = "Confirm Password is required";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalChangePass();", true);
                return false;
            }

            return true;
        }

        private Boolean ChangePass()
        {
            Boolean _isValid = false;

            _isValid = _VerifyFields();

            if (!_isValid)
                return false;

            _master = new BPersonnelRequisition();

            _master.Password = tbnewpass.Text.Trim();

            _master.RecID = Convert.ToInt32(Session["UserID"].ToString());

            _master.ChangePass();

            Show("Password Successfully Changed");

            tbconfirmpass.Text = tbnewpass.Text = string.Empty;

            AlertError.Visible = false;

            CloseModalChangePass();

            return true;
        }

        public void CloseModalChangePass()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalChangePass", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalChangePass').hide();", true);
        }

        protected void CloseModalChangePass_Click(object sender, EventArgs e)
        {
            //CloseModalChangePass();
        }
        
        protected void Logout(object sender, EventArgs e)
        {
            Session.Clear();

            Session.Abandon();

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Cache.SetNoStore();

            Response.Redirect("LoginPage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.InnerText = Session["FName"].ToString();

            lblPosition.InnerText = Session["Position"].ToString();
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = ChangePass();
        }
    }
}