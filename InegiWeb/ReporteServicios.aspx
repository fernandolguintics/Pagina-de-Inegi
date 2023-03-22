<%@ Page Language="C#" Title="INEGI - Crear Reporte Servicios" AutoEventWireup="true" CodeBehind="ReporteServicios.aspx.cs" Inherits="InegiWeb.ReporteServicios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
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
          <p style="font-size:20px; font-weight:bold">NOTA: Es necesario llenar todos los cuadros que esten arreglados. En caso que no se halla arreglado nada poner un 0.</p>
         <p style="font-size:20px; font-weight:bold">Adjunta los archivos antes de guardar y escribe los detalles del reporte.</p>   
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
       
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
      <h4> Materiales de Carpinteria y Cristales
    </h4>
   <asp:GridView runat="server" ID="GVCarpinteria" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


         <!--Tabla de Materiales de Herreria--> 
    <h4>Materiales de Herreria</h4>
   <asp:GridView runat="server" ID="GVHerreria" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
      <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

         <!--Tabla de Materiales de Pintura--> 
    <h4> Materiales de Pintura</h4>
   <asp:GridView runat="server" ID="GVPintura" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
       <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
                 <!--Tabla de Materiales de Hidrosanitarias--> 
    <h4> Materiales Hidrosanitarias</h4>
   <asp:GridView runat="server" ID="GVHidrosanitarias" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
       <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

                 <!--Tabla de Materiales de Pisos--> 
    <h4> Materiales de Pisos, Plafones y Techo</h4>
   <asp:GridView runat="server" ID="GVPisos" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
      <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
           <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

                 <!--Tabla de Materiales de Pintura--> 
    <h4> Otros Materiales</h4>
   <asp:GridView runat="server" ID="GVOtros" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
        <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso" Text='<%# Bind("Piso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal" Text='<%# Bind("Cantidad") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado" Text='<%# Bind("MalEstado") %>' Enabled="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ARREGLADOS">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="arreglados"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa" Text='<%# Bind("CambioNormativa") %>'></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button runat="server" CssClass="btn btn-primary" Text="Guardar Reporte" ID="btnGuardar" OnClick="btnGuardar_Click"/>

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

