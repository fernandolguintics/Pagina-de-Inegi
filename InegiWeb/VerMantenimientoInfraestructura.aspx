<%@ Page Title="INEGI - Ver Mantenimiento Infraestructura" Language="C#" AutoEventWireup="true" CodeBehind="VerMantenimientoInfraestructura.aspx.cs" Inherits="InegiWeb.VerMantenimientoInfraestructura" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
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
        
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        
   <asp:Label runat="server" ID="txtFecha" Font-Size="17px"></asp:Label>

           <h4>Illuminacion</h4>
        <asp:Button runat="server" CssClass="btn btn-danger" Text="Imprimir Reporte" OnClick="Unnamed_Click" />
    <asp:GridView runat="server" ID="GVMaterialesIlluminacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
           <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField DataField="Precio" Visible="false"/>
            <asp:BoundField DataField="Responsable" Visible="false"/>
            <asp:BoundField HeaderText="UNIDAD" DataField="Unidad"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
             <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad Total"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="Mal Estado"/>
             <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal1"/>
            <asp:BoundField DataField="Subtotal2" Visible="false"/>
        </Columns>
    </asp:GridView>
         <h4>Instalacion Electrica</h4>
    <asp:Button runat="server" CssClass="btn btn-danger" Text="Imprimir Reporte" ID="GenerarReporte" OnClick="GenerarReporte_Click" />
    <asp:GridView runat="server" ID="GVMaterialesInstalacion" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
           <asp:BoundField HeaderText="MATERIAL" DataField="Material"/>
            <asp:BoundField HeaderText="PRECIO" DataField="Precio" Visible="false"/>
            <asp:BoundField DataField="Responsable" Visible="false"/>
             <asp:BoundField HeaderText="UNIDAD" DataField="Unidad"/>
            <asp:BoundField HeaderText="PISO" DataField="Piso"/>
             <asp:BoundField HeaderText="CANTIDAD TOTAL" DataField="Cantidad Total"/>
             <asp:BoundField HeaderText="MAL ESTADO" DataField="Mal Estado"/>
             <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal1"/>
            <asp:BoundField DataField="Subtotal2" Visible="false"/>
        </Columns>
    </asp:GridView>

        <!-- Imprimir Illuminacion-->

     <asp:Panel runat="server" ID="modalImprimir" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%; height:100%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="Reporte" ></asp:Label>
            </div>
            <div class="modal-body" style="height:100%" >
                <asp:Panel runat="server" ScrollBars="Auto" Width="100%" Height="500px">
                     <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowZoomControl="false" ClientIDMode="AutoID" Height="500px" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="" LinkActiveColor="" LinkActiveHoverColor="" Width="1033px">
                   <LocalReport ReportPath="ReporteInfraestructura.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                </asp:Panel>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCerrar"/>
           </div>
       </div>
    </asp:Panel>
         <!-- Imprimir Instalacion-->
        
     <asp:Panel runat="server" ID="modalImprimir2" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%; height:100%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="Reporte" ></asp:Label>
            </div>
            <div class="modal-body" style="height:100%" >
                
                <asp:Panel runat="server" ScrollBars="Auto" Width="100%" Height="500px">
                     <rsweb:ReportViewer ID="ReportViewer2" runat="server" ShowZoomControl="False" ClientIDMode="AutoID" Height="500px" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="" LinkActiveColor="" LinkActiveHoverColor="" Width="1033px" BackColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                    <LocalReport ReportPath="ReporteInfraestructura.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                </asp:Panel>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCerrar2"/>
           </div>
       </div>
    </asp:Panel>
<!-- Ajax Imprimir Illuminacion-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeImprimir"
         TargetControlID="ReportViewer1"
         CancelControlID="btnCerrar"
        PopupControlID="modalImprimir" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>

        <!-- Ajax Imprimir Instalacion-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeImprimir2"
         TargetControlID="ReportViewer2"
         CancelControlID="btnCerrar2"
        PopupControlID="modalImprimir2" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>
