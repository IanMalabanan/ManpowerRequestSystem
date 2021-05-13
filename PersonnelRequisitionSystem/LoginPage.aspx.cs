using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClsLibBusiness;
using ClsLibConnection;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PersonnelRequisitionSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        BPersonnelRequisition _master = new BPersonnelRequisition();

        BPersonnelRequisition _dbmaster = new BPersonnelRequisition();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            //try
            //{
            _master = new BPersonnelRequisition();

            if (_master.UserLogin(tbUsername.Text.TrimEnd(), tbPassword.Text.TrimEnd()).Rows.Count == 0)
            {
                RadWindowManager1.RadAlert("No Records Found!", null, null, "Notification", null);
            }
            else
            {
                foreach (DataRow dr in _master.UserLogin(tbUsername.Text.TrimEnd(), tbPassword.Text.TrimEnd()).Rows)
                {
                    Session["UserRole"] = dr["UserRole"].ToString();

                    Session["RoleDesc"] = dr["RoleDesc"].ToString();

                    Session["EmpID"] = dr["EmpID"].ToString();

                    Session["UserEmail"] = dr["EmailAddress"].ToString();

                    Session["UserID"] = Convert.ToInt32(dr["UserID"].ToString());
                }

                DataTable dt;

                _dbmaster = new BPersonnelRequisition();

                dt = _dbmaster.SKPI_GetAllEmployeesByEmpIDDT(Session["EmpID"].ToString().Trim());

                foreach (DataRow row in dt.Rows)
                {
                    Session["Positions"] = row["Pos_Desc"].ToString();

                    Session["DeptCode"] = row["Dept_Code"].ToString();

                    Session["DeptName"] = row["Dept_Desc"].ToString();

                    Session["SectCode"] = row["Sect_Code"].ToString();

                    Session["SectName"] = row["Sect_Desc"].ToString();

                    Session["FName"] = row["FirstName"].ToString();

                    Session["FullName_LnameFirst"] = row["FullName_LnameFirst"].ToString();

                    Session["FullName_FnameFirst"] = row["FullName_FnameFirst"].ToString();
                }


                Regex rsSV = new Regex("Supervisor");
                bool containsSV = rsSV.IsMatch(Session["Positions"].ToString().Trim());

                Regex rsManager = new Regex("Manager");
                bool containsManager = rsManager.IsMatch(Session["Positions"].ToString().Trim());

                if (containsSV == true)
                {
                    Session["Position"] = Session["Positions"].ToString().Trim();
                }
                else if (containsManager == true)
                {
                    Session["Position"] = Session["Positions"].ToString().Trim();
                }
                else
                {
                    Session["Position"] = Session["RoleDesc"].ToString().Trim();
                }

                //Session["Position"] = "Supervisor";

                //System.Threading.Thread.Sleep(1000);

                Response.Redirect("MainPage.aspx");
            }
            //}
            //catch
            //{
            //    //Show("No Records Found!");

            //    //RadNotification1.Text = "No Records Found!";

            //    //RadNotification1.Title = "Notification";

            //    //RadNotification1.TitleIcon = string.Empty;

            //    //RadNotification1.ContentIcon = string.Empty;

            //    //RadNotification1.Show();

            //    RadWindowManager1.RadAlert("No Records Found!", null, null, "Notification", null);

            //    tbPassword.Text = string.Empty;

            //    tbUsername.Text = string.Empty;
            //}
        }

        protected void LoginHR(object sender, EventArgs e)
        {
            //try
            //{
            _master = new BPersonnelRequisition();

            if (_master.UserLogin_HR(tbHRUsername.Text.TrimEnd(), tbHRUserpass.Text.TrimEnd()).Rows.Count == 0)
            {
                RadWindowManager1.RadAlert("No Records Found!", null, null, "Notification", null);
            }
            else
            {
                foreach (DataRow dr in _master.UserLogin_HR(tbHRUsername.Text.TrimEnd(), tbHRUserpass.Text.TrimEnd()).Rows)
                {
                    Session["UserRole"] = dr["UserRole"].ToString();

                    Session["EmpID"] = dr["EmpID"].ToString();

                    Session["UserEmail"] = dr["EmailAddress"].ToString();

                    Session["UserID"] = Convert.ToInt32(dr["UserID"].ToString());
                }

                DataTable dt;

                _dbmaster = new BPersonnelRequisition();

                dt = _dbmaster.SKPI_GetAllEmployeesByEmpIDDT(Session["EmpID"].ToString().Trim());

                foreach (DataRow row in dt.Rows)
                {
                    Session["Position"] = row["Pos_Desc"].ToString();

                    Session["DeptCode"] = row["Dept_Code"].ToString();

                    Session["DeptName"] = row["Dept_Desc"].ToString();

                    Session["SectCode"] = row["Sect_Code"].ToString();

                    Session["SectName"] = row["Sect_Desc"].ToString();

                    Session["FName"] = row["FirstName"].ToString();

                    Session["FullName_LnameFirst"] = row["FullName_LnameFirst"].ToString();

                    Session["FullName_FnameFirst"] = row["FullName_FnameFirst"].ToString();
                }

                //Session["Position"] = "Factory Manager";

                //System.Threading.Thread.Sleep(1000);

                Response.Redirect("HRRequestsPage.aspx");
            }
            //}
            //catch
            //{
            //    //Show("No Records Found!");

            //    //RadNotification1.Text = "No Records Found!";

            //    //RadNotification1.Title = "Notification";

            //    //RadNotification1.TitleIcon = string.Empty;

            //    //RadNotification1.ContentIcon = string.Empty;

            //    //RadNotification1.Show();

            //    RadWindowManager1.RadAlert("No Records Found!", null, null, "Notification", null);

            //    tbPassword.Text = string.Empty;

            //    tbUsername.Text = string.Empty;
            //}
        }
    }
}