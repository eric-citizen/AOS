<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-animal.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_animal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Animal</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>Common Name:</label>
                <mack:RequiredTextBox runat="server" ID="txtCommonName" MaxLength="100" Required="true" Width="400px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the animal's common name" CssClass="focusme" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>House Name:</label>
                <mack:RequiredTextBox runat="server" ID="txtHouseName" MaxLength="150" Required="false" Width="400px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the animal's house name" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Scientific Name:</label>
                <mack:RequiredTextBox runat="server" ID="txtScientificName" MaxLength="150" Required="false" Width="400px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the animal's scientific name" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Gender:</label>
                 <asp:DropDownList runat="server" ID="ddlGender">
                     <asp:ListItem Selected="True" Text="Select Gender" Value="-1"></asp:ListItem>
                     <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                     <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                     <asp:ListItem Text="Unknown" Value="Unknown"></asp:ListItem>
                 </asp:DropDownList>
            </li>
            <li>
                <label>DOB:</label>
                <mack:RequiredTextBox runat="server" ID="txtDOB" MaxLength="50" Required="false" Width="200px"></mack:RequiredTextBox>   
            </li>
            <li>
                <label>Arrival Date:</label>
                <mack:RequiredTextBox runat="server" ID="txtArrivalDate" MaxLength="50" Required="false" Width="200px"></mack:RequiredTextBox>   
            </li>

            <li class="required">
                <label>Region Code:</label>
                 <mack:RequiredDropDownList runat="server" ID="ddlRegion" Required="true" DataTextField="AnimalRegionName" DataValueField="AnimalRegionCode" 
                     SetFocusOnError="true" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="select a region" InitialValue="-1" >                                                        
                </mack:RequiredDropDownList>
            </li>
            <li class="required">
                <label>Zoo ID:</label>
                <mack:RequiredTextBox runat="server" ID="txtZooID" MaxLength="50" Required="true" Width="200px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a zoo id" ></mack:RequiredTextBox>     
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
