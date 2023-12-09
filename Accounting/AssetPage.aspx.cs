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

public partial class Accounting_AssetPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAssetNo = "";
    public String sCurrAssetDesc = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisAsset = new ArrayList();

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
        lsFisAsset = oAccCon.getFisAssetList(sCurrComp, 0, sCurrAssetNo, sCurrAssetDesc, "", "");
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
    public static String getFisAssetDetail(string currcomp, int id)
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

        if (currcomp.Length > 0 && id > 0)
        {
            oAccMod = oAccCon.getFisAssetDetail(currcomp, id, "", "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisassetdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisAssetDetail(string currcomp, string assetno, string assetdesc, string assetcat, string assettyp, string status)
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

        if (currcomp.Length > 0 && assetno.Length > 0)
        {
            AccountingModel modAsset = oAccCon.getFisAssetDetail(currcomp, 0, assetno, "", "", assettyp, "");
            if (modAsset.GetSetid == 0)
            {
                modAsset = new AccountingModel();
                modAsset.GetSetcomp = currcomp;
                modAsset.GetSetassetno = assetno;
                modAsset.GetSetassetdesc = assetdesc;
                modAsset.GetSetassetcat = assetcat;
                modAsset.GetSetassettyp = assettyp;
                modAsset.GetSetstatus = status;
                if (modAsset.GetSetstatus.Equals("NEW"))
                {
                    modAsset.GetSetcreatedby = sUserId;
                }
                else if (modAsset.GetSetstatus.Equals("CONFIRMED"))
                {
                    modAsset.GetSetconfirmedby = sUserId;
                }
                else if (modAsset.GetSetstatus.Equals("CANCELLED"))
                {
                    modAsset.GetSetcancelledby = sUserId;
                }
                int i = oAccCon.insertFisAsset(modAsset);
                if (i > 0)
                {
                    sStatus = "Y";
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table FisAsset...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & AssetNo: " + modAsset.GetSetassetno + " & Nama/ Keterangan: " + modAsset.GetSetassetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisAssetDetail(string currcomp, int id, string assetno, string assetdesc, string assetcat, string assettyp, string status)
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

        if (currcomp.Length > 0 && id > 0)
        {
            AccountingModel modAsset = oAccCon.getFisAssetDetail(currcomp, id, "", "", "", "", "");
            if(modAsset.GetSetid > 0)
            {
                bool proceeUpdate = false;
                ArrayList lsFisCOATran = oAccCon.getFisCOATranList(modAsset.GetSetcomp, "", "", "", "", 0, "", "", modAsset.GetSetassetno, "", "", "");
                if (lsFisCOATran.Count > 0)
                {
                    if (modAsset.GetSetassetno.Equals(assetno) && modAsset.GetSetassetdesc.Equals(assetdesc))
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in FisCOATran for Comp: " + modAsset.GetSetcomp + " & Name/ Keterangan: " + modAsset.GetSetassetdesc + ". Please remove all record first...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }

                if (proceeUpdate) 
                {
                    modAsset.GetSetcomp = currcomp;
                    modAsset.GetSetassetno = assetno;
                    modAsset.GetSetassetdesc = assetdesc;
                    modAsset.GetSetassetcat = assetcat;
                    modAsset.GetSetassettyp = assettyp;
                    modAsset.GetSetstatus = status;
                    if (modAsset.GetSetstatus.Equals("NEW"))
                    {
                        modAsset.GetSetcreatedby = sUserId;
                    }
                    else if (modAsset.GetSetstatus.Equals("CONFIRMED"))
                    {
                        modAsset.GetSetconfirmedby = sUserId;
                    }
                    else if (modAsset.GetSetstatus.Equals("CANCELLED"))
                    {
                        modAsset.GetSetcancelledby = sUserId;
                    }
                    int i = oAccCon.updateFisAsset(modAsset);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisAsset...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Id: " + assetno + " & Nama/ Keterangan: " + assetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}