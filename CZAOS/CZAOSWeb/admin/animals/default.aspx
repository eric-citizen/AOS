<%@ Page Title="Manage Animals" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.Animals._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<%--    <a class="add-link ui-dialog-link" href="/admin/animals/edit-animal.aspx" data-args="500, 700, true, null, 1" title="Add New Animal">Add New Animal</a>--%>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>

    <div class="animalSearch">
        <label class="required">Free Text Search:</label>
        <span style="display:inline-block; width:220px;">
            <mack:RequiredTextBox runat="server" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
        </span>
        <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click"/>
        <asp:LinkButton runat="server" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
    </div>
 
    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.AnimalList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvAnimals" TransparentImageUrl="~/images/transparent.png" />

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <asp:GridView ID="gvAnimals" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
                PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
                DataKeyNames="AnimalID" OnRowDataBound="gvAnimals_RowDataBound" OnRowCommand="gvAnimals_RowCommand">
                <Columns>

                    <asp:BoundField DataField="CommonName" SortExpression="CommonName" HeaderText="Common Name">                
                    </asp:BoundField>
            
                    <asp:BoundField DataField="HouseName" SortExpression="HouseName" HeaderText="House Name">
                        <ItemStyle Width="300px" />
                    </asp:BoundField>  

                    <asp:BoundField DataField="ZooID" SortExpression="ZooID" HeaderText="Zoo ID">
                        <ItemStyle Width="80px" />
                    </asp:BoundField>
                
                    <asp:TemplateField SortExpression="AnimalRegion" HeaderText="Animal Region" ItemStyle-Width="250px">                    
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAnimalRegion" Text='<%# Bind("AnimalRegion") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                        </ItemTemplate> 
                    </asp:TemplateField>

<%--                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="600, 700, true, null, 1" Text="" ToolTip="Edit Animal" NavigateUrl='<%# Bind("AnimalID","~/admin/animals/edit-animal.aspx?animalId={0}") %>'></asp:HyperLink>
                        </ItemTemplate> 
                        <ItemStyle Width="60px" CssClass="tac" />               
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("AnimalID") %>' CommandName="DeleteAnimal" />
                        </ItemTemplate>                
<%--                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>--%>
                </Columns>
        
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvAnimals" />
    <asp:Button ID="btnRefresh" Text="refresh" runat="server" onclick="btnRefresh_Click" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvAnimals" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
