using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsLibInterface
{
    public interface IPersonnelRequisition
    {
        int? RecID { get; set; }
        string EmpID { get; set; }
        string ControlNo { get; set; }
        string ReqDeptCode { get; set; }
        string ReqDeptSectCode { get; set; }
        string Position { get; set; }
        int? MaleCount { get; set; }
        int? FemaleCount { get; set; }
        int? TotalCount { get; set; }
        int? TotalEmpProvided { get; set; }
        int? RemainingCount { get; set; }
        DateTime? DateNeeded { get; set; }
        string BriefDescOfDuties { get; set; }
        string SpecialSkillsQualificationsRequired { get; set; }
        string EducationRequired { get; set; }
        string StatusCode { get; set; }
        string JustificationCode { get; set; }
        string History { get; set; }
        string Remarks { get; set; }
        string ApplicationStatus { get; set; }
        DateTime? ServedDate { get; set; }
        string ApplicationRemarks { get; set; }
        string SigningStatus { get; set; }
        string SigningRemarks { get; set; }
        string UniqueCode { get; set; }
        DateTime? CreationDate { get; set; }
        int? UniqueCodeCounter { get; set; }
        string url { get; set; }
        string userrole { get; set; }
        string emailadd { get; set; }
        string emailfrom { get; set; }
        string sentto { get; set; }
        string RequestedBy { get; set; }
        string Remarks_RequestedBy { get; set; }
        DateTime? DateRequested { get; set; }
        string CheckedBy { get; set; }
        string Remarks_CheckedBy { get; set; }
        DateTime? DateChecked { get; set; }
        string Noter { get; set; }
        string Remarks_Noter { get; set; }
        DateTime? DateNoted { get; set; }
        string SecondNoter { get; set; }
        string Remarks_SecondNoter { get; set; }
        DateTime? DateSecondNoted { get; set; }
        string ApprovedBy { get; set; }
        string Remarks_ApprovedBy { get; set; }
        DateTime? DateApproved { get; set; }
        string ReceivedBy { get; set; }
        string Remarks_ReceivedBy { get; set; }
        DateTime? DateReceived { get; set; }
        string AttachmentFile { get; set; }
        string ContentType { get; set; }
        string AttachmentName { get; set; }
        string TypeOfAttachment { get; set; }
        int? NumberOfPages { get; set; }
        Boolean IsSubmitted { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        int? RecordCounter { get; set; }
        int? RequestMonth { get; set; }
        int? RequestYear { get; set; }
        string Sender { get; set; }
        string EmailBody { get; set; }

    }
}
