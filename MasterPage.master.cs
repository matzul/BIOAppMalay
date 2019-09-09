using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class MasterPage : System.Web.UI.MasterPage
{
    public MainController oMainCon = new MainController();
    public ArrayList lsUserRoleMod = new ArrayList();
    public ArrayList lsUserRoleSubMod = new ArrayList();
    
    public ArrayList lsUserCompInfo = new ArrayList();
    public MainModel modCompInfo = new MainModel();
    public UserProfileModel oModUser = new UserProfileModel();

    public String eventType = "";
    public String sCurrFyr = "";
    public String sCurrComp = "";
    public String sUserId = "";
    public String sUserName = "";
    public String sUserType = "";
    public String sUserStatus = "";
    public String sMessage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //must comment out this one to allow asp:button onclick is firing masterpage code behind
        //if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }

    }
    private void initialValues()
    {
        if (Session["comp"] != null)
            sCurrComp = oMainCon.replaceNull(Session["comp"].ToString());
        if (Session["userid"] != null)
            sUserId = oMainCon.replaceNull(Session["userid"].ToString());
        if (Session["username"] != null)
            sUserName = oMainCon.replaceNull(Session["username"].ToString());
        if (Session["usertype"] != null)
            sUserType = oMainCon.replaceNull(Session["usertype"].ToString());
        if (Session["userstatus"] != null)
            sUserStatus = oMainCon.replaceNull(Session["userstatus"].ToString());
        if (Session["fyr"] != null)
            sCurrFyr = Session["fyr"].ToString();
    }
    private void processValues()
    {
        if (Session["userid"] == null)
        {
            String sMessage = "System expired...";
            Response.Redirect("Default.aspx?message=" + sMessage);
        }
        else
        {
            modCompInfo = oMainCon.getCompInfoDetails(sCurrComp);
            oModUser = oMainCon.getUserProfile("", sUserId, "", "");
            lsUserCompInfo = oMainCon.getUserCompInfoList("", sUserId, "ACTIVE");
            lsUserRoleMod = oMainCon.getUserRoleModule(sUserId, sCurrComp);
            lsUserRoleSubMod = oMainCon.getUserRoleSubModule(sUserId, sCurrComp);
        }
    }

}
