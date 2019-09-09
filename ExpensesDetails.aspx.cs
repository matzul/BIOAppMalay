using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExpensesDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sExpensesNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModExpenses = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsPayTo = new ArrayList();
    public ArrayList lsOtherPayTo = new ArrayList();
    public ArrayList lsPendExpMod = new ArrayList();
    public ArrayList lsExpensesLineItem = new ArrayList();
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
        if (Request.QueryString["expensesno"] != null)
        {
            sExpensesNo = Request.QueryString["expensesno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        //lsPayTo = oMainCon.getBPListIncludeSub(sCurrComp);
        lsPayTo = oMainCon.getBPList(sCurrComp, "", "", "");
        lsOtherPayTo = oMainCon.getOtherBPList(sCurrComp, "", "", "");

        if (sAction.Equals("ADD"))
        {
            sExpensesNo = "";
            oModExpenses = new MainModel();
            oModExpenses.GetSetexpensesdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModExpenses.GetSetstatus = "NEW";
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
        if (Request.Params.Get("hidExpensesNo") != null)
        {
            sExpensesNo = oMainCon.replaceNull(Request.Params.Get("hidExpensesNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        //lsPayTo = oMainCon.getBPListIncludeSub(sCurrComp);
        lsPayTo = oMainCon.getBPList(sCurrComp, "", "", "");
        lsOtherPayTo = oMainCon.getOtherBPList(sCurrComp, "", "", "");

        //for reset
        if (sAction.Equals("ADD"))
        {
            sExpensesNo = "";
            oModExpenses = new MainModel();
            //oModExpenses.GetSetexpensesdate = DateTime.Now.ToShortTimeString();
            oModExpenses.GetSetexpensesdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModExpenses.GetSetstatus = "NEW";
            lsExpensesLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModExpenses = new MainModel();
            oModExpenses.GetSetcomp = sCurrComp;
            oModExpenses.GetSetexpensesdate = oMainCon.replaceNull(Request.Params.Get("expensesdate"));
            oModExpenses.GetSetexpensestype = oMainCon.replaceNull(Request.Params.Get("expensestype"));
            oModExpenses.GetSetexpensescat = oMainCon.replaceNull(Request.Params.Get("expensescat"));
            sExpensesNo = oMainCon.getNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
            oModExpenses.GetSetexpensesno = sExpensesNo;
            oModExpenses.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModExpenses.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModExpenses.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModExpenses.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModExpenses.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModExpenses.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModExpenses.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
            oModExpenses.GetSetexpensesdate = oMainCon.replaceNull(Request.Params.Get("expensesdate"));
            oModExpenses.GetSetexpensestype = oMainCon.replaceNull(Request.Params.Get("expensestype"));
            oModExpenses.GetSetexpensescat = oMainCon.replaceNull(Request.Params.Get("expensescat"));
            oModExpenses.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bpid"));
            oModExpenses.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModExpenses.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModExpenses.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
            oModExpenses.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModExpenses.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModExpenses.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetexpensesno = sExpensesNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetreceiptno = oMainCon.replaceNull(Request.Params.Get("hidReceiptNo"));
            oModLineItem.GetSetreceipt_lineno = oMainCon.replaceIntZero(Request.Params.Get("hidReceiptLineNo"));
            oModLineItem.GetSetorderno = oMainCon.replaceNull(Request.Params.Get("hidOrderNo"));
            oModLineItem.GetSetorder_lineno = oMainCon.replaceIntZero(Request.Params.Get("hidOrderLineNo"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("hidItemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("addunitprice"));
            oModLineItem.GetSetdiscamount = oMainCon.replaceDoubleZero(Request.Params.Get("adddiscamount"));
            oModLineItem.GetSetquantity = oMainCon.replaceIntZero(Request.Params.Get("addquantity"));
            oModLineItem.GetSetexpensesprice = oMainCon.replaceDoubleZero(Request.Params.Get("addexpensesprice"));
            oModLineItem.GetSettaxcode = oMainCon.replaceNull(Request.Params.Get("addtaxcode"));
            oModLineItem.GetSettaxrate = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxrate"));
            oModLineItem.GetSettaxamount = oMainCon.replaceDoubleZero(Request.Params.Get("addtaxamount"));
            oModLineItem.GetSettotalexpenses = oMainCon.replaceDoubleZero(Request.Params.Get("addtotalexpenses"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetexpensesno = sExpensesNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR BIL & BELANJA";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sExpensesNo.Length > 0)
            {
                //insert new Order
                if (oMainCon.insertExpensesHeader(oModExpenses).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
                    lsExpensesLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar maklumat Bil dan Belanja berjaya...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Bil dan Belanja tidak berjaya...";
                    sExpensesNo = "";
                    oModExpenses.GetSetexpensesno = sExpensesNo;
                    oModExpenses.GetSetbpid = "";
                    sAction = "ADD";
                    sActionString = "DAFTAR BIL & BELANJA";
                    lsExpensesLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Bil dan Belanja tidak berjaya...";
                sExpensesNo = "";
                oModExpenses.GetSetexpensesno = sExpensesNo;
                oModExpenses.GetSetbpid = "";
                sAction = "ADD";
                sActionString = "DAFTAR BIL & BELANJA";
                lsExpensesLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT BIL & BELANJA";
            if (sExpensesNo.Length > 0)
            {
                oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
                lsExpensesLineItem = oMainCon.getExpensesDetailsList(sCurrComp, sExpensesNo, 0, "");
                lsPendExpMod = oMainCon.getLineItemPendingExpenses(sCurrComp, "", oModExpenses.GetSetexpensescat, oModExpenses.GetSetexpensestype);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Bil dan Belanja...";
                oModExpenses = new MainModel();
                lsExpensesLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI BIL & BELANJA";
            if (sExpensesNo.Length > 0)
            {
                oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
                lsExpensesLineItem = oMainCon.getExpensesDetailsList(sCurrComp, sExpensesNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Bil dan Belanja...";
                oModExpenses = new MainModel();
                lsExpensesLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sExpensesNo.Length > 0)
            {
                //update Expenses
                if (oMainCon.updateExpensesHeader(oModExpenses).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Bil dan Belanja berjaya disimpan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI BIL & BELANJA";
                    oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
                    lsExpensesLineItem = oMainCon.getExpensesDetailsList(sCurrComp, sExpensesNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI BIL & BELANJA";
                oModExpenses = new MainModel();
                lsExpensesLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sExpensesNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //check whether already exist in Other Line Item or not
                MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, "", "NEW");
                if (modExistent.GetSetexpensesno.Length > 0 && oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo).GetSetexpensescat.Equals("PURCHASE_INVOICE"))
                {
                    sAlertMessage = "ERROR|Item Bil dan Belanja tidak berjaya ditambah. Item tersebut telah ditambah pada Bil dan Belanja: " + modExistent.GetSetexpensesno;
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
                    {
                        //update expenses header information
                        String result = oMainCon.updateExpensesHeaderInfo(sCurrComp, sExpensesNo);
                        sAlertMessage = "SUCCESS|Item Bil dan Belanja berjaya ditambah...";
                        Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Bil dan Belanja tidak berjaya ditambah...";
                        Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bil dan Belanja tidak berjaya ditambah...";
                Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sExpensesNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item
                if (oMainCon.deleteExpensesDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getExpensesDetailsList(sCurrComp, sExpensesNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteExpensesDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertExpensesDetails(oLineItem);
                    }
                    //update expenses header information
                    String result = oMainCon.updateExpensesHeaderInfo(sCurrComp, sExpensesNo);
                    sAlertMessage = "SUCCESS|Item Bil dan Belanja berjaya dikeluarkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Bil dan Belanja tidak berjaya dikeluarkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Bil dan Belanja tidak berjaya dikeluarkan...";
                Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sExpensesNo.Length > 0)
            {
                //update expenses header - CONFIRM
                oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
                oModExpenses.GetSetstatus = "CONFIRMED";
                oModExpenses.GetSetconfirmedby = sUserId;
                if (oMainCon.updateExpensesHeader(oModExpenses).Equals("Y"))
                {
                    //update Other BP if required
                    oMainCon.updateListOtherBP(sCurrComp, oModExpenses.GetSetbpid, oModExpenses.GetSetbpdesc, oModExpenses.GetSetbpaddress, oModExpenses.GetSetbpcontact);
                    
                    if (oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE"))
                    {
                        ArrayList lsExpLineItem = oMainCon.getExpensesDetailsList(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno, 0, "");
                        for (int i = 0; i < lsExpLineItem.Count; i++)
                        {
                            MainModel modExpDet = (MainModel)lsExpLineItem[i];

                            //to update Purchase Order Expenses Amount
                            MainModel oModOrder = oMainCon.getPurchaseOrderDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetorderno, modExpDet.GetSetorder_lineno, "");
                            oModOrder.GetSetbillingamount = oModOrder.GetSetbillingamount + modExpDet.GetSettotalexpenses;
                            String result = oMainCon.updatePurchaseOrderDetails(oModOrder);
                            
                            //update status for purchase has invoice (Receipt)
                            MainModel oModReceipt = oMainCon.getReceiptDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetreceiptno, modExpDet.GetSetreceipt_lineno, "");
                            oModReceipt.GetSethasbilling = "Y";
                            result = oMainCon.updateReceiptDetails(oModReceipt);
                            
                        }
                    }
                    else if (oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE"))
                    {
                        ArrayList lsExpLineItem = oMainCon.getExpensesDetailsList(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno, 0, "");
                        for (int i = 0; i < lsExpLineItem.Count; i++)
                        {
                            MainModel modExpDet = (MainModel)lsExpLineItem[i];

                            //to update Transfer Order Expenses Amount
                            MainModel oModTranferHeader = oMainCon.getTransferOrderHeaderDetails("", "", modExpDet.GetSetcomp, modExpDet.GetSetorderno);
                            MainModel oModOrder = oMainCon.getTransferOrderDetailsDetails(oModTranferHeader.GetSetCompFromDetails.GetSetcomp, modExpDet.GetSetorderno, modExpDet.GetSetorder_lineno, "");
                            oModOrder.GetSetbillingamount = oModOrder.GetSetbillingamount + modExpDet.GetSettotalexpenses;
                            String result = oMainCon.updateTransferOrderDetails(oModOrder);

                            //update status for Transfer has invoice (Receipt)
                            MainModel oModReceipt = oMainCon.getReceiptDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetreceiptno, modExpDet.GetSetreceipt_lineno, "");
                            oModReceipt.GetSethasbilling = "Y";
                            result = oMainCon.updateReceiptDetails(oModReceipt);

                        }
                    }
                    sAlertMessage = "SUCCESS|Maklumat Bil dan Belanja berjaya disahkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya disahkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya disahkan...";
                Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sExpensesNo.Length > 0)
            {
                //update expenses header - CANCEL
                oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
                oModExpenses.GetSetstatus = "CANCELLED";
                oModExpenses.GetSetcancelledby = sUserId;
                if (oMainCon.updateExpensesHeader(oModExpenses).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Bil dan Belanja berjaya dibatalkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya dibatalkan...";
                    Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Bil dan Belanja tidak berjaya dibatalkan...";
                Response.Redirect("ExpensesDetails.aspx?action=OPEN&comp=" + sCurrComp + "&expensesno=" + sExpensesNo + "&alertmessage=" + sAlertMessage);
            }
        }
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