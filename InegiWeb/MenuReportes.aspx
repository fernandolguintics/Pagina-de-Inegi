<%@ Page Language="C#" Title="Menu Reportes Infraestructura" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuReportes.aspx.cs" Inherits="InegiWeb.MenuReportes" %>

<asp:Content runat="server" ID="MenuReportes" ContentPlaceHolderID="MainContent">
     <h1>Reportes Infraestructura Eléctrica</h1> 
    <div  style="display:flex; padding-bottom:1em;">
        <div  style="display:flex">
            <asp:DropDownList runat="server" id="ddlTrimestre" CssClass="form-control">
            <asp:ListItem Selected="True" Value="E-M">Enero-Marzo</asp:ListItem>
                <asp:ListItem Value="A-J">Abril-Junio</asp:ListItem>
                <asp:ListItem Value="J-S">Julio-Septiembre</asp:ListItem>
                <asp:ListItem Value="O-D">Octubre-Diciembre</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="display:flex; padding-left:2em">
            <asp:DropDownList runat="server" id="ddlseleccionarAño" CssClass="form-control"></asp:DropDownList>
        </div>
        <div style="display:flex; padding-left:2em">
            <asp:Button runat="server" Text="Seleccionar" ID="seleccionarBtn" OnClick="seleccionarBtn_Click" CssClass="btn btn-primary" />
        </div>
    </div>
      <asp:GridView runat="server" Visible="false" AutoGenerateColumns="false" OnRowDataBound="GVReportes_RowDataBound" AllowPaging="true" OnRowCommand="GVReportes_RowCommand" OnPageIndexChanging="GVReportes_PageIndexChanging"  CssClass="table table-striped" GridLines="None" ID="GVReportes" Width="60%">
          <Columns>
              <asp:BoundField HeaderText="Id" DataField="IdReporte"/>
              <asp:BoundField HeaderText="EDIFICIO" DataField="NombreEdificio"/>
              <asp:BoundField HeaderText="FECHA DE ELABORACION" DataField="FechaReporte"/>
              <asp:BoundField HeaderText="ARREGLADOS" DataField="Arreglados"/>
              <asp:BoundField HeaderText="COSTO TOTAL" DataField="CostoTotal"/>
              <asp:BoundField HeaderText="VALIDACION" DataField="Validacion"/>
              <asp:BoundField HeaderText="USUARIO" DataField="Usuarios" Visible="false"/>
              <asp:ButtonField ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-primary"/>
              <asp:ButtonField ButtonType="Button" CommandName="Modificar" Text="Modificar" ControlStyle-CssClass="btn btn-warning" />
          </Columns>
      </asp:GridView>
</asp:Content>

