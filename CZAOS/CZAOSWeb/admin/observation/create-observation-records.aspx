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

                <div class="environment">
                    <h2>Enter Environment Conditions</h2>

                    <div class="environmentInner block">
                        <label>Time Start:</label>
                        <mack:RequiredTextBox runat="server" ID="txtStartTime" Required="true" MaxLength="8" ValidationGroup="weather"
                            ErrorMessage="Please enter a valid Start Time" TextMode="Time" ToolTip="Correct start time if necessary" Width="80px"
                             ValidatorCssClass="hidden"></mack:RequiredTextBox>
                    </div>

                    <div class="environmentInner">
                        <label>Weather Condition:</label>
                        <asp:DropDownList runat="server" ID="ddlWeather" Required="true" DataValueField="WeatherID"
                            DataTextField="Weather" ValidationGroup="weather" ValidatorToolTip="Select weather condition"
                            ToolTip="Select weather condition" ErrorMessage="Please select a Wind Condition" ValidatorCssClass="hidden">
                        </asp:DropDownList>
                    </div>

                    <div class="environmentInner">
                        <label>Temprature:</label>
                        <mack:RequiredTextBox runat="server" ID="txtTemp" Required="true" ValidationGroup="weather"
                            ValidatorToolTip="Enter temprature" ToolTip="Enter temprature" TextMode="Number" ValidatorCssClass="hidden"
                            ErrorMessage="Please enter a whole number"></mack:RequiredTextBox>
                    </div>

                    <div class="environmentInner">
                        <label>Wind:</label>
                        <asp:DropDownList runat="server" ID="ddlWind" Required="true" DataValueField="WindID"
                            DataTextField="Description" ValidationGroup="weather" ValidatorToolTip="Select wind speed"
                            ToolTip="Select wind speed" ErrorMessage="Please select a Wind Category"  ValidatorCssClass="hidden">
                        </asp:DropDownList>
                    </div>

                    <div class="environmentInner block">
                        <label>Crowd:</label>
                        <asp:DropDownList runat="server" ID="ddlCrowd" Required="true" DataValueField="CrowdID"
                            DataTextField="CrowdName" ValidationGroup="weather" ValidatorToolTip="Select crowd size" ValidatorCssClass="hidden"
                            ToolTip="Select crowd size" ErrorMessage="Please select a Crowd Size">
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="floatLeft">
                    <asp:ValidationSummary runat="server" ID="enviroValidation" ValidationGroup="weather" CssClass="password_strength" ShowSummary="true"/>
                </div>
                <asp:Button runat="server" ID="btnbEnv" CssClass="button floatRight" OnClick="btnSubEnv_Click" CausesValidation="true"
                    ValidationGroup="weather" Text="Save Environment" ToolTip="Submit Environment Conditions" />


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

                <div id="observationRecordEntry">

                </div>

                <div id="observationRecordList">

                </div>

            </asp:View>

        </asp:MultiView>
    </fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
