<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-location.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Location</asp:Literal>
        </legend>   

        <!-- 
             
     
      ,[Description]
      ,[LocationCode]
      ,[MaskAma]
      ,[MaskProf]
      ,[SortOrder]
      ,[Active]
            -->

        <ul>
            <li class="required">
                <label>Description:</label>
                <mack:RequiredTextBox runat="server" ID="txtDescription" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a location description" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Code:</label>
                <mack:RequiredTextBox runat="server" ID="txtLocationCode" CssClass="alphanumeric upper focusme unique-regioncode" MaxLength="2" Width="30px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a unique location code" ></mack:RequiredTextBox>     
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
