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

public partial class Accounting_ProfitLossPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrType = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBalance = new ArrayList();
    public ArrayList lsProfitLoss = new ArrayList();

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

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsFisBalance = oAccCon.getFisBalanceList(sCurrComp, sCurrFyr, sCurrType, "");
        if (lsFisBalance.Count == 1)
        {
            AccountingModel modItem = (AccountingModel)lsFisBalance[0];
            modItem.GetSetitemtype = "CURRENT_PROFIT_LOSS";
            modItem.GetSetitemtypedesc = "Laporan Penyata Pendapatan Semasa";
            modItem.GetSetdatefrom = modItem.GetSettrandate;
            modItem.GetSetdateto = DateTime.Now.ToString("dd-MM-yyyy") + " 23:59:59";
            modItem.GetSetdebit = 0;
            modItem.GetSetcredit = 0;
            lsProfitLoss.Add(modItem);
        }
        else 
        {
            String startdate = "";
            for(int i=0; i<lsFisBalance.Count; i++)
            {
                AccountingModel modItem = (AccountingModel)lsFisBalance[i];
                if (modItem.GetSettrancode.Equals("OPENING_BALANCE") && modItem.GetSetstatus.Equals("CONFIRMED"))
                {
                    startdate = modItem.GetSettrandate;
                }
                else if (modItem.GetSettrancode.Equals("OPENING_BALANCE") && modItem.GetSetstatus.Equals("CONFIRMED") && (i == lsFisBalance.Count))
                {
                    modItem.GetSetitemtype = "CURRENT_PROFIT_LOSS";
                    modItem.GetSetitemtypedesc = "Laporan Penyata Pendapatan Semasa";
                    modItem.GetSetdatefrom = modItem.GetSettrandate;
                    modItem.GetSetdateto = DateTime.Now.ToString("dd-MM-yyyy") + " 23:59:59";
                    modItem.GetSetdebit = 0;
                    modItem.GetSetcredit = 0;
                    lsProfitLoss.Add(modItem);
                }
                else if (modItem.GetSettrancode.Equals("OPENING_BALANCE") && modItem.GetSetstatus.Equals("NEW"))
                {
                    modItem.GetSetitemtype = "CURRENT_PROFIT_LOSS";
                    modItem.GetSetitemtypedesc = "Laporan Penyata Pendapatan Semasa";
                    modItem.GetSetdatefrom = modItem.GetSettrandate;
                    modItem.GetSetdateto = DateTime.Now.ToString("dd-MM-yyyy") + " 23:59:59";
                    modItem.GetSetdebit = 0;
                    modItem.GetSetcredit = 0;
                    lsProfitLoss.Add(modItem);
                }
                else if (modItem.GetSettrancode.Equals("CLOSING_BALANCE"))
                {
                    modItem.GetSetitemtype = "CLOSED_PROFIT_LOSS";
                    modItem.GetSetitemtypedesc = "Laporan Penyata Pendapatan Bagi Penutupan Baki Akaun " + modItem.GetSettrandate;
                    modItem.GetSetdatefrom = startdate;
                    modItem.GetSetdateto = modItem.GetSettrandate;
                    lsProfitLoss.Add(modItem);
                }
            }
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

        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
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

}