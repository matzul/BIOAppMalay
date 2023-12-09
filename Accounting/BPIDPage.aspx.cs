using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_BPIDPage : System.Web.UI.Page
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sCurrBpId = "";
    public String sCurrBpDesc = "";
    public String sCurrBpRef = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsFisBP = new ArrayList();

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
            sCurrComp = oAccCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oAccCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oAccCon.replaceNull(Session["userid"].ToString());
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oAccCon.replaceNull(Request.QueryString["fyr"]);
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsFisBP = oAccCon.getFisBpList(sCurrComp, 0, sCurrBpId, sCurrBpDesc, sCurrBpRef, "");
        this.lsPageList.Items.Add(new ListItem("1", "1"));
        this.lsPageList.SelectedValue = sCurrentPage;
        
    }
    private void getValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oAccCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oAccCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oAccCon.replaceNull(Session["userid"].ToString());

        if (Request.Params.Get("txtFindBpId") != null)
        {
            sCurrBpId = oAccCon.replaceNull(Request.Params.Get("txtFindBpId"));
        }
        if (Request.Params.Get("txtFindBpDesc") != null)
        {
            sCurrBpDesc = oAccCon.replaceNull(Request.Params.Get("txtFindBpDesc"));
        }

        if (Request.Params.Get("txtFindBpRef") != null)
        {
            sCurrBpRef = oAccCon.replaceNull(Request.Params.Get("txtFindBpRef"));
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    protected void lsPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
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
    public static String getFisBpList(string currcomp)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsFisBpOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsFisBp = oAccCon.getFisBpList(currcomp, 0, "", "", "", "");
            for (int i = 0; i < lsFisBp.Count; i++)
            {
                AccountingModel oAccMod = (AccountingModel)lsFisBp[i];

                Object objData = new
                {
                    GetSetbpid = oAccMod.GetSetbpid,
                    GetSetbpdesc = oAccMod.GetSetbpdesc,
                    GetSetbpreference = oAccMod.GetSetbpreference
                };
                lsFisBpOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbplist = lsFisBpOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getFisBpDetail(string currcomp, int id)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";

        AccountingModel oAccMod = new AccountingModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oAccMod = oAccCon.getFisBpDetail(currcomp, id, "", "", "", "");
            sStatus = "Y";
        }

        object retData = new { result = sStatus, fisbpdetail = oAccMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertFisBpDetail(string currcomp, string bpid, string bpdesc, string bpaddress, string bpcontact, string bpreference, string status)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && bpid.Length > 0)
        {
            AccountingModel modItem = oAccCon.getFisBpDetail(currcomp, 0, bpid, "", "", "");
            if (modItem.GetSetid == 0)
            {
                modItem = new AccountingModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetbpid = bpid;
                modItem.GetSetbpdesc = bpdesc;
                modItem.GetSetbpaddress = bpaddress;
                modItem.GetSetbpcontact = bpcontact;
                modItem.GetSetbpreference = bpreference;
                modItem.GetSetstatus = status;
                if (modItem.GetSetstatus.Equals("NEW"))
                {
                    modItem.GetSetcreatedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CONFIRMED"))
                {
                    modItem.GetSetconfirmedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CANCELLED"))
                {
                    modItem.GetSetcancelledby = sUserId;
                }
                int i = oAccCon.insertFisBp(modItem);
                if (i > 0)
                {
                    sStatus = "Y";
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table FisBp...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & BpId: " + modItem.GetSetbpid + " & Nama/ Keterangan: " + modItem.GetSetbpdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateFisBpDetail(string currcomp, int id, string bpid, string bpdesc, string bpaddress, string bpcontact, string bpreference, string status)
    {
        AccountingController oAccCon = new AccountingController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oAccCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            AccountingModel modItem = oAccCon.getFisBpDetail(currcomp, id, "", "", "", "");
            if(modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                ArrayList lsFisCOATran = oAccCon.getFisCOATranList(modItem.GetSetcomp, "", "", "", "", 0, "", "CUSTOMER", modItem.GetSetbpid, "", "", "");
                if (lsFisCOATran.Count > 0)
                {
                    if (modItem.GetSetbpid.Equals(bpid) && modItem.GetSetbpdesc.Equals(bpdesc))
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in FisCOATran for Comp: " + modItem.GetSetcomp + " & Name/ Keterangan: " + modItem.GetSetbpdesc + ". Please remove all record first...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }

                ArrayList lsFisCOATran2 = oAccCon.getFisCOATranList(modItem.GetSetcomp, "", "", "", "", 0, "", "SUPPLIER", modItem.GetSetbpid, "", "", "");
                if (lsFisCOATran2.Count > 0 && proceeUpdate)
                {
                    if (modItem.GetSetbpid.Equals(bpid) && modItem.GetSetbpdesc.Equals(bpdesc))
                    {
                        proceeUpdate = true;
                    }
                    else
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in FisCOATran for Comp: " + modItem.GetSetcomp + " & Name/ Keterangan: " + modItem.GetSetbpdesc + ". Please remove all record first...";
                    }
                }
                else
                {
                    proceeUpdate = true;
                }

                if (proceeUpdate) 
                {
                    modItem.GetSetbpid = bpid;
                    modItem.GetSetbpdesc = bpdesc;
                    modItem.GetSetbpaddress = bpaddress;
                    modItem.GetSetbpcontact = bpcontact;
                    modItem.GetSetbpreference = bpreference;
                    modItem.GetSetstatus = status;
                    if (modItem.GetSetstatus.Equals("NEW"))
                    {
                        modItem.GetSetcreatedby = sUserId;
                    }
                    else if (modItem.GetSetstatus.Equals("CONFIRMED"))
                    {
                        modItem.GetSetconfirmedby = sUserId;
                    }
                    else if (modItem.GetSetstatus.Equals("CANCELLED"))
                    {
                        modItem.GetSetcancelledby = sUserId;
                    }
                    int i = oAccCon.updateFisBp(modItem);
                    if (i > 0)
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table FisBp...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Id: " + bpid + " & Nama/ Keterangan: " + bpdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}