using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class Site1 : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            //testar se o cookie existe
            HttpCookie cookie = Request.Cookies["avisoCookies"];
            if (cookie != null)
                div_aviso.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e) {
            //gerar um cookie
            Guid g = Guid.NewGuid();

            HttpCookie cookie = new HttpCookie("avisoCookies", g.ToString());
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            div_aviso.Visible = false;
        }
    }
}