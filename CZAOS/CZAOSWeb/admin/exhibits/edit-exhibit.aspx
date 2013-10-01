<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-exhibit.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_exhibit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />
    
    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Exhibit</asp:Literal>
        </legend>   

        <!--              
    @AnimalRegionCode varchar(3),
    @Exhibit varchar(100),
    @Active bit
         -->

        <ul>
            <li class="required">
                <label>Exhibit Name:</label>
                <mack:RequiredTextBox runat="server" ID="txtExhibit" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter an exhibit" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Animal Region:</label>
                 <mack:RequiredDropDownList runat="server" ID="ddlRegion" Required="true" DataTextField="AnimalRegionName" DataValueField="AnimalRegionCode" 
                     SetFocusOnError="true" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="select a region" InitialValue="-1" >                                                        
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
