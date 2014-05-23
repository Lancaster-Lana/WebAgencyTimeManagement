using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_AuditObject : BaseEditPage<ENTAuditObjectEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_AUDIT_OBJECT = "AuditObject";

    #endregion Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += Master_SaveButton_Click;
        Master.CancelButton_Click += Master_CancelButton_Click;

        LoadPropertiesTable();
    }
                    
    protected override void LoadObjectFromScreen(ENTAuditObjectEO baseEO)
    {
        baseEO.ObjectName = ddlObjectName.SelectedItem.Text;
        baseEO.ObjectFullyQualifiedName = ddlObjectName.SelectedValue;

        //Set the values for the properties that are checked to audit.
        baseEO.Properties.Clear();

        //Load the properties
        if (tblProperties.Rows.Count > 1)
        {
            //skip the header
            for (int row = 1; row < tblProperties.Rows.Count; row++)
            {
                TableRow tr = tblProperties.Rows[row];

                string propertyName = tr.Cells[0].Text;

                if (((CheckBox)tr.Cells[1].Controls[0]).Checked)
                {
                    baseEO.Properties.Add(new ENTAuditObjectPropertyEO
                    {
                        ENTAuditObjectId = baseEO.ID,
                        PropertyName = propertyName
                    });
                }
            }
        }
    }

    protected override void LoadScreenFromObject(ENTAuditObjectEO baseEO)
    {
        var auditObject = baseEO;
                
        if (auditObject.ObjectName != null)
        {
            ddlObjectName.Items.Add(new ListItem(auditObject.ObjectName, auditObject.ObjectFullyQualifiedName));
            ddlObjectName.SelectedIndex = 0;
            
            //Load the properties table for this object
            LoadPropertiesTable();

            //Set the checkboxes for the properties that are selected to audit.
            if (tblProperties.Rows.Count > 1)
            {
                //skip the header
                for (int row = 1; row < tblProperties.Rows.Count; row++)
                {
                    TableRow tr = tblProperties.Rows[row];

                    string propertyName = tr.Cells[0].Text;

                    //Try to find the item in the properties list
                    if (auditObject.Properties.GetByPropertyName(propertyName) != null)
                    {
                        ((CheckBox)tr.Cells[1].Controls[0]).Checked = true;
                    }
                }
            }
        }

        ViewState[VIEW_STATE_KEY_AUDIT_OBJECT] = baseEO;        
    }

    protected override void LoadControls()
    {
        if (base.GetId() == 0)
        {            
            //Load the list of object that inherit from the EO object
            var assembly = Assembly.Load("Agency.PaidTimeOffBLL");

            var types = assembly.GetTypes();

            //Load the list of objects already being audited
            var auditObjects = new ENTAuditObjectEOList();
            auditObjects.Load();

            foreach (var t in types)
            {
                if ((t.IsClass) && (t.IsSubclassOf(typeof(ENTBaseEO))))
                {
                    //do not include any items in the list that already are being audited.
                    if (auditObjects.GetByObjectName(t.Name) == null)
                    {
                        ddlObjectName.Items.Add(new ListItem(t.Name, t.AssemblyQualifiedName));
                    }
                }
            }

            ddlObjectName.Items.Insert(0, new ListItem("", ""));
        }        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("AuditObjects.aspx");
    }

    public override string MenuItemName()
    {
        return "Auditing";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Auditing" };
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();

        ENTAuditObjectEO auditObject = (ENTAuditObjectEO)ViewState[VIEW_STATE_KEY_AUDIT_OBJECT];
        LoadObjectFromScreen(auditObject);

        if (!auditObject.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    private void LoadPropertiesTable()
    {
        if (ddlObjectName.SelectedValue != "")
        {                                           
            //Add header
            TableRow trHeader = new TableRow();
            TableCell tc1 = new TableCell();
            tc1.Text = "Property";
            trHeader.Cells.Add(tc1);

            TableCell tc2 = new TableCell();
            tc2.Text = "Audit this property";
            trHeader.Cells.Add(tc2);

            tblProperties.Rows.Add(trHeader);

            //Create an instance of the type.            
            PropertyInfo[] properties = Type.GetType(ddlObjectName.SelectedValue).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

            //Populate the table with all the fields.
            foreach (PropertyInfo prop in properties)
            {
                //Only show properties the have a public set property.
                MethodInfo[] methodInfo = prop.GetAccessors();

                //Get the set method
                IEnumerable<MethodInfo> set =
                    from m in methodInfo
                    where m.Name.StartsWith("set")
                    select m;

                if (set.Count() > 0)
                {
                    if (set.Single<MethodInfo>().IsPublic)
                    {
                        ENTWFStatePropertyEO entWFStateProperty = new ENTWFStatePropertyEO();
                                                
                        TableRow tr = new TableRow();

                        //Name of property
                        TableCell tcName = new TableCell();                        
                        tcName.Text = prop.Name;
                        tr.Cells.Add(tcName);

                        //Checkbox
                        TableCell tcAudit = new TableCell();
                        CheckBox chkAudit = new CheckBox();
                        chkAudit.Enabled = !ReadOnly;
                        chkAudit.ID = "chk" + prop.Name;
                        tcAudit.Controls.Add(chkAudit);
                        tr.Cells.Add(tcAudit);
                                                    
                        tblProperties.Rows.Add(tr);
                    }
                }
            }            
        }
        else
        {
            tblProperties.Rows.Clear();
        }
    }
    protected void ddlObjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblProperties.Rows.Clear();
        LoadPropertiesTable();
    }
}
