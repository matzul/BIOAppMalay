using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HumanResource_AttendanceWorkingStaffDay : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sDayDate = "";
    public String sSWGId = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsWorkingGroupStaffDayAll = new ArrayList();
    public ArrayList lsWorkingGroupStaffDaySearch = new ArrayList();
    public ArrayList lsPublicHolidayString = new ArrayList();

    public HRModel modWorkingGroupStaff = new HRModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }

    }
    private void initialValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oHRCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oHRCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oHRCon.replaceNull(Session["userid"].ToString());
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oHRCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["id"] != null)
        {
            sSWGId = oHRCon.replaceNull(Request.QueryString["id"]);
        }
        sDayDate = DateTime.Now.ToString("dd-MM-yyyy");

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsPublicHolidayString = oHRCon.getCompPublicHolidayStringList(sCurrComp, sCurrFyr, "", "", "");
        modWorkingGroupStaff = oHRCon.getStaffAttendanceGroupDetails(sCurrComp, sCurrFyr, "", 0, int.Parse(sSWGId.Length > 0 ? sSWGId : "0"));
        lsWorkingGroupStaffDayAll = oHRCon.getStaffAttendanceGroupDayList(modWorkingGroupStaff.GetSetcomp, modWorkingGroupStaff.GetSetfyr, modWorkingGroupStaff.GetSetstaffno, 0, 0, "", "");
        if (sDayDate.Length > 0)
        {
            lsWorkingGroupStaffDaySearch = oHRCon.getStaffAttendanceGroupDayList(modWorkingGroupStaff.GetSetcomp, modWorkingGroupStaff.GetSetfyr, modWorkingGroupStaff.GetSetstaffno, 0, 0, "", sDayDate);
        }
        else
        {
            lsWorkingGroupStaffDaySearch = oHRCon.getStaffAttendanceGroupDayList(modWorkingGroupStaff.GetSetcomp, modWorkingGroupStaff.GetSetfyr, modWorkingGroupStaff.GetSetstaffno, 0, 0, "", "");
        }
        this.lsPageList.Items.Add(new ListItem("1", "1"));
        this.lsPageList.SelectedValue = sCurrentPage;

    }
    private void getValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oHRCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oHRCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oHRCon.replaceNull(Session["userid"].ToString());
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oHRCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["id"] != null)
        {
            sSWGId = oHRCon.replaceNull(Request.QueryString["id"]);
        }
        if (Request.Params.Get("txtFindDayDate") != null)
        {
            sDayDate = oHRCon.replaceNull(Request.Params.Get("txtFindDayDate"));
        }
        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    protected void lsPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
        }
    }

}