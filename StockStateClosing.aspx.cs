using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockStateClosing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sStockStateNo = "";
    public String sAlertMessage = "";
    public MainModel oModStockState = new MainModel();
    public ArrayList lsCompForClosing = new ArrayList();
    public ArrayList lsStockBeginDetails = new ArrayList();
    public ArrayList lsStockInDetails = new ArrayList();
    public ArrayList lsStockOutDetails = new ArrayList();
    public ArrayList lsStockSOHDetails = new ArrayList();
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
        if (Request.QueryString["stockstateno"] != null)
        {
            sStockStateNo = Request.QueryString["stockstateno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sClosingDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + " 23:59:59";

        lsCompForClosing = oMainCon.getCompInfoListForClosing(sCurrComp, sClosingDate);

        //to refresh get cash flow details
        /*
        if (sCurrComp.Trim().Length > 0 && sUserId.Trim().Length > 0 && sAction.Trim().Length > 0)
        {
            if (sStockStateNo.Length > 0)
                oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");
            else
                oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, "", "IN-PROGRESS");
        }

        if (oModStockState.GetSetstockstateno.Length > 0 && oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
        {
            lsStockBeginDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "BEGIN");
            if (lsStockBeginDetails.Count > 0)
            {
                for (int i = 0; i < lsStockBeginDetails.Count; i++)
                {
                    MainModel oStockInDet = (MainModel)lsStockBeginDetails[i];
                    oModStockState.GetSetstockopeningamount = oModStockState.GetSetstockopeningamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                }
            }
            lsStockInDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "IN");
            if (lsStockInDetails.Count > 0)
            {
                for (int i = 0; i < lsStockInDetails.Count; i++)
                {
                    MainModel oStockInDet = (MainModel)lsStockInDetails[i];
                    oModStockState.GetSetstockinamount = oModStockState.GetSetstockinamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                }
            }
            lsStockOutDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "OUT");
            if (lsStockOutDetails.Count > 0)
            {
                for (int i = 0; i < lsStockOutDetails.Count; i++)
                {
                    MainModel oStockOutDet = (MainModel)lsStockOutDetails[i];
                    oModStockState.GetSetstockoutamount = oModStockState.GetSetstockoutamount + Math.Round(oStockOutDet.GetSettransqty * oStockOutDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                }
            }
            oModStockState.GetSetstockclosingamount = oModStockState.GetSetstockopeningamount + oModStockState.GetSetstockinamount - oModStockState.GetSetstockoutamount;

        }
        */
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
                    sStockStateNo = modCompForClosing.GetSetstockstateno;
                    MainModel oModStockState = new MainModel();

                    //to get details of stockstate info
                    if (sCurrComp.Trim().Length > 0 && sUserId.Trim().Length > 0)
                    {
                        if (sStockStateNo.Length > 0)
                            oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");
                        else
                            oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, "", "IN-PROGRESS");
                    }

                    if (oModStockState.GetSetstockstateno.Length > 0 && oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                    {
                        lsStockBeginDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "BEGIN");
                        if (lsStockBeginDetails.Count > 0)
                        {
                            for (int i = 0; i < lsStockBeginDetails.Count; i++)
                            {
                                MainModel oStockInDet = (MainModel)lsStockBeginDetails[i];
                                oModStockState.GetSetstockopeningamount = oModStockState.GetSetstockopeningamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                        lsStockInDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "IN");
                        if (lsStockInDetails.Count > 0)
                        {
                            for (int i = 0; i < lsStockInDetails.Count; i++)
                            {
                                MainModel oStockInDet = (MainModel)lsStockInDetails[i];
                                oModStockState.GetSetstockinamount = oModStockState.GetSetstockinamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                        lsStockOutDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "OUT");
                        if (lsStockOutDetails.Count > 0)
                        {
                            for (int i = 0; i < lsStockOutDetails.Count; i++)
                            {
                                MainModel oStockOutDet = (MainModel)lsStockOutDetails[i];
                                oModStockState.GetSetstockoutamount = oModStockState.GetSetstockoutamount + Math.Round(oStockOutDet.GetSettransqty * oStockOutDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                        oModStockState.GetSetstockclosingamount = oModStockState.GetSetstockopeningamount + oModStockState.GetSetstockinamount - oModStockState.GetSetstockoutamount;

                    }

                    if (oModStockState.GetSetstockstateno.Length > 0 && oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                    {
                        oModStockState.GetSetclosingtype = "NORMAL_CLOSE";
                        oModStockState.GetSetclosingdate = sClosingDate;
                        oModStockState.GetSetstatus = "CLOSED";
                        oModStockState.GetSetconfirmedby = sUserId;

                        if (oMainCon.updateStockStateHeader(oModStockState).Equals("Y"))
                        {
                            //store details of Stock Begin
                            lsStockBeginDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "BEGIN");
                            for (int i = 0; i < lsStockBeginDetails.Count; i++)
                            {
                                MainModel oStockTransDet = (MainModel)lsStockBeginDetails[i];
                                MainModel oModStockStateDet = new MainModel();
                                oModStockStateDet.GetSetcomp = oModStockState.GetSetcomp;
                                oModStockStateDet.GetSetstockstateno = oModStockState.GetSetstockstateno;
                                oModStockStateDet.GetSetstockstatetype = "STOCK_BEGIN";
                                oModStockStateDet.GetSettransno = oStockTransDet.GetSettransno;
                                oModStockStateDet.GetSettrans_lineno = oStockTransDet.GetSettrans_lineno;
                                oModStockStateDet.GetSettransdate = oStockTransDet.GetSettransdate;
                                oModStockStateDet.GetSettranstype = oStockTransDet.GetSettranstype;
                                oModStockStateDet.GetSetitemno = oStockTransDet.GetSetitemno;
                                oModStockStateDet.GetSetitemdesc = oStockTransDet.GetSetitemdesc;
                                oModStockStateDet.GetSetlocation = oStockTransDet.GetSetlocation;
                                oModStockStateDet.GetSetdatesoh = oStockTransDet.GetSetdatesoh;
                                oModStockStateDet.GetSetorderno = oStockTransDet.GetSetorderno;
                                oModStockStateDet.GetSetorder_lineno = oStockTransDet.GetSetorder_lineno;
                                oModStockStateDet.GetSettransprice = oStockTransDet.GetSettransprice;
                                oModStockStateDet.GetSettransqty = oStockTransDet.GetSettransqty;
                                String result = oMainCon.insertStockStateDetails(oModStockStateDet);
                            }

                            //store details of Stock In
                            lsStockInDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "IN");
                            for (int i = 0; i < lsStockInDetails.Count; i++)
                            {
                                MainModel oStockTransDet = (MainModel)lsStockInDetails[i];
                                MainModel oModStockStateDet = new MainModel();
                                oModStockStateDet.GetSetcomp = oModStockState.GetSetcomp;
                                oModStockStateDet.GetSetstockstateno = oModStockState.GetSetstockstateno;
                                oModStockStateDet.GetSetstockstatetype = "STOCK_IN";
                                oModStockStateDet.GetSettransno = oStockTransDet.GetSettransno;
                                oModStockStateDet.GetSettrans_lineno = oStockTransDet.GetSettrans_lineno;
                                oModStockStateDet.GetSettransdate = oStockTransDet.GetSettransdate;
                                oModStockStateDet.GetSettranstype = oStockTransDet.GetSettranstype;
                                oModStockStateDet.GetSetitemno = oStockTransDet.GetSetitemno;
                                oModStockStateDet.GetSetitemdesc = oStockTransDet.GetSetitemdesc;
                                oModStockStateDet.GetSetlocation = oStockTransDet.GetSetlocation;
                                oModStockStateDet.GetSetdatesoh = oStockTransDet.GetSetdatesoh;
                                oModStockStateDet.GetSetorderno = oStockTransDet.GetSetorderno;
                                oModStockStateDet.GetSetorder_lineno = oStockTransDet.GetSetorder_lineno;
                                oModStockStateDet.GetSettransprice = oStockTransDet.GetSettransprice;
                                oModStockStateDet.GetSettransqty = oStockTransDet.GetSettransqty;
                                String result = oMainCon.insertStockStateDetails(oModStockStateDet);
                            }

                            //store details of Stock Out
                            lsStockOutDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "OUT");
                            for (int i = 0; i < lsStockOutDetails.Count; i++)
                            {
                                MainModel oStockTransDet = (MainModel)lsStockOutDetails[i];
                                MainModel oModStockStateDet = new MainModel();
                                oModStockStateDet.GetSetcomp = oModStockState.GetSetcomp;
                                oModStockStateDet.GetSetstockstateno = oModStockState.GetSetstockstateno;
                                oModStockStateDet.GetSetstockstatetype = "STOCK_OUT";
                                oModStockStateDet.GetSettransno = oStockTransDet.GetSettransno;
                                oModStockStateDet.GetSettrans_lineno = oStockTransDet.GetSettrans_lineno;
                                oModStockStateDet.GetSettransdate = oStockTransDet.GetSettransdate;
                                oModStockStateDet.GetSettranstype = oStockTransDet.GetSettranstype;
                                oModStockStateDet.GetSetitemno = oStockTransDet.GetSetitemno;
                                oModStockStateDet.GetSetitemdesc = oStockTransDet.GetSetitemdesc;
                                oModStockStateDet.GetSetlocation = oStockTransDet.GetSetlocation;
                                oModStockStateDet.GetSetdatesoh = oStockTransDet.GetSetdatesoh;
                                oModStockStateDet.GetSetorderno = oStockTransDet.GetSetorderno;
                                oModStockStateDet.GetSetorder_lineno = oStockTransDet.GetSetorder_lineno;
                                oModStockStateDet.GetSettransprice = oStockTransDet.GetSettransprice;
                                oModStockStateDet.GetSettransqty = oStockTransDet.GetSettransqty;
                                String result = oMainCon.insertStockStateDetails(oModStockStateDet);
                            }

                            //store details of Stock Close
                            lsStockSOHDetails = oMainCon.getItemStockList(sCurrComp, "", "", "", false);
                            for (int i = 0; i < lsStockSOHDetails.Count; i++)
                            {
                                MainModel oStockSOHDet = (MainModel)lsStockSOHDetails[i];
                                MainModel oModStockStateDet = new MainModel();
                                oModStockStateDet.GetSetcomp = oModStockState.GetSetcomp;
                                oModStockStateDet.GetSetstockstateno = oModStockState.GetSetstockstateno;
                                oModStockStateDet.GetSetitemno = oStockSOHDet.GetSetitemno;
                                oModStockStateDet.GetSetitemdesc = oStockSOHDet.GetSetitemdesc;
                                oModStockStateDet.GetSetlocation = oStockSOHDet.GetSetlocation;
                                oModStockStateDet.GetSetdatesoh = oStockSOHDet.GetSetdatesoh;
                                oModStockStateDet.GetSetqtysoh = oStockSOHDet.GetSetqtysoh;
                                oModStockStateDet.GetSetcostsoh = oStockSOHDet.GetSetcostsoh;
                                String result = oMainCon.insertStockStateSOH(oModStockStateDet);
                            }

                            //open new Stock Statement
                            MainModel oModNewStockState = new MainModel();
                            oModNewStockState.GetSetcomp = sCurrComp;
                            oModNewStockState.GetSetstockstateno = oMainCon.getNextRunningNo(sCurrComp, "STOCK_STATEMENT", "ACTIVE");
                            String nextOpeningDate = oMainCon.getNextSecond(oModStockState.GetSetclosingdate, 1);
                            oModNewStockState.GetSetopeningdate = nextOpeningDate;
                            oModNewStockState.GetSetopeningtype = "NORMAL_OPEN";
                            oModNewStockState.GetSetstockopeningamount = oModStockState.GetSetstockclosingamount;
                            oModNewStockState.GetSetstatus = "IN-PROGRESS";
                            oModNewStockState.GetSetcreatedby = sUserId;
                            if (oModNewStockState.GetSetstockstateno.Length > 0)
                            {
                                if (oMainCon.insertStockStateHeader(oModNewStockState).Equals("Y"))
                                {
                                    oMainCon.updateNextRunningNo(sCurrComp, "STOCK_STATEMENT", "ACTIVE");
                                    sAlertMessage = "SUCCESS|TUTUP dan BUKA Penyata Stok seterusnya berjaya...";
                                }
                                else
                                {
                                    sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Stok seterusnya tidak berjaya...";
                                }
                            }
                            else
                            {
                                sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Stok seterusnya tidak berjaya...";
                            }
                        }
                        else
                        {
                            sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Stok seterusnya tidak berjaya...";
                        }
                    }
                    else
                    {
                        sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Stok seterusnya tidak berjaya...";
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|TIADA REKOD untuk penutupan Penyata Stok...";
            }
        }
        else
        {
            sAlertMessage = "ERROR|TUTUP dan BUKA Penyata Stok seterusnya tidak berjaya...";
        }
    }
}