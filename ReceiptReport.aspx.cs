using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReceiptReport : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sReceiptNo = "";
    public String sBPID = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sItemNo = "";
    public String sStatus = "";
    public String expensesStatus = "";
    public ArrayList lsBP = new ArrayList();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsReceiptDetailsHeader = new ArrayList();

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
        lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");
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

        lsBP = oMainCon.getBPListIncludeSub(sCurrComp);
        lsItem = oMainCon.getItemList(sCurrComp, "", "", "");

        if (sAction.Equals("SEARCH"))
        {
            sReceiptNo = oMainCon.replaceNull(Request.Params.Get("receiptno"));
            sBPID = oMainCon.replaceNull(Request.Params.Get("bpid"));
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
            sStartDate = oMainCon.replaceNull(Request.Params.Get("datefrom"));
            sEndDate = oMainCon.replaceNull(Request.Params.Get("dateto"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
            sStatus = oMainCon.replaceNull(Request.Params.Get("receiptstatus"));
            expensesStatus = oMainCon.replaceNull(Request.Params.Get("expensesstatus"));
        }
        if (sAction.Equals("RESET"))
        {
            sReceiptNo = "";
            sBPID = "";
            sOrderNo = "";
            sStartDate = "";
            sEndDate = "";
            sItemNo = "";
            sStatus = "";
            expensesStatus = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsReceiptDetailsHeader = oMainCon.getReceiptHeaderDetailsList(sCurrComp, sReceiptNo, sBPID, sStartDate, sEndDate, sOrderNo, sItemNo, sStatus, expensesStatus);
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