using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace M17AB_T1_17_18 {
    public partial class removerlivro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é um admin
            if (Session["perfil"] == null || Session["perfil"].Equals("1"))
                Response.Redirect("index.aspx");
            //livro a remover
            try {
                int nlivro = int.Parse(Request["id"].ToString());
                DataTable dados = BaseDados.Instance.devolveDadosLivro(nlivro);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("erro");
                //mostrar os dados do livro
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbAno.Text = dados.Rows[0]["ano"].ToString();
                lbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"]
                    .ToString()).ToShortDateString();
                lbPreco.Text = String.Format("{0:C}", Decimal.Parse(
                    dados.Rows[0]["preco"].ToString()));
                //capa
                string ficheiro = @"~\Imagens\" + nlivro + ".jpg";
                imgCapa.ImageUrl = ficheiro;
            } catch {
                Response.Redirect("areaadmin.aspx");
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e) {
            Response.Redirect("areaadmin.aspx");
        }

        protected void btRemover_Click(object sender, EventArgs e) {
            //remover o livro
            try {
                int nlivro = int.Parse(Request["id"].ToString());
                BaseDados.Instance.removerLivro(nlivro);
                //apagar ficheiro
                string ficheiro = Server.MapPath(@"~\Imagens\" + nlivro + ".jpg");
                System.IO.File.Delete(ficheiro);
            } catch { }
            Response.Redirect("areaadmin.aspx");
        }
    }
}