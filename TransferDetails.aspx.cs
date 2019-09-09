using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransferDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sOpenComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sOrderNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModOrder = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsComp = new ArrayList();
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
        if (Request.QueryString["comp"] != null)
        {
            sOpenComp = Request.QueryString["comp"].ToString();
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

        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsComp = oMainCon.getBPList(sCurrComp, "", "", "SUBSIDIARY");

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
        if (Request.QueryString["comp"] != null)
        {
            sOpenComp = Request.QueryString["comp"].ToString();
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

        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
        //lsComp = oMainCon.getBPList(sCurrComp, "", "", "SUBSIDIARY");

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
            sOrderNo = oMainCon.getNextRunningNo(sCurrComp, oModOrder.GetSetordercat, "ACTIVE","1");
            oModOrder.GetSetorderno = sOrderNo;
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetpricetype = oMainCon.replaceNull(Request.Params.Get("pricetype"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetCompFromDetails.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compfrom"));
            oModOrder.GetSetCompToDetails.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compto"));
            oModOrder.GetSetshipmentdate = oMainCon.replaceNull(Request.Params.Get("shipmentdate"));
            oModOrder.GetSetreceiptdate = oMainCon.replaceNull(Request.Params.Get("receiptdate"));
            oModOrder.GetSetorderremarks = oMainCon.replaceNull(Request.Params.Get("orderremarks"));
            oModOrder.GetSetorderstatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            oModOrder.GetSetordercreated = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
            oModOrder.GetSetorderdate = oMainCon.replaceNull(Request.Params.Get("orderdate"));
            oModOrder.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("ordercat"));
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetpricetype = oMainCon.replaceNull(Request.Params.Get("pricetype"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetCompFromDetails.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compfrom"));
            oModOrder.GetSetCompToDetails.GetSetcomp = oMainCon.replaceNull(Request.Params.Get("compto"));
            oModOrder.GetSetshipmentdate = oMainCon.replaceNull(Request.Params.Get("shipmentdate"));
            oModOrder.GetSetreceiptdate = oMainCon.replaceNull(Request.Params.Get("receiptdate"));
            oModOrder.GetSetorderremarks = oMainCon.replaceNull(Request.Params.Get("orderremarks"));
            oModOrder.GetSetorderstatus = oMainCon.replaceNull(Request.Params.Get("orderstatus"));
            oModOrder.GetSetordercreated = sUserId;
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sOpenComp;
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
            oModLineItem.GetSetcomp = sOpenComp;
            oModLineItem.GetSetorderno = sOrderNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PESANAN PINDAHAN";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sOrderNo.Length > 0)
            {
                //insert new Transfer Order
                if (oMainCon.insertTransferOrderHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModOrder.GetSetcomp, oModOrder.GetSetordercat, "ACTIVE");
                    lsOrderLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Pesanan Pindahan berjaya...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Pesanan Pindahan tidak berjaya...";
                    sOrderNo = "";
                    oModOrder.GetSetorderno = sOrderNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR PESANAN PINDAHAN";
                    lsOrderLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Pesanan Pindahan tidak berjaya...";
                sOrderNo = "";
                oModOrder.GetSetorderno = sOrderNo;
                sAction = "ADD";
                sActionString = "DAFTAR PESANAN PINDAHAN";
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PESANAN PINDAHAN";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
                lsOrderLineItem = oMainCon.getTransferOrderDetailsList(sOpenComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Pesanan Pindahan...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PESANAN PINDAHAN";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
                lsOrderLineItem = oMainCon.getTransferOrderDetailsList(sOpenComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Pesanan Pindahan...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sOrderNo.Length > 0)
            {
                //update Order
                if (oMainCon.updateTransferOrderHeader(oModOrder).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Pesanan Pindahan berjaya disimpan...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PESANAN PINDAHAN";
                    oModOrder = oMainCon.getTransferOrderHeaderDetails(oModOrder.GetSetcomp, "", "", oModOrder.GetSetorderno);
                    lsOrderLineItem = oMainCon.getTransferOrderDetailsList(oModOrder.GetSetcomp, oModOrder.GetSetorderno, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PESANAN PINDAHAN";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //check whether already exist in Transfer Order Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getTransferOrderDetailsDetails(oModLineItem.GetSetcomp, oModLineItem.GetSetorderno, 0, oModLineItem.GetSetitemno);
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Pindahan tidak berjaya ditambah. Item tersebut telah ditambah pada Pesanan Pindahan [Line No: " + modExistent.GetSetlineno + "]";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertTransferOrderDetails(oModLineItem).Equals("Y"))
                    //Boolean test = false;
                    //if(test)
                    {
                        //update transfer order header information
                        String result = oMainCon.updateTransferOrderHeaderInfo(oModLineItem.GetSetcomp, sOrderNo);
                        sAlertMessage = "SUCCESS|Item Pindahan berjaya ditambah...";
                        Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Pindahan tidak berjaya ditambah...";
                        Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Pindahan tidak berjaya ditambah...";
                Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + sOpenComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.updateTransferOrderDetails(oModLineItem).Equals("Y"))
                {
                    //update order header information
                    String result = oMainCon.updateTransferOrderHeaderInfo(oModLineItem.GetSetcomp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Pindahan berjaya dikemaskini...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Pindahan tidak berjaya dikemaskini...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Pindahan tidak berjaya dikemaskini...";
                Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + sOpenComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.deleteTransferOrderDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getTransferOrderDetailsList(oModLineItem.GetSetcomp, sOrderNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteTransferOrderDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertTransferOrderDetails(oLineItem);
                    }
                    //update purchase order header information
                    String result = oMainCon.updateTransferOrderHeaderInfo(oModLineItem.GetSetcomp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Pindahan berjaya dikeluarkan...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Pindahan tidak berjaya dikeluarkan...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModLineItem.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Pindahan tidak berjaya dikeluarkan...";
                Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + sOpenComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
            if (sOrderNo.Length > 0 && oModOrder.GetSetorderstatus != "CONFIRMED" && oModOrder.GetSetorderstatus != "CANCELLED")
            {
                //update purchase order header - CONFIRM
                oModOrder.GetSetorderstatus = "CONFIRMED";
                oModOrder.GetSetorderapproved = sUserId;

                Boolean proceedConfirmOrder = true;
                sAlertMessage = "";

                if (proceedConfirmOrder)
                {
                    if (oMainCon.updateTransferOrderHeader(oModOrder).Equals("Y"))
                    {
                        //check activity type and update relevant data if required
                        //update purchase order header information
                        sAlertMessage = "SUCCESS|Maklumat Pesanan Pindahan berjaya disahkan...";
                        Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya disahkan...";
                        Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                }
                else
                {
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya disahkan...";
                Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + sOpenComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
            if (sOrderNo.Length > 0 && oModOrder.GetSetorderstatus != "CONFIRMED" && oModOrder.GetSetorderstatus != "CANCELLED")
            {
                //update purchase order header - CANCEL
                oModOrder.GetSetorderstatus = "CANCELLED";
                oModOrder.GetSetordercancelled = sUserId;
                if (oMainCon.updateTransferOrderHeader(oModOrder).Equals("Y"))
                {
                    //update purchase order header information
                    sAlertMessage = "SUCCESS|Maklumat Pesanan Pindahan berjaya dibatalkankan...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya dibatalkankan...";
                    Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + oModOrder.GetSetcomp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pesanan Pindahan tidak berjaya dibatalkankan...";
                Response.Redirect("TransferDetails.aspx?action=OPEN&comp=" + sOpenComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }

        //get Item Details and discount for the Item
        lsItemDiscount = oMainCon.getItemDiscountList(sCurrComp, oModOrder.GetSetordercat, oModOrder.GetSetordertype, "", oModOrder.GetSetpricetype);
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