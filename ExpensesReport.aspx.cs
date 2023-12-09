using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExpensesReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sReceiptNo = "";
    public String sExpensesNo = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String paymentStatus = "";
    public String sPayToId = "";
    public ArrayList lsPayTo = new ArrayList();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsExpensesHeaderDetails = new ArrayList();

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
        lsPayTo = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");
        sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
        sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
    }
    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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

        lsPayTo = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sExpensesNo = oMainCon.replaceNull(Request.Params.Get("expensesno"));
            sPayToId = oMainCon.replaceNull(Request.Params.Get("bpid"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("expensesstatus"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("datefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("dateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            paymentStatus = oMainCon.replaceNull(Request.Params.Get("paymentstatus"));
            sReceiptNo = oMainCon.replaceNull(Request.Params.Get("receiptno"));
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
        }
        if (sAction.Equals("RESET"))
        {
            sExpensesNo = "";
            sPayToId = "";
            sStatus = "";
            sStartDate = FirstDayOfMonth().ToString("dd-MM-yyyy");
            sEndDate = DateTime.Now.ToString("dd-MM-yyyy");
            sItemNo = "";
            paymentStatus = "";
            sReceiptNo = "";
            sOrderNo = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsExpensesHeaderDetails = oMainCon.getExpensesHeaderDetailsList(sCurrComp, sExpensesNo, sPayToId, sStartDate, sEndDate, sReceiptNo, sOrderNo, sItemNo, sStatus, paymentStatus);
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