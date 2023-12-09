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

public partial class Accounting_COAMasterPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrType = "";
    public String sCurrCategory = "";
    public String sCurrAccNumber = "";
    public String sCurrOption = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisCOAMasterTran = new ArrayList();
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
        lsFisCOAMasterTran = oAccCon.getFisCOAMasterTranList(sCurrComp, sCurrFyr, sCurrAccId, "", "", 0, sCurrType, sCurrCategory, "", "", "", sCurrOption);
        /*
        lsFisCOAMaster = oAccCon.getFisCOAMasterList(sCurrComp, sCurrAccId, "", "", 0, sCurrType, sCurrCategory, "", "");
        lsFisCOATran = oAccCon.getFisCOATranList(sCurrComp, sCurrFyr, sCurrAccId, "", "", 0, sCurrType, sCurrCategory, "", "", "");
        for(int i=0; i<lsFisCOATran.Count; i++)
        {
            AccountingModel modItem = (AccountingModel)lsFisCOATran[i];
            lsFisAccId.Add(modItem.GetSetaccid);
        }
        */
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
            sCurrAccId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
        }
        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("lsFindType") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindType"));
        }
        if (Request.Params.Get("lsFindCategory") != null)
        {
            sCurrCategory = oAccCon.replaceNull(Request.Params.Get("lsFindCategory"));
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
    public static String getFisCOAList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOAMaster = oAccCon.getFisCOAMasterList(currcomp, "", "", "", 0, "", "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccMod.GetSetaccdesc
                };
                lsFisCOAId.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoalist = lsFisCOAId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisCOADetail(string currcomp, string accid)
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

        if (currcomp.Length > 0 && accid.Length > 0)
        {
            oAccMod = oAccCon.getFisCOAMasterDetail(currcomp, accid, "", "", 0, "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoadetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisCOADetail(string currcomp, string accid, string accdesc, string parentid, string accgroup, int acclevel, string endlevel, string acctype, string acccat, string acccode, string accnumber, string status)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && accid.Length > 0)
        {
            AccountingModel modItem = oAccCon.getFisCOAMasterDetail(currcomp, accid, "", "", 0, "", "", "", "");
            if (modItem.GetSetid == 0)
            {
                modItem = new AccountingModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetaccid = accid;
                modItem.GetSetaccdesc = accdesc;
                modItem.GetSetparentid = parentid;
                modItem.GetSetaccgroup = accgroup;
                modItem.GetSetacclevel = acclevel;
                modItem.GetSetendlevel = endlevel;
                modItem.GetSetacctype = acctype;
                modItem.GetSetacccategory = acccat;
                modItem.GetSetacccode = acccode;
                modItem.GetSetaccnumber = accnumber;
                modItem.GetSetstatus = status;
                if (modItem.GetSetstatus.Equals("NEW"))
                {
                    modItem.GetSetcreatedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CONFIRMED"))
                {
                    modItem.GetSetconfirmedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CANCELLED"))
                {
                    modItem.GetSetcancelledby = sUserId;
                }
                int i = oAccCon.insertFisCOAMaster(modItem);
                if (i > 0)
                {
                    sStatus = "Y";
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table FisCOAMaster...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Acc Id: " + modItem.GetSetaccid;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisCOADetail(string currcomp, int id, string accid, string accdesc, string parentid, string accgroup, int acclevel, string endlevel, string acctype, string acccat, string acccode, string accnumber, string status)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            AccountingModel modItem = oAccCon.getFisCOAMasterDetail(currcomp, id, "", "", "", 0, "", "", "", "");
            if(modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                ArrayList lsFisCOATran = oAccCon.getFisCOATranList(modItem.GetSetcomp, "", modItem.GetSetaccid, "", "", 0, "", "", "", "", "");
                if (lsFisCOATran.Count > 0)
                {
                    if (modItem.GetSetaccid.Equals(accid) && modItem.GetSetparentid.Equals(parentid))
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in FisCOATran for Comp: " + modItem.GetSetcomp + " & AccId: " + modItem.GetSetaccid + ". Please remove all record first...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }
                if (proceeUpdate) 
                { 
                    modItem.GetSetaccid = accid;
                    modItem.GetSetaccdesc = accdesc;
                    modItem.GetSetparentid = parentid;
                    modItem.GetSetaccgroup = accgroup;
                    modItem.GetSetacclevel = acclevel;
                    modItem.GetSetendlevel = endlevel;
                    modItem.GetSetacctype = acctype;
                    modItem.GetSetacccategory = acccat;
                    modItem.GetSetacccode = acccode;
                    modItem.GetSetaccnumber = accnumber;
                    modItem.GetSetstatus = status;
                    if (modItem.GetSetstatus.Equals("NEW"))
                    {
                        modItem.GetSetcreatedby = sUserId;
                    }
                    else if (modItem.GetSetstatus.Equals("CONFIRMED"))
                    {
                        modItem.GetSetconfirmedby = sUserId;
                    }
                    else if (modItem.GetSetstatus.Equals("CANCELLED"))
                    {
                        modItem.GetSetcancelledby = sUserId;
                    }
                    int i = oAccCon.updateFisCOAMaster(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisCOAMaster...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Acc Id: " + id ;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisCOATranList(string currcomp, string fyr, string coaadd, string coaremove)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && fyr.Length > 0)
        {
            if (coaadd.Length > 0)
            {
                ArrayList lsCOAToAdd = oAccCon.tokenString(coaadd, ",");
                for (int i = 0; i < lsCOAToAdd.Count; i++) {
                    String sAccId = (String)lsCOAToAdd[i];
                    AccountingModel modItem = oAccCon.getFisCOAMasterDetail(currcomp, sAccId, "", "", 0, "", "", "", "");
                    modItem.GetSetfyr = fyr;
                    if (modItem.GetSetaccid.Length > 0)
                    {
                        oAccCon.insertFisCOATran(modItem);
                    }
                }
            }

            if (coaremove.Length > 0)
            {
                ArrayList lsCOAToRemove = oAccCon.tokenString(coaremove, ",");
                for (int i = 0; i < lsCOAToRemove.Count; i++)
                {
                    String sAccId = (String)lsCOAToRemove[i];
                    if (sAccId.Length > 0)
                    {
                        oAccCon.deleteFisCOATran(currcomp, fyr, sAccId);
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

    [WebMethod(EnableSession = true)]
    public static String getLegderAccGroup(string currcomp, string acctype)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisAccGroup = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOAMaster = oAccCon.getFisCOAMasterList(currcomp, "", "", "", 1, acctype, "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccgroup = oAccMod.GetSetaccgroup,
                    GetSetaccdesc = oAccMod.GetSetaccdesc
                };
                lsFisAccGroup.Add(objData);
            }
            sStatus = "Y";
            sMessage = "";
        }

        object retData = new { result = sStatus, message = sMessage, fisaccgroup = lsFisAccGroup };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getLegderParentId(string currcomp, string acctype, string accgroup, int acclevel)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisParentId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOAMaster = oAccCon.getFisCOAMasterList(currcomp, "", "", accgroup, (acclevel-1), acctype, "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccMod.GetSetaccdesc
                };
                lsFisParentId.Add(objData);
            }
            sStatus = "Y";
            sMessage = "";
        }

        object retData = new { result = sStatus, message = sMessage, fisparentid = lsFisParentId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisBankList(string currcomp, string bankid, string accountno)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisBankId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisBank = oAccCon.getFisBankList(currcomp, 0, bankid, "", accountno, "");
            for (int i = 0; i < lsFisBank.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisBank[i];

                Object objData = new
                {
                    GetSetbankid = oAccMod.GetSetbankid,
                    GetSetaccountno = oAccMod.GetSetaccountno,
                    GetSetcurrency = oAccMod.GetSetcurrency,
                    GetSetbankname = oAccMod.GetSetbankname
                };
                lsFisBankId.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbanklist = lsFisBankId };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisBpList(string currcomp, string bpid, string bpdesc)
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
            ArrayList lsFisBp = oAccCon.getFisBpList(currcomp, 0, bpid, bpdesc, "", "");
            for (int i = 0; i < lsFisBp.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisBp[i];

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
    public static String getFisItemList(string currcomp, string itemno, string itemdesc)
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
            ArrayList lsFisItem = oAccCon.getFisItemList(currcomp, 0, itemno, itemdesc, "INVENTORY", "");
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
    public static String getFisAssetList(string currcomp, string assetno, string assetdesc, string assettyp)
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
            ArrayList lsFisAsset = oAccCon.getFisAssetList(currcomp, 0, assetno, assetdesc, "", assettyp, "");
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

}