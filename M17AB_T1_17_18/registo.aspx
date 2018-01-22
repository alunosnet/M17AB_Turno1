<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registo.aspx.cs" Inherits="M17AB_T1_17_18.registo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--email-->
    <div class="form-group">
        <label for="tbEmail">Email</label>
        <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control"></asp:TextBox>
    </div>
    <!--nome-->
    <div class="form-group">
        <label for="tbNome">Nome</label>
        <asp:TextBox runat="server" ID="tbNome" CssClass="form-control"></asp:TextBox>
    </div>
    <!--morada-->
    <div class="form-group">
        <label for="tbMorada">Morada</label>
        <asp:TextBox runat="server" ID="tbMorada" CssClass="form-control"></asp:TextBox>
    </div>
    <!--nif-->
    <div class="form-group">
        <label for="tbNif">Nif</label>
        <asp:TextBox runat="server" ID="tbNif" CssClass="form-control"></asp:TextBox>
    </div>
    <!--password-->
    <div class="form-group">
        <label for="tbPassword">Password</label>
        <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="g-recaptcha" data-sitekey="6Lc1vvoSAAAAAFjyIsG88_b-SoYcW5n89amtzucB"></div>
    <asp:Label ID="lbErro" runat="server"></asp:Label>
    <asp:Button CssClass="btn btn-danger" ID="btRegistar" runat="server" Text="Registar" OnClick="btRegistar_Click" />
</asp:Content>
