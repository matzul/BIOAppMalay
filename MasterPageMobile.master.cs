using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageMobile : System.Web.UI.MasterPage
{
    public MainController oMainCon = new MainController();

    public UserProfileModel modUserProfile = new UserProfileModel();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sUserName = "";
    public String sUserType = "";
    public String sUserStatus = "";

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
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
            sUserName = modUserProfile.GetSetusername;
            sUserType = modUserProfile.GetSetusertype;
            sUserStatus = modUserProfile.GetSetuserstatus;
        }
    }
    private void processValues()
    {
        if (sUserId.Trim().Length == 0)
        {
            String sMessage = "System expired...";
            Response.Redirect("ExpiredPage.aspx");
        }
    }
}
