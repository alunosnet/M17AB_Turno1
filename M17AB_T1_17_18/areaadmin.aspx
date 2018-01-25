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
        <asp:Button runat="server" ID="btAdicionarLivro" CssClass="btn btn-danger" OnClick="btAdicionarLivro_Click" />
    </div>
    <div id="divUtilizadores" runat="server"></div>
    <div id="divEmprestimos" runat="server"></div>
    <div id="divConsultas" runat="server"></div>
</asp:Content>
