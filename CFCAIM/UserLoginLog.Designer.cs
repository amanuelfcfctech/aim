namespace CFCAIM
{
    partial class UserLoginLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLoginLog));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnResize = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grdUsers = new DevExpress.XtraGrid.GridControl();
            this.grdUsersView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDatabaseConnection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnRemove = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnRemove)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(-1, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(791, 51);
            this.panel4.TabIndex = 154;
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(753, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 150;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(262, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 23);
            this.label7.TabIndex = 149;
            this.label7.Text = "Users Log";
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 4);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(122, 40);
            this.pnlTopBtns.TabIndex = 29;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(59, 1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.UserLoginLog_Load);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 26;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdUsers
            // 
            this.grdUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdUsers.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdUsers.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdUsers.Location = new System.Drawing.Point(-1, 55);
            this.grdUsers.MainView = this.grdUsersView;
            this.grdUsers.Margin = new System.Windows.Forms.Padding(2);
            this.grdUsers.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnRemove});
            this.grdUsers.Size = new System.Drawing.Size(787, 384);
            this.grdUsers.TabIndex = 155;
            this.grdUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdUsersView});
            // 
            // grdUsersView
            // 
            this.grdUsersView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdUsersView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdUsersView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdUsersView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.DetailTip.Options.UseFont = true;
            this.grdUsersView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.Empty.Options.UseFont = true;
            this.grdUsersView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.EvenRow.Options.UseFont = true;
            this.grdUsersView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdUsersView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdUsersView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FixedLine.Options.UseFont = true;
            this.grdUsersView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdUsersView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdUsersView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdUsersView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.GroupButton.Options.UseFont = true;
            this.grdUsersView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdUsersView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdUsersView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.GroupRow.Options.UseFont = true;
            this.grdUsersView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdUsersView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdUsersView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.HorzLine.Options.UseFont = true;
            this.grdUsersView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.OddRow.Options.UseFont = true;
            this.grdUsersView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.Preview.Options.UseFont = true;
            this.grdUsersView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.Row.Options.UseFont = true;
            this.grdUsersView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdUsersView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdUsersView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdUsersView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.VertLine.Options.UseFont = true;
            this.grdUsersView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdUsersView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdUsersView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdUsersView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdUsersView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdUsersView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdUsersView.AppearancePrint.Row.Options.UseFont = true;
            this.grdUsersView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCFCUserID,
            this.colCFCUserName,
            this.colDatabaseConnection,
            this.colLoginDate});
            this.grdUsersView.GridControl = this.grdUsers;
            this.grdUsersView.Name = "grdUsersView";
            this.grdUsersView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdUsersView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdUsersView.OptionsPrint.EnableAppearanceOddRow = true;
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colCFCUserID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserID.AppearanceCell.Options.UseBackColor = true;
            this.colCFCUserID.AppearanceCell.Options.UseFont = true;
            this.colCFCUserID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserID.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserID.Caption = "User ID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            this.colCFCUserID.OptionsColumn.AllowEdit = false;
            this.colCFCUserID.Width = 107;
            // 
            // colCFCUserName
            // 
            this.colCFCUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserName.AppearanceCell.Options.UseFont = true;
            this.colCFCUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserName.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserName.Caption = "User Name";
            this.colCFCUserName.FieldName = "CFCUserName";
            this.colCFCUserName.Name = "colCFCUserName";
            this.colCFCUserName.OptionsColumn.AllowEdit = false;
            this.colCFCUserName.Visible = true;
            this.colCFCUserName.VisibleIndex = 0;
            this.colCFCUserName.Width = 363;
            // 
            // colDatabaseConnection
            // 
            this.colDatabaseConnection.Caption = "DatabaseConnection";
            this.colDatabaseConnection.FieldName = "DatabaseConnection";
            this.colDatabaseConnection.Name = "colDatabaseConnection";
            this.colDatabaseConnection.Visible = true;
            this.colDatabaseConnection.VisibleIndex = 1;
            this.colDatabaseConnection.Width = 264;
            // 
            // colLoginDate
            // 
            this.colLoginDate.AppearanceCell.Options.UseTextOptions = true;
            this.colLoginDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLoginDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colLoginDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLoginDate.Caption = "Login Date";
            this.colLoginDate.DisplayFormat.FormatString = "g";
            this.colLoginDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLoginDate.FieldName = "LoginDate";
            this.colLoginDate.Name = "colLoginDate";
            this.colLoginDate.OptionsColumn.AllowEdit = false;
            this.colLoginDate.Visible = true;
            this.colLoginDate.VisibleIndex = 2;
            this.colLoginDate.Width = 270;
            // 
            // repbtnRemove
            // 
            this.repbtnRemove.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnRemove.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnRemove.Name = "repbtnRemove";
            this.repbtnRemove.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // UserLoginLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grdUsers);
            this.Controls.Add(this.panel4);
            this.Name = "UserLoginLog";
            this.Text = "UserLoginLog";
            this.Load += new System.EventHandler(this.UserLoginLog_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnRemove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private DevExpress.XtraGrid.GridControl grdUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView grdUsersView;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colLoginDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnRemove;
        private DevExpress.XtraGrid.Columns.GridColumn colDatabaseConnection;
    }
}