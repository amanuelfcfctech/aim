using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CFCAIMCore.Entity;

namespace CFCAIM.ReportDevExpress
{
	public partial class ExportBatchDetail : DevExpress.XtraReports.UI.XtraReport
	{
		public ExportBatchDetail()
		{
			InitializeComponent();
		}

		private void ExportBatchDetail_BeforePrint(object sender, CancelEventArgs e)
		{

			//-------------------------------------------
			//4/20/21
			Detail.SortFields.Add(new GroupField("CFCIngredientCode", XRColumnSortOrder.Ascending));			
			if (AIMGlobal.sortNutrientsBy == "Code")
			{
				
				Detail.SortFields.Add(new GroupField("SortNutrientCode", XRColumnSortOrder.Ascending));
			}
			else
			{
				Detail.SortFields.Add(new GroupField("CFCNutrientName", XRColumnSortOrder.Ascending));
			}

			//-------------------------------------------
			//10/18/23
			if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay || CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
			{
				xrlblLocation.Visible = true;
			}
			else
			{
				xrlblLocation.Visible = false;
			}

			//-------------------------------------------
			//2/7/24
			if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.Base || CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
			{
				xrLabel_HeaderFRM.Visible = false;
				xrLabel_HeaderPlant.Visible = false;
				xrLabel_CFCPlant.Visible = false;
			}
			else
			{
				xrLabel_HeaderFRM.Visible = true;
				xrLabel_HeaderPlant.Visible = true;
				xrLabel_CFCPlant.Visible = true;
			}


			//// Create a new rule and add it to a report.
			//FormattingRule rule = new FormattingRule();
			//this.FormattingRuleSheet.Add(rule);

			//// Specify the rule's properties.
			//rule.DataSource = this.DataSource;
			//rule.DataMember = this.DataMember;
			//rule.Condition = "[Override] > 0";
			//rule.Formatting.BackColor = Color.WhiteSmoke;
			//rule.Formatting.ForeColor = Color.IndianRed;

			//rule.Formatting.Font = new Font("Arial", 10, FontStyle.Bold);

			//// Apply this rule to the detail band.
			//this.Detail.FormattingRules.Add(rule);

		}
				
	}
}
