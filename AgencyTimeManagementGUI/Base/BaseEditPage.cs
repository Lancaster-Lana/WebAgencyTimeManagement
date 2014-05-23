using System;
using Agency.PaidTimeOffBLL.Framework;

/// <summary>
/// Summary description for BaseEditPage
/// </summary>
public abstract class BaseEditPage<T> : BasePage
    where T : ENTBaseEO, new()
{
    #region Constructor

    protected BaseEditPage() { }

    #endregion Constructor

    #region Properties

    #endregion Properties

    #region Virtual Methods

    /// <summary>
    /// Initializes a new edit object and then calls load object from screen.
    /// </summary>
    protected virtual void LoadNew()
    {
        T baseEO = new T();
        baseEO.Init();
        LoadScreenFromObject(baseEO);
    }

    #endregion Methods

    #region Abstract Methods

    /// <summary>
    /// Scrapes the screen and loads the edit object.
    /// </summary>
    /// <param name="baseEO"></param>
    protected abstract void LoadObjectFromScreen(T baseEO);

    /// <summary>
    /// Load the controls on the screen from the object's properties.
    /// </summary>
    /// <param name="baseEO"></param>
    protected abstract void LoadScreenFromObject(T baseEO);

    /// <summary>
    /// Preload the controls such as drop down lists and listboxes.
    /// </summary>
    protected abstract void LoadControls();

    /// <summary>
    /// Navigate the user back the list page.  The cancel button and a successful save should both call this.
    /// </summary>
    protected abstract void GoToGridPage();

    #endregion Abstract Methods

    #region Overrides

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!IsPostBack)
        {
            //Load any list boxes, drop downs, etc.
            LoadControls();

            int id = GetId();

            if (id == 0)
            {
                LoadNew();
            }
            else
            {
                T baseEO = new T();
                baseEO.Load(Convert.ToInt32(id));
                LoadScreenFromObject(baseEO);
            }
        }
    }

    #endregion Overrides

    #region Methods

    public int GetId()
    {
        //Decrypt the query string
        var queryString = DecryptQueryString(Request.QueryString.ToString());

        if (queryString == null)
        {
            return 0;
        }
        //Check if the id was passed in.
        string id = queryString["id"];

        if ((id == null) || (id == "0"))
        {
            return 0;
        }
        return Convert.ToInt32(id);
    }

    #endregion Methods
}

