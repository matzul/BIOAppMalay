using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HumanResource_AttendanceExcuseStaff : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sStaffName = "";
    public String sExcCat = "";
    public String sExcType = "";
    public String sFromDate = "";
    public String sToDate = "";
    public String sExcId = "";
    public String sAction = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsExcuseCategory = new ArrayList();
    public ArrayList lsExcuseStaffAttendance = new ArrayList();

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

        lsExcuseStaffAttendance = oHRCon.getStaffExcuseList(sCurrComp, sCurrFyr, sStaffNo, sExcCat, sExcType, "", "", "", Server.MapPath("../Attachment/HumanResource/"));
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

        if (Request.Params.Get("txtFindStaffNo") != null)
        {
            sStaffNo = oHRCon.replaceNull(Request.Params.Get("txtFindStaffNo"));
        }
        if (Request.Params.Get("txtFindStaffName") != null)
        {
            sStaffName = oHRCon.replaceNull(Request.Params.Get("txtFindStaffName"));
        }
        if (Request.Params.Get("lsFindExcCat") != null)
        {
            sExcCat = oHRCon.replaceNull(Request.Params.Get("lsFindExcCat"));
        }
        if (Request.Params.Get("lsFindExcType") != null)
        {
            sExcType = oHRCon.replaceNull(Request.Params.Get("lsFindExcType"));
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
    public static String getStaffEmployList(string currcomp, string currfyr)
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
            ArrayList lsItem = oHRCon.getStaffEmployList(currcomp, "", "", "", "", "");
            for (int i = 0; i < lsItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsItem[i];

                Object objData = new
                {
                    GetSetstaffno = oHRMod.GetSetstaffno,
                    GetSetname = oHRMod.GetSetname,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSetdept_id = oHRMod.GetSetdept_id,
                    GetSetdept_name = oHRMod.GetSetdept_name
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
    public static String getStaffEmployDetails(string currcomp, string staffno)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oModStaffEmploy = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0)
        {
            oModStaffEmploy = oHRCon.getStaffEmployDetails(currcomp, staffno);
            if (oModStaffEmploy.GetSetid > 0)
            {
                sStatus = "Y";
            }
        }

        object retData = new { result = sStatus, staffemploy = oModStaffEmploy };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getExcuseTypeList(string currcomp, string currfyr, string exccategory, string exctype)
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

        if (currcomp.Length > 0 && exccategory.Length > 0)
        {
            /*
            ArrayList lsItem = oHRCon.getExcuseTypeList(currcomp, currfyr, exccategory, exctype, "");
            for (int i = 0; i < lsItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsItem[i];

                Object objData = new
                {
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                };
                lsItemOutput.Add(objData);
            }
            */
            if (exccategory.Equals("PENGECUALIAN"))
            {
                Object objData1 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "KURSUS",
                };
                lsItemOutput.Add(objData1);
                Object objData2 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "BALIK AWAL",
                };
                lsItemOutput.Add(objData2);
                Object objData3 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "MASUK LAMBAT",
                };
                lsItemOutput.Add(objData3);
                Object objData4 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "KERJA LUAR",
                };
                lsItemOutput.Add(objData4);
                Object objData5 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "BEKERJA DARI RUMAH",
                };
                lsItemOutput.Add(objData5);
            }
            else if (exccategory.Equals("TIDAK DAFTAR KEHADIRAN"))
            {
                Object objData1 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "MASUK SAHAJA",
                };
                lsItemOutput.Add(objData1);
                Object objData2 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "KELUAR SAHAJA",
                };
                lsItemOutput.Add(objData2);
                Object objData3 = new
                {
                    GetSetcomp = currcomp,
                    GetSetfyr = currfyr,
                    GetSetcat = exccategory,
                    GetSettype = "MASUK & KELUAR",
                };
                lsItemOutput.Add(objData3);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemlist = lsItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String getStaffExcuseDetails(string currcomp, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        HRModel oModStaffExcuse = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oModStaffExcuse = oHRCon.getStaffExcuseDetails(currcomp, "", "", "", "", "", "", id, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
            if (oModStaffExcuse.GetSetid > 0)
            {
                sStatus = "Y";
                sMessage = "";
            }
        }

        object retData = new { result = sStatus, itemdetail = oModStaffExcuse, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String removeAttachedStaffExcuse(string currcomp, int id, string filenumber, string filename)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";
        String sFileName1 = "";
        String sFileName2 = "";
        String sFileName3 = "";
        String sMessage = "Internal Server Error!";

        HRModel oModStaffExcuse = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oModStaffExcuse = oHRCon.getStaffExcuseDetails(currcomp, "", "", "", "", "", "", id, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
            if (oModStaffExcuse.GetSetid > 0)
            {
                if (filenumber.Equals("1"))
                {
                    sFileName1 = "";
                    sFileName2 = oModStaffExcuse.GetSetfilename2;
                    sFileName3 = oModStaffExcuse.GetSetfilename3;

                    //To delete file from the folder
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename1, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                    }
                }
                else if (filenumber.Equals("2"))
                {
                    sFileName1 = oModStaffExcuse.GetSetfilename1;
                    sFileName2 = "";
                    sFileName3 = oModStaffExcuse.GetSetfilename3;

                    //To delete file from the folder
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename2, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                    }
                }
                else if (filenumber.Equals("3"))
                {
                    sFileName1 = oModStaffExcuse.GetSetfilename1;
                    sFileName2 = oModStaffExcuse.GetSetfilename2;
                    sFileName3 = "";

                    //To delete file from the folder
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename3, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                    }
                }
                sStatus = oHRCon.updateStaffExcuseAttachment(oModStaffExcuse, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"), sFileName1, sFileName2, sFileName3);
                sMessage = "";
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteStaffExcuse(string currcomp, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        HRModel oModStaffExcuse = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oModStaffExcuse = oHRCon.getStaffExcuseDetails(currcomp, "", "", "", "", "", "", id, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
            if (oModStaffExcuse.GetSetid > 0)
            {
                sStatus = oHRCon.deleteStaffExcuseDetails(oModStaffExcuse);
                sMessage = "";

                //delete from staff_exception_day
                sStatus = oHRCon.deleteStaffExceptionDetails(oModStaffExcuse, "EXCUSE");

                //To delete file from the folder
                ArrayList lsFileName = new ArrayList();
                lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename1, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                for (int j = 0; j < lsFileName.Count; j++)
                {
                    String sFileNameAndFolder = (String)lsFileName[j];
                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                    filetodelete.Delete();
                }

                //To delete file from the folder
                lsFileName = new ArrayList();
                lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename2, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                for (int j = 0; j < lsFileName.Count; j++)
                {
                    String sFileNameAndFolder = (String)lsFileName[j];
                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                    filetodelete.Delete();
                }

                //To delete file from the folder
                lsFileName = new ArrayList();
                lsFileName = oHRCon.getFileAttached(oModStaffExcuse.GetSetfilename3, HttpContext.Current.Server.MapPath("../Attachment/HumanResource/"));
                for (int j = 0; j < lsFileName.Count; j++)
                {
                    String sFileNameAndFolder = (String)lsFileName[j];
                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                    filetodelete.Delete();
                }
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

}