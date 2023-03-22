<%@ Page Language="C#" Title="Seguimiento Infraestructura" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="InegiWeb.Seguimiento" %>

<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" runat="server">

    
      <h1>Mantenimientos Infraestructura Eléctrica</h1> 
        
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
      <asp:GridView runat="server" AutoGenerateColumns="false" OnRowDataBound="GVMantenimientos_RowDataBound" OnRowCommand="GVMantenimientos_RowCommand" AllowPaging="true" OnPageIndexChanging="GVMantenimientos_PageIndexChanging" CssClass="table table-striped" GridLines="None" ID="GVMantenimientos" Width="80%">
          <Columns>
              <asp:BoundField HeaderText="ID" DataField="IdMantenimiento"/>
              <asp:BoundField HeaderText="INMUEBLE" DataField="Inmueble"/>
              <asp:BoundField HeaderText="FECHA EVALUACIÓN" DataField="Fecha"/>
              <asp:BoundField HeaderText="TOTAL ELEMENTOS" DataField="TotalElementosInstalados"/>
              <asp:BoundField HeaderText="MAL ESTADO" DataField="Defectuosos"/>
              <asp:BoundField HeaderText="EVALUACIÓN" DataField="Evaluacion"/>
              <asp:BoundField HeaderText="COSTO TOTAL" DataField="CostoTotal"/>
              <asp:BoundField HeaderText="USUARIO" DataField="Usuario" Visible="false"/>
              <asp:ButtonField ButtonType="Button"  CommandName="Detalles" Text="Ver Resumen" ControlStyle-CssClass="btn btn-info"/>
              <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" />
              <asp:ButtonField ButtonType="Button" CommandName="Reporte" Text="Reporte" ControlStyle-CssClass="btn btn-warning"/>
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

    </asp:Content>


