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

        <div class="detail" id="detailHead" style="width:100%;border: 2px solid black">
            <h2>Observation Details</h2>
            <asp:Literal runat="server" ID="litHead" />
            <a href="javascript:void(0)" onclick="window.print();" class="print-link">Print</a>
            <asp:HyperLink runat="server" ID="lnkHeadEdit" CssClass="gv-edit-link" Text="Edit" ToolTip="Edit this item" ></asp:HyperLink>
            <asp:HyperLink runat="server" ID="lnkHeadRecords" CssClass="gv-edit-link" Text="View Records" ToolTip="View Observation Records" ></asp:HyperLink>
            <asp:Button runat="server" ID="btnHeadDelete" class="gv-delete-link" OnClick="btnHeadDelete_Click" Text="Delete"></asp:Button>
            <button ID="btnHeadBack" Class="floatRight" OnClick="history.go(-1);return false;">Back</button>
        </div>

        <div id="observers" style="width: 100%; display: inline-block; border: 2px solid black">
            <div class="title">  
                <h2>Observer(s)</h2><br />
            </div>
            <div id="observersMeta" style="height: 100%; width: 50%; float: left; display: inline-block">
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
            <div id="observerList" style="height: 100%; width: 50%; float: right; display: inline-block">
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
            <div id="school" style="width: 100%; display: inline-block; border: 2px solid black">
            <div class="title">
                <h2>School</h2><br />
            </div>
            <div id="districtSchool" style="height: 100%; width: 50%; float: left; display: inline-block">
                
                <label>District</label>
                <asp:Literal runat="server" ID="litDistrict"></asp:Literal>
                <br />
                <br />

                <label>School</label>
                <asp:Literal runat="server" ID="litSchool"></asp:Literal>
            </div>
            <div id="gradeTeacher" style="height: 100%; width: 50%; float: right; display: inline-block">
                <label>Grade</label>
                <asp:Literal runat="server" ID="litGrade"></asp:Literal>
                <br />
                <br />

                <label>Teacher</label>
                <asp:Literal runat="server" ID="litTeacherName"></asp:Literal>
            </div>
        </div>
        </asp:Panel>

        <div id="observation" style="width: 100%; display: inline-block; border: 2px solid black">
            <div class="title">
                <h2>Observation</h2><br />
            </div>
            <div id="time" style="height: 100%; width: 50%; float: left; display: inline-block">
                <label>Date</label>
                <asp:Literal runat="server" ID="litDate"></asp:Literal>
                <br /><br />

                <label>Start</label>
                <asp:Literal runat="server" ID="litStart"></asp:Literal>
                <br /><br />

                <label>End</label>
                <asp:Literal runat="server" ID="litEnd"></asp:Literal>
            </div>
            <div id="timed" style="height: 100%; width: 50%; float: right; display: inline-block">
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

        <div id="Animals" style="width: 100%; display: inline-block; border: 2px solid black">
            <div class="title">
                <h2>Animal(s)</h2><br />
            </div>
            <div id="regionExhibit" style="height: 100%; width: 50%; float: left; display: inline-block">
                <label>Region</label>
                <asp:Literal runat="server" ID="litRegion"></asp:Literal>
                <br />
                <br />
                <label>Exhibit</label>
                <asp:Literal runat="server" ID="litExhibit"></asp:Literal>
            </div>
            <div id="animalList" style="height: 100%; width: 50%; float: right; display: inline-block">
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

        <div class="detail" id="detailFoot" style="width:100%;border: 2px solid black">
            <h2>Observation Details</h2>
            <asp:Literal runat="server" ID="litFoot" />
            <a href="javascript:void(0)" onclick="window.print();" class="print-link">Print</a>
            <asp:HyperLink runat="server" ID="lnkFootEdit" CssClass="gv-edit-link" Text="Edit" ToolTip="Edit this item" ></asp:HyperLink>
            <asp:HyperLink runat="server" ID="lnkFootRecords" CssClass="gv-edit-link" Text="View Records" ToolTip="View Observation Records" ></asp:HyperLink>
            <asp:Button runat="server" ID="btnFootDelete" class="gv-delete-link" OnClick="btnHeadDelete_Click" Text="Delete"></asp:Button>
            <Button ID="btnFootBack" Class="floatRight" OnClick="history.go(-1);return false;">Back</Button>

        </div>


    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
