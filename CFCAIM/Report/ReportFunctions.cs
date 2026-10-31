using System;
using System.Collections.Generic;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Test;
using DevExpress.XtraReports.UI;

namespace CFCAIM.Report
{
	public class ReportFunctions
	{		

		public static List<AnalysisReportsInfo> DataSetAnalysisSummary;
		public static List<AimNutrientRefAnalyzerNutrientOrphanInfo> DataSetAimAnalyzerNutrientRef;
		public static List<AimMaterialRefAnalyzerMaterialOrphanInfo> DataSetAimAnalyzerMaterialRef;		
		public static List<AimSourceLocationGuaranteeReportInfo> DataSetSourceLocationGuarantee;
		public static List<SampleComparisonReportInfo> DataSetSampleComparison;
        public static List<SampleActivityReportInfo> DataSetSampleActivity;        
        public static List<SampleResultsBySupplierInfo> DataSetSampleResultsBySupplier;
        public static List<ChartInfo> DataSetTrend;	   
        public static List<ManufacturedProductComplianceReportInfo> DataSetMfgProductCompliance;
		public static List<SampleResultsComparisonReportInfo> DataSetSampleResultsComparison;           //5/20/22
		public static List<SampleHeaderInfo> DataSetSamplesByOwnerLoc;                                  //6/27/22
		public static List<SampleSubmittedSamplesReportInfo> DataSetSampleSubmitted;                    //8/16/23
		public static List<SampleAnalysisRequestFormInfo> DataSetSampleAnalysisRequestForm;             //9/1/23
		public static List<TestGridInfo> DataSetTestGrid;                                               //Task1150 DB260113
		public static DateTime fromDate;                                                                //Task1150 DB260210
        public static DateTime toDate;                                                                  //Task1150 DB260210
		public static String locationName;																//Task1150 DB260210

