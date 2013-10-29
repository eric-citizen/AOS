<%@ Page Title="Manage Observations" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.observation._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div id="observationTabs">
        <ul>
            <li><a href="#UpcomingObservations">Upcoming Observations</a></li>
            <li><a href="#RecentObservations">Recent Observations</a></li>
        </ul>

        <div id="UpcomingObservations">
            <div class="filter">
                <h2 id="filterUpH2" class="filter-hide" onClick="">Filter Sessions</h2>
                <div id="filterUp" class="filterInner">              
                    <div id="AnimalDateUp" class="obsSectionInnerLeft">
                        <label>Animal</label><br />
                        <asp:DropDownList id="AnimalListUp" AutoPostBack="True" runat="server" Width="300px"/><br /><br />

                        <label>Timeframe</label><br />
                        <input type="date" id="dateFromUp" runat="server" /> -- <input type="date" id="dateToUp" runat="server"/><br /><br />

                        <label>Observation Category</label><br />
                        <asp:checkbox runat="server" AutoPostBack="false" id="timedUp" Text="Timed" Checked="true" /><br />
                        <asp:checkbox runat="server" AutoPostBack="false" id="behaviorUp" Text="Behavior"/><br />

                    </div>

                    <div id="schoolUp" class="obsSectionInnerRight">

                        <asp:checkbox id="searchStudentObservationsUp" runat="server" Checked="false" 
                            AutoPostBack="true" OnCheckedChanged="searchStudentObservationsUp_CheckedChanged"
                            Text="Search Student Observations" /><br /><br />

                        <label>District</label><br />
                        <asp:DropDownList id="DistrictListUp" AutoPostBack="true" OnSelectedIndexChanged="districtChange" runat="server" Enabled="false"/><br /><br />

                        <label>School</label><br />
                        <asp:DropDownList id="SchoolListUp" AutoPostBack="false" runat="server" Enabled="false"/><br /><br />
                        <asp:Button id="SearchUp" CssClass="button" Text="Search" runat="server" OnClick="searchUp_Click" PostBackUrl="~/admin/observation/default.aspx#UpcomingObservations"/>
                        <asp:Button id="clearUp" CssClass="button" Text="Clear Search" runat="server" OnClick="clearUp_Click"/>

                    </div>
                </div>
            </div>
            <hr />
            <h2>Upcoming Observations</h2>
            <hr />
            <a class="add-link" href="/admin/observation/edit-observation.aspx">Add New Observation</a>

            <asp:ObjectDataSource ID="cztDataSourceUpcoming" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting_Upcoming"
                SelectMethod="GetItemCollection" TypeName="CZBizObjects.ObservationList"
                EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">

                <SelectParameters>
                    <asp:Parameter Name="filterExpression" Type="string" DefaultValue="" />
                </SelectParameters>

            </asp:ObjectDataSource>

            <mack:GridViewSortExtender runat="server" ID="gvse"
                AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvObsUp" TransparentImageUrl="~/images/transparent.png" />

            <asp:GridView ID="gvObsUp" runat="server" DataSourceID="cztDataSourceUpcoming" AllowSorting="True" AllowPaging="True" CssClass="gridview"
                PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false"
                DataKeyNames="ObservationID" OnRowCommand="gvObsUp_RowCommand" OnRowDataBound="gvObsUp_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="ObserveStart" SortExpression="ObserveStart" HeaderText="Date" DataFormatString="{0:g}"></asp:BoundField>
                    <asp:BoundField DataField="ObservationID" SortExpression="ObservationID" HeaderText="Obs.ID" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="ObserveType" SortExpression="ObserveType" HeaderText="User Type" ItemStyle-Width="150px"></asp:BoundField>
                    <asp:BoundField DataField="Exhibit" SortExpression="Exhibit" HeaderText="Exhibit" ItemStyle-Width="150px"></asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="view"><asp:HyperLink runat="server" ID="lnkView" CssClass="gv-view-link" data-args="650, 650, true, null, 1" Text="" ToolTip="View this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="edit"><asp:HyperLink runat="server" ID="lnkEdit" CssClass="gv-edit-link" data-args="650, 650, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/edit-observation.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="records"><asp:HyperLink runat="server" ID="lnkRecords" CssClass="gv-edit-link" data-args="650, 650, true, null, 1" Text="" ToolTip="View Observation Records" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation-records.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ObservationID") %>' CommandName="DeleteObservation" />
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                </Columns>


            </asp:GridView>

            <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvObsUp" />

            <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvObsUp" Text="No records found!"></mack:MessageDiv>
        </div>
        <div id="RecentObservations">
            <div class="filter">
                <h2 id="filterRecH2" class="filter-hide" onClick="">Filter Sessions</h2>
                <div id="filterRec" class="filterInner">              
                    <div id="AnimalDateRec" class="obsSectionInnerLeft">
                        <label>Animal</label><br />
                        <asp:DropDownList id="AnimalListRec" AutoPostBack="True" runat="server" Width="300px"/><br /><br />

                        <label>Timeframe</label><br />
                        <input type="date" id="dateFromRec" runat="server" /> -- <input type="date" id="dateToRec" runat="server"/><br /><br />

                        <label>Observation Category</label><br />
                        <asp:Checkbox id="timedRec" runat="server" Checked="True" Text="Timed" AutoPostBack="false" /><br />
                        <asp:Checkbox ID="behaviorRec" runat="server" Text="Behavior" AutoPostBack="false" /><br />
                    </div>

                    <div id="schoolRec" class="obsSectionInnerRight">
                        <asp:CheckBox id="searchStudentObservationsRec" runat="server" Checked="false" 
                            AutoPostBack="true" OnCheckedChanged="searchStudentObservationsRec_CheckedChanged"
                            Text="Search Student Observations" /><br /><br />

                        <label>District</label><br />
                        <asp:DropDownList id="DistrictListRec" AutoPostBack="true" OnSelectedIndexChanged="districtChange" Enabled="false" runat="server"/><br /><br />

                        <label>School</label><br />
                        <asp:DropDownList id="SchoolListRec" AutoPostBack="false" Enabled="false" runat="server"/><br /><br />
