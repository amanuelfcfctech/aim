namespace CFCAIM
{
    partial class LanguageTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageTest));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.grdTest = new DevExpress.XtraGrid.GridControl();
            this.grdTestView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMathTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMathTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestView)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // grdTest
            // 
            resources.ApplyResources(this.grdTest, "grdTest");
            this.grdTest.EmbeddedNavigator.AccessibleDescription = resources.GetString("grdTest.EmbeddedNavigator.AccessibleDescription");
            this.grdTest.EmbeddedNavigator.AccessibleName = resources.GetString("grdTest.EmbeddedNavigator.AccessibleName");
            this.grdTest.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("grdTest.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.grdTest.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grdTest.EmbeddedNavigator.Anchor")));
            this.grdTest.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grdTest.EmbeddedNavigator.BackgroundImage")));
            this.grdTest.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("grdTest.EmbeddedNavigator.BackgroundImageLayout")));
            this.grdTest.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grdTest.EmbeddedNavigator.ImeMode")));
            this.grdTest.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("grdTest.EmbeddedNavigator.MaximumSize")));
            this.grdTest.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("grdTest.EmbeddedNavigator.TextLocation")));
            this.grdTest.EmbeddedNavigator.ToolTip = resources.GetString("grdTest.EmbeddedNavigator.ToolTip");
            this.grdTest.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("grdTest.EmbeddedNavigator.ToolTipIconType")));
            this.grdTest.EmbeddedNavigator.ToolTipTitle = resources.GetString("grdTest.EmbeddedNavigator.ToolTipTitle");
            this.grdTest.MainView = this.grdTestView;
            this.grdTest.Name = "grdTest";
            this.grdTest.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdTestView});
            // 
            // grdTestView
            // 
            resources.ApplyResources(this.grdTestView, "grdTestView");
            this.grdTestView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMathTypeID,
            this.colMathTypeName,
            this.colIsActive});
            this.grdTestView.GridControl = this.grdTest;
            this.grdTestView.Name = "grdTestView";
            // 
            // colMathTypeID
            // 
            resources.ApplyResources(this.colMathTypeID, "colMathTypeID");
            this.colMathTypeID.FieldName = "MathTypeID";
            this.colMathTypeID.Name = "colMathTypeID";
            // 
            // colMathTypeName
            // 
            resources.ApplyResources(this.colMathTypeName, "colMathTypeName");
            this.colMathTypeName.FieldName = "MathTypeName";
            this.colMathTypeName.Name = "colMathTypeName";
            // 
            // colIsActive
            // 
            resources.ApplyResources(this.colIsActive, "colIsActive");
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            // 
            // LanguageTest
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdTest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "LanguageTest";
            this.Load += new System.EventHandler(this.LanguageTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl grdTest;
        private DevExpress.XtraGrid.Views.Grid.GridView grdTestView;
        private DevExpress.XtraGrid.Columns.GridColumn colMathTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colMathTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
    }
}