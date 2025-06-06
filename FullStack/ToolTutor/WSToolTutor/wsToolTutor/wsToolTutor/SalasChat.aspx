<%@ Page Async="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/mpPrincipal.master" CodeBehind="SalasChat.aspx.cs" Inherits="wsToolTutor.SalasChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-sala {
            min-height: 200px;
        }
        .contenedor-salas {
            display: flex;
            flex-direction: column-reverse; /* Aquí ocurre la magia: de abajo hacia arriba */
            gap: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <h4 class="text-primary mb-3">Salas de Chat</h4>

        <div class="contenedor-salas">
            <asp:Repeater ID="rptSalas" runat="server" OnItemCommand="rptSalas_ItemCommand">
                <ItemTemplate>
                    <div class="card card-sala shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title text-dark">Sala Nº <%# Eval("numero_sala") %></h5>
                            <p class="card-text">
                                <strong>Fecha:</strong> <%# Eval("fecha_sala") %><br />
                                <strong>Participantes:</strong> <%# Eval("participantes") %><br />
                                <strong>Total mensajes:</strong> <%# Eval("total_mensajes") %><br />
                                <strong>Último mensaje:</strong> <%# Eval("ultimo_mensaje") %>
                            </p>
                            <asp:Button 
                                runat="server" 
                                Text="Abrir" 
                                CssClass="btn btn-primary btn-sm" 
                                CommandName="AbrirSala" 
                                CommandArgument='<%# Eval("numero_sala") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
