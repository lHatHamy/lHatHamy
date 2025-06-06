<%@ Page Async="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/mpPrincipal.master" CodeBehind="Docentes.aspx.cs" Inherits="wsToolTutor.Docentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-docente {
            min-height: 180px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <h4 class="text-primary mb-3">Lista de Docentes</h4>
        <div id="divBusqueda" class="mb-4">
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/imagenes/icons8-buscar-32.png" Height="32px" OnClick="ImageButton5_Click"/>
        </div>
<asp:Repeater ID="rptDocentes" runat="server" OnItemCommand="rptDocentes_ItemCommand">
    <ItemTemplate>
        <div class="col-md-4 mb-4 d-inline-block">
            <div class="card card-docente shadow-sm h-100">
                <div class="card-body">
                    <h5 class="card-title text-dark"><%# Eval("NombreCompleto") %></h5>
                    <h6 class="card-subtitle mb-2 text-muted">Usuario: <%# Eval("Usuario") %></h6>
                    <hr />
                    <asp:Button 
                        runat="server" 
                        Text="Iniciar chat" 
                        CssClass="btn btn-primary btn-sm me-2" 
                        CommandName="IniciarChat" 
                        CommandArgument='<%# Eval("Usuario") %>' />
                    <asp:Button 
                        runat="server" 
                        Text="Ver perfil" 
                        CssClass="btn btn-outline-secondary btn-sm" 
                        CommandName="VerPerfil" 
                        CommandArgument='<%# Eval("Usuario") %>' />
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
    </div>
</asp:Content>
