namespace CFCAIM.Maintenance
{
	partial class FormulationCrossReference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormulationCrossReference));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageIngredient = new System.Windows.Forms.TabPage();
            this.grdIngredientRef = new DevExpress.XtraGrid.GridControl();
            this.grdIngredientRefView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAimMaterialID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCIngredientID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCIngredientRefAIMMaterialID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCIngredientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCFCIngredientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colAIMMaterialCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colAIMMaterialName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteIngredient = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnPreviewIngredient = new System.Windows.Forms.Button();
            this.btnAddIngredient = new System.Windows.Forms.Button();
            this.gbCFC = new System.Windows.Forms.GroupBox();
            this.gbFrmFilterIngr = new System.Windows.Forms.GroupBox();
            this.rbFrmFilterMissingXrefIngr = new System.Windows.Forms.RadioButton();
            this.rbFrmFilterWithXrefIngr = new System.Windows.Forms.RadioButton();
            this.lueCFCNameIngredient = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCFCProductCode = new System.Windows.Forms.Label();
            this.lueCFCCodeIngredient = new DevExpress.XtraEditors.LookUpEdit();
            this.gbAnalyzer = new System.Windows.Forms.GroupBox();
            this.gbAimFilterIngr = new System.Windows.Forms.GroupBox();
            this.rbAimFilterMissingXrefIngr = new System.Windows.Forms.RadioButton();
            this.rbAimFilterWithXrefIngr = new System.Windows.Forms.RadioButton();
            this.lueAIMNameMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAnalyzerMaterialCode = new System.Windows.Forms.Label();
            this.lueAIMCodeMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.pageNutrient = new System.Windows.Forms.TabPage();
            this.grdNutrientRef = new DevExpress.XtraGrid.GridControl();
            this.grdNutrientRefView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAIMNutrientRefAnalyzerNutrientID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCCodeNutrient = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCFCNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCNameNutrient = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCFCUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMCodeNutrient = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colAIMNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMNameNutrient = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colUnitType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteNutrient = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPreviewNutrient = new System.Windows.Forms.Button();
            this.btnAddNutrient = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbFrmFilterNut = new System.Windows.Forms.GroupBox();
            this.rbFrmFilterMissingXrefNut = new System.Windows.Forms.RadioButton();
            this.rbFrmFilterWithXrefNut = new System.Windows.Forms.RadioButton();
            this.lueFrmNameNutrient = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFRMNutrientName = new System.Windows.Forms.Label();
            this.lblFRMProductCode = new System.Windows.Forms.Label();
            this.lueFrmCodeNutrient = new DevExpress.XtraEditors.LookUpEdit();
            this.gbAIM = new System.Windows.Forms.GroupBox();
            this.gbAimFilterNut = new System.Windows.Forms.GroupBox();
            this.rbAimFilterMissingXrefNut = new System.Windows.Forms.RadioButton();
            this.rbAimFilterWithXrefNut = new System.Windows.Forms.RadioButton();
            this.lueAIMNameNutrient = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lueAIMCodeNutrient = new DevExpress.XtraEditors.LookUpEdit();
            this.pageOwner = new System.Windows.Forms.TabPage();
            this.grdReferencePlant = new DevExpress.XtraGrid.GridControl();
            this.grdReferencePlantView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPlantCrossReferenceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCrossCFCPlantCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCPlantCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCrossCFCPlantName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCPlantName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCrossAIMLocationCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMLocationCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCrossAIMLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMLocationName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCrossDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteOwner = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPreviewOwner = new System.Windows.Forms.Button();
            this.btnAddOwner = new System.Windows.Forms.Button();
            this.grbAimOwner = new System.Windows.Forms.GroupBox();
            this.lueOwnerName = new DevExpress.XtraEditors.LookUpEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lueOwnerCode = new DevExpress.XtraEditors.LookUpEdit();
            this.gboCFCPlant = new System.Windows.Forms.GroupBox();
            this.gbFrmFilterLoc = new System.Windows.Forms.GroupBox();
            this.rbFrmFilterMissingXrefLoc = new System.Windows.Forms.RadioButton();
            this.rbFrmFilterWithXrefLoc = new System.Windows.Forms.RadioButton();
            this.lueCFCPlantName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCFCPlantName = new System.Windows.Forms.Label();
            this.lueCFCPlantCode = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCFCPlantCode = new System.Windows.Forms.Label();
            this.gboAIMPlant = new System.Windows.Forms.GroupBox();
            this.gbAimFilterLoc = new System.Windows.Forms.GroupBox();
            this.rbAimFilterMissingXrefLoc = new System.Windows.Forms.RadioButton();
            this.rbAimFilterWithXrefLoc = new System.Windows.Forms.RadioButton();
            this.lueAIMLocationName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblAIMPlantName = new System.Windows.Forms.Label();
            this.lblAIMPlantCode = new System.Windows.Forms.Label();
            this.lueAIMLocationCode = new DevExpress.XtraEditors.LookUpEdit();
            this.pageProduct = new System.Windows.Forms.TabPage();
            this.grdProduct = new DevExpress.XtraGrid.GridControl();
            this.grdProductView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductCrossReferenceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCProductCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCFCProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCProductName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMMaterialCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMMaterialName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteProduct = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCFCPlantCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMOwnerLocationCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCPlantID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AIMOwnerLocationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPreviewProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.gboCFCProduct = new System.Windows.Forms.GroupBox();
            this.gbFrmFilterProd = new System.Windows.Forms.GroupBox();
            this.rbFrmFilterMissingXrefProd = new System.Windows.Forms.RadioButton();
            this.rbFrmFilterWithXrefProd = new System.Windows.Forms.RadioButton();
            this.lueProductCode = new DevExpress.XtraEditors.LookUpEdit();
            this.lueProductName = new DevExpress.XtraEditors.LookUpEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.luePlant_Product = new DevExpress.XtraEditors.LookUpEdit();
            this.gboAnalyzerMaterial = new System.Windows.Forms.GroupBox();
            this.gbAimFilterProd = new System.Windows.Forms.GroupBox();
            this.rbAimFilterMissingXrefProd = new System.Windows.Forms.RadioButton();
            this.rbAimFilterWithXrefProd = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lueOwnerLocation_Material = new DevExpress.XtraEditors.LookUpEdit();
            this.lueMaterialName = new DevExpress.XtraEditors.LookUpEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.lueOwner_Material = new DevExpress.XtraEditors.LookUpEdit();
            this.lueMaterialCode = new DevExpress.XtraEditors.LookUpEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.tpProductsMissingXRef = new System.Windows.Forms.TabPage();
            this.btnUpdateXref = new System.Windows.Forms.Button();
            this.grdProdMissing = new DevExpress.XtraGrid.GridControl();
            this.grdProdMissingView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repHyperStatus = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.btnResize = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlTopBtns.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pageIngredient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdIngredientRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdIngredientRefView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteIngredient)).BeginInit();
            this.gbCFC.SuspendLayout();
            this.gbFrmFilterIngr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCNameIngredient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCCodeIngredient.Properties)).BeginInit();
            this.gbAnalyzer.SuspendLayout();
            this.gbAimFilterIngr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMNameMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMCodeMaterial.Properties)).BeginInit();
            this.pageNutrient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNutrientRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNutrientRefView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCCodeNutrient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCNameNutrient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMCodeNutrient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMNameNutrient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteNutrient)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbFrmFilterNut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFrmNameNutrient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFrmCodeNutrient.Properties)).BeginInit();
            this.gbAIM.SuspendLayout();
            this.gbAimFilterNut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMNameNutrient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMCodeNutrient.Properties)).BeginInit();
            this.pageOwner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReferencePlant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReferencePlantView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMLocationCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMLocationName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteOwner)).BeginInit();
            this.grbAimOwner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerCode.Properties)).BeginInit();
            this.gboCFCPlant.SuspendLayout();
            this.gbFrmFilterLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCPlantName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCPlantCode.Properties)).BeginInit();
            this.gboAIMPlant.SuspendLayout();
            this.gbAimFilterLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMLocationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMLocationCode.Properties)).BeginInit();
            this.pageProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCProductCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCProductName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMMaterialCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMMaterialName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteProduct)).BeginInit();
            this.gboCFCProduct.SuspendLayout();
            this.gbFrmFilterProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProductCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePlant_Product.Properties)).BeginInit();
            this.gboAnalyzerMaterial.SuspendLayout();
            this.gbAimFilterProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerLocation_Material.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwner_Material.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialCode.Properties)).BeginInit();
            this.tpProductsMissingXRef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdMissing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdMissingView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperStatus)).BeginInit();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 5);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(115, 37);
            this.pnlTopBtns.TabIndex = 37;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(58, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.FormulationCrossReference_Load);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 31;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.pageIngredient);
            this.tabControl1.Controls.Add(this.pageNutrient);
            this.tabControl1.Controls.Add(this.pageOwner);
            this.tabControl1.Controls.Add(this.pageProduct);
            this.tabControl1.Controls.Add(this.tpProductsMissingXRef);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(9, 65);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1081, 508);
            this.tabControl1.TabIndex = 38;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // pageIngredient
            // 
            this.pageIngredient.BackColor = System.Drawing.SystemColors.Control;
            this.pageIngredient.Controls.Add(this.grdIngredientRef);
            this.pageIngredient.Controls.Add(this.btnPreviewIngredient);
            this.pageIngredient.Controls.Add(this.btnAddIngredient);
            this.pageIngredient.Controls.Add(this.gbCFC);
            this.pageIngredient.Controls.Add(this.gbAnalyzer);
            this.pageIngredient.Location = new System.Drawing.Point(4, 23);
            this.pageIngredient.Margin = new System.Windows.Forms.Padding(2);
            this.pageIngredient.Name = "pageIngredient";
            this.pageIngredient.Padding = new System.Windows.Forms.Padding(2);
            this.pageIngredient.Size = new System.Drawing.Size(1073, 481);
            this.pageIngredient.TabIndex = 0;
            this.pageIngredient.Text = "Ingredient";
            // 
            // grdIngredientRef
            // 
            this.grdIngredientRef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdIngredientRef.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdIngredientRef.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdIngredientRef.Location = new System.Drawing.Point(7, 88);
            this.grdIngredientRef.MainView = this.grdIngredientRefView;
            this.grdIngredientRef.Margin = new System.Windows.Forms.Padding(2);
            this.grdIngredientRef.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdIngredientRef.Name = "grdIngredientRef";
            this.grdIngredientRef.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCCode,
            this.replueAIMCode,
            this.repbtnDeleteIngredient,
            this.replueCFCName,
            this.replueAIMName});
            this.grdIngredientRef.Size = new System.Drawing.Size(1062, 389);
            this.grdIngredientRef.TabIndex = 69;
            this.grdIngredientRef.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdIngredientRefView});
            // 
            // grdIngredientRefView
            // 
            this.grdIngredientRefView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.DetailTip.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.Empty.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.EvenRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FixedLine.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.GroupButton.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.GroupRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.HorzLine.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.OddRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.Preview.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.Row.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.VertLine.Options.UseFont = true;
            this.grdIngredientRefView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdIngredientRefView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdIngredientRefView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdIngredientRefView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdIngredientRefView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdIngredientRefView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIngredientRefView.AppearancePrint.Row.Options.UseFont = true;
            this.grdIngredientRefView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAimMaterialID,
            this.colCFCIngredientID,
            this.colCFCIngredientRefAIMMaterialID,
            this.colCFCIngredientCode,
            this.colCFCIngredientName,
            this.colAIMMaterialCode,
            this.colAIMMaterialName,
            this.colUserName,
            this.colUpdateStamp,
            this.colDelete});
            this.grdIngredientRefView.GridControl = this.grdIngredientRef;
            this.grdIngredientRefView.Name = "grdIngredientRefView";
            this.grdIngredientRefView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdIngredientRefView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdIngredientRefView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdIngredientRefView.OptionsView.EnableAppearanceEvenRow = true;
            this.grdIngredientRefView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdIngredientRefView_ValidateRow);
            // 
            // colAimMaterialID
            // 
            this.colAimMaterialID.Caption = "AimMaterialID";
            this.colAimMaterialID.FieldName = "AIMMaterialID";
            this.colAimMaterialID.Name = "colAimMaterialID";
            // 
            // colCFCIngredientID
            // 
            this.colCFCIngredientID.Caption = "CFC Ingredient ID";
            this.colCFCIngredientID.FieldName = "CFCIngredientID";
            this.colCFCIngredientID.Name = "colCFCIngredientID";
            // 
            // colCFCIngredientRefAIMMaterialID
            // 
            this.colCFCIngredientRefAIMMaterialID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientRefAIMMaterialID.AppearanceCell.Options.UseFont = true;
            this.colCFCIngredientRefAIMMaterialID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientRefAIMMaterialID.AppearanceHeader.Options.UseFont = true;
            this.colCFCIngredientRefAIMMaterialID.Caption = "CFCIngredient Analyzer Material Ref ID";
            this.colCFCIngredientRefAIMMaterialID.FieldName = "CFCIngredientRefAIMMaterialID";
            this.colCFCIngredientRefAIMMaterialID.Name = "colCFCIngredientRefAIMMaterialID";
            this.colCFCIngredientRefAIMMaterialID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colCFCIngredientCode
            // 
            this.colCFCIngredientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientCode.AppearanceCell.Options.UseFont = true;
            this.colCFCIngredientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCIngredientCode.Caption = "FRM Ingredient Code";
            this.colCFCIngredientCode.ColumnEdit = this.replueCFCCode;
            this.colCFCIngredientCode.FieldName = "CFCIngredientID";
            this.colCFCIngredientCode.Name = "colCFCIngredientCode";
            this.colCFCIngredientCode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colCFCIngredientCode.Visible = true;
            this.colCFCIngredientCode.VisibleIndex = 0;
            this.colCFCIngredientCode.Width = 137;
            // 
            // replueCFCCode
            // 
            this.replueCFCCode.AutoHeight = false;
            this.replueCFCCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCCode.Name = "replueCFCCode";
            this.replueCFCCode.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueCFCCode_EditValueChanging);
            // 
            // colCFCIngredientName
            // 
            this.colCFCIngredientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientName.AppearanceCell.Options.UseFont = true;
            this.colCFCIngredientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCIngredientName.AppearanceHeader.Options.UseFont = true;
            this.colCFCIngredientName.Caption = "FRM Ingredient Name";
            this.colCFCIngredientName.ColumnEdit = this.replueCFCName;
            this.colCFCIngredientName.FieldName = "CFCIngredientID";
            this.colCFCIngredientName.Name = "colCFCIngredientName";
            this.colCFCIngredientName.Visible = true;
            this.colCFCIngredientName.VisibleIndex = 1;
            this.colCFCIngredientName.Width = 194;
            // 
            // replueCFCName
            // 
            this.replueCFCName.AutoHeight = false;
            this.replueCFCName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCName.Name = "replueCFCName";
            this.replueCFCName.SortColumnIndex = 1;
            this.replueCFCName.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueCFCName_EditValueChanging);
            // 
            // colAIMMaterialCode
            // 
            this.colAIMMaterialCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialCode.AppearanceCell.Options.UseFont = true;
            this.colAIMMaterialCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMMaterialCode.Caption = "AIM Material Code";
            this.colAIMMaterialCode.ColumnEdit = this.replueAIMCode;
            this.colAIMMaterialCode.FieldName = "AIMMaterialID";
            this.colAIMMaterialCode.Name = "colAIMMaterialCode";
            this.colAIMMaterialCode.Visible = true;
            this.colAIMMaterialCode.VisibleIndex = 2;
            this.colAIMMaterialCode.Width = 170;
            // 
            // replueAIMCode
            // 
            this.replueAIMCode.AutoHeight = false;
            this.replueAIMCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMCode.Name = "replueAIMCode";
            this.replueAIMCode.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueAIMCode_EditValueChanging);
            // 
            // colAIMMaterialName
            // 
            this.colAIMMaterialName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialName.AppearanceCell.Options.UseFont = true;
            this.colAIMMaterialName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialName.AppearanceHeader.Options.UseFont = true;
            this.colAIMMaterialName.Caption = "AIM Material Name";
            this.colAIMMaterialName.ColumnEdit = this.replueAIMName;
            this.colAIMMaterialName.FieldName = "AIMMaterialID";
            this.colAIMMaterialName.Name = "colAIMMaterialName";
            this.colAIMMaterialName.Visible = true;
            this.colAIMMaterialName.VisibleIndex = 3;
            this.colAIMMaterialName.Width = 178;
            // 
            // replueAIMName
            // 
            this.replueAIMName.AutoHeight = false;
            this.replueAIMName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMName.Name = "replueAIMName";
            this.replueAIMName.SortColumnIndex = 1;
            this.replueAIMName.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueAIMName_EditValueChanging);
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 5;
            this.colUserName.Width = 134;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "Update Stamp";
            this.colUpdateStamp.DisplayFormat.FormatString = "g";
            this.colUpdateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 6;
            this.colUpdateStamp.Width = 104;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repbtnDeleteIngredient;
            this.colDelete.FieldName = "colDelete";
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.ShowInCustomizationForm = false;
            this.colDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colDelete.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 4;
            this.colDelete.Width = 71;
            // 
            // repbtnDeleteIngredient
            // 
            this.repbtnDeleteIngredient.AutoHeight = false;
            this.repbtnDeleteIngredient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteIngredient.Name = "repbtnDeleteIngredient";
            this.repbtnDeleteIngredient.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteIngredient.Click += new System.EventHandler(this.repbtnDeleteIngredient_Click);
            // 
            // btnPreviewIngredient
            // 
            this.btnPreviewIngredient.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewIngredient.Location = new System.Drawing.Point(1007, 36);
            this.btnPreviewIngredient.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewIngredient.Name = "btnPreviewIngredient";
            this.btnPreviewIngredient.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewIngredient.TabIndex = 68;
            this.btnPreviewIngredient.UseVisualStyleBackColor = true;
            this.btnPreviewIngredient.Click += new System.EventHandler(this.btnPreviewIngredient_Click);
            // 
            // btnAddIngredient
            // 
            this.btnAddIngredient.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddIngredient.Location = new System.Drawing.Point(949, 36);
            this.btnAddIngredient.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddIngredient.Name = "btnAddIngredient";
            this.btnAddIngredient.Size = new System.Drawing.Size(57, 37);
            this.btnAddIngredient.TabIndex = 67;
            this.btnAddIngredient.UseVisualStyleBackColor = true;
            this.btnAddIngredient.Click += new System.EventHandler(this.btnAddIngredient_Click);
            // 
            // gbCFC
            // 
            this.gbCFC.Controls.Add(this.gbFrmFilterIngr);
            this.gbCFC.Controls.Add(this.lueCFCNameIngredient);
            this.gbCFC.Controls.Add(this.label1);
            this.gbCFC.Controls.Add(this.lblCFCProductCode);
            this.gbCFC.Controls.Add(this.lueCFCCodeIngredient);
            this.gbCFC.Font = new System.Drawing.Font("Arial", 9F);
            this.gbCFC.Location = new System.Drawing.Point(7, 11);
            this.gbCFC.Name = "gbCFC";
            this.gbCFC.Size = new System.Drawing.Size(453, 75);
            this.gbCFC.TabIndex = 34;
            this.gbCFC.TabStop = false;
            this.gbCFC.Text = "FRM Ingredient";
            // 
            // gbFrmFilterIngr
            // 
            this.gbFrmFilterIngr.Controls.Add(this.rbFrmFilterMissingXrefIngr);
            this.gbFrmFilterIngr.Controls.Add(this.rbFrmFilterWithXrefIngr);
            this.gbFrmFilterIngr.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFrmFilterIngr.Location = new System.Drawing.Point(335, 11);
            this.gbFrmFilterIngr.Name = "gbFrmFilterIngr";
            this.gbFrmFilterIngr.Size = new System.Drawing.Size(113, 57);
            this.gbFrmFilterIngr.TabIndex = 32;
            this.gbFrmFilterIngr.TabStop = false;
            this.gbFrmFilterIngr.Text = "Filter dropdown by";
            // 
            // rbFrmFilterMissingXrefIngr
            // 
            this.rbFrmFilterMissingXrefIngr.AutoSize = true;
            this.rbFrmFilterMissingXrefIngr.Location = new System.Drawing.Point(12, 35);
            this.rbFrmFilterMissingXrefIngr.Name = "rbFrmFilterMissingXrefIngr";
            this.rbFrmFilterMissingXrefIngr.Size = new System.Drawing.Size(91, 18);
            this.rbFrmFilterMissingXrefIngr.TabIndex = 1;
            this.rbFrmFilterMissingXrefIngr.Text = "Missing Xrefs";
            this.rbFrmFilterMissingXrefIngr.UseVisualStyleBackColor = true;
            this.rbFrmFilterMissingXrefIngr.Click += new System.EventHandler(this.rbFrmFilterMissingXrefIngr_Click);
            // 
            // rbFrmFilterWithXrefIngr
            // 
            this.rbFrmFilterWithXrefIngr.AutoSize = true;
            this.rbFrmFilterWithXrefIngr.Checked = true;
            this.rbFrmFilterWithXrefIngr.Location = new System.Drawing.Point(12, 16);
            this.rbFrmFilterWithXrefIngr.Name = "rbFrmFilterWithXrefIngr";
            this.rbFrmFilterWithXrefIngr.Size = new System.Drawing.Size(76, 18);
            this.rbFrmFilterWithXrefIngr.TabIndex = 0;
            this.rbFrmFilterWithXrefIngr.TabStop = true;
            this.rbFrmFilterWithXrefIngr.Text = "With Xrefs";
            this.rbFrmFilterWithXrefIngr.UseVisualStyleBackColor = true;
            this.rbFrmFilterWithXrefIngr.Click += new System.EventHandler(this.rbFrmFilterWithXrefIngr_Click);
            // 
            // lueCFCNameIngredient
            // 
            this.lueCFCNameIngredient.Location = new System.Drawing.Point(58, 43);
            this.lueCFCNameIngredient.Name = "lueCFCNameIngredient";
            this.lueCFCNameIngredient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueCFCNameIngredient.Properties.Appearance.Options.UseFont = true;
            this.lueCFCNameIngredient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCFCNameIngredient.Properties.DropDownRows = 15;
            this.lueCFCNameIngredient.Properties.SortColumnIndex = 1;
            this.lueCFCNameIngredient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueCFCNameIngredient.Size = new System.Drawing.Size(273, 22);
            this.lueCFCNameIngredient.TabIndex = 31;
            this.lueCFCNameIngredient.EditValueChanged += new System.EventHandler(this.lueCFCNameIngredient_EditValueChanged);
            this.lueCFCNameIngredient.Click += new System.EventHandler(this.lueCFCNameIngredient_Click);
            this.lueCFCNameIngredient.Enter += new System.EventHandler(this.lueCFCNameIngredient_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Name";
            // 
            // lblCFCProductCode
            // 
            this.lblCFCProductCode.AutoSize = true;
            this.lblCFCProductCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblCFCProductCode.Location = new System.Drawing.Point(6, 22);
            this.lblCFCProductCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCFCProductCode.Name = "lblCFCProductCode";
            this.lblCFCProductCode.Size = new System.Drawing.Size(37, 15);
            this.lblCFCProductCode.TabIndex = 25;
            this.lblCFCProductCode.Text = "Code";
            // 
            // lueCFCCodeIngredient
            // 
            this.lueCFCCodeIngredient.Location = new System.Drawing.Point(58, 19);
            this.lueCFCCodeIngredient.Name = "lueCFCCodeIngredient";
            this.lueCFCCodeIngredient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueCFCCodeIngredient.Properties.Appearance.Options.UseFont = true;
            this.lueCFCCodeIngredient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCFCCodeIngredient.Properties.DropDownRows = 15;
            this.lueCFCCodeIngredient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueCFCCodeIngredient.Size = new System.Drawing.Size(273, 22);
            this.lueCFCCodeIngredient.TabIndex = 28;
            this.lueCFCCodeIngredient.EditValueChanged += new System.EventHandler(this.lueCFCCodeIngredient_EditValueChanged);
            this.lueCFCCodeIngredient.Click += new System.EventHandler(this.lueCFCCodeIngredient_Click);
            this.lueCFCCodeIngredient.Enter += new System.EventHandler(this.lueCFCCodeIngredient_Enter);
            // 
            // gbAnalyzer
            // 
            this.gbAnalyzer.Controls.Add(this.gbAimFilterIngr);
            this.gbAnalyzer.Controls.Add(this.lueAIMNameMaterial);
            this.gbAnalyzer.Controls.Add(this.label2);
            this.gbAnalyzer.Controls.Add(this.lblAnalyzerMaterialCode);
            this.gbAnalyzer.Controls.Add(this.lueAIMCodeMaterial);
            this.gbAnalyzer.Font = new System.Drawing.Font("Arial", 9F);
            this.gbAnalyzer.Location = new System.Drawing.Point(478, 11);
            this.gbAnalyzer.Name = "gbAnalyzer";
            this.gbAnalyzer.Size = new System.Drawing.Size(453, 75);
            this.gbAnalyzer.TabIndex = 35;
            this.gbAnalyzer.TabStop = false;
            this.gbAnalyzer.Text = "AIM Material";
            // 
            // gbAimFilterIngr
            // 
            this.gbAimFilterIngr.Controls.Add(this.rbAimFilterMissingXrefIngr);
            this.gbAimFilterIngr.Controls.Add(this.rbAimFilterWithXrefIngr);
            this.gbAimFilterIngr.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAimFilterIngr.Location = new System.Drawing.Point(335, 11);
            this.gbAimFilterIngr.Name = "gbAimFilterIngr";
            this.gbAimFilterIngr.Size = new System.Drawing.Size(113, 57);
            this.gbAimFilterIngr.TabIndex = 33;
            this.gbAimFilterIngr.TabStop = false;
            this.gbAimFilterIngr.Text = "Filter dropdown by";
            // 
            // rbAimFilterMissingXrefIngr
            // 
            this.rbAimFilterMissingXrefIngr.AutoSize = true;
            this.rbAimFilterMissingXrefIngr.Location = new System.Drawing.Point(12, 35);
            this.rbAimFilterMissingXrefIngr.Name = "rbAimFilterMissingXrefIngr";
            this.rbAimFilterMissingXrefIngr.Size = new System.Drawing.Size(91, 18);
            this.rbAimFilterMissingXrefIngr.TabIndex = 1;
            this.rbAimFilterMissingXrefIngr.Text = "Missing Xrefs";
            this.rbAimFilterMissingXrefIngr.UseVisualStyleBackColor = true;
            this.rbAimFilterMissingXrefIngr.Click += new System.EventHandler(this.rbAimFilterMissingXrefIngr_Click);
            // 
            // rbAimFilterWithXrefIngr
            // 
            this.rbAimFilterWithXrefIngr.AutoSize = true;
            this.rbAimFilterWithXrefIngr.Checked = true;
            this.rbAimFilterWithXrefIngr.Location = new System.Drawing.Point(12, 16);
            this.rbAimFilterWithXrefIngr.Name = "rbAimFilterWithXrefIngr";
            this.rbAimFilterWithXrefIngr.Size = new System.Drawing.Size(76, 18);
            this.rbAimFilterWithXrefIngr.TabIndex = 0;
            this.rbAimFilterWithXrefIngr.TabStop = true;
            this.rbAimFilterWithXrefIngr.Text = "With Xrefs";
            this.rbAimFilterWithXrefIngr.UseVisualStyleBackColor = true;
            this.rbAimFilterWithXrefIngr.Click += new System.EventHandler(this.rbAimFilterWithXrefIngr_Click);
            // 
            // lueAIMNameMaterial
            // 
            this.lueAIMNameMaterial.Location = new System.Drawing.Point(58, 43);
            this.lueAIMNameMaterial.Name = "lueAIMNameMaterial";
            this.lueAIMNameMaterial.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMNameMaterial.Properties.Appearance.Options.UseFont = true;
            this.lueAIMNameMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMNameMaterial.Properties.DropDownRows = 15;
            this.lueAIMNameMaterial.Properties.SortColumnIndex = 2;
            this.lueAIMNameMaterial.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMNameMaterial.Size = new System.Drawing.Size(273, 22);
            this.lueAIMNameMaterial.TabIndex = 31;
            this.lueAIMNameMaterial.EditValueChanged += new System.EventHandler(this.lueAIMNameMaterial_EditValueChanged);
            this.lueAIMNameMaterial.Click += new System.EventHandler(this.lueAIMNameMaterial_Click);
            this.lueAIMNameMaterial.Enter += new System.EventHandler(this.lueAIMNameMaterial_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Name";
            // 
            // lblAnalyzerMaterialCode
            // 
            this.lblAnalyzerMaterialCode.AutoSize = true;
            this.lblAnalyzerMaterialCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblAnalyzerMaterialCode.Location = new System.Drawing.Point(6, 22);
            this.lblAnalyzerMaterialCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAnalyzerMaterialCode.Name = "lblAnalyzerMaterialCode";
            this.lblAnalyzerMaterialCode.Size = new System.Drawing.Size(37, 15);
            this.lblAnalyzerMaterialCode.TabIndex = 27;
            this.lblAnalyzerMaterialCode.Text = "Code";
            // 
            // lueAIMCodeMaterial
            // 
            this.lueAIMCodeMaterial.Location = new System.Drawing.Point(58, 19);
            this.lueAIMCodeMaterial.Name = "lueAIMCodeMaterial";
            this.lueAIMCodeMaterial.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMCodeMaterial.Properties.Appearance.Options.UseFont = true;
            this.lueAIMCodeMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMCodeMaterial.Properties.DropDownRows = 15;
            this.lueAIMCodeMaterial.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMCodeMaterial.Size = new System.Drawing.Size(273, 22);
            this.lueAIMCodeMaterial.TabIndex = 29;
            this.lueAIMCodeMaterial.EditValueChanged += new System.EventHandler(this.lueAIMCodeMaterial_EditValueChanged);
            this.lueAIMCodeMaterial.Click += new System.EventHandler(this.lueAIMCodeMaterial_Click);
            this.lueAIMCodeMaterial.Enter += new System.EventHandler(this.lueAIMCodeMaterial_Enter);
            // 
            // pageNutrient
            // 
            this.pageNutrient.BackColor = System.Drawing.SystemColors.Control;
            this.pageNutrient.Controls.Add(this.grdNutrientRef);
            this.pageNutrient.Controls.Add(this.btnPreviewNutrient);
            this.pageNutrient.Controls.Add(this.btnAddNutrient);
            this.pageNutrient.Controls.Add(this.groupBox1);
            this.pageNutrient.Controls.Add(this.gbAIM);
            this.pageNutrient.Location = new System.Drawing.Point(4, 23);
            this.pageNutrient.Margin = new System.Windows.Forms.Padding(2);
            this.pageNutrient.Name = "pageNutrient";
            this.pageNutrient.Padding = new System.Windows.Forms.Padding(2);
            this.pageNutrient.Size = new System.Drawing.Size(1073, 481);
            this.pageNutrient.TabIndex = 1;
            this.pageNutrient.Text = "Nutrient";
            // 
            // grdNutrientRef
            // 
            this.grdNutrientRef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdNutrientRef.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdNutrientRef.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdNutrientRef.Location = new System.Drawing.Point(7, 88);
            this.grdNutrientRef.MainView = this.grdNutrientRefView;
            this.grdNutrientRef.Margin = new System.Windows.Forms.Padding(2);
            this.grdNutrientRef.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdNutrientRef.Name = "grdNutrientRef";
            this.grdNutrientRef.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCCodeNutrient,
            this.replueAIMCodeNutrient,
            this.repbtnDeleteNutrient,
            this.replueCFCNameNutrient,
            this.replueAIMNameNutrient});
            this.grdNutrientRef.Size = new System.Drawing.Size(1062, 389);
            this.grdNutrientRef.TabIndex = 70;
            this.grdNutrientRef.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdNutrientRefView});
            // 
            // grdNutrientRefView
            // 
            this.grdNutrientRefView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.DetailTip.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.Empty.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.EvenRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FixedLine.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.GroupButton.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.GroupRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.HorzLine.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.OddRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.Preview.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.Row.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.VertLine.Options.UseFont = true;
            this.grdNutrientRefView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNutrientRefView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdNutrientRefView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdNutrientRefView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdNutrientRefView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdNutrientRefView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdNutrientRefView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdNutrientRefView.AppearancePrint.Row.Options.UseFont = true;
            this.grdNutrientRefView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAIMNutrientRefAnalyzerNutrientID,
            this.colCFCNutrientCode,
            this.colCFCNutrientName,
            this.colCFCUnit,
            this.colAIMNutrientCode,
            this.colAIMNutrientName,
            this.colUnitType,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn13,
            this.gridColumn15});
            this.grdNutrientRefView.GridControl = this.grdNutrientRef;
            this.grdNutrientRefView.Name = "grdNutrientRefView";
            this.grdNutrientRefView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdNutrientRefView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdNutrientRefView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdNutrientRefView.OptionsView.EnableAppearanceEvenRow = true;
            this.grdNutrientRefView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdNutrientRefView_ValidateRow);
            // 
            // colAIMNutrientRefAnalyzerNutrientID
            // 
            this.colAIMNutrientRefAnalyzerNutrientID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientRefAnalyzerNutrientID.AppearanceCell.Options.UseFont = true;
            this.colAIMNutrientRefAnalyzerNutrientID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientRefAnalyzerNutrientID.AppearanceHeader.Options.UseFont = true;
            this.colAIMNutrientRefAnalyzerNutrientID.Caption = "FRM Nutrient Analyzer Nutrient Ref ID";
            this.colAIMNutrientRefAnalyzerNutrientID.FieldName = "AIMNutrientRefAnalyzerNutrientID";
            this.colAIMNutrientRefAnalyzerNutrientID.Name = "colAIMNutrientRefAnalyzerNutrientID";
            this.colAIMNutrientRefAnalyzerNutrientID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colCFCNutrientCode
            // 
            this.colCFCNutrientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCNutrientCode.AppearanceCell.Options.UseFont = true;
            this.colCFCNutrientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCNutrientCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCNutrientCode.Caption = "FRM Nutrient Code";
            this.colCFCNutrientCode.ColumnEdit = this.replueCFCCodeNutrient;
            this.colCFCNutrientCode.FieldName = "CFCNutrientID";
            this.colCFCNutrientCode.Name = "colCFCNutrientCode";
            this.colCFCNutrientCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCFCNutrientCode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colCFCNutrientCode.Visible = true;
            this.colCFCNutrientCode.VisibleIndex = 0;
            this.colCFCNutrientCode.Width = 151;
            // 
            // replueCFCCodeNutrient
            // 
            this.replueCFCCodeNutrient.AutoHeight = false;
            this.replueCFCCodeNutrient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCCodeNutrient.Name = "replueCFCCodeNutrient";
            this.replueCFCCodeNutrient.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueCFCCodeNutrient_EditValueChanging);
            // 
            // colCFCNutrientName
            // 
            this.colCFCNutrientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCNutrientName.AppearanceCell.Options.UseFont = true;
            this.colCFCNutrientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCNutrientName.AppearanceHeader.Options.UseFont = true;
            this.colCFCNutrientName.Caption = "FRM Nutrient Name";
            this.colCFCNutrientName.ColumnEdit = this.replueCFCNameNutrient;
            this.colCFCNutrientName.FieldName = "CFCNutrientID";
            this.colCFCNutrientName.Name = "colCFCNutrientName";
            this.colCFCNutrientName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCFCNutrientName.Visible = true;
            this.colCFCNutrientName.VisibleIndex = 1;
            this.colCFCNutrientName.Width = 214;
            // 
            // replueCFCNameNutrient
            // 
            this.replueCFCNameNutrient.AutoHeight = false;
            this.replueCFCNameNutrient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCNameNutrient.Name = "replueCFCNameNutrient";
            this.replueCFCNameNutrient.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueCFCNameNutrient_EditValueChanging);
            // 
            // colCFCUnit
            // 
            this.colCFCUnit.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUnit.AppearanceCell.Options.UseFont = true;
            this.colCFCUnit.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colCFCUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUnit.AppearanceHeader.Options.UseBackColor = true;
            this.colCFCUnit.AppearanceHeader.Options.UseFont = true;
            this.colCFCUnit.Caption = "FRM Units";
            this.colCFCUnit.FieldName = "Unit";
            this.colCFCUnit.Name = "colCFCUnit";
            this.colCFCUnit.OptionsColumn.ReadOnly = true;
            this.colCFCUnit.Visible = true;
            this.colCFCUnit.VisibleIndex = 2;
            this.colCFCUnit.Width = 82;
            // 
            // colAIMNutrientCode
            // 
            this.colAIMNutrientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientCode.AppearanceCell.Options.UseFont = true;
            this.colAIMNutrientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMNutrientCode.Caption = "AIM Nutrient Code";
            this.colAIMNutrientCode.ColumnEdit = this.replueAIMCodeNutrient;
            this.colAIMNutrientCode.FieldName = "AIMNutrientID";
            this.colAIMNutrientCode.Name = "colAIMNutrientCode";
            this.colAIMNutrientCode.Visible = true;
            this.colAIMNutrientCode.VisibleIndex = 3;
            this.colAIMNutrientCode.Width = 187;
            // 
            // replueAIMCodeNutrient
            // 
            this.replueAIMCodeNutrient.AutoHeight = false;
            this.replueAIMCodeNutrient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMCodeNutrient.Name = "replueAIMCodeNutrient";
            this.replueAIMCodeNutrient.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueAIMCodeNutrient_EditValueChanging);
            // 
            // colAIMNutrientName
            // 
            this.colAIMNutrientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientName.AppearanceCell.Options.UseFont = true;
            this.colAIMNutrientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientName.AppearanceHeader.Options.UseFont = true;
            this.colAIMNutrientName.Caption = "AIM Nutrient Name";
            this.colAIMNutrientName.ColumnEdit = this.replueAIMNameNutrient;
            this.colAIMNutrientName.FieldName = "AIMNutrientID";
            this.colAIMNutrientName.Name = "colAIMNutrientName";
            this.colAIMNutrientName.Visible = true;
            this.colAIMNutrientName.VisibleIndex = 4;
            this.colAIMNutrientName.Width = 196;
            // 
            // replueAIMNameNutrient
            // 
            this.replueAIMNameNutrient.AutoHeight = false;
            this.replueAIMNameNutrient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMNameNutrient.Name = "replueAIMNameNutrient";
            this.replueAIMNameNutrient.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.replueAIMNameNutrient_EditValueChanging);
            // 
            // colUnitType
            // 
            this.colUnitType.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUnitType.AppearanceCell.Options.UseFont = true;
            this.colUnitType.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colUnitType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUnitType.AppearanceHeader.Options.UseBackColor = true;
            this.colUnitType.AppearanceHeader.Options.UseFont = true;
            this.colUnitType.Caption = "AIM Units";
            this.colUnitType.FieldName = "UnitType";
            this.colUnitType.Name = "colUnitType";
            this.colUnitType.OptionsColumn.ReadOnly = true;
            this.colUnitType.Visible = true;
            this.colUnitType.VisibleIndex = 5;
            this.colUnitType.Width = 80;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "User Name";
            this.gridColumn1.FieldName = "UserName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 7;
            this.gridColumn1.Width = 151;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "Update Stamp";
            this.gridColumn2.DisplayFormat.FormatString = "g";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "UpdateStamp";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 8;
            this.gridColumn2.Width = 112;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Delete";
            this.gridColumn3.ColumnEdit = this.repbtnDeleteNutrient;
            this.gridColumn3.FieldName = "colDelete";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumn3.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 83;
            // 
            // repbtnDeleteNutrient
            // 
            this.repbtnDeleteNutrient.AutoHeight = false;
            this.repbtnDeleteNutrient.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteNutrient.Name = "repbtnDeleteNutrient";
            this.repbtnDeleteNutrient.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteNutrient.Click += new System.EventHandler(this.repbtnDeleteNutrient_Click);
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Code";
            this.gridColumn13.FieldName = "CFCNutrientCode";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Name";
            this.gridColumn15.FieldName = "CFCNutrientName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            // 
            // btnPreviewNutrient
            // 
            this.btnPreviewNutrient.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewNutrient.Location = new System.Drawing.Point(1007, 36);
            this.btnPreviewNutrient.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewNutrient.Name = "btnPreviewNutrient";
            this.btnPreviewNutrient.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewNutrient.TabIndex = 69;
            this.btnPreviewNutrient.UseVisualStyleBackColor = true;
            this.btnPreviewNutrient.Click += new System.EventHandler(this.btnPreviewNutrient_Click);
            // 
            // btnAddNutrient
            // 
            this.btnAddNutrient.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddNutrient.Location = new System.Drawing.Point(949, 36);
            this.btnAddNutrient.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNutrient.Name = "btnAddNutrient";
            this.btnAddNutrient.Size = new System.Drawing.Size(57, 37);
            this.btnAddNutrient.TabIndex = 68;
            this.btnAddNutrient.UseVisualStyleBackColor = true;
            this.btnAddNutrient.Click += new System.EventHandler(this.btnAddNutrient_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbFrmFilterNut);
            this.groupBox1.Controls.Add(this.lueFrmNameNutrient);
            this.groupBox1.Controls.Add(this.lblFRMNutrientName);
            this.groupBox1.Controls.Add(this.lblFRMProductCode);
            this.groupBox1.Controls.Add(this.lueFrmCodeNutrient);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox1.Location = new System.Drawing.Point(7, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 75);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FRM Nutrient";
            // 
            // gbFrmFilterNut
            // 
            this.gbFrmFilterNut.Controls.Add(this.rbFrmFilterMissingXrefNut);
            this.gbFrmFilterNut.Controls.Add(this.rbFrmFilterWithXrefNut);
            this.gbFrmFilterNut.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFrmFilterNut.Location = new System.Drawing.Point(335, 11);
            this.gbFrmFilterNut.Name = "gbFrmFilterNut";
            this.gbFrmFilterNut.Size = new System.Drawing.Size(113, 57);
            this.gbFrmFilterNut.TabIndex = 33;
            this.gbFrmFilterNut.TabStop = false;
            this.gbFrmFilterNut.Text = "Filter dropdown by";
            // 
            // rbFrmFilterMissingXrefNut
            // 
            this.rbFrmFilterMissingXrefNut.AutoSize = true;
            this.rbFrmFilterMissingXrefNut.Location = new System.Drawing.Point(12, 35);
            this.rbFrmFilterMissingXrefNut.Name = "rbFrmFilterMissingXrefNut";
            this.rbFrmFilterMissingXrefNut.Size = new System.Drawing.Size(91, 18);
            this.rbFrmFilterMissingXrefNut.TabIndex = 1;
            this.rbFrmFilterMissingXrefNut.Text = "Missing Xrefs";
            this.rbFrmFilterMissingXrefNut.UseVisualStyleBackColor = true;
            this.rbFrmFilterMissingXrefNut.Click += new System.EventHandler(this.rbFrmFilterMissingXrefNut_Click);
            // 
            // rbFrmFilterWithXrefNut
            // 
            this.rbFrmFilterWithXrefNut.AutoSize = true;
            this.rbFrmFilterWithXrefNut.Checked = true;
            this.rbFrmFilterWithXrefNut.Location = new System.Drawing.Point(12, 16);
            this.rbFrmFilterWithXrefNut.Name = "rbFrmFilterWithXrefNut";
            this.rbFrmFilterWithXrefNut.Size = new System.Drawing.Size(76, 18);
            this.rbFrmFilterWithXrefNut.TabIndex = 0;
            this.rbFrmFilterWithXrefNut.TabStop = true;
            this.rbFrmFilterWithXrefNut.Text = "With Xrefs";
            this.rbFrmFilterWithXrefNut.UseVisualStyleBackColor = true;
            this.rbFrmFilterWithXrefNut.Click += new System.EventHandler(this.rbFrmFilterWithXrefNut_Click);
            // 
            // lueFrmNameNutrient
            // 
            this.lueFrmNameNutrient.Location = new System.Drawing.Point(58, 43);
            this.lueFrmNameNutrient.Name = "lueFrmNameNutrient";
            this.lueFrmNameNutrient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFrmNameNutrient.Properties.Appearance.Options.UseFont = true;
            this.lueFrmNameNutrient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFrmNameNutrient.Properties.DropDownRows = 15;
            this.lueFrmNameNutrient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueFrmNameNutrient.Size = new System.Drawing.Size(273, 22);
            this.lueFrmNameNutrient.TabIndex = 31;
            this.lueFrmNameNutrient.EditValueChanged += new System.EventHandler(this.lueFrmNameNutrient_EditValueChanged);
            this.lueFrmNameNutrient.Click += new System.EventHandler(this.lueFrmNameNutrient_Click);
            this.lueFrmNameNutrient.Enter += new System.EventHandler(this.lueFrmNameNutrient_Enter);
            // 
            // lblFRMNutrientName
            // 
            this.lblFRMNutrientName.AutoSize = true;
            this.lblFRMNutrientName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblFRMNutrientName.Location = new System.Drawing.Point(6, 46);
            this.lblFRMNutrientName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFRMNutrientName.Name = "lblFRMNutrientName";
            this.lblFRMNutrientName.Size = new System.Drawing.Size(41, 15);
            this.lblFRMNutrientName.TabIndex = 30;
            this.lblFRMNutrientName.Text = "Name";
            // 
            // lblFRMProductCode
            // 
            this.lblFRMProductCode.AutoSize = true;
            this.lblFRMProductCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblFRMProductCode.Location = new System.Drawing.Point(6, 22);
            this.lblFRMProductCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFRMProductCode.Name = "lblFRMProductCode";
            this.lblFRMProductCode.Size = new System.Drawing.Size(37, 15);
            this.lblFRMProductCode.TabIndex = 25;
            this.lblFRMProductCode.Text = "Code";
            // 
            // lueFrmCodeNutrient
            // 
            this.lueFrmCodeNutrient.Location = new System.Drawing.Point(58, 19);
            this.lueFrmCodeNutrient.Name = "lueFrmCodeNutrient";
            this.lueFrmCodeNutrient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFrmCodeNutrient.Properties.Appearance.Options.UseFont = true;
            this.lueFrmCodeNutrient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFrmCodeNutrient.Properties.DropDownRows = 15;
            this.lueFrmCodeNutrient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueFrmCodeNutrient.Size = new System.Drawing.Size(273, 22);
            this.lueFrmCodeNutrient.TabIndex = 28;
            this.lueFrmCodeNutrient.EditValueChanged += new System.EventHandler(this.lueFrmCodeNutrient_EditValueChanged);
            this.lueFrmCodeNutrient.Click += new System.EventHandler(this.lueFrmCodeNutrient_Click);
            this.lueFrmCodeNutrient.Enter += new System.EventHandler(this.lueFrmCodeNutrient_Enter);
            // 
            // gbAIM
            // 
            this.gbAIM.Controls.Add(this.gbAimFilterNut);
            this.gbAIM.Controls.Add(this.lueAIMNameNutrient);
            this.gbAIM.Controls.Add(this.label3);
            this.gbAIM.Controls.Add(this.label4);
            this.gbAIM.Controls.Add(this.lueAIMCodeNutrient);
            this.gbAIM.Font = new System.Drawing.Font("Arial", 9F);
            this.gbAIM.Location = new System.Drawing.Point(478, 11);
            this.gbAIM.Name = "gbAIM";
            this.gbAIM.Size = new System.Drawing.Size(453, 75);
            this.gbAIM.TabIndex = 35;
            this.gbAIM.TabStop = false;
            this.gbAIM.Text = "AIM Nutrient";
            // 
            // gbAimFilterNut
            // 
            this.gbAimFilterNut.Controls.Add(this.rbAimFilterMissingXrefNut);
            this.gbAimFilterNut.Controls.Add(this.rbAimFilterWithXrefNut);
            this.gbAimFilterNut.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAimFilterNut.Location = new System.Drawing.Point(335, 11);
            this.gbAimFilterNut.Name = "gbAimFilterNut";
            this.gbAimFilterNut.Size = new System.Drawing.Size(113, 57);
            this.gbAimFilterNut.TabIndex = 34;
            this.gbAimFilterNut.TabStop = false;
            this.gbAimFilterNut.Text = "Filter dropdown by";
            // 
            // rbAimFilterMissingXrefNut
            // 
            this.rbAimFilterMissingXrefNut.AutoSize = true;
            this.rbAimFilterMissingXrefNut.Location = new System.Drawing.Point(12, 35);
            this.rbAimFilterMissingXrefNut.Name = "rbAimFilterMissingXrefNut";
            this.rbAimFilterMissingXrefNut.Size = new System.Drawing.Size(91, 18);
            this.rbAimFilterMissingXrefNut.TabIndex = 1;
            this.rbAimFilterMissingXrefNut.Text = "Missing Xrefs";
            this.rbAimFilterMissingXrefNut.UseVisualStyleBackColor = true;
            this.rbAimFilterMissingXrefNut.Click += new System.EventHandler(this.rbAimFilterMissingXrefNut_Click);
            // 
            // rbAimFilterWithXrefNut
            // 
            this.rbAimFilterWithXrefNut.AutoSize = true;
            this.rbAimFilterWithXrefNut.Checked = true;
            this.rbAimFilterWithXrefNut.Location = new System.Drawing.Point(12, 16);
            this.rbAimFilterWithXrefNut.Name = "rbAimFilterWithXrefNut";
            this.rbAimFilterWithXrefNut.Size = new System.Drawing.Size(76, 18);
            this.rbAimFilterWithXrefNut.TabIndex = 0;
            this.rbAimFilterWithXrefNut.TabStop = true;
            this.rbAimFilterWithXrefNut.Text = "With Xrefs";
            this.rbAimFilterWithXrefNut.UseVisualStyleBackColor = true;
            this.rbAimFilterWithXrefNut.Click += new System.EventHandler(this.rbAimFilterWithXrefNut_Click);
            // 
            // lueAIMNameNutrient
            // 
            this.lueAIMNameNutrient.Location = new System.Drawing.Point(58, 43);
            this.lueAIMNameNutrient.Name = "lueAIMNameNutrient";
            this.lueAIMNameNutrient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMNameNutrient.Properties.Appearance.Options.UseFont = true;
            this.lueAIMNameNutrient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMNameNutrient.Properties.DropDownRows = 15;
            this.lueAIMNameNutrient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMNameNutrient.Size = new System.Drawing.Size(273, 22);
            this.lueAIMNameNutrient.TabIndex = 31;
            this.lueAIMNameNutrient.EditValueChanged += new System.EventHandler(this.lueAIMNameNutrient_EditValueChanged);
            this.lueAIMNameNutrient.Click += new System.EventHandler(this.lueAIMNameNutrient_Click);
            this.lueAIMNameNutrient.Enter += new System.EventHandler(this.lueAIMNameNutrient_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Code";
            // 
            // lueAIMCodeNutrient
            // 
            this.lueAIMCodeNutrient.Location = new System.Drawing.Point(58, 19);
            this.lueAIMCodeNutrient.Name = "lueAIMCodeNutrient";
            this.lueAIMCodeNutrient.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMCodeNutrient.Properties.Appearance.Options.UseFont = true;
            this.lueAIMCodeNutrient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMCodeNutrient.Properties.DropDownRows = 15;
            this.lueAIMCodeNutrient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMCodeNutrient.Size = new System.Drawing.Size(273, 22);
            this.lueAIMCodeNutrient.TabIndex = 29;
            this.lueAIMCodeNutrient.EditValueChanged += new System.EventHandler(this.lueAIMCodeNutrient_EditValueChanged);
            this.lueAIMCodeNutrient.Click += new System.EventHandler(this.lueAIMCodeNutrient_Click);
            this.lueAIMCodeNutrient.Enter += new System.EventHandler(this.lueAIMCodeNutrient_Enter);
            // 
            // pageOwner
            // 
            this.pageOwner.BackColor = System.Drawing.SystemColors.Control;
            this.pageOwner.Controls.Add(this.grdReferencePlant);
            this.pageOwner.Controls.Add(this.btnPreviewOwner);
            this.pageOwner.Controls.Add(this.btnAddOwner);
            this.pageOwner.Controls.Add(this.grbAimOwner);
            this.pageOwner.Controls.Add(this.gboCFCPlant);
            this.pageOwner.Controls.Add(this.gboAIMPlant);
            this.pageOwner.Location = new System.Drawing.Point(4, 23);
            this.pageOwner.Margin = new System.Windows.Forms.Padding(2);
            this.pageOwner.Name = "pageOwner";
            this.pageOwner.Padding = new System.Windows.Forms.Padding(2);
            this.pageOwner.Size = new System.Drawing.Size(1073, 481);
            this.pageOwner.TabIndex = 2;
            this.pageOwner.Text = "Plant";
            // 
            // grdReferencePlant
            // 
            this.grdReferencePlant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdReferencePlant.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdReferencePlant.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdReferencePlant.Location = new System.Drawing.Point(7, 88);
            this.grdReferencePlant.MainView = this.grdReferencePlantView;
            this.grdReferencePlant.Margin = new System.Windows.Forms.Padding(2);
            this.grdReferencePlant.Name = "grdReferencePlant";
            this.grdReferencePlant.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCPlantCode,
            this.replueAIMLocationCode,
            this.repbtnDeleteOwner,
            this.replueCFCPlantName,
            this.replueAIMLocationName});
            this.grdReferencePlant.Size = new System.Drawing.Size(1062, 389);
            this.grdReferencePlant.TabIndex = 70;
            this.grdReferencePlant.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdReferencePlantView});
            // 
            // grdReferencePlantView
            // 
            this.grdReferencePlantView.Appearance.Row.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.Appearance.Row.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdReferencePlantView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdReferencePlantView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdReferencePlantView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdReferencePlantView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdReferencePlantView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReferencePlantView.AppearancePrint.Row.Options.UseFont = true;
            this.grdReferencePlantView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPlantCrossReferenceID,
            this.colCrossCFCPlantCode,
            this.colCrossCFCPlantName,
            this.colCrossAIMLocationCode,
            this.colCrossAIMLocationName,
            this.gridColumn4,
            this.gridColumn5,
            this.colCrossDelete,
            this.gridColumn11,
            this.gridColumn12});
            this.grdReferencePlantView.GridControl = this.grdReferencePlant;
            this.grdReferencePlantView.Name = "grdReferencePlantView";
            this.grdReferencePlantView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdReferencePlantView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdReferencePlantView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdReferencePlantView.OptionsView.EnableAppearanceEvenRow = true;
            this.grdReferencePlantView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdReferencePlantView_ValidateRow);
            // 
            // colPlantCrossReferenceID
            // 
            this.colPlantCrossReferenceID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colPlantCrossReferenceID.AppearanceCell.Options.UseFont = true;
            this.colPlantCrossReferenceID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colPlantCrossReferenceID.AppearanceHeader.Options.UseFont = true;
            this.colPlantCrossReferenceID.Caption = "Plant Cross Reference";
            this.colPlantCrossReferenceID.FieldName = "PlantCrossReferenceID";
            this.colPlantCrossReferenceID.Name = "colPlantCrossReferenceID";
            // 
            // colCrossCFCPlantCode
            // 
            this.colCrossCFCPlantCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossCFCPlantCode.AppearanceCell.Options.UseFont = true;
            this.colCrossCFCPlantCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossCFCPlantCode.AppearanceHeader.Options.UseFont = true;
            this.colCrossCFCPlantCode.Caption = "FRM Plant Code";
            this.colCrossCFCPlantCode.ColumnEdit = this.replueCFCPlantCode;
            this.colCrossCFCPlantCode.FieldName = "CFCPlantID";
            this.colCrossCFCPlantCode.Name = "colCrossCFCPlantCode";
            this.colCrossCFCPlantCode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colCrossCFCPlantCode.Visible = true;
            this.colCrossCFCPlantCode.VisibleIndex = 0;
            this.colCrossCFCPlantCode.Width = 139;
            // 
            // replueCFCPlantCode
            // 
            this.replueCFCPlantCode.AutoHeight = false;
            this.replueCFCPlantCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCPlantCode.Name = "replueCFCPlantCode";
            this.replueCFCPlantCode.ValueMember = "CFCPlantID";
            // 
            // colCrossCFCPlantName
            // 
            this.colCrossCFCPlantName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossCFCPlantName.AppearanceCell.Options.UseFont = true;
            this.colCrossCFCPlantName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossCFCPlantName.AppearanceHeader.Options.UseFont = true;
            this.colCrossCFCPlantName.Caption = "FRM Plant Name";
            this.colCrossCFCPlantName.ColumnEdit = this.replueCFCPlantName;
            this.colCrossCFCPlantName.FieldName = "CFCPlantID";
            this.colCrossCFCPlantName.Name = "colCrossCFCPlantName";
            this.colCrossCFCPlantName.Visible = true;
            this.colCrossCFCPlantName.VisibleIndex = 1;
            this.colCrossCFCPlantName.Width = 182;
            // 
            // replueCFCPlantName
            // 
            this.replueCFCPlantName.AutoHeight = false;
            this.replueCFCPlantName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCPlantName.Name = "replueCFCPlantName";
            // 
            // colCrossAIMLocationCode
            // 
            this.colCrossAIMLocationCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossAIMLocationCode.AppearanceCell.Options.UseFont = true;
            this.colCrossAIMLocationCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossAIMLocationCode.AppearanceHeader.Options.UseFont = true;
            this.colCrossAIMLocationCode.Caption = "AIM Owner";
            this.colCrossAIMLocationCode.ColumnEdit = this.replueAIMLocationCode;
            this.colCrossAIMLocationCode.FieldName = "AIMOwnerID";
            this.colCrossAIMLocationCode.Name = "colCrossAIMLocationCode";
            this.colCrossAIMLocationCode.Visible = true;
            this.colCrossAIMLocationCode.VisibleIndex = 2;
            this.colCrossAIMLocationCode.Width = 184;
            // 
            // replueAIMLocationCode
            // 
            this.replueAIMLocationCode.AutoHeight = false;
            this.replueAIMLocationCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMLocationCode.Name = "replueAIMLocationCode";
            this.replueAIMLocationCode.ValueMember = "AIMPlantID";
            // 
            // colCrossAIMLocationName
            // 
            this.colCrossAIMLocationName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossAIMLocationName.AppearanceCell.Options.UseFont = true;
            this.colCrossAIMLocationName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossAIMLocationName.AppearanceHeader.Options.UseFont = true;
            this.colCrossAIMLocationName.Caption = "AIM Owner Location";
            this.colCrossAIMLocationName.ColumnEdit = this.replueAIMLocationName;
            this.colCrossAIMLocationName.FieldName = "AIMOwnerLocationID";
            this.colCrossAIMLocationName.Name = "colCrossAIMLocationName";
            this.colCrossAIMLocationName.Visible = true;
            this.colCrossAIMLocationName.VisibleIndex = 3;
            this.colCrossAIMLocationName.Width = 202;
            // 
            // replueAIMLocationName
            // 
            this.replueAIMLocationName.AutoHeight = false;
            this.replueAIMLocationName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMLocationName.Name = "replueAIMLocationName";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "User Name";
            this.gridColumn4.FieldName = "UserName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 117;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "Update Stamp";
            this.gridColumn5.DisplayFormat.FormatString = "g";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "UpdateStamp";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 145;
            // 
            // colCrossDelete
            // 
            this.colCrossDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossDelete.AppearanceCell.Options.UseFont = true;
            this.colCrossDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCrossDelete.AppearanceHeader.Options.UseFont = true;
            this.colCrossDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colCrossDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCrossDelete.Caption = "Delete";
            this.colCrossDelete.ColumnEdit = this.repbtnDeleteOwner;
            this.colCrossDelete.Name = "colCrossDelete";
            this.colCrossDelete.OptionsColumn.AllowMove = false;
            this.colCrossDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colCrossDelete.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colCrossDelete.Visible = true;
            this.colCrossDelete.VisibleIndex = 4;
            this.colCrossDelete.Width = 65;
            // 
            // repbtnDeleteOwner
            // 
            this.repbtnDeleteOwner.AutoHeight = false;
            this.repbtnDeleteOwner.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteOwner.Name = "repbtnDeleteOwner";
            this.repbtnDeleteOwner.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteOwner.Click += new System.EventHandler(this.repbtnDeleteOwner_Click);
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "CFCPlantID";
            this.gridColumn11.FieldName = "CFCPlantID";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "AIMOwnerLocationID";
            this.gridColumn12.FieldName = "AIMOwnerLocationID";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // btnPreviewOwner
            // 
            this.btnPreviewOwner.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewOwner.Location = new System.Drawing.Point(1012, 47);
            this.btnPreviewOwner.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewOwner.Name = "btnPreviewOwner";
            this.btnPreviewOwner.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewOwner.TabIndex = 69;
            this.btnPreviewOwner.UseVisualStyleBackColor = true;
            this.btnPreviewOwner.Click += new System.EventHandler(this.btnPreviewOwner_Click);
            // 
            // btnAddOwner
            // 
            this.btnAddOwner.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddOwner.Location = new System.Drawing.Point(954, 47);
            this.btnAddOwner.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddOwner.Name = "btnAddOwner";
            this.btnAddOwner.Size = new System.Drawing.Size(57, 37);
            this.btnAddOwner.TabIndex = 68;
            this.btnAddOwner.UseVisualStyleBackColor = true;
            this.btnAddOwner.Click += new System.EventHandler(this.btnAddOwner_Click);
            // 
            // grbAimOwner
            // 
            this.grbAimOwner.Controls.Add(this.lueOwnerName);
            this.grbAimOwner.Controls.Add(this.label5);
            this.grbAimOwner.Controls.Add(this.label6);
            this.grbAimOwner.Controls.Add(this.lueOwnerCode);
            this.grbAimOwner.Font = new System.Drawing.Font("Arial", 9F);
            this.grbAimOwner.Location = new System.Drawing.Point(350, 11);
            this.grbAimOwner.Name = "grbAimOwner";
            this.grbAimOwner.Size = new System.Drawing.Size(222, 75);
            this.grbAimOwner.TabIndex = 40;
            this.grbAimOwner.TabStop = false;
            this.grbAimOwner.Text = "AIM Owner";
            // 
            // lueOwnerName
            // 
            this.lueOwnerName.Location = new System.Drawing.Point(51, 42);
            this.lueOwnerName.Margin = new System.Windows.Forms.Padding(2);
            this.lueOwnerName.Name = "lueOwnerName";
            this.lueOwnerName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueOwnerName.Properties.Appearance.Options.UseFont = true;
            this.lueOwnerName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueOwnerName.Properties.DropDownRows = 15;
            this.lueOwnerName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueOwnerName.Size = new System.Drawing.Size(164, 22);
            this.lueOwnerName.TabIndex = 34;
            this.lueOwnerName.EditValueChanged += new System.EventHandler(this.lueOwnerName_EditValueChanged);
            this.lueOwnerName.Click += new System.EventHandler(this.lueOwnerName_Click);
            this.lueOwnerName.Enter += new System.EventHandler(this.lueOwnerName_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "Code";
            // 
            // lueOwnerCode
            // 
            this.lueOwnerCode.Location = new System.Drawing.Point(51, 19);
            this.lueOwnerCode.Margin = new System.Windows.Forms.Padding(2);
            this.lueOwnerCode.Name = "lueOwnerCode";
            this.lueOwnerCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueOwnerCode.Properties.Appearance.Options.UseFont = true;
            this.lueOwnerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueOwnerCode.Properties.DropDownRows = 15;
            this.lueOwnerCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueOwnerCode.Size = new System.Drawing.Size(164, 22);
            this.lueOwnerCode.TabIndex = 29;
            this.lueOwnerCode.EditValueChanged += new System.EventHandler(this.lueOwnerCode_EditValueChanged);
            this.lueOwnerCode.Click += new System.EventHandler(this.lueOwnerCode_Click);
            this.lueOwnerCode.Enter += new System.EventHandler(this.lueOwnerCode_Enter);
            // 
            // gboCFCPlant
            // 
            this.gboCFCPlant.Controls.Add(this.gbFrmFilterLoc);
            this.gboCFCPlant.Controls.Add(this.lueCFCPlantName);
            this.gboCFCPlant.Controls.Add(this.lblCFCPlantName);
            this.gboCFCPlant.Controls.Add(this.lueCFCPlantCode);
            this.gboCFCPlant.Controls.Add(this.lblCFCPlantCode);
            this.gboCFCPlant.Font = new System.Drawing.Font("Arial", 9F);
            this.gboCFCPlant.Location = new System.Drawing.Point(7, 11);
            this.gboCFCPlant.Name = "gboCFCPlant";
            this.gboCFCPlant.Size = new System.Drawing.Size(340, 75);
            this.gboCFCPlant.TabIndex = 38;
            this.gboCFCPlant.TabStop = false;
            this.gboCFCPlant.Text = "FRM Plant";
            // 
            // gbFrmFilterLoc
            // 
            this.gbFrmFilterLoc.Controls.Add(this.rbFrmFilterMissingXrefLoc);
            this.gbFrmFilterLoc.Controls.Add(this.rbFrmFilterWithXrefLoc);
            this.gbFrmFilterLoc.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFrmFilterLoc.Location = new System.Drawing.Point(224, 10);
            this.gbFrmFilterLoc.Name = "gbFrmFilterLoc";
            this.gbFrmFilterLoc.Size = new System.Drawing.Size(113, 57);
            this.gbFrmFilterLoc.TabIndex = 34;
            this.gbFrmFilterLoc.TabStop = false;
            this.gbFrmFilterLoc.Text = "Filter dropdown by";
            // 
            // rbFrmFilterMissingXrefLoc
            // 
            this.rbFrmFilterMissingXrefLoc.AutoSize = true;
            this.rbFrmFilterMissingXrefLoc.Location = new System.Drawing.Point(11, 35);
            this.rbFrmFilterMissingXrefLoc.Name = "rbFrmFilterMissingXrefLoc";
            this.rbFrmFilterMissingXrefLoc.Size = new System.Drawing.Size(91, 18);
            this.rbFrmFilterMissingXrefLoc.TabIndex = 1;
            this.rbFrmFilterMissingXrefLoc.Text = "Missing Xrefs";
            this.rbFrmFilterMissingXrefLoc.UseVisualStyleBackColor = true;
            this.rbFrmFilterMissingXrefLoc.Click += new System.EventHandler(this.rbFrmFilterMissingXrefLoc_Click);
            // 
            // rbFrmFilterWithXrefLoc
            // 
            this.rbFrmFilterWithXrefLoc.AutoSize = true;
            this.rbFrmFilterWithXrefLoc.Checked = true;
            this.rbFrmFilterWithXrefLoc.Location = new System.Drawing.Point(11, 16);
            this.rbFrmFilterWithXrefLoc.Name = "rbFrmFilterWithXrefLoc";
            this.rbFrmFilterWithXrefLoc.Size = new System.Drawing.Size(76, 18);
            this.rbFrmFilterWithXrefLoc.TabIndex = 0;
            this.rbFrmFilterWithXrefLoc.TabStop = true;
            this.rbFrmFilterWithXrefLoc.Text = "With Xrefs";
            this.rbFrmFilterWithXrefLoc.UseVisualStyleBackColor = true;
            this.rbFrmFilterWithXrefLoc.Click += new System.EventHandler(this.rbFrmFilterWithXrefLoc_Click);
            // 
            // lueCFCPlantName
            // 
            this.lueCFCPlantName.Location = new System.Drawing.Point(51, 42);
            this.lueCFCPlantName.Margin = new System.Windows.Forms.Padding(2);
            this.lueCFCPlantName.Name = "lueCFCPlantName";
            this.lueCFCPlantName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueCFCPlantName.Properties.Appearance.Options.UseFont = true;
            this.lueCFCPlantName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCFCPlantName.Properties.DropDownRows = 15;
            this.lueCFCPlantName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueCFCPlantName.Size = new System.Drawing.Size(172, 22);
            this.lueCFCPlantName.TabIndex = 32;
            this.lueCFCPlantName.EditValueChanged += new System.EventHandler(this.lueCFCPlantName_EditValueChanged);
            this.lueCFCPlantName.Click += new System.EventHandler(this.lueCFCPlantName_Click);
            this.lueCFCPlantName.Enter += new System.EventHandler(this.lueCFCPlantName_Enter);
            // 
            // lblCFCPlantName
            // 
            this.lblCFCPlantName.AutoSize = true;
            this.lblCFCPlantName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblCFCPlantName.Location = new System.Drawing.Point(6, 45);
            this.lblCFCPlantName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCFCPlantName.Name = "lblCFCPlantName";
            this.lblCFCPlantName.Size = new System.Drawing.Size(41, 15);
            this.lblCFCPlantName.TabIndex = 31;
            this.lblCFCPlantName.Text = "Name";
            // 
            // lueCFCPlantCode
            // 
            this.lueCFCPlantCode.Location = new System.Drawing.Point(51, 19);
            this.lueCFCPlantCode.Margin = new System.Windows.Forms.Padding(2);
            this.lueCFCPlantCode.Name = "lueCFCPlantCode";
            this.lueCFCPlantCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueCFCPlantCode.Properties.Appearance.Options.UseFont = true;
            this.lueCFCPlantCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCFCPlantCode.Properties.DropDownRows = 15;
            this.lueCFCPlantCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueCFCPlantCode.Size = new System.Drawing.Size(172, 22);
            this.lueCFCPlantCode.TabIndex = 30;
            this.lueCFCPlantCode.EditValueChanged += new System.EventHandler(this.lueCFCPlantCode_EditValueChanged);
            this.lueCFCPlantCode.Click += new System.EventHandler(this.lueCFCPlantCode_Click);
            this.lueCFCPlantCode.Enter += new System.EventHandler(this.lueCFCPlantCode_Enter);
            // 
            // lblCFCPlantCode
            // 
            this.lblCFCPlantCode.AutoSize = true;
            this.lblCFCPlantCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblCFCPlantCode.Location = new System.Drawing.Point(6, 22);
            this.lblCFCPlantCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCFCPlantCode.Name = "lblCFCPlantCode";
            this.lblCFCPlantCode.Size = new System.Drawing.Size(37, 15);
            this.lblCFCPlantCode.TabIndex = 25;
            this.lblCFCPlantCode.Text = "Code";
            // 
            // gboAIMPlant
            // 
            this.gboAIMPlant.Controls.Add(this.gbAimFilterLoc);
            this.gboAIMPlant.Controls.Add(this.lueAIMLocationName);
            this.gboAIMPlant.Controls.Add(this.lblAIMPlantName);
            this.gboAIMPlant.Controls.Add(this.lblAIMPlantCode);
            this.gboAIMPlant.Controls.Add(this.lueAIMLocationCode);
            this.gboAIMPlant.Font = new System.Drawing.Font("Arial", 9F);
            this.gboAIMPlant.Location = new System.Drawing.Point(578, 11);
            this.gboAIMPlant.Name = "gboAIMPlant";
            this.gboAIMPlant.Size = new System.Drawing.Size(370, 75);
            this.gboAIMPlant.TabIndex = 39;
            this.gboAIMPlant.TabStop = false;
            this.gboAIMPlant.Text = "AIM Owner Location";
            // 
            // gbAimFilterLoc
            // 
            this.gbAimFilterLoc.Controls.Add(this.rbAimFilterMissingXrefLoc);
            this.gbAimFilterLoc.Controls.Add(this.rbAimFilterWithXrefLoc);
            this.gbAimFilterLoc.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAimFilterLoc.Location = new System.Drawing.Point(254, 10);
            this.gbAimFilterLoc.Name = "gbAimFilterLoc";
            this.gbAimFilterLoc.Size = new System.Drawing.Size(113, 57);
            this.gbAimFilterLoc.TabIndex = 35;
            this.gbAimFilterLoc.TabStop = false;
            this.gbAimFilterLoc.Text = "Filter dropdown by";
            // 
            // rbAimFilterMissingXrefLoc
            // 
            this.rbAimFilterMissingXrefLoc.AutoSize = true;
            this.rbAimFilterMissingXrefLoc.Location = new System.Drawing.Point(11, 35);
            this.rbAimFilterMissingXrefLoc.Name = "rbAimFilterMissingXrefLoc";
            this.rbAimFilterMissingXrefLoc.Size = new System.Drawing.Size(91, 18);
            this.rbAimFilterMissingXrefLoc.TabIndex = 1;
            this.rbAimFilterMissingXrefLoc.Text = "Missing Xrefs";
            this.rbAimFilterMissingXrefLoc.UseVisualStyleBackColor = true;
            this.rbAimFilterMissingXrefLoc.Click += new System.EventHandler(this.rbAimFilterMissingXrefLoc_Click);
            // 
            // rbAimFilterWithXrefLoc
            // 
            this.rbAimFilterWithXrefLoc.AutoSize = true;
            this.rbAimFilterWithXrefLoc.Checked = true;
            this.rbAimFilterWithXrefLoc.Location = new System.Drawing.Point(11, 16);
            this.rbAimFilterWithXrefLoc.Name = "rbAimFilterWithXrefLoc";
            this.rbAimFilterWithXrefLoc.Size = new System.Drawing.Size(76, 18);
            this.rbAimFilterWithXrefLoc.TabIndex = 0;
            this.rbAimFilterWithXrefLoc.TabStop = true;
            this.rbAimFilterWithXrefLoc.Text = "With Xrefs";
            this.rbAimFilterWithXrefLoc.UseVisualStyleBackColor = true;
            this.rbAimFilterWithXrefLoc.Click += new System.EventHandler(this.rbAimFilterWithXrefLoc_Click);
            // 
            // lueAIMLocationName
            // 
            this.lueAIMLocationName.Location = new System.Drawing.Point(51, 42);
            this.lueAIMLocationName.Margin = new System.Windows.Forms.Padding(2);
            this.lueAIMLocationName.Name = "lueAIMLocationName";
            this.lueAIMLocationName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMLocationName.Properties.Appearance.Options.UseFont = true;
            this.lueAIMLocationName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMLocationName.Properties.DropDownRows = 15;
            this.lueAIMLocationName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMLocationName.Size = new System.Drawing.Size(202, 22);
            this.lueAIMLocationName.TabIndex = 34;
            this.lueAIMLocationName.EditValueChanged += new System.EventHandler(this.lueAIMLocationName_EditValueChanged);
            this.lueAIMLocationName.Click += new System.EventHandler(this.lueAIMLocationName_Click);
            this.lueAIMLocationName.Enter += new System.EventHandler(this.lueAIMLocationName_Enter);
            // 
            // lblAIMPlantName
            // 
            this.lblAIMPlantName.AutoSize = true;
            this.lblAIMPlantName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblAIMPlantName.Location = new System.Drawing.Point(6, 45);
            this.lblAIMPlantName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAIMPlantName.Name = "lblAIMPlantName";
            this.lblAIMPlantName.Size = new System.Drawing.Size(41, 15);
            this.lblAIMPlantName.TabIndex = 33;
            this.lblAIMPlantName.Text = "Name";
            // 
            // lblAIMPlantCode
            // 
            this.lblAIMPlantCode.AutoSize = true;
            this.lblAIMPlantCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblAIMPlantCode.Location = new System.Drawing.Point(6, 22);
            this.lblAIMPlantCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAIMPlantCode.Name = "lblAIMPlantCode";
            this.lblAIMPlantCode.Size = new System.Drawing.Size(37, 15);
            this.lblAIMPlantCode.TabIndex = 27;
            this.lblAIMPlantCode.Text = "Code";
            // 
            // lueAIMLocationCode
            // 
            this.lueAIMLocationCode.Location = new System.Drawing.Point(51, 19);
            this.lueAIMLocationCode.Margin = new System.Windows.Forms.Padding(2);
            this.lueAIMLocationCode.Name = "lueAIMLocationCode";
            this.lueAIMLocationCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMLocationCode.Properties.Appearance.Options.UseFont = true;
            this.lueAIMLocationCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMLocationCode.Properties.DropDownRows = 15;
            this.lueAIMLocationCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMLocationCode.Size = new System.Drawing.Size(202, 22);
            this.lueAIMLocationCode.TabIndex = 29;
            this.lueAIMLocationCode.EditValueChanged += new System.EventHandler(this.lueAIMLocationCode_EditValueChanged);
            this.lueAIMLocationCode.Click += new System.EventHandler(this.lueAIMLocationCode_Click);
            this.lueAIMLocationCode.Enter += new System.EventHandler(this.lueAIMLocationCode_Enter);
            // 
            // pageProduct
            // 
            this.pageProduct.BackColor = System.Drawing.SystemColors.Control;
            this.pageProduct.Controls.Add(this.grdProduct);
            this.pageProduct.Controls.Add(this.btnPreviewProduct);
            this.pageProduct.Controls.Add(this.btnAddProduct);
            this.pageProduct.Controls.Add(this.gboCFCProduct);
            this.pageProduct.Controls.Add(this.gboAnalyzerMaterial);
            this.pageProduct.Location = new System.Drawing.Point(4, 23);
            this.pageProduct.Margin = new System.Windows.Forms.Padding(2);
            this.pageProduct.Name = "pageProduct";
            this.pageProduct.Padding = new System.Windows.Forms.Padding(2);
            this.pageProduct.Size = new System.Drawing.Size(1073, 481);
            this.pageProduct.TabIndex = 3;
            this.pageProduct.Text = "Product";
            // 
            // grdProduct
            // 
            this.grdProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdProduct.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdProduct.Location = new System.Drawing.Point(7, 134);
            this.grdProduct.MainView = this.grdProductView;
            this.grdProduct.Margin = new System.Windows.Forms.Padding(2);
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCProductCode,
            this.replueAIMMaterialCode,
            this.repbtnDeleteProduct,
            this.replueCFCProductName,
            this.replueAIMMaterialName});
            this.grdProduct.Size = new System.Drawing.Size(1062, 345);
            this.grdProduct.TabIndex = 71;
            this.grdProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdProductView});
            // 
            // grdProductView
            // 
            this.grdProductView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdProductView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdProductView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdProductView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.DetailTip.Options.UseFont = true;
            this.grdProductView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.Empty.Options.UseFont = true;
            this.grdProductView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.EvenRow.Options.UseFont = true;
            this.grdProductView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdProductView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdProductView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FixedLine.Options.UseFont = true;
            this.grdProductView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdProductView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdProductView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdProductView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.GroupButton.Options.UseFont = true;
            this.grdProductView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdProductView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdProductView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.GroupRow.Options.UseFont = true;
            this.grdProductView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdProductView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdProductView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.HorzLine.Options.UseFont = true;
            this.grdProductView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.OddRow.Options.UseFont = true;
            this.grdProductView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.Preview.Options.UseFont = true;
            this.grdProductView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.Row.Options.UseFont = true;
            this.grdProductView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdProductView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdProductView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdProductView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.VertLine.Options.UseFont = true;
            this.grdProductView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProductView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdProductView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdProductView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdProductView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdProductView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdProductView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdProductView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdProductView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdProductView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdProductView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdProductView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdProductView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdProductView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdProductView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdProductView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProductView.AppearancePrint.Row.Options.UseFont = true;
            this.grdProductView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductCrossReferenceID,
            this.colCFCProductCode,
            this.colCFCProductName,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.colCFCPlantCode,
            this.colAIMOwnerLocationCode,
            this.colCFCPlantID,
            this.AIMOwnerLocationID,
            this.gridColumn14,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.grdProductView.GridControl = this.grdProduct;
            this.grdProductView.Name = "grdProductView";
            this.grdProductView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdProductView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdProductView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdProductView.OptionsView.EnableAppearanceEvenRow = true;
            this.grdProductView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCFCProductCode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grdProductView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdProductView_ValidateRow);
            this.grdProductView.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.grdProductView_CustomColumnSort);
            // 
            // colProductCrossReferenceID
            // 
            this.colProductCrossReferenceID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colProductCrossReferenceID.AppearanceCell.Options.UseFont = true;
            this.colProductCrossReferenceID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colProductCrossReferenceID.AppearanceHeader.Options.UseFont = true;
            this.colProductCrossReferenceID.Caption = "gridColumn1";
            this.colProductCrossReferenceID.FieldName = "ProductCrossReferenceID";
            this.colProductCrossReferenceID.Name = "colProductCrossReferenceID";
            this.colProductCrossReferenceID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colCFCProductCode
            // 
            this.colCFCProductCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCProductCode.AppearanceCell.Options.UseFont = true;
            this.colCFCProductCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCProductCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCProductCode.Caption = "FRM Product Code";
            this.colCFCProductCode.ColumnEdit = this.replueCFCProductCode;
            this.colCFCProductCode.FieldName = "CFCProductID";
            this.colCFCProductCode.Name = "colCFCProductCode";
            this.colCFCProductCode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colCFCProductCode.Visible = true;
            this.colCFCProductCode.VisibleIndex = 0;
            this.colCFCProductCode.Width = 113;
            // 
            // replueCFCProductCode
            // 
            this.replueCFCProductCode.AutoHeight = false;
            this.replueCFCProductCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCProductCode.Name = "replueCFCProductCode";
            // 
            // colCFCProductName
            // 
            this.colCFCProductName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCProductName.AppearanceCell.Options.UseFont = true;
            this.colCFCProductName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCProductName.AppearanceHeader.Options.UseFont = true;
            this.colCFCProductName.Caption = "FRM Product Name";
            this.colCFCProductName.ColumnEdit = this.replueCFCProductName;
            this.colCFCProductName.FieldName = "CFCProductID";
            this.colCFCProductName.Name = "colCFCProductName";
            this.colCFCProductName.Visible = true;
            this.colCFCProductName.VisibleIndex = 1;
            this.colCFCProductName.Width = 159;
            // 
            // replueCFCProductName
            // 
            this.replueCFCProductName.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replueCFCProductName.Appearance.Options.UseFont = true;
            this.replueCFCProductName.AutoHeight = false;
            this.replueCFCProductName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCProductName.Name = "replueCFCProductName";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "AIM Material Code";
            this.gridColumn6.ColumnEdit = this.replueAIMMaterialCode;
            this.gridColumn6.FieldName = "AIMMaterialID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 146;
            // 
            // replueAIMMaterialCode
            // 
            this.replueAIMMaterialCode.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replueAIMMaterialCode.Appearance.Options.UseFont = true;
            this.replueAIMMaterialCode.AutoHeight = false;
            this.replueAIMMaterialCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMMaterialCode.Name = "replueAIMMaterialCode";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "AIM Material Name";
            this.gridColumn7.ColumnEdit = this.replueAIMMaterialName;
            this.gridColumn7.FieldName = "AIMMaterialID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 157;
            // 
            // replueAIMMaterialName
            // 
            this.replueAIMMaterialName.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replueAIMMaterialName.Appearance.Options.UseFont = true;
            this.replueAIMMaterialName.AutoHeight = false;
            this.replueAIMMaterialName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMMaterialName.Name = "replueAIMMaterialName";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Arial", 7.2F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "User Name";
            this.gridColumn8.FieldName = "UserName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 92;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Arial", 7.2F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "Update Stamp";
            this.gridColumn9.DisplayFormat.FormatString = "g";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn9.FieldName = "UpdateStamp";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 117;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Delete";
            this.gridColumn10.ColumnEdit = this.repbtnDeleteProduct;
            this.gridColumn10.FieldName = "colDelete";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumn10.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn10.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 57;
            // 
            // repbtnDeleteProduct
            // 
            this.repbtnDeleteProduct.AutoHeight = false;
            this.repbtnDeleteProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteProduct.Name = "repbtnDeleteProduct";
            this.repbtnDeleteProduct.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteProduct.Click += new System.EventHandler(this.repbtnDeleteProduct_Click);
            // 
            // colCFCPlantCode
            // 
            this.colCFCPlantCode.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colCFCPlantCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCPlantCode.AppearanceCell.Options.UseBackColor = true;
            this.colCFCPlantCode.AppearanceCell.Options.UseFont = true;
            this.colCFCPlantCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCFCPlantCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCFCPlantCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCPlantCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCPlantCode.Caption = "FRM Product Code";
            this.colCFCPlantCode.FieldName = "CFCPlantCode";
            this.colCFCPlantCode.Name = "colCFCPlantCode";
            this.colCFCPlantCode.OptionsColumn.AllowEdit = false;
            this.colCFCPlantCode.Width = 87;
            // 
            // colAIMOwnerLocationCode
            // 
            this.colAIMOwnerLocationCode.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colAIMOwnerLocationCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMOwnerLocationCode.AppearanceCell.Options.UseBackColor = true;
            this.colAIMOwnerLocationCode.AppearanceCell.Options.UseFont = true;
            this.colAIMOwnerLocationCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAIMOwnerLocationCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMOwnerLocationCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMOwnerLocationCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMOwnerLocationCode.Caption = "AIM Owner Location Code";
            this.colAIMOwnerLocationCode.FieldName = "LocationCode";
            this.colAIMOwnerLocationCode.Name = "colAIMOwnerLocationCode";
            this.colAIMOwnerLocationCode.OptionsColumn.AllowEdit = false;
            this.colAIMOwnerLocationCode.Width = 87;
            // 
            // colCFCPlantID
            // 
            this.colCFCPlantID.Caption = "CFCPlantID";
            this.colCFCPlantID.FieldName = "CFCPlantID";
            this.colCFCPlantID.Name = "colCFCPlantID";
            // 
            // AIMOwnerLocationID
            // 
            this.AIMOwnerLocationID.Caption = "AimOwnerLocationID";
            this.AIMOwnerLocationID.FieldName = "AIMOwnerLocationID";
            this.AIMOwnerLocationID.Name = "AIMOwnerLocationID";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "AimMaterialID";
            this.gridColumn14.FieldName = "AIMMaterialID";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "CFCProductID";
            this.gridColumn16.FieldName = "CFCProductID";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "MaterialType";
            this.gridColumn17.FieldName = "MaterialTypeID";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn18.AppearanceCell.Options.UseFont = true;
            this.gridColumn18.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn18.AppearanceHeader.Options.UseFont = true;
            this.gridColumn18.Caption = "FRM Plant Name";
            this.gridColumn18.FieldName = "CFCPlantName";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 2;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn19.AppearanceCell.Options.UseFont = true;
            this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn19.AppearanceHeader.Options.UseFont = true;
            this.gridColumn19.Caption = "AIM OwnerLocation Name";
            this.gridColumn19.FieldName = "LocationName";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 5;
            // 
            // btnPreviewProduct
            // 
            this.btnPreviewProduct.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewProduct.Location = new System.Drawing.Point(1009, 88);
            this.btnPreviewProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewProduct.Name = "btnPreviewProduct";
            this.btnPreviewProduct.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewProduct.TabIndex = 70;
            this.btnPreviewProduct.UseVisualStyleBackColor = true;
            this.btnPreviewProduct.Click += new System.EventHandler(this.btnPreviewProduct_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddProduct.Location = new System.Drawing.Point(951, 88);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(57, 37);
            this.btnAddProduct.TabIndex = 69;
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // gboCFCProduct
            // 
            this.gboCFCProduct.Controls.Add(this.gbFrmFilterProd);
            this.gboCFCProduct.Controls.Add(this.lueProductCode);
            this.gboCFCProduct.Controls.Add(this.lueProductName);
            this.gboCFCProduct.Controls.Add(this.label7);
            this.gboCFCProduct.Controls.Add(this.label8);
            this.gboCFCProduct.Controls.Add(this.label11);
            this.gboCFCProduct.Controls.Add(this.luePlant_Product);
            this.gboCFCProduct.Font = new System.Drawing.Font("Arial", 9F);
            this.gboCFCProduct.Location = new System.Drawing.Point(7, 14);
            this.gboCFCProduct.Name = "gboCFCProduct";
            this.gboCFCProduct.Size = new System.Drawing.Size(462, 114);
            this.gboCFCProduct.TabIndex = 36;
            this.gboCFCProduct.TabStop = false;
            this.gboCFCProduct.Text = "FRM Product";
            // 
            // gbFrmFilterProd
            // 
            this.gbFrmFilterProd.Controls.Add(this.rbFrmFilterMissingXrefProd);
            this.gbFrmFilterProd.Controls.Add(this.rbFrmFilterWithXrefProd);
            this.gbFrmFilterProd.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFrmFilterProd.Location = new System.Drawing.Point(343, 54);
            this.gbFrmFilterProd.Name = "gbFrmFilterProd";
            this.gbFrmFilterProd.Size = new System.Drawing.Size(113, 57);
            this.gbFrmFilterProd.TabIndex = 74;
            this.gbFrmFilterProd.TabStop = false;
            this.gbFrmFilterProd.Text = "Filter dropdown by";
            // 
            // rbFrmFilterMissingXrefProd
            // 
            this.rbFrmFilterMissingXrefProd.AutoSize = true;
            this.rbFrmFilterMissingXrefProd.Location = new System.Drawing.Point(10, 35);
            this.rbFrmFilterMissingXrefProd.Name = "rbFrmFilterMissingXrefProd";
            this.rbFrmFilterMissingXrefProd.Size = new System.Drawing.Size(91, 18);
            this.rbFrmFilterMissingXrefProd.TabIndex = 1;
            this.rbFrmFilterMissingXrefProd.Text = "Missing Xrefs";
            this.rbFrmFilterMissingXrefProd.UseVisualStyleBackColor = true;
            this.rbFrmFilterMissingXrefProd.Click += new System.EventHandler(this.rbFrmFilterMissingXrefProd_Click);
            // 
            // rbFrmFilterWithXrefProd
            // 
            this.rbFrmFilterWithXrefProd.AutoSize = true;
            this.rbFrmFilterWithXrefProd.Checked = true;
            this.rbFrmFilterWithXrefProd.Location = new System.Drawing.Point(10, 16);
            this.rbFrmFilterWithXrefProd.Name = "rbFrmFilterWithXrefProd";
            this.rbFrmFilterWithXrefProd.Size = new System.Drawing.Size(76, 18);
            this.rbFrmFilterWithXrefProd.TabIndex = 0;
            this.rbFrmFilterWithXrefProd.TabStop = true;
            this.rbFrmFilterWithXrefProd.Text = "With Xrefs";
            this.rbFrmFilterWithXrefProd.UseVisualStyleBackColor = true;
            this.rbFrmFilterWithXrefProd.Click += new System.EventHandler(this.rbFrmFilterWithXrefProd_Click);
            // 
            // lueProductCode
            // 
            this.lueProductCode.Location = new System.Drawing.Point(83, 63);
            this.lueProductCode.Name = "lueProductCode";
            this.lueProductCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueProductCode.Properties.Appearance.Options.UseFont = true;
            this.lueProductCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProductCode.Properties.DropDownRows = 15;
            this.lueProductCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueProductCode.Size = new System.Drawing.Size(257, 22);
            this.lueProductCode.TabIndex = 32;
            this.lueProductCode.EditValueChanged += new System.EventHandler(this.lueCFCProductCode_EditValueChanged);
            this.lueProductCode.Click += new System.EventHandler(this.lueProductCode_Click);
            this.lueProductCode.Enter += new System.EventHandler(this.lueProductCode_Enter);
            // 
            // lueProductName
            // 
            this.lueProductName.Location = new System.Drawing.Point(83, 85);
            this.lueProductName.Name = "lueProductName";
            this.lueProductName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueProductName.Properties.Appearance.Options.UseFont = true;
            this.lueProductName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProductName.Properties.DropDownRows = 15;
            this.lueProductName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueProductName.Size = new System.Drawing.Size(257, 22);
            this.lueProductName.TabIndex = 31;
            this.lueProductName.EditValueChanged += new System.EventHandler(this.lueCFCProductName_EditValueChanged);
            this.lueProductName.Click += new System.EventHandler(this.lueProductName_Click);
            this.lueProductName.Enter += new System.EventHandler(this.lueProductName_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(30, 67);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(30, 89);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 30;
            this.label8.Text = "Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(18, 22);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 15);
            this.label11.TabIndex = 72;
            this.label11.Text = "Plant ";
            // 
            // luePlant_Product
            // 
            this.luePlant_Product.Location = new System.Drawing.Point(64, 18);
            this.luePlant_Product.Name = "luePlant_Product";
            this.luePlant_Product.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.luePlant_Product.Properties.Appearance.Options.UseFont = true;
            this.luePlant_Product.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePlant_Product.Properties.DropDownRows = 15;
            this.luePlant_Product.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.luePlant_Product.Size = new System.Drawing.Size(261, 22);
            this.luePlant_Product.TabIndex = 73;
            this.luePlant_Product.EditValueChanged += new System.EventHandler(this.luePlant_Product_EditValueChanged);
            this.luePlant_Product.Click += new System.EventHandler(this.luePlant_Product_Click);
            this.luePlant_Product.Enter += new System.EventHandler(this.luePlant_Product_Enter);
            // 
            // gboAnalyzerMaterial
            // 
            this.gboAnalyzerMaterial.Controls.Add(this.gbAimFilterProd);
            this.gboAnalyzerMaterial.Controls.Add(this.label13);
            this.gboAnalyzerMaterial.Controls.Add(this.label9);
            this.gboAnalyzerMaterial.Controls.Add(this.lueOwnerLocation_Material);
            this.gboAnalyzerMaterial.Controls.Add(this.lueMaterialName);
            this.gboAnalyzerMaterial.Controls.Add(this.label12);
            this.gboAnalyzerMaterial.Controls.Add(this.lueOwner_Material);
            this.gboAnalyzerMaterial.Controls.Add(this.lueMaterialCode);
            this.gboAnalyzerMaterial.Controls.Add(this.label10);
            this.gboAnalyzerMaterial.Font = new System.Drawing.Font("Arial", 9F);
            this.gboAnalyzerMaterial.Location = new System.Drawing.Point(475, 14);
            this.gboAnalyzerMaterial.Name = "gboAnalyzerMaterial";
            this.gboAnalyzerMaterial.Size = new System.Drawing.Size(368, 114);
            this.gboAnalyzerMaterial.TabIndex = 37;
            this.gboAnalyzerMaterial.TabStop = false;
            this.gboAnalyzerMaterial.Text = "AIM Material";
            // 
            // gbAimFilterProd
            // 
            this.gbAimFilterProd.AutoSize = true;
            this.gbAimFilterProd.Controls.Add(this.rbAimFilterMissingXrefProd);
            this.gbAimFilterProd.Controls.Add(this.rbAimFilterWithXrefProd);
            this.gbAimFilterProd.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAimFilterProd.Location = new System.Drawing.Point(356, 54);
            this.gbAimFilterProd.Name = "gbAimFilterProd";
            this.gbAimFilterProd.Size = new System.Drawing.Size(112, 71);
            this.gbAimFilterProd.TabIndex = 78;
            this.gbAimFilterProd.TabStop = false;
            this.gbAimFilterProd.Text = "Filter dropdown by";
            this.gbAimFilterProd.Visible = false;
            // 
            // rbAimFilterMissingXrefProd
            // 
            this.rbAimFilterMissingXrefProd.AutoSize = true;
            this.rbAimFilterMissingXrefProd.Location = new System.Drawing.Point(6, 34);
            this.rbAimFilterMissingXrefProd.Name = "rbAimFilterMissingXrefProd";
            this.rbAimFilterMissingXrefProd.Size = new System.Drawing.Size(91, 18);
            this.rbAimFilterMissingXrefProd.TabIndex = 1;
            this.rbAimFilterMissingXrefProd.Text = "Missing Xrefs";
            this.rbAimFilterMissingXrefProd.UseVisualStyleBackColor = true;
            this.rbAimFilterMissingXrefProd.Click += new System.EventHandler(this.rbAimFilterMissingXrefProd_Click);
            // 
            // rbAimFilterWithXrefProd
            // 
            this.rbAimFilterWithXrefProd.AutoSize = true;
            this.rbAimFilterWithXrefProd.Checked = true;
            this.rbAimFilterWithXrefProd.Location = new System.Drawing.Point(6, 16);
            this.rbAimFilterWithXrefProd.Name = "rbAimFilterWithXrefProd";
            this.rbAimFilterWithXrefProd.Size = new System.Drawing.Size(76, 18);
            this.rbAimFilterWithXrefProd.TabIndex = 0;
            this.rbAimFilterWithXrefProd.TabStop = true;
            this.rbAimFilterWithXrefProd.Text = "With Xrefs";
            this.rbAimFilterWithXrefProd.UseVisualStyleBackColor = true;
            this.rbAimFilterWithXrefProd.Click += new System.EventHandler(this.rbAimFilterWithXrefProd_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.Location = new System.Drawing.Point(19, 43);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 15);
            this.label13.TabIndex = 76;
            this.label13.Text = "Location";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.Location = new System.Drawing.Point(40, 90);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 15);
            this.label9.TabIndex = 32;
            this.label9.Text = "Name";
            // 
            // lueOwnerLocation_Material
            // 
            this.lueOwnerLocation_Material.Location = new System.Drawing.Point(78, 40);
            this.lueOwnerLocation_Material.Name = "lueOwnerLocation_Material";
            this.lueOwnerLocation_Material.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueOwnerLocation_Material.Properties.Appearance.Options.UseFont = true;
            this.lueOwnerLocation_Material.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueOwnerLocation_Material.Properties.DropDownRows = 15;
            this.lueOwnerLocation_Material.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueOwnerLocation_Material.Size = new System.Drawing.Size(261, 22);
            this.lueOwnerLocation_Material.TabIndex = 77;
            this.lueOwnerLocation_Material.EditValueChanged += new System.EventHandler(this.lueOwnerLocation_Material_EditValueChanged);
            this.lueOwnerLocation_Material.Click += new System.EventHandler(this.lueOwnerLocation_Material_Click);
            this.lueOwnerLocation_Material.Enter += new System.EventHandler(this.lueOwnerLocation_Material_Enter);
            // 
            // lueMaterialName
            // 
            this.lueMaterialName.Location = new System.Drawing.Point(93, 86);
            this.lueMaterialName.Name = "lueMaterialName";
            this.lueMaterialName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueMaterialName.Properties.Appearance.Options.UseFont = true;
            this.lueMaterialName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueMaterialName.Properties.DropDownRows = 15;
            this.lueMaterialName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueMaterialName.Size = new System.Drawing.Size(257, 22);
            this.lueMaterialName.TabIndex = 33;
            this.lueMaterialName.EditValueChanged += new System.EventHandler(this.lueAIMNameProduct_EditValueChanged);
            this.lueMaterialName.Click += new System.EventHandler(this.lueMaterialName_Click);
            this.lueMaterialName.Enter += new System.EventHandler(this.lueMaterialName_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.Location = new System.Drawing.Point(19, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 15);
            this.label12.TabIndex = 74;
            this.label12.Text = "Owner";
            // 
            // lueOwner_Material
            // 
            this.lueOwner_Material.Location = new System.Drawing.Point(78, 18);
            this.lueOwner_Material.Name = "lueOwner_Material";
            this.lueOwner_Material.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueOwner_Material.Properties.Appearance.Options.UseFont = true;
            this.lueOwner_Material.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueOwner_Material.Properties.DropDownRows = 15;
            this.lueOwner_Material.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueOwner_Material.Size = new System.Drawing.Size(261, 22);
            this.lueOwner_Material.TabIndex = 75;
            this.lueOwner_Material.EditValueChanged += new System.EventHandler(this.lueOwner_Material_EditValueChanged);
            this.lueOwner_Material.Click += new System.EventHandler(this.lueOwner_Material_Click);
            this.lueOwner_Material.Enter += new System.EventHandler(this.lueOwner_Material_Enter);
            // 
            // lueMaterialCode
            // 
            this.lueMaterialCode.Location = new System.Drawing.Point(93, 64);
            this.lueMaterialCode.Name = "lueMaterialCode";
            this.lueMaterialCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueMaterialCode.Properties.Appearance.Options.UseFont = true;
            this.lueMaterialCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueMaterialCode.Properties.DropDownRows = 15;
            this.lueMaterialCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueMaterialCode.Size = new System.Drawing.Size(257, 22);
            this.lueMaterialCode.TabIndex = 29;
            this.lueMaterialCode.EditValueChanged += new System.EventHandler(this.lueAIMCodeProduct_EditValueChanged);
            this.lueMaterialCode.Click += new System.EventHandler(this.lueMaterialCode_Click);
            this.lueMaterialCode.Enter += new System.EventHandler(this.lueMaterialCode_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.Location = new System.Drawing.Point(40, 68);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 15);
            this.label10.TabIndex = 27;
            this.label10.Text = "Code";
            // 
            // tpProductsMissingXRef
            // 
            this.tpProductsMissingXRef.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tpProductsMissingXRef.Controls.Add(this.btnUpdateXref);
            this.tpProductsMissingXRef.Controls.Add(this.grdProdMissing);
            this.tpProductsMissingXRef.Location = new System.Drawing.Point(4, 23);
            this.tpProductsMissingXRef.Name = "tpProductsMissingXRef";
            this.tpProductsMissingXRef.Padding = new System.Windows.Forms.Padding(3);
            this.tpProductsMissingXRef.Size = new System.Drawing.Size(1073, 481);
            this.tpProductsMissingXRef.TabIndex = 4;
            this.tpProductsMissingXRef.Text = "Products Missing XRef";
            // 
            // btnUpdateXref
            // 
            this.btnUpdateXref.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateXref.BackColor = System.Drawing.SystemColors.Info;
            this.btnUpdateXref.Font = new System.Drawing.Font("Arial", 9F);
            this.btnUpdateXref.Location = new System.Drawing.Point(837, 35);
            this.btnUpdateXref.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateXref.Name = "btnUpdateXref";
            this.btnUpdateXref.Size = new System.Drawing.Size(127, 39);
            this.btnUpdateXref.TabIndex = 141;
            this.btnUpdateXref.Text = "Create Product Cross-Reference";
            this.btnUpdateXref.UseVisualStyleBackColor = false;
            this.btnUpdateXref.Click += new System.EventHandler(this.btnUpdateXref_Click);
            // 
            // grdProdMissing
            // 
            this.grdProdMissing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProdMissing.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdProdMissing.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdProdMissing.Location = new System.Drawing.Point(5, 78);
            this.grdProdMissing.MainView = this.grdProdMissingView;
            this.grdProdMissing.Margin = new System.Windows.Forms.Padding(2);
            this.grdProdMissing.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdProdMissing.Name = "grdProdMissing";
            this.grdProdMissing.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repHyperStatus});
            this.grdProdMissing.Size = new System.Drawing.Size(1062, 398);
            this.grdProdMissing.TabIndex = 70;
            this.grdProdMissing.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdProdMissingView});
            // 
            // grdProdMissingView
            // 
            this.grdProdMissingView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdProdMissingView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdProdMissingView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdProdMissingView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.DetailTip.Options.UseFont = true;
            this.grdProdMissingView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.Empty.Options.UseFont = true;
            this.grdProdMissingView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.EvenRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FixedLine.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdProdMissingView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.GroupButton.Options.UseFont = true;
            this.grdProdMissingView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdProdMissingView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdProdMissingView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.GroupRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdProdMissingView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.HorzLine.Options.UseFont = true;
            this.grdProdMissingView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.OddRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.Preview.Options.UseFont = true;
            this.grdProdMissingView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.Row.Options.UseFont = true;
            this.grdProdMissingView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdProdMissingView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdProdMissingView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.VertLine.Options.UseFont = true;
            this.grdProdMissingView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdProdMissingView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdProdMissingView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdProdMissingView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdProdMissingView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdProdMissingView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdProdMissingView.AppearancePrint.Row.Options.UseFont = true;
            this.grdProdMissingView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn30,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn29,
            this.gridColumn25,
            this.gridColumn26,
            this.colStatus});
            this.grdProdMissingView.GridControl = this.grdProdMissing;
            this.grdProdMissingView.Name = "grdProdMissingView";
            this.grdProdMissingView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdProdMissingView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdProdMissingView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdProdMissingView.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "CFCProductRefAIMMaterialID";
            this.gridColumn20.FieldName = "CFCProductRefAIMMaterialID";
            this.gridColumn20.Name = "gridColumn20";
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "CFCProductID";
            this.gridColumn21.FieldName = "CFCProductID";
            this.gridColumn21.Name = "gridColumn21";
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn22.AppearanceCell.Options.UseFont = true;
            this.gridColumn22.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn22.AppearanceHeader.Options.UseFont = true;
            this.gridColumn22.Caption = "CFCPlantID";
            this.gridColumn22.FieldName = "CFCPlantID";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "AIMOwnerLocationID";
            this.gridColumn30.FieldName = "AIMOwnerLocationID";
            this.gridColumn30.Name = "gridColumn30";
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn23.AppearanceCell.Options.UseFont = true;
            this.gridColumn23.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn23.AppearanceHeader.Options.UseFont = true;
            this.gridColumn23.Caption = "FRM Product Code";
            this.gridColumn23.FieldName = "CFCProductCode";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 0;
            this.gridColumn23.Width = 136;
            // 
            // gridColumn24
            // 
            this.gridColumn24.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn24.AppearanceCell.Options.UseFont = true;
            this.gridColumn24.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn24.AppearanceHeader.Options.UseFont = true;
            this.gridColumn24.Caption = "FRM Product Name";
            this.gridColumn24.FieldName = "CFCProductName";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 1;
            this.gridColumn24.Width = 201;
            // 
            // gridColumn29
            // 
            this.gridColumn29.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn29.AppearanceCell.Options.UseFont = true;
            this.gridColumn29.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn29.AppearanceHeader.Options.UseFont = true;
            this.gridColumn29.Caption = "FRM Plant Code";
            this.gridColumn29.FieldName = "CFCPlantCode";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 2;
            this.gridColumn29.Width = 110;
            // 
            // gridColumn25
            // 
            this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn25.AppearanceCell.Options.UseFont = true;
            this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn25.AppearanceHeader.Options.UseFont = true;
            this.gridColumn25.Caption = "FRM Plant Name";
            this.gridColumn25.FieldName = "CFCPlantName";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 3;
            this.gridColumn25.Width = 124;
            // 
            // gridColumn26
            // 
            this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn26.AppearanceCell.Options.UseFont = true;
            this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn26.AppearanceHeader.Options.UseFont = true;
            this.gridColumn26.Caption = "AIM Owner Location Name";
            this.gridColumn26.FieldName = "LocationName";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 4;
            this.gridColumn26.Width = 224;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colStatus.AppearanceCell.ForeColor = System.Drawing.Color.RoyalBlue;
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseForeColor = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.Caption = "Status";
            this.colStatus.ColumnEdit = this.repHyperStatus;
            this.colStatus.FieldName = "StatusDisplay";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 5;
            this.colStatus.Width = 242;
            // 
            // repHyperStatus
            // 
            this.repHyperStatus.AllowMouseWheel = false;
            this.repHyperStatus.AutoHeight = false;
            this.repHyperStatus.Name = "repHyperStatus";
            this.repHyperStatus.Click += new System.EventHandler(this.repHyperStatus_Click);
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(1047, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 113;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.DarkBlue;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.label14);
            this.panel11.Controls.Add(this.btnResize);
            this.panel11.Controls.Add(this.pnlTopBtns);
            this.panel11.Location = new System.Drawing.Point(2, 2);
            this.panel11.Margin = new System.Windows.Forms.Padding(2);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1088, 51);
            this.panel11.TabIndex = 170;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(406, 10);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(286, 23);
            this.label14.TabIndex = 149;
            this.label14.Text = "Formulation Cross Reference";
            // 
            // FormulationCrossReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1096, 577);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(9, 17);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormulationCrossReference";
            this.Text = "Formulation Cross Reference";
            this.Load += new System.EventHandler(this.FormulationCrossReference_Load);
            this.SizeChanged += new System.EventHandler(this.FormulationCrossReference_SizeChanged);
            this.pnlTopBtns.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.pageIngredient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdIngredientRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdIngredientRefView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteIngredient)).EndInit();
            this.gbCFC.ResumeLayout(false);
            this.gbCFC.PerformLayout();
            this.gbFrmFilterIngr.ResumeLayout(false);
            this.gbFrmFilterIngr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCNameIngredient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCCodeIngredient.Properties)).EndInit();
            this.gbAnalyzer.ResumeLayout(false);
            this.gbAnalyzer.PerformLayout();
            this.gbAimFilterIngr.ResumeLayout(false);
            this.gbAimFilterIngr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMNameMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMCodeMaterial.Properties)).EndInit();
            this.pageNutrient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNutrientRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNutrientRefView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCCodeNutrient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCNameNutrient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMCodeNutrient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMNameNutrient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteNutrient)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFrmFilterNut.ResumeLayout(false);
            this.gbFrmFilterNut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFrmNameNutrient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFrmCodeNutrient.Properties)).EndInit();
            this.gbAIM.ResumeLayout(false);
            this.gbAIM.PerformLayout();
            this.gbAimFilterNut.ResumeLayout(false);
            this.gbAimFilterNut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMNameNutrient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMCodeNutrient.Properties)).EndInit();
            this.pageOwner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReferencePlant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReferencePlantView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMLocationCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMLocationName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteOwner)).EndInit();
            this.grbAimOwner.ResumeLayout(false);
            this.grbAimOwner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerCode.Properties)).EndInit();
            this.gboCFCPlant.ResumeLayout(false);
            this.gboCFCPlant.PerformLayout();
            this.gbFrmFilterLoc.ResumeLayout(false);
            this.gbFrmFilterLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCPlantName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCPlantCode.Properties)).EndInit();
            this.gboAIMPlant.ResumeLayout(false);
            this.gboAIMPlant.PerformLayout();
            this.gbAimFilterLoc.ResumeLayout(false);
            this.gbAimFilterLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMLocationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMLocationCode.Properties)).EndInit();
            this.pageProduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCProductCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCProductName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMMaterialCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMMaterialName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteProduct)).EndInit();
            this.gboCFCProduct.ResumeLayout(false);
            this.gboCFCProduct.PerformLayout();
            this.gbFrmFilterProd.ResumeLayout(false);
            this.gbFrmFilterProd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProductCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePlant_Product.Properties)).EndInit();
            this.gboAnalyzerMaterial.ResumeLayout(false);
            this.gboAnalyzerMaterial.PerformLayout();
            this.gbAimFilterProd.ResumeLayout(false);
            this.gbAimFilterProd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwnerLocation_Material.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueOwner_Material.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialCode.Properties)).EndInit();
            this.tpProductsMissingXRef.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProdMissing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdMissingView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperStatus)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage pageIngredient;
		private System.Windows.Forms.TabPage pageNutrient;
		private System.Windows.Forms.TabPage pageOwner;
		private System.Windows.Forms.TabPage pageProduct;
		private System.Windows.Forms.GroupBox gbCFC;
		private DevExpress.XtraEditors.LookUpEdit lueCFCNameIngredient;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblCFCProductCode;
		private DevExpress.XtraEditors.LookUpEdit lueCFCCodeIngredient;
		private System.Windows.Forms.GroupBox gbAnalyzer;
		private DevExpress.XtraEditors.LookUpEdit lueAIMNameMaterial;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblAnalyzerMaterialCode;
		private DevExpress.XtraEditors.LookUpEdit lueAIMCodeMaterial;
		private System.Windows.Forms.Button btnPreviewIngredient;
		private System.Windows.Forms.Button btnAddIngredient;
		private DevExpress.XtraGrid.GridControl grdIngredientRef;
		private DevExpress.XtraGrid.Views.Grid.GridView grdIngredientRefView;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientRefAIMMaterialID;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCCode;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCName;
		private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMCode;
		private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMName;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteIngredient;
		private System.Windows.Forms.GroupBox groupBox1;
		private DevExpress.XtraEditors.LookUpEdit lueFrmNameNutrient;
		private System.Windows.Forms.Label lblFRMNutrientName;
		private System.Windows.Forms.Label lblFRMProductCode;
		private DevExpress.XtraEditors.LookUpEdit lueFrmCodeNutrient;
		private System.Windows.Forms.GroupBox gbAIM;
		private DevExpress.XtraEditors.LookUpEdit lueAIMNameNutrient;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private DevExpress.XtraEditors.LookUpEdit lueAIMCodeNutrient;
		private System.Windows.Forms.Button btnPreviewNutrient;
		private System.Windows.Forms.Button btnAddNutrient;
		private DevExpress.XtraGrid.GridControl grdNutrientRef;
		private DevExpress.XtraGrid.Views.Grid.GridView grdNutrientRefView;
		private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientRefAnalyzerNutrientID;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCNutrientCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCCodeNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCNutrientName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCNameNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUnit;
		private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMCodeNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMNameNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn colUnitType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteNutrient;
		private System.Windows.Forms.GroupBox grbAimOwner;
		private DevExpress.XtraEditors.LookUpEdit lueOwnerName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private DevExpress.XtraEditors.LookUpEdit lueOwnerCode;
		private System.Windows.Forms.GroupBox gboCFCPlant;
		private DevExpress.XtraEditors.LookUpEdit lueCFCPlantName;
		private System.Windows.Forms.Label lblCFCPlantName;
		private DevExpress.XtraEditors.LookUpEdit lueCFCPlantCode;
		private System.Windows.Forms.Label lblCFCPlantCode;
		private System.Windows.Forms.GroupBox gboAIMPlant;
		private DevExpress.XtraEditors.LookUpEdit lueAIMLocationName;
		private System.Windows.Forms.Label lblAIMPlantName;
		private System.Windows.Forms.Label lblAIMPlantCode;
		private DevExpress.XtraEditors.LookUpEdit lueAIMLocationCode;
		private System.Windows.Forms.Button btnPreviewOwner;
		private System.Windows.Forms.Button btnAddOwner;
		private DevExpress.XtraGrid.GridControl grdReferencePlant;
		private DevExpress.XtraGrid.Views.Grid.GridView grdReferencePlantView;
		private DevExpress.XtraGrid.Columns.GridColumn colPlantCrossReferenceID;
		private DevExpress.XtraGrid.Columns.GridColumn colCrossCFCPlantCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCPlantCode;
		private DevExpress.XtraGrid.Columns.GridColumn colCrossCFCPlantName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCPlantName;
		private DevExpress.XtraGrid.Columns.GridColumn colCrossAIMLocationCode;
		private DevExpress.XtraGrid.Columns.GridColumn colCrossAIMLocationName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn colCrossDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteOwner;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMLocationCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMLocationName;
		private System.Windows.Forms.GroupBox gboCFCProduct;
		private DevExpress.XtraEditors.LookUpEdit lueProductName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox gboAnalyzerMaterial;
		private System.Windows.Forms.Label label9;
		private DevExpress.XtraEditors.LookUpEdit lueMaterialName;
		private DevExpress.XtraEditors.LookUpEdit lueMaterialCode;
		private System.Windows.Forms.Label label10;
		private DevExpress.XtraEditors.LookUpEdit lueProductCode;
		private System.Windows.Forms.Button btnPreviewProduct;
		private System.Windows.Forms.Button btnAddProduct;
		private DevExpress.XtraGrid.GridControl grdProduct;
		private DevExpress.XtraGrid.Views.Grid.GridView grdProductView;
		private DevExpress.XtraGrid.Columns.GridColumn colProductCrossReferenceID;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCProductCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCProductCode;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCProductName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCProductName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMMaterialCode;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMMaterialName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteProduct;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.LookUpEdit luePlant_Product;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.LookUpEdit lueOwnerLocation_Material;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.LookUpEdit lueOwner_Material;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCPlantCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMOwnerLocationCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCPlantID;
        private DevExpress.XtraGrid.Columns.GridColumn AIMOwnerLocationID;
        private System.Windows.Forms.GroupBox gbFrmFilterIngr;
        private System.Windows.Forms.RadioButton rbFrmFilterMissingXrefIngr;
        private System.Windows.Forms.RadioButton rbFrmFilterWithXrefIngr;
        private System.Windows.Forms.GroupBox gbAimFilterIngr;
        private System.Windows.Forms.RadioButton rbAimFilterMissingXrefIngr;
        private System.Windows.Forms.RadioButton rbAimFilterWithXrefIngr;
        private System.Windows.Forms.GroupBox gbFrmFilterNut;
        private System.Windows.Forms.RadioButton rbFrmFilterMissingXrefNut;
        private System.Windows.Forms.RadioButton rbFrmFilterWithXrefNut;
        private System.Windows.Forms.GroupBox gbAimFilterNut;
        private System.Windows.Forms.RadioButton rbAimFilterMissingXrefNut;
        private System.Windows.Forms.RadioButton rbAimFilterWithXrefNut;
        private System.Windows.Forms.GroupBox gbFrmFilterLoc;
        private System.Windows.Forms.RadioButton rbFrmFilterMissingXrefLoc;
        private System.Windows.Forms.RadioButton rbFrmFilterWithXrefLoc;
        private System.Windows.Forms.GroupBox gbAimFilterLoc;
        private System.Windows.Forms.RadioButton rbAimFilterMissingXrefLoc;
        private System.Windows.Forms.RadioButton rbAimFilterWithXrefLoc;
        private DevExpress.XtraGrid.Columns.GridColumn colAimMaterialID;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
		private System.Windows.Forms.TabPage tpProductsMissingXRef;
        private System.Windows.Forms.GroupBox gbFrmFilterProd;
        private System.Windows.Forms.RadioButton rbFrmFilterMissingXrefProd;
        private System.Windows.Forms.RadioButton rbFrmFilterWithXrefProd;
        private System.Windows.Forms.GroupBox gbAimFilterProd;
        private System.Windows.Forms.RadioButton rbAimFilterMissingXrefProd;
        private System.Windows.Forms.RadioButton rbAimFilterWithXrefProd;
        private DevExpress.XtraGrid.GridControl grdProdMissing;
        private DevExpress.XtraGrid.Views.Grid.GridView grdProdMissingView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private System.Windows.Forms.Button btnUpdateXref;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repHyperStatus;
    }
}