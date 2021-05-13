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
using System.Drawing;

namespace PersonnelRequisitionSystem
{
    public partial class HRRequestsPage : System.Web.UI.Page
    {
        BPersonnelRequisition _dbmaster = new BPersonnelRequisition();

        BPersonnelRequisition _master = new BPersonnelRequisition();

        public static string uc = string.Empty;


        public static string sender1 = string.Empty, sender2 = string.Empty
            , _useremail = string.Empty, appr_name = string.Empty
            , appr_username = string.Empty, appr_pass = string.Empty, appr_email = string.Empty
            , role = string.Empty, roledesc = string.Empty, reqdept = string.Empty;



        

        private void GetRequestor()
        {
            _master = new BPersonnelRequisition();

            foreach (DataRow dr in _master.GetUserByEmpIDDT(Session["UserEmpID"].ToString().Trim()).Rows)
            {
                Session["UserRole"] = dr["UserRole"].ToString();

                //Session["EmpID"] = dr["EmpID"].ToString();

                Session["UserEmail"] = dr["EmailAddress"].ToString();

                Session["UserID"] = Convert.ToInt32(dr["UserID"].ToString());
            }
        }
        
        private Boolean SaveSignatureHR(string _code,string remarks)
        {
            _master = new BPersonnelRequisition();

            _master.RequestedBy = Session["FullName_LnameFirst"].ToString();

            _master.Remarks = remarks;

            _master.UniqueCode = _code;

            _master.SaveSignatureHR();

            return true;
        }

        private Boolean MarkAsReceived(string _code)
        {
            _master = new BPersonnelRequisition();

            _master.UniqueCode = _code;

            _master.MarkAsReceived();

            SaveSignatureHR(_code, "SIGNED");

            RadWindowManager1.RadAlert("Application Successfully Received", null, null, "Notification", null);

            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewRequestDetails.Visible = false;

            GetNewRequest();

            GetRequestsForReceive();

            GetRequestsForStatusUpdate();

            GetRequestsFinished();

            Session["RecUC"] = null;

            Session["StatUC"] = null;

            return true;
        }

        private Boolean SaveEndorsementLogs()
        {
            _master = new BPersonnelRequisition();

            _master.MaleCount = Convert.ToInt32(tbStatMaleEndorsed.Text);

            _master.FemaleCount = Convert.ToInt32(tbStatFemaleEndorsed.Text);

            _master.ControlNo = tbStatControlNo.Text;

            _master.ServedDate = dpDateServed.SelectedDate.Value;

            _master.SaveEndorsementLogs();

            return true;
        }

        private Boolean UpdateStatus()
        {
            _master = new BPersonnelRequisition();

            _master.UniqueCode = uc;

            _master.ApplicationStatus = ddlAppStatus.SelectedValue.ToString().Trim();

            if(ddlAppStatus.SelectedIndex == 1)
            {
                _master.ApplicationRemarks = "N/A";

                _master.ServedDate = dpDateServed.SelectedDate.Value;
            }
            else if(ddlAppStatus.SelectedIndex == 2)
            {
                _master.ApplicationRemarks = tbHoldRemarks.Text.Trim();

                _master.ServedDate = DateTime.Now.Date;
            }
            else if (ddlAppStatus.SelectedIndex == 3)
            {
                _master.ApplicationRemarks = "COMPLETED";

                _master.ServedDate = dpDateServed.SelectedDate.Value;
            }
            else if (ddlAppStatus.SelectedIndex == 4)
            {
                _master.ApplicationRemarks = tbRejectRemarks.Text.Trim() ;

                _master.ServedDate = DateTime.Now.Date;
            }            

            _master.MaleCount = Convert.ToInt32(tbStatMaleEndorsed.Text);

            _master.FemaleCount = Convert.ToInt32(tbStatFemaleEndorsed.Text);

            _master.UpdateStatus();

            Boolean res;

            res = SaveEndorsementLogs();

            RadWindowManager1.RadAlert("Status Successfully Updated", null, null, "Notification", null);

            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewRequestDetails.Visible = false;

            GetNewRequest();

            GetRequestsForReceive();

            GetRequestsForStatusUpdate();

            GetRequestsFinished();

            //Session["RecUC"] = null;

            //Session["StatUC"] = null;

            return true;
        }

        private void GetNewRequest()
        {
            _dbmaster = new BPersonnelRequisition();

            gridNewRequests.DataSource = _dbmaster.GetNewRequestDT();

            gridNewRequests.DataBind();
        }

