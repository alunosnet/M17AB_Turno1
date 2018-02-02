using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class recuperarpassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btPassword_Click(object sender, EventArgs e) {
            try {
                //guid
                string guid = Server.UrlDecode(Request["guid"].ToString());
                //atualizar a password
                string novapassword = tbPassword.Text;
                if (novapassword == String.Empty) {
                    lbErro.Text = "Tem de introduzir uma password";
                    return;
                }
                BaseDados.Instance.atualizarPassword(guid, novapassword);
                Response.Redirect("index.aspx");
            } catch {
                Response.Redirect("index.aspx");
            }
        }
    }
}