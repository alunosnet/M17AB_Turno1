<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17AB_T1_17_18.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divLogin" runat="server">
        Email:<asp:TextBox CssClass="form-control" ID="tbEmail" runat="server"></asp:TextBox>
        Password:<asp:TextBox CssClass="form-control" TextMode="Password" ID="tbPassword" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" CssClass="btn btn-info" runat="server" Text="Login" OnClick="Button1_Click" />
        <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Recuperar password" OnClick="Button2_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
