<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="M17AB_T1_17_18.areaadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área Admin</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btLivros" Text="Gerir Livros" CssClass="btn btn-info" OnClick="btLivros_Click" />
        <asp:Button runat="server" ID="btUtilizadores" Text="Gerir Utilizadores" CssClass="btn btn-info" OnClick="btUtilizadores_Click" />
        <asp:Button runat="server" ID="btEmprestismos" Text="Gerir Empréstimos" CssClass="btn btn-info" OnClick="btEmprestismos_Click" />
        <asp:Button runat="server" ID="btConsultas" Text="Consultas" CssClass="btn btn-info" OnClick="btConsultas_Click" />
    </div>
    <div id="divLivros" runat="server">
        <h2>Livros</h2>
        <asp:GridView runat="server" ID="gvLivros" CssClass="table table-responsive" AllowPaging="true"></asp:GridView>
        <h3>Adicionar</h3>
        <!--nome-->
        <div class="form-group">
            <label for="tbNomeLivro">Nome</label>
            <asp:TextBox runat="server" ID="tbNomeLivro" CssClass="form-control"></asp:TextBox>
        </div>
        <!--ano -->
        <div class="form-group">
            <label for="tbAnoLivro">Ano</label>
            <asp:TextBox runat="server" ID="tbAnoLivro" CssClass="form-control"></asp:TextBox>
        </div>
        <!--data_aquisicao -->
        <div class="form-group">
            <label for="tbDataLivro">Data Aquisição</label>
            <asp:TextBox runat="server" ID="tbDataLivro" CssClass="form-control"></asp:TextBox>
        </div>
        <!--preco -->
        <div class="form-group">
            <label for="tbPrecoLivro">Preço</label>
            <asp:TextBox runat="server" ID="tbPrecoLivro" CssClass="form-control"></asp:TextBox>
        </div>
        <!--capa-->
        <div class="form-group">
            <label for="fileCapa">Capa</label>
            <asp:FileUpload runat="server" ID="fileCapa" CssClass="form-control" />
        </div>
        <asp:Label runat="server" ID="lbErroLivro"></asp:Label>
        <asp:Button Text="Adicionar" runat="server" ID="btAdicionarLivro" CssClass="btn btn-danger" OnClick="btAdicionarLivro_Click" />
    </div>
    <div id="divUtilizadores" runat="server">
        <h2>Utilizadores</h2>
        <asp:GridView runat="server" ID="gvUtilizadores" CssClass="table table-responsive" />
        <h3>Adicionar</h3>
        <!--Email-->
        <div class="form-group">
            <label for="tbEmailUtil">Email:</label>
            <asp:TextBox CssClass="form-control" runat="server" ID="tbEmailUtil"></asp:TextBox>
        </div>
        <!--Nome-->
        <div class="form-group">
            <label for="tbNomeUtil">Nome:</label>
            <asp:TextBox CssClass="form-control" runat="server" ID="tbNomeUtil"></asp:TextBox>
        </div>
        <!--Morada-->
        <div class="form-group">
            <label for="tbMoradaUtil">Morada:</label>
            <asp:TextBox CssClass="form-control" runat="server" ID="tbMoradaUtil"></asp:TextBox>
        </div>
        <!--nif-->
        <div class="form-group">
            <label for="tbNifUtil">NIF:</label>
            <asp:TextBox CssClass="form-control" runat="server" ID="tbNifUtil"></asp:TextBox>
        </div>
        <!--password-->
        <div class="form-group">
            <label for="tbPasswordUtil">Password:</label>
            <asp:TextBox TextMode="Password" CssClass="form-control" runat="server" ID="tbPasswordUtil"></asp:TextBox>
        </div>
        <!--perfil-->
        <div class="form-group">
            <label for="ddPerfil">Perfil</label>
            <asp:DropDownList runat="server" ID="ddPerfil" CssClass="form-control">
                <asp:ListItem Value="0">Administrador</asp:ListItem>
                <asp:ListItem Value="1" Selected="True" >Leitor</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Label runat="server" ID="lbErroUtil"></asp:Label>
        <asp:Button Text="Adicionar" runat="server" ID="btAdicionarUtilizador" CssClass="btn btn-danger" OnClick="btAdicionarUtilizador_Click" />
     </div>
    <div id="divEmprestimos" runat="server">
        <h2>Empréstimos</h2>
        Listar só empréstimos por concluir:<asp:CheckBox OnCheckedChanged="cbEmprestimos_CheckedChanged" runat="server" ID="cbEmprestimos" AutoPostBack="true" />
        <asp:GridView runat="server" CssClass="table table-responsive" ID="gvEmprestimos" />
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="ddLivro">Livro:</label>
            <asp:DropDownList ID="ddLivro" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="ddLeitor">Leitor:</label>
            <asp:DropDownList ID="ddLeitor" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="cData">Data de Devolução</label>
            <asp:Calendar runat="server" ID="cData"></asp:Calendar>
        </div>
        <asp:Label runat="server" ID="lbErroEmprestimo"></asp:Label>
        <asp:Button runat="server" ID="btAdicionarEmprestimos" Text="Adicionar" CssClass="btn btn-danger" OnClick="btAdicionarEmprestimos_Click" />
    </div>
    <div id="divConsultas" runat="server"></div>
</asp:Content>
