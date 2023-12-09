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

public partial class HumanResource_LeaveCategoryStaff : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sStaffName = "";
    public String sLeaveCat = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsGredComp = new ArrayList();
    public ArrayList lsStaffLeaveGroup = new ArrayList();

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
        lsGredComp = oHRCon.getCompGredList(sCurrComp, "", "");
        lsStaffLeaveGroup = oHRCon.getStaffLeaveGroupList(sCurrComp, sCurrFyr, sStaffNo, 0, "", sLeaveCat, "", "");
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
        if (Request.Params.Get("lsFindLeaveCat") != null)
        {
            sLeaveCat = oHRCon.replaceNull(Request.Params.Get("lsFindLeaveCat"));
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
    public static String getStaffEmployDetails(string currcomp, string staffno)
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
            oModStaffEmploy = oHRCon.getStaffEmployDetails(currcomp, staffno);
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
    public static String getLGItemList(string currcomp, string currfyr, string leave_cat, string staffno)
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
            ArrayList lsFisItem = oHRCon.getCompLeaveGroupList(currcomp, currfyr, "", "", leave_cat, "", staffno);
            for (int i = 0; i < lsFisItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsFisItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetbrought = oHRMod.GetSetbrought,
                    GetSetcount = oHRMod.GetSetcount,
                    GetSettaken = oHRMod.GetSettaken
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
    public static String getLGStaffUpdate(string currcomp, Int64 id)
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
            oHRMod = oHRCon.getStaffLeaveGroupDetails(currcomp, "", "", 0, id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertLGStaffUpdate(string currcomp, string currfyr, string staffno, string fromdate, string todate, string status, LeaveInputArray[] inputarray)
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
            for(int i=0; i< inputarray.Length; i++)
            {
                LeaveInputArray modInput = (LeaveInputArray)inputarray[i];
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetfyr = currfyr;
                modItem.GetSetstaffno = staffno;
                modItem.GetSetlg_id = Int64.Parse(modInput.GetSetlg_id.Length > 0 ? modInput.GetSetlg_id : "0");
                modItem.GetSetcode = modInput.GetSetcode;
                modItem.GetSetfromdate = fromdate;
                modItem.GetSettodate = todate;
                modItem.GetSetcount = int.Parse(modInput.GetSetcount.Length > 0 ? modInput.GetSetcount : "0");
                modItem.GetSetbrought = int.Parse(modInput.GetSetbrought.Length > 0 ? modInput.GetSetbrought : "0");
                modItem.GetSettaken = int.Parse(modInput.GetSettaken.Length > 0 ? modInput.GetSettaken : "0");
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                if (modItem.GetSetlg_id > 0)
                {
                    String result = oHRCon.insertStaffLeaveGroup(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = result;
                        sMessage = "Tambah berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Tambah tidak berjaya! Error on inserting table Staff Leave Group...";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Tahun: " + modItem.GetSetfyr + " & Leave Id: " + modItem.GetSetlg_id;
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateLGStaffUpdate(string currcomp, string currfyr, string staffno, string fromdate, string todate, int brought, int count, int taken, string status, Int64 id)
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
            HRModel modItem = oHRCon.getStaffLeaveGroupDetails(currcomp, currfyr, "", 0, id);
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = true;
                if (proceeUpdate)
                {
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetfyr = currfyr;
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetfyr = currfyr;
                    modItem.GetSetstaffno = staffno;
                    modItem.GetSetfromdate = fromdate;
                    modItem.GetSettodate = todate;
                    modItem.GetSetbrought = brought;
                    modItem.GetSetcount = count;
                    modItem.GetSettaken = taken;
                    modItem.GetSetstatus = status;
                    modItem.GetSetmodifiedby = sUserId;
                    String result = oHRCon.updateStaffLeaveGroup(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table Staff Leave Group...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Tahun: " + currfyr + " & Nama Pekerja: " + staffno;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteLGStaffUpdate(string currcomp, Int64 id)
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
            HRModel modItem = oHRCon.getStaffLeaveGroupDetails(currcomp, "", "", 0, id);
            if (modItem.GetSetid > 0)
            {
                String result = oHRCon.deleteStaffLeaveGroup(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Staff Leave Group...";
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

public class LeaveInputArray
{
    private string code;
    public string GetSetcode
    {
        get
        {
            return code;
        }
        set
        {
            code = value;
        }
    }

    private string lg_id;
    public string GetSetlg_id
    {
        get
        {
            return lg_id;
        }
        set
        {
            lg_id = value;
        }
    }

    private string brought;
    public string GetSetbrought
    {
        get
        {
            return brought;
        }
        set
        {
            brought = value;
        }
    }

    private string count;
    public string GetSetcount
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }

    private string taken;
    public string GetSettaken
    {
        get
        {
            return taken;
        }
        set
        {
            taken = value;
        }
    }
}