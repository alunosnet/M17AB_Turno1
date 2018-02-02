using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class index : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se tem login feito
            if (Session["perfil"] != null)
                divLogin.Visible = false;
            //listar os livros
            if (!IsPostBack) {
                DataTable dados;
                //testar se o cookie existe
                HttpCookie cookie = Request.Cookies["ultimolivro"] as HttpCookie;
                try {
                    int id = int.Parse(cookie.Value);
                    dados = BaseDados.Instance.listaLivrosComPrecoInferior(id);
                } catch {

                    dados = BaseDados.Instance.devolveConsulta("SELECT nlivro,nome,preco from livros where estado=1");
                }
                atualizaDivLivros(dados);
            }
        }

        private void atualizaDivLivros(DataTable dados) {
            if (dados == null || dados.Rows.Count == 0) {
                divLivros.InnerHtml = "";
                return;
            }
            string grelha = "";

            grelha = "<div class='container-fluid'>";
            grelha += "<div class='row'>";
            foreach (DataRow livro in dados.Rows) {
                grelha += "<div class='col-md-4 text-center'>";
                grelha += "<img src='/Imagens/" + livro["nlivro"].ToString()
                    + ".jpg' class='img-responsive' />";
                grelha += "<span class='stat-title'>" + livro["nome"].ToString() + "</span>";
                grelha += "<span class='stat-title'>" +
                    String.Format(" | {0:C}", Decimal.Parse(livro["preco"].ToString())) +
                    "</span>";
                grelha += "<br/><a href='detalheslivro.aspx?id=" + livro["nlivro"].ToString() +
                    "'>Detalhes</a></div>";
            }
            grelha += "</div></div>";
            divLivros.InnerHtml = grelha;
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
            } catch (Exception erro) {
                Label1.Text = erro.Message;
                Label1.CssClass = "alert alert-danger";
            }
        }
        //recuperar password
        protected void Button2_Click(object sender, EventArgs e) {
            try {
                if (tbEmail.Text == String.Empty)
                    throw new Exception("Tem de indicar um email.");
                //verificar se email existe
                string email = tbEmail.Text;
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("");
                //guid
                Guid g = Guid.NewGuid();

                //guardar o guid na bd
                BaseDados.Instance.recuperarPassword(email, g.ToString());
                //enviar email
                string mensagem = "Clique no link para recuperar a sua password.\n";
                mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarpassword.aspx?guid=";
                mensagem += Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";
                string senha = ConfigurationManager.AppSettings["senha"].ToString();
                BaseDados.enviarMail("alunosnet@gmail.com", senha, email,
                    "Recuperação de palavra passe", mensagem);
                Label1.Text = "Foi enviado um email de recuperação.";
                Label1.CssClass = "alert alert-success";
            } catch (Exception erro) {
                Label1.Text = erro.Message;
                if (erro.Message != String.Empty)
                    Label1.CssClass = "alert alert-danger";
            }
        }

        protected void btPesquisa_Click(object sender, EventArgs e) {
            string livro = tbPesquisa.Text;
            DataTable dados = BaseDados.Instance.pesquisaLivrosPeloNome(livro);
            atualizaDivLivros(dados);
        }
    }
}