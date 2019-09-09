using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ZakatCalculation : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sZakatCalculationNo = "";
    public String sAlertMessage = "";
    public MainModel oModZakatCalculation = new MainModel();
    public ArrayList lsPayRcptHeaderDetails = new ArrayList();
    public ArrayList lsPayPaidHeaderDetails = new ArrayList();
    public ArrayList lsPendRcptHeaderDetails = new ArrayList();
    public ArrayList lsPendPaidHeaderDetails = new ArrayList();
    public ArrayList lsItemZakatAddition = new ArrayList();
    public ArrayList lsItemZakatSubstraction = new ArrayList();
    public MainModel modZakatRate = new MainModel();
    public MainModel modZakatNisab = new MainModel();
    public String sOpeningDate = "";
    public String sClosingDate = "";
    public MainModel oModBankOfAccount = new MainModel();
    public MainModel oModCashOnHand = new MainModel();
    private ArrayList lsStockTransDetails = new ArrayList();

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
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["zakatcalculationno"] != null)
        {
            sZakatCalculationNo = Request.QueryString["zakatcalculationno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        sClosingDate = sOpeningDate;

    }

    private void getValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidZakatCalculationNo") != null)
        {
            sZakatCalculationNo = oMainCon.replaceNull(Request.Params.Get("hidZakatCalculationNo"));
        }
        if (Request.Params.Get("begindate") != null)
        {
            sOpeningDate = oMainCon.replaceNull(Request.Params.Get("begindate"));
        }
        if (Request.Params.Get("closedate") != null)
        {
            sClosingDate = oMainCon.replaceNull(Request.Params.Get("closedate"));
        }

        if (sOpeningDate.Trim().Length == 0)
        {
            sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        if (sClosingDate.Trim().Length == 0)
        {
            sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        DateTime datetimeClosing1 = Convert.ToDateTime(sOpeningDate, oMainCon.ukDtfi);
        String sZakatClosingDate1 = "30-11-" + datetimeClosing1.ToString("yyyy") + " 23:59:59";

        if (oMainCon.compareTwoDateTime(sOpeningDate, sZakatClosingDate1) > 0)
        {
            if (oMainCon.compareTwoDateTime(sZakatClosingDate1, sClosingDate) > 0)
            {
                sClosingDate = sZakatClosingDate1;
            }
        }else
        {
            DateTime datetimeClosing2 = Convert.ToDateTime(sClosingDate, oMainCon.ukDtfi);
            String sZakatClosingDate2 = "30-11-" + datetimeClosing2.ToString("yyyy") + " 23:59:59";

            if (oMainCon.compareTwoDateTime(sZakatClosingDate2, sClosingDate) > 0)
            {
                sClosingDate = sZakatClosingDate2;
            }
        }

        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                oModZakatCalculation = new MainModel();
                oModZakatCalculation.GetSetcomp = sCurrComp;
                oModZakatCalculation.GetSetzakatcalculationno = oMainCon.getNextRunningNo(sCurrComp, "ZAKAT_CALCULATION", "ACTIVE");
                oModZakatCalculation.GetSetopeningdate = sOpeningDate;
                oModZakatCalculation.GetSetopeningtype = "BEGIN";
                oModZakatCalculation.GetSetsharepercentage = 100;
                oModZakatCalculation.GetSetstatus = "IN-PROGRESS";
                oModZakatCalculation.GetSetcreatedby = sUserId;
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (sZakatCalculationNo.Length > 0)
                    oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, sZakatCalculationNo, "");

                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    oModZakatCalculation.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModZakatCalculation.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModZakatCalculation.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModZakatCalculation.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModZakatCalculation.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModZakatCalculation.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModZakatCalculation.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModZakatCalculation.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));
                    oModZakatCalculation.GetSetstockopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOpeningAmount"));
                    oModZakatCalculation.GetSetstockinamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockInAmount"));
                    oModZakatCalculation.GetSetstockoutamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOutAmount"));
                    oModZakatCalculation.GetSetstockclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockClosingAmount"));
                    oModZakatCalculation.GetSetpendingreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidPendingReceiptAmount"));
                    oModZakatCalculation.GetSetpendingpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidPendingPaidAmount"));
                    oModZakatCalculation.GetSetzakatnisabamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidZakatNisabAmount"));
                    oModZakatCalculation.GetSetsharepercentage = oMainCon.replaceDoubleZero(Request.Params.Get("hidSharePercentage"));
                    oModZakatCalculation.GetSetzakatrate = oMainCon.replaceDoubleZero(Request.Params.Get("hidZakatRate"));
                    oModZakatCalculation.GetSetclosingtype = "NORMAL_CLOSE";
                    oModZakatCalculation.GetSetclosingdate = sClosingDate;
                    oModZakatCalculation.GetSetstatus = "CLOSED";
                    oModZakatCalculation.GetSetconfirmedby = sUserId;
                }
            }
            else if (sAction.Equals("ENDING"))
            {
                if (sZakatCalculationNo.Length > 0)
                    oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, sZakatCalculationNo, "");

                if (oModZakatCalculation.GetSetcashflowno.Length > 0)
                {
                    oModZakatCalculation.GetSetbankopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankOpeningAmount"));
                    oModZakatCalculation.GetSetcashopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashOpeningAmount"));
                    oModZakatCalculation.GetSetbankpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentReceiptAmount"));
                    oModZakatCalculation.GetSetcashpaymentreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentReceiptAmount"));
                    oModZakatCalculation.GetSetbankpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankPaymentPaidAmount"));
                    oModZakatCalculation.GetSetcashpaymentpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashPaymentPaidAmount"));
                    oModZakatCalculation.GetSetbankclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidBankClosingAmount"));
                    oModZakatCalculation.GetSetcashclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidCashClosingAmount"));
                    oModZakatCalculation.GetSetstockopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOpeningAmount"));
                    oModZakatCalculation.GetSetstockinamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockInAmount"));
                    oModZakatCalculation.GetSetstockoutamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOutAmount"));
                    oModZakatCalculation.GetSetstockclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockClosingAmount"));
                    oModZakatCalculation.GetSetpendingreceiptamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidPendingReceiptAmount"));
                    oModZakatCalculation.GetSetpendingpaidamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidPendingPaidAmount"));
                    oModZakatCalculation.GetSetzakatnisabamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidZakatNisabAmount"));
                    oModZakatCalculation.GetSetsharepercentage = oMainCon.replaceDoubleZero(Request.Params.Get("hidSharePercentage"));
                    oModZakatCalculation.GetSetzakatrate = oMainCon.replaceDoubleZero(Request.Params.Get("hidZakatRate"));
                    oModZakatCalculation.GetSetclosingtype = "END";
                    oModZakatCalculation.GetSetclosingdate = sClosingDate;
                    oModZakatCalculation.GetSetstatus = "CLOSED";
                    oModZakatCalculation.GetSetconfirmedby = sUserId;
                }
            }            
            else if (sAction.Equals("ADJUSTMENT"))
            {
                if (sZakatCalculationNo.Length > 0)
                    oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, sZakatCalculationNo, "");

                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    oModZakatCalculation.GetSetadjustmenttype = oMainCon.replaceNull(Request.Params.Get("txtAdjustmentType"));
                    oModZakatCalculation.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("txtLineNo"));
                    oModZakatCalculation.GetSetadjustmentno = oMainCon.replaceNull(Request.Params.Get("lblAdjustmentNo"));
                    oModZakatCalculation.GetSettotalamount = oMainCon.replaceDoubleZero(Request.Params.Get("txtTotalAmount"));
                }
            }
            else if (sAction.Equals("SHARE_PERCENTAGE"))
            {
                if (sZakatCalculationNo.Length > 0)
                    oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, sZakatCalculationNo, "");

                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    oModZakatCalculation.GetSetsharepercentage = oMainCon.replaceDoubleZero(Request.Params.Get("txtSharePercentage"));
                }
            }
        }
    }

    private void processValues()
    {
        oMainCon.WriteToLogFile("MainController-ZakatCalculatio.aspx.cs [sClosingDate]: " + sClosingDate);

        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    if (oMainCon.insertZakatCalculationHeader(oModZakatCalculation).Equals("Y"))
                    {
                        ArrayList lsItemZakatParam = oMainCon.getParamList("000", "", "ZAKAT_SUBTRACTION", "");
                        for(int i = 0; i < lsItemZakatParam.Count; i++)
                        {
                            MainModel modParam = (MainModel)lsItemZakatParam[i];
                            MainModel modZakatDet = new MainModel();
                            modZakatDet.GetSetcomp = oModZakatCalculation.GetSetcomp;
                            modZakatDet.GetSetzakatcalculationno = oModZakatCalculation.GetSetzakatcalculationno;
                            modZakatDet.GetSetadjustmentno = modParam.GetSetparamid;
                            modZakatDet.GetSetparamdesc = modParam.GetSetparamdesc;
                            modZakatDet.GetSetlineno = i+1;
                            modZakatDet.GetSetadjustmenttype = modParam.GetSetparamtype;
                            oMainCon.insertZakatCalculationDetails(modZakatDet);

                        }
                        lsItemZakatParam = oMainCon.getParamList("000", "", "ZAKAT_ADDITION", "");
                        for (int i = 0; i < lsItemZakatParam.Count; i++)
                        {
                            MainModel modParam = (MainModel)lsItemZakatParam[i];
                            MainModel modZakatDet = new MainModel();
                            modZakatDet.GetSetcomp = oModZakatCalculation.GetSetcomp;
                            modZakatDet.GetSetzakatcalculationno = oModZakatCalculation.GetSetzakatcalculationno;
                            modZakatDet.GetSetadjustmentno = modParam.GetSetparamid;
                            modZakatDet.GetSetparamdesc = modParam.GetSetparamdesc;
                            modZakatDet.GetSetlineno = i + 1;
                            modZakatDet.GetSetadjustmenttype = modParam.GetSetparamtype;
                            oMainCon.insertZakatCalculationDetails(modZakatDet);

                        }

                        oMainCon.updateNextRunningNo(sCurrComp, "ZAKAT_CALCULATION", "ACTIVE");
                        sAlertMessage = "SUCCESS|MULA Penyata Taksiran Zakat berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|MULA Penyata Taksiran Zakat tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|MULA Penyata Taksiran Zakat tidak berjaya...";
                }
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    if (oMainCon.updateZakatCalculationHeader(oModZakatCalculation).Equals("Y"))
                    {
                        //update zakat calculation details
                        ArrayList lsZakatCalculationDetails = oMainCon.getZakatCalculationDetailsList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetzakatcalculationno, "");
                        for(int x=0; x<lsZakatCalculationDetails.Count; x++)
                        {
                            MainModel modZakatDet = (MainModel)lsZakatCalculationDetails[x];
                            modZakatDet.GetSetconfirmeddate = DateTime.Now.ToString();
                            oMainCon.updateZakatCalculationDetails(modZakatDet);
                        }

                        //open new zakat calculation 
                        MainModel oModNewZakatCalculation = new MainModel();
                        oModNewZakatCalculation.GetSetcomp = sCurrComp;
                        oModNewZakatCalculation.GetSetzakatcalculationno = oMainCon.getNextRunningNo(sCurrComp, "ZAKAT_CALCULATION", "ACTIVE");
                        String nextOpeningDate = oMainCon.getNextSecond(oModZakatCalculation.GetSetclosingdate, 1);
                        oModNewZakatCalculation.GetSetopeningdate = nextOpeningDate;
                        oModNewZakatCalculation.GetSetopeningtype = "NORMAL_OPEN";
                        oModNewZakatCalculation.GetSetbankopeningamount = oModZakatCalculation.GetSetbankclosingamount;
                        oModNewZakatCalculation.GetSetcashopeningamount = oModZakatCalculation.GetSetcashclosingamount;
                        oModNewZakatCalculation.GetSetstockopeningamount = oModZakatCalculation.GetSetstockclosingamount;
                        oModNewZakatCalculation.GetSetsharepercentage = oModZakatCalculation.GetSetsharepercentage;
                        oModNewZakatCalculation.GetSetstatus = "IN-PROGRESS";
                        oModNewZakatCalculation.GetSetcreatedby = sUserId;
                        if (oModNewZakatCalculation.GetSetzakatcalculationno.Length > 0)
                        {
                            if (oMainCon.insertZakatCalculationHeader(oModNewZakatCalculation).Equals("Y"))
                            {
                                ArrayList lsItemZakatParam = oMainCon.getParamList("000", "", "ZAKAT_SUBTRACTION", "");
                                for (int i = 0; i < lsItemZakatParam.Count; i++)
                                {
                                    MainModel modParam = (MainModel)lsItemZakatParam[i];
                                    MainModel modZakatDet = new MainModel();
                                    modZakatDet.GetSetcomp = oModNewZakatCalculation.GetSetcomp;
                                    modZakatDet.GetSetzakatcalculationno = oModNewZakatCalculation.GetSetzakatcalculationno;
                                    modZakatDet.GetSetadjustmentno = modParam.GetSetparamid;
                                    modZakatDet.GetSetparamdesc = modParam.GetSetparamdesc;
                                    modZakatDet.GetSetlineno = i + 1;
                                    modZakatDet.GetSetadjustmenttype = modParam.GetSetparamtype;
                                    oMainCon.insertZakatCalculationDetails(modZakatDet);

                                }
                                lsItemZakatParam = oMainCon.getParamList("000", "", "ZAKAT_ADDITION", "");
                                for (int i = 0; i < lsItemZakatParam.Count; i++)
                                {
                                    MainModel modParam = (MainModel)lsItemZakatParam[i];
                                    MainModel modZakatDet = new MainModel();
                                    modZakatDet.GetSetcomp = oModNewZakatCalculation.GetSetcomp;
                                    modZakatDet.GetSetzakatcalculationno = oModNewZakatCalculation.GetSetzakatcalculationno;
                                    modZakatDet.GetSetadjustmentno = modParam.GetSetparamid;
                                    modZakatDet.GetSetparamdesc = modParam.GetSetparamdesc;
                                    modZakatDet.GetSetlineno = i + 1;
                                    modZakatDet.GetSetadjustmenttype = modParam.GetSetparamtype;
                                    oMainCon.insertZakatCalculationDetails(modZakatDet);

                                }

                                oMainCon.updateNextRunningNo(sCurrComp, "ZAKAT_CALCULATION", "ACTIVE");
                                sAlertMessage = "SUCCESS|TUTUP dan BUKA Penyata Taksiran Zakat seterusnya berjaya...";
                            }
                            else
                            {
                                sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Taksiran Zakat seterusnya tidak berjaya...";
                            }
                        }
                        else
                        {
                            sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Taksiran Zakat seterusnya tidak berjaya...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Taksiran Zakat seterusnya tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Taksiran Zakat seterusnya tidak berjaya...";
                }
            }
            else if (sAction.Equals("ENDING"))
            {
                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                {
                    //store details of zakat calculation
                    /* NOT REQUIRED, ALREADY HANDLE in ASPX PAGE
                    lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetopeningdate, oModZakatCalculation.GetSetclosingdate, "CONFIRMED");
                    for (int i = 0; i < lsPendRcptHeaderDetails.Count; i++)
                    {
                        MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

                        oModZakatCalculation.GetSetbankpaymentreceiptamount = oModZakatCalculation.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                        oModZakatCalculation.GetSetcashpaymentreceiptamount = oModZakatCalculation.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                    }

                    lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetopeningdate, oModZakatCalculation.GetSetclosingdate, "CONFIRMED");
                    for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
                    {
                        MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

                        oModZakatCalculation.GetSetbankpaymentpaidamount = oModZakatCalculation.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                        oModZakatCalculation.GetSetcashpaymentpaidamount = oModZakatCalculation.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                    }

                    lsPendRcptHeaderDetails = oMainCon.getLineItemPendingPaymentReceipt(oModZakatCalculation.GetSetcomp, "", "", "");
                    for (int i = 0; i < lsPendRcptHeaderDetails.Count; i++)
                    {
                        MainModel modPendRcpt = (MainModel)lsPendRcptHeaderDetails[i];
                        oModZakatCalculation.GetSetpendingreceiptamount = oModZakatCalculation.GetSetpendingreceiptamount + (modPendRcpt.GetSettotalamount - modPendRcpt.GetSetpayrcptamount);
                    }

                    lsPendPaidHeaderDetails = oMainCon.getLineItemPendingPaymentPaid(oModZakatCalculation.GetSetcomp, "", "", "");
                    for (int i = 0; i < lsPendPaidHeaderDetails.Count; i++)
                    {
                        MainModel modPendPaid = (MainModel)lsPendPaidHeaderDetails[i];
                        oModZakatCalculation.GetSetpendingpaidamount = oModZakatCalculation.GetSetpendingpaidamount + (modPendPaid.GetSettotalamount - modPendPaid.GetSetpaypaidamount);
                    }
                    */

                    if (oMainCon.updateZakatCalculationHeader(oModZakatCalculation).Equals("Y"))
                    {
                        //update zakat calculation details
                        ArrayList lsZakatCalculationDetails = oMainCon.getZakatCalculationDetailsList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetzakatcalculationno, "");
                        for (int x = 0; x < lsZakatCalculationDetails.Count; x++)
                        {
                            MainModel modZakatDet = (MainModel)lsZakatCalculationDetails[x];
                            modZakatDet.GetSetconfirmeddate = DateTime.Now.ToString();
                            oMainCon.updateZakatCalculationDetails(modZakatDet);
                        }

                        sAlertMessage = "SUCCESS|AKHIR Penyata Taksiran Zakat berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|AKHIR Penyata Taksiran Zakat tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|AKHIR Penyata Taksiran Zakat tidak berjaya...";
                }
            }   
            else if(sAction.Equals("ADJUSTMENT"))
            {
                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0 && oModZakatCalculation.GetSetadjustmenttype.Length > 0 && oModZakatCalculation.GetSetlineno > 0 && oModZakatCalculation.GetSetadjustmentno.Length > 0)
                {
                    if (oMainCon.updateZakatCalculationDetails(oModZakatCalculation).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Kemaskini Perlarasan Zakat berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Kemaskini Perlarasan Zakat tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Kemaskini Perlarasan Zakat tidak berjaya...";
                }
            }
            else if(sAction.Equals("SHARE_PERCENTAGE"))
            {
                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0 && oModZakatCalculation.GetSetsharepercentage > 0)
                {
                    if (oMainCon.updateZakatCalculationHeader(oModZakatCalculation).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Kemaskini Pemilikan Saham Muslim berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Kemaskini Pemilikan Saham Muslim tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|Kemaskini Pemilikan Saham Muslim tidak berjaya...";
                }
            }
        }
        else
        {
            sAlertMessage = "ERROR|Tarikh Tutup mesti lewat dari Tarikh Buka...";
        }        

        //to refresh get cash flow details
        if(sZakatCalculationNo.Length > 0)
            oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, sZakatCalculationNo, "");
        else
            oModZakatCalculation = oMainCon.getZakatCalculationHeaderDetails(sCurrComp, "", "IN-PROGRESS");

        modZakatRate = oMainCon.getParamDetails("000","", "ZAKAT_RATE", "ZAKAT_RATE");
        modZakatNisab = oMainCon.getParamDetails("000", "", "ZAKAT_NISAB", "ZAKAT_NISAB");

        oMainCon.WriteToLogFile("MainController-ZakatCalculatio.aspx.cs [sClosingDate]: " + sClosingDate);

        if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0 && oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS"))
        {

            DateTime datetimeClosing1 = Convert.ToDateTime(oModZakatCalculation.GetSetopeningdate, oMainCon.ukDtfi);
            String sZakatClosingDate1 = "30-11-" + datetimeClosing1.ToString("yyyy") + " 23:59:59";

            if (oMainCon.compareTwoDateTime(oModZakatCalculation.GetSetopeningdate, sZakatClosingDate1) > 0)
            {
                if (oMainCon.compareTwoDateTime(sZakatClosingDate1, sClosingDate) > 0)
                {
                    sClosingDate = sZakatClosingDate1;
                }
            }
            else
            {
                DateTime datetimeClosing2 = Convert.ToDateTime(sClosingDate, oMainCon.ukDtfi);
                String sZakatClosingDate2 = "30-11-" + datetimeClosing2.ToString("yyyy") + " 23:59:59";

                if (oMainCon.compareTwoDateTime(sZakatClosingDate2, sClosingDate) > 0)
                {
                    sClosingDate = sZakatClosingDate2;
                }
            }

            lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetopeningdate, sClosingDate, "CONFIRMED");
            //refresh back
            oModZakatCalculation.GetSetbankpaymentreceiptamount = 0;
            oModZakatCalculation.GetSetcashpaymentreceiptamount = 0;
            for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
            {
                MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

                oModZakatCalculation.GetSetbankpaymentreceiptamount = oModZakatCalculation.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                oModZakatCalculation.GetSetcashpaymentreceiptamount = oModZakatCalculation.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
            }

            lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModZakatCalculation.GetSetcomp, oModZakatCalculation.GetSetopeningdate, sClosingDate, "CONFIRMED");
            //refresh back
            oModZakatCalculation.GetSetbankpaymentpaidamount = 0;
            oModZakatCalculation.GetSetcashpaymentpaidamount = 0;
            for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
            {
                MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

                oModZakatCalculation.GetSetbankpaymentpaidamount = oModZakatCalculation.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                oModZakatCalculation.GetSetcashpaymentpaidamount = oModZakatCalculation.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
            }

            lsPendRcptHeaderDetails = oMainCon.getLineItemPendingPaymentReceipt(oModZakatCalculation.GetSetcomp, "", "", "");
            //refresh back
            oModZakatCalculation.GetSetpendingreceiptamount = 0;
            for (int i=0; i < lsPendRcptHeaderDetails.Count; i++)
            {
                MainModel modPendRcpt = (MainModel)lsPendRcptHeaderDetails[i];
                DateTime datetime = Convert.ToDateTime(modPendRcpt.GetSetconfirmeddate);
                String confirmedDate = datetime.ToString("dd-MM-yyyy HH:mm:ss");
                if (oMainCon.compareTwoDateTime(confirmedDate, sClosingDate) > 0)
                {
                    oModZakatCalculation.GetSetpendingreceiptamount = oModZakatCalculation.GetSetpendingreceiptamount + (modPendRcpt.GetSettotalamount - modPendRcpt.GetSetpayrcptamount);
                }
            }

            lsPendPaidHeaderDetails = oMainCon.getLineItemPendingPaymentPaid(oModZakatCalculation.GetSetcomp, "", "", "");
            //refresh back
            oModZakatCalculation.GetSetpendingpaidamount = 0;
            for (int i = 0; i < lsPendPaidHeaderDetails.Count; i++)
            {
                MainModel modPendPaid = (MainModel)lsPendPaidHeaderDetails[i];
                DateTime datetime = Convert.ToDateTime(modPendPaid.GetSetconfirmeddate);
                String confirmedDate = datetime.ToString("dd-MM-yyyy HH:mm:ss");
                if (oMainCon.compareTwoDateTime(confirmedDate, sClosingDate) > 0)
                {
                    oModZakatCalculation.GetSetpendingpaidamount = oModZakatCalculation.GetSetpendingpaidamount + (modPendPaid.GetSettotalamount - modPendPaid.GetSetpaypaidamount);
                }
            }

            lsStockTransDetails = oMainCon.getItemStockTransactionsList(oModZakatCalculation.GetSetcomp, "", "", "", oModZakatCalculation.GetSetopeningdate, sClosingDate, "");
            //refresh back
            oModZakatCalculation.GetSetstockinamount = 0;
            oModZakatCalculation.GetSetstockoutamount = 0;
            if (lsStockTransDetails.Count > 0)
            {
                for (int i = 0; i < lsStockTransDetails.Count; i++)
                {
                    MainModel oStockTransDet = (MainModel)lsStockTransDetails[i];
                    if (oStockTransDet.GetSettransflow.Equals("IN"))
                    {
                        oModZakatCalculation.GetSetstockinamount = oModZakatCalculation.GetSetstockinamount + Math.Round(oStockTransDet.GetSettransqty * oStockTransDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                    }
                    else if (oStockTransDet.GetSettransflow.Equals("OUT"))
                    {
                        oModZakatCalculation.GetSetstockoutamount = oModZakatCalculation.GetSetstockoutamount + Math.Round(oStockTransDet.GetSettransqty * oStockTransDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
            
            oModZakatCalculation.GetSetbankclosingamount = oModZakatCalculation.GetSetbankopeningamount + oModZakatCalculation.GetSetbankpaymentreceiptamount - oModZakatCalculation.GetSetbankpaymentpaidamount;
            oModZakatCalculation.GetSetcashclosingamount = oModZakatCalculation.GetSetcashopeningamount + oModZakatCalculation.GetSetcashpaymentreceiptamount - oModZakatCalculation.GetSetcashpaymentpaidamount;
            oModZakatCalculation.GetSetstockclosingamount = oModZakatCalculation.GetSetstockopeningamount + oModZakatCalculation.GetSetstockinamount - oModZakatCalculation.GetSetstockoutamount;
            lsItemZakatAddition = oMainCon.getZakatCalculationDetailsList(sCurrComp, oModZakatCalculation.GetSetzakatcalculationno, "ZAKAT_ADDITION");
            lsItemZakatSubstraction = oMainCon.getZakatCalculationDetailsList(sCurrComp, oModZakatCalculation.GetSetzakatcalculationno, "ZAKAT_SUBTRACTION");
            oModZakatCalculation.GetSetzakatnisabamount = double.Parse(modZakatNisab.GetSetparamdesc);
            oModZakatCalculation.GetSetzakatrate = double.Parse(modZakatRate.GetSetparamdesc);
            //to update latest zakat header
            oMainCon.updateZakatCalculationHeader(oModZakatCalculation);
        }
        else if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0 && oModZakatCalculation.GetSetstatus.Equals("CLOSED"))
        {
            lsItemZakatAddition = oMainCon.getZakatCalculationDetailsList(sCurrComp, oModZakatCalculation.GetSetzakatcalculationno, "ZAKAT_ADDITION");
            lsItemZakatSubstraction = oMainCon.getZakatCalculationDetailsList(sCurrComp, oModZakatCalculation.GetSetzakatcalculationno, "ZAKAT_SUBTRACTION");
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

}