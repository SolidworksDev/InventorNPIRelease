using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web.Services;
using System.Runtime.InteropServices;
using InventorApprentice;
using Inventor;
using EPDM.Interop.epdm;
using EPDM.Interop.EPDMResultCode;
using VDF = Autodesk.DataManagement.Client.Framework;
using ACW = Autodesk.Connectivity.WebServices;


namespace Inventor_NPI_Release
{
    public partial class InventorRelease : Form
    {
        public VDF.Vault.Currency.Connections.Connection vaultConnection = null;
        private InventorApprentice.ApprenticeServerComponent apprentice;
        private InventorApprentice.ApprenticeServerDocument assemblyDoc;
        private List<string> selectedDrawings = new List<string>();
        private List<ReferenceDoc> referenceDocs = new List<ReferenceDoc>();
        private VDF.Vault.Results.AcquireFilesResults FilesResults = null;
        private string assemblyPath = "";
        private string topAssemblyName = "";
        private string PDFfilePath = "C:\\Production Vault\\DEVELOPMENT\\T.Clift\\PDFDrawings\\";
        private ACW.File[] drawingFiles = { };
        private Inventor.Application invApp;
        private Inventor.TranslatorAddIn pdfAddin;

        private VDF.Vault.Forms.Models.BrowseVaultNavigationModel m_model = null;
        private List<VDF.Forms.Controls.GridLayout> m_availableLayouts = new List<VDF.Forms.Controls.GridLayout>();
        private List<ToolStripMenuItem> m_viewButtons = new List<ToolStripMenuItem>();
        private Func<VDF.Vault.Currency.Entities.IEntity, bool> m_filterCanDisplayEntity;
        private VDF.Vault.Currency.Entities.FileIteration selectedFile;

        private EdmVault5 vault5;
        private IEdmVault7 vault7;
        private Boolean validUser;

        public InventorRelease()

        {
            InitializeComponent();
            btnOK.Enabled = false;
            VDF.Vault.Forms.Settings.SelectEntitySettings.EntityFilter initialFilter = new VDF.Vault.Forms.Settings.SelectEntitySettings.EntityRegularExpressionFilter("Assembly Files (*.iam)", ".+iam", VDF.Vault.Currency.Entities.EntityClassIds.Files);
            m_filterCanDisplayEntity = initialFilter.CanDisplayEntity;
            apprentice = new InventorApprentice.ApprenticeServerComponent();
            LoginToVault();
            LoginToEPDM();
            fileName_multiPartTextBox.EditMode = VDF.Forms.Controls.MultiPartTextBoxControl.EditModeOption.ReadOnly;
            initalizeGrid();
        }

        private void InventorRelease_Shown(object sender, EventArgs e)
        {
            //save each available layout of the browser control as well as generate a button to use in the switch view dropdown
            foreach (VDF.Forms.Controls.GridLayout layout in vaultBrowserControl.AvailableLayouts)
            {
                m_availableLayouts.Add(layout);
                ToolStripMenuItem item = new ToolStripMenuItem(layout.Name);
                item.Tag = layout;
                item.CheckOnClick = true;
                item.Click += new EventHandler(switchViewDropdown_itemClick);
                switchView_toolStripSplitButton.DropDownItems.Add(item);
                m_viewButtons.Add(item);
            }

            vaultConnection = VDF.Vault.Forms.Library.Login(null);
            controlStates(vaultConnection != null);
            if (vaultConnection != null)
                initalizeGrid();
        }

        private void InventorRelease_FormClosed(object sender, FormClosedEventArgs e)
        {
            //we need to be sure to release all our connections when the app closes
            VDF.Vault.Library.ConnectionManager.CloseAllConnections();
        }

        void m_model_SelectedContentChanged(object sender, VDF.Vault.Forms.Currency.SelectionChangedEventArgs e)
        {
            //when the selected content changes, we need to update the filename field to reflect the selected entities
            List<VDF.Vault.Currency.Entities.IEntity> selectedEntities = new List<VDF.Vault.Currency.Entities.IEntity>(e.SelectedEntities);

            bool fileSelected = false;
            List<string> selectedEntityNames = new List<string>();
            foreach (VDF.Vault.Currency.Entities.IEntity entity in selectedEntities)
            {
                if (entity is VDF.Vault.Currency.Entities.FileIteration)
                    fileSelected = true;
                selectedEntityNames.Add(entity.EntityName);
            }
            fileName_multiPartTextBox.Parts = selectedEntityNames;

            drawingListView.Clear();
            referenceDocs.Clear();
            Array.Clear(drawingFiles, 0, drawingFiles.Length);
            btnOK.Enabled = false;

            UpdateAssociationsTreeView();
        }

