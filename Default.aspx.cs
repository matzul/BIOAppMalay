using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public String sUserId = "";
    public String sPassword = "";
    public String sMessage = "";

    public MainController oMainCon = new MainController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
        }
    }
    private void initialValues()
    {
        if (Session["comp"] != null)
        {
            Session["comp"] = null;
        }
        if (Session["userid"] != null)
        {
            Session["userid"] = null;
        }
        if (Session["username"] != null)
        {
            Session["username"] = null;
        }
        if (Session["usertype"] != null)
        {
            Session["usertype"] = null;
        }
        if (Session["userstatus"] != null)
        {
            Session["userstatus"] = null;
        }
        if (Session["fyr"] != null) 
        {
            Session["fyr"] = null;
        }
        if (Request.QueryString["message"] != null)
        {
            sMessage = oMainCon.replaceNull(Request.QueryString["message"]);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            if (Request.Params.Get("txtUserId") != null)
            {
                sUserId = oMainCon.replaceNull(Request.Params.Get("txtUserId"));
            }
            if (Request.Params.Get("txtPassword") != null)
            {
                sPassword = oMainCon.replaceNull(Request.Params.Get("txtPassword"));
            }
            Response.Redirect("HiddenEvent.aspx?event=login&userid="+sUserId+"&password="+sPassword);           
        }
    }
}