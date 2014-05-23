using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.FrameworkControls
{    
    [ToolboxData("<{0}:ValidationErrorMessages runat=server></{0}:ValidationErrorMessages>")]
    public class ValidationErrorMessages : WebControl
    {
        #region Constructor

        public ValidationErrorMessages()
        {
            ValidationErrors = new ENTValidationErrors();
        }

        #endregion Constructor

        #region Properties

        [Bindable(false),
        Browsable(false)]
        public ENTValidationErrors ValidationErrors { get; set; }

        #endregion Properties

        #region Methods

        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            //Show all the messages in the ENTValidationErrorsAL

            //Check if there are an items in the array list
            if (ValidationErrors.Count != 0)
            {
                //There are items so create a table with the list of messages.
                var table = new HtmlTable();

                var trHeader = new HtmlTableRow();
                var tcHeader = new HtmlTableCell();
                tcHeader.InnerText = "Please review the following issues:";
                tcHeader.Attributes.Add("class", "validatioErrorMessageHeader");
                trHeader.Cells.Add(tcHeader);
                table.Rows.Add(trHeader);

                foreach (var ve in ValidationErrors)
                {
                    var tr = new HtmlTableRow();
                    var tc = new HtmlTableCell();
                    tc.InnerText = ve.ErrorMessage;
                    tc.Attributes.Add("class", "validationErrorMessage");
                    tr.Cells.Add(tc);
                    table.Rows.Add(tr);
                }

                table.RenderControl(output);
            }
            else
            {
                //Write nothing.
                output.Write("");
            }
        }

        #endregion Methods
    }
}
