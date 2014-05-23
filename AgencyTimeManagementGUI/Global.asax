<%@ Application Language="C#" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling" %>

<script runat="server">
    
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        try
        {
            /*
            AuthentificationWebService.eAuthDBWebService  service = new  AuthentificationWebService.eAuthDBWebService();
            service.Credentials = System.Net.CredentialCache.DefaultCredentials;

            string encTicket = service.WinAuthenticate();

            Response.Cookies.Add(new HttpCookie("EGTicketCookieName", encTicket));
            Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings["WebPortalUrl"].Replace("$Host$", Request.Url.Host));
             */

            //FormsAuthentication.GetAuthCookie(HttpContext.Current.User.Identity.Name, false);

            //var appl = (HttpApplication)sender;
            // Response.Redirect(!HttpContext.Current.Request.IsAuthenticated ? "Startup.aspx" : "Default.aspx");
        }
        catch (Exception ex)
        {
            Context.Items["ErrorMessage"] = ex.Message;
        }
    }

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        Exception _exception = this.Server.GetLastError();
        //this.Context.Cache.Remove("_CONFIGURATION_ALL_/");
        //if (this.Context.Application[Constants.ApplicationErrors] == null)
        //    this.Context.Application.Add(Constants.ApplicationErrors, new List<Exception>());
        //((List<Exception>)this.Context.Application[Constants.ApplicationErrors]).Add(_exception);
        var properties = (Dictionary<string, object>)GetRequestProperties();

        //try
        //{
        //ApplicationErrorLog errorLog = new ApplicationErrorLog();
        var builder = new StringBuilder();
        builder.AppendLine(_exception.ToString());
        builder.AppendLine();
        builder.AppendLine("Extended Properties:");
        foreach (KeyValuePair<string, object> property in properties)
        {
            builder.AppendFormat("{0}: {1}\n", property.Key, property.Value);
        }
        //errorLog.Msg = builder.ToString();
        // errorLog.IsSystem = true;
        // errorLog.Insert();

        //Redirect to  Error Page
        /*
            HttpContext.Current.Response.Redirect("ErrorPage.aspx?ErrorMessage=" + ex.Message, true);
                             
         */
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    private IDictionary<string, object> GetRequestProperties()
    {
        var properties = new Dictionary<string, object>();
        properties.Add("Requested Url", HttpContext.Current.Request.RawUrl);
        properties.Add("User Name", HttpContext.Current.User != null
                                    ? HttpContext.Current.User.Identity.Name
                                    : String.Empty);
        properties.Add("IsAuthenticated", HttpContext.Current.Request.IsAuthenticated);
        properties.Add("HttpMethod", HttpContext.Current.Request.HttpMethod);
        //properties.Add("UserID", UserSettingsManager.Instance.UserID);
        //properties.Add("LanguageID", UserSettingsManager.Instance.LanguageID);

        //foreach (string header in HttpContext.Current.Request.Headers)            
        //    properties.Add(header, HttpContext.Current.Request.Headers[header]);

        //foreach (KeyValuePair<string, string> parameter in UserSettingManagers.Instance.CurrentParameters)            
        //    properties.Add(parameter.Key, parameter.Value);

        return properties;
    }
    
</script>
