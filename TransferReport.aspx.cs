using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransferReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sCompFrom = "";
    public String sCompTo = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String shipmentStatus = "";
    public String invoiceStatus = "";
    public String receiptStatus = "";
    public String expensesStatus = "";
    public ArrayList lsComp = new ArrayList();
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
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");
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

        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
            sCompFrom = oMainCon.replaceNull(Request.Params.Get("compfrom"));
            sCompTo = oMainCon.replaceNull(Request.Params.Get("compto"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("orderdatefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("orderdateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            shipmentStatus = oMainCon.replaceNull(Request.Params.Get("shipmentstatus"));
            invoiceStatus = oMainCon.replaceNull(Request.Params.Get("invoicestatus"));
            receiptStatus = oMainCon.replaceNull(Request.Params.Get("receiptstatus"));
            expensesStatus = oMainCon.replaceNull(Request.Params.Get("expensesstatus"));
        }
        if (sAction.Equals("RESET"))
        {
            sOrderNo = "";
            sCompFrom = "";
            sCompTo = "";
            sStatus = "";
            sStartDate = "";
            sEndDate = "";
            sItemNo = "";
            shipmentStatus = "";
            invoiceStatus = "";
            receiptStatus = "";
            expensesStatus = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsOrderHeaderDetails = oMainCon.getTransferOrderHeaderDetailsList("", sOrderNo, sCompFrom, sCompTo, sStartDate, sEndDate, sItemNo, sStatus, shipmentStatus, invoiceStatus, receiptStatus, expensesStatus);
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
        for (int i = 0; i < lsShipment.Count; i++)
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

    [WebMethod]
    public static String getReceiptNo(String comp, String orderno, String itemno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        String receiptno = "";
        ArrayList lsReceipt = oMainCon.getReceiptDetailsList(comp, "", 0, orderno, 0, itemno);
        for (int i = 0; i < lsReceipt.Count; i++)
        {
            MainModel modReceiptDet = (MainModel)lsReceipt[i];
            if (i.Equals(0))
            {
                receiptno = receiptno + modReceiptDet.GetSetreceiptno;
            }
            else
            {
                receiptno = receiptno + ", " + modReceiptDet.GetSetreceiptno;
            }
        }
        object objData = new { result = receiptno };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

    [WebMethod]
    public static String getExpensesNo(String comp, String orderno, String itemno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        String expensesno = "";
        ArrayList lsExpenses = oMainCon.getExpensesDetailsList(comp, "", 0, "", 0, orderno, 0, itemno);
        for (int i = 0; i < lsExpenses.Count; i++)
        {
            MainModel modExpDet = (MainModel)lsExpenses[i];
            if (i.Equals(0))
            {
                expensesno = expensesno + modExpDet.GetSetexpensesno;
            }
            else
            {
                expensesno = expensesno + ", " + modExpDet.GetSetexpensesno;
            }
        }
        object objData = new { result = expensesno };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}