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

public partial class HumanResource_AttendanceWorkingGroup : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sAction = "";

    public String sCode = "";
    public String sDesc = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsWorkingGroup = new ArrayList();

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

        sCurrentPage = this.lsPageList.SelectedValue;
        this.lsPageList.Items.Clear();
    }
    private void processValues()
    {
        sTotalPage = "1";
        sCurrentPage = "1";
        lsWorkingGroup = oHRCon.getCompWorkingGroupList(sCurrComp, sCurrFyr, sCode, sDesc, "", "");
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

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oHRCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (!sAction.Equals("RESET"))
        {
            if (Request.Params.Get("lsFindFyr") != null)
            {
                sCurrFyr = oHRCon.replaceNull(Request.Params.Get("lsFindFyr"));
            }
        }

        if (Request.Params.Get("txtFindItemCode") != null)
        {
            sCode = oHRCon.replaceNull(Request.Params.Get("txtFindItemCode"));
        }
        if (Request.Params.Get("txtFindItemDesc") != null)
        {
            sDesc = oHRCon.replaceNull(Request.Params.Get("txtFindItemDesc"));
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
    public static String getWGItemList(string currcomp, string currfyr)
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
            ArrayList lsFisItem = oHRCon.getCompWorkingGroupList(currcomp, currfyr, "", "", "", "");
            for (int i = 0; i < lsFisItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsFisItem[i];

                Object objData = new
                {
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat
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
    public static String getWGItemDetail(string currcomp, int id)
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
            oHRMod = oHRCon.getCompWorkingGroupDetails(currcomp, "", "", id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String insertWGItemDetail(string currcomp, string currfyr, string code, string desc, string fromdate, string todate, string rest_day, string status)
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

        if (currcomp.Length > 0 && currfyr.Length > 0)
        {
            HRModel modItem = oHRCon.getCompWorkingGroupDetails(currcomp, currfyr, code, 0);
            if (modItem.GetSetid == 0)
            {
                modItem = new HRModel();
                modItem.GetSetcomp = currcomp;
                modItem.GetSetfyr = currfyr;
                modItem.GetSetcode = code;
                modItem.GetSetdesc = desc;
                modItem.GetSetfromdate = fromdate;
                modItem.GetSettodate = todate;
                modItem.GetSetrest_day = rest_day;
                modItem.GetSetstatus = status;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertCompWorkingGroup(modItem);
                if (result.Equals("Y"))
                {
                    modItem = oHRCon.getCompWorkingGroupDetails(currcomp, currfyr, code, 0);
                    if (modItem.GetSetid != 0)
                    {
                        result = oHRCon.insertCompWorkingDayBulk(modItem);
                    }
                    sStatus = result;
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table Working Group...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Tarikh: " + modItem.GetSetfromdate + " & Nama/ Keterangan: " + modItem.GetSetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateWGItemDetail(string currcomp, string currfyr, string code, string desc, string fromdate, string todate, string rest_day, string status, int id)
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

        if (currcomp.Length > 0 && id > 0)
        {
            HRModel modItem = oHRCon.getCompWorkingGroupDetails(currcomp, "", "", id);
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = false;
                if (!modItem.GetSetcode.Equals(code))
                {
                    ArrayList lsItemList = oHRCon.getCompWorkingGroupList(currcomp, currfyr, code, "", "", "");
                    if (lsItemList.Count > 0)
                    {
                        proceeUpdate = false;
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Record already exist in Working Group for Comp: " + modItem.GetSetcomp + " & Code: " + code + " & Name/ Keterangan: " + modItem.GetSetdesc + ". Please remove record first...";
                    }
                    else
                    {
                        proceeUpdate = true;
                    }
                }
                else
                {
                    proceeUpdate = true;
                }
                if (proceeUpdate)
                {
                    modItem.GetSetcomp = currcomp;
                    modItem.GetSetfyr = currfyr;
                    modItem.GetSetcode = code;
                    modItem.GetSetdesc = desc;
                    modItem.GetSetfromdate = fromdate;
                    modItem.GetSettodate = todate;
                    modItem.GetSetrest_day = rest_day;
                    modItem.GetSetstatus = status;
                    modItem.GetSetmodifiedby = sUserId;
                    String result = oHRCon.updateCompWorkingGroup(modItem);
                    if (result.Equals("Y"))
                    {
                        modItem = oHRCon.getCompWorkingGroupDetails(currcomp, "", "", id);
                        if (modItem.GetSetid != 0)
                        {
                            result = oHRCon.updateCompWorkingDayBulk(modItem);
                            //result = oHRCon.deleteCompWorkingDayBulk(modItem);
                            result = oHRCon.insertCompWorkingDayBulk(modItem);
                        }
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table Working Group...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + currcomp + " & Tarikh: " + fromdate + " & Nama/ Keterangan: " + desc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteWGItemDetail(string currcomp, int id)
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

        if (currcomp.Length > 0 && id > 0)
        {
            HRModel modItem = oHRCon.getCompWorkingGroupDetails(currcomp, "", "", id);
            if (modItem.GetSetid > 0)
            {
                String result = oHRCon.deleteCompWorkingGroup(modItem);
                if (result.Equals("Y"))
                {
                    result = oHRCon.deleteCompWorkingDayBulk(modItem);
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Working Group...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Hapus tidak berjaya! Record not found for Comp: " + currcomp + " & Id: " + id;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}