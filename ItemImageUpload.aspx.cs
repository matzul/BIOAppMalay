using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemImageUpload : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sActionString = "";
    public String sItemNo = "";
    public String sAlertMessage = "";
    public MainModel oModItem = new MainModel();
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
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["itemno"] != null)
        {
            sItemNo = Request.QueryString["itemno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
    }
    private void processValues()
    {
        if (sAction.Equals("OPEN"))
        {
            sActionString = "";
            oModItem = oMainCon.getItemDetails(sCurrComp, sItemNo);
            lsItemImage = oMainCon.getBLOBFile(sCurrComp, sItemNo, Server.MapPath("./Attachment/"), "");
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void uploadFile()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        string comp = "";
        string userid = "";
        string status = "N";
        string fileName = "";
        string imageurl = "";
        string message = "Internal Server Error!";


        MainController oMainCon = new MainController();
        MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            comp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            userid = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            string comp2 = HttpContext.Current.Request.Form["inputitem[0]"];
            string userid2 = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            var FILE = HttpContext.Current.Request.Files["inputitem[3]"];

            if (comp.Equals(comp2) && userid.Equals(userid2))
            {

                string fileExt = Path.GetExtension(FILE.FileName).ToLower();
                String sFileNameOnly = Path.GetFileName(FILE.FileName);
                fileName = "upload_" + userid + "_" + comp + "_" + itemno + "_" + Path.GetFileName(FILE.FileName);
                if (fileName != string.Empty)
                {
                    //imageurl = "http://websvc.zakatkedah.com.my" + HttpContext.Current.Request.ApplicationPath + "/Attachment/" + fileName;
                    imageurl = "http://localhost:62709/Attachment/" + fileName;
                    FILE.SaveAs(Server.MapPath("./Attachment/") + fileName);
                }
            }else
            {
                message = "invalid comp code & userid!";
            }
            /*
            ArrayList lsFileName = new ArrayList();
            lsFileName = getFileAttached(fileName);
            for (int j = 0; j < lsFileName.Count; j++)
            {
                String sFileNameAndFolder = (String)lsFileName[j];
                oMainCon.storeBLOBFile(sFileNameAndFolder, fileName, userid, cols);
                //To delete file from the folder
                //FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                //filetodelete.Delete();

            }
            */
        }
        object result = new { status = status, filename = fileName, imageurl = imageurl, message = message };

        jsonResponse = new JavaScriptSerializer().Serialize(result);

        //return jsonResponse;
        
        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    public static String storeFile(String itemno, String filename)
    {
        MainController oMainCon = new MainController();
        MainModel modComp = new MainModel();

        HttpContext.Current.Response.ContentType = "text/json";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        object objData = new { result = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        return jsonResponse;
    }

}