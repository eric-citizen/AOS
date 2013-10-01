<%@ Page Title="View Error Logs" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="CZAOSWeb.admin.master.logs" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/SimpleConfirmControl.ascx" TagPrefix="uc1" TagName="SimpleConfirmControl" %>
<%@ Register Src="~/admin/master/masterlinks.ascx" TagPrefix="uc1" TagName="masterlinks" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">    

    <uc1:masterlinks runat="server" id="masterlinks" />

    <div class="clear pb15">
        <uc1:SimpleConfirmControl runat="server" ID="confirmDeleteAll" Content="Delete all log files?" LinkText="Delete All" LinkCssClass="add-link" Title="Delete all log files" OnConfirm="confirmDeleteAll_Confirm" />
        <uc1:SimpleConfirmControl runat="server" ID="confirmDeleteEmpty" DataControlId="cde-box" Content="Delete all empty log files?" LinkText="Delete Empty" Title="Delete all empty log files" OnConfirm="confirmDeleteEmpty_Confirm"/>
    </div>

    <div style="width:600px;" class="floatLeft">
    <asp:GridView ID="gvConfig" runat="server" AllowSorting="False" AllowPaging="False" CssClass="gridview" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="Name" PagerSettings-Visible="false" OnRowCommand="gvConfig_RowCommand" >
        <Columns>
            
            <asp:BoundField DataField="Name" HeaderText="Name">                
            </asp:BoundField> 
            <asp:BoundField DataField="LastWriteTime" HeaderText="Date" ItemStyle-Width="120px" DataFormatString="{0:MM/dd/yyy hh:mmtt}">                
            </asp:BoundField> 
            <asp:BoundField DataField="Length" HeaderText="Size" ItemStyle-Width="80px">                
            </asp:BoundField> 
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkView" CssClass="gv-view-link view-log-link" Text="View" ToolTip="View Log File" NavigateUrl='<%# Bind("Name") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="20px" CssClass="tac" />               
            </asp:TemplateField>           
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("Name") %>' CommandName="DeleteLog" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    </div>

    <div class="floatLeft w596">
        <div id="logdiv" class="hidden"></div>        
    </div>
    <div class="clear">&nbsp;</div>

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvConfig" Text="No records found!"></mack:MessageDiv>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>

        $(document).ready(function () {

            $(".view-log-link").click(function (e) {
                $("#logdiv").addClass("cell-wait");
                e.preventDefault();
                filename = $(this).attr("href");                
                GetLogFile("logdiv", filename)
            });

        });

    </script>
</asp:Content>
