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

public partial class Accounting_LedgerTranPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrDateFrom = "";
    public String sCurrDateTo = "";
    public String sCurrStatus = "";
    public double dTotalDebit = 0;
    public double dTotalCredit = 0;
    public String sLeftRight = "LEFT";
    public String sExcludeClosing = "N";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisLedgerTran = new ArrayList();
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
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["accid"] != null)
        {
            sCurrAccId = oAccCon.replaceNull(Request.QueryString["accid"]);
        }
        if (Request.QueryString["datefrom"] != null)
        {
            sCurrDateFrom = oAccCon.replaceNull(Request.QueryString["datefrom"]);
            if(sCurrDateFrom.Length == 0)
            {
                sCurrDateFrom = "01-01-" + sCurrFyr + " 00:00:00";
            }
        }
        else
        {
            sCurrDateFrom = "01-01-" + sCurrFyr + " 00:00:00";
        }
        if (Request.QueryString["dateto"] != null)
        {
            sCurrDateTo = oAccCon.replaceNull(Request.QueryString["dateto"]);
            if(sCurrDateTo.Length == 0)
            {
                sCurrDateTo = "31-12-" + sCurrFyr + " 23:59:59";
            }
        }
        else
        {
            sCurrDateTo = "31-12-" + sCurrFyr + " 23:59:59";
        }
        if (Request.QueryString["excludeclosing"] != null)
        {
            sExcludeClosing = oAccCon.replaceNull(Request.QueryString["excludeclosing"]);
        }
        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        //DateTime datetime1 = Convert.ToDateTime(sCurrDateFrom + " 00:00:00", oAccCon.ukDtfi);
        //DateTime datetime2 = Convert.ToDateTime(sCurrDateTo + " 23:59:59", oAccCon.ukDtfi);
        DateTime datetime1 = Convert.ToDateTime(sCurrDateFrom , oAccCon.ukDtfi);
        DateTime datetime2 = Convert.ToDateTime(sCurrDateTo , oAccCon.ukDtfi);
        String accumulatedaccid = oAccCon.getFISCOATranChildAccid(sCurrComp, sCurrFyr, sCurrAccId, "");
        if (accumulatedaccid.Length > 0)
        {
            lsFisLedgerTran = oAccCon.getFisLedgerTranList(sCurrComp, sCurrFyr, 0, "", accumulatedaccid, 0, datetime1.ToString("dd-MM-yyyy HH:mm:ss"), datetime2.ToString("dd-MM-yyyy HH:mm:ss"), 0, "", sExcludeClosing, sCurrStatus);
        }
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

        if (Request.Params.Get("txtFindLedgerNo") != null)
        {
            sCurrAccId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
        }
        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("lsFindStatus") != null)
        {
            sCurrStatus = oAccCon.replaceNull(Request.Params.Get("lsFindStatus"));
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
    public static String getFisCOAList(string currcomp)
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
            ArrayList lsFisCOAMaster = oAccCon.getFisCOAMasterList(currcomp, "", "", "", 0, "", "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccMod.GetSetaccdesc
                };
                lsFisCOAId.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoalist = lsFisCOAId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisLedgerDetail(string currcomp, string currfyr, string accid, int tranno, string trancode)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisLedger = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && currfyr.Length > 0)
        {
            lsFisLedger = oAccCon.getFisLedgerTranList(currcomp, currfyr, 0, "", 0, "", tranno, trancode, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisledger = lsFisLedger };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }
}