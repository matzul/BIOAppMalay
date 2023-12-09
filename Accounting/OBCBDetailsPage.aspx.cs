using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_OBCBDetailsPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrAccId = "";
    public String sCurrType = "";
    public String sCurrTranCode = "";
    public int iCurrTranNo = 0;
    public String sCurrTranDate = "";
    public String sCurrCurrency = "";
    public double dCurrExRate = 0;
    public String sStatusCases = "CONFIRMED";
    public double dTotalProfitLoss = 0;

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBalanceDetails = new ArrayList();
    public AccountingModel modFisBalance = new AccountingModel();

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
        if (Request.QueryString["tranno"] != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(Request.QueryString["tranno"]);
        }
        if (Request.QueryString["trancode"] != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.QueryString["trancode"]);
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        modFisBalance = oAccCon.getFisBalance(sCurrComp, sCurrFyr, iCurrTranNo, sCurrTranCode, "");
        lsFisBalanceDetails = oAccCon.getFisAccountBalanceList(sCurrComp, sCurrFyr, sCurrAccId, "", iCurrTranNo, sCurrTranCode, "");
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

        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["tranno"] != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(Request.QueryString["tranno"]);
        }
        if (Request.QueryString["trancode"] != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.QueryString["trancode"]);
        }

        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }

        if (Request.Params.Get("txtFindLedgerNo") != null)
        {
            sCurrAccId = oAccCon.replaceNull(Request.Params.Get("txtFindLedgerNo"));
        }

        if (Request.Params.Get("lsFindType") != null)
        {
            sCurrType = oAccCon.replaceNull(Request.Params.Get("lsFindType"));
        }

        if (Request.Params.Get("lsFindTranCode") != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.Params.Get("lsFindTranCode"));
        }

        if (Request.Params.Get("txtFindTranNo") != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(Request.Params.Get("txtFindTranNo"));
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
    public static String getFisCOAList(string currcomp, string fyr)
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
            ArrayList lsFisCOAMaster = oAccCon.getFisCOATranList(currcomp, fyr, "", "", "", "Y", "", "", "", "", "");
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
    public static String updateFisBalanceDetails2(string currcomp, string fyr, int tranno, string trancode, string fisbalanceupdate)
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


        if (currcomp.Length > 0 && tranno > 0 && trancode.Length > 0)
        {
            if (fisbalanceupdate.Length > 0)
            {
                ArrayList lsFisBalanceToUpdate = oAccCon.tokenString(fisbalanceupdate, ",");
                for (int i = 0; i < lsFisBalanceToUpdate.Count; i++)
                {
                    String fisbalancedata = (String)lsFisBalanceToUpdate[i];
                    ArrayList lsFisBalanceData = oAccCon.tokenString(fisbalancedata, "|");
                    if (lsFisBalanceData.Count > 0)
                    {
                        String accid = lsFisBalanceData.Count > 0 ? (String)lsFisBalanceData[0] : "";
                        double debit = lsFisBalanceData.Count > 1 ? ((String)lsFisBalanceData[1]).Trim().Length > 0 ? double.Parse((String)lsFisBalanceData[1]) : 0 : 0;
                        double credit = lsFisBalanceData.Count > 2 ? ((String)lsFisBalanceData[2]).Trim().Length > 0 ? double.Parse((String)lsFisBalanceData[2]) : 0 : 0;
                        String remarks = lsFisBalanceData.Count > 3 ? (String)lsFisBalanceData[3] : "";

                        AccountingModel modItemLedger = oAccCon.getFisLedgerTran(currcomp, fyr, 0, accid, 0, "", tranno, trancode, "");
                        if (modItemLedger.GetSetid > 0)
                        {
                            modItemLedger.GetSetdebit = debit;
                            modItemLedger.GetSetcredit = credit;
                            modItemLedger.GetSetremarks = remarks;
                            int k = oAccCon.updateFisLedgerTran(modItemLedger);
                        }
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";

                        /*
                        AccountingModel modItem = oAccCon.getFisBalance(currcomp, fyr, tranno, trancode, "");
                        if (modItem.GetSetid > 0)
                        {

                            AccountingModel modItemLedger = oAccCon.getFisLedgerTran(modItem.GetSetcomp, modItem.GetSetfyr, 0, accid, 0, modItem.GetSettrandate, modItem.GetSettranno, modItem.GetSettrancode, "");
                            if (modItemLedger.GetSetid > 0)
                            {
                                modItemLedger.GetSetcurrency = modItem.GetSetcurrency;
                                modItemLedger.GetSetexrate = modItem.GetSetexrate;
                                modItemLedger.GetSetdebit = debit;
                                modItemLedger.GetSetcredit = credit;
                                modItemLedger.GetSetremarks = remarks;
                                int k = oAccCon.updateFisLedgerTran(modItemLedger);
                            }
                            sStatus = "Y";
                            sMessage = "Kemaskini berjaya!";
                        }
                        */
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & TranCode: " + trancode + " & TranNo: " + tranno;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisBalanceInfo(string currcomp, string fyr, int tranno, string trancode)
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

        if (currcomp.Length > 0 && tranno > 0 && fyr.Length > 0 && trancode.Length > 0)
        {
            int z = oAccCon.updateFisBalanceInfo(currcomp, fyr, tranno, trancode);
            if (z > 0)
            {
                sStatus = "Y";
                sMessage = "Kemaskini berjaya!";
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Error on updating table updateFisBalanceInfo...";
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteFisBalanceDetails(string currcomp, string fyr, int id)
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

        if (currcomp.Length > 0 && id > 0 && fyr.Length > 0)
        {
            bool proceeUpdate = false;

            AccountingModel modItem = oAccCon.getFisLedgerTran(currcomp, fyr, id, "", 0, "", 0, "", "");
            if (modItem.GetSetid > 0)
            {

                int y = oAccCon.deleteFisLedgerTran(modItem.GetSetcomp, modItem.GetSetid);
                proceeUpdate = true;
            }

            if (proceeUpdate)
            {
                int z = oAccCon.updateFisBalanceInfo(currcomp, fyr, modItem.GetSettranno, modItem.GetSettrancode);
                if (z > 0)
                {
                    sStatus = "Y";
                    sMessage = "Hapus rekod berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus rekod tidak berjaya! Error on updating table updateFisBalance...";
                }
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
    public static String getLegderAccId(string currcomp, string fyr, string acctype, string accgrp, string endlevel)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisAccId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOATran = oAccCon.getFisCOATranList(currcomp, fyr, "", "", accgrp, endlevel, acctype, "", "", "", "");
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOATran[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccMod.GetSetaccdesc
                };
                lsFisAccId.Add(objData);
            }
            sStatus = "Y";
            sMessage = "";
        }

        object retData = new { result = sStatus, message = sMessage, fisaccid = lsFisAccId };

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
    public static String insertFisBalanceDetails(string currcomp, string fyr, int tranno, string trancode, string accid, double exrate, double debit, double credit)
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

        if (currcomp.Length > 0 && fyr.Length > 0 && tranno > 0 && trancode.Length > 0 && accid.Length > 0)
        {
            AccountingModel modItem = oAccCon.getFisBalance(currcomp, fyr, tranno, trancode, "");

            ArrayList lsFisBalanceDet = oAccCon.getFisAccountBalanceList(modItem.GetSetcomp, modItem.GetSetfyr, accid, "", modItem.GetSettranno, modItem.GetSettrancode, "");

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
            sStatus = "Y";
            sMessage = "Tambah berjaya!";

            /*
            AccountingModel modItem = oAccCon.getFisBalance(currcomp, fyr, tranno, trancode, "");

            ArrayList lsFisBalanceDet = oAccCon.getFisAccountBalanceList(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetid, accid, "", tranno, trancode, "");

            for (int x = 0; x < lsFisBalanceDet.Count; x++)
            {
                AccountingModel modItemBalance = (AccountingModel)lsFisBalanceDet[x];
                if (modItemBalance.GetSetendlevel.Equals("Y"))
                {
                    modItemBalance.GetSetbalid = modItem.GetSetid;
                    modItemBalance.GetSettranno = tranno;
                    modItemBalance.GetSetbaldatetime = modItem.GetSetbaldatetime;
                    modItemBalance.GetSetcurrency = modItem.GetSetcurrency;
                    modItemBalance.GetSetexrate = 1.0;
                    modItemBalance.GetSetdebit = debit;
                    modItemBalance.GetSetcredit = credit;
                    modItemBalance.GetSetstatus = "NEW";
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
                    modItemLedgerTran.GetSetledgerdate = modItemBalance.GetSetbaldatetime;
                    if (modItemLedgerTran.GetSetacctype.Equals("A") || modItemLedgerTran.GetSetacctype.Equals("B"))
                    {
                        modItemLedgerTran.GetSetledgerno = 1;
                    }
                    else
                    {
                        modItemLedgerTran.GetSetledgerno = 2;
                    }
                    int y = oAccCon.insertFisLedgerTran(modItemLedgerTran);

                    int z = oAccCon.insertFisBalanceDetails(modItemBalance);
                }
            }

            sStatus = "Y";
            sMessage = "Tambah berjaya!";
            */

        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String confirmFisBalanceDetails(string currcomp, string fyr, int tranno, string trancode)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsFisBalanceDetails = new ArrayList();
        AccountingModel modItem = new AccountingModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && tranno > 0 && fyr.Length > 0 && trancode.Length > 0)
        {
            if (trancode.Equals("OPENING_BALANCE"))
            {
                modItem = oAccCon.getFisBalance(currcomp, fyr, tranno, trancode, "");
                if (modItem.GetSetid > 0)
                {
                    bool proceeUpdate = false;
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

                    if (proceeUpdate)
                    {
                        modItem.GetSetstatus = "CONFIRMED";
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
                    sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Fyr: " + fyr + " & TranCode: " + trancode + " & TranNo: " + tranno;

                }

            }
            else if (trancode.Equals("CLOSING_BALANCE"))
            {

                modItem = oAccCon.getFisBalance(currcomp, fyr, tranno, trancode, "");

                String sNextTranDate = oAccCon.getNextSecond(modItem.GetSettrandate, 1);

                if (sNextTranDate.Trim().Length > 0)
                {
                    DateTime nextdatetime = Convert.ToDateTime(sNextTranDate, oAccCon.ukDtfi);

                    if (oAccCon.getNextRunningNo(currcomp, nextdatetime.ToString("yyyy"), "OPENING_BALANCE", "ACTIVE") > 0 && oAccCon.getNextRunningNo(currcomp, fyr, "PROFIT_LOSS", "ACTIVE") > 0)
                    {
                        //BEGIN: ADDED NEW insert into ledger for profit and loss 
                        Double ClosingPnLDebit = 0;
                        Double ClosingPnLCredit = 0;
                        AccountingModel modFisOpeningBalance = oAccCon.getLastFisBalance(currcomp, fyr, "OPENING_BALANCE", modItem.GetSettrandate, "");
                        ArrayList lsProfitLossDetails = oAccCon.getFisCOATranListWithoutClosing(currcomp, fyr, "", "", "", "Y", "", "", "", modFisOpeningBalance.GetSettrandate, modItem.GetSettrandate, "");
                        for (int i = 0; i < lsProfitLossDetails.Count; i++)
                        {
                            AccountingModel modFisCOA = (AccountingModel)lsProfitLossDetails[i];
                            if (modFisCOA.GetSetacctype.Equals("H") || modFisCOA.GetSetacctype.Equals("B"))
                            {
                                ClosingPnLDebit = ClosingPnLDebit + modFisCOA.GetSetdebit;
                                ClosingPnLCredit = ClosingPnLCredit + modFisCOA.GetSetcredit;
                            }
                        }
                        if(ClosingPnLCredit - ClosingPnLDebit != 0)
                        {
                            //confirm insert into ledger for profit and loss 
                            AccountingModel modJournalTran = new AccountingModel();
                            
                            modJournalTran.GetSetcomp = currcomp;
                            modJournalTran.GetSetfyr = fyr;
                            modJournalTran.GetSettranno = oAccCon.getNextRunningNo(currcomp, fyr, "PROFIT_LOSS", "ACTIVE");
                            modJournalTran.GetSettrancode = "PROFIT_LOSS";
                            modJournalTran.GetSettrandesc = "KEUNTUNGAN ATAU KERUGIAN";
                            modJournalTran.GetSettrandate = modItem.GetSettrandate;
                            modJournalTran.GetSetcurrency = "MYR";
                            modJournalTran.GetSettranamount = (ClosingPnLCredit > ClosingPnLDebit ? ClosingPnLCredit - ClosingPnLDebit: ClosingPnLDebit - ClosingPnLCredit);
                            modJournalTran.GetSetexrate = 1;
                            modJournalTran.GetSetstatus = "CONFIRMED";
                            if (modJournalTran.GetSetstatus.Equals("NEW"))
                            {
                                modJournalTran.GetSetcreatedby = sUserId;
                            }
                            else if (modJournalTran.GetSetstatus.Equals("CONFIRMED"))
                            {
                                modJournalTran.GetSetconfirmedby = sUserId;
                            }
                            else if (modJournalTran.GetSetstatus.Equals("CANCELLED"))
                            {
                                modJournalTran.GetSetcancelledby = sUserId;
                            }
                            int i = oAccCon.insertJournalEntryDetails(modJournalTran);
                            if (i > 0)
                            {
                                oAccCon.updateNextRunningNo(modJournalTran.GetSetcomp, modJournalTran.GetSetfyr, modJournalTran.GetSettrancode, "ACTIVE");
                                if (ClosingPnLCredit > ClosingPnLDebit)
                                {                                    
                                    //Debit Side
                                    AccountingModel modLedgerTran = new AccountingModel();
                                    modLedgerTran.GetSetcomp = modJournalTran.GetSetcomp;
                                    modLedgerTran.GetSetfyr = modJournalTran.GetSetfyr;
                                    modLedgerTran.GetSettranno = modJournalTran.GetSettranno;
                                    modLedgerTran.GetSettrancode = modJournalTran.GetSettrancode;
                                    modLedgerTran.GetSettrandate = modJournalTran.GetSettrandate;
                                    modLedgerTran.GetSetledgerdate = modJournalTran.GetSettrandate;
                                    modLedgerTran.GetSetrefno = modJournalTran.GetSetrefno;

                                    String COASales = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COASales"]);
                                    AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modLedgerTran.GetSetcomp, modLedgerTran.GetSetfyr, COASales, "", "", 0, "", "", "", "", "");
                                    modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                                    modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                                    modLedgerTran.GetSetcurrency = modJournalTran.GetSetcurrency;
                                    modLedgerTran.GetSetexrate = modJournalTran.GetSetexrate;
                                    modLedgerTran.GetSetledgerno = 1;
                                    modLedgerTran.GetSetdebit = ClosingPnLCredit - ClosingPnLDebit;
                                    modLedgerTran.GetSetcredit = 0;
                                    modLedgerTran.GetSetstatus = modJournalTran.GetSetstatus;
                                    if (modLedgerTran.GetSetstatus.Equals("NEW"))
                                    {
                                        modLedgerTran.GetSetcreatedby = sUserId;
                                    }
                                    else if (modLedgerTran.GetSetstatus.Equals("CONFIRMED"))
                                    {
                                        modLedgerTran.GetSetconfirmedby = sUserId;
                                    }
                                    else if (modLedgerTran.GetSetstatus.Equals("CANCELLED"))
                                    {
                                        modLedgerTran.GetSetcancelledby = sUserId;
                                    }

                                    //Credit Side
                                    AccountingModel modLedgerTran2 = new AccountingModel();
                                    modLedgerTran2.GetSetcomp = modJournalTran.GetSetcomp;
                                    modLedgerTran2.GetSetfyr = modJournalTran.GetSetfyr;
                                    modLedgerTran2.GetSettranno = modJournalTran.GetSettranno;
                                    modLedgerTran2.GetSettrancode = modJournalTran.GetSettrancode;
                                    modLedgerTran2.GetSettrandate = modJournalTran.GetSettrandate;
                                    modLedgerTran2.GetSetledgerdate = modJournalTran.GetSettrandate;
                                    modLedgerTran2.GetSetrefno = modJournalTran.GetSetrefno;

                                    String COAPnL = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAPnL"]);
                                    AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAPnL, "", "", 0, "", "", "", "", "");

                                    modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                                    modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                                    modLedgerTran2.GetSetcurrency = modJournalTran.GetSetcurrency;
                                    modLedgerTran2.GetSetexrate = modJournalTran.GetSetexrate;
                                    modLedgerTran2.GetSetledgerno = 2;
                                    modLedgerTran2.GetSetdebit = 0;
                                    modLedgerTran2.GetSetcredit = ClosingPnLCredit - ClosingPnLDebit;
                                    modLedgerTran2.GetSetstatus = modJournalTran.GetSetstatus;
                                    if (modLedgerTran2.GetSetstatus.Equals("NEW"))
                                    {
                                        modLedgerTran2.GetSetcreatedby = sUserId;
                                    }
                                    else if (modLedgerTran2.GetSetstatus.Equals("CONFIRMED"))
                                    {
                                        modLedgerTran2.GetSetconfirmedby = sUserId;
                                    }
                                    else if (modLedgerTran2.GetSetstatus.Equals("CANCELLED"))
                                    {
                                        modLedgerTran2.GetSetcancelledby = sUserId;
                                    }

                                    //to update debit & credit site
                                    if (modAccidDet.GetSetaccid.Trim().Length > 0 && modAccidDet2.GetSetaccid.Trim().Length > 0)
                                    {
                                        int x = oAccCon.insertFisLedgerTran(modLedgerTran);
                                        int y = oAccCon.insertFisLedgerTran(modLedgerTran2);
                                    }

                                }
                                else
                                {
                                    AccountingModel modLedgerTran = new AccountingModel();
                                    modLedgerTran.GetSetcomp = modJournalTran.GetSetcomp;
                                    modLedgerTran.GetSetfyr = modJournalTran.GetSetfyr;
                                    modLedgerTran.GetSettranno = modJournalTran.GetSettranno;
                                    modLedgerTran.GetSettrancode = modJournalTran.GetSettrancode;
                                    modLedgerTran.GetSettrandate = modJournalTran.GetSettrandate;
                                    modLedgerTran.GetSetledgerdate = modJournalTran.GetSettrandate;
                                    modLedgerTran.GetSetrefno = modJournalTran.GetSetrefno;

                                    String COAPnL = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAPnL"]);
                                    AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modLedgerTran.GetSetcomp, modLedgerTran.GetSetfyr, COAPnL, "", "", 0, "", "", "", "", "");
                                    modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                                    modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                                    modLedgerTran.GetSetcurrency = modJournalTran.GetSetcurrency;
                                    modLedgerTran.GetSetexrate = modJournalTran.GetSetexrate;
                                    modLedgerTran.GetSetledgerno = 1;
                                    modLedgerTran.GetSetdebit = ClosingPnLDebit - ClosingPnLCredit;
                                    modLedgerTran.GetSetcredit = 0;
                                    modLedgerTran.GetSetstatus = modJournalTran.GetSetstatus;
                                    if (modLedgerTran.GetSetstatus.Equals("NEW"))
                                    {
                                        modLedgerTran.GetSetcreatedby = sUserId;
                                    }
                                    else if (modLedgerTran.GetSetstatus.Equals("CONFIRMED"))
                                    {
                                        modLedgerTran.GetSetconfirmedby = sUserId;
                                    }
                                    else if (modLedgerTran.GetSetstatus.Equals("CANCELLED"))
                                    {
                                        modLedgerTran.GetSetcancelledby = sUserId;
                                    }

                                    //Credit Side
                                    AccountingModel modLedgerTran2 = new AccountingModel();
                                    modLedgerTran2.GetSetcomp = modJournalTran.GetSetcomp;
                                    modLedgerTran2.GetSetfyr = modJournalTran.GetSetfyr;
                                    modLedgerTran2.GetSettranno = modJournalTran.GetSettranno;
                                    modLedgerTran2.GetSettrancode = modJournalTran.GetSettrancode;
                                    modLedgerTran2.GetSettrandate = modJournalTran.GetSettrandate;
                                    modLedgerTran2.GetSetledgerdate = modJournalTran.GetSettrandate;
                                    modLedgerTran2.GetSetrefno = modJournalTran.GetSetrefno;

                                    String COASales = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COASales"]);
                                    AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modLedgerTran2.GetSetcomp, modLedgerTran2.GetSetfyr, COASales, "", "", 0, "", "", "", "", "");

                                    modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                                    modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                                    modLedgerTran2.GetSetcurrency = modJournalTran.GetSetcurrency;
                                    modLedgerTran2.GetSetexrate = modJournalTran.GetSetexrate;
                                    modLedgerTran2.GetSetledgerno = 2;
                                    modLedgerTran2.GetSetdebit = 0;
                                    modLedgerTran2.GetSetcredit = ClosingPnLDebit - ClosingPnLCredit;
                                    modLedgerTran2.GetSetstatus = modJournalTran.GetSetstatus;
                                    if (modLedgerTran2.GetSetstatus.Equals("NEW"))
                                    {
                                        modLedgerTran2.GetSetcreatedby = sUserId;
                                    }
                                    else if (modLedgerTran2.GetSetstatus.Equals("CONFIRMED"))
                                    {
                                        modLedgerTran2.GetSetconfirmedby = sUserId;
                                    }
                                    else if (modLedgerTran2.GetSetstatus.Equals("CANCELLED"))
                                    {
                                        modLedgerTran2.GetSetcancelledby = sUserId;
                                    }

                                    //to update debit & credit site
                                    if (modAccidDet.GetSetaccid.Trim().Length > 0 && modAccidDet2.GetSetaccid.Trim().Length > 0)
                                    {
                                        int x = oAccCon.insertFisLedgerTran(modLedgerTran);
                                        int y = oAccCon.insertFisLedgerTran(modLedgerTran2);
                                    }
                                }
                            }
                        }
                        //END: ADDED NEW insert into ledger for profit and loss 

                        lsFisBalanceDetails = oAccCon.getFisAccountBalanceList(currcomp, fyr, "", "", tranno, trancode, "");

                        for (int x = 0; x < lsFisBalanceDetails.Count; x++)
                        {
                            AccountingModel modItemBalance = (AccountingModel)lsFisBalanceDetails[x];
                            if (modItemBalance.GetSetendlevel.Equals("Y"))
                            {
                                //modItemBalance.GetSetbalid = modItem.GetSetid;
                                modItemBalance.GetSettranno = modItem.GetSettranno;
                                modItemBalance.GetSettrancode = modItem.GetSettrancode;
                                modItemBalance.GetSettrandate = modItem.GetSettrandate;
                                modItemBalance.GetSetcurrency = modItem.GetSetcurrency;
                                modItemBalance.GetSetexrate = modItem.GetSetexrate;
                                modItemBalance.GetSetstatus = "CONFIRMED";
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

                                AccountingModel modItemLedgerTran = oAccCon.Clone(modItemBalance);
                                modItemLedgerTran.GetSetledgerdate = modItemBalance.GetSettrandate;

                                if (modItemLedgerTran.GetSetacctype.Equals("A"))
                                {
                                    modItemLedgerTran.GetSetledgerno = 1;
                                    modItemLedgerTran.GetSetcredit = (modItemBalance.GetSetcredit - modItemBalance.GetSetdebit > 0 ? modItemBalance.GetSetcredit - modItemBalance.GetSetdebit : 0);
                                    modItemLedgerTran.GetSetdebit = (modItemBalance.GetSetdebit - modItemBalance.GetSetcredit >= 0 ? modItemBalance.GetSetdebit - modItemBalance.GetSetcredit : 0);
                                }
                                else
                                {
                                    modItemLedgerTran.GetSetledgerno = 2;
                                    modItemLedgerTran.GetSetdebit = (modItemBalance.GetSetdebit - modItemBalance.GetSetcredit > 0 ? modItemBalance.GetSetdebit - modItemBalance.GetSetcredit : 0);
                                    modItemLedgerTran.GetSetcredit = (modItemBalance.GetSetcredit - modItemBalance.GetSetdebit >= 0 ? modItemBalance.GetSetcredit - modItemBalance.GetSetdebit : 0);
                                }
                                int y = oAccCon.insertFisLedgerTran(modItemLedgerTran);

                                modItem.GetSetdebit = modItem.GetSetdebit + modItemBalance.GetSetdebit;
                                modItem.GetSetcredit = modItem.GetSetcredit + modItemBalance.GetSetcredit;

                            }
                        }
                        modItem.GetSetstatus = "CONFIRMED";
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

                        int z = oAccCon.updateFisBalance(modItem);
                        if (z > 0)
                        {
                            AccountingModel modItemOpening = new AccountingModel();
                            modItemOpening.GetSetcomp = currcomp;
                            modItemOpening.GetSetfyr = nextdatetime.ToString("yyyy");
                            modItemOpening.GetSettranno = oAccCon.getNextRunningNo(currcomp, nextdatetime.ToString("yyyy"), "OPENING_BALANCE", "ACTIVE");
                            modItemOpening.GetSettrancode = "OPENING_BALANCE";
                            modItemOpening.GetSettrandate = sNextTranDate;
                            modItemOpening.GetSettrandesc = "Pembukaan Semula atas " + modItem.GetSettrandesc;
                            modItemOpening.GetSetcurrency = modItem.GetSetcurrency;
                            modItemOpening.GetSetexrate = 1;
                            modItemOpening.GetSetdebit = modItem.GetSetcredit;
                            modItemOpening.GetSetcredit = modItem.GetSetdebit;
                            modItemOpening.GetSetstatus = "NEW";
                            if (modItemOpening.GetSetstatus.Equals("NEW"))
                            {
                                modItemOpening.GetSetcreatedby = sUserId;
                            }
                            else if (modItemOpening.GetSetstatus.Equals("CONFIRMED"))
                            {
                                modItemOpening.GetSetconfirmedby = sUserId;
                            }
                            else if (modItemOpening.GetSetstatus.Equals("CANCELLED"))
                            {
                                modItemOpening.GetSetcancelledby = sUserId;
                            }
                            int i = oAccCon.insertFisBalance(modItemOpening);
                            if (i > 0)
                            {
                                oAccCon.updateNextRunningNo(currcomp, nextdatetime.ToString("yyyy"), "OPENING_BALANCE", "ACTIVE");

                                //looping insert ledger tran
                                for (int x = 0; x < lsFisBalanceDetails.Count; x++)
                                {
                                    AccountingModel modItemBalance = (AccountingModel)lsFisBalanceDetails[x];
                                    if (modItemBalance.GetSetendlevel.Equals("Y"))
                                    {
                                        //modItemBalance.GetSetbalid = modItem.GetSetid;
                                        modItemBalance.GetSetfyr = modItemOpening.GetSetfyr;
                                        modItemBalance.GetSettranno = modItemOpening.GetSettranno;
                                        modItemBalance.GetSettrancode = modItemOpening.GetSettrancode;
                                        modItemBalance.GetSettrandate = modItemOpening.GetSettrandate;
                                        modItemBalance.GetSetcurrency = modItemOpening.GetSetcurrency;
                                        modItemBalance.GetSetexrate = modItemOpening.GetSetexrate;
                                        modItemBalance.GetSetstatus = "NEW";
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

                                        AccountingModel modItemLedgerTran = oAccCon.Clone(modItemBalance);
                                        modItemLedgerTran.GetSetledgerdate = modItemBalance.GetSettrandate;

                                        if (modItemLedgerTran.GetSetacctype.Equals("A"))
                                        {
                                            modItemLedgerTran.GetSetledgerno = 1;
                                            modItemLedgerTran.GetSetdebit = (modItemBalance.GetSetcredit - modItemBalance.GetSetdebit > 0 ? modItemBalance.GetSetcredit - modItemBalance.GetSetdebit : 0);
                                            modItemLedgerTran.GetSetcredit = (modItemBalance.GetSetdebit - modItemBalance.GetSetcredit >= 0 ? modItemBalance.GetSetdebit - modItemBalance.GetSetcredit : 0);
                                        }
                                        else
                                        {
                                            modItemLedgerTran.GetSetledgerno = 2;
                                            modItemLedgerTran.GetSetcredit = (modItemBalance.GetSetdebit - modItemBalance.GetSetcredit > 0 ? modItemBalance.GetSetdebit - modItemBalance.GetSetcredit : 0);
                                            modItemLedgerTran.GetSetdebit = (modItemBalance.GetSetcredit - modItemBalance.GetSetdebit >= 0 ? modItemBalance.GetSetcredit - modItemBalance.GetSetdebit : 0);
                                        }

                                        int y = oAccCon.insertFisLedgerTran(modItemLedgerTran);

                                    }
                                }

                                sStatus = "Y";
                                sMessage = "Confirm berjaya!";
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Confirm tidak berjaya! Error on updating table updateFisBalance...";
                            }
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Confirm tidak berjaya! Error on updating table updateFisBalance...";
                        }
                    }
                    else //issue to get next running no accounting
                    {
                        sStatus = "N";
                        sMessage = "Confirm tidak berjaya! Error on getting next running number of accounting fyr & trans profit and loss...";
                    }
                }
                else //issue to get next date time
                {
                    sStatus = "N";
                    sMessage = "Confirm tidak berjaya! Error on getting next date and time...";
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}