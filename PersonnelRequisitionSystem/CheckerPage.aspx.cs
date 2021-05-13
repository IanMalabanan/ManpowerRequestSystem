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
    public partial class CheckerPage : System.Web.UI.Page
    {
        BPersonnelRequisition _dbmaster = new BPersonnelRequisition();

        BPersonnelRequisition _master = new BPersonnelRequisition();


        public static string sender1 = string.Empty, sender2 = string.Empty
            , uc = string.Empty, _useremail = string.Empty, appr_name = string.Empty
            , appr_username = string.Empty, appr_pass = string.Empty, appr_email = string.Empty
            , role = string.Empty, roledesc = string.Empty, reqdept = string.Empty;


        private void GetAttachment()
        {
            _dbmaster = new BPersonnelRequisition();

            gridAttachment.DataSource = _dbmaster.GetAttachmentDT(uc);

            gridAttachment.DataBind();
        }

        private void GetRequestDetails()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
            {
                tbControlNo.Text = dr["ControlNo"].ToString();

                tbDateFiled.Text = dr["DateFiled"].ToString();

                tbName.Text = dr["FullName_LnameFirst"].ToString();

                reqdept = dr["ReqDeptCode"].ToString().Trim();

                tbDepartment.Text = dr["DeptDesc"].ToString();

                tbSection.Text = dr["SectDesc"].ToString();

                tbMaleCount.Text = dr["MaleCount"].ToString();

                tbFemaleCount.Text = dr["FemaleCount"].ToString();

                tbTotalCount.Text = dr["TotalCount"].ToString();

                tbPosition.Text = dr["Position"].ToString();

                tbDateNeeded.Text = dr["DateStr"].ToString();

                //tbBriefDescriptionofDuties.Text = dr["BriefDescOfDuties"].ToString();

                //tbSpecialSkills_QualificationsRequired.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                //tbEducationRequired.Text = dr["EducationRequired"].ToString();

                tbWorkStatus.Text = dr["StatusDesc"].ToString();

                tbJustification.Text = dr["JustificationDesc"].ToString();

                tbHistory.Text = dr["History"].ToString();
            }

            GetAttachment();

            GetSignStatus();
        }

        public string GetCommentThreads()
        {
            string name = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            int index = 0;

            StringBuilder sb = new StringBuilder();

            if (_dbmaster.GetCommunicationThreadDT(uc, sender1, sender2).Rows.Count == 0)
            {
                sb.Append("<div class='alert alert-info alert-dismissible'>");
                sb.Append("<h4>No conversations yet. Send comments to start the conversation.</h4>");
                sb.Append("</div>");
            }
            else
            {
                sb.Append("<div class='box box-primary direct-chat direct-chat-primary'>");
                sb.Append("<div class='box-body'>");
                sb.Append("<div class='direct-chat-messages'>");
                foreach (DataRow dr in _dbmaster.GetCommunicationThreadDT(uc, sender1, sender2).Rows)
                {
                    _master = new BPersonnelRequisition();

                    foreach (DataRow dr1 in _master.SKPI_GetAllEmployeesByEmpIDDT(dr["Sender"].ToString().Trim()).Rows)
                    {
                        name = dr1["FullName_LnameFirst"].ToString().Trim();
                    }
                    sb.Append(" <!-- Message.Default to the left -->");
                    sb.Append("<div class='direct-chat-msg'>");
                    sb.Append(" <div class='direct-chat-info clearfix'>");
                    sb.Append(" <span class='direct-chat-name pull-left'>" + name);
                    sb.Append("</span>");
                    sb.Append(" <span class='direct-chat-timestamp pull-right'>" + "<i class='fa fa-calendar-check-o'></i>&nbsp;&nbsp;"
                        + dr["DateStr"].ToString().Trim() + "&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-clock-o'></i>&nbsp;&nbsp;"
                        + dr["TimeStr"].ToString().Trim() + "</span>");
                    sb.Append("</div>");
                    sb.Append("<!-- /.direct-chat-info -->");

                    sb.Append("<img class='direct-chat-img' src='images/mailicon1.png'><!-- /.direct-chat-img -->");
                    sb.Append("<div class='direct-chat-text'>");
                    sb.Append(dr["EmailBody"].ToString().Trim());
                    sb.Append("</div>");
                    sb.Append(" <!-- /.direct-chat-text -->");
                    sb.Append("</div>");
                    sb.Append(" <!-- /.direct-chat-msg -->");

                    index++;
                }

                sb.Append(" <!-- /.direct-chat-pane -->");
                sb.Append("</div>");
                sb.Append("<!-- /.box-body -->");
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        public void GetSignStatusDeptManager()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-Supervisor"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = true;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = true;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
            }
        }

        public void GetSignStatusHRManager()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = true;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = false;


                    if (reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P" || reqdept.Trim() == "S")
                    {
                        rowCommandButtonsOnly.Visible = true;

                        rowShowApproversForHRManager.Visible = false;
                    }
                    else
                    {
                        rowCommandButtonsOnly.Visible = false;

                        rowShowApproversForHRManager.Visible = true;
                    }                        
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = true;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
            }
        }

        public void GetSignStatusGM_FactoryManager()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = true;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = true;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
            }
        }

        public void GetSignStatusVP()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = true;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = false;

                    //GetRequestDetails();

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = true;

                    rowSuccess.Visible = false;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
                else
                {
                    rowDetails.Visible = false;

                    rowRejected.Visible = false;

                    rowSuccess.Visible = true;

                    rowCommandButtonsOnly.Visible = false;

                    rowShowApproversForHRManager.Visible = false;
                }
            }
        }

        public void GetSignStatus()
        {
            switch (Request.QueryString["userrole"].ToString().Trim())
            {
                case "02"://Dept Manager
                    GetSignStatusDeptManager();
                    break;
                case "03"://HR Manager
                    GetSignStatusHRManager();
                    break;
                case "04"://GM
                    GetSignStatusGM_FactoryManager();
                    break;
                case "07"://Factory Manager
                    GetSignStatusGM_FactoryManager();
                    break;
                case "08"://Sales Manager
                    GetSignStatusGM_FactoryManager();
                    break;
                case "05"://Vice President
                    GetSignStatusVP();
                    break;
                default:
                    break;
            }
        }

        private void GetRequestorDetails()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetUserByEmpIDDT(Request.QueryString["userempid"].ToString().Trim()).Rows)
            {
                _useremail = dr["EmailAddress"].ToString();
            }
        }

        private void SendRejectedEmail()
        {
            var rx = new Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["FullName_FnameFirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That Your Personnel Requisition Has Been Rejected By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below For The Reason.";

            body += "<br />" + "<br />" + "\"" + tbreason.Text + "\"";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Rejected Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    //ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    "Personnel Requisition System");

                //newString);

                mm.To.Add(_useremail);

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

        private void SendRejectedEmailToHR()
        {
            var rx = new Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["FullName_FnameFirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That The Personnel Requisition With The Control No of "
                + "<b>" + "\"" + tbControlNo.Text.Trim() + "\"" + "</b>" + " Is Rejected By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below For The Reason.";

            body += "<br />" + "<br />" + "\"" + tbreason.Text + "\"";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Rejected Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    //ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    "Personnel Requisition System");

                //newString);

                mm.To.Add(_useremail);

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

        private void SendPendingRemarksEmail()
        {
            var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That Your Personnel Requisition Is Set As Pending By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below.";

            body += "<br />" + "<br />" + "\"" + tbPendingRemarks.Text + "\"";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Pending Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    ConfigurationManager.AppSettings["MailSenderName"].ToString());

                mm.To.Add(_useremail);

                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigurationManager.AppSettings["MailServer"].ToString();

                smtp.EnableSsl = true;

                mm.IsBodyHtml = true;

                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailSenderEmailAddress"],
                    ConfigurationManager.AppSettings["MailSenderEmailPassword"]);

                smtp.Credentials = NetworkCred;

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                                                                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                                { return true; };
                smtp.Send(mm);
            }
        }

        private void SendPendingRemarksEmailToHR()
        {
            var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That The Personnel Requisition With The Control No of "
                + "\"" + tbControlNo.Text.Trim() + "\"" + " Is Set As Pending By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below.";

            body += "<br />" + "<br />" + "\"" + tbPendingRemarks.Text.Trim() + "\"";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Pending Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

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

                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailSenderEmailAddress"],
                    ConfigurationManager.AppSettings["MailSenderEmailPassword"]);

                smtp.Credentials = NetworkCred;

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                                                                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                                { return true; };
                smtp.Send(mm);
            }
        }

        private void SendApprovedEmail()
        {
            var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));


            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That Your Personnel Requisition With The Control No. Of "
                + "<b>" + "\"" + tbControlNo.Text.Trim() + "\"" + "</b>" + " Has Been Approved By " + newString + ".";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Approved Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    ConfigurationManager.AppSettings["MailSenderName"].ToString());

                //newString);

                mm.To.Add(_useremail);

                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigurationManager.AppSettings["MailServer"].ToString();

                smtp.EnableSsl = true;

                mm.IsBodyHtml = true;

                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailSenderEmailAddress"],
                    ConfigurationManager.AppSettings["MailSenderEmailPassword"]);

                smtp.Credentials = NetworkCred;

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                                                                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                                { return true; };
                smtp.Send(mm);
            }
        }

        private void InformHR()
        {
            var rx2 = new Regex(@"(?<=\w)\w");

            var newString2 = rx2.Replace(appr_name, new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            var newString3 = rx2.Replace(Session["FullName_FnameFirst"].ToString().Trim(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please be informed that there is a personnel request waiting for checking and approval under " + newString2 + ".";

            body += "<br />" + "<br />" + "See below details:";

            body += "<br />" + "<br />" + "<b>Department:</b> " + tbDepartment.Text.Trim();

            body += "<br />" + "<br />" + "<b>Section:</b> " + tbSection.Text.Trim();

            body += "<br />" + "<br />" + "<b>Position:</b> " + tbPosition.Text.Trim();

            body += "<br />" + "<br />" + "<b>Date Needed:</b> " + tbDateNeeded.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Brief Description of Duties:</b> " + tbBriefDescriptionofDuties.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Special Skills/Qualifications Required:</b> " + tbSpecialSkills_QualificationsRequired.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Education Required:</b> " + tbEducationRequired.Text.Trim();

            body += "<br />" + "<br />" + "<b>Employment/Work Status:</b> " + tbWorkStatus.Text.Trim();

            if (tbJustification.Text.Trim() == "Others(Pls. specify)")
            {
                body += "<br />" + "<br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " - " + tbHistory.Text.Trim();
            }
            else
            {
                body += "<br />" + "<br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " " + tbHistory.Text.Trim();
            }

            body += "<br />" + "<br />" + "Thank You.";

            body += "<br />" + "<br />" + "From: " + newString3;

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";


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

        private void SendEmailToApprover()
        {
            var rx2 = new Regex(@"(?<=\w)\w");

            var newString2 = rx2.Replace(appr_name, new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            var newString3 = rx2.Replace(Session["FullName_FnameFirst"].ToString().Trim(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + Request.QueryString["userempid"].ToString()//Session["EmpID"].ToString().Trim()

                    + "&userrole=" + role;


            Boolean res;

            res = _SaveEmailLogs(link, roledesc, appr_email
                , Session["Fullname_Fnamefirst"].ToString().Trim(), appr_username);


            string body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

            body += "<br /><br />" + "Please be informed that there is a personnel request waiting for your checking and approval.";

            body += "<br /><br />" + "See below details:";

            body += "<br /><br />" + "<b>Department:</b> " + tbDepartment.Text.Trim();

            body += "<br /><br />" + "<b>Section:</b> " + tbSection.Text.Trim();

            body += "<br /><br />" + "<b>Position:</b> " + tbPosition.Text.Trim();

            body += "<br /><br />" + "<b>Date Needed:</b> " + tbDateNeeded.Text.Trim();

            //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbBriefDescriptionofDuties.Text.Trim();

            //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbSpecialSkills_QualificationsRequired.Text.Trim();

            //body += "<br /><br />" + "<b>Education Required:</b> " + tbEducationRequired.Text.Trim();

            body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbWorkStatus.Text.Trim();

            if (tbJustification.Text.Trim() == "Others(Pls. specify)")
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " - " + tbHistory.Text.Trim();
            }
            else
            {
                body += "<br /><br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " " + tbHistory.Text.Trim();
            }

            body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

            body += "<br />" + ConfigurationManager.AppSettings["PRWebAddress"] +

                ConfigurationManager.AppSettings["CheckerPage"];

            body += "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                + "&userempid=" + Request.QueryString["userempid"].ToString()

                + "&userrole=" + role;

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

        private void SendEmailToHR()
        {
            var rx2 = new Regex(@"(?<=\w)\w");

            var newString2 = rx2.Replace(appr_name, new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            var newString3 = rx2.Replace(Session["FullName_FnameFirst"].ToString().Trim(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please be informed that there is a personnel request waiting for your checking and approval.";

            body += "<br />" + "<br />" + "See below details:";

            body += "<br />" + "<br />" + "<b>Department:</b> " + tbDepartment.Text.Trim();

            body += "<br />" + "<br />" + "<b>Section:</b> " + tbSection.Text.Trim();

            body += "<br />" + "<br />" + "<b>Position:</b> " + tbPosition.Text.Trim();

            body += "<br />" + "<br />" + "<b>Date Needed:</b> " + tbDateNeeded.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Brief Description of Duties:</b> " + tbBriefDescriptionofDuties.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Special Skills/Qualifications Required:</b> " + tbSpecialSkills_QualificationsRequired.Text.Trim();

            //body += "<br />" + "<br />" + "<b>Education Required:</b> " + tbEducationRequired.Text.Trim();

            body += "<br />" + "<br />" + "<b>Employment/Work Status:</b> " + tbWorkStatus.Text.Trim();

            if (tbJustification.Text.Trim() == "Others(Pls. specify)")
            {
                body += "<br />" + "<br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " - " + tbHistory.Text.Trim();
            }
            else
            {
                body += "<br />" + "<br />" + "<b>Justification and History:</b> " + tbJustification.Text.Trim() + " " + tbHistory.Text.Trim();
            }

            body += "<br />" + "<br />" + "Please Click On The Link Below To Redirect To Login Page: ";

            body += "<br />" + ConfigurationManager.AppSettings["PRWebAddress"];

            body += "<br />" + "<br />" + "Thank You.";

            body += "<br />" + "<br />" + "From: " + newString3;

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";


            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage())
            {
                string sub = "Personnel Requisition System: Receiving of Request";

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

        private Boolean SaveCommentThread(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.EmailBody = remarks;//tbPendingRemarks.Text.Trim();

            _master.Sender = Session["EmpID"].ToString().Trim();

            _master.UniqueCode = uc;

            _master.SaveCommentThread();

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

        private void GetApprover()
        {
            _dbmaster = new BPersonnelRequisition();

            switch (Request.QueryString["userrole"].ToString().Trim())
            {
                case "02"://Dept Manager
                    foreach (DataRow dr in _dbmaster.GetHRManagerDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["Username"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;

                    break;
                case "03"://HR Manager

                    if (reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P" || reqdept.Trim() == "S")
                    {
                        foreach (DataRow dr in _dbmaster.GetFactoryManagerDT().Rows)
                        {
                            appr_name = dr["FullName_LnameFirst"].ToString();

                            appr_username = dr["Username"].ToString();

                            appr_pass = dr["UserPass"].ToString();

                            appr_email = dr["EmailAddress"].ToString();

                            role = dr["UserRole"].ToString();

                            roledesc = dr["RoleDesc"].ToString();
                        }

                        rowCommandButtonsOnly.Visible = true;

                        rowShowApproversForHRManager.Visible = false;
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            RadcboAppover.DataSource = _dbmaster.GetAllManagersDT();

                            RadcboAppover.DataBind();
                        }

                        rowCommandButtonsOnly.Visible = false;

                        rowShowApproversForHRManager.Visible = true;
                    }

                    break;
                case "04"://GM
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["Username"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;

                    break;
                case "07"://Factory Manager
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["Username"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;

                    break;
                case "08"://Factory Manager
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["Username"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;

                    break;
                case "05"://Vice President
                    rowCommandButtonsOnly.Visible = true;

                    rowShowApproversForHRManager.Visible = false;
                    break;
            }
        }

        public Boolean UpdateSignStatus(string stat, string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.UniqueCode = uc;

            _master.SigningStatus = stat;

            _master.SigningRemarks = remarks;

            _master.UpdateSignStatus();

            return true;
        }

        protected void RadcboAppover_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (RadcboAppover.SelectedIndex > -1)
            //{
            Button2.Visible = true;

            Button3.Visible = true;

            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetAllManagersByEmpIDDT(RadcboAppover.SelectedValue.ToString().Trim()).Rows)
            {
                appr_name = dr["FullName_LnameFirst"].ToString();

                appr_username = dr["EmpID"].ToString();

                appr_pass = dr["UserPass"].ToString();

                appr_email = dr["EmailAddress"].ToString();

                role = dr["UserRole"].ToString();

                roledesc = dr["RoleDesc"].ToString();
            }

            //RadNotification1.Text = appr_name;

            //RadNotification1.Show();
            //}
            //else
            //{
            //    Button2.Visible = false;

            //    Button3.Visible = false;
            //}
        }

        protected void RadcboAppover_DataBound(object sender, EventArgs e)
        {
            //set the initial footer label
            ((Literal)RadcboAppover.Footer.FindControl("RadComboItemsCount")).Text = Convert.ToString(RadcboAppover.Items.Count);
        }

        protected void RadcboAppover_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //set the Text and Value property of every item
            //here you can set any other properties like Enabled, ToolTip, Visible, etc.
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FullName_LnameFirst"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmpID"].ToString();
        }

        public Boolean UpdateRemarksforRejected(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.UniqueCode = uc;

            _master.SigningRemarks = remarks;

            _master.UpdateRemarksforRejected();

            return true;
        }

        private Boolean SaveSignatureDeptManager(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString().Trim();

            _master.Remarks = remarks;

            _master.UniqueCode = uc;

            _master.SaveSignatureDeptManager();

            return true;
        }

        private Boolean SaveSignatureGM_FactoryManager(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString().Trim();

            _master.Remarks = remarks;

            _master.UniqueCode = uc;

            _master.SaveSignatureGM_FactoryManager();

            return true;
        }

        private Boolean SaveSignatureHRManager(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString().Trim();

            _master.Remarks = remarks;

            _master.UniqueCode = uc;

            _master.SaveSignatureHRManager();

            return true;
        }

        private Boolean SaveSignatureVP(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString().Trim();

            _master.Remarks = remarks;

            _master.UniqueCode = uc;

            _master.SaveSignatureVP();

            return true;
        }

        private void DownloadFileFromDatabase(string databytes, string attachmentname, string _contenttype)
        {
            //Download Data From Database

            byte[] bytes = null;

            bytes = Convert.FromBase64String(databytes);

            Response.Clear();

            Response.Buffer = true;

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = _contenttype;

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + attachmentname);

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            LinkButton lnkDownloadAttachment = (LinkButton)sender;

            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetAttachmentForDownloadDT(Convert.ToInt32(lnkDownloadAttachment.CommandArgument)).Rows)
            {
                DownloadFileFromDatabase(dr["AttachmentFile"].ToString(), dr["AttachmentName"].ToString().Trim(), dr["ContentType"].ToString().Trim());
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            switch (Request.QueryString["userrole"].ToString().Trim())
            {
                case "02"://Dept Manager
                    Boolean resDept;

                    resDept = UpdateSignStatus("Approved-DeptManager", "SIGNED");

                    Boolean resDept2;

                    resDept2 = SaveSignatureDeptManager("SIGNED");

                    SendApprovedEmail();

                    SendEmailToApprover();

                    InformHR();

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
                case "03"://HR Manager

                    //if(reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P")
                    //{
                    //    Boolean resFM2;

                    //    resFM2 = UpdateSignStatus("Approved-FactoryManager", "SIGNED");

                    //    Boolean resFM3;

                    //    resFM3 = SaveSignatureGM_FactoryManager("SIGNED");

                    //    SendApprovedEmail();

                    //    SendEmailToApprover();

                    //    InformHR();
                    //}
                    //else if (reqdept.Trim() == "S")
                    //{
                    //    Boolean resFM2;

                    //    resFM2 = UpdateSignStatus("Approved-SalesManager", "SIGNED");

                    //    Boolean resFM3;

                    //    resFM3 = SaveSignatureGM_FactoryManager("SIGNED");

                    //    SendApprovedEmail();

                    //    SendEmailToApprover();

                    //    InformHR();
                    //}
                    //else
                    //{
                        Boolean resHR;

                        resHR = UpdateSignStatus("Approved-HRManager", "SIGNED");

                        Boolean resHR1;

                        resHR1 = SaveSignatureHRManager("SIGNED");

                        SendApprovedEmail();

                        SendEmailToApprover();

                        InformHR();
                    //}                    

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
                case "04"://GM

                    Boolean resGM;

                    resGM = UpdateSignStatus("Approved-GeneralManager", "SIGNED");

                    Boolean resGM1;

                    resGM1 = SaveSignatureGM_FactoryManager("SIGNED");

                    SendApprovedEmail();

                    SendEmailToApprover();

                    InformHR();

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
                case "07"://Factory Manager

                    Boolean resFM;

                    resFM = UpdateSignStatus("Approved-FactoryManager", "SIGNED");

                    Boolean resFM1;

                    resFM1 = SaveSignatureGM_FactoryManager("SIGNED");

                    SendApprovedEmail();

                    SendEmailToApprover();

                    InformHR();

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
                case "08"://Sales Manager
                    Boolean resSM;
                    resSM = UpdateSignStatus("Approved-SalesManager", "SIGNED");

                    Boolean resSM1;
                    resSM1 = SaveSignatureGM_FactoryManager("SIGNED");

                    SendApprovedEmail();

                    SendEmailToApprover();

                    InformHR();

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
                case "05"://Vice President
                    Boolean resVP;
                    resVP = UpdateSignStatus("Approved-VP", "SIGNED");

                    Boolean resVP1;
                    resVP1 = SaveSignatureVP("SIGNED");

                    SendApprovedEmail();

                    SendEmailToHR();

                    GetSignStatus();

                    RadWindowManager1.RadAlert("Successfully Signed!", null, null, "Notification", "RedirectPageToMSN");

                    break;
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = SaveCommentThread(tbPendingRemarks.Text.Trim());

            SendPendingRemarksEmail();

            SendPendingRemarksEmailToHR();

            RadWindowManager1.RadAlert("Successfully Sent To Requestor!", null, null, "Notification", null);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            switch (Request.QueryString["userrole"].ToString().Trim())
            {
                case "02"://Dept Manager
                    UpdateSignStatus("Approved-DeptManager", "REJECTED");
                    SaveSignatureDeptManager("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res;
                    res = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                case "03"://HR Manager
                    UpdateSignStatus("Approved-HRManager", "REJECTED");
                    SaveSignatureHRManager("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res1;
                    res1 = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                case "04"://GM / Factory Manager
                    UpdateSignStatus("Approved-GeneralManager", "REJECTED");
                    SaveSignatureGM_FactoryManager("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res2;
                    res2 = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                case "07"://GM / Factory Manager
                    UpdateSignStatus("Approved-FactoryManager", "REJECTED");
                    SaveSignatureGM_FactoryManager("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res3;
                    res3 = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                case "08"://GM / Factory Manager
                    UpdateSignStatus("Approved-SalesManager", "REJECTED");
                    SaveSignatureGM_FactoryManager("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res4;
                    res4 = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                case "05"://Vice President
                    UpdateSignStatus("Approved-VP", "REJECTED");
                    SaveSignatureVP("REJECTED");
                    UpdateRemarksforRejected("REJECTED");
                    Boolean res5;
                    res5 = SaveCommentThread("Rejection Remarks: " + tbreason.Text);
                    SendRejectedEmail();
                    SendRejectedEmailToHR();
                    GetSignStatus();
                    RadWindowManager1.RadAlert("Successfully Rejected", null, null, "Notification", "RedirectPageToMSN");
                    break;
                default:
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDSBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            _dbmaster = new BPersonnelRequisition();

            if (_dbmaster.CheckerLogin(
                  Request.QueryString["username"].ToString().Trim()
                , Request.QueryString["userpass"].ToString().Trim()
                , Request.QueryString["userrole"].ToString().Trim()).Rows.Count == 0)
            {
                rowDetails.Visible = false;

                rowRejected.Visible = false;

                rowSuccess.Visible = false;
            }
            else
            {
                GetRequestorDetails();

                GetApprover();

                foreach (DataRow dr in _dbmaster.CheckerLogin(
                      Request.QueryString["username"].ToString().Trim()
                    , Request.QueryString["userpass"].ToString().Trim()
                    , Request.QueryString["userrole"].ToString().Trim()).Rows)
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

                this.Master.lblUserFname = "Hi!" + Session["FName"].ToString();

                uc = Request.QueryString["uc"];

                sender1 = Request.QueryString["username"];

                sender2 = Request.QueryString["userempid"];

                GetRequestDetails();
            }
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
    }
}