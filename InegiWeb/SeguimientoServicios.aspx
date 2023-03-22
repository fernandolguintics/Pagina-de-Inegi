<%@ Page Language="C#" Title="Seguimiento Servicios" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeguimientoServicios.aspx.cs" Inherits="InegiWeb.SeguimientoServicios" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Mantenimientos Servicios Generales</h1> 
        
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
      <asp:GridView runat="server" AutoGenerateColumns="false" OnRowDataBound="GVMantenimientoServicios_RowDataBound" OnRowCommand="GVMantenimientoServicios_RowCommand" AllowPaging="true" OnPageIndexChanging="GVMantenimientoServicios_PageIndexChanging" CssClass="table table-striped" GridLines="None" ID="GVMantenimientoServicios" Width="85%">
          <Columns>
              <asp:BoundField HeaderText="ID" DataField="IdMantenimiento"/>
              <asp:BoundField HeaderText="INMUEBLE" DataField="Inmueble"/>
              <asp:BoundField HeaderText="FECHA EVALUACIÓN" DataField="Fecha"/>
              <asp:BoundField HeaderText="EVALUACIÓN" DataField="Evaluacion"/>
              <asp:BoundField HeaderText="COSTO" DataField="Costo"/>
              <asp:BoundField HeaderText="USUARIO" DataField="Usuario" Visible="false"/>
              <asp:ButtonField ButtonType="Button"  CommandName="Detalles" Text="Ver Resumen" ControlStyle-CssClass="btn btn-info"/>
               <asp:ButtonField ButtonType="Button" CommandName="Reporte" Text="Crear Reporte" ControlStyle-CssClass="btn btn-success"/>
               <asp:ButtonField ButtonType="Button" CommandName="Imprimir" Text="Imprimir" ControlStyle-CssClass="btn btn-warning"/>
              <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" />
             
          </Columns>
      </asp:GridView>


      <!-- Modal Borrar Mantenimiento-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar el mantenimiento?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="15px" ID="nombreMantenimientotxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>
<!-- Ajax Borrar Mantenimiento-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrar"
        CancelControlID="btnCancelar2" TargetControlID="nombreMantenimientotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
       <!-- Imprimir Materiales-->
     <asp:Panel runat="server" ID="modalImprimir" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%; height:100%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="30px" Text="Reporte" ></asp:Label>
            </div>
            <div class="modal-body" style="height:100%" >
                <asp:Panel runat="server" ScrollBars="Auto" Width="100%" Height="500px">
                     <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowZoomControl="false" ClientIDMode="AutoID" Height="500px" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="" LinkActiveColor="" LinkActiveHoverColor="" Width="1033px">
                    <LocalReport ReportPath="ReporteServicios.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                </asp:Panel>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCerrar"/>
           </div>
       </div>
    </asp:Panel>
          <!--AJAX Imprimir Materiales-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeImprimir"
             TargetControlID="ReportViewer1" CancelControlID="btnCerrar"
             PopupControlID="modalImprimir" BackgroundCssClass="ModalPopupFondo"
            ></ajaxToolkit:ModalPopupExtender>
</asp:Content>
