using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace CFCAIM
{
    class AnalysisSharedFunctions
    {
        public static List<AIMOwnerInfo> populateAimOwner(List<AIMOwnerInfo> aimOwner, Boolean isActive)
        {
            aimOwner = AIMManager.GetAIMOwner(isActive);
            aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--Select--"
                }
            );
            return aimOwner;
        }

        public static List<AIMOwnerLocationInfo> populateAimOwnerLocation(List<AIMOwnerLocationInfo> aimOwnerLocation, Boolean isActive)
        {
            aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
            aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--Select--"
                }
            );
            return aimOwnerLocation;
        }

        public static List<AIMSupplierInfo> populateAimSupplier(List<AIMSupplierInfo> aimSupplier)
        {
            aimSupplier = AIMManager.GetAllSupplier();
            aimSupplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    SupplierName = "--Select--"
                }
            );
            return aimSupplier;
        }

        public static List<AIMSupplierLocationInfo> populateAimSupplierLocation(List<AIMSupplierLocationInfo> aimSupplierLocation)
        {
            aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);
            aimSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    SupplierLocationName = "--Select--"
                }
            );
            return aimSupplierLocation;
        }

        public static List<AIMMaterialInfo> populateAimMaterial(List<AIMMaterialInfo> aimMaterial, Boolean isActive)
        {
            aimMaterial = AIMManager.GetAIMMaterial(isActive);
            aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--"
                }
            );
            return aimMaterial;
        }

        public static List<AIMNutrientInfo> populateAimNutrient(List<AIMNutrientInfo> aimNutrient, Boolean isActive)
        {
            aimNutrient = AIMManager.GetAIMNutrient(isActive);
            aimNutrient.Insert
            (
                0,
                new AIMNutrientInfo
                {
                    AIMNutrientID = 0,
                    AIMNutrientCode = "--Select--"
                }
            );
            return aimNutrient;
        }

        public static List<CFCPlantInfo> populateCFCPlant(List<CFCPlantInfo> cfcPlant, Boolean isActive, DevExpress.XtraEditors.LookUpEdit lue)
        {
            cfcPlant = AIMManager.GetCFCPlant(isActive);
            cfcPlant.Insert
            (
                0,
                new CFCPlantInfo
                {
                    CFCPlantID = 0,
                    CFCPlantName = "--Select--"
                }
            );
            lue.Properties.DataSource = cfcPlant;
            XtraEditors.LoadLookUpEdit(ref lue, "CFCPlantID", "CFCPlantName");
            lue.Properties.Columns[2].Caption = "FRM Plant Name";
            return cfcPlant;
        }

        public static List<CFCPlantRefAIMOwnerLocationInfo> populateCFCPlantCrossReference(List<CFCPlantRefAIMOwnerLocationInfo> aimCFCPlantCrossReference, DevExpress.XtraEditors.LookUpEdit lue)
        {
            aimCFCPlantCrossReference = AIMManager.GetAllCFCPlantAIMOwnerLocation();
            aimCFCPlantCrossReference.Insert
            (
                0,
                new CFCPlantRefAIMOwnerLocationInfo
                {
                    CFCPlantID = 0,
                    LocationName = "--Select--"
                }
            );
            lue.Properties.DataSource = aimCFCPlantCrossReference;
            XtraEditors.LoadLookUpEdit(ref lue, "CFCPlantID", "LocationName");
            return aimCFCPlantCrossReference;
        }

        //10/31/22
        //public static void populateViewTemplate(LookUpEdit lue, LookUpEdit lueInTab)
        public static void populateViewTemplate(LookUpEdit lue, LookUpEdit lueInTab, String comingFrom)
        {

            //10/31/22
            Int32 intGridNumber = 0;
            if (comingFrom == "P")
            {
                intGridNumber = (Int32)EnumGridNumber.AnalysisProduct;
            }
            else
            {
                intGridNumber = (Int32)EnumGridNumber.AnalysisIngredient;
            }


            List<AnalysisViewTemplateInfo> viewTemplate = new List<AnalysisViewTemplateInfo>();

            //10/31/22
            //viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, (Int32)EnumGridNumber.AnalysisIngredient, (Int32)EnumReportDisplayType.None);
            viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, intGridNumber, (Int32)EnumReportDisplayType.None);

            viewTemplate.Insert
                (
                    0,
                    new AnalysisViewTemplateInfo
                    {
                        AnalysisViewTemplateID = 0,
                        AnalysisViewTemplateName = "--Select--"
                    }
                );
            
            lue.Properties.DataSource = viewTemplate;
            XtraEditors.LoadLookUpEdit(ref lue, "AnalysisViewTemplateID", "AnalysisViewTemplateName");
            lueInTab.Properties.DataSource = viewTemplate;
            XtraEditors.LoadLookUpEdit(ref lueInTab, "AnalysisViewTemplateID", "AnalysisViewTemplateName");
        }

        //10/31/22
        //public static void populateQueryTemplate(LookUpEdit lue, LookUpEdit lueInTab)
        public static void populateQueryTemplate(LookUpEdit lue, LookUpEdit lueInTab, String comingFrom)
        {

            List<QueryTemplateInfo> queryTemplate = new List<QueryTemplateInfo>();

            //10/31/22
            //queryTemplate = AIMManager.GetQueryTemplate();
            queryTemplate = AIMManager.GetQueryTemplate(comingFrom);

            queryTemplate.Insert
            (
                0,
                new QueryTemplateInfo
                {
                    QueryTemplateID = 0,
                    Name = "--None--",
                    IsAnalysis = true
                }
            );

            lue.Properties.DataSource = queryTemplate;
            XtraEditors.LoadLookUpEdit(ref lue, "QueryTemplateID", "Name", "Created");
            lue.Properties.Columns[4].Caption = "";
            lueInTab.Properties.DataSource = queryTemplate;
            XtraEditors.LoadLookUpEdit(ref lueInTab, "QueryTemplateID", "Name", "Created");

            lueInTab.Properties.Columns[4].Caption = "";           
            lueInTab.Properties.PopupWidth = 350;
            lueInTab.Properties.Columns[0].Width = 150;
            lueInTab.Properties.Columns[1].Width = 200;

        }

        public static void popOperatorLue(LookUpEdit lue)
        {
            //SAMPLE DATE -----------------------------------------------------------------
            List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
            queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);
            queryOperation.Insert
            (
                0,
                new QueryOperationInfo
                {
                    QueryOperationID = 0,
                    Operation = "--Select--"
                }
            );
            lue.Properties.DataSource = queryOperation;
            XtraEditors.LoadLookUpEdit(ref lue, "QueryOperationID", "Operation");
            lue.EditValue = 0;

        }

        public static void popMaterialNutrientLue(LookUpEdit lueCode, LookUpEdit lueName, Boolean isActive)
        {
            //MATERIAL/NUTRIENT ----------------------------------------------------------            
            List<AIMMaterialInfo> AimMaterial = new List<AIMMaterialInfo>();
            AimMaterial = AIMManager.GetAIMMaterial(isActive);
            AimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--"
                }
            );
            lueCode.Properties.DataSource = AimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
            lueName.Properties.DataSource = AimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

        }

        public static void popNutrientLueAndReplue(LookUpEdit lueCode, LookUpEdit lueName,
            RepositoryItemLookUpEdit replueCode,
            RepositoryItemLookUpEdit replueName)
        {

            //NUTRIENT --------------------------------------------------------------------
            List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
            aimNutrient = AIMManager.GetAllAIMNutrient(true);
            aimNutrient.Insert
            (
                0,
                new AIMNutrientInfo
                {
                    AIMNutrientID = 0,
                    AIMNutrientCode = "--Select--",
                    AIMNutrientName = "--Select--"
                }
            );
            lueCode.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
            lueName.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

            replueCode.DataSource = aimNutrient;
            XtraEditors.LoadGridLookUpEdit(ref replueCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
            replueName.DataSource = aimNutrient;
            XtraEditors.LoadGridLookUpEdit(ref replueName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");
        }

        public static void popOwnerLue(
            LookUpEdit lueOwnerCode,
            LookUpEdit lueOwnerName,
            LookUpEdit lueOwnerCode_Loc,
            LookUpEdit lueOwnerName_Loc,

            //10/17/23
            Int32 ownerLocationGroupID
            
            )
        {
            //OWNER -----------------------------------------------------------------------
            List<AIMOwnerInfo> AimOwner = new List<AIMOwnerInfo>();

            //10/17/23
            //AimOwner = AIMManager.GetAIMOwner(true);
            AimOwner = AIMManager.GetAIMOwner(ownerLocationGroupID,true);

            AimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--Select--",
                    OwnerName = "--Select--"
                }
            );
            lueOwnerCode.Properties.DataSource = AimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode", "OwnerName");
            lueOwnerName.Properties.DataSource = AimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName", "OwnerCode");                      

            lueOwnerCode_Loc.Properties.DataSource = AimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerCode_Loc, "AIMOwnerID", "OwnerCode", "OwnerName");
            lueOwnerCode_Loc.Properties.Columns["OwnerCode"].Visible = true;           

            lueOwnerName_Loc.Properties.DataSource = AimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerName_Loc, "AIMOwnerID", "OwnerName", "OwnerCode");
            lueOwnerName_Loc.Properties.Columns["OwnerName"].Visible = true;

            lueOwnerCode.EditValue = 0;
            lueOwnerCode_Loc.EditValue = 0;
            if (AimOwner.Count == 2)
            {
                //selecting value if there is only one
                lueOwnerCode.EditValue = AimOwner[1].AIMOwnerID;
                lueOwnerCode_Loc.EditValue = AimOwner[1].AIMOwnerID;

                //7/27/22 - added                
                lueOwnerName.EditValue = AimOwner[1].AIMOwnerID;
                lueOwnerName_Loc.EditValue = AimOwner[1].AIMOwnerID;

            }

        }

        public static void popSupplierLue(
            LookUpEdit lueSupplierCode,
            LookUpEdit lueSupplierName,
            LookUpEdit lueSupplierCode_Loc,
            LookUpEdit lueSupplierName_Loc,
            
            //10/17/23
            Int32 supplierGroupID

            )
        {
            //SUPPLIER --------------------------------------------------------------------
            List<AIMSupplierInfo> AimSupplier = new List<AIMSupplierInfo>();

            //10/17/23
            //AimSupplier = AIMManager.GetSupplier(true);
            AimSupplier = AIMManager.GetSupplier(true, supplierGroupID);

            AimSupplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    AIMSupplierCode = "--Select--",
                    SupplierName = "--Select--"
                }
            );

            lueSupplierCode.Properties.DataSource = AimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierCode, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplierCode.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplierName.Properties.DataSource = AimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
            lueSupplierName.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplierCode_Loc.Properties.DataSource = AimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierCode_Loc, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplierCode_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplierName_Loc.Properties.DataSource = AimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierName_Loc, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
            lueSupplierName_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";
        }

        public static void popSampleTypeLue(LookUpEdit lue, Boolean isActive)
        {
            //1/25/21
            //SAMPLE TYPE --------------------------------------------------------------
            List<SampleTypeInfo> sampleType = new List<SampleTypeInfo>();
            sampleType = AIMManager.GetSampleType(isActive);
            sampleType.Insert
            (
                0,
                new SampleTypeInfo
                {
                    SampleTypeID = 0,
                    SampleTypeName = "--Select--"
                }
            );
            lue.Properties.DataSource = sampleType;
            XtraEditors.LoadLookUpEdit(ref lue, "SampleTypeID", "SampleTypeName");
        }

        //12/21/22
        public static void popProductTypeLue(LookUpEdit lue, Int32 aimDatabaseID)
        {
            //PRODUCT TYPE --------------------------------------------------------------
            List<CFCProductTypeInfo> sampleType = new List<CFCProductTypeInfo>();
            sampleType = AIMManager.GetCFCProductType(aimDatabaseID);
            sampleType.Insert
            (
                0,
                new CFCProductTypeInfo
                {
                    CFCProductTypeID = 0,
                    ProductTypeName = "--Select--"
                }
            );
            lue.Properties.DataSource = sampleType;
            XtraEditors.LoadLookUpEdit(ref lue, "CFCProductTypeID", "ProductTypeName");
        }

        public static void DataBindOwnerLocationLue(
            LookUpEdit lueOwnerName_Loc,
            LookUpEdit lueOwnerLocationName,
            LookUpEdit lueOwnerLocationCode,
            
            //10/17/23
            Int32 ownerLocationGroupID
            )
        {
            //OWNER LOCATION -------------------------------------------------------------
            Int32 owner = 0;

            List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
            if (lueOwnerName_Loc.EditValue != null)
            {
                Int32.TryParse(lueOwnerName_Loc.EditValue.ToString(), out owner);
            }
            
            //10/17/23
            //aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner);
            aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner, ownerLocationGroupID);

            aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--Select--",
                    LocationName = "--Select--"
                }
            );

            lueOwnerLocationName.Properties.DataSource = aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");
            lueOwnerLocationName.Properties.Columns["LocationName"].Visible = true;


            lueOwnerLocationCode.Properties.DataSource = aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationCode, "AIMOwnerLocationID", "LocationCode", "LocationName");
            lueOwnerLocationCode.Properties.Columns["LocationCode"].Visible = true;

            if (aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one                
                lueOwnerLocationCode.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueOwnerLocationCode.EditValue = 0;
            }
        }

        public static void DataBindSupplierLocationLue(
            LookUpEdit lueSupplierName_Loc,
            LookUpEdit lueSupplierLocationCode,
            LookUpEdit lueSupplierLocationName, 
            
            //10/17/23
            Int32 supplierGroupID,
            Int32 supplierLocationGroupID
            )
        {
            //SUPPLIER LOCATION -------------------------------------------------------------
            Int32 supplier = 0;

            List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
            if (lueSupplierName_Loc.EditValue != null)
            {
                Int32.TryParse(lueSupplierName_Loc.EditValue.ToString(), out supplier);
            }

            //10/17/23
            //aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
            aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier, supplierGroupID, supplierLocationGroupID);

            aimSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--",
                    SupplierLocationName = "--Select--"
                }
            );

            lueSupplierLocationCode.Properties.DataSource = aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocationCode, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueSupplierLocationCode.Properties.Columns["SupplierLocationName"].Caption = "AIM Location Name";

            lueSupplierLocationName.Properties.DataSource = aimSupplierLocation;

            //2/16/21
            //XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");

            lueSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Location Name";

            if (aimSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSupplierLocationName.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSupplierLocationName.EditValue = aimSupplierLocation;
            }
        }

        //3/2/21 - changed this back to separate functions for Ingredient and Product
        ////public static void DataBindSummary(Boolean blnIngredient,
        ////    DevExpress.XtraGrid.Views.Base.ColumnView grd,
        ////    TextBox txtAimMaterialID,
        ////    TextBox txtNutrientID,
        ////    TextBox txtNutrientCode,
        ////    TextBox txtNutrientName,
        ////    TextBox txtCurrentValue,
        ////    TextBox txtProposeNutrientValue,
        ////    TextBox txtDifference,
        ////    TextBox txtMean,
        ////    TextBox txtStandardDeviation,
        ////    TextBox txtSafetyMargin,
        ////    TextBox txtNumberOfSamples,
        ////    TextBox txtTrimmedSampleSize,
        ////    TextBox txtTotalSampleSize,

        ////    TextBox txtIngredientID,                    //For Product -- txtProductID
        ////    TextBox txtIngredientCode,                  //For Product -- txtProductCode
        ////    TextBox txtCFCIngredientName,               //For Product -- txtProductName

        ////    TextBox txtFrmPlant,
        ////    GridColumn colAIMMaterialID,
        ////    GridColumn colAIMNutrientID,
        ////    GridColumn colCFCNutrientCode,
        ////    GridColumn colCFCNutrientName,
        ////    GridColumn colCurrentNutrientVal,
        ////    GridColumn colProposedNutrientValue,
        ////    GridColumn colDifference,
        ////    GridColumn colTrimmedMean,
        ////    GridColumn colTrimmedStandardDeviation,
        ////    GridColumn colSafetyMargin,
        ////    GridColumn colNumberOfSample,
        ////    GridColumn colTrimmedSampleSize,
        ////    GridColumn colTotalSampleSize,
        ////    GridColumn colCFCIngredientName,
        ////    GridColumn colPlantCode

        ////    )
        ////{

        ////    try
        ////    {
        ////        if (blnIngredient == true)
        ////        {
        ////            txtCFCIngredientName.Text = (String)grd.GetFocusedRowCellValue(colCFCIngredientName);
        ////            txtFrmPlant.Text = (String)grd.GetFocusedRowCellValue(colPlantCode);
        ////            if (grd.GetFocusedRowCellValue(colAIMMaterialID) == null)
        ////            {
        ////                txtIngredientID.Text = "";
        ////            }
        ////            else
        ////            {
        ////                txtIngredientID.Text = grd.GetFocusedRowCellValue(colAIMMaterialID).ToString();
        ////            }
        ////            if (txtIngredientCode.Text == "")
        ////            {
        ////                txtIngredientCode.Text = "No XRef";
        ////                txtIngredientID.Text = "0";
        ////            }
        ////            if (txtCFCIngredientName.Text == "")
        ////            {
        ////                txtCFCIngredientName.Text = "No XRef";
        ////            }                    
        ////        }

        ////        //3/2/21
        ////        else
        ////        {
        ////            txtCFCIngredientName.Text = (String)grd.GetFocusedRowCellValue(colCFCIngredientName);
        ////            txtFrmPlant.Text = (String)grd.GetFocusedRowCellValue(colPlantCode);
        ////            if (grd.GetFocusedRowCellValue(colAIMMaterialID) == null)
        ////            {
        ////                txtIngredientID.Text = "";
        ////            }
        ////            else
        ////            {
        ////                txtIngredientID.Text = grd.GetFocusedRowCellValue(colAIMMaterialID).ToString();
        ////            }
        ////            if (txtIngredientCode.Text == "")
        ////            {
        ////                txtIngredientCode.Text = "No XRef";
        ////                txtIngredientID.Text = "0";
        ////            }
        ////            if (txtCFCIngredientName.Text == "")
        ////            {
        ////                txtCFCIngredientName.Text = "No XRef";
        ////            }
        ////        }

        ////        txtNutrientCode.Text = (String)grd.GetFocusedRowCellValue(colCFCNutrientCode);
        ////        txtNutrientName.Text = (String)grd.GetFocusedRowCellValue(colCFCNutrientName);

        ////        if (grd.GetFocusedRowCellValue(colAIMNutrientID) == null)
        ////        {
        ////            txtNutrientID.Text = "";
        ////        }
        ////        else
        ////        {
        ////            txtNutrientID.Text = grd.GetFocusedRowCellValue(colAIMNutrientID).ToString();
        ////        }

        ////        //10/20/20 - added for charts; ingredientid gets changed below (not sure why), so added separate column for Aim ID
        ////        txtAimMaterialID.Text = String.Empty;

        ////        if (grd.GetFocusedRowCellValue(colAIMMaterialID) == null)
        ////        {
        ////            txtAimMaterialID.Text = "";
        ////        }
        ////        else
        ////        {
        ////            txtAimMaterialID.Text = grd.GetFocusedRowCellValue(colAIMMaterialID).ToString();
        ////        }

        ////        if (txtNutrientCode.Text == "")
        ////        {
        ////            txtNutrientCode.Text = "No XRef";
        ////            txtNutrientID.Text = "0";
        ////        }
        ////        if (txtNutrientName.Text == "")
        ////        {
        ////            txtNutrientName.Text = "No XRef";
        ////        }


        ////        txtCurrentValue.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colCurrentNutrientVal)).ToString("N3");
        ////        txtProposeNutrientValue.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colProposedNutrientValue)).ToString("N3");
        ////        txtDifference.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colDifference)).ToString("N3");
        ////        txtMean.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colTrimmedMean)).ToString("N3");
        ////        txtStandardDeviation.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colTrimmedStandardDeviation)).ToString("N3");
        ////        txtSafetyMargin.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colSafetyMargin)).ToString("N3");
        ////        txtNumberOfSamples.Text = Convert.ToDecimal(grd.GetFocusedRowCellValue(colNumberOfSample)).ToString();
        ////        txtTrimmedSampleSize.Text = Convert.ToInt32(grd.GetFocusedRowCellValue(colTrimmedSampleSize)).ToString();
        ////        txtTotalSampleSize.Text = Convert.ToInt32(grd.GetFocusedRowCellValue(colTotalSampleSize)).ToString();


        ////    }
        ////    catch (Exception)
        ////    {
        ////        txtIngredientCode.Text = String.Empty;
        ////        txtCFCIngredientName.Text = String.Empty;
        ////        txtNutrientCode.Text = String.Empty;
        ////        txtNutrientName.Text = String.Empty;
        ////        txtCurrentValue.Text = String.Empty;
        ////        txtProposeNutrientValue.Text = String.Empty;
        ////        txtDifference.Text = String.Empty;
        ////        txtMean.Text = String.Empty;
        ////        txtStandardDeviation.Text = String.Empty;
        ////        txtSafetyMargin.Text = String.Empty;
        ////        txtNumberOfSamples.Text = String.Empty;
        ////        txtTrimmedSampleSize.Text = String.Empty;
        ////        txtTotalSampleSize.Text = String.Empty;

        ////    }

        ////}

       //10/26/21 - GetDateDuration replaces ValidateDates function below (because of resize issues)
        public static void GetDateDuration(TextBox txtDuration, DateTime startDate, DateTime endDate)
        {
            AIMGlobal.blnChangeAnalysisDate = false;
            if (startDate.Year < 1980)
            {
                //10/26/21 - Using "System.Windows.MessageBox" instead of "System.Windows.Forms.MessageBox" caused resize issues for select customers
                //System.Windows.MessageBox.Show("Start date cannot be earlier than 1980. We are changing it for you.", "Warning...");
                MessageBox.Show("Start date cannot be earlier than 1980. We are changing it for you.", "Warning...");

                AIMGlobal.blnChangeAnalysisDate = true;            
            }            
            txtDuration.Text = "Duration:        " + ((endDate - startDate).Days + 1).ToString() + " Days";
        }

        //10/26/21 - replated with GetDateDuration function above (because of resize issues)
        //public static void ValidateDates(TextBox txtDuration, DateTimePicker dtpStart, DateTimePicker dtpEnd)               
        //{           
        //    DateTime startDate = dtpStart.Value;
        //    DateTime endDate = dtpEnd.Value;
        //    if (startDate.Year < 1980)
        //    {            
        //        System.Windows.MessageBox.Show("Start date cannot be earlier than 1980. We are changing it for you.", "Warning...");
        //        startDate = Convert.ToDateTime("1/1/1980");        
        //        dtpStart.Value = startDate;                
        //    }                       
        //    txtDuration.Text = "Duration:        " + ((endDate - startDate).Days + 1).ToString() + " Days";            
        //}

        public static void CheckYellowBox(Label lblYellowBox, TextBox txtYellowBox, LookUpEdit lue)
        {
            //If template detail exists, show yellow box help text	   
            lblYellowBox.Visible = false;
            txtYellowBox.Visible = false;
            ErrorInfo countField = new ErrorInfo();
            countField = AIMManager.CountQueryTemplateTotal((Int32)lue.EditValue);
            if (countField.RecordID > 0)
            {
                lblYellowBox.Visible = true;
                txtYellowBox.Visible = true;
            }
        }

        public static void SetDateTextField(TextBox txtDateOption, DateTimePicker dtpValue, DateTimePicker dtpSecondValue, QueryOperationEnum queryOperation, Int32 days)
        {
            switch (queryOperation)
            {
                case QueryOperationEnum.PastNDaysToDate:
                    txtDateOption.Text = days + " Days Past Current Date";
                    break;
                case QueryOperationEnum.MonthToDate:
                    txtDateOption.Text = "Month-To-Date";
                    break;
                case QueryOperationEnum.YearToDate:
                    txtDateOption.Text = "Year-To-Date";
                    break;
                case QueryOperationEnum.Equal:
                    txtDateOption.Text = "Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.GreaterThan:
                    txtDateOption.Text = "Greater Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.LessThan:
                    txtDateOption.Text = "Less Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.GreaterThanEqual:
                    txtDateOption.Text = "Greater Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.LessThanEqual:
                    txtDateOption.Text = "Less Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.NotEqual:
                    txtDateOption.Text = "Not Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
                    break;
                case QueryOperationEnum.Between:
                    txtDateOption.Text = queryOperation.ToString() + " " + dtpValue.Value.Date.ToString("MM/dd/yyyy") + " and " + dtpSecondValue.Value.Date.ToString("MM/dd/yyyy");
                    break;

                default:
                    txtDateOption.Text = queryOperation.ToString();

                    break;
            }
        }

        public static void DataBindBox(GridView view, RadioButton rbInclude, RadioButton rbExclude, String type)
        {
            if (view.DataRowCount != 0)
            {

                rbInclude.Checked = true;
                Int32 queryOperationID = (Int32)QueryOperationEnum.Equal;

                switch (type)
                {
                    case "Material":
                        queryOperationID = ((List<AIMMaterialInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "Nutrient":
                        queryOperationID = ((List<AIMNutrientInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "Owner":
                        queryOperationID = ((List<AIMOwnerInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "OwnerLoc":
                        queryOperationID = ((List<AIMOwnerLocationInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "Supplier":
                        queryOperationID = ((List<AIMSupplierInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "SupplierLoc":
                        queryOperationID = ((List<AIMSupplierLocationInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "MaterialNutrient":
                        queryOperationID = ((List<TemplateMaterialNutrientInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                    case "Ingredient":
                        queryOperationID = ((List<CFCIngredientInfo>)view.DataSource)[0].QueryOperationID;
                        break;
                }

                if (queryOperationID == (Int32)QueryOperationEnum.NotEqual)
                {
                    rbExclude.Checked = true;
                }
            }

        }

        //10/31/22 - commented; duplicate function, called PopulateQueryTemplate, is the one used
        ////public static void DataBindTemplateDropdowns(LookUpEdit lue, LookUpEdit lueInTab)
        ////{
        ////    List<QueryTemplateInfo> queryTemplate = new List<QueryTemplateInfo>();
        ////    queryTemplate = AIMManager.GetQueryTemplate();
        ////    queryTemplate.Insert
        ////    (
        ////        0,
        ////        new QueryTemplateInfo
        ////        {
        ////            QueryTemplateID = 0,
        ////            Name = "--None--",
        ////            IsAnalysis = true

        ////        }
        ////    );

        ////    lue.Properties.DataSource = queryTemplate;
        ////    XtraEditors.LoadLookUpEdit(ref lue, "QueryTemplateID", "Name", "Created");
        ////    lue.Properties.Columns[4].Caption = "";

        ////    lueInTab.Properties.DataSource = queryTemplate;
        ////    XtraEditors.LoadLookUpEdit(ref lueInTab, "QueryTemplateID", "Name", "Created");
        ////    lueInTab.Properties.Columns[4].Caption = "";

        ////}

        public static void RemoveZeroAimMaterialNutrientRecords(DevExpress.XtraGrid.GridControl grd, Boolean isAnalysis, LookUpEdit lueInTab)
        {
            //loop thru grid - delete any records with AIMNutrientID = 0
            List<TemplateMaterialNutrientInfo> multi = new List<TemplateMaterialNutrientInfo>();
            multi = (List<TemplateMaterialNutrientInfo>)grd.DataSource;
            if (multi != null)
            {
                foreach (var item in multi)
                {
                    if (item.AIMNutrientID == 0)
                    {
                        DeleteQueryTemplateDetail(isAnalysis, lueInTab, item.QueryTemplateDetailID);
                    }
                }
            }
            grd.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
        }

        public static void DeleteQueryTemplateDetail(Boolean isAnalysis, LookUpEdit lueInTab, Int32 queryTemplateDetailID)
        {
            try
            {
                if (isAnalysis == false && (Int32)lueInTab.EditValue != 0)
                {
                    MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
                    return;
                }
                AIMManager.DeleteQueryTemplateDetail(queryTemplateDetailID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Int32 CreatedInReportsCheck(LookUpEdit lueInTab)
        {
            Int32 rtn = 0;

            QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueInTab.GetSelectedDataRow();
            if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
            {
                if (analysisTemplate.IsAnalysis == false && (Int32)lueInTab.EditValue != 0)
                {
                    MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
                    rtn = -1;
                }
            }
            return rtn;
        }

        public static Int32 ChecksBeforeAdding(String type, LookUpEdit lueInTab, LookUpEdit lue)
        {
            Int32 rtn = 0;

            if (CreatedInReportsCheck(lueInTab) != 0)
            {
                rtn = -1;
            }
            else
            {

                if ((Int32)lueInTab.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM " + type + ".", "Warning...");
                    if (type == "Supplier Location" || type == "Owner Location") { }
                    else
                    {
                        lueInTab.ShowPopup();
                    }
                    rtn = -1;
                }

                //2/23/21
                if ((Int32)lue.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM " + type + ".", "Warning...");
                    if (type == "Supplier Location" || type == "Owner Location") { }
                    else
                    {
                        lue.ShowPopup();
                    }
                    rtn = -1;
                }

                //Check to see if item has already been selected
                switch (type)
                {
                    case "Material":
                        List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
                        material = AIMManager.GetAIMMaterialForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.Material, (Int32)lue.EditValue);
                        if (material.Count != 0)
                        {
                            MessageBox.Show("This material has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;

                    case "Nutrient":
                        List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
                        nutrient = AIMManager.GetAIMNutrientForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode, (Int32)lue.EditValue);
                        if (nutrient.Count != 0)
                        {
                            MessageBox.Show("This nutrient has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;
                    case "Owner":
                        List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
                        owner = AIMManager.GetAIMOwnerForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.Owner, (Int32)lue.EditValue);
                        if (owner.Count != 0)
                        {
                            MessageBox.Show("This owner has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;
                    case "Owner Location":
                        List<AIMOwnerLocationInfo> ownerLocation = new List<AIMOwnerLocationInfo>();
                        ownerLocation = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation, (Int32)lue.EditValue);
                        if (ownerLocation.Count != 0)
                        {
                            MessageBox.Show("This owner location has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;
                    case "Supplier":
                        List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
                        supplier = AIMManager.GetAIMSupplierForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.Supplier, (Int32)lue.EditValue);
                        if (supplier.Count != 0)
                        {
                            MessageBox.Show("This supplier has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;
                    case "Supplier Location":
                        List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
                        supplierLocation = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation, (Int32)lue.EditValue);
                        if (supplierLocation.Count != 0)
                        {
                            MessageBox.Show("This supplier location has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;

                    case "Ingredient":
                        List<CFCIngredientInfo> ingredient = new List<CFCIngredientInfo>();
                        ingredient = AIMManager.GetCFCIngredientForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode, (Int32)lue.EditValue);
                        if (ingredient.Count != 0)
                        {
                            MessageBox.Show("This ingredient has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;

                    //1/25/21
                    case "SampleType":
                        List<SampleTypeInfo> sampleType = new List<SampleTypeInfo>();
                        sampleType = AIMManager.GetSampleTypeForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.SampleType, (Int32)lue.EditValue);
                        if (sampleType.Count != 0)
                        {
                            MessageBox.Show("This sample type has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;

                    //12/21/22
                    case "ProductType":
                        List<CFCProductTypeInfo> productType = new List<CFCProductTypeInfo>();
                        productType = AIMManager.GetCFCProductTypeForTemplate((Int32)lueInTab.EditValue, (Int32)QueryFeildEnum.ProductType, (Int32)lue.EditValue);
                        if (productType.Count != 0)
                        {
                            MessageBox.Show("This product type has been selected.", "Information...");
                            rtn = -1;
                        }
                        break;

                    default:
                        break;
                }
            }
            return rtn;

        }

        public static void SyncValueLue(LookUpEdit from, LookUpEdit to)
        {
            if (from == null || to == null)
            {
                return;
            }
            if (to.EditValue == from.EditValue)
            {
                return;
            }
            to.EditValue = from.EditValue;

        }

        public static void SaveQueryTemplateDetail(Int32 FieldID, LookUpEdit lueInTab, RadioButton rb, LookUpEdit lue, Int32 cfcUserID)
        {
            QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
            queryTemplateDetail.QueryTemplateDetailID = 0;
            queryTemplateDetail.QueryTemplateID = (Int32)lueInTab.EditValue;
            queryTemplateDetail.QueryFieldID = FieldID;
            queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
            queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
            if (rb.Checked == true)
            {
                //If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0                    
                queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
            }

            queryTemplateDetail.Value = (Int32)lue.EditValue;
            queryTemplateDetail.CFCUserID = cfcUserID;
            AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
        }

        //10/9/23
        public static void SaveQueryTemplateDetailValue(Int32 FieldID, LookUpEdit lueInTab, RadioButton rb, Int32 value, Int32 cfcUserID)
        {
            QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
            queryTemplateDetail.QueryTemplateDetailID = 0;
            queryTemplateDetail.QueryTemplateID = (Int32)lueInTab.EditValue;
            queryTemplateDetail.QueryFieldID = FieldID;
            queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
            queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
            if (rb.Checked == true)
            {
                //If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0                    
                queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
            }

            queryTemplateDetail.Value = value;
            queryTemplateDetail.CFCUserID = cfcUserID;
            AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
        }

    }
}
