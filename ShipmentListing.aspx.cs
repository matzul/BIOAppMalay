using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShipmentListing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sShipmentNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsShipmentHeader = new ArrayList();

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
            sShipmentNo = oMainCon.replaceNull(Request.Params.Get("shipmentno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
        }
        if (sAction.Equals("RESET"))
        {
            sShipmentNo = "";
            sBPID = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsShipmentHeader = oMainCon.getShipmentHeaderList(sCurrComp, sShipmentNo, sBPID, sStartDate, sEndDate, sStatus);
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
    public static String getFBZM_PendingShipmentList(String comp, String bpid, String giveno, String ordercat)
    {

        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();

        String jsonResponse = "";
        int countpendshipment = 0;
        ArrayList lsPendShipment = new ArrayList();

        object objPendShipmentList = new { countpendshipment = countpendshipment, arraypendshipmentno = new { } };

        ArrayList lsPendShipmentList = oMainCon.getLineItemPendingShipment(comp, bpid, "", giveno, ordercat);
        if (lsPendShipmentList.Count > 0)
        {
            countpendshipment = lsPendShipmentList.Count;
            objPendShipmentList = new { countpendshipment = countpendshipment, arraypendshipmentno = lsPendShipmentList };
        }
        jsonResponse = new JavaScriptSerializer().Serialize(objPendShipmentList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static String createFBZM_ShipmentOrder(String comp, String giveno, String userid, String ordercat)
    {
        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();

        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get order header if exist
        MainModel oModOrder = oMainCon.getOrderHeaderDetails(comp, giveno, ordercat);
        if (oModOrder.GetSetorderno.Trim().Length > 0)
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetshipmentdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModHeader.GetSetshipmentcat = oModOrder.GetSetordercat;
            String sShipmentNo = oMainCon.getNextRunningNo(comp, "SHIPMENT", "ACTIVE");
            oModHeader.GetSetshipmentno = sShipmentNo;
            oModHeader.GetSetbpid = oModOrder.GetSetbpid;
            oModHeader.GetSetbpdesc = oModOrder.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModOrder.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModOrder.GetSetbpcontact;
            oModHeader.GetSetremarks = oModOrder.GetSetordertype;
            oModHeader.GetSetstatus = "NEW";
            oModHeader.GetSetcreatedby = userid;

            if (oMainCon.insertShipmentHeader(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "SHIPMENT", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetshipmentno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static String addFBZM_ShipmentItemOrder(String comp, String shipmentno, int shipmentlineno, String giveno, int lineno, String itemno, int qty, String location, String datesoh, int qtyavailable, String ordercat)
    {
        HttpContext.Current.Response.ContentType = "text/json";

        MainController oMainCon = new MainController();
        
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        MainModel oModItem = oMainCon.getOrderDetailsDetails(comp, giveno, lineno, itemno, ordercat, shipmentno);

        if (oModItem.GetSetorderno.Trim().Length > 0)
        {
            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetshipmentno = shipmentno;
            oModLineItem.GetSetlineno = shipmentlineno;
            oModLineItem.GetSetorderno = giveno;
            oModLineItem.GetSetorder_lineno = lineno;
            oModLineItem.GetSetitemno = oModItem.GetSetitemno;
            oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
            oModLineItem.GetSetorder_quantity = oModItem.GetSetorder_quantity;
            oModLineItem.GetSetshipment_quantity = qty;
            oModLineItem.GetSetlocation = location;
            oModLineItem.GetSetdatesoh = datesoh;
            oModLineItem.GetSetqtysoh = 0;
            oModLineItem.GetSetqtyavailable = qtyavailable;
            oModLineItem.GetSetremarks = "";
            oModLineItem.GetSethasinvoice = "N";

            //insert new line item
            if (oMainCon.insertShipmentDetails(oModLineItem).Equals("Y"))
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