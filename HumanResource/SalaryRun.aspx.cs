using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HumanResource_SalaryRun : System.Web.UI.Page
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
    public static String updateSGItemDetail(string currcomp, string currfyr, Int64 ss_id, int run_month, string status, HRModel[] inputarray)
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

        if (currcomp.Length > 0 && ss_id > 0)
        {
            HRModel modItem = oHRCon.getRunSalaryDetails(currcomp, "", "", "", ss_id);
            if (modItem.GetSetid > 0)
            {
                String notin = "";
                for (int i = 0; i < inputarray.Length; i++)
                {
                    HRModel modInput = (HRModel)inputarray[i];
                    modItem.GetSetstaffno = modInput.GetSetstaffno;
                    modItem.GetSetss_id = modItem.GetSetid;
                    modItem.GetSetsg_id = modInput.GetSetsg_id;
                    modItem.GetSetssg_id = modInput.GetSetssg_id;
                    modItem.GetSetcat = modInput.GetSetcat;
                    modItem.GetSettype = modInput.GetSettype;
                    HRModel modCheck = oHRCon.getRunSalaryStaffDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, "", "", modItem.GetSetsg_id, modItem.GetSetss_id, modItem.GetSetssg_id, 0);
                    if (modCheck.GetSetss_id > 0 && modCheck.GetSetsg_id > 0 && modCheck.GetSetssg_id > 0)
                    {
                        //skip - don't do anything!!!
                        sStatus = "Y";
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        String result = oHRCon.insertRunSalaryStaff(modItem);
                        if (result.Equals("Y"))
                        {
                            HRModel modItemGaji = oHRCon.getRunSalaryStaffDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, modItem.GetSetcat, modItem.GetSettype, modItem.GetSetsg_id, modItem.GetSetss_id, modItem.GetSetssg_id, 0);
                            if (modItemGaji.GetSetid > 0)
                            {
                                result = oHRCon.insertRunSalaryStaffItem(modItemGaji.GetSetcomp, modItemGaji.GetSetfyr, modItemGaji.GetSetstaffno, modItemGaji.GetSetssg_id, modItemGaji.GetSetss_id, modItemGaji.GetSetid);
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
                        else
                        {
                            sStatus = "N";
                            sMessage = "Kemaskini tidak berjaya! Error on inserting table Salary Staff Details...";
                            break;
                        }
                    }
                    if (notin.Length > 0)
                    {
                        notin = notin + ",'" + modItem.GetSetstaffno + "'";
                    }
                    else
                    {
                        notin = "'" + modItem.GetSetstaffno + "'";
                    }
                }
                if (sStatus.Equals("Y"))
                {
                    //delete for those not in
                    ArrayList lsItem = oHRCon.getRunSalaryStaffList(currcomp, currfyr, "", "", "", 0, ss_id, 0, notin);
                    for (int i = 0; i < lsItem.Count; i++)
                    {
                        HRModel oHRMod = (HRModel)lsItem[i];
                        String result = oHRCon.deleteRunSalaryStaff(oHRMod.GetSetcomp, oHRMod.GetSetfyr, oHRMod.GetSetstaffno, oHRMod.GetSetss_id, oHRMod.GetSetssg_id);
                        if (result.Equals("Y"))
                        {
                            result = oHRCon.deleteRunSalaryStaffItem(oHRMod.GetSetcomp, oHRMod.GetSetfyr, oHRMod.GetSetss_id, oHRMod.GetSetid);
                        }
                    }
                }
                //update Run Salary Details (month & status only)
                if(run_month > 0)
                {
                    modItem.GetSetmonth = run_month;
                }
                if (status.Length > 0)
                {
                    modItem.GetSetstatus = status;
                }

                if (modItem.GetSetstatus.Equals("MODIFIED"))
                {
                    modItem.GetSetmodifiedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("VERIFIED"))
                {
                    modItem.GetSetverifiedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CONFIRMED"))
                {
                    modItem.GetSetconfirmedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("APPROVED"))
                {
                    modItem.GetSetapprovedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("REJECTED"))
                {
                    modItem.GetSetrejectedby = sUserId;
                }
                else if (modItem.GetSetstatus.Equals("CANCELLED"))
                {
                    modItem.GetSetcancelledby = sUserId;
                }
                sStatus = oHRCon.updateRunSalary(modItem);
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! Please contact System Administrator!";

            }
        }

        object retData = new { result = sStatus, message = sMessage };

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
                    if (result.Equals("Y")) {
                        result = oHRCon.deleteRunSalaryStaffItem(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetid, 0);
                    }
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
                if (modItem.GetSetid > 0)
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

    [WebMethod(EnableSession = true)]
    public static String processSGStaffGaji(String currcomp, String currfyr, Int64 ss_id)
    {
        HRController oHRCon = new HRController();
        MainController oMainCon = new MainController();

        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        String jsonResponse = "";

        if (currcomp.Length > 0 && ss_id > 0)
        {
            MainModel modCompInfo = oMainCon.getCompInfoDetails(currcomp);

            HRModel modItem = oHRCon.getRunSalaryDetails(currcomp, currfyr, "", "", ss_id);
            if (modItem.GetSetid > 0)
            {
                //process Run Gaji
                ArrayList lsItem = oHRCon.getRunSalaryStaffList(currcomp, currfyr, "", "", "", 0, ss_id, 0);
                for (int i = 0; i < lsItem.Count; i++)
                {
                    HRModel oHRModStaff = (HRModel)lsItem[i];

                    ArrayList lsSalaryItem = oHRCon.getRunStaffSalaryItemList(currcomp, currfyr, oHRModStaff.GetSetstaffno, 0, 0, oHRModStaff.GetSetss_id, oHRModStaff.GetSetid, "", "", "");

                    if (lsSalaryItem.Count > 0)
                    {
                        //generate PDF File for Gaji
                        double jumlahgajibersih = 0;
                        double jumlahgajiaddition = 0;
                        double jumlahgajideduction = 0;

                        // Create a MigraDoc document
                        Document doc = new Document();
                        doc.Info.Title = "PENYATA GAJI";
                        doc.Info.Subject = "PENYATA GAJI BULAN:" + oHRModStaff.GetSetmonth + " TAHUN:" + oHRModStaff.GetSetyear;
                        doc.Info.Author = "B.I.O.App System";

                        //set page orientation
                        doc.DefaultPageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;
                        doc.DefaultPageSetup.TopMargin = "7.5cm"; //120
                        doc.DefaultPageSetup.BottomMargin = "8.5cm"; //150
                        doc.DefaultPageSetup.LeftMargin = 40;
                        doc.DefaultPageSetup.RightMargin = 40;
                        doc.DefaultPageSetup.HeaderDistance = "1.5cm"; //20
                        doc.DefaultPageSetup.FooterDistance = "5.5cm"; //20

                        MigraDoc.DocumentObjectModel.Style style = doc.Styles["Normal"];
                        // Because all styles are derived from Normal, the next line changes the 
                        // font of the whole document. Or, more exactly, it changes the font of
                        // all styles and paragraphs that do not redefine the font.
                        style.Font.Name = "Verdana";

                        // Create a new style called Table based on style Normal
                        style = doc.Styles.AddStyle("Table", "Normal");
                        style.Font.Name = "Verdana";
                        style.Font.Name = "Arial";
                        style.Font.Size = 8;

                        // Each MigraDoc document needs at least one section.
                        Section section = doc.AddSection();

                        // Put Capital Digital logo in the header
                        /*
                        string logo_lima = Server.MapPath("~/images/logo_CD_bgBLUE_BLACK_310x98.png");
                        MigraDoc.DocumentObjectModel.Shapes.Image image = section.Headers.Primary.AddImage(logo_lima);
                        image.Height = "1cm";
                        image.LockAspectRatio = true;
                        image.RelativeVertical = RelativeVertical.Line;
                        image.RelativeHorizontal = RelativeHorizontal.Margin;
                        image.Top = ShapePosition.Top;
                        image.Left = ShapePosition.Right;
                        image.WrapFormat.Style = WrapStyle.Through;
                        */
                        // Put CD logo in the header
                        /*
                        string logo_mod = Server.MapPath("~/images/logo_versi_4.png");
                        MigraDoc.DocumentObjectModel.Shapes.Image image2 = section.Headers.Primary.AddImage(logo_mod);
                        image2.Height = "1.5cm";
                        image2.LockAspectRatio = true;
                        image2.RelativeVertical = RelativeVertical.Line;
                        image2.RelativeHorizontal = RelativeHorizontal.Margin;
                        image2.Top = ShapePosition.Top;
                        image2.Left = ShapePosition.Left;
                        image2.WrapFormat.Style = WrapStyle.Through;
                        */

                        // Create Header

                        Paragraph header2 = section.Headers.Primary.AddParagraph();
                        header2.AddText(modCompInfo.GetSetcomp_name);
                        header2.Format.Font.Size = 8;
                        header2.Format.Font.Bold = true;
                        header2.Format.Alignment = ParagraphAlignment.Center;

                        Paragraph header3 = section.Headers.Primary.AddParagraph();
                        header3.AddText(modCompInfo.GetSetcomp_address);
                        header3.Format.Font.Size = 7;
                        header3.Format.Font.Bold = false;
                        header3.Format.Alignment = ParagraphAlignment.Center;

                        header3 = section.Headers.Primary.AddParagraph();
                        header3.AddText("website: " + modCompInfo.GetSetcomp_website);
                        header3.Format.Font.Size = 7;
                        header3.Format.Font.Bold = false;
                        header3.Format.Alignment = ParagraphAlignment.Center;

                        header3 = section.Headers.Primary.AddParagraph();
                        header3.AddText("Tel: " + modCompInfo.GetSetcomp_contactno);
                        header3.Format.Font.Size = 7;
                        header3.Format.Font.Bold = false;
                        header3.Format.Alignment = ParagraphAlignment.Center;

                        Paragraph header = section.Headers.Primary.AddParagraph();
                        header.AddText("");
                        header.Format.Font.Size = 12;
                        header.Format.Font.Bold = true;
                        header.Format.Alignment = ParagraphAlignment.Center;

                        header = section.Headers.Primary.AddParagraph();
                        header.AddText("SLIP BAYARAN GAJI/UPAH/IMBUHAN/BONUS/LAIN-LAIN");
                        header.Format.Font.Size = 12;
                        header.Format.Font.Bold = true;
                        header.Format.Alignment = ParagraphAlignment.Center;


                        // Create main section for Sales Order 
                        //Paragraph main = section.AddParagraph();
                        // main = section.AddParagraph();
                        //main.Format.SpaceBefore = 1;

                        // Create the item table for header
                        //MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.AddTable();
                        MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.Headers.Primary.AddTable();
                        tableTop.Style = "Table";
                        tableTop.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
                        tableTop.Borders.Width = 0.25;
                        tableTop.Borders.Left.Width = 0.5;
                        tableTop.Borders.Right.Width = 0.5;
                        tableTop.Rows.LeftIndent = 0;

                        // Before you can add a row, you must define the columns
                        Column columnTop = tableTop.AddColumn("2cm");
                        columnTop.Format.Alignment = ParagraphAlignment.Left;
                        columnTop = tableTop.AddColumn("7cm");
                        columnTop.Format.Alignment = ParagraphAlignment.Left;
                        columnTop = tableTop.AddColumn("2cm");
                        columnTop.Format.Alignment = ParagraphAlignment.Left;
                        columnTop = tableTop.AddColumn("2cm");
                        columnTop.Format.Alignment = ParagraphAlignment.Left;
                        columnTop = tableTop.AddColumn("5cm");
                        columnTop.Format.Alignment = ParagraphAlignment.Left;

                        Row rowTop = tableTop.AddRow();
                        rowTop.Borders.Left.Visible = false;
                        rowTop.Borders.Right.Visible = false;
                        rowTop.Borders.Top.Visible = false;
                        rowTop.Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Borders.Left.Visible = false;
                        rowTop.Borders.Right.Visible = false;
                        rowTop.Borders.Top.Visible = false;
                        rowTop.Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Borders.Left.Visible = false;
                        rowTop.Borders.Right.Visible = false;
                        rowTop.Borders.Top.Visible = false;
                        rowTop.Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Borders.Left.Visible = false;
                        rowTop.Borders.Right.Visible = false;
                        rowTop.Borders.Top.Visible = false;
                        rowTop.Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Cells[0].AddParagraph("Bayar Kepada: ");
                        rowTop.Cells[0].Borders.Left.Visible = false;
                        rowTop.Cells[0].Borders.Right.Visible = false;
                        rowTop.Cells[0].Borders.Bottom.Visible = false;
                        rowTop.Cells[1].AddParagraph(oHRModStaff.GetSetname);
                        rowTop.Cells[1].Borders.Left.Visible = false;
                        rowTop.Cells[1].Borders.Right.Visible = false;
                        rowTop.Cells[1].Borders.Bottom.Visible = false;
                        rowTop.Cells[2].AddParagraph();
                        rowTop.Cells[2].Borders.Left.Visible = false;
                        rowTop.Cells[2].Borders.Right.Visible = false;
                        rowTop.Cells[2].Borders.Bottom.Visible = false;
                        rowTop.Cells[3].AddParagraph("No. Pekerja: ");
                        rowTop.Cells[3].Borders.Left.Visible = false;
                        rowTop.Cells[3].Borders.Right.Visible = false;
                        rowTop.Cells[3].Borders.Bottom.Visible = false;
                        rowTop.Cells[4].AddParagraph(oHRModStaff.GetSetstaffno);
                        rowTop.Cells[4].Borders.Left.Visible = false;
                        rowTop.Cells[4].Borders.Right.Visible = false;
                        rowTop.Cells[4].Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Cells[0].AddParagraph();
                        rowTop.Cells[0].Borders.Left.Visible = false;
                        rowTop.Cells[0].Borders.Right.Visible = false;
                        rowTop.Cells[0].Borders.Bottom.Visible = false;
                        rowTop.Cells[0].MergeDown = 2;
                        rowTop.Cells[1].AddParagraph(oHRModStaff.GetSetdept_name);
                        rowTop.Cells[1].AddParagraph(oHRModStaff.GetSetgred_name);
                        rowTop.Cells[1].Borders.Left.Visible = false;
                        rowTop.Cells[1].Borders.Right.Visible = false;
                        rowTop.Cells[1].Borders.Bottom.Visible = false;
                        rowTop.Cells[1].MergeDown = 2;
                        rowTop.Cells[2].AddParagraph();
                        rowTop.Cells[2].Borders.Left.Visible = false;
                        rowTop.Cells[2].Borders.Right.Visible = false;
                        rowTop.Cells[2].Borders.Bottom.Visible = false;
                        rowTop.Cells[2].MergeDown = 2;
                        rowTop.Cells[3].AddParagraph("Tarikh: ");
                        rowTop.Cells[3].Borders.Left.Visible = false;
                        rowTop.Cells[3].Borders.Right.Visible = false;
                        rowTop.Cells[3].Borders.Bottom.Visible = false;
                        rowTop.Cells[4].AddParagraph(oHRModStaff.GetSetrundate);
                        rowTop.Cells[4].Borders.Left.Visible = false;
                        rowTop.Cells[4].Borders.Right.Visible = false;
                        rowTop.Cells[4].Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Cells[3].AddParagraph("Status: ");
                        rowTop.Cells[3].Borders.Left.Visible = false;
                        rowTop.Cells[3].Borders.Right.Visible = false;
                        rowTop.Cells[3].Borders.Bottom.Visible = false;
                        rowTop.Cells[4].AddParagraph(oHRModStaff.GetSetstatus);
                        rowTop.Cells[4].Borders.Left.Visible = false;
                        rowTop.Cells[4].Borders.Right.Visible = false;
                        rowTop.Cells[4].Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Cells[0].AddParagraph("Rujukan: ");
                        rowTop.Cells[0].Borders.Left.Visible = false;
                        rowTop.Cells[0].Borders.Right.Visible = false;
                        rowTop.Cells[0].Borders.Bottom.Visible = false;
                        rowTop.Cells[1].AddParagraph(oHRModStaff.GetSetremarks);
                        rowTop.Cells[1].Borders.Left.Visible = false;
                        rowTop.Cells[1].Borders.Right.Visible = false;
                        rowTop.Cells[1].Borders.Bottom.Visible = false;
                        rowTop.Cells[2].AddParagraph();
                        rowTop.Cells[2].Borders.Left.Visible = false;
                        rowTop.Cells[2].Borders.Right.Visible = false;
                        rowTop.Cells[2].Borders.Bottom.Visible = false;
                        rowTop.Cells[3].AddParagraph();
                        rowTop.Cells[3].Borders.Left.Visible = false;
                        rowTop.Cells[3].Borders.Right.Visible = false;
                        rowTop.Cells[3].Borders.Bottom.Visible = false;
                        rowTop.Cells[4].AddParagraph();
                        rowTop.Cells[4].Borders.Left.Visible = false;
                        rowTop.Cells[4].Borders.Right.Visible = false;
                        rowTop.Cells[4].Borders.Bottom.Visible = false;

                        rowTop = tableTop.AddRow();
                        rowTop.Cells[0].AddParagraph("Jawatan: ");
                        rowTop.Cells[0].Borders.Left.Visible = false;
                        rowTop.Cells[0].Borders.Right.Visible = false;
                        rowTop.Cells[0].Borders.Bottom.Visible = false;
                        rowTop.Cells[1].AddParagraph(oHRModStaff.GetSetgred_name);
                        rowTop.Cells[1].Borders.Left.Visible = false;
                        rowTop.Cells[1].Borders.Right.Visible = false;
                        rowTop.Cells[1].Borders.Bottom.Visible = false;
                        rowTop.Cells[2].AddParagraph();
                        rowTop.Cells[2].Borders.Left.Visible = false;
                        rowTop.Cells[2].Borders.Right.Visible = false;
                        rowTop.Cells[2].Borders.Bottom.Visible = false;
                        rowTop.Cells[3].AddParagraph("Kategori:");
                        rowTop.Cells[3].Borders.Left.Visible = false;
                        rowTop.Cells[3].Borders.Right.Visible = false;
                        rowTop.Cells[3].Borders.Bottom.Visible = false;
                        rowTop.Cells[4].AddParagraph(oHRModStaff.GetSettype);
                        rowTop.Cells[4].Borders.Left.Visible = false;
                        rowTop.Cells[4].Borders.Right.Visible = false;
                        rowTop.Cells[4].Borders.Bottom.Visible = false;

                        // Create the item table
                        MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
                        table.Style = "Table";
                        table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
                        table.Borders.Width = 0.25;
                        table.Borders.Left.Width = 0.5;
                        table.Borders.Right.Width = 0.5;
                        table.Rows.LeftIndent = 0;

                        // Before you can add a row, you must define the columns
                        Column column = table.AddColumn("2cm");
                        column.Format.Alignment = ParagraphAlignment.Center;

                        column = table.AddColumn("5cm");
                        column.Format.Alignment = ParagraphAlignment.Right;

                        column = table.AddColumn("2cm");
                        column.Format.Alignment = ParagraphAlignment.Right;

                        column = table.AddColumn("2cm");
                        column.Format.Alignment = ParagraphAlignment.Right;

                        column = table.AddColumn("5cm");
                        column.Format.Alignment = ParagraphAlignment.Right;

                        column = table.AddColumn("2cm");
                        column.Format.Alignment = ParagraphAlignment.Right;

                        // Create the header of the table
                        Row row = table.AddRow();
                        row.HeadingFormat = true;
                        row.Format.Alignment = ParagraphAlignment.Center;
                        row.Format.Font.Bold = true;
                        row.Height = "1cm";
                        row.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
                        row.Cells[0].AddParagraph("PENDAPATAN (RM)");
                        row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                        row.Cells[0].MergeRight = 2;
                        row.Cells[3].AddParagraph("POTONGAN (RM)");
                        row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                        row.Cells[3].MergeRight = 2;

                        ArrayList lsIncome = new ArrayList();
                        ArrayList lsExpense = new ArrayList();
                        ArrayList lsEmployer = new ArrayList();
                        for (int j = 0; j < lsSalaryItem.Count; j++)
                        {
                            HRModel modSalaryDet = (HRModel)lsSalaryItem[j];

                            if (modSalaryDet.GetSetcat.Equals("SALARY") || modSalaryDet.GetSetcat.Equals("ADDITION"))
                            {
                                lsIncome.Add(modSalaryDet);
                                jumlahgajiaddition = jumlahgajiaddition + modSalaryDet.GetSetitemamount;
                            }
                            else if (modSalaryDet.GetSetcat.Equals("DEDUCTION"))
                            {
                                lsExpense.Add(modSalaryDet);
                                jumlahgajideduction = jumlahgajideduction + modSalaryDet.GetSetitemamount;
                            }
                            else if (modSalaryDet.GetSetcat.Equals("EMPLOYEE"))
                            {
                                lsEmployer.Add(modSalaryDet);
                            }

                            if (modSalaryDet.GetSetcat.Equals("DEDUCTION"))
                            {
                                jumlahgajibersih = jumlahgajibersih - modSalaryDet.GetSetitemamount;
                            }
                            else if (modSalaryDet.GetSetcat.Equals("EMPLOYEE"))
                            {
                                //do nothing
                                jumlahgajibersih = jumlahgajibersih;
                            }
                            else
                            {
                                jumlahgajibersih = jumlahgajibersih + modSalaryDet.GetSetitemamount;
                            }
                        }
                        int maxrow = lsIncome.Count;
                        if (maxrow < lsExpense.Count)
                        {
                            maxrow = lsExpense.Count;
                        }
                        for (int j = 0; j < maxrow; j++)
                        {
                            HRModel modIncome = new HRModel();
                            HRModel modExpense = new HRModel();
                            if (j < lsIncome.Count)
                            {
                                modIncome = (HRModel)lsIncome[j];
                            }
                            if (j < lsExpense.Count)
                            {
                                modExpense = (HRModel)lsExpense[j];
                            }
                            // Each item fills two rows
                            Row row1 = table.AddRow();
                            row1.Height = "1cm";
                            row1.TopPadding = 1.5;
                            //row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                            row1.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                            row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                            row1.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                            row1.Cells[3].Format.Alignment = ParagraphAlignment.Left;
                            row1.Cells[4].Format.Alignment = ParagraphAlignment.Left;
                            row1.Cells[5].Format.Alignment = ParagraphAlignment.Right;

                            row1.Cells[0].AddParagraph(modIncome.GetSetcode);
                            row1.Cells[1].AddParagraph(modIncome.GetSetdesc);
                            if (modIncome.GetSetitemamount > 0)
                            {
                                row1.Cells[2].AddParagraph(modIncome.GetSetitemamount.ToString("#,##0.00"));
                            }
                            else
                            {
                                row1.Cells[2].AddParagraph("");
                            }
                            row1.Cells[3].AddParagraph(modExpense.GetSetcode);
                            row1.Cells[4].AddParagraph(modExpense.GetSetdesc);
                            if (modExpense.GetSetitemamount > 0)
                            {
                                row1.Cells[5].AddParagraph(modExpense.GetSetitemamount.ToString("#,##0.00"));
                            }
                            else
                            {
                                row1.Cells[5].AddParagraph("");
                            }

                            if (j > 0 && ((j + 1) % 10) == 0)
                            {
                                row1.Cells[0].Borders.Bottom.Visible = true;
                                row1.Cells[1].Borders.Bottom.Visible = true;
                                row1.Cells[2].Borders.Bottom.Visible = true;
                                row1.Cells[3].Borders.Bottom.Visible = true;
                                row1.Cells[4].Borders.Bottom.Visible = true;
                                row1.Cells[5].Borders.Bottom.Visible = true;
                            }
                            else
                            {
                                row1.Cells[0].Borders.Bottom.Visible = false;
                                row1.Cells[1].Borders.Bottom.Visible = false;
                                row1.Cells[2].Borders.Bottom.Visible = false;
                                row1.Cells[3].Borders.Bottom.Visible = false;
                                row1.Cells[4].Borders.Bottom.Visible = false;
                                row1.Cells[5].Borders.Bottom.Visible = false;
                            }
                        }

                        if ((maxrow % 10) > 0)
                        {
                            int totalremainingrow = 10 - (maxrow % 10);
                            for (int j = 0; j < totalremainingrow; j++)
                            {
                                Row rowRemain = table.AddRow();
                                rowRemain.Height = "1cm";
                                rowRemain.Cells[0].AddParagraph();
                                rowRemain.Cells[1].AddParagraph();
                                rowRemain.Cells[2].AddParagraph();
                                rowRemain.Cells[3].AddParagraph();
                                rowRemain.Cells[4].AddParagraph();
                                rowRemain.Cells[5].AddParagraph();

                                if (j == (totalremainingrow - 1))
                                {
                                    rowRemain.Cells[0].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[1].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[2].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[3].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[4].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[5].Borders.Bottom.Visible = true;
                                }
                                else if (j > 0 && (j % (totalremainingrow - 1)) == 0)
                                {
                                    rowRemain.Cells[0].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[1].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[2].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[3].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[4].Borders.Bottom.Visible = true;
                                    rowRemain.Cells[5].Borders.Bottom.Visible = true;
                                }
                                else
                                {
                                    rowRemain.Cells[0].Borders.Bottom.Visible = false;
                                    rowRemain.Cells[1].Borders.Bottom.Visible = false;
                                    rowRemain.Cells[2].Borders.Bottom.Visible = false;
                                    rowRemain.Cells[3].Borders.Bottom.Visible = false;
                                    rowRemain.Cells[4].Borders.Bottom.Visible = false;
                                    rowRemain.Cells[5].Borders.Bottom.Visible = false;
                                }
                            }
                        }

                        Row rowTot = table.AddRow();
                        rowTot.Height = "1cm";
                        rowTot.Format.Font.Bold = true;
                        rowTot.Cells[0].AddParagraph("JUMLAH BERSIH (RM):");
                        rowTot.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                        rowTot.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                        rowTot.Cells[0].MergeRight = 4;

                        rowTot.Cells[5].AddParagraph(jumlahgajibersih.ToString("#,##0.00"));
                        rowTot.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                        rowTot.Cells[5].VerticalAlignment = VerticalAlignment.Center;

                        //footer.AddText("Footer");
                        //footer.Format.Font.Size = 9;
                        //footer.Format.Alignment = ParagraphAlignment.Center;

                        // Create the item table for footer
                        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Headers.Primary.AddImage(logo_lima);
                        MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Footers.Primary.AddTable();
                        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.AddTable();
                        tblBtm.Style = "Table";
                        tblBtm.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
                        tblBtm.Borders.Width = 0.25;
                        tblBtm.Borders.Left.Width = 0.5;
                        tblBtm.Borders.Right.Width = 0.5;
                        tblBtm.Rows.LeftIndent = 0;

                        // Before you can add a row, you must define the columns
                        Column colTblBtm = tblBtm.AddColumn("18cm");
                        colTblBtm.Format.Alignment = ParagraphAlignment.Left;

                        Row rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Borders.Left.Visible = false;
                        rowTblBtm.Borders.Right.Visible = false;
                        rowTblBtm.Borders.Top.Visible = false;
                        //rowTblBtm.Borders.Bottom.Visible = false;
                        rowTblBtm.Cells[0].AddParagraph();

                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Format.Font.Bold = true;
                        rowTblBtm.Height = "1cm";
                        rowTblBtm.Shading.Color = MigraDoc.DocumentObjectModel.Colors.Gray;
                        rowTblBtm.Cells[0].AddParagraph("Catatan:");
                        rowTblBtm.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                        rowTblBtm.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Borders.Bottom.Visible = false;
                        rowTblBtm.Cells[0].AddParagraph();

                        for (int x = 0; x < lsEmployer.Count; x++)
                        {
                            HRModel modEmployer = (HRModel)lsEmployer[x];

                            rowTblBtm = tblBtm.AddRow();
                            rowTblBtm.Borders.Bottom.Visible = false;
                            rowTblBtm.Cells[0].AddParagraph(modEmployer.GetSetdesc + ": RM" + modEmployer.GetSetitemamount.ToString("#,##0.00"));
                        }

                        /*
                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Borders.Bottom.Visible = false;
                        rowTblBtm.Cells[0].AddParagraph("Nama:");

                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Borders.Bottom.Visible = false;
                        rowTblBtm.Cells[0].AddParagraph("Jawatan:");

                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Borders.Bottom.Visible = false;
                        rowTblBtm.Cells[0].AddParagraph("Tarikh:");
                        */

                        rowTblBtm = tblBtm.AddRow();
                        rowTblBtm.Height = "2cm";

                        // Create a renderer for PDF that uses Unicode font encoding
                        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                        // Set the MigraDoc document
                        pdfRenderer.Document = doc;

                        // Create the PDF document
                        pdfRenderer.RenderDocument();

                        // Save the document...
                        string pdfFilename = "Penyata_Gaji_" + oHRModStaff.GetSetyear + "_" + oHRModStaff.GetSetmonth + "_" + oHRModStaff.GetSetss_id + "_" + oHRModStaff.GetSetstaffno + ".pdf";
                        //string file = Server.MapPath("~/App_Data/" + pdfFilename);
                        string file = HttpContext.Current.Server.MapPath("~/App_Data/" + pdfFilename);
                        //string file = "C:/TEMP/" + pdfFilename;

                        // If start a viewer //
                        pdfRenderer.Save(file);
                        Process.Start(file);

                        oHRCon.updateRunSalaryStaffAttachment(oHRModStaff, file, pdfFilename);

                        // If Send PDF to browser //
                        /*
                        MemoryStream stream = new MemoryStream();
                        pdfRenderer.Save(stream, false);
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", stream.Length.ToString());
                        Response.BinaryWrite(stream.ToArray());
                        Response.Flush();
                        stream.Close();
                        Response.End();
                        */
                    }

                }
                sStatus = "Y";
                sMessage = "Proses berjaya!";
            }
            else
            {
                sStatus = "N";
                sMessage = "Proses tidak berjaya! Record not found for Comp: " + currcomp + " & Id: " + ss_id;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String approveSGStaffGaji(String currcomp, String currfyr, Int64 ss_id)
    {
        HRController oHRCon = new HRController();
        MainController oMainCon = new MainController();

        String sUserId = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error!";

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        String jsonResponse = "";
        ArrayList recordPayment = new ArrayList();

        if (currcomp.Length > 0 && ss_id > 0)
        {
            MainModel modCompInfo = oMainCon.getCompInfoDetails(currcomp);

            HRModel modItem = oHRCon.getRunSalaryDetails(currcomp, currfyr, "", "", ss_id);
            if (modItem.GetSetid > 0)
            {
                //assigning item for Gaji, socso, zakat, kwsp, others
                double jumlahgaji = 0;
                double jumlahsocso = 0;
                double jumlahkwsp = 0;
                double jumlahzakat = 0;
                double jumlahlain2 = 0;

                //process Run Gaji
                ArrayList lsItem = oHRCon.getRunSalaryStaffList(currcomp, currfyr, "", "", "", 0, ss_id, 0);
                for (int i = 0; i < lsItem.Count; i++)
                {
                    HRModel oHRModStaff = (HRModel)lsItem[i];

                    ArrayList lsSalaryItem = oHRCon.getRunStaffSalaryItemList(currcomp, currfyr, oHRModStaff.GetSetstaffno, 0, 0, oHRModStaff.GetSetss_id, oHRModStaff.GetSetid, "", "", "");

                    if (lsSalaryItem.Count > 0)
                    {
                        double jumlahgajibersih = 0;

                        for (int j = 0; j < lsSalaryItem.Count; j++)
                        {
                            HRModel modSalaryDet = (HRModel)lsSalaryItem[j];

                            if (modSalaryDet.GetSetcat.Equals("DEDUCTION"))
                            {
                                jumlahgajibersih = jumlahgajibersih - modSalaryDet.GetSetitemamount;

                                if (modSalaryDet.GetSetcode.Equals("ZAKAT"))
                                {
                                    jumlahzakat = jumlahzakat + modSalaryDet.GetSetitemamount;
                                }
                                else if (modSalaryDet.GetSetcode.Equals("PERKESO") || modSalaryDet.GetSetcode.Equals("PERKESO(E)"))
                                {
                                    jumlahsocso = jumlahsocso + modSalaryDet.GetSetitemamount;
                                }
                                else if (modSalaryDet.GetSetcode.Equals("KWSP") || modSalaryDet.GetSetcode.Equals("KWSP(E)"))
                                {
                                    jumlahkwsp = jumlahkwsp + modSalaryDet.GetSetitemamount;
                                }
                                else
                                {
                                    jumlahlain2 = jumlahlain2 + modSalaryDet.GetSetitemamount;
                                }
                            }
                            else if (modSalaryDet.GetSetcat.Equals("EMPLOYEE"))
                            {
                                //do nothing
                                //jumlahgajibersih = jumlahgajibersih;

                                if (modSalaryDet.GetSetcode.Equals("ZAKAT"))
                                {
                                    jumlahzakat = jumlahzakat + modSalaryDet.GetSetitemamount;
                                }
                                else if (modSalaryDet.GetSetcode.Equals("PERKESO") || modSalaryDet.GetSetcode.Equals("PERKESO(E)"))
                                {
                                    jumlahsocso = jumlahsocso + modSalaryDet.GetSetitemamount;
                                }
                                else if (modSalaryDet.GetSetcode.Equals("KWSP") || modSalaryDet.GetSetcode.Equals("KWSP(E)"))
                                {
                                    jumlahkwsp = jumlahkwsp + modSalaryDet.GetSetitemamount;
                                }
                                else
                                {
                                    jumlahlain2 = jumlahlain2 + modSalaryDet.GetSetitemamount;
                                }
                            }
                            else
                            {
                                jumlahgajibersih = jumlahgajibersih + modSalaryDet.GetSetitemamount;
                            }

                        }
                        //end loop for each item gaji

                        jumlahgaji = jumlahgaji + jumlahgajibersih;
                    }
                    //end loop for each staff

                }
                //create info of expenses - salary, socso, kwsp, zakat & others
                MainModel modBP = oMainCon.getBPDetails(currcomp, "BP000000");
                String paytype = "BANKING";
                String paycat = "PAYMENT_VOUCHER";
                //Salary
                object paymentSalary = new { comp = currcomp, id = ss_id, bpid = modBP.GetSetbpid, bpdesc = modBP.GetSetbpdesc, bpaddress = modBP.GetSetbpaddress, exp_cat = paycat, exp_type = "EMOLUMEN_GAJI_UPAH", exp_item = "PAR201600006", exp_itemdesc = "EMOLUMEN DAN GAJI", exp_paytype = paytype, exp_amount = jumlahgaji, exp_remarks = "Pembayaran Jumlah GAJI bagi Bulan " + modItem.GetSetmonth + " Tahun " + modItem.GetSetyear };
                recordPayment.Add(paymentSalary);
                //zakat
                object paymentZakat = new { comp = currcomp, id = ss_id, bpid = modBP.GetSetbpid, bpdesc = modBP.GetSetbpdesc, bpaddress = modBP.GetSetbpaddress, exp_cat = paycat, exp_type = "MANFAAT_KEMUDAHAN_PEKERJA", exp_item = "PAR201600027", exp_itemdesc = "ZAKAT", exp_paytype = paytype, exp_amount = jumlahzakat, exp_remarks = "Pembayaran Jumlah ZAKAT bagi Bulan " + modItem.GetSetmonth + " Tahun " + modItem.GetSetyear };
                recordPayment.Add(paymentZakat);
                //kwsp
                object paymentKWSP = new { comp = currcomp, id = ss_id, bpid = modBP.GetSetbpid, bpdesc = modBP.GetSetbpdesc, bpaddress = modBP.GetSetbpaddress, exp_cat = paycat, exp_type = "MANFAAT_KEMUDAHAN_PEKERJA", exp_item = "PAR201600025", exp_itemdesc = "KWSP", exp_paytype = paytype, exp_amount = jumlahkwsp, exp_remarks = "Pembayaran Jumlah KWSP bagi Bulan " + modItem.GetSetmonth + " Tahun " + modItem.GetSetyear };
                recordPayment.Add(paymentKWSP);
                //socso
                object paymentSocso = new { comp = currcomp, id = ss_id, bpid = modBP.GetSetbpid, bpdesc = modBP.GetSetbpdesc, bpaddress = modBP.GetSetbpaddress, exp_cat = paycat, exp_type = "MANFAAT_KEMUDAHAN_PEKERJA", exp_item = "PAR201600026", exp_itemdesc = "SOCSO", exp_paytype = paytype, exp_amount = jumlahsocso, exp_remarks = "Pembayaran Jumlah SOCSO bagi Bulan " + modItem.GetSetmonth + " Tahun " + modItem.GetSetyear };
                recordPayment.Add(paymentSocso);
                //socso
                object paymentOthers = new { comp = currcomp, id = ss_id, bpid = modBP.GetSetbpid, bpdesc = modBP.GetSetbpdesc, bpaddress = modBP.GetSetbpaddress, exp_cat = paycat, exp_type = "MANFAAT_KEMUDAHAN_PEKERJA", exp_item = "PAR201600033", exp_itemdesc = "MANAFAAT LAIN-LAIN", exp_paytype = paytype, exp_amount = jumlahlain2, exp_remarks = "Pembayaran Jumlah Lain-Lain bagi Bulan " + modItem.GetSetmonth + " Tahun " + modItem.GetSetyear };
                recordPayment.Add(paymentOthers);

                sStatus = "Y";
                sMessage = "Kelulusan berjaya!";
            }
            else
            {
                sStatus = "N";
                sMessage = "Kelulusan tidak berjaya! Record not found for Comp: " + currcomp + " & Id: " + ss_id;
            }
        }

        object retData = new { result = sStatus, id = ss_id, paymentlist = recordPayment, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String paymentSGStaffGaji(String currcomp, String currfyr, Int64 ss_id, LocalModel[] paymentlist)
    {
        MainController oMainCon = new MainController();
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

        if (currcomp.Length > 0 && ss_id > 0)
        {

            HRModel modItem = oHRCon.getRunSalaryDetails(currcomp, currfyr, "", "", ss_id);

            if (modItem.GetSetid > 0)
            {
                sStatus = "Y";
                sMessage = "Penyediaan Pembayaran berjaya!";
                
                for (int i=0; i<paymentlist.Length; i++)
                {
                    if (sStatus.Equals("Y"))
                    {
                        LocalModel modSalaryExpenses = (LocalModel)paymentlist[i];
                        //insert into table Expenses
                        MainModel oModExpenses = new MainModel();
                        oModExpenses.GetSetcomp = modItem.GetSetcomp;
                        oModExpenses.GetSetexpensesdate = DateTime.Now.ToString();
                        oModExpenses.GetSetexpensescat = modSalaryExpenses.exp_cat;
                        oModExpenses.GetSetexpensestype = modSalaryExpenses.exp_type;
                        String sExpensesNo = oMainCon.getNextRunningNo(modItem.GetSetcomp, "EXPENSES", "ACTIVE");
                        oModExpenses.GetSetexpensesno = sExpensesNo;
                        oModExpenses.GetSetbpid = modSalaryExpenses.bpid;
                        oModExpenses.GetSetbpdesc = modSalaryExpenses.bpdesc;
                        oModExpenses.GetSetbpaddress = modSalaryExpenses.bpaddress;
                        oModExpenses.GetSetbpcontact = "";
                        oModExpenses.GetSetremarks = modSalaryExpenses.exp_remarks;
                        oModExpenses.GetSetstatus = "CONFIRMED";
                        oModExpenses.GetSetexpensesamount = modSalaryExpenses.exp_amount;
                        oModExpenses.GetSettotalamount = modSalaryExpenses.exp_amount;
                        oModExpenses.GetSetpaypaidamount = 0;
                        oModExpenses.GetSetpurchaseamount = 0;
                        oModExpenses.GetSetcreatedby = sUserId;
                        oModExpenses.GetSetcreateddate = DateTime.Now.ToString();
                        oModExpenses.GetSetconfirmedby = sUserId;
                        oModExpenses.GetSetconfirmeddate = DateTime.Now.ToString();

                        if (oMainCon.insertExpensesHeader2(oModExpenses).Equals("Y"))
                        {
                            oMainCon.updateNextRunningNo(oModExpenses.GetSetcomp, "EXPENSES", "ACTIVE");

                            MainModel oModLineItem = new MainModel();
                            oModLineItem.GetSetcomp = oModExpenses.GetSetcomp;
                            oModLineItem.GetSetexpensesno = oModExpenses.GetSetexpensesno;
                            oModLineItem.GetSetlineno = oMainCon.getExpensesDetailsList(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno, 0, "").Count + 1;
                            oModLineItem.GetSetitemno = modSalaryExpenses.exp_item;
                            oModLineItem.GetSetitemdesc = modSalaryExpenses.exp_itemdesc;
                            oModLineItem.GetSetunitprice = modSalaryExpenses.exp_amount;
                            oModLineItem.GetSetdiscamount = 0;
                            oModLineItem.GetSetquantity = 1;
                            oModLineItem.GetSettaxcode = "N/A";
                            oModLineItem.GetSetexpensesprice = modSalaryExpenses.exp_amount;
                            oModLineItem.GetSettotalexpenses = modSalaryExpenses.exp_amount;

                            MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, modSalaryExpenses.exp_item, "NEW");
                            if (modExistent.GetSetinvoiceno.Length > 0)
                            {
                                sStatus = "N";
                                sMessage = "Expenses item already assigned, Please contact System Admin!";
                            }
                            else
                            {
                                //insert new line item
                                if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
                                {
                                    //update expenses header information
                                    sStatus = oMainCon.updateExpensesHeaderInfo(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno);
                                    sMessage = "Penyediaan Pembayaran berjaya!";
                                }
                                else
                                {
                                    sStatus = "N";
                                    sMessage = "Unable to add expenses item, Please contact System Admin!";
                                }
                            }
                        }

                        if (sStatus.Equals("Y"))
                        {
                            //insert into table link staff_salary_expenses
                            HRModel modItemDetails = new HRModel();
                            modItemDetails.GetSetcomp = modItem.GetSetcomp;
                            modItemDetails.GetSetfyr = modItem.GetSetfyr;
                            modItemDetails.GetSetss_id = modItem.GetSetid;
                            modItemDetails.GetSetcat = modSalaryExpenses.exp_cat;
                            modItemDetails.GetSettype = modSalaryExpenses.exp_type;
                            modItemDetails.GetSetmonth = modItem.GetSetmonth;
                            modItemDetails.GetSetyear = modItem.GetSetyear;
                            modItemDetails.GetSetcode = modSalaryExpenses.exp_item;
                            modItemDetails.GetSetdesc = modSalaryExpenses.exp_itemdesc;
                            modItemDetails.GetSetitemamount = modSalaryExpenses.exp_amount;
                            modItemDetails.GetSetremarks = oModExpenses.GetSetexpensesno;
                            String result = oHRCon.insertRunSalaryExpenses(modItem);
                            if (result.Equals("Y"))
                            {
                                sStatus = "Y";
                                sMessage = "Penyediaan Pembayaran berjaya!";
                            }
                            else
                            {
                                sStatus = "N";
                                sMessage = "Penyediaan Pembayaran tidak berjaya!\nUnable to create link between salary and expenses info, Please contact System Administrator!";
                            }
                        }
                        else
                        {
                            sStatus = "N";
                            sMessage = "Penyediaan Pembayaran tidak berjaya!\nUnable to create expenses info, Please contact System Administrator!";
                        }
                    }
                }
                //update as completed
                if (sStatus.Equals("Y"))
                {
                    modItem.GetSetstatus = "APPROVED";
                    modItem.GetSetapprovedby = sUserId;
                    modItem.GetSetapproveddate = DateTime.Now.ToString();
                    String result = oHRCon.updateRunSalary(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Penyediaan Pembayaran berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Penyediaan Pembayaran tidak berjaya!\nUnable to update status of salary run, Please contact System Administrator!";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Penyediaan Pembayaran tidak berjaya! Record not found for Comp: " + currcomp + " & Id: " + ss_id;
            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    public class LocalModel
    {
        public string comp { get; set; }
        public string id { get; set; }
        public string bpid { get; set; }
        public string bpdesc { get; set; }
        public string bpaddress { get; set; }
        public string exp_cat { get; set; }
        public string exp_type { get; set; }
        public string exp_item { get; set; }
        public string exp_itemdesc { get; set; }
        public string exp_paytype { get; set; }
        public double exp_amount { get; set; }
        public string exp_remarks { get; set; }
    }
}