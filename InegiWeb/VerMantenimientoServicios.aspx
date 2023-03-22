<%@ Page Language="C#" Title="INEGI - Mantenimiento Servicio" AutoEventWireup="true" CodeBehind="VerMantenimientoServicios.aspx.cs" Inherits="InegiWeb.VerMantenimientoServicios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="../Content/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="Estilos/Usuarios.css" type="text/css"/>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/scripts/listaMateriales.js"></script>
    <title></title>
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
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:Label runat="server" ID="txtFecha" Font-Size="17px"></asp:Label>
        <br /> <br />
   <asp:Button runat="server" ID="btnMateriales" Text="Ver Materiales" CssClass="btn btn-primary" />

           <h4>Materiales de Carpinteria y Cristales</h4>
    <asp:GridView runat="server" ID="GVCarpinteria" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
       <h4>Materiales de Herreria</h4>
    <asp:GridView runat="server" ID="GVHerreria" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
          <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
        <h4>Materiales de Pintura</h4>
    <asp:GridView runat="server" ID="GVPintura" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
        <h4>Materiales Hidrosanitarias</h4>
    <asp:GridView runat="server" ID="GVHidrosanitarias" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
          <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
         <h4>Materiales de Piso,Plafones y Techo</h4>
    <asp:GridView runat="server" ID="GVPisos" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
         <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
         <h4>Otros Materiales</h4>
    <asp:GridView runat="server" ID="GVOtros" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
          <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>

         <div style="padding-bottom:1em">
            <fieldset style="width: 700px;">
                <legend>Lista de Materiales para el Mantenimiento</legend>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeyup="SearchEmployees(this,'#cblMateriales');"
                    placeholder="Search employee">
                </asp:TextBox>
                <span id="spnCount"></span>
                <div style="height:300px; overflow-y: auto; overflow-x: hidden">
                    <asp:CheckBoxList ID="cblMateriales" runat="server" RepeatColumns="1"
                        RepeatDirection="Vertical" Width="400px" ClientIDMode="Static">
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </fieldset>           
        </div>
        <asp:Button runat="server" Text="Ordenar Materiales" CssClass="btn btn-primary" ID="btnOrdenar" OnClick="btnOrdenar_Click" />
         <!-- Modal Seleccionar Inmueble-->
           <asp:Panel runat="server" ID="modalMostrarMantenimiento" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <h3 class="modal-title">Lista de Adquisición de Compra</h3>
            </div>
            <div class="modal-body">
            <asp:GridView runat="server" ID="GVMateriales" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="table table-striped">
                 <Columns>
                    <asp:BoundField HeaderText="No.MATERIAL" DataField ="Id"/>
                    <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
                     <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
                     <asp:TemplateField HeaderText="CANTIDAD">
                         <ItemTemplate>
                             <asp:TextBox runat="server" ID="txbCantidad" CssClass="form-control"></asp:TextBox>
                         </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Cancelar" CssClass="btn btn-danger" ID="btnCancelar"/>
                <asp:Button  runat="server" Text="Siguiente" CssClass="btn btn-primary" ID="btnCrear"  OnClick="btnCrear_Click"/>
           </div>
       </div>
    </asp:Panel>
        <!-- Ajax Seleccionar Inmueble-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeMostar"
             TargetControlID="GVMateriales" CancelControlID="btnCancelar"
             PopupControlID="modalMostrarMantenimiento" BackgroundCssClass="ModalPopupFondo"
        ></ajaxToolkit:ModalPopupExtender>
        <!-- Modal Guardar Lista-->
          <asp:Panel runat="server" ID="modalGuardar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <h3 class="modal-title">Total de Adquisicion de Compra</h3>
            </div>
            <div class="modal-body">
            <asp:GridView runat="server" ID="GVTotal2" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="table table-striped">
                 <Columns>
                    <asp:BoundField HeaderText="No.MATERIAL" DataField ="Id"/>
                    <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
                     <asp:BoundField HeaderText="CANTIDAD" DataField="Cantidad"/>
                    <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
                </Columns>
            </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Label Font-Bold="true" Font-Size="20px" runat="server" ID="txtTotal2"></asp:Label>  
                 <asp:Button runat="server" Text="Regresar" CssClass="btn btn-danger" ID="btnAtras2" OnClick="btnAtras2_Click"/>
                <asp:Button  runat="server" Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click"/>
           </div>
       </div>
    </asp:Panel>
<!-- Ajax GuardarLista-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeTotal2"
            TargetControlID="txtTotal2" PopupControlID="modalGuardar"
             BackgroundCssClass="ModalPopupFondo"
         ></ajaxToolkit:ModalPopupExtender>
        <!-- Modal Seleccionar Inmueble-->
         <asp:Panel runat="server" ID="modalGuardarMateriales" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <h3 class="modal-title">Lista de Materiales para Realizar el Mantenimiento</h3>
            </div>
            <div class="modal-body">
            <asp:GridView runat="server" ID="GVTotal" OnRowCommand="GVTotal_RowCommand" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="table table-striped">
                 <Columns>
                     <asp:BoundField HeaderText="ID" DataField="Id"/>
                    <asp:BoundField HeaderText="MATERIAL" DataField="NombreMaterial"/>
                     <asp:BoundField DataField="UnidadMedida" Visible="false"/>
                     <asp:BoundField DataField="Responsable" Visible="false"/>
                     <asp:BoundField HeaderText="FECHA DE PEDIDO" DataField="Fecha"/>
                     <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
                    <asp:BoundField HeaderText="CANTIDAD" DataField="Cantidad"/>
                      <asp:BoundField HeaderText="SUBTOTAL" DataField="SubTotal"/>
                     <asp:BoundField DataField="Subtotal" Visible="false"/>
                      <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar"  ControlStyle-CssClass="btn btn-danger" />
                </Columns>
            </asp:GridView>
            <asp:Label Font-Bold="true" Font-Size="20px" runat="server" ID="txtTotal"></asp:Label>   
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Salir" CssClass="btn btn-danger" ID="btnAtras"/>
           </div>
       </div>
    </asp:Panel>
        <!-- AJAX Seleccionar Inmueble-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeTotal"
             TargetControlID="btnMateriales" CancelControlID="btnAtras"
             PopupControlID="modalGuardarMateriales" BackgroundCssClass="ModalPopupFondo"
            ></ajaxToolkit:ModalPopupExtender>
<!-- Modal Borrar Mantenimiento-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar el mantenimiento?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="15px" ID="nombreMaterialtxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrar"
             TargetControlID="nombreMaterialtxt" CancelControlID="btnCancelar2"
             PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo"
            ></ajaxToolkit:ModalPopupExtender>
        
    </form>
</body>
</html>
