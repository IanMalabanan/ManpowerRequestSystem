using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClsLibBusiness;
using ClsLibConnection;
using ClsCommon;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Data;
using Telerik.Web.UI;
using System.IO;

namespace PersonnelRequisitionSystem
{
    public partial class PersonnelRequisitionPage : System.Web.UI.Page
    {
        BPersonnelRequisition _master = new BPersonnelRequisition();

        BPersonnelRequisition _dbmaster = new BPersonnelRequisition();

        ClsGenerateRandomString clsGenerateRandomString = new ClsGenerateRandomString();

        public static string uc = string.Empty, appr_username = string.Empty, appr_pass = string.Empty
            , appr_email = string.Empty, fileext = string.Empty, filetype = string.Empty, appr_name = string.Empty;



        private void GetWorkStatus()
        {
            _dbmaster = new BPersonnelRequisition();

            rdolstWorkStat.DataSource = _dbmaster.GetWorkStatusDT();

            rdolstWorkStat.DataBind();
        }

        private string GenerateControlNo()
        {
            string ControlNo = string.Empty;

            int year = DateTime.Now.Year % 100;

            int total = 0;

            _dbmaster = new BPersonnelRequisition();
            
            if ((radcboDepartment.SelectedValue.ToString().Trim() == "P" && radcboSection.SelectedValue.ToString().Trim() == "08")
                    || (radcboDepartment.SelectedValue.ToString().Trim() == "S" && radcboSection.SelectedValue.ToString().Trim() == "01")
                    || (radcboDepartment.SelectedValue.ToString().Trim() == "S" && radcboSection.SelectedValue.ToString().Trim() == "02"))
            {
                foreach (DataRow dr in _dbmaster.CountControlNoPerDeptPerSectDT(radcboDepartment.SelectedValue.ToString().Trim()
                    , radcboSection.SelectedValue.ToString().Trim()
                    , DateTime.Now.Month, DateTime.Now.Year).Rows)
                {
                    total = Convert.ToInt32(dr["CountPerDeptPerSect"]);
                }
                if (total >= 1 && total <= 9)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim() + radcboSection.SelectedValue.ToString().Trim()
                        + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-00" + total;
                }
                else if (total >= 10 && total <= 99)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim() + radcboSection.SelectedValue.ToString().Trim()
                    + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-0" + total;
                }
                else
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim() + radcboSection.SelectedValue.ToString().Trim()
                    + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-" + total;
                }
            }
            else if (radcboDepartment.SelectedValue.ToString().Trim() == "P" && radcboSection.SelectedValue.ToString().Trim() != "08")
            {
                foreach (DataRow dr in _dbmaster.CountControlNoForProductionDT(radcboDepartment.SelectedValue.ToString().Trim()
                    , DateTime.Now.Month, DateTime.Now.Year).Rows)
                {
                    total = Convert.ToInt32(dr["CountPerDeptPerSect"]);
                }

                if (total >= 1 && total <= 9)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                    + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-00" + total;
                }
                else if (total >= 10 && total <= 99)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                        + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-0" + total;
                }
                else
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                        + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-" + total;
                }
            }
            else
            {
                foreach (DataRow dr in _dbmaster.CountControlNoPerDeptDT(radcboDepartment.SelectedValue.ToString().Trim()
                    , DateTime.Now.Month, DateTime.Now.Year).Rows)
                {
                    total = Convert.ToInt32(dr["CountPerDeptPerSect"]);
                }

                if (total >= 1 && total <= 9)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                    + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-00" + total;
                }
                else if (total >= 10 && total <= 99)
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                        + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-0" + total;
                }
                else
                {
                    ControlNo = "PRF-" + radcboDepartment.SelectedValue.ToString().Trim()
                        + "-" + year.ToString().Trim() + DateTime.Now.Month.ToString("00") + "-" + total;
                }
            }

            return ControlNo;
        }

        private void GetDeptManager()
        {
            _dbmaster = new BPersonnelRequisition();

            RadcboApprover.DataSource = _dbmaster.GetDeptManagerDT();

            RadcboApprover.DataTextField = "FullName_LnameFirst";

            RadcboApprover.DataValueField = "EmpID";

            RadcboApprover.DataBind();
        }

        private void ClearControl(System.Web.UI.Control control)
        {
            var textbox = control as System.Web.UI.WebControls.TextBox;

            if (textbox != null)

                textbox.Text = string.Empty;

            var radio = control as System.Web.UI.WebControls.RadioButton;

            if (radio != null)

                radio.Checked = false;

            var cbo = control as Telerik.Web.UI.RadComboBox;
            if (cbo != null && cbo.SelectedIndex > -1)
            {
                cbo.ClearSelection();

                cbo.Text = string.Empty;
            }

            var radiolist = control as System.Web.UI.WebControls.RadioButtonList;

            if (radiolist != null)

                radiolist.ClearSelection();


            foreach (System.Web.UI.Control childControl in control.Controls)
            {
                ClearControl(childControl);
            }
        }

        private void GetAttachment()
        {
            _dbmaster = new BPersonnelRequisition();

            gridAttachment.DataSource = _dbmaster.GetAttachmentDT(uc);

            gridAttachment.DataBind();
        }

        private Boolean _SaveUniqueCode(string code)
        {
            _master = new BPersonnelRequisition();

            _master.UniqueCode = code;

            _master.SaveUniqueCode();

            return true;
        }

        private Boolean SaveSignature()
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString();

            _master.UniqueCode = uc;

            _master.SaveSignature();

            return true;
        }

        private Boolean SaveSignatureReqManager()
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString();

            _master.UniqueCode = uc;

            _master.SaveSignatureReqManager();

            return true;
        }

        private string GenerateCode()
        {
            string code = string.Empty;

            Random r = new Random();

            int rInt = r.Next(Convert.ToInt32(ConfigurationManager.AppSettings["CodeLengthFrom"]),
                Convert.ToInt32(ConfigurationManager.AppSettings["CodeLengthTo"]));

            code = clsGenerateRandomString.RandomAlphanumericString(rInt);

            _master = new BPersonnelRequisition();

            _master.UniqueCode = code;

            _master.UniqueCodeCounters();

            if (_master.UniqueCodeCounter > 0)
            {
                Random newr = new Random();
                int newrInt = newr.Next(Convert.ToInt32(ConfigurationManager.AppSettings["CodeLengthFrom"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["CodeLengthTo"]));

                code = clsGenerateRandomString.RandomAlphanumericString(newrInt);
            }
            else
            {
                Boolean res;

                res = _SaveUniqueCode(code);
            }

            return code;
        }

        private void ShowMessage(string msg)
        {
            RadNotification1.Text = msg;

            RadNotification1.Title = "Message";

            RadNotification1.ContentIcon = "";

            RadNotification1.TitleIcon = "";

            RadNotification1.Show();
        }

        private Boolean _VerifyFields()
        {
            if (radcboDepartment.SelectedIndex == -1)
            {
                ShowMessage("Select department first");
                return false;
            }

            if (radcboSection.SelectedIndex == -1)
            {
                ShowMessage("Select section first");
                return false;
            }

            if (string.IsNullOrEmpty(tbMaleCount.Text))
            {
                ShowMessage("Number of male manpower is required. If none, set as 0.");

                tbMaleCount.Text = "0";

                return false;
            }

            if (string.IsNullOrEmpty(tbFemaleCount.Text))
            {
                ShowMessage("Number of female manpower is required. If none, set as 0.");

                tbFemaleCount.Text = "0";

                return false;
            }

            if (!dpDateNeeded.SelectedDate.HasValue)
            {
                ShowMessage("Date needed is required");

                return false;
            }

            //if (string.IsNullOrEmpty(tbBriefDescriptionofDuties.Text))
            //{
            //    ShowMessage("Brief description of duties cannot be nulled");

            //    tbBriefDescriptionofDuties.Text = "N/A";

            //    return false;
            //}

            //if (string.IsNullOrEmpty(tbSpecialSkills_QualificationsRequired.Text))
            //{
            //    ShowMessage("Special skills / qualifications required cannot be nulled");

            //    tbSpecialSkills_QualificationsRequired.Text = "N/A";

            //    return false;
            //}

            //if (string.IsNullOrEmpty(tbEducationRequired.Text))
            //{
            //    ShowMessage("Education Required cannot be nulled");

            //    tbEducationRequired.Text = "N/A";

            //    return false;
            //}

            if (radcboJustification.SelectedIndex == -1)
            {
                ShowMessage("Select justification first");

                return false;
            }

            if (rdolstWorkStat.SelectedIndex == -1)
            {
                ShowMessage("Select work status first");

                return false;
            }

            if (string.IsNullOrEmpty(tbHistory.Text))
            {
                ShowMessage("Select justification first");

                return false;
            }

            Regex rsSV = new Regex("Supervisor");
            bool containsSV = rsSV.IsMatch(Session["Position"].ToString().Trim());

            Regex rsManager = new Regex("Manager");
            bool containsManager = rsManager.IsMatch(Session["Position"].ToString().Trim());

            if (containsSV == true)
            {
                if (RadcboApprover.SelectedIndex == -1)
                {
                    ShowMessage("Select approver first");

                    return false;
                }
            }


            int total = Convert.ToInt32(tbMaleCount.Text) + Convert.ToInt32(tbFemaleCount.Text);

            if (total == 0)
            {
                ShowMessage("You must fill the number of manpower needed");

                return false;
            }

            if (gridAttachment.Rows.Count == 0)
            {
                ShowMessage("Attachment is required");

                return false;
            }

            return true;
        }

        private Boolean SavePersonnelRequisition()
        {
            Boolean isValid = false;

            isValid = _VerifyFields();

            if (!isValid)
                return false;

            _master = new BPersonnelRequisition();

            _master.EmpID = Session["EmpID"].ToString().Trim();

            _master.ControlNo = GenerateControlNo();

            _master.ReqDeptCode = radcboDepartment.SelectedValue.ToString().Trim();

            _master.ReqDeptSectCode = radcboSection.SelectedValue.ToString().Trim();

            _master.Position = radcboPosition.SelectedValue.ToString().Trim();

            _master.MaleCount = Convert.ToInt32(tbMaleCount.Text.Trim());

            _master.FemaleCount = Convert.ToInt32(tbFemaleCount.Text.Trim());

            _master.DateNeeded = dpDateNeeded.SelectedDate.Value;

            _master.BriefDescOfDuties = tbBriefDescriptionofDuties.Text.Trim();

            _master.SpecialSkillsQualificationsRequired = tbSpecialSkills_QualificationsRequired.Text.Trim();

            _master.EducationRequired = tbEducationRequired.Text.Trim();

            _master.StatusCode = rdolstWorkStat.SelectedValue.ToString();

            _master.JustificationCode = radcboJustification.SelectedValue.ToString().Trim();

            _master.History = tbHistory.Text.Trim();

            Regex rsSV = new Regex("Supervisor");
            bool containsSV = rsSV.IsMatch(Session["Position"].ToString().Trim());

            Regex rsManager = new Regex("Manager");
            bool containsManager = rsManager.IsMatch(Session["Position"].ToString().Trim());

            if (containsSV == true)
            {
                _master.SigningStatus = "Approved-Supervisor";
            }
            else if (containsManager == true)
            {
                _master.SigningStatus = "Approved-DeptManager";
            }

            _master.SigningRemarks = "SIGNED";

            _master.UniqueCode = uc;

            _master.SavePersonnelRequisition();

            Regex rsSV1 = new Regex("Supervisor");
            bool containsSV1 = rsSV.IsMatch(Session["Position"].ToString().Trim());

            Regex rsManager1 = new Regex("Manager");
            bool containsManager1 = rsManager.IsMatch(Session["Position"].ToString().Trim());

            if (containsSV1 == true)
            {
                Boolean res;

                res = SaveSignature();
            }
            else if (containsManager1 == true)
            {
                Boolean res;

                res = SaveSignatureReqManager();
            }

            SubmitDocument();

            InformHR();

            ClearControl(this.Page);

            divDetails.Visible = false;

            divSuccess.Visible = true;

            return true;
        }

        private Boolean _SaveEmailLogs(string url, string _urole, string _eadd, string _efrom, string _sentto)
        {
            _master = new BPersonnelRequisition();

            _master.url = url;

            _master.userrole = _urole;

            _master.emailadd = _eadd;

            _master.emailfrom = _efrom;

            _master.sentto = _sentto;

            _master.UniqueCode = uc;

            _master.SaveEmailLogs();

            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = SavePersonnelRequisition();

            //MasterPage MasterPage = (MasterPage)Page.Master;
            //MasterPage.GetMe();
        }

        protected void btnGoToMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void radcboJustification_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbHistory.Enabled = true;

            tbHistory.Text = string.Empty;

            tbHistory.Focus();
        }

        private void InformHR()
        {
            var rx2 = new Regex(@"(?<=\w)\w");

            var newString2 = rx2.Replace(appr_name, new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            var newString3 = rx2.Replace(Session["FullName_FnameFirst"].ToString().Trim(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));


            string body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

            body += "<br /><br />" + "Please be informed that there is a personnel request waiting for checking and approval under " + newString2 + ".";

            body += "<br /><br />" + "See below details:";

            body += "<br /><br />" + "<b>Department:</b> " + radcboDepartment.SelectedItem.Text;

            body += "<br /><br />" + "<b>Section</b>: " + radcboSection.SelectedItem.Text;

            body += "<br /><br />" + "<b>Position:</b> " + radcboPosition.SelectedItem.Text;

            body += "<br /><br />" + "<b>Male Count:</b> " + tbMaleCount.Text;

            body += "<br /><br />" + "<b>Female Count:</b> " + tbFemaleCount.Text;

            body += "<br /><br />" + "<b>Date Needed:</b> " + Convert.ToDateTime(dpDateNeeded.SelectedDate.Value).ToString("dd/MMM/yyyy");

            //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbBriefDescriptionofDuties.Text;

            //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbSpecialSkills_QualificationsRequired.Text;

            //body += "<br /><br />" + "<b>Education Required:</b> " + tbEducationRequired.Text;

            body += "<br /><br />" + "<b>Employment/Work Status:</b> " + rdolstWorkStat.Items[rdolstWorkStat.SelectedIndex].Text.ToString();

            if (radcboJustification.SelectedItem.Text.Trim() == "Others(Pls. specify)")
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + radcboJustification.SelectedItem.Text.Trim() + " - " + tbHistory.Text.Trim();
            }
            else
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + radcboJustification.SelectedItem.Text.Trim() + " " + tbHistory.Text.Trim();
            }

            body += "<br /><br />" + "Thank You.";

            body += "<br /><br />" + "From: " + newString3;

            body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";


            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage())
            {
                string sub = "Personnel Requisition System: Requesting For Approval";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                var rx = new Regex(@"(?<=\w)\w");

                var newString = rx.Replace(Session["FullName_FnameFirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    ConfigurationManager.AppSettings["MailSenderName"].ToString());


                _dbmaster = new BPersonnelRequisition();

                foreach (DataRow dr in _dbmaster.GetHREmailAddressessDT().Rows)
                {
                    mm.To.Add(dr["EmailAddress"].ToString().Trim());
                }

                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigurationManager.AppSettings["MailServer"].ToString();

                smtp.EnableSsl = true;

                mm.IsBodyHtml = true;

                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),
                    ConfigurationManager.AppSettings["MailSenderEmailPassword"].ToString());

                smtp.Credentials = NetworkCred;

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                                                                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                                { return true; };
                smtp.Send(mm);
            }
        }

        private void SubmitDocument()
        {
            string role = string.Empty;

            string body = string.Empty;

            string link = string.Empty;

            string roledesc = string.Empty;

            var rx = new Regex(@"(?<=\w)\w");

            var newString2 = rx.Replace(Session["FullName_FnameFirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));


            Regex rsSV = new Regex("Supervisor");
            bool containsSV = rsSV.IsMatch(Session["Position"].ToString().Trim());

            Regex rsManager = new Regex("Manager");
            bool containsManager = rsManager.IsMatch(Session["Position"].ToString().Trim());

            if (containsSV == true)
            {
                _dbmaster = new BPersonnelRequisition();

                foreach (DataRow dr in _dbmaster.GetDeptManagerByEmpIDDT(RadcboApprover.SelectedValue.ToString().Trim()).Rows)
                {
                    role = dr["UserRole"].ToString().Trim();

                    roledesc = dr["RoleDesc"].ToString();

                    appr_username = dr["Username"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString().Trim();

                    appr_name = dr["FullName_LnameFirst"].ToString();
                }
            }
            else if (containsManager == true)
            {
                _dbmaster = new BPersonnelRequisition();

                foreach (DataRow dr in _dbmaster.GetHRManagerDT().Rows)
                {
                    appr_name = dr["FullName_LnameFirst"].ToString();

                    appr_username = dr["Username"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    role = dr["UserRole"].ToString().Trim();

                    roledesc = dr["RoleDesc"].ToString();

                    appr_email = dr["EmailAddress"].ToString().Trim();
                }
            }

            link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + Session["EmpID"].ToString().Trim()

                    + "&userrole=" + role;


            Boolean res;

            res = _SaveEmailLogs(link, roledesc, appr_email
                , Session["fullname_fnamefirst"].ToString().Trim(), appr_username);


            body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

            body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

            body += "<br /><br />" + "See below details:";

            body += "<br /><br />" + "<b>Department:</b> " + radcboDepartment.SelectedItem.Text;

            body += "<br /><br />" + "<b>Section</b>: " + radcboSection.SelectedItem.Text;

            body += "<br /><br />" + "<b>Position:</b> " + radcboPosition.SelectedItem.Text;

            body += "<br /><br />" + "<b>Male Count:</b> " + tbMaleCount.Text;

            body += "<br /><br />" + "<b>Female Count:</b> " + tbFemaleCount.Text;

            body += "<br /><br />" + "<b>Date Needed:</b> " + Convert.ToDateTime(dpDateNeeded.SelectedDate.Value).ToString("dd/MMM/yyyy");

            //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbBriefDescriptionofDuties.Text;

            //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbSpecialSkills_QualificationsRequired.Text;

            //body += "<br /><br />" + "<b>Education Required:</b> " + tbEducationRequired.Text;

            body += "<br /><br />" + "<b>Employment/Work Status:</b> " + rdolstWorkStat.Items[rdolstWorkStat.SelectedIndex].Text.ToString();

            if (radcboJustification.SelectedItem.Text.Trim() == "Others(Pls. specify)")
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + radcboJustification.SelectedItem.Text.Trim() + " - " + tbHistory.Text.Trim();
            }
            else
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + radcboJustification.SelectedItem.Text.Trim() + " " + tbHistory.Text.Trim();
            }

            body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

            body += "<br />" + ConfigurationManager.AppSettings["PRWebAddress"] +

                ConfigurationManager.AppSettings["CheckerPage"];

            body += "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                + "&userempid=" + Session["EmpID"].ToString()

                + "&userrole=" + role;

            body += "<br /><br />" + "Thank You.";

            body += "<br /><br />" + "From: " + newString2;//Session["FullName_FnameFirst"].ToString().Trim();

            body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Requesting For Approval";

                mm.Subject = sub.ToUpper();

                mm.Body = body;


                var newString = rx.Replace(Session["FullName_FnameFirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    ConfigurationManager.AppSettings["MailSenderName"].ToString());

                mm.To.Add(appr_email);

                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigurationManager.AppSettings["MailServer"].ToString();

                smtp.EnableSsl = true;

                mm.IsBodyHtml = true;

                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),
                    ConfigurationManager.AppSettings["MailSenderEmailPassword"].ToString());

                smtp.Credentials = NetworkCred;

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                                                                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                                { return true; };
                smtp.Send(mm);

                
            }
        }

        protected void btnSubmitIfManager_Click(object sender, EventArgs e)
        {

        }

        protected void RadcboApprover_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //ShowMessage(RadcboApprover.SelectedValue);
        }

        protected void rdolstWorkStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowMessage(rdolstWorkStat.Items[rdolstWorkStat.SelectedIndex].Text.ToString());
        }

        protected void RadcboApprover_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FullName_LnameFirst"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmpID"].ToString();
        }

        protected void gridBriefDesc_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridBriefDesc.MasterTableView.PageSize * gridBriefDesc.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridBriefDesc_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;

                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be updated. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Updated";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridBriefDesc_ItemDeleted(object sender, GridDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be deleted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                //RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Deleted";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridBriefDesc_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridBriefDesc_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {

        }

        protected void gridBriefDesc_ItemInserted(object sender, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be inserted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);

                RadNotification1.Text = "Successfully Saved";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();
            }
        }

        protected void radcboPosition_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rowJobRequirements.Visible = true;
        }

        protected void gridSpecialSkills_QualificationsRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridSpecialSkills_QualificationsRequired.MasterTableView.PageSize * gridSpecialSkills_QualificationsRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridSpecialSkills_QualificationsRequired_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;

                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be updated. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Updated";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridSpecialSkills_QualificationsRequired_ItemDeleted(object sender, GridDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be deleted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                //RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Deleted";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridSpecialSkills_QualificationsRequired_ItemInserted(object sender, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be inserted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);

                RadNotification1.Text = "Successfully Saved";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();
            }
        }

        protected void gridEducationRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridEducationRequired.MasterTableView.PageSize * gridEducationRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridEducationRequired_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;

                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be updated. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Updated";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridEducationRequired_ItemDeleted(object sender, GridDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be deleted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                //RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                RadNotification1.Text = "Deleted";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
        }

        protected void gridEducationRequired_ItemInserted(object sender, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;

                RadNotification1.Text = "Record cannot be inserted. Reason: " + e.Exception.Message;

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Show();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openmodalSKPIGroupPolicy();", true);

                RadNotification1.Text = "Successfully Saved";

                RadNotification1.ContentIcon = string.Empty;

                RadNotification1.Title = "Notification";

                RadNotification1.Width = 200;

                RadNotification1.Height = 100;

                RadNotification1.Show();
            }
        }

        protected void RadcboApprover_DataBound(object sender, EventArgs e)
        {
            ((Literal)RadcboApprover.Footer.FindControl("RadComboItemsCount")).Text = Convert.ToString(RadcboApprover.Items.Count);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["EmpID"] = "230695-12534";

            //Session["FullName_FnameFirst"] = "IAN LEMUEL MALABANAN";

            SqlDSBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;


            if (!IsPostBack)
            {
                uc = GenerateCode();

                GetWorkStatus();

                GetDeptManager();
            }

            SqlDSGetJustification.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSGetPosition.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSGetAllDepartment.ConnectionString = ClsConfig.PISConnectionString;

            SqlDSSKPI_GetSection.ConnectionString = ClsConfig.PISConnectionString;

            GetAttachment();


            Regex rsSV = new Regex("Supervisor");
            bool containsSV = rsSV.IsMatch(Session["Position"].ToString().Trim());

            Regex rsManager = new Regex("Manager");
            bool containsManager = rsManager.IsMatch(Session["Position"].ToString().Trim());

            if (containsSV == true)
            {
                rowIfManager.Visible = false;

                rowIfSupervisor.Visible = true;
            }
            else if (containsManager == true)
            {
                rowIfManager.Visible = true;

                rowIfSupervisor.Visible = false;
            }
            else
            {
                rowIfManager.Visible = false;

                rowIfSupervisor.Visible = true;
            }

            tb_name.Text = Session["FullName_FnameFirst"].ToString().Trim();

            radcboDepartment.SelectedValue = Session["DeptCode"].ToString().Trim();
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            if (RadAsyncUpload1.UploadedFiles.Count == 0)
            {
                ShowMessage("Select File First");
            }
            else
            {
                foreach (UploadedFile file2 in RadAsyncUpload1.UploadedFiles)
                {
                    byte[] bytes2 = new byte[file2.ContentLength];

                    file2.InputStream.Read(bytes2, 0, bytes2.Length);

                    //Session["proposedfilename"] = file2.GetName();

                    //Session["proposedfiletype"] = file2.ContentType;

                    Response.Charset = string.Empty;

                    //Session["proposedbase64StringFile"] = Convert.ToBase64String(bytes2, 0, bytes2.Length);                    

                    _master = new BPersonnelRequisition();

                    _master.AttachmentName = file2.GetName();

                    _master.AttachmentFile = Convert.ToBase64String(bytes2, 0, bytes2.Length);

                    fileext = Path.GetExtension(file2.GetName());

                    if (fileext == ".pdf")
                    {
                        filetype = "application/pdf";
                    }
                    else if (fileext == ".xls")
                    {
                        filetype = "application/vnd.ms-excel";
                    }
                    else if (fileext == ".xlsx")
                    {
                        filetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else if (fileext == ".pptx")
                    {
                        filetype = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    }
                    else if (fileext == ".ppt")
                    {
                        filetype = "application/vnd.ms-powerpoint";
                    }
                    else if (fileext == ".docx")
                    {
                        filetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }
                    else if (fileext == ".doc")
                    {
                        filetype = "application/msword";
                    }

                    _master.ContentType = filetype;

                    _master.NumberOfPages = 0;

                    _master.TypeOfAttachment = DBNull.Value.ToString();

                    _master.IsSubmitted = false;

                    _master.UniqueCode = uc;

                    _master.SaveAttachments();

                    GetAttachment();
                }
            }
        }

        private void DownloadFileFromDatabase(string databytes, string attachmentname)
        {
            //Download Data From Database

            byte[] bytes = null;

            bytes = Convert.FromBase64String(databytes);

            Response.Clear();

            Response.Buffer = true;

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = ContentType;

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + attachmentname);

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            //try
            //{
            LinkButton lnkDownloadAttachment = (LinkButton)sender;

            foreach (DataRow dr in _dbmaster.GetAttachmentForDownloadDT(Convert.ToInt32(lnkDownloadAttachment.CommandArgument)).Rows)
            {
                DownloadFileFromDatabase(dr["AttachmentFile"].ToString(), dr["AttachmentName"].ToString().Trim());
            }
            //}
            //catch (System.Threading.ThreadAbortException)
            //{

            //}
            //catch (Exception es)
            //{
            //    Session["ComputerUser"] = "<b>User:</b> " + Environment.UserDomainName + @"\" + Environment.UserName;

            //    Session["ComputerName"] = "<b>Computer Name:</b> " + Environment.MachineName;

            //    Session["Message"] = "<b>Error Message:</b> " + es.Message;

            //    Session["TargetSite"] = "<b>Target Event Method:</b> " + es.TargetSite;

            //    Session["StackTrace"] = "<b>Error Details:</b> " + es.StackTrace.ToString();

            //    Session["SystemName"] = "Document Control System";

            //    Session["WebErrorUrl"] = HttpContext.Current.Request.Url.AbsoluteUri.ToString();

            //    Dispose();

            //    Session["url"] = null;

            //    Session["url"] = Request.UrlReferrer.AbsoluteUri.ToString();

            //    Response.Redirect(ConfigurationManager.AppSettings["PRWebAddress"].ToString()

            //        + ConfigurationManager.AppSettings["PageError"].ToString());
            //}
        }

        protected void lnkRemoveAttachment_Click(object sender, EventArgs e)
        {
            LinkButton lnkRemoveAttachment = (LinkButton)sender;

            _master = new BPersonnelRequisition();

            _master.RecID = Convert.ToInt32(lnkRemoveAttachment.CommandArgument);

            _master.DeleteAttachment();

            ShowMessage("Successfully Deleted");

            GetAttachment();
        }

    }
}