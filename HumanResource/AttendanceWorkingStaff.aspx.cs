using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HumanResource_AttendanceWorkingStaff : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sStaffName = "";
    public String sGrpId = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsWorkingGroup = new ArrayList();
    public ArrayList lsWorkingGroupStaff = new ArrayList();

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

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsWorkingGroup = oHRCon.getCompWorkingGroupList(sCurrComp, sCurrFyr, "", "", "", "");
        lsWorkingGroupStaff = oHRCon.getStaffAttendanceGroupList(sCurrComp, sCurrFyr, sStaffNo, int.Parse(sGrpId.Length>0?sGrpId:"0"), "", "");
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
        }

        if (Request.Params.Get("txtFindStaffNo") != null)
        {
            sStaffNo = oHRCon.replaceNull(Request.Params.Get("txtFindStaffNo"));
        }
        if (Request.Params.Get("txtFindStaffName") != null)
        {
            sStaffName = oHRCon.replaceNull(Request.Params.Get("txtFindStaffName"));
        }
        if (Request.Params.Get("lsFindGrpId") != null)
        {
            sGrpId = oHRCon.replaceNull(Request.Params.Get("lsFindGrpId"));
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

    [WebMethod(EnableSession = true)]
    public static String getStaffEmployDetails(string currcomp, string staffno, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oModStaffEmploy = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            oModStaffEmploy = oHRCon.getStaffEmployDetails(currcomp, staffno, id);
            if (oModStaffEmploy.GetSetid > 0)
            {
                sStatus = "Y";
            }
        }

        object retData = new { result = sStatus, staffemploy = oModStaffEmploy };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getWGItemDetail(string currcomp, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oHRMod = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oHRMod = oHRCon.getCompWorkingGroupDetails(currcomp, "", "", id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getWGStaff(string currcomp, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oHRMod = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oHRMod = oHRCon.getStaffAttendanceGroupDetails(currcomp, "", "", 0, id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertWGStaff(string currcomp, string currfyr, string staffno, int grpid, string grpcode, string status)
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

        if (currcomp.Length > 0 && currfyr.Length > 0)
        {
            HRModel modItem = oHRCon.getStaffAttendanceGroupDetails(currcomp, currfyr, staffno, grpid, 0);
            if (modItem.GetSetid == 0)
            {
                modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetfyr = currfyr;
                modItem.GetSetstaffno = staffno;
                modItem.GetSetwg_id = grpid;
                modItem.GetSetcode = grpcode;
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertStaffAttendanceGroup(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = result;
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table Staff Attendance Group...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Tahun: " + modItem.GetSetfyr + " & Nama Pekerja: " + modItem.GetSetstaffno + " & Kumpulan Kerja: " + modItem.GetSetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateWGStaff(string currcomp, string currfyr, string staffno, int grpid, string grpcode, string status, int id)
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
            HRModel modItem = oHRCon.getStaffAttendanceGroupDetails(currcomp, currfyr, "", 0, id);
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                if (!modItem.GetSetwg_id.Equals(grpid))
                {
                    ArrayList lsItemList = oHRCon.getStaffAttendanceGroupList(currcomp, currfyr, staffno, grpid, "", "");
                    if (lsItemList.Count > 0)
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist for for Comp: " + currcomp + " & Tahun: " + currfyr + " & Nama Pekerja: " + staffno + " & Group Id: " + grpid + ". Please remove record first...";
                    }
                    else
                    {
                        proceeUpdate = true;
                    }
                }
                else
                {
                    proceeUpdate = true;
                }
                if (proceeUpdate)
                {
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetfyr = currfyr;
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetfyr = currfyr;
                    modItem.GetSetstaffno = staffno;
                    modItem.GetSetwg_id = grpid;
                    modItem.GetSetcode = grpcode;
                    modItem.GetSetstatus = status;
                    modItem.GetSetmodifiedby = sUserId;
                    String result = oHRCon.updateStaffAttendanceGroup(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table Staff Attendance Group...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Tahun: " + currfyr + " & Nama Pekerja: " + staffno + " & Group Code: " + grpcode;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteWGStaff(string currcomp, int id)
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
            HRModel modItem = oHRCon.getStaffAttendanceGroupDetails(currcomp, "", "", 0, id);
            if (modItem.GetSetid > 0)
            {
                String result = oHRCon.deleteStaffAttendanceGroup(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Staff Attendance Group...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Hapus tidak berjaya! Record not found for Comp: " + currcomp + " & Id: " + id;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}