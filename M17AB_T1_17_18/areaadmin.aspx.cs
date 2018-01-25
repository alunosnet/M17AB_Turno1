using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class areaadmin : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se o utilizar fez login e se é admin
            if (Session["perfil"] == null || Session["perfil"].Equals("1"))
                Response.Redirect("index.aspx");
            //se é primeira vez que carrega a página esconde as divs
            if (!IsPostBack) {
                divLivros.Visible = false;
                divUtilizadores.Visible = false;
                divEmprestimos.Visible = false;
                divConsultas.Visible = false;
            }
        }
        #region Livros
        protected void btLivros_Click(object sender, EventArgs e) {
            //mostrar div livros
            divLivros.Visible = true;
            //esconder as restantes
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info active";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestismos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
            //desativar cache
            Response.CacheControl = "no-cache";
            //TODO:atualizar grelha dos livros
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e) {

        }
        #endregion
        #region Utilizadores
        protected void btUtilizadores_Click(object sender, EventArgs e) {

        }
        #endregion
        #region Emprestimos
        protected void btEmprestismos_Click(object sender, EventArgs e) {

        }
        #endregion
        #region Consultas
        protected void btConsultas_Click(object sender, EventArgs e) {

        }
        #endregion

    }
}