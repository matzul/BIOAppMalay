using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoiceDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sInvoiceNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModInvoice = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsOtherBP = new ArrayList();
    public ArrayList lsPendInvMod = new ArrayList();
    public ArrayList lsInvoiceLineItem = new ArrayList();
    public ArrayList lsInvoiceType = new ArrayList();
    public ArrayList lsTax = new ArrayList();
    public String selected_bp = "";

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
        if (Request.QueryString["invoiceno"] != null)
        {
            sInvoiceNo = Request.QueryString["invoiceno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        //lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsOtherBP = oMainCon.getOtherBPList(sCurrComp, "", "", "");

        if (sAction.Equals("ADD"))
        {
            sInvoiceNo = "";
            oModInvoice = new MainModel();
            oModInvoice.GetSetinvoicedate = DateTime.Now.ToString("dd-MM-yyyy");
            oModInvoice.GetSetstatus = "NEW";
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
        if (Request.Params.Get("hidInvoiceNo") != null)
        {
            sInvoiceNo = oMainCon.replaceNull(Request.Params.Get("hidInvoiceNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        //lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsOtherBP = oMainCon.getOtherBPList(sCurrComp, "", "", "");

        //for reset
        if (sAction.Equals("ADD"))
        {
            sInvoiceNo = "";
            oModInvoice = new MainModel();
            oModInvoice.GetSetinvoicedate = DateTime.Now.ToString("dd-MM-yyyy");
            oModInvoice.GetSetstatus = "NEW";
            lsInvoiceLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModInvoice = new MainModel();
            oModInvoice.GetSetcomp = sCurrComp;
            oModInvoice.GetSetinvoicedate = oMainCon.replaceNull(Request.Params.Get("invoicedate"));
            oModInvoice.GetSetinvoicecat = oMainCon.replaceNull(Request.Params.Get("invoicecat"));
            oModInvoice.GetSetinvoicetype = oMainCon.replaceNull(Request.Params.Get("invoicetype"));
            oModInvoice.GetSetinvoiceterm = oMainCon.replaceNull(Request.Params.Get("invoiceterm"));
            sInvoiceNo = oMainCon.getNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
            oModInvoice.GetSetinvoiceno = sInvoiceNo;
            oModInvoice.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModInvoice.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModInvoice.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModInvoice.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModInvoice.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModInvoice.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModInvoice.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
            oModInvoice.GetSetinvoicedate = oMainCon.replaceNull(Request.Params.Get("invoicedate"));
            oModInvoice.GetSetinvoicecat = oMainCon.replaceNull(Request.Params.Get("invoicecat"));
            oModInvoice.GetSetinvoicetype = oMainCon.replaceNull(Request.Params.Get("invoicetype"));
            oModInvoice.GetSetinvoiceterm = oMainCon.replaceNull(Request.Params.Get("invoiceterm"));
            oModInvoice.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModInvoice.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModInvoice.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModInvoice.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModInvoice.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModInvoice.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModInvoice.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetinvoiceno = sInvoiceNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetshipmentno = oMainCon.replaceNull(Request.Params.Get("hidShipmentNo"));
            oModLineItem.GetSetshipment_lineno = oMainCon.replaceIntZero(Request.Params.Get("hidShipmentLineNo"));
            oModLineItem.GetSetorderno = oMainCon.replaceNull(Request.Params.Get("hidOrderNo"));
            oModLineItem.GetSetorder_lineno = oMainCon.replaceIntZero(Request.Params.Get("hidOrderLineNo"));
            oModLineItem.GetSetunitcost = oMainCon.replaceDoubleZero(Request.Params.Get("hidUnitCost"));
            oModLineItem.GetSetcostprice = oMainCon.replaceDoubleZero(Request.Params.Get("hidCostPrice"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("hidItemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("addunitprice"));
            oModLineItem.GetSetdiscamount = oMainCon.replaceDoubleZero(Request.Params.Get("adddiscamount"));
            oModLineItem.GetSetquantity = oMainCon.replaceIntZero(Request.Params.Get("addquantity"));
            oModLineItem.GetSetinvoiceprice = oMainCon.replaceDoubleZero(Request.Params.Get("addinvoiceprice"));
            oModLineItem.GetSettaxcode = oMainCon.replaceNull(Request.Params.Get("addtaxcode"));
            oModLineItem.GetSettaxrate = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxrate"));
            oModLineItem.GetSettaxamount = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxamount"));
            oModLineItem.GetSettotalinvoice = oMainCon.replaceDoubleZero(Request.Params.Get("addtotalprice"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetinvoiceno = sInvoiceNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR INVOIS";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sInvoiceNo.Length > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //insert new Order
                if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
                    lsInvoiceLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Invois berjaya...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Invois tidak berjaya...";
                    sInvoiceNo = "";
                    oModInvoice.GetSetinvoiceno = sInvoiceNo;
                    oModInvoice.GetSetbpid = "";
                    sAction = "ADD";
                    sActionString = "ADD NEW INVOICE";
                    lsInvoiceLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Invois tidak berjaya...";
                sInvoiceNo = "";
                oModInvoice.GetSetinvoiceno = sInvoiceNo;
                oModInvoice.GetSetbpid = "";
                sAction = "ADD";
                sActionString = "DAFTAR INVOIS";
                lsInvoiceLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT INVOIS";
            if (sInvoiceNo.Length > 0)
            {
                oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
                lsInvoiceLineItem = oMainCon.getInvoiceDetailsList(sCurrComp, sInvoiceNo, 0, "");
                lsPendInvMod = oMainCon.getLineItemPendingInvoice(sCurrComp, oModInvoice.GetSetbpid, "", oModInvoice.GetSetinvoicecat, oModInvoice.GetSetinvoicetype, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Invois...";
                oModInvoice = new MainModel();
                lsInvoiceLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI INVOIS";
            if (sInvoiceNo.Length > 0)
            {
                oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
                lsInvoiceLineItem = oMainCon.getInvoiceDetailsList(sCurrComp, sInvoiceNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Invois...";
                oModInvoice = new MainModel();
                lsInvoiceLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sInvoiceNo.Length > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //update Order
                if (oMainCon.updateInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Invois berjaya disimpan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Invois tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI INVOIS";
                    oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
                    lsInvoiceLineItem = oMainCon.getInvoiceDetailsList(sCurrComp, sInvoiceNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Invois tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI INVOIS";
                oModInvoice = new MainModel();
                lsInvoiceLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sInvoiceNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item or not
                MainModel modExistent = oMainCon.getInvoiceDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetshipmentno, oModLineItem.GetSetshipment_lineno, "", "NEW");
                if (modExistent.GetSetinvoiceno.Length > 0 && (oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetinvoicecat.Equals("SALES_INVOICE") || oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetinvoicecat.Equals("TRANSFER_INVOICE")))
                {
                    sAlertMessage = "ERROR|Item Invois tidak berjaya ditambah. Item tersebut telah ditambah pada Invois: " + modExistent.GetSetinvoiceno;
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertInvoiceDetails(oModLineItem).Equals("Y"))
                    {
                        //update order header information
                        String result = oMainCon.updateInvoiceHeaderInfo(sCurrComp, sInvoiceNo);
                        sAlertMessage = "SUCCESS|Item Invois berjaya ditambah...";
                        Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Invois tidak berjaya ditambah...";
                        Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Invois tidak berjaya ditambah...";
                Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sInvoiceNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deleteInvoiceDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getInvoiceDetailsList(sCurrComp, sInvoiceNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteInvoiceDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertInvoiceDetails(oLineItem);
                    }
                    //update invoice header information
                    String result = oMainCon.updateInvoiceHeaderInfo(sCurrComp, sInvoiceNo);
                    sAlertMessage = "SUCCESS|Item Invois berjaya dikeluarkan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Invois tidak berjaya dikeluarkan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Invois tidak berjaya dikeluarkan...";
                Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sInvoiceNo.Length > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //update invoice header - CONFIRM
                oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
                oModInvoice.GetSetstatus = "CONFIRMED";
                oModInvoice.GetSetconfirmedby = sUserId;
                if (oMainCon.updateInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    //update Other BP if required
                    oMainCon.updateListOtherBP(sCurrComp, oModInvoice.GetSetbpid, oModInvoice.GetSetbpdesc, oModInvoice.GetSetbpaddress, oModInvoice.GetSetbpcontact);

                    if (oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE"))
                    {
                        ArrayList lsInvLineItem = oMainCon.getInvoiceDetailsList(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno, 0, "");
                        for (int i = 0; i < lsInvLineItem.Count; i++)
                        {
                            MainModel modInvDet = (MainModel)lsInvLineItem[i];

                            //to update Sales Order Invoice Amount
                            MainModel oModOrder = oMainCon.getOrderDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetorderno, modInvDet.GetSetorder_lineno, "");
                            oModOrder.GetSetinvoiceamount = oModOrder.GetSetinvoiceamount + modInvDet.GetSettotalinvoice;
                            String result = oMainCon.updateOrderDetails(oModOrder);

                            //update status for shipment has invoice
                            MainModel oModShipment = oMainCon.getShipmentDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetshipmentno, modInvDet.GetSetshipment_lineno, "");
                            oModShipment.GetSethasinvoice = "Y";
                            result = oMainCon.updateShipmentDetails(oModShipment);
                        }
                    }
                    else if (oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE"))
                    {
                        ArrayList lsInvLineItem = oMainCon.getInvoiceDetailsList(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno, 0, "");
                        for (int i = 0; i < lsInvLineItem.Count; i++)
                        {
                            MainModel modInvDet = (MainModel)lsInvLineItem[i];

                            //to update Sales Order Invoice Amount
                            MainModel oModOrder = oMainCon.getTransferOrderDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetorderno, modInvDet.GetSetorder_lineno, "");
                            oModOrder.GetSetinvoiceamount = oModOrder.GetSetinvoiceamount + modInvDet.GetSettotalinvoice;
                            String result = oMainCon.updateTransferOrderDetails(oModOrder);

                            //update status for shipment has invoice
                            MainModel oModShipment = oMainCon.getShipmentDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetshipmentno, modInvDet.GetSetshipment_lineno, "");
                            oModShipment.GetSethasinvoice = "Y";
                            result = oMainCon.updateShipmentDetails(oModShipment);
                        }

                    }
                    sAlertMessage = "SUCCESS|Maklumat Invois berjaya disahkan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Invois tidak berjaya disahkan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Invois tidak berjaya disahkan...";
                Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sInvoiceNo.Length > 0 && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CONFIRMED" && oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo).GetSetstatus != "CANCELLED")
            {
                //update invoice header - CANCEL
                oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, sInvoiceNo);
                oModInvoice.GetSetstatus = "CANCELLED";
                oModInvoice.GetSetcancelledby = sUserId;
                if (oMainCon.updateInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Invois berjaya dibatalkankan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Invois tidak berjaya dibatalkankan...";
                    Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Invois tidak berjaya dibatalkankan...";
                Response.Redirect("InvoiceDetails.aspx?action=OPEN&comp=" + sCurrComp + "&invoiceno=" + sInvoiceNo + "&alertmessage=" + sAlertMessage);
            }
        }
        lsTax = oMainCon.getTaxList(sCurrComp);
        lsInvoiceType = oMainCon.getParametertype("'CAP-INCOME','INCOME'", "ACTIVE");

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