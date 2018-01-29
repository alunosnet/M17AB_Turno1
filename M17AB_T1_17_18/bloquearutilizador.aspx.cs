using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class bloquearutilizador : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é admin
            if (Session["perfil"] == null || Session["perfil"].Equals("1"))
                Response.Redirect("index.aspx");
            //bloquear o utilizador com base no id
            try {
                int id = int.Parse(Request["id"].ToString());
                BaseDados.Instance.ativarDesativarUtilizador(id);
            }catch{}
            Response.Redirect("areaadmin.aspx");
        }
    }
}