<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="recuperarpassword.aspx.cs" Inherits="M17AB_T1_17_18.recuperarpassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <label for="tbPassword">Nova password</label>
        <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" />
        <asp:Button runat="server" ID="btPassword" Text="Atualizar" OnClick="btPassword_Click" />
        <asp:Label runat="server" ID="lbErro" />
    </div>
</asp:Content>
