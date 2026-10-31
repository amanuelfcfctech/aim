using CFCAIMCore.Entity;
using System;
using System.Collections.Generic;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Windows;

//9/16/20
//namespace CFCAIMCustom
namespace CFCAIM.Custom.Globals
{
    public class Global
    {
        public static Int32 sampleID = 0;
        public static Int32 lineNumber = 0;
        public static List<AnalysisExportBatchMaintenance> exportHeader = new List<AnalysisExportBatchMaintenance>();
        public static List<UpdateIngredientDetail> exportDetail = new List<UpdateIngredientDetail>();
        public static Boolean validationSuccess = false;

        //8/27/19
        public static Boolean blnMassExport = false;
                
        public static System.Drawing.Icon _AIMIcon;
        public System.Drawing.Icon AIMIcon
        {
            get
            {
                //return _AIMIcon;
                //return Properties.Resources.CFCAIM;
                return (System.Drawing.Icon)Properties.Resources.CFCAIM;
            }
            set
            {
                //_AIMIcon = value;
                _AIMIcon = Properties.Resources.CFCAIM;
            }
        }

        //9/2/20
        public static String checkedButton = String.Empty;
        public static String checkedConcept5Status = String.Empty;

    }
}