using System;
using System.Windows.Forms;
using System.Collections;

namespace Agency.EnvSettings
{
    public partial class MWISettingsCtrl : UserControl
    {
        public MWISettingsCtrl()
        {
            InitializeComponent();
        }


        private void MWISettingsCtrl_Load(object sender, EventArgs e)
        {

            // The LoadList procedure is in 
            // the "enum Handling" region above.
            // It loads a list box  all the 
            // elements of an enumeration. if you're
            // interested, give it a look. You could load
            // the list  items one at a time, but this
            // technique is far simpler.

            LoadList(lstFolders, typeof(Environment.SpecialFolder));

            // LoadList (in the "enum Handling" 
            // region) also loads a list box given
            // an ICollection object. The alternative would be 
            // to loop through all the elements of the collection.

            //LoadList(lstEnvironmentVariables, Environment.GetEnvironmentVariables.Keys);
            LoadList(lstEnvironmentVariables, ((System.Collections.IDictionary)Environment.GetEnvironmentVariables()).Keys);

            // Display properties on the Properties tab.

            LoadProperties();

            // Set up simple methods.

            RunMethods();

            // Load values from SystemInformation class

            LoadSystemInformation();

        }

        #region " enum Handling ";

        private void LoadList(ListBox lst, Type typ)
        {
            lst.DataSource = System.Enum.GetNames(typ);
        }

        private Environment.SpecialFolder GetSpecialFolderFromList()
        {

            // lstFolders.SelectedItem returns the name of the 
            // special folder. System.Enum.Parse will turn that
            // into an object corresponding to the enumerated value
            // matching the specific text. CType then converts the
            // object into an Environment.SpecialFolder object.
            // This is all required because Option Strict is on.

            //return CType(System.Enum.Parse(typeof(Environment.SpecialFolder),lstFolders.SelectedItem.ToString(),Environment.SpecialFolder);
            return (Environment.SpecialFolder)System.Enum.Parse(typeof(Environment.SpecialFolder), lstFolders.SelectedItem.ToString());
        }

        private void LoadList(ListBox lst, ICollection ic)
        {

            // This procedure sets the data source 

            // for a list box to be the contents

            // of an object that implements

            // the ICollection interface. You must

            // first copy the contents to something

            // that implements IList (such an array)

            // and then bind to that.

            string[] astrItems = new string[ic.Count];
            //string[] astrItems =  new string[ic.Count-1];
            ic.CopyTo(astrItems, 0);
            lst.DataSource = astrItems;

        }

        #endregion

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Environment.Exit((int)nudExitCode.Value);
        }

        private void btnExpand_Click(object sender, System.EventArgs e)
        {
            lblExpandResults.Text = Environment.ExpandEnvironmentVariables(txtExpand.Text);
        }

        private void btnGetEnvironmentVariable_Click(object sender, System.EventArgs e)
        {
            lblTEMP.Text = Environment.GetEnvironmentVariable("TEMP");
        }

