using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClsLibInterface;
using ClsLibDataAccess;
using System.Data;

namespace ClsLibBusiness
{
    public class BPersonnelRequisition : IPersonnelRequisition
    {
        private int? _RecID;
        private string _EmpID;
        private string _ControlNo;
        private string _ReqDeptCode;
        private string _ReqDeptSectCode;
        private string _Position;
        private int? _MaleCount;
        private int? _FemaleCount;
        private int? _TotalCount;
        private int? _TotalEmpProvided;
        private int? _RemainingCount;
        private DateTime? _DateNeeded;
        private string _BriefDescOfDuties;
        private string _SpecialSkillsQualificationsRequired;
        private string _EducationRequired;
        private string _StatusCode;
        private string _JustificationCode;
        private string _History;
        private string _Remarks;
        private string _ApplicationStatus;
        private DateTime? _ServedDate;
        private string _ApplicationRemarks;
        private string _SigningStatus;
        private string _SigningRemarks;
        private string _UniqueCode;
        private DateTime? _CreationDate;
        private int? _UniqueCodeCounter;
        private string _url;
        private string _userrole;
        private string _emailfrom;
        private string _emailadd;
        private string _sentto;
        private string _RequestedBy;
        private string _Remarks_RequestedBy;
        private DateTime? _DateRequested;
        private string _CheckedBy;
        private string _Remarks_CheckedBy;
        private DateTime? _DateChecked;
        private string _Noter;
        private string _Remarks_Noter;
        private DateTime? _DateNoted;
        private string _SecondNoter;
        private string _Remarks_SecondNoter;
        private DateTime? _DateSecondNoted;
        private string _ApprovedBy;
        private string _Remarks_ApprovedBy;
        private DateTime? _DateApproved;
        private string _ReceivedBy;
        private string _Remarks_ReceivedBy;
        private DateTime? _DateReceived;
        private string _AttachmentFile ;
        private string _ContentType ;
        private string _AttachmentName ;
        private string _TypeOfAttachment ;
        private int? _NumberOfPages ;
        private Boolean _IsSubmitted ;
        private string _Username;
        private string _Password;
        private int? _RecordCounter;
        private int? _RequestMonth;
        private int? _RequestYear;
        private string _Sender;
        private string _EmailBody;




