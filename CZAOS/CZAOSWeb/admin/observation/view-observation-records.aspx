﻿<%@ Page Title="View Observation Records" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="view-observation-records.aspx.cs" Inherits="CZAOSWeb.admin.observation.view_observation_records" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
        <div id="ObservationRecordList">

            <asp:HiddenField runat="server" ID="hdnID" />

            <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
                SelectMethod="GetItemCollection" TypeName="CZBizObjects.ObservationRecordList"
                EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">

                <SelectParameters>
                    <asp:Parameter Name="filterExpression" Type="string" DefaultValue="" />
                </SelectParameters>

            </asp:ObjectDataSource>

            <mack:GridViewSortExtender runat="server" ID="gvse"
                AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvObs" TransparentImageUrl="~/images/transparent.png" />

            <asp:GridView ID="gvObs" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
                PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false"
                DataKeyNames="ObservationID"><%--OnRowCommand="gvObs_RowCommand"  OnRowDataBound="gvObs_RowDataBound"--%>
                <Columns>

                    <asp:BoundField DataField="Username" SortExpression="Username" HeaderText="Username" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="ObserverTime" SortExpression="ObserverTime" HeaderText="Time" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="Behavior" SortExpression="Behavior" HeaderText="Behavior" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="LocationID" SortExpression="LocationID" HeaderText="Location" ItemStyle-Width="100px"></asp:BoundField>
                    <asp:BoundField DataField="Flagged" SortExpression="Flagged" HeaderText="Flagged" ItemStyle-Width="100px"></asp:BoundField>

                </Columns>


            </asp:GridView>

            <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvObs" />

            <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvObs" Text="No records found!"></mack:MessageDiv>
        </div>

     

</asp:Content>