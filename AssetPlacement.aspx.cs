using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssetPlacement : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sAssetNo = "";
    public String sAssetDesc = "";
    public String sAssetCat = "";
    public String sAssetType = "";
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
            sAssetNo = oMainCon.replaceNull(Request.Params.Get("assetno"));
            sAssetDesc = oMainCon.replaceNull(Request.Params.Get("assetdesc"));
            //sAssetCat = oMainCon.replaceNull(Request.Params.Get("assetcat"));
            //sAssetType = oMainCon.replaceNull(Request.Params.Get("assettype"));
        }
        if (sAction.Equals("RESET"))
        {
            sAssetNo = "";
            sAssetDesc = "";
            sAssetCat = "";
            sAssetType = "";
        }
        if (sAction.Equals("NEXT_PAGE"))
        {
            sAssetNo = oMainCon.replaceNull(Request.Params.Get("assetno"));
            sAssetDesc = oMainCon.replaceNull(Request.Params.Get("assetdesc"));
            //sAssetCat = oMainCon.replaceNull(Request.Params.Get("assetcat"));
            //sAssetType = oMainCon.replaceNull(Request.Params.Get("assettype"));
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
            lsAssetCount = oMainCon.getAssetListPlacement(sCurrComp, sAssetNo, sAssetDesc, sAssetCat, sAssetType, "", "");
            lsAsset = oMainCon.getAssetListPlacement(sCurrComp, sAssetNo, sAssetDesc, sAssetCat, sAssetType, "", "", sCurrPage);
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