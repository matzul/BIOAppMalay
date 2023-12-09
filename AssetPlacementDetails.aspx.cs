using System;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class AssetPlacementDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sAssetNo = "";
    public int iLineNo = 0;
    public int iOccupiedQty = 0;
    public String sAlertMessage = "";
    public MainModel oModAsset = new MainModel();
    public ArrayList lsAssetPlacement = new ArrayList();
    public ArrayList lsAssetCategory = new ArrayList();
    public ArrayList lsCountry = new ArrayList();
    public ArrayList lsState = new ArrayList();
    public ArrayList lsDistrict = new ArrayList();

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
        if (Request.QueryString["assetno"] != null)
        {
            sAssetNo = Request.QueryString["assetno"].ToString();
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
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidAssetNo") != null)
        {
            sAssetNo = oMainCon.replaceNull(Request.Params.Get("hidAssetNo"));
        }
        if (Request.Params.Get("hidLineNo") != null)
        {
            if (Request.Params.Get("hidLineNo").Length > 0)
                iLineNo = oMainCon.replaceIntZero(Request.Params.Get("hidLineNo"));
        }

        if (sAction.Equals("INSERT"))
        {
            oModAsset = new MainModel();
            oModAsset.GetSetcomp = sCurrComp;
            oModAsset.GetSetassetno = oMainCon.replaceNull(Request.Params.Get("addassetno"));
            oModAsset.GetSetassetdesc = oMainCon.replaceNull(Request.Params.Get("addassetdesc"));
            oModAsset.GetSettranqty = oMainCon.replaceIntZero(Request.Params.Get("addtranqty"));
            oModAsset.GetSettrandate = oMainCon.replaceNull(Request.Params.Get("addtrandate"));
            oModAsset.GetSetcountry = oMainCon.replaceNull(Request.Params.Get("addcountry"));
            oModAsset.GetSetstate = oMainCon.replaceNull(Request.Params.Get("addstate"));
            oModAsset.GetSetdistrict = oMainCon.replaceNull(Request.Params.Get("adddistrict"));
            oModAsset.GetSetlocation = oMainCon.replaceNull(Request.Params.Get("addlocation"));
            oModAsset.GetSetpurpose = oMainCon.replaceNull(Request.Params.Get("addpurpose"));
            oModAsset.GetSetofficerid = oMainCon.replaceNull(Request.Params.Get("addofficerid"));
            oModAsset.GetSetofficername = oMainCon.replaceNull(Request.Params.Get("addofficername"));
            oModAsset.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("addstatus"));
            oModAsset.GetSetcreatedby = sUserId;
        }
        else if (sAction.Equals("UPDATE"))
        {
            oModAsset = oMainCon.getPlacementDetails(iLineNo, sCurrComp, sAssetNo, "", "");
            oModAsset.GetSettranqty = oMainCon.replaceIntZero(Request.Params.Get("addtranqty"));
            oModAsset.GetSettrandate = oMainCon.replaceNull(Request.Params.Get("addtrandate"));
            oModAsset.GetSetcountry = oMainCon.replaceNull(Request.Params.Get("addcountry"));
            oModAsset.GetSetstate = oMainCon.replaceNull(Request.Params.Get("addstate"));
            oModAsset.GetSetdistrict = oMainCon.replaceNull(Request.Params.Get("adddistrict"));
            oModAsset.GetSetlocation = oMainCon.replaceNull(Request.Params.Get("addlocation"));
            oModAsset.GetSetpurpose = oMainCon.replaceNull(Request.Params.Get("addpurpose"));
            oModAsset.GetSetofficerid = oMainCon.replaceNull(Request.Params.Get("addofficerid"));
            oModAsset.GetSetofficername = oMainCon.replaceNull(Request.Params.Get("addofficername"));
            oModAsset.GetSetstatus = oMainCon.replaceNull(Request.Params.Get("addstatus"));
        }
    }

    private void processValues()
    {
        if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PENEMPATAN ASET";
            if (sAssetNo.Length > 0)
            {
                oModAsset = oMainCon.getAssetDetails(sCurrComp, sAssetNo);
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Penempatan Asset...";
                oModAsset = new MainModel();
            }
        }
        else if (sAction.Equals("INSERT"))
        {
            //insert new Asset
            if (oMainCon.insertPlacementDetails(oModAsset).Equals("Y"))
            {
                sAlertMessage = "SUCCESS|Daftar maklumat Penempatan Aset berjaya...";
                Response.Redirect("AssetPlacementDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + oModAsset.GetSetassetno + "&alertmessage=" + sAlertMessage);
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Penempatan Aset tidak berjaya...";
                Response.Redirect("AssetPlacementDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + oModAsset.GetSetassetno + "&alertmessage=" + sAlertMessage);
            }
        }
        else if (sAction.Equals("UPDATE"))
        {
            //insert new Asset
            if (oMainCon.updatePlacementDetails(oModAsset).Equals("Y"))
            {
                sAlertMessage = "SUCCESS|Kemaskini maklumat Penempatan Aset berjaya...";
                Response.Redirect("AssetPlacementDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + oModAsset.GetSetassetno + "&alertmessage=" + sAlertMessage);
            }
            else
            {
                sAlertMessage = "ERROR|Kemaskini maklumat Penempatan Aset tidak berjaya...";
                Response.Redirect("AssetPlacementDetails.aspx?action=OPEN&comp=" + sCurrComp + "&assetno=" + oModAsset.GetSetassetno + "&alertmessage=" + sAlertMessage);
            }
        }

        //get list asset placement
        if (sCurrComp.Trim().Length > 0 && sAssetNo.Trim().Length > 0)
        {
            lsAssetPlacement = oMainCon.getListPlacement(sCurrComp, sAssetNo, "", "");
        }

        lsAssetCategory = oMainCon.getParamList("000", "", "ASET_HARTA_MODAL", "");
        lsCountry = oMainCon.getParamList("000", "", "COUNTRY", "");
        lsState = oMainCon.getParamList("000", "", "STATE", "");
        lsDistrict = oMainCon.getParamList("000", "", "DISTRICT", "");

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
    public static String getOfficerList(string comp)
    {
        MainController oMainCon = new MainController();
        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String result = "N";
        String sUserId = "";
        ArrayList lsOfficer = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oMainCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        if (sUserId.Length > 0)
        {
            lsOfficer = oMainCon.getOfficerList(comp, "", "");
            result = "Y";
        }

        object obOfficer = new { result = result, officerlist = lsOfficer };

        jsonResponse = new JavaScriptSerializer().Serialize(obOfficer);

        return jsonResponse;
    }

}