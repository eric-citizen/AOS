<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="view-observation.aspx.cs" Inherits="CZAOSWeb.admin.observation.view_observation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" />


    <fieldset class="form-fieldset fs11">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend"></asp:Literal>
            Observation
        </legend>
        
        <button id="btnHeadBack" class="floatLeft" style="margin-bottom: 20px" onclick="history.go(-1);return false;">Back</button>

        <div id="detailHead" class="obsSection" style="border-top: 2px solid #a2a6ad">
            <h2>Observation Details</h2>
            <div style="padding-left: 15px">
                <asp:Literal runat="server" ID="litHead" />
                <span class="print"><a href="javascript:void(0)" onclick="window.print();" ></a></span>
                <div class="vr"></div>
                <asp:ImageButton runat="server" ID="btnHeadDelete" ImageUrl="~/assets/images/icons/admin/delete.png" OnClick="btnHeadDelete_Click" CssClass="delete" Text="Delete"></asp:ImageButton>
                <span class="records"><asp:HyperLink runat="server" ID="lnkHeadRecords" Text="" ToolTip="View Observation Records"></asp:HyperLink></span>
                <span class="edit"><asp:HyperLink runat="server" ID="lnkHeadEdit" Text="" ToolTip="Edit this item"></asp:HyperLink></span>
            </div> 
        </div>

        <div id="observers" class="obsSection">
            <div class="title">  
                <h2>Observer(s)</h2><br />
            </div>
            <div id="observersMeta" class="obsSectionInnerLeft">
                <label>Number of Observers</label>
                <asp:Literal runat="server" ID="litObsCount"></asp:Literal>&nbsp;observers
                <br />
                <br />

                <asp:Panel ID="pnlLogin" runat="server">
                    <label>Teacher Login</label>
                    <asp:Literal runat="server" ID="litTeacherLogin"></asp:Literal>
                    <br />
                    <br />

                    <label>Teacher Password</label>
                    <asp:Literal runat="server" ID="litTeacherPassword"></asp:Literal>
                    <br />
                    <br />

                    <label>Student Password</label>
                    <asp:Literal runat="server" ID="litStudentPassword"></asp:Literal>
                </asp:Panel>
            </div>
            <div id="observerList" class="obsSectionInnerRight">
                <label>Attendee(s)</label>
                <asp:Repeater runat="server" ID="rptObservers">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litObsName" Text='<%#Bind("DisplayName") %>'></asp:Literal>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>                
            </div>
        </div>

        <asp:Panel ID="pnlSchool" runat="server">
            <div id="school" class="obsSection">
            <div class="title">
                <h2>School</h2><br />
            </div>
            <div id="districtSchool" class="obsSectionInnerLeft">
                
                <label>District</label>
                <asp:Literal runat="server" ID="litDistrict"></asp:Literal>
                <br />
                <br />

                <label>School</label>
                <asp:Literal runat="server" ID="litSchool"></asp:Literal>
            </div>
            <div id="gradeTeacher" class="obsSectionInnerRight">
                <label>Grade</label>
                <asp:Literal runat="server" ID="litGrade"></asp:Literal>
                <br />
                <br />

                <label>Teacher</label>
                <asp:Literal runat="server" ID="litTeacherName"></asp:Literal>
            </div>
        </div>
        </asp:Panel>

        <div id="observation" class="obsSection">
            <div class="title">
                <h2>Observation</h2><br />
            </div>
            <div id="time" class="obsSectionInnerLeft">
                <label>Date</label>
                <asp:Literal runat="server" ID="litDate"></asp:Literal>
                <br /><br />

                <label>Start</label>
                <asp:Literal runat="server" ID="litStart"></asp:Literal>
                <br /><br />

                <label>End</label>
                <asp:Literal runat="server" ID="litEnd"></asp:Literal>
            </div>
            <div id="timed" class="obsSectionInnerRight">
                <label>Category</label>
                <asp:Literal runat="server" ID="litCategory"></asp:Literal>
                <br /><br />

                <asp:Panel runat="server" ID="pnlTimer">
                    <label>Manual</label>
                    <asp:Literal runat="server" ID="litManual"></asp:Literal>
                    <br /><br />

                    <label>Show Timer</label>
                    <asp:Literal runat="server" ID="litTimer"></asp:Literal>
                    <br /><br />

                    <label>Timed Interval</label>
                    <asp:Literal runat="server" ID="litInterval"></asp:Literal>
                </asp:Panel>
            </div>
        </div>

        <div id="Animals" class="obsSection">
            <div class="title">
                <h2>Animal(s)</h2><br />
            </div>
            <div id="regionExhibit" class="obsSectionInnerLeft">
                <label>Region</label>
                <asp:Literal runat="server" ID="litRegion"></asp:Literal>
                <br />
                <br />
                <label>Exhibit</label>
                <asp:Literal runat="server" ID="litExhibit"></asp:Literal>
            </div>
            <div id="animalList" class="obsSectionInnerRight">
                <label>Animal(s)</label>
                <asp:repeater runat="server" id="rptAnimal">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litAnimal" Text='<%#Bind("CommonName") %>'></asp:Literal><br />
                    </ItemTemplate>
                </asp:repeater>
                
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

            </div>
        </div>

        <div  id="detailFoot" class="obsSection">
            <h2>Observation Details</h2>
            <div style="padding-left: 15px">
                <asp:Literal runat="server" ID="litFoot" />
                <span class="print"><a href="javascript:void(0)" onclick="window.print();" class="print-link"></a></span>
                <div class="vr"></div>
                <asp:ImageButton runat="server" ID="btnFootDelete" ImageUrl="~/assets/images/icons/admin/delete.png" OnClick="btnHeadDelete_Click" CssClass="delete" Text=""></asp:ImageButton>
                <span class="records"><asp:HyperLink runat="server" ID="lnkFootRecords" CssClass="gv-edit-link" Text="" ToolTip="View Observation Records"></asp:HyperLink></span>
                <span class="edit"><asp:HyperLink runat="server" ID="lnkFootEdit" CssClass="gv-edit-link" Text="" ToolTip="Edit this item"></asp:HyperLink></span>
            </div>
        </div>
        
        <button id="btnFootBack" class="floatLeft" onclick="history.go(-1);return false;">Back</button>

    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
