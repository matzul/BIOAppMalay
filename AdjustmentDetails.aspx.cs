using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdjustmentDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sAdjustmentNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModAdjustment = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsAdjustmentLineItem = new ArrayList();
    public ArrayList lsStockLocation = new ArrayList();

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
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["adjustmentno"] != null)
        {
            sAdjustmentNo = Request.QueryString["adjustmentno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        lsItem = oMainCon.getItemList(sCurrComp, "", "", "INVENTORY");
        lsStockLocation = oMainCon.getParamList(sCurrComp, "", "STOCK_LOCATION", "");

        if (sAction.Equals("ADD"))
        {
            sAdjustmentNo = "";
            oModAdjustment = new MainModel();
            oModAdjustment.GetSetadjustmentdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModAdjustment.GetSetstatus = "NEW";
        }
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
        if (Request.Params.Get("hidAdjustmentNo") != null)
        {
            sAdjustmentNo = oMainCon.replaceNull(Request.Params.Get("hidAdjustmentNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        lsItem = oMainCon.getItemList(sCurrComp, "", "", "INVENTORY");
        lsStockLocation = oMainCon.getParamList(sCurrComp, "", "STOCK_LOCATION", "");

        //for reset
        if (sAction.Equals("ADD"))
        {
            sAdjustmentNo = "";
            oModAdjustment = new MainModel();
            oModAdjustment.GetSetadjustmentdate = DateTime.Now.ToString("dd-MM-yyyy");
            oModAdjustment.GetSetstatus = "NEW";
            lsAdjustmentLineItem = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModAdjustment = new MainModel();
            oModAdjustment.GetSetcomp = sCurrComp;
            oModAdjustment.GetSetadjustmentdate = oMainCon.replaceNull(Request.Params.Get("adjustmentdate"));
            oModAdjustment.GetSetadjustmenttype = oMainCon.replaceNull(Request.Params.Get("adjustmenttype"));
            sAdjustmentNo = oMainCon.getNextRunningNo(sCurrComp, "ADJUSTMENT_ORDER", "ACTIVE");
            oModAdjustment.GetSetadjustmentno = sAdjustmentNo;
            oModAdjustment.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModAdjustment.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModAdjustment.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
            oModAdjustment.GetSetadjustmentdate = oMainCon.replaceNull(Request.Params.Get("adjustmentdate"));
            oModAdjustment.GetSetadjustmenttype = oMainCon.replaceNull(Request.Params.Get("adjustmenttype"));
            oModAdjustment.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModAdjustment.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModAdjustment.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetadjustmentno = sAdjustmentNo;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("additemdesc"));
            oModLineItem.GetSetlocation = oMainCon.replaceNull(Request.Params.Get("addlocation"));
            oModLineItem.GetSetdatesoh = oMainCon.replaceNull(Request.Params.Get("adddatesoh"));
            oModLineItem.GetSetqtysoh = oMainCon.replaceIntZero(Request.Params.Get("addqtysoh"));
            oModLineItem.GetSetcostsoh = oMainCon.replaceDoubleZero(Request.Params.Get("addcostsoh"));
            oModLineItem.GetSetqtyvariance = oMainCon.replaceIntZero(Request.Params.Get("addqtyvariance"));
            oModLineItem.GetSetpricevariance = oMainCon.replaceDoubleZero(Request.Params.Get("addpricevariance"));
            oModLineItem.GetSetqtyadjusted = oMainCon.replaceIntZero(Request.Params.Get("addqtyadjusted"));
            oModLineItem.GetSetcostadjusted = oMainCon.replaceDoubleZero(Request.Params.Get("addcostadjusted"));
            oModLineItem.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("addremarks"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetadjustmentno = sAdjustmentNo;
            oModLineItem.GetSetlineno = iLineNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PERLARASAN STOK & INVENTORI";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //insert new Adjustment Order
                if (oMainCon.insertAdjustmentHeader(oModAdjustment).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "ADJUSTMENT_ORDER", "ACTIVE");
                    lsAdjustmentLineItem = new ArrayList();
                    sAlertMessage = "SUCCESS|Daftar Maklumat Perlarasan Stok dan Inventori berjaya...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar Maklumat Perlarasan Stok dan Inventori tidak berjaya...";
                    sAdjustmentNo = "";
                    oModAdjustment.GetSetadjustmentno = sAdjustmentNo;
                    sAction = "ADD";
                    sActionString = "DAFTAR PERLARASAN STOK & INVENTORI";
                    lsAdjustmentLineItem = new ArrayList();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar Maklumat Perlarasan Stok dan Inventori tidak berjaya...";
                sAdjustmentNo = "";
                oModAdjustment.GetSetadjustmentno = sAdjustmentNo;
                sAction = "ADD";
                sActionString = "DAFTAR PERLARASAN STOK & INVENTORI";
                lsAdjustmentLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PERLARASAN STOK & INVENTORI";
            if (sAdjustmentNo.Length > 0)
            {
                oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                lsAdjustmentLineItem = oMainCon.getAdjustmentDetailsList(sCurrComp, sAdjustmentNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Perlarasan Stok dan Inventori...";
                oModAdjustment = new MainModel();
                lsAdjustmentLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PERLARASAN STOK & INVENTORI";
            if (sAdjustmentNo.Length > 0)
            {
                oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                lsAdjustmentLineItem = oMainCon.getAdjustmentDetailsList(sCurrComp, sAdjustmentNo, 0, "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Perlarasan Stok dan Inventori...";
                oModAdjustment = new MainModel();
                lsAdjustmentLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //update Adjustment Header
                if (oMainCon.updateAdjustmentHeader(oModAdjustment).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Perlarasan Stok dan Inventori berjaya disimpan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Perlarasan Stok dan Inventori tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PERLARASAN STOK & INVENTORI";
                    oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                    lsAdjustmentLineItem = oMainCon.getAdjustmentDetailsList(sCurrComp, sAdjustmentNo, 0, "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Perlarasan Stok dan Inventori tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PERLARASAN STOK & INVENTORI";
                oModAdjustment = new MainModel();
                lsAdjustmentLineItem = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getAdjustmentDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetitemno, oModLineItem.GetSetlocation, oModLineItem.GetSetdatesoh, "NEW");
                if (modExistent.GetSetadjustmentno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya ditambah. Item tersebut telah ditambah pada No. Perlarasan: " + modExistent.GetSetadjustmentno;
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertAdjustmentDetails(oModLineItem).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Item Perlarasan Stok dan Inventori berjaya ditambah...";
                        Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori berjaya ditambah...";
                        Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori berjaya ditambah...";
                Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getAdjustmentDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetitemno, oModLineItem.GetSetlocation, oModLineItem.GetSetdatesoh, "NEW");
                if (modExistent.GetSetadjustmentno.Length > 0 && !(modExistent.GetSetadjustmentno.Equals(oModLineItem.GetSetadjustmentno) && modExistent.GetSetlineno.Equals(oModLineItem.GetSetlineno)))
                {
                    sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya dikemaskini. Item tersebut telah ditambah pada No. Perlarasan: " + modExistent.GetSetadjustmentno;
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //update line item
                    if (oMainCon.updateAdjustmentDetails(oModLineItem).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Item Perlarasan Stok dan Inventori berjaya dikemaskini...";
                        Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya dikemaskini...";
                        Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya dikemaskini...";
                Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //update line item
                if (oMainCon.deleteAdjustmentDetails(oModLineItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getAdjustmentDetailsList(sCurrComp, sAdjustmentNo, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModLineItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteAdjustmentDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertAdjustmentDetails(oLineItem);
                    }
                    sAlertMessage = "SUCCESS|Item Perlarasan Stok dan Inventori berjaya dikeluarkan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya dikeluarkan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya dikeluarkan...";
                Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //update adjustment header - CONFIRM
                oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                oModAdjustment.GetSetstatus = "CONFIRMED";
                oModAdjustment.GetSetconfirmedby = sUserId;
                if (oMainCon.updateAdjustmentHeader(oModAdjustment).Equals("Y"))
                {
                    //get latest information about Adjustment Header ie. Confirmed Date which is needed for storing item stock transactions
                    oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                    //store stock SOH & stock transaction
                    lsAdjustmentLineItem = oMainCon.getAdjustmentDetailsList(oModAdjustment.GetSetcomp, oModAdjustment.GetSetadjustmentno, 0, "");
                    for (int i = 0; i < lsAdjustmentLineItem.Count; i++)
                    {
                        MainModel oModLineItem = (MainModel)lsAdjustmentLineItem[i];
                        
                        MainModel oModItemStock = new MainModel();
                        oModItemStock.GetSetcomp = oModLineItem.GetSetcomp;
                        oModItemStock.GetSetitemno = oModLineItem.GetSetitemno;
                        oModItemStock.GetSetitemdesc = oModLineItem.GetSetitemdesc;
                        oModItemStock.GetSetlocation = oModLineItem.GetSetlocation;
                        oModItemStock.GetSetdatesoh = oModLineItem.GetSetdatesoh;
                        oModItemStock.GetSetqtysoh = oModLineItem.GetSetqtyadjusted;
                        oModItemStock.GetSetcostsoh = oModLineItem.GetSetcostadjusted;
                        if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                        {
                            String result1 = oMainCon.updateItemStock(oModItemStock);
                        }
                        else 
                        {
                            String result1 = oMainCon.insertItemStock(oModItemStock);
                        }

                        MainModel oModItemStockTrans = new MainModel();
                        oModItemStockTrans.GetSetcomp = oModLineItem.GetSetcomp;
                        oModItemStockTrans.GetSetitemno = oModLineItem.GetSetitemno;
                        oModItemStockTrans.GetSetitemdesc = oModLineItem.GetSetitemdesc;
                        oModItemStockTrans.GetSetlocation = oModLineItem.GetSetlocation;
                        oModItemStockTrans.GetSetdatesoh = oModLineItem.GetSetdatesoh;
                        oModItemStockTrans.GetSettranstype = "ADJUSTMENT_ORDER";
                        oModItemStockTrans.GetSettransdate = oModAdjustment.GetSetconfirmeddate;
                        oModItemStockTrans.GetSettransno = oModLineItem.GetSetadjustmentno;
                        oModItemStockTrans.GetSettrans_lineno = oModLineItem.GetSetlineno;
                        oModItemStockTrans.GetSetorderno = "";
                        oModItemStockTrans.GetSetorder_lineno = 0;
                        oModItemStockTrans.GetSettransqty = oModLineItem.GetSetqtyvariance;
                        oModItemStockTrans.GetSettransprice = oModLineItem.GetSetpricevariance;
                        oModItemStockTrans.GetSetqtysoh = oModLineItem.GetSetqtyadjusted;
                        oModItemStockTrans.GetSetcostsoh = oModLineItem.GetSetcostadjusted;
                        String result2 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                    }
                    sAlertMessage = "SUCCESS|Item Perlarasan Stok dan Inventori berjaya disahkan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya disahkan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Perlarasan Stok dan Inventori tidak berjaya disahkan...";
                Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sAdjustmentNo.Length > 0 && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CONFIRMED" && oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo).GetSetstatus != "CANCELLED")
            {
                //update adjustment header - CANCEL
                oModAdjustment = oMainCon.getAdjustmentHeaderDetails(sCurrComp, sAdjustmentNo);
                oModAdjustment.GetSetstatus = "CANCELLED";
                oModAdjustment.GetSetcancelledby = sUserId;
                if (oMainCon.updateAdjustmentHeader(oModAdjustment).Equals("Y"))
                {
                    //update adjustment header information
                    sAlertMessage = "SUCCESS|Maklumat Perlarasan Stok dan Inventori berjaya dibatalkankan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Perlarasan Stok dan Inventori tidak berjaya dibatalkankan...";
                    Response.Redirect("AdjustmentDetails.aspx?action=OPEN&comp=" + sCurrComp + "&adjustmentno=" + sAdjustmentNo + "&alertmessage=" + sAlertMessage);
                }
            }
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