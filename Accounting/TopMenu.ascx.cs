using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_TopMenu : System.Web.UI.UserControl
{
    public AccountingController oAccCon = new AccountingController();

    public String sTabMenu = "";
    public String sTabMenuParent = "";
    public String sTabMenuChild = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack) has to remove in order to let visiting this line every reload page/action button clicked.
        {
            initialValues();
            processValues();
        }
    }
    private void initialValues()
    {
        if (Request.QueryString["sTabMenu"] != null)
        {
            sTabMenu = Request.QueryString["sTabMenu"].ToString();
            if(sTabMenu.Length > 0)
                Session["TAB_MENU"] = sTabMenu;
            else
                if (Session["TAB_MENU"] != null)
                    sTabMenu = Session["TAB_MENU"].ToString();

        }
        else
        {
            if (Session["TAB_MENU"] != null)
            {
                sTabMenu = Session["TAB_MENU"].ToString();
            }
        }
        //oAccCon.WriteToLogFile("sTabMenu: " + sTabMenu);
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
            Response.Redirect("UnauthorizedError.aspx");
        }

    }
    private void processValues()
    {
    }
}