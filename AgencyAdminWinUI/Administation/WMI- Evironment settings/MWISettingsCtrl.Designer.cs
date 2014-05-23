namespace Agency.EnvSettings
{
    partial class MWISettingsCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabExplore = new System.Windows.Forms.TabControl();
            this.pgeProperties = new System.Windows.Forms.TabPage();
            this.btnRefreshTickCount = new System.Windows.Forms.Button();
            this.btnStackTrace = new System.Windows.Forms.Button();
            this.lblWorkingset = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.lblUserDomainName = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.lblOSVersion = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.lblTickCount = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblSystemDirectory = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblCurrentDirectory = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.lblCommandLine = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.pgeSpecialFolders = new System.Windows.Forms.TabPage();
            this.lblSystemFolder = new System.Windows.Forms.Label();
            this.btnGetSystemFolder = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblSpecialFolder = new System.Windows.Forms.Label();
            this.pgeMethods = new System.Windows.Forms.TabPage();
            this.lstLogicalDrives = new System.Windows.Forms.ListBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.lstCommandLineArgs = new System.Windows.Forms.ListBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.grpMethods = new System.Windows.Forms.GroupBox();
            this.btnExpand = new System.Windows.Forms.Button();
            this.lblExpandResults = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtExpand = new System.Windows.Forms.TextBox();
            this.nudExitCode = new System.Windows.Forms.NumericUpDown();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.pgeEnvironmentVariables = new System.Windows.Forms.TabPage();
            this.lblTEMP = new System.Windows.Forms.Label();
            this.btnGetEnvironmentVariable = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblEnvironmentVariable = new System.Windows.Forms.Label();
            this.lstEnvironmentVariables = new System.Windows.Forms.ListBox();
            this.pgeSystemInformation = new System.Windows.Forms.TabPage();
            this.lvwSystemInformation = new System.Windows.Forms.ListView();
            this.colProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstFolders = new System.Windows.Forms.ListBox();
            this.tabExplore.SuspendLayout();
            this.pgeProperties.SuspendLayout();
            this.pgeSpecialFolders.SuspendLayout();
            this.pgeMethods.SuspendLayout();
            this.grpMethods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExitCode)).BeginInit();
            this.pgeEnvironmentVariables.SuspendLayout();
            this.pgeSystemInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabExplore
            // 
            this.tabExplore.Controls.Add(this.pgeProperties);
            this.tabExplore.Controls.Add(this.pgeSpecialFolders);
            this.tabExplore.Controls.Add(this.pgeMethods);
            this.tabExplore.Controls.Add(this.pgeEnvironmentVariables);
            this.tabExplore.Controls.Add(this.pgeSystemInformation);
            this.tabExplore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabExplore.ItemSize = new System.Drawing.Size(59, 18);
            this.tabExplore.Location = new System.Drawing.Point(0, 0);
            this.tabExplore.Name = "tabExplore";
            this.tabExplore.SelectedIndex = 0;
            this.tabExplore.Size = new System.Drawing.Size(510, 314);
            this.tabExplore.TabIndex = 2;
            // 
            // pgeProperties
            // 
            this.pgeProperties.Controls.Add(this.btnRefreshTickCount);
            this.pgeProperties.Controls.Add(this.btnStackTrace);
            this.pgeProperties.Controls.Add(this.lblWorkingset);
            this.pgeProperties.Controls.Add(this.Label21);
            this.pgeProperties.Controls.Add(this.lblVersion);
            this.pgeProperties.Controls.Add(this.Label19);
            this.pgeProperties.Controls.Add(this.lblUserName);
            this.pgeProperties.Controls.Add(this.Label17);
            this.pgeProperties.Controls.Add(this.lblUserDomainName);
            this.pgeProperties.Controls.Add(this.Label15);
            this.pgeProperties.Controls.Add(this.lblOSVersion);
            this.pgeProperties.Controls.Add(this.Label13);
            this.pgeProperties.Controls.Add(this.lblMachineName);
            this.pgeProperties.Controls.Add(this.Label11);
            this.pgeProperties.Controls.Add(this.lblTickCount);
            this.pgeProperties.Controls.Add(this.Label9);
            this.pgeProperties.Controls.Add(this.lblSystemDirectory);
            this.pgeProperties.Controls.Add(this.Label7);
            this.pgeProperties.Controls.Add(this.lblCurrentDirectory);
            this.pgeProperties.Controls.Add(this.Label5);
            this.pgeProperties.Controls.Add(this.lblCommandLine);
            this.pgeProperties.Controls.Add(this.Label3);
            this.pgeProperties.Location = new System.Drawing.Point(4, 22);
            this.pgeProperties.Name = "pgeProperties";
            this.pgeProperties.Size = new System.Drawing.Size(502, 288);
            this.pgeProperties.TabIndex = 2;
            this.pgeProperties.Text = "Properties";
            // 
            // btnRefreshTickCount
            // 
            this.btnRefreshTickCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRefreshTickCount.Location = new System.Drawing.Point(285, 129);
            this.btnRefreshTickCount.Name = "btnRefreshTickCount";
            this.btnRefreshTickCount.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshTickCount.TabIndex = 21;
            this.btnRefreshTickCount.Text = "&Refresh";
            this.btnRefreshTickCount.Click += new System.EventHandler(this.btnRefreshTickCount_Click);
            // 
            // btnStackTrace
            // 
            this.btnStackTrace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStackTrace.Location = new System.Drawing.Point(144, 256);
            this.btnStackTrace.Name = "btnStackTrace";
            this.btnStackTrace.Size = new System.Drawing.Size(224, 23);
            this.btnStackTrace.TabIndex = 20;
            this.btnStackTrace.Text = "&Display Current Stack Trace";
            this.btnStackTrace.Click += new System.EventHandler(this.btnStackTrace_Click);
            // 
            // lblWorkingset
            // 
            this.lblWorkingset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorkingset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWorkingset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblWorkingset.Location = new System.Drawing.Point(144, 224);
            this.lblWorkingset.Name = "lblWorkingset";
            this.lblWorkingset.Size = new System.Drawing.Size(350, 23);
            this.lblWorkingset.TabIndex = 19;
            this.lblWorkingset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label21
            // 
            this.Label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label21.Location = new System.Drawing.Point(8, 224);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(136, 23);
            this.Label21.TabIndex = 18;
            this.Label21.Text = "WorkingSet";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(144, 200);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(350, 23);
            this.lblVersion.TabIndex = 17;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label19
            // 
            this.Label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label19.Location = new System.Drawing.Point(8, 200);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(136, 23);
            this.Label19.TabIndex = 16;
            this.Label19.Text = "Version";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUserName.Location = new System.Drawing.Point(144, 176);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(350, 23);
            this.lblUserName.TabIndex = 15;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label17
            // 
            this.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label17.Location = new System.Drawing.Point(8, 176);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(136, 23);
            this.Label17.TabIndex = 14;
            this.Label17.Text = "UserName";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserDomainName
            // 
            this.lblUserDomainName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserDomainName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUserDomainName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUserDomainName.Location = new System.Drawing.Point(144, 152);
            this.lblUserDomainName.Name = "lblUserDomainName";
            this.lblUserDomainName.Size = new System.Drawing.Size(350, 23);
            this.lblUserDomainName.TabIndex = 13;
            this.lblUserDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label15.Location = new System.Drawing.Point(8, 152);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(136, 23);
            this.Label15.TabIndex = 12;
            this.Label15.Text = "UserDomainName";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOSVersion
            // 
            this.lblOSVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOSVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOSVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOSVersion.Location = new System.Drawing.Point(144, 80);
            this.lblOSVersion.Name = "lblOSVersion";
            this.lblOSVersion.Size = new System.Drawing.Size(350, 23);
            this.lblOSVersion.TabIndex = 11;
            this.lblOSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            this.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label13.Location = new System.Drawing.Point(8, 80);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(136, 23);
            this.Label13.TabIndex = 10;
            this.Label13.Text = "OSVersion";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMachineName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineName.Location = new System.Drawing.Point(144, 56);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(350, 23);
            this.lblMachineName.TabIndex = 9;
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            this.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label11.Location = new System.Drawing.Point(8, 56);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(136, 23);
            this.Label11.TabIndex = 8;
            this.Label11.Text = "MachineName";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTickCount
            // 
            this.lblTickCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTickCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTickCount.Location = new System.Drawing.Point(144, 128);
            this.lblTickCount.Name = "lblTickCount";
            this.lblTickCount.Size = new System.Drawing.Size(128, 23);
            this.lblTickCount.TabIndex = 7;
            this.lblTickCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label9.Location = new System.Drawing.Point(8, 128);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(136, 23);
            this.Label9.TabIndex = 6;
            this.Label9.Text = "TickCount";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSystemDirectory
            // 
            this.lblSystemDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSystemDirectory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSystemDirectory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSystemDirectory.Location = new System.Drawing.Point(144, 104);
            this.lblSystemDirectory.Name = "lblSystemDirectory";
            this.lblSystemDirectory.Size = new System.Drawing.Size(350, 23);
            this.lblSystemDirectory.TabIndex = 5;
            this.lblSystemDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label7.Location = new System.Drawing.Point(8, 104);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(136, 23);
            this.Label7.TabIndex = 4;
            this.Label7.Text = "SystemDirectory";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentDirectory
            // 
            this.lblCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentDirectory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentDirectory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentDirectory.Location = new System.Drawing.Point(144, 32);
            this.lblCurrentDirectory.Name = "lblCurrentDirectory";
            this.lblCurrentDirectory.Size = new System.Drawing.Size(350, 23);
            this.lblCurrentDirectory.TabIndex = 3;
            this.lblCurrentDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label5.Location = new System.Drawing.Point(8, 32);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(136, 23);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "CurrentDirectory";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCommandLine
            // 
            this.lblCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommandLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCommandLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCommandLine.Location = new System.Drawing.Point(144, 8);
            this.lblCommandLine.Name = "lblCommandLine";
            this.lblCommandLine.Size = new System.Drawing.Size(350, 23);
            this.lblCommandLine.TabIndex = 1;
            this.lblCommandLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label3.Location = new System.Drawing.Point(8, 8);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(136, 23);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "CommandLine:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgeSpecialFolders
            // 
            this.pgeSpecialFolders.Controls.Add(this.lblSystemFolder);
            this.pgeSpecialFolders.Controls.Add(this.btnGetSystemFolder);
            this.pgeSpecialFolders.Controls.Add(this.Label1);
            this.pgeSpecialFolders.Controls.Add(this.lblSpecialFolder);
            this.pgeSpecialFolders.Controls.Add(this.lstFolders);
            this.pgeSpecialFolders.Location = new System.Drawing.Point(4, 22);
            this.pgeSpecialFolders.Name = "pgeSpecialFolders";
            this.pgeSpecialFolders.Size = new System.Drawing.Size(502, 288);
            this.pgeSpecialFolders.TabIndex = 0;
            this.pgeSpecialFolders.Text = "Special Folders";
            this.pgeSpecialFolders.Visible = false;
            // 
            // lblSystemFolder
            // 
            this.lblSystemFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSystemFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSystemFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSystemFolder.Location = new System.Drawing.Point(224, 168);
            this.lblSystemFolder.Name = "lblSystemFolder";
            this.lblSystemFolder.Size = new System.Drawing.Size(270, 23);
            this.lblSystemFolder.TabIndex = 4;
            this.lblSystemFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetSystemFolder
            // 
            this.btnGetSystemFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetSystemFolder.Location = new System.Drawing.Point(224, 144);
            this.btnGetSystemFolder.Name = "btnGetSystemFolder";
            this.btnGetSystemFolder.Size = new System.Drawing.Size(152, 23);
            this.btnGetSystemFolder.TabIndex = 3;
            this.btnGetSystemFolder.Text = "&get {System Folder";
            this.btnGetSystemFolder.Click += new System.EventHandler(this.btnGetSystemFolder_Click);
            // 
            // Label1
            // 
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(224, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(200, 23);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Special Folder Path:";
            // 
            // lblSpecialFolder
            // 
            this.lblSpecialFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpecialFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpecialFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSpecialFolder.Location = new System.Drawing.Point(224, 32);
            this.lblSpecialFolder.Name = "lblSpecialFolder";
            this.lblSpecialFolder.Size = new System.Drawing.Size(270, 96);
            this.lblSpecialFolder.TabIndex = 2;
            // 
            // pgeMethods
            // 
            this.pgeMethods.Controls.Add(this.lstLogicalDrives);
            this.pgeMethods.Controls.Add(this.Label12);
            this.pgeMethods.Controls.Add(this.lstCommandLineArgs);
            this.pgeMethods.Controls.Add(this.Label10);
            this.pgeMethods.Controls.Add(this.grpMethods);
            this.pgeMethods.Controls.Add(this.nudExitCode);
            this.pgeMethods.Controls.Add(this.Label4);
            this.pgeMethods.Controls.Add(this.btnExit);
            this.pgeMethods.Location = new System.Drawing.Point(4, 22);
            this.pgeMethods.Name = "pgeMethods";
            this.pgeMethods.Size = new System.Drawing.Size(502, 288);
            this.pgeMethods.TabIndex = 3;
            this.pgeMethods.Text = "Methods";
            this.pgeMethods.Visible = false;
            // 
            // lstLogicalDrives
            // 
            this.lstLogicalDrives.Location = new System.Drawing.Point(16, 152);
            this.lstLogicalDrives.Name = "lstLogicalDrives";
            this.lstLogicalDrives.Size = new System.Drawing.Size(56, 95);
            this.lstLogicalDrives.TabIndex = 5;
            // 
            // Label12
            // 
            this.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label12.Location = new System.Drawing.Point(8, 128);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(120, 23);
            this.Label12.TabIndex = 4;
            this.Label12.Text = "GetLogicalDrives";
            // 
            // lstCommandLineArgs
            // 
            this.lstCommandLineArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommandLineArgs.HorizontalScrollbar = true;
            this.lstCommandLineArgs.Location = new System.Drawing.Point(144, 152);
            this.lstCommandLineArgs.Name = "lstCommandLineArgs";
            this.lstCommandLineArgs.Size = new System.Drawing.Size(342, 95);
            this.lstCommandLineArgs.TabIndex = 7;
            // 
            // Label10
            // 
            this.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label10.Location = new System.Drawing.Point(136, 128);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(120, 23);
            this.Label10.TabIndex = 6;
            this.Label10.Text = "GetCommandLineArgs";
            // 
            // grpMethods
            // 
            this.grpMethods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMethods.Controls.Add(this.btnExpand);
            this.grpMethods.Controls.Add(this.lblExpandResults);
            this.grpMethods.Controls.Add(this.Label8);
            this.grpMethods.Controls.Add(this.Label6);
            this.grpMethods.Controls.Add(this.txtExpand);
            this.grpMethods.Location = new System.Drawing.Point(8, 40);
            this.grpMethods.Name = "grpMethods";
            this.grpMethods.Size = new System.Drawing.Size(486, 80);
            this.grpMethods.TabIndex = 3;
            this.grpMethods.TabStop = false;
            this.grpMethods.Text = "ExpandEnvironmentVariables";
            // 
            // btnExpand
            // 
            this.btnExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExpand.Location = new System.Drawing.Point(366, 16);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(112, 23);
            this.btnExpand.TabIndex = 2;
            this.btnExpand.Text = "&Expand";
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // lblExpandResults
            // 
            this.lblExpandResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpandResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExpandResults.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExpandResults.Location = new System.Drawing.Point(104, 48);
            this.lblExpandResults.Name = "lblExpandResults";
            this.lblExpandResults.Size = new System.Drawing.Size(374, 24);
            this.lblExpandResults.TabIndex = 4;
            this.lblExpandResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            this.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label8.Location = new System.Drawing.Point(16, 48);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(88, 16);
            this.Label8.TabIndex = 3;
            this.Label8.Text = "Results:";
            // 
            // Label6
            // 
            this.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label6.Location = new System.Drawing.Point(16, 24);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(88, 16);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Input:";
            // 
            // txtExpand
            // 
            this.txtExpand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpand.Location = new System.Drawing.Point(104, 16);
            this.txtExpand.Name = "txtExpand";
            this.txtExpand.Size = new System.Drawing.Size(254, 20);
            this.txtExpand.TabIndex = 1;
            this.txtExpand.Text = "windir = %windir%";
            // 
            // nudExitCode
            // 
            this.nudExitCode.Location = new System.Drawing.Point(192, 8);
            this.nudExitCode.Name = "nudExitCode";
            this.nudExitCode.Size = new System.Drawing.Size(40, 20);
            this.nudExitCode.TabIndex = 2;
            // 
            // Label4
            // 
            this.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label4.Location = new System.Drawing.Point(104, 8);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(88, 23);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "Exit &Code:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnExit
            // 
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(8, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 24);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pgeEnvironmentVariables
            // 
            this.pgeEnvironmentVariables.Controls.Add(this.lblTEMP);
            this.pgeEnvironmentVariables.Controls.Add(this.btnGetEnvironmentVariable);
            this.pgeEnvironmentVariables.Controls.Add(this.Label2);
            this.pgeEnvironmentVariables.Controls.Add(this.lblEnvironmentVariable);
            this.pgeEnvironmentVariables.Controls.Add(this.lstEnvironmentVariables);
            this.pgeEnvironmentVariables.Location = new System.Drawing.Point(4, 22);
            this.pgeEnvironmentVariables.Name = "pgeEnvironmentVariables";
            this.pgeEnvironmentVariables.Size = new System.Drawing.Size(502, 288);
            this.pgeEnvironmentVariables.TabIndex = 1;
            this.pgeEnvironmentVariables.Text = "Environment Variables";
            this.pgeEnvironmentVariables.Visible = false;
            // 
            // lblTEMP
            // 
            this.lblTEMP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTEMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTEMP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTEMP.Location = new System.Drawing.Point(224, 168);
            this.lblTEMP.Name = "lblTEMP";
            this.lblTEMP.Size = new System.Drawing.Size(270, 23);
            this.lblTEMP.TabIndex = 4;
            this.lblTEMP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetEnvironmentVariable
            // 
            this.btnGetEnvironmentVariable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetEnvironmentVariable.Location = new System.Drawing.Point(224, 144);
            this.btnGetEnvironmentVariable.Name = "btnGetEnvironmentVariable";
            this.btnGetEnvironmentVariable.Size = new System.Drawing.Size(152, 23);
            this.btnGetEnvironmentVariable.TabIndex = 3;
            this.btnGetEnvironmentVariable.Text = "&get {TEMP Variable";
            this.btnGetEnvironmentVariable.Click += new System.EventHandler(this.btnGetEnvironmentVariable_Click);
            // 
            // Label2
            // 
            this.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label2.Location = new System.Drawing.Point(224, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(200, 23);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Environment Variable Value:";
            // 
            // lblEnvironmentVariable
            // 
            this.lblEnvironmentVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnvironmentVariable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEnvironmentVariable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnvironmentVariable.Location = new System.Drawing.Point(224, 32);
            this.lblEnvironmentVariable.Name = "lblEnvironmentVariable";
            this.lblEnvironmentVariable.Size = new System.Drawing.Size(270, 96);
            this.lblEnvironmentVariable.TabIndex = 2;
            // 
            // lstEnvironmentVariables
            // 
            this.lstEnvironmentVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEnvironmentVariables.Location = new System.Drawing.Point(8, 8);
            this.lstEnvironmentVariables.Name = "lstEnvironmentVariables";
            this.lstEnvironmentVariables.Size = new System.Drawing.Size(208, 251);
            this.lstEnvironmentVariables.TabIndex = 0;
            this.lstEnvironmentVariables.SelectedIndexChanged += new System.EventHandler(this.lstEnvironmentVariables_SelectedIndexChanged);
            // 
            // pgeSystemInformation
            // 
            this.pgeSystemInformation.Controls.Add(this.lvwSystemInformation);
            this.pgeSystemInformation.Location = new System.Drawing.Point(4, 22);
            this.pgeSystemInformation.Name = "pgeSystemInformation";
            this.pgeSystemInformation.Size = new System.Drawing.Size(502, 288);
            this.pgeSystemInformation.TabIndex = 4;
            this.pgeSystemInformation.Text = "System Information";
            this.pgeSystemInformation.Visible = false;
            // 
            // lvwSystemInformation
            // 
            this.lvwSystemInformation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProperty,
            this.colValue});
            this.lvwSystemInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSystemInformation.Location = new System.Drawing.Point(0, 0);
            this.lvwSystemInformation.Name = "lvwSystemInformation";
            this.lvwSystemInformation.Size = new System.Drawing.Size(502, 288);
            this.lvwSystemInformation.TabIndex = 0;
            this.lvwSystemInformation.UseCompatibleStateImageBehavior = false;
            this.lvwSystemInformation.View = System.Windows.Forms.View.Details;
            this.lvwSystemInformation.Resize += new System.EventHandler(this.lvwSystemInformation_Resize);
            // 
            // colProperty
            // 
            this.colProperty.Text = "Property";
            this.colProperty.Width = 117;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 341;
            // 
            // lstFolders
            // 
            this.lstFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFolders.Location = new System.Drawing.Point(8, 8);
            this.lstFolders.Name = "lstFolders";
            this.lstFolders.Size = new System.Drawing.Size(208, 251);
            this.lstFolders.TabIndex = 0;
            this.lstFolders.SelectedIndexChanged += new System.EventHandler(this.lstFolders_SelectedIndexChanged);
            // 
            // MWISettingsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabExplore);
            this.Name = "MWISettingsCtrl";
            this.Size = new System.Drawing.Size(510, 314);
            this.Load += new System.EventHandler(this.MWISettingsCtrl_Load);
            this.tabExplore.ResumeLayout(false);
            this.pgeProperties.ResumeLayout(false);
            this.pgeSpecialFolders.ResumeLayout(false);
            this.pgeMethods.ResumeLayout(false);
            this.grpMethods.ResumeLayout(false);
            this.grpMethods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExitCode)).EndInit();
            this.pgeEnvironmentVariables.ResumeLayout(false);
            this.pgeSystemInformation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabExplore;
        private System.Windows.Forms.TabPage pgeProperties;
        private System.Windows.Forms.Button btnRefreshTickCount;
        private System.Windows.Forms.Button btnStackTrace;
        private System.Windows.Forms.Label lblWorkingset;
        private System.Windows.Forms.Label Label21;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label Label19;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label lblUserDomainName;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label lblOSVersion;
        private System.Windows.Forms.Label Label13;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label lblTickCount;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label lblSystemDirectory;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label lblCurrentDirectory;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label lblCommandLine;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.TabPage pgeSpecialFolders;
        private System.Windows.Forms.Label lblSystemFolder;
        private System.Windows.Forms.Button btnGetSystemFolder;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label lblSpecialFolder;
        private System.Windows.Forms.TabPage pgeMethods;
        private System.Windows.Forms.ListBox lstLogicalDrives;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.ListBox lstCommandLineArgs;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.GroupBox grpMethods;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.Label lblExpandResults;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.TextBox txtExpand;
        private System.Windows.Forms.NumericUpDown nudExitCode;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabPage pgeEnvironmentVariables;
        private System.Windows.Forms.Label lblTEMP;
        private System.Windows.Forms.Button btnGetEnvironmentVariable;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label lblEnvironmentVariable;
        private System.Windows.Forms.ListBox lstEnvironmentVariables;
        private System.Windows.Forms.TabPage pgeSystemInformation;
        private System.Windows.Forms.ListView lvwSystemInformation;
        private System.Windows.Forms.ColumnHeader colProperty;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.ListBox lstFolders;
    }
}
