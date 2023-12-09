using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompInfoListing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sCurrPage = "";
    public String sCompId = "";
    public String sinfoType = "";
    public String sinfoStatus = "";
    public ArrayList lsInfo = new ArrayList();
    public ArrayList lsInfoCount = new ArrayList();

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
            sinfoType = oMainCon.replaceNull(Request.Params.Get("infoType"));
            sinfoStatus = oMainCon.replaceNull(Request.Params.Get("infoStatus"));
        }
        if (sAction.Equals("RESET"))
        {
            sinfoType = "";
            sinfoStatus = "";
        }
        if (sAction.Equals("NEXT_PAGE"))
        {
            sinfoType = oMainCon.replaceNull(Request.Params.Get("infoType"));
            sinfoStatus = oMainCon.replaceNull(Request.Params.Get("infoStatus"));
            sCurrPage = oMainCon.replaceNull(Request.Params.Get("hidNextPage"));
        }
    }

    private void processValues()
    {
        if (sCurrPage.Trim().Length == 0)
        {
            sCurrPage = "1";
        }
        lsInfo = oMainCon.getInfoList(sCurrComp, "", sinfoType, sinfoStatus);
        lsInfoCount = oMainCon.getInfoList(sCurrComp, "", sinfoType, sinfoStatus, sCurrPage);
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