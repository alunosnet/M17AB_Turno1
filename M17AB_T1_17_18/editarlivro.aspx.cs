using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class editarlivro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é um admin
            if (Session["perfil"] == null || Session["perfil"].Equals("1"))
                Response.Redirect("index.aspx");
            //livro a editar
            if (IsPostBack) return;
            try {
                int nlivro = int.Parse(Request["id"].ToString());
                DataTable dados = BaseDados.Instance.devolveDadosLivro(nlivro);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("erro");
                //mostrar os dados do livro
                tbNomeLivro.Text = dados.Rows[0]["nome"].ToString();
                tbAnoLivro.Text = dados.Rows[0]["ano"].ToString();
                tbDataLivro.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"]
                    .ToString()).ToShortDateString();
                tbPrecoLivro.Text = String.Format("{0:C}", Decimal.Parse(
                    dados.Rows[0]["preco"].ToString()));
                //capa
                string ficheiro = @"~\Imagens\" + nlivro + ".jpg";
                imgCapa.ImageUrl = ficheiro;
            } catch {
                Response.Redirect("areaadmin.aspx");
            }
        }

        protected void btEditarLivro_Click(object sender, EventArgs e) {
            try {
                int id = int.Parse(Request["id"].ToString());
                //validar os dados
                string nome = tbNomeLivro.Text;
                int ano = int.Parse(tbAnoLivro.Text);
                DateTime data = DateTime.Parse(tbDataLivro.Text);
                decimal preco = Decimal.Parse(tbPrecoLivro.Text);
                if (nome == String.Empty || nome.Length < 3)
                    throw new Exception("O nome do livro tem de ter pelo menos 3 letras;");
                //validar ano
                if (ano <= 0 || ano > DateTime.Now.Year)
                    throw new Exception("O ano tem de ser superior a zero e menor ou igual ao ano atual");
                //validar data
                if (data > DateTime.Now)
                    throw new Exception("Data tem de ser inferior à data atual.");
                //preço
                if (preco < 0 || preco >= 100)
                    throw new Exception(@"O preço tem de ser superior ou igual 
                            a 0 e inferior a 100.");
                //enviar a imagem da capa
                if (fileCapa.HasFile == true) {
                    if (fileCapa.PostedFile.ContentLength == 0 ||
                        fileCapa.PostedFile.ContentLength > 5000000)
                        throw new Exception(@"O tamanho do ficheiro tem de ser superior a 0 
                                e inferior a 5Mbytes");
                    if (fileCapa.PostedFile.ContentType == "image/jpeg" ||
                        fileCapa.PostedFile.ContentType == "image/png") {
                        string ficheiro = Server.MapPath(@"~\Imagens\");
                        ficheiro += id + ".jpg";
                        fileCapa.SaveAs(ficheiro);
                    }
                }
                BaseDados.Instance.atualizaLivro(id, nome, ano, data, preco);
                //redirecionar para area admin
                Response.Redirect("areaadmin.aspx");
            } catch (Exception erro) {
                lbErroLivro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroLivro.CssClass = "alert alert-danger";
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e) {
            Response.Redirect("areaadmin.aspx");
        }
    }
}