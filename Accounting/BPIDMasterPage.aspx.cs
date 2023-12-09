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

public partial class Accounting_BPIDMasterPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrBpId = "";
    public String sCurrBpDesc = "";
    public String sCurrBpRef = "";
    public String sCurrOption = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBPMasterTran = new ArrayList();
    public ArrayList lsFisCOAMaster = new ArrayList();
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
        lsFisBPMasterTran = oAccCon.getBPMasterTranList(sCurrComp, sCurrBpId, sCurrBpDesc, sCurrOption);

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
            sCurrBpId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
        }
        if (Request.Params.Get("lsFindType") != null)
        {
            sCurrBpDesc = oAccCon.replaceNull(Request.Params.Get("lsFindType"));
        }
        if (Request.Params.Get("lsFindCategory") != null)
        {
            sCurrBpRef = oAccCon.replaceNull(Request.Params.Get("lsFindCategory"));
        }
        if (Request.Params.Get("lsFindOption") != null)
        {
            sCurrOption = oAccCon.replaceNull(Request.Params.Get("lsFindOption"));
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
    public static String getFisBpList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisBpId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisBpMaster = oAccCon.getBPMasterList(currcomp, "", "", "");
            for (int i = 0; i < lsFisBpMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisBpMaster[i];

                Object objData = new
                {
                    GetSetbpid = oAccMod.GetSetbpid,
                    GetSetbpdesc = oAccMod.GetSetbpdesc,
                    GetSetbpreference = oAccMod.GetSetbpreference
                };
                lsFisBpId.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbplist = lsFisBpId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisBpDetail(string currcomp, string bpid)
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

        if (currcomp.Length > 0 && bpid.Length > 0)
        {
            oAccMod = oAccCon.getBPMasterDetail(currcomp, bpid, "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbpdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }    

    [WebMethod(EnableSession = true)]
    public static String updateFisBpMasterTranList(string currcomp, string bpadd, string bpremove)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisBpId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            if (bpadd.Length > 0)
            {
                ArrayList lsBpToAdd = oAccCon.tokenString(bpadd, ",");
                for (int i = 0; i < lsBpToAdd.Count; i++) {
                    String sBpId = (String)lsBpToAdd[i];
                    AccountingModel modItem = oAccCon.getBPMasterDetail(currcomp, sBpId, "", "");
                    if (modItem.GetSetstatus.Equals("ACTIVE"))
                    {
                        modItem.GetSetstatus = "CONFIRMED";
                    }
                    else
                    {
                        modItem.GetSetstatus = "NEW";
                    }

                    if (modItem.GetSetbpid.Length > 0)
                    {
                        oAccCon.insertFisBp(modItem);
                    }
                }
            }

            if (bpremove.Length > 0)
            {
                ArrayList lsBpToRemove = oAccCon.tokenString(bpremove, ",");
                for (int i = 0; i < lsBpToRemove.Count; i++)
                {
                    String sBpId = (String)lsBpToRemove[i];
                    if (sBpId.Length > 0)
                    {
                        oAccCon.deleteFisBp(currcomp, sBpId);
                    }
                }
            }
            sStatus = "Y";
            sMessage = "";
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}