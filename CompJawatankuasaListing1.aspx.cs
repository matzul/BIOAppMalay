using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompJawatankuasaListing1 : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sCurrPage = "";
    public String sComtID = "";
    public String sComtName = "";
    public String sCompId = "";
    public ArrayList lsCommittee = new ArrayList();
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
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (sAction.Equals("SEARCH"))
        {
            sComtID = oMainCon.replaceNull(Request.Params.Get("committeeid"));
            sComtName = oMainCon.replaceNull(Request.Params.Get("commiteename"));
        }
        if (sAction.Equals("RESET"))
        {
            sComtID = "";
            sComtName = "";
        }
        if (sAction.Equals("NEXT_PAGE"))
        {
            sComtID = oMainCon.replaceNull(Request.Params.Get("committeeid"));
            sComtName = oMainCon.replaceNull(Request.Params.Get("commiteename"));
            sCurrPage = oMainCon.replaceNull(Request.Params.Get("hidNextPage"));
        }
    }

    private void processValues()
    {
        if (sCurrPage.Trim().Length == 0)
        {
            sCurrPage = "1";
        }
        lsCommittee = oMainCon.getJKCommitteeList(sCurrComp, sComtID, sComtName, "");
        lsCommitteeCount = oMainCon.getJKCommitteeList(sCurrComp, sComtID, sComtName, "", sCurrPage);
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
    public static string getPositionListHistory(String comp, String committeerole, String committeetype, String committee_status)
    {
        String sStatus = "Y";
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsPosition = oMainCon.getCommitteePositionList(comp, "", committeerole, committeetype, committee_status);

        object posData = new { result = sStatus, positionlist = lsPosition };

        jsonResponse = new JavaScriptSerializer().Serialize(posData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
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