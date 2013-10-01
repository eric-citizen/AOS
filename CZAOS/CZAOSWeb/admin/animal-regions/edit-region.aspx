<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-region.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_region" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Animal Region</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>Region Code:</label>
                <mack:RequiredTextBox runat="server" ID="txtRegionCode" CssClass="alphanumeric upper focusme unique-regioncode" MaxLength="3" Width="30px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a unique region code" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Animal Region:</label>
                <mack:RequiredTextBox runat="server" ID="txtAnimalRegion" MaxLength="100" Required="true" Width="200px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter an animal region" ></mack:RequiredTextBox>     
            </li>
             <li class="required">
                <label>Region Name:</label>
                <mack:RequiredTextBox runat="server" ID="txtRegionName" MaxLength="100" Required="true" Width="200px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a region name" ></mack:RequiredTextBox>     
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

    <script>

        $(".unique-regioncode").blur(function () {
            var regionId = $(this).val();
            AnimalRegionExists($(this).attr("Id"), regionId);
        });

    </script>

</asp:Content>
