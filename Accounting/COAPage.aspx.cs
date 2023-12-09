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

public partial class Accounting_COAPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrType = "";
    public String sCurrCategory = "";
    public String sCurrAccNumber = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
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

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        if (sAction.Equals("CREATE"))
        {
            ArrayList lsFisCOAMaster = new ArrayList();
            lsFisCOAMaster = oAccCon.getFisCOAMasterList(sCurrComp, "", "", "", 0, "", "", "", "CONFIRMED");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel modItem = (AccountingModel)lsFisCOAMaster[i];
                modItem.GetSetfyr = sCurrFyr;
                if (modItem.GetSetaccid.Length > 0)
                {
                    oAccCon.insertFisCOATran(modItem);
                }
            }
            if(oAccCon.getRunningNoList(sCurrComp, sCurrFyr,"","ACTIVE").Count == 0)
            {
                int i = oAccCon.createNewRunningNoList(sCurrComp, sCurrFyr);
            } 
        }

        sTotalPage = "1";
        sCurrentPage = "1";
        lsFisCOATran = oAccCon.getFisCOATranList(sCurrComp, sCurrFyr, "", "", "", 0, sCurrType, "", "", "", "");
        if (sCurrComp.Length > 0 && sCurrFyr.Length > 0 && (sCurrAccId.Length > 0 || sCurrCategory.Length > 0 || sCurrAccNumber.Length > 0))
        {
            lsFisAccId = oAccCon.searchFisCOATranList(lsFisAccId, sCurrComp, sCurrFyr, sCurrAccId, sCurrType, sCurrCategory, sCurrAccNumber, "");
        }
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
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oAccCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("txtFindLedgerNo") != null)
        {
            sCurrAccId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
        }
        if (!sAction.Equals("RESET"))
        {
            if (Request.Params.Get("lsFindFyr") != null)
            {
                sCurrFyr = oAccCon.replaceNull(Request.Params.Get("lsFindFyr"));
            }
        }
        if (Request.Params.Get("lsFindType") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindType"));
        }
        if (Request.Params.Get("lsFindCategory") != null)
        {
            sCurrCategory = oAccCon.replaceNull(Request.Params.Get("lsFindCategory"));
        }
        if (Request.Params.Get("txtFindAccNumber") != null)
        {
            sCurrAccNumber = oAccCon.replaceNull(Request.Params.Get("txtFindAccNumber"));
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
    public static String getFisCOAList(string currcomp, string currfyr)
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
            ArrayList lsFisCOATran = oAccCon.getFisCOATranList(currcomp, currfyr, "", "", "", 0, "", "", "", "", "");
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOATran[i];

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
    public static String getFisCOADetail(string currcomp, string fyr, string accid)
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
            oAccMod = oAccCon.getFisCOATranDetail(currcomp, fyr, accid, "", "", 0, "", "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fiscoadetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisCOADetail(string currcomp, int id, string fyr, string accid, string accdesc, string parentid, string accgroup, int acclevel, string endlevel, string acctype, string acccat, string acccode, string accnumber, string status)
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
            AccountingModel modItem = oAccCon.getFisCOATranDetail(currcomp, id, fyr, "", "", "", 0, "", "", "", "", "");
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = true;
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
                    int i = oAccCon.updateFisCOATran(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisCOATran...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Acc Id: " + id;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getLegderAccGroup(string currcomp, string fyr, string acctype)
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
            ArrayList lsFisCOATran = oAccCon.getFisCOATranList(currcomp, fyr, "", "", "", 1, acctype, "", "", "", "");
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOATran[i];

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
    public static String getLegderParentId(string currcomp, string fyr, string acctype, string accgroup, int acclevel)
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
            ArrayList lsFisCOATran = oAccCon.getFisCOATranList(currcomp, fyr, "", "", accgroup, (acclevel - 1), acctype, "", "", "", "");
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOATran[i];

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

}