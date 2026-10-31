using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using DevExpress.Spreadsheet;

namespace CFCAIM.AIMUtilities.Imports
{
    //12/22/17 - COMMENTED IN ORDER TO COMPILE PROGRAM - AFTER VIRUS
    //             Think this was for initial Excel import -- not used anymore, 
    //             using DevExpress spreadsheet control instead.
    ////public class DisposableWorkbook : IDisposable
    ////{

    ////    private Workbook _workbook = null;

    ////    public void DisposableWorkbookExcel(DisposableExcelApplication appXL, String path,
    ////                              XlFileAccess access)
    ////    {
    ////        _workbook = appXL.WorkbooksOpen(path, access);
    ////    }

    ////    public Worksheet DisposableWorksheet(Int32 sheetNumber)
    ////    {
    ////        return _workbook.Sheets[sheetNumber];
    ////    }


    ////    public IWorkbook Workbook
    ////    {
    ////        get { return _workbook; }
    ////    }

    ////    public void Dispose()
    ////    {
    ////        if (_workbook != null)
    ////        {
    ////            _workbook.Close(0);
    ////            _workbook = null;
    ////        }
    ////    }
    ////}
}
