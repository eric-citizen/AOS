<%@ Page Title="Manual Record Entry" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="create-observation-records.aspx.cs" Inherits="CZAOSWeb.admin.observation.create_observation_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Manual Record Entry</asp:Literal>
        </legend>

        <asp:HiddenField runat="server" ID="hdnID" />

        <asp:MultiView runat="server" ID="mvObs" ActiveViewIndex="0">
            <asp:View runat="server" ID="vwWeather">

                <h2>Enter Environment Conditions</h2>

                Time Start:
                <mack:RequiredTextBox runat="server" ID="txtStartTime" Required="true" MaxLength="8" ValidationGroup="weather"
                     ValidatorToolTip="Correct start time if necessary" ToolTip="Correct start time if necessary" ></mack:RequiredTextBox>

                Weather Condition:
                <mack:RequiredDropDownList runat="server" ID="ddlWeather" Required="true" DataValueField="WeatherID" 
                    DataTextField="Weather" ValidationGroup="weather" ValidatorToolTip="Select weather condition"  InitialValue="-1"
                    Tooltip="Select weather condition"></mack:RequiredDropDownList>

                Wind:
                <mack:RequiredDropDownList runat="server" ID="ddlWind" Required="true" DataValueField="WindID" 
                    DataTextField="Description" ValidationGroup="weather" ValidatorToolTip="Select wind speed"  InitialValue="-1"
                    Tooltip="Select wind speed"></mack:RequiredDropDownList>

                Temprature:
                <mack:RequiredTextBox runat="server" ID="txtTemp" Required="true" ValidationGroup="weather"
                    ValidatorToolTip="Enter temprature" ToolTip="Enter temprature"></mack:RequiredTextBox>

                Crowd:
                <mack:RequiredDropDownList runat="server" ID="ddlCrowd" Required="true" DataValueField="CrowdID" 
                    DataTextField="CrowdName" ValidationGroup="weather" ValidatorToolTip="Select crowd size"  InitialValue="-1"
                    Tooltip="Select crowd size"></mack:RequiredDropDownList>

                <asp:Button runat="server" ID="btnSubEnv" OnClick="btnSubEnv_Click" Text="Save Environment" ToolTip="Submit Environment Conditions"/>

            </asp:View>
            <asp:View runat="server" ID="vwRecords">
                <div class="weather" id="weather">
                    <h2>Weather Conditions</h2>
                    <div class="innerWeather">
                        <label>Temprature</label>
                        <asp:Literal runat="server" ID="litTemp"></asp:Literal>
                    </div>
                    <div class="innerWeather">
                        <label>Weather Condition</label>
                        <asp:Literal runat="server" ID="litWeatherCond"></asp:Literal>
                    </div>
                    <div class="innerWeather">
                        <label>Wind</label>
                        <asp:Literal runat="server" ID="litWind"></asp:Literal>
                    </div>
                    <div class="innerWeather">
                        <label>Crowd</label>
                        <asp:Literal runat="server" ID="litCrowd"></asp:Literal>
                    </div>
                </div>
            </asp:View>

        </asp:MultiView>
    </fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
