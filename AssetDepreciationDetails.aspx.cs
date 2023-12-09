using System;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class AssetDepreciationDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrFyr = DateTime.Now.ToString("yyyy");
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sTranNo = "";
    public String sTranCode = "";
    public String sTranCat = "";
    public String sStatus = "";
    public String sAssetNo = "";
    public int iLineNo = 0;
    public String sAlertMessage = "";
    public MainModel oModDep = new MainModel();
    public MainModel oModAsset = new MainModel();
    public ArrayList lsAssetDepreciation = new ArrayList();

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
        if (Request.QueryString["tranno"] != null)
        {
            sTranNo = Request.QueryString["tranno"].ToString();
        }
        if (Request.QueryString["trancode"] != null)
        {
            sTranCode = Request.QueryString["trancode"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }

        if (sAction.Equals("ADD"))
        {
            sTranNo = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
            sTranCode = "DEPCOST";
            oModDep = new MainModel();
            oModDep.GetSetcomp = sCurrComp;
            oModDep.GetSettranno = sTranNo;
            oModDep.GetSettrancode = sTranCode;
            oModDep.GetSettrandate = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
            oModDep.GetSetstatus = "NEW";
            oModAsset = new MainModel();
            lsAssetDepreciation = new ArrayList();
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
        if (Request.Params.Get("txttranno") != null)
        {
            sTranNo = oMainCon.replaceNull(Request.Params.Get("txttranno"));
        }
        if (Request.Params.Get("txttrancode") != null)
        {
            sTranCode = oMainCon.replaceNull(Request.Params.Get("txttrancode"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }
        if (Request.Params.Get("hidAssetNo") != null)
        {
            if (Request.Params.Get("hidAssetNo").Length > 0)
                sAssetNo = oMainCon.replaceNull(Request.Params.Get("hidAssetNo"));
        }

        //for reset
        if (sAction.Equals("ADD"))
        {
            sTranNo = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
            sTranCode = "DEPCOST";
            oModDep = new MainModel();
            oModDep.GetSetcomp = sCurrComp;
            oModDep.GetSettranno = sTranNo;
            oModDep.GetSettrancode = sTranCode;
            oModDep.GetSettrandate = DateTime.Now.ToString("dd-MM") + "-" + sCurrFyr;
            oModDep.GetSetstatus = "NEW";
            oModAsset = new MainModel();
            lsAssetDepreciation = new ArrayList();
        }
        else if (sAction.Equals("CREATE"))
        {
            oModDep = new MainModel();
            oModDep.GetSetcomp = sCurrComp;
            oModDep.GetSettranno = sTranNo;
            oModDep.GetSettrancode = sTranCode;
            oModDep.GetSettrandate = oMainCon.replaceNull(Request.Params.Get("txttrandate"));
            oModDep.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("txtremarks"));
            oModDep.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("txtstatus"));
            oModDep.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("SAVE"))
        {
            oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, "", "");
            oModDep.GetSettrandate = oMainCon.replaceNull(Request.Params.Get("txttrandate"));
            oModDep.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("txtremarks"));
            oModDep.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("txtstatus"));
            oModDep.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("INSERT"))
        {
            oModAsset = new MainModel();
            oModAsset.GetSetcomp = sCurrComp;
            oModAsset.GetSettranno = sTranNo;
            oModAsset.GetSettrancode = sTranCode;
            oModAsset.GetSetassetno = oMainCon.replaceNull(Request.Params.Get("addassetno"));
            oModAsset.GetSetassetdesc = oMainCon.replaceNull(Request.Params.Get("addassetdesc"));
            oModAsset.GetSettranqty = oMainCon.replaceIntZero(Request.Params.Get("addtranqty"));
            oModAsset.GetSettranvalue = oMainCon.replaceDoubleZero(Request.Params.Get("addtranvalue"));
            oModAsset.GetSetremarks = oMainCon.replaceNull(Request.Params.Get("addremarks"));
        }
        else if (sAction.Equals("DELETE"))
        {
            oModAsset = new MainModel();
            oModAsset.GetSetcomp = sCurrComp;
            oModAsset.GetSettranno = sTranNo;
            oModAsset.GetSettrancode = sTranCode;
            oModAsset.GetSetlineno = iLineNo;
            oModAsset.GetSetassetno = sAssetNo;
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR TRANSAKSI SUSUT NILAI";
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sCurrComp.Length > 0 && sTranNo.Length > 0 && sTranCode.Length > 0)
            {
                //insert new tran header
                if (oMainCon.insertAssetTranHeader(oModDep).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", sCurrFyr);
                    //register list of asset for the transaction
                    if (oMainCon.insertListAssetTranDetails(oModDep).Equals("Y"))
                    {
                        oModAsset = new MainModel();
                        lsAssetDepreciation = oMainCon.getAssetTranListBefore(sCurrComp, sTranNo, sTranCode, sTranCat, "", "");
                    }
                    else
                    {
                        oModAsset = new MainModel();
                        lsAssetDepreciation = new ArrayList();
                    }
                    sAlertMessage = "SUCCESS|Daftar maklumat Transaksi Susut Nilai berjaya...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Daftar maklumat Transaksi Susut Nilai tidak berjaya...";
                    sTranNo = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                    sTranCode = "DEPCOST";
                    oModDep = new MainModel();
                    oModDep.GetSetcomp = sCurrComp;
                    oModDep.GetSettranno = sTranNo;
                    oModDep.GetSettrancode = sTranCode;
                    oModDep.GetSettrandate = DateTime.Now.ToShortDateString();
                    oModDep.GetSetstatus = "NEW";
                    oModAsset = new MainModel();
                    lsAssetDepreciation = new ArrayList();
                    sActionString = "DAFTAR TRANSAKSI SUSUT NILAI";
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Transaksi Susut Nilai tidak berjaya...";
                sTranNo = oMainCon.getNextRunningNo(sCurrComp, "ASSET_TRAN", "ACTIVE", "", sCurrFyr);
                sTranCode = "DEPCOST";
                oModDep = new MainModel();
                oModDep.GetSetcomp = sCurrComp;
                oModDep.GetSettranno = sTranNo;
                oModDep.GetSettrancode = sTranCode;
                oModDep.GetSettrandate = DateTime.Now.ToShortDateString();
                oModDep.GetSetstatus = "NEW";
                oModAsset = new MainModel();
                lsAssetDepreciation = new ArrayList();
                sActionString = "DAFTAR TRANSAKSI SUSUT NILAI";
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT TRANSAKSI SUSUT NILAI";
            if (sCurrComp.Length > 0 && sTranNo.Length > 0 && sTranCode.Length > 0)
            {
                oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, sTranCat, sStatus);
                lsAssetDepreciation = oMainCon.getAssetTranListBefore(sCurrComp, sTranNo, sTranCode, sTranCat, "", "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Transaksi Susut Nilai...";
                oModDep = new MainModel();
                lsAssetDepreciation = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI TRANSAKSI SUSUT NILAI";
            if (sCurrComp.Length > 0 && sTranNo.Length > 0 && sTranCode.Length > 0)
            {
                oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, sTranCat, sStatus);
                lsAssetDepreciation = oMainCon.getAssetTranListBefore(sCurrComp, sTranNo, sTranCode, sTranCat, "", "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Transaksi Susut Nilai...";
                oModDep = new MainModel();
                lsAssetDepreciation = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sCurrComp.Length > 0 && sTranNo.Length > 0 && sTranCode.Length > 0)
            {
                //update Order
                if (oMainCon.updateAssetTranHeader(oModDep).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Transaksi Susut Nilai berjaya disimpan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Transaksi Susut Nilai tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI TRANSAKSI SUSUT NILAI";
                    oModAsset = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, sTranCat, sStatus);
                    lsAssetDepreciation = oMainCon.getAssetTranListBefore(sCurrComp, sTranNo, sTranCode, sTranCat, "", "");
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Transaksi Susut Nilai tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI TRANSAKSI SUSUT NILAI";
                oModAsset = new MainModel();
                lsAssetDepreciation = new ArrayList();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            if (oModAsset.GetSettranno.Length > 0 && oModAsset.GetSettrancode.Length > 0 && oModAsset.GetSetassetno.Length > 0)
            {
                //check whether already exist in Order Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getAssetTranDetails(oModAsset.GetSetcomp, oModAsset.GetSettranno, oModAsset.GetSettrancode, "", oModAsset.GetSetassetno, "");
                if (modExistent.GetSettranno.Length > 0)
                {
                    sAlertMessage = "ERROR|Item Susut Nilai tidak berjaya ditambah. Item tersebut telah ditambah pada Transaksi Susut Nilai!!!";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + oModAsset.GetSettranno + "&trancode=" + oModAsset.GetSettrancode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertAssetTranDetails(oModAsset).Equals("Y"))
                    {
                        sAlertMessage = "SUCCESS|Item Susut Nilai berjaya ditambah...";
                        Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + oModAsset.GetSettranno + "&trancode=" + oModAsset.GetSettrancode + "&alertmessage=" + sAlertMessage);
                    }
                    else
                    {
                        sAlertMessage = "ERROR|Item Susut Nilai tidak berjaya ditambah...";
                        Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + oModAsset.GetSettranno + "&trancode=" + oModAsset.GetSettrancode + "&alertmessage=" + sAlertMessage);
                    }
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Susut Nilai tidak berjaya ditambah...";
                Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("DELETE"))
        {
            if (oModAsset.GetSettranno.Length > 0 && oModAsset.GetSettrancode.Length > 0 && oModAsset.GetSetassetno.Length > 0)
            {
                //delete line item
                if (oMainCon.deleteAssetTranDetails(oModAsset).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Item Susut Nilai berjaya dikeluarkan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + oModAsset.GetSettranno + "&trancode=" + oModAsset.GetSettrancode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Item Susut Nilai tidak berjaya dikeluarkan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + oModAsset.GetSettranno + "&trancode=" + oModAsset.GetSettrancode + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Item Susut Nilai tidak berjaya dikeluarkan...";
                Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CONFIRM"))
        {
            oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, "", "");
            if (oModDep.GetSetcomp.Length > 0 && oModDep.GetSettranno.Length > 0 && oModDep.GetSettrancode.Length > 0 && oModDep.GetSetstatus != "CONFIRMED" && oModDep.GetSetstatus != "CANCELLED")
            {
                //update header - CONFIRM
                oModDep.GetSetstatus = "CONFIRMED";
                oModDep.GetSetconfirmedby = sUserId;
                if (oMainCon.updateAssetTranHeader(oModDep).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Susut Nilai berjaya disahkan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Susut Nilai tidak berjaya disahkan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }

            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Susut Nilai tidak berjaya disahkan...";
                Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("CANCEL"))
        {
            oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, "", "");
            if (oModDep.GetSetcomp.Length > 0 && oModDep.GetSettranno.Length > 0 && oModDep.GetSettrancode.Length > 0)
            {
                //update header - CANCEL
                oModDep = oMainCon.getAssetTranHeaderDetails(sCurrComp, sTranNo, sTranCode, "", "");
                oModDep.GetSetstatus = "CANCELLED";
                oModDep.GetSetordercancelled = sUserId;
                if (oMainCon.updateAssetTranHeader(oModDep).Equals("Y"))
                {
                    //update order header information
                    sAlertMessage = "SUCCESS|Maklumat Susut Nilai berjaya dibatalkankan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Susut Nilai tidak berjaya dibatalkankan...";
                    Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
                }
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Susut Nilai tidak berjaya dibatalkankan...";
                Response.Redirect("AssetDepreciationDetails.aspx?action=OPEN&comp=" + sCurrComp + "&tranno=" + sTranNo + "&trancode=" + sTranCode + "&alertmessage=" + sAlertMessage);
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

    [WebMethod(EnableSession = true)]
    public static String updateAssetDepr(string currcomp, string fyr, string tranno, string trancode, string assetdeprupdate)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        ArrayList lsDeprAsset = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";


        if (currcomp.Length > 0 && tranno.Length > 0 && trancode.Length > 0)
        {
            if (assetdeprupdate.Length > 0)
            {
                ArrayList lsAssetDeprToUpdate = oMainCon.tokenString(assetdeprupdate, ",");
                for (int i = 0; i < lsAssetDeprToUpdate.Count; i++)
                {
                    String deprdata = (String)lsAssetDeprToUpdate[i];
                    ArrayList lsData = oMainCon.tokenString(deprdata, "|");
                    if (lsData.Count > 0)
                    {
                        String assetno = lsData.Count > 0 ? (String)lsData[0] : "";
                        double tranvalue = lsData.Count > 1 ? ((String)lsData[1]).Trim().Length > 0 ? double.Parse((String)lsData[1]) : 0 : 0;

                        MainModel modAssetTranDet = oMainCon.getAssetTranDetails(currcomp, tranno, trancode,"", assetno, "");
                        if (modAssetTranDet.GetSetassetno.Length > 0)
                        {
                            modAssetTranDet.GetSettranvalue = tranvalue;
                            String k = oMainCon.updateAssetTranDetails(modAssetTranDet);
                        }
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";

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
    public static String getAssetList(string currcomp, string status)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsAsset = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisCOATran = oMainCon.getAssetList(currcomp, "", "", "", "", status);
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                MainModel oMainMod = (MainModel)lsFisCOATran[i];

                Object objData = new
                {
                    GetSetassetno = oMainMod.GetSetassetno,
                    GetSetassetdesc = oMainMod.GetSetassetdesc
                };
                lsAsset.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, assetlist = lsAsset };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getAssetDetails(string currcomp, string assetno)
    {
        MainController oMainCon = new MainController();
        String sUserId = "";
        String sStatus = "N";

        MainModel oMainMod = new MainModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && assetno.Length > 0)
        {
            oMainMod = oMainCon.getAssetDetails(currcomp, assetno);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, assetdetail = oMainMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}