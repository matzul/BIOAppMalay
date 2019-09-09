using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentPaidDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sPayPaidNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModPayPaid = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsOtherBP = new ArrayList();
    public ArrayList lsPendPayPaidMod = new ArrayList();
    public ArrayList lsPayPaidLineItem = new ArrayList();
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
        if (Request.QueryString["paypaidno"] != null)
        {
            sPayPaidNo = Request.QueryString["paypaidno"].ToString();
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
            sPayPaidNo = "";
            oModPayPaid = new MainModel();
            oModPayPaid.GetSetpaypaiddate = DateTime.Now.ToString("dd-MM-yyyy");
            oModPayPaid.GetSetstatus = "NEW";
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
        if (Request.Params.Get("hidPayPaidNo") != null)
        {
            sPayPaidNo = oMainCon.replaceNull(Request.Params.Get("hidPayPaidNo"));
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
            sPayPaidNo = "";
            oModPayPaid = new MainModel();
            oModPayPaid.GetSetpaypaiddate = DateTime.Now.ToString("dd-MM-yyyy");
            oModPayPaid.GetSetstatus = "NEW";
            lsPayPaidLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModPayPaid = new MainModel();
            oModPayPaid.GetSetcomp = sCurrComp;
            oModPayPaid.GetSetpaypaiddate = oMainCon.replaceNull(Request.Params.Get("paypaiddate"));
            oModPayPaid.GetSetpaypaidtype = oMainCon.replaceNull(Request.Params.Get("paypaidtype"));
            sPayPaidNo = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");
            oModPayPaid.GetSetpaypaidno = sPayPaidNo;
            oModPayPaid.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModPayPaid.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModPayPaid.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModPayPaid.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModPayPaid.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModPayPaid.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModPayPaid.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo);
            oModPayPaid.GetSetpaypaiddate = oMainCon.replaceNull(Request.Params.Get("paypaiddate"));
            oModPayPaid.GetSetpaypaidtype = oMainCon.replaceNull(Request.Params.Get("paypaidtype"));
            oModPayPaid.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModPayPaid.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModPayPaid.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModPayPaid.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModPayPaid.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModPayPaid.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModPayPaid.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetpaypaidno = sPayPaidNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetexpensesno = oMainCon.replaceNull(Request.Params.Get("addexpensesno"));
            oModLineItem.GetSetexpensesdate = oMainCon.replaceNull(Request.Params.Get("addexpensesdate"));
            oModLineItem.GetSetexpensesprice = oMainCon.replaceDoubleZero(Request.Params.Get("addexpensesamount"));
            oModLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("addpaytype"));
            oModLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("addpayrefno"));
            oModLineItem.GetSetpaypaidprice = oMainCon.replaceDoubleZero(Request.Params.Get("addpaypaidamount"));
            oModLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("addpayremarks"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetpaypaidno = sPayPaidNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR BAYARAN BELANJA";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sPayPaidNo.Length > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //insert new Order
                if (oMainCon.insertPaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");
                    lsPayPaidLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Bayaran Belanja berjaya...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Bayaran Belanja tidak berjaya...";
                    sPayPaidNo = "";
                    oModPayPaid.GetSetpaypaidno = sPayPaidNo;
                    //not required because don't apply new entry of other BP
                    //oModPayPaid.GetSetbpid = "";
                    sAction = "ADD";
                    sActionString = "DAFTAR BAYARAN BELANJA";
                    lsPayPaidLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Bayaran Belanja tidak berjaya...";
                sPayPaidNo = "";
                oModPayPaid.GetSetpaypaidno = sPayPaidNo;
                //not required because don't apply new entry of other BP
                //oModPayPaid.GetSetbpid = "";
                sAction = "ADD";
                sActionString = "DAFTAR BAYARAN BELANJA";
                lsPayPaidLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT BAYARAN BELANJA";
            if (sPayPaidNo.Length > 0)
            {
                oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo);
                lsPayPaidLineItem = oMainCon.getPaymentPaidDetailsList(sCurrComp, sPayPaidNo, 0, "");
                lsPendPayPaidMod = oMainCon.getLineItemPendingPaymentPaid(sCurrComp, oModPayPaid.GetSetbpid, oModPayPaid.GetSetbpdesc, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Bayaran Belanja...";
                oModPayPaid = new MainModel();
                lsPayPaidLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI BAYARAN BELANJA";
            if (sPayPaidNo.Length > 0)
            {
                oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo);
                lsPayPaidLineItem = oMainCon.getPaymentPaidDetailsList(sCurrComp, sPayPaidNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Bayaran Belanja...";
                oModPayPaid = new MainModel();
                lsPayPaidLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sPayPaidNo.Length > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //update Payment Paid Header
                if (oMainCon.updatePaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Belanja berjaya disimpan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Belanja tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI BAYARAN BELANJA";
                    oModPayPaid = oMainCon.getPaymentReceiptHeaderDetails(sCurrComp, sPayPaidNo);
                    lsPayPaidLineItem = oMainCon.getPaymentReceiptDetailsList(sCurrComp, sPayPaidNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bayaran Belanja tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI BAYARAN BELANJA";
                oModPayPaid = new MainModel();
                lsPayPaidLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sPayPaidNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPaymentPaidDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetexpensesno,"NEW");
                if (modExistent.GetSetpaypaidno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya ditambah. Item tersebut telah ditambah pada Bayaran Belanja: " + modExistent.GetSetpaypaidno;
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPaymentPaidDetails(oModLineItem).Equals("Y"))
                    {
                        //update payment paid header information
                        String result = oMainCon.updatePaymentPaidHeaderInfo(sCurrComp, sPayPaidNo);
                        sAlertMessage = "SUCCESS|Item Bayaran Belanja berjaya ditambah...";
                        Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya ditambah...";
                        Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya ditambah...";
                Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sPayPaidNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.updatePaymentPaidDetails(oModLineItem).Equals("Y"))
                {
                    //update payment paid header information
                    String result = oMainCon.updatePaymentPaidHeaderInfo(sCurrComp, sPayPaidNo);
                    sAlertMessage = "SUCCESS|Item Bayaran Belanja berjaya dikemaskini...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya dikemaskini...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya dikemaskini...";
                Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sPayPaidNo.Length > 0 && oModLineItem.GetSetlineno > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deletePaymentPaidDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getPaymentPaidDetailsList(sCurrComp, sPayPaidNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deletePaymentPaidDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertPaymentPaidDetails(oLineItem);
                    }
                    //update payment paid header information
                    String result = oMainCon.updatePaymentPaidHeaderInfo(sCurrComp, sPayPaidNo);
                    sAlertMessage = "SUCCESS|Item Bayaran Belanja berjaya dikeluarkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya dikeluarkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bayaran Belanja tidak berjaya dikeluarkan...";
                Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sPayPaidNo.Length > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //update payment paid header - CONFIRM
                oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo);
                oModPayPaid.GetSetstatus = "CONFIRMED";
                oModPayPaid.GetSetconfirmedby = sUserId;
                if (oMainCon.updatePaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    ArrayList lsPayPaidLineItem = oMainCon.getPaymentPaidDetailsList(oModPayPaid.GetSetcomp, oModPayPaid.GetSetpaypaidno, 0, "");
                    for (int i = 0; i < lsPayPaidLineItem.Count; i++)
                    {
                        MainModel modPayPaidDet = (MainModel)lsPayPaidLineItem[i];

                        //to update expenses & paid Amount
                        MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(modPayPaidDet.GetSetcomp, modPayPaidDet.GetSetexpensesno);
                        oModExpenses.GetSetpaypaidamount = oModExpenses.GetSetpaypaidamount + modPayPaidDet.GetSetpaypaidprice;
                        String result = oMainCon.updateExpensesHeader(oModExpenses);
                    }
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Belanja berjaya disahkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Belanja tidak berjaya disahkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bayaran Belanja tidak berjaya disahkan...";
                Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sPayPaidNo.Length > 0 && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CONFIRMED" && oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo).GetSetstatus != "CANCELLED")
            {
                //update payment paid header - CANCEL
                oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(sCurrComp, sPayPaidNo);
                oModPayPaid.GetSetstatus = "CANCELLED";
                oModPayPaid.GetSetcancelledby = sUserId;
                if (oMainCon.updatePaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    //update payment paid header information
                    sAlertMessage = "SUCCESS|Maklumat Bayaran Belanja berjaya dibatalkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bayaran Belanja tidak berjaya dibatalkan...";
                    Response.Redirect("PaymentPaidDetails.aspx?action=OPEN&comp=" + sCurrComp + "&paypaidno=" + sPayPaidNo + "&alertmessage=" + sAlertMessage);
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