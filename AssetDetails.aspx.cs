using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssetDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrFyr = DateTime.Now.ToString("yyyy");
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sAssetNo = "";
    public String sAlertMessage = "";
    public MainModel oModAsset = new MainModel();
    public ArrayList lsAssetValueTrans = new ArrayList();
    public ArrayList lsAssetCategory = new ArrayList();

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
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["assetno"] != null)
        {
            sAssetNo = Request.QueryString["assetno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        if (sAction.Equals("ADD"))
        {
            sAssetNo = "";
            oModAsset = new MainModel();
            oModAsset.GetSetdatereg = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
            oModAsset.GetSetstatus = "NEW";
            lsAssetValueTrans = new ArrayList();
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
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidAssetNo") != null)
        {
            sAssetNo = oMainCon.replaceNull(Request.Params.Get("hidAssetNo"));
        }

        //for reset
        if (sAction.Equals("ADD"))
        {
            sAssetNo = "";
            oModAsset = new MainModel();
            oModAsset.GetSetdatereg = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
            oModAsset.GetSetstatus = "NEW";
            lsAssetValueTrans = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModAsset = new MainModel();
            oModAsset.GetSetcomp = sCurrComp;
            oModAsset.GetSetassetno = oMainCon.replaceNull(Request.Params.Get("assetno"));
            oModAsset.GetSetassetdesc = oMainCon.replaceNull(Request.Params.Get("assetdesc"));
            oModAsset.GetSetassetcat = oMainCon.replaceNull(Request.Params.Get("assetcat"));
            oModAsset.GetSetassettyp = oMainCon.replaceNull(Request.Params.Get("assettyp"));
            oModAsset.GetSetassetowner = oMainCon.replaceNull(Request.Params.Get("assetowner"));
            oModAsset.GetSetassetrefno = oMainCon.replaceNull(Request.Params.Get("assetrefno"));
            oModAsset.GetSetdatemfg = oMainCon.replaceNull(Request.Params.Get("datemfg"));
            oModAsset.GetSetwarranty = oMainCon.replaceNull(Request.Params.Get("warranty"));
            oModAsset.GetSetdatewarend = oMainCon.replaceNull(Request.Params.Get("datewarend"));
            oModAsset.GetSetdatereg = oMainCon.replaceNull(Request.Params.Get("datereg"));
            oModAsset.GetSetcostreg = oMainCon.replaceDoubleZero(Request.Params.Get("costreg"));
            oModAsset.GetSetqtyreg = oMainCon.replaceIntZero(Request.Params.Get("qtyreg"));
            oModAsset.GetSetdeprtyp = oMainCon.replaceNull(Request.Params.Get("deprtyp"));
            oModAsset.GetSetdeprrate = oMainCon.replaceDoubleZero(Request.Params.Get("deprrate"));
            oModAsset.GetSetdepraccum = oMainCon.replaceDoubleZero(Request.Params.Get("depraccum"));
            oModAsset.GetSetassetnbv = oMainCon.replaceDoubleZero(Request.Params.Get("assetnbv"));
            oModAsset.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModAsset.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModAsset.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            oModAsset.GetSetassetdesc = oMainCon.replaceNull(Request.Params.Get("assetdesc"));
            oModAsset.GetSetassetcat = oMainCon.replaceNull(Request.Params.Get("assetcat"));
            oModAsset.GetSetassettyp = oMainCon.replaceNull(Request.Params.Get("assettyp"));
            oModAsset.GetSetassetowner = oMainCon.replaceNull(Request.Params.Get("assetowner"));
            oModAsset.GetSetassetrefno = oMainCon.replaceNull(Request.Params.Get("assetrefno"));
            oModAsset.GetSetdatemfg = oMainCon.replaceNull(Request.Params.Get("datemfg"));
            oModAsset.GetSetwarranty = oMainCon.replaceNull(Request.Params.Get("warranty"));
            oModAsset.GetSetdatewarend = oMainCon.replaceNull(Request.Params.Get("datewarend"));
            oModAsset.GetSetdatereg = oMainCon.replaceNull(Request.Params.Get("datereg"));
            oModAsset.GetSetcostreg = oMainCon.replaceDoubleZero(Request.Params.Get("costreg"));
            oModAsset.GetSetqtyreg = oMainCon.replaceIntZero(Request.Params.Get("qtyreg"));
            oModAsset.GetSetdeprtyp = oMainCon.replaceNull(Request.Params.Get("deprtyp"));
            oModAsset.GetSetdeprrate = oMainCon.replaceDoubleZero(Request.Params.Get("deprrate"));
            oModAsset.GetSetdepraccum = oMainCon.replaceDoubleZero(Request.Params.Get("depraccum"));
            oModAsset.GetSetassetnbv = oMainCon.replaceDoubleZero(Request.Params.Get("assetnbv"));
            oModAsset.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("remarks"));
            oModAsset.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("status"));
            oModAsset.GetSetmodifiedby = sUserId;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR ASET HARTA MODAL";
        }
        if (sAction.Equals("CREATE"))
        {
            //insert new Asset
            if (oMainCon.insertAssetDetails(oModAsset).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(sCurrComp, "ASSET_" + oModAsset.GetSetassettyp, "ACTIVE", sCurrFyr);
                if (oModAsset.GetSetcostreg > 0)
                {
                    MainModel modAssetTran = new MainModel();
                    modAssetTran.GetSetcomp = oModAsset.GetSetcomp;
                    modAssetTran.GetSettranno = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                    modAssetTran.GetSettrancode = "REGCOST";
                    modAssetTran.GetSetassetno = oModAsset.GetSetassetno;
                    modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                    modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                    modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                    modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                    modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                    modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                    modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                    modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                    modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                    if (oMainCon.insertAssetTranHeader(modAssetTran).Equals("Y")){
                        oMainCon.updateNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", sCurrFyr);
                        oMainCon.insertAssetTranDetails(modAssetTran);
                    }
                }
                if (oModAsset.GetSetdepraccum > 0)
                {
                    MainModel modAssetTran = new MainModel();
                    modAssetTran.GetSetcomp = oModAsset.GetSetcomp;
                    modAssetTran.GetSettranno = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                    modAssetTran.GetSettrancode = "DEPCOST";
                    modAssetTran.GetSetassetno = oModAsset.GetSetassetno;
                    modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                    modAssetTran.GetSettranvalue = oModAsset.GetSetdepraccum;
                    modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                    modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                    modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                    modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                    modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                    modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                    modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                    if (oMainCon.insertAssetTranHeader(modAssetTran).Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", sCurrFyr);
                        oMainCon.insertAssetTranDetails(modAssetTran);
                    }
                }
                sAlertMessage = "SUCCESS|Daftar maklumat Asset Harta Modal berjaya...";
                Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + oModAsset.GetSetassetno + "&alertmessage=" + sAlertMessage);
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Asset Harta Modal tidak berjaya...";
                sAction = "ADD";
                sActionString = "DAFTAR ASET HARTA MODAL";
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT ASET HARTA MODAL";
            if (sAssetNo.Length > 0)
            {
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Asset Harta Modal...";
                oModAsset = new MainModel();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI ASET HARTA MODAL";
            if (sAssetNo.Length > 0)
            {
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Asset Harta Modal...";
                oModAsset = new MainModel();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sAssetNo.Length > 0)
            {
                //update Asset Details
                if (oMainCon.updateAssetDetails(oModAsset).Equals("Y"))
                {
                    MainModel modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "REGCOST", "", oModAsset.GetSetassetno, "");
                    if (oModAsset.GetSetcostreg > 0 && modAssetTran.GetSettranno.Length > 0)
                    {
                        modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                        modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    else if (oModAsset.GetSetcostreg > 0 && modAssetTran.GetSettranno.Length == 0)
                    {
                        modAssetTran.GetSetcomp = oModAsset.GetSetcomp;
                        modAssetTran.GetSettranno = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                        modAssetTran.GetSettrancode = "REGCOST";
                        modAssetTran.GetSetassetno = oModAsset.GetSetassetno;
                        modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                        modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.insertAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", sCurrFyr);
                            oMainCon.insertAssetTranDetails(modAssetTran);
                        }
                    }
                    else if (oModAsset.GetSetcostreg == 0 && modAssetTran.GetSettranno.Length > 0)
                    {
                        if (oMainCon.deleteAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.deleteAssetTranDetails(modAssetTran);
                        }
                    }

                    modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "DEPCOST", "", oModAsset.GetSetassetno, "");
                    if (oModAsset.GetSetdepraccum > 0 && modAssetTran.GetSettranno.Length > 0)
                    {
                        modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                        modAssetTran.GetSettranvalue = oModAsset.GetSetdepraccum;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    else if (oModAsset.GetSetdepraccum > 0 && modAssetTran.GetSettranno.Length == 0)
                    {
                        modAssetTran.GetSetcomp = oModAsset.GetSetcomp;
                        modAssetTran.GetSettranno = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                        modAssetTran.GetSettrancode = "DEPCOST";
                        modAssetTran.GetSetassetno = oModAsset.GetSetassetno;
                        modAssetTran.GetSettrandate = oModAsset.GetSetdatereg;
                        modAssetTran.GetSettranvalue = oModAsset.GetSetdepraccum;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.insertAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", sCurrFyr);
                            oMainCon.insertAssetTranDetails(modAssetTran);
                        }
                    }
                    else if (oModAsset.GetSetdepraccum == 0 && modAssetTran.GetSettranno.Length > 0)
                    {
                        if (oMainCon.deleteAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.deleteAssetTranDetails(modAssetTran);
                        }
                    }

                    sAlertMessage = "SUCCESS|Maklumat Asset Harta Modal berjaya disimpan...";
                    Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI ASET HARTA MODAL";
                    oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI ASET HARTA MODAL";
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            if (sAssetNo.Length > 0)
            {
                //update CANCEL
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
                oModAsset.GetSetstatus = "CONFIRMED";
                oModAsset.GetSetconfirmedby = sUserId;
                if (oMainCon.updateAssetDetails(oModAsset).Equals("Y"))
                {
                    if (oModAsset.GetSetcostreg > 0)
                    {
                        MainModel modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "REGCOST", "", oModAsset.GetSetassetno, "");
                        modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    if (oModAsset.GetSetdepraccum > 0)
                    {
                        MainModel modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "DEPCOST", "", oModAsset.GetSetassetno, "");
                        modAssetTran.GetSettranvalue = oModAsset.GetSetdepraccum;
                        modAssetTran.GetSettranqty = oModAsset.GetSetqtyreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    sAlertMessage = "SUCCESS|Maklumat Asset Harta Modal berjaya disahkan...";
                    Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya disahkan...";
                    Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya disahkan...";
                Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            if (sAssetNo.Length > 0)
            {
                //update CANCEL
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
                oModAsset.GetSetstatus = "CANCELLED";
                oModAsset.GetSetcancelledby = sUserId;
                if (oMainCon.updateAssetDetails(oModAsset).Equals("Y"))
                {
                    if (oModAsset.GetSetcostreg > 0)
                    {
                        MainModel modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "REGCOST", "", oModAsset.GetSetassetno, "");
                        modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    if (oModAsset.GetSetdepraccum > 0)
                    {
                        MainModel modAssetTran = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, "", "DEPCOST", "", oModAsset.GetSetassetno, "");
                        modAssetTran.GetSettranvalue = oModAsset.GetSetcostreg;
                        modAssetTran.GetSetremarks = oModAsset.GetSetremarks;
                        modAssetTran.GetSetstatus = oModAsset.GetSetstatus;
                        modAssetTran.GetSetcreatedby = oModAsset.GetSetcreatedby;
                        modAssetTran.GetSetmodifiedby = oModAsset.GetSetmodifiedby;
                        modAssetTran.GetSetconfirmedby = oModAsset.GetSetconfirmedby;
                        modAssetTran.GetSetcancelledby = oModAsset.GetSetcancelledby;
                        if (oMainCon.updateAssetTranHeader(modAssetTran).Equals("Y"))
                        {
                            oMainCon.updateAssetTranDetails(modAssetTran);
                        }
                    }
                    sAlertMessage = "SUCCESS|Maklumat Asset Harta Modal berjaya dibatalkan...";
                    Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya dibatalkan...";
                    Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Asset Harta Modal tidak berjaya dibatalkan...";
                Response.Redirect("AssetDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + sAssetNo + "&alertmessage=" + sAlertMessage);
            }
        }

        //get list asset value transaction
        if (sCurrComp.Trim().Length > 0 && sAssetNo.Trim().Length > 0)
        {
            lsAssetValueTrans = oMainCon.getAssetTranList(sCurrComp, "", "", "", sAssetNo, "");
        }
        lsAssetCategory = oMainCon.getParamList("000", "", "ASET_HARTA_MODAL", "");

    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
        }
    }

    //for asset - matzul 07/02/2021

    [WebMethod(EnableSession = true)]
    public static String getAssetNoOnType(string comp, string assettyp, string year)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String nextrunno = "";
        String result = "N";
        String sUserId = "";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        if (sUserId.Length > 0)
        {
            nextrunno = oMainCon.getNextRunningNo(comp, assettyp, "ACTIVE", "", year);
            result = "Y";
        }

        object objNextRunNo = new { status = result, assetno = nextrunno };

        jsonResponse = new JavaScriptSerializer().Serialize(objNextRunNo);

        return jsonResponse;
    }


}