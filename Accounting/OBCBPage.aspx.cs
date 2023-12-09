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

public partial class Accounting_OBCBPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrType = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBalance = new ArrayList();

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
        lsFisBalance = oAccCon.getFisBalanceList(sCurrComp, sCurrFyr, sCurrType, "");
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

        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("lsFindOption") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindOption"));
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
    public static String getFisFYRList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisFYR = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            lsFisFYR = oAccCon.getFisFYRList(currcomp);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisfyrlist = lsFisFYR };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisBalance(string currcomp, string fyr, int id)
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
            oAccMod = oAccCon.getFisBalance(currcomp, fyr, id, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbalance = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisBalance(string currcomp, string fyr, string trancode, string trandate, string trandesc, string currency, double debit, double credit, string status)
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

        if (currcomp.Length > 0 && fyr.Length > 0 && trancode.Length > 0)
        {
            AccountingModel modItem = new AccountingModel();
            modItem.GetSetcomp = currcomp;
            modItem.GetSetfyr = fyr;
            modItem.GetSettranno = oAccCon.getNextRunningNo(modItem.GetSetcomp, modItem.GetSetfyr, trancode, "ACTIVE");
            modItem.GetSettrancode = trancode;
            modItem.GetSettrandate = trandate;
            modItem.GetSettrandesc = trandesc;
            modItem.GetSetcurrency = currency;
            modItem.GetSetexrate = 1;
            modItem.GetSetdebit = debit;
            modItem.GetSetcredit = credit;
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
            int i = oAccCon.insertFisBalance(modItem);
            if (i > 0)
            {
                oAccCon.updateNextRunningNo(modItem.GetSetcomp, modItem.GetSetfyr, trancode, "ACTIVE");

                if (trancode.Equals("OPENING_BALANCE"))
                {
                    ArrayList lsFisBalanceDet = oAccCon.getFisAccountBalanceList(modItem.GetSetcomp, modItem.GetSetfyr, "", "", modItem.GetSettranno, modItem.GetSettrancode, "");

                    for (int x = 0; x < lsFisBalanceDet.Count; x++)
                    {
                        AccountingModel modItemBalance = (AccountingModel)lsFisBalanceDet[x];
                        if (modItemBalance.GetSetendlevel.Equals("Y"))
                        {
                            //modItemBalance.GetSetbalid = modItem.GetSetid;
                            modItemBalance.GetSettranno = modItem.GetSettranno;
                            modItemBalance.GetSettrancode = modItem.GetSettrancode;
                            modItemBalance.GetSettrandate = modItem.GetSettrandate;
                            modItemBalance.GetSetcurrency = modItem.GetSetcurrency;
                            modItemBalance.GetSetexrate = modItem.GetSetexrate;
                            modItemBalance.GetSetstatus = modItem.GetSetstatus;
                            if (modItemBalance.GetSetstatus.Equals("NEW"))
                            {
                                modItemBalance.GetSetcreatedby = sUserId;
                            }
                            else if (modItemBalance.GetSetstatus.Equals("CONFIRMED"))
                            {
                                modItemBalance.GetSetconfirmedby = sUserId;
                            }
                            else if (modItemBalance.GetSetstatus.Equals("CANCELLED"))
                            {
                                modItemBalance.GetSetcancelledby = sUserId;
                            }

                            AccountingModel modItemLedgerTran = modItemBalance;
                            modItemLedgerTran.GetSetledgerdate = modItemBalance.GetSettrandate;

                            if (modItemLedgerTran.GetSetacctype.Equals("A") || modItemLedgerTran.GetSetacctype.Equals("B"))
                            {
                                modItemLedgerTran.GetSetledgerno = 1;
                            }
                            else
                            {
                                modItemLedgerTran.GetSetledgerno = 2;
                            }
                            int y = oAccCon.insertFisLedgerTran(modItemLedgerTran);

                        }
                    }
                }
                else if (trancode.Equals("CLOSING_BALANCE"))
                {

                }
                sStatus = "Y";
                sMessage = "Tambah berjaya!";
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record error for Comp: " + currcomp + " & fyr: " + fyr + " & Type: " + trancode;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisBalance(string currcomp, string fyr, int id, string trandate, string trandesc, string currency, double debit, double credit, string status)
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

        if (currcomp.Length > 0 && fyr.Length > 0 && id > 0)
        {
            AccountingModel modItem = oAccCon.getFisBalance(currcomp, fyr, id, "");
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                if (modItem.GetSetstatus.Equals("CONFIRMED"))
                {
                    if (modItem.GetSetdebit == modItem.GetSetcredit)
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Jumlah Debit: [" + modItem.GetSetdebit + "] TIDAK BERSAMAAN dengan Jumlah Credit: [" + modItem.GetSetcredit + "]...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }

                if (proceeUpdate)
                {
                    modItem.GetSetfyr = fyr;
                    modItem.GetSettrandate = trandate;
                    modItem.GetSettrandesc = trandesc;
                    modItem.GetSetcurrency = currency;
                    modItem.GetSetdebit = debit;
                    modItem.GetSetcredit = credit;
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
                    int i = oAccCon.updateFisBalance(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                        if (modItem.GetSetstatus.Equals("CONFIRMED"))
                        {
                            ArrayList lsLedgerTran = oAccCon.getFisLedgerTranList(modItem.GetSetcomp, modItem.GetSetfyr, 0, "", 0, "", modItem.GetSettranno, modItem.GetSettrancode, "");
                            for (int j = 0; j < lsLedgerTran.Count; j++)
                            {
                                AccountingModel modLedgerTran = (AccountingModel)lsLedgerTran[j];
                                modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                                modLedgerTran.GetSetstatus = modItem.GetSetstatus;
                                modLedgerTran.GetSetconfirmedby = modItem.GetSetconfirmedby;
                                int k = oAccCon.updateFisLedgerTran(modLedgerTran);
                            }
                        }
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisBalance...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Fyr: " + fyr + " & Balance Id: " + id;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}