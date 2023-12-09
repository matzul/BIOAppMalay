using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_PostingPage2 : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrDateFrom = "";
    public String sCurrDateTo = "";
    public String sCurrType = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";

    public ArrayList lsPostData = new ArrayList();

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
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }
        if(sCurrFyr.Length > 0)
        {
            sCurrDateFrom = FirstDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
            sCurrDateTo = LastDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
        }
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
        lsPostData = oAccCon.getPostingDataList(sCurrComp, sCurrFyr, datetime1.ToString(), datetime2.ToString(), 0, sCurrType, "", 0, "");
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

        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("lsFindOption") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindOption"));
        }
        if (Request.Params.Get("txtFindFromDate") != null)
        {
            sCurrDateFrom = oAccCon.replaceNull(Request.Params.Get("txtFindFromDate"));
        }
        if (Request.Params.Get("txtFindToDate") != null)
        {
            sCurrDateTo = oAccCon.replaceNull(Request.Params.Get("txtFindToDate"));
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
    public static String getPostingDataList(string currcomp, string fyr, string datefrom, string dateto, string trancode)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsPostData = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && fyr.Length > 0 && datefrom.Length > 0 && dateto.Length > 0)
        {
            lsPostData = oAccCon.getPostingDataList(currcomp, fyr, datefrom, dateto, 0, trancode, "", 0, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, postingdate = lsPostData };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}