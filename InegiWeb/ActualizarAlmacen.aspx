<%@ Page Title="INEGI - Actualizar Almacen" Language="C#" AutoEventWireup="true" CodeBehind="ActualizarAlmacen.aspx.cs" Inherits="InegiWeb.ActualizarAlmacen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="../Content/bootstrap.min.css" rel="stylesheet" />
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
        <div style="display:flex; width:20%">
              <asp:Label runat="server" Text="Fecha de Actualización" Font-Size="19px"></asp:Label>
              <asp:TextBox runat="server" ID="txbFecha2" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
         <h4> Consumo</h4>
   <asp:GridView runat="server" ID="GVConsumo"  AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField  DataField="IdConsumo"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                 <asp:RadioButton runat="server" ID="rbCumple" GroupName="con"  Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="con" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>'  />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                   <asp:Textbox runat="server" ID="Observacion" Text='<%# Eval("Observacion") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado").ToString()%>' ID="estado"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                   <asp:FileUpload runat="server"  ID="evidencia"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


        <h4> Inventario</h4>
   <asp:GridView runat="server" ID="GVInventario" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField  DataField="IdInventario"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="inv" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="inv" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                   <asp:Textbox runat="server" ID="Observacion" Text='<%# Eval("Observacion") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado").ToString()%>' ID="estado"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                   <asp:FileUpload runat="server"  ID="evidencia"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
           <!--Tabla de Area de Acervo--> 
    <h4> Área de Acervo</h4>
   <asp:GridView runat="server" ID="GVArea" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField  DataField="IdConcentracion"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="area"  Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="area"  Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
          <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                   <asp:Textbox runat="server" ID="Observacion"  Text='<%# Eval("Observacion") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado").ToString()%>' ID="estado"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                   <asp:FileUpload runat="server"  ID="evidencia"/>
                </ItemTemplate>
            </asp:TemplateField>
              
        </Columns>
    </asp:GridView>
          <!--Tabla de Procesos Tecnicos--> 
    <h4> Procesos Técnicos</h4>
   <asp:GridView runat="server" ID="GVProcesos" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField  DataField="IdConcentracion"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="Acervo"   Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="Acervo" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                   <asp:Textbox runat="server" ID="Observacion" Text='<%# Eval("Observacion") %>'   />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado").ToString()%>'  ID="estado"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                   <asp:FileUpload runat="server"  ID="evidencia"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <!--Tabla de Consulta--> 
    <h4> Procesos Técnicos, Consulta y Acervo</h4>
   <asp:GridView runat="server" ID="GVAcervo" AutoGenerateColumns="false" Width="70%" GridLines="None" CssClass="table table-striped">
        
        <Columns>
            <asp:BoundField  DataField="IdConcentracion"/>
           <asp:BoundField HeaderText="CRITERIO" DataField="Criterio"/>
            <asp:TemplateField HeaderText="CUMPLE">
                <ItemTemplate>
                    <asp:RadioButton runat="server" ID="rbCumple" GroupName="area" Checked='<%# Eval("Cumple").ToString() == "False" ? false : true  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NO CUMPLE">
                <ItemTemplate>
                   <asp:RadioButton runat="server" ID="rbNoCumple" GroupName="area" Checked='<%# Eval("NoCumple").ToString() == "False" ? false : true  %>'/>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="OBSERVACION">
                <ItemTemplate>
                   <asp:Textbox runat="server" ID="Observacion" Text='<%# Eval("Observacion") %>'   />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="VALIDACIÓN">
               <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Estado").ToString()%>' ID="estado"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="EVIDENCIA">
                <ItemTemplate>
                   <asp:FileUpload runat="server"  ID="evidencia"/>
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
    </asp:GridView>
        <asp:Button runat="server" ID="btnActualizar" OnClick="btnActualizar_Click" Text="Actualizar Mantenimiento" CssClass="btn btn-success"/>
    </form>
</body>
</html>
