using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace CFCAIM
{
	public partial class Test5 : Form
	{
		public Test5()
		{
			InitializeComponent();
		}

		private void Test5_Load(object sender, System.EventArgs e)
		{
			DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.ConvertBindingsToExpressions = DevExpress.XtraReports.UI.PromptBoolean.False;

			//ReportDevExpress.SourceReport report = new ReportDevExpress.SourceReport();
			//ReportDesignTool designTool = new ReportDesignTool(report);
			//IDesignForm designForm = designTool.DesignRibbonForm;
			////this isn't doing what i thought it would do
			//((Form)designForm).MaximizeBox = false;
			//((Form)designForm).MinimizeBox = false;
			//designTool.ShowDesignerDialog();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			AIMGlobal.SplashScreenShow();
			ReportDevExpress.SampleActivityReport report = new ReportDevExpress.SampleActivityReport();
			
			ReportDesignTool designTool = new ReportDesignTool(report);

			List<SampleActivityReportInfo> sampleActivity = new List<SampleActivityReportInfo>();
			SampleActivityReportInfo activity = new SampleActivityReportInfo();
			AIMGlobal.sampleActivityReportOption = 1;
			
			activity.FromDateString = "2022-05-10";
			activity.ToDateString = "2022-06-27";
			activity.MaterialTypeID = 1;


			sampleActivity = AIMManager.GetSampleActivity(ref activity);

			report.DataSource = sampleActivity;

			IDesignForm designForm = designTool.DesignRibbonForm;

			

			//this isn't doing what i thought it would do
			//((Form)designForm).MaximizeBox = false;
			//((Form)designForm).MinimizeBox = false;

			designTool.ShowDesignerDialog();
			////designTool.ShowRibbonDesignerDialog();

			AIMGlobal.SplashScreenHide();
		}

		
	}
}