        public static void LoadReport(Int32 reportID)
		{			

			switch (reportID)
			{
				case (Int32)ReportEnum.AnalysisSupplierComparison:
					LoadAnalysisSourceComparisonReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.AnalysisSummary:
					LoadAnalysisSummaryReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.AnalysisDetail:
					LoadAnalysisDetailReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.AnalysisDateComparisonReport:
					LoadAnalysisDateComparisonReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.GuaranteeComparisonByMean:
					LoadGuaranteeComparisonByMeanReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.GuaranteeComparisonBySample:
					LoadGuaranteeComparisonBySampleReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.ProductSpecificationComparisonByMean:
					LoadProductSpecificationComparisonByMeanReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.ProductSpecificationComparisonBySample:
					LoadProductSpecificationComparisonBySampleReport(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.AimNutrientRefAnalyzerNutrientOphans:
					LoadAimNutrientRefAnalyzerNutrientOphansReport(DataSetAimAnalyzerNutrientRef);
					break;
				case (Int32)ReportEnum.AimMaterialRefAnalyzerMaterialOphans:
					LoadAimMaterialRefAnalyzerMaterialOphansReport(DataSetAimAnalyzerMaterialRef);
					break;
				case (Int32)ReportEnum.ExportBatchDetail:
					LoadExportBatchDetailReport(CFCAIMCore.Global.DataSetExportBatchDetail);
					break;
				case (Int32)ReportEnum.AnalysisPivotReport:
					LoadAnalysisPivotReport(DataSetAnalysisSummary);					
					break;                
                case (Int32)ReportEnum.AnalysisPivotReportGroupSummary:                                     //1/19/21
					LoadAnalysisPivotReportGroupSummary(DataSetAnalysisSummary);
                    break;
				case (Int32)ReportEnum.AnalysisPivotReportGroupSummaryWithNoSubTotals:
					LoadAnalysisPivotReportGroupSummaryNoSubTotals(DataSetAnalysisSummary);
					break;
				case (Int32)ReportEnum.SupplierLocationGuarantees:
					LoadSourceLocationGuaranteeReport(DataSetSourceLocationGuarantee);
					break;
				case (Int32)ReportEnum.SampleComparisonReport:
					LoadSampleComparisonReport(DataSetSampleComparison);
					break;
				case (Int32)ReportEnum.AnalysisPivotSummary:
					LoadAnalysisPivotSummary(DataSetAnalysisSummary);
					break;				
                case (Int32)ReportEnum.ManufacturedProductComplianceReport:
                    LoadManufacturedProductComplianceReport(DataSetMfgProductCompliance);
                    break;
                case (Int32)ReportEnum.SampleActivityReport:

					//4/26/22
					//LoadSampleActivityReport(DataSetSampleActivity);
					if (AIMGlobal.sampleActivityReportOption == 2)
					{
						LoadSampleActivityQualityControlReport(DataSetSampleActivity);
					}
					else
					{
						LoadSampleActivityReport(DataSetSampleActivity);
					}					
                    break;

                case (Int32)ReportEnum.SampleResultsBySupplier:
                    LoadSampleResultsBySupplierReport(DataSetSampleResultsBySupplier);
                    break;

                //5/20/19
                case (Int32)ReportEnum.UserDefinedSamplePivotData:
                    break;

				////5/20/22 - currently not used
				//case (Int32)ReportEnum.SampleResultsComparisonReport:
				//	LoadSampleResultsComparisonReport(DataSetSampleResultsComparison);
				//	break;

				//6/27/22
				case (Int32)ReportEnum.SamplesByOwnerLocation:
					LoadSamplesByOwnerLocReport(DataSetSamplesByOwnerLoc);
					break;

				case (Int32)ReportEnum.SamplesSubmittedReport:
					LoadSampleSubmittedReport(DataSetSampleSubmitted);
					break;
				case (Int32)ReportEnum.SampleAnalysisRequestFormEvonik:
				case (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto:
				case (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth:
					LoadSampleAnalysisRequestForm(DataSetSampleAnalysisRequestForm);
					break;
				case (Int32)ReportEnum.AnalysisCost:                    //Task1150 DB260109
					LoadTestGrid(DataSetTestGrid, fromDate, toDate, locationName);
                    break;

			}
		}

        private static void LoadManufacturedProductComplianceReport(List<ManufacturedProductComplianceReportInfo> reportDataset)
        {                 
            ReportDevExpress.ManufacturedProductComplianceReport report = new ReportDevExpress.ManufacturedProductComplianceReport();
            report.DataSource = reportDataset;
            ReportPrintTool tool = new ReportPrintTool(report);
            AIMGlobal.SplashScreenHide();
            tool.ShowPreviewDialog();
        }

        private static void LoadAnalysisSourceComparisonReport(List<AnalysisReportsInfo> reportDataset)
		{
            ReportDevExpress.AnalysisSourceComparisonReport report = new ReportDevExpress.AnalysisSourceComparisonReport();
            report.DataSource = reportDataset;
            ReportPrintTool tool = new ReportPrintTool(report);
            AIMGlobal.SplashScreenHide();
            tool.ShowPreviewDialog();
            
		}

		private static void LoadAnalysisSummaryReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisSummaryReport report = new ReportDevExpress.AnalysisSummaryReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAnalysisDetailReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisDetailReport report = new ReportDevExpress.AnalysisDetailReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			tool.ShowPreviewDialog();
		}
		private static void LoadAnalysisDateComparisonReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisDateComparisonReport report = new ReportDevExpress.AnalysisDateComparisonReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadGuaranteeComparisonByMeanReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.GuaranteeComparisonByMeanReport report = new ReportDevExpress.GuaranteeComparisonByMeanReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadGuaranteeComparisonBySampleReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.GuaranteeComparisonBySampleReport report = new ReportDevExpress.GuaranteeComparisonBySampleReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadProductSpecificationComparisonByMeanReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.ProductSpecificationComparisonByMeanReport report = new ReportDevExpress.ProductSpecificationComparisonByMeanReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadProductSpecificationComparisonBySampleReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.ProductSpecificationComparisonBySampleReport report = new ReportDevExpress.ProductSpecificationComparisonBySampleReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAimNutrientRefAnalyzerNutrientOphansReport(List<AimNutrientRefAnalyzerNutrientOrphanInfo> reportDataset)
		{
			ReportDevExpress.OrphansAimNutrientRefAnalyzerNutrient report = new ReportDevExpress.OrphansAimNutrientRefAnalyzerNutrient();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAimMaterialRefAnalyzerMaterialOphansReport(List<AimMaterialRefAnalyzerMaterialOrphanInfo> reportDataset)
		{
			ReportDevExpress.OrphansAimMaterialRefAnalyzerMaterial report = new ReportDevExpress.OrphansAimMaterialRefAnalyzerMaterial();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}
		private static void LoadExportBatchDetailReport(List<ExportBatchMaintenanceReportInfo> reportDataset)
		{
			ReportDevExpress.ExportBatchDetail report = new ReportDevExpress.ExportBatchDetail();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}
		private static void LoadAnalysisPivotReport(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisPivotReport report = new ReportDevExpress.AnalysisPivotReport();			
			report.DataSource = reportDataset;			
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAnalysisPivotReportGroupSummary(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisPivotReportGroupSummary report = new ReportDevExpress.AnalysisPivotReportGroupSummary();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAnalysisPivotReportGroupSummaryNoSubTotals(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisPivotReportGroupSummaryNoSubTotals report = new ReportDevExpress.AnalysisPivotReportGroupSummaryNoSubTotals();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		private static void LoadAnalysisPivotSummary(List<AnalysisReportsInfo> reportDataset)
		{
			ReportDevExpress.AnalysisPivotReportSummary report = new ReportDevExpress.AnalysisPivotReportSummary();			
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

        private static void LoadSourceLocationGuaranteeReport(List<AimSourceLocationGuaranteeReportInfo> reportDataset)
		{
            if (AIMGlobal.groupByLoadNumber == true)
            {
                ReportDevExpress.SourceLocationGuaranteesReportGrouped report = new ReportDevExpress.SourceLocationGuaranteesReportGrouped();
                report.DataSource = reportDataset;
                ReportPrintTool tool = new ReportPrintTool(report);
                AIMGlobal.SplashScreenHide();
                tool.ShowPreviewDialog();
            }
            else
            {                                
                ReportDevExpress.SourceLocationGuaranteesReport report = new ReportDevExpress.SourceLocationGuaranteesReport();
                report.DataSource = reportDataset;
                ReportPrintTool tool = new ReportPrintTool(report);
                AIMGlobal.SplashScreenHide();
                tool.ShowPreviewDialog();
            }

            
		}
		private static void LoadSampleComparisonReport(List<SampleComparisonReportInfo> reportDataset)
		{
			ReportDevExpress.SampleComparisonReport report = new ReportDevExpress.SampleComparisonReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

        //12/28/17
        private static void LoadSampleActivityReport(List<SampleActivityReportInfo> reportDataset)
        {	

			ReportDevExpress.SampleActivityReport report = new ReportDevExpress.SampleActivityReport();
            report.DataSource = reportDataset;
            ReportPrintTool tool = new ReportPrintTool(report);
            AIMGlobal.SplashScreenHide();
            tool.ShowPreviewDialog();
        }

		//4/26/22
		private static void LoadSampleActivityQualityControlReport(List<SampleActivityReportInfo> reportDataset)
		{
			ReportDevExpress.SampleActivityQualityControlReport report = new ReportDevExpress.SampleActivityQualityControlReport();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}


		//2/12/18
		private static void LoadSampleResultsBySupplierReport(List<SampleResultsBySupplierInfo> reportDataset)
        {
            ReportDevExpress.SampleResultsBySupplier report = new ReportDevExpress.SampleResultsBySupplier();
            report.DataSource = reportDataset;
            ReportPrintTool tool = new ReportPrintTool(report);
            AIMGlobal.SplashScreenHide();
            tool.ShowPreviewDialog();
        }

		//8/16/23
		private static void LoadSampleSubmittedReport(List<SampleSubmittedSamplesReportInfo> reportDataset)
		{
			ReportDevExpress.SampleSubmittedSamples report = new ReportDevExpress.SampleSubmittedSamples();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

		//9/1/23		
		private static void LoadSampleAnalysisRequestForm(List<SampleAnalysisRequestFormInfo> reportDataset)
		{
			if (reportDataset.Count > 0)
			{
				if (reportDataset[0].DataSourceTypeID == (Int32)DataSourceTypeEnum.Evonik)
				{
					ReportDevExpress.SampleAnalysisRequestFormEvonik report = new ReportDevExpress.SampleAnalysisRequestFormEvonik();
					report.DataSource = reportDataset;
					ReportPrintTool tool = new ReportPrintTool(report);
					AIMGlobal.SplashScreenHide();
					tool.ShowPreviewDialog();
				}
				if (reportDataset[0].DataSourceTypeID == (Int32)DataSourceTypeEnum.Ajinomoto)
				{
					ReportDevExpress.SampleAnalysisRequestFormAjinomoto report = new ReportDevExpress.SampleAnalysisRequestFormAjinomoto();
					report.DataSource = reportDataset;
					ReportPrintTool tool = new ReportPrintTool(report);
					AIMGlobal.SplashScreenHide();
					tool.ShowPreviewDialog();
				}
				if (reportDataset[0].DataSourceTypeID == (Int32)DataSourceTypeEnum.UnitedAnimalHealth)
				{
					ReportDevExpress.SampleAnalysisRequestFormUnitedAnimalHealth report = new ReportDevExpress.SampleAnalysisRequestFormUnitedAnimalHealth();
					report.DataSource = reportDataset;
					ReportPrintTool tool = new ReportPrintTool(report);
					AIMGlobal.SplashScreenHide();
					tool.ShowPreviewDialog();
				}
			}			
		}

		////TASK951 6/4/24
		//private static void LoadExportLogReport(List<ExportBatchLogInfo> reportDataset)
		//{

		//	ReportDevExpress.SampleActivityReport report = new ReportDevExpress.SampleActivityReport();
		//	report.DataSource = reportDataset;
		//	ReportPrintTool tool = new ReportPrintTool(report);
		//	AIMGlobal.SplashScreenHide();
		//	tool.ShowPreviewDialog();
		//}

		////5/20/22 - currently not used
		//private static void LoadSampleResultsComparisonReport(List<SampleResultsComparisonReportInfo> reportDataset)
		//{
		//	ReportDevExpress.SampleResultsComparisonReport report = new ReportDevExpress.SampleResultsComparisonReport();
		//	report.DataSource = reportDataset;
		//	ReportPrintTool tool = new ReportPrintTool(report);
		//	AIMGlobal.SplashScreenHide();
		//	tool.ShowPreviewDialog();
		//}

		//private static void LoadTrendReport(List<ChartInfo> reportDataset)
		//{				

		//	ReportDevExpress.Trend report = new ReportDevExpress.Trend();
		//	report.DataSource = reportDataset;
		//	ReportPrintTool tool = new ReportPrintTool(report);
		//	AIMGlobal.SplashScreenHide();
		//	tool.ShowPreviewDialog();
		//}


		//6/27/22
		private static void LoadSamplesByOwnerLocReport(List<SampleHeaderInfo> reportDataset)
		{
			ReportDevExpress.SamplesByOwnerLocation report = new ReportDevExpress.SamplesByOwnerLocation();
			report.DataSource = reportDataset;
			ReportPrintTool tool = new ReportPrintTool(report);
			AIMGlobal.SplashScreenHide();
			tool.ShowPreviewDialog();
		}

        private static void LoadTestGrid(List<TestGridInfo> reportDataset, DateTime fromDate, DateTime toDate, String locationName)					//Task1150 DB260113
        {
            ReportDevExpress.CostAnalysisReport report = new ReportDevExpress.CostAnalysisReport();
			report.ToDate = toDate;
			report.FromDate = fromDate;
			report.LocationName = locationName;
            report.DataSource = reportDataset;
            ReportPrintTool tool = new ReportPrintTool(report);
            AIMGlobal.SplashScreenHide();
            tool.ShowPreviewDialog();
        }
    }
}
