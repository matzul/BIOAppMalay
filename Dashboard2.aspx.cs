using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard2 : System.Web.UI.Page
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

    public MainModel oModSalesPlan = new MainModel();
    public MainModel oModSalesActual = new MainModel();
    public MainModel oModSalesPackage1ThisMonth = new MainModel();
    public MainModel oModSalesPackage1ToDate = new MainModel();
    public MainModel oModSalesPackage2ThisMonth = new MainModel();
    public MainModel oModSalesPackage2ToDate = new MainModel();
    public MainModel oModSalesPackage3ThisMonth = new MainModel();
    public MainModel oModSalesPackage3ToDate = new MainModel();
    public MainModel oModSalesPackage4ThisMonth = new MainModel();
    public MainModel oModSalesPackage4ToDate = new MainModel();
    public MainModel oModSalesAllThisMonth = new MainModel();
    public MainModel oModSalesAllToDate = new MainModel();

    public MainModel oModSlotPlan = new MainModel();
    public MainModel oModSlotActual = new MainModel();

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
        }        //to get the year and month from session
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

        //3. For Payment Receipt
        Double dCollection = oMainCon.getReportCollection(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultCollection = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "COLLECTION_ACTUAL", "dashboard_collection", oModFYR.GetSetfinancemonth, dCollection);

        //get report fyr data for selected fyr
        oModCollectionPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "COLLECTION_PLAN", "dashboard_collection");
        oModCollectionActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "COLLECTION_ACTUAL", "dashboard_collection");

        oModRevenueActualSummary = oMainCon.getReportFYRDetailsSummary(sCurrComp, "", "REVENUE_ACTUAL", "dashboard_revenue");
        oModCollectionActualSummary = oMainCon.getReportFYRDetailsSummary(sCurrComp, "", "COLLECTION_ACTUAL", "dashboard_collection");
        lsPayRcptHeaderByBP = oMainCon.getInvoicePaymentReceiptHeaderListSumByBP(sCurrComp, "", "", "", "CONFIRMED");

        //4. For SalesOrder
        Double dSalesOrder = oMainCon.getReportSales(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        int resultSalesOrder = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "SALES_ACTUAL", "dashboard_sales", oModFYR.GetSetfinancemonth, dSalesOrder);

        //get report fyr data for selected fyr
        oModSalesPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SALES_PLAN", "dashboard_sales");
        oModSalesActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SALES_ACTUAL", "dashboard_sales");

        oModSalesPackage1ThisMonth = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-01", sCurrFyr, sCurrFMon, "CONFIRMED");
        oModSalesPackage1ToDate = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-01", sCurrFyr, "", "CONFIRMED");
        oModSalesPackage2ThisMonth = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-02", sCurrFyr, sCurrFMon, "CONFIRMED");
        oModSalesPackage2ToDate = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-02", sCurrFyr, "", "CONFIRMED");
        oModSalesPackage3ThisMonth = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-03", sCurrFyr, sCurrFMon, "CONFIRMED");
        oModSalesPackage3ToDate = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-03", sCurrFyr, "", "CONFIRMED");
        oModSalesPackage4ThisMonth = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-04", sCurrFyr, sCurrFMon, "CONFIRMED");
        oModSalesPackage4ToDate = oMainCon.getOrderDetailsDetailsByItem(sCurrComp, "", 0, "CD-LED RENTAL-PACKAGE-04", sCurrFyr, "", "CONFIRMED");
        oModSalesAllThisMonth = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, sCurrFMon,"", "", "CONFIRMED");
        oModSalesAllToDate = oMainCon.getOrderDetailsDetailsByAllItem(sCurrComp, sCurrFyr, "", "", "", "CONFIRMED");

        //5. For Slot Allocation -- update manually
        //Double dSlot = oMainCon.getReportSlot(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
        //int resultSlot = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "SLOT_ACTUAL", "dashboard_slot", oModFYR.GetSetfinancemonth, dSlot);

        //get report fyr data for selected fyr
        oModSlotPlan = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SLOT_PLAN", "dashboard_slot");
        oModSlotActual = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "SLOT_ACTUAL", "dashboard_slot");

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