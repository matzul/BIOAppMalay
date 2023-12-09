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

public partial class AdminCPanel1 : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserComp = "";
    public String sUserId = "";
    public String sUserName = "";
    public String sUserPassword = "";
    public String sUserAdd = "";
    public String sUserTelNo = "";

    public String sAction = "";
    public String sActionString = "";

    public String sAlertMessage = "";

    public UserProfileModel oUserProfile = new UserProfileModel();

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
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidUserId") != null)
        {
            sUserId = oMainCon.replaceNull(Request.Params.Get("hidUserId"));
        }
        if (Request.Params.Get("hidUserId") != null)
        {
            sUserComp = oMainCon.replaceNull(Request.Params.Get("hidUserId"));
        }

        if (sAction.Equals("SAVE"))
        {
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "");
            oUserProfile.GetSetusername = oMainCon.replaceNull(Request.Params.Get("username"));
            oUserProfile.GetSetuseradd = oMainCon.replaceNull(Request.Params.Get("useradd"));
            oUserProfile.GetSetusertelno = oMainCon.replaceNull(Request.Params.Get("usertelno"));
            oUserProfile.GetSetuserpwd = oMainCon.replaceNull(Request.Params.Get("userpassword"));
            oUserProfile.GetSetusertype = oMainCon.replaceNull(Request.Params.Get("usertype"));

        }
    }

    private void processValues()
    {
        if (sAction.Equals(""))
        {

        }
        else if (sAction.Equals("OPEN"))
        {
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "");
        }
        else if (sAction.Equals("EDIT"))
        {
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "");
        }
        else if (sAction.Equals("SAVE"))
        {
            if (oMainCon.updateUserDetails(sCurrComp, sCurrComp, sUserId, oUserProfile.GetSetuserpwd, oUserProfile.GetSetusername, oUserProfile.GetSetuseradd, oUserProfile.GetSetusertelno, oUserProfile.GetSetusertype, oUserProfile.GetSetuserstatus, oUserProfile.GetSetscreenid) == 1)
            {
                sAlertMessage = "SUCCESS|Maklumat Pengguna berjaya disimpan...";
                Response.Redirect("AdminCPanel1.aspx?action=OPEN");
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pengguna tidak berjaya disimpan...";
                Response.Redirect("AdminCPanel1.aspx?action=OPEN");
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
    public static String getCompListObject(String userid)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "N";
        ArrayList lsCompObject = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsCompList = oMainCon.getCompInfoList("", sUserId);
            if (lsCompList.Count > 0)
            {
                sStatus = "Y";
                for (int i = 0; i < lsCompList.Count; i++)
                {
                    MainModel modComp = (MainModel)lsCompList[i];

                    Object objRptData = new
                    {
                        comp = modComp.GetSetcomp,
                        comp_name = modComp.GetSetcomp_name,
                        comp_id = modComp.GetSetcomp_id,
                        comp_accountbank = modComp.GetSetcomp_accountbank,
                        comp_accountno = modComp.GetSetcomp_accountno,
                        comp_address = modComp.GetSetcomp_address,
                        comp_contact = modComp.GetSetcomp_contact,
                        comp_website = modComp.GetSetcomp_website,
                        comp_email = modComp.GetSetcomp_email,
                        comp_icon = modComp.GetSetcomp_icon,
                        comp_logo1 = modComp.GetSetcomp_logo1,
                        comp_logo2 = modComp.GetSetcomp_logo2,
                        status = modComp.GetSetstatus,
                        createdby = modComp.GetSetcreatedby,
                        createddate = modComp.GetSetcreateddate,
                        confirmedby = modComp.GetSetconfirmedby,
                        confirmeddate = modComp.GetSetconfirmeddate
                    };

                    lsCompObject.Add(objRptData);
                }
            }
            else
            {
                sStatus = "N";
            }
        }

        object objData = new { result = sStatus, complist = lsCompObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getCompObject(String compcode)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "N";
        MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modComp = oMainCon.getCompInfoDetails(compcode);

            Object objRptData = new
            {
                comp = modComp.GetSetcomp,
                comp_name = modComp.GetSetcomp_name,
                comp_id = modComp.GetSetcomp_id,
                comp_accountbank = modComp.GetSetcomp_accountbank,
                comp_accountno = modComp.GetSetcomp_accountno,
                comp_address = modComp.GetSetcomp_address,
                comp_contact = modComp.GetSetcomp_contact,
                comp_contactno = modComp.GetSetcomp_contactno,
                comp_website = modComp.GetSetcomp_website,
                comp_email = modComp.GetSetcomp_email,
                comp_icon = modComp.GetSetcomp_icon,
                comp_logo1 = modComp.GetSetcomp_logo1,
                comp_logo2 = modComp.GetSetcomp_logo2,
                status = modComp.GetSetstatus,
                createdby = modComp.GetSetcreatedby,
                createddate = modComp.GetSetcreateddate,
                confirmedby = modComp.GetSetconfirmedby,
                confirmeddate = modComp.GetSetconfirmeddate
            };

            if (modComp.GetSetcomp.Trim().Length > 0)
            {
                sStatus = "Y";
            }
            else
            {
                sStatus = "N";
            }
            object objData = new { result = sStatus, compinfo = objRptData };
            jsonResponse = new JavaScriptSerializer().Serialize(objData);
        }
        else
        {
            object objData = new { result = sStatus, compinfo = new object() };
            jsonResponse = new JavaScriptSerializer().Serialize(objData);
        }
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String putCompObject(String flag, String comp_code, String comp_name, String comp_id, String comp_accountbank, String comp_accountno, String comp_address, String comp_contact, String comp_contactno, String comp_website, String comp_email, String comp_icon, String comp_logo1, String comp_logo2, String status, String createdby, String confirmedby)
    {
        MainController oMainCon = new MainController();
        MainModel modComp = new MainModel();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        DateTime dateTime = DateTime.UtcNow.Date;
        String sYear = dateTime.ToString("yyyy");
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        else
        {
            sUserId = createdby;
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modComp = oMainCon.getCompInfoDetails(comp_code);
            if (flag.Equals("I") && modComp.GetSetcomp.Trim().Length == 0)
            {
                int i = oMainCon.createCompany(comp_code, comp_name, comp_id, comp_accountbank, comp_accountno, comp_address, comp_contact, comp_contactno, comp_website, comp_email, comp_icon, comp_logo1, comp_logo2, status, createdby, confirmedby, sYear);
                if (i == 1)
                {
                    sStatus = "Y";
                    sMessage = "company created successfully";

                    //No need to create fiscalyear which already handle in createcompany function
                    //int j = oMainCon.createfiscalyeardashboard(comp_code, sYear);

                    UserProfileModel modUser = oMainCon.getUserProfile("", sUserId, "", "");
                    int k = oMainCon.createUser(comp_code, modUser.GetSetuserid, modUser.GetSetuserpwd, modUser.GetSetusername, modUser.GetSetuseradd, modUser.GetSetusertelno, "01", "A", "010010", "01", createdby);
                    UserProfileModel modUserSysAdmin = oMainCon.getUserProfile("", "sysadmin", "", "");
                    int l = oMainCon.createUser(comp_code, modUserSysAdmin.GetSetuserid, modUserSysAdmin.GetSetuserpwd, modUserSysAdmin.GetSetusername, modUserSysAdmin.GetSetuseradd, modUserSysAdmin.GetSetusertelno, "01", "A", "010010", "01", "sysadmin");

                    ArrayList lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
                    for (int x = 0; x < lsComp.Count; x++)
                    {
                        MainModel modCompInfo = (MainModel)lsComp[x];
                        for (int y = 0; y < lsComp.Count; y++)
                        {
                            MainModel modCompInfo2 = (MainModel)lsComp[y];
                            if (modCompInfo.GetSetcomp != modCompInfo2.GetSetcomp)
                            {
                                MainModel oModBP = oMainCon.getBPDetails(modCompInfo.GetSetcomp, modCompInfo2.GetSetcomp);
                                if (oModBP.GetSetbpid.Trim().Length > 0)
                                {
                                    /*
                                    oModBP.GetSetcashguarantee = 0;
                                    oModBP.GetSetbankguarantee = 0;
                                    oModBP.GetSetcreditlimit = 0;
                                    */
                                    oModBP.GetSetbpcat = "SUBSIDIARY";
                                    oModBP.GetSetdiscounttype = "NORMAL";
                                    oModBP.GetSetbpstatus = modCompInfo2.GetSetstatus;
                                    oModBP.GetSetbpreference = modCompInfo2.GetSetcomp_id;
                                    oModBP.GetSetbpdesc = modCompInfo2.GetSetcomp_name;
                                    oModBP.GetSetbpaddress = modCompInfo2.GetSetcomp_address;
                                    oModBP.GetSetbpcontact = modCompInfo2.GetSetcomp_contact + "-" + modCompInfo2.GetSetcomp_contactno;

                                    oMainCon.updateBusinessPartner(oModBP);
                                }
                                else
                                {
                                    oModBP.GetSetcomp = modCompInfo.GetSetcomp;
                                    oModBP.GetSetbpid = modCompInfo2.GetSetcomp;
                                    oModBP.GetSetbpcat = "SUBSIDIARY";
                                    oModBP.GetSetdiscounttype = "NORMAL";
                                    oModBP.GetSetcashguarantee = 0;
                                    oModBP.GetSetbankguarantee = 0;
                                    oModBP.GetSetcreditlimit = 0;
                                    oModBP.GetSetbpstatus = modCompInfo2.GetSetstatus;
                                    oModBP.GetSetbpreference = modCompInfo2.GetSetcomp_id;
                                    oModBP.GetSetbpdesc = modCompInfo2.GetSetcomp_name;
                                    oModBP.GetSetbpaddress = modCompInfo2.GetSetcomp_address;
                                    oModBP.GetSetbpcontact = modCompInfo2.GetSetcomp_contact + "-" + modCompInfo2.GetSetcomp_contactno;

                                    oMainCon.insertBusinessPartner(oModBP);
                                }
                            }
                        }
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "internal server error";
                }
            }
            else if (flag.Equals("U") && modComp.GetSetcomp.Trim().Length > 0)
            {
                int i = oMainCon.updateCompany(comp_code, comp_name, comp_id, comp_accountbank, comp_accountno, comp_address, comp_contact, comp_contactno, comp_website, comp_email, comp_icon, comp_logo1, comp_logo2, status);
                if (i == 1)
                {
                    sStatus = "Y";
                    sMessage = "company updated successfully";
                    UserProfileModel modUser = oMainCon.getUserProfile("", sUserId, "", "");
                    int j = oMainCon.createUser(comp_code, modUser.GetSetuserid, modUser.GetSetuserpwd, modUser.GetSetusername, modUser.GetSetuseradd, modUser.GetSetusertelno, "01", "A", "010010", "01", createdby);

                    ArrayList lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
                    for (int x = 0; x < lsComp.Count; x++)
                    {
                        MainModel modCompInfo = (MainModel)lsComp[x];
                        for (int y = 0; y < lsComp.Count; y++)
                        {
                            MainModel modCompInfo2 = (MainModel)lsComp[y];
                            if (modCompInfo.GetSetcomp != modCompInfo2.GetSetcomp)
                            {
                                MainModel oModBP = oMainCon.getBPDetails(modCompInfo.GetSetcomp, modCompInfo2.GetSetcomp);
                                if (oModBP.GetSetbpid.Trim().Length > 0)
                                {
                                    /*
                                    oModBP.GetSetcashguarantee = 0;
                                    oModBP.GetSetbankguarantee = 0;
                                    oModBP.GetSetcreditlimit = 0;
                                    */
                                    oModBP.GetSetbpstatus = modCompInfo2.GetSetstatus;
                                    oModBP.GetSetbpreference = modCompInfo2.GetSetcomp_id;
                                    oModBP.GetSetbpdesc = modCompInfo2.GetSetcomp_name;
                                    oModBP.GetSetbpaddress = modCompInfo2.GetSetcomp_address;
                                    oModBP.GetSetbpcontact = modCompInfo2.GetSetcomp_contact + "-" + modCompInfo2.GetSetcomp_contactno;

                                    oMainCon.updateBusinessPartner(oModBP);
                                }
                                else
                                {
                                    oModBP.GetSetcomp = modCompInfo.GetSetcomp;
                                    oModBP.GetSetbpid = modCompInfo2.GetSetcomp;
                                    oModBP.GetSetbpcat = "SUBSIDIARY";
                                    oModBP.GetSetdiscounttype = "NORMAL";
                                    oModBP.GetSetcashguarantee = 0;
                                    oModBP.GetSetbankguarantee = 0;
                                    oModBP.GetSetcreditlimit = 0;
                                    oModBP.GetSetbpstatus = modCompInfo2.GetSetstatus;
                                    oModBP.GetSetbpreference = modCompInfo2.GetSetcomp_id;
                                    oModBP.GetSetbpdesc = modCompInfo2.GetSetcomp_name;
                                    oModBP.GetSetbpaddress = modCompInfo2.GetSetcomp_address;
                                    oModBP.GetSetbpcontact = modCompInfo2.GetSetcomp_contact + "-" + modCompInfo2.GetSetcomp_contactno;

                                    oMainCon.insertBusinessPartner(oModBP);
                                }
                            }
                        }
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "internal server error";
                }
            }
            else
            {

            }
        }

        object objData = new { result = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getUserCompListObject(String comp, String userid)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "N";
        ArrayList lsCompUserObject = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsCompUserList = oMainCon.getCompUserProfileList(comp, userid);
            if (lsCompUserList.Count > 0)
            {
                sStatus = "Y";
                for (int i = 0; i < lsCompUserList.Count; i++)
                {
                    UserProfileModel modUser = (UserProfileModel)lsCompUserList[i];

                    Object objRptData = new
                    {
                        comp = modUser.GetSetcomp,
                        userid = modUser.GetSetuserid,
                        username = modUser.GetSetusername,
                        usertype = modUser.GetSetusertype,
                        roleid = modUser.GetSetuserroleid,
                        status = modUser.GetSetuserstatus
                    };

                    lsCompUserObject.Add(objRptData);
                }
            }
            else
            {
                sStatus = "N";
            }
        }

        object objData = new { result = sStatus, compuserlist = lsCompUserObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getUserListObject(String userid)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "N";
        ArrayList lsUserObject = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsUserList = oMainCon.getUserProfileList("", "", "", "", sUserId);
            if (lsUserList.Count > 0)
            {
                sStatus = "Y";
                for (int i = 0; i < lsUserList.Count; i++)
                {
                    UserProfileModel modUser = (UserProfileModel)lsUserList[i];

                    Object objRptData = new
                    {
                        comp = modUser.GetSetcomp,
                        userid = modUser.GetSetuserid,
                        username = modUser.GetSetusername,
                        usertype = modUser.GetSetusertype,
                        status = modUser.GetSetuserstatus
                    };

                    lsUserObject.Add(objRptData);
                }
            }
            else
            {
                sStatus = "N";
            }

        }

        object objData = new { result = sStatus, userlist = lsUserObject };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertUserRoleObject(String comp, String userid, String roleid)
    {
        MainController oMainCon = new MainController();
        MainModel modComp = new MainModel();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            UserProfileModel modUser = oMainCon.getUserProfile("", userid, "", "");
            int i = oMainCon.insertUserRole(comp, modUser.GetSetuserid, roleid);
            if (i == 1)
            {
                sStatus = "Y";
                sMessage = "user inserted successfully";
            }
            else
            {
                sStatus = "N";
                sMessage = "internal server error";
            }

        }

        object objData = new { result = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String delUserObject(String comp, String userid)
    {
        MainController oMainCon = new MainController();
        MainModel modComp = new MainModel();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        String TokenNumber = "M05kit0@1";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            int i = oMainCon.deleteCompUser(comp, userid);
            if (i == 1)
            {
                sStatus = "Y";
                sMessage = "user remove successfully";
            }
            else
            {
                sStatus = "N";
                sMessage = "internal server error";
            }

        }

        object objData = new { result = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}