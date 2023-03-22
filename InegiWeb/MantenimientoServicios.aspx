<%@ Page Language="C#" Title="INEGI - Mantenimiento Servicios" AutoEventWireup="true" CodeBehind="MantenimientoServicios.aspx.cs" Inherits="InegiWeb.MantenimientoServicios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/scripts/listaMateriales.js"></script>
      <link rel="stylesheet" href="Estilos/Usuarios.css" type="text/css"/>
    <title>Mantenimiento a Inmuebles</title>
</head>
<body>
    <form id="form1" runat="server">
      <div style="text-align:center; align-items:center">
       <h2>Mantenimiento a Servicios Generales</h2>
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
   <table>
        <tr>
            <td>
                <h5>Fecha de Evaluacion</h5>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </td>
        </tr>
    </table>
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <!--Tabla de Materiales de Carpinteria y Cristales--> 
    <h4> Materiales de Carpinteria y Cristales
    </h4>
   <asp:GridView runat="server" ID="GVCarpinteria" AutoGenerateColumns="false" Width="80%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdCriterio"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
             <asp:TemplateField HeaderText="PISO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="piso"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD TOTAL">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="cantidadTotal"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAL ESTADO">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="malEstado"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CAMBIO OBLIGACION NORMATIVA">
                <ItemTemplate>
                    <asp:Textbox runat="server" ID="cambioNormativa"></asp:Textbox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACIONES">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observaciones"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

         <div style="padding-bottom:1em">
            <fieldset style="width: 600px;">
                <legend>Lista de Materiales para el Mantenimiento</legend>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeyup="SearchEmployees(this,'#cblMateriales');"
                    placeholder="Search employee">
                </asp:TextBox>
                <span id="spnCount"></span>
                <div style="height:300px; overflow-y: auto; overflow-x: hidden">
                    <asp:CheckBoxList ID="cblMateriales" runat="server" RepeatColumns="1"
                        RepeatDirection="Vertical" Width="300px" ClientIDMode="Static">
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </fieldset>           
        </div>

    <asp:Button runat="server" CssClass="btn btn-primary" Text="Siguiente" ID="btnGuardar" OnClick="btnGuardar_Click"/>

        
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
                <asp:Button  runat="server" Text="Siguiente" CssClass="btn btn-primary" ID="btnCrear" OnClick="btnCrear_Click"/>
           </div>
       </div>
    </asp:Panel>

     <!-- Modal Seleccionar Inmueble-->
         <asp:Panel runat="server" ID="modalGuardarMateriales" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <h3 class="modal-title">Total de Adquisición de Compra</h3>
            </div>
            <div class="modal-body">
            <asp:GridView runat="server" ID="GVTotal" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="table table-striped">
                 <Columns>
                    <asp:BoundField HeaderText="No.MATERIAL" DataField ="Id"/>
                    <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
                    <asp:BoundField HeaderText="CANTIDAD" DataField="Cantidad"/>
                      <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal"/>
                </Columns>
            </asp:GridView>
            <asp:Label Font-Bold="true" Font-Size="20px" runat="server" ID="txtTotal"></asp:Label>   
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Regresar" CssClass="btn btn-danger" ID="btnAtras" OnClick="btnGuardar_Click"/>
                <asp:Button  runat="server" Text="Guardar" CssClass="btn btn-primary" ID="btnGuardarMantenimiento" OnClick="btnGuardarMantenimiento_Click"/>
           </div>
       </div>
    </asp:Panel>   


     <ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar"
     TargetControlID="GVMateriales"
     CancelControlID="btnCancelar"
     PopupControlID="modalMostrarMantenimiento"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>

     <ajaxToolkit:ModalPopupExtender runat="server" id="mpeTotal"
     TargetControlID="txtTotal"
     PopupControlID="modalGuardarMateriales"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>
