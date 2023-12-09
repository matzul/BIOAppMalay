using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_PostingPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();
    public MainController oMainCon = new MainController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrDateFrom = "";
    public String sCurrDateTo = "";
    public String sCurrType = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";

    public ArrayList lsPostData = new ArrayList();

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
        if(sCurrFyr.Length > 0)
        {
            //sCurrDateFrom = FirstDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
            //sCurrDateTo = LastDayOfMonth().ToString("dd-MM") + "-" + sCurrFyr;
            sCurrDateFrom = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
            sCurrDateTo = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
        }
        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
    private DateTime LastDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        
        DateTime datetime1 = Convert.ToDateTime(sCurrDateFrom + " 00:00:00", oAccCon.ukDtfi);
        DateTime datetime2 = Convert.ToDateTime(sCurrDateTo + " 23:59:59", oAccCon.ukDtfi);
        lsPostData = oAccCon.getPostingDataList(sCurrComp, sCurrFyr, datetime1.ToString(), datetime2.ToString(), 0, sCurrType, "", 0, "");
        for (int x = 0; x < lsPostData.Count; x++)
        {
            AccountingModel modItem = (AccountingModel)lsPostData[x];
            if (modItem.GetSettranno.Equals(0))
            {
                //modItem.GetSetcomp = currcomp;
                //modItem.GetSetfyr = fyr;
                modItem.GetSettranno = oAccCon.getNextRunningNo(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSettrancode, "ACTIVE");
                //modItem.GetSettrancode = trancode;
                //modItem.GetSettrandate = trandate;
                modItem.GetSettrandesc = "POSTING DATA TRANSAKSI";
                modItem.GetSetcurrency = "MYR";
                modItem.GetSetexrate = 1;
                //modItem.GetSetdebit = debit;
                //modItem.GetSetcredit = credit;
                modItem.GetSetstatus = "NEW";
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
                int i = oAccCon.insertPostingData(modItem);
                if (i > 0)
                {
                    oAccCon.updateNextRunningNo(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSettrancode, "ACTIVE");

                    //begin auto insert ledger if listing below
                    if (modItem.GetSetrefno.StartsWith("GR"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "06", 0, "A", "INVENTORY", modItem.GetSetbpid, "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "B0620000", "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }
                    }
                    else if (modItem.GetSetrefno.StartsWith("DO"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "B0620000", "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "06", 0, "A", "INVENTORY", modItem.GetSetbpid, "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }
                    }
                    else if (modItem.GetSetrefno.StartsWith("INV") && modItem.GetSettrancode.Equals("SALES_INVOICE"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        if (modAccidDet.GetSetid == 0)
                        {
                            String COAARBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAARBP"]);
                            modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAARBP, "", "", 0, "", "", "", "", "");
                        }
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        String COABusInc = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABusInc"]);
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COABusInc, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    else if (modItem.GetSetrefno.StartsWith("PV") && modItem.GetSettrancode.Equals("PURCHASE_INVOICE"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        String COABusExp = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABusExp"]);
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COABusExp, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        if (modAccidDet2.GetSetid == 0)
                        {
                            String COAAPBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAPBP"]);
                            modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAPBP, "", "", 0, "", "", "", "", "");
                        }
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }                 
                    else if (modItem.GetSetrefno.StartsWith("PV") && modItem.GetSettrantype.Equals("BANK_DEPOSIT"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        String COAAdjExp = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAdjExp"]);
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAdjExp, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    #region comment
                    /*
                    else if (modItem.GetSetrefno.StartsWith("PPD") && modItem.GetSettrantype.Equals("BANK_DEPOSIT"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        String COACash = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COACash"]);
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COACash, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    */
                    #endregion comment
                    else if (modItem.GetSetrefno.StartsWith("INV") && modItem.GetSettrantype.Equals("BANK_DEPOSIT"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        String COAAdjInc = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAdjInc"]);
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAdjInc, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    #region comment
                    /*
                    else if (modItem.GetSetrefno.StartsWith("PRC") && modItem.GetSettrantype.Equals("BANK_DEPOSIT"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        String COABank = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABank"]);
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COABank, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    */
                    #endregion
                    else if (modItem.GetSetrefno.StartsWith("PV") && modItem.GetSettrantype.Equals("CASH_WITHDRAWAL"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        String COAAdjExp = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAdjExp"]);
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAdjExp, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    #region comment
                    /*                    
                    else if (modItem.GetSetrefno.StartsWith("PPD") && modItem.GetSettrantype.Equals("CASH_WITHDRAWAL"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        String COABank = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABank"]);
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COABank, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    */
                    #endregion
                    else if (modItem.GetSetrefno.StartsWith("INV") && modItem.GetSettrantype.Equals("CASH_WITHDRAWAL"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        
                        String COAAdjInc = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAdjInc"]);
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAdjInc, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    #region comment
                    /*                    
                    else if (modItem.GetSetrefno.StartsWith("PRC") && modItem.GetSettrantype.Equals("CASH_WITHDRAWAL"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        String COACash = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COACash"]);
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COACash, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }                    
                    */
                    #endregion
                    
                    else if (modItem.GetSetrefno.StartsWith("INV") && modItem.GetSettrancode.Equals("RECEIPT_VOUCHER") && (!modItem.GetSettrantype.Equals("BANK_DEPOSIT") || !modItem.GetSettrantype.Equals("CASH_WITHDRAWAL")))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        if (modAccidDet.GetSetid == 0)
                        {
                            String COAARBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAARBP"]);
                            modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAARBP, "", "", 0, "", "", "", "", "");
                        }
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        MainModel modInvDetails = oMainCon.getInvoiceDetailsDetails(modItem.GetSetcomp, modItem.GetSetrefno, modItem.GetSetlineno, "", 0, "", "");
                        MainModel modParamDetails = oMainCon.getParamDetails("000", modInvDetails.GetSetitemno, "", "");

                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, modParamDetails.GetSetparamcode);
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    else if (modItem.GetSetrefno.StartsWith("PV") && modItem.GetSettrancode.Equals("PAYMENT_VOUCHER") && (!modItem.GetSettrantype.Equals("BANK_DEPOSIT") || !modItem.GetSettrantype.Equals("CASH_WITHDRAWAL")))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        MainModel modExpDetails = oMainCon.getExpensesDetailsDetails(modItem.GetSetcomp, modItem.GetSetrefno, modItem.GetSetlineno, "", 0, "", "");
                        MainModel modParamDetails = oMainCon.getParamDetails("000", modExpDetails.GetSetitemno, "", "");

                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, modParamDetails.GetSetparamcode);
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        if (modAccidDet2.GetSetid == 0)
                        {
                            String COAAPBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAPBP"]);
                            modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAPBP, "", "", 0, "", "", "", "", "");
                        }

                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }

                    else if (modItem.GetSetrefno.StartsWith("PPD"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;
                        
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "L", "SUPPLIER", modItem.GetSetbpid, "", "");
                        if (modAccidDet.GetSetid == 0)
                        {
                            String COAAPBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAAPBP"]);
                            modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAAPBP, "", "", 0, "", "", "", "", "");
                        }

                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        String COAPayType = "NOT_FOUND";
                        if (modItem.GetSettrancode.EndsWith("CASH"))
                        {
                                COAPayType = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COACash"]);
                        }
                        else
                        {
                            COAPayType = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABank"]);
                        }
                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAPayType, "", "", 0, "", "", "", "", "");
                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    else if (modItem.GetSetrefno.StartsWith("PRC"))
                    {
                        //Debit Side
                        AccountingModel modLedgerTran = new AccountingModel();
                        modLedgerTran.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran.GetSettranno = modItem.GetSettranno;
                        modLedgerTran.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran.GetSetrefno = modItem.GetSetrefno;

                        String COAPayType = "NOT_FOUND";
                        if (modItem.GetSettrancode.EndsWith("CASH"))
                        {
                            COAPayType = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COACash"]);
                        }
                        else
                        {
                            COAPayType = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COABank"]);
                        }
                        AccountingModel modAccidDet = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAPayType, "", "", 0, "", "", "", "", "");
                        modLedgerTran.GetSetaccid = modAccidDet.GetSetaccid;
                        modLedgerTran.GetSetaccdesc = modAccidDet.GetSetaccdesc;
                        modLedgerTran.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran.GetSetledgerno = 1;
                        modLedgerTran.GetSetdebit = modItem.GetSettranamount;
                        modLedgerTran.GetSetcredit = 0;
                        modLedgerTran.GetSetstatus = modItem.GetSetstatus;
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
                        modLedgerTran2.GetSetcomp = modItem.GetSetcomp;
                        modLedgerTran2.GetSetfyr = modItem.GetSetfyr;
                        modLedgerTran2.GetSettranno = modItem.GetSettranno;
                        modLedgerTran2.GetSettrancode = modItem.GetSettrancode;
                        modLedgerTran2.GetSettrandate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetledgerdate = modItem.GetSettrandate;
                        modLedgerTran2.GetSetrefno = modItem.GetSetrefno;

                        AccountingModel modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, "", "", "", 0, "A", "CUSTOMER", modItem.GetSetbpid, "", "");
                        if (modAccidDet2.GetSetid == 0)
                        {
                            String COAARBP = oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAARBP"]);
                            modAccidDet2 = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetfyr, COAARBP, "", "", 0, "", "", "", "", "");
                        }

                        modLedgerTran2.GetSetaccid = modAccidDet2.GetSetaccid;
                        modLedgerTran2.GetSetaccdesc = modAccidDet2.GetSetaccdesc;
                        modLedgerTran2.GetSetcurrency = modItem.GetSetcurrency;
                        modLedgerTran2.GetSetexrate = modItem.GetSetexrate;
                        modLedgerTran2.GetSetledgerno = 2;
                        modLedgerTran2.GetSetdebit = 0;
                        modLedgerTran2.GetSetcredit = modItem.GetSettranamount;
                        modLedgerTran2.GetSetstatus = modItem.GetSetstatus;
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
                            int y = oAccCon.insertFisLedgerTran(modLedgerTran);
                            int z = oAccCon.insertFisLedgerTran(modLedgerTran2);
                        }

                    }
                    //end auto insert ledger if listing below
                }
            }
        }
        //refresh back data listing
        lsPostData = oAccCon.getPostingDataList(sCurrComp, sCurrFyr, datetime1.ToString(), datetime2.ToString(), 0, sCurrType, "", 0, "");

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
        if (Request.Params.Get("txtFindFromDate") != null)
        {
            sCurrDateFrom = oAccCon.replaceNull(Request.Params.Get("txtFindFromDate"));
        }
        if (Request.Params.Get("txtFindToDate") != null)
        {
            sCurrDateTo = oAccCon.replaceNull(Request.Params.Get("txtFindToDate"));
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
    public static String getPostingDataList(string currcomp, string fyr, string datefrom, string dateto, string trancode)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsPostData = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && fyr.Length > 0 && datefrom.Length > 0 && dateto.Length > 0)
        {
            lsPostData = oAccCon.getPostingDataList(currcomp, fyr, datefrom, dateto, 0, trancode, "", 0, "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, postingdate = lsPostData };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
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
    public static String updatePostingData(string currcomp, string fyr, string postdataupdate)
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

        if (currcomp.Length > 0 && fyr.Length > 0 && postdataupdate.Length > 0)
        {
            ArrayList lsPostDataToUpdate = oAccCon.tokenString(postdataupdate, ",");
            for (int i = 0; i < lsPostDataToUpdate.Count; i++)
            {
                String postingdata = (String)lsPostDataToUpdate[i];
                ArrayList lsPostingData = oAccCon.tokenString(postingdata, "|");
                if (lsPostingData.Count > 0)
                {
                    int tranno = lsPostingData.Count > 0 ? int.Parse((String)lsPostingData[0]) : 0;
                    String trancode = lsPostingData.Count > 1 ? (String)lsPostingData[1] : "";
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
                    String status = lsPostingData.Count > 12 ? (String)lsPostingData[12] : "NEW";

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
                                    sMessage = "Simpan tidak berjaya! Error on updating table FisLedgerTran..." + currcomp + "|" + fyr + "|" + id;
                                    break;
                                }
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Simpan tidak berjaya! Error on finding table FisLedgerTran..." + currcomp + "|" + fyr + "|" + id;
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
                        if (sStatus.Equals("Y") && status.Equals("CONFIRMED"))
                        {
                            AccountingModel modPostingData = oAccCon.getPostingData(currcomp, fyr, tranno, trancode, "");
                            modPostingData.GetSetstatus = status;
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
                            int y = oAccCon.updatePostingData(modPostingData);
                        }
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Simpan tidak berjaya! Error on updating table updatePostingData...";
                    }
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}