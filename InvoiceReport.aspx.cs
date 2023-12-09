using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoiceReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sShipmentNo = "";
    public String sInvoiceNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String paymentStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsInvoiceHeaderDetails = new ArrayList();

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
        lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");
        sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
        sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
    }
    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
            sInvoiceNo = oMainCon.replaceNull(Request.Params.Get("invoiceno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("invoicestatus"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("datefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("dateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            paymentStatus = oMainCon.replaceNull(Request.Params.Get("paymentstatus"));
            sShipmentNo = oMainCon.replaceNull(Request.Params.Get("shipmentno"));
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
        }
        if (sAction.Equals("RESET"))
        {
            sOrderNo = "";
            sShipmentNo = "";
            sInvoiceNo = "";
            sBPID = "";
            sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
            sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
            sItemNo = "";
            sStatus = "";
            paymentStatus = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsInvoiceHeaderDetails = oMainCon.getInvoiceHeaderDetailsList(sCurrComp, sInvoiceNo, sBPID, sStartDate, sEndDate, sShipmentNo, sOrderNo, sItemNo, sStatus, paymentStatus);
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