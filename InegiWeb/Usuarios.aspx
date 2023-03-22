<%@ Page Language="C#" Title="Usuarios" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="InegiWeb.Usuarios" %>

<asp:Content ID="MenuUsuarios" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Estilos/Usuarios.css" type="text/css"/>
   
     <div style="text-align:center">
           <h1 >Usuarios</h1>
     </div>

    <!-- Boton para activar modal-->

<div style="display:flex; margin-bottom:20px">
    <asp:Button runat="server" Text="Agregar Usuario" CssClass="btn btn-primary"  ID="btnIngresarUsuario"  />
  
</div>


    <!-- Tabla de usuarios-->
  
           <asp:GridView ID="GVUsuario" runat="server" AllowPaging="True" OnPageIndexChanging="GVUsuario_PageIndexChanging" OnRowCommand="GVUsuario_RowCommand" AutoGenerateColumns="False" Width="70%" GridLines="None" CssClass="table table-striped" CellPadding="4" ForeColor="#333333" >
               <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <pagerstyle backcolor="#2461BF"
          height="30px"
          verticalalign="Bottom"
          horizontalalign="Center" ForeColor="White"/>
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="NOMBRES" DataField="Nombres"  />
                   <asp:BoundField HeaderText="APELLIDOS" DataField="Apellidos"/>
                   <asp:BoundField HeaderText="USUARIO" DataField="Usuario"/>
                   <asp:BoundField HeaderText="ESTADO" DataField="Estado"/>
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
               <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>  

<!-- Modal Agregar Usuarios-->
    <asp:Panel runat="server" ID="modalAgregarUsuario" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" ID="modalTitleText" Text="Ingresar Usuario" ></asp:Label>
            </div>
            <div class="modal-body">
            <h5>Nombres</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbNombres"></asp:TextBox>
            <h5>Apellidos</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbApellidos"></asp:TextBox>
            <h5>Usuario</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbUsuario"></asp:TextBox>
            <h5>Tipo de Usuario</h5>
            <asp:DropDownList runat="server" ID="TipoUsuario" CssClass="form-control">
                <asp:ListItem Selected="True" Value="Administrador">Administrador</asp:ListItem>
                <asp:ListItem Value="Tecnico"> Tecnico</asp:ListItem>
            </asp:DropDownList>
            <h5>Estado</h5>
            <asp:DropDownList runat="server" ID="Estado" CssClass="form-control">
                <asp:ListItem Selected="True" Value="Activo">Activo</asp:ListItem>
                <asp:ListItem Value="No Activo">No Activo</asp:ListItem>
            </asp:DropDownList>
            <h5>Contraseña</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbContraseña"></asp:TextBox>
            <h5>Confirmar Contraseña</h5>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txbConfContraseña"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Guardar" OnClick="btnAgregarUsuario" CssClass="btn btn-primary" ID="btnAgregar"/>
                <asp:Button runat="server" Text="Actualizar" OnClick="btnActualizar_Click" Visible="false" CssClass="btn btn-primary" ID="btnActualizar"/>
                <asp:Button runat="server" Text="Cerrar" OnClick="btnCancelar_Click" CssClass="btn btn-danger" ID="btnCancelar"/>
           </div>
       </div>
    </asp:Panel>
<!-- Modal Borrar Usuarios-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar a?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="15px" ID="nombreUsuariotxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>
<!-- Ajax Agregar Usuario-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeMostrar"
         TargetControlID="btnIngresarUsuario" 
        PopupControlID="modalAgregarUsuario" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
<!-- Ajax Borrar Usuario-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrarUsuario"
        CancelControlID="btnCancelar2" TargetControlID="nombreUsuariotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
</asp:Content>

