using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentReceiptDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sPayRcptNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModPayRcpt = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsOtherBP = new ArrayList();
    public ArrayList lsPendPayRcptMod = new ArrayList();
    public ArrayList lsPayRcptLineItem = new ArrayList();
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
        if (Request.QueryString["payrcptno"] != null)
        {
            sPayRcptNo = Request.QueryString["payrcptno"].ToString();
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
            sPayRcptNo = "";
            oModPayRcpt = new MainModel();
            oModPayRcpt.GetSetpayrcptdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModPayRcpt.GetSetstatus = "NEW";
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
        if (Request.Params.Get("hidPayRcptNo") != null)
        {
            sPayRcptNo = oMainCon.replaceNull(Request.Params.Get("hidPayRcptNo"));
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
            sPayRcptNo = "";
            oModPayRcpt = new MainModel();
            oModPayRcpt.GetSetpayrcptdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModPayRcpt.GetSetstatus = "NEW";
            lsPayRcptLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModPayRcpt = new MainModel();
            oModPayRcpt.GetSetcomp = sCurrComp;
            oModPayRcpt.GetSetpayrcptdate = oMainCon.replaceNull(Request.Params.Get("payrcptdate"));
            oModPayRcpt.GetSetpayrcpttype = oMainCon.replaceNull(Request.Params.Get("payrcpttype"));
            sPayRcptNo = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");
            oModPayRcpt.GetSetpayrcptno = sPayRcptNo;
            oModPayRcpt.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModPayRcpt.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModPayRcpt.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModPayRcpt.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModPayRcpt.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModPayRcpt.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModPayRcpt.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
            oModPayRcpt.GetSetpayrcptdate = oMainCon.replaceNull(Request.Params.Get("payrcptdate"));
            oModPayRcpt.GetSetpayrcpttype = oMainCon.replaceNull(Request.Params.Get("payrcpttype"));
            oModPayRcpt.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModPayRcpt.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModPayRcpt.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModPayRcpt.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModPayRcpt.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModPayRcpt.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModPayRcpt.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetpayrcptno = sPayRcptNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetinvoiceno = oMainCon.replaceNull(Request.Params.Get("addinvoiceno"));
            oModLineItem.GetSetinvoicedate = oMainCon.replaceNull(Request.Params.Get("addinvoicedate"));
            oModLineItem.GetSetinvoiceprice = oMainCon.replaceDoubleZero(Request.Params.Get("addinvoiceamount"));
            oModLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("addpaytype"));
            oModLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("addpayrefno"));
            oModLineItem.GetSetpayrcptprice = oMainCon.replaceDoubleZero(Request.Params.Get("addpayrcptamount"));
            oModLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("addpayremarks"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetpayrcptno = sPayRcptNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR BAYARAN TERIMA";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sPayRcptNo.Length > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //insert new Order
                if (oMainCon.insertPaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");
                    lsPayRcptLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Bayaran Terima berjaya...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Bayaran Terima tidak berjaya...";
                    sPayRcptNo = "";
                    oModPayRcpt.GetSetpayrcptno = sPayRcptNo;
                    //oModPayRcpt.GetSetbpid = "";
                    sAction = "ADD";
                    sActionString = "DAFTAR BAYARAN TERIMA";
                    lsPayRcptLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Bayaran Terima tidak berjaya...";
                sPayRcptNo = "";
                oModPayRcpt.GetSetpayrcptno = sPayRcptNo;
                //oModPayRcpt.GetSetbpid = "";
                sAction = "ADD";
                sActionString = "DAFTAR BAYARAN TERIMA";
                lsPayRcptLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT BAYARAN TERIMA";
            if (sPayRcptNo.Length > 0)
            {
                oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
                lsPayRcptLineItem = oMainCon.getPaymentReceiptDetailsList(sCurrComp, sPayRcptNo, 0, "");
                lsPendPayRcptMod = oMainCon.getLineItemPendingPaymentReceipt(sCurrComp, oModPayRcpt.GetSetbpid, oModPayRcpt.GetSetbpdesc, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Bayaran Terima...";
                oModPayRcpt = new MainModel();
                lsPayRcptLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI BAYARAN INVOIS";
            if (sPayRcptNo.Length > 0)
            {
                oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
                lsPayRcptLineItem = oMainCon.getPaymentReceiptDetailsList(sCurrComp, sPayRcptNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Bayaran Terima...";
                oModPayRcpt = new MainModel();
                lsPayRcptLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sPayRcptNo.Length > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //update Payment Receipt Header
                if (oMainCon.updatePaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Terima berjaya disimpan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Terima tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI BAYARAN INVOIS";
                    oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
                    lsPayRcptLineItem = oMainCon.getPaymentReceiptDetailsList(sCurrComp, sPayRcptNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bayaran Terima tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI BAYARAN INVOIS";
                oModPayRcpt = new MainModel();
                lsPayRcptLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sPayRcptNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPaymentReceiptDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetinvoiceno,"NEW");
                if (modExistent.GetSetpayrcptno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya ditambah. Item tersebut telah ditambah pada Bayaran Terima: " + modExistent.GetSetpayrcptno;
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPaymentReceiptDetails(oModLineItem).Equals("Y"))
                    {
                        //update payment receipt header information
                        String result = oMainCon.updatePaymentReceiptHeaderInfo(sCurrComp, sPayRcptNo);
                        sAlertMessage = "SUCCESS|Item Bayaran Terima berjaya ditambah...";
                        Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya ditambah...";
                        Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya ditambah...";
                Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sPayRcptNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.updatePaymentReceiptDetails(oModLineItem).Equals("Y"))
                {
                    //update payment receipt header information
                    String result = oMainCon.updatePaymentReceiptHeaderInfo(sCurrComp, sPayRcptNo);
                    sAlertMessage = "SUCCESS|Item Bayaran Terima berjaya dikemaskini...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya dikemaskini...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya dikemaskini...";
                Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sPayRcptNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deletePaymentReceiptDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getPaymentReceiptDetailsList(sCurrComp, sPayRcptNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deletePaymentReceiptDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertPaymentReceiptDetails(oLineItem);
                    }
                    //update payment receipt header information
                    String result = oMainCon.updatePaymentReceiptHeaderInfo(sCurrComp, sPayRcptNo);
                    sAlertMessage = "SUCCESS|Item Bayaran Terima berjaya dikeluarkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya dikeluarkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Terima tidak berjaya dikeluarkan...";
                Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sPayRcptNo.Length > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //update payment receipt header - CONFIRM
                oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
                oModPayRcpt.GetSetstatus = "CONFIRMED";
                oModPayRcpt.GetSetconfirmedby = sUserId;
                if (oMainCon.updatePaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    ArrayList lsPayRcptLineItem = oMainCon.getPaymentReceiptDetailsList(oModPayRcpt.GetSetcomp, oModPayRcpt.GetSetpayrcptno, 0, "");
                    for (int i = 0; i < lsPayRcptLineItem.Count; i++)
                    {
                        MainModel modPayRcptDet = (MainModel)lsPayRcptLineItem[i];

                        //to update Invoice & receipt Amount
                        MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(modPayRcptDet.GetSetcomp, modPayRcptDet.GetSetinvoiceno);
                        oModInvoice.GetSetpayrcptamount = oModInvoice.GetSetpayrcptamount + modPayRcptDet.GetSetpayrcptprice;
                        String result = oMainCon.updateInvoiceHeader(oModInvoice);
                    }
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Terima berjaya disahkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Terima tidak berjaya disahkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bayaran Terima tidak berjaya disahkan...";
                Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sPayRcptNo.Length > 0 && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo).GetSetstatus != "CANCELLED")
            {
                //update payment receipt header - CANCEL
                oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayRcptNo);
                oModPayRcpt.GetSetstatus = "CANCELLED";
                oModPayRcpt.GetSetcancelledby = sUserId;
                if (oMainCon.updatePaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    //update payment receipt header information
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Terima berjaya dibatalkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Terima tidak berjaya dibatalkan...";
                    Response.Redirect("PaymentReceiptDetails.aspx?action=OPEN&comp=" + sCurrComp + "&payrcptno=" + sPayRcptNo + "&alertmessage=" + sAlertMessage);
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