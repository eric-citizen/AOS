<%@ Page Title="Edit Observation" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="edit-observation.aspx.cs" Inherits="CZAOSWeb.admin.observation.edit_observation" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GroupControl.ascx" TagPrefix="uc1" TagName="GroupControl" %>
<%@ Register Src="~/controls/EditableTextBox.ascx" TagPrefix="uc1" TagName="EditableTextBox" %>
<%@ MasterType VirtualPath="~/masterpages/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/assets/scripts/jquery.timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="Static" />

    <fieldset class="form-fieldset">

            <legend>
                <asp:Literal runat="server" ID="fieldsetLegend">Edit Observation</asp:Literal>
            </legend>
        
        <asp:HiddenField runat="server" ID="hdnType" />

        <asp:MultiView runat="server" ID="mvObs" ActiveViewIndex="0">
            <asp:View runat="server" ID="vwInit">

                <div class="message question">

                    Please select the observation type:
                    <mack:RequiredDropDownList runat="server" ID="ddType" ValidatorToolTip="select an observation type" Required="true" ValidationGroup="init" InitialValue="-1"
                        ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" >
                        <asp:ListItem Selected="True" Value="-1" Text="Select Type"></asp:ListItem>
                        <asp:ListItem Value="Professional" Text="Professional"></asp:ListItem>
                        <asp:ListItem Value="School" Text="School"></asp:ListItem>
                    </mack:RequiredDropDownList>

                    <div class="clear">
                        <mack:WaitButton runat="server" ID="btnInit" Text="Continue" ValidationGroup="init" CssClass="button" OnClick="btnInit_Click" />
                    </div>

                </div>

            </asp:View>
            <asp:View runat="server" ID="vwPro">


                <div id="proHeader" style="width:100%">
                    <label>Observation Type:</label>
                    <asp:Literal runat="server" ID="litType"></asp:Literal>
                    <button ID="btnHeadBack" Class="floatRight" OnClick="history.go(-1);return false;">Back</button>
                    <br />
                    <br />
                </div>

                <div id="observers" style="width:100%;display:inline-block">

                    <h2>Observer(s)</h2>

                    <div id="numObservers" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>Number of Observers</label><br />

                        <mack:RequiredDropDownList runat="server" ID="ddNumObs" ValidatorToolTip="select the number of observers" Required="true" InitialValue="-1"
                            ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" AutoPostBack="true" OnSelectedIndexChanged="ddNumObs_SelectedIndexChanged">                                            
                        </mack:RequiredDropDownList>
                    </div>
                    <div id="attending" style="height:100%;width:50%;display:inline-block;float:right">
                            
                        <%--<div style="height: 130px; width:300px; overflow: auto; border: solid 1px #dcdcdc;" class="p5 mt5">--%>
                            <asp:ListBox runat="server" ID="lstObservers" ClientIDMode="Static" SelectionMode="Multiple" Rows="10"
                                DataTextField="DisplayName" DataValueField="UserName" CssClass="" ></asp:ListBox>
                        <%--</div>--%>
                        
                        Selected Observers:<span id="user-count" class="pl10">0</span>
                        <mack:ListBoxValidator runat="server" ID="lstval" ControlToValidate="lstObservers"
                            CssClass="error" ToolTip="Select {0} observers" SetFocusOnError="true"
                            MinimumNumberOfSelectedCheckBoxes="1" ></mack:ListBoxValidator>
                    </div>                
                </div>

                <div id="observation" style="width:100%;display:inline-block">
                    
                    <h2>Observation</h2>

                    <div id="dateTime" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>Date</label><br />
                        <mack:DatePicker runat="server" ID="dteDate" Required="true" CssClass="focusme" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            ValidatorToolTip="select the observation date" OnInit="dteDate_Init"></mack:DatePicker>
                        <br /><br />

                        <label>Start Time</label><br />
                        <mack:RequiredTextBox runat="server" ID="txtStartTime" ClientIDMode="Static" MaxLength="7" Required="true" Width="100px" CssClass="starttime"
                            ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the start time"></mack:RequiredTextBox>
                        <br /><br />

                        <label>End Time</label><br />
                        <mack:RequiredTextBox runat="server" ID="txtEndTime" ClientIDMode="Static" MaxLength="7" Required="true" Width="100px" CssClass="endtime"
                            ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the end time"></mack:RequiredTextBox>

                    </div>
                    <div id="timeDetails" style="height:100%;width:50%;display:inline-block;float:right">
                        <label>Category</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddCategory" ClientIDMode="Static" ValidatorToolTip="select a category" Required="true" InitialValue="-1"
                            ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" CssClass="cat-dd" >
                            <asp:ListItem Selected="True" Value="-1" Text="Select Category"></asp:ListItem>
                            <asp:ListItem Value="Behavior" Text="Behavior"></asp:ListItem>
                            <asp:ListItem Value="Timed" Text="Timed"></asp:ListItem>
                        </mack:RequiredDropDownList>
                        <br /><br />

                        <asp:Panel runat="server" ID="pnlTimed">
                            <label>Manual</label>
                            <asp:RadioButtonList runat="server" ID="rdoManual" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="rdo-table">
                                <asp:ListItem Value="Yes" Text="Yes" ></asp:ListItem>
                                <asp:ListItem Value="No" Text="No" Selected="True" ></asp:ListItem>
                            </asp:RadioButtonList>
                            <br /><br />

                            <label>Timer</label><br />
                            <asp:RadioButtonList runat="server" ID="rdoTimer" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="rdo-table">
                                <asp:ListItem Value="Show" Text="Show" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Hide" Text="Hide" ></asp:ListItem>
                            </asp:RadioButtonList>
                            <br /><br />

                            <label>Time Interval</label><br />
                            <mack:RequiredDropDownList runat="server" ID="ddTimeInterval" ValidatorToolTip="select an interval" Required="true" InitialValue="-1"
                            ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" >                                            
                        </mack:RequiredDropDownList>
                        </asp:Panel>
                    </div>
                </div>

                <div id="animals" style="width:100%;display:inline-block">
                                        
                    <h2>Animal(s)</h2>

                    <div id="animalInfo" style="height:100%;width:50%;display:inline-block;float:left">
                    
                        <label>Animal Region</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddAnimalRegion" ClientIDMode="Static" DataValueField="AnimalRegionCode" 
                            DataTextField="AnimalRegionName" Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            SetFocusOnError="true" AutoPostBack="true" OnSelectedIndexChanged="ddAnimalRegion_SelectedIndexChanged" ></mack:RequiredDropDownList>
                        <br /><br />


                        <label>Exhibit</label>
                        <mack:RequiredDropDownList runat="server" ID="ddExhibit" ClientIDMode="Static" DataValueField="ExhibitID" DataTextField="ExhibitName" 
                            Required="false" OnPreRender="ddExhibit_PreRender"></mack:RequiredDropDownList>
                        <br /><br />

                        <label>Groups</label>
                        <mack:RequiredDropDownList runat="server" ID="ddGroupCount" ValidatorToolTip="select the number of groups" Required="true" InitialValue="-1"
                            ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" AutoPostBack="true" OnSelectedIndexChanged="ddGroupCount_SelectedIndexChanged" >                                                 
                        </mack:RequiredDropDownList>

                    </div>
                    <div id="groupInfo" style="height:100%;width:50%;display:inline-block;float:right">
                        <asp:PlaceHolder runat="server" ID="groupsPlaceHolder">
                            <uc1:GroupControl runat="server" ID="GroupControl1" />
                            <uc1:GroupControl runat="server" ID="GroupControl2" />
                            <uc1:GroupControl runat="server" ID="GroupControl3" />
                            <uc1:GroupControl runat="server" ID="GroupControl4" />
                            <uc1:GroupControl runat="server" ID="GroupControl5" />
                            <uc1:GroupControl runat="server" ID="GroupControl6" />
                            <uc1:GroupControl runat="server" ID="GroupControl7" />
                            <uc1:GroupControl runat="server" ID="GroupControl8" />
                            <uc1:GroupControl runat="server" ID="GroupControl9" />
                            <uc1:GroupControl runat="server" ID="GroupControl10" />
                        </asp:PlaceHolder>
                        <br />

                        <span id="gc-grand-total">0</span> of 40 total animals selected.
                    </div>
                </div>

                <div id="reports" style="width:100%;display:inline-block">

                </div>

                <div class="floatRight">
                    <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" />
                </div>

            </asp:View>
            <asp:View runat="server" ID="vwEdu">

                <label>Observation Type:</label>
                <asp:Literal runat="server" ID="litSchType"></asp:Literal>
                <br /><br />

                <div id="schObservers" style="width:100%;display:inline-block">
                    <h2>Observer(s)</h2><br />

                    <div id="observerType" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>Observer Type</label><br />
                        <asp:Literal runat="server" ID="litSchObserverType" Text="Student"></asp:Literal>
                    </div>
                    <div id="numOfObservers" style="height:100%;width:50%;display:inline-block;float:right">
                        <label>Number of Observers</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddlSchoolObserverCount" ValidatorToolTip="select the number of observers" 
                            Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" 
                            AutoPostBack="false" >                                            
                        </mack:RequiredDropDownList>
                    </div>

                </div>

                <div id="school" style="width:100%;display:inline-block">
                    <h2>School</h2><br />


                    <div id="schoolInfo" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>District</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddDistrict" ClientIDMode="Static" DataValueField="DistrictID" 
                            DataTextField="District" Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            SetFocusOnError="true" ValidatorToolTip="please select a district" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddDistrict_SelectedIndexChanged" ></mack:RequiredDropDownList>
                        <br /><br />

                        <label>School</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddSchool" ClientIDMode="Static" DataValueField="SchoolID" 
                            DataTextField="SchoolName" Visible="false" Required="true" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            SetFocusOnError="true" InitialValue="-1" ValidatorToolTip="please select a school"></mack:RequiredDropDownList>

                    </div>
                    <div id="classInfo" style="height:100%;width:50%;display:inline-block;float:right">
                        <label>Grade</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddGrade" ClientIDMode="Static" DataValueField="GradeID" 
                            DataTextField="GradeName" Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            SetFocusOnError="true" ValidatorToolTip="please select a grade" ></mack:RequiredDropDownList>
                        <br /><br />

                        <label>Teacher</label><br />
                        <mack:RequiredTextBox runat="server" ID="txtTeacherName" ClientIDMode="Static" MaxLength="100" Required="false" 
                            Width="200px" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter teacher name"></mack:RequiredTextBox> 
                    
                    </div>

                </div>

                <div id="schObservation" style="width:100%;display:inline-block">
                    <h2>Observation</h2><br />

                    <div id="schDateTime" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>Date</label><br />
                        <mack:DatePicker runat="server" ID="dteSchoolDate" Required="true" CssClass="focusme" 
                            ErrorMessage="&nbsp;" ValidatorCssClass="error" ValidatorToolTip="select the observation date" 
                            OnInit="dteDate_Init"></mack:DatePicker>
                        <br /><br />

                        <label>Start</label><br />
                        <mack:RequiredTextBox runat="server" ID="txtSchoolStart" ClientIDMode="Static" MaxLength="7" 
                            Required="true" Width="100px" CssClass="starttime" ErrorMessage="*" ValidatorCssClass="error" 
                            ValidatorToolTip="Enter the start time"></mack:RequiredTextBox>     
                        <br /><br />

                        <label>End</label><br />
                        <mack:RequiredTextBox runat="server" ID="txtSchoolEnd" ClientIDMode="Static" MaxLength="7" 
                            Required="true" Width="100px" CssClass="endtime" ErrorMessage="*" ValidatorCssClass="error" 
                            ValidatorToolTip="Enter the end time"></mack:RequiredTextBox>     
                        </div>
                    <div id="schTimeDetails" style="height:100%;width:50%;display:inline-block;float:right">
                        <label>Category</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddSchoolCat" ClientIDMode="Static" ValidatorToolTip="select a category" 
                            Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" CssClass="cat-dd" >
                            <asp:ListItem Selected="True" Value="-1" Text="Select Category"></asp:ListItem>
                            <asp:ListItem Value="Behavior" Text="Behavior"></asp:ListItem>
                            <asp:ListItem Value="Timed" Text="Timed"></asp:ListItem>
                        </mack:RequiredDropDownList>
                        <br /><br />

                        <label>Manual Entry</label><br />
                        <asp:RadioButtonList runat="server" ID="rdoSchoolManual" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="rdo-table">
                            <asp:ListItem Value="Yes" Text="Yes" ></asp:ListItem>
                            <asp:ListItem Value="No" Text="No" Selected="True" ></asp:ListItem>
                        </asp:RadioButtonList>
                        <br /><br />

                        <label>Show Timer</label><br />
                        <asp:RadioButtonList runat="server" ID="rdoSchoolTimer" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="rdo-table">
                            <asp:ListItem Value="Show" Text="Show" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Hide" Text="Hide" ></asp:ListItem>
                        </asp:RadioButtonList>
                        <br /><br />

                        <label>Timed Interval</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddSchoolInterval" ValidatorToolTip="select an interval" Required="true" 
                            InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" SetFocusOnError="true" >                                            
                        </mack:RequiredDropDownList>

                    </div>


                </div>

                <div id="schAnimals" style="width:100%;display:inline-block">
                    <h2>Animal(s)</h2><br />

                    <div id="schAnimalInfo" style="height:100%;width:50%;display:inline-block;float:left">
                        <label>Region</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddSchoolAnimRegions" ClientIDMode="Static" DataValueField="AnimalRegionCode" 
                            DataTextField="AnimalRegionName" Required="true" InitialValue="-1" ErrorMessage="&nbsp;" ValidatorCssClass="error" 
                            SetFocusOnError="true" AutoPostBack="true" OnSelectedIndexChanged="ddSchoolAnimRegions_SelectedIndexChanged" ></mack:RequiredDropDownList>
                        <br /><br />

                        <label>Exhibit</label><br />
                        <mack:RequiredDropDownList runat="server" ID="ddSchoolExhibit" ClientIDMode="Static" DataValueField="ExhibitID" 
                            DataTextField="ExhibitName" Required="false" OnPreRender="ddSchoolExhibit_PreRender"></mack:RequiredDropDownList>                                   

                    </div>
                    <div id="schGroupInfo" style="height:100%;width:50%;display:inline-block;float:right">
                        <label>Animals</label><br />
                        <uc1:GroupControl runat="server" ID="schoolAnimalGroup" GroupNameVisible="false" />             

                    </div>


                </div>

                <div id="schReports" style="width:100%;display:inline-block;border:1px solid #cccccc">
                    <h2>Reports</h2><br />

                    <asp:GridView runat="server" ID="gvReports" Width="100%" CssClass="gridview" AutoGenerateColumns="false" 
                        AllowPaging="false" AllowSorting="false" PagerSettings-Visible="false" DataKeyNames="ReportID" 
                        OnRowCommand="gvReports_RowCommand" OnRowDataBound="gvReports_RowDataBound" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <uc1:EditableTextBox runat="server" id="EditableTextBox" CommandName="UpdateText" 
                                        CommandArgument='<%# Bind("ReportID") %>' Text='<%# Bind("ReportName") %>'  />                                        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemStyle Width="40px" CssClass="tac" />
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="lnkFile" NavigateUrl='<%#Bind("ReportLink") %>'  
                                        CssClass="download-link" ToolTip="download this file" ></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" 
                                        CommandArgument='<%#Bind("ReportID") %>' CommandName="DeleteReport" />
                                        
                                </ItemTemplate>                
                                <ItemStyle Width="60px" CssClass="tac" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                        
                    <mack:MessageDiv runat="server" ID="divEmptyReports" Text="No reports for this observation." 
                        MessageType="info" ListControlID="gvReports"></mack:MessageDiv><br />                     

                    <asp:HyperLink runat="server" ID="lnkUpload" Text="Upload Report" CssClass="ui-dialog-link upload-link" 
                        data-args="350, 500, true, null, 1" ></asp:HyperLink>
                </div>

                <div class="floatRight">
                    <mack:WaitButton runat="server" ID="btnSchSave" OnClick="btnSaveEdu_Click" CssClass="button" Text="Save" />
                </div>

            </asp:View>
        </asp:MultiView>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script src="/assets/scripts/jquery.timepicker.js"></script>


    <script>
        //http://jonthornton.github.io/jquery-timepicker/

        Date.prototype.addHours = function (h) {
            this.setHours(this.getHours() + h);
            return this;
        }

        $('#ddCategory, #ddSchoolCat').on('change', function () {
            if (this.value != "-1" && this.value === "Behavior") {
                $("#pnlTimed").hide();
                //ValidatorEnable(val, enable)
            }
            if (this.value != "-1" && this.value === "Timed") {
                $("#pnlTimed").show();
            }
            else {
                
            }
        });

        $(function () {
            
            var dd = $('.cat-dd').find(":selected").text();

            if (dd != "-1" && dd === "Behavior") {
                $(".timed").hide();                
            }
            if (dd != "-1" && dd === "Timed") {
                $(".timed").show(); 
            }
        });

        //$('#ddDistrict').on('change', function () {
            
        //    if (this.value != "-1")
        //    {
        //        GetSchoolList(this.value);
        //    }
        //    else
        //    {
        //        $("#ddSchool").hide();
        //    }
        //});

        //$('#ddSchool').on('change', function () {
        //    $("#hdnSchoolID").text(this.value);
        //});       

        $(function () {

            $('.starttime').timepicker({ 'minTime': '8:00am', 'maxTime': '11:00pm', 'showDuration': false, 'timeFormat': 'h:i A' });
            $('.endtime').timepicker({ 'minTime': '8:00am', 'maxTime': '11:00pm', 'showDuration': false, 'timeFormat': 'h:i A'  });

            $('.starttime').on('changeTime', function () {
                var a = "1/1/2000 " + $(this).val();                
                var d = Date.parse(a);
                console.log(d);
                var dd = new Date(d);
                console.log(dd);
                var ddd = new Date(dd.setHours(dd.getHours() + 1));
                console.log(ddd);
                $('.endtime').timepicker('setTime', ddd);
            });
        });

        $('#lstObservers :selected').click(function () {
            var $this = $(this);
            var count = parseInt($("#user-count").html());
            // $this will contain a reference to the checkbox   
            if ($this.is(':checked')) {                
                count++;

            } else {
                // the checkbox was unchecked
                if (count > 0)
                    count--;
            }
            $("#user-count").html(count)

        });

        $(function () {

            var count = parseInt($("#user-count").html());
            $('#cbxObservers :selected').each(function () {

                var $this = $(this);
                if ($this.is(':selected')) {
                    count++;
                }
            });

            $("#user-count").html(count)
        });
        
        $(".group-cbx input").on('change', function () {

            var tab = $(this).closest('table');
            var id = ($(this).closest('table').attr("id"));
            var max = 15;
            var gmax = 40;
            var numberOfChecked = $('#' + id + ' tr td input:checked').length;          
           

            if (numberOfChecked > max)
            {
                $(this).removeAttr("checked");
                toastr.error("You have already selected the maximum of " + max + " animals for this group.");
                numberOfChecked--;
            }            

            tab.parent().next().text(numberOfChecked);

            var gtotal = 0;
            $(".gc-count").each(function () {
                if (!isNaN($(this).text())) {
                    count = parseInt($(this).text());
                    gtotal = gtotal + count;                    
                }
            });

            if (gtotal > gmax) {
                $(this).removeAttr("checked");
                toastr.error("You have already selected the maximum of " + gmax + " total animals for this observation.");
                gtotal--;
            }

            $("#gc-grand-total").text(gtotal);

        });

        $(function () {

            var gtotal = 0;
            var max = 15;
            var gmax = 40;

            $(".group-cbx").each(function () {

                var id = ($(this).attr("id"));                
                var numberOfChecked = $('#' + id + ' tr td input:checked').length;              

                $(this).parent().next().text(numberOfChecked);                
                gtotal = gtotal + numberOfChecked;

            });

            $("#gc-grand-total").text(gtotal);

        });
         

	</script>


</asp:Content>
