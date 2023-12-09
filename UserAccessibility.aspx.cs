using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccessibility : System.Web.UI.Page
{

    public MainController oMainCon = new MainController();
    public String sUserIdComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sUserAction = "";
    public String sActionString = "";
    public String sAlertMessage = "";
    public String sCompId = "";
    public MainModel oModComp = new MainModel();
    public ArrayList lsRole = new ArrayList();
    public MainModel oModBP = new MainModel();


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
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

    }

    private void processValues()
    {
        if (sAction.Equals("OPEN"))
        {
            sActionString = "Capaian Pengguna";
            if (sCompId.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sCompId);
                lsRole = oMainCon.getCompRoleList();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
                oModComp = oMainCon.getCompInfoDetails(sCompId);
                lsRole = oMainCon.getCompRoleList();
            }
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
        if (Request.Params.Get("hidComp") != null)
        {
            sCompId = oMainCon.replaceNull(Request.Params.Get("hidCompId"));
        }
        if (Request.Params.Get("hidUserAction") != null)
        {
            sUserAction = oMainCon.replaceNull(Request.Params.Get("hidUserAction"));
        }

        //for reset
        if (sAction.Equals("ADD"))
        {
            sCompId = "";
            oModComp = new MainModel();
        }
        else if(sAction.Equals("OPEN"))
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
            sCompId = oMainCon.replaceNull(Request.Params.Get("hidComp"));
            oModBP.GetSetuseraccess = oMainCon.replaceNull(Request.Params.Get("bpcat"));
        }

    }

    [WebMethod(EnableSession = true)]
    public static string getModule()
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        MainController oMainCon = new MainController();
        ArrayList lsmodule = new ArrayList();

        lsmodule = oMainCon.getModule("A");
        
        jsonResponse = new JavaScriptSerializer().Serialize(lsmodule);
        //jsonResponse = convertJson(lsmodule);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getSubModule(string moduleid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        MainController oMainCon = new MainController();
        ArrayList lssubmodule = new ArrayList();

        lssubmodule = oMainCon.getSubModule(moduleid, "A");

        jsonResponse = new JavaScriptSerializer().Serialize(lssubmodule);
        //jsonResponse = convertJson(lsmodule);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getUserRoleSubModule(string comp, string roleid )
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        MainController oMainCon = new MainController();
        ArrayList mRolesubmodule = new ArrayList();

        mRolesubmodule = oMainCon.getRoleSubModule(comp, roleid);

        jsonResponse = new JavaScriptSerializer().Serialize(mRolesubmodule);
        //jsonResponse = convertJson(lsmodule);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string updateUserRoleSubModule(string comp, string roleid, String list)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();
        String UserRoleSubModule = "";
        string sStatus = "";
        string sMessage = "";

        string[] split = list.Split("|".ToCharArray());
        string[] module = split[0].Split(",".ToCharArray());
        string[] submodule = split[1].Split(",".ToCharArray());

        UserRoleSubModule = oMainCon.updateUserRoleSubModule(comp, roleid, module, submodule);
        object objData = new { status = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        //jsonResponse = convertJson(lsmodule);

        return jsonResponse;

        //HttpContext.Current.Response.Write(jsonResponse);
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
        }
    }


}