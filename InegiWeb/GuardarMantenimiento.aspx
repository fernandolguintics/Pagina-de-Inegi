<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuardarMantenimiento.aspx.cs" Inherits="InegiWeb.GuardarMantenimiento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <title>Mantenimiento</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; align-items:center">
       <p style="font-weight:bold"> Mantenimiento a: <asp:Label runat="server"  Text="Nombre del usuario" ID="txtnombreInmueble" Font-Bold="false" Font-Size="17px"></asp:Label></p>
       <p style="font-weight:bold"> Direccion: <asp:Label runat="server" ID="txtDireccion" Font-Size="17px" Font-Bold="false"></asp:Label></p>
        <p style="font-weight:bold">Posesion: <asp:Label runat="server" ID="txtPosesion" Font-Bold="false" Font-Size="17px" ></asp:Label>
                               Ocupacion: <asp:Label runat="server" ID="txtOcupacion" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Superficie: <asp:Label runat="server" ID="txtSuperficie" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Uso: <asp:Label runat="server" ID="txtUso" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Niveles: <asp:Label runat="server" ID="txtNiveles" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Cajones: <asp:Label runat="server" ID="txtCajones" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Antiguedad: <asp:Label runat="server" ID="txtAntiguedad" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Terreno: <asp:Label runat="server" ID="txtTerreno" Font-Bold="false" Font-Size="17px"></asp:Label>
       </p> 
   </div>
   <asp:Label runat="server" ID="txtFecha" Font-Size="17px"></asp:Label>

           <h4>Illuminacion</h4>
    <asp:GridView runat="server" ID="GVMaterialesIlluminacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField="idMaterial" Visible="false" />
           <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="UNIDAD" DataField="Unidad"/>
            <asp:BoundField HeaderText="RESPONSABLE" DataField="Responsable"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
             <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad Total"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="Mal Estado"/>
             <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
        </Columns>
    </asp:GridView>
         <h4>Instalacion Electrica</h4>
    <asp:GridView runat="server" ID="GVMaterialesInstalacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField="idMaterial" Visible="false"/>
           <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="UNIDAD" DataField="Unidad"/>
            <asp:BoundField HeaderText="RESPONSABLE" DataField="Responsable"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
             <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad Total"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="Mal Estado"/>
             <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
        </Columns>
    </asp:GridView>
    
    <asp:Button runat="server" CssClass="btn btn-success" Text="Guardar Mantenimiento" ID="Guardar" OnClick="Guardar_Click"/>
    </form>
</body>
</html>
