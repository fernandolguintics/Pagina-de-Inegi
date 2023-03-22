<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="InegiWeb.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<link href="Content/bootstrap.min.css" rel="stylesheet" />

    <title>Inicio</title>
     
</head>
<body class="justify-content-center align-items-center" style="background-color:#0077C8; font-family:Arial; color:white;">
    
    <form id="form1" runat="server" class="container d-flex justify-content-center">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="position:absolute; top:250px; left:250px; height: 97px; width: 398px;">
            <img src="Imagenes/INEGI_a (2).png"/>
        </div>
        <div style=" display:flex; flex-direction:column; justify-content:center; align-items:center; position:relative; top:200px; left:250px;">
             <h3>Iniciar Sesión</h3>
             <asp:TextBox runat="server" placeholder="Usuario" CssClass="form-control-lg" ID="tbUsuario" ></asp:TextBox>
             <asp:TextBox runat="server" placeholder="Contraseña" TextMode="Password" CssClass="form-control-lg" ID="tbContraseña"></asp:TextBox>
             <asp:Button runat="server" OnClick="AccederPagina" CssClass="btn btn-secondary btn-block btn-lg" Text="Acceder"/>
        </div>
    </form>
</body></html>