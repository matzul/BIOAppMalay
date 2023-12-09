using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompJawatankuasaDetails1 : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sUserIdComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sUserAction = "";
    public String sActionString = "";
    public String sAlertMessage = "";
    public String sCompId = "";
    public String sMasjidUserId = "";
    public String sDUserId = "";
    public String sDUserComp = "";
    public MainModel oModComp = new MainModel();
    public MainModel oModCommittee = new MainModel();
    public ArrayList lsUser = new ArrayList();
    public ArrayList lsCommitteeCount = new ArrayList();

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
            sUserIdComp = Session["comp"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["compid"] != null)
        {
            sCompId = Request.QueryString["compid"].ToString();
        }
        if (Request.QueryString["user"] != null)
        {
            sMasjidUserId = Request.QueryString["user"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        if (sAction.Equals("ADD"))
        {
            oModComp = new MainModel();
        }
        if (sAction.Equals("CREATE"))
        {
            oModComp = new MainModel();
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
            sUserIdComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidCompId") != null)
        {
            sCompId = oMainCon.replaceNull(Request.Params.Get("hidCompId"));
        }
        if (Request.Params.Get("hidUserAction") != null)
        {
            sUserAction = oMainCon.replaceNull(Request.Params.Get("hidUserAction"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            sDUserId = oMainCon.replaceNull(Request.Params.Get("hidLineNo"));
        }
        if (Request.Params.Get("hidLineNo1") != null)
        {
            sDUserComp = oMainCon.replaceNull(Request.Params.Get("hidLineNo1"));
        }

        //for reset
        if (sAction.Equals("CREATE"))
        {
            oModCommittee = new MainModel();
            oModCommittee.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compCode"));
            oModCommittee.GetSetcommittee_id = oMainCon.replaceNull(Request.Params.Get("committeeid"));
            oModCommittee.GetSetcommittee_name = oMainCon.replaceNull(Request.Params.Get("committeename"));
            oModCommittee.GetSetcommittee_address = oMainCon.replaceNull(Request.Params.Get("committeeaddress"));
            oModCommittee.GetSetcommittee_contact = oMainCon.replaceNull(Request.Params.Get("committeecontact"));
            oModCommittee.GetSetcommittee_role = oMainCon.replaceNull(Request.Params.Get("committeerole"));
            oModCommittee.GetSetcommittee_status = oMainCon.replaceNull(Request.Params.Get("committeestatus"));
            oModCommittee.GetSetcommittee_dob = oMainCon.replaceNull(Request.Params.Get("committeedob"));
            oModCommittee.GetSetcommittee_age = oMainCon.replaceNull(Request.Params.Get("committeeage"));
            oModCommittee.GetSetcommittee_doa = oMainCon.replaceNull(Request.Params.Get("committeedoa"));
            oModCommittee.GetSetcommittee_appointmentby = oMainCon.replaceNull(Request.Params.Get("committeeappointmentby"));
            oModCommittee.GetSetcommittee_job = oMainCon.replaceNull(Request.Params.Get("committeejob"));
            oModCommittee.GetSetexchangeid = oMainCon.replaceNull(Request.Params.Get("exchangeid"));

        }
        else if (sAction.Equals("SAVE"))
        {
            oModCommittee = new MainModel();
            oModCommittee.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compCode"));
            oModCommittee.GetSetcommittee_id = oMainCon.replaceNull(Request.Params.Get("committeeid"));
            oModCommittee.GetSetcommittee_name = oMainCon.replaceNull(Request.Params.Get("committeename"));
            oModCommittee.GetSetcommittee_address = oMainCon.replaceNull(Request.Params.Get("committeeaddress"));
            oModCommittee.GetSetcommittee_contact = oMainCon.replaceNull(Request.Params.Get("committeecontact"));
            oModCommittee.GetSetcommittee_role = oMainCon.replaceNull(Request.Params.Get("committeerole"));
            oModCommittee.GetSetcommittee_status = oMainCon.replaceNull(Request.Params.Get("committeestatus"));
            oModCommittee.GetSetcommittee_dob = oMainCon.replaceNull(Request.Params.Get("committeedob"));
            oModCommittee.GetSetcommittee_age = oMainCon.replaceNull(Request.Params.Get("committeeage"));
            oModCommittee.GetSetcommittee_doa = oMainCon.replaceNull(Request.Params.Get("committeedoa"));
            oModCommittee.GetSetcommittee_appointmentby = oMainCon.replaceNull(Request.Params.Get("committeeappointmentby"));
            oModCommittee.GetSetcommittee_job = oMainCon.replaceNull(Request.Params.Get("committeejob"));

        }
        else if (sAction.Equals("EDIT"))
        {
            sMasjidUserId = Request.QueryString["user"].ToString();
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "MAKLUMAT JAWATANKUASA";
            if (sUserIdComp.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = new MainModel();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat pegawai...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = new MainModel();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT JAWATANKUASA";
            if (sUserIdComp.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sUserIdComp, sMasjidUserId);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat pegawai...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sUserIdComp, sMasjidUserId);
            }
        }
        else if (sAction.Equals("CREATE"))
        {
            bool process = true;
            lsUser = oMainCon.getCommitteeList1(sUserIdComp, "", "", "");

            if (lsUser.Count > 0)
            {
                for (int i = 0; i < lsUser.Count; i++)
                {
                    MainModel modCommittee = (MainModel)lsUser[i];
                    String a = modCommittee.GetSetcommittee_role;
                    //if (modCommittee.GetSetcommittee_role.Equals(oModCommittee.GetSetcommittee_role))
                    if (modCommittee.GetSetcommittee_role.Equals(oModCommittee.GetSetcommittee_role) && (modCommittee.GetSetcommittee_role == "Pengerusi" || modCommittee.GetSetcommittee_role == "Timbalan Pengerusi" || modCommittee.GetSetcommittee_role == "Setiausaha" || modCommittee.GetSetcommittee_role == "Bendahari") && !modCommittee.GetSetcommittee_id.Equals(oModCommittee.GetSetcommittee_id))
                    {
                        if (modCommittee.GetSetcommittee_status.Equals("ACTIVE"))
                        {
                            process = false;
                            break;
                        }
                    }
                }
            }

            if (process)
            {

                sActionString = "DAFTAR JAWATANKUASA";
                if (sUserIdComp.Length > 0)
                {
                    int i = oMainCon.insertCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id, oModCommittee.GetSetcommittee_name, oModCommittee.GetSetcommittee_address, oModCommittee.GetSetcommittee_contact, oModCommittee.GetSetcommittee_role, oModCommittee.GetSetcommittee_status, sUserId, oModCommittee.GetSetcommittee_dob, oModCommittee.GetSetcommittee_age, "", oModCommittee.GetSetcommittee_doa, oModCommittee.GetSetcommittee_appointmentby, "", "JK_COMP", oModCommittee.GetSetcommittee_job, oModCommittee.GetSetexchangeid);

                    if (i == 1)
                    {
                        sAction = "OPEN";
                        sAlertMessage = "SUCCESS|Daftar maklumat pegawai berjaya...";
                        oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                        oModCommittee = oMainCon.getCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id);
                    }
                    else
                    {
                        sAction = "ADD";
                        sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat pegawai...";
                        oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    }
                }
                else
                {
                    sAction = "ADD";
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat pegawai...";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                }
            }
            else
            {
                sAction = "ADD";
                sAlertMessage = "ERROR|Jawatan tidak boleh lebih daripada satu...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PEGAWAI";
            if (sUserIdComp.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sUserIdComp, sMasjidUserId);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat pegawai...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sUserIdComp, sMasjidUserId);
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sUserIdComp.Length > 0)
            {
                bool process = true;
                lsUser = oMainCon.getCommitteeList(sUserIdComp, "", "", "");

                if (lsUser.Count > 0)
                {
                    for (int i = 0; i < lsUser.Count; i++)
                    {
                        MainModel modCommittee = (MainModel)lsUser[i];
                        String a = modCommittee.GetSetcommittee_role;
                        if (modCommittee.GetSetcommittee_role.Equals(oModCommittee.GetSetcommittee_role) && (modCommittee.GetSetcommittee_role == "Pengerusi" || modCommittee.GetSetcommittee_role == "Timbalan Pengerusi" || modCommittee.GetSetcommittee_role == "Setiausaha" || modCommittee.GetSetcommittee_role == "Bendahari") && !modCommittee.GetSetcommittee_id.Equals(oModCommittee.GetSetcommittee_id))
                        {
                            if (modCommittee.GetSetcommittee_status.Equals("ACTIVE"))
                            {
                                process = false;
                                break;
                            }
                        }
                    }
                }

                if (process)
                {
                    String sStatus = "Y";
                    sAction = "OPEN";

                    int z = oMainCon.updateCommittee1(sUserIdComp, oModCommittee.GetSetcommittee_id, oModCommittee.GetSetcommittee_name, oModCommittee.GetSetcommittee_address, oModCommittee.GetSetcommittee_contact, oModCommittee.GetSetcommittee_role, oModCommittee.GetSetcommittee_status, oModCommittee.GetSetcommittee_dob, oModCommittee.GetSetcommittee_age, oModCommittee.GetSetcommittee_doa, oModCommittee.GetSetcommittee_appointmentby, oModCommittee.GetSetcommittee_job);

                    if (z == 1)
                    {
                        sStatus = "Y";
                        sAlertMessage = "SUCCESS|Maklumat Pegawai disimpan...";
                        sAction = "OPEN";
                        sActionString = "KEMASKINI JAWATANKUASA";
                        oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                        oModCommittee = oMainCon.getCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id);
                    }
                    else
                    {
                        sStatus = "N";
                        sAlertMessage = "ERROR|Maklumat Pegawai tidak berjaya disimpan...";
                        sAction = "EDIT";
                        sActionString = "KEMASKINI JAWATANKUASA";
                        oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                        oModCommittee = oMainCon.getCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id);
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Jawatan tidak boleh lebih daripada satu...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI JAWATANKUASA";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModCommittee = oMainCon.getCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id);
                }

            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pegawai tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI JAWATANKUASA";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sUserIdComp, oModCommittee.GetSetcommittee_id);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sDUserId.Length > 0 && sDUserComp.Length > 0)
            {
                int i = 0;
                i = oMainCon.deleteCommitteeUser(sDUserComp, sDUserId);
                if (i == 1)
                {
                    sAlertMessage = "SUCCESS|Maklumat Pegawai berjaya dikeluarkan...";
                    sAction = "OPEN";
                    sActionString = "KEMASKINI JAWATANKUASA";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModCommittee = oMainCon.getCommittee(sDUserComp, sDUserId);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pegawai tidak berjaya dikeluarkan...";
                    sAction = "OPEN";
                    sActionString = "KEMASKINI JAWATANKUASA";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModCommittee = oMainCon.getCommittee(sDUserComp, sDUserId);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pegawai tidak berjaya dikeluarkan...";
                sAction = "OPEN";
                sActionString = "KEMASKINI JAWATANKUASA";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModCommittee = oMainCon.getCommittee(sDUserComp, sDUserId);
            }
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
    public static string getPositionList(String compId, String committeeid, String committeerole, String committeetype)
    {
        String sStatus = "Y";
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsPosition = oMainCon.getCommitteePositionList(compId, committeeid, committeerole, committeetype, "ACTIVE");

        object posData = new { result = sStatus, positionlist = lsPosition };

        jsonResponse = new JavaScriptSerializer().Serialize(posData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static String getBusinessPartnerDetails(string bpid)
    {
        MainController oMainCon = new MainController();
        String sCurrComp = "";
        String sUserId = "";
        String sStatus = "N";

        MainModel modBP = new MainModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = oMainCon.replaceNull(HttpContext.Current.Session["comp"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (sCurrComp.Length > 0 && sUserId.Length > 0 && bpid.Length > 0)
        {
            modBP = oMainCon.getBPDetails(bpid, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, bpdetails = modBP };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getBusinessPartnerList(string bpid)
    {
        MainController oMainCon = new MainController();
        String sCurrComp = "";
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsBPList = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = oMainCon.replaceNull(HttpContext.Current.Session["comp"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (sCurrComp.Length > 0 && sUserId.Length > 0 && bpid.Length > 0)
        {
            lsBPList = oMainCon.getBPList(bpid, "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, bplist = lsBPList };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCommitteeDetails(string commid)
    {
        MainController oMainCon = new MainController();
        String sCurrComp = "";
        String sUserId = "";
        String sStatus = "N";

        MainModel modComm = new MainModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = oMainCon.replaceNull(HttpContext.Current.Session["comp"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (sCurrComp.Length > 0 && sUserId.Length > 0 && commid.Length > 0)
        {
            modComm = oMainCon.getCommittee(sCurrComp,commid);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, commdetails = modComm };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCommitteeList(string commid)
    {
        MainController oMainCon = new MainController();
        String sCurrComp = "";
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsCommList = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = oMainCon.replaceNull(HttpContext.Current.Session["comp"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (sCurrComp.Length > 0 && sUserId.Length > 0 && commid.Length > 0)
        {
            lsCommList = oMainCon.getJKCommitteeList(sCurrComp, commid, "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, commlist = lsCommList };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}