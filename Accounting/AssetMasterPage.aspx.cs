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

public partial class Accounting_AssetMasterPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAssetNo = "";
    public String sCurrAssetDesc = "";
    public String sCurrOption = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisAssetMasterTran = new ArrayList();

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
        lsFisAssetMasterTran = oAccCon.getAssetMasterTranList(sCurrComp, sCurrAssetNo, sCurrAssetDesc, "", sCurrOption);

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

        if (Request.Params.Get("txtFindAssetNo") != null)
        {
            sCurrAssetNo = oAccCon.replaceNull(Request.Params.Get("txtFindAssetNo"));
        }
        if (Request.Params.Get("txtFindAssetDesc") != null)
        {
            sCurrAssetDesc = oAccCon.replaceNull(Request.Params.Get("txtFindAssetDesc"));
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
    public static String getFisAssetList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisAssetOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisAsset = oAccCon.getFisAssetList(currcomp, 0, "", "", "", "");
            for (int i = 0; i < lsFisAsset.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisAsset[i];

                Object objData = new
                {
                    GetSetassetno = oAccMod.GetSetassetno,
                    GetSetassetdesc = oAccMod.GetSetassetdesc,
                    GetSetassetcat = oAccMod.GetSetassetcat,
                    GetSetassettyp = oAccMod.GetSetassettyp
                };
                lsFisAssetOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisassetlist = lsFisAssetOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisAssetDetail(string currcomp, string assetno, string assettyp)
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

        if (currcomp.Length > 0 && assetno.Length > 0)
        {
            oAccMod = oAccCon.getFisAssetDetail(currcomp, 0, assetno, "", "", assettyp, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisassetdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisAssetMasterTranList(string currcomp, string assetadd, string assetremove)
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
            if (assetadd.Length > 0)
            {
                ArrayList lsAssetToAdd = oAccCon.tokenString(assetadd, ",");
                for (int i = 0; i < lsAssetToAdd.Count; i++) {
                    String sAssetNo = "";
                    String sAssetTyp = "";
                    ArrayList lsAsset = oAccCon.tokenString((String)lsAssetToAdd[i], "|");
                    if (lsAsset.Count > 0)
                    {
                        sAssetNo = lsAsset[0].ToString();
                        sAssetTyp = lsAsset[1].ToString();
                    }
                    if (sAssetNo.Length > 0 && sAssetTyp.Length > 0)
                    {
                        AccountingModel modAsset = oAccCon.getAssetMasterTranDetail(currcomp, sAssetNo, "", "", sAssetTyp);

                        if (modAsset.GetSetassetno.Length > 0 && modAsset.GetSetassettyp.Length > 0)
                        {
                            oAccCon.insertFisAsset(modAsset);
                        }
                    }
                }
            }

            if (assetremove.Length > 0)
            {
                ArrayList lsAssetToRemove = oAccCon.tokenString(assetremove, ",");
                for (int i = 0; i < lsAssetToRemove.Count; i++)
                {
                    String sAssetNo = "";
                    String sAssetTyp = "";
                    ArrayList lsAsset = oAccCon.tokenString((String)lsAssetToRemove[i], "|");
                    if (lsAsset.Count > 0)
                    {
                        sAssetNo = lsAsset[0].ToString();
                        sAssetTyp = lsAsset[1].ToString();
                    }

                    if (sAssetNo.Length > 0 && sAssetTyp.Length > 0)
                    {
                        oAccCon.deleteFisAsset(currcomp, sAssetNo, sAssetTyp);
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