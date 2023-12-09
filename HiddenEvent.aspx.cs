using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HiddenEvent : System.Web.UI.Page
{
    
    public MainController oMainCon = new MainController();
    public String eventType = "";
    public String sCurrFyr = "";
    public String sCurrComp = "";
    public String sUserId = "";
    public String sPassword = "";
    public String sMessage = "";
    public String sScreenId = "";
    public UserProfileModel oModUser = new UserProfileModel();
    public MainModel oModMain = new MainModel();
    public MainModel oModComp = new MainModel();

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
        if (Request.QueryString["event"] != null)
        {
            eventType = oMainCon.replaceNull(Request.QueryString["event"]);
        }
        if (eventType.Equals("select_comp"))
        {
            if (Request.QueryString["comp"] != null)
            {
                sCurrComp = oMainCon.replaceNull(Request.QueryString["comp"]);
            }
        }
        if (eventType.Equals("select_fyr"))
        {
            if (Request.QueryString["fyr"] != null)
            {
                sCurrFyr = oMainCon.replaceNull(Request.QueryString["fyr"]);
            }
        }
        if (eventType.Equals("login"))
        {
            if (Request.QueryString["userid"] != null)
            {
                sUserId = oMainCon.replaceNull(Request.QueryString["userid"]);
            }
            if (Request.QueryString["password"] != null)
            {
                sPassword = oMainCon.replaceNull(Request.QueryString["password"]);
            }
        }
        if (eventType.Equals("open_screen"))
        {
            if (Request.QueryString["screenid"] != null)
            {
                sScreenId = oMainCon.replaceNull(Request.QueryString["screenid"]);
            }
        }
        
    }

    private void processValues()
    {
        if (Session["userid"] != null)
        {            
            sUserId = Session["userid"].ToString();

            if (eventType.Equals("select_comp"))
            {
                Session["comp"] = sCurrComp;
                oModUser = oMainCon.getUserProfile("", sUserId, "", "");
                oMainCon.updateUserDetails(oModUser.GetSetcomp, sCurrComp, oModUser.GetSetuserid, oModUser.GetSetuserpwd, oModUser.GetSetusername, oModUser.GetSetuseradd, oModUser.GetSetusertelno, oModUser.GetSetusertype, oModUser.GetSetuserstatus, oModUser.GetSetscreenid);
                Response.Redirect("HiddenEvent.aspx?event=open_screen&screenid="+ oModUser.GetSetscreenid);
                /*
                if (sCurrComp.Equals("CDG"))
                {
                    Response.Redirect("HiddenEvent.aspx?event=Dashboard2");
                }
                else
                {
                    Response.Redirect("HiddenEvent.aspx?event=Dashboard1");
                }
                */
            }

            sCurrComp = Session["comp"].ToString();

            if (eventType.Equals("select_fyr"))
            {
                Session["fyr"] = sCurrFyr;
                oModUser = oMainCon.getUserProfile("", sUserId, "", "");
                Response.Redirect("HiddenEvent.aspx?event=open_screen&screenid=" + oModUser.GetSetscreenid);
                /*
                if (sCurrComp.Equals("CDG"))
                {
                    Response.Redirect("HiddenEvent.aspx?event=Dashboard2");
                }
                else
                {
                    Response.Redirect("HiddenEvent.aspx?event=Dashboard1");
                }
                */
            }

            if (eventType.Equals("login"))
            {
                oModUser = oMainCon.getUserProfile("", sUserId, sPassword, "");
                if (oModUser.GetSetuserid.Trim().Length > 0)
                {
                    Session["userid"] = oModUser.GetSetuserid;
                    Session["username"] = oModUser.GetSetusername;
                    Session["useradd"] = oModUser.GetSetuseradd;
                    Session["usertelno"] = oModUser.GetSetusertelno;
                    Session["usertype"] = oModUser.GetSetusertype;
                    Session["userstatus"] = oModUser.GetSetuserstatus;

                    Session["comp"] = oModUser.GetSetcomp;
                    /*
                    if (oModUser.GetSetcomp.Trim().Length > 0)
                        Session["comp"] = oModUser.GetSetcomp;
                    else
                        Session["comp"] = "CDG";
                    */
                    sCurrComp = Session["comp"].ToString();

                    MainModel oModFYR = oMainCon.getReportFYRYearMonth(sCurrComp, "", "", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
                    Session["fyr"] = oModFYR.GetSetfinanceyear;
                    Session["fmon"] = oModFYR.GetSetfinancemonth;
                    Session["ayr"] = oModFYR.GetSetactualyear;
                    Session["amon"] = oModFYR.GetSetactualmonth;

                    if (oModUser.GetSetusertype.Equals("00") || oModUser.GetSetusertype.Equals("01"))
                    {
                        //Response.Redirect("AdminCPanel1.aspx?action=OPEN");
                    }
                    else
                    {
                        Response.Redirect("HiddenEvent.aspx?event=select_comp&comp=" + Session["comp"]);
                    }
                }
                else
                {
                    sMessage = "Id Pengguna dan Katalaluan salah...";
                    Response.Redirect("Default.aspx?message=" + sMessage);
                }
            }

            if (eventType.Equals("logout"))
            {
                Response.Redirect("Default.aspx");
            }
            /*
            if (eventType.Equals("Dashboard1"))
            {
                Response.Redirect("Dashboard1.aspx");
            }
            if (eventType.Equals("Dashboard2"))
            {
                Response.Redirect("Dashboard2.aspx");
            }
            */
            if (eventType.Equals("open_screen"))
            {
                //check screenid
                oModMain = oMainCon.getUserRoleSreen(sUserId, sCurrComp, sScreenId);
                if (oModMain.GetSetscreenfilename.Length > 0)
                {
                    Response.Redirect(oModMain.GetSetscreenfilename);
                }
                else
                {
                    Response.Redirect("NotAuthorizedPage.aspx");
                }
            }
        }
        else
        {
            if (eventType.Equals("login"))
            {
                oModUser = oMainCon.getUserProfile("", sUserId, sPassword, "");
                if (oModUser.GetSetuserid.Trim().Length > 0 && oModUser.GetSetuserstatus.Equals("A"))
                {
                    oModComp = oMainCon.getCompInfoDetails(oModUser.GetSetcomp);
                    if (oModComp.GetSetstatus.Equals("ACTIVE") || (oModUser.GetSetusertype.Equals("01") && oModUser.GetSetcomp.Equals("T01")))
                    {
                        Session["userid"] = oModUser.GetSetuserid;
                        Session["username"] = oModUser.GetSetusername;
                        Session["useradd"] = oModUser.GetSetuseradd;
                        Session["usertelno"] = oModUser.GetSetusertelno;
                        Session["usertype"] = oModUser.GetSetusertype;
                        Session["userstatus"] = oModUser.GetSetuserstatus;

                        Session["comp"] = oModUser.GetSetcomp;
                        /*
                        if (oModUser.GetSetcomp.Trim().Length > 0)
                            Session["comp"] = oModUser.GetSetcomp;
                        else
                            Session["comp"] = "CDG";
                        */
                        sCurrComp = Session["comp"].ToString();

                        MainModel oModFYR = oMainCon.getReportFYRYearMonth(sCurrComp, "", "", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
                        Session["fyr"] = oModFYR.GetSetfinanceyear;
                        Session["fmon"] = oModFYR.GetSetfinancemonth;
                        Session["ayr"] = oModFYR.GetSetactualyear;
                        Session["amon"] = oModFYR.GetSetactualmonth;

                        if (oModUser.GetSetusertype.Equals("00") || oModUser.GetSetusertype.Equals("01"))
                        {
                            //Response.Redirect("AdminCPanel1.aspx?action=OPEN");
                        }
                        else
                        {
                            Response.Redirect("HiddenEvent.aspx?event=select_comp&comp=" + Session["comp"]);
                        }
                    }
                    else
                    {
                        sMessage = "Default Comp Pengguna Tidak Aktif...";
                        Response.Redirect("Default.aspx?message=" + sMessage);
                    }
                }
                else if (oModUser.GetSetuserid.Trim().Length > 0 && oModUser.GetSetuserstatus.Equals("I"))
                {
                    sMessage = "Id Pengguna Tidak Aktif...";
                    Response.Redirect("Default.aspx?message=" + sMessage);
                }
                else
                { 
                    sMessage = "Id Pengguna dan Katalaluan salah...";
                    Response.Redirect("Default.aspx?message=" + sMessage);
                }
            }
            else
            {
                sMessage = "Sistem Tamat Tempoh...";
                Response.Redirect("Default.aspx?message=" + sMessage);
            }
        }
    }

}