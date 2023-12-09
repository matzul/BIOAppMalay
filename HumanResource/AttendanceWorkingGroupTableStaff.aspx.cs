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

public partial class HumanResource_AttendanceWorkingGroupTableStaff : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sDayDate = "";
    public String sSWGId = "";
    public String sAction = "";
    public String sViewBy = "";

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
        sViewBy = "DATE";

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
        sDayDate = DateTime.Now.ToString("dd-MM-yyyy");
        sViewBy = "DATE";

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (!sAction.Equals("RESET"))
        {
            if (Request.Params.Get("txtFindDayDate") != null)
            {
                sDayDate = oHRCon.replaceNull(Request.Params.Get("txtFindDayDate"));
            }
            if (Request.Params.Get("viewby") != null)
            {
                sViewBy = oHRCon.replaceNull(Request.Params.Get("viewby"));
            }
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
    public static String updateWGTableStaff(string currcomp, string currfyr, string staffno, Int64 id, string fromtime, string totime, string status)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            HRModel modWDDetails = oHRCon.getCompWorkingDayDetails(currcomp, currfyr, "", "", id);
            String day_name = modWDDetails.GetSetday_name;
            String day_date = modWDDetails.GetSetday_date;

            HRModel modItem = oHRCon.getStaffAttendanceDayDetails(currcomp, currfyr, staffno, day_date, 0);
            if (modItem.GetSetid > 0)
            {
                if(fromtime.Length > 0 || totime.Length > 0)
                {
                    if (!fromtime.Equals("00:00:00") || !totime.Equals("00:00:00"))
                    {
                        //update staffattendance
                        modItem.GetSetfromtime = fromtime;
                        modItem.GetSettotime = totime;
                        String result = oHRCon.updateStaffAttendanceDay(modItem);
                        if (result.Equals("Y"))
                        {
                            sStatus = "Y";
                            sMessage = "Kemaskini berjaya!";
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Kemaskini tidak berjaya! Error on updating table Staff Attendance Day...";
                        }
                    }
                    else
                    {
                        String result = oHRCon.deleteStaffAttendanceDay(modItem);
                        if (result.Equals("Y"))
                        {
                            sStatus = "Y";
                            sMessage = "Kemaskini berjaya!";
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Kemaskini tidak berjaya! Error on deleting table Staff Attendance Day...";
                        }
                    }
                }
                else
                {
                    String result = oHRCon.deleteStaffAttendanceDay(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on deleting table Staff Attendance Day...";
                    }
                }
            }
            else
            {
                if (fromtime.Length > 0 || totime.Length > 0)
                {
                    if (!fromtime.Equals("00:00:00") || !totime.Equals("00:00:00"))
                    {
                        modItem = new HRModel();
                        modItem.GetSetcomp = currcomp;
                        modItem.GetSetfyr = currfyr;
                        modItem.GetSetstaffno = staffno;
                        modItem.GetSetday_name = day_name;
                        modItem.GetSetday_date = day_date;
                        modItem.GetSetfromtime = fromtime;
                        modItem.GetSettotime = totime;
                        modItem.GetSetlocation = "HQ";
                        modItem.GetSetstatus = status;
                        modItem.GetSetcreatedby = sUserId;
                        String result = oHRCon.insertStaffAttendanceDay(modItem);
                        if (result.Equals("Y"))
                        {
                            sStatus = "Y";
                            sMessage = "Tambah berjaya!";
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Tambah tidak berjaya! Error on inserting table Staff Attendance Day...";
                        }
                    }
                    else
                    {
                        sStatus = "Y";
                        sMessage = "Tiada Perubahan!";
                    }
                }
                else
                {
                    sStatus = "Y";
                    sMessage = "Tiada Perubahan!";
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}