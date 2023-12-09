using System;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class CompStaffListing : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sStaffNo = "";
    public String sStaffName = "";
    public String sStaffDept = "";    
    public ArrayList lsStaff = new ArrayList();
    public ArrayList lsSalute = new ArrayList();
    public ArrayList lsDept = new ArrayList();
    public ArrayList lsMarital = new ArrayList();
    public ArrayList lsRace = new ArrayList();
    public ArrayList lsReligion = new ArrayList();
    public ArrayList lsNationality = new ArrayList();

    private HRModel oModStaff = new HRModel();

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
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
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
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (sAction.Equals("SEARCH"))
        {
            sStaffNo = oHRCon.replaceNull(Request.Params.Get("staffno"));
            sStaffName = oHRCon.replaceNull(Request.Params.Get("staffname"));
        }
        if (sAction.Equals("RESET"))
        {
            sStaffNo = "";
            sStaffName = "";
        }
        if (sAction.Equals("CREATE"))
        {
            oModStaff = new HRModel();
            oModStaff.GetSetcomp = sCurrComp;
            oModStaff.GetSetstaffno = oHRCon.replaceNull(Request.Params.Get("addstaffno"));
            oModStaff.GetSetsalute = oHRCon.replaceNull(Request.Params.Get("addstaffsalute"));
            oModStaff.GetSetname = oHRCon.replaceNull(Request.Params.Get("addstaffname"));
            oModStaff.GetSetnickname = oHRCon.replaceNull(Request.Params.Get("addstaffnickname"));
            oModStaff.GetSetnicno = oHRCon.replaceNull(Request.Params.Get("addstaffnicno"));
            oModStaff.GetSetpassport = oHRCon.replaceNull(Request.Params.Get("addstaffpassport"));
            oModStaff.GetSetdob = oHRCon.replaceNull(Request.Params.Get("addstaffdob"));
            oModStaff.GetSetbirthplace = oHRCon.replaceNull(Request.Params.Get("addstaffbirthplace"));
            oModStaff.GetSetnationality = oHRCon.replaceNull(Request.Params.Get("addstaffnationality"));
            oModStaff.GetSetgender = oHRCon.replaceNull(Request.Params.Get("addstaffgender"));
            oModStaff.GetSetrace = oHRCon.replaceNull(Request.Params.Get("addstaffrace"));
            oModStaff.GetSetreligion = oHRCon.replaceNull(Request.Params.Get("addstaffreligion"));
            oModStaff.GetSetmarital = oHRCon.replaceNull(Request.Params.Get("addstaffmarital"));
            oModStaff.GetSetuserid = oHRCon.replaceNull(Request.Params.Get("addstaffuserid"));
            oModStaff.GetSetpassword = oHRCon.replaceNull(Request.Params.Get("addstaffpassword"));
            oModStaff.GetSetstatus = oHRCon.replaceNull(Request.Params.Get("addstaffstatus"));

            oModStaff.GetSetdept_id = oHRCon.replaceNull(Request.Params.Get("addstaffdeptid"));
            oModStaff.GetSetgred_id = oHRCon.replaceNull(Request.Params.Get("addstaffgredid"));
            oModStaff.GetSetpos_id = oHRCon.replaceNull(Request.Params.Get("addstaffposid"));
            oModStaff.GetSettype = oHRCon.replaceNull(Request.Params.Get("addstafftype"));
            oModStaff.GetSetcat = oHRCon.replaceNull(Request.Params.Get("addstaffcat"));
            oModStaff.GetSetfromdate = oHRCon.replaceNull(Request.Params.Get("addstafffromdate"));

            oModStaff.GetSetpaddress1 = oHRCon.replaceNull(Request.Params.Get("addstaffpaddress1"));
            oModStaff.GetSetpaddress2 = oHRCon.replaceNull(Request.Params.Get("addstaffpaddress2"));
            oModStaff.GetSetpaddress3 = oHRCon.replaceNull(Request.Params.Get("addstaffpaddress3"));
            oModStaff.GetSetpaddress4 = oHRCon.replaceNull(Request.Params.Get("addstaffpaddress4"));
            oModStaff.GetSetppostcode = oHRCon.replaceNull(Request.Params.Get("addstaffppostcode"));
            oModStaff.GetSetpcity = oHRCon.replaceNull(Request.Params.Get("addstaffpcity"));
            oModStaff.GetSetpstate = oHRCon.replaceNull(Request.Params.Get("addstaffpstate"));
            oModStaff.GetSetpcountry = oHRCon.replaceNull(Request.Params.Get("addstaffpcountry"));
            oModStaff.GetSetptelephone = oHRCon.replaceNull(Request.Params.Get("addstaffptelephone"));
            oModStaff.GetSetmobile1 = oHRCon.replaceNull(Request.Params.Get("addstaffmobile1"));
            oModStaff.GetSetremarks = oHRCon.replaceNull(Request.Params.Get("addstaffremarks"));
            oModStaff.GetSetcreatedby = sUserId;
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            if (sAction.Equals("CREATE"))
            {
                String result = oHRCon.insertStaffInfo(oModStaff);
                if (result.Equals("Y"))
                {
                    result = oHRCon.insertStaffEmploy(oModStaff);
                }
            }

            lsStaff = oHRCon.getStaffList(sCurrComp, sStaffNo, sStaffName, sStaffDept);
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

    #region ****BEGIN FOR DEPT COMP****
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
    public static String insertDeptComp(string currcomp, string deptid, string deptname, int deptlevel, string deptreportto, string status)
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

        if (currcomp.Length > 0 && deptid.Length > 0)
        {
            if(oHRCon.getCompDeptList(currcomp, deptid, "").Count > 0){
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record already exist for Comp: " + currcomp + " & Dept Code: " + deptid;
            }
            else {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = deptid; 
                modItem.GetSetname = deptname;
                modItem.GetSetlevel = deptlevel;
                modItem.GetSetreportto = deptreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertCompDept(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on inserting table Dept Comp...";
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateDeptComp(string currcomp, string deptid, string deptname, int deptlevel, string deptreportto, string status)
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

        if (currcomp.Length > 0 && deptid.Length > 0)
        {
            HRModel modItem = oHRCon.getCompDeptDetails(currcomp, deptid, "");

            if (modItem.GetSetid > 0)
            {
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = deptid;
                modItem.GetSetname = deptname;
                modItem.GetSetlevel = deptlevel;
                modItem.GetSetreportto = deptreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetmodifiedby = sUserId;
                String result = oHRCon.updateCompDept(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on updating table Dept Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & Dept Code: " + deptid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteDeptComp(string currcomp, string deptid)
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

        if (currcomp.Length > 0 && deptid.Length > 0)
        {
            if (oHRCon.getCompDeptList(currcomp, deptid, "").Count > 0)
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = deptid;
                String result = oHRCon.deleteCompDept(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on deleting table Dept Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & Dept Code: " + deptid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }
    #endregion ****END FOR DEPT COMP****
    
    #region ****BEGIN FOR GRED COMP****
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
    public static String getCompGredReportTo(string currcomp, int gredlevel)
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
            lsGredComp = oHRCon.getCompGredReportTo(currcomp, "", "", gredlevel);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, gredlist = lsGredComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertGredComp(string currcomp, string gredid, string gredname, int gredlevel, string gredreportto, string status)
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

        if (currcomp.Length > 0 && gredid.Length > 0)
        {
            if (oHRCon.getCompGredList(currcomp, gredid, "").Count > 0)
            {
                sStatus = "N";
                sMessage = "Penambahan tidak berjaya! Record already exist for Comp: " + currcomp + " & Gred Code: " + gredid;
            }
            else
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = gredid;
                modItem.GetSetname = gredname;
                modItem.GetSetlevel = gredlevel;
                modItem.GetSetreportto = gredreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertCompGred(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Penambahan berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Penambahan tidak berjaya! Error on inserting table Gred Comp...";
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateGredComp(string currcomp, string gredid, string gredname, int gredlevel, string gredreportto, string status)
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

        if (currcomp.Length > 0 && gredid.Length > 0)
        {
            HRModel modItem = oHRCon.getCompGredDetails(currcomp, gredid, "");

            if (modItem.GetSetid > 0)
            {
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = gredid;
                modItem.GetSetname = gredname;
                modItem.GetSetlevel = gredlevel;
                modItem.GetSetreportto = gredreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetmodifiedby = sUserId;
                String result = oHRCon.updateCompGred(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on updating table Gred Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & Gred Code: " + gredid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteGredComp(string currcomp, string gredid)
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

        if (currcomp.Length > 0 && gredid.Length > 0)
        {
            if (oHRCon.getCompGredList(currcomp, gredid, "").Count > 0)
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = gredid;
                String result = oHRCon.deleteCompGred(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Dept Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Hapus tidak berjaya! Record not found for Comp: " + currcomp + " & Gred Code: " + gredid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }
    #endregion ****END FOR GRED COMP****

    #region ****BEGIN FOR POS COMP****
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
    public static String getCompPosReportTo(string currcomp, int poslevel)
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
            lsPosComp = oHRCon.getCompPosReportTo(currcomp, "", "", poslevel);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, poslist = lsPosComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertPosComp(string currcomp, string posid, string posname, int poslevel, string posreportto, string status)
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

        if (currcomp.Length > 0 && posid.Length > 0)
        {
            if (oHRCon.getCompPosList(currcomp, posid, "").Count > 0)
            {
                sStatus = "N";
                sMessage = "Penambahan tidak berjaya! Record already exist for Comp: " + currcomp + " & Pos Code: " + posid;
            }
            else
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = posid;
                modItem.GetSetname = posname;
                modItem.GetSetlevel = poslevel;
                modItem.GetSetreportto = posreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertCompPos(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Penambahan berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Penambahan tidak berjaya! Error on inserting table Pos Comp...";
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updatePosComp(string currcomp, string posid, string posname, int poslevel, string posreportto, string status)
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

        if (currcomp.Length > 0 && posid.Length > 0)
        {
            HRModel modItem = oHRCon.getCompPosDetails(currcomp, posid, "");

            if (modItem.GetSetid > 0)
            {
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = posid;
                modItem.GetSetname = posname;
                modItem.GetSetlevel = poslevel;
                modItem.GetSetreportto = posreportto;
                modItem.GetSetstatus = status;
                modItem.GetSetmodifiedby = sUserId;
                String result = oHRCon.updateCompPos(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on updating table Pos Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & Pos Code: " + posid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deletePosComp(string currcomp, string posid)
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

        if (currcomp.Length > 0 && posid.Length > 0)
        {
            if (oHRCon.getCompPosList(currcomp, posid, "").Count > 0)
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetsid = posid;
                String result = oHRCon.deleteCompPos(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini tidak berjaya! Error on deleting table Pos Comp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Record not found for Comp: " + currcomp + " & Pos Code: " + posid;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }
    #endregion ****END FOR POS COMP****

}