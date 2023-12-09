using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_BankVoucherPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrType = "";
    public String sCurrCategory = "";
    public String sCurrAccNumber = "";
    public String sCurrDateFrom = "";
    public String sCurrDateTo = "";
    public String sCurrStatus = "";
    public String sAction = "";
    public String sTranCode = "BANK_VOUCHER";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisJournalTran = new ArrayList();
    public ArrayList lsFisAccId = new ArrayList();

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

        lsFisJournalTran = oAccCon.getJournalEntryList(sCurrComp, sCurrFyr, datetime1.ToString("dd-MM-yyyy HH:mm:ss"), datetime2.ToString("dd-MM-yyyy HH:mm:ss"), 0, sTranCode, sCurrAccId, "", 0, sCurrStatus);

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
        if (Request.Params.Get("txtFindLedgerNo") != null)
        {
            sCurrAccId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
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
        if (Request.Params.Get("lsFindType") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindType"));
        }
        if (Request.Params.Get("lsFindCategory") != null)
        {
            sCurrCategory = oAccCon.replaceNull(Request.Params.Get("lsFindCategory"));
        }
        if (Request.Params.Get("txtFindAccNumber") != null)
        {
            sCurrAccNumber = oAccCon.replaceNull(Request.Params.Get("txtFindAccNumber"));
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
    public static String getFisCOAList(string currcomp, string fyr)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOAMaster = oAccCon.getFisCOATranList(currcomp, fyr, "", "", "", "Y", "", "", "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccCon.getFISCOATranParentDesc(oAccMod.GetSetcomp, oAccMod.GetSetfyr, oAccMod.GetSetaccid, oAccMod.GetSetacclevel)
                };
                lsFisCOAId.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoalist = lsFisCOAId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}