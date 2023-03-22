<%@ Page Language="C#" Title="INEGI - Guardar Reporte" AutoEventWireup="true" CodeBehind="GuardarReporte.aspx.cs" Inherits="InegiWeb.GuardarReporte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Estilos/Usuarios.css" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; align-items:center">
        <h2>Reporte de Mantenimiento</h2>
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
     <p style="font-size:20px; font-weight:bold">NOTA: Adjunta los archivos antes de guardar y los detalles del mantenimiento.</p>      
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div style="display:flex; padding-bottom:1em; width:20%">
       <asp:Label runat="server" Text="Fecha" Font-Size="16px" ID="tituloFecha"></asp:Label>
       <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" TextMode="Date"></asp:TextBox>
   </div>
   <div style="display:flex; padding-bottom:1em; width:30%">
       <asp:Label runat="server" Text="Requisicion de Compra" Font-Size="16px" ID="detallesCompratxt"></asp:Label>
       <asp:TextBox runat="server" ID="detallesDeCompra" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
   </div>
       <div style="display:flex; padding-bottom:1em;">
           <div style="display:flex">
               <asp:Label runat="server" Text="Orden de Compra" Font-Size="16px" ID="ordenCompratxt"></asp:Label>
               <asp:FileUpload ID="pdfOrdenCompra" runat="server" />
           </div>
           <div style="display:flex; padding-left:6em">
                <asp:Label runat="server" Text="Evidencias" Font-Size="16px" ID="evidenciastxt"></asp:Label>
            <asp:FileUpload ID="pdfEvidencias" runat="server" />
           </div>
       </div>
         <!--Tabla de Materiales de Illuminacion--> 
    <h4> Illuminacion</h4>
    <asp:GridView runat="server"  ID="GVMaterialIlluminacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
          <asp:BoundField DataField="IdMaterial"/>
          <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
            <asp:BoundField HeaderText ="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText ="CANTIDAD TOTAL" DataField="CantidadTotal"/>
            <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
        </Columns>
    </asp:GridView>
        
      <!--Tabla de Materiales de Instalacion--> 
    <h4>Instalacion Electrica</h4>
    <asp:GridView runat="server" ID="GVMaterialesInstalacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="IdMaterial"/>
            <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
            <asp:BoundField HeaderText ="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText ="CANTIDAD TOTAL" DataField="CantidadTotal"/>
            <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
        </Columns>
    </asp:GridView>
    
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Guardar Reporte" ID="btnGuardarDatos" OnClick="btnGuardarDatos_Click"  />
    
        <!--ModalErrordeCarga-->
         <asp:Panel runat="server" ID="modalErrorCarga" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:80%">
            <div class="modal-header">
                <asp:Label runat="server" ID="headTextModal" Text="Hubo un error" Font-Size="30px"></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" ID="idMessage" Font-Size="25px" Text="Hace falta cargar el archivo de orden de compra y/o evidencia."></asp:Label>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCerrar"/>
           </div>
       </div>
    </asp:Panel>
        <!--AjaxErrordeCarga-->
         <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeErrorCarga"
         TargetControlID="idMessage" CancelControlID="btnCerrar" 
        PopupControlID="modalErrorCarga" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>

