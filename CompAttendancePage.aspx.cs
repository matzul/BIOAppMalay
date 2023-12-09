using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompAttendancePage : System.Web.UI.Page
{
    public HRController oMainCon = new HRController();
    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sAction = "";
    public Int16 jumharitahun = 0, jumharikerja = 0, jumhariperlepasan = 0, jummasuklambat = 0, jumkeluarawal = 0, jumtiadakehadiran = 0;

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
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
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
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }

    }
    private void processValues()
    {
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