<%@ Page Title="Manage Animals" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.Exhibits._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/exhibits/edit-exhibit.aspx" data-args="350, 700, true, null, 1">Add New Exhibit</a>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.ExhibitList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvExhibit" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvExhibit" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" OnRowCommand="gvExhibit_RowCommand" PagerSettings-Visible="false" 
        DataKeyNames="ExhibitID">
        <Columns>

            <asp:BoundField DataField="ExhibitName" SortExpression="ExhibitName" HeaderText="Name">                
            </asp:BoundField>
            
            <asp:BoundField DataField="AnimalRegion" SortExpression="AnimalRegion" HeaderText="Animal Region">
                <ItemStyle Width="300px" />
            </asp:BoundField>
            
           <asp:TemplateField ItemStyle-Width="100px" HeaderText="Behavior Count" SortExpression="BehaviorCount">                    
                <ItemTemplate>                    
                    <asp:HyperLink runat="server" ID="lnkBehaviorEdit" CssClass="ui-dialog-link" data-args="750, 800, true, null, 1" Text='<%#Bind("BehaviorCount")%>' ToolTip="Edit Behaviors" NavigateUrl='<%# Bind("ExhibitID","~/admin/exhibits/behaviors.aspx?exId={0}") %>'></asp:HyperLink>                
                </ItemTemplate> 
            </asp:TemplateField>                

            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Location Count" SortExpression="LocationCount">                    
                <ItemTemplate>                    
                    <asp:HyperLink runat="server" ID="lnkLocationEdit" CssClass="ui-dialog-link" data-args="750, 800, true, null, 1" Text='<%#Bind("LocationCount") %>' ToolTip="Edit Locations" NavigateUrl='<%# Bind("ExhibitID","~/admin/exhibits/locations.aspx?exId={0}") %>'></asp:HyperLink>                
                </ItemTemplate> 
            </asp:TemplateField> 

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="225, 300, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("ExhibitID","~/admin/exhibits/edit-exhibit.aspx?exId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ExhibitID") %>' CommandName="DeleteExhibit" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvExhibit" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvExhibit" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
