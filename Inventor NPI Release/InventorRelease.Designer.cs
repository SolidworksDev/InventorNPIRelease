namespace Inventor_NPI_Release
{
    partial class InventorRelease
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventorRelease));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.login_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logout_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.drawingListView = new System.Windows.Forms.ListView();
            this.tbECONumber = new System.Windows.Forms.TextBox();
            this.lbECONumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.vaultNavigationPathComboboxControl1 = new Autodesk.DataManagement.Client.Framework.Vault.Forms.Controls.VaultNavigationPathComboboxControl();
            this.lbLookIn = new System.Windows.Forms.Label();
            this.vaultBrowserControl = new Autodesk.DataManagement.Client.Framework.Vault.Forms.Controls.VaultBrowserControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.fileName_label = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileName_multiPartTextBox = new Autodesk.DataManagement.Client.Framework.Forms.Controls.MultiPartTextBoxControl();
            this.navigateBack_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.navigateUp_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.switchView_toolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1003, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.quitToolStripMenuItem,
            this.login_ToolStripMenuItem,
            this.logout_ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(42, 20);
            this.toolStripMenuItem1.Text = "Files";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // login_ToolStripMenuItem
            // 
            this.login_ToolStripMenuItem.Name = "login_ToolStripMenuItem";
            this.login_ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.login_ToolStripMenuItem.Text = "Login";
            // 
            // logout_ToolStripMenuItem
            // 
            this.logout_ToolStripMenuItem.Name = "logout_ToolStripMenuItem";
            this.logout_ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.logout_ToolStripMenuItem.Text = "Logout";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            // 
            // drawingListView
            // 
            this.drawingListView.AutoArrange = false;
            this.drawingListView.CheckBoxes = true;
            this.tableLayoutPanel2.SetColumnSpan(this.drawingListView, 3);
            this.drawingListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingListView.Location = new System.Drawing.Point(3, 313);
            this.drawingListView.Name = "drawingListView";
            this.drawingListView.Size = new System.Drawing.Size(997, 278);
            this.drawingListView.TabIndex = 24;
            this.drawingListView.UseCompatibleStateImageBehavior = false;
            this.drawingListView.View = System.Windows.Forms.View.Details;
            // 
            // tbECONumber
            // 
            this.tbECONumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbECONumber.Location = new System.Drawing.Point(78, 605);
            this.tbECONumber.Name = "tbECONumber";
            this.tbECONumber.Size = new System.Drawing.Size(108, 20);
            this.tbECONumber.TabIndex = 27;
            // 
            // lbECONumber
            // 
            this.lbECONumber.AutoSize = true;
            this.lbECONumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbECONumber.Location = new System.Drawing.Point(3, 594);
            this.lbECONumber.Name = "lbECONumber";
            this.lbECONumber.Size = new System.Drawing.Size(69, 42);
            this.lbECONumber.TabIndex = 26;
            this.lbECONumber.Text = "ECO Number";
            this.lbECONumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.76112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.76112F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(772, 597);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(228, 36);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(117, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(108, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // vaultNavigationPathComboboxControl1
            // 
            this.vaultNavigationPathComboboxControl1.AutoSize = true;
            this.vaultNavigationPathComboboxControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vaultNavigationPathComboboxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vaultNavigationPathComboboxControl1.Enabled = false;
            this.vaultNavigationPathComboboxControl1.Location = new System.Drawing.Point(78, 3);
            this.vaultNavigationPathComboboxControl1.Name = "vaultNavigationPathComboboxControl1";
            this.vaultNavigationPathComboboxControl1.Size = new System.Drawing.Size(688, 26);
            this.vaultNavigationPathComboboxControl1.TabIndex = 1;
            // 
            // lbLookIn
            // 
            this.lbLookIn.AutoSize = true;
            this.lbLookIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLookIn.Location = new System.Drawing.Point(3, 0);
            this.lbLookIn.Name = "lbLookIn";
            this.lbLookIn.Size = new System.Drawing.Size(69, 32);
            this.lbLookIn.TabIndex = 2;
            this.lbLookIn.Text = "Look in:";
            this.lbLookIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // vaultBrowserControl
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.vaultBrowserControl, 3);
            this.vaultBrowserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vaultBrowserControl.Location = new System.Drawing.Point(3, 35);
            this.vaultBrowserControl.Name = "vaultBrowserControl";
            this.vaultBrowserControl.Size = new System.Drawing.Size(997, 242);
            this.vaultBrowserControl.TabIndex = 28;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.fileName_label, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.vaultBrowserControl, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbLookIn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbECONumber, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.vaultNavigationPathComboboxControl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.fileName_multiPartTextBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.drawingListView, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbECONumber, 1, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.61972F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.38028F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1003, 636);
            this.tableLayoutPanel2.TabIndex = 29;
            // 
            // fileName_label
            // 
            this.fileName_label.AutoSize = true;
            this.fileName_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileName_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.fileName_label.Location = new System.Drawing.Point(2, 280);
            this.fileName_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fileName_label.Name = "fileName_label";
            this.fileName_label.Size = new System.Drawing.Size(71, 30);
            this.fileName_label.TabIndex = 32;
            this.fileName_label.Text = "File name:";
            this.fileName_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigateBack_toolStripButton,
            this.navigateUp_toolStripButton,
            this.switchView_toolStripSplitButton});
            this.toolStrip1.Location = new System.Drawing.Point(769, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(234, 32);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileName_multiPartTextBox
            // 
            this.fileName_multiPartTextBox.AutoSize = true;
            this.fileName_multiPartTextBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fileName_multiPartTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileName_multiPartTextBox.EditMode = Autodesk.DataManagement.Client.Framework.Forms.Controls.MultiPartTextBoxControl.EditModeOption.FullEdit;
            this.fileName_multiPartTextBox.Location = new System.Drawing.Point(78, 283);
            this.fileName_multiPartTextBox.Name = "fileName_multiPartTextBox";
            this.fileName_multiPartTextBox.Parts = ((System.Collections.Generic.IEnumerable<string>)(resources.GetObject("fileName_multiPartTextBox.Parts")));
            this.fileName_multiPartTextBox.Size = new System.Drawing.Size(688, 24);
            this.fileName_multiPartTextBox.TabIndex = 30;
            // 
            // navigateBack_toolStripButton
            // 
            this.navigateBack_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateBack_toolStripButton.Image = global::Inventor_NPI_Release.Properties.Resources.Back_16;
            this.navigateBack_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateBack_toolStripButton.Name = "navigateBack_toolStripButton";
            this.navigateBack_toolStripButton.Size = new System.Drawing.Size(23, 29);
            this.navigateBack_toolStripButton.Text = "Back";
            this.navigateBack_toolStripButton.Click += new System.EventHandler(this.navigateBack_toolStripButton_Click);
            // 
            // navigateUp_toolStripButton
            // 
            this.navigateUp_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateUp_toolStripButton.Image = global::Inventor_NPI_Release.Properties.Resources.uplevel_16;
            this.navigateUp_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateUp_toolStripButton.Name = "navigateUp_toolStripButton";
            this.navigateUp_toolStripButton.Size = new System.Drawing.Size(23, 29);
            this.navigateUp_toolStripButton.Text = "Up";
            this.navigateUp_toolStripButton.Click += new System.EventHandler(this.navigateUp_toolStripButton_Click);
            // 
            // switchView_toolStripSplitButton
            // 
            this.switchView_toolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.switchView_toolStripSplitButton.Image = global::Inventor_NPI_Release.Properties.Resources.ViewOptions_16;
            this.switchView_toolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.switchView_toolStripSplitButton.Name = "switchView_toolStripSplitButton";
            this.switchView_toolStripSplitButton.Size = new System.Drawing.Size(32, 29);
            this.switchView_toolStripSplitButton.Text = "Switch View";
            this.switchView_toolStripSplitButton.ButtonClick += new System.EventHandler(this.switchView_toolStripSplitButton_ButtonClick);
            this.switchView_toolStripSplitButton.DropDownOpening += new System.EventHandler(this.switchView_toolStripSplitButton_DropDownOpening);
            // 
            // InventorRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1003, 660);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InventorRelease";
            this.Text = "Inventor Release";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventorRelease_FormClosed);
            this.Shown += new System.EventHandler(this.InventorRelease_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.ListView drawingListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton navigateBack_toolStripButton;
        private Autodesk.DataManagement.Client.Framework.Vault.Forms.Controls.VaultBrowserControl vaultBrowserControl;
        private System.Windows.Forms.Label lbLookIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbECONumber;
        private System.Windows.Forms.TextBox tbECONumber;
        private Autodesk.DataManagement.Client.Framework.Vault.Forms.Controls.VaultNavigationPathComboboxControl vaultNavigationPathComboboxControl1;
        private System.Windows.Forms.Label fileName_label;
        private System.Windows.Forms.ToolStripButton navigateUp_toolStripButton;
        private System.Windows.Forms.ToolStripSplitButton switchView_toolStripSplitButton;
        private Autodesk.DataManagement.Client.Framework.Forms.Controls.MultiPartTextBoxControl fileName_multiPartTextBox;
        private System.Windows.Forms.ToolStripMenuItem login_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logout_ToolStripMenuItem;
    }
}

