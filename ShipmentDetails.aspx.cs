using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShipmentDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sShipmentNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModHeader = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsLineItem = new ArrayList();
    public ArrayList lsPendShipment = new ArrayList();
    public ArrayList lsBP = new ArrayList();
    //String selected_bp = "";
    public ArrayList lsComp = new ArrayList();
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
        if (Request.QueryString["shipmentno"] != null)
        {
            sShipmentNo = Request.QueryString["shipmentno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsBP = oMainCon.getOrderPendingShipment(sCurrComp, "", sShipmentNo);
        //lsComp = oMainCon.getTransferPendingShipment(sCurrComp, "", sShipmentNo);

        if (sAction.Equals("ADD"))
        {
            sShipmentNo = "";
            oModHeader = new MainModel();
            oModHeader.GetSetshipmentdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModHeader.GetSetstatus = "NEW";
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
        if (Request.Params.Get("hidShipmentNo") != null)
        {
            sShipmentNo = oMainCon.replaceNull(Request.Params.Get("hidShipmentNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsBP = oMainCon.getOrderPendingShipment(sCurrComp, "", sShipmentNo);
        //lsComp = oMainCon.getTransferPendingShipment(sCurrComp, "", sShipmentNo);

        //for reset
        if (sAction.Equals("ADD"))
        {
            sShipmentNo = "";
            oModHeader = new MainModel();
            oModHeader.GetSetshipmentdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModHeader.GetSetstatus = "NEW";
            lsLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModHeader = new MainModel();
            oModHeader.GetSetcomp = sCurrComp;
            oModHeader.GetSetshipmentdate = oMainCon.replaceNull(Request.Params.Get("shipmentdate"));
            oModHeader.GetSetshipmentcat = oMainCon.replaceNull(Request.Params.Get("shipmentcat"));
            sShipmentNo = oMainCon.getNextRunningNo(sCurrComp, "SHIPMENT", "ACTIVE");
            oModHeader.GetSetshipmentno = sShipmentNo;
            oModHeader.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModHeader.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModHeader.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModHeader.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModHeader.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModHeader.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModHeader.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
            oModHeader.GetSetshipmentdate = oMainCon.replaceNull(Request.Params.Get("shipmentdate"));
            oModHeader.GetSetshipmentcat = oMainCon.replaceNull(Request.Params.Get("shipmentcat"));
            oModHeader.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModHeader.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModHeader.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModHeader.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModHeader.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModHeader.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModHeader.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetshipmentno = sShipmentNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetorderno = oMainCon.replaceNull(Request.Params.Get("addorderno"));
            oModLineItem.GetSetorder_lineno = oMainCon.replaceIntZero(Request.Params.Get("addorder_lineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetorder_quantity = oMainCon.replaceIntZero(Request.Params.Get("addorder_quantity"));
            oModLineItem.GetSetshipment_quantity = oMainCon.replaceIntZero(Request.Params.Get("addshipment_quantity"));
            oModLineItem.GetSetlocation = oMainCon.replaceNull(Request.Params.Get("addlocation"));
            oModLineItem.GetSetdatesoh = oMainCon.replaceNull(Request.Params.Get("adddatesoh"));
            oModLineItem.GetSetqtysoh = oMainCon.replaceIntZero(Request.Params.Get("addqtysoh"));
            oModLineItem.GetSetqtyavailable = oMainCon.replaceIntZero(Request.Params.Get("addqtyavailable"));
            oModLineItem.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("addremarks"));
            oModLineItem.GetSethasinvoice = "N";
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetshipmentno = sShipmentNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PENGHANTARAN PESANAN";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sShipmentNo.Length > 0 && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CANCELLED")
            {
                //insert new Shipment
                if (oMainCon.insertShipmentHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "SHIPMENT", "ACTIVE");
                    lsLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Penghantaran Pesanan berjaya...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Penghantaran Pesanan tidak berjaya...";
                    sShipmentNo = "";
                    oModHeader.GetSetshipmentno = sShipmentNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR PENGHANTARAN PESANAN";
                    lsLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Penghantaran Pesanan tidak berjaya...";
                sShipmentNo = "";
                oModHeader.GetSetshipmentno = sShipmentNo;
                sAction = "ADD";
                sActionString = "DAFTAR PENGHANTARAN PESANAN";
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PENGHANTARAN PESANAN";
            if (sShipmentNo.Length > 0)
            {
                oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                lsLineItem = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, 0, "");
                lsPendShipment = oMainCon.getLineItemPendingShipment(sCurrComp, oModHeader.GetSetbpid, oModHeader.GetSetshipmentno, "", oModHeader.GetSetshipmentcat);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Penghantaran Pesanan...";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PENGHANTARAN PESANAN";
            if (sShipmentNo.Length > 0)
            {
                oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                lsLineItem = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Penghantaran Pesanan...";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sShipmentNo.Length > 0 && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CANCELLED")
            {
                //update Shipment Header
                if (oMainCon.updateShipmentHeader(oModHeader).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Penghantaran Pesanan berjaya disimpan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PENGHANTARAN PESANAN";
                    oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                    lsLineItem = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PENGHANTARAN PESANAN";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sShipmentNo.Length > 0 && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getShipmentDetailsOther(oModLineItem.GetSetcomp, oModLineItem.GetSetshipmentno, oModLineItem.GetSetorderno, oModLineItem.GetSetorder_lineno, oModLineItem.GetSetitemno, "NEW");
                if (modExistent.GetSetshipmentno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Penghantaran Pesanan tidak berjaya ditambah. Item tersebut telah ditambah pada Penghantaran Pesanan: " + modExistent.GetSetshipmentno;
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertShipmentDetails(oModLineItem).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Item Penghantaran Pesanan berjaya ditambah...";
                        Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Penghantaran Pesanan tidak berjaya ditambah...";
                        Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Penghantaran Pesanan tidak berjaya ditambah...";
                Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sShipmentNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deleteShipmentDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteShipmentDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertShipmentDetails(oLineItem);
                    }
                    sAlertMessage = "SUCCESS|Item Penghantaran Pesanan berjaya dikeluarkan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Penghantaran Pesanan tidak berjaya dikeluarkan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Penghantaran Pesanan tidak berjaya dikeluarkan...";
                Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
            if (sShipmentNo.Length > 0 && oModHeader.GetSetstatus != "CONFIRMED" && oModHeader.GetSetstatus != "CANCELLED")
            {
                Boolean proceed = true;
                //check if 'TRANSFER_ORDER', must create new item on other comp
                if (oModHeader.GetSetshipmentcat.Equals("TRANSFER_ORDER"))
                {
                    ArrayList lsShipmentLineItem = oMainCon.getShipmentDetailsList(oModHeader.GetSetcomp, oModHeader.GetSetshipmentno, 0, "");
                    for (int i = 0; i < lsShipmentLineItem.Count; i++)
                    {
                        MainModel modShipmentDet = (MainModel)lsShipmentLineItem[i];
                        MainModel modTransferHeader = oMainCon.getTransferOrderHeaderDetails(modShipmentDet.GetSetcomp, "", "", modShipmentDet.GetSetorderno);
                        MainModel modItemDetails = oMainCon.getItemDetails(modTransferHeader.GetSetCompToDetails.GetSetcomp, modShipmentDet.GetSetitemno);
                        if (proceed)
                        {
                            if (modItemDetails.GetSetitemno.Trim().Length == 0 && modTransferHeader.GetSetCompToDetails.GetSetcomp.Trim().Length > 0)
                            {
                                MainModel modItemToCreate = oMainCon.getItemDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetitemno);
                                modItemToCreate.GetSetcomp = modTransferHeader.GetSetCompToDetails.GetSetcomp;
                                if (oMainCon.insertItemMaster(modItemToCreate).Equals("Y"))
                                {
                                    proceed = true;
                                }
                                else
                                {
                                    proceed = false;
                                }
                            }
                            else if (modItemDetails.GetSetitemno.Trim().Length > 0 && modTransferHeader.GetSetCompToDetails.GetSetcomp.Trim().Length > 0)
                            {
                                proceed = true;
                            }
                            else
                            {
                                proceed = false;
                            }
                        }
                    }
                }
                if (proceed)
                {
                    //update payment paid header - CONFIRM
                    oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                    oModHeader.GetSetstatus = "CONFIRMED";
                    oModHeader.GetSetconfirmedby = sUserId;
                    if (oMainCon.updateShipmentHeader(oModHeader).Equals("Y"))
                    {
                        //get latest information about Shipment Header ie. Confirmed Date which is needed for storing item stock transactions
                        oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                        ArrayList lsShipmentLineItem = oMainCon.getShipmentDetailsList(oModHeader.GetSetcomp, oModHeader.GetSetshipmentno, 0, "");
                        for (int i = 0; i < lsShipmentLineItem.Count; i++)
                        {
                            MainModel modShipmentDet = (MainModel)lsShipmentLineItem[i];

                            //to update shipped quantity order item @ transfer item
                            if (oModHeader.GetSetshipmentcat.Equals("SALES_ORDER") || oModHeader.GetSetshipmentcat.Equals("GIVE_ORDER"))
                            {
                                MainModel oModOrderLineItem = oMainCon.getOrderDetailsDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetorderno, modShipmentDet.GetSetorder_lineno, modShipmentDet.GetSetitemno);
                                oModOrderLineItem.GetSetdeliverqty = oModOrderLineItem.GetSetdeliverqty + modShipmentDet.GetSetshipment_quantity;
                                String result = oMainCon.updateOrderDetails(oModOrderLineItem);
                            }
                            else if (oModHeader.GetSetshipmentcat.Equals("TRANSFER_ORDER"))
                            {
                                MainModel oModOrderLineItem = oMainCon.getTransferOrderDetailsDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetorderno, modShipmentDet.GetSetorder_lineno, modShipmentDet.GetSetitemno);
                                oModOrderLineItem.GetSetdeliverqty = oModOrderLineItem.GetSetdeliverqty + modShipmentDet.GetSetshipment_quantity;
                                String result = oMainCon.updateTransferOrderDetails(oModOrderLineItem);
                            }

                            //to update item stock & stock transaction
                            MainModel oModLatestItemStock = oMainCon.getItemStockDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetitemno, modShipmentDet.GetSetlocation, modShipmentDet.GetSetdatesoh);
                            if (oModLatestItemStock.GetSetitemcat.Equals("INVENTORY") || oModLatestItemStock.GetSetitemcat.Equals("ASSET"))
                            {
                                MainModel oModItemStock = new MainModel();
                                oModItemStock.GetSetcomp = modShipmentDet.GetSetcomp;
                                oModItemStock.GetSetitemno = modShipmentDet.GetSetitemno;
                                oModItemStock.GetSetitemdesc = modShipmentDet.GetSetitemdesc;
                                oModItemStock.GetSetlocation = modShipmentDet.GetSetlocation;
                                oModItemStock.GetSetdatesoh = modShipmentDet.GetSetdatesoh;
                                oModItemStock.GetSetqtysoh = oModLatestItemStock.GetSetqtysoh - modShipmentDet.GetSetshipment_quantity;
                                oModItemStock.GetSetcostsoh = Math.Round(Math.Round(oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh, 2, MidpointRounding.AwayFromZero) * (oModLatestItemStock.GetSetqtysoh - modShipmentDet.GetSetshipment_quantity), 2, MidpointRounding.AwayFromZero);
                                if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                                {
                                    String result1 = oMainCon.updateItemStock(oModItemStock);
                                }

                                MainModel oModItemStockTrans = new MainModel();
                                oModItemStockTrans.GetSetcomp = modShipmentDet.GetSetcomp;
                                oModItemStockTrans.GetSetitemno = modShipmentDet.GetSetitemno;
                                oModItemStockTrans.GetSetitemdesc = modShipmentDet.GetSetitemdesc;
                                oModItemStockTrans.GetSetlocation = modShipmentDet.GetSetlocation;
                                oModItemStockTrans.GetSetdatesoh = modShipmentDet.GetSetdatesoh;
                                oModItemStockTrans.GetSettranstype = "SHIPMENT";
                                oModItemStockTrans.GetSettransdate = oModHeader.GetSetconfirmeddate;
                                oModItemStockTrans.GetSettransno = modShipmentDet.GetSetshipmentno;
                                oModItemStockTrans.GetSettrans_lineno = modShipmentDet.GetSetlineno;
                                oModItemStockTrans.GetSetorderno = modShipmentDet.GetSetorderno;
                                oModItemStockTrans.GetSetorder_lineno = modShipmentDet.GetSetorder_lineno;
                                oModItemStockTrans.GetSettransqty = modShipmentDet.GetSetshipment_quantity * -1;
                                oModItemStockTrans.GetSettransprice = Math.Round(oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh, 2, MidpointRounding.AwayFromZero);
                                oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                                oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                                String result2 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                            }

                        }
                        sAlertMessage = "SUCCESS|Maklumat Penghantaran Pesanan berjaya disahkan...";
                        Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya disahkan...";
                        Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya disahkan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya disahkan...";
                Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sShipmentNo.Length > 0 && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo).GetSetstatus != "CANCELLED")
            {
                //update Shipment header - CANCEL
                oModHeader = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
                oModHeader.GetSetstatus = "CANCELLED";
                oModHeader.GetSetcancelledby = sUserId;
                if (oMainCon.updateShipmentHeader(oModHeader).Equals("Y"))
                {
                    //update Shipment header information
                    sAlertMessage = "SUCCESS|Maklumat Penghantaran Pesanan berjaya dibatalkankan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penghantaran Pesanan tidak berjaya dibatalkankan...";
                    Response.Redirect("ShipmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&shipmentno=" + sShipmentNo + "&alertmessage=" + sAlertMessage);
                }
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