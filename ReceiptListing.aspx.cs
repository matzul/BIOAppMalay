using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReceiptListing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sReceiptNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsReceiptHeader = new ArrayList();

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

        if (sAction.Equals("SEARCH"))
        {
            sReceiptNo = oMainCon.replaceNull(Request.Params.Get("receiptno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
        }
        if (sAction.Equals("RESET"))
        {
            sReceiptNo = "";
            sBPID = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsReceiptHeader = oMainCon.getReceiptHeaderList(sCurrComp, sReceiptNo, sBPID, sStartDate, sEndDate, sStatus);
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

    [WebMethod(EnableSession = true)]
    public static String getFBZM_PendingReceiptList(String comp, String bpid, String receiveno, String ordercat)
    {
        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();

        String jsonResponse = "";
        int countpendreceipt = 0;
        ArrayList lsPendReceipt = new ArrayList();

        object objPendReceiptList = new { countpendreceipt = countpendreceipt, arraypendreceiptno = new { } };

        ArrayList lsPendReceiptList = oMainCon.getLineItemPendingReceipt(comp, bpid, "", receiveno, ordercat);
        if (lsPendReceiptList.Count > 0)
        {
            countpendreceipt = lsPendReceiptList.Count;
            objPendReceiptList = new { countpendreceipt = countpendreceipt, arraypendreceiptno = lsPendReceiptList };
        }
        jsonResponse = new JavaScriptSerializer().Serialize(objPendReceiptList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }


    [WebMethod(EnableSession = true)]
    public static String createFBZM_ReceiptOrder(String comp, String receiveno, String userid, String ordercat)
    {
        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();

        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get order header if exist
        MainModel oModOrder = oMainCon.getPurchaseOrderHeaderDetails(comp, receiveno, ordercat);
        if (oModOrder.GetSetorderno.Trim().Length > 0)
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetreceiptdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModHeader.GetSetreceiptcat = oModOrder.GetSetordercat;
            String sReceiptNo = oMainCon.getNextRunningNo(comp, "RECEIPT", "ACTIVE");
            oModHeader.GetSetreceiptno = sReceiptNo;
            oModHeader.GetSetbpid = oModOrder.GetSetbpid;
            oModHeader.GetSetbpdesc = oModOrder.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModOrder.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModOrder.GetSetbpcontact;
            oModHeader.GetSetremarks = oModOrder.GetSetordertype;
            oModHeader.GetSetstatus = "NEW";
            oModHeader.GetSetcreatedby = userid;

            if (oMainCon.insertReceiptHeader(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "RECEIPT", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetreceiptno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static String addFBZM_ReceiptItemOrder(String comp, String receiptno, int receiptlineno, String receiveno, int lineno, String itemno, int qty, String location, String ordercat)
    {
        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();

        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        MainModel oModItem = oMainCon.getPurchaseOrderDetailsDetails(comp, receiveno, lineno, itemno, ordercat, receiptno);

        if (oModItem.GetSetorderno.Trim().Length > 0)
        {
            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetreceiptno = receiptno;
            oModLineItem.GetSetlineno = receiptlineno;
            oModLineItem.GetSetorderno = receiveno;
            oModLineItem.GetSetorder_lineno = lineno;
            oModLineItem.GetSetitemno = oModItem.GetSetitemno;
            oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
            oModLineItem.GetSetorder_quantity = oModItem.GetSetorder_quantity;
            oModLineItem.GetSetreceipt_quantity = qty;
            oModLineItem.GetSetlocation = location;
            oModLineItem.GetSetremarks = "";
            oModLineItem.GetSethasbilling = "N";

            if (oMainCon.insertReceiptDetails(oModLineItem).Equals("Y"))
            {
                sStatus = "Y";
                sMessage = "";
            }
        }
        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

}