using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockStateSOH : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sStockStateNo = "";
    public String sAlertMessage = "";
    public MainModel oModStockState = new MainModel();
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
        if (Request.QueryString["stockstateno"] != null)
        {
            sStockStateNo = Request.QueryString["stockstateno"].ToString();
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
        if (Request.Params.Get("hidStockStateNo") != null)
        {
            sStockStateNo = oMainCon.replaceNull(Request.Params.Get("hidStockStateNo"));
        }

    }

    private void processValues()
    {
        if (sAction.Equals("OPEN"))
        {
            oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");
            sActionString = "LIST OF CLOSING STOCK AS AT " + oModStockState.GetSetclosingdate;
        }
        //get list item stock
        if (sCurrComp.Trim().Length > 0) 
        {
            if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
            {
                lsStockListing = oMainCon.getItemStockList(sCurrComp, "", "", "", false);
            }
            else
            {
                lsStockListing = oMainCon.getStockStateSOHList(sCurrComp, sStockStateNo, "", "", "");
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
}