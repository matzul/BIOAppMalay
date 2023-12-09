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

public partial class HumanResource_SalaryRunBAK : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sAction = "";

    public String sCat = "";
    public String sType = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsRunSalary = new ArrayList();
    public ArrayList lsGredComp = new ArrayList();
    public ArrayList lsSalaryItem = new ArrayList();

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
        lsGredComp = oHRCon.getCompGredList(sCurrComp, "", "");
        lsRunSalary = oHRCon.getRunSalaryList(sCurrComp, sCurrFyr, sCat, sType);
        lsSalaryItem = oHRCon.getCompSalaryItemList(sCurrComp, "", "", "", "");
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

        if (Request.Params.Get("txtFindItemCat") != null)
        {
            sCat = oHRCon.replaceNull(Request.Params.Get("txtFindItemCat"));
        }
        if (Request.Params.Get("txtFindItemType") != null)
        {
            sType = oHRCon.replaceNull(Request.Params.Get("txtFindItemType"));
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
    public static String getSGItemList(string currcomp, string currfyr)
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
            ArrayList lsItem = oHRCon.getRunSalaryList(currcomp, currfyr, "", "");
            for (int i = 0; i < lsItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsItem[i];

                Object objData = new
                {
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype
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
    public static String getSGItemDetail(string currcomp, int id)
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
            oHRMod = oHRCon.getRunSalaryDetails(currcomp, "", "", "", id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oHRMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteSGItemDetail(string currcomp, string currfyr, int id)
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
            HRModel modItem = oHRCon.getRunSalaryDetails(currcomp, currfyr, "", "", id);
            if (modItem.GetSetid > 0)
            {
                String result = oHRCon.deleteRunSalary(modItem);
                if (result.Equals("Y"))
                {
                    result = oHRCon.deleteRunSalaryStaff(modItem.GetSetcomp, modItem.GetSetfyr, "", modItem.GetSetid, 0);
                    result = oHRCon.deleteRunSalaryStaffItem(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetid, 0);
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Salary Staff ...";
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

    [WebMethod(EnableSession = true)]
    public static String insertSGItemDetail(string currcomp, string currfyr, string run_cat, string run_type, int run_count, int run_month, int run_year, HRModel[] inputarray)
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

        if (currcomp.Length > 0 && currfyr.Length > 0 && run_cat.Length > 0 && run_type.Length > 0 && run_month > 0 && run_year > 0)
        {
            HRModel modItem = new HRModel();
            modItem.GetSetcomp = currcomp;
            modItem.GetSetfyr = currfyr;
            modItem.GetSetcat = run_cat;
            modItem.GetSettype = run_type;
            modItem.GetSetcount = run_count;
            modItem.GetSetmonth = run_month;
            modItem.GetSetyear = run_year;
            modItem.GetSetstatus = "CREATED";
            modItem.GetSetcreatedby = sUserId;
            String result = oHRCon.insertRunSalary(modItem);
            if (result.Equals("Y"))
            {
                modItem = oHRCon.getRunSalaryDetails(currcomp, currfyr, run_cat, run_type, 0, run_month, run_year, "CREATED");
                if(modItem.GetSetid > 0)
                {

                    //delete everything from table salary_group_item
                    String deleteFlag = oHRCon.deleteRunSalaryStaff(currcomp, currfyr, "", modItem.GetSetid, 0);
                    for (int i = 0; i < inputarray.Length; i++)
                    {
                        HRModel modInput = (HRModel)inputarray[i];
                        modItem.GetSetstaffno = modInput.GetSetstaffno;
                        modItem.GetSetss_id = modItem.GetSetid;
                        modItem.GetSetsg_id = modInput.GetSetsg_id;
                        modItem.GetSetssg_id = modInput.GetSetssg_id;
                        modItem.GetSetcat = modInput.GetSetcat;
                        modItem.GetSettype = modInput.GetSettype;
                        if (modItem.GetSetss_id > 0 && modItem.GetSetsg_id > 0 && modItem.GetSetssg_id > 0)
                        {
                            result = oHRCon.insertRunSalaryStaff(modItem);
                            if (result.Equals("Y"))
                            {
                                HRModel modItemGaji = oHRCon.getRunSalaryStaffDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, modItem.GetSetcat, modItem.GetSettype, modItem.GetSetsg_id, modItem.GetSetss_id, modItem.GetSetssg_id, 0);
                                if (modItemGaji.GetSetid > 0)
                                {
                                    result = oHRCon.insertRunSalaryStaffItem(modItemGaji.GetSetcomp, modItemGaji.GetSetfyr, modItemGaji.GetSetstaffno, modItemGaji.GetSetssg_id, modItemGaji.GetSetss_id, modItemGaji.GetSetid);
                                    sStatus = "Y";
                                    sMessage = "Tambah berjaya!";
                                }
                                else
                                {
                                    sStatus = "N";
                                    sMessage = "Tambah tidak berjaya! Error on inserting table Salary Staff Details Item...";
                                }
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Tambah tidak berjaya! Error on inserting table Salary Staff Details...";
                            }
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + currcomp + " & Tahun: " + modItem.GetSetfyr + " & Salary Id: " + modItem.GetSetss_id;
                        }
                    }
                }
            }

        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }


    [WebMethod(EnableSession = true)]
    public static String getSGStaffGajiList(string currcomp, string currfyr, Int64 sg_id, String salary_cat, String salary_type)
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
            ArrayList lsSGItem = oHRCon.getStaffSalaryGroupList(currcomp, currfyr, "", sg_id, "", salary_cat, salary_type, "");
            for (int i = 0; i < lsSGItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsSGItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetstaffno = oHRMod.GetSetstaffno,
                    GetSetname = oHRMod.GetSetname,
                    GetSetdept_id = oHRMod.GetSetdept_id,
                    GetSetdept_name = oHRMod.GetSetdept_name,
                    GetSetgred_id = oHRMod.GetSetgred_id,
                    GetSetgred_name = oHRMod.GetSetgred_name,
                    GetSetpos_id = oHRMod.GetSetpos_id,
                    GetSetpos_name = oHRMod.GetSetpos_name,
                    GetSetsg_id = oHRMod.GetSetsg_id,
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetfromdate = oHRMod.GetSetfromdate,
                    GetSettodate = oHRMod.GetSettodate,
                    GetSetstatus = oHRMod.GetSetstatus,
                    GetSetremarks = oHRMod.GetSetremarks
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
    public static String getSGItemGajiChecked(string currcomp, string currfyr, string staffno, Int64 sg_id)
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
            ArrayList lsFisItem = oHRCon.getRunStaffSalaryItemList(currcomp, currfyr, staffno, sg_id, 0, 0, 0, "", "","");
            for (int i = 0; i < lsFisItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsFisItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetstaffno = oHRMod.GetSetstaffno,
                    GetSetsg_id = oHRMod.GetSetsg_id,
                    GetSetssg_id = oHRMod.GetSetssg_id,
                    GetSetss_id = oHRMod.GetSetss_id,
                    GetSetssd_id = oHRMod.GetSetssd_id,
                    GetSetcode = oHRMod.GetSetcode,
                    GetSetdesc = oHRMod.GetSetdesc,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetitemvalue = oHRMod.GetSetitemvalue,
                    GetSetitemgroup = oHRMod.GetSetitemgroup
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
    public static String getSGStaffGajiChecked(string currcomp, string currfyr, Int64 ss_id)
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
            ArrayList lsItem = oHRCon.getRunSalaryStaffList(currcomp, currfyr, "", "", "", 0, ss_id, 0);
            for (int i = 0; i < lsItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsItem[i];

                Object objData = new
                {
                    GetSetid = oHRMod.GetSetid,
                    GetSetcomp = oHRMod.GetSetcomp,
                    GetSetfyr = oHRMod.GetSetfyr,
                    GetSetstaffno = oHRMod.GetSetstaffno,
                    GetSetname = oHRMod.GetSetname,
                    GetSetss_id = oHRMod.GetSetss_id,
                    GetSetsg_id = oHRMod.GetSetsg_id,
                    GetSetssg_id = oHRMod.GetSetssg_id,
                    GetSetcat = oHRMod.GetSetcat,
                    GetSettype = oHRMod.GetSettype,
                    GetSetcount = oHRMod.GetSetcount,
                    GetSetmonth = oHRMod.GetSetmonth,
                    GetSetyear = oHRMod.GetSetyear,
                    GetSetstatus = oHRMod.GetSetstatus
                };
                lsItemOutput.Add(objData);
            }
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemlist = lsItemOutput };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }
}