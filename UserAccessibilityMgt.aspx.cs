using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccessibilityMgt : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public MainModel oModComp = new MainModel();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sCurrPage = "";
    public String sCompName = "";
    public String sCompId = "";
    public String sActionString = "";
    public String sAlertMessage = "";
    public String sUserAction = "";
    public MainModel oModRole = new MainModel();
    public MainModel oModModule = new MainModel();
    public MainModel oModSubmodule = new MainModel();
    public MainModel oModScreen = new MainModel();
    public ArrayList lsRole = new ArrayList();
    public ArrayList lsModule = new ArrayList();
    public ArrayList lsSubmodule = new ArrayList();
    public ArrayList lsScreen = new ArrayList();
    public string roleid = "";
    public string moduleid = "";
    public string submoduleid = "";
    public string screenid = "";

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
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
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
        if (Request.Params.Get("hidUserAction") != null)
        {
            sUserAction = oMainCon.replaceNull(Request.Params.Get("hidUserAction"));
        }

        if (sAction.Equals("VIEW_ROLE"))
        {
            roleid = oMainCon.replaceNull(Request.Params.Get("hidRoleid"));
        }
        else if (sAction.Equals("VIEW_MODULE"))
        {
            moduleid = oMainCon.replaceNull(Request.Params.Get("hidModuleid"));
        }
        else if (sAction.Equals("VIEW_SUBMODULE"))
        {
            submoduleid = oMainCon.replaceNull(Request.Params.Get("hidSubmoduleid"));
        }
        else if (sAction.Equals("VIEW_SCREEN"))
        {
            screenid = oMainCon.replaceNull(Request.Params.Get("hidScreenid"));
        }
        else if (sAction.Equals("SAVE_ROLE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetroleid = oMainCon.replaceNull(Request.Params.Get("roleid"));
            oModComp.GetSetrolename = oMainCon.replaceNull(Request.Params.Get("rolename"));
            oModComp.GetSetroledesc = oMainCon.replaceNull(Request.Params.Get("roledesc"));
            oModComp.GetSetrolestatus = oMainCon.replaceNull(Request.Params.Get("rolestatus"));
        }
        else if (sAction.Equals("SAVE_MODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetmoduleid = oMainCon.replaceNull(Request.Params.Get("moduleid"));
            oModComp.GetSetmodulename = oMainCon.replaceNull(Request.Params.Get("modulename"));
            oModComp.GetSetmoduledesc = oMainCon.replaceNull(Request.Params.Get("moduledesc"));
            oModComp.GetSetmodulestatus = oMainCon.replaceNull(Request.Params.Get("modulestatus"));
            oModComp.GetSetmoduleicon = oMainCon.replaceNull(Request.Params.Get("moduleicon"));
        }
        else if (sAction.Equals("SAVE_SUBMODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetsubmoduleid = oMainCon.replaceNull(Request.Params.Get("submoduleid"));
            oModComp.GetSetmoduleid = oMainCon.replaceNull(Request.Params.Get("submodule-moduleid"));
            oModComp.GetSetsubmodulename = oMainCon.replaceNull(Request.Params.Get("submodulename"));
            oModComp.GetSetsubmoduledesc = oMainCon.replaceNull(Request.Params.Get("submoduledesc"));
            oModComp.GetSetsubmodulestatus = oMainCon.replaceNull(Request.Params.Get("submodulestatus"));
        }
        else if (sAction.Equals("SAVE_SCREEN"))
        {
            oModComp = new MainModel();
            oModComp.GetSetscreenid = oMainCon.replaceNull(Request.Params.Get("screenid"));
            oModComp.GetSetscreenfilename = oMainCon.replaceNull(Request.Params.Get("screenfilename"));
            oModComp.GetSetscreendesc = oMainCon.replaceNull(Request.Params.Get("screendesc"));
            oModComp.GetSetscreenstatus = oMainCon.replaceNull(Request.Params.Get("screenstatus"));
        }
        else if (sAction.Equals("STORE_ROLE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetroleid = oMainCon.replaceNull(Request.Params.Get("roleid"));
            oModComp.GetSetrolename = oMainCon.replaceNull(Request.Params.Get("rolename"));
            oModComp.GetSetroledesc = oMainCon.replaceNull(Request.Params.Get("roledesc"));
            oModComp.GetSetrolestatus = oMainCon.replaceNull(Request.Params.Get("rolestatus"));
        }
        else if (sAction.Equals("STORE_MODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetmoduleid = oMainCon.replaceNull(Request.Params.Get("moduleid"));
            oModComp.GetSetmodulename = oMainCon.replaceNull(Request.Params.Get("modulename"));
            oModComp.GetSetmoduledesc = oMainCon.replaceNull(Request.Params.Get("moduledesc"));
            oModComp.GetSetmodulestatus = oMainCon.replaceNull(Request.Params.Get("modulestatus"));
            oModComp.GetSetmoduleicon = oMainCon.replaceNull(Request.Params.Get("moduleicon"));
        }
        else if (sAction.Equals("STORE_SUBMODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetsubmoduleid = oMainCon.replaceNull(Request.Params.Get("submoduleid"));
            oModComp.GetSetmoduleid = oMainCon.replaceNull(Request.Params.Get("submodule-moduleid"));
            oModComp.GetSetsubmodulename = oMainCon.replaceNull(Request.Params.Get("submodulename"));
            oModComp.GetSetsubmoduledesc = oMainCon.replaceNull(Request.Params.Get("submoduledesc"));
            oModComp.GetSetsubmodulestatus = oMainCon.replaceNull(Request.Params.Get("submodulestatus"));
        }
        else if (sAction.Equals("STORE_SCREEN"))
        {
            oModComp = new MainModel();
            oModComp.GetSetscreenid = oMainCon.replaceNull(Request.Params.Get("screenid"));
            oModComp.GetSetscreenfilename = oMainCon.replaceNull(Request.Params.Get("screenfilename"));
            oModComp.GetSetscreendesc = oMainCon.replaceNull(Request.Params.Get("screendesc"));
            oModComp.GetSetscreenstatus = oMainCon.replaceNull(Request.Params.Get("screenstatus"));
        }
        else if (sAction.Equals("DELETE_ROLE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetroleid = oMainCon.replaceNull(Request.Params.Get("roleid"));
        }
        else if (sAction.Equals("DELETE_MODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetmoduleid = oMainCon.replaceNull(Request.Params.Get("moduleid"));
        }
        else if (sAction.Equals("DELETE_SUBMODULE"))
        {
            oModComp = new MainModel();
            oModComp.GetSetsubmoduleid = oMainCon.replaceNull(Request.Params.Get("submoduleid"));
        }
        else if (sAction.Equals("DELETE_SCREEN"))
        {
            oModComp = new MainModel();
            oModComp.GetSetscreenid = oMainCon.replaceNull(Request.Params.Get("screenid"));
        }
    }

    private void processValues()
    {
        String a = sAction;
        if (sAction.Equals("OPEN"))
        {
            sActionString = "Mengurus Capaian Pengguna";
            if (sCurrComp.Length > 0)
            {
                lsRole = oMainCon.getRole();
                lsModule = oMainCon.getModule();
                lsSubmodule = oMainCon.getSubModule("");
                lsScreen = oMainCon.getScreen();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
            }
        }
        else if (sAction.Equals("VIEW_ROLE"))
        {
            if(roleid.Length > 0)
            {
                oModRole = oMainCon.getRoleDetails(roleid);

                lsRole = oMainCon.getRole();
                lsModule = oMainCon.getModule();
                lsSubmodule = oMainCon.getSubModule("");
                lsScreen = oMainCon.getScreen();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
            }
        }
        else if (sAction.Equals("VIEW_MODULE"))
        {
            if (moduleid.Length > 0)
            {
                oModModule = oMainCon.getModuleDetails(moduleid);

                lsRole = oMainCon.getRole();
                lsModule = oMainCon.getModule();
                lsSubmodule = oMainCon.getSubModule("");
                lsScreen = oMainCon.getScreen();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
            }
        }
        else if (sAction.Equals("VIEW_SUBMODULE"))
        {
            if (submoduleid.Length > 0)
            {
                oModSubmodule = oMainCon.getSubModuleDetails(submoduleid);

                lsRole = oMainCon.getRole();
                lsModule = oMainCon.getModule();
                lsSubmodule = oMainCon.getSubModule("");
                lsScreen = oMainCon.getScreen();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
            }
        }
        else if (sAction.Equals("VIEW_SCREEN"))
        {
            if (screenid.Length > 0)
            {
                oModScreen = oMainCon.getScreenDetails(screenid);

                lsRole = oMainCon.getRole();
                lsModule = oMainCon.getModule();
                lsSubmodule = oMainCon.getSubModule("");
                lsScreen = oMainCon.getScreen();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Capaian Pengguna...";
            }
        }
        else if (sAction.Equals("SAVE_ROLE"))
        {
            if (sCurrComp.Length > 0)
            {

                if (oMainCon.updateRole(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini Role...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Role...";
            }
        }
        else if (sAction.Equals("SAVE_MODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.updateModule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini Module...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Module...";
            }
        }
        else if (sAction.Equals("SAVE_SUBMODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.updateSubmodule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini Submodule...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Submodule...";
            }
        }
        else if (sAction.Equals("SAVE_SCREEN"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.updateScreen(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini Screen...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Screen...";
            }
        }
        else if (sAction.Equals("STORE_ROLE"))
        {
            if (sCurrComp.Length > 0)
            {

                if (oMainCon.insertRole(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Role...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Role...";
            }
        }
        else if (sAction.Equals("STORE_MODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.insertModule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Module...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Module...";
            }
        }
        else if (sAction.Equals("STORE_SUBMODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.insertSubmodule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Submodule...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Submodule...";
            }
        }
        else if (sAction.Equals("STORE_SCREEN"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.insertScreen(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Screen...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Screen...";
            }
        }
        else if (sAction.Equals("DELETE_ROLE"))
        {
            if (sCurrComp.Length > 0)
            {

                if (oMainCon.deleteRole(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Role...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Role...";
            }
        }
        else if (sAction.Equals("DELETE_MODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.deleteModule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Module...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Module...";
            }
        }
        else if (sAction.Equals("DELETE_SUBMODULE"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.deleteSubmodule(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Submodule...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Submodule...";
            }
        }
        else if (sAction.Equals("DELETE_SCREEN"))
        {
            if (sCurrComp.Length > 0)
            {
                if (oMainCon.deleteScreen(oModComp) == 1)
                {
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
                else
                {
                    sAlertMessage = "ERROR|Tidak dapat Memasukkan Screen...";
                    lsRole = oMainCon.getRole();
                    lsModule = oMainCon.getModule();
                    lsSubmodule = oMainCon.getSubModule("");
                    lsScreen = oMainCon.getScreen();
                }
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Screen...";
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