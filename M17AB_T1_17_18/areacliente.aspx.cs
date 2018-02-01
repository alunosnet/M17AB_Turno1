using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class areacliente : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se tem login de leitor
            if (Session["perfil"] == null || Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //verificar se não é postback
            if (!IsPostBack) {
                divDevolver.Visible = false;
                divEmprestimo.Visible = false;
                divHistorico.Visible = false;
            }
            //evento rowcommand para gvdados
            gvDados.RowCommand += GvDados_RowCommand;
        }

        private void GvDados_RowCommand(object sender, GridViewCommandEventArgs e) {
            try {
                int linha = int.Parse(e.CommandArgument as string);
                int idlivro = int.Parse(gvDados.Rows[linha].Cells[1].Text);
                int idleitor = int.Parse(Session["id"].ToString());
                //todo função para registar empréstimo em nome do leitor com login
                BaseDados.Instance.adicionarEmprestimo(idlivro, idleitor, DateTime.Now.AddDays(7));

                atualizaGrelhaEmprestimo();

            } catch { }
        }

        protected void btEmprestimos_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = true;
            divHistorico.Visible = false;
            divDevolver.Visible = false;
            btEmprestimos.CssClass = "btn btn-info active";
            btHistorico.CssClass = "btn btn-info";
            btDevolve.CssClass = "btn btn-info";

            //atualizar a grelha com a lista dos livros disponíveis
            atualizaGrelhaEmprestimo();
        }

        private void atualizaGrelhaEmprestimo() {
            gvDados.Columns.Clear();
            gvDados.DataSource = null;
            gvDados.DataBind();
            gvDados.DataSource = BaseDados.Instance.listaLivrosDisponiveis();
            //coluna com botão para requisitar
            ButtonField bfRequisitar = new ButtonField();
            bfRequisitar.HeaderText = "Requisitar";
            bfRequisitar.Text = "Requisitar";
            bfRequisitar.ButtonType = ButtonType.Button;
            bfRequisitar.ControlStyle.CssClass = "btn btn-default";
            bfRequisitar.CommandName = "requisitar";
            gvDados.Columns.Add(bfRequisitar);

            gvDados.DataBind();
        }

        protected void btDevolve_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divHistorico.Visible = false;
            divDevolver.Visible = true;
            btEmprestimos.CssClass = "btn btn-info";
            btHistorico.CssClass = "btn btn-info";
            btDevolve.CssClass = "btn btn-info active";
            //atualizar a grelha com a lista dos livros a devolver
            atualizaGrelhaDevolve();
        }

        private void atualizaGrelhaDevolve() {
            gvDados.Columns.Clear();
            gvDados.DataSource = null;
            gvDados.DataBind();

            //listar os livros do leitor que ainda não foram devolvidos
            int idLeitor = int.Parse(Session["id"].ToString());
            gvDados.DataSource = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes(idLeitor);
            gvDados.DataBind();
        }

        protected void btHistorico_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divHistorico.Visible = true;
            divDevolver.Visible = false;
            btEmprestimos.CssClass = "btn btn-info";
            btHistorico.CssClass = "btn btn-info active";
            btDevolve.CssClass = "btn btn-info";
            //atualizar a grelha com a lista dos livros emprestados
            atualizaGrelhaHistorico();
        }

        private void atualizaGrelhaHistorico() {
            gvDados.Columns.Clear();
            gvDados.DataSource = null;
            gvDados.DataBind();

            //listar todos os empréstimos do leitor
            int idLeitor = int.Parse(Session["id"].ToString());
            gvDados.DataSource = BaseDados.Instance.listaTodosEmprestimosComNomes(idLeitor);
            gvDados.DataBind();
        }

   }
}