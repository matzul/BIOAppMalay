using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExpensesListing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sExpensesNo = "";
    public String sPayToId = "";
    public String sPayToOther = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sStatus = "";
    public ArrayList lsPayTo = new ArrayList();
    public ArrayList lsExpensesHeader = new ArrayList();

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
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        lsPayTo = oMainCon.getBPList(sCurrComp, "", "", "");
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
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }

        lsPayTo = oMainCon.getBPList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sExpensesNo = oMainCon.replaceNull(Request.Params.Get("expensesno"));
            sPayToId = oMainCon.replaceNull(Request.Params.Get("payto"));
            sPayToOther = oMainCon.replaceNull(Request.Params.Get("paytoother"));
        }
        if (sAction.Equals("RESET"))
        {
            sExpensesNo = "";
            sPayToId = "";
            sPayToOther = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsExpensesHeader = oMainCon.getExpensesHeaderList(sCurrComp, sExpensesNo, sPayToId, sStartDate, sEndDate, sStatus);
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