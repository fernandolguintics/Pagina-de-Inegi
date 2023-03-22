<%@ Page Language="C#" Title="Configuración de Administrador" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfAdmi.aspx.cs" Inherits="InegiWeb.ConfAdmi" %>

<asp:Content runat="server" ID="ConfiAdmin" ContentPlaceHolderID="MainContent">
    <link href="Estilos/Usuarios.css" rel="stylesheet" type="text/css"/>
    <div style="text-align:center; margin-bottom:30px">
        <h1>Configuraciones de Administrador</h1>
    </div>

    <asp:Button runat="server" Text="Agregar Nuevo Inmueble" CssClass="btn btn-primary"  ID="btnIngresarInmueble"/>
  
    <!--Tabla Inmuebles-->
   
           <asp:GridView ID="GVEdificios" runat="server" AllowPaging="true" OnPageIndexChanging="GVEdificios_PageIndexChanging" OnRowCommand="GVEdificios_RowCommand" AutoGenerateColumns="False" Width="45%" GridLines="None" CssClass="table table-striped" Height="90px" CellPadding="4" ForeColor="#333333">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="EDIFICIOS" DataField="Edificio"/>
                   <asp:CommandField ButtonType="Button" ShowSelectButton="true"  SelectText="Ver" ControlStyle-CssClass="btn btn-primary ">
<ControlStyle CssClass="btn btn-primary "></ControlStyle>
                   </asp:CommandField>
                   <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" ControlStyle-CssClass="btn btn-warning" >
<ControlStyle CssClass="btn btn-warning"></ControlStyle>
                   </asp:ButtonField>
                   <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" > 
<ControlStyle CssClass="btn btn-danger"></ControlStyle>
                   </asp:ButtonField>
               </Columns>
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>  
     

    <!--Modal Inmuebles-->
    <asp:Panel runat="server" ID="modalMostrarInmueble" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:450px">
            <div class="modal-header">
               <asp:Label runat="server" Font-Size="17px" Text="Ingresar Datos" ID="nombreTituloModal"></asp:Label>
            </div>
            <div class="modal-body" >
             <table>
                 <tr>
                     <td style="padding-right:30px">
                         <h5>Nombre de Inmueble</h5>
                         <asp:TextBox runat="server" CssClass="form-control" ID="txbNomInmueble"></asp:TextBox>
                         <h5>Dirección</h5>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txbDireccion"></asp:TextBox>
                         <h5>Superficie Arrendada</h5>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txbSuperficie"></asp:TextBox>
                         <h5>Niveles</h5>
                         <asp:TextBox runat="server" CssClass="form-control" ID="txbNiveles"></asp:TextBox>
                         <h5>Antiguedad</h5>
                         <asp:TextBox runat="server" CssClass="form-control" ID="txbAntiguedad"></asp:TextBox>
                     </td>
                     <td>
                        <h5>Tipo de Posesión</h5>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txbPosesion"></asp:TextBox>
                         <h5>Año de Ocupación</h5>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txbOcupacion"></asp:TextBox>
                         <h5>Uso</h5>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txbUso"></asp:TextBox>
                         <h5>Cajones de Estacionamiento</h5>
                         <asp:TextBox runat="server" CssClass="form-control" ID="txbCajones"></asp:TextBox>
                         <h5>Terreno</h5>
                         <asp:TextBox runat="server" CssClass="form-control" ID="txbTerreno"></asp:TextBox> 
                     </td>
                 </tr>
                
             </table> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Actualizar" Visible="false" CssClass="btn btn-primary" OnClick="btnActualizar_Click" ID="btnActualizar"/>
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarInmueble_Click"  ID="btnAgregarInmueble"/>
                <asp:Button runat="server" Text="Cerrar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" ID="btnCancelar"/>
           </div>
       </div>
    </asp:Panel>
<!-- Modal Borrar Edificios-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar a?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="20px" ID="nombreEdificiotxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>
<!-- Ajax Agregar Inmueble-->
<ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar"
     TargetControlID="btnIngresarInmueble"
     PopupControlID="modalMostrarInmueble"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>
<!-- Ajax Borrar Edificio-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrarEdificio"
         TargetControlID="nombreEdificiotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
</asp:Content>
