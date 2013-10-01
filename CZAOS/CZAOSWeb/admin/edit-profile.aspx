<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-profile.aspx.cs" Inherits="CZAOSWeb.admin.users.edit_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">    

    <!-- jquery tabs ui //-->
    <div id="tabs" class="no-tab-persist" style="width: 740px; margin: 0 auto;">

        <ul>
            <li><asp:HyperLink runat="server" ID="lnkAdmin" NavigateUrl="#tab1">Profile</asp:HyperLink></li>
            <li><asp:HyperLink runat="server" ID="lnkPro" NavigateUrl="#tab2">Email Prefs</asp:HyperLink></li>
            <li><asp:HyperLink runat="server" ID="lnkRegions" NavigateUrl="#tab3">Animal Regions</asp:HyperLink></li>                     
        </ul>     
    
        <div class="tab" id="tab1">
             
             <fieldset class="form-fieldset">

                <legend>
                    <asp:Literal runat="server" ID="fieldsetLegend">Edit Your Profile</asp:Literal>
                </legend>   

                <ul>                    
                    <li class="pr required">
                        <label>Change Password:</label>                        
                        <mack:RequiredTextBox ID="txtNewPassword" runat="server" Display="Dynamic" ValidatorToolTip="Please enter a new password" ValidatorCssClass="error" ErrorMessage="*"
                            MaxLength="50" SetFocusOnError="true" CssClass="pwd-strength focusme" Width="220px" TextMode="Password" ValidationGroup="profile"></mack:RequiredTextBox>
                        <asp:RegularExpressionValidator runat="server" ID="regexPassword" ControlToValidate="txtNewPassword" OnInit="regexPassword_Init"
                                Display="dynamic" ErrorMessage="Password does not meet complexity requirements" CssClass="password_strength"
                                ValidationExpression="^([a-zA-Z0-9]{6,15})$" ValidationGroup="profile"></asp:RegularExpressionValidator>
                        <div>&nbsp;</div>

                        <label>Confirm Password:</label>                        
                        <mack:RequiredTextBox ID="txtConfirmPassword" runat="server" Display="Dynamic" ErrorMessage="Please confirm the new password" ValidatorCssClass="verbose-error"  
                            MaxLength="50" SetFocusOnError="true" CssClass="pwd-strength" Width="220px" TextMode="Password" ValidationGroup="profile"></mack:RequiredTextBox>
                        <asp:RegularExpressionValidator runat="server" ID="regexPassword2" ControlToValidate="txtConfirmPassword" 
                                Display="dynamic" ErrorMessage="Password does not meet complexity requirements" CssClass="password_strength"
                                ValidationExpression="^([a-zA-Z0-9]{6,15})$" ValidationGroup="profile"></asp:RegularExpressionValidator>
                        <div>&nbsp;</div>
                        <asp:CompareValidator runat="server" ID="pwdCompare" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" CssClass="password_strength"
                            Display="Dynamic" ErrorMessage="Passwords do not match!" SetFocusOnError="true" Type="String" ValidationGroup="profile" ></asp:CompareValidator>

                        <a href="pwd-req" id="pwd-req-link" class="ui-content" title="Password Complexity">Password Complexity</a>
                        <div id="pwd-req" class="info" style="display:none;">
                            Password must be a minimum of 8 characters. It must contain at least one letter and one number. It can also contain one special character from this list: ! @ # $ * \ ^
                        </div>
                    </li>                    
                    
                    <li class="required">
                        <label>Email:</label>
                        <mack:EmailTextBox runat="server" ID="txtEmail" CssClass="useremailcheck" Width="220px" Required="true" MaxLength="256" ErrorMessage="*" ValidatorCssClass="error"
                            RegexCssClass="verbose-error" RegexErrorMessage="enter valid email" ValidationGroup="profile"></mack:EmailTextBox>
                    </li>
                    <li class="required">
                        <label>Display Name:</label>
                        <mack:RequiredTextBox runat="server" ID="txtDisplayName" MaxLength="100" Width="220px" Required="true" ValidationGroup="profile"
                            ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a display name" ></mack:RequiredTextBox>     
                    </li>     
                </ul>
        
        </fieldset>

        </div>
        <div class="tab" id="tab2">
            
            <fieldset class="form-fieldset">

                <legend>
                    <asp:Literal runat="server" ID="Literal1">Edit Your Email Prefs</asp:Literal>
                </legend>   

                <ul id="user-emailprefs-list">
                    <li>
                        <label>Do you want to receive an email for new observations?</label>   
                        <asp:CheckBox runat="server" ID="chkNewEmail" Checked="true" ValidationGroup="emailprefs" />  
                    </li>
                    <li>
                        <label>Do you want to receive an email for completed observations?</label>   
                        <asp:CheckBox runat="server" ID="chkCompEmail" Checked="true" ValidationGroup="emailprefs"/>  
                    </li>
                    <li>
                        <label>Do you want to receive an email for observations where you are an observer?</label>   
                        <asp:CheckBox runat="server" ID="chkObserveEmail" Checked="true" ValidationGroup="emailprefs" /> 
                    </li>
                    <li>
                        <label>Do you want to receive a weekly email with observations for your regions?</label>   
                        <asp:CheckBox runat="server" ID="chkWeekEmail" Checked="true" ValidationGroup="emailprefs" />  
                    </li>                    
                </ul>

            </fieldset>
            
        </div>
        <div class="tab" id="tab3">              
            
            <mack:MessageDiv runat="server" ID="divMessage" MessageType="info" Text="Animal Regions can only be assigned to existing users. Once the new user is saved you will be able to select animal regions on this tab." ></mack:MessageDiv>

            <asp:Panel runat="server" ID="regionPanel">
            
            <fieldset class="form-fieldset">

                <legend>
                    <asp:Literal runat="server" ID="Literal2">Choose Animal Regions</asp:Literal>
                </legend>   

                <ul>
                    <li>        
                       <asp:DataList runat="server" ID="dlRegions" RepeatLayout="Table" RepeatColumns="3">
                           <ItemTemplate>
                               <asp:Literal runat="server" ID="litRegion" Text='<%# Bind("AnimalRegionName") %>'></asp:Literal>
                           </ItemTemplate>
                       </asp:DataList>                           
                                            
                    </li>             
                    
                </ul>
        
            </fieldset>
             
            </asp:Panel>

        </div>        
 
    </div>

    <div class="tar pt10 pr30">
        <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" ValidationGroup="profile" />
    </div>
    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
