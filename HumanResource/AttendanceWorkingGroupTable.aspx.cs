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

public partial class HumanResource_AttendanceWorkingGroupTable : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sGrpId = "";
    public String sStaffNo = "";
    public String sStaffName = "";
    public String sAction = "";
    public String sDateFrom = "";
    public String sDateTo = "";
    public String sViewBy = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsWorkingGroup = new ArrayList();

    public ArrayList lsWorkingGroupTableAll = new ArrayList();
    public ArrayList lsWorkingGroupTableSearch = new ArrayList();
    public ArrayList lsPublicHolidayString = new ArrayList();

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
        sViewBy = "GROUP";
        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsWorkingGroup = oHRCon.getCompWorkingGroupList(sCurrComp, sCurrFyr, "", "", "", "");
        //lsPublicHolidayString = oHRCon.getCompPublicHolidayStringList(sCurrComp, sCurrFyr, "", "", "");
        lsWorkingGroupTableAll = oHRCon.getAttendanceWorkingGroupTableList(sCurrComp, sCurrFyr, 0, "", "", "");
        lsWorkingGroupTableSearch = oHRCon.getAttendanceWorkingGroupTableList(sCurrComp, sCurrFyr, int.Parse(sGrpId.Length > 0 ? sGrpId : "0"), sStaffNo, sDateFrom, sDateTo);

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
        sViewBy = "GROUP";

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (!sAction.Equals("RESET"))
        {
            if (Request.Params.Get("lsFindFyr") != null)
            {
                sCurrFyr = oHRCon.replaceNull(Request.Params.Get("lsFindFyr"));
            }
            if (Request.Params.Get("viewby") != null)
            {
                sViewBy = oHRCon.replaceNull(Request.Params.Get("viewby"));
            }
        }

        if (Request.Params.Get("lsFindGrpId") != null)
        {
            sGrpId = oHRCon.replaceNull(Request.Params.Get("lsFindGrpId"));
        }
        if (Request.Params.Get("txtFindStaffNo") != null)
        {
            sStaffNo = oHRCon.replaceNull(Request.Params.Get("txtFindStaffNo"));
        }
        if (Request.Params.Get("txtFindStaffName") != null)
        {
            sStaffName = oHRCon.replaceNull(Request.Params.Get("txtFindStaffName"));
        }
        if (Request.Params.Get("txtFindDateFrom") != null)
        {
            sDateFrom = oHRCon.replaceNull(Request.Params.Get("txtFindDateFrom"));
        }
        if (Request.Params.Get("txtFindDateTo") != null)
        {
            sDateTo = oHRCon.replaceNull(Request.Params.Get("txtFindDateTo"));
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

    [WebMethod(EnableSession = true)]
    public static String getStaffEmployList(string currcomp, string currfyr)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsItemOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsItem = oHRCon.getStaffEmployList(currcomp, "", "", "", "", "");
            for (int i = 0; i < lsItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsItem[i];

                Object objData = new
                {
                    GetSetstaffno = oHRMod.GetSetstaffno,
                    GetSetname = oHRMod.GetSetname,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSetdept_id = oHRMod.GetSetdept_id,
                    GetSetdept_name = oHRMod.GetSetdept_name
                };
                lsItemOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemlist = lsItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}