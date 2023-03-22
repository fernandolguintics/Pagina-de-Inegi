<%@ Page Language="C#" Title="Clasificación Almacenes" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfAlmacen.aspx.cs" Inherits="InegiWeb.ConfAlmacen" %>

<asp:Content runat="server" ID="ConfiAdmin" ContentPlaceHolderID="MainContent" >
    <link href="Estilos/Usuarios.css" rel="stylesheet" />

     <div style="text-align:center; margin-bottom:30px">
        <h1>Configuraciones de Almacenes</h1>
    </div>
    <div style="display:flex; padding-bottom:1em" >
        <h4>Mostrar tabla de:</h4>
         <asp:DropDownList runat="server" CssClass="form-control" ID="seleccionarTabla" Font-Size="18px">
        <asp:ListItem Selected="True" Value="Consumo">Consumo</asp:ListItem>
        <asp:ListItem Value="Inventarios">Inventarios</asp:ListItem>
        <asp:ListItem Value="Concentracion">Concentración</asp:ListItem>
    </asp:DropDownList>
        <asp:Button runat="server" CssClass="btn btn-success" Text="Mostrar" ID="mostrar" OnClick="mostrar_Click" Font-Size="18px"/>
    </div>
   

    <!--Tabla Consumo-->
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" Height="800px">
    <asp:Button runat="server"  CssClass="btn btn-primary" ID="btnAgregarConsumo" Text="Agregar Criterio de Consumo" Font-Size="18px" Visible="false"/>
           <asp:GridView ID="GVConsumo" runat="server" Visible="false" AllowPaging="true" OnPageIndexChanging="GVConsumo_PageIndexChanging" OnRowCommand="GVConsumo_RowCommand" Font-Size="18px" AutoGenerateColumns="False" Width="120%" GridLines="None" CssClass="table table-striped" Height="90px" CellPadding="4" ForeColor="#333333">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
                   <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" ControlStyle-CssClass="btn btn-warning" ><ControlStyle CssClass="btn btn-warning"></ControlStyle>
                   </asp:ButtonField>
                   <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" > <ControlStyle CssClass="btn btn-danger"></ControlStyle>
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

      <!--Tabla Inventario-->
    <asp:Button runat="server"  CssClass="btn btn-primary" ID="btnAgregarInventario" Text="Agregar Criterio de Inventario" Font-Size="18px" Visible="false"/>
           <asp:GridView ID="GVInventario" runat="server" Visible="false" AllowPaging="true" OnPageIndexChanging="GVInventario_PageIndexChanging" OnRowCommand="GVInventario_RowCommand" Font-Size="18px" AutoGenerateColumns="False" Width="120%" GridLines="None" CssClass="table table-striped" Height="90px" CellPadding="4" ForeColor="#333333">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
                   <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" ControlStyle-CssClass="btn btn-warning" ><ControlStyle CssClass="btn btn-warning"></ControlStyle>
                   </asp:ButtonField>
                   <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" > <ControlStyle CssClass="btn btn-danger"></ControlStyle>
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
    
     <!--Tabla Concentracion-->
    <asp:Button runat="server"  CssClass="btn btn-primary" ID="btnAgregarConcentracion" Text="Agregar Criterio de Concentración" Font-Size="17px" Width="600px" Visible="false" />
           <asp:GridView ID="GVConcentracion" runat="server" Visible="false" AllowPaging="true" OnPageIndexChanging="GVConcentracion_PageIndexChanging" OnRowCommand="GVConcentracion_RowCommand" Font-Size="18px" AutoGenerateColumns="False" Width="100%" GridLines="None" CssClass="table table-striped" Height="90px" CellPadding="4" ForeColor="#333333">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField HeaderText="ID" DataField="Id"/>
                   <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
                   <asp:BoundField HeaderText="AREA" DataField="Area"/>
                   <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" ControlStyle-CssClass="btn btn-warning" ><ControlStyle CssClass="btn btn-warning"></ControlStyle>
                   </asp:ButtonField>
                   <asp:ButtonField ButtonType="Button" CommandName="Borrar" Text="Borrar" ControlStyle-CssClass="btn btn-danger" > <ControlStyle CssClass="btn btn-danger"></ControlStyle>
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
     <!-- Modal Agregar Consumo-->
     <asp:Panel runat="server" ID="modalConsumo" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:180%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="25px" Text="Agregar Criterio" ></asp:Label>
            </div>
            <div class="modal-body">
            <h4>Ingresa el criterio:</h4>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="txbCriterio" CssClass="form-control form-control-lg" ></asp:TextBox> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Agregar"  CssClass="btn btn-primary" OnClick="btnAgregar_Click" ID="btnConsumo"/>
                <asp:Button runat="server"  Text="Actualizar"  CssClass="btn btn-primary" Visible="false" OnClick="btnActualizar_Click" ID="btnActualizarConsu" />
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger"  OnClick="btnCancelar_Click"/>
           </div>
       </div>
    </asp:Panel>
     <!-- Modal Agregar Consumo-->
     <asp:Panel runat="server" ID="modalInventario" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:180%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="25px" Text="Agregar Criterio" ></asp:Label>
            </div>
            <div class="modal-body">
            <h4>Ingresa el criterio:</h4>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="criterioInventario" CssClass="form-control form-control-lg" ></asp:TextBox> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Agregar"  CssClass="btn btn-primary" OnClick="btnAgregar_Click" ID="btnInventario"/>
                <asp:Button runat="server"  Text="Actualizar"  CssClass="btn btn-primary" Visible="false" OnClick="btnActualizar_Click" ID="btnActualizarInv" />
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger"  OnClick="btnCancelar_Click"/>
           </div>
       </div>
    </asp:Panel>
      <!-- Modal Agregar Concentracion-->
     <asp:Panel runat="server" ID="modalConcentracion" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:180%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="25px" Text="Agregar Criterio" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" ID="areatxt" Text="Seleccione la Área:" ></asp:Label>
            <asp:DropDownList runat="server" ID="seleccionarArea" CssClass="form-control">
                <asp:ListItem Selected="True" Value="Area de acervo">Área de acervo</asp:ListItem>
                <asp:ListItem Value="Procesos Tecnicos">Procesos Técnicos</asp:ListItem>
                <asp:ListItem Value="Procesos Tecnicos,Consulta y Acervo">Procesos Técnicos,Consulta y Acervo</asp:ListItem>
            </asp:DropDownList>
            <h4>Ingresa el criterio:</h4>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="criterioConcentracion" CssClass="form-control form-control-lg" ></asp:TextBox> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Agregar"  CssClass="btn btn-primary" OnClick="btnAgregar_Click" ID="btnConcentracion"/>
                <asp:Button runat="server"  Text="Actualizar"  CssClass="btn btn-primary" Visible="false" OnClick="btnActualizar_Click" ID="btnActualizarCon" />
                <asp:Button runat="server" Text="Cancelar" OnClick="btnCancelar_Click"  CssClass="btn btn-danger" ID="btnCancelar"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Modal Borrar Criterio-->
     <asp:Panel runat="server" ID="modalBorrar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="¿Quieres borrar el criterio?" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="20px" ID="tipoCriteriotxt" ></asp:Label> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server"  Text="Borrar"  CssClass="btn btn-primary" OnClick="btnBorrar_Click" ID="btnBorrar"/>
                <asp:Button runat="server" Text="Cancelar"  CssClass="btn btn-danger" ID="Button1"/>
           </div>
       </div>
    </asp:Panel>

    <!-- Ajax Concentracion-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeConcentra"
         TargetControlID="btnAgregarConcentracion" 
        PopupControlID="modalConcentracion" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
    <!-- Ajax Inventario-->
     <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeInventario"
         TargetControlID="btnAgregarInventario" 
        PopupControlID="modalInventario" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
    <!-- Ajax Concentracion-->
     <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeConsumo"
         TargetControlID="btnAgregarConsumo" 
        PopupControlID="modalConsumo" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>

    <!-- Ajax Borrar Criterio-->
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeBorrar"
         TargetControlID="tipoCriteriotxt" 
        PopupControlID="modalBorrar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
</asp:Content>