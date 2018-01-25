using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class logout : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Session.Clear();
            Response.Redirect("index.aspx");
        }
    }
}