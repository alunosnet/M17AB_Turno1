using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class index : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }
        //login
        protected void Button1_Click(object sender, EventArgs e) {
            try {
                string email = tbEmail.Text;
                string password = tbPassword.Text;

                DataTable dados = BaseDados.Instance.verificarLogin(email, password);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Login falhou.");
                //guardar informação da sessão do utilizador
                Session["nome"] = dados.Rows[0]["nome"].ToString();
                Session["perfil"] = dados.Rows[0]["perfil"].ToString();
                Session["id"] = dados.Rows[0]["id"].ToString();
                //redirecionar em função do perfil
                if (Session["perfil"].Equals("0"))
                    Response.Redirect("areaadmin.aspx");
                else
                    Response.Redirect("areacliente.aspx");
            }catch(Exception erro) {
                Label1.Text = erro.Message;
                Label1.CssClass = "alert alert-danger";
            }
        }
        //recuperar password
        protected void Button2_Click(object sender, EventArgs e) {

        }
    }
}