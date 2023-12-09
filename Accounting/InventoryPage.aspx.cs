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

public partial class Accounting_InventoryPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrItemNo = "";
    public String sCurrItemDesc = "";
    public String sCurrItemCat = "INVENTORY";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisItem = new ArrayList();

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
        lsFisItem = oAccCon.getFisItemList(sCurrComp, 0, sCurrItemNo, sCurrItemDesc, sCurrItemCat, "");
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
    public static String getFisItemDetail(string currcomp, int id)
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
            oAccMod = oAccCon.getFisItemDetail(currcomp, id, "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisitemdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisItemDetail(string currcomp, string itemno, string itemdesc, string itemcat, string itemtype, string status)
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

        if (currcomp.Length > 0 && itemno.Length > 0)
        {
            AccountingModel modItem = oAccCon.getFisItemDetail(currcomp, 0, itemno, "", "", "");
            if (modItem.GetSetid == 0)
            {
                modItem = new AccountingModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetitemno = itemno;
                modItem.GetSetitemdesc = itemdesc;
                modItem.GetSetitemcat = itemcat;
                modItem.GetSetitemtype = itemtype;
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
                int i = oAccCon.insertFisItem(modItem);
                if (i > 0)
                {
                    sStatus = "Y";
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table FisItem...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & ItemNo: " + modItem.GetSetitemno + " & Nama/ Keterangan: " + modItem.GetSetitemdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisItemDetail(string currcomp, int id, string itemno, string itemdesc, string itemcat, string itemtype, string status)
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
            AccountingModel modItem = oAccCon.getFisItemDetail(currcomp, id, "", "", "", "");
            if(modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                ArrayList lsFisCOATran = oAccCon.getFisCOATranList(modItem.GetSetcomp, "", "", "", "", 0, "", "INVENTORY", modItem.GetSetitemno, "", "", "");
                if (lsFisCOATran.Count > 0)
                {
                    if (modItem.GetSetitemno.Equals(itemno) && modItem.GetSetitemdesc.Equals(itemdesc))
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in FisCOATran for Comp: " + modItem.GetSetcomp + " & Name/ Keterangan: " + modItem.GetSetitemdesc + ". Please remove all record first...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }

                if (proceeUpdate) 
                {
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetitemno = itemno;
                    modItem.GetSetitemdesc = itemdesc;
                    modItem.GetSetitemcat = itemcat;
                    modItem.GetSetitemtype = itemtype;
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
                    int i = oAccCon.updateFisItem(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisItem...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Id: " + itemno + " & Nama/ Keterangan: " + itemdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}