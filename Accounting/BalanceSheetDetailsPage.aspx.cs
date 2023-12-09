using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_BalanceSheetDetailsPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sItemType = "";
    public String sItemTypeDesc = "";
    public String sStartTranDate = "";
    public String sEndTranDate = "";
    public String sCurrency = "";
    public String sStatus = "";
    public double dTotalAsset = 0;
    public double dTotalLiability = 0;
    public double dTotalEquity = 0;
    public double dTotalProfitLoss = 0;
    public double dTotalRevenue = 0;
    public double dTotalExpenses = 0;

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsBalanceSheetDetails = new ArrayList();

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
        if (Request.QueryString["itemtype"] != null)
        {
            sItemType = oAccCon.replaceNull(Request.QueryString["itemtype"]);
        }
        if (Request.QueryString["itemtypedesc"] != null)
        {
            sItemTypeDesc = oAccCon.replaceNull(Request.QueryString["itemtypedesc"]);
        }
        if (Request.QueryString["datefrom"] != null)
        {
            sStartTranDate = oAccCon.replaceNull(Request.QueryString["datefrom"]);
        }
        if (Request.QueryString["dateto"] != null)
        {
            sEndTranDate = oAccCon.replaceNull(Request.QueryString["dateto"]);
        }
        if (Request.QueryString["currency"] != null)
        {
            sCurrency = oAccCon.replaceNull(Request.QueryString["currency"]);
        }
        if (Request.QueryString["status"] != null)
        {
            sStatus = oAccCon.replaceNull(Request.QueryString["status"]);
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsBalanceSheetDetails = oAccCon.getFisCOATranListWithoutClosingDualReport(sCurrComp, sCurrFyr, "", "", "", "Y", "", "", "", sStartTranDate, sEndTranDate, "");
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

        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["itemtype"] != null)
        {
            sItemType = oAccCon.replaceNull(Request.QueryString["itemtype"]);
        }
        if (Request.QueryString["itemtypedesc"] != null)
        {
            sItemTypeDesc = oAccCon.replaceNull(Request.QueryString["itemtypedesc"]);
        }
        if (Request.QueryString["datefrom"] != null)
        {
            sStartTranDate = oAccCon.replaceNull(Request.QueryString["datefrom"]);
        }
        if (Request.QueryString["dateto"] != null)
        {
            sEndTranDate = oAccCon.replaceNull(Request.QueryString["dateto"]);
        }
        if (Request.QueryString["currency"] != null)
        {
            sCurrency = oAccCon.replaceNull(Request.QueryString["currency"]);
        }
        if (Request.QueryString["status"] != null)
        {
            sStatus = oAccCon.replaceNull(Request.QueryString["status"]);
        }

        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }

        if (Request.Params.Get("txtFindItemType") != null)
        {
            sItemType = oAccCon.replaceNull(Request.Params.Get("txtFindItemType"));
        }

        if (Request.Params.Get("txtFindItemTypeDesc") != null)
        {
            sItemTypeDesc = oAccCon.replaceNull(Request.Params.Get("txtFindItemTypeDesc"));
        }

        if (Request.Params.Get("txtFindDateFrom") != null)
        {
            sStartTranDate = oAccCon.replaceNull(Request.Params.Get("txtFindDateFrom"));
        }

        if (Request.Params.Get("txtFindDateTo") != null)
        {
            sEndTranDate = oAccCon.replaceNull(Request.Params.Get("txtFindDateTo"));
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

}