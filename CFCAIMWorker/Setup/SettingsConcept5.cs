using CFCAIMCore.Entity;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CFCAIMWorker.Setup
{
    public partial class SettingsConcept5 : Form
    {
        private String _Concept5SettingsXML = Application.StartupPath + @"\" + "CFCAIMWorkerConcept5Settings.xml";

        private String BrowserFolder()
        {
            String startPath = @"C:\Foss\DirectExport";
            return BrowserFolder(startPath);
        }

        private String BrowserFolder(String startPath)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            String path = String.Empty;

            Thread t = new Thread((ThreadStart)(() =>
            {
                if (startPath.Length > 0)
                {
                    folderBrowserDialog.SelectedPath = startPath;
                }
                else
                {
                    folderBrowserDialog.SelectedPath = @"C:\Foss\DirectExport";
                }

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    path = folderBrowserDialog.SelectedPath;
                }
            }
            ));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return path;
        }

        public SettingsConcept5()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void SettingsConcept5_Load(object sender, EventArgs e)
        {
            SettingsConcept5Info settings = new SettingsConcept5Info();
            Library.LoadConcept5Settings(ref settings, _Concept5SettingsXML);
            if (settings.SettingsConcept5ID != -1)
            {
                txtID.Text = settings.SettingsConcept5ID.ToString();
            }
            txtFormulationPath.Text = settings.FormulationPath;
            txtDatabaseConnection.Text = settings.DatabaseConnection;
            lueSyncType.EditValue = settings.SyncTypeNum;
        }        

        private void btnSave_Click(object sender, EventArgs e)
        {
            SettingsConcept5Info settings = new SettingsConcept5Info();
            Int32 id = 0;

            if (txtID.Text.Length > 0)
            {
                txtID.Text = id.ToString();
                settings.SettingsConcept5ID = id;
                settings.FormulationPath = txtFormulationPath.Text;
                settings.DatabaseConnection = txtFormulationPath.Text;
                settings.SyncTypeNum = (Int32)lueSyncType.EditValue;                
                Library.SaveXmlConcept5Settings(ref settings, _Concept5SettingsXML);                
                MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowseFormulationPath_Click(object sender, EventArgs e)
        {
            String path = String.Empty;
            txtFormulationPath.Text = String.Empty;
            path = BrowserFolder(txtFormulationPath.Text);

            if (path.Length > 0)
            {
                txtFormulationPath.Text = path;
            }
        }
    }
}
