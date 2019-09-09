using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockOnHandDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sItemNo = "";
    public String sAlertMessage = "";
    public MainModel oModItem = new MainModel();
    public ArrayList lsStockListing = new ArrayList();

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
        if (Request.QueryString["itemno"] != null)
        {
            sItemNo = Request.QueryString["itemno"].ToString();
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
        if (Request.Params.Get("hidItemNo") != null)
        {
            sItemNo = oMainCon.replaceNull(Request.Params.Get("hidItemNo"));
        }

    }

    private void processValues()
    {
        if (sAction.Equals("OPEN"))
        {
            sActionString = "OPEN ITEM STOCK";
            if (sItemNo.Length > 0)
            {
                oModItem = oMainCon.getItemStockSummary2(sCurrComp, sItemNo);
                lsStockListing = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Unable to open Item Stock...";
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsStockListing = new ArrayList();
            }
        }
        //get list item stock
        if (sCurrComp.Trim().Length > 0 && sItemNo.Trim().Length > 0)
        {
            lsStockListing = oMainCon.getItemStockList(sCurrComp, sItemNo, "", "", false);
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