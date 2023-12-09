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

public partial class Accounting_BankPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrBankId = "";
    public String sCurrBankAcctNo = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBank = new ArrayList();
    public ArrayList lsFisBankId = new ArrayList();
    public ArrayList lsFisBankAcctNo = new ArrayList();

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
        lsFisBank = oAccCon.getFisBankList(sCurrComp, 0, sCurrBankId, "", sCurrBankAcctNo, "");
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

        if (Request.Params.Get("txtFindBankId") != null)
        {
            sCurrBankId = oAccCon.replaceNull(Request.Params.Get("txtFindBankId"));
        }
        if (Request.Params.Get("txtFindAccountNo") != null)
        {
            sCurrBankAcctNo = oAccCon.replaceNull(Request.Params.Get("txtFindAccountNo"));
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
    public static String getFisBankList(string currcomp)
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
            ArrayList lsFisBank = oAccCon.getFisBankList(currcomp, 0, "", "", "", "");
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
    public static String getFisBankDetail(string currcomp, int id)
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
            oAccMod = oAccCon.getFisBankDetail(currcomp, id, "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbankdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisBankDetail(string currcomp, string bankid, string bankname, string address, string accnumber, string currency, double exrate, string contact, string contactno, string status)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisBankId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && bankid.Length > 0 && accnumber.Length > 0)
        {
            AccountingModel modItem = oAccCon.getFisBankDetail(currcomp, 0, bankid, "", accnumber, "");
            if (modItem.GetSetid == 0)
            {
                modItem = new AccountingModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetbankid = bankid;
                modItem.GetSetbankname = bankname;
                modItem.GetSetaccountno = accnumber;
                modItem.GetSetaddress = address;
                modItem.GetSetcontact = contact;
                modItem.GetSetcontactno = contactno;
                modItem.GetSetcurrency = currency;
                modItem.GetSetexrate = exrate;
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
                int i = oAccCon.insertFisBank(modItem);
                if (i > 0)
                {
                    sStatus = "Y";
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table FisBank...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Bank Id: " + modItem.GetSetbankid + " & No. Akaun: " + modItem.GetSetaccnumber;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisBankDetail(string currcomp, int id, string bankid, string bankname, string address, string accnumber, string currency, double exrate, string contact, string contactno, string status)
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
            AccountingModel modItem = oAccCon.getFisBankDetail(currcomp, id, "", "", "", "");
            if(modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                ArrayList lsFisCOATran = oAccCon.getFisCOATranList(modItem.GetSetcomp, "", "", "", "", 0, "", "BANK", modItem.GetSetacccode, modItem.GetSetaccnumber, "", "");
                if (lsFisCOATran.Count > 0)
                {
                    if (modItem.GetSetaccountno.Equals(accnumber) && modItem.GetSetbankid.Equals(bankid))
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
                    modItem.GetSetbankid = bankid;
                    modItem.GetSetbankname = bankname;
                    modItem.GetSetaccountno = accnumber;
                    modItem.GetSetaddress = address;
                    modItem.GetSetcontact = contact;
                    modItem.GetSetcontactno = contactno;
                    modItem.GetSetcurrency = currency;
                    modItem.GetSetexrate = exrate;
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
                    int i = oAccCon.updateFisBank(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisBank...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Bank Id: " + bankid + " & No. Akaun: " + accnumber;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}