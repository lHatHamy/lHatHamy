<%@ Page Async="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/mpPrincipal.master" CodeBehind="Chat.aspx.cs" Inherits="wsToolTutor.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mensaje {
            margin-bottom: 1rem;
            padding: 0.75rem;
            border-radius: 0.5rem;
            background-color: #f1f1f1;
        }

        .mensaje.profesor {
            background-color: #d1ecf1;
        }

        .mensaje.estudiante {
            background-color: #d4edda;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <h4 class="text-primary mb-3">Historial de conversación</h4>
        <asp:Repeater ID="rptMensajes" runat="server">
            <ItemTemplate>
                <div class='mensaje <%# Eval("tipo_usuario").ToString().ToLower() %>'>
                    <strong><%# Eval("nombre_completo") %>:</strong>
                    <p><%# Eval("msg_mensaje") %></p>
                    <small><%# Eval("msg_fecha") %> - <%# Eval("estado_mensaje") %> (<%# Eval("tipo_mensaje") %>)</small>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
            <div id="divBusqueda" class="mb-4">
            <asp:TextBox ID="TextBox1" runat="server" Width="818px"></asp:TextBox>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/imagenes/icons8-enviar-50.png" OnClick="ImageButton5_Click" Height ="24px" />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="1">Terminada</asp:ListItem>
                    <asp:ListItem Value="2">Seguimiento</asp:ListItem>
                    <asp:ListItem Value="3">Canalizada</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem Value="1">Duda</asp:ListItem>
                    <asp:ListItem Value="2">Pregunta</asp:ListItem>
                    <asp:ListItem Value="3">Urgencia</asp:ListItem>
                    <asp:ListItem Value="4">Info</asp:ListItem>
                    <asp:ListItem Value="5">Crítico</asp:ListItem>
                </asp:DropDownList>
        </div>
</asp:Content>