        private void GetRequestsForReceive()
        {
            _dbmaster = new BPersonnelRequisition();

            gridForReceive.DataSource = _dbmaster.GetRequestsForReceiveDT();

            gridForReceive.DataBind();
        }

        private void GetRequestsForStatusUpdate()
        {
            _dbmaster = new BPersonnelRequisition();

            gridForStatusUpdate.DataSource = _dbmaster.GetRequestsForStatusUpdateDT();

            gridForStatusUpdate.DataBind();
        }

        private void GetRequestsFinished()
        {
            _dbmaster = new BPersonnelRequisition();

            gridFinishedRequests.DataSource = _dbmaster.GetRequestsFinishedDT();

            gridFinishedRequests.DataBind();
        }
        
        private void GetAttachmentNew(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridNewAttachment.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridNewAttachment.DataBind();
        }
        
        private void GetAttachment(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridRecAttachment.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridRecAttachment.DataBind();
        }

        private void GetAttachmentForStatUpdate(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridStatAttachment.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridStatAttachment.DataBind();
        }

        private void GetAttachment_Viewing(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            gridViewAttachments.DataSource = _dbmaster.GetAttachmentDT(_code);

            gridViewAttachments.DataBind();
        }

        private void GetRequestDetailsNew(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                tbNewControlNo.Text = dr["ControlNo"].ToString();

                Session["UserEmpID"] = dr["EmpID"].ToString();

                tbNewDateFiled.Text = dr["DateFiled"].ToString();

                tbNewReqName.Text = dr["FullName_LnameFirst"].ToString();

                tbNewDepartment.Text = dr["DeptDesc"].ToString();

                tbNewSection.Text = dr["SectDesc"].ToString();

                tbNewMale.Text = dr["MaleCount"].ToString();

                tbNewFemale.Text = dr["FemaleCount"].ToString();

                tbNewTotal.Text = dr["TotalCount"].ToString();

                tbNewPosition.Text = dr["Position"].ToString();

                tbNewDateNeeded.Text = dr["DateStr"].ToString();

                //tbRecBriefDescriptionofDuties.Text = dr["BriefDescOfDuties"].ToString();

                //tbRecSpecialSkills_QualificationsRequired.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                //tbRecEducationRequired.Text = dr["EducationRequired"].ToString();

                tbNewWorkStatus.Text = dr["StatusDesc"].ToString();

                tbNewJustification.Text = dr["JustificationDesc"].ToString();

                tbNewHistory.Text = dr["History"].ToString();

                //RadWindowManager1.RadAlert(dr["DeptDesc"].ToString(), null, null, "Notification", null);
            }

            GetAttachmentNew(_code);

            GetSignersNew(_code);

            GetSignStatusNew(_code);

            Session["RecUC"] = _code;

            GetRequestor();

            uc = _code;
        }

        private void GetRequestDetails(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                tbRecControlNo.Text = dr["ControlNo"].ToString();

                Session["UserEmpID"] = dr["EmpID"].ToString();

                tbRecDateFiled.Text = dr["DateFiled"].ToString();

                tbRecEmpName.Text = dr["FullName_LnameFirst"].ToString();

                tbRecDepartment.Text = dr["DeptDesc"].ToString();

                tbRecSection.Text = dr["SectDesc"].ToString();

                tbRecMaleCount.Text = dr["MaleCount"].ToString();

                tbRecFemaleCount.Text = dr["FemaleCount"].ToString();

                tbRecTotalCount.Text = dr["TotalCount"].ToString();

                tbRecPosition.Text = dr["Position"].ToString();

                tbRecDateNeeded.Text = dr["DateStr"].ToString();

                tbRecBriefDescriptionofDuties.Text = dr["BriefDescOfDuties"].ToString();

                tbRecSpecialSkills_QualificationsRequired.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                tbRecEducationRequired.Text = dr["EducationRequired"].ToString();

                tbRecWorkStatus.Text = dr["StatusDesc"].ToString();

                tbRecJustification.Text = dr["JustificationDesc"].ToString();

                tbRecHistory.Text = dr["History"].ToString();

                //RadWindowManager1.RadAlert(dr["DeptDesc"].ToString(), null, null, "Notification", null);
            }

            GetAttachment(_code);

            GetSigners(_code);

            GetSignStatus(_code);

            Session["RecUC"] = _code;

            GetRequestor();

