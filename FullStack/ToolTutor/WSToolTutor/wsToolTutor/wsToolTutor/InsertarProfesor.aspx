<%@ Page Async="true" Title="Insertar Profesor" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="InsertarProfesor.aspx.cs" Inherits="wsToolTutor.InsertarProfesor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            margin-bottom: 1rem;
        }

        label {
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 0.5rem;
            margin-top: 0.2rem;
            margin-bottom: 0.5rem;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .form-container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #f9f9f9;
            padding: 30px;
            border-radius: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Registro de Profesor</h2>

    <div class="form-container">
        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresa el nombre"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtApellidoPaterno">Apellido Paterno</label>
            <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control" placeholder="Ingresa el apellido paterno"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtApellidoMaterno">Apellido Materno</label>
            <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control" placeholder="Ingresa el apellido materno"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtMatricula">Matrícula</label>
            <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" MaxLength="3" placeholder="Ej. P01"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtContrasena">Contraseña</label>
            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresa la contraseña"></asp:TextBox>
        </div>

        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Profesor" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
    </div>
</asp:Content>