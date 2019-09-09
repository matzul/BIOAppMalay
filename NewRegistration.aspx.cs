using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewRegistration : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sAction = "OPEN";
    public String sActionString = "";

    public String sName = "";
    public String sAddress = "";
    public String sTelNo = "";
    public String sEmail = "";
    public String sPassword = "";
    public String sMessage = "";

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
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
    }

    private void getValues()
    {
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (sAction.Equals("ADD"))
        {
            sName = oMainCon.replaceNull(Request.Params.Get("name"));
            sAddress = oMainCon.replaceNull(Request.Params.Get("address"));
            sTelNo = oMainCon.replaceNull(Request.Params.Get("telno"));
            sEmail = oMainCon.replaceNull(Request.Params.Get("email"));
            sPassword = oMainCon.replaceNull(Request.Params.Get("password"));
        }

    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "";
            UserProfileModel modUser = oMainCon.getUserProfile("", sEmail, "", "");
            if (modUser.GetSetuserid.Trim().Length > 0)
            {
                sAction = "OPEN";
                sMessage = "Email (Id Pengguna) " + sEmail + " telah didaftarkan!";
            }
            else
            {
                int i = oMainCon.createUser("T01", sEmail, sPassword, sName, sAddress, sTelNo, "01", "A", "010010", "01", "sysadmin");
                if (i == 1)
                {
                    sAction = "SUCCESS";
                }
                else
                {
                    sAction = "OPEN";
                    sMessage = "Internal Server Error, Please contact System Supports!";
                }
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