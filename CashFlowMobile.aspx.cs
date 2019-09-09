using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashFlowMobile : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public UserProfileModel modUserProfile = new UserProfileModel();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sCashFlowNo = "";
    public String sAlertMessage = "";
    public MainModel oModCashFlow = new MainModel();
    public ArrayList lsPayRcptHeaderDetails = new ArrayList();
    public ArrayList lsPayPaidHeaderDetails = new ArrayList();
    public String sOpeningDate = "";
    public String sClosingDate = "";
    public MainModel oModBankOfAccount = new MainModel();
    public MainModel oModCashOnHand = new MainModel();
    public MainModel oModBankDepositParam = new MainModel();
    public MainModel oModCashWithdrawalParam = new MainModel();

    //For Deposit & Withdrawal
    public MainModel oModExpenses = new MainModel();
    public MainModel oModExpLineItem = new MainModel();

    public MainModel oModPayPaid = new MainModel();
    public MainModel oModPaidLineItem = new MainModel();

    public MainModel oModInvoice = new MainModel();
    public MainModel oModInvLineItem = new MainModel();

    public MainModel oModPayRcpt = new MainModel();
    public MainModel oModRcptLineItem = new MainModel();

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
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
        }

        if (sUserId.Trim().Length == 0)
        {
            Response.Redirect("ExpiredPage.aspx");
        }

        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["cashflowno"] != null)
        {
            sCashFlowNo = Request.QueryString["cashflowno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }

    private void getValues()
    {
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
        }

        if (sUserId.Trim().Length == 0)
        {
            Response.Redirect("ExpiredPage.aspx");
        }

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidCashFlowNo") != null)
        {
            sCashFlowNo = oMainCon.replaceNull(Request.Params.Get("hidCashFlowNo"));
        }
        if (Request.Params.Get("begindate") != null)
        {
            sOpeningDate = oMainCon.replaceNull(Request.Params.Get("begindate"));
        }
        if (Request.Params.Get("closedate") != null)
        {
            sClosingDate = oMainCon.replaceNull(Request.Params.Get("closedate"));
        }

        if(sOpeningDate.Trim().Length == 0)
            sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        if (sClosingDate.Trim().Length == 0)
        {
            sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }else
        {
            if (oMainCon.compareTwoDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), sClosingDate) > 0)
            {
                sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }

        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                oModCashFlow = new MainModel();
                oModCashFlow.GetSetcomp = sCurrComp;
                oModCashFlow.GetSetcashflowno = oMainCon.getNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                oModCashFlow.GetSetopeningdate = sOpeningDate;
                oModCashFlow.GetSetopeningtype = "BEGIN";
                oModCashFlow.GetSetstatus = "IN-PROGRESS";
                oModCashFlow.GetSetcreatedby = sUserId;
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (sCashFlowNo.Length > 0)
                    oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, sCashFlowNo, "");

                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    oModCashFlow.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModCashFlow.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModCashFlow.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModCashFlow.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModCashFlow.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModCashFlow.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModCashFlow.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModCashFlow.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));
                    oModCashFlow.GetSetclosingtype = "NORMAL_CLOSE";
                    oModCashFlow.GetSetclosingdate = sClosingDate;
                    oModCashFlow.GetSetstatus = "CLOSED";
                    oModCashFlow.GetSetconfirmedby = sUserId;
                }
            }
            else if (sAction.Equals("ENDING"))
            {
                if (sCashFlowNo.Length > 0)
                    oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, sCashFlowNo, "");

                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    oModCashFlow.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModCashFlow.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModCashFlow.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModCashFlow.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModCashFlow.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModCashFlow.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModCashFlow.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModCashFlow.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));
                    oModCashFlow.GetSetclosingtype = "END";
                    oModCashFlow.GetSetclosingdate = sClosingDate;
                    oModCashFlow.GetSetstatus = "CLOSED";
                    oModCashFlow.GetSetconfirmedby = sUserId;
                }
            }
            else if (sAction.Equals("BANK_DEPOSIT"))
            {
                if (sCashFlowNo.Length > 0)
                    oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, sCashFlowNo, "");

                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    oModCashFlow.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModCashFlow.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModCashFlow.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModCashFlow.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModCashFlow.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModCashFlow.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModCashFlow.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModCashFlow.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));

                    oModExpenses = new MainModel();
                    oModExpenses.GetSetcomp = sCurrComp;
                    oModExpenses.GetSetexpensesdate = oMainCon.replaceNull(Request.Params.Get("bddate"));
                    oModExpenses.GetSetexpensescat = "JOURNAL_VOUCHER";
                    oModExpenses.GetSetexpensestype = oMainCon.replaceNull(Request.Params.Get("bdtype"));
                    oModExpenses.GetSetexpensesno = oMainCon.getNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
                    oModExpenses.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bdtobpid"));
                    oModExpenses.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bdtobpdesc"));
                    oModExpenses.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bdtobpaddress"));
                    oModExpenses.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bdtobpcontact"));
                    oModExpenses.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));
                    oModExpenses.GetSetstatus = "CONFIRMED";
                    oModExpenses.GetSetcreatedby = sUserId;
                    oModExpenses.GetSetconfirmedby = sUserId;

                    oModExpLineItem = new MainModel();
                    oModExpLineItem.GetSetcomp = oModExpenses.GetSetcomp;
                    oModExpLineItem.GetSetexpensesno = oModExpenses.GetSetexpensesno;
                    oModExpLineItem.GetSetlineno = 1;
                    oModExpLineItem.GetSetreceiptno = "";
                    oModExpLineItem.GetSetreceipt_lineno = 0;
                    oModExpLineItem.GetSetorderno = "";
                    oModExpLineItem.GetSetorder_lineno = 0;
                    oModExpLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("bditemid"));
                    oModExpLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("bddesc"));
                    oModExpLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("bdamount"));
                    oModExpLineItem.GetSetdiscamount = 0;
                    oModExpLineItem.GetSetquantity = 1;
                    oModExpLineItem.GetSetexpensesprice = oModExpLineItem.GetSetunitprice * oModExpLineItem.GetSetquantity;
                    oModExpLineItem.GetSettaxcode = "NA";
                    oModExpLineItem.GetSettaxrate = 0;
                    oModExpLineItem.GetSettaxamount = 0;
                    oModExpLineItem.GetSettotalexpenses = oModExpLineItem.GetSetexpensesprice + oModExpLineItem.GetSettaxamount;

                    //update expenses header
                    oModExpenses.GetSetexpensesamount = oModExpLineItem.GetSettotalexpenses;
                    oModExpenses.GetSettotalamount = oModExpenses.GetSetexpensesamount;
                    oModExpenses.GetSetpaypaidamount = oModExpenses.GetSettotalamount;

                    oModPayPaid = new MainModel();
                    oModPayPaid.GetSetcomp = sCurrComp;
                    oModPayPaid.GetSetpaypaiddate = oMainCon.replaceNull(Request.Params.Get("bddate"));
                    oModPayPaid.GetSetpaypaidtype = "VOUCHER";
                    oModPayPaid.GetSetpaypaidno = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");
                    oModPayPaid.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bdtobpid"));
                    oModPayPaid.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bdtobpdesc"));
                    oModPayPaid.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bdtobpaddress"));
                    oModPayPaid.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bdtobpcontact"));
                    oModPayPaid.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));
                    oModPayPaid.GetSetstatus = "CONFIRMED";
                    oModPayPaid.GetSetcreatedby = sUserId;
                    oModPayPaid.GetSetconfirmedby = sUserId;

                    oModPaidLineItem = new MainModel();
                    oModPaidLineItem.GetSetcomp = oModPayPaid.GetSetcomp;
                    oModPaidLineItem.GetSetpaypaidno = oModPayPaid.GetSetpaypaidno;
                    oModPaidLineItem.GetSetlineno = 1;
                    oModPaidLineItem.GetSetexpensesno = oModExpenses.GetSetexpensesno;
                    oModPaidLineItem.GetSetexpensesdate = oModExpenses.GetSetexpensesdate;
                    oModPaidLineItem.GetSetexpensesprice = oModExpenses.GetSettotalamount;
                    oModPaidLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("bdpaytypeout"));
                    oModPaidLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("bdpayrefno"));
                    oModPaidLineItem.GetSetpaypaidprice = oModExpenses.GetSetpaypaidamount;
                    oModPaidLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));

                    //update Paid header
                    oModPayPaid.GetSetexpensesamount = oModPaidLineItem.GetSetexpensesprice;
                    oModPayPaid.GetSetpaypaidamount = oModPaidLineItem.GetSetpaypaidprice;

                    oModInvoice = new MainModel();
                    oModInvoice.GetSetcomp = sCurrComp;
                    oModInvoice.GetSetinvoicedate = oMainCon.replaceNull(Request.Params.Get("bddate"));
                    oModInvoice.GetSetinvoicecat = "JOURNAL_VOUCHER";
                    oModInvoice.GetSetinvoicetype = oMainCon.replaceNull(Request.Params.Get("bdtype"));
                    oModInvoice.GetSetinvoiceterm = "COD";
                    oModInvoice.GetSetinvoiceno = oMainCon.getNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
                    oModInvoice.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bdfrombpid"));
                    oModInvoice.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bdfrombpdesc"));
                    oModInvoice.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bdfrombpaddress"));
                    oModInvoice.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bdfrombpcontact"));
                    oModInvoice.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));
                    oModInvoice.GetSetstatus = "CONFIRMED";
                    oModInvoice.GetSetcreatedby = sUserId;
                    oModInvoice.GetSetconfirmedby = sUserId;

                    oModInvLineItem = new MainModel();
                    oModInvLineItem.GetSetcomp = oModInvoice.GetSetcomp;
                    oModInvLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                    oModInvLineItem.GetSetlineno = 1;
                    oModInvLineItem.GetSetshipmentno = "";
                    oModInvLineItem.GetSetshipment_lineno = 0;
                    oModInvLineItem.GetSetorderno = "";
                    oModInvLineItem.GetSetorder_lineno = 1;
                    oModInvLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("bditemid"));
                    oModInvLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("bddesc"));
                    oModInvLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("bdamount"));
                    oModInvLineItem.GetSetdiscamount = 0;
                    oModInvLineItem.GetSetquantity = 1;
                    oModInvLineItem.GetSetinvoiceprice = oModInvLineItem.GetSetunitprice * oModInvLineItem.GetSetquantity;
                    oModInvLineItem.GetSettaxcode = "NA";
                    oModInvLineItem.GetSettaxrate = 0;
                    oModInvLineItem.GetSettaxamount = 0;
                    oModInvLineItem.GetSettotalinvoice = oModInvLineItem.GetSetinvoiceprice + oModInvLineItem.GetSettaxamount;

                    //update invoice header
                    oModInvoice.GetSetinvoiceamount = oModInvLineItem.GetSettotalinvoice;
                    oModInvoice.GetSettotalamount = oModInvoice.GetSetinvoiceamount;
                    oModInvoice.GetSetpayrcptamount = oModInvoice.GetSettotalamount;

                    oModPayRcpt = new MainModel();
                    oModPayRcpt.GetSetcomp = sCurrComp;
                    oModPayRcpt.GetSetpayrcptdate = oMainCon.replaceNull(Request.Params.Get("bddate")); ;
                    oModPayRcpt.GetSetpayrcpttype = "INVOICE";
                    oModPayRcpt.GetSetpayrcptno = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");
                    oModPayRcpt.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("bdfrombpid"));
                    oModPayRcpt.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bdfrombpdesc"));
                    oModPayRcpt.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bdfrombpaddress"));
                    oModPayRcpt.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bdfrombpcontact"));
                    oModPayRcpt.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));
                    oModPayRcpt.GetSetstatus = "CONFIRMED";
                    oModPayRcpt.GetSetcreatedby = sUserId;
                    oModPayRcpt.GetSetconfirmedby = sUserId;

                    oModRcptLineItem = new MainModel();
                    oModRcptLineItem.GetSetcomp = oModPayRcpt.GetSetcomp;
                    oModRcptLineItem.GetSetpayrcptno = oModPayRcpt.GetSetpayrcptno;
                    oModRcptLineItem.GetSetlineno = 1;
                    oModRcptLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                    oModRcptLineItem.GetSetinvoicedate = oModInvoice.GetSetinvoicedate;
                    oModRcptLineItem.GetSetinvoiceprice = oModInvoice.GetSettotalamount;
                    oModRcptLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("bdpaytypein"));
                    oModRcptLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("bdpayrefno"));
                    oModRcptLineItem.GetSetpayrcptprice = oModInvoice.GetSetpayrcptamount;
                    oModRcptLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("bdpayremarks"));

                    //update Rcpt header
                    oModPayRcpt.GetSetinvoiceamount = oModRcptLineItem.GetSetinvoiceprice;
                    oModPayRcpt.GetSetpayrcptamount = oModRcptLineItem.GetSetpayrcptprice;
                }
            }
            else if (sAction.Equals("CASH_WITHDRAWAL"))
            {
                if (sCashFlowNo.Length > 0)
                    oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, sCashFlowNo, "");

                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    oModCashFlow.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModCashFlow.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModCashFlow.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModCashFlow.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModCashFlow.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModCashFlow.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModCashFlow.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModCashFlow.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));

                    oModExpenses = new MainModel();
                    oModExpenses.GetSetcomp = sCurrComp;
                    oModExpenses.GetSetexpensesdate = oMainCon.replaceNull(Request.Params.Get("cwdate"));
                    oModExpenses.GetSetexpensescat = "JOURNAL_VOUCHER";
                    oModExpenses.GetSetexpensestype = oMainCon.replaceNull(Request.Params.Get("cwtype"));
                    oModExpenses.GetSetexpensesno = oMainCon.getNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
                    oModExpenses.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("cwtobpid"));
                    oModExpenses.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("cwtobpdesc"));
                    oModExpenses.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("cwtobpaddress"));
                    oModExpenses.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("cwtobpcontact"));
                    oModExpenses.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));
                    oModExpenses.GetSetstatus = "CONFIRMED";
                    oModExpenses.GetSetcreatedby = sUserId;
                    oModExpenses.GetSetconfirmedby = sUserId;

                    oModExpLineItem = new MainModel();
                    oModExpLineItem.GetSetcomp = oModExpenses.GetSetcomp;
                    oModExpLineItem.GetSetexpensesno = oModExpenses.GetSetexpensesno;
                    oModExpLineItem.GetSetlineno = 1;
                    oModExpLineItem.GetSetreceiptno = "";
                    oModExpLineItem.GetSetreceipt_lineno = 0;
                    oModExpLineItem.GetSetorderno = "";
                    oModExpLineItem.GetSetorder_lineno = 0;
                    oModExpLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("cwitemid"));
                    oModExpLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("cwdesc"));
                    oModExpLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("cwamount"));
                    oModExpLineItem.GetSetdiscamount = 0;
                    oModExpLineItem.GetSetquantity = 1;
                    oModExpLineItem.GetSetexpensesprice = oModExpLineItem.GetSetunitprice * oModExpLineItem.GetSetquantity;
                    oModExpLineItem.GetSettaxcode = "NA";
                    oModExpLineItem.GetSettaxrate = 0;
                    oModExpLineItem.GetSettaxamount = 0;
                    oModExpLineItem.GetSettotalexpenses = oModExpLineItem.GetSetexpensesprice + oModExpLineItem.GetSettaxamount;

                    //update expenses header
                    oModExpenses.GetSetexpensesamount = oModExpLineItem.GetSettotalexpenses;
                    oModExpenses.GetSettotalamount = oModExpenses.GetSetexpensesamount;
                    oModExpenses.GetSetpaypaidamount = oModExpenses.GetSettotalamount;

                    oModPayPaid = new MainModel();
                    oModPayPaid.GetSetcomp = sCurrComp;
                    oModPayPaid.GetSetpaypaiddate = oMainCon.replaceNull(Request.Params.Get("cwdate"));
                    oModPayPaid.GetSetpaypaidtype = "VOUCHER";
                    oModPayPaid.GetSetpaypaidno = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");
                    oModPayPaid.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("cwtobpid"));
                    oModPayPaid.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("cwtobpdesc"));
                    oModPayPaid.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("cwtobpaddress"));
                    oModPayPaid.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("cwtobpcontact"));
                    oModPayPaid.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));
                    oModPayPaid.GetSetstatus = "CONFIRMED";
                    oModPayPaid.GetSetcreatedby = sUserId;
                    oModPayPaid.GetSetconfirmedby = sUserId;

                    oModPaidLineItem = new MainModel();
                    oModPaidLineItem.GetSetcomp = oModPayPaid.GetSetcomp;
                    oModPaidLineItem.GetSetpaypaidno = oModPayPaid.GetSetpaypaidno;
                    oModPaidLineItem.GetSetlineno = 1;
                    oModPaidLineItem.GetSetexpensesno = oModExpenses.GetSetexpensesno;
                    oModPaidLineItem.GetSetexpensesdate = oModExpenses.GetSetexpensesdate;
                    oModPaidLineItem.GetSetexpensesprice = oModExpenses.GetSettotalamount;
                    oModPaidLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("cwpaytypeout"));
                    oModPaidLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("cwpayrefno"));
                    oModPaidLineItem.GetSetpaypaidprice = oModExpenses.GetSetpaypaidamount;
                    oModPaidLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));

                    //update Paid header
                    oModPayPaid.GetSetexpensesamount = oModPaidLineItem.GetSetexpensesprice;
                    oModPayPaid.GetSetpaypaidamount = oModPaidLineItem.GetSetpaypaidprice;

                    oModInvoice = new MainModel();
                    oModInvoice.GetSetcomp = sCurrComp;
                    oModInvoice.GetSetinvoicedate = oMainCon.replaceNull(Request.Params.Get("cwdate"));
                    oModInvoice.GetSetinvoicecat = "JOURNAL_VOUCHER";
                    oModInvoice.GetSetinvoicetype = oMainCon.replaceNull(Request.Params.Get("cwtype"));
                    oModInvoice.GetSetinvoiceterm = "COD";
                    oModInvoice.GetSetinvoiceno = oMainCon.getNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
                    oModInvoice.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("cwfrombpid"));
                    oModInvoice.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("cwfrombpdesc"));
                    oModInvoice.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("cwfrombpaddress"));
                    oModInvoice.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bdfrombpcontact"));
                    oModInvoice.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));
                    oModInvoice.GetSetstatus = "CONFIRMED";
                    oModInvoice.GetSetcreatedby = sUserId;
                    oModInvoice.GetSetconfirmedby = sUserId;

                    oModInvLineItem = new MainModel();
                    oModInvLineItem.GetSetcomp = oModInvoice.GetSetcomp;
                    oModInvLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                    oModInvLineItem.GetSetlineno = 1;
                    oModInvLineItem.GetSetshipmentno = "";
                    oModInvLineItem.GetSetshipment_lineno = 0;
                    oModInvLineItem.GetSetorderno = "";
                    oModInvLineItem.GetSetorder_lineno = 1;
                    oModInvLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("cwitemid"));
                    oModInvLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("cwdesc"));
                    oModInvLineItem.GetSetunitprice = oMainCon.replaceDoubleZero(Request.Params.Get("cwamount"));
                    oModInvLineItem.GetSetdiscamount = 0;
                    oModInvLineItem.GetSetquantity = 1;
                    oModInvLineItem.GetSetinvoiceprice = oModInvLineItem.GetSetunitprice * oModInvLineItem.GetSetquantity;
                    oModInvLineItem.GetSettaxcode = "NA";
                    oModInvLineItem.GetSettaxrate = 0;
                    oModInvLineItem.GetSettaxamount = 0;
                    oModInvLineItem.GetSettotalinvoice = oModInvLineItem.GetSetinvoiceprice + oModInvLineItem.GetSettaxamount;

                    //update invoice header
                    oModInvoice.GetSetinvoiceamount = oModInvLineItem.GetSettotalinvoice;
                    oModInvoice.GetSettotalamount = oModInvoice.GetSetinvoiceamount;
                    oModInvoice.GetSetpayrcptamount = oModInvoice.GetSettotalamount;

                    oModPayRcpt = new MainModel();
                    oModPayRcpt.GetSetcomp = sCurrComp;
                    oModPayRcpt.GetSetpayrcptdate = oMainCon.replaceNull(Request.Params.Get("cwdate")); ;
                    oModPayRcpt.GetSetpayrcpttype = "INVOICE";
                    oModPayRcpt.GetSetpayrcptno = oMainCon.getNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");
                    oModPayRcpt.GetSetbpid = oMainCon.replaceNull(Request.Params.Get("cwfrombpid"));
                    oModPayRcpt.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("cwfrombpdesc"));
                    oModPayRcpt.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("cwfrombpaddress"));
                    oModPayRcpt.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("cwfrombpcontact"));
                    oModPayRcpt.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));
                    oModPayRcpt.GetSetstatus = "CONFIRMED";
                    oModPayRcpt.GetSetcreatedby = sUserId;
                    oModPayRcpt.GetSetconfirmedby = sUserId;

                    oModRcptLineItem = new MainModel();
                    oModRcptLineItem.GetSetcomp = oModPayRcpt.GetSetcomp;
                    oModRcptLineItem.GetSetpayrcptno = oModPayRcpt.GetSetpayrcptno;
                    oModRcptLineItem.GetSetlineno = 1;
                    oModRcptLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                    oModRcptLineItem.GetSetinvoicedate = oModInvoice.GetSetinvoicedate;
                    oModRcptLineItem.GetSetinvoiceprice = oModInvoice.GetSettotalamount;
                    oModRcptLineItem.GetSetpaytype = oMainCon.replaceNull(Request.Params.Get("cwpaytypein"));
                    oModRcptLineItem.GetSetpayrefno = oMainCon.replaceNull(Request.Params.Get("cwpayrefno"));
                    oModRcptLineItem.GetSetpayrcptprice = oModInvoice.GetSetpayrcptamount;
                    oModRcptLineItem.GetSetpayremarks = oMainCon.replaceNull(Request.Params.Get("cwpayremarks"));

                    //update Rcpt header
                    oModPayRcpt.GetSetinvoiceamount = oModRcptLineItem.GetSetinvoiceprice;
                    oModPayRcpt.GetSetpayrcptamount = oModRcptLineItem.GetSetpayrcptprice;
                }
            }
        }
    }

    private void processValues()
    {
        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    if (oMainCon.insertCashFlowHeader(oModCashFlow).Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                        sAlertMessage = "SUCCESS|MULA Aliran Kewangan berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|MULA Aliran Kewangan tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|MULA Aliran Kewangan tidak berjaya...";
                }
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    if (oMainCon.updateCashFlowHeader(oModCashFlow).Equals("Y"))
                    {
                        //store details of cashflow
                        lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                        for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
                        {
                            MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];
                            MainModel oModCashFlowDet = new MainModel();
                            oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                            oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                            oModCashFlowDet.GetSetcashflowtype = "PAYMENT_RECEIPT";
                            oModCashFlowDet.GetSetpaymentno = oPayRcptDet.GetSetpaymentno;
                            oModCashFlowDet.GetSetpaymentdate = oPayRcptDet.GetSetpaymentdate;
                            oModCashFlowDet.GetSetpaymentconfirmeddate = oPayRcptDet.GetSetpaymentconfirmeddate;
                            oModCashFlowDet.GetSetpaymenttype = oPayRcptDet.GetSetpaymenttype;
                            oModCashFlowDet.GetSetbpid = oPayRcptDet.GetSetbpid;
                            oModCashFlowDet.GetSetbpdesc = oPayRcptDet.GetSetbpdesc;
                            oModCashFlowDet.GetSetpaydetno = oPayRcptDet.GetSetpaydetno;
                            oModCashFlowDet.GetSetlineno = oPayRcptDet.GetSetlineno;
                            oModCashFlowDet.GetSetpaytype = oPayRcptDet.GetSetpaytype;
                            oModCashFlowDet.GetSetpayrefno = oPayRcptDet.GetSetpayrefno;
                            oModCashFlowDet.GetSetpayremarks = oPayRcptDet.GetSetpayremarks;
                            oModCashFlowDet.GetSetpayamount = oPayRcptDet.GetSetpayamount;
                            String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                        }

                        lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                        for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
                        {
                            MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];
                            MainModel oModCashFlowDet = new MainModel();
                            oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                            oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                            oModCashFlowDet.GetSetcashflowtype = "PAYMENT_PAID";
                            oModCashFlowDet.GetSetpaymentno = oPayPaidDet.GetSetpaymentno;
                            oModCashFlowDet.GetSetpaymentdate = oPayPaidDet.GetSetpaymentdate;
                            oModCashFlowDet.GetSetpaymentconfirmeddate = oPayPaidDet.GetSetpaymentconfirmeddate;
                            oModCashFlowDet.GetSetpaymenttype = oPayPaidDet.GetSetpaymenttype;
                            oModCashFlowDet.GetSetbpid = oPayPaidDet.GetSetbpid;
                            oModCashFlowDet.GetSetbpdesc = oPayPaidDet.GetSetbpdesc;
                            oModCashFlowDet.GetSetpaydetno = oPayPaidDet.GetSetpaydetno;
                            oModCashFlowDet.GetSetlineno = oPayPaidDet.GetSetlineno;
                            oModCashFlowDet.GetSetpaytype = oPayPaidDet.GetSetpaytype;
                            oModCashFlowDet.GetSetpayrefno = oPayPaidDet.GetSetpayrefno;
                            oModCashFlowDet.GetSetpayremarks = oPayPaidDet.GetSetpayremarks;
                            oModCashFlowDet.GetSetpayamount = oPayPaidDet.GetSetpayamount;
                            String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                        }

                        //open new cash flow
                        MainModel oModNewCashFlow = new MainModel();
                        oModNewCashFlow.GetSetcomp = sCurrComp;
                        oModNewCashFlow.GetSetcashflowno = oMainCon.getNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                        String nextOpeningDate = oMainCon.getNextSecond(oModCashFlow.GetSetclosingdate, 1);
                        oModNewCashFlow.GetSetopeningdate = nextOpeningDate;
                        oModNewCashFlow.GetSetopeningtype = "NORMAL_OPEN";
                        oModNewCashFlow.GetSetbankopeningamount = oModCashFlow.GetSetbankclosingamount;
                        oModNewCashFlow.GetSetcashopeningamount = oModCashFlow.GetSetcashclosingamount;
                        oModNewCashFlow.GetSetstatus = "IN-PROGRESS";
                        oModNewCashFlow.GetSetcreatedby = sUserId;
                        if (oModNewCashFlow.GetSetcashflowno.Length > 0)
                        {
                            if (oMainCon.insertCashFlowHeader(oModNewCashFlow).Equals("Y"))
                            {
                                oMainCon.updateNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                                sAlertMessage = "SUCCESS|TUTUP dan BUKA Aliran Kewangan seterusnya berjaya...";
                            }
                            else
                            {
                                sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                            }
                        }
                        else
                        {
                            sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                }
            }
            else if (sAction.Equals("ENDING"))
            {
                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    if (oMainCon.updateCashFlowHeader(oModCashFlow).Equals("Y"))
                    {
                        //store details of cashflow
                        lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                        for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
                        {
                            MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];
                            MainModel oModCashFlowDet = new MainModel();
                            oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                            oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                            oModCashFlowDet.GetSetcashflowtype = "PAYMENT_RECEIPT";
                            oModCashFlowDet.GetSetpaymentno = oPayRcptDet.GetSetpaymentno;
                            oModCashFlowDet.GetSetpaymentdate = oPayRcptDet.GetSetpaymentdate;
                            oModCashFlowDet.GetSetpaymentconfirmeddate = oPayRcptDet.GetSetpaymentconfirmeddate;
                            oModCashFlowDet.GetSetpaymenttype = oPayRcptDet.GetSetpaymenttype;
                            oModCashFlowDet.GetSetbpid = oPayRcptDet.GetSetbpid;
                            oModCashFlowDet.GetSetbpdesc = oPayRcptDet.GetSetbpdesc;
                            oModCashFlowDet.GetSetpaydetno = oPayRcptDet.GetSetpaydetno;
                            oModCashFlowDet.GetSetlineno = oPayRcptDet.GetSetlineno;
                            oModCashFlowDet.GetSetpaytype = oPayRcptDet.GetSetpaytype;
                            oModCashFlowDet.GetSetpayrefno = oPayRcptDet.GetSetpayrefno;
                            oModCashFlowDet.GetSetpayremarks = oPayRcptDet.GetSetpayremarks;
                            oModCashFlowDet.GetSetpayamount = oPayRcptDet.GetSetpayamount;
                            String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                        }

                        lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                        for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
                        {
                            MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];
                            MainModel oModCashFlowDet = new MainModel();
                            oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                            oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                            oModCashFlowDet.GetSetcashflowtype = "PAYMENT_PAID";
                            oModCashFlowDet.GetSetpaymentno = oPayPaidDet.GetSetpaymentno;
                            oModCashFlowDet.GetSetpaymentdate = oPayPaidDet.GetSetpaymentdate;
                            oModCashFlowDet.GetSetpaymentconfirmeddate = oPayPaidDet.GetSetpaymentconfirmeddate;
                            oModCashFlowDet.GetSetpaymenttype = oPayPaidDet.GetSetpaymenttype;
                            oModCashFlowDet.GetSetbpid = oPayPaidDet.GetSetbpid;
                            oModCashFlowDet.GetSetbpdesc = oPayPaidDet.GetSetbpdesc;
                            oModCashFlowDet.GetSetpaydetno = oPayPaidDet.GetSetpaydetno;
                            oModCashFlowDet.GetSetlineno = oPayPaidDet.GetSetlineno;
                            oModCashFlowDet.GetSetpaytype = oPayPaidDet.GetSetpaytype;
                            oModCashFlowDet.GetSetpayrefno = oPayPaidDet.GetSetpayrefno;
                            oModCashFlowDet.GetSetpayremarks = oPayPaidDet.GetSetpayremarks;
                            oModCashFlowDet.GetSetpayamount = oPayPaidDet.GetSetpayamount;
                            String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                        }
                        sAlertMessage = "SUCCESS|AKHIR Aliran Kewangan berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|AKHIR Aliran Kewangan tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|AKHIR Aliran Kewangan tidak berjaya...";
                }
            }
            else if (sAction.Equals("BANK_DEPOSIT"))
            {
                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    if (oModExpenses.GetSettotalamount <= oModCashFlow.GetSetcashclosingamount)
                    {
                        if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModPayPaid.GetSetpaypaidno.Trim().Length > 0 && oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModPayRcpt.GetSetpayrcptno.Trim().Length > 0)
                        {
                            String sResult = oMainCon.insertExpensesHeader(oModExpenses);
                            sResult = oMainCon.insertExpensesDetails(oModExpLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
                            sResult = oMainCon.insertPaymentPaidHeader(oModPayPaid);
                            sResult = oMainCon.insertPaymentPaidDetails(oModPaidLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");

                            sResult = oMainCon.insertInvoiceHeader(oModInvoice);
                            sResult = oMainCon.insertInvoiceDetails(oModInvLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
                            sResult = oMainCon.insertPaymentReceiptHeader(oModPayRcpt);
                            sResult = oMainCon.insertPaymentReceiptDetails(oModRcptLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");

                            sAlertMessage = "SUCCESS|Deposit Tunai ke Bank berjaya...";
                            sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        }
                        else
                        {
                            sAlertMessage = "ERROR|Deposit Tunai ke Bank tidak berjaya <system running number error>...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Jumlah Penutupan Tunai kurang dari Tunai untuk didepositkan...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Deposit Tunai ke Bank tidak berjaya <system cash flow data error>...";
                }
            }
            else if (sAction.Equals("CASH_WITHDRAWAL"))
            {
                if (oModCashFlow.GetSetcashflowno.Length > 0)
                {
                    if (oModExpenses.GetSettotalamount <= oModCashFlow.GetSetbankclosingamount)
                    {
                        if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModPayPaid.GetSetpaypaidno.Trim().Length > 0 && oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModPayRcpt.GetSetpayrcptno.Trim().Length > 0)
                        {
                            String sResult = oMainCon.insertExpensesHeader(oModExpenses);
                            sResult = oMainCon.insertExpensesDetails(oModExpLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "EXPENSES", "ACTIVE");
                            sResult = oMainCon.insertPaymentPaidHeader(oModPayPaid);
                            sResult = oMainCon.insertPaymentPaidDetails(oModPaidLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_PAID", "ACTIVE");

                            sResult = oMainCon.insertInvoiceHeader(oModInvoice);
                            sResult = oMainCon.insertInvoiceDetails(oModInvLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "INVOICE", "ACTIVE");
                            sResult = oMainCon.insertPaymentReceiptHeader(oModPayRcpt);
                            sResult = oMainCon.insertPaymentReceiptDetails(oModRcptLineItem);
                            oMainCon.updateNextRunningNo(sCurrComp, "PAYMENT_RECEIPT", "ACTIVE");

                            sAlertMessage = "SUCCESS|Withdraw Tunai dari Bank berjaya...";
                            sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        }
                        else
                        {
                            sAlertMessage = "ERROR|Withdraw Tunai dari Bank tidak berjaya <system running number error>...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Jumlah Penutupan Bank kurang dari Tunai untuk dikeluarkan...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Withdraw Tunai dari Bank tidak berjaya <system cash flow data error>...";
                }
            }
        }
        else
        {
            sAlertMessage = "ERROR|Tarikh Tutup mesti lewat dari Tarikh Buka...";
        }        

        //to refresh get cash flow details
        if(sCashFlowNo.Length > 0)
            oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp,sCashFlowNo,"");
        else
            oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, "", "IN-PROGRESS");

        if(oModCashFlow.GetSetcashflowno.Length > 0 && oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
        {
            lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
            lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
        }
        else if (oModCashFlow.GetSetcashflowno.Length > 0 && oModCashFlow.GetSetstatus.Equals("CLOSED"))
        {
            lsPayRcptHeaderDetails = oMainCon.getCashFlowDetailsList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetcashflowno, "PAYMENT_RECEIPT");
            lsPayPaidHeaderDetails = oMainCon.getCashFlowDetailsList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetcashflowno, "PAYMENT_PAID");
        }
        
        //to refresh info data of bank deposit & cash withdrawal
        oModBankOfAccount = oMainCon.getBPDetails(sCurrComp, "BP000002", "");
        oModCashOnHand = oMainCon.getBPDetails(sCurrComp, "BP000001", "");
        oModBankDepositParam = oMainCon.getParamDetails("000", "", "BANK_DEPOSIT", "INTERNAL0101");
        oModCashWithdrawalParam = oMainCon.getParamDetails("000", "", "CASH_WITHDRAWAL", "INTERNAL0102");
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