<%@ Page Language="C#" Title="Catalogo Infraestructura" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogoInfraestructura.aspx.cs" Inherits="InegiWeb.CatalogoInfraestructura" %>

<asp:Content runat="server" ID="MenuPresupuesto" ContentPlaceHolderID="MainContent" >
    <link href="Estilos/Usuarios.css" rel="stylesheet" type="text/css"/>
    <!--Cabecera -->

    <div style="text-align:center; margin-bottom:30px">
        <h1>Presupuesto de Infraestructura Eléctrica</h1>
    </div>
    
    <div style="display:flex; margin-bottom:20px">
        <asp:Button runat="server" Text="Agregar Material" CssClass="btn btn-primary"  ID="btnIngresarMaterial" Font-Size="20px" />
        <asp:Button runat="server" Text="Agregar Responsable" CssClass="btn btn-secondary"  ID="btnIngresarResponsable" Visible="false" style="position:absolute; left:300px" />
        <asp:Button runat="server" Text="Agregar Categoria" CssClass="btn btn-secondary" Visible="false"  ID="btnIngresarCategoria" style="position:absolute; left:150px" />
        <!--<asp:Button runat="server" Text="Buscar"  OnClick="Buscar_Click" CssClass="btn btn-success"  style="position:absolute; right:300px"/> 
        <asp:TextBox runat="server"  CssClass="form-control" ID="txbBuscar" style="position:absolute; right:10px"></asp:TextBox> -->
    </div>
  

    <!--Tabla de Materiales -->
 <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="80%" Height="500px">    
           <asp:GridView ID="GVMateriales" runat="server" AllowPaging="True" OnPageIndexChanging="GVMateriales_PageIndexChanging" OnRowCommand="GVMateriales_RowCommand" AutoGenerateColumns="False"   Width="100%" GridLines="None" CssClass="table table-striped" CellPadding="4" ForeColor="#333333" Font-Size="20px">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="MATERIAL" DataField="Material"  />
                   <asp:BoundField HeaderText="CATEGORIA" DataField="Categoria"/>
                   <asp:BoundField HeaderText="PRECIO" DataField="Precio"/>
                   <asp:ButtonField ButtonType="Button"  CommandName="Select" Text="Ver" ControlStyle-CssClass="btn btn-primary" >
<ControlStyle CssClass="btn btn-primary"></ControlStyle>
                   </asp:ButtonField>
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
</asp:Panel>
<!-- Modal Agregar Responsable-->
    <asp:Panel runat="server" ID="modalMostrarResponsable" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <h3 class="modal-title">Ingresar Responsable</h3>
            </div>
            <div class="modal-body">
            <h5>Responsable</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbResponsable"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarResponsable_Click" ID="btnAgregarResponsable"/>
                <asp:Button runat="server" Text="Cerrar" CssClass="btn btn-danger" ID="btnCancelar"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Ajax Agregar Responsable-->
<ajaxToolkit:ModalPopupExtender runat="server" id="mpeMostrar"
     TargetControlID="btnIngresarResponsable"
     CancelControlID="btnCancelar"
     PopupControlID="modalMostrarResponsable"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>

    <!-- Modal Agregar Categoria-->
    <asp:Panel runat="server" ID="modalCategoria" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <h3 class="modal-title">Ingresar Categoria</h3>
            </div>
            <div class="modal-body">
            <h5>Categoria</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbCategoria"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarCat_Click" ID="btnAgregarCat"/>
                <asp:Button runat="server" Text="Cerrar" CssClass="btn btn-danger" ID="btnCancelarCategoria"/>
           </div>
       </div>
    </asp:Panel>

     <!-- Ajax Agregar Categoria-->
<ajaxToolkit:ModalPopupExtender runat="server" id="mpeCategoria"
     TargetControlID="btnIngresarCategoria"
     CancelControlID="btnCancelarCategoria"
     PopupControlID="modalCategoria"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>
     
    <!-- Modal Agregar Material-->
 <asp:Panel runat="server" ID="modalMaterial" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Text="Agregar Material" Font-Size="30px" ID="txtTituloModal"></asp:Label>
            </div>
            <div class="modal-body">
            <h5>Nombre del Material</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbNombreMat"></asp:TextBox>
            <h5>Descripcion</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbDescripcion"></asp:TextBox>
            <h5>Precio</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbPrecio"></asp:TextBox>
            <h5>Unidad de Medida</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbUnidadMedida"></asp:TextBox>
            <h5>Categoria</h5>
            <asp:DropDownList ID="DropDownListCategorias" runat="server" CssClass="form-control"></asp:DropDownList>
            <h5>Responsable</h5>
            <asp:DropDownList ID="DropDownListResponsables" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Actualizar" Visible="false" CssClass="btn btn-primary" OnClick="btnActualizar_Click" ID="btnActualizar"/>
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="BtnAgregarMaterial_Click" ID="btnAgregarMaterial"/>
                <asp:Button runat="server" Text="Cerrar" OnClick="btnCancelarMaterial_Click" CssClass="btn btn-danger" ID="btnCancelarMaterial"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Modal Borrar Presupuesto-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar a?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="15px" ID="nombrePresupuestotxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Ajax Agregar Material-->
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpeMaterial"
     TargetControlID="btnIngresarMaterial"
     PopupControlID="modalMaterial"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>

    <!-- Ajax Borrar Material-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrarMaterial"
        CancelControlID="btnCancelar2" TargetControlID="nombrePresupuestotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
</asp:Content>
