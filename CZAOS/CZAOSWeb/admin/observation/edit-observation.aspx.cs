using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore;
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;
using CZAOS.controls;

namespace CZAOSWeb.admin.observation
{
    public partial class edit_observation : MainBase 
    {
        private int ObservationID
        {
            get
            {
                if (Request.QueryString.Contains("observationID"))
                {
                    string s = Request.QueryString["observationID"];
                    if (s.IsNumeric())
                    {
                        hdnID.Value = s;
                    }
                    else
                        hdnID.Value = 0.ToString();

                }
                else
                {
                    hdnID.Value = 0.ToString();
                }

                return hdnID.Value.ToInt32();
            }
        }

        #region PRO

        public int GroupCount
        {
            get
            {
                return Convert.ToInt32(Session["GroupCount"]);
            }
            set
            {
                Session["GroupCount"] = value;
            }

        }

        public string AnimalRegionCode
        {
            get
            {
                return Convert.ToString(Session["AnimalRegionCode"]);
            }
            set
            {
                Session["AnimalRegionCode"] = value;
            }

        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            this.LoadAnimalRegions();
            this.LoadSchoolAnimalRegions();
            this.LoadDistricts();
            this.LoadGrades();
            this.LoadSchoolDropDowns();  
    
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //TODO - if czt_ObservationRecord has records for this obs then it is not editable
            if (!this.IsPostBack)
            {                
                this.GroupCount = 0;
                this.AnimalRegionCode = string.Empty;
                this.LoadIntervals();

                this.InitializeGroups();

                if(this.ObservationID > 0)
                    this.LoadData();

            }

            if (this.ObservationID == 0)
            {
                //add a new animal!
                Master.ContentTitle = "Add New Observation";
                fieldsetLegend.Text = "Add New Observation";

                gvReports.Visible = false;
                divEmptyReports.Text = "Observations must be saved first before reports can be uploaded.";
                lnkUpload.Visible = false;
                     
            }
            else
            {
                Master.ContentTitle = "Edit Observation";
                fieldsetLegend.Text = "Edit Observation";
                
            }            

            //this.CheckTotalGroupCount();
        }

        private void InitializeGroups()
        {
            for (int i = 0; i < 10; i++)
            {
                CZAOSWeb.controls.GroupControl gc = groupsPlaceHolder.Controls[i] as CZAOSWeb.controls.GroupControl;
                gc.Clear();
                gc.Visible = false;
            }

            ddGroupCount.SelectedIndex = 0;
        }

        private void LoadData()
        {
            Observation obs = ObservationList.GetItem(this.ObservationID);

            if (obs == null)
            {
                this.DisplayError("Observation ID {0} not found!".FormatWith(this.ObservationID));
            }
            else
            {
                litType.Text = obs.ObservationTypeName;

                if (obs.ObserveType == Observation.ObservationTypeEnum.Professional )
                {
                    mvObs.ActiveViewIndex = 1;

                    this.LoadObservers(true);

                    dteDate.Text = obs.ObserveStart.ToShortDateString();
                    txtStartTime.Text = obs.ObserveStart.ToShortTimeString();
                    txtEndTime.Text = obs.ObserveEnd.ToShortTimeString();

                    ddCategory.SelectListItemByValue(obs.Category);

                    ddTimeInterval.SelectListItemByValue(obs.Interval.ToString());
                    rdoTimer.SelectedIndex = obs.Timer ? 0 : 1;
                    rdoManual.SelectedIndex = obs.Manual ? 0 : 1;

                    ddNumObs.SelectListItemByValue(obs.ObserverNo.ToString());
                    ddExhibit.SelectListItemByValue(obs.ExhibitID.ToString());
                    Exhibit ex = ExhibitList.Get(obs.ExhibitID);
                    ddAnimalRegion.SelectListItemByValue(ex.AnimalRegionCode);
                    this.AnimalRegionCode = ex.AnimalRegionCode;
                    this.LoadExhibits();
                    ddExhibit.SelectListItemByValue(obs.ExhibitID.ToString());

                    AnimalGroupList animalGroups = AnimalGroupList.GetActiveByObservationID(obs.ObservationID);
                    ddGroupCount.SelectListItemByValue(animalGroups.Count.ToString());
                    this.GroupCount = animalGroups.Count;                                     

                    AnimalObservationList aol = AnimalObservationList.GetActive(obs.ObservationID);

                    this.LoadGroups();

                    for (int i = 1; i < this.GroupCount + 1; i++)
                    {
                        CZAOSWeb.controls.GroupControl gc = groupsPlaceHolder.Controls[i - 1] as CZAOSWeb.controls.GroupControl;
                        gc.Count = i.ToString();
                        //gc.LoadGroup(this.AnimalRegionCode);
                        gc.GroupName = animalGroups[i - 1].GrpName;
                        gc.SelectedAnimalIDs = aol.GetAnimalIDsByGroup(i);                        

                    }

                    ObserverList olist = ObserverList.GetActive(obs.ObservationID);

                    foreach (ListItem lio in lstObservers.Items)
                    {
                        if (olist.ContainsUser(lio.Value))
                        {
                            lio.Selected = true;
                        }
                    }                    

                }
                else
                {
                    mvObs.ActiveViewIndex = 2;
                    this.LoadEduData(obs);
                }                

            }

        }
        
