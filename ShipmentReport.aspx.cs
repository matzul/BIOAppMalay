using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShipmentReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sShipmentNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String invoiceStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsShipmentHeaderDetails = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }
    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
        lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");
        sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
        sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
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

        lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sShipmentNo = oMainCon.replaceNull(Request.Params.Get("shipmentno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("shipmentstatus"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("datefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("dateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            invoiceStatus = oMainCon.replaceNull(Request.Params.Get("invoicestatus"));
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
        }
        if (sAction.Equals("RESET"))
        {
            sShipmentNo = "";
            sBPID = "";
            sStatus = "";
            sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
            sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
            sItemNo = "";
            invoiceStatus = "";
            sOrderNo = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsShipmentHeaderDetails = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, sBPID, sStartDate, sEndDate, sOrderNo, sItemNo, sStatus, invoiceStatus);
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