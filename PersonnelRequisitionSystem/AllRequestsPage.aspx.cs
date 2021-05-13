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
using System.Text;
using System.Data.SqlClient;
using System.Drawing;

namespace PersonnelRequisitionSystem
{
    public partial class AllRequestsPage : System.Web.UI.Page
    {
        BPersonnelRequisition _dbmaster = new BPersonnelRequisition();

        BPersonnelRequisition _master = new BPersonnelRequisition();

        public static string uc = string.Empty, _remarks = string.Empty, filetype = string.Empty, fileext = string.Empty;

        public static string sender1 = string.Empty, sender2 = string.Empty
            , _useremail = string.Empty, appr_name = string.Empty
            , appr_username = string.Empty, appr_pass = string.Empty, appr_email = string.Empty
            , role = string.Empty, roledesc = string.Empty, reqdept = string.Empty
            ,_reqEmpID = string.Empty;


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

                        appr_username = dr["EmpID"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    break;
                case "03"://HR Manager

                    if (reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P" || reqdept.Trim() == "S")
                    {
                        foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                        {
                            appr_name = dr["FullName_LnameFirst"].ToString();

                            appr_username = dr["EmpID"].ToString();

                            appr_pass = dr["UserPass"].ToString();

                            appr_email = dr["EmailAddress"].ToString();

                            role = dr["UserRole"].ToString();

                            roledesc = dr["RoleDesc"].ToString();
                        }
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            RadcboApprover.DataSource = _dbmaster.GetAllManagersDT();

                            RadcboApprover.DataBind();
                        }
                    }

                    break;
                case "04"://GM
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["EmpID"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }


                    break;
                case "07"://Factory Manager
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["EmpID"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    break;
                case "08"://Factory Manager
                    foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                    {
                        appr_name = dr["FullName_LnameFirst"].ToString();

                        appr_username = dr["EmpID"].ToString();

                        appr_pass = dr["UserPass"].ToString();

                        appr_email = dr["EmailAddress"].ToString();

                        role = dr["UserRole"].ToString();

                        roledesc = dr["RoleDesc"].ToString();
                    }

                    break;
                case "05"://Vice President
                    break;
            }
        }
        
        private void ResendEmail()
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-Supervisor"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    ResendEmailToDeptManager();
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {   
                    ResendEmailToHRManager();
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    if (reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P" || reqdept.Trim() == "S")
                    {
                        ResendEmailToFactoryManager();
                    }
                    else
                    {
                        ResendEmailToAllManager();
                    }
                    //
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    ResendEmailToVicePresident();
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP")
                {
                    //SendPendingRemarksEmailToVicePresident();
                }
            }
        }

