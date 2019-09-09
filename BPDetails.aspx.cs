using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BPDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sBPId = "";
    public String sAlertMessage = "";
    public MainModel oModBP = new MainModel();
    public ArrayList lsInvoiceCollection = new ArrayList();
    public ArrayList lsBillingPayment = new ArrayList();

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
        if (Request.QueryString["bpid"] != null)
        {
            sBPId = Request.QueryString["bpid"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        if (sAction.Equals("ADD"))
        {
            sBPId = "";
            oModBP = new MainModel();
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
        if (Request.Params.Get("hidBPId") != null)
        {
            sBPId = oMainCon.replaceNull(Request.Params.Get("hidBPId"));
        }

        //for reset
        if (sAction.Equals("ADD"))
        {
            sBPId = "";
            oModBP = new MainModel();
        }
        else if (sAction.Equals("CREATE"))
        {
            sBPId = oMainCon.getNextRunningNo(sCurrComp, "BUSINESS_PARTNER", "ACTIVE");
            oModBP = new MainModel();
            oModBP.GetSetcomp = sCurrComp;
            oModBP.GetSetbpid = sBPId;
            oModBP.GetSetbpcat = oMainCon.replaceNull(Request.Params.Get("bpcat"));
            oModBP.GetSetdiscounttype = oMainCon.replaceNull(Request.Params.Get("discounttype"));
            if (oMainCon.replaceNull(Request.Params.Get("cashguarantee")).Length > 0)
            {
                oModBP.GetSetcashguarantee = double.Parse(oMainCon.replaceNull(Request.Params.Get("cashguarantee")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("bankguarantee")).Length > 0)
            {
                oModBP.GetSetbankguarantee = double.Parse(oMainCon.replaceNull(Request.Params.Get("bankguarantee")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("creditlimit")).Length > 0)
            {
                oModBP.GetSetcreditlimit = double.Parse(oMainCon.replaceNull(Request.Params.Get("creditlimit")));
            }
            oModBP.GetSetbpstatus = oMainCon.replaceNull(Request.Params.Get("bpstatus"));
            oModBP.GetSetbpreference = oMainCon.replaceNull(Request.Params.Get("bpreference"));
            oModBP.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModBP.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModBP.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
        }
        else if (sAction.Equals("SAVE"))
        {
            oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
            oModBP.GetSetbpcat = oMainCon.replaceNull(Request.Params.Get("bpcat"));
            oModBP.GetSetdiscounttype = oMainCon.replaceNull(Request.Params.Get("discounttype"));
            if (oMainCon.replaceNull(Request.Params.Get("cashguarantee")).Length > 0)
            {
                oModBP.GetSetcashguarantee = double.Parse(oMainCon.replaceNull(Request.Params.Get("cashguarantee")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("bankguarantee")).Length > 0)
            {
                oModBP.GetSetbankguarantee = double.Parse(oMainCon.replaceNull(Request.Params.Get("bankguarantee")));
            }
            if (oMainCon.replaceNull(Request.Params.Get("creditlimit")).Length > 0)
            {
                oModBP.GetSetcreditlimit = double.Parse(oMainCon.replaceNull(Request.Params.Get("creditlimit")));
            }
            oModBP.GetSetbpstatus = oMainCon.replaceNull(Request.Params.Get("bpstatus"));
            oModBP.GetSetbpreference = oMainCon.replaceNull(Request.Params.Get("bpreference"));
            oModBP.GetSetbpdesc = oMainCon.replaceNull(Request.Params.Get("bpdesc"));
            oModBP.GetSetbpaddress = oMainCon.replaceNull(Request.Params.Get("bpaddress"));
            oModBP.GetSetbpcontact = oMainCon.replaceNull(Request.Params.Get("bpcontact"));
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "DAFTAR PEMBEKAL & PELANGGAN";
            sBPId = "";
            oModBP = new MainModel();
        }
        else if (sAction.Equals("CREATE"))
        {
            if (sBPId.Length > 0)
            {
                //insert new BP
                if (oMainCon.insertBusinessPartner(oModBP).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(sCurrComp, "BUSINESS_PARTNER", "ACTIVE");
                    sAlertMessage = "SUCCESS|Daftar maklumat Pembekal dan Pelanggan berjaya...";
                    Response.Redirect("BPDetails.aspx?action=OPEN&comp=" + sCurrComp + "&bpid=" + sBPId + "&alertmessage=" + sAlertMessage);
                }
                else 
                {
                    sAlertMessage = "ERROR|Daftar maklumat Pembekal dan Pelanggan tidak berjaya...";
                    sBPId = "";
                    oModBP.GetSetbpid = sBPId;
                    sAction = "ADD";
                    sActionString = "DAFTAR PEMBEKAL & PELANGGAN";
                }
            }
            else
            {
                sAlertMessage = "ERROR|Daftar maklumat Pembekal dan Pelanggan tidak berjaya...";
                sBPId = "";
                oModBP.GetSetbpid = sBPId;
                sAction = "ADD";
                sActionString = "DAFTAR PEMBEKAL & PELANGGAN";
                //Response.Redirect("BPDetails.aspx?action=ADD&comp=" + sCurrComp);
            }
        }
        else if (sAction.Equals("OPEN"))
        {
            sActionString = "MAKLUMAT PEMBEKAL & PELANGGAN";
            if (sBPId.Length > 0)
            {
                oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Pembekal dan Pelanggan...";
                oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI PEMBEKAL & PELANGGAN";
            if (sBPId.Length > 0)
            {
                oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Pembekal dan Pelanggan...";
                oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sBPId.Length > 0)
            {
                //update BP
                if (oMainCon.updateBusinessPartner(oModBP).Equals("Y"))
                {
                    sAlertMessage = "SUCCESS|Maklumat Pembekal dan Pelanggan berjaya disimpan...";
                    Response.Redirect("BPDetails.aspx?action=OPEN&comp=" + sCurrComp + "&bpid=" + sBPId + "&alertmessage=" + sAlertMessage);
                }
                else
                {
                    sAlertMessage = "ERROR|Maklumat Pembekal dan Pelanggan tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI PEMBEKAL & PELANGGAN";
                    oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                }
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Pembekal dan Pelanggan tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI PEMBEKAL & PELANGGAN";
                oModBP = oMainCon.getBPDetails(sCurrComp, sBPId);
                lsInvoiceCollection = new ArrayList();
                lsBillingPayment = new ArrayList();
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