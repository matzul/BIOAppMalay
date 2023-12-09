using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String shipmentStatus = "";
    public String invoiceStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsOrderHeaderDetails = new ArrayList();

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
        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
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

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("orderdatefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("orderdateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            shipmentStatus = oMainCon.replaceNull(Request.Params.Get("shipmentstatus"));
            invoiceStatus = oMainCon.replaceNull(Request.Params.Get("invoicestatus"));
        }
        else if (sAction.Equals("RESET"))
        {
            sOrderNo = "";
            sBPID = "";
            sStatus = "";
            sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
            sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
            sItemNo = "";
            shipmentStatus = "";
            invoiceStatus = "";

        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsOrderHeaderDetails = oMainCon.getOrderHeaderDetailList(sCurrComp, sOrderNo, sBPID, sStartDate, sEndDate, sItemNo, sStatus, shipmentStatus, invoiceStatus);
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

    [WebMethod]
    public static String getShipmentNo(String comp, String orderno, String itemno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        String shipmentno = "";
        ArrayList lsShipment = oMainCon.getShipmentDetailsList(comp, "", 0, orderno, 0, itemno);
        for(int i=0; i<lsShipment.Count; i++)
        {
            MainModel modShipmentDet = (MainModel)lsShipment[i];
            if (i.Equals(0))
            {
                shipmentno = shipmentno + modShipmentDet.GetSetshipmentno;
            }
            else
            {
                shipmentno = shipmentno + ", " + modShipmentDet.GetSetshipmentno;
            }
        }
        object objData = new { result = shipmentno };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod]
    public static String getInvoiceNo(String comp, String orderno, String itemno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        String invoiceno = "";
        ArrayList lsInvoice = oMainCon.getInvoiceDetailsList(comp, "", 0, "", 0, orderno, 0, itemno);
        for (int i = 0; i < lsInvoice.Count; i++)
        {
            MainModel modInvoiceDet = (MainModel)lsInvoice[i];
            if (i.Equals(0))
            {
                invoiceno = invoiceno + modInvoiceDet.GetSetinvoiceno;
            }
            else
            {
                invoiceno = invoiceno + ", " + modInvoiceDet.GetSetinvoiceno;
            }
        }
        object objData = new { result = invoiceno };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}