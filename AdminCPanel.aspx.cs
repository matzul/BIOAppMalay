using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminCPanel : System.Web.UI.Page
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
            oUserProfile = oMainCon.getUserProfile(sCurrComp, sUserId, "", "01");
            oUserProfile.GetSetusername = oMainCon.replaceNull(Request.Params.Get("username"));
            oUserProfile.GetSetuseradd = oMainCon.replaceNull(Request.Params.Get("useradd"));
            oUserProfile.GetSetusertelno = oMainCon.replaceNull(Request.Params.Get("usertelno"));
            oUserProfile.GetSetuserpwd = oMainCon.replaceNull(Request.Params.Get("userpassword"));

        }
        else if (sAction.Equals("CREATE"))
        {
        }
        else if (sAction.Equals(""))
        {
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
        }
        else if (sAction.Equals("DELETE"))
        {
        }
    }

    private void processValues()
    {
        if (sAction.Equals(""))
        {

        }
        else if (sAction.Equals("CREATE"))
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
                Response.Redirect("AdminCPanel.aspx?action=OPEN");
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pengguna tidak berjaya disimpan...";
                Response.Redirect("AdminCPanel.aspx?action=OPEN");
            }
        }
        else if (sAction.Equals("INSERT"))
        {
        }
        else if (sAction.Equals("UPDATE"))
        {
        }
        else if (sAction.Equals("DELETE"))
        {
        }
        else if (sAction.Equals("CONFIRM"))
        {
        }
        else if (sAction.Equals("CANCEL"))
        {
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

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        ArrayList lsCompList = oMainCon.getUserCompInfoList("", sUserId,"");
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

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

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
        String sYear = "2019";
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        modComp = oMainCon.getCompInfoDetails(comp_code);
        if (flag.Equals("I") && modComp.GetSetcomp.Trim().Length == 0)
        {
            int i = oMainCon.createCompany(comp_code, comp_name, comp_id, comp_accountbank, comp_accountno, comp_address, comp_contact, comp_contactno, comp_website, comp_email, comp_icon, comp_logo1, comp_logo2, status, createdby, confirmedby, sYear);
            if (i == 1)
            {
                sStatus = "Y";
                sMessage = "company created successfully";
                UserProfileModel modUser = oMainCon.getUserProfile("", sUserId, "", "");
                int j = oMainCon.createUser(comp_code, modUser.GetSetuserid, modUser.GetSetuserpwd, modUser.GetSetusername, modUser.GetSetuseradd, modUser.GetSetusertelno, "01", "A", "010010", "01", createdby);
                UserProfileModel modUserSysAdmin = oMainCon.getUserProfile("", "sysadmin", "", "");
                int k = oMainCon.createUser(comp_code, modUserSysAdmin.GetSetuserid, modUserSysAdmin.GetSetuserpwd, modUserSysAdmin.GetSetusername, modUserSysAdmin.GetSetuseradd, modUserSysAdmin.GetSetusertelno, "01", "A", "010010", "01", createdby);
            }
            else
            {
                sStatus = "N";
                sMessage = "internal server error";
            }
        }
        else if (flag.Equals("U") && modComp.GetSetcomp.Trim().Length > 0)
        {

        }
        else
        {

        }

        object objData = new { result = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}