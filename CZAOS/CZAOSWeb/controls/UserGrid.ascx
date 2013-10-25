<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserGrid.ascx.cs" Inherits="CZAOSWeb.controls.UserGrid" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>



<asp:ObjectDataSource ID="userDataSource" runat="server" EnablePaging="true" OnSelected="userDataSource_Selected" OnSelecting="userDataSource_Selecting"
        SelectMethod="GetUserCollection" 
        SortParameterName="sortExpression"  
        SelectCountMethod="GetCount"            
        TypeName="CZBizObjects.MembershipList" StartRowIndexParameterName="pageIndex">
            
    <SelectParameters>
        <asp:Parameter Name="pageIndex" Type="Int32" />
        <asp:Parameter Name="pageSize" Type="Int32" />
        <asp:Parameter Name="startsWith" Type="String" />
    </SelectParameters>
                
</asp:ObjectDataSource>

<div>  
    <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    
    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="userGridView" TransparentImageUrl="~/images/transparent.png"   />

    <asp:GridView ID="userGridView" runat="server" DataSourceID="userDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview" 
                PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
                DataKeyNames="UserName" OnRowDataBound="userGridView_RowDataBound" OnRowCommand="userGridView_RowCommand" OnPreRender="userGridView_PreRender">    
            <Columns>                
                
                <asp:BoundField DataField="Username" SortExpression="Username" HeaderText="User">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Display Name">                   
                    <ItemTemplate>    
                        <asp:Literal runat="server" ID="litDName" ></asp:Literal>                        
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Email" SortExpression="Email">                   
                    <ItemTemplate>           
                        <asp:HyperLink runat="server" ID="lnkEmail" Text='<%# Bind("Email") %>' NavigateUrl='<%# Bind("Email","mailto:{0}") %>'></asp:HyperLink>     
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="User Type">                   
                    <ItemTemplate>    
                        <asp:Literal runat="server" ID="litUserType" ></asp:Literal>                        
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                                
                <%--<asp:TemplateField HeaderText="Expiration" Visible="false">                   
                    <ItemTemplate>    
                        <asp:Literal runat="server" ID="litExpDate" ></asp:Literal>                        
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                </asp:TemplateField>--%>

                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="60px" CssClass="" />
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUnlockUser" CssClass="unlock-user" Text="Unlock" ToolTip="Unlock User" CommandName="UnlockUser"></asp:LinkButton>
                    </ItemTemplate>                                 
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="30px" CssClass="" />
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkSendPassword" CssClass="send-password" Text="SendPassword" ToolTip="Send user credentials" CommandArgument='<%# Bind("UserName") %>' CommandName="SendPassword"></asp:LinkButton>
                    </ItemTemplate>                                 
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="850, 800, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("ProviderUserKey","~/admin/users/edit-user.aspx?UserId={0}") %>'></asp:HyperLink>
                    </ItemTemplate>       
                    <ItemStyle Width="60px" CssClass="tac"/>         
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ProviderUserKey") %>' CommandName="DeleteUser" />
                    </ItemTemplate>                    
                    <ItemStyle Width="80px" CssClass="tac"/>
                </asp:TemplateField>
                
            </Columns>  
            
        </asp:GridView> 

        <uc1:GridPager runat="server" id="userGridViewPagerControl" GridViewID="userGridView" />

        <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="userGridView" Text="No records found!"></mack:MessageDiv>

</div>


