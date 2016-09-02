using System;
using System.Configuration;
using System.Text;

namespace Microsoft.Legal.MatterCenter.Jasmine
{
    public partial class TestResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.Append("<script type=\"text/javascript\">var " + "appSettings" + "={");
            scriptBuilder.Append("'clientId':" + "'" + ConfigurationManager.AppSettings["CLIENT_ID"] + "',");
            scriptBuilder.Append("'tenantId':" + "'" + ConfigurationManager.AppSettings["TENANT_ID"] + "',");
            scriptBuilder.Append("'tenantURL':" + "'" + ConfigurationManager.AppSettings["TENANT_URL"] + "',");
            scriptBuilder.Append("'azureSiteName':" + "'" + ConfigurationManager.AppSettings["AZURE_SITE_NAME"] + "'");
            scriptBuilder.Append("};</script>");
            
        }
    }
}