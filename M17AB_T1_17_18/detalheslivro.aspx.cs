using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class detalheslivro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            try {
                int id = int.Parse(Request["id"].ToString());
                DataTable dados = BaseDados.Instance.devolveDadosLivro(id);
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbAno.Text = dados.Rows[0]["ano"].ToString();
                lbPreco.Text = String.Format("{0:C}", Decimal.Parse(
                    dados.Rows[0]["preco"].ToString()));
                string ficheiro = @"~\Imagens\" + dados.Rows[0]["nlivro"].ToString() +
                    ".jpg";
                imgCapa.ImageUrl = ficheiro;
                imgCapa.Width = 200;
                //criar cookie
                HttpCookie cookie = new HttpCookie("ultimolivro", id.ToString());
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie);
            } catch {
                Response.Redirect("index.aspx");
            }
        }
    }
}