<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="editarlivro.aspx.cs" Inherits="M17AB_T1_17_18.editarlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h3>Editar Livro</h3>
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
        <asp:Image runat="server" ID="imgCapa" Width="100" />
        <div class="form-group">
            <label for="fileCapa">Capa</label>
            <asp:FileUpload runat="server" ID="fileCapa" CssClass="form-control" />
        </div>
        <asp:Label runat="server" ID="lbErroLivro"></asp:Label>
        <asp:Button Text="Atualizar" runat="server" ID="btEditarLivro" CssClass="btn btn-danger" OnClick="btEditarLivro_Click" />
        <asp:Button Text="Voltar" runat="server" ID="btVoltar" OnClick="btVoltar_Click" CssClass="btn btn-info" />
</asp:Content>
