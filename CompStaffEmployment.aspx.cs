using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompStaffEmployment : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sEmploymentId = "";

    public String sAction = "";
    public String sActionString = "";

    public String sStatus = "";
    public String sAlertMessage = "";

    public HRModel oModStaff = new HRModel();
    public ArrayList lsStaffEmployment = new ArrayList();

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
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["staffno"] != null)
        {
            sStaffNo = Request.QueryString["staffno"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

    }

    private void getValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidStaffNo") != null)
        {
            sStaffNo = oHRCon.replaceNull(Request.Params.Get("hidStaffNo"));
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidEmploymentId") != null)
        {
            sEmploymentId = oHRCon.replaceNull(Request.Params.Get("hidEmploymentId"));
        }
        
        if (sAction.Equals("SAVE"))
        {
            oModStaff = oHRCon.getStaffDetails(sCurrComp, sStaffNo);
            oModStaff.GetSetname = oHRCon.replaceNull(Request.Params.Get("staffname"));
            oModStaff.GetSetnicno = oHRCon.replaceNull(Request.Params.Get("staffnicno"));
            oModStaff.GetSetpassport = oHRCon.replaceNull(Request.Params.Get("staffpassport"));
            oModStaff.GetSetstatus = oHRCon.replaceNull(Request.Params.Get("staffstatus"));
            oModStaff.GetSetmobile1 = oHRCon.replaceNull(Request.Params.Get("staffmobile1"));
            oModStaff.GetSetemail1 = oHRCon.replaceNull(Request.Params.Get("staffemail1"));
            oModStaff.GetSetmodifiedby = sUserId;
        }
        else if (sAction.Equals("DELETE"))
        {
            oModStaff = new HRModel();
            oModStaff.GetSetcomp = sCurrComp;
            oModStaff.GetSetstaffno = sStaffNo;
            oModStaff.GetSetid = Int64.Parse(sEmploymentId.Length>0 ? sEmploymentId : "0");
        }
    }

    private void processValues()
    {
        if (sAction.Equals("OPEN") || sAction.Equals("EDIT"))
        {
            oModStaff = oHRCon.getStaffDetails(sCurrComp, sStaffNo);
            lsStaffEmployment = oHRCon.getStaffEmployList(sCurrComp, sStaffNo, "", "", "", "");
        }
        else if (sAction.Equals("SAVE"))
        {
            if (oModStaff.GetSetstaffno.Length > 0)
            {
                sAction = "OPEN";

                sStatus = oHRCon.updateStaffInfo(oModStaff);

                if (sStatus.Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Kemaskini berjaya disimpan...";
                    sAction = "OPEN";
                    sActionString = "KEMASKINI MAKLUMAT KAKITANGAN";
                }
                else
                {
                    sAlertMessage = "ERROR|Kemaskini tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI MAKLUMAT KAKITANGAN";
                }

            }
            else
            {
                sStatus = "N";
                sAlertMessage = "ERROR|Kemaskini tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI MAKLUMAT KAKITANGAN";
            }
            oModStaff = oHRCon.getStaffDetails(sCurrComp, sStaffNo);
            lsStaffEmployment = oHRCon.getStaffEmployList(sCurrComp, sStaffNo, "", "", "", "");
        }
        else if (sAction.Equals("DELETE"))
        {
            if(oModStaff.GetSetstaffno.Length > 0 && oModStaff.GetSetid > 0)
            {
                sStatus = oHRCon.deleteStaffEmploy(oModStaff);
                sAlertMessage = "SUCCESS|Rekod berjaya dihapuskan...";
            }
            else
            {
                sStatus = "N";
                sAlertMessage = "ERROR|Rekod tidak berjaya dihapuskan...";
            }
            //delete employment row process
            oModStaff = oHRCon.getStaffDetails(sCurrComp, sStaffNo);
            lsStaffEmployment = oHRCon.getStaffEmployList(sCurrComp, sStaffNo, "", "", "", "");
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
    public static String getSaluteList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsSalute = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsObject = oHRCon.getSaluteList(currcomp, "", "");
            for (int i = 0; i < lsObject.Count; i++)
            {
                HRModel oMod = (HRModel)lsObject[i];

                Object objData = new
                {
                    GetSetid = oMod.GetSetcode,
                    GetSetdesc = oMod.GetSetdesc
                };
                lsSalute.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, salutelist = lsSalute };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getRaceList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsRace = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsObject = oHRCon.getRaceList(currcomp, "", "");
            for (int i = 0; i < lsObject.Count; i++)
            {
                HRModel oMod = (HRModel)lsObject[i];

                Object objData = new
                {
                    GetSetid = oMod.GetSetcode,
                    GetSetdesc = oMod.GetSetdesc
                };
                lsRace.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, racelist = lsRace };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getReligionList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsReligion = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsObject = oHRCon.getReligionList(currcomp, "", "");
            for (int i = 0; i < lsObject.Count; i++)
            {
                HRModel oMod = (HRModel)lsObject[i];

                Object objData = new
                {
                    GetSetid = oMod.GetSetcode,
                    GetSetdesc = oMod.GetSetdesc
                };
                lsReligion.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, religionlist = lsReligion };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCountryList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsCountry = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsObject = oHRCon.getCountryList(currcomp, "", "");
            for (int i = 0; i < lsObject.Count; i++)
            {
                HRModel oMod = (HRModel)lsObject[i];

                Object objData = new
                {
                    GetSetid = oMod.GetSetcode,
                    GetSetdesc = oMod.GetSetdesc
                };
                lsCountry.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, countrylist = lsCountry };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStateList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsState = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsObject = oHRCon.getStateList(currcomp, "", "");
            for (int i = 0; i < lsObject.Count; i++)
            {
                HRModel oMod = (HRModel)lsObject[i];

                Object objData = new
                {
                    GetSetid = oMod.GetSetcode,
                    GetSetdesc = oMod.GetSetdesc
                };
                lsState.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, statelist = lsState };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCompDeptList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsDeptComp = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsDeptComp = oHRCon.getCompDeptList(currcomp, "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, deptlist = lsDeptComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCompDeptReportTo(string currcomp, int deptlevel)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsDeptComp = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsDeptComp = oHRCon.getCompDeptReportTo(currcomp, "", "", deptlevel);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, deptlist = lsDeptComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStaffGredReportTo(string currcomp, string staffno, string gredid)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsStaffComp = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsStaffComp = oHRCon.getStaffListReportTo(currcomp, staffno, gredid);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, stafflist = lsStaffComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCompGredList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsGredComp = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsGredComp = oHRCon.getCompGredList(currcomp, "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, gredlist = lsGredComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCompPosList(string currcomp)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsPosComp = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsPosComp = oHRCon.getCompPosList(currcomp, "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, poslist = lsPosComp };

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
            if(oModStaffEmploy.GetSetid > 0)
            {
                sStatus = "Y";
            }
        }

        object retData = new { result = sStatus, staffemploy = oModStaffEmploy };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateStaffEmploy(string currcomp, string staffno, int id, string deptid, string gredid, string posid, string type, string cat, int probation, string fromdate,
        string todate, string reportto, string status, string remarks)
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

        if (currcomp.Length > 0 && staffno.Length > 0)
        {
            HRModel modItem = oHRCon.getStaffEmployDetails(currcomp, staffno, id);

            if (modItem.GetSetid > 0)
            {
                modItem.GetSetdept_id = deptid;
                modItem.GetSetgred_id = gredid;
                modItem.GetSetpos_id = posid;
                modItem.GetSettype = type;
                modItem.GetSetcat = cat;
                modItem.GetSetprobation = probation;
                modItem.GetSetfromdate = fromdate;
                modItem.GetSettodate = todate;
                modItem.GetSetreportto = reportto;
                modItem.GetSetstatus = status;
                modItem.GetSetremarks = remarks;
                modItem.GetSetmodifiedby = sUserId;
                String result = oHRCon.updateStaffEmploy(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on updating table Staff Employment...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & StaffNo: " + staffno + " & Id: " + id;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertStaffEmploy(string currcomp, string staffno, string deptid, string gredid, string posid, string type, string cat, int probation, string fromdate,
        string todate, string reportto, string status, string remarks)
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

        if (currcomp.Length > 0 && staffno.Length > 0)
        {
            ArrayList lsModItem = oHRCon.getStaffEmployList(currcomp, staffno, "ACTIVE");
            if(lsModItem.Count > 0)
            {
                for (int i = 0; i < lsModItem.Count; i++)
                {
                    HRModel modStaff = (HRModel)lsModItem[i];
                    modStaff.GetSettodate = fromdate;
                    modStaff.GetSetstatus = "IN-ACTIVE";
                    modStaff.GetSetmodifiedby = sUserId;
                    string sResultUpd = oHRCon.updateStaffEmploy(modStaff);
                }
            }
            HRModel modItem = new HRModel();
            modItem.GetSetcomp = currcomp;
            modItem.GetSetstaffno = staffno;
            modItem.GetSetdept_id = deptid;
            modItem.GetSetgred_id = gredid;
            modItem.GetSetpos_id = posid;
            modItem.GetSettype = type;
            modItem.GetSetcat = cat;
            modItem.GetSetprobation = probation;
            modItem.GetSetfromdate = fromdate;
            modItem.GetSettodate = todate;
            modItem.GetSetreportto = reportto;
            modItem.GetSetstatus = status;
            modItem.GetSetremarks = remarks;
            modItem.GetSetcreatedby = sUserId;
            String result = oHRCon.insertStaffEmploy(modItem);
            if (result.Equals("Y"))
            {
                sStatus = "Y";
                sMessage = "Tambah berjaya!";
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Error on inserting table Staff Employment...";
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}