using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReceiptDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sReceiptNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModHeader = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsLineItem = new ArrayList();
    public ArrayList lsPendReceipt = new ArrayList();
    public ArrayList lsBP = new ArrayList();
    public String selected_bp = "";
    public ArrayList lsComp = new ArrayList();
    public ArrayList lsStockLocation = new ArrayList();
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
        if (Request.QueryString["receiptno"] != null)
        {
            sReceiptNo = Request.QueryString["receiptno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsBP = oMainCon.getOrderPendingReceipt(sCurrComp, "", sReceiptNo);
        //lsComp = oMainCon.getTransferPendingReceipt(sCurrComp, "", sReceiptNo);
        lsStockLocation = oMainCon.getParamList(sCurrComp, "", "STOCK_LOCATION", "");

        if (sAction.Equals("ADD"))
        {
            sReceiptNo = "";
            oModHeader = new MainModel();
            oModHeader.GetSetreceiptdate = DateTime.Now.ToString("dd-MM-yyyy");
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
        if (Request.Params.Get("hidReceiptNo") != null)
        {
            sReceiptNo = oMainCon.replaceNull(Request.Params.Get("hidReceiptNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        lsBP = oMainCon.getBPList(sCurrComp, "", "", "");
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsBP = oMainCon.getOrderPendingReceipt(sCurrComp, "", sReceiptNo);
        //lsComp = oMainCon.getTransferPendingReceipt(sCurrComp, "", sReceiptNo);
        lsStockLocation = oMainCon.getParamList(sCurrComp, "", "STOCK_LOCATION", "");

        //for reset
        if (sAction.Equals("ADD"))
        {
            sReceiptNo = "";
            oModHeader = new MainModel();
            oModHeader.GetSetreceiptdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModHeader.GetSetstatus = "NEW";
            lsLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModHeader = new MainModel();
            oModHeader.GetSetcomp = sCurrComp;
            oModHeader.GetSetreceiptdate = oMainCon.replaceNull(Request.Params.Get("receiptdate"));
            oModHeader.GetSetreceiptcat = oMainCon.replaceNull(Request.Params.Get("receiptcat"));
            sReceiptNo = oMainCon.getNextRunningNo(sCurrComp, "RECEIPT", "ACTIVE");
            oModHeader.GetSetreceiptno = sReceiptNo;
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
            oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
            oModHeader.GetSetreceiptdate = oMainCon.replaceNull(Request.Params.Get("receiptdate"));
            oModHeader.GetSetreceiptcat = oMainCon.replaceNull(Request.Params.Get("receiptcat"));
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
            oModLineItem.GetSetreceiptno = sReceiptNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetorderno = oMainCon.replaceNull(Request.Params.Get("addorderno"));
            oModLineItem.GetSetorder_lineno = oMainCon.replaceIntZero(Request.Params.Get("addorder_lineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetorder_quantity = oMainCon.replaceIntZero(Request.Params.Get("addorder_quantity"));
            oModLineItem.GetSetreceipt_quantity = oMainCon.replaceIntZero(Request.Params.Get("addreceipt_quantity"));
            oModLineItem.GetSetlocation = oMainCon.replaceNull(Request.Params.Get("addlocation"));
            oModLineItem.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("addremarks"));
            oModLineItem.GetSethasbilling = "N";
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetreceiptno = sReceiptNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PENERIMAAN PESANAN";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sReceiptNo.Length > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //insert new Receipt
                if (oMainCon.insertReceiptHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "RECEIPT", "ACTIVE");
                    lsLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Penerimaan Belian/Terimaan berjaya...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Penerimaan Belian/Terimaan tidak berjaya...";
                    sReceiptNo = "";
                    oModHeader.GetSetreceiptno = sReceiptNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR PENERIMAAN PESANAN";
                    lsLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Penerimaan Belian/Terimaan tidak berjaya...";
                sReceiptNo = "";
                oModHeader.GetSetreceiptno = sReceiptNo;
                sAction = "ADD";
                sActionString = "DAFTAR PENERIMAAN PESANAN";
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PENERIMAAN PESANAN";
            if (sReceiptNo.Length > 0)
            {
                oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                lsLineItem = oMainCon.getReceiptDetailsList(sCurrComp, sReceiptNo, 0, "");
                lsPendReceipt = oMainCon.getLineItemPendingReceipt(sCurrComp, oModHeader.GetSetbpid, oModHeader.GetSetreceiptno, "", oModHeader.GetSetreceiptcat);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Penerimaan Belian/Terimaan...";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PENERIMAAN PESANAN";
            if (sReceiptNo.Length > 0)
            {
                oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                lsLineItem = oMainCon.getReceiptDetailsList(sCurrComp, sReceiptNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Penerimaan Belian/Terimaan...";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sReceiptNo.Length > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //update Receipt Header
                if (oMainCon.updateReceiptHeader(oModHeader).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Penerimaan Belian/Terimaan berjaya disimpan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penerimaan Belian/Terimaan tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PENERIMAAN PESANAN";
                    oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                    lsLineItem = oMainCon.getReceiptDetailsList(sCurrComp, sReceiptNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Penerimaan Belian/Terimaan tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PENERIMAAN PESANAN";
                oModHeader = new MainModel();
                lsLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sReceiptNo.Length > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getReceiptDetailsOther(oModLineItem.GetSetcomp, oModLineItem.GetSetreceiptno, oModLineItem.GetSetorderno, oModLineItem.GetSetorder_lineno, oModLineItem.GetSetitemno, "NEW");
                if (modExistent.GetSetreceiptno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Penerimaan Belian/Terimaan tidak berjaya ditambah. Item tersebut telah ditambah pada Penerimaan Belian/Terimaan: " + modExistent.GetSetreceiptno;
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertReceiptDetails(oModLineItem).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Item Penerimaan Belian/Terimaan berjaya ditambah...";
                        Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Penerimaan Belian/Terimaan tidak berjaya ditambah...";
                        Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Penerimaan Belian/Terimaan tidak berjaya ditambah...";
                Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sReceiptNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deleteReceiptDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getReceiptDetailsList(sCurrComp, sReceiptNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteReceiptDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertReceiptDetails(oLineItem);
                    }
                    sAlertMessage = "SUCCESS|Item Penerimaan Belian/Terimaan berjaya dikeluarkan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Penerimaan Belian/Terimaan tidak berjaya dikeluarkan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Penerimaan Belian/Terimaan tidak berjaya dikeluarkan...";
                Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sReceiptNo.Length > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //update receipt header - CONFIRM
                oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                oModHeader.GetSetstatus = "CONFIRMED";
                oModHeader.GetSetconfirmedby = sUserId;
                if (oMainCon.updateReceiptHeader(oModHeader).Equals("Y"))
                {
                    //get latest information about Receipt Header ie. Confirmed Date which is needed for storing item stock transactions
                    oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                    ArrayList lsReceiptLineItem = oMainCon.getReceiptDetailsList(oModHeader.GetSetcomp, oModHeader.GetSetreceiptno, 0, "");
                    for (int i = 0; i < lsReceiptLineItem.Count; i++)
                    {
                        MainModel modReceiptDet = (MainModel)lsReceiptLineItem[i];

                        //update Receipt Details Date SOH
                        modReceiptDet.GetSetdatesoh = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        String result0 = oMainCon.updateReceiptDetails(modReceiptDet);

                        MainModel oModOrderLineItem = new MainModel();
                        //to update shipped quantity order item @ transfer item
                        if (oModHeader.GetSetreceiptcat.Equals("PURCHASE_ORDER") || oModHeader.GetSetreceiptcat.Equals("RECEIVE_ORDER"))
                        {
                            //to update received quantity order item
                            oModOrderLineItem = oMainCon.getPurchaseOrderDetailsDetails(modReceiptDet.GetSetcomp, modReceiptDet.GetSetorderno, modReceiptDet.GetSetorder_lineno, modReceiptDet.GetSetitemno, modReceiptDet.GetSetreceiptno);
                            oModOrderLineItem.GetSetreceiptqty = oModOrderLineItem.GetSetreceiptqty + modReceiptDet.GetSetreceipt_quantity;
                            String result1 = oMainCon.updatePurchaseOrderDetails(oModOrderLineItem);
                        }
                        else if (oModHeader.GetSetreceiptcat.Equals("TRANSFER_ORDER"))
                        {
                            MainModel oModTranferHeader = oMainCon.getTransferOrderHeaderDetails("", "", modReceiptDet.GetSetcomp, modReceiptDet.GetSetorderno);
                            oModOrderLineItem = oMainCon.getTransferOrderDetailsDetails(oModTranferHeader.GetSetCompFromDetails.GetSetcomp, modReceiptDet.GetSetorderno, modReceiptDet.GetSetorder_lineno, modReceiptDet.GetSetitemno);
                            oModOrderLineItem.GetSetreceiptqty = oModOrderLineItem.GetSetreceiptqty + modReceiptDet.GetSetreceipt_quantity;
                            String result = oMainCon.updateTransferOrderDetails(oModOrderLineItem);
                        }


                        //to update item stock & stock transaction
                        MainModel oModItemDet = oMainCon.getItemDetails(modReceiptDet.GetSetcomp, modReceiptDet.GetSetitemno);
                        if (oModItemDet.GetSetitemcat.Equals("INVENTORY") || oModItemDet.GetSetitemcat.Equals("ASSET"))
                        {
                            MainModel oModItemStock = new MainModel();
                            oModItemStock.GetSetcomp = modReceiptDet.GetSetcomp;
                            oModItemStock.GetSetitemno = modReceiptDet.GetSetitemno;
                            oModItemStock.GetSetitemdesc = modReceiptDet.GetSetitemdesc;
                            oModItemStock.GetSetlocation = modReceiptDet.GetSetlocation;
                            oModItemStock.GetSetdatesoh = modReceiptDet.GetSetdatesoh;
                            oModItemStock.GetSetqtysoh = modReceiptDet.GetSetreceipt_quantity;
                            /** Modified by Zul - 17.7.2021
                            if (oModOrderLineItem.GetSetordercat.Equals("RECEIVE_ORDER"))
                            {
                                oModItemStock.GetSetcostsoh = Math.Round(oModItemDet.GetSetcostprice * modReceiptDet.GetSetreceipt_quantity, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                oModItemStock.GetSetcostsoh = Math.Round(Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero) * (modReceiptDet.GetSetreceipt_quantity), 2, MidpointRounding.AwayFromZero);
                            }
                            **/
                            oModItemStock.GetSetcostsoh = Math.Round(Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero) * (modReceiptDet.GetSetreceipt_quantity), 2, MidpointRounding.AwayFromZero);
                            String result2 = oMainCon.insertItemStock(oModItemStock);

                            MainModel oModItemStockTrans = new MainModel();
                            oModItemStockTrans.GetSetcomp = modReceiptDet.GetSetcomp;
                            oModItemStockTrans.GetSetitemno = modReceiptDet.GetSetitemno;
                            oModItemStockTrans.GetSetitemdesc = modReceiptDet.GetSetitemdesc;
                            oModItemStockTrans.GetSetlocation = modReceiptDet.GetSetlocation;
                            oModItemStockTrans.GetSetdatesoh = modReceiptDet.GetSetdatesoh;
                            oModItemStockTrans.GetSettranstype = "RECEIPT";
                            oModItemStockTrans.GetSettransdate = oModHeader.GetSetconfirmeddate;
                            oModItemStockTrans.GetSettransno = modReceiptDet.GetSetreceiptno;
                            oModItemStockTrans.GetSettrans_lineno = modReceiptDet.GetSetlineno;
                            oModItemStockTrans.GetSetorderno = modReceiptDet.GetSetorderno;
                            oModItemStockTrans.GetSetorder_lineno = modReceiptDet.GetSetorder_lineno;
                            oModItemStockTrans.GetSettransqty = modReceiptDet.GetSetreceipt_quantity;
                            /** Modified by Zul - 17.7.2021
                            if (oModOrderLineItem.GetSetordercat.Equals("RECEIVE_ORDER"))
                            {
                                oModItemStockTrans.GetSettransprice = Math.Round(oModItemDet.GetSetcostprice, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                oModItemStockTrans.GetSettransprice = Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero);
                            }
                            **/
                            oModItemStockTrans.GetSettransprice = Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero);
                            oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                            oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                            String result3 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                        }

                    }
                    sAlertMessage = "SUCCESS|Maklumat Penerimaan Belian/Terimaan berjaya disahkan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penerimaan Belian/Terimaan tidak berjaya disahkan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Penerimaan Belian/Terimaan tidak berjaya disahkan...";
                Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sReceiptNo.Length > 0 && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CONFIRMED" && oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo).GetSetstatus != "CANCELLED")
            {
                //update Receipt header - CANCEL
                oModHeader = oMainCon.getReceiptHeaderDetails(sCurrComp, sReceiptNo);
                oModHeader.GetSetstatus = "CANCELLED";
                oModHeader.GetSetcancelledby = sUserId;
                if (oMainCon.updateReceiptHeader(oModHeader).Equals("Y"))
                {
                    //update Receipt header information
                    sAlertMessage = "SUCCESS|Maklumat Penerimaan Belian/Terimaan berjaya dibatalkankan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Penerimaan Belian/Terimaan tidak berjaya dibatalkankan...";
                    Response.Redirect("ReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&receiptno=" + sReceiptNo + "&alertmessage=" + sAlertMessage);
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