        public string GetCommentThreads()
        {
            string name = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            int index = 0;

            StringBuilder sb = new StringBuilder();

            if (_dbmaster.GetCommunicationThreadForHRDT(uc).Rows.Count == 0)
            {
                sb.Append("<div class='alert alert-info alert-dismissible' style='margin-top:5px;'>");
                //sb.Append("<h4><i class='icon fa fa-info'></i> Note!</h4>");
                sb.Append("<h3>No conversations yet. Send comments to start the conversation.</h3>");
                sb.Append("</div>");
            }
            else
            {
                sb.Append("<div class='box box-primary direct-chat direct-chat-primary'>");
                sb.Append("<div class='box-body'>");
                sb.Append(" <!-- Conversations are loaded here -->");
                sb.Append("<div class='direct-chat-messages'>");

                foreach (DataRow dr in _dbmaster.GetCommunicationThreadForHRDT(uc).Rows)
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
                //sb.Append("<div class='box-footer'>");
                //sb.Append("<div>");
                //sb.Append("<div class='input-group'>");
                //sb.Append("<input type='text' name='message' placeholder='Type Message ...' class='form-control'>");
                //sb.Append("<span class='input-group-btn'>");
                //sb.Append("<button type = 'submit' class='btn btn-primary btn-flat'>Send</button>");
                //sb.Append("</span>");
                //sb.Append("</div>");
                //sb.Append("</div>");
                //sb.Append("</div>");
                //sb.Append("<!-- /.box-footer-->");
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        private void GetRequestsFinishedPerDepartment()
        {
            gridRequests.DataSource = _dbmaster.GetRequestsFinishedPerDepartmentDT(Session["DeptCode"].ToString().Trim(), Session["SectCode"].ToString().Trim());
            gridRequests.DataBind();
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            if (RadAsyncUpload1.UploadedFiles.Count == 0)
            {
                //ShowMessage("Select File First");
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

                    GetAttachment(uc);
                }
            }
        }

        private void GetAttachment_Viewing(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridViewAttachments.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridViewAttachments.DataBind();
        }

        private void GetRequestDetails_Viewing(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                _reqEmpID = dr["EmpID"].ToString().Trim();

                reqdept = dr["ReqDeptCode"].ToString().Trim();

                tbViewControlNo.Text = dr["ControlNo"].ToString();

                tbViewDateFiled.Text = dr["DateFiled"].ToString();

                tbViewDateReceived.Text = dr["DateReceivedStr"].ToString();

                tbViewReqName.Text = dr["FullName_LnameFirst"].ToString();

                tbViewDepartment.Text = dr["DeptDesc"].ToString();

                tbViewSection.Text = dr["SectDesc"].ToString();

                tbViewMaleCount.Text = dr["MaleCount"].ToString();

                tbViewFemaleCount.Text = dr["FemaleCount"].ToString();

                tbViewTotal.Text = dr["TotalCount"].ToString();

                tbViewPosition.Text = dr["Position"].ToString();

                Session["SPosition"] = dr["Position"].ToString();

                tbViewDateNeeded.Text = dr["DateStr"].ToString();

                tbViewBriefDesc.Text = dr["BriefDescOfDuties"].ToString();

                tbViewSpecialSkills.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                tbViewEducationRequired.Text = dr["EducationRequired"].ToString();

                tbViewWorkStatus.Text = dr["StatusDesc"].ToString();

                tbViewJustification.Text = dr["JustificationDesc"].ToString();

                tbViewHistory.Text = dr["History"].ToString();

                if (string.IsNullOrEmpty(dr["Male_Endorsed"].ToString()))
                {
                    tbViewMaleEndorsed.Text = "0";
                }
                else
                {
                    tbViewMaleEndorsed.Text = dr["Male_Endorsed"].ToString();
                }

                if (string.IsNullOrEmpty(dr["Female_Endorsed"].ToString()))
                {
                    tbViewFemaleEndorsed.Text = "0";
                }
                else
                {
                    tbViewFemaleEndorsed.Text = dr["Female_Endorsed"].ToString();
                }

                if (string.IsNullOrEmpty(dr["Male_Balance"].ToString()))
                {
                    tbViewMaleBalance.Text = "0";
                }
                else
                {
                    tbViewMaleBalance.Text = dr["Male_Balance"].ToString();
                }

                if (string.IsNullOrEmpty(dr["Female_Balance"].ToString()))
                {
                    tbViewFemaleBalance.Text = "0";
                }
                else
                {
                    tbViewFemaleBalance.Text = dr["Female_Balance"].ToString();
                }

                if (dr["ApplicationStatus"].ToString().Trim() == "04")
                {
                    AlertCancelled.Visible = true;

                    _remarks = dr["ApplicationRemarks"].ToString().Trim();
                }
                else
                {
                    AlertCancelled.Visible = false;
                }

                tbViewApplicationStatus.Text = dr["AppStatus"].ToString();

                tbViewServedDate.Text = dr["ServedDateStr"].ToString();
            }

            GetAttachment_Viewing(_code);

            //Session["ViewUC"] = _code;

            uc = _code;
        }

        public void GetSignStatus(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(_code).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-Supervisor"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Processing";
                    liSignChecker.Attributes["Class"] = "active";

                    lblRemarksNoted.InnerText = "Pending";

                    lblRemarksSecondNoted.InnerText = "Pending";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = true;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    if (!IsPostBack)
                    {
                        GetDeptManager();
                    }

                    Regex rsSV = new Regex("Supervisor");
                    bool containsSV = rsSV.IsMatch(Session["Position"].ToString().Trim());

                    Regex rsManager = new Regex("Manager");
                    bool containsManager = rsManager.IsMatch(Session["Position"].ToString().Trim());

                    if (containsSV == true)
                    {
                        divApprover.Visible = true;
                    }
                    else if (containsManager == true)
                    {
                        divApprover.Visible = false;
                    }

                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-Supervisor"
                    && dr["SigningRemarks"].ToString().Trim() == "CANCELLED")
                {
                    lblRemarksSubmittion.InnerText = "Cancelled";
                    liSignPreparer.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksChecking.InnerText = "Pending";

                    lblRemarksNoted.InnerText = "Pending";

                    lblRemarksSecondNoted.InnerText = "Pending";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Processing";
                    liSignNoter.Attributes["Class"] = "active";

                    lblRemarksSecondNoted.InnerText = "Pending";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = true;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "REJECTED";
                    liSignChecker.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksNoted.InnerText = "Pending";

                    lblRemarksSecondNoted.InnerText = "Pending";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "CANCELLED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "CANCELLED";
                    liSignChecker.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksNoted.InnerText = "Pending";

                    lblRemarksSecondNoted.InnerText = "Pending";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Processing";
                    liSignSecondNoter.Attributes["Class"] = "active";

                    lblRemarksVPApproval.InnerText = "Pending";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = true;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    if (reqdept.Trim() == "E" || reqdept.Trim() == "V" || reqdept.Trim() == "P" || reqdept.Trim() == "S")
                    {
                        divApprover.Visible = false;
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            GetAllManager();
                        }

                        divApprover.Visible = true;
                    }

