using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HiddenEventMobile : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String eventType = "";
    public UserProfileModel modUserProfile = new UserProfileModel();

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
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
            Session["userid"] = modUserProfile.GetSetuserid;
            Session["username"] = modUserProfile.GetSetusername;
            Session["useradd"] = modUserProfile.GetSetuseradd;
            Session["usertelno"] = modUserProfile.GetSetusertelno;
            Session["usertype"] = modUserProfile.GetSetusertype;
            Session["userstatus"] = modUserProfile.GetSetuserstatus;

            Session["comp"] = modUserProfile.GetSetcomp;
        }
        if (Request.QueryString["event"] != null)
        {
            eventType = oMainCon.replaceNull(Request.QueryString["event"]);
        }
    }
    private void processValues()
    {
        if (Session["userid"] != null)
        {
            Response.Redirect("AdminCPanel1.aspx?action=OPEN");
        }
        else
        {
            Response.Redirect("NotAuthorizedPage.aspx");
        }
    }
}