<%@ Page Language="C#" Title="INEGI - Mantenimiento Almacen" AutoEventWireup="true" CodeBehind="MantenimientoAlmacenConcentracion.aspx.cs" Inherits="InegiWeb.MantenimientoAlmacenConcentracion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
    <title>Mantenimiento a Inmuebles</title>
</head>
<body>
    <form id="form1" runat="server">
            <div style="text-align:center; align-items:center">
             <h2>Mantenimiento a Almacenes:Concentración</h2>
       <p style="font-weight:bold"> Mantenimiento a: <asp:Label runat="server"  Text="Nombre del usuario" ID="txtnombreInmueble" Font-Bold="false" Font-Size="17px"></asp:Label></p>
       <p style="font-weight:bold"> Direccion: <asp:Label runat="server" ID="txtDireccion" Font-Size="17px" Font-Bold="false"></asp:Label></p>
        <p style="font-weight:bold">Posesion: <asp:Label runat="server" ID="txtPosesion" Font-Bold="false" Font-Size="17px" ></asp:Label>
                               Ocupacion: <asp:Label runat="server" ID="txtOcupacion" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Superficie: <asp:Label runat="server" ID="txtSuperficie" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Uso: <asp:Label runat="server" ID="txtUso" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Niveles: <asp:Label runat="server" ID="txtNiveles" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Cajones: <asp:Label runat="server" ID="txtCajones" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Antiguedad: <asp:Label runat="server" ID="txtAntiguedad" Font-Bold="false" Font-Size="17px"></asp:Label>
                               Terreno: <asp:Label runat="server" ID="txtTerreno" Font-Bold="false" Font-Size="17px"></asp:Label>
       </p> 
   </div>     
        
           <!--Tabla de Area de Acervo--> 
    <h4> Área de Acervo</h4>
   <asp:GridView runat="server" ID="GVArea" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdConcentracion"/>
           <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="acervo" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="acervo" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observacion"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                    <asp:FileUpload runat="server" ID="evidencia"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
          <!--Tabla de Procesos Tecnicos--> 
    <h4> Procesos Técnicos</h4>
   <asp:GridView runat="server" ID="GVProcesos" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdConcentracion"/>
           <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="procesos" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="procesos" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observacion"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                    <asp:FileUpload runat="server" ID="evidencia"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <!--Tabla de Consulta--> 
    <h4> Procesos Técnicos, Consulta y Acervo</h4>
   <asp:GridView runat="server" ID="GVAcervo" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField DataField ="IdConcentracion"/>
           <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="area" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="observacion" GroupName="area"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                    <asp:FileUpload runat="server" ID="evidencia"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:Button runat="server" ID="btnGuardarDatos" Text="Guardar Mantenimiento" CssClass="btn btn-primary" OnClick="btnGuardarDatos_Click"/>
        <!-- Modal Guardar-->
        <asp:Panel runat="server" ID="modalGuardar" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:150%">
            <div class="modal-header">
                <asp:Label runat="server" Font-Size="15px" Text="Exito" ></asp:Label>
            </div>
            <div class="modal-body">
            <asp:Label runat="server" Font-Size="20px" ID="nombreEdificiotxt" Text="Se ha guardado el mantenimiento" ></asp:Label> 
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" Text="Cerrar"  CssClass="btn btn-danger" ID="btnCancelar2"/>
           </div>
       </div>
    </asp:Panel>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Ajax Guardar-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeGuardarMantenimiento"
         TargetControlID="nombreEdificiotxt"
          CancelControlID="btnCancelar2"
        PopupControlID="modalGuardar" BackgroundCssClass="ModalPopupFondo" 
        ></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>
