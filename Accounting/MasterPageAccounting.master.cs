using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_MasterPageAccounting : System.Web.UI.MasterPage
{
    public AccountingController oAccCon = new AccountingController();

    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sUserName = "";
    public String sUserType = "";
    public String sUserStatus = "";

    public String sTabMenu = "";
    public String sTabMenuParent = "";
    public String sTabMenuChild = "";

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
            sCurrComp = oAccCon.replaceNull(Session["comp"].ToString());
        else
            Response.Redirect("../ExpiredPage.aspx");

        if (Session["fyr"] != null)
            sCurrFyr = oAccCon.replaceNull(Session["fyr"].ToString());
        if (Session["userid"] != null)
            sUserId = oAccCon.replaceNull(Session["userid"].ToString());
        if (Session["username"] != null)
            sUserName = oAccCon.replaceNull(Session["username"].ToString());
        if (Session["usertype"] != null)
            sUserType = oAccCon.replaceNull(Session["usertype"].ToString());
        if (Session["userstatus"] != null)
            sUserStatus = oAccCon.replaceNull(Session["userstatus"].ToString());

        if (Request.QueryString["sTabMenu"] != null)
        {
            sTabMenu = Request.QueryString["sTabMenu"].ToString();
            Session["TAB_MENU"] = sTabMenu;
        }
        else
        {
            if (Session["TAB_MENU"] != null)
            {
                sTabMenu = Session["TAB_MENU"].ToString();
            }
        }
        if (sTabMenu.Length > 0)
        {
            ArrayList sTM = oAccCon.tokenString(sTabMenu, ".");
            if (sTM.Count > 0)
            {
                sTabMenuParent = sTM[0].ToString();
                sTabMenuChild = sTM[1].ToString();
            }
        }
        else
        {
            //IF null Value, Redirect to Session Expired Screen
            Response.Redirect("../ExpiredPage.aspx");
        }

    }
    private void processValues()
    {
    }
}
