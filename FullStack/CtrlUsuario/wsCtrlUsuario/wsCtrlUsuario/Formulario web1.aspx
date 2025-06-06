    <%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="Formulario web1.aspx.cs" Inherits="wsCtrlUsuario.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="app_themes/principal/principal.css" rel="stylesheet" /></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Reporte de Usuarios Registrados" CssClass="tituloContenido"></asp:Label>
    <br />
    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/imagenes/icon_logalum.GIF" OnClick="ImageButton5_Click" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br /> <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="3">
        <HeaderStyle BackColor="Blue" Font-Names="Arial" Font-Size="Medium" />
        <PagerStyle BackColor="Blue" Font-Names="Arial" Font-Size="Small" ForeColor="White" />
        <RowStyle Font-Names="Arial" Font-Size="Small" ForeColor="Black" BackColor="#999999" />
        <SelectedRowStyle BackColor="#66FFFF" />
    </asp:GridView>
    <br />
    <br /> <br />
</asp:Content>
