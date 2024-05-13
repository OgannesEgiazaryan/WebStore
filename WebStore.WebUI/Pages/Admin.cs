using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebStore.WebUI.Pages
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SoftUrl
        {
            get
            {
                return generateURL("Admin_Soft");
            }
        }

        public string CategoryUrl
        {
            get
            {
                return generateURL("Admin_Category");
            }
        }

        public string EventUrl
        {
            get
            {
                return generateURL("Admin_Event");
            }
        }

        public string OrderUrl
        {
            get
            {
                return generateURL("Admin_Order");
            }
        }

        public string ReviewUrl
        {
            get
            {
                return generateURL("Admin_Review");
            }
        }

        public string SellerUrl
        {
            get
            {
                return generateURL("Admin_Seller");
            }
        }

        private string generateURL(string routeName)
        {
            return RouteTable.Routes.GetVirtualPath(null, routeName, null).VirtualPath;
        }
    }
}