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

public partial class Accounting_JournalPostingPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public int iCurrTranNo = 0;
    public String sCurrTranCode = "";
    public String sCurrTranDesc = "";
    public String sCurrTranDate = "";
    public String sCurrTranCurrency = "";
    public String sCurrStatus = "";
    public double dTotalDebit = 0;
    public double dTotalCredit = 0;
    public String sAction = "";
    public String sAlertMessage = "";

    public int iTotalLine = 0;
    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public AccountingModel modJournalTran = new AccountingModel();
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
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = oAccCon.replaceNull(Request.QueryString["action"]);
        }
        if (Request.QueryString["tranno"] != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(oAccCon.replaceNull(Request.QueryString["tranno"]).Equals("")?"0": Request.QueryString["tranno"]);
        }
        if (Request.QueryString["trancode"] != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.QueryString["trancode"]);
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";

        if (iCurrTranNo > 0 && sCurrTranCode.Length > 0)
        {
            modJournalTran = oAccCon.getPostingData(sCurrComp, sCurrFyr, iCurrTranNo, sCurrTranCode, "");
            lsFisJournalTran = oAccCon.getFisLedgerTranList(sCurrComp, sCurrFyr, 0, "", 0, "", iCurrTranNo, sCurrTranCode, "");
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

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oAccCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("txtFindTranNo") != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(Request.Params.Get("txtFindTranNo"));
        }
        if (Request.Params.Get("lsFindTranCode") != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.Params.Get("lsFindTranCode"));
        }
        if (Request.Params.Get("txtFindTranDesc") != null)
        {
            sCurrTranDesc = oAccCon.replaceNull(Request.Params.Get("txtFindTranDesc"));
        }
        if (Request.Params.Get("txtFindTranDate") != null)
        {
            sCurrTranDate = oAccCon.replaceNull(Request.Params.Get("txtFindTranDate"));
        }
        if (Request.Params.Get("txtFindCurrency") != null)
        {
            sCurrTranCurrency = oAccCon.replaceNull(Request.Params.Get("txtFindCurrency"));
        }
        if (Request.Params.Get("lsFindStatus") != null)
        {
            sCurrStatus = oAccCon.replaceNull(Request.Params.Get("lsFindStatus"));
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
    public static String getFisCOATranList(string currcomp, string fyr, string acctype)
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
            ArrayList lsFisCOAMaster = oAccCon.getFisCOATranList(currcomp, fyr, "", "", "", "Y", acctype, "", "", "", "");
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

    [WebMethod(EnableSession = true)]
    public static String getFisCOADetail(string currcomp, string accid)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        AccountingModel oAccMod = new AccountingModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && accid.Length > 0)
        {
            oAccMod = oAccCon.getFisCOAMasterDetail(currcomp, accid, "", "", 0, "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoadetail = oAccMod };

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