<%@ Page Language="C#" Title="Seguimiento Almacenes" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeguimientoAlmacenes.aspx.cs" Inherits="InegiWeb.SeguimientoAlmacenes" %>

<asp:Content ID="MenuUsuarios" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="text-align:center">Mantenimientos a Almacenes</h1> 
        
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
      <asp:GridView runat="server" AutoGenerateColumns="false" OnRowDataBound="GVMantenimientoAlmacenes_RowDataBound" OnRowCommand="GVMantenimientoAlmacenes_RowCommand" OnPageIndexChanging="GVMantenimientoAlmacenes_PageIndexChanging" CssClass="table table-striped" GridLines="None" ID="GVMantenimientoAlmacenes" Width="70%">
          <Columns>
              <asp:BoundField HeaderText="ID" DataField="IdMantenimiento"/>
              <asp:BoundField HeaderText="INMUEBLE" DataField="NombreEdificio"/>
              <asp:BoundField HeaderText="FECHA EVALUACIÓN" DataField="Fecha"/>
              <asp:BoundField HeaderText="ULTIMA ACTUALIZACIÓN" DataField="FechaActualizacion"/>
              <asp:BoundField HeaderText="CONSUMO" DataField="Consumo"/>
              <asp:BoundField HeaderText="INVENTARIO" DataField="Inventario"/>
              <asp:BoundField HeaderText="CONCENTRACION" DataField="Concentracion"/>
              <asp:BoundField HeaderText="PROMEDIO" DataField="Promedio"/>
              <asp:BoundField HeaderText="USUARIO" DataField="Usuario" Visible="false" />
              <asp:ButtonField ButtonType="Button"  CommandName="Detalles" Text="Ver Resumen" ControlStyle-CssClass="btn btn-info"/>
              <asp:ButtonField ButtonType="Button" CommandName="Actualizar" Text="Actualizar" ControlStyle-CssClass="btn btn-warning"/>
          </Columns>
      </asp:GridView>
    </asp:Content>