<<<<<<< HEAD
                        <asp:Button id="searchRec" CssClass="button" Text="Search" runat="server" OnClick="searchRec_Click" PostBackUrl="~/admin/observation/default.aspx#RecentObservations"/>
=======
                        <asp:Button id="searchRec" CssClass="button" Text="Search" runat="server" OnClick="searchRec_Click" />
>>>>>>> origin/master
                        <asp:Button id="clearRec" CssClass="button" Text="Clear Search" runat="server" OnClick="clearRec_Click"/>
                    </div>

                </div>
            </div>

            <hr />
            <h2>Recent Observations</h2>
            <hr />
            <a class="add-link" href="/admin/observation/edit-observation.aspx">Add New Observation</a>

            <asp:ObjectDataSource ID="cztDataSourceRecent" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting_Recent"
                SelectMethod="GetItemCollection" TypeName="CZBizObjects.ObservationList"
                EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">

                <SelectParameters>
                    <asp:Parameter Name="filterExpression" Type="string" DefaultValue="" />
                </SelectParameters>

            </asp:ObjectDataSource>
            <mack:GridViewSortExtender runat="server" ID="GridViewSortExtender1"
                AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvObsRec" TransparentImageUrl="~/images/transparent.png" />

            <asp:GridView  ID="gvObsRec" runat="server" DataSourceID="cztDataSourceRecent" AllowSorting="True" AllowPaging="True" CssClass="gridview"
                PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false"
                DataKeyNames="ObservationID" OnRowCommand="gvObsRec_RowCommand" OnRowDataBound="gvObsRec_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="ObserveStart" SortExpression="ObserveStart" HeaderText="Date" DataFormatString="{0:g}"></asp:BoundField>
                    <asp:BoundField DataField="ObservationID" SortExpression="ObservationID" HeaderText="Obs.ID" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="ObserveType" SortExpression="ObserveType" HeaderText="User Type" ItemStyle-Width="150px"></asp:BoundField>
                    <asp:BoundField DataField="Exhibit" SortExpression="Exhibit" HeaderText="Exhibit" ItemStyle-Width="150px"></asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="view"><asp:HyperLink runat="server" ID="lnkView" CssClass="gv-view-link" data-args="650, 650, true, null, 1" Text="" ToolTip="View this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="edit"><asp:HyperLink runat="server" ID="lnkEdit" CssClass="" data-args="650, 650, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/edit-observation.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="records"><asp:HyperLink runat="server" ID="lnkRecords" CssClass="gv-edit-link" data-args="650, 650, true, null, 1" Text="" ToolTip="View Observation Records" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation-records.aspx?observationId={0}") %>'></asp:HyperLink></span>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ObservationID") %>' CommandName="DeleteObservation" />
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                </Columns>


            </asp:GridView>

            <uc1:GridPager runat="server" ID="GridPager1" GridViewID="gvObsRec" />

            <mack:MessageDiv runat="server" ID="MessageDiv1" ListControlID="gvObsRec" Text="No records found!"></mack:MessageDiv>
        </div>
    </div >

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>
        $(function() {
            $("#observationTabs").tabs();

            $("#filterUpH2").click(function () {
                $('#filterUp').toggle();
                $("#filterUpH2").toggleClass("filter-hide").toggleClass("filter-show");
            });
            $("#filterRecH2").click(function () {
                $('#filterRec').toggle();
                $("#filterRecH2").toggleClass("filter-hide").toggleClass("filter-show");
            });

            //alert('close');
            //$('#div-dialog').dialog('close');
            //$(".ui-dialog-content").dialog().dialog("close");
        });
    </script>
</asp:Content>
