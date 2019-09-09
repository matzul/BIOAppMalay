using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReceiveDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sOrderNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModOrder = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsItemDiscount = new ArrayList();
    public ArrayList lsTax = new ArrayList();
    public ArrayList lsOrderLineItem = new ArrayList();

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
        if (Request.QueryString["orderno"] != null)
        {
            sOrderNo = Request.QueryString["orderno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        
        lsBP = oMainCon.getBPList(sCurrComp, "", "", "", "Y");

        if (sAction.Equals("ADD"))
        {
            sOrderNo = "";
            oModOrder = new MainModel();
            oModOrder.GetSetorderdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModOrder.GetSetorderstatus = "NEW";
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
        if (Request.Params.Get("hidOrderNo") != null)
        {
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("hidOrderNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "", "Y");

        //for reset
        if (sAction.Equals("ADD"))
        {
            sOrderNo = "";
            oModOrder = new MainModel();
            oModOrder.GetSetorderdate = DateTime.Now.ToShortDateString();
            oModOrder.GetSetorderstatus = "NEW";
            lsOrderLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModOrder = new MainModel();
            oModOrder.GetSetcomp = sCurrComp;
            oModOrder.GetSetorderdate = oMainCon.replaceNull(Request.Params.Get("orderdate"));
            oModOrder.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("ordercat"));
            sOrderNo = oMainCon.getNextRunningNo(sCurrComp, oModOrder.GetSetordercat, "ACTIVE");
            oModOrder.GetSetorderno = sOrderNo;
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("paytype"));
            oModOrder.GetSetplandeliverydate = oMainCon.replaceNull(Request.Params.Get("plandate"));
            oModOrder.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModOrder.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModOrder.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModOrder.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModOrder.GetSetorderremarks = oMainCon.replaceNull(Request.Params.Get("orderremarks"));
            oModOrder.GetSetorderstatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            oModOrder.GetSetordercreated = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp,sOrderNo);
            oModOrder.GetSetorderdate = oMainCon.replaceNull(Request.Params.Get("orderdate"));
            oModOrder.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("ordercat"));
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("paytype"));
            oModOrder.GetSetplandeliverydate = oMainCon.replaceNull(Request.Params.Get("plandate"));
            oModOrder.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModOrder.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModOrder.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModOrder.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModOrder.GetSetorderremarks = oMainCon.replaceNull(Request.Params.Get("orderremarks"));
            oModOrder.GetSetorderstatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            oModOrder.GetSetordercreated = sUserId;
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetorderno = sOrderNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("addunitprice"));
            oModLineItem.GetSetdiscamount = oMainCon.replaceDoubleZero(Request.Params.Get("adddiscamount"));
            oModLineItem.GetSetquantity = oMainCon.replaceIntZero(Request.Params.Get("addquantity"));
            oModLineItem.GetSetorderprice = oMainCon.replaceDoubleZero(Request.Params.Get("addorderprice"));
            oModLineItem.GetSettaxcode = oMainCon.replaceNull(Request.Params.Get("addtaxcode"));
            oModLineItem.GetSettaxrate = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxrate"));
            oModLineItem.GetSettaxamount = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxamount"));
            oModLineItem.GetSettotalprice = oMainCon.replaceDoubleZero(Request.Params.Get("addtotalprice"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetorderno = sOrderNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PESANAN TERIMAAN";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sOrderNo.Length > 0)
            {
                //insert new Purchase Order
                if (oMainCon.insertPurchaseOrderHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, oModOrder.GetSetordercat, "ACTIVE");
                    lsOrderLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Pesanan Terimaan berjaya...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Pesanan Terimaan tidak berjaya...";
                    sOrderNo = "";
                    oModOrder.GetSetorderno = sOrderNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR PESANAN TERIMAAN";
                    lsOrderLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Pesanan Terimaan tidak berjaya...";
                sOrderNo = "";
                oModOrder.GetSetorderno = sOrderNo;
                sAction = "ADD";
                sActionString = "DAFTAR PESANAN TERIMAAN";
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PESANAN TERIMAAN";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
                lsOrderLineItem = oMainCon.getPurchaseOrderDetailsList(sCurrComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Pesanan Terimaan...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PESANAN TERIMAAN";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
                lsOrderLineItem = oMainCon.getPurchaseOrderDetailsList(sCurrComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Pesanan Terimaan...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sOrderNo.Length > 0)
            {
                //update Order
                if (oMainCon.updatePurchaseOrderHeader(oModOrder).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Pesanan Terimaan berjaya disimpan...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PESANAN TERIMAAN";
                    oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
                    lsOrderLineItem = oMainCon.getPurchaseOrderDetailsList(sCurrComp, sOrderNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PESANAN TERIMAAN";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //check whether already exist in Purchase Order Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPurchaseOrderDetailsDetails(oModLineItem.GetSetcomp, oModLineItem.GetSetorderno, 0, oModLineItem.GetSetitemno);
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Terimaan tidak berjaya ditambah. Item tersebut telah ditambah pada Pesanan Terimaan [Line No: " + modExistent.GetSetlineno + "]";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPurchaseOrderDetails(oModLineItem).Equals("Y"))
                    {
                        //update purchase order header information
                        String result = oMainCon.updatePurchaseOrderHeaderInfo(sCurrComp, sOrderNo);
                        sAlertMessage = "SUCCESS|Item Terimaan berjaya ditambah...";
                        Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Terimaan tidak berjaya ditambah...";
                        Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Terimaan tidak berjaya ditambah...";
                Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.updatePurchaseOrderDetails(oModLineItem).Equals("Y"))
                {
                    //update order header information
                    String result = oMainCon.updatePurchaseOrderHeaderInfo(sCurrComp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Terimaan berjaya dikemaskini...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Terimaan tidak berjaya dikemaskini...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Terimaan tidak berjaya dikemaskini...";
                Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.deletePurchaseOrderDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getPurchaseOrderDetailsList(sCurrComp, sOrderNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deletePurchaseOrderDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertPurchaseOrderDetails(oLineItem);
                    }
                    //update purchase order header information
                    String result = oMainCon.updatePurchaseOrderHeaderInfo(sCurrComp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Terimaan berjaya dikeluarkan...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Terimaan tidak berjaya dikeluarkan...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Terimaan tidak berjaya dikeluarkan...";
                Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sOrderNo.Length > 0 && oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo).GetSetorderstatus != "CONFIRMED" && oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo).GetSetorderstatus != "CANCELLED")
            {
                //update purchase order header - CONFIRM
                oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
                oModOrder.GetSetorderstatus = "CONFIRMED";
                oModOrder.GetSetorderapproved = sUserId;

                Boolean proceedConfirmOrder = true;
                String stockLocation = "STORE";
                sAlertMessage = "";

                if (oModOrder.GetSetorderactivity.Equals("ACTIVITY00"))
                {
                    //RECEIPT - MANUAL & BILLING - MANUAL
                    //do nothing
                    proceedConfirmOrder = true;
                }

                if (oModOrder.GetSetorderactivity.Equals("ACTIVITY10"))
                {
                    //RECEIPT - AUTO & BILLING - MANUAL
                    /*
                    if (stockLocation.Trim().Length > 0)
                    {
                        //proceed create shipment automatically
                        //insert data to shipment table
                        MainModel oModShipment = new MainModel();
                        oModShipment.GetSetcomp = sCurrComp;
                        oModShipment.GetSetshipmentno = oMainCon.getNextRunningNo(sCurrComp, "SHIPMENT", "ACTIVE");
                        //let system define the shipment date
                        //oModShipment.GetSetshipmentdate = oModOrder.GetSetorderapproveddate;
                        oModShipment.GetSetshipmentcat = "NORMAL";
                        oModShipment.GetSetbpid = oModOrder.GetSetbpid;
                        oModShipment.GetSetbpdesc = oModOrder.GetSetbpdesc;
                        oModShipment.GetSetbpaddress = oModOrder.GetSetbpaddress;
                        oModShipment.GetSetbpcontact = oModOrder.GetSetbpcontact;
                        oModShipment.GetSetremarks = "SHIPMENT CREATED BY SYSTEM";
                        oModShipment.GetSetstatus = "CONFIRMED";
                        oModShipment.GetSetcreatedby = sUserId;
                        oModShipment.GetSetconfirmedby = sUserId;

                        if (oMainCon.insertShipmentHeader(oModShipment).Equals("Y"))
                        {
                            //get latest information about Shipment Header ie. Confirmed Date which is needed for storing item stock transactions
                            oModShipment = oMainCon.getShipmentHeaderDetails(sCurrComp, oModShipment.GetSetshipmentno);

                            oMainCon.updateNextRunningNo(oModOrder.GetSetcomp, "SHIPMENT", "ACTIVE");

                            lsOrderLineItem = oMainCon.getOrderDetailsList(oModOrder.GetSetcomp, oModOrder.GetSetorderno, 0, "");
                            int shipment_lino = 1;
                            for (int i = 0; i < lsOrderLineItem.Count; i++)
                            {
                                MainModel oModOrderDet = (MainModel)lsOrderLineItem[i];

                                if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))
                                {
                                    int stockRemaining = oModOrderDet.GetSetquantity;
                                    ArrayList lsStockAvailable = oMainCon.getItemStockList(oModOrderDet.GetSetcomp, oModOrderDet.GetSetitemno, "", "", true);
                                    for (int x = 0; x < lsStockAvailable.Count; x++)
                                    {
                                        MainModel stockAssigned = (MainModel)lsStockAvailable[x];

                                        if (stockRemaining > 0)
                                        {
                                            int stockforthisloop = 0;
                                            if (stockAssigned.GetSetqtyavailable >= stockRemaining)
                                            {
                                                stockforthisloop = stockRemaining;
                                            }
                                            else
                                            {
                                                stockforthisloop = stockAssigned.GetSetqtyavailable;
                                            }

                                            MainModel oModShipmentDet = new MainModel();

                                            oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                                            oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                                            oModShipmentDet.GetSetlineno = shipment_lino;
                                            oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                                            oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                                            oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                                            oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                                            oModShipmentDet.GetSetorder_quantity = stockRemaining;
                                            oModShipmentDet.GetSetshipment_quantity = stockforthisloop;
                                            oModShipmentDet.GetSetlocation = stockAssigned.GetSetlocation;
                                            oModShipmentDet.GetSetdatesoh = stockAssigned.GetSetdatesoh;
                                            oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                                            oModShipmentDet.GetSethasinvoice = "N";

                                            String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                                            shipment_lino = shipment_lino + 1;

                                            stockRemaining = stockRemaining - stockforthisloop;

                                            //to update item stock & stock transaction
                                            MainModel oModLatestItemStock = oMainCon.getItemStockDetails(oModShipmentDet.GetSetcomp, oModShipmentDet.GetSetitemno, oModShipmentDet.GetSetlocation, oModShipmentDet.GetSetdatesoh);
                                            if (oModLatestItemStock.GetSetitemcat.Equals("INVENTORY"))
                                            {
                                                MainModel oModItemStock = new MainModel();
                                                oModItemStock.GetSetcomp = oModShipmentDet.GetSetcomp;
                                                oModItemStock.GetSetitemno = oModShipmentDet.GetSetitemno;
                                                oModItemStock.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                                                oModItemStock.GetSetlocation = oModShipmentDet.GetSetlocation;
                                                oModItemStock.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                                                oModItemStock.GetSetqtysoh = oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity;
                                                oModItemStock.GetSetcostsoh = (oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh) * (oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity);
                                                if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                                                {
                                                    String result1 = oMainCon.updateItemStock(oModItemStock);
                                                }

                                                MainModel oModItemStockTrans = new MainModel();
                                                oModItemStockTrans.GetSetcomp = oModShipmentDet.GetSetcomp;
                                                oModItemStockTrans.GetSetitemno = oModShipmentDet.GetSetitemno;
                                                oModItemStockTrans.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                                                oModItemStockTrans.GetSetlocation = oModShipmentDet.GetSetlocation;
                                                oModItemStockTrans.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                                                oModItemStockTrans.GetSettranstype = "SHIPMENT";
                                                oModItemStockTrans.GetSettransdate = oModShipment.GetSetconfirmeddate;
                                                oModItemStockTrans.GetSettransno = oModShipmentDet.GetSetshipmentno;
                                                oModItemStockTrans.GetSettrans_lineno = oModShipmentDet.GetSetlineno;
                                                oModItemStockTrans.GetSetorderno = oModShipmentDet.GetSetorderno;
                                                oModItemStockTrans.GetSetorder_lineno = oModShipmentDet.GetSetorder_lineno;
                                                oModItemStockTrans.GetSettransqty = oModShipmentDet.GetSetshipment_quantity * -1;
                                                oModItemStockTrans.GetSettransprice = oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh;
                                                oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                                                oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                                                String result2 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                                            }

                                        }
                                        else
                                        {
                                            x = lsStockAvailable.Count;
                                        }
                                    }//for (int x = 0; x < lsStockAvailable.Count; x++)
                                }
                                else
                                {
                                    MainModel oModShipmentDet = new MainModel();

                                    oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                                    oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                                    oModShipmentDet.GetSetlineno = shipment_lino;
                                    oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                                    oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                                    oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                                    oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                                    oModShipmentDet.GetSetorder_quantity = oModOrderDet.GetSetquantity;
                                    oModShipmentDet.GetSetshipment_quantity = oModOrderDet.GetSetquantity;
                                    oModShipmentDet.GetSetlocation = "";
                                    oModShipmentDet.GetSetdatesoh = "";
                                    oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                                    oModShipmentDet.GetSethasinvoice = "N";

                                    String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                                    shipment_lino = shipment_lino + 1;

                                }//if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))

                                //update qty delivered in order details
                                oModOrderDet.GetSetdeliverqty = oModOrderDet.GetSetdeliverqty + oModOrderDet.GetSetquantity;
                                String result = oMainCon.updateOrderDetails(oModOrderDet);
                            }
                            proceedConfirmOrder = true;
                        }
                        else
                        {
                            proceedConfirmOrder = false;
                            sAlertMessage = "ERROR|Unable to confirm Order. Shipment can not be processed due Internal Server Error!";
                        }
                    }
                    else
                    {
                        proceedConfirmOrder = false;
                        sAlertMessage = "ERROR|Unable to confirm Order. Shipment can not be processed due to Stock for some Order Items are Not Available!";
                    }//if (stockLocation.Trim().Length > 0)
                    */
                }
                else if (oModOrder.GetSetorderactivity.Equals("ACTIVITY01"))
                {
                    //RECEIPT - MANUAL & BILLING - AUTO
                    //do nothing
                    proceedConfirmOrder = true;
                }
                else if (oModOrder.GetSetorderactivity.Equals("ACTIVITY11"))
                {
                    //RECEIPT - AUTO & BILLING - AUTO
                    /*
                    if (stockLocation.Trim().Length > 0)
                    {
                        //proceed create shipment automatically
                        //insert data to shipment table
                        MainModel oModShipment = new MainModel();
                        oModShipment.GetSetcomp = sCurrComp;
                        oModShipment.GetSetshipmentno = oMainCon.getNextRunningNo(sCurrComp, "SHIPMENT", "ACTIVE");
                        //let system define the shipment date
                        //oModShipment.GetSetshipmentdate = oModOrder.GetSetorderapproveddate;
                        oModShipment.GetSetshipmentcat = "NORMAL";
                        oModShipment.GetSetbpid = oModOrder.GetSetbpid;
                        oModShipment.GetSetbpdesc = oModOrder.GetSetbpdesc;
                        oModShipment.GetSetbpaddress = oModOrder.GetSetbpaddress;
                        oModShipment.GetSetbpcontact = oModOrder.GetSetbpcontact;
                        oModShipment.GetSetremarks = "SHIPMENT CREATED BY SYSTEM";
                        oModShipment.GetSetstatus = "CONFIRMED";
                        oModShipment.GetSetcreatedby = sUserId;
                        oModShipment.GetSetconfirmedby = sUserId;

                        if (oMainCon.insertShipmentHeader(oModShipment).Equals("Y"))
                        {
                            //get latest information about Shipment Header ie. Confirmed Date which is needed for storing item stock transactions
                            oModShipment = oMainCon.getShipmentHeaderDetails(sCurrComp, oModShipment.GetSetshipmentno);

                            oMainCon.updateNextRunningNo(oModOrder.GetSetcomp, "SHIPMENT", "ACTIVE");

                            lsOrderLineItem = oMainCon.getOrderDetailsList(oModOrder.GetSetcomp, oModOrder.GetSetorderno, 0, "");
                            int shipment_lino = 1;
                            for (int i = 0; i < lsOrderLineItem.Count; i++)
                            {
                                MainModel oModOrderDet = (MainModel)lsOrderLineItem[i];

                                if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))
                                {
                                    int stockRemaining = oModOrderDet.GetSetquantity;
                                    ArrayList lsStockAvailable = oMainCon.getItemStockList(oModOrderDet.GetSetcomp, oModOrderDet.GetSetitemno, "", "", true);
                                    for (int x = 0; x < lsStockAvailable.Count; x++)
                                    {
                                        MainModel stockAssigned = (MainModel)lsStockAvailable[x];

                                        if (stockRemaining > 0)
                                        {
                                            int stockforthisloop = 0;
                                            if (stockAssigned.GetSetqtyavailable >= stockRemaining)
                                            {
                                                stockforthisloop = stockRemaining;
                                            }
                                            else
                                            {
                                                stockforthisloop = stockAssigned.GetSetqtyavailable;
                                            }

                                            MainModel oModShipmentDet = new MainModel();

                                            oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                                            oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                                            oModShipmentDet.GetSetlineno = shipment_lino;
                                            oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                                            oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                                            oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                                            oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                                            oModShipmentDet.GetSetorder_quantity = stockRemaining;
                                            oModShipmentDet.GetSetshipment_quantity = stockforthisloop;
                                            oModShipmentDet.GetSetlocation = stockAssigned.GetSetlocation;
                                            oModShipmentDet.GetSetdatesoh = stockAssigned.GetSetdatesoh;
                                            oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                                            oModShipmentDet.GetSethasinvoice = "N";

                                            String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                                            shipment_lino = shipment_lino + 1;

                                            stockRemaining = stockRemaining - stockforthisloop;

                                            //to update item stock & stock transaction
                                            MainModel oModLatestItemStock = oMainCon.getItemStockDetails(oModShipmentDet.GetSetcomp, oModShipmentDet.GetSetitemno, oModShipmentDet.GetSetlocation, oModShipmentDet.GetSetdatesoh);
                                            if (oModLatestItemStock.GetSetitemcat.Equals("INVENTORY"))
                                            {
                                                MainModel oModItemStock = new MainModel();
                                                oModItemStock.GetSetcomp = oModShipmentDet.GetSetcomp;
                                                oModItemStock.GetSetitemno = oModShipmentDet.GetSetitemno;
                                                oModItemStock.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                                                oModItemStock.GetSetlocation = oModShipmentDet.GetSetlocation;
                                                oModItemStock.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                                                oModItemStock.GetSetqtysoh = oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity;
                                                oModItemStock.GetSetcostsoh = (oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh) * (oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity);
                                                if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                                                {
                                                    String result1 = oMainCon.updateItemStock(oModItemStock);
                                                }

                                                MainModel oModItemStockTrans = new MainModel();
                                                oModItemStockTrans.GetSetcomp = oModShipmentDet.GetSetcomp;
                                                oModItemStockTrans.GetSetitemno = oModShipmentDet.GetSetitemno;
                                                oModItemStockTrans.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                                                oModItemStockTrans.GetSetlocation = oModShipmentDet.GetSetlocation;
                                                oModItemStockTrans.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                                                oModItemStockTrans.GetSettranstype = "SHIPMENT";
                                                oModItemStockTrans.GetSettransdate = oModShipment.GetSetconfirmeddate;
                                                oModItemStockTrans.GetSettransno = oModShipmentDet.GetSetshipmentno;
                                                oModItemStockTrans.GetSettrans_lineno = oModShipmentDet.GetSetlineno;
                                                oModItemStockTrans.GetSetorderno = oModShipmentDet.GetSetorderno;
                                                oModItemStockTrans.GetSetorder_lineno = oModShipmentDet.GetSetorder_lineno;
                                                oModItemStockTrans.GetSettransqty = oModShipmentDet.GetSetshipment_quantity * -1;
                                                oModItemStockTrans.GetSettransprice = oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh;
                                                oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                                                oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                                                String result2 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                                            }

                                        }
                                        else
                                        {
                                            x = lsStockAvailable.Count;
                                        }
                                    }//for (int x = 0; x < lsStockAvailable.Count; x++)
                                }
                                else
                                {
                                    MainModel oModShipmentDet = new MainModel();

                                    oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                                    oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                                    oModShipmentDet.GetSetlineno = shipment_lino;
                                    oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                                    oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                                    oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                                    oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                                    oModShipmentDet.GetSetorder_quantity = oModOrderDet.GetSetquantity;
                                    oModShipmentDet.GetSetshipment_quantity = oModOrderDet.GetSetquantity;
                                    oModShipmentDet.GetSetlocation = "";
                                    oModShipmentDet.GetSetdatesoh = "";
                                    oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                                    oModShipmentDet.GetSethasinvoice = "N";

                                    String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                                    shipment_lino = shipment_lino + 1;

                                }//if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))

                                //update qty delivered in order details
                                oModOrderDet.GetSetdeliverqty = oModOrderDet.GetSetdeliverqty + oModOrderDet.GetSetquantity;
                                String result = oMainCon.updateOrderDetails(oModOrderDet);

                            }//for (int i = 0; i < lsOrderLineItem.Count; i++)

                            //get latest information about Shipment Header
                            oModShipment = oMainCon.getShipmentHeaderDetails(sCurrComp, oModShipment.GetSetshipmentno);

                            //proceed create invoice automatically
                            //insert invoice header - CONFIRM
                            MainModel oModInvoice = new MainModel();
                            oModInvoice.GetSetcomp = sCurrComp;
                            //let system define the invoice date
                            //oModInvoice.GetSetinvoicedate = oModShipment.GetSetshipmentdate;
                            oModInvoice.GetSetinvoicetype = "SALES_INVOICE";
                            oModInvoice.GetSetinvoiceterm = "COD";
                            oModInvoice.GetSetinvoiceno = oMainCon.getNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");;
                            oModInvoice.GetSetbpid = oModShipment.GetSetbpid;
                            oModInvoice.GetSetbpdesc = oModShipment.GetSetbpdesc;
                            oModInvoice.GetSetbpaddress = oModShipment.GetSetbpaddress;
                            oModInvoice.GetSetbpcontact = oModShipment.GetSetbpcontact;
                            oModInvoice.GetSetremarks = "INVOICE CREATED BY SYSTEM";
                            oModInvoice.GetSetstatus = "CONFIRMED";
                            oModInvoice.GetSetcreatedby = sUserId;
                            oModInvoice.GetSetconfirmedby = sUserId;

                            if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))
                            {
                                oMainCon.updateNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");

                                //get latest information about Invoice Header
                                oModInvoice = oMainCon.getInvoiceHeaderDetails(sCurrComp, oModInvoice.GetSetinvoiceno);

                                ArrayList lsPendInvLineItem = oMainCon.getLineItemPendingInvoice(oModInvoice.GetSetcomp, oModInvoice.GetSetbpid, "", oModInvoice.GetSetinvoicetype, oModShipment.GetSetshipmentno);

                                for(int y=0; y <lsPendInvLineItem.Count; y++)
                                {
                                    MainModel modPendInv = (MainModel)lsPendInvLineItem[y];

                                    MainModel oModLineItem = new MainModel();
                                    oModLineItem.GetSetcomp = modPendInv.GetSetcomp;
                                    oModLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                                    oModLineItem.GetSetlineno = y+1;
                                    oModLineItem.GetSetshipmentno = modPendInv.GetSetshipmentno;
                                    oModLineItem.GetSetshipment_lineno = modPendInv.GetSetlineno;
                                    oModLineItem.GetSetorderno = modPendInv.GetSetorderno;
                                    oModLineItem.GetSetorder_lineno = modPendInv.GetSetorder_lineno;
                                    oModLineItem.GetSetitemno = modPendInv.GetSetitemno;
                                    oModLineItem.GetSetitemdesc = modPendInv.GetSetitemdesc;

                                    oModLineItem.GetSetunitcost = modPendInv.GetSetunitcost;
                                    oModLineItem.GetSetcostprice = modPendInv.GetSetcostprice;
                                    oModLineItem.GetSetunitprice = modPendInv.GetSetunitprice;
                                    oModLineItem.GetSetdiscamount = modPendInv.GetSetdiscamount;
                                    oModLineItem.GetSetquantity = modPendInv.GetSetquantity;
                                    oModLineItem.GetSetinvoiceprice = modPendInv.GetSetinvoiceprice;

                                    oModLineItem.GetSettaxcode = modPendInv.GetSettaxcode;
                                    oModLineItem.GetSettaxrate = modPendInv.GetSettaxrate;
                                    oModLineItem.GetSettaxamount = modPendInv.GetSettaxamount;
                                    oModLineItem.GetSettotalinvoice = modPendInv.GetSettotalinvoice;

                                    String result0 = oMainCon.insertInvoiceDetails(oModLineItem);
                    
                                    //to update Sales Order Invoice Amount
                                    MainModel oModOrderDet = oMainCon.getOrderDetailsDetails(modPendInv.GetSetcomp, modPendInv.GetSetorderno, modPendInv.GetSetorder_lineno, "");
                                    oModOrderDet.GetSetinvoiceamount = oModOrderDet.GetSetinvoiceamount + modPendInv.GetSettotalinvoice;
                                    String result1 = oMainCon.updateOrderDetails(oModOrderDet);

                                    //update status for shipment has invoice
                                    MainModel oModShipmentDet = oMainCon.getShipmentDetailsDetails(modPendInv.GetSetcomp, modPendInv.GetSetshipmentno, modPendInv.GetSetlineno, "");
                                    oModShipmentDet.GetSethasinvoice = "Y";
                                    String result3 = oMainCon.updateShipmentDetails(oModShipmentDet);

                                }//for(int y=0; y <lsShipmentLineItem.Count; y++){                                
                                //update order header information
                                String result4 = oMainCon.updateInvoiceHeaderInfo(sCurrComp, oModInvoice.GetSetinvoiceno);
                
                            }//if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))

                            proceedConfirmOrder = true;
                        }
                        else
                        {
                            proceedConfirmOrder = false;
                            sAlertMessage = "ERROR|Unable to confirm Order. Shipment can not be processed due Internal Server Error!";
                        }
                    }
                    else
                    {
                        proceedConfirmOrder = false;
                        sAlertMessage = "ERROR|Unable to confirm Order. Shipment can not be processed due to Stock for some Order Items are Not Available!";
                    }//if (stockLocation.Trim().Length > 0)
                    */
                }

                if (proceedConfirmOrder)
                {
                    if (oMainCon.updatePurchaseOrderHeader(oModOrder).Equals("Y"))
                    {
                        //check activity type and update relevant data if required
                        //update purchase order header information
                        sAlertMessage = "SUCCESS|Maklumat Pesanan Terimaan berjaya disahkan...";
                        Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya disahkan...";
                        Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                }
                else
                {
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya disahkan...";
                Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sOrderNo.Length > 0)
            {
                //update purchase order header - CANCEL
                oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
                oModOrder.GetSetorderstatus = "CANCELLED";
                oModOrder.GetSetordercancelled = sUserId;
                if (oMainCon.updatePurchaseOrderHeader(oModOrder).Equals("Y"))
                {
                    //update purchase order header information
                    sAlertMessage = "SUCCESS|Maklumat Pesanan Terimaan berjaya dibatalkankan...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya dibatalkankan...";
                    Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Terimaan tidak berjaya dibatalkankan...";
                Response.Redirect("ReceiveDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }

        //get Item Details and discount for the Item
        lsItemDiscount = oMainCon.getItemDiscountList(sCurrComp, oModOrder.GetSetordercat, oModOrder.GetSetordertype, "");
        //get Tax Details 
        lsTax = oMainCon.getTaxList(sCurrComp);
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