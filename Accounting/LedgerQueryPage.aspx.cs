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

public partial class Accounting_LedgerQueryPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrType = "";
    public String sCurrCategory = "";
    public String sCurrAccNumber = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisCOATran = new ArrayList();
    public ArrayList lsFisAccId = new ArrayList();
    public ArrayList lsFisSubAccId = new ArrayList();

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

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsFisCOATran = oAccCon.getFisCOATranList(sCurrComp, sCurrFyr, "", "", "", 0, sCurrType, "", "", "", "");
        if (sCurrComp.Length > 0 && sCurrFyr.Length > 0 && (sCurrAccId.Length > 0 || sCurrCategory.Length > 0 || sCurrAccNumber.Length > 0))
        {
            lsFisAccId = oAccCon.searchFisCOATranList(lsFisAccId, sCurrComp, sCurrFyr, sCurrAccId, sCurrType, sCurrCategory, sCurrAccNumber, "");
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
    public static String getFisCOAList(string currcomp, string currfyr)
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
            ArrayList lsFisCOATran = oAccCon.getFisCOATranList(currcomp, currfyr, "", "", "", 0, "", "", "", "", "");
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOATran[i];

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

}