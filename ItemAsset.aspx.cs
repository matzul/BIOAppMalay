using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemAsset : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";

    public String sPeopleId = "";
    public String sNoKP = "";
    public String sName = "";
    public String sTelNo = "";
    public String sStitchNo = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public ArrayList lsPeople = new ArrayList();
    public ArrayList lsStitchDetails = new ArrayList();

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
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("fname") != null)
        {
            sName = oMainCon.replaceNull(Request.Params.Get("fname"));
        }
        if (Request.Params.Get("fnokp") != null)
        {
            sNoKP = oMainCon.replaceNull(Request.Params.Get("fnokp"));
        }
        if (Request.Params.Get("ftelno") != null)
        {
            sTelNo = oMainCon.replaceNull(Request.Params.Get("ftelno"));
        }
        if (Request.Params.Get("datefrom") != null)
        {
            sStartDate = oMainCon.replaceNull(Request.Params.Get("datefrom"));
        }
        if (Request.Params.Get("dateto") != null)
        {
            sEndDate = oMainCon.replaceNull(Request.Params.Get("dateto"));
        }
    }

    private void processValues()
    {
        if (sAction.Equals("SEARCH"))
        {
            //do nnothing
        }
        if (sAction.Equals("RESET"))
        {
            sPeopleId = "";
            sNoKP = "";
            sName = "";
            sTelNo = "";
            sStitchNo = "";
            sStartDate = "";
            sEndDate = "";
        }
        lsStitchDetails = oMainCon.getStitchList(sCurrComp, sStitchNo, sPeopleId, sName, sNoKP, sTelNo, sStartDate, sEndDate);
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
    public static String getPeopleAutocompleteList(String id)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        ArrayList lsPeopleList = oMainCon.getPeopleList(sCurrComp, "");
        ArrayList lsPeopleObject = new ArrayList();
        if (lsPeopleList.Count > 0)
        {
            sStatus = "Y";
            for (int i = 0; i < lsPeopleList.Count; i++)
            {
                MainModel itemData = (MainModel)lsPeopleList[i];

                object objPeople = new
                {
                    value = itemData.GetSetname,
                    data = itemData.GetSetid
                };
                lsPeopleObject.Add(objPeople);
            }
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, peoplelist = lsPeopleObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getPeopleAutocompleteList2(String id)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        ArrayList lsPeopleList = oMainCon.getPeopleList(sCurrComp, "");
        ArrayList lsPeopleObject = new ArrayList();
        if (lsPeopleList.Count > 0)
        {
            sStatus = "Y";
            for (int i = 0; i < lsPeopleList.Count; i++)
            {
                MainModel itemData = (MainModel)lsPeopleList[i];

                object objPeople = new
                {
                    value = itemData.GetSetnokp,
                    data = itemData.GetSetid
                };
                lsPeopleObject.Add(objPeople);
            }
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, peoplelist = lsPeopleObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getPeopleAutocompleteList3(String id)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        ArrayList lsPeopleList = oMainCon.getPeopleList(sCurrComp, "");
        ArrayList lsPeopleObject = new ArrayList();
        if (lsPeopleList.Count > 0)
        {
            sStatus = "Y";
            for (int i = 0; i < lsPeopleList.Count; i++)
            {
                MainModel itemData = (MainModel)lsPeopleList[i];

                object objPeople = new
                {
                    value = itemData.GetSettelno,
                    data = itemData.GetSetid
                };
                lsPeopleObject.Add(objPeople);
            }
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, peoplelist = lsPeopleObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getPeopleObject(String peopleid, String name)
    {
        MainController oMainCon = new MainController();
        object objPeople = null;
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        MainModel itemData = oMainCon.getPeople(sCurrComp, peopleid, name);
        if (itemData.GetSetid.Trim().Length > 0)
        {
            sStatus = "Y";
            objPeople = new
            {
                comp = itemData.GetSetcomp,
                peopleid = itemData.GetSetid,
                name = itemData.GetSetname,
                nokp = itemData.GetSetnokp,
                address = itemData.GetSetaddress,
                gender = itemData.GetSetgender,
                telno = itemData.GetSettelno,
                email = itemData.GetSetemail,
                status = itemData.GetSetstatus
            };
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, people = objPeople };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStitchObject(String stitchno)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        MainModel modItem = oMainCon.getStitch(sCurrComp, stitchno);
        Object objRptData = new
        {
            comp = modItem.GetSetcomp,
            stitchno = modItem.GetSetstitchno,
            stitchdate = modItem.GetSetstitchdate,
            peopleid = modItem.GetSetpeopleid,
            name = modItem.GetSetname,
            nokp = modItem.GetSetnokp,
            address = modItem.GetSetaddress,
            telno = modItem.GetSettelno,
            email = modItem.GetSetemail,
            remarks = modItem.GetSetremarks,
            measurement = modItem.GetSetmeasurement,
            baju_bahu = modItem.GetSetbaju_bahu,
            baju_labuh_lengan = modItem.GetSetbaju_labuh_lengan,
            baju_labuh_baju = modItem.GetSetbaju_labuh_baju,
            baju_dada = modItem.GetSetbaju_dada,
            baju_pinggang = modItem.GetSetbaju_pinggang,
            baju_punggung = modItem.GetSetbaju_punggung,
            baju_labuh_kain = modItem.GetSetbaju_labuh_kain,
            baju_leher = modItem.GetSetbaju_leher,
            baju_span = modItem.GetSetbaju_span,
            baju_bahu_dada = modItem.GetSetbaju_bahu_dada,
            baju_bahu_pinggang = modItem.GetSetbaju_bahu_pinggang,
            seluar_pinggang = modItem.GetSetseluar_pinggang,
            seluar_punggung = modItem.GetSetseluar_punggung,
            seluar_cawat = modItem.GetSetseluar_cawat,
            seluar_paha = modItem.GetSetseluar_paha,
            seluar_lutut = modItem.GetSetseluar_lutut,
            seluar_bukaan_kaki = modItem.GetSetseluar_bukaan_kaki,
            seluar_labuh_seluar = modItem.GetSetseluar_labuh_seluar,
            status = modItem.GetSetstatus
        };

        if (modItem.GetSetstitchno.Trim().Length > 0)
        {
            sStatus = "Y";
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, stitch = objRptData };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStitchListObject(String stitchno, String peopleid, String name, String nokp, String telno, String datefrom, String dateto)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        ArrayList lsStitchList = oMainCon.getStitchListObject(sCurrComp, stitchno, peopleid, name, nokp, telno, datefrom, dateto);
        if (lsStitchList.Count > 0)
        {
            sStatus = "Y";
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, stitchlist = lsStitchList };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String saveStitch(String stitchno, String stitchdate, String peopleid, String name, String nokp, String address, String telno, String email, String remarks, String measurement,
                                    String baju_bahu, String baju_labuh_lengan, String baju_labuh_baju, String baju_dada, String baju_pinggang, String baju_punggung, String baju_labuh_kain,
                                    String baju_leher, String baju_span, String baju_bahu_dada, String baju_bahu_pinggang,
                                    String seluar_pinggang, String seluar_punggung, String seluar_cawat, String seluar_paha, String seluar_lutut, String seluar_bukaan_kaki, String seluar_labuh_seluar,
                                    String status)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String Result = "";
        String sStatus = "";

        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (peopleid.Trim().Length > 0)
        {
            MainModel oPeople = oMainCon.getPeople(sCurrComp, peopleid, "");
            oPeople.GetSetname = name;
            oPeople.GetSetnokp = nokp;
            oPeople.GetSetaddress = address;
            oPeople.GetSetgender = "";
            oPeople.GetSettelno = telno;
            oPeople.GetSetemail = email;
            oPeople.GetSetstatus = "ACTIVE";
            oPeople.GetSetmodifiedby = sUserId;
            Result = oMainCon.updatePeople(oPeople);
        }
        else
        {
            MainModel oPeople = new MainModel();
            oPeople.GetSetcomp = sCurrComp;
            oPeople.GetSetid = oMainCon.getNextRunningNo(sCurrComp, "PEOPLE", "ACTIVE");
            oPeople.GetSetname = name;
            oPeople.GetSetnokp = nokp;
            oPeople.GetSetaddress = address;
            oPeople.GetSetgender = "";
            oPeople.GetSettelno = telno;
            oPeople.GetSetemail = email;
            oPeople.GetSetstatus = "ACTIVE";
            oPeople.GetSetcreatedby = sUserId;
            Result = oMainCon.insertPeople(oPeople);
            if (Result.Equals("Y"))
            {
                oMainCon.updateNextRunningNo(sCurrComp, "PEOPLE", "ACTIVE");
                peopleid = oPeople.GetSetid;
            }
        }
        if (Result.Equals("Y"))
        {
            if (stitchno.Trim().Length > 0)
            {
                if (stitchno.Trim().Equals("..."))
                {
                    MainModel oStitch = new MainModel();
                    oStitch.GetSetcomp = sCurrComp;
                    oStitch.GetSetstitchno = oMainCon.getNextRunningNo(sCurrComp, "STITCH", "ACTIVE");
                    oStitch.GetSetstitchdate = stitchdate;
                    oStitch.GetSetpeopleid = peopleid;
                    oStitch.GetSetremarks = remarks;
                    oStitch.GetSetmeasurement = measurement;

                    oStitch.GetSetbaju_bahu = baju_bahu; oStitch.GetSetbaju_labuh_lengan = baju_labuh_lengan; oStitch.GetSetbaju_labuh_baju = baju_labuh_baju;
                    oStitch.GetSetbaju_dada = baju_dada; oStitch.GetSetbaju_pinggang = baju_pinggang; oStitch.GetSetbaju_punggung = baju_punggung;
                    oStitch.GetSetbaju_labuh_kain = baju_labuh_kain; oStitch.GetSetbaju_leher = baju_leher; oStitch.GetSetbaju_span = baju_span;
                    oStitch.GetSetbaju_bahu_dada = baju_bahu_dada; oStitch.GetSetbaju_bahu_pinggang = baju_bahu_pinggang;

                    oStitch.GetSetseluar_pinggang = seluar_pinggang; oStitch.GetSetseluar_punggung = seluar_punggung;
                    oStitch.GetSetseluar_cawat = seluar_cawat; oStitch.GetSetseluar_paha = seluar_paha;
                    oStitch.GetSetseluar_lutut = seluar_lutut; oStitch.GetSetseluar_bukaan_kaki = seluar_bukaan_kaki;
                    oStitch.GetSetseluar_labuh_seluar = seluar_labuh_seluar;

                    oStitch.GetSetstatus = "ACTIVE";
                    oStitch.GetSetcreatedby = sUserId;
                    Result = oMainCon.insertStitch(oStitch);
                    if (Result.Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(sCurrComp, "STITCH", "ACTIVE");
                        stitchno = oStitch.GetSetstitchno;
                    }
                }
                else
                {
                    MainModel oStitch = oMainCon.getStitch(sCurrComp, stitchno);
                    oStitch.GetSetstitchdate = stitchdate;
                    oStitch.GetSetpeopleid = peopleid;
                    oStitch.GetSetremarks = remarks;
                    oStitch.GetSetmeasurement = measurement;

                    oStitch.GetSetbaju_bahu = baju_bahu; oStitch.GetSetbaju_labuh_lengan = baju_labuh_lengan; oStitch.GetSetbaju_labuh_baju = baju_labuh_baju;
                    oStitch.GetSetbaju_dada = baju_dada; oStitch.GetSetbaju_pinggang = baju_pinggang; oStitch.GetSetbaju_punggung = baju_punggung;
                    oStitch.GetSetbaju_labuh_kain = baju_labuh_kain; oStitch.GetSetbaju_leher = baju_leher; oStitch.GetSetbaju_span = baju_span;
                    oStitch.GetSetbaju_bahu_dada = baju_bahu_dada; oStitch.GetSetbaju_bahu_pinggang = baju_bahu_pinggang;

                    oStitch.GetSetseluar_pinggang = seluar_pinggang; oStitch.GetSetseluar_punggung = seluar_punggung;
                    oStitch.GetSetseluar_cawat = seluar_cawat; oStitch.GetSetseluar_paha = seluar_paha;
                    oStitch.GetSetseluar_lutut = seluar_lutut; oStitch.GetSetseluar_bukaan_kaki = seluar_bukaan_kaki;
                    oStitch.GetSetseluar_labuh_seluar = seluar_labuh_seluar;

                    oStitch.GetSetstatus = status;
                    oStitch.GetSetmodifiedby = sUserId;
                    Result = oMainCon.updateStitch(oStitch);

                }
            }
            else
            {
                MainModel oStitch = new MainModel();
                oStitch.GetSetcomp = sCurrComp;
                oStitch.GetSetstitchno = oMainCon.getNextRunningNo(sCurrComp, "STITCH", "ACTIVE");
                oStitch.GetSetstitchdate = stitchdate;
                oStitch.GetSetpeopleid = peopleid;
                oStitch.GetSetremarks = remarks;
                oStitch.GetSetmeasurement = measurement;

                oStitch.GetSetbaju_bahu = baju_bahu; oStitch.GetSetbaju_labuh_lengan = baju_labuh_lengan; oStitch.GetSetbaju_labuh_baju = baju_labuh_baju;
                oStitch.GetSetbaju_dada = baju_dada; oStitch.GetSetbaju_pinggang = baju_pinggang; oStitch.GetSetbaju_punggung = baju_punggung;
                oStitch.GetSetbaju_labuh_kain = baju_labuh_kain; oStitch.GetSetbaju_leher = baju_leher; oStitch.GetSetbaju_span = baju_span;
                oStitch.GetSetbaju_bahu_dada = baju_bahu_dada; oStitch.GetSetbaju_bahu_pinggang = baju_bahu_pinggang;

                oStitch.GetSetseluar_pinggang = seluar_pinggang; oStitch.GetSetseluar_punggung = seluar_punggung;
                oStitch.GetSetseluar_cawat = seluar_cawat; oStitch.GetSetseluar_paha = seluar_paha;
                oStitch.GetSetseluar_lutut = seluar_lutut; oStitch.GetSetseluar_bukaan_kaki = seluar_bukaan_kaki;
                oStitch.GetSetseluar_labuh_seluar = seluar_labuh_seluar;

                oStitch.GetSetstatus = "ACTIVE";
                oStitch.GetSetcreatedby = sUserId;
                Result = oMainCon.insertStitch(oStitch);
                if (Result.Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "STITCH", "ACTIVE");
                    stitchno = oStitch.GetSetstitchno;
                }
            }
        }
        if (Result.Equals("Y"))
        {
            sStatus = "Y";
        }
        else
        {
            sStatus = "N";
        }

        object objData = new { result = sStatus, stitchno = stitchno };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}