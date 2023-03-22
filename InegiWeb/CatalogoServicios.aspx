<%@ Page Language="C#" Title="Catalogo Servicios" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogoServicios.aspx.cs" Inherits="InegiWeb.CatalogoServicios" %>

<asp:Content runat="server" ID="MenuCatalogo" ContentPlaceHolderID="MainContent">
    <link href="Estilos/Usuarios.css" rel="stylesheet" type="text/css"/>
    <!--Cabecera -->
    <div style="text-align:center; margin-bottom:30px">
        <h1>Presupuesto de Servicios Generales</h1>
    </div>
 <div style="display:flex; padding-bottom:1em" >
        <h4>Mostrar tabla de:</h4>
         <asp:DropDownList runat="server" CssClass="form-control" ID="seleccionarTabla">
        <asp:ListItem Selected="True" Value="Materiales">Lista de Materiales</asp:ListItem>
        <asp:ListItem Value="Criterios">Lista de Criterios</asp:ListItem>
    </asp:DropDownList>
        <asp:Button runat="server" CssClass="btn btn-success" Text="Mostrar" ID="mostrar"  OnClick="mostrar_Click"/>
    </div>

    <!--Tabla de Materiales -->
     <div style="display:flex; margin-bottom:20px">
        <asp:Button runat="server" Text="Agregar Material" Visible="false" CssClass="btn btn-primary"  ID="btnIngresarMaterial" />
      
    </div>

    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="70%" Height="600px">
           <asp:GridView ID="GVMateriales" runat="server" AllowPaging="True" Visible="false" OnPageIndexChanging="GVMateriales_PageIndexChanging" Font-Size="20px" OnRowCommand="GVMateriales_RowCommand" AutoGenerateColumns="False"   Width="100%" GridLines="None" CssClass="table table-striped" CellPadding="4" ForeColor="#333333">
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
   
     <!--Tabla de Criterios -->

    <div style="display:flex; margin-bottom:20px">
        <asp:Button runat="server" Text="Agregar Criterio" Visible="false" CssClass="btn btn-primary"  ID="btnAgregarCriterio" />
      
    </div>
      <asp:GridView ID="GVCriterios" runat="server" AllowPaging="True" Visible="false" OnPageIndexChanging="GVCriterios_PageIndexChanging" Font-Size="20px" OnRowCommand="GVCriterios_RowCommand" AutoGenerateColumns="False"   Width="100%" GridLines="None" CssClass="table table-striped" CellPadding="4" ForeColor="#333333">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"  />
                   <asp:BoundField HeaderText="CATEGORIA" DataField="Categoria"/>
                  <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" ControlStyle-CssClass="btn btn-warning" >
                    <ControlStyle CssClass="btn btn-warning"></ControlStyle>
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
    <!-- Modal Agregar Material-->
 <asp:Panel runat="server" ID="modalMaterial" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Text="Agregar Material" Font-Size="15px" ID="txtTituloModal"></asp:Label>
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
            <asp:DropDownList ID="DropDownListCategorias" runat="server" CssClass="form-control">
                <asp:ListItem Value="Carpintería y Cristales">Carpintería y Cristales</asp:ListItem>
                <asp:ListItem Value="Herrería">Herrería</asp:ListItem>
                <asp:ListItem Value="Pintura">Pintura</asp:ListItem>
                <asp:ListItem Value="Instalaciones Hidrosanitarias">Instalaciones Hidrosanitarias</asp:ListItem>
                <asp:ListItem Value="Piso,Plafones y Techo">Piso,Plafones y Techo</asp:ListItem>
                <asp:ListItem Value="Otros">Otros</asp:ListItem>
            </asp:DropDownList>
            <h5>Responsable</h5>
            <asp:DropDownList ID="DropDownListResponsables" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="INEGI">INEGI</asp:ListItem>
                <asp:ListItem Value="Contrato">Contrato</asp:ListItem>
            </asp:DropDownList>
                
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Actualizar" Visible="false" CssClass="btn btn-primary" OnClick="btnActualizar_Click" ID="btnActualizar"/>
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarMaterial_Click" ID="btnAgregarMaterial"/>
                <asp:Button runat="server" Text="Cerrar" OnClick="btnCancelarMaterial_Click" CssClass="btn btn-danger" ID="btnCancelarMaterial"/>
           </div>
       </div>
    </asp:Panel>
     <!-- Modal Agregar Criterio-->
     <asp:Panel runat="server" ID="modalCriterio" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Text="Agregar Criterio" Font-Size="15px" ID="txtTituloModal2"></asp:Label>
            </div>
            <div class="modal-body">
            <h5>Criterio</h5>
            <asp:TextBox runat="server" CssClass="form-control" ID="txbCriterio"></asp:TextBox>
            <h5>Categoria</h5>
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                <asp:ListItem Value="Carpintería y Cristales">Carpintería y Cristales</asp:ListItem>
                <asp:ListItem Value="Herrería">Herrería</asp:ListItem>
                <asp:ListItem Value="Pintura">Pintura</asp:ListItem>
                <asp:ListItem Value="Instalaciones Hidrosanitarias">Instalaciones Hidrosanitarias</asp:ListItem>
                <asp:ListItem Value="Piso,Plafones y Techo">Piso,Plafones y Techo</asp:ListItem>
                <asp:ListItem Value="Otros">Otros</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Actualizar" Visible="false" CssClass="btn btn-primary" OnClick="actualizarCriterio_Click" ID="actualizarCriterio"/>
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="agregarCriterio_Click" ID="agregarCriterio"/>
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCancelarCriterio"/>
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

      <!-- Ajax Agregar Criterio-->
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpeCriterio"
     TargetControlID="btnAgregarCriterio"
     PopupControlID="modalCriterio"
     CancelControlID="btnCancelarCriterio"
     BackgroundCssClass="ModalPopupFondo" ></ajaxToolkit:ModalPopupExtender>
    <!-- Ajax Borrar Material-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrarMaterial"
        CancelControlID="btnCancelar2" TargetControlID="nombrePresupuestotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
</asp:Content>

