using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard3 : System.Web.UI.Page
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
    
    public MainModel oModStockTransIN = new MainModel();
    public MainModel oModStockTransOUT = new MainModel();
    public MainModel oModStockPosition = new MainModel();

    public ArrayList lsClosingStockValue = new ArrayList();

    /*
    public MainModel oModStockOrderThisMonth = new MainModel();
    public MainModel oModStockOrderToDate = new MainModel();
    public MainModel oModStockReceivedThisMonth = new MainModel();
    public MainModel oModStockReceivedToDate = new MainModel();
    */

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
        //1. For Stock Value IN
        Double dStockTransIN = oMainCon.getReportStockTrans(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "IN");
        int resultStockIn = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "STOCK_IN", "dashboard_stockin", oModFYR.GetSetfinancemonth, dStockTransIN);

        //get report fyr data for selected fyr
        oModStockTransIN = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "STOCK_IN", "dashboard_stockin");

        //2. For Stock Value OUT
        Double dStockTransOUT = oMainCon.getReportStockTrans(sCurrComp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "OUT");
        int resultStockOut = oMainCon.updateReportFYRDetails(sCurrComp, oModFYR.GetSetfinanceyear, "STOCK_OUT", "dashboard_stockout", oModFYR.GetSetfinancemonth, dStockTransOUT);

        //get report fyr data for selected fyr
        oModStockTransOUT = oMainCon.getReportFYRDetails(sCurrComp, sCurrFyr, "STOCK_OUT", "dashboard_stockout");

        oModStockPosition = oMainCon.getItemStockSummary(sCurrComp, "", "", "");

        //3. For Stock Closing 
        lsClosingStockValue = oMainCon.getStockStateHeaderList(sCurrComp, "", "", "", "", "", "");

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