            uc = _code;
        }

        private void GetRequestDetailsForStatUpdate(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                tbStatControlNo.Text = dr["ControlNo"].ToString();

                Session["UserEmpID"] = dr["EmpID"].ToString();

                tbStatDateFiled.Text = dr["DateFiled"].ToString();

                tbStatDateReceived.Text = dr["DateReceivedStr"].ToString();

                tbStatReqName.Text = dr["FullName_LnameFirst"].ToString();

                tbStatDepartment.Text = dr["DeptDesc"].ToString();

                tbStatSection.Text = dr["SectDesc"].ToString();

                tbStatMaleCount.Text = dr["MaleCount"].ToString();

                tbStatFemaleCount.Text = dr["FemaleCount"].ToString();

                tbStatTotalCount.Text = dr["TotalCount"].ToString();

                tbStatPosition.Text = dr["Position"].ToString();

                tbStatDateNeeded.Text = dr["DateStr"].ToString();

                tbStatBriefDescOfDuties.Text = dr["BriefDescOfDuties"].ToString();

                tbStatSpecialSkills.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                tbStatEducationRequired.Text = dr["EducationRequired"].ToString();

                tbStatWorkStatus.Text = dr["StatusDesc"].ToString();

                tbStatJustification.Text = dr["JustificationDesc"].ToString();

                tbStatHistory.Text = dr["History"].ToString();

                //if (string.IsNullOrEmpty(dr["Male_Endorsed"].ToString()))
                //{
                    tbStatMaleEndorsed.Text = "0";
                //}
                //else
                //{
                   // tbStatMaleEndorsed.Text = dr["Male_Endorsed"].ToString();
                //}

                //if (string.IsNullOrEmpty(dr["Female_Endorsed"].ToString()))
                //{
                    tbStatFemaleEndorsed.Text = "0";
                //}
                //else
                //{
                //    tbStatFemaleEndorsed.Text = dr["Female_Endorsed"].ToString();
                //}

                if (string.IsNullOrEmpty(dr["Male_Balance"].ToString()))
                {
                    tbStatMaleBalance.Text = "0";
                }
                else
                {
                    tbStatMaleBalance.Text = dr["Male_Balance"].ToString();
                }

                if (string.IsNullOrEmpty(dr["Female_Balance"].ToString()))
                {
                    tbStatFemaleBalance.Text = "0";
                }
                else
                {
                    tbStatFemaleBalance.Text = dr["Female_Balance"].ToString();
                }

                if(dr["ApplicationStatus"].ToString().Trim() == "02")
                {
                    AlertOnHold.Visible = true;

                    Session["ApplicationRemarks"] = dr["ApplicationRemarks"].ToString().Trim();
                }
                else
                {
                    AlertOnHold.Visible = false;
                }

                if (!string.IsNullOrEmpty(dr["ApplicationStatus"].ToString().Trim()))
                {
                    ddlAppStatus.SelectedValue = dr["ApplicationStatus"].ToString().Trim();
                }
                else
                {
                    ddlAppStatus.SelectedIndex = 0;
                }
            }

            if (ddlAppStatus.SelectedIndex == 1 || ddlAppStatus.SelectedIndex == 3)
            {
                //served or on-going
                btnShowHold.Visible = false;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = true;
                //if (ddlAppStatus.SelectedIndex == 3)
                //{
                    colServedDate.Visible = true;

                    dpDateServed.Clear();
                //}
                //else
                //{
                //    colServedDate.Visible = false;

                //    dpDateServed.Clear();
                //}
            }
            else if (ddlAppStatus.SelectedIndex == 2)
            {
                btnShowHold.Visible = true;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = false;
                colServedDate.Visible = false;
                dpDateServed.Clear();
            }
            else if (ddlAppStatus.SelectedIndex == 4)
            {
                btnShowHold.Visible = false;
                btnShowCancel.Visible = true;
                btnUpdate.Visible = false;
                colServedDate.Visible = false;
                dpDateServed.Clear();
            }
            else
            {
                btnShowHold.Visible = false;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = false;
                colServedDate.Visible = false;
                dpDateServed.Clear();
            }

            GetAttachmentForStatUpdate(_code);

            GetRequestor();

            Session["StatUC"] = _code;

            uc = _code;
        }

        private void GetRequestDetails_Viewing(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetRequestDetailsDT(_code).Rows)
            {
                tbViewControlNo.Text = dr["ControlNo"].ToString();

                Session["UserEmpID"] = dr["EmpID"].ToString();

                tbViewDateFiled.Text = dr["DateFiled"].ToString();

                tbViewDateReceived.Text = dr["DateReceivedStr"].ToString();

                tbViewReqName.Text = dr["FullName_LnameFirst"].ToString();

                tbViewDepartment.Text = dr["DeptDesc"].ToString();

                tbViewSection.Text = dr["SectDesc"].ToString();

                tbViewMaleCount.Text = dr["MaleCount"].ToString();

                tbViewFemaleCount.Text = dr["FemaleCount"].ToString();

                tbViewTotal.Text = dr["TotalCount"].ToString();

                tbViewPosition.Text = dr["Position"].ToString();

                tbViewDateNeeded.Text = dr["DateStr"].ToString();

                tbViewBriefDesc.Text = dr["BriefDescOfDuties"].ToString();

                tbViewSpecialSkills.Text = dr["SpecialSkills_QualificationsRequired"].ToString();

                tbViewEducationRequired.Text = dr["EducationRequired"].ToString();

                tbViewWorkStatus.Text = dr["StatusDesc"].ToString();

                tbViewJustification.Text = dr["JustificationDesc"].ToString();

                tbViewHistory.Text = dr["History"].ToString();

                tbViewMaleEndorsed.Text = dr["Male_Endorsed"].ToString();

                tbViewFemaleEndorsed.Text = dr["Female_Endorsed"].ToString();

                tbViewMaleBalance.Text = dr["Male_Balance"].ToString();

                tbViewFemaleBalance.Text = dr["Female_Balance"].ToString();

                if (dr["ApplicationStatus"].ToString().Trim() == "04")
                {
                    AlertCancelled.Visible = true;

                    Session["ApplicationRemarks"] = dr["ApplicationRemarks"].ToString().Trim();
                }
                else
                {
                    AlertCancelled.Visible = false;
                }

                tbViewApplicationStatus.Text = dr["AppStatus"].ToString();

                tbViewServedDate.Text = dr["ServedDateStr"].ToString();
            }

            GetAttachment_Viewing(_code);

            GetRequestor();

            Session["ViewUC"] = _code;

            uc = _code;

            colViewServedDate.Visible = true;
        }

        public void GetSignStatusNew(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(_code).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
                    && dr["SigningRemarks"].ToString().Trim() == "SIGNED")
                {
                    lblNewPreparedByStat.InnerText = "Done";
                    liNewPreparedBy.Attributes["Class"] = "completed";

                    lblNewPreparedByStat.InnerText = "Done";
                    liNewCheckedBy.Attributes["Class"] = "completed";

                    lblNewNotedByStat.InnerText = "Done";
                    liNewNotedBy.Attributes["Class"] = "completed";

                    lblNewSecondNotedByStat.InnerText = "Done";
                    liNewSecondNotedBy.Attributes["Class"] = "completed";

                    lblNewApprovedByStat.InnerText = "Done";
                    liNewApprovedBy.Attributes["Class"] = "completed";

                    lblNewReceivedStat.InnerText = "Processing";
                    liNewReceived.Attributes["Class"] = "active";
                }
            }
        }

        public void GetSignStatus(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignStatusDT(_code).Rows)
            {
                if (dr["SigningStatus"].ToString().Trim() == "Approved-VP"
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

        private void GetSignersNew(string _code)
        {
            _dbmaster = new BPersonnelRequisition();

            foreach (DataRow dr in _dbmaster.GetSignersDT(_code).Rows)
            {
                lblNewPreparedBy.InnerText = dr["RequestedBy"].ToString();

                lblNewCheckedBy.InnerText = dr["CheckedBy"].ToString();

                lblNewNotedBy.InnerText = dr["Noter"].ToString();

                lblNewSecondNotedBy.InnerText = dr["SecondNoter"].ToString();

                lblNewApprovedBy.InnerText = dr["ApprovedBy"].ToString();
            }
        }

        private void GetApplicationStatus()
        {
            _master = new BPersonnelRequisition();

            ddlAppStatus.DataSource = _master.GetApplicationStatusDT();

            ddlAppStatus.DataTextField = "AppStatus";

            ddlAppStatus.DataValueField = "AppStatusCode";

            ddlAppStatus.DataBind();

            ddlAppStatus.Items.Insert(0, new ListItem("--Select--", DBNull.Value.ToString()));
        }

        public string NewCommentThreads(string _code)
        {
            string name = string.Empty;

            StringBuilder sb = new StringBuilder();

            _dbmaster = new BPersonnelRequisition();

            if (_dbmaster.GetCommunicationThreadForHRDT(_code).Rows.Count == 0)
            {
                sb.Append("<div class='alert alert-icon alert-primary' role='alert'>");
                sb.Append("     <i class='fe fe-alert-triangle mr-2' aria-hidden='true'></i>No conversations for this application.");
                sb.Append("</div>");
            }
            else
            {
                sb.Append("<ul class='list-group card-list-group' style='height:300px;overflow-y:overflow-y: scroll;overflow-x:hidden;'>");

                foreach (DataRow dr in _dbmaster.GetCommunicationThreadForHRDT(_code).Rows)
                {
                    _master = new BPersonnelRequisition();

                    foreach (DataRow dr1 in _master.SKPI_GetAllEmployeesByEmpIDDT(dr["Sender"].ToString().Trim()).Rows)
                    {
                        name = dr1["FullName_LnameFirst"].ToString().Trim();
                    }

                    sb.Append("<li class='list-group-item py-5'>");
                    sb.Append("<div class='media'>");
                    sb.Append("<div class='media-object avatar avatar-md mr-4' style='background-image: url(images/mailicon1.png)'></div>");
                    sb.Append("<div class='media-body'>");
                    sb.Append("<div class='media-heading'>");
                    sb.Append("<small class='float-right text-muted'>" + dr["DateStr"].ToString().Trim()
                        + "&nbsp;&nbsp;&nbsp;&nbsp;" + dr["TimeStr"].ToString().Trim() + "</small>");
                    sb.Append("<h5>" + name + "</h5>");
                    sb.Append("</div>");
                    sb.Append("<div>");
                    sb.Append(dr["EmailBody"].ToString().Trim());
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</li>");

                }

                sb.Append("</ul>");
            }

            return sb.ToString();
        }

        public string GetCommentThreads(string _code)
        {
            string name = string.Empty;

            _dbmaster = new BPersonnelRequisition();

            int index = 0;

            StringBuilder sb = new StringBuilder();

            if (_dbmaster.GetCommunicationThreadForHRDT(_code).Rows.Count == 0)
            {
                sb.Append("<div class='alert alert-info alert-dismissible'>");
                //sb.Append("<h4><i class='icon fa fa-info'></i> Note!</h4>");
                sb.Append("<h3>There are no conversations for this application.</h3>");
                sb.Append("</div>");
            }
            else
            {
                sb.Append("<div class='box box-primary direct-chat direct-chat-primary'>");
                //sb.Append("<div class='box-header with-border bg-gray-light bg-blue hide'>");
                //sb.Append("<h3 class='box-title'><b>COMMUNICATION THREAD</b></h3>");
                //sb.Append("<div class='box-tools pull-right'>");
                ////sb.Append("<span data-toggle='tooltip' title='3 New Messages' class='badge bg-light-blue'>3</span>");
                //sb.Append("<button type = 'button' class='btn btn-box-tool' data-widget='collapse'><i class='fa fa-minus'></i>");
                //sb.Append("</button>");
                //sb.Append("<button type = 'button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button>");
                //sb.Append("</div>");
                //sb.Append("</div>");
                //sb.Append("<!-- /.box-header -->");

                sb.Append("<div class='box-body'>");
                sb.Append("<div class='direct-chat-messages'>");

                foreach (DataRow dr in _dbmaster.GetCommunicationThreadForHRDT(_code).Rows)
                {
                    _master = new BPersonnelRequisition();

                    foreach (DataRow dr1 in _master.SKPI_GetAllEmployeesByEmpIDDT(dr["Sender"].ToString().Trim()).Rows)
                    {
                        name = dr1["FullName_LnameFirst"].ToString().Trim();
                    }
                    sb.Append("<div class='direct-chat-msg'>");
                    sb.Append(" <div class='direct-chat-info clearfix'>");
                    sb.Append(" <span class='direct-chat-name pull-left'>" + name);
                    sb.Append("</span>");
                    sb.Append(" <span class='direct-chat-timestamp pull-right'>" + "<i class='fa fa-calendar-check-o'></i>&nbsp;&nbsp;"
                        + dr["DateStr"].ToString().Trim() + "&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-clock-o'></i>&nbsp;&nbsp;"
                        + dr["TimeStr"].ToString().Trim() + "</span>");
                    sb.Append("</div>");
                    sb.Append("<!-- /.direct-chat-info -->");

                    sb.Append("<img class='direct-chat-img' src='../Bootstraps/AdminLTE_Bootstrap/dist/img/email-icon.png'><!-- /.direct-chat-img -->");
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

        public string HoldAlert(string msg)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='alert alert-icon alert-warning' role='alert'>");
            sb.Append("<i class='fe fe-bell mr-2' aria-hidden='true'></i>" + msg);
            sb.Append("</div>");

            return sb.ToString();
        }

        public string CancelAlert(string msg)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='alert alert-icon alert-danger' role='alert'>");
            sb.Append("<i class='fe fe-alert-triangle mr-2' aria-hidden='true'></i>" + msg);
            sb.Append("</div>");

            return sb.ToString();
        }
        
        private void LoadYear()
        {
            for (int i = DateTime.Now.Year; i >= 2010; i--)
            {
                RadDDLYear.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
            }

            RadDDLYear.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Select Year", DBNull.Value.ToString()));

            RadDDLYear.SelectedValue = DateTime.Now.Year.ToString();
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = ClsConfig.PISConnectionString;

            SQLDSEndorsementLogs.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SQLDSEndorsementLogs2.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;



            SqlDSStatBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSStatSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSStatEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;



            SqlDSRecBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSRecSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSRecEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;



            SqlDSNewBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSNewSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSNewEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;



            SqlDSViewBriefDescOfDuties.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSViewSpecialSkills_QualificationsReq.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSViewEducationRequired.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            SqlDSDisplaySummaryOfRecords.ConnectionString = ClsConfig.PersonnelRequisitionConnectionString;

            GetNewRequest();

            GetRequestsForReceive();

            GetRequestsForStatusUpdate();

            GetRequestsFinished();

            //LoadYear();


            if (!IsPostBack)
            {
                GetApplicationStatus();

                LoadYear();

                //GetRequestDetailsForStatUpdate("3y5IwM1ZbWFB5lURyCK4GEC4uVMopB8xH3TFzYwFWRw39xmyGk9");

                if (Session["RecUC"] == null || Session["StatUC"] == null || Session["ViewUC"] == null)
                {
                    rowRecords.Visible = true;

                    rowDetails.Visible = false;

                    cardReceive.Visible = false;

                    cardForStatusUpdate.Visible = false;

                    if (ddlAppStatus.SelectedIndex > 0)
                    {
                        ddlAppStatus.SelectedIndex = 0;

                        colServedDate.Visible = false;

                        btnShowCancel.Visible = false;

                        btnShowHold.Visible = false;

                        btnUpdate.Visible = false;
                    }
                }
            }
        }

        protected void gridNewRequests_ItemDataBound(object sender, GridItemEventArgs e)
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


                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridNewRequests.MasterTableView.PageSize * gridNewRequests.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString() + ".)";
            }
        }

        protected void gridForReceive_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridForReceive.MasterTableView.PageSize * gridForReceive.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString() + ".)";
            }
        }

        protected void gridForStatusUpdate_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridForStatusUpdate.MasterTableView.PageSize * gridForStatusUpdate.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString() + ".)";
            }
        }

        protected void gridFinishedRequests_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridFinishedRequests.MasterTableView.PageSize * gridFinishedRequests.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString() + ".)";
            }
        }

        protected void ddlAppStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAppStatus.SelectedIndex == 1 || ddlAppStatus.SelectedIndex == 3)
            {
                //served or on-going
                btnShowHold.Visible = false;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = true;
                if (ddlAppStatus.SelectedIndex == 3)
                {
                    colServedDate.Visible = true;

                    dpDateServed.Clear();
                }
                else
                {
                    colServedDate.Visible = false;

                    dpDateServed.Clear();
                }
            }
            else if (ddlAppStatus.SelectedIndex == 2)
            {
                
                btnShowHold.Visible = true;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = false;
            }
            else if (ddlAppStatus.SelectedIndex == 4)
            {
                btnShowHold.Visible = false;
                btnShowCancel.Visible = true;
                btnUpdate.Visible = false;
            }
            else
            {
                btnShowHold.Visible = false;
                btnShowCancel.Visible = false;
                btnUpdate.Visible = false;
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

        protected void ViewDetailsNew(object sender, EventArgs e)
        {
            LinkButton lnkViewDetailsNew = (LinkButton)sender;

            GetRequestDetailsNew(lnkViewDetailsNew.CommandArgument.ToString().Trim());

            rowRecords.Visible = false;

            rowDetails.Visible = true;

            cardViewOngoingRequest.Visible = true;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewRequestDetails.Visible = false;
        }

        protected void ViewDetailsForReceive(object sender, EventArgs e)
        {
            LinkButton lnkViewDetailsForReceive = (LinkButton)sender;

            GetRequestDetails(lnkViewDetailsForReceive.CommandArgument.ToString().Trim());

            rowRecords.Visible = false;

            rowDetails.Visible = true;

            cardReceive.Visible = true;

            cardViewOngoingRequest.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewRequestDetails.Visible = false;
        }

        protected void ViewRequestDetails(object sender, EventArgs e)
        {
            LinkButton lnkView = (LinkButton)sender;

            GetRequestDetails_Viewing(lnkView.CommandArgument.ToString().Trim());

            rowRecords.Visible = false;

            rowDetails.Visible = true;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewOngoingRequest.Visible = false;

            cardViewRequestDetails.Visible = true;
        }

        protected void ViewDetailsForStatUpdate(object sender, EventArgs e)
        {
            LinkButton lnkViewDetailsForStatUpdate = (LinkButton)sender;

            GetRequestDetailsForStatUpdate(lnkViewDetailsForStatUpdate.CommandArgument.ToString().Trim());

            rowRecords.Visible = false;

            rowDetails.Visible = true;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = true;

            cardViewOngoingRequest.Visible = false;

            cardViewRequestDetails.Visible = false;
        }

        protected void gridStatAttachment_PreRender(object sender, EventArgs e)
        {
            if (gridStatAttachment.Rows.Count > 0)
            {
                var gridView = (GridView)sender;
                var header = (GridViewRow)gridView.Controls[0].Controls[0];

                header.Cells[0].ColumnSpan = 2;
                //header.Cells[1].Text = "Header";
                header.Cells[1].Visible = false;
                //header.Cells[2].Visible = false;
            }
        }

        protected void Compute_TextChanged(object sender, EventArgs e)
        {
            int parsedValue1, parsedValue2;
            if (!int.TryParse(tbStatMaleEndorsed.Text, out parsedValue1))
            {
                RadWindowManager1.RadAlert("This is a number only field", null, null, "Notification", null);
                tbStatMaleEndorsed.Text = "0";
                return;
            }
            else if (!int.TryParse(tbStatFemaleEndorsed.Text, out parsedValue2))
            {
                RadWindowManager1.RadAlert("This is a number only field", null, null, "Notification", null);
                tbStatFemaleEndorsed.Text = "0";
                return;
            }
            else
            {
                _dbmaster = new BPersonnelRequisition();

                if (_dbmaster.CheckIfHasEndorsementDT(tbStatControlNo.Text.Trim()).Rows.Count > 0)
                {
                    foreach(DataRow dr in _dbmaster.CheckIfHasEndorsementDT(tbStatControlNo.Text.Trim()).Rows)
                    {
                        int malecount = 0;

                        int femalecount = 0;

                        malecount = Convert.ToInt32(dr["MaleBalance"].ToString().Trim()) - Convert.ToInt32(tbStatMaleEndorsed.Text);

                        femalecount = Convert.ToInt32(dr["FemaleBalance"].ToString().Trim()) - Convert.ToInt32(tbStatFemaleEndorsed.Text);

                        tbStatMaleBalance.Text = malecount.ToString();

                        tbStatFemaleBalance.Text = femalecount.ToString();
                    }
                }
                else
                {
                    int malecount = 0;

                    int femalecount = 0;

                    malecount = Convert.ToInt32(tbStatMaleCount.Text) - Convert.ToInt32(tbStatMaleEndorsed.Text);

                    femalecount = Convert.ToInt32(tbStatFemaleCount.Text) - Convert.ToInt32(tbStatFemaleEndorsed.Text);


                    tbStatMaleBalance.Text = malecount.ToString();

                    tbStatFemaleBalance.Text = femalecount.ToString();
                }
            }

            if (tbStatMaleBalance.Text == "0" && tbStatFemaleBalance.Text == "0")
            {
                ddlAppStatus.SelectedIndex = 2;
            }

        }

        protected void btnMarkasReceived_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = MarkAsReceived(Session["RecUC"].ToString().Trim());
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = UpdateStatus();
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

        protected void CloseForm(object sender, EventArgs e)
        {
            rowRecords.Visible = true;

            rowDetails.Visible = false;

            cardReceive.Visible = false;

            cardForStatusUpdate.Visible = false;

            cardViewRequestDetails.Visible = false;

            cardViewOngoingRequest.Visible = false;

            GetNewRequest();

            GetRequestsForReceive();

            GetRequestsForStatusUpdate();

            GetRequestsFinished();

            Session["RecUC"] = null;

            Session["StatUC"] = null;

            Session["ViewUC"] = null;

            if(ddlAppStatus.SelectedIndex > 0)
            {
                ddlAppStatus.SelectedIndex = 0;

                colServedDate.Visible = false;

                btnShowCancel.Visible = false;

                btnShowHold.Visible = false;

                btnUpdate.Visible = false;
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

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            GetNewRequest();

            GetRequestsForReceive();

            GetRequestsForStatusUpdate();

            GetRequestsFinished();
        }

        protected void gridStatBriefDesc_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridStatBriefDesc.MasterTableView.PageSize * gridStatBriefDesc.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridStatSpecialSkills_QualificationsRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridStatSpecialSkills_QualificationsRequired.MasterTableView.PageSize * gridStatSpecialSkills_QualificationsRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridStatEducationRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridStatEducationRequired.MasterTableView.PageSize * gridStatEducationRequired.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
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

        private void SendPendingRemarksEmail()
        {
            var rx = new System.Text.RegularExpressions.Regex(@"(?<=\w)\w");

            var newString = rx.Replace(Session["Fullname_Fnamefirst"].ToString(), new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            string body = "Dear Sir/Ma'am," + "<br />" + "<br />" + "Good Day!";

            body += "<br />" + "<br />" + "Please Be Informed That Your Personnel Requisition Is Set As Pending By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below.";

            body += "<br />" + "<br />" + "\"" + tbPendingMessage.Value + "\"";

            body += "<br />" + "<br />" + "Note: This is a system generated email. Please do not reply. Thank you";

            using (MailMessage mm = new MailMessage())
            {
                string sub = "Personnel Requisition System: Pending Request";

                mm.Subject = sub.ToUpper();

                mm.Body = body;

                mm.From = new MailAddress(ConfigurationManager.AppSettings["MailSenderEmailAddress"].ToString(),

                    ConfigurationManager.AppSettings["MailSenderName"].ToString());

                mm.To.Add(Session["UserEmail"].ToString().Trim());

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
                + "\"" + tbStatControlNo.Text.Trim() + "\"" + " Is Set As Pending By " + newString + ".";

            body += "<br />" + "<br />" + "See Details Below.";

            body += "<br />" + "<br />" + "\"" + tbPendingMessage.Value.Trim() + "\"";

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

        protected void btnStatHold_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = SaveCommentThread(tbPendingMessage.Value.Trim());

            SendPendingRemarksEmail();

            SendPendingRemarksEmailToHR();

            RadWindowManager1.RadAlert("Successfully Sent To Requestor!", null, null, "Notification", null);
        }

        protected void btnRecHold_Click(object sender, EventArgs e)
        {
            Boolean res;

            res = SaveCommentThread(tbPendingHR.Value.Trim());

            SendPendingRemarksEmail();

            //SendPendingRemarksEmailToHR();

            RadWindowManager1.RadAlert("Successfully Sent To Requestor!", null, null, "Notification", null);
        }

        protected void gridViewBriefDesc_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridViewBriefDesc.MasterTableView.PageSize * gridViewBriefDesc.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridViewSpecialSkills_QualificationsReq_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridViewSpecialSkills_QualificationsReq.MasterTableView.PageSize * gridViewSpecialSkills_QualificationsReq.MasterTableView.CurrentPageIndex;

                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
            }
        }

        protected void gridViewEducationRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int rowCounter = new int();

                Label lbl = e.Item.FindControl("numberLabel") as Label;

                rowCounter = gridViewEducationRequired.MasterTableView.PageSize * gridViewEducationRequired.MasterTableView.CurrentPageIndex;

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

        protected void gridNewAttachment_PreRender(object sender, EventArgs e)
        {
            if (gridNewAttachment.Rows.Count > 0)
            {
                var gridView = (GridView)sender;
                var header = (GridViewRow)gridView.Controls[0].Controls[0];

                header.Cells[0].ColumnSpan = 2;
                //header.Cells[1].Text = "Header";
                header.Cells[1].Visible = false;
                //header.Cells[2].Visible = false;
            }
        }

        protected void gridNewBriefDescOfDuties_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void gridNewSpecialSkills_QualificationsReq_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void gridNewEducationRequired_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
    }
}