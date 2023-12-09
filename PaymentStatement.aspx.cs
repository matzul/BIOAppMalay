using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentStatement : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sAlertMessage = "";
    public MainModel oModCashFlow = new MainModel();
    public ArrayList lsPayRcptHeaderDetails = new ArrayList();
    public ArrayList lsPayPaidHeaderDetails = new ArrayList();

    public String sDateFrom = "";
    public String sDateTo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }

    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sDateFrom = FirstDayOfMonth().ToString("dd-MM-yyyy 00:00:00");
        sDateTo = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
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
        if (Request.Params.Get("datefrom") != null)
        {
            sDateFrom = oMainCon.replaceNull(Request.Params.Get("datefrom"));
        }
        if (Request.Params.Get("dateto") != null)
        {
            sDateTo = oMainCon.replaceNull(Request.Params.Get("dateto"));
        }

    }

    private void processValues()
    {
        if (oMainCon.compareTwoDateTime(sDateFrom, sDateTo) > 0)
        {
            if (sAction.Equals("OPEN"))
            {
                lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(sCurrComp, sDateFrom, sDateTo, "CONFIRMED");
                lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(sCurrComp, sDateFrom, sDateTo, "CONFIRMED");
            }
            else
            {
                lsPayRcptHeaderDetails = new ArrayList();
                lsPayPaidHeaderDetails = new ArrayList();
            }
        }
        else
        {
            lsPayRcptHeaderDetails = new ArrayList();
            lsPayPaidHeaderDetails = new ArrayList();
            sAlertMessage = "ERROR|The Date To must later than Date From...";
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