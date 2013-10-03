<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="view-observation.aspx.cs" Inherits="CZAOSWeb.admin.observation.view_observation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" />

    <a href="javascript:void(0)" onclick="window.print();" class="print-link floatRight">Print</a>

    <fieldset class="form-fieldset fs11">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend"></asp:Literal>
            Observation
        </legend>

        <ul>
            <li>
                <label>Category:</label>
                <asp:Literal runat="server" ID="litCategory"></asp:Literal>
            </li>
            <li>
                <label>Observers:</label><br />        
                <asp:Repeater runat="server" ID="rptObservers">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litObsName" Text='<%#Bind("DisplayName") %>'></asp:Literal>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal runat="server" ID="litObsCount"></asp:Literal> observers
            </li>                
            <asp:Panel ID="pnlLoginInfo" runat="server" >
            <li>
                <table id="cred-table" class="w100pc" >
                    <tr>
                        <td class="b">Teacher Name:</td>
                        <td><asp:Literal runat="server" ID="litTeacherName"></asp:Literal></td>
                        <td class="b">Teacher Login:</td>
                        <td><asp:Literal runat="server" ID="litTeacherLogin"></asp:Literal></td>
                        <td class="b">Teacher Password:</td>
                        <td><asp:Literal runat="server" ID="litTeacherPassword"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="b">Student Password:</td>
                        <td colspan="5"><asp:Literal runat="server" ID="litStudentPassword"></asp:Literal></td>
                    </tr>
                </table>
            </li>
            <li>
                <table class="w100pc" >
                    <tr>
                        <td class="b">District:</td>
                        <td><asp:Literal runat="server" ID="litDistrict"></asp:Literal></td>
                        <td class="b">School:</td>
                        <td><asp:Literal runat="server" ID="litSchool"></asp:Literal></td>
                        <td class="b">Grade:</td>
                        <td><asp:Literal runat="server" ID="litGrade"></asp:Literal></td>
                    </tr>                    
                </table>
            </li>
            </asp:Panel>
            <li>
                <table class="w100pc">
                    <tr>
                        <td class="b">Date:</td>
                        <td><asp:Literal runat="server" ID="litDate"></asp:Literal></td>
                        <td class="b">Start:</td>
                        <td><asp:Literal runat="server" ID="litStart"></asp:Literal></td>
                        <td class="b">End:</td>
                        <td><asp:Literal runat="server" ID="litEnd"></asp:Literal></td>
                    </tr>
                </table>                
            </li>            
            <asp:Panel ID="pnlTimer" runat="server" >

            <li>
                <table class="w100pc">
                    <tr>
                        <td class="b" style="width:110px;">Timed Interval:</td>
                        <td><asp:Literal runat="server" ID="litInterval"></asp:Literal></td>
                        <td class="b w100">Timer:</td>
                        <td><asp:Literal runat="server" ID="litTimer"></asp:Literal></td>
                        <td class="b w100">Manual:</td>
                        <td><asp:Literal runat="server" ID="litManual"></asp:Literal></td>
                    </tr>
                </table>                    
            </li>
            </asp:Panel>
            <li>
                <span class="b">Region:</span>
                <asp:Literal runat="server" ID="litRegion"></asp:Literal>
                <span class="b pl10">Exhibit:</span>
                <asp:Literal runat="server" ID="litExhibit"></asp:Literal>
            </li>     
            <li>
                <label>Groups</label>
                             <div class="clear" style="height:1px"></div>   
                 <asp:Repeater runat="server" ID="rptGroups" OnItemDataBound="rptGroups_ItemDataBound">
                     <ItemTemplate>
                         <span class="group-name"><asp:Literal runat="server" ID="litGroupName" Text='<%#Bind("GrpName") %>'></asp:Literal></span>
                         <div class="view-animal-group">
                         <asp:Repeater runat="server" ID="rptGroupAnimals">
                             <ItemTemplate>
                                 <span class="group-animal-name"><asp:Literal runat="server" ID="litGroupAnimalName" Text='<%#Bind("CommonName") %>'></asp:Literal></span>
                                 <br />
                             </ItemTemplate>
                         </asp:Repeater>
                             </div>
                     </ItemTemplate>
                 </asp:Repeater>
            </li>        
        </ul>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
