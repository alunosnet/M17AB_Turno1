using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

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
            //ativar paginação
            gvLivros.PageSize = 5;
            gvLivros.AllowPaging = true;
            gvLivros.PageIndexChanging += GvLivros_PageIndexChanging;

            gvEmprestimos.RowCommand += GvEmprestimos_RowCommand;
        }



        #region Livros
        private void GvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
        }
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
            //mostrar div utilizadores
            divUtilizadores.Visible = true;
            //esconder as restantes
            divLivros.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info active";
            btEmprestismos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
            //desativar cache
            Response.CacheControl = "no-cache";
            //atualizar grelha dos utilizadores
            atualizaGrelhaUtilizadores();
        }

        private void atualizaGrelhaUtilizadores() {
            gvUtilizadores.Columns.Clear();
            gvUtilizadores.DataSource = null;
            gvUtilizadores.DataBind();

            DataTable dados = BaseDados.Instance.listaTodosUtilizadores();

            //colunas
            //remover
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);
            //editar
            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);
            //bloquear
            DataColumn dcBloquear = new DataColumn();
            dcBloquear.ColumnName = "Bloquear";
            dcBloquear.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcBloquear);
            //histórico
            DataColumn dcHistorico = new DataColumn();
            dcHistorico.ColumnName = "Historico";
            dcHistorico.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcHistorico);

            //gridview
            gvUtilizadores.DataSource = dados;
            gvUtilizadores.AutoGenerateColumns = false;
            //remover
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover";
            hlRemover.DataTextField = "Remover";
            hlRemover.Text = "Remover Utilizador";  //TODO
            hlRemover.DataNavigateUrlFormatString = "removerutilizador.aspx?id={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlRemover);
            //editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar";
            hlEditar.DataTextField = "Editar";
            hlEditar.Text = "Editar Utilizador";    //TODO
            hlEditar.DataNavigateUrlFormatString = "editarutilizador.aspx?id={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlEditar);
            //bloquear
            HyperLinkField hlBloquear = new HyperLinkField();
            hlBloquear.HeaderText = "Bloquear";
            hlBloquear.DataTextField = "Bloquear";
            hlBloquear.Text = "Bloquear Utilizador";
            hlBloquear.DataNavigateUrlFormatString = "bloquearutilizador.aspx?id={0}";
            hlBloquear.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlBloquear);
            //histórico
            HyperLinkField hlHistorico = new HyperLinkField();
            hlHistorico.HeaderText = "Histórico";
            hlHistorico.DataTextField = "Historico";
            hlHistorico.Text = "Histórico Utilizador";  //TODO
            hlHistorico.DataNavigateUrlFormatString = "historicoutilizador.aspx?id={0}";
            hlHistorico.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlHistorico);
            //restantes campos
            //id
            BoundField bfId = new BoundField();
            bfId.HeaderText = "Id";
            bfId.DataField = "id";
            gvUtilizadores.Columns.Add(bfId);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvUtilizadores.Columns.Add(bfNome);
            //email
            BoundField bfEmail = new BoundField();
            bfEmail.HeaderText = "Email";
            bfEmail.DataField = "email";
            gvUtilizadores.Columns.Add(bfEmail);
            //morada
            BoundField bfMorada = new BoundField();
            bfMorada.HeaderText = "Morada";
            bfMorada.DataField = "morada";
            gvUtilizadores.Columns.Add(bfMorada);
            //nif
            BoundField bfNif = new BoundField();
            bfNif.HeaderText = "NIF";
            bfNif.DataField = "nif";
            gvUtilizadores.Columns.Add(bfNif);
            //perfil
            BoundField bfPerfil = new BoundField();
            bfPerfil.HeaderText = "Perfil";
            bfPerfil.DataField = "perfil";
            gvUtilizadores.Columns.Add(bfPerfil);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvUtilizadores.Columns.Add(bfEstado);

            gvUtilizadores.DataBind();
        }
        protected void btAdicionarUtilizador_Click(object sender, EventArgs e) {
            try {
                string email = tbEmailUtil.Text;
                string nome = tbNomeUtil.Text;
                string morada = tbMoradaUtil.Text;
                string nif = tbNifUtil.Text;
                string password = tbPasswordUtil.Text;
                int perfil = int.Parse(ddPerfil.SelectedValue);
                //validar os dados
                if (nome == String.Empty || nome.Length < 3)
                    throw new Exception("O nome tem de ter no mínimo 3 letras");
                if (morada == String.Empty || morada.Length < 3)
                    throw new Exception("A morada tem de ter no mínimo 3 letras");
                if (email.Contains("@") == false)
                    throw new Exception("O email não é válido");
                if (nif.Length != 9)
                    throw new Exception("O nif tem de ter 9 algarismos");
                int intnif = int.Parse(nif);
                if (password == String.Empty || password.Length < 5)
                    throw new Exception("A password tem de ter no mínimo 5 letras");
                if (perfil != 0 && perfil != 1)
                    throw new Exception("O perfil não é válido");
                //adicionar o utilizador
                BaseDados.Instance.registarUtilizador(email, nome, morada, nif, password,1, perfil);
                //limpar o form
                tbEmailUtil.Text = "";
                tbNomeUtil.Text = "";
                tbNifUtil.Text = "";
                tbMoradaUtil.Text = "";
                tbPasswordUtil.Text = "";
                //atualizar grelha
                atualizaGrelhaUtilizadores();
            } catch (Exception erro) {
                lbErroUtil.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroUtil.CssClass = "alert alert-danger";
            }
        }
        #endregion
        #region Emprestimos
        protected void btEmprestismos_Click(object sender, EventArgs e) {
            //mostrar div utilizadores
            divEmprestimos.Visible = true;
            //esconder as restantes
            divLivros.Visible = false;
            divUtilizadores.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestismos.CssClass = "btn btn-info active";
            btConsultas.CssClass = "btn btn-info";
            //desativar cache
            Response.CacheControl = "no-cache";
            //atualizar grelha dos emprestimos
            atualizaGrelhaEmprestimos();
            //atualizar as dropdown
            atualizaDDLeitores();
            atualizaDDLivros();
        }
        private void atualizaDDLivros() {
            ddLivro.Items.Clear();
            DataTable dados = BaseDados.Instance.listaLivrosDisponiveis();
            foreach (DataRow livro in dados.Rows)
                ddLivro.Items.Add(new ListItem(livro[1].ToString(),
                    livro[0].ToString()
                    ));
        }
        private void atualizaDDLeitores() {
            ddLeitor.Items.Clear();
            DataTable dados = BaseDados.Instance.listaUtilizadoresDisponiveis();
            foreach (DataRow leitor in dados.Rows)
                ddLeitor.Items.Add(new ListItem(
                    leitor[1].ToString(),leitor[0].ToString()
                    ));
        }
        protected void btAdicionarEmprestimos_Click(object sender, EventArgs e) {
            try {
                int idLeitor = int.Parse(ddLeitor.SelectedValue);
                int idLivro = int.Parse(ddLivro.SelectedValue);
                DateTime data = cData.SelectedDate;
                BaseDados.Instance.adicionarEmprestimo(idLivro, idLeitor, data);
                //atualizar a grelha e dd livros
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            }catch(Exception erro) {
                lbErroEmprestimo.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroEmprestimo.CssClass = "alert alert-danger";
            }
        }
        private void atualizaGrelhaEmprestimos() {
            gvEmprestimos.Columns.Clear();
            gvEmprestimos.DataSource = null;
            gvEmprestimos.DataBind();

            DataTable dados;
            if (cbEmprestimos.Checked)
                dados = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes();
            else
                dados= BaseDados.Instance.listaTodosEmprestimosComNomes();

            if (dados == null || dados.Rows.Count == 0) return;

            //coluna para terminar um empréstimo
            ButtonField bfReceberLivro = new ButtonField();
            bfReceberLivro.HeaderText = "Receber livro";
            bfReceberLivro.Text = "Receber";
            bfReceberLivro.ButtonType = ButtonType.Button;
            bfReceberLivro.ControlStyle.CssClass = "btn btn-default";
            bfReceberLivro.CommandName = "receber";
            gvEmprestimos.Columns.Add(bfReceberLivro);
            //coluna para enviar um email ao leitor
            ButtonField bfEmail = new ButtonField();
            bfEmail.HeaderText = "Enviar email";
            bfEmail.Text = "Email";
            bfEmail.ButtonType = ButtonType.Button;
            bfEmail.CommandName = "email";
            gvEmprestimos.Columns.Add(bfEmail);

            gvEmprestimos.DataSource = dados;
            gvEmprestimos.AutoGenerateColumns = true;
            gvEmprestimos.DataBind();
        }
        private void GvEmprestimos_RowCommand(object sender, GridViewCommandEventArgs e) {
            //linha em que o utilizador clicou
            int linha = int.Parse(e.CommandArgument as string);
            //id do empréstimo
            int id = int.Parse(gvEmprestimos.Rows[linha].Cells[2].Text);
            if (e.CommandName == "receber") {
                BaseDados.Instance.concluirEmprestimo(id);
                atualizaDDLivros();
                atualizaGrelhaEmprestimos();
            }
            if (e.CommandName == "email") {
                DataTable dadosEmprestimo = BaseDados.Instance.devolveDadosEmprestimo(id);
                int idUtilizador=int.Parse(dadosEmprestimo.Rows[0]["idutilizador"].ToString());
                DataTable dadosUtilizador = BaseDados.Instance.devolveDadosUtilizador(idUtilizador);
                string email = dadosUtilizador.Rows[0]["email"].ToString();
                string password = ConfigurationManager.AppSettings["senha"].ToString();
                BaseDados.enviarMail("alunosnet@gmail.com", password, email,
                    "Empréstimo fora de prazo", "Caro leitor deve devolver o livro que tem emprestado");
                lbErroEmprestimo.Text = "Email enviado com sucesso";
            }
        }
        //checkbox foi selecionada
        protected void cbEmprestimos_CheckedChanged(object sender, EventArgs e) {
            atualizaGrelhaEmprestimos();
        }
        #endregion
        #region Consultas
        protected void btConsultas_Click(object sender, EventArgs e) {
            //mostrar div consultar
            divConsultas.Visible = true;
            //esconder as restantes
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divLivros.Visible = false;
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestismos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info active";
            //desativar cache
            Response.CacheControl = "no-cache";
        }

        protected void ddConsulta_SelectedIndexChanged(object sender, EventArgs e) {
            //limpar grid
            gvConsultas.Columns.Clear();

            //verificar id consulta
            int iconsulta = int.Parse(ddConsulta.SelectedValue);
            //datatable
            DataTable dados;
            //switch sql
            string sql = "";
            switch (iconsulta) {
                case 1: sql = @"SELECT TOP 5 nome,count(*)
                                FROM emprestimos
                                INNER JOIN utilizadores ON idutilizador=id
                                group by idutilizador,nome
                                order by count(*) DESC";
                    break;
                case 2: sql = "";
                    break;
            }
            //associar o datatable consulta
            dados = BaseDados.Instance.devolveConsulta(sql);
            //associar a grid ao datatable
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }
        #endregion


    }
}