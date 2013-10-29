<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-behavior-category.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_behavior_category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Behavior Category</asp:Literal>
        </legend>   

        <!-- 
             
    @BvrCat varchar(100),
    @BvrCatCode varchar(2),
    @Description varchar(100),
    @MaskAma char(1),
    @MaskProf char(1),
    @SortOrder int,
    @Active bit
            -->

        <ul>
            <li class="required">
                <label>Category:</label>
                <mack:RequiredTextBox runat="server" ID="txtCategory" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a category name" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Code:</label>
                <mack:RequiredTextBox runat="server" ID="txtCategoryCode" CssClass="alphanumeric upper focusme unique-regioncode" MaxLength="2" Width="30px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a unique category code" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Description:</label>
                <mack:RequiredTextBox runat="server" ID="txtDesc" MaxLength="100" Width="200px" Required="false" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Show to Amateurs:</label>   
                <asp:CheckBox runat="server" ID="chkMaskAma" Checked="false" />  
            </li>
            <li>
                <label>Show to Professionals:</label>   
                <asp:CheckBox runat="server" ID="chkMaskPro" Checked="false" />  
            </li>
            
            <li>
                <label>Active:</label>   
                <asp:CheckBox runat="server" ID="chkActive" Checked="true" />  
            </li>
            
            <li class="tar pt15">
                <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" />
            </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
