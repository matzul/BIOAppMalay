using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssetImageUpload : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sAssetNo = "";
    public String sAlertMessage = "";
    public MainModel oModAsset = new MainModel();
    public ArrayList lsAssetImage = new ArrayList();

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
        if (Request.QueryString["assetno"] != null)
        {
            sAssetNo = Request.QueryString["assetno"].ToString();
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
            sActionString = "";
            oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            lsAssetImage = oMainCon.getBLOBFileAsset(sCurrComp, sAssetNo, Server.MapPath("./Attachment/"), "");
        }
    }

}