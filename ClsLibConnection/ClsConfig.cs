using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClsLibConnection
{
    public class ClsConfig
    {
        #region Server Name

        private static String Server_Skpi_WPCS
        {
            get { return ConfigurationManager.AppSettings["Server_SKPI-WPCS"].ToString(); }
        }

        private static String Server_Skpi_Apps1
        {
            get { return ConfigurationManager.AppSettings["Server_Skpi-Apps1"].ToString(); }
        }

        #endregion
    
        #region Database Name

        private static string DB_PersonnelRequisition
        {
            get { return ConfigurationManager.AppSettings["DB_PersonnelRequisition"].ToString(); }
        }        

        private static string DB_PIS
        {
            get { return ConfigurationManager.AppSettings["DB_PIS"].ToString(); }
        }        

        #endregion

        #region Username and Password

        private static String User_Skpi_WPCS
        {
            get { return ConfigurationManager.AppSettings["User_Skpi-WPCS"].ToString(); }  
        }

        private static String User_Skpi_Apps1
        {
            get { return ConfigurationManager.AppSettings["User_Skpi-Apps1"].ToString(); }
        }

        
        private static String Password_Skpi_WPCS
        {
            get { return ConfigurationManager.AppSettings["Password_Skpi-WPCS"].ToString(); }  
        }

        private static String Password_Skpi_Apps1
        {
            get { return ConfigurationManager.AppSettings["Password_Skpi-Apps1"].ToString(); }
        }

        #endregion
        
        #region Connection Strings

        public static String PersonnelRequisitionConnectionString
        {
            get { return string.Format(Properties.Resources.ConStr, Server_Skpi_WPCS, DB_PersonnelRequisition, User_Skpi_WPCS, Password_Skpi_WPCS); }
        }        

        public static String PISConnectionString
        {
            get { return string.Format(Properties.Resources.ConStr, Server_Skpi_Apps1, DB_PIS, User_Skpi_Apps1, Password_Skpi_Apps1); }
        }

        #endregion

        public static Int32 ProgramID
        {
            get { return 1; }
        }

        public static String ProgramVersion
        {
            get { return "1.0"; }
        }

        #region Reports

        public static String ReportServer
        {
            //get { return DesktopConfiguration.Settings["Server"]; }
            get { return ConfigurationManager.AppSettings["Server_Skpi-WPCS"].ToString(); }
        }

        public static String ReportUser
        {
            //get { return DesktopConfiguration.Settings["User"]; }
            get { return ConfigurationManager.AppSettings["User_Skpi-WPCS"].ToString(); }
        }

        public static String ReportPassword
        {
            //get { return DesktopConfiguration.Settings["Password"]; }
            get { return ConfigurationManager.AppSettings["Password_Skpi-WPCS"].ToString(); } 
        }

        public static String ReportDatabase
        {
            //get { return DesktopConfiguration.Settings["PACSISDatabase"]; }
            get { return ConfigurationManager.AppSettings["DB_OnlineReports"].ToString(); }
        }

        #endregion

       
    }
}
