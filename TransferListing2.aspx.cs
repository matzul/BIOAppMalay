using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransferListing2 : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sOrderNo = "";
    public String sCompFrom = "";
    public String sCompTo = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sStatus = "";
    public ArrayList lsComp = new ArrayList();
    public ArrayList lsOrderHeader = new ArrayList();

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
        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");
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

        lsComp = oMainCon.getCompInfoList("", sUserId, "T01");

        if (sAction.Equals("SEARCH"))
        {
            sOrderNo = oMainCon.replaceNull(Request.Params.Get("orderno"));
            sCompFrom = oMainCon.replaceNull(Request.Params.Get("compfrom"));
            sCompTo = oMainCon.replaceNull(Request.Params.Get("compto"));
        }
        if (sAction.Equals("RESET"))
        {
            sOrderNo = "";
            sCompFrom = "";
            sCompTo = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsOrderHeader = oMainCon.getTransferOrderHeaderList("", sCompFrom.Trim().Length>0? sCompFrom : sCurrComp, sCompTo.Trim().Length > 0 ? sCompTo : sCurrComp, sOrderNo, "TRANSFER_ORDER", "", sStartDate, sEndDate, sStatus);
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