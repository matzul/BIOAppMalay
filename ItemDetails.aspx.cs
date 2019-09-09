using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sUSerType = "";
    public String sAction = "";
    public String sActionString = "";
    public String sItemNo = "";
    public String sAlertMessage = "";
    public MainModel oModItem = new MainModel();
    public MainModel oModLineItem = new MainModel();
    public ArrayList lsItemSOH = new ArrayList();
    public ArrayList lsItemDiscount = new ArrayList();

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
        if(Session["usertype"] != null)
        {
            sUSerType = Session["usertype"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["itemno"] != null)
        {
            sItemNo = Request.QueryString["itemno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
    }

    private void getValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["usertype"] != null)
        {
            sUSerType = Session["usertype"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidItemNo") != null)
        {
            sItemNo = oMainCon.replaceNull(Request.Params.Get("hidItemNo"));
        }

        //for reset
        if (sAction.Equals("ADD"))
        {
            sItemNo = "";
            oModItem = new MainModel();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModItem = new MainModel();
            oModItem.GetSetcomp = sCurrComp;
            oModItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("itemno"));
            oModItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("itemdesc"));
            oModItem.GetSetitemcat = oMainCon.replaceNull(Request.Params.Get("itemcat"));
            oModItem.GetSetitemtype = oMainCon.replaceNull(Request.Params.Get("itemtype"));
            oModItem.GetSetitemstatus = oMainCon.replaceNull(Request.Params.Get("itemstatus"));
            if (oMainCon.replaceNull(Request.Params.Get("purchaseprice")).Length > 0)
            {
                oModItem.GetSetpurchaseprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("purchaseprice")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("costprice")).Length > 0)
            {
                oModItem.GetSetcostprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("costprice")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("salesprice")).Length > 0)
            {
                oModItem.GetSetsalesprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("salesprice")));
            }
            oModItem.GetSetqtyorder = oMainCon.replaceIntZero(Request.Params.Get("qtyorder"));
            oModItem.GetSetqtydemand = oMainCon.replaceIntZero(Request.Params.Get("qtydemand"));
            oModItem.GetSetqtysoh = oMainCon.replaceIntZero(Request.Params.Get("qtysoh"));
            oModItem.GetSetcostsoh = oMainCon.replaceDoubleZero(Request.Params.Get("costsoh"));
            oModItem.GetSetqtysafetystock = oMainCon.replaceIntZero(Request.Params.Get("qtysafetystock"));
        }
        else if (sAction.Equals("SAVE"))
        {
            oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
            oModItem.GetSetitemdesc = oMainCon.replaceNull(Request.Params.Get("itemdesc"));
            oModItem.GetSetitemcat = oMainCon.replaceNull(Request.Params.Get("itemcat"));
            oModItem.GetSetitemtype = oMainCon.replaceNull(Request.Params.Get("itemtype"));
            oModItem.GetSetitemstatus = oMainCon.replaceNull(Request.Params.Get("itemstatus"));
            if (oMainCon.replaceNull(Request.Params.Get("purchaseprice")).Length > 0)
            {
                oModItem.GetSetpurchaseprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("purchaseprice")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("costprice")).Length > 0)
            {
                oModItem.GetSetcostprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("costprice")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("salesprice")).Length > 0)
            {
                oModItem.GetSetsalesprice = double.Parse(oMainCon.replaceNull(Request.Params.Get("salesprice")));
            }
            oModItem.GetSetqtyorder = oMainCon.replaceIntZero(Request.Params.Get("qtyorder"));
            oModItem.GetSetqtydemand = oMainCon.replaceIntZero(Request.Params.Get("qtydemand"));
            oModItem.GetSetqtysoh = oMainCon.replaceIntZero(Request.Params.Get("qtysoh"));
            oModItem.GetSetcostsoh = oMainCon.replaceDoubleZero(Request.Params.Get("costsoh"));
            oModItem.GetSetqtysafetystock = oMainCon.replaceIntZero(Request.Params.Get("qtysafetystock"));
        }
        else if (sAction.Equals("INSERT") || sAction.Equals("UPDATE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("addordercat"));
            oModLineItem.GetSetdiscounttype = oMainCon.replaceNull(Request.Params.Get("addordertype"));
            oModLineItem.GetSetdisccat = oMainCon.replaceNull(Request.Params.Get("adddisccat"));
            oModLineItem.GetSetdiscvalue = oMainCon.replaceDoubleZero(Request.Params.Get("adddiscvalue"));
            oModLineItem.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("addstatus"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = sCurrComp;
            oModLineItem.GetSetlineno = oMainCon.replaceIntZero(Request.Params.Get("addlineno"));
            oModLineItem.GetSetitemno = oMainCon.replaceNull(Request.Params.Get("additemno"));
            oModLineItem.GetSetordercat = oMainCon.replaceNull(Request.Params.Get("addordercat"));
            oModLineItem.GetSetdiscounttype = oMainCon.replaceNull(Request.Params.Get("addordertype"));
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR ITEM & PRODUK";
            sItemNo = "";
            oModItem = new MainModel();
        }
        if (sAction.Equals("CREATE"))
        {
            //insert new Item
            if (oMainCon.insertItemMaster(oModItem).Equals("Y"))
            {
                sAlertMessage = "SUCCESS|Daftar maklumat Item dan Produk berjaya...";
                Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + oModItem.GetSetitemno + "&alertmessage=" + sAlertMessage);
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Item dan Produk tidak berjaya...";
                sAction = "ADD";
                sActionString = "DAFTAR ITEM & PRODUK";
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT ITEM & PRODUK";
            if (sItemNo.Length > 0)
            {
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsItemSOH = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Item dan Produk...";
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsItemSOH = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI ITEM & PRODUK";
            if (sItemNo.Length > 0)
            {
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsItemSOH = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Item dan Produk...";
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsItemSOH = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sItemNo.Length > 0)
            {
                //update Item Master
                if (oMainCon.updateItemMaster(oModItem).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Item dan Produk berjaya disimpan...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Item dan Produk tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI ITEM & PRODUK";
                    oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                }
                lsItemSOH = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Item dan Produk tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI ITEM & PRODUK";
                oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
                lsItemSOH = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (sItemNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //insert new line item discount
                if (oMainCon.insertItemDiscount(oModLineItem).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Jadual Harga Item berjaya ditambah...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya ditambah...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya ditambah...";
                Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            if (sItemNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //update line item discount
                if (oMainCon.updateItemDiscount(oModLineItem).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Jadual Harga Item berjaya dikemaskini...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya dikemaskini...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya dikemaskini...";
                Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (sItemNo.Length > 0 && oModLineItem.GetSetlineno > 0)
            {
                //delete line item discount
                if (oMainCon.deleteItemDiscount(oModLineItem).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Jadual Harga Item berjaya dikeluarkan...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya dikeluarkan...";
                    Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);

                }
            }
            else
            {
                sAlertMessage = "ERROR|Jadual Harga Item tidak berjaya dikeluarkan...";
                Response.Redirect("ItemDetails.aspx?action=OPEN&comp=" + sCurrComp + "&itemno=" + sItemNo + "&alertmessage=" + sAlertMessage);
            }
        }
        //get list item discount
        if (sCurrComp.Trim().Length > 0 && sItemNo.Trim().Length > 0)
        {
            lsItemDiscount = oMainCon.getItemDiscountList(sCurrComp, "", "", sItemNo);
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