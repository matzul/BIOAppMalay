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

public partial class CompStaffFamily : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sTabName = "";

    public String sAction = "";
    public String sActionString = "";

    public String sStatus = "";
    public String sAlertMessage = "";

    public HRModel oModStaff = new HRModel();
    public ArrayList lsStaffFamily = new ArrayList();

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
    }

    private void processValues()
    {
        if (sAction.Equals("OPEN") || sAction.Equals("EDIT"))
        {
            oModStaff = oHRCon.getStaffDetails(sCurrComp,sStaffNo);
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

}