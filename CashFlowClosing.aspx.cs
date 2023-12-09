using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashFlowClosing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sCashFlowNo = "";
    public String sAlertMessage = "";
    public MainModel oModCashFlow = new MainModel();
    public ArrayList lsPayRcptHeaderDetails = new ArrayList();
    public ArrayList lsPayPaidHeaderDetails = new ArrayList();
    public ArrayList lsCompForClosing = new ArrayList();

    /*
    public ArrayList lsStockBeginDetails = new ArrayList();
    public ArrayList lsStockInDetails = new ArrayList();
    public ArrayList lsStockOutDetails = new ArrayList();
    public ArrayList lsStockSOHDetails = new ArrayList();
    */

    public String sClosingDate = "";

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
        if (Request.QueryString["comp"] != null)
        {
            sCurrComp = Request.QueryString["comp"].ToString();
        }
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["cashflowno"] != null)
        {
            sCashFlowNo = Request.QueryString["cashflowno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sClosingDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + " 23:59:59";

        lsCompForClosing = oMainCon.getCompInfoListForCashClosing(sCurrComp, sClosingDate);

    }

    private void processValues()
    {
        if (sAction.Equals("CLOSING"))
        {
            if (lsCompForClosing.Count > 0)
            {
                for (int x = 0; x < lsCompForClosing.Count; x++)
                {
                    MainModel modCompForClosing = (MainModel)lsCompForClosing[x];
                    sCurrComp = modCompForClosing.GetSetcomp;
                    sCashFlowNo = modCompForClosing.GetSetcashflowno;
                    MainModel oModStockState = new MainModel();

                    //to get details of cashflow info
                    if (sCurrComp.Trim().Length > 0 && sUserId.Trim().Length > 0)
                    {
                        if (sCashFlowNo.Length > 0)
                            oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, sCashFlowNo, "");
                        else
                            oModCashFlow = oMainCon.getCashFlowHeaderDetails(sCurrComp, "", "IN-PROGRESS");

                    }

                    if (oModCashFlow.GetSetcashflowno.Length > 0 && oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                    {
                        lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                        if (lsPayRcptHeaderDetails.Count > 0)
                        {
                            for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
                            {
                                MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];
                                oModCashFlow.GetSetbankpaymentreceiptamount = oModCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                                oModCashFlow.GetSetcashpaymentreceiptamount = oModCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                            }
                        }
                        lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                        if (lsPayPaidHeaderDetails.Count > 0)
                        {
                            for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
                            {
                                MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];
                                oModCashFlow.GetSetbankpaymentpaidamount = oModCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                                oModCashFlow.GetSetcashpaymentpaidamount = oModCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                            }
                        }
                        oModCashFlow.GetSetbankclosingamount = oModCashFlow.GetSetbankopeningamount + oModCashFlow.GetSetbankpaymentreceiptamount - oModCashFlow.GetSetbankpaymentpaidamount;
                        oModCashFlow.GetSetcashclosingamount = oModCashFlow.GetSetcashopeningamount + oModCashFlow.GetSetcashpaymentreceiptamount - oModCashFlow.GetSetcashpaymentpaidamount;
                    }

                    if (oModCashFlow.GetSetcashflowno.Length > 0 && oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                    {
                        oModCashFlow.GetSetclosingtype = "NORMAL_CLOSE";
                        oModCashFlow.GetSetclosingdate = sClosingDate;
                        oModCashFlow.GetSetstatus = "CLOSED";
                        oModCashFlow.GetSetconfirmedby = sUserId;

                        if (oMainCon.updateCashFlowHeader(oModCashFlow).Equals("Y"))
                        {
                            //store details of cashflow
                            lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                            for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
                            {
                                MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];
                                MainModel oModCashFlowDet = new MainModel();
                                oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                                oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                                oModCashFlowDet.GetSetcashflowtype = "PAYMENT_RECEIPT";
                                oModCashFlowDet.GetSetpaymentno = oPayRcptDet.GetSetpaymentno;
                                oModCashFlowDet.GetSetpaymentdate = oPayRcptDet.GetSetpaymentdate;
                                oModCashFlowDet.GetSetpaymentconfirmeddate = oPayRcptDet.GetSetpaymentconfirmeddate;
                                oModCashFlowDet.GetSetpaymenttype = oPayRcptDet.GetSetpaymenttype;
                                oModCashFlowDet.GetSetbpid = oPayRcptDet.GetSetbpid;
                                oModCashFlowDet.GetSetbpdesc = oPayRcptDet.GetSetbpdesc;
                                oModCashFlowDet.GetSetpaydetno = oPayRcptDet.GetSetpaydetno;
                                oModCashFlowDet.GetSetlineno = oPayRcptDet.GetSetlineno;
                                oModCashFlowDet.GetSetpaytype = oPayRcptDet.GetSetpaytype;
                                oModCashFlowDet.GetSetpayrefno = oPayRcptDet.GetSetpayrefno;
                                oModCashFlowDet.GetSetpayremarks = oPayRcptDet.GetSetpayremarks;
                                oModCashFlowDet.GetSetpayamount = oPayRcptDet.GetSetpayamount;
                                String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                            }

                            lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, oModCashFlow.GetSetclosingdate, "CONFIRMED");
                            for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
                            {
                                MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];
                                MainModel oModCashFlowDet = new MainModel();
                                oModCashFlowDet.GetSetcomp = oModCashFlow.GetSetcomp;
                                oModCashFlowDet.GetSetcashflowno = oModCashFlow.GetSetcashflowno;
                                oModCashFlowDet.GetSetcashflowtype = "PAYMENT_PAID";
                                oModCashFlowDet.GetSetpaymentno = oPayPaidDet.GetSetpaymentno;
                                oModCashFlowDet.GetSetpaymentdate = oPayPaidDet.GetSetpaymentdate;
                                oModCashFlowDet.GetSetpaymentconfirmeddate = oPayPaidDet.GetSetpaymentconfirmeddate;
                                oModCashFlowDet.GetSetpaymenttype = oPayPaidDet.GetSetpaymenttype;
                                oModCashFlowDet.GetSetbpid = oPayPaidDet.GetSetbpid;
                                oModCashFlowDet.GetSetbpdesc = oPayPaidDet.GetSetbpdesc;
                                oModCashFlowDet.GetSetpaydetno = oPayPaidDet.GetSetpaydetno;
                                oModCashFlowDet.GetSetlineno = oPayPaidDet.GetSetlineno;
                                oModCashFlowDet.GetSetpaytype = oPayPaidDet.GetSetpaytype;
                                oModCashFlowDet.GetSetpayrefno = oPayPaidDet.GetSetpayrefno;
                                oModCashFlowDet.GetSetpayremarks = oPayPaidDet.GetSetpayremarks;
                                oModCashFlowDet.GetSetpayamount = oPayPaidDet.GetSetpayamount;
                                String result = oMainCon.insertCashFlowDetails(oModCashFlowDet);
                            }

                            //open new cash flow
                            MainModel oModNewCashFlow = new MainModel();
                            oModNewCashFlow.GetSetcomp = sCurrComp;
                            oModNewCashFlow.GetSetcashflowno = oMainCon.getNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                            String nextOpeningDate = oMainCon.getNextSecond(oModCashFlow.GetSetclosingdate, 1);
                            oModNewCashFlow.GetSetopeningdate = nextOpeningDate;
                            oModNewCashFlow.GetSetopeningtype = "NORMAL_OPEN";
                            oModNewCashFlow.GetSetbankopeningamount = oModCashFlow.GetSetbankclosingamount;
                            oModNewCashFlow.GetSetcashopeningamount = oModCashFlow.GetSetcashclosingamount;
                            oModNewCashFlow.GetSetstatus = "IN-PROGRESS";
                            oModNewCashFlow.GetSetcreatedby = sUserId;
                            if (oModNewCashFlow.GetSetcashflowno.Length > 0)
                            {
                                if (oMainCon.insertCashFlowHeader(oModNewCashFlow).Equals("Y"))
                                {
                                    oMainCon.updateNextRunningNo(sCurrComp, "CASH_FLOW", "ACTIVE");
                                    sAlertMessage = "SUCCESS|TUTUP dan BUKA Aliran Kewangan seterusnya berjaya...";
                                }
                                else
                                {
                                    sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                                }
                            }
                            else
                            {
                                sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                            }
                        }
                        else
                        {
                            sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|TUTUP dan BUKA Aliran Kewangan seterusnya tidak berjaya...";
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|TIADA REKOD untuk penutupan Penyata Aliran Kewangan...";
            }
        }
        else
        {
            sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Aliran Kewangan seterusnya tidak berjaya...";
        }
    }
}