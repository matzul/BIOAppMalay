using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BukuTunai : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public MainModel modCompInfo = new MainModel();

    public String sCurrComp = "";
    public String sCurrUserid = "";
    public String sCurrFyr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
            initialValues();
            processValues();
    }

    private void initialValues()
    {
        if (Session["comp"] != null)
        {
            sCurrComp = oMainCon.replaceNull(Session["comp"].ToString());
        }
        if (Session["userid"] != null)
        {
            sCurrUserid = oMainCon.replaceNull(Session["comp"].ToString());
        }
        if (Session["fyr"] != null)
        {
            sCurrFyr = oMainCon.replaceNull(Session["fyr"].ToString());
        }
    }
    private void processValues()
    {

    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_YearMonthList(String comp)
    {
        MainController oMainCon = new MainController();
        String sStatus = "Y";
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String currYear = DateTime.Now.ToString("yyyy");
        String currMonth = DateTime.Now.ToString("MM");
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }
        if (HttpContext.Current.Session["fyr"] != null)
        {
            currYear = oMainCon.replaceNull(HttpContext.Current.Session["fyr"].ToString());
        }

        ArrayList lsYear = new ArrayList();
        object objYear = new { yearid = "2018", yearval = "2018" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2019", yearval = "2019" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2020", yearval = "2020" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2021", yearval = "2021" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2022", yearval = "2022" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2023", yearval = "2023" };
        lsYear.Add(objYear);

        ArrayList lsMonth = new ArrayList();
        object objMonth = new { monthid = "", monthval = "-Bulan-" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "01", monthval = "Januari" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "02", monthval = "Februari" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "03", monthval = "Mac" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "04", monthval = "April" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "05", monthval = "Mei" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "06", monthval = "Jun" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "07", monthval = "Julai" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "08", monthval = "Ogos" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "09", monthval = "September" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "10", monthval = "Oktober" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "11", monthval = "November" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "12", monthval = "Disember" };
        lsMonth.Add(objMonth);

        object objYearMonthList = new { result=sStatus ,currentyear = currYear, currentmonth = currMonth, arrayyear = lsYear, arraymonth = lsMonth };

        //jsonResponse = convertJson(objYearMonthList);
        jsonResponse = new JavaScriptSerializer().Serialize(objYearMonthList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_PaymentReportSummary(String comp, String selectyear, String selectmonth, String selectday)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        double totalcashopeningamount = 0;
        double totalcashpayrcptamount = 0;
        double totalcashpaypaidamount = 0;
        double totalcashclosingamount = 0;


            if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
            {
                sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
                int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
                sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
                sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
            }

            MainModel modLastCashFlow = oMainCon.getCashFlowLastHeaderDetails(comp, sDateFrom, "CLOSED");

            MainModel modCashFlow = new MainModel();
            modCashFlow.GetSetcomp = comp;
            modCashFlow.GetSetopeningdate = sDateFrom;
            modCashFlow.GetSetbankopeningamount = modLastCashFlow.GetSetbankclosingamount;
            modCashFlow.GetSetcashopeningamount = modLastCashFlow.GetSetcashclosingamount;

            ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
            for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
            {
                MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

                modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
            }

            ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
            for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
            {
                MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

                modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
            }

            ArrayList lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
            for (int i = 0; i < lsPaymentReceipt.Count; i++)
            {
                MainModel oPayRcptDet = (MainModel)lsPaymentReceipt[i];

                modCashFlow.GetSetbankpaymentreceiptamount = modCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                modCashFlow.GetSetcashpaymentreceiptamount = modCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
            }

            ArrayList lsPaymentPaid = oMainCon.getPaymentPaidCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
            for (int i = 0; i < lsPaymentPaid.Count; i++)
            {
                MainModel oPayPaidDet = (MainModel)lsPaymentPaid[i];

                modCashFlow.GetSetbankpaymentpaidamount = modCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                modCashFlow.GetSetcashpaymentpaidamount = modCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
            }

            modCashFlow.GetSetbankclosingamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetbankpaymentreceiptamount - modCashFlow.GetSetbankpaymentpaidamount;
            modCashFlow.GetSetcashclosingamount = modCashFlow.GetSetcashopeningamount + modCashFlow.GetSetcashpaymentreceiptamount - modCashFlow.GetSetcashpaymentpaidamount;
            modCashFlow.GetSetclosingdate = sDateTo;

            totalcashopeningamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetcashopeningamount;
            totalcashpayrcptamount = modCashFlow.GetSetbankpaymentreceiptamount + modCashFlow.GetSetcashpaymentreceiptamount;
            totalcashpaypaidamount = modCashFlow.GetSetbankpaymentpaidamount + modCashFlow.GetSetcashpaymentpaidamount;
            totalcashclosingamount = modCashFlow.GetSetbankclosingamount + modCashFlow.GetSetcashclosingamount;

            object objStockReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalcashopeningamount = totalcashopeningamount, totalcashpayrcptamount = totalcashpayrcptamount, totalcashpaypaidamount = totalcashpaypaidamount * -1, totalcashclosingamount = totalcashclosingamount, arraypaymentreceipt = lsPaymentReceipt, arraypaymentpaid = lsPaymentPaid };

        jsonResponse = convertJson(objStockReportSummary);

        //jsonResponse = new JavaScriptSerializer().Serialize(objStockReportSummary);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_PaymentReportSummary2(String comp, String selectyear, String selectmonth, String selectday)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        double totalcashopeningamount = 0;
        double totalcashpayrcptamount = 0;
        double totalcashpaypaidamount = 0;
        double totalcashclosingamount = 0;


        if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
        {
            sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
            sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
        {
            sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
            DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
            int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
            sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
        {
            sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
            sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
        }

        MainModel modLastCashFlow = oMainCon.getCashFlowLastHeaderDetails(comp, sDateFrom, "CLOSED");

        MainModel modCashFlow = new MainModel();
        modCashFlow.GetSetcomp = comp;
        modCashFlow.GetSetopeningdate = sDateFrom;
        modCashFlow.GetSetbankopeningamount = modLastCashFlow.GetSetbankclosingamount;
        modCashFlow.GetSetcashopeningamount = modLastCashFlow.GetSetcashclosingamount;

        ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
        for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
        {
            MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

            modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
            modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
        }

        ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
        for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
        {
            MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

            modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
            modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
        }

        ArrayList lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
        for (int i = 0; i < lsPaymentReceipt.Count; i++)
        {
            MainModel oPayRcptDet = (MainModel)lsPaymentReceipt[i];

            modCashFlow.GetSetbankpaymentreceiptamount = modCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
            modCashFlow.GetSetcashpaymentreceiptamount = modCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
        }

        ArrayList lsPaymentPaid = oMainCon.getPaymentPaidCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
        for (int i = 0; i < lsPaymentPaid.Count; i++)
        {
            MainModel oPayPaidDet = (MainModel)lsPaymentPaid[i];

            modCashFlow.GetSetbankpaymentpaidamount = modCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
            modCashFlow.GetSetcashpaymentpaidamount = modCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
        }

        modCashFlow.GetSetbankclosingamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetbankpaymentreceiptamount - modCashFlow.GetSetbankpaymentpaidamount;
        modCashFlow.GetSetcashclosingamount = modCashFlow.GetSetcashopeningamount + modCashFlow.GetSetcashpaymentreceiptamount - modCashFlow.GetSetcashpaymentpaidamount;
        modCashFlow.GetSetclosingdate = sDateTo;

        totalcashopeningamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetcashopeningamount;
        totalcashpayrcptamount = modCashFlow.GetSetbankpaymentreceiptamount + modCashFlow.GetSetcashpaymentreceiptamount;
        totalcashpaypaidamount = modCashFlow.GetSetbankpaymentpaidamount + modCashFlow.GetSetcashpaymentpaidamount;
        totalcashclosingamount = modCashFlow.GetSetbankclosingamount + modCashFlow.GetSetcashclosingamount;

        object objStockReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalcashopeningamount = totalcashopeningamount, totalcashpayrcptamount = totalcashpayrcptamount, totalcashpaypaidamount = totalcashpaypaidamount * -1, totalcashclosingamount = totalcashclosingamount };
        
        jsonResponse = convertJson(objStockReportSummary);

        //jsonResponse = new JavaScriptSerializer().Serialize(objStockReportSummary);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_ListCashInOut(String comp, String selectyear, String selectmonth, String selectday)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        ArrayList lsInvoiceHeader = new ArrayList();

            if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
            {
                sDateFrom = selectyear + "-" + selectmonth + "-" + selectday + " 00:00:00";
                sDateTo = selectyear + "-" + selectmonth + "-" + selectday + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
            {
                sDateFrom = selectyear + "-" + selectmonth + "-" + "01" + " 00:00:00";

                DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
                int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
                sDateTo = selectyear + "-" + selectmonth + "-" + maxdt + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
            {
                sDateFrom = selectyear + "-" + "01" + "-" + "01" + " 00:00:00";
                sDateTo = selectyear + "-" + "12" + "-" + "31" + " 23:59:59";
            }

            lsInvoiceHeader = oMainCon.getCashInOut(comp, sDateFrom, sDateTo);

        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsInvoiceHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_ListCashInOut2(String comp, String selectyear, String selectmonth, String selectday)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        ArrayList lsInvoiceHeader = new ArrayList();

        if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
        {
            sDateFrom = selectyear + "-" + selectmonth + "-" + selectday + " 00:00:00";
            sDateTo = selectyear + "-" + selectmonth + "-" + selectday + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
        {
            sDateFrom = selectyear + "-" + selectmonth + "-" + "01" + " 00:00:00";

            DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
            int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
            sDateTo = selectyear + "-" + selectmonth + "-" + maxdt + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
        {
            sDateFrom = selectyear + "-" + "01" + "-" + "01" + " 00:00:00";
            sDateTo = selectyear + "-" + "12" + "-" + "31" + " 23:59:59";
        }

        lsInvoiceHeader = oMainCon.getCashInOut2(comp, sDateFrom, sDateTo);

        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsInvoiceHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_IncomeDetails(String comp, String paypaidno, String receiptno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modInvoiceHeaderDetails = new MainModel();

        modInvoiceHeaderDetails = oMainCon.getIncomeDetails(comp, paypaidno, receiptno);

        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(modInvoiceHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }


    [WebMethod(EnableSession = true)]
    public static string getMobile_ExpensesDetails2(String comp, String paypaidno, String receiptno)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modExpensesHeaderDetails = new MainModel();


        modExpensesHeaderDetails = oMainCon.getExpensesHeaderDetails2(comp, paypaidno, receiptno);

        
        //jsonResponse = new JavaScriptSerializer().Serialize(modExpensesHeaderDetails);
        jsonResponse = convertJson(modExpensesHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_parametertype(String paramtcategory)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendInvoiceList = new ArrayList();


        lsPendInvoiceList = oMainCon.getParametertype(paramtcategory,"ACTIVE");

        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendInvoiceList);
        jsonResponse = convertJson(lsPendInvoiceList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_PendingExpensesList(String comp, String bpid, String expensescat, String expensestype)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendExpensesList = new ArrayList();

        lsPendExpensesList = oMainCon.getLineItemPendingExpenses(comp, bpid, "", expensescat, expensestype, "");

        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendExpensesList);
        jsonResponse = convertJson(lsPendExpensesList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_PendingInvoiceList(String comp, String bpid, String invoicecat, String invoicetype)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendInvoiceList = new ArrayList();

            lsPendInvoiceList = oMainCon.getLineItemPendingInvoice(comp, bpid, "", invoicecat, invoicetype, "");
       
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendInvoiceList);
        jsonResponse = convertJson(lsPendInvoiceList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }


    [WebMethod(EnableSession = true)]
    public static string createMobile_InvoiceInvoice2(String comp, String bpid, double inc_amount, String invoicecat, String inc_type, String inc_remarks, String datecreation, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        MainController oMainCon = new MainController();

        //get comp bp if exist
        MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
        if (oModBP.GetSetbpid.Trim().Length > 0)
        {
            MainModel oModInvoice = new MainModel();
            oModInvoice.GetSetcomp = comp;
            oModInvoice.GetSetinvoicedate = datecreation;
            oModInvoice.GetSetinvoicecat = invoicecat;
            oModInvoice.GetSetinvoicetype = inc_type;
            oModInvoice.GetSetinvoiceterm = "NOT_APPLICABLE";
            String sInvoiceNo = oMainCon.getNextRunningNo(comp, "INVOICE", "ACTIVE");
            oModInvoice.GetSetinvoiceno = sInvoiceNo;
            oModInvoice.GetSetbpid = oModBP.GetSetbpid;
            oModInvoice.GetSetbpdesc = oModBP.GetSetbpdesc;
            oModInvoice.GetSetbpaddress = oModBP.GetSetbpaddress;
            oModInvoice.GetSetbpcontact = oModBP.GetSetbpcontact;
            oModInvoice.GetSetsalesamount = inc_amount;
            oModInvoice.GetSetinvoiceamount = inc_amount;
            oModInvoice.GetSettotalamount = inc_amount;
            oModInvoice.GetSetpayrcptamount = inc_amount;
            oModInvoice.GetSetremarks = inc_remarks;
            oModInvoice.GetSetstatus = "CONFIRMED";
            oModInvoice.GetSetcreatedby = userid;
            oModInvoice.GetSetcreateddate = datecreation;
            oModInvoice.GetSetconfirmedby = userid;
            oModInvoice.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertInvoiceHeader2(oModInvoice).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(oModInvoice.GetSetcomp, "INVOICE", "ACTIVE");
                sStatus = "Y";
                sMessage = oModInvoice.GetSetinvoiceno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string addMobile_InvoiceItemDetails2(String comp, String invoiceno, String inc_item, String inc_itemdesc, double inc_amount)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        MainModel oModLineItem = new MainModel();
        oModLineItem.GetSetcomp = comp;
        oModLineItem.GetSetinvoiceno = invoiceno;
        oModLineItem.GetSetlineno = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "").Count + 1;
        oModLineItem.GetSetitemno = inc_item;
        oModLineItem.GetSetitemdesc = inc_itemdesc;
        oModLineItem.GetSetunitprice = inc_amount;
        oModLineItem.GetSetquantity = 1;
        oModLineItem.GetSetinvoiceprice = inc_amount;
        oModLineItem.GetSettaxcode = "N/A";
        oModLineItem.GetSettotalinvoice = inc_amount;

        MainModel modExistent = oMainCon.getInvoiceDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetshipmentno, oModLineItem.GetSetshipment_lineno, inc_item, "NEW");
        if (modExistent.GetSetinvoiceno.Length > 0)
        {
            sStatus = "N";
            sMessage = "Invoice item already assigned, Please contact System Admin!";
        }
        else
        {
            //insert new line item
            if (oMainCon.insertInvoiceDetails(oModLineItem).Equals("Y"))
            {
                //update invoice header information
                sStatus = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);
                sMessage = "";
            }
            else
            {
                sStatus = "N";
                sMessage = "Unable to add invoice item, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }


    [WebMethod(EnableSession = true)]
    public static string createMobile_PayRcptInvoice2(String comp, String invoiceno, String datecreation, double inc_amount, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get invoice header if exist
        MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
        if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetpayrcptdate = datecreation;
            oModHeader.GetSetpayrcpttype = "INVOICE";
            String sPayRcptNo = oMainCon.getNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
            oModHeader.GetSetpayrcptno = sPayRcptNo;
            oModHeader.GetSetbpid = oModInvoice.GetSetbpid;
            oModHeader.GetSetbpdesc = oModInvoice.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModInvoice.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModInvoice.GetSetbpcontact;
            oModHeader.GetSetinvoiceamount = inc_amount;
            oModHeader.GetSetpayrcptamount = inc_amount;
            oModHeader.GetSetremarks = oModInvoice.GetSetremarks;
            oModHeader.GetSetstatus = "CONFIRMED";
            oModHeader.GetSetcreatedby = userid;
            oModHeader.GetSetcreateddate = datecreation;
            oModHeader.GetSetconfirmedby = userid;
            oModHeader.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertPaymentReceiptHeader2(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetpayrcptno;
            }
        }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string addMobile_PayRcptItemInvoice2(String comp, String payrcptno, String invoiceno, String datecreation, double inc_amount, String inc_paytype)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get invoice header if exist
        MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
        if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetpayrcptno = payrcptno;
            oModLineItem.GetSetlineno = 1;
            oModLineItem.GetSetinvoiceno = invoiceno;
            oModLineItem.GetSetinvoicedate = datecreation;
            oModLineItem.GetSetinvoiceprice = inc_amount;
            oModLineItem.GetSetpaytype = inc_paytype;
            oModLineItem.GetSetpayrcptprice = inc_amount;

            //check whether already exist in Other Line Item that is not confirm yet or not
            MainModel modExistent = oMainCon.getPaymentReceiptDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetinvoiceno, "NEW");
            if (modExistent.GetSetpayrcptno.Length > 0)
            {
                sStatus = "Y";
                sMessage = "Item tersebut telah ditambah pada Bayaran Terima: " + modExistent.GetSetpayrcptno;
            }
            else
            {
                //insert new line item
                if (oMainCon.insertPaymentReceiptDetails(oModLineItem).Equals("Y"))
                {
                    //update payment receipt header information
                    sStatus = oMainCon.updatePaymentReceiptHeaderInfo(comp, payrcptno);
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string getMobile_BPList(String comp, String bpid, String bpdesc, String bpcat, String solidbp)
    {
        MainController oMainCon = new MainController();

        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsBPList = new ArrayList();

        lsBPList = oMainCon.getBPList(comp, bpid, bpdesc, bpcat, solidbp);

        jsonResponse = convertJson(lsBPList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsBPList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string createMobile_ExpensesHeader2(String comp, String bpid, String expensescat, String expensestype, Double exp_amount, String exp_remarks, String datecreation, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get comp bp if exist
        MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
        if (oModBP.GetSetbpid.Trim().Length > 0)
        {
            MainModel oModExpenses = new MainModel();
            oModExpenses.GetSetcomp = comp;
            oModExpenses.GetSetexpensesdate = datecreation;
            oModExpenses.GetSetexpensescat = expensescat;
            oModExpenses.GetSetexpensestype = expensestype;
            String sExpensesNo = oMainCon.getNextRunningNo(comp, "EXPENSES", "ACTIVE");
            oModExpenses.GetSetexpensesno = sExpensesNo;
            oModExpenses.GetSetbpid = oModBP.GetSetbpid;
            oModExpenses.GetSetbpdesc = oModBP.GetSetbpdesc;
            oModExpenses.GetSetbpaddress = oModBP.GetSetbpaddress;
            oModExpenses.GetSetbpcontact = oModBP.GetSetbpcontact;
            oModExpenses.GetSetremarks = exp_remarks;
            oModExpenses.GetSetstatus = "CONFIRMED";
            oModExpenses.GetSetexpensesamount = exp_amount;
            oModExpenses.GetSettotalamount = exp_amount;
            oModExpenses.GetSetpaypaidamount = exp_amount;
            oModExpenses.GetSetpurchaseamount = exp_amount;
            oModExpenses.GetSetcreatedby = userid;
            oModExpenses.GetSetcreateddate = datecreation;
            oModExpenses.GetSetconfirmedby = userid;
            oModExpenses.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertExpensesHeader2(oModExpenses).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(oModExpenses.GetSetcomp, "EXPENSES", "ACTIVE");
                sStatus = "Y";
                sMessage = oModExpenses.GetSetexpensesno;
            }
        }
        
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string addMobile_ExpensesReceiptItemDetails2(String comp, String expensesno, String expensescat, String exp_item, String exp_itemdesc, Double exp_amount, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        MainModel oModLineItem = new MainModel();
        oModLineItem.GetSetcomp = comp;
        oModLineItem.GetSetexpensesno = expensesno;
        oModLineItem.GetSetlineno = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "").Count + 1;
        oModLineItem.GetSetitemno = exp_item;
        oModLineItem.GetSetitemdesc = exp_itemdesc;
        oModLineItem.GetSetunitprice = exp_amount;
        oModLineItem.GetSetdiscamount = 0;
        oModLineItem.GetSetquantity = 1;
        oModLineItem.GetSettaxcode = "N/A";
        oModLineItem.GetSetexpensesprice = exp_amount;
        oModLineItem.GetSettotalexpenses = exp_amount;

        MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, exp_item, "NEW");
        if (modExistent.GetSetinvoiceno.Length > 0)
        {
            sStatus = "N";
            sMessage = "Expenses item already assigned, Please contact System Admin!";
        }
        else
        {
            //insert new line item
            if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
            {
                //update expenses header information
                sStatus = oMainCon.updateExpensesHeaderInfo(comp, expensesno);
                sMessage = "";
            }
            else
            {
                sStatus = "N";
                sMessage = "Unable to add expenses item, Please contact System Admin!";
            }
        }        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string createMobile_PayPaidExpenses2(String comp, String expensesno, Double exp_amount, String datecreation, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        //get expenses header if exist
        MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
        if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetpaypaiddate = datecreation;
            oModHeader.GetSetpaypaidtype = "EXPENSES";
            String sPayPaidNo = oMainCon.getNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
            oModHeader.GetSetpaypaidno = sPayPaidNo;
            oModHeader.GetSetbpid = oModExpenses.GetSetbpid;
            oModHeader.GetSetbpdesc = oModExpenses.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModExpenses.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModExpenses.GetSetbpcontact;
            oModHeader.GetSetremarks = oModExpenses.GetSetremarks;
            oModHeader.GetSetstatus = "CONFIRMED";
            oModHeader.GetSetexpensesamount = exp_amount;
            oModHeader.GetSetpaypaidamount = exp_amount;
            oModHeader.GetSetcreatedby = userid;
            oModHeader.GetSetcreateddate = datecreation;
            oModHeader.GetSetconfirmedby = userid;
            oModHeader.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertPaymentPaidHeader2(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetpaypaidno;
            }
        }
        
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string addMobile_PayPaidItemExpenses2(String comp, String paypaidno, String expensesno, Double exp_amount, String exp_paytype, String datecreation)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


            //get expenses header if exist
            MainModel oModExpenses = (MainModel)oMainCon.getExpensesHeaderDetails(comp, expensesno);
            if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetpaypaidno = paypaidno;
                oModLineItem.GetSetlineno = 1;
                oModLineItem.GetSetexpensesno = expensesno;
                oModLineItem.GetSetexpensesdate = datecreation;
                oModLineItem.GetSetexpensesprice = exp_amount;
                oModLineItem.GetSetpaytype = exp_paytype;
                oModLineItem.GetSetpayrefno = "";
                oModLineItem.GetSetpaypaidprice = exp_amount;
                oModLineItem.GetSetpayremarks = "";

                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPaymentPaidDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetexpensesno, "NEW");
                if (modExistent.GetSetpaypaidno.Length > 0)
                {
                    sStatus = "Y";
                    sMessage = "Item tersebut telah ditambah pada Bayaran Belanja: " + modExistent.GetSetpayrcptno;
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPaymentPaidDetails(oModLineItem).Equals("Y"))
                    {
                        //update payment paid header information
                        sStatus = oMainCon.updatePaymentPaidHeaderInfo(comp, paypaidno);
                        sMessage = "";
                    }
                }
            }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string updateMobile_PayRcptInvoiceStatus(String comp, String payrcptno, String status, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        MainController oMainCon = new MainController();
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


            MainModel oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(comp, payrcptno);
            if (oModPayRcpt.GetSetpayrcptno.Trim().Length > 0)
            {
                oModPayRcpt.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModPayRcpt.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModPayRcpt.GetSetcancelledby = userid;
                }
                if (oMainCon.updatePaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
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

                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string updateMobile_InvoiceHeaderStatus(String comp, String invoiceno, String status, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


            MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
            if (oModInvoice.GetSetinvoiceno.Trim().Length > 0)
            {
                oModInvoice.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModInvoice.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModInvoice.GetSetcancelledby = userid;
                }
                if (oMainCon.updateInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
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

                                //to update transfer Order Invoice Amount
                                MainModel oModOrder = oMainCon.getTransferOrderDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetorderno, modInvDet.GetSetorder_lineno, "");
                                oModOrder.GetSetinvoiceamount = oModOrder.GetSetinvoiceamount + modInvDet.GetSettotalinvoice;
                                String result = oMainCon.updateTransferOrderDetails(oModOrder);

                                //update status for shipment has invoice
                                MainModel oModShipment = oMainCon.getShipmentDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetshipmentno, modInvDet.GetSetshipment_lineno, "");
                                oModShipment.GetSethasinvoice = "Y";
                                result = oMainCon.updateShipmentDetails(oModShipment);
                            }

                        }
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string updateMobile_ExpensesHeaderStatus(String comp, String expensesno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
            if (oModExpenses.GetSetexpensesno.Trim().Length > 0)
            {
                oModExpenses.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModExpenses.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModExpenses.GetSetcancelledby = userid;
                }
                if (oMainCon.updateExpensesHeader(oModExpenses).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
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
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    public static string updateMobile_PayPaidExpensesStatus(String comp, String paypaidno, String status, String userid)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainController oMainCon = new MainController();

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


            MainModel oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(comp, paypaidno);
            if (oModPayPaid.GetSetpaypaidno.Trim().Length > 0)
            {
                oModPayPaid.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModPayPaid.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModPayPaid.GetSetcancelledby = userid;
                }
                if (oMainCon.updatePaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        ArrayList lsPayPaidLineItem = oMainCon.getPaymentPaidDetailsList(oModPayPaid.GetSetcomp, oModPayPaid.GetSetpaypaidno, 0, "");
                        for (int i = 0; i < lsPayPaidLineItem.Count; i++)
                        {
                            MainModel modPayPaidDet = (MainModel)lsPayPaidLineItem[i];

                            //to update Expenses & paid Amount
                            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(modPayPaidDet.GetSetcomp, modPayPaidDet.GetSetexpensesno);
                            oModExpenses.GetSetpaypaidamount = oModExpenses.GetSetpaypaidamount + modPayPaidDet.GetSetpaypaidprice;
                            String result = oMainCon.updateExpensesHeader(oModExpenses);
                        }

                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    private static string convertJson(ArrayList lsItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(lsItem);

        return jsonResponse;
    }

    private static string convertJson(MainModel modItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(modItem);

        return jsonResponse;
    }

    private static string convertJson(object objItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(objItem);

        return jsonResponse;
    }
}