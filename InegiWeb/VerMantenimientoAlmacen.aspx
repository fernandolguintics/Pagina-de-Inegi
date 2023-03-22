<%@ Page Language="C#" Title="INEGI - Ver Mantenimiento Almacen" AutoEventWireup="true" CodeBehind="VerMantenimientoAlmacen.aspx.cs" Inherits="InegiWeb.VerMantenimientoAlmacen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Estilos/Usuarios.css" rel="stylesheet" />
    <title></title>
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
     
            <asp:Label runat="server" ID="txtFecha1" Font-Size="19px"></asp:Label>
        <br />
            <asp:Label runat="server" ID="txtFecha2" Font-Size="19px"></asp:Label>
        
         <h4> Consumo</h4>
   <asp:GridView runat="server" ID="GVConsumo"  AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped" OnRowCommand="GVConsumo_RowCommand">
        
        <Columns>
            <asp:BoundField HeaderText="No" DataField="IdCriterio" />
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" Enabled="false" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" Enabled="false" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>'  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
           <asp:ButtonField  HeaderText="EVIDENCIA" ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-info"/>
           <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                   <asp:CheckBox runat="server" Checked='<%# Eval("Estado").ToString() == "False" ? false : true  %>' ID="estado" />
               </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado2").ToString()%>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
        </Columns>
    </asp:GridView>

        <h4> Inventario</h4>
   <asp:GridView runat="server" ID="GVInventario" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped" OnRowCommand="GVInventario_RowCommand">
        
        <Columns>
            <asp:BoundField HeaderText="No" DataField="IdCriterio" />
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" Enabled="false" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" Enabled="false" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
             <asp:ButtonField  HeaderText="EVIDENCIA" ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-info"/>
            <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                   <asp:CheckBox runat="server" Checked='<%# Eval("Estado").ToString() == "False" ? false : true  %>' ID="estado" />
               </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado2").ToString()%>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
        </Columns>
    </asp:GridView>
           <!--Tabla de Area de Acervo--> 
    <h4> Área de Acervo</h4>
   <asp:GridView runat="server" ID="GVArea" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped" OnRowCommand="GVArea_RowCommand">
        
        <Columns>
            <asp:BoundField HeaderText="No" DataField="IdCriterio" />
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" Enabled="false" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" Enabled="false" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
           <asp:ButtonField  HeaderText="EVIDENCIA" ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-info"/>
           <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                   <asp:CheckBox runat="server" Checked='<%# Eval("Estado").ToString() == "False" ? false : true  %>' ID="estado" />
               </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado2").ToString()%>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
        </Columns>
    </asp:GridView>
          <!--Tabla de Procesos Tecnicos--> 
    <h4> Procesos Técnicos</h4>
   <asp:GridView runat="server" ID="GVProcesos" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped" OnRowCommand="GVProcesos_RowCommand">
        
        <Columns>
            <asp:BoundField HeaderText="No" DataField="IdCriterio" />
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" Enabled="false" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" Enabled="false" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
            <asp:ButtonField  HeaderText="EVIDENCIA" ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-info"/>
           <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                   <asp:CheckBox runat="server" Checked='<%# Eval("Estado").ToString() == "False" ? false : true  %>' ID="estado" />
               </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado2").ToString()%>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <!--Tabla de Consulta--> 
    <h4> Procesos Técnicos, Consulta y Acervo</h4>
   <asp:GridView runat="server" ID="GVAcervo" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped" OnRowCommand="GVAcervo_RowCommand">
        
        <Columns>
            <asp:BoundField HeaderText="No" DataField="IdCriterio" />
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" Enabled="false" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" Enabled="false" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>'/>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:BoundField HeaderText="OBSERVACION" DataField="Observacion"/>
            <asp:ButtonField  HeaderText="EVIDENCIA" ButtonType="Button" CommandName="Ver" Text="Ver" ControlStyle-CssClass="btn btn-info"/>
           <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                   <asp:CheckBox runat="server" Checked='<%# Eval("Estado").ToString() == "False" ? false : true  %>' ID="estado" />
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado2").ToString()%>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <asp:ScriptManager runat="server" ></asp:ScriptManager>
         <!-- Modal Seleccionar Inmueble-->
           <asp:Panel runat="server" ID="modalMostrarEvidencia" Style="display:none" CssClass="ModalPopup" >
        <div class="modal-content" style="width:100%">
            <div class="modal-header">
                <h3 class="modal-title">Imagen</h3>
            </div>
            <div class="modal-body">
            <asp:Image runat="server" ID="mostrarEvidencia" />
            </div>
            <div class="modal-footer">
                 <asp:Button runat="server" Text="Cerrar" CssClass="btn btn-danger" ID="btnCerrar"/>
           </div>
       </div>
    </asp:Panel>
        <!-- Ajax Seleccionar Inmueble-->
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeMostrar"
             TargetControlID="mostrarEvidencia" CancelControlID="btnCerrar"
             PopupControlID="modalMostrarEvidencia" BackgroundCssClass="ModalPopupFondo"
        ></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>

