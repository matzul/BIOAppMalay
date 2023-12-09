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

public partial class Accounting_JournalEntryPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public int iCurrTranNo = 0;
    public String sCurrTranCode = "";
    public String sCurrTranDesc = "";
    public String sCurrTranDate = "";
    public String sCurrTranCurrency = "";
    public String sCurrStatus = "";
    public double dTotalDebit = 0;
    public double dTotalCredit = 0;
    public String sAction = "";
    public String sAlertMessage = "";

    public int iTotalLine = 0;
    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public AccountingModel modJournalTran = new AccountingModel();
    public ArrayList lsFisJournalTran = new ArrayList();
    public ArrayList lsFisAccId = new ArrayList();

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
        if (Request.QueryString["action"] != null)
        {
            sAction = oAccCon.replaceNull(Request.QueryString["action"]);
        }
        if (Request.QueryString["tranno"] != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(oAccCon.replaceNull(Request.QueryString["tranno"]).Equals("")?"0": Request.QueryString["tranno"]);
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

        if (sAction.Equals("SAVE"))
        {
            int newtranno = oAccCon.getNextRunningNo(sCurrComp, sCurrFyr, sCurrTranCode, "ACTIVE");

            modJournalTran = new AccountingModel();
            modJournalTran.GetSetcomp = sCurrComp;
            modJournalTran.GetSetfyr = sCurrFyr;
            modJournalTran.GetSettranno = newtranno;
            modJournalTran.GetSettrancode = sCurrTranCode;
            modJournalTran.GetSettrandesc = sCurrTranDesc;
            modJournalTran.GetSetfyr = sCurrFyr;
            modJournalTran.GetSettrandate = sCurrTranDate;
            modJournalTran.GetSetcurrency = sCurrTranCurrency;
            modJournalTran.GetSetexrate = 1;
            modJournalTran.GetSetstatus = sCurrStatus;
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
                if (newtranno == iCurrTranNo)
                {
                    sAlertMessage = "SUCCESS|Daftar maklumat Baucar berjaya!";
                }
                else
                {
                    sAlertMessage = "SUCCESS|Daftar maklumat Baucar berjaya! No. Transaksi telah diganti baharu kepada: " + newtranno;
                    iCurrTranNo = newtranno;
                }
            }
        }
        else if (sAction.Equals("UPDATE") || sAction.Equals("CANCEL"))
        {
            modJournalTran = oAccCon.getJournalEntryDetails(sCurrComp, sCurrFyr, iCurrTranNo, sCurrTranCode, "");
            modJournalTran.GetSettrandesc = sCurrTranDesc;
            modJournalTran.GetSettrandate = sCurrTranDate;
            modJournalTran.GetSetcurrency = sCurrTranCurrency;
            modJournalTran.GetSetstatus = sCurrStatus;
            int i = oAccCon.updateJournalEntryDetails(modJournalTran);
            if (i > 0)
            {
                if (modJournalTran.GetSetstatus.Equals("CANCELLED"))
                {
                    lsFisJournalTran = oAccCon.getFisLedgerTranList(sCurrComp, sCurrFyr, 0, "", 0, "", iCurrTranNo, sCurrTranCode, "");
                    for(int x=0; x< lsFisJournalTran.Count; x++)
                    {
                        AccountingModel modJournalDet = (AccountingModel)lsFisJournalTran[x];
                        modJournalDet.GetSetstatus = modJournalTran.GetSetstatus;
                        int y = oAccCon.updateFisLedgerTran(modJournalDet);
                    }
                }
            }
        }

        if (iCurrTranNo > 0 && sCurrTranCode.Length > 0)
        {
            modJournalTran = oAccCon.getJournalEntryDetails(sCurrComp, sCurrFyr, iCurrTranNo, sCurrTranCode, "");
            lsFisJournalTran = oAccCon.getFisLedgerTranList(sCurrComp, sCurrFyr, 0, "", 0, "", iCurrTranNo, sCurrTranCode, "");
        }
        if(modJournalTran.GetSetid == 0 && sCurrTranCode.Length > 0)
        {
            modJournalTran.GetSettranno = oAccCon.getNextRunningNo(sCurrComp, sCurrFyr, sCurrTranCode, "ACTIVE");
            modJournalTran.GetSettrancode = sCurrTranCode;
            modJournalTran.GetSetfyr = sCurrFyr;
            modJournalTran.GetSettrandate = DateTime.Now.ToString("dd-MM")+ "-" + sCurrFyr + " " + DateTime.Now.ToString("HH:mm:ss");
            //modJournalTran.GetSettrandate = DateTime(sCurrFyr, DateTime.Now.Month, DateTime.Now.Day).ToString("dd-MM-yyyy HH:mm:ss");
            modJournalTran.GetSetcurrency = "MYR";
            modJournalTran.GetSetexrate = 1;
            modJournalTran.GetSetstatus = "NEW";
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
        if (Request.Params.Get("txtFindFyr") != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.Params.Get("txtFindFyr"));
        }
        if (Request.Params.Get("txtFindTranNo") != null)
        {
            iCurrTranNo = oAccCon.replaceIntZero(Request.Params.Get("txtFindTranNo"));
        }
        if (Request.Params.Get("lsFindTranCode") != null)
        {
            sCurrTranCode = oAccCon.replaceNull(Request.Params.Get("lsFindTranCode"));
        }
        if (Request.Params.Get("txtFindTranDesc") != null)
        {
            sCurrTranDesc = oAccCon.replaceNull(Request.Params.Get("txtFindTranDesc"));
        }
        if (Request.Params.Get("txtFindTranDate") != null)
        {
            sCurrTranDate = oAccCon.replaceNull(Request.Params.Get("txtFindTranDate"));
        }
        if (Request.Params.Get("txtFindCurrency") != null)
        {
            sCurrTranCurrency = oAccCon.replaceNull(Request.Params.Get("txtFindCurrency"));
        }
        if (Request.Params.Get("lsFindStatus") != null)
        {
            sCurrStatus = oAccCon.replaceNull(Request.Params.Get("lsFindStatus"));
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
    public static String getFisCOATranList(string currcomp, string fyr, string acctype)
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
            ArrayList lsFisCOAMaster = oAccCon.getFisCOATranList(currcomp, fyr, "", "", "", "Y", acctype, "", "", "", "");
            for (int i = 0; i < lsFisCOAMaster.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisCOAMaster[i];

                Object objData = new
                {
                    GetSetaccid = oAccMod.GetSetaccid,
                    GetSetaccdesc = oAccCon.getFISCOATranParentDesc(oAccMod.GetSetcomp, oAccMod.GetSetfyr, oAccMod.GetSetaccid, oAccMod.GetSetacclevel)
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
    public static String getFisLedgerDetail(string currcomp, string currfyr, string accid, int tranno, string trancode)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisLedger = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && currfyr.Length > 0)
        {
            lsFisLedger = oAccCon.getFisLedgerTranList(currcomp, currfyr, 0, "", 0, "", tranno, trancode, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisledger = lsFisLedger };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updatePostingData(string currcomp, string fyr, string postdataupdate)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";
        double dTotalDebit = 0, dTotalCredit = 0;
        int tranno = 0;
        String trancode = "";
        String status = "";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && fyr.Length > 0 && postdataupdate.Length > 0)
        {
            ArrayList lsPostDataToUpdate = oAccCon.tokenString(postdataupdate, ",");
            for (int i = 0; i < lsPostDataToUpdate.Count; i++)
            {
                String postingdata = (String)lsPostDataToUpdate[i];
                ArrayList lsPostingData = oAccCon.tokenString(postingdata, "|");
                if (lsPostingData.Count > 0)
                {
                    tranno = lsPostingData.Count > 0 ? int.Parse((String)lsPostingData[0]) : 0;
                    trancode = lsPostingData.Count > 1 ? (String)lsPostingData[1] : "";
                    String trandate = lsPostingData.Count > 2 ? (String)lsPostingData[2] : "";
                    String refno = lsPostingData.Count > 3 ? (String)lsPostingData[3] : "";
                    Int64 id = lsPostingData.Count > 4 ? Int64.Parse((String)lsPostingData[4]) : 0;
                    String accid = lsPostingData.Count > 5 ? (String)lsPostingData[5] : "";
                    String accdesc = lsPostingData.Count > 6 ? (String)lsPostingData[6] : "";
                    String currency = lsPostingData.Count > 7 ? (String)lsPostingData[7] : "";
                    double exrate = lsPostingData.Count > 8 ? double.Parse((String)lsPostingData[8]) : 0;
                    int ledgerno = lsPostingData.Count > 9 ? int.Parse((String)lsPostingData[9]) : 0;
                    double debit = lsPostingData.Count > 10 ? ((String)lsPostingData[10]).Trim().Length > 0 ? double.Parse((String)lsPostingData[10]) : 0 : 0;
                    double credit = lsPostingData.Count > 11 ? ((String)lsPostingData[11]).Trim().Length > 0 ? double.Parse((String)lsPostingData[11]) : 0 : 0;
                    status = lsPostingData.Count > 12 ? (String)lsPostingData[12] : "NEW";

                    dTotalDebit = dTotalDebit + debit;
                    dTotalCredit = dTotalCredit + credit;

                    if (tranno > 0 && trancode.Length > 0 && accid.Length > 0 && trandate.Length > 0)
                    {
                        if (id > 0)
                        {
                            AccountingModel modLedgerTran = oAccCon.getFisLedgerTran(currcomp, fyr, id, "", 0, "", 0, "", "");
                            if (modLedgerTran.GetSetid > 0)
                            {
                                modLedgerTran.GetSettranno = tranno;
                                modLedgerTran.GetSettrancode = trancode;
                                modLedgerTran.GetSettrandate = trandate;
                                modLedgerTran.GetSetledgerdate = trandate;
                                modLedgerTran.GetSetrefno = refno;
                                modLedgerTran.GetSetaccid = accid;
                                modLedgerTran.GetSetaccdesc = accdesc;
                                modLedgerTran.GetSetcurrency = currency;
                                modLedgerTran.GetSetexrate = exrate;
                                modLedgerTran.GetSetledgerno = ledgerno;
                                modLedgerTran.GetSetdebit = debit;
                                modLedgerTran.GetSetcredit = credit;
                                modLedgerTran.GetSetstatus = status;
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
                                int x = oAccCon.updateFisLedgerTran(modLedgerTran);
                                if (x > 0)
                                {
                                    sStatus = "Y";
                                    sMessage = "Simpan berjaya!";
                                }
                                else
                                {
                                    sStatus = "N";
                                    sMessage = "Simpan tidak berjaya! Error on updating table FisLedgerTran...";
                                }
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Simpan tidak berjaya! Error on finding table FisLedgerTran...";
                            }
                        }
                        else
                        {
                            AccountingModel modLedgerTran = new AccountingModel();
                            modLedgerTran.GetSetcomp = currcomp;
                            modLedgerTran.GetSetfyr = fyr;
                            modLedgerTran.GetSettranno = tranno;
                            modLedgerTran.GetSettrancode = trancode;
                            modLedgerTran.GetSettrandate = trandate;
                            modLedgerTran.GetSetledgerdate = trandate;
                            modLedgerTran.GetSetrefno = refno;
                            modLedgerTran.GetSetaccid = accid;
                            modLedgerTran.GetSetaccdesc = accdesc;
                            modLedgerTran.GetSetcurrency = currency;
                            modLedgerTran.GetSetexrate = exrate;
                            modLedgerTran.GetSetledgerno = ledgerno;
                            modLedgerTran.GetSetdebit = debit;
                            modLedgerTran.GetSetcredit = credit;
                            modLedgerTran.GetSetstatus = status;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            if (y > 0)
                            {
                                sStatus = "Y";
                                sMessage = "Simpan berjaya!";
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Simpan tidak berjaya! Error on inserting table FisLedgerTran...";
                            }
                        }
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Simpan tidak berjaya! Error on updating table updatePostingData...";
                    }
                }
            }
            if (tranno > 0 && trancode.Length > 0 && status.Length > 0)
            {
                AccountingModel modPostingData = oAccCon.getJournalEntryDetails(currcomp, fyr, tranno, trancode, "");
                modPostingData.GetSetstatus = status;
                modPostingData.GetSettranamount = dTotalDebit >= dTotalCredit ? dTotalDebit : dTotalCredit;

                if (modPostingData.GetSetstatus.Equals("NEW"))
                {
                    modPostingData.GetSetcreatedby = sUserId;
                }
                else if (modPostingData.GetSetstatus.Equals("CONFIRMED"))
                {
                    modPostingData.GetSetconfirmedby = sUserId;
                }
                else if (modPostingData.GetSetstatus.Equals("CANCELLED"))
                {
                    modPostingData.GetSetcancelledby = sUserId;
                }
                int y = oAccCon.updateJournalEntryDetails(modPostingData);
            }

        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deletePostingData(string currcomp, string fyr, string postdatadelete)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";
        double dTotalDebit = 0, dTotalCredit = 0;
        int tranno = 0;
        String trancode = "";

        ArrayList lsFisCOAId = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && fyr.Length > 0 && postdatadelete.Length > 0)
        {
            ArrayList lsPostDataToUpdate = oAccCon.tokenString(postdatadelete, ",");
            for (int i = 0; i < lsPostDataToUpdate.Count; i++)
            {
                String postingdata = (String)lsPostDataToUpdate[i];
                ArrayList lsPostingData = oAccCon.tokenString(postingdata, "|");
                if (lsPostingData.Count > 0)
                {
                    tranno = lsPostingData.Count > 0 ? int.Parse((String)lsPostingData[0]) : 0;
                    trancode = lsPostingData.Count > 1 ? (String)lsPostingData[1] : "";
                    Int64 id = lsPostingData.Count > 2 ? Int64.Parse((String)lsPostingData[2]) : 0;

                    if (tranno > 0 && trancode.Length > 0 && id > 0)
                    {
                        AccountingModel modLedgerTran = oAccCon.getFisLedgerTran(currcomp, fyr, id, "", 0, "", 0, "", "");
                        if (modLedgerTran.GetSetid > 0)
                        {
                            int x = oAccCon.deleteFisLedgerTran(currcomp, id);
                            if (x > 0)
                            {
                                sStatus = "Y";
                                sMessage = "Hapus berjaya!";
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Hapus tidak berjaya! Error on deleting table FisLedgerTran...";
                            }
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Hapus tidak berjaya! Error on finding table FisLedgerTran...";
                        }
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Hapus tidak berjaya! Error on finding table updatePostingData...";
                    }
                }
            }
            if (tranno > 0 && trancode.Length > 0)
            {
                dTotalDebit = 0;
                dTotalCredit = 0;
                ArrayList lsTranLedger = oAccCon.getFisLedgerTranList(currcomp, fyr, 0, "", 0, "", tranno, trancode, "");
                for (int z = 0; z < lsTranLedger.Count; z++)
                {
                    AccountingModel modFis = (AccountingModel)lsTranLedger[z];
                    dTotalDebit = dTotalDebit + modFis.GetSetdebit;
                    dTotalCredit = dTotalCredit + modFis.GetSetcredit;
                }
                AccountingModel modPostingData = oAccCon.getJournalEntryDetails(currcomp, fyr, tranno, trancode, "");
                modPostingData.GetSettranamount = (dTotalDebit >= dTotalCredit ? dTotalDebit : dTotalCredit);
                int y = oAccCon.updateJournalEntryDetails(modPostingData);
            }

        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}