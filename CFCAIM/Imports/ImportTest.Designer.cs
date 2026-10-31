namespace CFCAIM.Imports
{
	partial class ImportTest
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.sampleInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.colSampleID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.colSampleCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.colCreatedStamp = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.colAnalysisDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.colTicketNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.sampleResultInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnExpand = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sampleInfoBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sampleResultInfoBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.DataSource = this.sampleInfoBindingSource;
			this.gridControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridControl1.Location = new System.Drawing.Point(12, 12);
			this.gridControl1.MainView = this.advBandedGridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
			this.gridControl1.Size = new System.Drawing.Size(967, 490);
			this.gridControl1.TabIndex = 46;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
			this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
			// 
			// sampleInfoBindingSource
			// 
			this.sampleInfoBindingSource.DataSource = typeof(CFCAIMCore.Entity.SampleInfo);
			// 
			// advBandedGridView1
			// 
			this.advBandedGridView1.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.advBandedGridView1.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.advBandedGridView1.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.advBandedGridView1.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.DetailTip.Options.UseFont = true;
			this.advBandedGridView1.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.Empty.Options.UseFont = true;
			this.advBandedGridView1.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FilterCloseButton.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FilterPanel.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FixedLine.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView1.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.GroupButton.Options.UseFont = true;
			this.advBandedGridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.GroupFooter.Options.UseFont = true;
			this.advBandedGridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.GroupPanel.Options.UseFont = true;
			this.advBandedGridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.GroupRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.HideSelectionRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.HorzLine.Options.UseFont = true;
			this.advBandedGridView1.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.Preview.Options.UseFont = true;
			this.advBandedGridView1.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.Row.Options.UseFont = true;
			this.advBandedGridView1.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.RowSeparator.Options.UseFont = true;
			this.advBandedGridView1.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.TopNewRow.Options.UseFont = true;
			this.advBandedGridView1.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.VertLine.Options.UseFont = true;
			this.advBandedGridView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.Appearance.ViewCaption.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.advBandedGridView1.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.advBandedGridView1.AppearancePrint.EvenRow.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.GroupRow.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.Lines.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.advBandedGridView1.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.advBandedGridView1.AppearancePrint.OddRow.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.Preview.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.advBandedGridView1.AppearancePrint.Row.Options.UseFont = true;
			this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
			this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSampleID,
            this.colSampleCode,
            this.colCreatedStamp,
            this.colTicketNumber,
            this.colAnalysisDate});
			this.advBandedGridView1.GridControl = this.gridControl1;
			this.advBandedGridView1.Name = "advBandedGridView1";
			this.advBandedGridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
			this.advBandedGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.advBandedGridView1.OptionsPrint.EnableAppearanceEvenRow = true;
			this.advBandedGridView1.OptionsPrint.EnableAppearanceOddRow = true;
			this.advBandedGridView1.OptionsPrint.PrintBandHeader = false;
			this.advBandedGridView1.OptionsView.ShowBands = false;
			this.advBandedGridView1.MasterRowEmpty += new DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventHandler(this.advBandedGridView1_MasterRowEmpty);
			this.advBandedGridView1.MasterRowGetChildList += new DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventHandler(this.advBandedGridView1_MasterRowGetChildList);
			this.advBandedGridView1.MasterRowGetRelationName += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.advBandedGridView1_MasterRowGetRelationName);
			this.advBandedGridView1.MasterRowGetRelationCount += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventHandler(this.advBandedGridView1_MasterRowGetRelationCount);
			// 
			// gridBand1
			// 
			this.gridBand1.Columns.Add(this.colSampleID);
			this.gridBand1.Columns.Add(this.colSampleCode);
			this.gridBand1.Name = "gridBand1";
			this.gridBand1.VisibleIndex = 0;
			this.gridBand1.Width = 386;
			// 
			// colSampleID
			// 
			this.colSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colSampleID.AppearanceCell.Options.UseFont = true;
			this.colSampleID.AppearanceCell.Options.UseTextOptions = true;
			this.colSampleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSampleID.AppearanceHeader.Options.UseTextOptions = true;
			this.colSampleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSampleID.FieldName = "SampleID";
			this.colSampleID.Name = "colSampleID";
			this.colSampleID.Visible = true;
			this.colSampleID.Width = 85;
			// 
			// colSampleCode
			// 
			this.colSampleCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.colSampleCode.AppearanceCell.Options.UseFont = true;
			this.colSampleCode.FieldName = "SampleCode";
			this.colSampleCode.Name = "colSampleCode";
			this.colSampleCode.Visible = true;
			this.colSampleCode.Width = 301;
			// 
			// gridBand2
			// 
			this.gridBand2.Caption = "Aim Required Fields";
			this.gridBand2.Columns.Add(this.colCreatedStamp);
			this.gridBand2.Columns.Add(this.colAnalysisDate);
			this.gridBand2.Columns.Add(this.colTicketNumber);
			this.gridBand2.Name = "gridBand2";
			this.gridBand2.VisibleIndex = 1;
			this.gridBand2.Width = 386;
			// 
			// colCreatedStamp
			// 
			this.colCreatedStamp.FieldName = "CreatedStamp";
			this.colCreatedStamp.Name = "colCreatedStamp";
			this.colCreatedStamp.Visible = true;
			this.colCreatedStamp.Width = 280;
			// 
			// colAnalysisDate
			// 
			this.colAnalysisDate.FieldName = "AnalysisDate";
			this.colAnalysisDate.Name = "colAnalysisDate";
			this.colAnalysisDate.RowIndex = 1;
			this.colAnalysisDate.Visible = true;
			this.colAnalysisDate.Width = 280;
			// 
			// colTicketNumber
			// 
			this.colTicketNumber.FieldName = "TicketNumber";
			this.colTicketNumber.Name = "colTicketNumber";
			this.colTicketNumber.RowIndex = 2;
			this.colTicketNumber.Visible = true;
			this.colTicketNumber.Width = 386;
			// 
			// gridBand3
			// 
			this.gridBand3.Caption = "gridBand3";
			this.gridBand3.Name = "gridBand3";
			this.gridBand3.VisibleIndex = 2;
			this.gridBand3.Width = 82;
			// 
			// repositoryItemLookUpEdit1
			// 
			this.repositoryItemLookUpEdit1.AutoHeight = false;
			this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
			// 
			// sampleResultInfoBindingSource
			// 
			this.sampleResultInfoBindingSource.DataSource = typeof(CFCAIMCore.Entity.SampleResultInfo);
			// 
			// btnExpand
			// 
			this.btnExpand.Location = new System.Drawing.Point(864, 17);
			this.btnExpand.Name = "btnExpand";
			this.btnExpand.Size = new System.Drawing.Size(100, 28);
			this.btnExpand.TabIndex = 47;
			this.btnExpand.Text = "Expand All";
			this.btnExpand.UseVisualStyleBackColor = true;
			this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
			// 
			// ImportTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(991, 527);
			this.Controls.Add(this.btnExpand);
			this.Controls.Add(this.gridControl1);
			this.Name = "ImportTest";
			this.Text = "ImportTest";
			this.Load += new System.EventHandler(this.ImportTest_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sampleInfoBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sampleResultInfoBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
		private System.Windows.Forms.BindingSource sampleInfoBindingSource;
		private System.Windows.Forms.BindingSource sampleResultInfoBindingSource;
		private System.Windows.Forms.Button btnExpand;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSampleID;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSampleCode;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCreatedStamp;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTicketNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAnalysisDate;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;

	}
}