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

public partial class Accounting_InventoryMasterPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrItemNo = "";
    public String sCurrItemDesc = "";
    public String sCurrOption = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisItemMasterTran = new ArrayList();

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
        lsFisItemMasterTran = oAccCon.getItemMasterTranList(sCurrComp, sCurrItemNo, sCurrItemDesc, "INVENTORY", sCurrOption);

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

        if (Request.Params.Get("txtFindItemNo") != null)
        {
            sCurrItemNo = oAccCon.replaceNull(Request.Params.Get("txtFindItemNo"));
        }
        if (Request.Params.Get("txtFindItemDesc") != null)
        {
            sCurrItemDesc = oAccCon.replaceNull(Request.Params.Get("txtFindItemDesc"));
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
    public static String getFisItemList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisItemOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisItem = oAccCon.getFisItemList(currcomp, 0, "", "", "INVENTORY", "");
            for (int i = 0; i < lsFisItem.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisItem[i];

                Object objData = new
                {
                    GetSetitemno = oAccMod.GetSetitemno,
                    GetSetitemdesc = oAccMod.GetSetitemdesc,
                    GetSetitemcat = oAccMod.GetSetitemcat
                };
                lsFisItemOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisitemlist = lsFisItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisItemDetail(string currcomp, string itemno)
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

        if (currcomp.Length > 0 && itemno.Length > 0)
        {
            oAccMod = oAccCon.getFisItemDetail(currcomp, 0, itemno, "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisitemdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisItemMasterTranList(string currcomp, string itemadd, string itemremove)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            if (itemadd.Length > 0)
            {
                ArrayList lsItemToAdd = oAccCon.tokenString(itemadd, ",");
                for (int i = 0; i < lsItemToAdd.Count; i++) {
                    String sItemNo = (String)lsItemToAdd[i];
                    AccountingModel modItem = oAccCon.getItemMasterDetail(currcomp, sItemNo, "", "");
                    if (modItem.GetSetstatus.Equals("ACTIVE"))
                    {
                        modItem.GetSetstatus = "CONFIRMED";
                    }
                    else
                    {
                        modItem.GetSetstatus = "NEW";
                    }

                    if (modItem.GetSetitemno.Length > 0)
                    {
                        oAccCon.insertFisItem(modItem);
                    }
                }
            }

            if (itemremove.Length > 0)
            {
                ArrayList lsItemToRemove = oAccCon.tokenString(itemremove, ",");
                for (int i = 0; i < lsItemToRemove.Count; i++)
                {
                    String sItemNo = (String)lsItemToRemove[i];
                    if (sItemNo.Length > 0)
                    {
                        oAccCon.deleteFisItem(currcomp, sItemNo);
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