using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_T1_17_18 {
    public partial class registo : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btRegistar_Click(object sender, EventArgs e) {
            try {
                string email = tbEmail.Text;
                string nome = tbNome.Text;
                string morada = tbMorada.Text;
                string nif = tbNif.Text;
                string password = tbPassword.Text;
                if (email == String.Empty || email.Contains("@") == false)
                    throw new Exception("O email não é válido");
                if (nome.Length < 3)
                    throw new Exception("O nome tem de ter pelo menos 3 letras");
                if (morada == String.Empty)
                    throw new Exception("Tem de indicar uma morada");
                if (nif.Length != 9)
                    throw new Exception("O nif tem de ter 9 números");
                if (password.Length < 5)
                    throw new Exception("A password tem de ter 5 letras");
                //validar o recaptchar
                var resposta = Request.Form["g-Recaptcha-Response"];
                bool acertou = ReCaptcha.Validate(resposta);
                if (!acertou)
                    throw new Exception("Tem de provar que é humano");
                //registar
                BaseDados.Instance.registarUtilizador(email, nome, morada, nif, password);
                //redirecionar
                Response.Redirect("index.aspx");
            }catch(Exception erro) {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}