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

public partial class AdminCPanel2 : System.Web.UI.Page
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
    public ArrayList lsComp = new ArrayList();

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
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "01");
            oUserProfile.GetSetusername = oMainCon.replaceNull(Request.Params.Get("username"));
            oUserProfile.GetSetuseradd = oMainCon.replaceNull(Request.Params.Get("useradd"));
            oUserProfile.GetSetusertelno = oMainCon.replaceNull(Request.Params.Get("usertelno"));
            oUserProfile.GetSetuserpwd = oMainCon.replaceNull(Request.Params.Get("userpassword"));

        }
    }

    private void processValues()
    {
        if (sAction.Equals(""))
        {

        }
        else if (sAction.Equals("OPEN"))
        {
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "01");
        }
        else if (sAction.Equals("EDIT"))
        {
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "01");
        }
        else if (sAction.Equals("SAVE"))
        {
            if (oMainCon.updateUserDetails(sCurrComp, sCurrComp, sUserId, oUserProfile.GetSetuserpwd, oUserProfile.GetSetusername, oUserProfile.GetSetuseradd, oUserProfile.GetSetusertelno, oUserProfile.GetSetusertype, oUserProfile.GetSetuserstatus, oUserProfile.GetSetscreenid) == 1)
            {
                sAlertMessage = "SUCCESS|Maklumat Pengguna berjaya disimpan...";
                Response.Redirect("AdminCPanel2.aspx?action=OPEN");
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pengguna tidak berjaya disimpan...";
                Response.Redirect("AdminCPanel2.aspx?action=OPEN");
            }
        }
        lsComp = oMainCon.getCompInfoList("", sUserId);

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

        String TokenNumber = "00000000";
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
                        useradd = modUser.GetSetuseradd,
                        usertelno = modUser.GetSetusertelno,
                        usertype = modUser.GetSetusertype,
                        roleid = modUser.GetSetuserroleid,
                        userstatus = modUser.GetSetuserstatus,
                        screenid = modUser.GetSetscreenid
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
    public static String getUserObject(String userid)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sCurrComp = "";
        String sStatus = "N";
        UserProfileModel modUser = new UserProfileModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }
        if (HttpContext.Current.Session["comp"] != null)
        {
            sCurrComp = HttpContext.Current.Session["comp"].ToString();
        }

        String TokenNumber = "00000000";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modUser = oMainCon.getUserProfile("", userid, "", "");

            Object objRptData = new
            {
                comp = modUser.GetSetcomp,
                userid = modUser.GetSetuserid,
                userpwd = modUser.GetSetuserpwd,
                username = modUser.GetSetusername,
                useradd = modUser.GetSetuseradd,
                usertelno = modUser.GetSetusertelno,
                usertype = modUser.GetSetusertype,
                userstatus = modUser.GetSetuserstatus,
                screenid = modUser.GetSetscreenid
            };

            if (modUser.GetSetuserid.Trim().Length > 0)
            {
                sStatus = "Y";
            }
            else
            {
                sStatus = "N";
            }
            object objData = new { result = sStatus, userinfo = objRptData };
            jsonResponse = new JavaScriptSerializer().Serialize(objData);
        }
        else
        {
            object objData = new { result = sStatus, userinfo = new object() };
            jsonResponse = new JavaScriptSerializer().Serialize(objData);
        }
        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String putUserObject(String flag, String userid, String userpwd, String usercomp, String username, String useradd, String usertelno, String usertype, String screenid, String userroleid, String userstatus, String createdby, String confirmedby)
    {
        MainController oMainCon = new MainController();
        UserProfileModel modUser = new UserProfileModel();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        String TokenNumber = "00000000";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modUser = oMainCon.getUserProfile("", userid, "", "");
            if (flag.Equals("I") && modUser.GetSetuserid.Trim().Length == 0)
            {
                int i = oMainCon.createUser(usercomp, userid, userpwd, username, useradd, usertelno, usertype, userstatus, screenid, userroleid, createdby);
                if (i == 1)
                {
                    sStatus = "Y";
                    sMessage = "user created successfully";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "internal server error";
                }
            }
            else if (flag.Equals("U") && modUser.GetSetuserid.Trim().Length > 0)
            {
                int i = oMainCon.updateUserDetails(modUser.GetSetcomp, usercomp, userid, userpwd, username, useradd, usertelno, modUser.GetSetusertype, userstatus, screenid);
                if (i == 1)
                {
                    int j = oMainCon.createUser(usercomp, userid, userpwd, username, useradd, usertelno, modUser.GetSetusertype, userstatus, screenid, userroleid, createdby);
                    sStatus = "Y";
                    sMessage = "user updated successfully";
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

        String TokenNumber = "00000000";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsCompUserList = oMainCon.getUserCompInfoList(comp, userid, "");
            if (lsCompUserList.Count > 0)
            {
                sStatus = "Y";
                for (int i = 0; i < lsCompUserList.Count; i++)
                {
                    MainModel modComp = (MainModel)lsCompUserList[i];

                    Object objRptData = new
                    {
                        userid = modComp.GetSetuserid,
                        roleid = modComp.GetSetroleid,
                        comp = modComp.GetSetcomp,
                        comp_name = modComp.GetSetcomp_name,
                        comp_id = modComp.GetSetcomp_id,
                        comp_address = modComp.GetSetcomp_address,
                        comp_status = modComp.GetSetstatus
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

        String TokenNumber = "00000000";
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

        String TokenNumber = "00000000";
        String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            UserProfileModel modUser = oMainCon.getUserProfile("", userid, "", "");
            int i = oMainCon.insertUserRole(comp, modUser.GetSetuserid, roleid);
            if (i == 1)
            {
                sStatus = "Y";
                sMessage = "comp inserted successfully";
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

        String TokenNumber = "00000000";
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