                    //ShowMessage(reqdept.Trim());
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Rejected";
                    liSignNoter.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksSecondNoted.InnerText = "Cancelled";
                    liSignSecondNoter.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksVPApproval.InnerText = "Cancelled";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "CANCELLED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Cancelled";
                    liSignNoter.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksSecondNoted.InnerText = "Cancelled";
                    liSignSecondNoter.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksVPApproval.InnerText = "Cancelled";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Done";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Processing";
                    liSignVPApproval.Attributes["Class"] = "active";

                    lblRemarksHRGA.InnerText = "Pending";

                    btnResend.Visible = true;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    divApprover.Visible = false;
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Rejected";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Cancelled";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "CANCELLED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Cancelled";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Cancelled";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Done";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Done";
                    liSignVPApproval.Attributes["Class"] = "completed";

                    lblRemarksHRGA.InnerText = "Processing";
                    liSignHRGA.Attributes["Class"] = "active";

                    btnResend.Visible = true;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "REJECTED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Done";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Rejected";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "CANCELLED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Done";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Cancelled";
                    liSignVPApproval.Attributes["Class"] = "rejected_cancelled";

                    lblRemarksHRGA.InnerText = "Cancelled";
                    liSignHRGA.Attributes["Class"] = "rejected_cancelled";

                    btnResend.Visible = false;

                    btnReActivate.Visible = true;

                    btnCancel.Visible = false;

                    divApprover.Visible = false;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Received"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblRemarksSubmittion.InnerText = "Done";
                    liSignPreparer.Attributes["Class"] = "completed";

                    lblRemarksChecking.InnerText = "Done";
                    liSignChecker.Attributes["Class"] = "completed";

                    lblRemarksNoted.InnerText = "Done";
                    liSignNoter.Attributes["Class"] = "completed";

                    lblRemarksSecondNoted.InnerText = "Done";
                    liSignSecondNoter.Attributes["Class"] = "completed";

                    lblRemarksVPApproval.InnerText = "Done";
                    liSignVPApproval.Attributes["Class"] = "completed";

                    lblRemarksHRGA.InnerText = "Done";
                    liSignHRGA.Attributes["Class"] = "completed";

                    btnResend.Visible = false;

                    btnReActivate.Visible = false;

                    btnCancel.Visible = true;

