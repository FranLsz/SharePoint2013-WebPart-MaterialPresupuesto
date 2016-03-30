<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs" Inherits="SpMaterialPresupuesto.VisualWebPart1.VisualWebPart1UserControl" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:ListView runat="server" ID="lstExpenses">
            <LayoutTemplate>

                <table runat="server" id="tblExpenses" cellpadding="2">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Titulo</th>
                            <th>Peticion</th>
                            <th>Realizada por</th>
                            <th>Fecha</th>
                            <th>Importe</th>
                        </tr>
                    </thead>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>

                <asp:DataPager runat="server" ID="dataPager" PageSize="10">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="10" PreviousPageText="<--" NextPageText="-->" />
                    </Fields>
                </asp:DataPager>

            </LayoutTemplate>
            <ItemTemplate>
                <tr runat="server">
                    <td runat="server">
                        <asp:CheckBox ID="chkUpdate" runat="server" />
                        <asp:HiddenField ID="hdCodigo" runat="server" Value='<%# Eval("ID") %>' />
                    </td>

                    <td runat="server">
                        <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("Title") %>' />
                    </td>

                    <td runat="server">
                        <asp:Label ID="lblPeticion" runat="server" Text='<%# Eval("Peticion") %>' />
                    </td>

                    <td runat="server">
                        <asp:Label ID="lblRealizada" runat="server" Text='<%# Eval("Realizado_x0020_por") %>' />
                    </td>

                    <td runat="server">
                        <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha") %>' />
                    </td>

                    <td runat="server">
                        <asp:Label ID="lblImporte" runat="server" Text='<%# Eval("Importe") %>' />
                    </td>
                </tr>
            
        </ItemTemplate>
        </asp:ListView>
        
        <asp:Button runat="server" ID="btnApprove" Text="Approve" Width="100" OnClick="btnApprove_Click" />
        &nbsp;
        <asp:Button runat="server" ID="btnReject" Text="Reject" Width="100" OnClick="btnReject_Click" />
    </ContentTemplate>
</asp:UpdatePanel>



