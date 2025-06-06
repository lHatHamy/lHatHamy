<%@ Page Async="true" Language="C#" AutoEventWireup="True" CodeBehind="wpAcceso.aspx.cs" Inherits="wsToolTutor.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login Chat - Rol</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(135deg, #6fb1fc, #4364f7, #004e92);
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Segoe UI', sans-serif;
        }

        .card-login {
            background-color: #ffffff;
            padding: 2rem;
            border-radius: 15px;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 420px;
        }

        .card-login h3 {
            color: #004e92;
            margin-bottom: 1.5rem;
        }

        .form-control:focus {
            box-shadow: none;
            border-color: #004e92;
        }

        .btn-estudiante {
            background-color: #d4e6f1;
            border: none;
        }

        .btn-estudiante:hover {
            background-color: #1e7e34;
        }

        .btn-profesor {
            background-color: #2e6ada;
            border: none;
        }

        .btn-profesor:hover {
            background-color: #117a8b;
        }

        .input-group-text {
            background-color: #004e92;
            color: white;
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card-login">
            <h3 class="text-center">Bienvenido a ToolTutor</h3>

            <div class="mb-3">
                <label for="TextBox1" class="form-label">Usuario</label>
                <div class="input-group">
                    <span class="input-group-text">@</span>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingresa tu usuario"></asp:TextBox>
                </div>
            </div>

            <div class="mb-4">
                <label for="TextBox2" class="form-label">Contraseña</label>
                <div class="input-group">
                    <span class="input-group-text">🔒</span>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresa tu contraseña"></asp:TextBox>
                </div>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnEstudiante" runat="server" Text="Acceder como Estudiante" CssClass="btn btn-estudiante mb-2" OnClick="btnEstudiante_Click" />
                <asp:Button ID="btnProfesor" runat="server" Text="Acceder como Profesor" CssClass="btn btn-profesor" OnClick="btnProfesor_Click" />
            </div>
        </div>
    </form>

    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="bootstrap/js/popper.js"></script>
    <script src="bootstrap/js/jquery-3.5.1.min.js"></script>
</body>
</html>
