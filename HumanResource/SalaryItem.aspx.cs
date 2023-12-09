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

public partial class HumanResource_SalaryItem : System.Web.UI.Page
{
    public HRController oHRCon = new HRController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sStaffNo = "";
    public String sAction = "";

    public String sCode = "";
    public String sDesc = "";
    public String sType = "";
    public String sCat = "";

    public String sTotalPage = "1";
    public String sCurrentPage = "1";
    public ArrayList lsSalaryItem = new ArrayList();
    public ArrayList lsSalaryItemCat = new ArrayList();
    public ArrayList lsItemGroup= new ArrayList();

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
        //define Item Category
        getItemTypeList();
        lsSalaryItem = oHRCon.getCompSalaryItemList(sCurrComp, sCat, sType, sCode, sDesc);
        lsItemGroup = oHRCon.getCompSalaryItemGroupList(sCurrComp, "ACTIVE");
        this.lsPageList.Items.Add(new ListItem("1", "1"));
        this.lsPageList.SelectedValue = sCurrentPage;
        
    }

    private void getItemTypeList()
    {
        lsSalaryItemCat = new ArrayList();
        Object objData = new
        {
            GetSetcode = "SALARY",
            GetSettype = "SALARY"
        };
        lsSalaryItemCat.Add(objData);

        objData = new
        {
            GetSetcode = "ADDITION",
            GetSettype = "ADDITION"
        };
        lsSalaryItemCat.Add(objData);

        objData = new
        {
            GetSetcode = "DEDUCTION",
            GetSettype = "DEDUCTION"
        };
        lsSalaryItemCat.Add(objData);

        objData = new
        {
            GetSetcode = "EMPLOYEE",
            GetSettype = "EMPLOYEE"
        };
        lsSalaryItemCat.Add(objData);


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
        if (Request.Params.Get("lsFindCat") != null)
        {
            sCat = oHRCon.replaceNull(Request.Params.Get("lsFindCat"));
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
    public static String getSalaryItemList(string currcomp)
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
            ArrayList lsSalaryItem = oHRCon.getCompSalaryItemList(currcomp, "", "", "", "");
            for (int i = 0; i < lsSalaryItem.Count; i++)
            {
                HRModel oHRMod = (HRModel)lsSalaryItem[i];

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
    public static String getSalaryItemDetail(string currcomp, int id)
    {
        HRController oHRCon = new HRController();
        String sUserId = "";
        String sStatus = "N";

        HRModel oItemMod = new HRModel();

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = oHRCon.replaceNull(HttpContext.Current.Session["userid"].ToString());
        }

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";

        if (currcomp.Length > 0 && id > 0)
        {
            oItemMod = oHRCon.getCompSalaryItemDetails(currcomp, "", "", "", id);
            sStatus = "Y";
        }

        object retData = new { result = sStatus, itemdetail = oItemMod };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    //public static String insertPHItemDetail(string currcomp, string currfyr, string phdate, string code, string desc, string cat, string status)
    public static String insertSalaryItemDetail(HRModel itemsalary)
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

        if (itemsalary.GetSetcomp.Length > 0 )
        {
            HRModel modItem = oHRCon.getCompSalaryItemDetails(itemsalary.GetSetcomp, itemsalary.GetSetcat, "", itemsalary.GetSetcode, 0);
            if (modItem.GetSetid == 0)
            {
                modItem = itemsalary;
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertCompSalaryItem(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = result;
                    sMessage = "Tambah berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Tambah tidak berjaya! Error on inserting table Salary Item...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Tambah tidak berjaya! Record already exist for Comp: " + itemsalary.GetSetcomp + " & Kod: " + itemsalary.GetSetcode + " & Kategori: " + itemsalary.GetSetcat + " & Jenis: " + itemsalary.GetSettype + " & Nama/ Keterangan: " + itemsalary.GetSetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String updateSalaryItemDetail(HRModel itemsalary)
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

        if (itemsalary.GetSetcomp.Length > 0 && itemsalary.GetSetid > 0)
        {
            HRModel modItem = oHRCon.getCompSalaryItemDetails(itemsalary.GetSetcomp, "", "", "", itemsalary.GetSetid);
            if (modItem.GetSetid > 0)
            {
                bool proceeUpdate = true;
                
                if (proceeUpdate) 
                {
                    modItem.GetSetcomp = itemsalary.GetSetcomp;
                    modItem.GetSetcode = itemsalary.GetSetcode;
                    modItem.GetSetdesc = itemsalary.GetSetdesc;
                    modItem.GetSetcat = itemsalary.GetSetcat;
                    modItem.GetSettype = itemsalary.GetSettype;
                    modItem.GetSetitemvalue = itemsalary.GetSetitemvalue;
                    modItem.GetSetitemgroup = itemsalary.GetSetitemgroup;
                    modItem.GetSetstatus = itemsalary.GetSetstatus;
                    modItem.GetSetmodifiedby = sUserId;
                    String result = oHRCon.updateCompSalaryItem(modItem);
                    if (result.Equals("Y"))
                    {
                        sStatus = result;
                        sMessage = "Kemaskini berjaya!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini tidak berjaya! Error on updating table Salary Item...";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini tidak berjaya! No record found for Comp: " + itemsalary.GetSetcomp + " & Jenis: " + itemsalary.GetSettype + " & Nama/ Keterangan: " + itemsalary.GetSetdesc;

            }
        }

        object retData = new { result = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod(EnableSession = true)]
    public static String deleteSalaryItemDetail(string currcomp, int id)
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
            HRModel modItem = oHRCon.getCompSalaryItemDetails(currcomp, "", "", "", id);
            if (modItem.GetSetid > 0)
            {
                String result = oHRCon.deleteCompSalaryItem(modItem);
                if (result.Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "Hapus berjaya!";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Hapus tidak berjaya! Error on deleting table Salary Item...";
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