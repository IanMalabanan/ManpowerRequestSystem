using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClsLibConnection;
using ClsLibInterface;
using System.Data.SqlClient;
using System.Data;

namespace ClsLibDataAccess
{
    public class DBPersonnelRequisition : ClsConfig
    {
        public static Boolean SavePersonnelRequisition(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SavePersonnelRequisition");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", _master.EmpID);

            cmd.Parameters.AddWithValue("@ControlNo", _master.ControlNo);

            cmd.Parameters.AddWithValue("@ReqDeptCode", _master.ReqDeptCode);

            cmd.Parameters.AddWithValue("@ReqDeptSectCode", _master.ReqDeptSectCode);

            cmd.Parameters.AddWithValue("@Position", _master.Position);

            cmd.Parameters.AddWithValue("@MaleCount", _master.MaleCount);

            cmd.Parameters.AddWithValue("@FemaleCount", _master.FemaleCount);

            cmd.Parameters.AddWithValue("@DateNeeded", _master.DateNeeded);

            cmd.Parameters.AddWithValue("@BriefDescOfDuties", _master.BriefDescOfDuties);

            cmd.Parameters.AddWithValue("@SpecialSkills_QualificationsRequired", _master.SpecialSkillsQualificationsRequired);

            cmd.Parameters.AddWithValue("@EducationRequired", _master.EducationRequired);

            cmd.Parameters.AddWithValue("@WorkStatus", _master.StatusCode);

            cmd.Parameters.AddWithValue("@JustificationCode", _master.JustificationCode);

            cmd.Parameters.AddWithValue("@History", _master.History);

            cmd.Parameters.AddWithValue("@SigningStatus", _master.SigningStatus);

            cmd.Parameters.AddWithValue("@SigningRemarks", _master.SigningRemarks);

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveUniqueCode(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveUniqueCode");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UpdateControlNo(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UpdateControlNo");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ControlNo", _master.ControlNo);

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UniqueCodeCounter(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UniqueCodeCounter");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            _master.UniqueCodeCounter = Convert.ToInt32(SqlHelper.ExecuteScalar(ClsConfig.PersonnelRequisitionConnectionString, cmd));

            return true;
        }

        public static Boolean CountControlNo(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("CountControlNo");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DeptCode", _master.ReqDeptCode);

            cmd.Parameters.AddWithValue("@SectCode", _master.ReqDeptSectCode);

            cmd.Parameters.AddWithValue("@Month", _master.RequestMonth);

            cmd.Parameters.AddWithValue("@Year", _master.RequestYear);

            _master.RecordCounter = Convert.ToInt32((SqlHelper.ExecuteScalar(ClsConfig.PersonnelRequisitionConnectionString, cmd)).ToString());

            return true;
        }


        public static Boolean SaveEmailLogs(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveEmailLogs");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@emailurl", _master.url);

            cmd.Parameters.AddWithValue("@userrole", _master.userrole);

            cmd.Parameters.AddWithValue("@email_add", _master.emailadd);

            cmd.Parameters.AddWithValue("@emailfrom", _master.emailfrom);

            cmd.Parameters.AddWithValue("@sentto", _master.sentto);

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignature(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignature");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@RequestedBy", _master.RequestedBy);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureReqManager(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureReqManager");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@RequestedBy", _master.RequestedBy);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureDeptManager(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureDeptManager");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Checker", _master.RequestedBy);

            cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureGM_FactoryManager(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureGM_FactoryManager");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Checker", _master.RequestedBy);

            cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureHRManager(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureHRManager");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Checker", _master.RequestedBy);

            cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureVP(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureVP");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Checker", _master.RequestedBy);

            cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveSignatureHR(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveSignatureHR");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Checker", _master.RequestedBy);

            cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UpdateSignStatus(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UpdateSignStatus");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Status", _master.SigningStatus);