        void m_model_ParentChanged(object sender, EventArgs e)
        {
            navigateBack_toolStripButton.Enabled = m_model.CanMoveBack;
            navigateUp_toolStripButton.Enabled = m_model.CanMoveUp;
        }

        /// <summary>
        /// List all children for a file.
        /// </summary>
        private void UpdateAssociationsTreeView()
        {

            selectedFile = m_model.SelectedContent.FirstOrDefault() as VDF.Vault.Currency.Entities.FileIteration;
            if (selectedFile == null)
                return;

            OpenFile();
        }

        private void LoginToVault()
        {

            VDF.Vault.Forms.Library.Initialize();
            Autodesk.DataManagement.Client.Framework.Vault.Results.LogInResult loginResult = Autodesk.DataManagement.Client.Framework.Vault.Library.ConnectionManager.LogIn
            ("svr19", "Anthro_Vault", "cliftt", "1234", VDF.Vault.Currency.Connections.AuthenticationFlags.ReadOnly, null);
            if (loginResult.Success)
            {
                vaultConnection = loginResult.Connection;
            }
        }

        private void LoginToEPDM()
        {

            if ( vault5 == null )
            {
                vault5 = new EdmVault5();
                vault7 = (IEdmVault7)vault5;
            }

            if (!vault5.IsLoggedIn)
            {
                try
                {
                    vault5.LoginAuto("Production Vault", this.Handle.ToInt32());
                }
                catch
                {
                    MessageBox.Show("The Production Vault is Currently Unavailable");
                    return;
                }
            }

            validUser = CheckUser();

        }

