<%@ Page Async="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/mpPrincipal.master" CodeBehind="Estudiantes.aspx.cs" Inherits="wsToolTutor.Estudiantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-estudiante {
            min-height: 180px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <h4 class="text-success mb-3">Lista de Estudiantes</h4>
        <div id="divBusqueda" class="mb-4">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control d-inline w-50 me-2" placeholder="Buscar estudiante..."></asp:TextBox>
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/img/search.png" OnClick="btnBuscar_Click" />
        </div>
            <asp:Repeater ID="rptEstudiantes" runat="server" OnItemCommand="rptEstudiantes_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4 mb-4 d-inline-block">
                        <div class="card card-estudiante shadow-sm h-100">
                            <div class="card-body">
                                <h5 class="card-title text-dark"><%# Eval("NombreCompleto") %></h5>
                                <h6 class="card-subtitle mb-2 text-muted">Usuario: <%# Eval("Usuario") %></h6>
                                <hr />
                                <asp:Button 
                                    runat="server" 
                                    Text="Ver perfil" 
                                    CssClass="btn btn-outline-secondary btn-sm me-2" 
                                    CommandName="VerPerfil" 
                                    CommandArgument='<%# Eval("Usuario") %>' />

                                <asp:Button 
                                    ID="btnIniciarChat" 
                                    runat="server" 
                                    Text="Iniciar chat"
                                    CommandName="IniciarChat"
                                    CommandArgument='<%# Eval("Usuario") %>' 
                                    CssClass="btn btn-primary btn-sm mt-2" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </div>
    <div id="insProfesor" class="mb-5">
        Registrar asesor
        <asp:ImageButton ID="ImageButton5" runat="server" Height="32px" ImageUrl="~/imagenes/notepad.gif"  PostBackUrl="~/InsertarProfesor.aspx" Width="36px" />
    </div>
</asp:Content>