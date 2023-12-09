using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard1 : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sCurrFMon = "";
    public String sCurrAyr = "";
    public String sCurrAMon = "";
    public String sCurrDate = ""; 
    public String sUserId = "";
    //public String sCurrYear = "";
    //public String sCurrMonth = "";
    public String sUpdYear = "";
    public String sUpdMonth = "";
    public String sAction = "";
    public String sUserType = "";
    public MainModel oModFYR = new MainModel();
    
    public MainModel oModRevenuePlan = new MainModel();
    public MainModel oModRevenueActual = new MainModel();
    public MainModel oModRevenueActualSummary = new MainModel();
    
    public MainModel oModExpensesPlan = new MainModel();
    public MainModel oModExpensesActual = new MainModel();
    
    public MainModel oModCollectionPlan = new MainModel();
    public MainModel oModCollectionActual = new MainModel();
    public MainModel oModCollectionActualSummary = new MainModel();
    public ArrayList lsPayRcptHeaderByBP = new ArrayList();

    public ArrayList lsClosingCashFlow = new ArrayList();
    public MainModel oModCashCollectionActual = new MainModel();
    public MainModel oModOtherCollectionActual = new MainModel();

    public MainModel oModSalesPlan = new MainModel();
    public MainModel oModSalesActual = new MainModel();
    public MainModel oModSalesOrderThisMonth = new MainModel();
    public MainModel oModSalesOrderToDate = new MainModel();
    public MainModel oModSalesOtherThisMonth = new MainModel();
    public MainModel oModSalesOtherToDate = new MainModel();
    public MainModel oModSalesAllThisMonth = new MainModel();
    public MainModel oModSalesAllToDate = new MainModel();

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
        if (Session["usertype"] != null)
        {
            sUserType = oMainCon.replaceNull(Session["usertype"].ToString());
        }
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }
        if (Session["fmon"] != null)
        {
            sCurrFMon = Session["fmon"].ToString();
        }
        if (Session["ayr"] != null)
        {
            sCurrAyr = Session["ayr"].ToString();
        }
        if (Session["amon"] != null)
        {
            sCurrAMon = Session["amon"].ToString();
        }        
        //to get the year and month from session
        sUpdYear = DateTime.Now.ToString("yyyy");
        sUpdMonth = DateTime.Now.ToString("MM");
        sCurrDate = DateTime.Now.ToString("dd/MM/yyyy");
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
        if (Session["usertype"] != null)
        {
            sUserType = oMainCon.replaceNull(Session["usertype"].ToString());
        }
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }
        if (Session["fmon"] != null)
        {
            sCurrFMon = Session["fmon"].ToString();
        }
        if (Session["ayr"] != null)
        {
            sCurrAyr = Session["ayr"].ToString();
        }
        if (Session["amon"] != null)
        {
            sCurrAMon = Session["amon"].ToString();
        } 
        
        sUpdYear = DateTime.Now.ToString("yyyy");
        sUpdMonth = DateTime.Now.ToString("MM");
        sCurrDate = DateTime.Now.ToString("dd/MM/yyyy");

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }

        if (sAction.Equals("UPDATE_DASHBOARD"))
        {
            if (Request.Params.Get("actualyear") != null)
            {
                sUpdYear = oMainCon.replaceNull(Request.Params.Get("actualyear"));
            }
            if (Request.Params.Get("actualmonth") != null)
            {
                sUpdMonth = oMainCon.replaceNull(Request.Params.Get("actualmonth"));
            }
        }
    }    
    private void processValues()
    {
        //get fyr year & month
        oModFYR = oMainCon.getReportFYRYearMonth(sCurrComp, "", "", sUpdYear, sUpdMonth);

        //update report fyr data for selected year & month
        //1. For Revenue
        Double dRevenue = oMainCon.getReportRevenue(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultRevenue = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "REVENUE_ACTUAL", "dashboard_revenue", oModFYR.GetSetfinancemonth, dRevenue);

        //get report fyr data for selected fyr
        oModRevenuePlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "REVENUE_PLAN", "dashboard_revenue");
        oModRevenueActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "REVENUE_ACTUAL", "dashboard_revenue");

        //2. For Expenses -- update manually
        Double dExpenses = oMainCon.getReportExpenses(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultExpenses = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "EXPENSES_ACTUAL", "dashboard_expenses", oModFYR.GetSetfinancemonth, dExpenses);

        //get report fyr data for selected fyr
        oModExpensesPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "EXPENSES_PLAN", "dashboard_expenses");
        oModExpensesActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "EXPENSES_ACTUAL", "dashboard_expenses");

        //3. For Collection & Payment Receipt
        Double dCollection = oMainCon.getReportCollection(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultCollection = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "COLLECTION_ACTUAL", "dashboard_collection", oModFYR.GetSetfinancemonth, dCollection);

        //get report fyr collection and payment receipt
        oModCollectionPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "COLLECTION_PLAN", "dashboard_collection");
        oModCollectionActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "COLLECTION_ACTUAL", "dashboard_collection");

        //Disable and replace by cash flow
        /*
        Double dCashCollection = oMainCon.getReportPaymentReceipt(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CASH", "CONFIRMED");
        int resultCashColletion = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "CASHCOLLECTION_ACTUAL", "dashboard_collection", oModFYR.GetSetfinancemonth, dCashCollection);
        oModCashCollectionActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "CASHCOLLECTION_ACTUAL", "dashboard_collection");

        int resultOtherColletion = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "OTHERCOLLECTION_ACTUAL", "dashboard_collection", oModFYR.GetSetfinancemonth, dCollection - dCashCollection);
        oModOtherCollectionActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "OTHERCOLLECTION_ACTUAL", "dashboard_collection");
        */
        //oModRevenueActualSummary = oMainCon.getReportFYRDetailsSummary(sCurrComp, sCurrFyr, "REVENUE_ACTUAL", "dashboard_revenue");
        //oModRevenueActualSummary.GetSetTODATE = oMainCon.getReportRevenueExcludeJV(sCurrComp, oModFYR.GetSetactualyear, "", "CONFIRMED");
        oModRevenueActualSummary.GetSetTODATE = oMainCon.getReportRevenueExcludeJV(sCurrComp, sCurrFyr, "", "CONFIRMED");
        //oModCollectionActualSummary = oMainCon.getReportFYRDetailsSummary(sCurrComp, sCurrFyr, "COLLECTION_ACTUAL", "dashboard_collection");
        //oModCollectionActualSummary.GetSetTODATE = oMainCon.getReportCollectionBasedOnInvoice(sCurrComp, oModFYR.GetSetactualyear, "", "CONFIRMED");
        oModCollectionActualSummary.GetSetTODATE = oMainCon.getReportCollectionBasedOnInvoice(sCurrComp, sCurrFyr, "", "CONFIRMED");
        lsPayRcptHeaderByBP = oMainCon.getInvoicePaymentReceiptHeaderListSumByBP(sCurrComp, "", "", sCurrFyr, "CONFIRMED");

        //4. For SalesOrder
        Double dSalesOrder = oMainCon.getReportSales(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultSalesOrder = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "SALES_ACTUAL", "dashboard_sales", oModFYR.GetSetfinancemonth, dSalesOrder);

        //get report fyr data for selected fyr
        oModSalesPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SALES_PLAN", "dashboard_sales");
        oModSalesActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SALES_ACTUAL", "dashboard_sales");

        oModSalesOrderThisMonth = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, sCurrFMon, "", "SALES_ORDER", "CONFIRMED");
        oModSalesOrderToDate = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, "", "", "SALES_ORDER", "CONFIRMED");
        oModSalesAllThisMonth = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, sCurrFMon, "", "", "CONFIRMED");
        oModSalesAllToDate = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, "", "", "", "CONFIRMED");

        //5. For Cash Flow 
        ArrayList lsCashFlowHeader = oMainCon.getCashFlowHeaderList(sCurrComp, "", "", "", "", "", "");
        for(int i = 0; i < lsCashFlowHeader.Count; i++)
        {
            MainModel oModCashFlow = (MainModel)lsCashFlowHeader[i];
            if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
            {
                String sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                for (int x = 0; x < lsPayRcptHeaderDetails.Count; x++)
                {
                    MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[x];
                    oModCashFlow.GetSetbankpaymentreceiptamount = oModCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                    oModCashFlow.GetSetcashpaymentreceiptamount = oModCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                }
                ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                for (int y = 0; y < lsPayPaidHeaderDetails.Count; y++)
                {
                    MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[y];
                    if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                    {
                        oModCashFlow.GetSetbankpaymentpaidamount = oModCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                        oModCashFlow.GetSetcashpaymentpaidamount = oModCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                    }
                }
                oModCashFlow.GetSetbankclosingamount = oModCashFlow.GetSetbankopeningamount + oModCashFlow.GetSetbankpaymentreceiptamount - oModCashFlow.GetSetbankpaymentpaidamount;
                oModCashFlow.GetSetcashclosingamount = oModCashFlow.GetSetcashopeningamount + oModCashFlow.GetSetcashpaymentreceiptamount - oModCashFlow.GetSetcashpaymentpaidamount;
                oModCashFlow.GetSetclosingdate = sClosingDate;
            }
            lsClosingCashFlow.Add(oModCashFlow);
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

    public String getMonthText(int iMonth)
    {
        String text = "";
        if (iMonth.Equals(1))
        {
            text = "Jan";
        }
        else if (iMonth.Equals(2))
        {
            text = "Feb";
        }
        else if (iMonth.Equals(3))
        {
            text = "Mar";
        }
        else if (iMonth.Equals(4))
        {
            text = "Apr";
        }
        else if (iMonth.Equals(5))
        {
            text = "May";
        }
        else if (iMonth.Equals(6))
        {
            text = "Jun";
        }
        else if (iMonth.Equals(7))
        {
            text = "Jul";
        }
        else if (iMonth.Equals(8))
        {
            text = "Aug";
        }
        else if (iMonth.Equals(9))
        {
            text = "Sep";
        }
        else if (iMonth.Equals(10))
        {
            text = "Oct";
        }
        else if (iMonth.Equals(11))
        {
            text = "Nov";
        }
        else if (iMonth.Equals(12))
        {
            text = "Dec";
        }
        return text;
    }

}