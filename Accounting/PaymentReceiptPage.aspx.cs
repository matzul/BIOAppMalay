using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_PaymentReceiptPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();
    public MainController oMainCon = new MainController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrPayRcptNo = "";
    public String sCurrDateFrom = "";
    public String sCurrDateTo = "";
    public String sCurrStatus = "";
    public String sAction = "";
    public String sTranCode = "INV";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsPaymentReceipt = new ArrayList();
    public ArrayList lsPayRcptNo = new ArrayList();

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
        if (Session["comp"] != null)
            sCurrComp = oAccCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oAccCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oAccCon.replaceNull(Session["userid"].ToString());

        sCurrDateFrom = FirstDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
        sCurrDateTo = LastDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
    private DateTime LastDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
    }

    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";

        DateTime datetime1 = Convert.ToDateTime(sCurrDateFrom + " 00:00:00", oAccCon.ukDtfi);
        DateTime datetime2 = Convert.ToDateTime(sCurrDateTo + " 23:59:59", oAccCon.ukDtfi);

        lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(sCurrComp, sCurrFyr, datetime1.ToString(), datetime2.ToString(), "CONFIRMED");

        this.lsPageList.Items.Add(new ListItem("1", "1"));
        this.lsPageList.SelectedValue = sCurrentPage;
    }
    private void getValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oAccCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oAccCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oAccCon.replaceNull(Session["userid"].ToString());

        sCurrDateFrom = FirstDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
        sCurrDateTo = LastDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oAccCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("txtFindRcptNo") != null)
        {
            sCurrPayRcptNo = oAccCon.replaceNull(Request.Params.Get("txtFindRcptNo"));
        }
        if (!sAction.Equals("RESET"))
        {
            if (Request.Params.Get("lsFindFyr") != null)
            {
                sCurrFyr = oAccCon.replaceNull(Request.Params.Get("lsFindFyr"));
            }
            if (Request.Params.Get("txtFindFromDate") != null)
            {
                sCurrDateFrom = oAccCon.replaceNull(Request.Params.Get("txtFindFromDate"));
            }
            if (Request.Params.Get("txtFindToDate") != null)
            {
                sCurrDateTo = oAccCon.replaceNull(Request.Params.Get("txtFindToDate"));
            }
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    protected void lsPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
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

    [WebMethod(EnableSession = true)]
    public static String getFisFYRList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisFYR = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsFisFYR = oAccCon.getFisFYRList(currcomp);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisfyrlist = lsFisFYR };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getPayRcptList(string currcomp, string fyr)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsPayRcptNo = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsPayRcpt = oMainCon.getPaymentReceiptHeaderList(currcomp, "", "", "01-01-" + fyr, "31-12-" + fyr, "CONFIRMED");
            for (int i = 0; i < lsPayRcpt.Count; i++)
            {
                MainModel oMod = (MainModel)lsPayRcpt[i];

                Object objData = new
                {
                    GetSetpayrcptno = oMod.GetSetpayrcptno,
                    GetSetbpdesc = oMod.GetSetbpdesc + " [" + oMod.GetSetpayrcptamount + "]"
                };
                lsPayRcptNo.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, payrcptlist = lsPayRcptNo };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}