        //TODO: Refactor
        private void LoadIntervals()
        {
            for (int i = 1; i < 16; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());                

                if (!this.IsPostBack && i == 1)
                {
                    item.Selected = true;
                }

                ddTimeInterval.Items.Add(item);
            }

            for (int i = 1; i < 16; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());

                if (!this.IsPostBack && i == 1)
                {
                    item.Selected = true;
                }

                ddNumObs.Items.Add(item);
            }

            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());

                if (!this.IsPostBack && i == 1)
                {
                    //item.Selected = true;
                }

                ddGroupCount.Items.Add(item);
            }

            ddGroupCount.AddEmptyListItem("Select Group Count", "-1");
            
        }        

        private void LoadObservers(bool pro)
        {
            UserList users;

            //lstObservers
            if (pro)
            {
                users = UserList.GetActiveProfessionalUsers();
            }
            else
            {
                users = UserList.GetActiveAmateurUsers();
            }

            lstObservers.DataSource = users;
            lstObservers.DataBind();

        }

        private void LoadAnimalRegions()
        {
            ddAnimalRegion.DataSource = AnimalRegionList.GetActive();
            ddAnimalRegion.DataBind();
            ddAnimalRegion.AddEmptyListItem("Select Region", "-1");
        }

        private void LoadExhibits()
        {
            ddExhibit.DataSource = ExhibitList.GetActiveByRegion(ddAnimalRegion.SelectedValue);
            ddExhibit.DataBind();
            ddExhibit.AddEmptyListItem("Select Exhibit", "-1");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {                       
            if (this.IsValid)
            {
                this.SaveProObservation();
            }
            else
            {
                string valerrs = this.GetValidationErrorText();
                this.DisplayError("There are one or more data errors. Please correct and try again:<br/>" + valerrs);
            }
        }
               
        protected void btnInit_Click(object sender, EventArgs e)
        {
            hdnType.Value = ddType.SelectedValue;
            mvObs.ActiveViewIndex = ddType.SelectedIndex;

            fieldsetLegend.Text += " - " + hdnType.Value;
            litType.Text = hdnType.Value;

            if (ddType.SelectedIndex == 1) //PRO
            {
                this.LoadObservers(true);
            }
            else //EDU-AM ddType.SelectedIndex == 2
            {
                this.LoadObservers(false);
            }
        }

        protected void ddNumObs_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstval.MinimumNumberOfItems = ddNumObs.SelectedValue.ToInt32();
            lstval.ToolTip = "Select {0} observers".FormatWith(lstval.MinimumNumberOfItems);
        }

        protected void ddGroupCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GroupCount = ddGroupCount.SelectedValue.ToInt32();
            this.AnimalRegionCode = ddAnimalRegion.SelectedValue;

            this.LoadGroups();           
        }

        private void LoadGroups()
        {
            //groupsPlaceHolder.Controls.Clear();
            //litGroupMax.Text = string.Empty;

            if (AnimalRegionCode.IsNotNullOrEmpty() && !AnimalRegionCode.Equals("-1"))
            {
                int count = 0;

                for (int i = 0; i < 10; i++)
                {
                    count++;

                    if (count <= this.GroupCount)
                    {                       
                        CZAOSWeb.controls.GroupControl gc = groupsPlaceHolder.Controls[i] as CZAOSWeb.controls.GroupControl;
                        gc.Visible = true;
                        gc.Count = count.ToString();
                        gc.LoadGroup(AnimalRegionCode);

                        if (this.GroupCount == 1)
                        {
                            gc.GroupNameVisible = false;
                        }
                    }
                    else
                    {                        
                        CZAOSWeb.controls.GroupControl gc = groupsPlaceHolder.Controls[i] as CZAOSWeb.controls.GroupControl;
                        gc.Clear();
                        gc.Visible = false;
                        gc.GroupNameVisible = true;
                    }
                }
            }


            
        }

        //private void CheckTotalGroupCount()
        //{
        //    int total = 0;
        //    SysCode config = SysCodeList.Get("maxanimalcount");
        //    int max = config.Value.ToInt32();

        //    foreach (Control c in groupsPlaceHolder.Controls)
        //    {
        //        CZAOSWeb.controls.GroupControl gc = c as CZAOSWeb.controls.GroupControl;
        //        int si = gc.SelectedCount;

        //        total += si;

        //        if (total > max)
        //        {
        //            gc.DeselectLastOption();
        //        }

        //    }

        //    //litGroupMax.Text = "{0} of {1} total animals selected.".FormatWith(total, max);

        //    if (total > max)
        //    {
        //        this.Toast(PageExtensions.ToastMessageType.error, "You have exceeded the maximum animal selections");
        //    }
        //}

        private void SaveProObservation()
        {
            if (this.ObservationID == 0)
            {
                if (this.CreateNewProObservation())
                {
                    var strRedirect = string.Format("view-observation.aspx?observationId={0}", ObservationID);
                    Response.Redirect(strRedirect, false);
                }

            }
            else
            {
                if (this.UpdateProObservation())
                {
                    var strRedirect = string.Format("view-observation.aspx?observationId={0}", ObservationID);
                    Response.Redirect(strRedirect, false);
                }

            }
        }

        private bool CreateNewProObservation()
        {
            Observation obs = new Observation();

            obs = new Observation();
            obs.ObserveType = Observation.ObservationTypeEnum.Professional;

            PasswordGenerator pg = new PasswordGenerator();
            pg.IncludeLower = true;
            pg.IncludeNumber = true;
            pg.IncludeSpecial = false;
            pg.IncludeUpper = true;
            pg.MinimumLength = 8;
            pg.MaximumLength = 8;

            obs.TeacherName = pg.Create();
            obs.TeacherLogin = pg.Create();
            obs.TeacherPass = pg.Create();
            obs.StudentPass = pg.Create();

            DateTime start = Convert.ToDateTime(txtStartTime.Text);
            obs.ObserveStart = dteDate.DateValue.AddHours(start.Hour).AddMinutes(start.Minute);

            DateTime end = Convert.ToDateTime(txtEndTime.Text);
            obs.ObserveEnd = dteDate.DateValue.AddHours(end.Hour).AddMinutes(end.Minute);

            if (!end.IsAfterDate(start))
            {
                this.Toast(PageExtensions.ToastMessageType.error, "End time must be after start time.", "Data Error");
                return false;
            }

            obs.Category = ddCategory.SelectedItem.Text;
            if (ddCategory.SelectedIndex == 1) //behaviour
            {

            }
            else //timed
            {
                obs.Interval = ddTimeInterval.SelectedValue.ToInt32();
                obs.Timer = (rdoTimer.SelectedIndex == 0);
                obs.Manual = (rdoManual.SelectedIndex == 0);
            }

            obs.ObserverNo = ddNumObs.SelectedValue.ToInt32();

            obs.ExhibitID = ddExhibit.SelectedValue.ToInt32();

            Observation newObs = ObservationList.AddItem(obs);
            AnimalObservationList.DeleteByObservation(newObs.ObservationID);

            foreach (Control c in groupsPlaceHolder.Controls)
            {
                CZAOSWeb.controls.GroupControl gc = c as CZAOSWeb.controls.GroupControl;

                if (gc.Visible)
                {
                    int groupId = gc.Count.ToInt32();
                    //TODO: animal-group table
                    AnimalGroup ag = new AnimalGroup();
                    ag.Deleted = false;
                    ag.GrpID = groupId;
                    ag.GrpName = gc.GroupName;
                    ag.ObservationID = newObs.ObservationID;

                    AnimalGroupList.AddItem(ag);

                    foreach (int aid in gc.SelectedAnimalIDs)
                    {
                        AnimalObservation ao = new AnimalObservation();
                        ao.ObservationID = newObs.ObservationID;
                        ao.GroupID = groupId;
                        ao.AnimalID = aid;

                        AnimalObservationList.AddItem(ao);
                    }
                }

            }

            foreach (ListItem observer in lstObservers.Items)
            {
                if (observer.Selected)
                {
                    //TODO: observer table
                    Observer newGuy = new Observer();
                    newGuy.Deleted = false;
                    newGuy.Locked = false;
                    newGuy.ObservationID = newObs.ObservationID;
                    newGuy.Username = observer.Value;

                    ObserverList.AddItem(newGuy);
                }
                
            }

            CZAOSMail.Mail.ObservationMail mailer = new CZAOSMail.Mail.ObservationMail();
            mailer.SendNewObservationEmail(obs);

            this.Toast(PageExtensions.ToastMessageType.success, "Observation created successfully", "Record Created");

            return true;
        }

        private bool UpdateProObservation()
        {
            Observation obs = ObservationList.Get(this.ObservationID);

            //PasswordGenerator pg = new PasswordGenerator();
            //pg.IncludeLower = true;
            //pg.IncludeNumber = true;
            //pg.IncludeSpecial = false;
            //pg.IncludeUpper = true;
            //pg.MinimumLength = 8;
            //pg.MaximumLength = 8;

            //obs.TeacherName = pg.Create();
            //obs.TeacherLogin = pg.Create();
            //obs.TeacherPass = pg.Create();
            //obs.StudentPass = pg.Create();

            obs.ObserveType = Observation.ObservationTypeEnum.Professional;

            DateTime start = Convert.ToDateTime(txtStartTime.Text);
            obs.ObserveStart = dteDate.DateValue.AddHours(start.Hour).AddMinutes(start.Minute);

            DateTime end = Convert.ToDateTime(txtEndTime.Text);
            obs.ObserveEnd = dteDate.DateValue.AddHours(end.Hour).AddMinutes(end.Minute);

            if (!end.IsAfterDate(start))
            {
                this.Toast(PageExtensions.ToastMessageType.error, "End time must be after start time.", "Data Error");
                return false;
            }

            obs.Category = ddCategory.SelectedItem.Text;
            if (ddCategory.SelectedIndex == 1) //behaviour
            {

            }
            else //timed
            {
                obs.Interval = ddTimeInterval.SelectedValue.ToInt32();
                obs.Timer = (rdoTimer.SelectedIndex == 0);
                obs.Manual = (rdoManual.SelectedIndex == 0);
            }

            obs.ObserverNo = ddNumObs.SelectedValue.ToInt32();

            obs.ExhibitID = ddExhibit.SelectedValue.ToInt32();

            ObservationList.UpdateItem(obs);
            AnimalObservationList.DeleteByObservation(obs.ObservationID);
            AnimalGroupList.DeleteByObservation(obs.ObservationID);

            foreach (Control c in groupsPlaceHolder.Controls)
            {
                CZAOSWeb.controls.GroupControl gc = c as CZAOSWeb.controls.GroupControl;

                if (gc.Visible)
                {
                    int groupId = gc.Count.ToInt32();
                    //TODO: animal-group table
                    AnimalGroup ag = new AnimalGroup();
                    ag.Deleted = false;
                    ag.GrpID = groupId;
                    ag.GrpName = gc.GroupName;
                    ag.ObservationID = obs.ObservationID;

                    AnimalGroupList.AddItem(ag);

                    foreach (int aid in gc.SelectedAnimalIDs)
                    {
                        AnimalObservation ao = new AnimalObservation();
                        ao.ObservationID = obs.ObservationID;
                        ao.GroupID = groupId;
                        ao.AnimalID = aid;

                        AnimalObservationList.AddItem(ao);
                    }
                }

            }

            //clear out observers then add selected
            ObserverList.DeleteByObservation(this.ObservationID);
            foreach (ListItem observer in lstObservers.Items)
            {
                if (observer.Selected)
                {
                    //TODO: observer table
                    Observer newGuy = new Observer();
                    newGuy.Deleted = false;
                    newGuy.Locked = false;
                    newGuy.ObservationID = obs.ObservationID;
                    newGuy.Username = observer.Value;

                    ObserverList.AddItem(newGuy);
                }                
            }

            CZAOSMail.Mail.ObservationMail mailer = new CZAOSMail.Mail.ObservationMail();
            mailer.SendNewObservationEmail(obs);

            this.Toast(PageExtensions.ToastMessageType.success, "Observation updated successfully", "Record Updated");

            return true;
             
        }

        protected void ddAnimalRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadExhibits();
            this.InitializeGroups();      
        }

        protected void ddExhibit_PreRender(object sender, EventArgs e)
        {
            if (ddExhibit.Items.Count == 0)
            {
                ddExhibit.CssClass = "hidden";
                ddGroupCount.CssClass = "hidden";
            }
            else
            {
                ddExhibit.CssClass = "";
                ddGroupCount.CssClass = "";
            }
        }

        #endregion

        #region EDU

        private void LoadEduData(Observation obs)
        {           
            this.LoadObservers(false);

            litSchType.Text = obs.ObservationTypeName;


            dteSchoolDate.Text = obs.ObserveStart.ToShortDateString();
            txtSchoolStart.Text = obs.ObserveStart.ToShortTimeString();
            txtSchoolEnd.Text = obs.ObserveEnd.ToShortTimeString();

            ddSchoolCat.SelectListItemByValue(obs.Category);

            if (ddSchoolCat.SelectedIndex == 1) //behaviour
            {

            }
            else //timed
            {
                ddSchoolInterval.SelectListItemByValue(obs.Interval.ToString()) ;
            }
            
            ddDistrict.SelectListItemByValue(obs.DistrictID.ToString());

            this.LoadSchools(obs.DistrictID);
            ddSchool.SelectListItemByValue(obs.SchoolID.ToString());
            ddSchool.Visible = true;
            ddGrade.SelectListItemByValue(obs.GradeID.ToString());

            txtTeacherName.Text = obs.TeacherName;

            ddlSchoolObserverCount.SelectListItemByValue(obs.ObserverNo.ToString());
            ddSchoolExhibit.SelectListItemByValue(obs.ExhibitID.ToString());
            Exhibit ex = ExhibitList.Get(obs.ExhibitID);
            ddSchoolAnimRegions.SelectListItemByValue(ex.AnimalRegionCode);
            this.AnimalRegionCode = ex.AnimalRegionCode;
            this.LoadSchoolExhibits();
            ddSchoolExhibit.SelectListItemByValue(obs.ExhibitID.ToString());

            schoolAnimalGroup.LoadGroup(ddSchoolAnimRegions.SelectedValue);
            AnimalGroupList animalGroups = AnimalGroupList.GetActiveByObservationID(obs.ObservationID);
            AnimalGroup animalGroup = animalGroups[0];     
            AnimalObservationList aol = AnimalObservationList.GetActive(obs.ObservationID);

            schoolAnimalGroup.SelectedAnimalIDs = aol.GetAnimalIDsByGroup(1);

            this.LoadReports();
            
        }

        private void LoadGrades()
        {
            ddGrade.DataSource = GradeList.GetActive();
            ddGrade.DataBind();
            ddGrade.AddEmptyListItem("Select Grade", "-1");
        }

        private void LoadDistricts()
        {
            SchoolDistrictList list = SchoolDistrictList.GetActive();
            ddDistrict.DataSource = list;
            ddDistrict.DataBind();
            ddDistrict.AddEmptyListItem("Select District", "-1");

        }

        private void LoadSchoolDropDowns()
        {
            for (int i = 1; i < 16; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());

                if (!this.IsPostBack && i == 1)
                {
                    item.Selected = true;
                }

                ddSchoolInterval.Items.Add(item);
            }

            for (int i = 1; i < 51; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());

                if (!this.IsPostBack && i == 1)
                {
                    item.Selected = true;
                }

                ddlSchoolObserverCount.Items.Add(item);
            }

        }

        private void LoadSchoolAnimalRegions()
        {
            ddSchoolAnimRegions.DataSource = AnimalRegionList.GetActive();
            ddSchoolAnimRegions.DataBind();
            ddSchoolAnimRegions.AddEmptyListItem("Select Region", "-1");
        }

        private void LoadSchoolExhibits()
        {
            ddSchoolExhibit.DataSource = ExhibitList.GetActiveByRegion(ddSchoolAnimRegions.SelectedValue);
            ddSchoolExhibit.DataBind();
            ddSchoolExhibit.AddEmptyListItem("Select Exhibit", "-1");
        }

        private void LoadSchools(int districtId)
        {
            ddSchool.Visible = true;
            ddSchool.DataSource = SchoolList.GetActiveSchoolsByDistrict(districtId);
            ddSchool.DataBind();
            ddSchool.AddEmptyListItem("Select School", "-1");
        }

        private void LoadReports()
        {
            if(this.ObservationID > 0)
            {
                ObservationReportList reports = ObservationReportList.GetActive(this.ObservationID);
                gvReports.DataSource = reports;
                gvReports.DataBind();

                lnkUpload.NavigateUrl = "~/admin/observation/reports.aspx?id={0}".FormatWith(this.ObservationID);
            }
            else{
                gvReports.Visible = false;
                divEmptyReports.Text = "Observations must be saved first before reports can be uploaded.";
                lnkUpload.Visible = false;
            }
            

        }

        protected void ddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddDistrict.SelectedIndex == 0)
            {
                ddSchool.Visible = false;
            }
            else
            {
                this.LoadSchools(ddDistrict.SelectedItem.Value.ToInt32());
            }
            
        }

        protected void ddlSchoolObserverCount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddSchoolAnimRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSchoolExhibits();
            schoolAnimalGroup.LoadGroup(ddSchoolAnimRegions.SelectedValue);
        }

        protected void ddSchoolExhibit_PreRender(object sender, EventArgs e)
        {

        }
        
        protected void btnSaveEdu_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                this.SaveEduObservation();
            }
            else
            {
                string valerrs = this.GetValidationErrorText();
                this.DisplayError("There are one or more data errors. Please correct and try again:<br/>" + valerrs);
            }
        }

        private void SaveEduObservation()
        {
            if (this.ObservationID == 0)
            {
                if (this.CreateNewEduObservation())
                {
                    var strRedirect = string.Format("view-observation.aspx?observationId={0}", ObservationID);
                    Response.Redirect(strRedirect, false);
                }

            }
            else
            {
                if (this.UpdateEduObservation())
                {
                    var strRedirect = string.Format("view-observation.aspx?observationId={0}", ObservationID);
                    Response.Redirect(strRedirect, false);
                }

            }
        }

        private bool CreateNewEduObservation()
        {
            Observation obs = new Observation();

            obs = new Observation();
            obs.ObserveType = Observation.ObservationTypeEnum.School;

            PasswordGenerator pg = new PasswordGenerator();
            pg.IncludeLower = true;
            pg.IncludeNumber = true;
            pg.IncludeSpecial = false;
            pg.IncludeUpper = false;
            pg.MinimumLength = 8;
            pg.MaximumLength = 8;

            obs.TeacherName = txtTeacherName.Text;
            obs.TeacherLogin = pg.Create();
            obs.TeacherPass = pg.Create();
            obs.StudentPass = pg.Create();

            DateTime start = Convert.ToDateTime(txtSchoolStart.Text);
            obs.ObserveStart = dteSchoolDate.DateValue.AddHours(start.Hour).AddMinutes(start.Minute);

            DateTime end = Convert.ToDateTime(txtSchoolEnd.Text);
            obs.ObserveEnd = dteSchoolDate.DateValue.AddHours(end.Hour).AddMinutes(end.Minute);

            if (!end.IsAfterDate(start))
            {
                this.Toast(PageExtensions.ToastMessageType.error, "End time must be after start time.", "Data Error");
                return false;
            }

            obs.Category = ddSchoolCat.SelectedItem.Text;
            if (ddSchoolCat.SelectedIndex == 1) //behaviour
            {

            }
            else //timed
            {
                obs.Interval = ddSchoolInterval.SelectedValue.ToInt32();
                obs.Timer = true;
                obs.Manual = false;
            }

            obs.DistrictID = ddDistrict.SelectedValue.ToInt32();
            obs.SchoolID = ddSchool.SelectedValue.ToInt32();
            obs.GradeID = ddGrade.SelectedValue.ToInt32();

            obs.ObserverNo = ddlSchoolObserverCount.SelectedValue.ToInt32();

            obs.ExhibitID = ddSchoolExhibit.SelectedValue.ToInt32();

            Observation newObs = ObservationList.AddItem(obs);
            
            AnimalGroup ag = new AnimalGroup();
            ag.Deleted = false;
            ag.GrpID = 1;
            ag.GrpName = "Group 1";
            ag.ObservationID = newObs.ObservationID;

            AnimalGroupList.AddItem(ag);

            foreach (int aid in schoolAnimalGroup.SelectedAnimalIDs)
            {
                AnimalObservation ao = new AnimalObservation();
                ao.ObservationID = newObs.ObservationID;
                ao.GroupID = 1;
                ao.AnimalID = aid;

                AnimalObservationList.AddItem(ao);
            }            

            CZAOSMail.Mail.ObservationMail mailer = new CZAOSMail.Mail.ObservationMail();
            mailer.SendNewObservationEmail(obs);

            this.Toast(PageExtensions.ToastMessageType.success, "Observation created successfully", "Record Created");

            return true;
        }

        private bool UpdateEduObservation()
        {
            Observation obs = ObservationList.Get(this.ObservationID);           

            obs.ObserveType = Observation.ObservationTypeEnum.School;

            DateTime start = Convert.ToDateTime(txtSchoolStart.Text);
            obs.ObserveStart = dteSchoolDate.DateValue.AddHours(start.Hour).AddMinutes(start.Minute);

            DateTime end = Convert.ToDateTime(txtSchoolEnd.Text);
            obs.ObserveEnd = dteSchoolDate.DateValue.AddHours(end.Hour).AddMinutes(end.Minute);

            if (!end.IsAfterDate(start))
            {
                this.Toast(PageExtensions.ToastMessageType.error, "End time must be after start time.", "Data Error");
                return false;
            }

            obs.Category = ddSchoolCat.SelectedItem.Text;
            if (ddSchoolCat.SelectedIndex == 1) //behaviour
            {

            }
            else //timed
            {
                obs.Interval = ddSchoolInterval.SelectedValue.ToInt32();
                obs.Timer = true;
                obs.Manual = false;
            }

            obs.ObserverNo = ddlSchoolObserverCount.SelectedValue.ToInt32();

            obs.ExhibitID = ddSchoolExhibit.SelectedValue.ToInt32();
            obs.TeacherName = txtTeacherName.Text;
            obs.DistrictID = ddDistrict.SelectedValue.ToInt32();
            obs.SchoolID = ddSchool.SelectedValue.ToInt32();
            obs.GradeID = ddGrade.SelectedValue.ToInt32();

            ObservationList.UpdateItem(obs);

            foreach (int aid in schoolAnimalGroup.SelectedAnimalIDs)
            {
                AnimalObservation ao = new AnimalObservation();
                ao.ObservationID = obs.ObservationID;
                ao.GroupID = 1;
                ao.AnimalID = aid;

                AnimalObservationList.AddItem(ao);
            }

            CZAOSMail.Mail.ObservationMail mailer = new CZAOSMail.Mail.ObservationMail();
            mailer.SendNewObservationEmail(obs);

            this.Toast(PageExtensions.ToastMessageType.success, "Observation updated successfully", "Record Updated");

            return true;

        }

        #endregion

        protected void gvReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("DeleteReport"))
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ObservationReport item = ObservationReportList.GetItem(id);
                ObservationReportList.DeleteItem(item);
                this.LoadReports();
                this.Toast("report deleted");
            }

            if (e.CommandName.Equals("UpdateText"))
            {
                LinkButton lb = e.CommandSource as LinkButton;
                EditableTextBox etb = lb.NamingContainer as EditableTextBox;

                if (etb.IsValid)
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    ObservationReport item = ObservationReportList.GetItem(id);

                    item.ReportName = etb.UpdatedText;
                    ObservationReportList.UpdateItem(item);
                    this.Toast("report updated");
                }
            }
        }

        protected void gvReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkFile = e.FindControl<HyperLink>("lnkFile");
                ObservationReport report = e.Row.DataItem as ObservationReport;

                if (report.ReportName.IsNullOrEmpty())
                {
                    lnkFile.Text = report.ReportLink;
                }
                else
                {
                    lnkFile.Text = report.ReportName;
                }

              
            }
        }

    }
}