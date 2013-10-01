<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-school.aspx.cs" Inherits="CZAOSWeb.admin.schools.edit_school" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit School</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>School:</label>
                <mack:RequiredTextBox runat="server" ID="txtSchool" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter school name" ></mack:RequiredTextBox>     
            </li>   
            <li class="required">
                <label>District:</label>
                 <mack:RequiredDropDownList runat="server" ID="ddlDistrict" Required="true" DataTextField="District" DataValueField="DistrictID" 
                     SetFocusOnError="true" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="select a district" InitialValue="-1" >                                                        
                </mack:RequiredDropDownList>
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
