<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="InegiWeb.Menu" %>

<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" runat="server">
     <link href="Estilos/Usuarios.css" rel="stylesheet" type="text/css"/>
        <div style="text-align:center; align-items:center">
            <h1 >Bienvenido</h1>
            <asp:Label runat="server"  Text="Nombre del usuario" Font-Size="20px" ID="txtnombre"></asp:Label>
            <asp:Label runat="server" ></asp:Label>
           
        </div>
    <div style="display:flex">
        <div class="card" style="width: 25rem;">
            <div class="card-body">
                <h3 class="card-title">Mantenimiento a Instalación Eléctrica</h3>
                <p class="card-text">Crear nuevo mantenimiento</p>
                 <asp:Button runat="server" Text="Crear" ID="btnCrearMantenimiento" CssClass="btn btn-success" Font-Size="15px"/>
            </div>
       </div>
        
        <div style="padding-left:1em">
             <div class="card" style="width: 25rem;">
                <div class="card-body">
                    <h3 class="card-title">Mantenimiento a Servicios Generales</h3>
                    <p class="card-text">Crear nuevo mantenimiento</p>
                    <asp:Button runat="server" Text="Crear" ID="btnCrearMantenimientoServicio" CssClass="btn btn-success" Font-Size="15px"/>
                </div>
            </div>
        </div>
        <div style="padding-left:1em">
             <div class="card" style="width: 25rem;">
                <div class="card-body">
                    <h3 class="card-title">Mantenimiento a Almacenes</h3>
                    <p class="card-text">Crear nuevo mantenimiento</p>
                    <asp:Button runat="server" Text="Crear" ID="btnCrearMantenimientoAlmacenes" CssClass="btn btn-success" Font-Size="15px"/>
                </div>
            </div>
        </div>
    </div>
        
    



<!-- Modal Seleccionar Inmueble-->
    <asp:Panel runat="server" ID="modalMostrarMantenimiento" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:60%">
            <div class="modal-header">
                <h3 class="modal-title">Agregar Mantenimiento Infraestructura Electrica</h3>
            </div>
            <div class="modal-body">
            <h4>Seleccione el Inmueble:</h4>
            <asp:DropDownList ID="DropDownListInmuebles" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Cancelar" CssClass="btn btn-danger" ID="btnCancelar"/>
                <asp:Button  runat="server" Text="Crear" CssClass="btn btn-primary" ID="Crear" OnClick="Crear_Click"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Modal Seleccionar Inmueble-->
    <asp:Panel runat="server" ID="modalSeleccionarInmueble" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:60%">
            <div class="modal-header">
                <h3 class="modal-title">Agregar Mantenimiento Servicios Generales</h3>
            </div>
            <div class="modal-body">
            <h4>Seleccione el Inmueble:</h4>
            <asp:DropDownList ID="DropDownListInmuebles2" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Cancelar" CssClass="btn btn-danger" ID="btnCancelar2"/>
                <asp:Button  runat="server" Text="Crear" CssClass="btn btn-primary" ID="btnCrearServicio" OnClick="btnCrearServicio_Click" />
           </div>
       </div>
    </asp:Panel>
     <!-- Modal Seleccionar Inmueble-->
    <asp:Panel runat="server" ID="modalseleccionarAlmacen" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:60%">
            <div class="modal-header">
                <h3 class="modal-title">Crear Mantenimiento de Almacen</h3>
            </div>
            <div class="modal-body">
            <h4>Seleccione el Inmueble:</h4>
            <asp:DropDownList ID="DropDownListInmuebles3" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Cancelar" CssClass="btn btn-danger" ID="btnCancelar3"/>
                <asp:Button  runat="server" Text="Crear" CssClass="btn btn-primary" ID="btnMantenimientoAlmacen" OnClick="btnMantenimientoAlmacen_Click" />
           </div>
        </div>
    </asp:Panel>
    
<ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar"
     TargetControlID="btnCrearMantenimiento"
     CancelControlID="btnCancelar"
     PopupControlID="modalMostrarMantenimiento"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>

<ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar2"
     TargetControlID="btnCrearMantenimientoServicio"
     CancelControlID="btnCancelar2"
     PopupControlID="modalseleccionarInmueble"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>

    <ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar3"
     TargetControlID="btnCrearMantenimientoAlmacenes"
     CancelControlID="btnCancelar3"
     PopupControlID="modalSeleccionarAlmacen"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>
</asp:Content>