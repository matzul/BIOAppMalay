using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CompInfoDetails : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sUserIdComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sUserAction = "";
    public String sActionString = "";
    public String sAlertMessage = "";
    public String sCompId = "";
    public String sInfoNo = "";
    public MainModel oModComp = new MainModel();
    public MainModel oModnfo = new MainModel();
    public ArrayList lsItemImage = new ArrayList();

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
            sUserIdComp = Session["comp"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["compid"] != null)
        {
            sCompId = Request.QueryString["compid"].ToString();
        }
        if (Request.QueryString["compno"] != null)
        {
            sInfoNo = Request.QueryString["compno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        if (sAction.Equals("ADD"))
        {
            oModComp = new MainModel();
        }
        if (sAction.Equals("CREATE"))
        {
            oModComp = new MainModel();
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
            sUserIdComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidCompId") != null)
        {
            sCompId = oMainCon.replaceNull(Request.Params.Get("hidCompId"));
        }
        if (Request.Params.Get("hidUserAction") != null)
        {
            sUserAction = oMainCon.replaceNull(Request.Params.Get("hidUserAction"));
        }
        if (Request.QueryString["compno"] != null)
        {
            sInfoNo = Request.QueryString["compno"].ToString();
        }

        //for reset
        if (sAction.Equals("CREATE"))
        {
            oModnfo = new MainModel();
            oModnfo.GetSetinfo_no = oMainCon.getNextRunningNo(sUserIdComp, "INFO_COMP", "ACTIVE");
            oModnfo.GetSetinfo_type = oMainCon.replaceNull(Request.Params.Get("infoType"));
            oModnfo.GetSetinfo_desc = oMainCon.replaceNull(Request.Params.Get("infoDesc"));
            oModnfo.GetSetinfo_status = oMainCon.replaceNull(Request.Params.Get("infoStatus"));
        }
        else if (sAction.Equals("SAVE"))
        {
            oModnfo = new MainModel();
            oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
            oModnfo.GetSetinfo_type = oMainCon.replaceNull(Request.Params.Get("infoType"));
            oModnfo.GetSetinfo_desc = oMainCon.replaceNull(Request.Params.Get("infoDesc"));
            oModnfo.GetSetinfo_status = oMainCon.replaceNull(Request.Params.Get("infoStatus"));
        }
        else if (sAction.Equals("EDIT"))
        {
            sInfoNo = Request.QueryString["compno"].ToString();
        }
    }

    private void processValues()
    {
        if (sAction.Equals("ADD"))
        {
            sActionString = "MAKLUMAT INFO";
            if (sUserIdComp.Length > 0)
            {
                var url = HttpContext.Current.Server.MapPath("~/Attachment/Info/" + sUserIdComp);
                if (!Directory.Exists(url))
                {
                    Directory.CreateDirectory(url);
                }

                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = new MainModel();
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Info...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = new MainModel();
            }
        }
        else if (sAction.Equals("OPEN")) 
        {
            sActionString = "MAKLUMAT INFO";
            if (sUserIdComp.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
                lsItemImage = oMainCon.getBLOBFile(sUserIdComp, sInfoNo, Server.MapPath("./Attachment/Info/" + sUserIdComp + "/"), "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat membuka maklumat Info...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
            }
        }
        else if (sAction.Equals("CREATE"))
        {
            sActionString = "DAFTAR INFO";
            if (sUserIdComp.Length > 0)
            {
                int i = oMainCon.insertInfoComp(sUserIdComp, oModnfo.GetSetinfo_no, oModnfo.GetSetinfo_type, oModnfo.GetSetinfo_desc, oModnfo.GetSetinfo_status, sUserId);                
                if (i == 1)
                {
                    sAction = "OPEN";
                    sAlertMessage = "SUCCESS|Daftar maklumat Info berjaya...";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
                    oMainCon.updateNextRunningNo(sUserIdComp, "INFO_COMP", "ACTIVE");
                    lsItemImage = oMainCon.getBLOBFile(sUserIdComp, oModnfo.GetSetinfo_no, Server.MapPath("./Attachment/Info/" + sUserIdComp + "/"), "");
                }
                else {
                    sAction = "ADD";
                    sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Info...";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                }
            }
            else
            {
                sAction = "ADD";
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Info...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
            }
        }
        else if (sAction.Equals("EDIT"))
        {
            sActionString = "KEMASKINI INFO";
            if (sUserIdComp.Length > 0)
            {
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
            }
            else
            {
                sAlertMessage = "ERROR|Tidak dapat mengemaskini maklumat Info...";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
            }
        }
        else if (sAction.Equals("SAVE"))
        {
            if (sUserIdComp.Length > 0)
            {
                String sStatus = "Y";
                sAction = "OPEN";

                int z = oMainCon.updateInfoComp(oModnfo.GetSetcomp, oModnfo.GetSetinfo_no, oModnfo.GetSetinfo_type, oModnfo.GetSetinfo_desc, sUserId, oModnfo.GetSetinfo_status);

                if (z == 1)
                {
                    sStatus = "Y";
                    sAlertMessage = "SUCCESS|Maklumat Info disimpan...";
                    sAction = "OPEN";
                    sActionString = "KEMASKINI INFO";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
                    lsItemImage = oMainCon.getBLOBFile(sUserIdComp, sInfoNo, Server.MapPath("./Attachment/Info/" + sUserIdComp + "/"), "");
                }
                else
                {
                    sStatus = "N";
                    sAlertMessage = "ERROR|Maklumat  Info tidak berjaya disimpan...";
                    sAction = "EDIT";
                    sActionString = "KEMASKINI INFO";
                    oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                    oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
                }

            }
            else
            {
                sAlertMessage = "ERROR|Maklumat Info tidak berjaya disimpan...";
                sAction = "EDIT";
                sActionString = "KEMASKINI INFO";
                oModComp = oMainCon.getCompInfoDetails(sUserIdComp);
                oModnfo = oMainCon.getInfoComp(sUserIdComp, sInfoNo, "", "", "", "");
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