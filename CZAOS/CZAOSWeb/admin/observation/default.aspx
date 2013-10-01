<%@ Page Title="Manage Observations" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.observation._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" data-args="650, 650, true, null, 1" href="/admin/observation/edit-observation.aspx">Add New Observation</a>    

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.ObservationList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvObs" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvObs" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="ObservationID" OnRowCommand="gvObs_RowCommand" OnRowDataBound="gvObs_RowDataBound">
        <Columns>

            <asp:BoundField DataField="ObserveStart" SortExpression="ObserveStart" HeaderText="Date" DataFormatString="{0:dddd, MMMM dd yyy hh:mm tt}">                
            </asp:BoundField>            
            <asp:BoundField DataField="ObservationID" SortExpression="ObservationID" HeaderText="ID" ItemStyle-Width="100px">                
            </asp:BoundField>  
            <asp:BoundField DataField="ObserveType" SortExpression="ObserveType" HeaderText="Type" ItemStyle-Width="150px">                
            </asp:BoundField>  
             <asp:BoundField DataField="Exhibit" SortExpression="Exhibit" HeaderText="Exhibit" ItemStyle-Width="150px">                
            </asp:BoundField> 
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkView" CssClass="ui-dialog-link gv-view-link" data-args="650, 650, true, null, 1" Text="View" ToolTip="View this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation.aspx?observationId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>            

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="650, 650, true, null, 1" Text="Edit" ToolTip="Edit this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/edit-observation.aspx?observationId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ObservationID") %>' CommandName="DeleteObservation" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

        
    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvObs" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvObs" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>
        function XXX() {
            //alert('close');
            //$('#div-dialog').dialog('close');
            //$(".ui-dialog-content").dialog().dialog("close");
        }
    </script>
</asp:Content>
