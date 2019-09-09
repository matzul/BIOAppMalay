using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockState : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sStockStateNo = "";
    public String sAlertMessage = "";
    public MainModel oModStockState = new MainModel();
    public ArrayList lsStockBeginDetails = new ArrayList();
    public ArrayList lsStockInDetails = new ArrayList();
    public ArrayList lsStockOutDetails = new ArrayList();
    public ArrayList lsStockSOHDetails = new ArrayList();
    public String sOpeningDate = "";
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
        if (Request.QueryString["stockstateno"] != null)
        {
            sStockStateNo = Request.QueryString["stockstateno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
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
        if (Request.Params.Get("hidStockStateNo") != null)
        {
            sStockStateNo = oMainCon.replaceNull(Request.Params.Get("hidStockStateNo"));
        }
        if (Request.Params.Get("begindate") != null)
        {
            sOpeningDate = oMainCon.replaceNull(Request.Params.Get("begindate"));
        }
        if (Request.Params.Get("closedate") != null)
        {
            sClosingDate = oMainCon.replaceNull(Request.Params.Get("closedate"));
        }

        if(sOpeningDate.Trim().Length == 0)
            sOpeningDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        if (sClosingDate.Trim().Length == 0)
            sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                oModStockState = new MainModel();
                oModStockState.GetSetcomp = sCurrComp;
                oModStockState.GetSetstockstateno = oMainCon.getNextRunningNo(sCurrComp, "STOCK_STATEMENT", "ACTIVE");
                oModStockState.GetSetopeningdate = sOpeningDate;
                oModStockState.GetSetopeningtype = "BEGIN";
                oModStockState.GetSetstatus = "IN-PROGRESS";
                oModStockState.GetSetcreatedby = sUserId;
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (sStockStateNo.Length > 0)
                    oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");

                if (oModStockState.GetSetstockstateno.Length > 0)
                {
                    oModStockState.GetSetstockopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOpeningAmount"));
                    oModStockState.GetSetstockinamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockInAmount"));
                    oModStockState.GetSetstockoutamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOutAmount"));
                    oModStockState.GetSetstockclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockClosingAmount"));
                    oModStockState.GetSetclosingtype = "NORMAL_CLOSE";
                    oModStockState.GetSetclosingdate = sClosingDate;
                    oModStockState.GetSetstatus = "CLOSED";
                    oModStockState.GetSetconfirmedby = sUserId;
                }
            }
            else if (sAction.Equals("ENDING"))
            {
                if (sStockStateNo.Length > 0)
                    oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");

                if (oModStockState.GetSetstockstateno.Length > 0)
                {
                    oModStockState.GetSetstockopeningamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOpeningAmount"));
                    oModStockState.GetSetstockinamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockInAmount"));
                    oModStockState.GetSetstockoutamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockOutAmount"));
                    oModStockState.GetSetstockclosingamount = oMainCon.replaceDoubleZero(Request.Params.Get("hidStockClosingAmount"));
                    oModStockState.GetSetclosingtype = "END";
                    oModStockState.GetSetclosingdate = sClosingDate;
                    oModStockState.GetSetstatus = "CLOSED";
                    oModStockState.GetSetconfirmedby = sUserId;
                }
            }
        }
    }

    private void processValues()
    {
        if (oMainCon.compareTwoDateTime(sOpeningDate, sClosingDate) > 0)
        {
            if (sAction.Equals("BEGINING"))
            {
                if (oModStockState.GetSetstockstateno.Length > 0)
                {
                    if (oMainCon.insertStockStateHeader(oModStockState).Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(sCurrComp, "STOCK_STATEMENT", "ACTIVE");
                        sAlertMessage = "SUCCESS|MULA Penyata Stok berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|MULA Penyata Stok tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|MULA Penyata Stok tidak berjaya...";
                }
            }
            else if (sAction.Equals("CLOSING"))
            {
                if (oModStockState.GetSetstockstateno.Length > 0)
                {
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
            else if (sAction.Equals("ENDING"))
            {
                if (oModStockState.GetSetstockstateno.Length > 0)
                {
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

                        sAlertMessage = "SUCCESS|AKHIR Penyata Stok berjaya...";
                    }
                    else
                    {
                        sAlertMessage = "ERROR|AKHIR Penyata Stok tidak berjaya...";
                    }
                }
                else
                {
                    sAlertMessage = "ERROR|AKHIR Penyata Stok tidak berjaya...";
                }
            }
        }
        else
        {
            sAlertMessage = "ERROR|Tarikh Tutup mesti lewat dari Tarikh Buka...";
        }        

        //to refresh get cash flow details
        if(sStockStateNo.Length > 0)
            oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, sStockStateNo, "");
        else
            oModStockState = oMainCon.getStockStateHeaderDetails(sCurrComp, "", "IN-PROGRESS");

        if (oModStockState.GetSetstockstateno.Length > 0 && oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
        {
            lsStockBeginDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "BEGIN");
            lsStockInDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "IN");
            lsStockOutDetails = oMainCon.getItemStockTransactionsList(oModStockState.GetSetcomp, "", "", "", oModStockState.GetSetopeningdate, sClosingDate, "OUT");
        }
        else if (oModStockState.GetSetstockstateno.Length > 0 && oModStockState.GetSetstatus.Equals("CLOSED"))
        {
            lsStockBeginDetails = oMainCon.getStockStateDetailsList(oModStockState.GetSetcomp, oModStockState.GetSetstockstateno, "STOCK_BEGIN");
            lsStockInDetails = oMainCon.getStockStateDetailsList(oModStockState.GetSetcomp, oModStockState.GetSetstockstateno, "STOCK_IN");
            lsStockOutDetails = oMainCon.getStockStateDetailsList(oModStockState.GetSetcomp, oModStockState.GetSetstockstateno, "STOCK_OUT");
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