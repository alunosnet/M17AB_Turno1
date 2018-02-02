<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17AB_T1_17_18.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divLogin" runat="server" class="pull-right col-md-3 table-bordered text-center">
        Email:<asp:TextBox CssClass="form-control" ID="tbEmail" runat="server"></asp:TextBox>
        Password:<asp:TextBox CssClass="form-control" TextMode="Password" ID="tbPassword" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" CssClass="btn btn-info" runat="server" Text="Login" OnClick="Button1_Click" />
        <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Recuperar password" OnClick="Button2_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    <div class="pull-left col-md-4 col-sm-4 input-group">
        <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button runat="server" ID="btPesquisa" Text="Pesquisar"
                CssClass="btn btn-info" OnClick="btPesquisa_Click" />
        </span>
    </div>
    <div id="divLivros" runat="server" class="pull-left col-md-9"></div>
</asp:Content>
