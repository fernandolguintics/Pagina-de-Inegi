<%@ Page Language="C#" Title="INEGI - Validar Reporte" AutoEventWireup="true" CodeBehind="ValidacionReportes.aspx.cs" Inherits="InegiWeb.ValidacionReportes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div style="text-align:center; align-items:center">
        <h2>Reporte de Mantenimiento</h2>
       <p style="font-weight:bold"> Reporte a: <asp:Label runat="server"  ID="txtnombreInmueble" Font-Bold="false" Font-Size="17px"></asp:Label></p>
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
        
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div style="display:flex; padding-bottom:1em; width:20%">
       <asp:Label runat="server" Font-Size="16px" ID="tituloFecha" Font-Bold="true"></asp:Label>
   </div>
   <div style="display:flex; padding-bottom:1em; width:30%">
       <h6 style="font-weight:bold">Detalles de Compra:</h6>
       <asp:Label runat="server" Font-Size="16px" ID="detallesCompratxt"></asp:Label>
       <asp:TextBox runat="server" TextMode="MultiLine" ID="txbDetalles" Visible="false" CssClass="form-control"></asp:TextBox>
       
   </div>
       <div style="display:flex; padding-bottom:1em;">
           <div style="display:flex">
               <asp:Label runat="server" Text="Requisicion de Compra:" Font-Size="16px" ID="ordenCompratxt" Font-Bold="true"></asp:Label>
               <asp:HyperLink runat="server" ID="urlRequision"  Text="Ver Requision" Target="_blank"></asp:HyperLink>
                 <asp:FileUpload ID="pdfRequisicionCompra" runat="server" Visible="false" />
           </div>
           <div style="display:flex; padding-left:6em">
                <asp:Label runat="server" Text="Evidencias:" Font-Size="16px" ID="evidenciastxt" Font-Bold="true"></asp:Label>
                <asp:HyperLink runat="server" ID="urlEvidencias"  Text="Ver Evidencias"  Target="_blank"></asp:HyperLink>
            <asp:FileUpload ID="pdfEvidencias" runat="server" Visible="false" />
           </div>
       </div>
        <div style="display:flex; padding-bottom:1em; width:20%">
            <asp:Label runat="server" Text="Validar" Font-Bold="true" Font-Size="20px" ID="txtValidar" Visible="true"></asp:Label>
            <asp:DropDownList runat="server" id="validacionDDL" CssClass="form-control" Visible="true">
            <asp:ListItem Selected="True" Value="Pendiente">Pendiente</asp:ListItem>
                <asp:ListItem Value="Aprobado">Aprobado</asp:ListItem>
                <asp:ListItem Value="No Aprobado">No Aprobado</asp:ListItem>
        </asp:DropDownList>
        </div>
         <!--Tabla de Materiales de Illuminacion--> 
    <h4> Illuminacion</h4>
    <asp:GridView runat="server"  ID="GVMaterialIlluminacion" AutoGenerateColumns="false" Width="50%" GridLines="None" CssClass="table table-striped">
        <Columns>
          <asp:BoundField DataField="Id" Visible="false"/>
          <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio" Visible="false"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal2" Visible="false"/>
        </Columns>
    </asp:GridView>
        
      <!--Tabla de Materiales de Instalacion--> 
    <h4>Instalacion Electrica</h4>
    <asp:GridView runat="server" ID="GVMaterialesInstalacion" AutoGenerateColumns="false" Width="50%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="Id" Visible="false"/>
            <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio" Visible="false"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal2" Visible="false"/>
        </Columns>
    </asp:GridView>
    <div style="display:flex; padding-top:1em; width:50%">
        <div style="display:flex">
            <h5>Observaciones:</h5>
        </div>
        <div style="padding-left:6em">
             <asp:TextBox runat="server" TextMode="MultiLine" ID="txbObservaciones" CssClass="form-control" Enabled="false" Visible="true" Text=" "></asp:TextBox>
        </div>
    </div>
       
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Guardar Cambios" ID="btnGuardarDatos"  OnClick="btnGuardarDatos_Click"  />
        <asp:Button runat="server" CssClass="btn btn-success" Text="Guardar Cambios" ID="btnActualizar" Visible="false" OnClick="btnActualizar_Click"/>
    
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

