<%@ Page Language="C#" Title="INEGI - Validación Reporte" AutoEventWireup="true" CodeBehind="ValidacionReporteServicio.aspx.cs" Inherits="InegiWeb.ValidacionReporteServicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
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

          <h4>Materiales de Carpinteria y Cristales</h4>
    <asp:GridView runat="server" ID="GVCarpinteria" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
          <!--Tablas de Actualizacion-->
         <asp:GridView runat="server" ID="GVCarpinteria2" Visible="false" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
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

       <h4>Materiales de Herreria</h4>
    <asp:GridView runat="server" ID="GVHerreria" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
          <!--Tablas de Actualizacion-->
         <asp:GridView runat="server" ID="GVHerreria2" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
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


        <h4>Materiales de Pintura</h4>
    <asp:GridView runat="server" ID="GVPintura" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
          <!--Tablas de Actualizacion-->
          <asp:GridView runat="server" ID="GVPintura2" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
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

        <h4>Materiales Hidrosanitarias</h4>
    <asp:GridView runat="server" ID="GVHidrosanitarias" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
          <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
          <!--Tablas de Actualizacion-->
        
         <asp:GridView runat="server" ID="GVHidrosanitarias2" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
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

         <h4>Materiales de Piso,Plafones y Techo</h4>
    <asp:GridView runat="server" ID="GVPisos" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
          <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
          <!--Tablas de Actualizacion-->
         <asp:GridView runat="server" ID="GVPisos2" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
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

         <h4>Otros Materiales</h4>
    <asp:GridView runat="server" ID="GVOtros" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField  DataField="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:BoundField HeaderText="PISO" DataField="Piso"/>
            <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="MalEstado"/>
            <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
             <asp:BoundField HeaderText="CAMBIO POR NORMATIVA" DataField="CambioNormativa"/>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
        </Columns>
    </asp:GridView>
        <!--Tablas de Actualizacion-->
         <asp:GridView runat="server" ID="GVOtros2" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
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

        <div style="display:flex; padding-top:1em; width:50%">
        <div style="display:flex">
            <h5>Observaciones:</h5>
        </div>
        <div style="padding-left:6em">
             <asp:TextBox runat="server" TextMode="MultiLine" ID="txbObservaciones" CssClass="form-control" Visible="true" Text=" "></asp:TextBox>
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
