using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssetDepreciation : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sTranNo = "";
    public String sTranCode = "DEPCOST";
    public String sTranCat = "";
    public String sStatus = "";
    public String sCurrPage = "";
    public ArrayList lsAsset = new ArrayList();
    public ArrayList lsAssetCount = new ArrayList();

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
            sTranNo = oMainCon.replaceNull(Request.Params.Get("tranno"));
            sTranCode = oMainCon.replaceNull(Request.Params.Get("trancode"));
        }
        if (sAction.Equals("RESET"))
        {
            sTranNo = "";
            sTranCode = "DEPCOST";
        }
        if (sAction.Equals("NEXT_PAGE"))
        {
            sTranNo = oMainCon.replaceNull(Request.Params.Get("tranno"));
            sTranCode = oMainCon.replaceNull(Request.Params.Get("trancode"));
            sCurrPage = oMainCon.replaceNull(Request.Params.Get("hidNextPage"));
        }
    }

    private void processValues()
    {
        if(sCurrPage.Trim().Length == 0)
        {
            sCurrPage = "1";
        }
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsAssetCount = oMainCon.getAssetTranHeaderList(sCurrComp, sTranNo, sTranCode, sTranCat, sStatus);
            lsAsset = oMainCon.getAssetTranHeaderList(sCurrComp, sTranNo, sTranCode, sTranCat, sStatus, sCurrPage);
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

}