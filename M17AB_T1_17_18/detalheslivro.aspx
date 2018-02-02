<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="detalheslivro.aspx.cs" Inherits="M17AB_T1_17_18.detalheslivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Image runat="server" ID="imgCapa" /><br />
    <asp:Label ID="lbNome" runat="server" /><br />
    <asp:Label ID="lbAno" runat="server" /><br />
    <asp:Label ID="lbPreco" runat="server" /><br />
    <a href="index.aspx">Voltar</a>
</asp:Content>