        private void btnGetSystemFolder_Click(object sender, System.EventArgs e)
        {

            // if you just want to retrieve a single 
            // special folder path, pass in one of the
            // SpecialFolder enumeration values.
            // GetFolderPath is actually System.Environment.GetFolderPath.
            // See the using statement at the top of this file.

            lblSystemFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.System);

        }

        private void btnRefreshTickCount_Click(object sender, System.EventArgs e)
        {
            lblTickCount.Text = Environment.TickCount.ToString();
        }

        private void btnStackTrace_Click(object sender, System.EventArgs e)
        {
            try
            {
                MessageBox.Show(Environment.StackTrace, this.Text);
            }
            catch (System.Security.SecurityException exp)
            {
                MessageBox.Show("A security exception occurred." + Environment.NewLine + exp.Message, exp.Source);

            }
            catch (System.Exception exp)
            {
                MessageBox.Show("An unexpected error occurred accessing the StackTrace." + Environment.NewLine + exp.Message, exp.Source);
            }
        }

        private void lstEnvironmentVariables_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            // GetEnvironmentVariable is actually 
            // System.Environment.GetEnvironmentVariable.
            // lstEnvironmentVariables contains a list
            // of all the current environment variables.
            // GetEnvironmentVariable returns the value
            // associated  an environment variable.

            lblEnvironmentVariable.Text = Environment.GetEnvironmentVariable(lstEnvironmentVariables.SelectedItem.ToString());

        }

        private void lstFolders_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            Environment.SpecialFolder sf;

            // GetSpecialFolderFromList is a method
            // in the hidden "enum Handling" region 
            // above. It returns a member of the
            // Environment.SpecialFolder enumeration, 
            // and is specific to this demonstration.

            sf = GetSpecialFolderFromList();

            // GetFolderPath is a method provided by 
            // the System.Environment namespace.
            // Specifically, you could call the GetFolderPath
            // method like this:
            // YourPath = GetFolderPath(SpecialFolder.Favorites)
            // GetFolderPath is actually System.Environment.GetFolderPath.
            // See the using statement at the top of this file.

            lblSpecialFolder.Text = Environment.GetFolderPath(sf);

        }

        private void lvwSystemInformation_Resize(object sender, System.EventArgs e)
        {

            lvwSystemInformation.Columns[1].Width = lvwSystemInformation.ClientRectangle.Width - lvwSystemInformation.Columns[0].Width;

        }

        private void LoadProperties()
        {

            // This procedure fills in items
            // on the Properties tab. These are
            // most of the properties provided by the 
            // Environment class.

            lblWorkingset.Text = Environment.WorkingSet.ToString();

            lblVersion.Text = Environment.Version.ToString();

            lblUserName.Text = Environment.UserName;

            lblUserDomainName.Text = Environment.UserDomainName;

            lblTickCount.Text = Environment.TickCount.ToString();

            lblSystemDirectory.Text = Environment.SystemDirectory;

            lblOSVersion.Text = Environment.OSVersion.ToString();

            lblMachineName.Text = Environment.MachineName;

            lblCurrentDirectory.Text = Environment.CurrentDirectory;

            lblCommandLine.Text = Environment.CommandLine;

        }

        private void LoadSystemInformation()
        {

            // Add information about the static properties
            // of the SystemInformation class to a ListView control.
            // The text for the item is the name 
            // of the property provided by the 
            // SystemInformation class. The first (and only)
            // subitem is the value of the property.
            // See help on the SystemInformation class
            // for more details about each individual 
            // property.


            lvwSystemInformation.Items.Add("ArrangeDirection").SubItems.Add(SystemInformation.ArrangeDirection.ToString());

            lvwSystemInformation.Items.Add("ArrangeStartingPosition").SubItems.Add(SystemInformation.ArrangeStartingPosition.ToString());

            lvwSystemInformation.Items.Add("BootMode").SubItems.Add(SystemInformation.BootMode.ToString());

            lvwSystemInformation.Items.Add("Border3DSize").SubItems.Add(SystemInformation.Border3DSize.ToString());

            lvwSystemInformation.Items.Add("BorderSize").SubItems.Add(SystemInformation.BorderSize.ToString());

            lvwSystemInformation.Items.Add("CaptionButtonSize").SubItems.Add(SystemInformation.CaptionButtonSize.ToString());

            lvwSystemInformation.Items.Add("CaptionHeight").SubItems.Add(SystemInformation.CaptionHeight.ToString());

            lvwSystemInformation.Items.Add("ComputerName").SubItems.Add(SystemInformation.ComputerName.ToString());

            lvwSystemInformation.Items.Add("CursorSize").SubItems.Add(SystemInformation.CursorSize.ToString());

            lvwSystemInformation.Items.Add("DbcsEnabled").SubItems.Add(SystemInformation.DbcsEnabled.ToString());

            lvwSystemInformation.Items.Add("DebugOS").SubItems.Add(SystemInformation.DebugOS.ToString());

            lvwSystemInformation.Items.Add("DoubleClickSize").SubItems.Add(SystemInformation.DoubleClickSize.ToString());

            lvwSystemInformation.Items.Add("DoubleClickTime").SubItems.Add(SystemInformation.DoubleClickTime.ToString());

            lvwSystemInformation.Items.Add("DragFullWindows").SubItems.Add(SystemInformation.DragFullWindows.ToString());

            lvwSystemInformation.Items.Add("DragSize").SubItems.Add(SystemInformation.DragSize.ToString());

            lvwSystemInformation.Items.Add("FixedFrameBorderSize").SubItems.Add(SystemInformation.FixedFrameBorderSize.ToString());

            lvwSystemInformation.Items.Add("FrameBorderSize").SubItems.Add(SystemInformation.FrameBorderSize.ToString());

            lvwSystemInformation.Items.Add("HighContrast").SubItems.Add(SystemInformation.HighContrast.ToString());

            lvwSystemInformation.Items.Add("HorizontalScrollBarArrowWidth").SubItems.Add(SystemInformation.HorizontalScrollBarArrowWidth.ToString());

            lvwSystemInformation.Items.Add("HorizontalScrollBarHeight").SubItems.Add(SystemInformation.HorizontalScrollBarHeight.ToString());

            lvwSystemInformation.Items.Add("HorizontalScrollBarThumbWidth").SubItems.Add(SystemInformation.HorizontalScrollBarThumbWidth.ToString());

            lvwSystemInformation.Items.Add("IconSize").SubItems.Add(SystemInformation.IconSize.ToString());

            lvwSystemInformation.Items.Add("IconSpacingSize").SubItems.Add(SystemInformation.IconSpacingSize.ToString());

            lvwSystemInformation.Items.Add("KanjiWindowHeight").SubItems.Add(SystemInformation.KanjiWindowHeight.ToString());

            lvwSystemInformation.Items.Add("MaxWindowTrackSize").SubItems.Add(SystemInformation.MaxWindowTrackSize.ToString());

            lvwSystemInformation.Items.Add("MenuButtonSize").SubItems.Add(SystemInformation.MenuButtonSize.ToString());

            lvwSystemInformation.Items.Add("MenuCheckSize").SubItems.Add(SystemInformation.MenuCheckSize.ToString());

            lvwSystemInformation.Items.Add("MenuFont").SubItems.Add(SystemInformation.MenuFont.ToString());

            lvwSystemInformation.Items.Add("MenuHeight").SubItems.Add(SystemInformation.MenuHeight.ToString());

            lvwSystemInformation.Items.Add("MidEastEnabled").SubItems.Add(SystemInformation.MidEastEnabled.ToString());

            lvwSystemInformation.Items.Add("MinimizedWindowSize").SubItems.Add(SystemInformation.MinimizedWindowSize.ToString());

            lvwSystemInformation.Items.Add("MinimizedWindowSpacingSize").SubItems.Add(SystemInformation.MinimizedWindowSpacingSize.ToString());

            lvwSystemInformation.Items.Add("MinimumWindowSize").SubItems.Add(SystemInformation.MinimumWindowSize.ToString());

            lvwSystemInformation.Items.Add("MinWindowTrackSize").SubItems.Add(SystemInformation.MinWindowTrackSize.ToString());

            lvwSystemInformation.Items.Add("MonitorCount").SubItems.Add(SystemInformation.MonitorCount.ToString());

            lvwSystemInformation.Items.Add("MonitorsSameDisplayFormat").SubItems.Add(SystemInformation.MonitorsSameDisplayFormat.ToString());

            lvwSystemInformation.Items.Add("MouseButtons").SubItems.Add(SystemInformation.MouseButtons.ToString());

            lvwSystemInformation.Items.Add("MouseButtonsSwapped").SubItems.Add(SystemInformation.MouseButtonsSwapped.ToString());

            lvwSystemInformation.Items.Add("MousePresent").SubItems.Add(SystemInformation.MousePresent.ToString());

            lvwSystemInformation.Items.Add("MouseWheelPresent").SubItems.Add(SystemInformation.MouseWheelPresent.ToString());

            lvwSystemInformation.Items.Add("MouseWheelScrollLines").SubItems.Add(SystemInformation.MouseWheelScrollLines.ToString());

            lvwSystemInformation.Items.Add("NativeMouseWheelSupport").SubItems.Add(SystemInformation.NativeMouseWheelSupport.ToString());

            lvwSystemInformation.Items.Add("Network").SubItems.Add(SystemInformation.Network.ToString());

            lvwSystemInformation.Items.Add("PenWindows").SubItems.Add(SystemInformation.PenWindows.ToString());

            lvwSystemInformation.Items.Add("PrimaryMonitorMaximizedWindowSize").SubItems.Add(SystemInformation.PrimaryMonitorMaximizedWindowSize.ToString());

            lvwSystemInformation.Items.Add("PrimaryMonitorSize").SubItems.Add(SystemInformation.PrimaryMonitorSize.ToString());

            lvwSystemInformation.Items.Add("RightAlignedMenus").SubItems.Add(SystemInformation.RightAlignedMenus.ToString());

            lvwSystemInformation.Items.Add("Secure").SubItems.Add(SystemInformation.Secure.ToString());

            lvwSystemInformation.Items.Add("ShowSounds").SubItems.Add(SystemInformation.ShowSounds.ToString());

            lvwSystemInformation.Items.Add("SmallIconSize").SubItems.Add(SystemInformation.SmallIconSize.ToString());

            lvwSystemInformation.Items.Add("ToolWindowCaptionButtonSize").SubItems.Add(SystemInformation.ToolWindowCaptionButtonSize.ToString());

            lvwSystemInformation.Items.Add("ToolWindowCaptionHeight").SubItems.Add(SystemInformation.ToolWindowCaptionHeight.ToString());

            lvwSystemInformation.Items.Add("UserDomainName").SubItems.Add(SystemInformation.UserDomainName.ToString());

            lvwSystemInformation.Items.Add("UserInteractive").SubItems.Add(SystemInformation.UserInteractive.ToString());

            lvwSystemInformation.Items.Add("UserName").SubItems.Add(SystemInformation.UserName.ToString());

            lvwSystemInformation.Items.Add("VerticalScrollBarArrowHeight").SubItems.Add(SystemInformation.VerticalScrollBarArrowHeight.ToString());

            lvwSystemInformation.Items.Add("VerticalScrollBarThumbHeight").SubItems.Add(SystemInformation.VerticalScrollBarThumbHeight.ToString());

            lvwSystemInformation.Items.Add("VerticalScrollBarWidth").SubItems.Add(SystemInformation.VerticalScrollBarWidth.ToString());

            lvwSystemInformation.Items.Add("VirtualScreen").SubItems.Add(SystemInformation.VirtualScreen.ToString());

            lvwSystemInformation.Items.Add("WorkingArea").SubItems.Add(SystemInformation.WorkingArea.ToString());

        }

        private void RunMethods()
        {

            // Environment.GetLogicalDrives returns an 
            // array of drive names. You can iterate
            // through the array just like you would  
            // any other array, or you can use the array
            // the data source for a list, seen here.
            // Once you have a list of drives, you might
            // want to retrieve information about the files
            // on those drives. See the File and Directory
            // classes for more infomation on working 
            //  those logical entities.

            lstLogicalDrives.DataSource = Environment.GetLogicalDrives();

            lstCommandLineArgs.DataSource = Environment.GetCommandLineArgs();

        }

    }
}
