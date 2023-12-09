using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuotationDetails : System.Web.UI.Page
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
        //lsBP = oMainCon.getBPListIncludeSub(sCurrComp);

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
        //lsBP = oMainCon.getBPListIncludeSub(sCurrComp);

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
            sOrderNo = oMainCon.getNextRunningNo(sCurrComp, "QUOTATION", "ACTIVE");
            oModOrder.GetSetorderno = sOrderNo;
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("paytype"));
            oModOrder.GetSetexpirydate = oMainCon.replaceNull(Request.Params.Get("expirydate"));
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
            oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp,sOrderNo);
            oModOrder.GetSetorderdate = oMainCon.replaceNull(Request.Params.Get("orderdate"));
            oModOrder.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("ordercat"));
            oModOrder.GetSetorderactivity = oMainCon.replaceNull(Request.Params.Get("orderactivity"));
            oModOrder.GetSetordertype = oMainCon.replaceNull(Request.Params.Get("ordertype"));
            oModOrder.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("paytype"));
            oModOrder.GetSetexpirydate = oMainCon.replaceNull(Request.Params.Get("expirydate"));
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
            sActionString = "DAFTAR SEBUT HARGA";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sOrderNo.Length > 0)
            {
                //insert new Order
                if (oMainCon.insertQuotationHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "QUOTATION", "ACTIVE");
                    lsOrderLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Sebut Harga berjaya...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Sebut Harga tidak berjaya...";
                    sOrderNo = "";
                    oModOrder.GetSetorderno = sOrderNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR SEBUT HARGA";
                    lsOrderLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Sebut Harga tidak berjaya...";
                sOrderNo = "";
                oModOrder.GetSetorderno = sOrderNo;
                sAction = "ADD";
                sActionString = "DAFTAR SEBUT HARGA";
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT SEBUT HARGA";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                lsOrderLineItem = oMainCon.getQuotationDetailsList(sCurrComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Sebut Harga...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI SEBUT HARGA";
            if (sOrderNo.Length > 0)
            {
                oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                lsOrderLineItem = oMainCon.getQuotationDetailsList(sCurrComp, sOrderNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Sebut Harga...";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sOrderNo.Length > 0)
            {
                //update Order
                if (oMainCon.updateQuotationHeader(oModOrder).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Sebut Harga berjaya disimpan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI SEBUT HARGA";
                    oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                    lsOrderLineItem = oMainCon.getQuotationDetailsList(sCurrComp, sOrderNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI SEBUT HARGA";
                oModOrder = new MainModel();
                lsOrderLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //check whether already exist in Order Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getQuotationDetailsDetails(oModLineItem.GetSetcomp, oModLineItem.GetSetorderno, 0, oModLineItem.GetSetitemno);
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya ditambah. Item tersebut telah ditambah pada Sebut Harga [Line No: " + modExistent.GetSetlineno + "]";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertQuotationDetails(oModLineItem).Equals("Y"))
                    {
                        //update order header information
                        String result = oMainCon.updateQuotationHeaderInfo(sCurrComp, sOrderNo);
                        sAlertMessage = "SUCCESS|Item Sebut Harga berjaya ditambah...";
                        Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya ditambah...";
                        Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya ditambah...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.updateQuotationDetails(oModLineItem).Equals("Y"))
                {
                    //update order header information
                    String result = oMainCon.updateQuotationHeaderInfo(sCurrComp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Sebut Harga berjaya dikemaskini...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya dikemaskini...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya dikemaskini...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sOrderNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //delete line item
                if (oMainCon.deleteQuotationDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getQuotationDetailsList(sCurrComp, sOrderNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteQuotationDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertQuotationDetails(oLineItem);
                    }
                    //update order header information
                    String result = oMainCon.updateQuotationHeaderInfo(sCurrComp, sOrderNo);
                    sAlertMessage = "SUCCESS|Item Sebut Harga berjaya dikeluarkan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya dikeluarkan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Sebut Harga tidak berjaya dikeluarkan...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sOrderNo.Length > 0 && oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo).GetSetorderstatus != "CONFIRMED" && oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo).GetSetorderstatus != "CANCELLED")
            {
                //update order header - CONFIRM
                oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                oModOrder.GetSetorderstatus = "CONFIRMED";
                oModOrder.GetSetorderapproved = sUserId;
                if (oMainCon.updateQuotationHeader(oModOrder).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Sebut Harga berjaya disahkan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya disahkan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }

            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya disahkan...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONVERT"))
        {
            if (sOrderNo.Length > 0 && oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo).GetSetorderstatus == "CONFIRMED")
            {
                //update order header - CONFIRM
                oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                oModOrder.GetSetorderno = oMainCon.getNextRunningNo(sCurrComp, oModOrder.GetSetordercat, "ACTIVE");
                oModOrder.GetSetorderactivity = "ACTIVITY00";
                oModOrder.GetSetorderremarks = "SEBUT HARGA: " + sOrderNo;
                oModOrder.GetSetorderstatus = "NEW";
                oModOrder.GetSetordercreated = sUserId;
                if (oMainCon.insertOrderHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, oModOrder.GetSetordercat, "ACTIVE");
                    ArrayList lsLineItem = oMainCon.getQuotationDetailsList(sCurrComp, sOrderNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        oLineItem.GetSetorderno = oModOrder.GetSetorderno;
                        String flag = oMainCon.insertOrderDetails(oLineItem);
                    }
                    //sAlertMessage = "SUCCESS|Sebut Harga berjaya ditukar kepada Pesanan Jualan: <a href='OrderDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + oModOrder.GetSetorderno + "'>" + oModOrder.GetSetorderno + "</a>";
                    sAlertMessage = "SUCCESS|Sebut Harga berjaya ditukar kepada Pesanan Jualan: " + oModOrder.GetSetorderno;
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Sebut Harga tidak berjaya ditukar kepada Pesanan Jualan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }

            }
            else
            {
                sAlertMessage = "ERROR|Sebut Harga tidak berjaya ditukar kepada Pesanan Jualan...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sOrderNo.Length > 0)
            {
                //update order header - CANCEL
                oModOrder = oMainCon.getQuotationHeaderDetails(sCurrComp, sOrderNo);
                oModOrder.GetSetorderstatus = "CANCELLED";
                oModOrder.GetSetordercancelled = sUserId;
                if (oMainCon.updateQuotationHeader(oModOrder).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Sebut Harga berjaya dibatalkankan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya dibatalkankan...";
                    Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Sebut Harga tidak berjaya dibatalkankan...";
                Response.Redirect("QuotationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&orderno=" + sOrderNo + "&alertmessage=" + sAlertMessage);
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