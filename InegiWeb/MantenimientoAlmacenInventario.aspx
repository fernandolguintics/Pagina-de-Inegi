<%@ Page Language="C#" Title="INEGI - Mantenimiento Almacen" AutoEventWireup="true" CodeBehind="MantenimientoAlmacenInventario.aspx.cs" Inherits="InegiWeb.MantenimientoAlmacenInventario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <title>Mantenimiento a Inmuebles</title>
</head>
<body>
    <form id="form1" runat="server">
            <div style="text-align:center; align-items:center">
             <h2>Mantenimiento a Almacenes:Inventario</h2>
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
         <!--Tabla de Inventario--> 
    <h4> Inventario</h4>
   <asp:GridView runat="server" ID="GVInventario" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdInventario"/>
           <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="Cumple" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="Cumple" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observacion" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                    <asp:FileUpload runat="server" ID="evidencia"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:Button runat="server" ID="btnSiguiente" CssClass="btn btn-primary" OnClick="btnSiguiente_Click"  Text="Siguiente"/>
    </form>
</body>
</html>

