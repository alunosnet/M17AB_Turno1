using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
            //atualizar grelha dos livros
            atualizaGrelhaLivros();
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e) {
            try {
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
                if (fileCapa.HasFile == false)
                    throw new Exception("Tem de selecionar uma capa para o livro");
                if (fileCapa.PostedFile.ContentLength == 0 ||
                    fileCapa.PostedFile.ContentLength > 5000000)
                    throw new Exception(@"O tamanho do ficheiro tem de ser superior a 0 
                                e inferior a 5Mbytes");
                if (fileCapa.PostedFile.ContentType == "image/jpeg" ||
                    fileCapa.PostedFile.ContentType == "image/png") {
                    int id = BaseDados.Instance.adicionarLivro(nome, ano, data, preco);
                    string ficheiro=Server.MapPath(@"~\Imagens\");
                    ficheiro += id + ".jpg";
                    fileCapa.SaveAs(ficheiro);
                }
                //atualizar grelha dos livros
                atualizaGrelhaLivros();
                //limpar o form
                tbAnoLivro.Text = "";
                tbDataLivro.Text = "";
                tbNomeLivro.Text = "";
                tbPrecoLivro.Text = "";
            } catch (Exception erro) {
                lbErroLivro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroLivro.CssClass = "alert alert-danger";
            }
        }
        private void atualizaGrelhaLivros() {
            gvLivros.Columns.Clear();
            gvLivros.DataSource = null;
            gvLivros.DataBind();

            DataTable dados = BaseDados.Instance.listaLivros();
            if (dados == null || dados.Rows.Count == 0)
                return;
            //gvLivros.DataSource = dados;
            //gvLivros.DataBind();

            //configurar manualmente as colunas do datatable
            DataColumn dcRemover = new DataColumn();
            dcRemover.DataType = Type.GetType("System.String");
            dcRemover.ColumnName = "Remover";
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.DataType = Type.GetType("System.String");
            dcEditar.ColumnName = "Editar";
            dados.Columns.Add(dcEditar);

            //configurar manualmente as colunas da grid
            //associar o datatable à grid
            gvLivros.DataSource = dados;
            //desativar a geração automatica das colunas
            gvLivros.AutoGenerateColumns = false;
            //remover
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover";   //título da coluna
            hlRemover.DataTextField = "Remover"; //campo associado
            //removerlivro.aspx?id=1
            hlRemover.Text = "Remover livro";
            hlRemover.DataNavigateUrlFormatString = "removerlivro.aspx?id={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlRemover);
            //editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar";   //título da coluna
            hlEditar.DataTextField = "Editar"; //campo associado
            hlEditar.Text = "Editar livro";
            hlEditar.DataNavigateUrlFormatString = "editarlivro.aspx?id={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlEditar);

            //nlivro
            BoundField bfNlivro = new BoundField();
            bfNlivro.HeaderText = "Nº livro";
            bfNlivro.DataField = "nlivro";
            gvLivros.Columns.Add(bfNlivro);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvLivros.Columns.Add(bfNome);
            //ano
            BoundField bfAno = new BoundField();
            bfAno.HeaderText = "Ano";
            bfAno.DataField = "ano";
            gvLivros.Columns.Add(bfAno);
            //data_aquisicao
            BoundField bfData = new BoundField();
            bfData.HeaderText = "Data Aquisição";
            bfData.DataField = "data_aquisicao";
            bfData.DataFormatString = "{0:dd-MM-yyyy}";
            gvLivros.Columns.Add(bfData);
            //preco
            BoundField bfPreco = new BoundField();
            bfPreco.HeaderText = "Preço";
            bfPreco.DataField = "preco";
            bfPreco.DataFormatString = "{0:C}";
            gvLivros.Columns.Add(bfPreco);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvLivros.Columns.Add(bfEstado);
            //capa
            ImageField ifCapa = new ImageField();
            ifCapa.HeaderText = "Capa";
            ifCapa.ControlStyle.Width = 100;
            int rand = new Random().Next(999999);
            ifCapa.DataImageUrlFormatString = "~/Imagens/{0}.jpg?"+rand;
            ifCapa.DataImageUrlField = "nlivro";
            gvLivros.Columns.Add(ifCapa);

            gvLivros.DataBind();
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