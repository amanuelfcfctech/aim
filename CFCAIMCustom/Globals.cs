using CFCAIMCore.Entity;
using System;
using System.Collections.Generic;

namespace CFCAIMCustom
{
    public class Globals
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

    }
}