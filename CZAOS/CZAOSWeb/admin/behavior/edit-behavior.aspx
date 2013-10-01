<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-behavior.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_behavior" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />
    <mack:HiddenID runat="server" ID="hdnCategoryID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Behavior</asp:Literal>
        </legend>   

        <!-- 
             
     ,[Behavior]
      ,[BvrCatID]
      ,[BehaviorCode]
      ,[Description]
      ,[SortOrder]
      ,[Active]
      ,[BvrCat]
      ,[BvrCatCode]
      ,[CategoryActive]
            -->

        <ul>
            <li class="required">
                <label>Behavior:</label>
                <mack:RequiredTextBox runat="server" ID="txtBehavior" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a behavior" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Code:</label>
                <mack:RequiredTextBox runat="server" ID="txtBehaviorCode" CssClass="alphanumeric upper focusme" MaxLength="3" Width="30px" Required="false"></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Description:</label>
                <mack:RequiredTextBox runat="server" ID="txtDesc" MaxLength="500" Width="400px" Required="false" TextMode="MultiLine" Rows="6" ></mack:RequiredTextBox>     
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