        private void initalizeGrid()
        {
            VDF.Vault.Currency.Properties.PropertyDefinitionDictionary propDefs = vaultConnection.PropertyManager.GetPropertyDefinitions(null, null, VDF.Vault.Currency.Properties.PropertyDefinitionFilter.IncludeAll);

            VDF.Vault.Forms.Controls.VaultBrowserControl.Configuration initialConfig = new VDF.Vault.Forms.Controls.VaultBrowserControl.Configuration(vaultConnection, "VaultBrowserSample", propDefs);

            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Client.EntityIcon);
            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Client.VaultStatus);
            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.EntityName);
            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.CheckInDate);
            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.Comment);
            initialConfig.AddInitialColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.ThumbnailSystem);
            initialConfig.AddInitialSortCriteria(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.EntityName, true);

            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Client.EntityIcon);
            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Client.VaultStatus);
            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.EntityName);
            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.CheckInDate);
            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.Comment);
            initialConfig.AddInitialQuickListColumn(VDF.Vault.Currency.Properties.PropertyDefinitionIds.Server.ThumbnailSystem);

            m_model = new VDF.Vault.Forms.Models.BrowseVaultNavigationModel(vaultConnection, true, true);

            m_model.ParentChanged += new EventHandler(m_model_ParentChanged);
            m_model.SelectedContentChanged += new EventHandler<VDF.Vault.Forms.Currency.SelectionChangedEventArgs>(m_model_SelectedContentChanged);

            vaultBrowserControl.SetDataSource(initialConfig, m_model);
            vaultBrowserControl.OptionsCustomizations.CanDisplayEntityHandler = canDisplayEntity;
            vaultBrowserControl.OptionsBehavior.MultiSelect = false;
            vaultBrowserControl.OptionsBehavior.AllowOverrideSelections = false;

            vaultNavigationPathComboboxControl1.SetDataSource(vaultConnection, m_model, null);

            m_model.Navigate(vaultConnection.FolderManager.RootFolder, VDF.Vault.Forms.Currency.NavigationContext.NewContext);
        }

        private bool CheckUser()
        {

            IEdmUserMgr7 userMgr = default(IEdmUserMgr7);
            userMgr = (IEdmUserMgr7)vault7.CreateUtility(EdmUtility.EdmUtil_UserMgr);

            string userName = System.Environment.UserName;
            List<string> usersGroups = new List<string>();

            IEdmUser8 CurrentUser = (IEdmUser8)userMgr.GetUser(userName);
            object[] groups = default(object[]);

            CurrentUser.GetGroupMemberships(out groups);
            string message = "Current users: " + userName + "\n Does not have permission to use this tool";

            foreach (object g in groups)
            {
                IEdmUserGroup7 group = (IEdmUserGroup7)g;
                usersGroups.Add(group.Name);
            }

            if (!usersGroups.Contains("engineer"))
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private void OpenFile()
        {
            try
            {
                invApp = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                System.Type oType = System.Type.GetTypeFromProgID("Inventor.Application");
                invApp = (Inventor.Application)System.Activator.CreateInstance(oType);
                invApp.Visible = false;
            }

            try
            {
                GetFiles();

                drawingListView.Columns.Add("File Name");
                drawingListView.Columns.Add("Revision");
                drawingListView.Columns[0].Width = 250;
                drawingListView.Columns[1].Width = 150;
                drawingListView.BeginUpdate();

                foreach (ReferenceDoc rd in referenceDocs)
                {
                    ListViewItem lv = new ListViewItem(rd.DrawingName, 0);
                    lv.SubItems.Add(rd.Revision);
                    drawingListView.Items.Add(lv);
                    
                }

                drawingListView.EndUpdate();

                assemblyDoc = apprentice.Open(assemblyPath);
                btnOK.Enabled = true;

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetFiles()
        {
           
            topAssemblyName = selectedFile.EntityName;
            VDF.Vault.Settings.AcquireFilesSettings acquireFileSettings = new VDF.Vault.Settings.AcquireFilesSettings(vaultConnection);
            acquireFileSettings.AddFileToAcquire(selectedFile, acquireFileSettings.DefaultAcquisitionOption);
            acquireFileSettings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeChildren = true;
            acquireFileSettings.OptionsRelationshipGathering.FileRelationshipSettings.RecurseChildren = true;
            acquireFileSettings.DefaultAcquisitionOption = VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Download;
            FilesResults = vaultConnection.FileManager.AcquireFiles(acquireFileSettings);

            foreach (VDF.Vault.Results.FileAcquisitionResult fileResults in FilesResults.FileResults)
            {
                if (fileResults.File.EntityName == topAssemblyName)
                {
                    if (!InList(fileResults.File.EntityName, referenceDocs))
                    {
                        ReferenceDoc newDoc = new ReferenceDoc();
                        newDoc.PartName = fileResults.File.EntityName;
                        newDoc.PartPath = fileResults.LocalPath.ToString();
                        newDoc.DrawingName = System.IO.Path.ChangeExtension(fileResults.File.EntityName, ".idw");
                        newDoc.DrawingPath = System.IO.Path.ChangeExtension(fileResults.LocalPath.ToString(), ".idw");
                        newDoc.VaultDrawingPath = newDoc.DrawingPath.Replace("C:\\_Vault_Working_Folder", "$").Replace("\\","/");
                        referenceDocs.Add(newDoc);
                    }
                    VDF.Currency.FilePathAbsolute filePathAbs = fileResults.LocalPath;
                    assemblyPath = filePathAbs.ToString();
                }

                if (IsDoc(fileResults.File.EntityName) && fileResults.File.EntityName != topAssemblyName)
                {
                    if (!InList(fileResults.File.EntityName, referenceDocs))
                    {
                        ReferenceDoc newDoc = new ReferenceDoc();
                        newDoc.PartName = fileResults.File.EntityName;
                        newDoc.PartPath = fileResults.LocalPath.ToString();
                        newDoc.DrawingName = System.IO.Path.ChangeExtension(fileResults.File.EntityName, ".idw");
                        newDoc.DrawingPath = System.IO.Path.ChangeExtension(fileResults.LocalPath.ToString(), ".idw");
                        newDoc.VaultDrawingPath = newDoc.DrawingPath.Replace("C:\\_Vault_Working_Folder", "$").Replace("\\", "/");
                        referenceDocs.Add(newDoc);
                    }
                }
            }

            referenceDocs.Sort((x, y) => x.DrawingName.CompareTo(y.DrawingName));

            string[] vaultDrawingFiles = new string[referenceDocs.Count];
            int i = 0;

            foreach (ReferenceDoc rd in referenceDocs)
            {
                if(CheckForDups(rd.DrawingName, referenceDocs))
                {
                    vaultDrawingFiles[i] = rd.VaultDrawingPath;
                    i++;
                }
            }

            drawingFiles = vaultConnection.WebServiceManager.DocumentService.FindLatestFilesByPaths(vaultDrawingFiles);

            List<VDF.Vault.Currency.Entities.FileIteration> fileIters = new List<VDF.Vault.Currency.Entities.FileIteration>();

            foreach (ACW.File vFile in drawingFiles)
            {
                if (vFile.Name != null)
                {
                    fileIters.Add(new VDF.Vault.Currency.Entities.FileIteration(vaultConnection, vFile));
                }
            }

            VDF.Vault.Settings.AcquireFilesSettings drawingFileSetting = new VDF.Vault.Settings.AcquireFilesSettings(vaultConnection);

            foreach (VDF.Vault.Currency.Entities.FileIteration fi in fileIters)
            {
                if (fi.EntityName != null)
                {
                    
                    drawingFileSetting.AddFileToAcquire(fi, VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Download);
                }
            }

            VDF.Vault.Results.AcquireFilesResults drawingFileResults = vaultConnection.FileManager.AcquireFiles(drawingFileSetting);

            foreach (ReferenceDoc rd in referenceDocs)
            {
                InventorApprentice.ApprenticeServerDocument doc;
                doc = apprentice.Open(rd.PartPath);
                rd.Revision = GetRevision(doc);
                rd.PDFName = System.IO.Path.GetFileNameWithoutExtension(rd.PartName) + "rev--" + rd.Revision + ".pdf";
            }

        }

        private Boolean IsDoc(string docName)
        {
            string strFirstThreeChr = docName.Substring(0, 4);

            if ((strFirstThreeChr == "100-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "101-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "125-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "225-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "240-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "500-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "835-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "405-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "426-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "347-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "514-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "515-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "105-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "115-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "334-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "630-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "634-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "695-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "698-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "718-"))
            {
                return true;
            }
            else if ((strFirstThreeChr == "850-"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean InList(string fileToCheck, List<ReferenceDoc> refDocs)
        {
            string fileToCheckWithNoExt = System.IO.Path.GetFileNameWithoutExtension(fileToCheck);

            foreach (ReferenceDoc rd in refDocs)
            {
                string refDocWithNoExt = System.IO.Path.GetFileNameWithoutExtension(rd.PartName);

                if (refDocWithNoExt == fileToCheckWithNoExt)
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean CheckForDups(string refToCheck, List<ReferenceDoc> refDocs)
        {
            foreach (ReferenceDoc rd in refDocs)
            {
                if (rd.DrawingName == refToCheck)
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean GetSelectedFiles()
        {
            if (drawingListView.CheckedItems.Count <= 0)
            {
                return false;
            }

            foreach (ListViewItem lvi in drawingListView.CheckedItems)
            {
                if (!selectedDrawings.Contains(lvi.Text))
                {
                    selectedDrawings.Add(lvi.Text);
                }
            }            
            return true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            referenceDocs.Clear();
            selectedDrawings.Clear();
            drawingListView.Clear();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Make sure the user has selected at least one file
            if (!GetSelectedFiles())
            {
                MessageBox.Show("You must select a file");
                return;
            }
            // Make sure the user has entered and ECO Number
            if (tbECONumber.Text == "")
            {
                MessageBox.Show("You must enter an ECO Number");
                tbECONumber.Focus();
                return;
            }

            Inventor.TranslationContext transContext;
            transContext = invApp.TransientObjects.CreateTranslationContext();
            transContext.Type = Inventor.IOMechanismEnum.kFileBrowseIOMechanism;
            Inventor.NameValueMap oOptions;
            oOptions = invApp.TransientObjects.CreateNameValueMap();
            oOptions.set_Value("Sheet_Range", Inventor.PrintRangeEnum.kPrintAllSheets);
            Inventor.DataMedium oData;
            oData = invApp.TransientObjects.CreateDataMedium();
            pdfAddin = (Inventor.TranslatorAddIn)invApp.ApplicationAddIns.get_ItemById("{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}");
            Inventor.DrawingDocument drawingDoc;

            IEdmBatchUpdate2 Update = default(IEdmBatchUpdate2);
            Update = (IEdmBatchUpdate2)vault7.CreateUtility(EdmUtility.EdmUtil_BatchUpdate);
            IEdmVariableMgr5 varMgr = default(IEdmVariableMgr5);
            IEdmFile5 File;
            IEdmFolder8 Folder = (IEdmFolder8)vault7.GetFolderFromPath(PDFfilePath);
            EdmAddFileInfo[] Files = new EdmAddFileInfo[1];
            int addFileStatus;
            int ECONumberID = 0;
            int RevisionID = 0;
            EdmBatchError2[] Errors = null;
            int errorSize = 0;

            foreach (ReferenceDoc rd in referenceDocs)
            {
                foreach (string sd in selectedDrawings)
                {
                    if (rd.DrawingName == sd)
                    {
                        rd.IsSelected = true;
                    }
                }

                if (rd.IsSelected)
                {
                    drawingDoc = (Inventor.DrawingDocument)invApp.Documents.Open(rd.DrawingPath, true);
                    if (!System.IO.File.Exists(PDFfilePath + rd.PDFName))
                    {
                        oData.FileName = PDFfilePath + rd.PDFName;
                        pdfAddin.SaveCopyAs(drawingDoc, transContext, oOptions, oData);
                        Files[0].mbsPath = PDFfilePath + rd.PDFName;
                        Files[0].mlEdmAddFlags = (int)EdmAddFlag.EdmAdd_Simple;
                        Files[0].mlFileID = 0;
                        Files[0].mlSrcDocumentID = 0;
                        Files[0].mlSrcProjectID = 0;
                        Files[0].mbsNewName = "";
                        Folder.AddFile2(this.Handle.ToInt32(), Files[0].mbsPath, out addFileStatus, "", Files[0].mlEdmAddFlags);
                        File = Folder.GetFile(rd.PDFName);
                        varMgr = (IEdmVariableMgr5)File.Vault;
                        ECONumberID = varMgr.GetVariable("ECO Number").ID;
                        RevisionID = varMgr.GetVariable("Revision").ID;
                        Update.SetVar(File.ID, ECONumberID, tbECONumber.Text, "", (int)EdmBatchFlags.EdmBatch_Nothing);
                        Update.SetVar(File.ID, RevisionID, "--" + rd.Revision, "", (int)EdmBatchFlags.EdmBatch_Nothing);
                        errorSize = Update.CommitUpdate(out Errors, null);
                        File.UnlockFile(this.Handle.ToInt32(), "");
                        drawingDoc.Close();
                    }
                    else
                    {
                        MessageBox.Show("File: " + rd.PDFName +  " is already checked into EPDM", "File Exist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            referenceDocs.Clear();
            selectedDrawings.Clear();
            drawingListView.Clear();
            tbECONumber.Clear();
            btnOK.Enabled = false;
            invApp.Quit();
        }

        private string GetRevision(InventorApprentice.ApprenticeServerDocument Doc)
        {
            InventorApprentice.PropertySets propSets;
            InventorApprentice.PropertySet propSet;
            InventorApprentice.Property revProp;
            string revValue;

            propSets = Doc.PropertySets;
            propSet = propSets["Inventor Summary Information"];
            revProp = propSet["Revision Number"];
            revValue = revProp.Value;
            return revValue;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            referenceDocs.Clear();
            selectedDrawings.Clear();
            drawingListView.Clear();
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void navigateBack_toolStripButton_Click(object sender, EventArgs e)
        {
            m_model.MoveBack();
        }

        private void navigateUp_toolStripButton_Click(object sender, EventArgs e)
        {
            m_model.MoveUp();
        }

        private void switchView_toolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            //cycle through the list of available layouts when the switch view button is pressed without using the dropdown
            int setIdx = (m_availableLayouts.IndexOf(vaultBrowserControl.CurrentLayout) + 1) % m_availableLayouts.Count;
            vaultBrowserControl.CurrentLayout = m_availableLayouts[setIdx];
        }

        private void switchView_toolStripSplitButton_DropDownOpening(object sender, EventArgs e)
        {
            //Check the currenly visible view in the menu
            foreach (ToolStripMenuItem button in m_viewButtons)
            {
                button.Checked = button.Tag.Equals(vaultBrowserControl.CurrentLayout);
            }
        }

        void switchViewDropdown_itemClick(object sender, EventArgs e)
        {
            //switch to the exact layout that was chosen with the switch view dropdown menu
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            vaultBrowserControl.CurrentLayout = item.Tag as VDF.Forms.Controls.GridLayout;
        }

        /// <summary>
        /// Set the enabled/disabled states of all the controls in the app based on if we have an active connection or not.
        /// </summary>
        /// <param name="activeConnection">True if there is an active connection.</param>
        private void controlStates(bool activeConnection)
        {
            login_ToolStripMenuItem.Enabled = !activeConnection;
            logout_ToolStripMenuItem.Enabled = activeConnection;
            vaultNavigationPathComboboxControl1.Enabled = activeConnection;
            switchView_toolStripSplitButton.Enabled = activeConnection;
            vaultBrowserControl.Enabled = activeConnection;

            //navigate up and back are normally handled by the model (m_model_ParentChanged), but we need to specifically disable them when we log out
            if (activeConnection == false)
            {
                navigateBack_toolStripButton.Enabled = false;
                navigateUp_toolStripButton.Enabled = false;
            }
        }

        /// <summary>
        /// Wrapper between the filetype filters and the CanDisplayEntity deleagate on the Vault Browser control.
        /// </summary>
        /// <param name="entity">The entity to run the filter against.</param>
        /// <returns>True if the entity can be displayed.</returns>
        private bool canDisplayEntity(VDF.Vault.Currency.Entities.IEntity entity)
        {
            if (m_filterCanDisplayEntity != null)
            {
                if (!m_filterCanDisplayEntity(entity))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