        public int? RecID { get { return _RecID; } set { _RecID = value; } }
        public string EmpID { get { return _EmpID; } set { _EmpID = value; } }
        public string ControlNo { get { return _ControlNo; } set { _ControlNo = value; } }
        public string ReqDeptCode { get { return _ReqDeptCode; } set { _ReqDeptCode = value; } }
        public string ReqDeptSectCode { get { return _ReqDeptSectCode; } set { _ReqDeptSectCode = value; } }
        public string Position { get { return _Position; } set { _Position = value; } }
        public int? MaleCount { get { return _MaleCount; } set { _MaleCount = value; } }
        public int? FemaleCount { get { return _FemaleCount; } set { _FemaleCount = value; } }
        public int? TotalCount { get { return _TotalCount; } set { _TotalCount = value; } }
        public int? TotalEmpProvided { get { return _TotalEmpProvided; } set { _TotalEmpProvided = value; } }
        public int? RemainingCount { get { return _RemainingCount; } set { _RemainingCount = value; } }
        public DateTime? DateNeeded { get { return _DateNeeded; } set { _DateNeeded = value; } }
        public string BriefDescOfDuties { get { return _BriefDescOfDuties; } set { _BriefDescOfDuties = value; } }
        public string SpecialSkillsQualificationsRequired { get { return _SpecialSkillsQualificationsRequired; } set { _SpecialSkillsQualificationsRequired = value; } }
        public string EducationRequired { get { return _EducationRequired; } set { _EducationRequired = value; } }
        public string StatusCode { get { return _StatusCode; } set { _StatusCode = value; } }
        public string JustificationCode { get { return _JustificationCode; } set { _JustificationCode = value; } }
        public string History { get { return _History; } set { _History = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public string ApplicationStatus { get { return _ApplicationStatus; } set { _ApplicationStatus = value; } }
        public DateTime? ServedDate { get { return _ServedDate; } set { _ServedDate = value; } }
        public string ApplicationRemarks { get { return _ApplicationRemarks; } set { _ApplicationRemarks = value; } }
        public string SigningStatus { get { return _SigningStatus; } set { _SigningStatus = value; } }
        public string SigningRemarks { get { return _SigningRemarks; } set { _SigningRemarks = value; } }
        public string UniqueCode { get { return _UniqueCode; } set { _UniqueCode = value; } }
        public DateTime? CreationDate { get { return _CreationDate; } set { _CreationDate = value; } }
        public int? UniqueCodeCounter { get { return _UniqueCodeCounter; } set { _UniqueCodeCounter = value; } }
        public string url { get { return _url; } set { _url = value; } }
        public string userrole { get { return _userrole; } set { _userrole = value; } }
        public string emailadd { get { return _emailadd; } set { _emailadd = value; } }
        public string emailfrom { get { return _emailfrom; } set { _emailfrom = value; } }
        public string sentto { get { return _sentto; } set { _sentto = value; } }
        public string RequestedBy { get { return _RequestedBy; } set { _RequestedBy = value; } }
        public string Remarks_RequestedBy { get { return _Remarks_RequestedBy; } set { _Remarks_RequestedBy = value; } } 
        public DateTime? DateRequested { get { return _DateRequested; } set { _DateRequested = value; } }
        public string CheckedBy { get { return _CheckedBy; } set { _CheckedBy = value; } }
        public string Remarks_CheckedBy { get { return _Remarks_CheckedBy; } set { _Remarks_CheckedBy = value; } }
        public DateTime? DateChecked { get { return _DateChecked; } set { _DateChecked = value; } }
        public string Noter { get { return _Noter; } set { _Noter = value; } }
        public string Remarks_Noter { get { return _Remarks_Noter; } set { _Remarks_Noter = value; } }
        public DateTime? DateNoted { get { return _DateNoted; } set { _DateNoted = value; } }
        public string SecondNoter { get { return _SecondNoter; } set { _SecondNoter = value; } }
        public string Remarks_SecondNoter { get { return _Remarks_SecondNoter; } set { _Remarks_SecondNoter = value; } }
        public DateTime? DateSecondNoted { get { return _DateSecondNoted; } set { _DateSecondNoted = value; } }
        public string ApprovedBy { get { return _ApprovedBy; } set { _ApprovedBy = value; } }
        public string Remarks_ApprovedBy { get { return _Remarks_ApprovedBy; } set { _Remarks_ApprovedBy = value; } }
        public DateTime? DateApproved { get { return _DateApproved; } set { _DateApproved = value; } }
        public string ReceivedBy { get { return _ReceivedBy; } set { _ReceivedBy = value; } }
        public string Remarks_ReceivedBy { get { return _Remarks_ReceivedBy; } set { _Remarks_ReceivedBy = value; } }
        public DateTime? DateReceived { get { return _DateReceived; } set { _DateReceived = value; } }
        public string AttachmentFile { get { return _AttachmentFile; } set { _AttachmentFile = value; } }
        public string ContentType { get { return _ContentType; } set { _ContentType = value; } }
        public string AttachmentName { get { return _AttachmentName; } set { _AttachmentName = value; } }
        public string TypeOfAttachment { get { return _TypeOfAttachment; } set { _TypeOfAttachment = value; } }
        public int? NumberOfPages { get { return _NumberOfPages; } set { _NumberOfPages = value; } }
        public Boolean IsSubmitted { get { return _IsSubmitted; } set { _IsSubmitted = value; } }
        public string Username { get { return _Username; } set { _Username = value; } }
        public string Password { get { return _Password; } set { _Password = value; } }
        public int? RecordCounter { get { return _RecordCounter; } set { _RecordCounter = value; } }
        public int? RequestMonth { get { return _RequestMonth; } set { _RequestMonth = value; } }
        public int? RequestYear { get { return _RequestYear; } set { _RequestYear = value; } }
        public string Sender { get { return _Sender; } set { _Sender = value; } }
        public string EmailBody { get { return _EmailBody; } set { _EmailBody = value; } }




        public Boolean UpdateUniqueCode()
        {
            return DBPersonnelRequisition.UpdateUniqueCode(this);
        }
        
        public Boolean SavePersonnelRequisition()
        {
            return DBPersonnelRequisition.SavePersonnelRequisition(this);
        }

        public Boolean SaveUniqueCode()
        {
            return DBPersonnelRequisition.SaveUniqueCode(this);
        }

        public Boolean UniqueCodeCounters()
        {
            return DBPersonnelRequisition.UniqueCodeCounter(this);
        }

        public Boolean SaveEmailLogs()
        {
            return DBPersonnelRequisition.SaveEmailLogs(this);
        }

        public Boolean SaveSignature()
        {
            return DBPersonnelRequisition.SaveSignature(this);
        }

        public Boolean SaveSignatureReqManager()
        {
            return DBPersonnelRequisition.SaveSignatureReqManager(this);
        }
        
        public Boolean SaveAttachments()
        {
            return DBPersonnelRequisition.SaveAttachments(this);
        }

        public Boolean DeleteAttachment()
        {
            return DBPersonnelRequisition.DeleteAttachment(this);
        }

        public Boolean CountControlNo()
        {
            return DBPersonnelRequisition.CountControlNo(this);
        }

        public Boolean UpdateControlNo()
        {
            return DBPersonnelRequisition.UpdateControlNo(this);
        }

        public Boolean UpdateSignStatus()
        {
            return DBPersonnelRequisition.UpdateSignStatus(this);
        }

        public Boolean UpdateRemarksforRejected()
        {
            return DBPersonnelRequisition.UpdateRemarksforRejected(this);
        }

        public Boolean SaveSignatureDeptManager()
        {
            return DBPersonnelRequisition.SaveSignatureDeptManager(this);
        }

        public Boolean SaveSignatureGM_FactoryManager()
        {
            return DBPersonnelRequisition.SaveSignatureGM_FactoryManager(this);
        }

        public Boolean SaveSignatureHRManager()
        {
            return DBPersonnelRequisition.SaveSignatureHRManager(this);
        }

        public Boolean SaveSignatureVP()
        {
            return DBPersonnelRequisition.SaveSignatureVP(this);
        }

        public Boolean SaveSignatureHR()
        {
            return DBPersonnelRequisition.SaveSignatureHR(this);
        }

        public Boolean MarkAsReceived()
        {
            return DBPersonnelRequisition.MarkAsReceived(this);
        }

        public Boolean UpdateStatus()
        {
            return DBPersonnelRequisition.UpdateStatus(this);
        }

        public Boolean SaveEndorsementLogs()
        {
            return DBPersonnelRequisition.SaveEndorsementLogs(this);
        }

        public Boolean ChangePass()
        {
            return DBPersonnelRequisition.ChangePass(this);
        }

        public Boolean SaveCommentThread()
        {
            return DBPersonnelRequisition.SaveCommentThread(this);
        }        





        public DataTable GetApplicationStatusDT()
        {
            return DBPersonnelRequisition.GetApplicationStatus().Tables[0];
        }

        public DataTable UserLogin(string user, string pass)
        {
            return DBPersonnelRequisition.UserLogin(user, pass).Tables[0];
        }

        public DataTable UserLogin_HR(string user, string pass)
        {
            return DBPersonnelRequisition.UserLogin_HR(user, pass).Tables[0];
        }        

        public DataTable CheckerLogin(string user, string pass, string role)
        {
            return DBPersonnelRequisition.CheckerLogin(user, pass, role).Tables[0];
        }

        public DataTable GetWorkStatusDT()
        {
            return DBPersonnelRequisition.GetWorkStatus().Tables[0];
        }
        
        public DataTable GetHREmailAddressessDT()
        {
            return DBPersonnelRequisition.GetHREmailAddressess().Tables[0];
        }

        public DataTable GetCheckerDT()
        {
            return DBPersonnelRequisition.GetChecker().Tables[0];
        }

        public DataTable GetHRManagerDT()
        {
            return DBPersonnelRequisition.GetHRManager().Tables[0];
        }

        public DataTable GetDeptManagerDT()
        {
            return DBPersonnelRequisition.GetDeptManager().Tables[0];
        }

        public DataTable GetGeneralManagerDT()
        {
            return DBPersonnelRequisition.GetGeneralManager().Tables[0];
        }

        public DataTable GetVicePresidentDT()
        {
            return DBPersonnelRequisition.GetVicePresident().Tables[0];
        }

        public DataTable GetAllManagersDT()
        {
            return DBPersonnelRequisition.GetAllManagers().Tables[0];
        }

        public DataTable GetAllManagersByEmpIDDT(string empid)
        {
            return DBPersonnelRequisition.GetAllManagersByEmpID(empid).Tables[0];
        }        

        public DataTable GetFactoryManagerDT()
        {
            return DBPersonnelRequisition.GetFactoryManager().Tables[0];
        }        

        public DataTable GetDeptManagerByEmpIDDT(string empid)
        {
            return DBPersonnelRequisition.GetDeptManagerByEmpID(empid).Tables[0];
        }

        public DataTable GetUserByEmpIDDT(string empid)
        {
            return DBPersonnelRequisition.GetUserByEmpID(empid).Tables[0];
        }        

        public DataTable GetDepartmentDT()
        {
            return DBPersonnelRequisition.GetDepartment().Tables[0];
        }

        public DataTable GetSectionDT(string deptcode)
        {
            return DBPersonnelRequisition.GetSection(deptcode).Tables[0];
        }

        public DataTable GetAttachmentDT(string uc)
        {
            return DBPersonnelRequisition.GetAttachment(uc).Tables[0];
        }

        public DataTable GetSignStatusDT(string uc)
        {
            return DBPersonnelRequisition.GetSignStatus(uc).Tables[0];
        }
        
        public DataTable GetAttachmentForDownloadDT(int id)
        {
            return DBPersonnelRequisition.GetAttachmentForDownload(id).Tables[0];
        }

        public DataTable SKPI_GetAllEmployeesByEmpIDDT(string empid)
        {
            return DBPersonnelRequisition.SKPI_GetAllEmployeesByEmpID(empid).Tables[0];
        }

        public DataTable GetCommunicationThreadDT(string UniqueCode, string Sender1, string Sender2)
        {
            return DBPersonnelRequisition.GetCommunicationThread(UniqueCode, Sender1,Sender2).Tables[0];
        }

        public DataTable GetCommunicationThreadForHRDT(string UniqueCode)
        {
            return DBPersonnelRequisition.GetCommunicationThreadForHR(UniqueCode).Tables[0];
        }        

        public DataTable GetRequestDetailsDT(string UniqueCode)
        {
            return DBPersonnelRequisition.GetRequestDetails(UniqueCode).Tables[0];
        }

        public DataTable GetSignersDT(string UniqueCode)
        {
            return DBPersonnelRequisition.GetSigners(UniqueCode).Tables[0];
        }        

        public DataTable GetNewRequestDT()
        {
            return DBPersonnelRequisition.GetNewRequest().Tables[0];
        }

        public DataTable GetRequestsForReceiveDT()
        {
            return DBPersonnelRequisition.GetRequestsForReceive().Tables[0];
        }

        public DataTable GetRequestsForStatusUpdateDT()
        {
            return DBPersonnelRequisition.GetRequestsForStatusUpdate().Tables[0];
        }

        public DataTable GetRequestsFinishedDT()
        {
            return DBPersonnelRequisition.GetRequestsFinished().Tables[0];
        }

        public DataTable GetRequestsFinishedPerDepartmentDT(string code, string sectcode)
        {
            return DBPersonnelRequisition.GetRequestsFinishedPerDepartment(code, sectcode).Tables[0];
        }

        public DataTable GetEmailLogsDT(string code, string role)
        {
            return DBPersonnelRequisition.GetEmailLogs(code, role).Tables[0];
        }

        public DataTable CheckIfHasEndorsementDT(string ControlNo)
        {
            return DBPersonnelRequisition.CheckIfHasEndorsement(ControlNo).Tables[0];
        }

        public DataTable CountControlNoForProductionDT(string deptcode, int month, int year)
        {
            return DBPersonnelRequisition.CountControlNoForProduction(deptcode, month, year).Tables[0];
        }

        public DataTable CountControlNoPerDeptPerSectDT(string deptcode, string sectcode, int month, int year)
        {
            return DBPersonnelRequisition.CountControlNoPerDeptPerSect(deptcode,sectcode ,month, year).Tables[0];
        }

        public DataTable CountControlNoPerDeptDT(string deptcode, int month, int year)
        {
            return DBPersonnelRequisition.CountControlNoPerDept(deptcode, month, year).Tables[0];
        }
        


    }
}
