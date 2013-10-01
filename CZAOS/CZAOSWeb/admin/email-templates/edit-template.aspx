<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-template.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_template" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" ClientIDMode="Static" Value="0" />
    
    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Template</asp:Literal>
        </legend>          

        <ul>
            <li class="required">
                <label>Key:</label>
                <mack:RequiredTextBox runat="server" ID="txtKey" CssClass="alphanumeric focusme et-key-check" MaxLength="50" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a unique key" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Instructions:</label>
                 <mack:RequiredTextBox runat="server" ID="txtInstructions" Width="400px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter instructional text" TextMode="MultiLine" Rows="3"></mack:RequiredTextBox> 
            </li>
            <li class="required">
                <label>Subject:</label>
                <mack:RequiredTextBox runat="server" ID="txtSubject" CssClass="focusme" MaxLength="100" Width="400px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a subject" ></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>Email Body:</label>
                <div class="clear" style="height:1px;">&nbsp;</div>
                <h1>HTML EDITOR NEEDED</h1>
            </li>                  
            <li>
                <label>Active:</label>   
                <asp:CheckBox runat="server" ID="chkActive" Checked="true" />  
                <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button floatRight" Text="Save" />
            </li>
            
           
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="/admin/editor/redactor/init-redactor.js"></script>

    <script>
        $(".et-key-check").blur(function () {
            var key = $(this).val();
            TemplateKeyExists($(this).attr("Id"), key, $("#hdnItemID").val());
        });
    </script>
</asp:Content>