                    divApprover.Visible = false;
                }
            }
        }

        private void GetSigners(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignersDT(_code).Rows)
            {
                lblSignRequestor.InnerText = dr["RequestedBy"].ToString();

                lblSignDeptManager.InnerText = dr["CheckedBy"].ToString();

                lblSignHRManager.InnerText = dr["Noter"].ToString();

                lblSignGM_FactoryManager.InnerText = dr["SecondNoter"].ToString();

                lblSignVP.InnerText = dr["ApprovedBy"].ToString();
            }
        }

        private void GetAttachment(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridRecAttachment.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridRecAttachment.DataBind();
        }

        private void GetRequestDetails(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                tbRecControlNo.Text = dr["ControlNo"].ToString();

                tbRecDateFiled.Text = dr["DateFiled"].ToString();

                tbRecEmpName.Text = dr["FullName_LnameFirst"].ToString();

                tbRecDepartment.Text = dr["DeptDesc"].ToString();

                reqdept = dr["ReqDeptCode"].ToString().Trim();

                tbRecSection.Text = dr["SectDesc"].ToString();

                tbRecMaleCount.Text = dr["MaleCount"].ToString();

                tbRecFemaleCount.Text = dr["FemaleCount"].ToString();

                tbRecTotalCount.Text = dr["TotalCount"].ToString();

                tbRecPosition.Text = dr["Position"].ToString();

                Session["SPosition"] = dr["Position"].ToString();

                tbRecDateNeeded.Text = dr["DateStr"].ToString();

                tbRecBriefDescriptionofDuties.Text = dr["BriefDescOfDuties"].ToString();

                tbRecSpecialSkills_QualificationsRequired.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                tbRecEducationRequired.Text = dr["EducationRequired"].ToString();

                tbRecWorkStatus.Text = dr["StatusDesc"].ToString();

                tbRecJustification.Text = dr["JustificationDesc"].ToString();

                tbRecHistory.Text = dr["History"].ToString();

                //RadWindowManager1.RadAlert(dr["DeptDesc"].ToString(), null, null, "Notification", null);

                Session["SigningRemarks"] = dr["SigningRemarks"].ToString();
            }

            if (Session["SigningRemarks"].ToString().Trim() == "REJECTED")
            {
                rowNewMessage.Visible = false;
            }
            else
            {
                rowNewMessage.Visible = true;
            }

            GetAttachment(_code);

            GetSigners(_code);

            GetSignStatus(_code);

            //Session["RecUC"] = _code;

            uc = _code;
        }

        private void SendPendingRemarksEmailToHRManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "HR Manager");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                body += Environment.NewLine + Environment.NewLine + "See below details:";

                body += Environment.NewLine + Environment.NewLine + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                body += Environment.NewLine + Environment.NewLine + "<b>Section</b>: " + tbRecSection.Text.Trim();

                body += Environment.NewLine + Environment.NewLine + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                body += Environment.NewLine + Environment.NewLine + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                body += Environment.NewLine + Environment.NewLine + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                body += Environment.NewLine + Environment.NewLine + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                //body += Environment.NewLine + Environment.NewLine + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                //body += Environment.NewLine + Environment.NewLine + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                //body += Environment.NewLine + Environment.NewLine + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                body += Environment.NewLine + Environment.NewLine + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                {
                    body += Environment.NewLine + Environment.NewLine + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                }
                else
                {
                    body += Environment.NewLine + Environment.NewLine + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                }

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Personnel Requisition: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Approval On-Hold";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void SendPendingRemarksEmailToDeptManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Department Manager");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "New Message Has Been Created On Comment Threads.";

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Details: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Approval On-Hold";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void SendPendingRemarksEmailToGeneralManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "General Manager");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "New Message Has Been Created On Comment Threads.";

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Details: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Approval On-Hold";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void SendPendingRemarksEmailToFactoryManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Factory Manager");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "New Message Has Been Created On Comment Threads.";

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Details: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Requesting for Approval";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void SendPendingRemarksEmailToSalesManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Sales Manager");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "New Message Has Been Created On Comment Threads.";

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Personnel Requisition: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Requesting For Approval";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void SendPendingRemarksEmailToVicePresident()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Vice President");

            foreach (DataRow dr in dt.Rows)
            {
                body = "Dear Sir/Ma'am," + Environment.NewLine + Environment.NewLine + "Good Day!";

                body += Environment.NewLine + Environment.NewLine + "New Message Has Been Created On Comment Threads.";

                body += Environment.NewLine + Environment.NewLine + "Please Click On The Link Below To View The Details: ";

                body += Environment.NewLine + dr["EmailUrl"].ToString();

                body += Environment.NewLine + Environment.NewLine + "Thank You.";

                body += Environment.NewLine + Environment.NewLine + "From: " + dr["EmailFrom"].ToString().Trim();

                body += Environment.NewLine + Environment.NewLine + "Note: This is a system generated email. Please do not reply. Thank you";

                using (MailMessage mm = new MailMessage())
                {
                    string sub = "Personnel Requisition System: Approval On-Hold";

                    mm.Subject = sub.ToUpper();

                    mm.Body = body;

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                        ConfigurationManager.AppSettings["MailSenderName"].ToString());

                    mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
        }

        private void ReActivateApplication()
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand("ReActivateApplication");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", uc);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            RadWindowManager1.RadAlert("Request is now active.", null, null, "Notification", null);

            GetRequestDetails(uc);

            rowRecords.Visible = false;

            rowDetails.Visible = true;

            cardReceive.Visible = true;

            cardViewRequestDetails.Visible = false;
        }

        private Boolean SaveCommentThread(string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.EmailBody = remarks;

            _master.Sender = Session["EmpID"].ToString().Trim();

            _master.UniqueCode = uc;

            _master.SaveCommentThread();

            return true;
        }

        private void CancelApplication()
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand("CancelApplication");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", uc);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            Boolean res;

            res = SaveCommentThread("Cancellation Remarks: " + tbreason.Text.Trim());

            RadWindowManager1.RadAlert("Request successfully cancelled.", null, null, "Notification", null);

            //GetRequestDetails(uc);

            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardViewRequestDetails.Visible = false;

            Session["SigningRemarks"] = null;


        }

        private void ResendEmailToHRManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "HR Manager");

            if (dt.Rows.Count == 0)
            {
                //RadWindowManager1.RadAlert("No Logs Created. This error occured of possible network timeout. Kindly inform HRGA to reassign next approver for this application. Thank you.", 350, 350, "Message", null);

                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();

                foreach (DataRow dr in _dbmaster.GetHRManagerDT().Rows)
                {
                    appr_name = dr["FullName_LnameFirst"].ToString();

                    appr_username = dr["EmpID"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString();

                    role = dr["UserRole"].ToString();

                    roledesc = dr["RoleDesc"].ToString();
                }

                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);

                foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + dr["DeptDesc"].ToString();

                    body += "<br /><br />" + "<b>Section</b>: " + dr["SectDesc"].ToString();

                    body += "<br /><br />" + "<b>Position:</b> " + dr["Position"].ToString();

                    body += "<br /><br />" + "<b>Male Count:</b> " + dr["MaleCount"].ToString();

                    body += "<br /><br />" + "<b>Female Count:</b> " + dr["FemaleCount"].ToString();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + dr["DateStr"].ToString();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + dr["StatusDesc"].ToString();

                    if (dr["JustificationDesc"].ToString().Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " - " + dr["History"].ToString();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " " + dr["History"].ToString();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + link;

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["FullName_LnameFirst"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(appr_email);

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
            }
            else
            {     
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }

        private void ResendEmailToDeptManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Department Manager");

            if (dt.Rows.Count == 0)
            {
                //RadWindowManager1.RadAlert("No Logs Created. This error occured of possible network timeout. Kindly inform HRGA to reassign next approver for this application. Thank you.", 350, 350, "Message", null);

                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();


                foreach (DataRow dr in _dbmaster.GetDeptManagerByEmpIDDT(RadcboApprover.SelectedValue.ToString().Trim()).Rows)
                {
                    role = dr["UserRole"].ToString().Trim();

                    roledesc = dr["RoleDesc"].ToString();

                    appr_username = dr["EmpID"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString().Trim();

                    appr_name = dr["FullName_LnameFirst"].ToString();
                }

                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);

                foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + dr["DeptDesc"].ToString();

                    body += "<br /><br />" + "<b>Section</b>: " + dr["SectDesc"].ToString();

                    body += "<br /><br />" + "<b>Position:</b> " + dr["Position"].ToString();

                    body += "<br /><br />" + "<b>Male Count:</b> " + dr["MaleCount"].ToString();

                    body += "<br /><br />" + "<b>Female Count:</b> " + dr["FemaleCount"].ToString();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + dr["DateStr"].ToString();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + dr["StatusDesc"].ToString();

                    if (dr["JustificationDesc"].ToString().Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " - " + dr["History"].ToString();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " " + dr["History"].ToString();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + link;

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["FullName_LnameFirst"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(appr_email);

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
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }

        private void ResendEmailToGeneralManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "General Manager");

            if (dt.Rows.Count == 0)
            {
                RadWindowManager1.RadAlert("No Logs Created. This error occured of possible network timeout. Kindly inform HRGA to reassign next approver for this application. Thank you.", 350, 350, "Message", null);

                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();               
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }

        private void ResendEmailToFactoryManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Factory Manager");

            if (dt.Rows.Count == 0)
            {
                RadWindowManager1.RadAlert("No Logs Created. This error occured of possible network timeout. Kindly inform HRGA to reassign next approver for this application. Thank you.", 350, 350, "Message", null);

                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();

                foreach (DataRow dr in _dbmaster.GetDeptManagerByEmpIDDT(RadcboApprover.SelectedValue.ToString().Trim()).Rows)
                {
                    role = dr["UserRole"].ToString().Trim();

                    roledesc = dr["RoleDesc"].ToString();

                    appr_username = dr["EmpID"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString().Trim();

                    appr_name = dr["FullName_LnameFirst"].ToString();
                }

                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);

                foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + dr["DeptDesc"].ToString();

                    body += "<br /><br />" + "<b>Section</b>: " + dr["SectDesc"].ToString();

                    body += "<br /><br />" + "<b>Position:</b> " + dr["Position"].ToString();

                    body += "<br /><br />" + "<b>Male Count:</b> " + dr["MaleCount"].ToString();

                    body += "<br /><br />" + "<b>Female Count:</b> " + dr["FemaleCount"].ToString();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + dr["DateStr"].ToString();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + dr["StatusDesc"].ToString();

                    if (dr["JustificationDesc"].ToString().Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " - " + dr["History"].ToString();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " " + dr["History"].ToString();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + link;

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["FullName_LnameFirst"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(appr_email);

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
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }

        private void ResendEmailToSalesManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Sales Manager");

            if (dt.Rows.Count == 0)
            {
                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadWindowManager1.RadAlert("No Logs Created. This error occured of possible network timeout. Kindly inform HRGA to reassign next approver for this application. Thank you.", 350, 350, "Message", null);

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();

                foreach (DataRow dr in _dbmaster.GetDeptManagerByEmpIDDT(RadcboApprover.SelectedValue.ToString().Trim()).Rows)
                {
                    role = dr["UserRole"].ToString().Trim();

                    roledesc = dr["RoleDesc"].ToString();

                    appr_username = dr["EmpID"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString().Trim();

                    appr_name = dr["FullName_LnameFirst"].ToString();
                }

                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);

                foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + dr["DeptDesc"].ToString();

                    body += "<br /><br />" + "<b>Section</b>: " + dr["SectDesc"].ToString();

                    body += "<br /><br />" + "<b>Position:</b> " + dr["Position"].ToString();

                    body += "<br /><br />" + "<b>Male Count:</b> " + dr["MaleCount"].ToString();

                    body += "<br /><br />" + "<b>Female Count:</b> " + dr["FemaleCount"].ToString();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + dr["DateStr"].ToString();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + dr["StatusDesc"].ToString();

                    if (dr["JustificationDesc"].ToString().Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " - " + dr["History"].ToString();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " " + dr["History"].ToString();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + link;

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["FullName_LnameFirst"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(appr_email);

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

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }

        private void ResendEmailToVicePresident()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, "Vice President");

            if (dt.Rows.Count == 0)
            {
                //RadNotification1.Text = "No Logs Created. This error occured of possible network timeout. Kindly reassign next approver for this application. Thank you.";

                //RadNotification1.TitleIcon = string.Empty;

                //RadNotification1.ContentIcon = string.Empty;

                //RadNotification1.Show();

                foreach (DataRow dr in _dbmaster.GetVicePresidentDT().Rows)
                {
                    appr_name = dr["FullName_LnameFirst"].ToString();

                    appr_username = dr["EmpID"].ToString();

                    appr_pass = dr["UserPass"].ToString();

                    appr_email = dr["EmailAddress"].ToString();

                    role = dr["UserRole"].ToString();

                    roledesc = dr["RoleDesc"].ToString();
                }

                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);


            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }
        
        private void ResendEmailToAllManager()
        {
            //var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            //var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            BPersonnelRequisition _dbmaster2 = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster2.GetAllManagersByEmpIDDT(RadcboApprover.SelectedValue.ToString().Trim()).Rows)
            {
                role = dr["UserRole"].ToString().Trim();

                roledesc = dr["RoleDesc"].ToString();

                appr_username = dr["EmpID"].ToString();

                appr_pass = dr["UserPass"].ToString();

                appr_email = dr["EmailAddress"].ToString().Trim();

                appr_name = dr["FullName_LnameFirst"].ToString();
            }

            _dbmaster = new BPersonnelRequisition();

            DataTable dt;

            dt = _dbmaster.GetEmailLogsDT(uc, roledesc);

            if (dt.Rows.Count == 0)
            {   
                string link = ConfigurationManager.AppSettings["PRWebAddress"] +

                    ConfigurationManager.AppSettings["CheckerPage"]

                    + "?uc=" + uc + "&username=" + appr_username + "&userpass=" + appr_pass

                    + "&userempid=" + _reqEmpID

                    + "&userrole=" + role;


                Boolean res;

                res = _SaveEmailLogs(link, roledesc, appr_email
                    , Session["FullName_FnameFirst"].ToString().Trim(), appr_username);

                foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(uc).Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + dr["DeptDesc"].ToString();

                    body += "<br /><br />" + "<b>Section</b>: " + dr["SectDesc"].ToString();

                    body += "<br /><br />" + "<b>Position:</b> " + dr["Position"].ToString();

                    body += "<br /><br />" + "<b>Male Count:</b> " + dr["MaleCount"].ToString();

                    body += "<br /><br />" + "<b>Female Count:</b> " + dr["FemaleCount"].ToString();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + dr["DateStr"].ToString();

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + dr["StatusDesc"].ToString();

                    if (dr["JustificationDesc"].ToString().Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " - " + dr["History"].ToString();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + dr["JustificationDesc"].ToString() + " " + dr["History"].ToString();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + link;

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["FullName_LnameFirst"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(appr_email);

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
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

                    body += "<br /><br />" + "I Have Prepared A Personnel Requisition For Your Checking And Approval.";

                    body += "<br /><br />" + "See below details:";

                    body += "<br /><br />" + "<b>Department:</b> " + tbRecDepartment.Text.Trim();

                    body += "<br /><br />" + "<b>Section</b>: " + tbRecSection.Text.Trim();

                    body += "<br /><br />" + "<b>Position:</b> " + tbRecPosition.Text.Trim();

                    body += "<br /><br />" + "<b>Male Count:</b> " + tbRecMaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Female Count:</b> " + tbRecFemaleCount.Text.Trim();

                    body += "<br /><br />" + "<b>Date Needed:</b> " + tbRecDateNeeded.Text.Trim();

                    //body += "<br /><br />" + "<b>Brief Description of Duties:</b> " + tbRecBriefDescriptionofDuties.Text.Trim();

                    //body += "<br /><br />" + "<b>Special Skills/Qualifications Required:</b> " + tbRecSpecialSkills_QualificationsRequired.Text.Trim();

                    //body += "<br /><br />" + "<b>Education Required:</b> " + tbRecEducationRequired.Text;

                    body += "<br /><br />" + "<b>Employment/Work Status:</b> " + tbRecWorkStatus.Text.Trim();

                    if (tbRecJustification.Text.Trim() == "Others(Pls. specify)")
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " - " + tbRecHistory.Text.Trim();
                    }
                    else
                    {
                        body += "<br /><br />" + "<b>Justification and History:</b> " + tbRecJustification.Text.Trim() + " " + tbRecHistory.Text.Trim();
                    }

                    body += "<br /><br />" + "Please Click On The Link Below To View The Personnel Requisition: ";

                    body += "<br />" + dr["EmailUrl"].ToString();

                    body += "<br /><br />" + "Thank You.";

                    body += "<br /><br />" + "From: " + dr["EmailFrom"].ToString().Trim();

                    body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";

                    using (MailMessage mm = new MailMessage())
                    {
                        string sub = "Personnel Requisition System: Requesting for Approval";

                        mm.Subject = sub.ToUpper();

                        mm.Body = body;

                        mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                            ConfigurationManager.AppSettings["MailSenderName"].ToString());

                        mm.To.Add(dr["EmailAdd"].ToString().Trim());

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
            }
        }
               
        private void SendPendingRemarksEmailToHR()
        {
            var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = string.Empty;

            body = "Dear Sir/Ma'am," + "<br /><br />" + "Good Day!";

            body += "<br /><br />" + "Please Be Informed That The Personnel Requisition With The Control No of "
                + "\"" + tbRecControlNo.Text.Trim() + "\"" + " Has Some Remarks.";

            body += "<br /><br />" + "See Details Below.";

            body += "<br /><br />" + "\"" + tbPendingMessage.Value.ToString().Trim() + "\"";

            body += "<br /><br />" + "Note: This is a system generated email. Please do not reply. Thank you";


            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Approval On-Hold";

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

        private void ShowMessage(string msg)
        {
            RadNotification1.Text = msg;

            RadNotification1.Title = "Message";

            RadNotification1.ContentIcon = "";

            RadNotification1.TitleIcon = "";

            RadNotification1.Show();
        }




        protected void lnkRemoveAttachment_Click(object sender, EventArgs e)
        {
            LinkButton lnkRemoveAttachment = (LinkButton)sender;

            _master = new BPersonnelRequisition();

            _master.RecID = Convert.ToInt32(lnkRemoveAttachment.CommandArgument);

            _master.DeleteAttachment();

            ShowMessage("Successfully Deleted");

            GetAttachment(uc);
        }

        protected void ViewDetails(object sender, EventArgs e)
        {
            LinkButton lnkViewDetails = (LinkButton)sender;

            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(lnkViewDetails.CommandArgument.ToString().Trim()).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Received" &&
                   (dr["ApplicationStatus"].ToString().Trim() == "03" ||
                    dr["ApplicationStatus"].ToString().Trim() == "04"))
                {
                    GetRequestDetails_Viewing(lnkViewDetails.CommandArgument.ToString().Trim());

                    rowRecords.Visible = false;

                    rowDetails.Visible = true;

                    cardReceive.Visible = false;

                    cardViewRequestDetails.Visible = true;
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Received" &&
                        (dr["ApplicationStatus"].ToString().Trim() == "01" ||
                         dr["ApplicationStatus"].ToString().Trim() == "02"))
                {
                    GetRequestDetails_Viewing(lnkViewDetails.CommandArgument.ToString().Trim());

                    rowRecords.Visible = false;

                    rowDetails.Visible = true;

                    cardReceive.Visible = false;

                    cardViewRequestDetails.Visible = true;
                }
                else
                {
                    GetRequestDetails(lnkViewDetails.CommandArgument.ToString().Trim());

                    rowRecords.Visible = false;

                    rowDetails.Visible = true;

                    cardReceive.Visible = true;

                    cardViewRequestDetails.Visible = false;
                }
            }
        }

        protected void gridRecAttachment_PreRender(object sender, EventArgs e)
        {
            if (gridRecAttachment.Rows.Count > 0)
            {
                var gridView = (GridView)sender;
                var header = (GridViewRow)gridView.Controls[0].Controls[0];

                header.Cells[0].ColumnSpan = 2;
                //header.Cells[1].Text = "Header";
                header.Cells[1].Visible = false;
                //header.Cells[2].Visible = false;
            }
        }

        protected void gridViewAttachments_PreRender(object sender, EventArgs e)
        {
            if (gridViewAttachments.Rows.Count > 0)
            {
                var gridView = (GridView)sender;
                var header = (GridViewRow)gridView.Controls[0].Controls[0];

                header.Cells[0].ColumnSpan = 2;
                //header.Cells[1].Text = "Header";
                header.Cells[1].Visible = false;
                //header.Cells[2].Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = ClsConfig.PISConnectionString;

            SQLDSEndorsementLogs2.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            if (!string.IsNullOrEmpty(Session["DeptCode"].ToString()))
            {
                GetRequestsFinishedPerDepartment();

                
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
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

        protected void RefreshGrid(object sender, EventArgs e)
        {
            gridRequests.MasterTableView.FilterExpression = string.Empty;

            foreach (GridColumn column in gridRequests.MasterTableView.RenderColumns)
            {
                if (column is GridBoundColumn)
                {
                    GridBoundColumn boundColumn = column as GridBoundColumn;
                    boundColumn.CurrentFilterValue = string.Empty;
                }
            }

            gridRequests.MasterTableView.Rebind();
        }

        protected void CloseForm(object sender, EventArgs e)
        {
            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardViewRequestDetails.Visible = false;
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(uc).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-Supervisor"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    Boolean res;

                    res = SaveCommentThread(tbPendingMessage.Value.ToString().Trim());

                    SendPendingRemarksEmailToDeptManager();
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-DeptManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    Boolean res;

                    res = SaveCommentThread(tbPendingMessage.Value.ToString().Trim());

                    SendPendingRemarksEmailToHRManager();
                }
                else if (dr["SigningStatus"].ToString().Trim() == "Approved-HRManager"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    Boolean res;

                    res = SaveCommentThread(tbPendingMessage.Value.ToString().Trim());

                    _master = new BPersonnelRequisition();

                    if (_master.GetEmailLogsDT(uc, "General Manager").Rows.Count > 0)
                    {
                        SendPendingRemarksEmailToGeneralManager();
                    }
                    else if (_master.GetEmailLogsDT(uc, "Sales Manager").Rows.Count > 0)
                    {
                        SendPendingRemarksEmailToSalesManager();
                    }
                    else if (_master.GetEmailLogsDT(uc, "Factory Manager").Rows.Count > 0)
                    {
                        SendPendingRemarksEmailToFactoryManager();
                    }
                }
                else if ((dr["SigningStatus"].ToString().Trim() == "Approved-GeneralManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-FactoryManager"
                    || dr["SigningStatus"].ToString().Trim() == "Approved-SalesManager")
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    Boolean res;

                    res = SaveCommentThread(tbPendingMessage.Value.ToString().Trim());

                    SendPendingRemarksEmailToVicePresident();
                }
            }

            SendPendingRemarksEmailToHR();

            RadWindowManager1.RadAlert("Successfully Sent", null, null, "Notification", null);

            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardViewRequestDetails.Visible = false;
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

        protected void gridRecBriefDesc_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridRecBriefDesc.MasterTableView.PageSize * gridRecBriefDesc.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridRecSpecialSkills_QualificationsRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridRecSpecialSkills_QualificationsRequired.MasterTableView.PageSize * gridRecSpecialSkills_QualificationsRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            ResendEmail();

            RadWindowManager1.RadAlert("Successfully Sent", null, null, "Notification", null);

            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardViewRequestDetails.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelApplication();
        }

        protected void btnReActivate_Click(object sender, EventArgs e)
        {
            ReActivateApplication();
        }

        protected void gridRequests_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                if (item["SigningRemarks"].Text == "REJECTED")
                {
                    item["SIGNSTAT"].BackColor = Color.Red;

                    item["SIGNSTAT"].ForeColor = Color.White;
                }
                else if (item["SigningRemarks"].Text == "CANCELLED")
                {
                    item["SIGNSTAT"].BackColor = Color.Orange;

                    item["SIGNSTAT"].ForeColor = Color.White;
                }
                else if (item["SigningRemarks"].Text == "SIGNED")
                {
                    item["SIGNSTAT"].BackColor = Color.Green;

                    item["SIGNSTAT"].ForeColor = Color.White;
                }
                else
                {
                    item["SIGNSTAT"].ForeColor = Color.Black;
                }
            }
        }

        protected void gridRecEducationRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridRecEducationRequired.MasterTableView.PageSize * gridRecEducationRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }


        protected void RadcboApprover_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FullName_LnameFirst"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmpID"].ToString();
        }

        protected void RadcboApprover_DataBound(object sender, EventArgs e)
        {
            ((Literal)RadcboApprover.Footer.FindControl("RadComboItemsCount")).Text = Convert.ToString(RadcboApprover.Items.Count);
        }

        private void GetDeptManager()
        {
            _dbmaster = new BPersonnelRequisition();

            RadcboApprover.DataSource = _dbmaster.GetDeptManagerDT();

            RadcboApprover.DataTextField = "FullName_LnameFirst";

            RadcboApprover.DataValueField = "EmpID";

            RadcboApprover.DataBind();
        }

        private void GetAllManager()
        {
            _dbmaster = new BPersonnelRequisition();

            RadcboApprover.DataSource = _dbmaster.GetAllManagersDT();

            RadcboApprover.DataTextField = "FullName_LnameFirst";

            RadcboApprover.DataValueField = "EmpID";

            RadcboApprover.DataBind();
        }
    }
}