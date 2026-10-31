using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace CFCAIM.AIMUtilities.Imports
{
    //12/22/17 - COMMENTED IN ORDER TO COMPILE PROGRAM - AFTER VIRUS
    //             Think this was for initial Excel import -- not used anymore, 
    //             using DevExpress spreadsheet control instead.

    ////public class DisposableExcelApplication : IDisposable
    ////{

    ////    private System.Windows.Application _excelApp = null;

    ////    public void DisposableExcelApp()
    ////    {
    ////        _excelApp = new System.Windows.Application();
    ////    }

    ////    public IWorkbook WorkbooksOpen(String path, XlFileAccess access)
    ////    {
    ////        return _excelApp.Workbooks.Open(path, access);
    ////    }

    ////    public void ErrorCheckingOptions(Boolean numberAsText)
    ////    {
    ////        _excelApp.ErrorCheckingOptions.NumberAsText = numberAsText;
    ////    }

    ////    public void Dispose()
    ////    {
    ////        if (_excelApp != null)
    ////        {
    ////            _excelApp.Quit();
    ////            _excelApp = null;
    ////        }
    ////    }
    ////}
}
