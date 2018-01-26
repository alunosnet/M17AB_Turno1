<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="removerlivro.aspx.cs" Inherits="M17AB_T1_17_18.removerlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Remover livro</h1>
    Nome:<asp:Label runat="server" ID="lbNome" /><br />
    Ano:<asp:Label runat="server" ID="lbAno" /><br />
    Data Aquisição:<asp:Label runat="server" ID="lbData" /><br />
    Preço:<asp:Label runat="server" ID="lbPreco"/><br />
    Capa<br />
    <asp:Image runat="server" ID="imgCapa" Width="100" /><br />
    <asp:Button CssClass="btn btn-info" ID="btVoltar" runat="server" Text="Voltar" OnClick="btVoltar_Click" />
    <asp:Button CssClass="btn btn-danger" ID="btRemover" runat="server" Text="Remover" OnClick="btRemover_Click" />
</asp:Content>
