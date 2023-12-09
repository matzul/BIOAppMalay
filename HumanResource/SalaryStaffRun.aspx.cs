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

public partial class HumanResource_SalaryStaffRun : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public Int64 sId = 0;
    public String sSalaryCat = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public HRModel sRunSalaryDetails = new HRModel();
    public ArrayList lsStaffSalaryList = new ArrayList();

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
            sCurrComp = oHRCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oHRCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oHRCon.replaceNull(Session["userid"].ToString());
        if (Request.QueryString["fyr"] != null)
        {
            sCurrFyr = oHRCon.replaceNull(Request.QueryString["fyr"]);
        }
        if (Request.QueryString["id"] != null)
        {
            sId = oHRCon.replaceIntZero(Request.QueryString["id"]);
        }

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";

        sRunSalaryDetails = oHRCon.getRunSalaryDetails(sCurrComp, sCurrFyr, "", "", sId);
        lsStaffSalaryList = oHRCon.getRunSalaryStaffListWithAttachment(sCurrComp, sCurrFyr, "", "", "", 0, sId, 0, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
        this.lsPageList.Items.Add(new ListItem("1", "1"));
        this.lsPageList.SelectedValue = sCurrentPage;
        
    }
    private void getValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oHRCon.replaceNull(Session["comp"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = oHRCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oHRCon.replaceNull(Session["userid"].ToString());

        if (Request.Params.Get("hidFyr") != null)
        {
            sCurrFyr = oHRCon.replaceNull(Request.Params.Get("hidFyr"));
        }
        if (Request.Params.Get("hidId") != null)
        {
            sId = oHRCon.replaceIntZero(Request.Params.Get("hidId"));
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
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
    public static String getStaffItemDetail(string currcomp, string currfyr, Int64 ss_id, Int64 id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oHRMod = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oHRMod = oHRCon.getRunSalaryStaffDetails(currcomp, currfyr, "", "", "", 0, ss_id, 0, id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getSGItemGajiList(string currcomp, string currfyr, string staffno, Int64 sg_id, Int64 ssg_id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsItemOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsSalaryItem = oHRCon.getStaffSalaryItemList(currcomp, currfyr, staffno, sg_id, "", "", "", ssg_id);
            for (int i = 0; i < lsSalaryItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsSalaryItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetsg_id = oHRMod.GetSetsg_id,
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetitemvalue = oHRMod.GetSetitemvalue,
                    GetSetitemgroup = oHRMod.GetSetitemgroup,
                    GetSetitemamount = oHRMod.GetSetitemamount
                };
                lsItemOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemlist = lsItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStaffItemGajiChecked(string currcomp, string currfyr, string staffno, Int64 ss_id, Int64 ssd_id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        ArrayList lsItemOutput = new ArrayList();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            ArrayList lsSalaryItem = oHRCon.getRunStaffSalaryItemList(currcomp, currfyr, staffno, 0, 0, ss_id, ssd_id, "", "", "");
            for (int i = 0; i < lsSalaryItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsSalaryItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetsg_id = oHRMod.GetSetsg_id,
                    GetSetssg_id = oHRMod.GetSetssg_id,
                    GetSetss_id = oHRMod.GetSetss_id,
                    GetSetssd_id = oHRMod.GetSetssd_id,
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetitemvalue = oHRMod.GetSetitemvalue,
                    GetSetitemgroup = oHRMod.GetSetitemgroup,
                    GetSetitemamount = oHRMod.GetSetitemamount
                };
                lsItemOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemlist = lsItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertSGItemGajiUpdate(string currcomp, string currfyr, string staffno, Int64 ss_id, Int64 ssd_id, Int64 sg_id, Int64 ssg_id, HRModel[] inputarray)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && currfyr.Length > 0 && staffno.Length > 0 && ss_id > 0)
        {
            String notin = "";
            HRModel modItem = oHRCon.getRunSalaryStaffDetails(currcomp, currfyr, staffno, "", "", sg_id, ss_id, ssg_id, ssd_id);
            for (int i = 0; i < inputarray.Length; i++)
            {
                HRModel modInput = (HRModel)inputarray[i];
                modItem.GetSetssd_id = ssd_id;
                modItem.GetSetcode = modInput.GetSetcode;
                modItem.GetSetdesc = modInput.GetSetdesc;
                modItem.GetSettype = modInput.GetSettype;
                modItem.GetSetcat = modInput.GetSetcat;
                modItem.GetSetitemvalue = modInput.GetSetitemvalue;
                modItem.GetSetitemgroup = modInput.GetSetitemgroup;
                modItem.GetSetitemamount = modInput.GetSetitemamount;
                HRModel modCheck = oHRCon.getRunStaffSalaryItemDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, modItem.GetSetsg_id, modItem.GetSetssg_id, modItem.GetSetss_id, modItem.GetSetssd_id, modItem.GetSetcat, modItem.GetSettype, modItem.GetSetcode, 0);
                if (modCheck.GetSetid > 0)
                {
                    //just update only!!!
                    modItem.GetSetid = modCheck.GetSetid;
                    String result = oHRCon.updateRunSalaryStaffItem(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table Salary Staff Details Item...";
                        break;
                    }
                    sStatus = "Y";
                    sMessage = "Kemaskini berjaya!";
                }
                else
                {
                    String result = oHRCon.insertRunSalaryStaffItem(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on inserting table Salary Staff Details Item...";
                        break;
                    }
                }
                if (notin.Length > 0)
                {
                    notin = notin + ",'" + modItem.GetSetcode + "'";
                }
                else
                {
                    notin = "'" + modItem.GetSetcode + "'";
                }
            }
            if (sStatus.Equals("Y"))
            {
                //delete for those not in
                ArrayList lsItem = oHRCon.getRunStaffSalaryItemList(currcomp, currfyr, staffno, sg_id, ssg_id, ss_id, ssd_id, "", "", "", notin);
                for (int i = 0; i < lsItem.Count; i++)
                {
                    HRModel oHRMod = (HRModel)lsItem[i];
                    String result = oHRCon.deleteRunSalaryStaffItem(oHRMod);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on cleaning table Salary Staff Details Item...";
                        break;
                    }
                }
            }

        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}

