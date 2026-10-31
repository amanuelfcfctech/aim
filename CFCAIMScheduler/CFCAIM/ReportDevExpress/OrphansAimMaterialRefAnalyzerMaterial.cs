using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
	public partial class OrphansAimMaterialRefAnalyzerMaterial : DevExpress.XtraReports.UI.XtraReport
	{
		public OrphansAimMaterialRefAnalyzerMaterial()
		{
			InitializeComponent();

			if (AIMGlobal.orphanCrossRefReport == 1)
			{
				xrLblPageHeading.Text = "Aim/Analyzer Material Cross Reference Orphans - Ingredients";
				xrLabelDataSource.Visible = true;
			}
			else if (AIMGlobal.orphanCrossRefReport == 2)
			{
				xrLblPageHeading.Text = "Aim/Analyzer Material Cross Reference Orphans - Products";
				xrLabelDataSource.Visible = true;
			}
			else if (AIMGlobal.orphanCrossRefReport == 3)
			{
				xrLblPageHeading.Text = "Aim/Formulation Ingredient Cross Reference Orphans";
				xrLabelDataSource.Visible = false;
			}
			else if (AIMGlobal.orphanCrossRefReport == 4)
			{
				xrLblPageHeading.Text = "Aim/Formulation Product Cross Reference Orphans";
				xrLabelDataSource.Visible = false;
			}
		}
	}
}