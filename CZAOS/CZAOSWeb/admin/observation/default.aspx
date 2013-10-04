﻿<%@ Page Title="Manage Observations" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.observation._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div id="observationTabs">
        <ul>
            <li><a href="#RecentObservations">Recent Observations</a></li>
            <li><a href="#UpcomingObservations">Upcoming Observations</a></li>
        </ul>

        <div id="RecentObservations">
            <div onClick="$('#filterRec').toggle();">
                <hr />
                <h2>Filter Sessions</h2></div>
                <div id="filterRec">              
                    <hr />
                    <div id="AnimalDateRec" style="width:50%;float:left">
                        <label>Animal</label><br />
                        <asp:DropDownList id="AnimalListRec" AutoPostBack="True" runat="server"/><br />

                        <label>Timeframe</label><br />
                        <input type="date" id="dateFromRec" runat="server" /> -- <input type="date" id="dateToRec" runat="server"/><br />

                        <asp:RadioButton id="timedRec" runat="server" Checked="True" Text="Timed" AutoPostBack="false" GroupName="BehaviorTimed" /><br />
                        <asp:RadioButton ID="behaviorRec" runat="server" Text="Behavior" AutoPostBack="false" GroupName="BehaviorTimed" /><br />
                    </div>

                    <div id="schoolRec" style="width:50%;float:right">
                        <asp:CheckBox id="includeStudentObservationsRec" runat="server" Checked="true" Text="Include Student Observations" AutoPostBack="false"/><br />

                        <label>District</label><br />
                        <asp:DropDownList id="DistrictListRec" AutoPostBack="true" OnSelectedIndexChanged="districtChange" runat="server"/><br />

                        <label>School</label><br />
                        <asp:DropDownList id="SchoolListRec" AutoPostBack="True" runat="server"/><br />
                    </div>

                    <%--<input  type="button" onclick="" value="Search" runat="server"/>--%>
                    <asp:Button id="searchRec" Text="Search" runat="server" PostBackUrl="./#RecentObservations" />
                </div>

            <hr />
            <h2>Recent Observations</h2>
            <hr />
            <a class="add-link ui-dialog-link" data-args="650, 650, true, null, 1" href="/admin/observation/edit-observation.aspx">Add New Observation</a>

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
                            <asp:HyperLink runat="server" ID="lnkView" CssClass="gv-view-link" data-args="650, 650, true, null, 1" Text="View" ToolTip="View this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation.aspx?observationId={0}") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkEdit" CssClass="gv-edit-link" data-args="650, 650, true, null, 1" Text="Edit" ToolTip="Edit this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/edit-observation.aspx?observationId={0}") %>'></asp:HyperLink>
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
        <div id="UpcomingObservations">
            <div onClick="$('#filterUp').toggle();"><h2>Filter Sessions</h2></div>
            <div id="filterUp">              

                <div id="AnimalDateUp" style="width:50%;float:left">
                    <label>Animal</label><br />
                    <asp:DropDownList id="AnimalListUp"
                        AutoPostBack="True"
                        runat="server"/><br />

                    <label>Timeframe</label><br />
                    <input type="date" id="fromUp" /> -- <input type="date" id="toUp" /><br />

                    <input type="checkbox" checked id="timedUp" />Timed<br />
                    <input type="checkbox" checked id="behaviorUp" />Behavior<br />

                </div>

                <div id="schoolUp" style="width:50%;float:right">

                    <input type="checkbox" checked id="includeStudentObservationsUp" />Include Student Observations<br />

                    <label>District</label><br />
                    <asp:DropDownList id="DistrictListUp"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="districtChange"
                        runat="server"/><br />

                    <label>School</label><br />
                    <asp:DropDownList id="SchoolListUp"
                        AutoPostBack="True"
                        runat="server"/><br />

                </div>
                <input id="searchUp" type="button" onclick="" value="Search"/>
            </div>
            <br />
            <h2>Upcoming Observations</h2>
            <br />
            <a class="add-link ui-dialog-link" data-args="650, 650, true, null, 1" href="/admin/observation/edit-observation.aspx">Add New Observation</a>

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

                    <asp:BoundField DataField="ObserveStart" SortExpression="ObserveStart" HeaderText="Date" DataFormatString="{0:dddd, MMMM dd yyy hh:mm tt}"></asp:BoundField>
                    <asp:BoundField DataField="ObservationID" SortExpression="ObservationID" HeaderText="Obs.ID" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="ObserveType" SortExpression="ObserveType" HeaderText="User Type" ItemStyle-Width="150px"></asp:BoundField>
                    <asp:BoundField DataField="Exhibit" SortExpression="Exhibit" HeaderText="Exhibit" ItemStyle-Width="150px"></asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkView" CssClass="ui-dialog-link gv-view-link" data-args="650, 650, true, null, 1" Text="View" ToolTip="View this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/view-observation.aspx?observationId={0}") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="60px" CssClass="tac" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="650, 650, true, null, 1" Text="Edit" ToolTip="Edit this item" NavigateUrl='<%# Bind("ObservationID","~/admin/observation/edit-observation.aspx?observationId={0}") %>'></asp:HyperLink>
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
    </div >

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>
        $(function() {
            $("#observationTabs").tabs();

            //alert('close');
            //$('#div-dialog').dialog('close');
            //$(".ui-dialog-content").dialog().dialog("close");
        });
    </script>
</asp:Content>