            cmd.Parameters.AddWithValue("@Remarks", _master.SigningRemarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UpdateRemarksforRejected(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UpdateRemarksforRejected");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Remarks", _master.SigningRemarks);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveAttachments(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveAttachments");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@attachmentname", _master.AttachmentName);

            cmd.Parameters.AddWithValue("@contenttype", _master.ContentType);

            cmd.Parameters.AddWithValue("@attachmentfile", _master.AttachmentFile);

            cmd.Parameters.AddWithValue("@typeofattachment", _master.TypeOfAttachment);

            cmd.Parameters.AddWithValue("@numberofpages", _master.NumberOfPages);

            cmd.Parameters.AddWithValue("@issubmitted", _master.IsSubmitted);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean DeleteAttachment(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("DeleteAttachment");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", _master.RecID);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean MarkAsReceived(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("MarkAsReceived");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UpdateStatus(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UpdateStatus");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            cmd.Parameters.AddWithValue("@Status", _master.ApplicationStatus);

            if (string.IsNullOrEmpty(_master.ApplicationRemarks))
            {
                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Remarks", _master.ApplicationRemarks);
            }

            if (!_master.ServedDate.HasValue)
            {
                cmd.Parameters.AddWithValue("@Date", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date", _master.ServedDate);
            }

            if (!_master.MaleCount.HasValue)
            {
                cmd.Parameters.AddWithValue("@MaleEndorsed", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MaleEndorsed", _master.MaleCount);
            }

            if (!_master.FemaleCount.HasValue)
            {
                cmd.Parameters.AddWithValue("@FemaleEndorsed", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FemaleEndorsed", _master.FemaleCount);
            }

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveEndorsementLogs(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveEndorsementLogs");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ControlNo", _master.ControlNo);

            if (!_master.ServedDate.HasValue)
            {
                cmd.Parameters.AddWithValue("@ServedDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ServedDate", _master.ServedDate);
            }

            if (!_master.MaleCount.HasValue)
            {
                cmd.Parameters.AddWithValue("@MaleEndorsed", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MaleEndorsed", _master.MaleCount);
            }

            if (!_master.FemaleCount.HasValue)
            {
                cmd.Parameters.AddWithValue("@FemaleEndorsed", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FemaleEndorsed", _master.FemaleCount);
            }

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean ChangePass(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("ChangePass");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserPass", _master.Password);

            cmd.Parameters.AddWithValue("@id", _master.RecID);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean SaveCommentThread(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("SaveCommentThread");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Sender", _master.Sender);

            cmd.Parameters.AddWithValue("@EmailBody", _master.EmailBody);

            cmd.Parameters.AddWithValue("@UniqueCode", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }

        public static Boolean UpdateUniqueCode(IPersonnelRequisition _master)
        {
            SqlCommand cmd = new SqlCommand("UpdateUniqueCode");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", _master.UniqueCode);

            SqlHelper.ExecuteNonQuery(ClsConfig.PersonnelRequisitionConnectionString, cmd);

            return true;
        }








        public static DataSet GetApplicationStatus()
        {
            SqlCommand cmd = new SqlCommand("GetApplicationStatus");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetSignStatus(string uc)
        {
            SqlCommand cmd = new SqlCommand("GetSignStatus");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", uc);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet UserLogin(string user, string pass)
        {
            SqlCommand cmd = new SqlCommand("UserLogin");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", user);

            cmd.Parameters.AddWithValue("@UserPass", pass);

            //cmd.Parameters.AddWithValue("@UserRole", _master.userrole);

            //foreach (DataRow dr in SqlHelper.ExecuteDataReader(ClsConfig.PersonnelRequisitionConnectionString, cmd).Rows)
            //{
            //    _master.userrole = dr["UserRole"].ToString();

            //    _master.EmpID = dr["EmpID"].ToString();

            //    _master.emailadd = dr["EmailAddress"].ToString();

            //    _master.RecID = Convert.ToInt32(dr["UserID"].ToString());
            //}

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet UserLogin_HR(string user, string pass)
        {
            SqlCommand cmd = new SqlCommand("UserLogin_HR");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", user);

            cmd.Parameters.AddWithValue("@UserPass", pass);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet CheckerLogin(string user, string pass, string role)
        {
            SqlCommand cmd = new SqlCommand("CheckerLogin");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", user);

            cmd.Parameters.AddWithValue("@UserPass", pass);

            cmd.Parameters.AddWithValue("@UserRole", role);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetWorkStatus()
        {
            SqlCommand cmd = new SqlCommand("GetWorkStatus");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetHREmailAddressess()
        {
            SqlCommand cmd = new SqlCommand("GetHREmailAddress");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetChecker()
        {
            SqlCommand cmd = new SqlCommand("GetChecker");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetHRManager()
        {
            SqlCommand cmd = new SqlCommand("GetHRManager");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetDeptManager()
        {
            SqlCommand cmd = new SqlCommand("GetDeptManager");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetAllManagers()
        {
            SqlCommand cmd = new SqlCommand("GetAllManagers");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetAllManagersByEmpID(string empid)
        {
            SqlCommand cmd = new SqlCommand("GetAllManagersByEmpID");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", empid);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetFactoryManager()
        {
            SqlCommand cmd = new SqlCommand("GetFactoryManager");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetGeneralManager()
        {
            SqlCommand cmd = new SqlCommand("GetGeneralManager");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetVicePresident()
        {
            SqlCommand cmd = new SqlCommand("GetVicePresident");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetDeptManagerByEmpID(string empid)
        {
            SqlCommand cmd = new SqlCommand("GetDeptManagerByEmpID");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", empid);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetDepartment()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "GetAllDepartment";

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PISConnectionString, cmd);
        }

        public static DataSet GetSection(string deptcode)
        {
            SqlCommand cmd = new SqlCommand("SKPI_GetSection");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@deptcode", deptcode);

            return SqlHelper.ExecuteDataSet(ClsConfig.PISConnectionString, cmd);
        }

        public static DataSet GetAttachment(string uc)
        {
            SqlCommand cmd = new SqlCommand("GetAttachment");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", uc);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetAttachmentForDownload(int id)
        {
            SqlCommand cmd = new SqlCommand("GetAttachmentForDownload");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet SKPI_GetAllEmployeesByEmpID(string empid)
        {
            SqlCommand cmd = new SqlCommand("SKPI_GetAllEmployeesByEmpID");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empid", empid);

            return SqlHelper.ExecuteDataSet(ClsConfig.PISConnectionString, cmd);
        }

        public static DataSet GetCommunicationThread(string UniqueCode, string Sender1, string Sender2)
        {
            SqlCommand cmd = new SqlCommand("GetCommunicationThread");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", UniqueCode);

            cmd.Parameters.AddWithValue("@Sender", Sender1);

            cmd.Parameters.AddWithValue("@Receiver", Sender2);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetCommunicationThreadForHR(string UniqueCode)
        {
            SqlCommand cmd = new SqlCommand("GetCommunicationThreadForHR");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", UniqueCode);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetSigners(string UniqueCode)
        {
            SqlCommand cmd = new SqlCommand("GetSigners");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", UniqueCode);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetRequestDetails(string UniqueCode)
        {
            SqlCommand cmd = new SqlCommand("GetRequestDetails");

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UniqueCode", UniqueCode);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetUserByEmpID(string empid)
        {
            SqlCommand cmd = new SqlCommand("GetUserByEmpID");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", empid);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetNewRequest()
        {
            SqlCommand cmd = new SqlCommand("GetNewRequest");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetRequestsForReceive()
        {
            SqlCommand cmd = new SqlCommand("GetRequestsForReceive");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetRequestsForStatusUpdate()
        {
            SqlCommand cmd = new SqlCommand("GetRequestsForStatusUpdate");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetRequestsFinished()
        {
            SqlCommand cmd = new SqlCommand("GetRequestsFinished");

            cmd.CommandType = CommandType.StoredProcedure;

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetEmailLogs(string uc, string role)
        {
            SqlCommand cmd = new SqlCommand("GetEmailLogs");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uc", uc);

            cmd.Parameters.AddWithValue("@role", role);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet GetRequestsFinishedPerDepartment(string code, string sectcode)
        {
            SqlCommand cmd = new SqlCommand("GetRequestsFinishedPerDepartment");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DeptCode", code);

            cmd.Parameters.AddWithValue("@SectCode", sectcode);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet CheckIfHasEndorsement(string ControlNo)
        {
            SqlCommand cmd = new SqlCommand("CheckIfHasEndorsement");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ControlNo", ControlNo);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet CountControlNoForProduction(string deptcode, int month, int year)
        {
            SqlCommand cmd = new SqlCommand("CountControlNoForProduction");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DeptCode", deptcode);

            cmd.Parameters.AddWithValue("@Month", month);

            cmd.Parameters.AddWithValue("@Year", year);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }

        public static DataSet CountControlNoPerDeptPerSect(string deptcode, string sectcode, int month, int year)
        {
            SqlCommand cmd = new SqlCommand("CountControlNoPerDeptPerSect");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DeptCode", deptcode);

            cmd.Parameters.AddWithValue("@SectCode", sectcode);

            cmd.Parameters.AddWithValue("@Month", month);

            cmd.Parameters.AddWithValue("@Year", year);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }


        public static DataSet CountControlNoPerDept(string deptcode, int month, int year)
        {
            SqlCommand cmd = new SqlCommand("CountControlNoForProduction");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DeptCode", deptcode);

            cmd.Parameters.AddWithValue("@Month", month);

            cmd.Parameters.AddWithValue("@Year", year);

            return SqlHelper.ExecuteDataSet(ClsConfig.PersonnelRequisitionConnectionString, cmd);
        }
        

    }
}
