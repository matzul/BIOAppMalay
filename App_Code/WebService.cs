using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.Script.Services;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/WebServiceSoap/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    private String TokenNumber = "M05kit0@1";
    private String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

    public MainController oMainCon = new MainController();
    public HRController oHRCon = new HRController();

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public UserProfileModel getUserProfile()
    {
        UserProfileModel modUser = new UserProfileModel();

        modUser.GetSetuserid = "support@bioappsystem.com";
        modUser.GetSetusertype = "support";

        return modUser;
    }

    [WebMethod]
    public string getUserInfoString()
    {
        UserInfo modUser = new UserInfo();

        modUser.GetSetuserid = "support@bioappsystem.com";

        return modUser.GetSetuserid;
    }


    [WebMethod]
    public UserInfo getUserInfo()
    {
        UserInfo modUser = new UserInfo();

        modUser.GetSetuserid = "support@bioappsystem.com";

        return modUser;
    }

    public class UserInfo
    {

        public UserInfo()
        {
        }

        private string userid = "";

        public string GetSetuserid
        {
            get
            {
                string text = userid;
                if (text != null)
                    return text;
                else
                    return string.Empty;
            }
            set
            {
                userid = value;
            }
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
        string message = "Intenal Server Error!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
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
                    fileName = "upload_" + userid + "_" + comp + "_" + itemno + "_" + Path.GetFileName(FILE.FileName);
                    if (fileName != string.Empty)
                    {
                        imageurl = "./Attachment/" + fileName;
                        FILE.SaveAs(Server.MapPath("./Attachment/") + fileName);
                        status = "Y";
                    }
                }
                else
                {
                    message = "invalid comp code & userid!";
                }
            }
        }
        object result = new { status = status, filename = fileName, imageurl = imageurl, message = message };

        jsonResponse = new JavaScriptSerializer().Serialize(result);

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void storeFile()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                string comp = HttpContext.Current.Request.Form["inputitem[0]"];
                string userid = HttpContext.Current.Request.Form["inputitem[1]"];
                string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
                string filename = HttpContext.Current.Request.Form["inputitem[3]"];
                string imgwidth = HttpContext.Current.Request.Form["inputitem[4]"];
                string imgheight = HttpContext.Current.Request.Form["inputitem[5]"];

                if (comp.Equals(sComp) && userid.Equals(sUserId))
                {
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = getFileAttached(filename);
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        if (oMainCon.storeBLOBFile(comp, itemno, userid, sFileNameAndFolder, filename, imgwidth, imgheight).Equals("Y"))
                        {
                            //To delete file from the folder
                            FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                            filetodelete.Delete();
                            sStatus = "Y";
                            sMessage = "";
                        }
                        else
                        {
                            sMessage = "unable to store blob file!";
                        }
                    }
                }
                else
                {
                    sMessage = "invalid comp code & userid!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void deleteFile()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                string comp = HttpContext.Current.Request.Form["inputitem[0]"];
                string userid = HttpContext.Current.Request.Form["inputitem[1]"];
                string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
                string filename = HttpContext.Current.Request.Form["inputitem[3]"];

                if (comp.Equals(sComp) && userid.Equals(sUserId))
                {

                    if (oMainCon.deleteBLOBFile(comp, itemno, userid, filename).Equals("Y"))
                    {
                        //To delete file from the folder
                        ArrayList lsFileName = new ArrayList();
                        lsFileName = getFileAttached(filename);
                        for (int j = 0; j < lsFileName.Count; j++)
                        {
                            String sFileNameAndFolder = (String)lsFileName[j];
                            FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                            filetodelete.Delete();
                        }
                        sStatus = "Y";
                        sMessage = "";
                    }
                    else
                    {
                        sMessage = "unable to delete image file!";
                    }

                }
                else
                {
                    sMessage = "invalid comp code & userid!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void uploadFileAsset()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        string comp = "";
        string userid = "";
        string status = "N";
        string fileName = "";
        string imageurl = "";
        string message = "Intenal Server Error!";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

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
                fileName = "upload_" + userid + "_" + comp + "_" + itemno + "_" + Path.GetFileName(FILE.FileName);
                if (fileName != string.Empty)
                {
                    //imageurl = "http://websvc.zakatkedah.com.my" + HttpContext.Current.Request.ApplicationPath + "/Attachment/" + fileName;
                    //imageurl = "http://localhost:62709/Attachment/" + fileName;
                    imageurl = "./Attachment/" + fileName;
                    FILE.SaveAs(Server.MapPath("./Attachment/") + fileName);
                    status = "Y";
                }
            }
            else
            {
                message = "invalid comp code & userid!";
            }
        }
        object result = new { status = status, filename = fileName, imageurl = imageurl, message = message };

        jsonResponse = new JavaScriptSerializer().Serialize(result);

        //return jsonResponse;

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void storeFileAsset()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            string comp = HttpContext.Current.Request.Form["inputitem[0]"];
            string userid = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            string filename = HttpContext.Current.Request.Form["inputitem[3]"];
            string imgwidth = HttpContext.Current.Request.Form["inputitem[4]"];
            string imgheight = HttpContext.Current.Request.Form["inputitem[5]"];

            if (comp.Equals(sComp) && userid.Equals(sUserId))
            {
                ArrayList lsFileName = new ArrayList();
                lsFileName = getFileAttached(filename);
                for (int j = 0; j < lsFileName.Count; j++)
                {
                    String sFileNameAndFolder = (String)lsFileName[j];
                    if (oMainCon.storeBLOBFileAsset(comp, itemno, userid, sFileNameAndFolder, filename, imgwidth, imgheight).Equals("Y"))
                    {
                        //To delete file from the folder
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                        sStatus = "Y";
                        sMessage = "";
                    }
                    else
                    {
                        sMessage = "unable to store blob file!";
                    }
                }
            }
            else
            {
                sMessage = "invalid comp code & userid!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void deleteFileAsset()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            string comp = HttpContext.Current.Request.Form["inputitem[0]"];
            string userid = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            string filename = HttpContext.Current.Request.Form["inputitem[3]"];

            if (comp.Equals(sComp) && userid.Equals(sUserId))
            {

                if (oMainCon.deleteBLOBFileAsset(comp, itemno, userid, filename).Equals("Y"))
                {
                    //To delete file from the folder
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = getFileAttached(filename);
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sMessage = "unable to delete image file!";
                }

            }
            else
            {
                sMessage = "invalid comp code & userid!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = new JavaScriptSerializer().Serialize(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    public ArrayList getFileAttached(String sFileName)
    {
        ArrayList lsFileAttached = new ArrayList();
        try
        {
            String sFilePath = Server.MapPath("./Attachment/");
            String[] filePaths = Directory.GetFiles(sFilePath, sFileName);
            for (int i = 0; i < filePaths.Length; i++)
            {
                //lsFileAttached.Add(Path.GetFileName(filePaths[i].ToString()));
                lsFileAttached.Add(Path.GetFullPath(filePaths[i].ToString()));
            }
        }
        catch (Exception e)
        {
            oMainCon.WriteToLogFile(DateTime.Now.ToString() + ": [WebService.cs:getFileAttached()] " + e.Message.ToString());
        }
        return lsFileAttached;
    }

    public ArrayList getFileAttached(String sFileName, String serverMapPath)
    {
        ArrayList lsFileAttached = new ArrayList();
        try
        {
            String sFilePath = serverMapPath;
            String[] filePaths = Directory.GetFiles(sFilePath, sFileName);
            for (int i = 0; i < filePaths.Length; i++)
            {
                //lsFileAttached.Add(Path.GetFileName(filePaths[i].ToString()));
                lsFileAttached.Add(Path.GetFullPath(filePaths[i].ToString()));
            }
        }
        catch (Exception e)
        {
            oMainCon.WriteToLogFile(DateTime.Now.ToString() + ": [WebService.cs:getFileAttached()] " + e.Message.ToString());
        }
        return lsFileAttached;
    }

    //for human resource - matzul 22/06/2022
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void insertStaffExcuse()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sComp = "";
        String sUserId = "";
        String sStatus = "N";
        String sFileName1 = "";
        String sFileName2 = "";
        String sFileName3 = "";
        String sMessage = "Intenal Server Error!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = sComp;
                modItem.GetSetfyr = HttpContext.Current.Request.Form["inputitem[0]"];
                modItem.GetSetstaffno = HttpContext.Current.Request.Form["inputitem[1]"];
                modItem.GetSetcat = HttpContext.Current.Request.Form["inputitem[2]"];
                modItem.GetSettype = HttpContext.Current.Request.Form["inputitem[3]"];
                modItem.GetSetfromdate = HttpContext.Current.Request.Form["inputitem[4]"];
                modItem.GetSettodate = HttpContext.Current.Request.Form["inputitem[5]"];
                modItem.GetSetstatus = HttpContext.Current.Request.Form["inputitem[6]"];
                modItem.GetSetreason = HttpContext.Current.Request.Form["inputitem[7]"];
                modItem.GetSetfromtime = HttpContext.Current.Request.Form["inputitem[11]"];
                modItem.GetSettotime = HttpContext.Current.Request.Form["inputitem[12]"];
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertStaffExcuseDetails(modItem);
                if (result.Equals("Y"))
                {
                    modItem = oHRCon.getStaffExcuseDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, modItem.GetSetcat, modItem.GetSettype, modItem.GetSetfromdate, modItem.GetSettodate, 0, Server.MapPath("./Attachment/HumanResource/"));

                    var FILE1 = HttpContext.Current.Request.Files["inputitem[8]"];
                    if (Path.GetFileName(FILE1 == null ? "" : FILE1.FileName) != string.Empty)
                    {
                        sFileName1 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE1.FileName);
                        FILE1.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName1);
                    }

                    var FILE2 = HttpContext.Current.Request.Files["inputitem[9]"];
                    if (Path.GetFileName(FILE2 == null ? "" : FILE2.FileName) != string.Empty)
                    {
                        sFileName2 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE2.FileName);
                        FILE2.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName2);
                    }

                    var FILE3 = HttpContext.Current.Request.Files["inputitem[10]"];
                    if (Path.GetFileName(FILE3 == null ? "" : FILE3.FileName) != string.Empty)
                    {
                        sFileName3 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE3.FileName);
                        FILE3.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName3);
                    }
                    //update table together with blob file
                    result = oHRCon.updateStaffExcuseAttachment(modItem, Server.MapPath("./Attachment/HumanResource/"), sFileName1, sFileName2, sFileName3);

                    //insert staff exception day
                    result = oHRCon.insertStaffExceptionDetails(modItem, "EXCUSE");

                    sStatus = result;
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void updateStaffExcuse()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sComp = "";
        String sUserId = "";
        String sStatus = "N";
        String sFileName1 = "";
        String sFileName2 = "";
        String sFileName3 = "";
        String sMessage = "Intenal Server Error!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                String exc_id = HttpContext.Current.Request.Form["inputitem[0]"];
                if (exc_id.Length > 0)
                {
                    HRModel modItem = oHRCon.getStaffExcuseDetails(sComp, "", "", "", "", "", "", Int64.Parse(exc_id), Server.MapPath("./Attachment/HumanResource/"));
                    if (modItem.GetSetid > 0)
                    {
                        modItem.GetSetcomp = sComp;
                        modItem.GetSetstaffno = HttpContext.Current.Request.Form["inputitem[1]"];
                        modItem.GetSetcat = HttpContext.Current.Request.Form["inputitem[2]"];
                        modItem.GetSettype = HttpContext.Current.Request.Form["inputitem[3]"];
                        modItem.GetSetfromdate = HttpContext.Current.Request.Form["inputitem[4]"];
                        modItem.GetSettodate = HttpContext.Current.Request.Form["inputitem[5]"];
                        modItem.GetSetstatus = HttpContext.Current.Request.Form["inputitem[6]"];
                        modItem.GetSetreason = HttpContext.Current.Request.Form["inputitem[7]"];
                        modItem.GetSetfromtime = HttpContext.Current.Request.Form["inputitem[11]"];
                        modItem.GetSettotime = HttpContext.Current.Request.Form["inputitem[12]"];
                        if (modItem.GetSetstatus.Equals("PERMOHONAN"))
                        {
                            modItem.GetSetmodifiedby = sUserId;
                        }
                        else if (modItem.GetSetstatus.Equals("PENGESAHAN"))
                        {
                            modItem.GetSetreliefedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("LULUS"))
                        {
                            modItem.GetSetapprovedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("DITOLAK"))
                        {
                            modItem.GetSetrejectedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("BATAL"))
                        {
                            modItem.GetSetcancelledby = sUserId;

                        }

                        var FILE1 = HttpContext.Current.Request.Files["inputitem[8]"];
                        if (Path.GetFileName(FILE1 == null ? "" : FILE1.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename1.Equals(FILE1.FileName))
                            {
                                sFileName1 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE1.FileName);
                                FILE1.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName1);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename1, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName1 = modItem.GetSetfilename1;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename1.Length > 0)
                            {
                                sFileName1 = modItem.GetSetfilename1;
                            }
                        }

                        var FILE2 = HttpContext.Current.Request.Files["inputitem[9]"];
                        if (Path.GetFileName(FILE2 == null ? "" : FILE2.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename2.Equals(FILE2.FileName))
                            {
                                sFileName2 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE2.FileName);
                                FILE2.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName2);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename2, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName2 = modItem.GetSetfilename2;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename2.Length > 0)
                            {
                                sFileName2 = modItem.GetSetfilename2;
                            }
                        }

                        var FILE3 = HttpContext.Current.Request.Files["inputitem[10]"];
                        if (Path.GetFileName(FILE3 == null ? "" : FILE3.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename3.Equals(FILE3.FileName))
                            {
                                sFileName3 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE3.FileName);
                                FILE3.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName3);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename3, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName3 = modItem.GetSetfilename3;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename3.Length > 0)
                            {
                                sFileName3 = modItem.GetSetfilename3;
                            }
                        }

                        //update table staff excuse only
                        String result = oHRCon.updateStaffExcuseDetails(modItem);

                        //delete staff exception day - old data
                        result = oHRCon.deleteStaffExceptionDetails(modItem, "EXCUSE");

                        //insert staff exception day - new data with new date
                        result = oHRCon.insertStaffExceptionDetails(modItem, "EXCUSE");

                        //update table together with blob file
                        result = oHRCon.updateStaffExcuseAttachment(modItem, Server.MapPath("./Attachment/HumanResource/"), sFileName1, sFileName2, sFileName3);

                        sStatus = result;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "record not found!";

                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "exc_id is not passing!";
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void insertStaffLeave()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sComp = "";
        String sUserId = "";
        String sStatus = "N";
        String sFileName1 = "";
        String sFileName2 = "";
        String sFileName3 = "";
        String sMessage = "Intenal Server Error!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                HRModel modItem = new HRModel();
                modItem.GetSetcomp = sComp;
                modItem.GetSetfyr = HttpContext.Current.Request.Form["inputitem[0]"];
                modItem.GetSetstaffno = HttpContext.Current.Request.Form["inputitem[1]"];
                modItem.GetSetcat = HttpContext.Current.Request.Form["inputitem[2]"];
                modItem.GetSettype = HttpContext.Current.Request.Form["inputitem[3]"];
                modItem.GetSetfromdate = HttpContext.Current.Request.Form["inputitem[4]"];
                modItem.GetSettodate = HttpContext.Current.Request.Form["inputitem[5]"];
                modItem.GetSetstatus = HttpContext.Current.Request.Form["inputitem[6]"];
                modItem.GetSetreason = HttpContext.Current.Request.Form["inputitem[7]"];
                modItem.GetSetfromtime = HttpContext.Current.Request.Form["inputitem[11]"];
                modItem.GetSettotime = HttpContext.Current.Request.Form["inputitem[12]"];
                modItem.GetSetvariety = (HttpContext.Current.Request.Form["inputitem[13]"].Length > 0 ? int.Parse(HttpContext.Current.Request.Form["inputitem[13]"]) : 1); 
                modItem.GetSetcreatedby = sUserId;
                String result = oHRCon.insertStaffLeaveDetails(modItem);
                if (result.Equals("Y"))
                {
                    modItem = oHRCon.getStaffLeaveDetails(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetstaffno, modItem.GetSetcat, modItem.GetSettype, modItem.GetSetfromdate, modItem.GetSettodate, 0, Server.MapPath("./Attachment/HumanResource/"));

                    var FILE1 = HttpContext.Current.Request.Files["inputitem[8]"];
                    if (Path.GetFileName(FILE1 == null ? "" : FILE1.FileName) != string.Empty)
                    {
                        sFileName1 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE1.FileName);
                        FILE1.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName1);
                    }

                    var FILE2 = HttpContext.Current.Request.Files["inputitem[9]"];
                    if (Path.GetFileName(FILE2 == null ? "" : FILE2.FileName) != string.Empty)
                    {
                        sFileName2 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE2.FileName);
                        FILE2.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName2);
                    }

                    var FILE3 = HttpContext.Current.Request.Files["inputitem[10]"];
                    if (Path.GetFileName(FILE3 == null ? "" : FILE3.FileName) != string.Empty)
                    {
                        sFileName3 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE3.FileName);
                        FILE3.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName3);
                    }
                    //update table together with blob file
                    result = oHRCon.updateStaffLeaveAttachment(modItem, Server.MapPath("./Attachment/HumanResource/"), sFileName1, sFileName2, sFileName3);

                    //insert staff exception day
                    result = oHRCon.insertStaffExceptionDetails(modItem, "LEAVE");

                    sStatus = result;
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void updateStaffLeave()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sComp = "";
        String sUserId = "";
        String sStatus = "N";
        String sFileName1 = "";
        String sFileName2 = "";
        String sFileName3 = "";
        String sMessage = "Intenal Server Error!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (HttpContext.Current.Session["comp"] != null)
            {
                sComp = HttpContext.Current.Session["comp"].ToString();
            }

            if (HttpContext.Current.Session["userid"] != null)
            {
                sUserId = HttpContext.Current.Session["userid"].ToString();
            }

            if (HttpContext.Current.Request.Form.AllKeys.Any())
            {
                String leave_id = HttpContext.Current.Request.Form["inputitem[0]"];
                if (leave_id.Length > 0)
                {
                    HRModel modItem = oHRCon.getStaffLeaveDetails(sComp, "", "", "", "", "", "", Int64.Parse(leave_id), Server.MapPath("./Attachment/HumanResource/"));
                    if (modItem.GetSetid > 0)
                    {
                        modItem.GetSetcomp = sComp;
                        modItem.GetSetstaffno = HttpContext.Current.Request.Form["inputitem[1]"];
                        modItem.GetSetcat = HttpContext.Current.Request.Form["inputitem[2]"];
                        modItem.GetSettype = HttpContext.Current.Request.Form["inputitem[3]"];
                        modItem.GetSetfromdate = HttpContext.Current.Request.Form["inputitem[4]"];
                        modItem.GetSettodate = HttpContext.Current.Request.Form["inputitem[5]"];
                        modItem.GetSetstatus = HttpContext.Current.Request.Form["inputitem[6]"];
                        modItem.GetSetreason = HttpContext.Current.Request.Form["inputitem[7]"];
                        modItem.GetSetfromtime = HttpContext.Current.Request.Form["inputitem[11]"];
                        modItem.GetSettotime = HttpContext.Current.Request.Form["inputitem[12]"];
                        modItem.GetSetvariety = (HttpContext.Current.Request.Form["inputitem[13]"].Length > 0 ? int.Parse(HttpContext.Current.Request.Form["inputitem[13]"]) : 1);
                        if (modItem.GetSetstatus.Equals("PERMOHONAN"))
                        {
                            modItem.GetSetmodifiedby = sUserId;
                        }
                        else if (modItem.GetSetstatus.Equals("PENGESAHAN"))
                        {
                            modItem.GetSetreliefedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("LULUS"))
                        {
                            modItem.GetSetapprovedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("DITOLAK"))
                        {
                            modItem.GetSetrejectedby = sUserId;

                        }
                        else if (modItem.GetSetstatus.Equals("BATAL"))
                        {
                            modItem.GetSetcancelledby = sUserId;

                        }

                        var FILE1 = HttpContext.Current.Request.Files["inputitem[8]"];
                        if (Path.GetFileName(FILE1 == null ? "" : FILE1.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename1.Equals(FILE1.FileName))
                            {
                                sFileName1 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE1.FileName);
                                FILE1.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName1);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename1, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName1 = modItem.GetSetfilename1;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename1.Length > 0)
                            {
                                sFileName1 = modItem.GetSetfilename1;
                            }
                        }

                        var FILE2 = HttpContext.Current.Request.Files["inputitem[9]"];
                        if (Path.GetFileName(FILE2 == null ? "" : FILE2.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename2.Equals(FILE2.FileName))
                            {
                                sFileName2 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE2.FileName);
                                FILE2.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName2);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename2, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName2 = modItem.GetSetfilename2;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename2.Length > 0)
                            {
                                sFileName2 = modItem.GetSetfilename2;
                            }
                        }

                        var FILE3 = HttpContext.Current.Request.Files["inputitem[10]"];
                        if (Path.GetFileName(FILE3 == null ? "" : FILE3.FileName) != string.Empty)
                        {
                            if (!modItem.GetSetfilename3.Equals(FILE3.FileName))
                            {
                                sFileName3 = modItem.GetSetcomp + "_" + modItem.GetSetfyr + "_" + modItem.GetSetstaffno + "_" + modItem.GetSetid + "_" + Path.GetFileName(FILE3.FileName);
                                FILE3.SaveAs(Server.MapPath("./Attachment/HumanResource/") + sFileName3);

                                //To delete file from the folder
                                ArrayList lsFileName = new ArrayList();
                                lsFileName = getFileAttached(modItem.GetSetfilename3, Server.MapPath("./Attachment/HumanResource/"));
                                for (int j = 0; j < lsFileName.Count; j++)
                                {
                                    String sFileNameAndFolder = (String)lsFileName[j];
                                    FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                                    filetodelete.Delete();
                                }
                            }
                            else
                            {
                                sFileName3 = modItem.GetSetfilename3;
                            }
                        }
                        else
                        {
                            if (modItem.GetSetfilename3.Length > 0)
                            {
                                sFileName3 = modItem.GetSetfilename3;
                            }
                        }

                        //update table staff excuse only
                        String result = oHRCon.updateStaffLeaveDetails(modItem);

                        //delete staff exception day - old data
                        result = oHRCon.deleteStaffExceptionDetails(modItem, "LEAVE");

                        //insert staff exception day - new data with new date
                        result = oHRCon.insertStaffExceptionDetails(modItem, "LEAVE");

                        //update table together with blob file
                        result = oHRCon.updateStaffLeaveAttachment(modItem, Server.MapPath("./Attachment/HumanResource/"), sFileName1, sFileName2, sFileName3);

                        sStatus = result;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "record not found!";

                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "exc_id is not passing!";
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };

        jsonResponse = new JavaScriptSerializer().Serialize(objData);

        HttpContext.Current.Response.Write(jsonResponse);

    }

    // Begin add - fakhrul 04/09/2020

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void uploadFile2()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        string sComp = "";
        string sUserId = "";
        string status = "N";
        string fileName = "";
        string imageurl = "";
        string message = "Intenal Server Error!";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            String comp = HttpContext.Current.Request.Form["inputitem[0]"];
            String userid = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            var FILE = HttpContext.Current.Request.Files["inputitem[3]"];
            string dir = HttpContext.Current.Request.Form["inputitem[4]"];

            //if (comp.Equals(comp2) && userid.Equals(userid2))
            if (comp.Trim().Length > 0 && itemno.Trim().Length > 0)
            {
                //fileName = "upload_" + userid + "_" + comp + "_" + itemno + "_" + Path.GetFileName(FILE.FileName);
                fileName = "upload_" + userid + "_" + itemno + "_" + Path.GetFileName(FILE.FileName);
                if (fileName != string.Empty)
                {
                    imageurl = ConfigurationSettings.AppSettings["imageurl"] + dir + fileName;
                    FILE.SaveAs(Server.MapPath("./Attachment/" + dir) + fileName);
                    status = "Y";
                }
            }
            else
            {
                message = "invalid comp code & itemno!";
            }
        }
        object result = new { status = status, filename = fileName, imageurl = imageurl, message = message };

        jsonResponse = convertJson(result);

        //return jsonResponse;

        HttpContext.Current.Response.Write(jsonResponse);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void storeFile2()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            string comp = HttpContext.Current.Request.Form["inputitem[0]"];
            string userid = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            string filename = HttpContext.Current.Request.Form["inputitem[3]"];
            string imgwidth = HttpContext.Current.Request.Form["inputitem[4]"];
            string imgheight = HttpContext.Current.Request.Form["inputitem[5]"];
            string dir = HttpContext.Current.Request.Form["inputitem[6]"];

            //if (comp.Equals(sComp) && userid.Equals(sUserId))
            if (comp.Trim().Length > 0 && itemno.Trim().Length > 0)
            {
                ArrayList lsFileName = new ArrayList();
                lsFileName = getFileAttached2(dir, filename);
                for (int j = 0; j < lsFileName.Count; j++)
                {
                    String sFileNameAndFolder = (String)lsFileName[j];
                    if (oMainCon.storeBLOBFile(comp, itemno, userid, sFileNameAndFolder, filename, imgwidth, imgheight).Equals("Y"))
                    {
                        //To delete file from the folder
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                        sStatus = "Y";
                        sMessage = "";
                    }
                    else
                    {
                        sMessage = "unable to store blob file!";
                    }
                }
            }
            else
            {
                sMessage = "invalid comp code & itemno!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = convertJson(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void deleteFile2()
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";

        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        String sComp = "";
        String sUserId = "";

        //MainController oMainCon = new MainController();
        //MainModel modComp = new MainModel();

        if (HttpContext.Current.Session["comp"] != null)
        {
            sComp = HttpContext.Current.Session["comp"].ToString();
        }

        if (HttpContext.Current.Session["userid"] != null)
        {
            sUserId = HttpContext.Current.Session["userid"].ToString();
        }

        if (HttpContext.Current.Request.Form.AllKeys.Any())
        {
            string comp = HttpContext.Current.Request.Form["inputitem[0]"];
            string userid = HttpContext.Current.Request.Form["inputitem[1]"];
            string itemno = HttpContext.Current.Request.Form["inputitem[2]"];
            string filename = HttpContext.Current.Request.Form["inputitem[3]"];
            string dir = HttpContext.Current.Request.Form["inputitem[4]"];

            //if (comp.Equals(sComp) && userid.Equals(sUserId))
            if (comp.Trim().Length > 0 && itemno.Trim().Length > 0)
            {

                if (oMainCon.deleteBLOBFile(comp, itemno, userid, filename).Equals("Y"))
                {
                    //To delete file from the folder
                    ArrayList lsFileName = new ArrayList();
                    lsFileName = getFileAttached2(dir, filename);
                    for (int j = 0; j < lsFileName.Count; j++)
                    {
                        String sFileNameAndFolder = (String)lsFileName[j];
                        FileInfo filetodelete = new FileInfo(sFileNameAndFolder);
                        filetodelete.Delete();
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sMessage = "unable to delete image file!";
                }

            }
            else
            {
                sMessage = "invalid comp code & itemno!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        jsonResponse = convertJson(objData);
        HttpContext.Current.Response.Write(jsonResponse);
    }

    public ArrayList getFileAttached2(String sDir, String sFileName)
    {
        ArrayList lsFileAttached = new ArrayList();
        try
        {
            String sFilePath = Server.MapPath("./Attachment/" + sDir);
            String[] filePaths = Directory.GetFiles(sFilePath, sFileName);
            for (int i = 0; i < filePaths.Length; i++)
            {
                //lsFileAttached.Add(Path.GetFileName(filePaths[i].ToString()));
                lsFileAttached.Add(Path.GetFullPath(filePaths[i].ToString()));
            }
        }
        catch (Exception e)
        {
            oMainCon.WriteToLogFile(DateTime.Now.ToString() + ": [WebService.cs:getFileAttached2()] " + e.Message.ToString());
        }
        return lsFileAttached;
    }

    //for mobile apps - matzul 11/06/2019

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_UserProfile(String userid)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        UserProfileModel modUserProfile = new UserProfileModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modUserProfile = new UserProfileModel();
            if (userid.Trim().Length > 0)
            {
                modUserProfile = oMainCon.getUserProfile("", userid, "", "");
            }
        }

        jsonResponse = new JavaScriptSerializer().Serialize(modUserProfile);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_UserProfileLogin(String userid, String password)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        UserProfileModel modUserProfile = new UserProfileModel();
        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (userid.Trim().Length > 0)
            {
                modUserProfile = oMainCon.getUserProfile("", userid, password, "");
            }
        }
        jsonResponse = new JavaScriptSerializer().Serialize(modUserProfile);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_BPList(String comp, String bpid, String bpdesc, String bpcat, String solidbp)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsBPList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsBPList = oMainCon.getBPList(comp, bpid, bpdesc, bpcat, solidbp);
        }

        jsonResponse = convertJson(lsBPList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsBPList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_BPListAutocomplete(String comp, String bpid, String bpdesc, String bpcat, String solidbp)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList objDataList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsBPList = oMainCon.getBPList(comp, bpid, bpdesc, bpcat, solidbp);
            for (int i = 0; i < lsBPList.Count; i++)
            {
                MainModel modData = (MainModel)lsBPList[i];

                object objData = new { modData.GetSetbpid, modData.GetSetbpdesc };
                objDataList.Add(objData);
            }
        }

        jsonResponse = convertJson(objDataList);
        //jsonResponse = new JavaScriptSerializer().Serialize(objDataList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_BPListing(String comp, String desc, String bpid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsBP = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsBP = oMainCon.getBPList(comp, bpid, desc, "");
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsBP);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_BPDetails(String comp, String bpid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modBP = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            modBP = oMainCon.getBPDetails(comp, bpid, "");
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(modBP);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_BPDetails(String comp, String bpid, String bpdesc, String bpaddress, String bpcontact, String bpcat, String discounttype, String cashguarantee, String bankguarantee, String creditlimit, String bpstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (bpid.Length == 0)
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetbpid = oMainCon.getNextRunningNo(comp, "BUSINESS_PARTNER", "ACTIVE");
                oModHeader.GetSetbpdesc = bpdesc;
                oModHeader.GetSetbpaddress = bpaddress;
                oModHeader.GetSetbpcontact = bpcontact;
                oModHeader.GetSetbpcat = bpcat;
                oModHeader.GetSetdiscounttype = discounttype;
                oModHeader.GetSetcashguarantee = Convert.ToDouble(cashguarantee);
                oModHeader.GetSetbankguarantee = Convert.ToDouble(bankguarantee);
                oModHeader.GetSetcreditlimit = Convert.ToDouble(creditlimit);
                oModHeader.GetSetbpreference = "";
                oModHeader.GetSetbpstatus = bpstatus;

                MainModel bpdetails = oMainCon.getBPDetails(comp, "", bpdesc);
                if (bpdetails.GetSetbpid.Trim().Length == 0)
                {
                    if (oMainCon.insertBusinessPartner(oModHeader).Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(comp, "BUSINESS_PARTNER", "ACTIVE");
                        sStatus = "Y";
                        sMessage = oModHeader.GetSetbpid;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Pendaftaran Tidak Berjaya!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Pembekal/ Pelanggan ini telah didaftarkan!";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Pendaftaran Tidak Berjaya! Internal Server Error...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_BPDetails(String comp, String bpid, String bpdesc, String bpaddress, String bpcontact, String bpcat, String discounttype, String cashguarantee, String bankguarantee, String creditlimit, String bpstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (bpid.Length > 0)
            {
                MainModel bpdetails = oMainCon.getBPDetails(comp, bpid);
                if (bpdetails.GetSetbpid.Trim().Length > 0)
                {
                    bpdetails.GetSetbpdesc = bpdesc;
                    bpdetails.GetSetbpaddress = bpaddress;
                    bpdetails.GetSetbpcontact = bpcontact;
                    bpdetails.GetSetbpcat = bpcat;
                    bpdetails.GetSetdiscounttype = discounttype;
                    bpdetails.GetSetcashguarantee = Convert.ToDouble(cashguarantee);
                    bpdetails.GetSetbankguarantee = Convert.ToDouble(bankguarantee);
                    bpdetails.GetSetcreditlimit = Convert.ToDouble(creditlimit);
                    bpdetails.GetSetbpstatus = bpstatus;

                    if (oMainCon.updateBusinessPartner(bpdetails).Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = bpdetails.GetSetbpid;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini Tidak Berjaya!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini Tidak Berjaya! Internal Server Error...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini Tidak Berjaya! No BPId is provided...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ItemList(String comp, String itemno, String ordercat, String ordertype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsItemList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsItemList = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
        }

        jsonResponse = convertJson(lsItemList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsItemList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ItemListAutocomplete(String comp, String itemno, String ordercat, String ordertype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList objDataList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsItemList = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
            for (int i = 0; i < lsItemList.Count; i++)
            {
                MainModel modData = (MainModel)lsItemList[i];

                object objData = new { modData.GetSetitemno, modData.GetSetitemdesc };
                objDataList.Add(objData);
            }
        }

        jsonResponse = convertJson(objDataList);
        //jsonResponse = new JavaScriptSerializer().Serialize(objDataList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ItemListing(String comp, String desc, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsItem = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsItem = oMainCon.getItemList(comp, itemno, desc, "");
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsItem);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ItemDetails(String comp, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modItem = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            modItem = oMainCon.getItemDetails(comp, itemno);
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(modItem);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ItemDetails(String comp, String itemno, String itemdesc, String itemcat, String itemtype, double purchaseprice, double costprice, double salesprice, String itemstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (itemno.Length != 0)
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetitemno = itemno;
                oModHeader.GetSetitemdesc = itemdesc;
                oModHeader.GetSetitemcat = itemcat;
                oModHeader.GetSetitemtype = itemtype;
                oModHeader.GetSetpurchaseprice = Convert.ToDouble(purchaseprice);
                oModHeader.GetSetcostprice = Convert.ToDouble(costprice);
                oModHeader.GetSetsalesprice = Convert.ToDouble(salesprice);
                oModHeader.GetSetitemstatus = itemstatus;

                MainModel itemdetails = oMainCon.getItemDetails(comp, itemno);
                if (itemdetails.GetSetitemno.Trim().Length == 0)
                {
                    if (oMainCon.insertItemMaster(oModHeader).Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = oModHeader.GetSetitemno;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Pendaftaran Tidak Berjaya!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Item/ Produk ini telah didaftarkan!";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Pendaftaran Tidak Berjaya! Internal Server Error...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ItemDetails(String comp, String itemno, String itemdesc, String itemcat, String itemtype, double purchaseprice, double costprice, double salesprice, String itemstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (itemno.Length > 0)
            {
                MainModel itemdetails = oMainCon.getItemDetails(comp, itemno);
                if (itemdetails.GetSetitemno.Trim().Length > 0)
                {
                    itemdetails.GetSetitemdesc = itemdesc;
                    itemdetails.GetSetitemcat = itemcat;
                    itemdetails.GetSetitemtype = itemtype;
                    itemdetails.GetSetpurchaseprice = Convert.ToDouble(purchaseprice);
                    itemdetails.GetSetcostprice = Convert.ToDouble(costprice);
                    itemdetails.GetSetsalesprice = Convert.ToDouble(salesprice);
                    itemdetails.GetSetbpstatus = itemstatus;

                    if (oMainCon.updateItemMaster(itemdetails).Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = itemdetails.GetSetitemno;
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Kemaskini Tidak Berjaya!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Kemaskini Tidak Berjaya! Internal Server Error...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Kemaskini Tidak Berjaya! No ItemNo is provided...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ItemDiscountListing(String comp, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsItem = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsItem = oMainCon.getItemDiscountList(comp, "", "", itemno);
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsItem);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ItemDiscount(String comp, String itemno, String ordercat, String disctype, String disccat, double discvalue, String discstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (itemno.Length != 0)
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetitemno = itemno;
                oModLineItem.GetSetordercat = ordercat;
                oModLineItem.GetSetdiscounttype = disctype;
                oModLineItem.GetSetdisccat = disccat;
                oModLineItem.GetSetdiscvalue = discvalue;
                oModLineItem.GetSetstatus = discstatus;

                ArrayList lsItem = oMainCon.getItemDiscountList(comp, ordercat, disctype, itemno);
                if (lsItem.Count == 0)
                {
                    if (oMainCon.insertItemDiscount(oModLineItem).Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Jadual Harga Item berjaya ditambah!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Jadual Harga Item tidak berjaya ditambah!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Jadual Harga Item tidak berjaya ditambah! Internal Server Error...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Jadual Harga Item tidak berjaya ditambah! No ItemNo is provided...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ItemDiscount(String comp, String itemno, String ordercat, String disctype, String disccat, double discvalue, String discstatus)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "Y";
        String sMessage = "";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            if (itemno.Length > 0)
            {
                ArrayList lsItem = oMainCon.getItemDiscountList(comp, ordercat, disctype, itemno);
                if (lsItem.Count  > 0)
                {
                    MainModel oModLineItem = new MainModel();
                    oModLineItem.GetSetcomp = comp;
                    oModLineItem.GetSetitemno = itemno;
                    oModLineItem.GetSetordercat = ordercat;
                    oModLineItem.GetSetdiscounttype = disctype;
                    oModLineItem.GetSetdisccat = disccat;
                    oModLineItem.GetSetdiscvalue = discvalue;
                    oModLineItem.GetSetstatus = discstatus;

                    if (oMainCon.updateItemDiscount(oModLineItem).Equals("Y"))
                    {
                        sStatus = "Y";
                        sMessage = "Jadual Harga Item berjaya dikemaskini!";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Jadual Harga Item tidak berjaya dikemaskini!";
                    }
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Jadual Harga Item tidak berjaya dikemaskini! Internal Server Error...";
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Jadual Harga Item tidak berjaya dikemaskini! No ItemNo is provided...";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string deleteMobile_ItemDiscount(String comp, String itemno, String ordercat, String disctype)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            //get item details if exist
            ArrayList lsItem = oMainCon.getItemDiscountList(comp, ordercat, disctype, itemno);
            if (lsItem.Count > 0)
            {
                MainModel oLineItem = (MainModel)lsItem[lsItem.Count-1];
                if (oMainCon.deleteItemDiscount(oLineItem).Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_YearMonthList(String comp)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String currYear = DateTime.Now.ToString("yyyy");
        String currMonth = DateTime.Now.ToString("MM");


        ArrayList lsYear = new ArrayList();
        object objYear = new { yearid = "2018", yearval = "2018" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2019", yearval = "2019" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2020", yearval = "2020" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2021", yearval = "2021" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2022", yearval = "2022" };
        lsYear.Add(objYear);
        objYear = new { yearid = "2023", yearval = "2023" };
        lsYear.Add(objYear);

        ArrayList lsMonth = new ArrayList();
        object objMonth = new { monthid = "", monthval = "-Bulan-" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "01", monthval = "Januari" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "02", monthval = "Februari" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "03", monthval = "Mac" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "04", monthval = "April" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "05", monthval = "Mei" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "06", monthval = "Jun" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "07", monthval = "Julai" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "08", monthval = "Ogos" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "09", monthval = "September" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "10", monthval = "Oktober" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "11", monthval = "November" };
        lsMonth.Add(objMonth);
        objMonth = new { monthid = "12", monthval = "Disember" };
        lsMonth.Add(objMonth);

        object objYearMonthList = new { currentyear = currYear, currentmonth = currMonth, arrayyear = lsYear, arraymonth = lsMonth };

        jsonResponse = convertJson(objYearMonthList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsBPList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_DayList(String comp, String year, String month)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String currDay = DateTime.Now.ToString("dd");

        ArrayList lsDay = new ArrayList();
        object objDay = new { dayid = "", dayval = "-Hari-" };
        lsDay.Add(objDay);

        object objDayList = new { currentday = currDay, arrayday = lsDay };

        jsonResponse = convertJson(objDayList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsBPList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_TaxCodeList(String comp)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String currDay = DateTime.Now.ToString("dd");
        ArrayList lsTaxCode = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsTaxCode = oMainCon.getTaxList(comp);
        }

        jsonResponse = convertJson(lsTaxCode);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsBPList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ReportGenFYRYearMonth(String comp, String financeyear, String financemonth, String actualyear, String actualmonth)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModFYR = oMainCon.getReportFYRYearMonth(comp, "", "", actualyear, actualmonth);

            //update report fyr data for selected year & month
            //1. For Revenue
            Double dRevenue = oMainCon.getReportRevenue(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
            int resultRevenue = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "REVENUE_ACTUAL", "dashboard_revenue", oModFYR.GetSetfinancemonth, dRevenue);

            //2. For Expenses -- update manually
            Double dExpenses = oMainCon.getReportExpenses(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
            int resultExpenses = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "EXPENSES_ACTUAL", "dashboard_expenses", oModFYR.GetSetfinancemonth, dExpenses);

            //3. For Collection & Payment Receipt
            Double dCollection = oMainCon.getReportCollection(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
            int resultCollection = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "COLLECTION_ACTUAL", "dashboard_collection", oModFYR.GetSetfinancemonth, dCollection);

            //4. For SalesOrder
            Double dSalesOrder = oMainCon.getReportSales(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "CONFIRMED");
            int resultSalesOrder = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "SALES_ACTUAL", "dashboard_sales", oModFYR.GetSetfinancemonth, dSalesOrder);

            sStatus = "Y";
            sMessage = "";

        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ReportGenTrans(String comp, String fyr, String type, String tablename)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel oModStockTrans = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            oModStockTrans = oMainCon.getReportFYRDetails(comp, fyr, type, tablename);
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockTrans);
        jsonResponse = convertJson(oModStockTrans);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_GeneralReportSummary(String comp, String selectyear, String selectmonth, String selectday)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";
        String additionalquery = "";

        double totalrevenueamount = 0;
        double totalexpensesamount = 0;
        double totalsubtractionstockamount = 0;
        double totalprofitlossamount = 0;
        double totalordersalesamount = 0;
        double totalothersalesamount = 0;

        ArrayList lsArrayRevenue = new ArrayList();
        ArrayList lsArrayExpenses = new ArrayList();
        ArrayList lsSubstractionStock = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
            {
                sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
                int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
                sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
                sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
            }

            //additionalquery = " and  (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat = 'RECEIPT_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME') or (invoice_header.invoicecat = 'JOURNAL_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME')) ";
            //lsArrayRevenue = oMainCon.getInvoiceHeaderListSum(comp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");

            ArrayList lsParamType = oMainCon.getParametertype("INCOME");
            ArrayList revenuetype = new ArrayList();
            for (int i = 0; i < lsParamType.Count; i++)
            {
                MainModel modParam = (MainModel)lsParamType[i];
                revenuetype.Add(modParam.GetSetparamttype);
            }

            additionalquery = " and  (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat in ('RECEIPT_VOUCHER','JOURNAL_VOUCHER') ";
            String revtyp = "";
            for (int i = 0; i < revenuetype.Count; i++)
            {
                String str = (String)revenuetype[i];
                if (i.Equals(0))
                {
                    revtyp = "'" + str + "'";
                }
                else
                {
                    revtyp = revtyp + ",'" + str + "'";
                }
            }
            additionalquery = additionalquery + " and  invoice_header.invoicetype in (" + revtyp + "))) ";
            lsArrayRevenue = oMainCon.getInvoiceHeaderListSum(comp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");

            /*
            ArrayList expensestype = new ArrayList();
            expensestype.Add("SUPPLY_EXPENSES");
            expensestype.Add("SALARIES_WAGES");
            expensestype.Add("TRAVEL_EXPENSES");
            expensestype.Add("ENTERTAINMENT_EXPENSES");
            expensestype.Add("MARKETING_ADVERTISING");
            expensestype.Add("RENTAL_LEASING");
            expensestype.Add("REPAIR_MAINTENANCE");
            expensestype.Add("DEPRECIATION_EXPENSES");
            expensestype.Add("BAD_DEBT_EXPENSES");
            expensestype.Add("SUBSCRIPTION_REGISTRATION");
            expensestype.Add("INSURANCE_SECURITY");
            expensestype.Add("PROFESSIONAL_STATUTORY");
            expensestype.Add("BILL_UTILITIES");
            expensestype.Add("TAXATION");
            expensestype.Add("SELLING_SERVICES");
            expensestype.Add("OTHER_EXPENSES");
            additionalquery = " and  (expenses_header.expensescat = 'PURCHASE_INVOICE' or expenses_header.expensescat = 'TRANSFER_INVOICE' or (expenses_header.expensescat in ('PAYMENT_VOUCHER','JOURNAL_VOUCHER') ";
            String exptyp = "";
            for (int i = 0; i < expensestype.Count; i++)
            {
                String str = (String)expensestype[i];
                if (i.Equals(0))
                {
                    exptyp = "'" + str + "'";
                }
                else
                {
                    exptyp = exptyp + ",'" + str + "'";
                }
            }
            additionalquery = additionalquery + " and  expenses_header.expensestype in (" + exptyp + "))) ";
            additionalquery = additionalquery + @"  and  NOT EXISTS (select item.itemno from item, expenses_details 
                                                        where expenses_header.expensesno = expenses_details.expensesno and expenses_header.comp = expenses_details.comp and expenses_header.expensescat = 'PURCHASE_INVOICE'
                                                        and expenses_details.itemno = item.itemno and expenses_details.comp = item.comp
                                                        and item.itemcat = 'INVENTORY')";
            lsArrayExpenses = oMainCon.getExpensesHeaderListSum(comp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");
            */

            ArrayList lsParamType2 = oMainCon.getParametertype("EXPENSES");
            ArrayList expensestype = new ArrayList();
            for (int i = 0; i < lsParamType2.Count; i++)
            {
                MainModel modParam = (MainModel)lsParamType2[i];
                expensestype.Add(modParam.GetSetparamttype);
            }

            additionalquery = " and  (expenses_header.expensescat = 'PURCHASE_INVOICE' or expenses_header.expensescat = 'TRANSFER_INVOICE' or (expenses_header.expensescat in ('PAYMENT_VOUCHER','JOURNAL_VOUCHER') ";
            String exptyp = "";
            for (int i = 0; i < expensestype.Count; i++)
            {
                String str = (String)expensestype[i];
                if (i.Equals(0))
                {
                    exptyp = "'" + str + "'";
                }
                else
                {
                    exptyp = exptyp + ",'" + str + "'";
                }
            }
            additionalquery = additionalquery + " and  expenses_header.expensestype in (" + exptyp + "))) ";
            additionalquery = additionalquery + @"  and  NOT EXISTS (select item.itemno from item, expenses_details 
                                                        where expenses_header.expensesno = expenses_details.expensesno and expenses_header.comp = expenses_details.comp and expenses_header.expensescat = 'PURCHASE_INVOICE'
                                                        and expenses_details.itemno = item.itemno and expenses_details.comp = item.comp
                                                        and item.itemcat in ('INVENTORY','ASSET'))";
            lsArrayExpenses = oMainCon.getExpensesHeaderListSum(comp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");

            var lsStockTransSOHList = oMainCon.getItemStockTransactionsListing(comp, "", "", "", sDateFrom, sDateTo, "");
            List<MainModel> lsSubtractionStockTransListing = new List<MainModel>();
            foreach (var itemtrans in lsStockTransSOHList)
            {
                if (itemtrans.GetSettransqty > 0)
                {
                }
                else
                {
                    MainModel modItem = new MainModel();
                    modItem.GetSetcomp = itemtrans.GetSetcomp;
                    modItem.GetSetstockstateno = itemtrans.GetSetstockstateno;
                    modItem.GetSetopeningdate = itemtrans.GetSetopeningdate;
                    modItem.GetSetopeningtype = itemtrans.GetSetopeningtype;
                    modItem.GetSetstockopeningamount = itemtrans.GetSetstockopeningamount;
                    modItem.GetSetstockinamount = itemtrans.GetSetstockinamount;
                    modItem.GetSetstockoutamount = itemtrans.GetSetstockoutamount;
                    modItem.GetSetstockclosingamount = itemtrans.GetSetstockclosingamount;
                    modItem.GetSetclosingdate = itemtrans.GetSetclosingdate;
                    modItem.GetSetclosingtype = itemtrans.GetSetclosingtype;
                    modItem.GetSetremarks = itemtrans.GetSetremarks;
                    modItem.GetSetstatus = itemtrans.GetSetstatus;
                    modItem.GetSetitemno = itemtrans.GetSetitemno;
                    modItem.GetSetitemdesc = itemtrans.GetSetitemdesc;
                    modItem.GetSetlocation = itemtrans.GetSetlocation;
                    modItem.GetSetdatesoh = itemtrans.GetSetdatesoh;
                    modItem.GetSetqtysoh = 0;
                    modItem.GetSetcostsoh = 0;
                    modItem.GetSetqtysoh = modItem.GetSetqtysoh + itemtrans.GetSettransqty;
                    modItem.GetSetcostsoh = modItem.GetSetcostsoh + (itemtrans.GetSettransprice * itemtrans.GetSettransqty);
                    lsSubtractionStockTransListing.Add(modItem);
                }
            }

            MainModel oModSalesOrderToDate = oMainCon.getOrderDetailsDetailsByAllItem(comp, selectyear, "", "", "SALES_ORDER", "CONFIRMED");
            MainModel oModSalesAllToDate = oMainCon.getOrderDetailsDetailsByAllItem(comp, selectyear, "", "", "", "CONFIRMED");

            totalordersalesamount = oModSalesOrderToDate.GetSettotalprice;
            totalothersalesamount = oModSalesAllToDate.GetSettotalprice - oModSalesOrderToDate.GetSettotalprice;

            foreach (var itemls in lsArrayRevenue)
            {
                MainModel item = (MainModel)itemls;
                //oMainCon.WriteToLogFile("Opening Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalrevenueamount = totalrevenueamount + item.GetSettotalamount;
            }

            foreach (var itemls in lsArrayExpenses)
            {
                MainModel item = (MainModel)itemls;
                //oMainCon.WriteToLogFile("Opening Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalexpensesamount = totalexpensesamount + item.GetSettotalamount;
            }

            foreach (var item in lsSubtractionStockTransListing)
            {
                //oMainCon.WriteToLogFile("Opening Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalsubtractionstockamount = totalsubtractionstockamount + item.GetSetcostsoh;
                lsSubstractionStock.Add(item);
            }

        }

        object objGeneralReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalrevenueamount = totalrevenueamount, totalexpensesamount = totalexpensesamount, totalsubtractionstockamount = totalsubtractionstockamount, totalprofitlossamount = totalprofitlossamount, totalordersalesamount = totalordersalesamount, totalothersalesamount = totalothersalesamount, arrayrevenue = lsArrayRevenue, arrayexpenses = lsArrayExpenses, arraysubtractionstock = lsSubstractionStock };
        jsonResponse = convertJson(objGeneralReportSummary);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_CashFlowHeaderList(String comp, String cashflowno, String openingdate, String openingtype, String closingdate, String closingtype, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsClosingCashFlow = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsCashFlowHeader = oMainCon.getCashFlowHeaderList(comp, cashflowno, openingdate, openingtype, closingdate, closingtype, status);
            for (int i = 0; i < lsCashFlowHeader.Count; i++)
            {
                MainModel oModCashFlow = (MainModel)lsCashFlowHeader[i];
                if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                {
                    String sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                    for (int x = 0; x < lsPayRcptHeaderDetails.Count; x++)
                    {
                        MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[x];
                        oModCashFlow.GetSetbankpaymentreceiptamount = oModCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                        oModCashFlow.GetSetcashpaymentreceiptamount = oModCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                    }
                    ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(oModCashFlow.GetSetcomp, oModCashFlow.GetSetopeningdate, sClosingDate, "CONFIRMED");
                    for (int y = 0; y < lsPayPaidHeaderDetails.Count; y++)
                    {
                        MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[y];
                        if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                        {
                            oModCashFlow.GetSetbankpaymentpaidamount = oModCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                            oModCashFlow.GetSetcashpaymentpaidamount = oModCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                        }
                    }
                    oModCashFlow.GetSetbankclosingamount = oModCashFlow.GetSetbankopeningamount + oModCashFlow.GetSetbankpaymentreceiptamount - oModCashFlow.GetSetbankpaymentpaidamount;
                    oModCashFlow.GetSetcashclosingamount = oModCashFlow.GetSetcashopeningamount + oModCashFlow.GetSetcashpaymentreceiptamount - oModCashFlow.GetSetcashpaymentpaidamount;
                    oModCashFlow.GetSetclosingdate = sClosingDate;
                }
                lsClosingCashFlow.Add(oModCashFlow);
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(lsClosingStockValue);
        jsonResponse = convertJson(lsClosingCashFlow);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ReportPaymentFYRYearMonth(String comp, String financeyear, String financemonth, String actualyear, String actualmonth)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModFYR = oMainCon.getReportFYRYearMonth(comp, "", "", actualyear, actualmonth);

            //update report fyr data for selected year & month
            //1. For Payment Receipt
            Double dPaymentReceipt = oMainCon.getReportPaymentReceipt(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "", "CONFIRMED");
            int resultPaymentReceipt = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "PAYMENT_RECEIPT", "dashboard_payment", oModFYR.GetSetfinancemonth, dPaymentReceipt);

            //2. For Payment Paid
            Double dPaymentPaid = oMainCon.getReportPaymentPaid(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "", "CONFIRMED");
            int resultPaymentPaid = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "PAYMENT_PAID", "dashboard_payment", oModFYR.GetSetfinancemonth, dPaymentPaid);

            sStatus = "Y";
            sMessage = "";

        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ReportPaymentTrans(String comp, String fyr, String type, String tablename)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel oModStockTrans = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            oModStockTrans = oMainCon.getReportFYRDetails(comp, fyr, type, tablename);
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockTrans);
        jsonResponse = convertJson(oModStockTrans);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PaymentReportSummary(String comp, String selectyear, String selectmonth, String selectday)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        double totalcashopeningamount = 0;
        double totalcashpayrcptamount = 0;
        double totalcashpaypaidamount = 0;
        double totalcashclosingamount = 0;

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
            {
                sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
                int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
                sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
                sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
            }

            MainModel modLastCashFlow = oMainCon.getCashFlowLastHeaderDetails(comp, sDateFrom, "CLOSED");

            MainModel modCashFlow = new MainModel();
            modCashFlow.GetSetcomp = comp;
            modCashFlow.GetSetopeningdate = sDateFrom;
            modCashFlow.GetSetbankopeningamount = modLastCashFlow.GetSetbankclosingamount;
            modCashFlow.GetSetcashopeningamount = modLastCashFlow.GetSetcashclosingamount;

            ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
            for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
            {
                MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

                modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
            }

            ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
            for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
            {
                MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

                modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
            }

            ArrayList lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
            for (int i = 0; i < lsPaymentReceipt.Count; i++)
            {
                MainModel oPayRcptDet = (MainModel)lsPaymentReceipt[i];

                modCashFlow.GetSetbankpaymentreceiptamount = modCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                modCashFlow.GetSetcashpaymentreceiptamount = modCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
            }

            ArrayList lsPaymentPaid = oMainCon.getPaymentPaidCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
            for (int i = 0; i < lsPaymentPaid.Count; i++)
            {
                MainModel oPayPaidDet = (MainModel)lsPaymentPaid[i];

                modCashFlow.GetSetbankpaymentpaidamount = modCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                modCashFlow.GetSetcashpaymentpaidamount = modCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
            }

            modCashFlow.GetSetbankclosingamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetbankpaymentreceiptamount - modCashFlow.GetSetbankpaymentpaidamount;
            modCashFlow.GetSetcashclosingamount = modCashFlow.GetSetcashopeningamount + modCashFlow.GetSetcashpaymentreceiptamount - modCashFlow.GetSetcashpaymentpaidamount;
            modCashFlow.GetSetclosingdate = sDateTo;

            totalcashopeningamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetcashopeningamount;
            totalcashpayrcptamount = modCashFlow.GetSetbankpaymentreceiptamount + modCashFlow.GetSetcashpaymentreceiptamount;
            totalcashpaypaidamount = modCashFlow.GetSetbankpaymentpaidamount + modCashFlow.GetSetcashpaymentpaidamount;
            totalcashclosingamount = modCashFlow.GetSetbankclosingamount + modCashFlow.GetSetcashclosingamount;

            object objStockReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalcashopeningamount = totalcashopeningamount, totalcashpayrcptamount = totalcashpayrcptamount, totalcashpaypaidamount = totalcashpaypaidamount * -1, totalcashclosingamount = totalcashclosingamount, arraypaymentreceipt = lsPaymentReceipt, arraypaymentpaid = lsPaymentPaid };
            jsonResponse = convertJson(objStockReportSummary);
        }
        else
        {
            object objStockReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalcashopeningamount = totalcashopeningamount, totalcashpayrcptamount = totalcashpayrcptamount, totalcashpaypaidamount = totalcashpaypaidamount * -1, totalcashclosingamount = totalcashclosingamount, arraypaymentreceipt = new ArrayList(), arraypaymentpaid = new ArrayList() };
            jsonResponse = convertJson(objStockReportSummary);
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockPosition);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ReconcileCashFlow(String comp, String datefrom, String dateto, String cashflowno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            sDateFrom = datefrom + " 00:00:00";
            sDateTo = dateto + " 23:59:59";

            ArrayList lsCashFlowClosing = oMainCon.getCashFlowHeaderList(comp, "", "", "", "", "", "CLOSED");
            for (int x = 0; x < lsCashFlowClosing.Count; x++)
            {
                MainModel modCashFlowHdr = (MainModel)lsCashFlowClosing[x];
                if (modCashFlowHdr.GetSetopeningtype.Equals("NORMAL_OPEN"))
                {
                    MainModel modPrevCashFlowHdr = oMainCon.getCashFlowLastHeaderDetails(comp, modCashFlowHdr.GetSetopeningdate, "CLOSED");
                    modCashFlowHdr.GetSetbankopeningamount = modPrevCashFlowHdr.GetSetbankclosingamount;
                    modCashFlowHdr.GetSetcashopeningamount = modPrevCashFlowHdr.GetSetcashclosingamount;
                }
                else
                {
                    modCashFlowHdr.GetSetbankopeningamount = 0;
                    modCashFlowHdr.GetSetcashopeningamount = 0;
                }

                modCashFlowHdr.GetSetbankpaymentreceiptamount = 0;
                modCashFlowHdr.GetSetcashpaymentreceiptamount = 0;
                ArrayList lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(comp, modCashFlowHdr.GetSetopeningdate, modCashFlowHdr.GetSetclosingdate, "CONFIRMED");
                for (int i = 0; i < lsPaymentReceipt.Count; i++)
                {
                    MainModel oPayRcptDet = (MainModel)lsPaymentReceipt[i];

                    modCashFlowHdr.GetSetbankpaymentreceiptamount = modCashFlowHdr.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                    modCashFlowHdr.GetSetcashpaymentreceiptamount = modCashFlowHdr.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                }

                modCashFlowHdr.GetSetbankpaymentpaidamount = 0;
                modCashFlowHdr.GetSetcashpaymentpaidamount = 0;
                ArrayList lsPaymentPaid = oMainCon.getPaymentPaidCashFlowList(comp, modCashFlowHdr.GetSetopeningdate, modCashFlowHdr.GetSetclosingdate, "CONFIRMED");
                for (int i = 0; i < lsPaymentPaid.Count; i++)
                {
                    MainModel oPayPaidDet = (MainModel)lsPaymentPaid[i];

                    modCashFlowHdr.GetSetbankpaymentpaidamount = modCashFlowHdr.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                    modCashFlowHdr.GetSetcashpaymentpaidamount = modCashFlowHdr.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                }

                modCashFlowHdr.GetSetbankclosingamount = modCashFlowHdr.GetSetbankopeningamount + modCashFlowHdr.GetSetbankpaymentreceiptamount - modCashFlowHdr.GetSetbankpaymentpaidamount;
                modCashFlowHdr.GetSetcashclosingamount = modCashFlowHdr.GetSetcashopeningamount + modCashFlowHdr.GetSetcashpaymentreceiptamount - modCashFlowHdr.GetSetcashpaymentpaidamount;

                sStatus = oMainCon.updateCashFlowHeader(modCashFlowHdr);
            }
        }

        object objStockReportSummary = new { status = sStatus, message = sMessage };

        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockPosition);
        jsonResponse = convertJson(objStockReportSummary);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //for purchasing - matzul 03/05/2019

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PurchaseList(String comp, String searchitem, String datefrom, String dateto, String ordercat)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsOrderHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsOrderHeader = oMainCon.getPurchaseOrderHeaderListSearching(comp, searchitem, ordercat, datefrom, dateto, "");
        }

        jsonResponse = convertJson(lsOrderHeader);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsOrderHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PurchaseDetails(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modOrderHeaderDetails = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modOrderHeaderDetails = oMainCon.getPurchaseOrderHeaderDetails(comp, orderno);
        }

        jsonResponse = convertJson(modOrderHeaderDetails);
        //jsonResponse = new JavaScriptSerializer().Serialize(modOrderHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PurchaseItems(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsOrderHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsOrderHeader = oMainCon.getPurchaseOrderDetailsList(comp, orderno, 0, "");
        }
        jsonResponse = convertJson(lsOrderHeader);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsOrderHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PurchaseReceiptList(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countReceipt = 0;
        String receiptNo = "";
        ArrayList lsReceiptNo = new ArrayList();

        object objPurchaseReceiptList = new { countreceipt = countReceipt, arrayreceiptno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsReceiptHeaderDetailsList = oMainCon.getReceiptHeaderDetailsList(comp, "", "", "", "", orderno, "", "", "");
            if (lsReceiptHeaderDetailsList.Count > 0)
            {
                for (int i = 0; i < lsReceiptHeaderDetailsList.Count; i++)
                {
                    MainModel modReceipt = (MainModel)lsReceiptHeaderDetailsList[i];
                    if (i.Equals(0))
                    {
                        receiptNo = modReceipt.GetSetreceiptno;
                        countReceipt = countReceipt + 1;
                        lsReceiptNo.Add(receiptNo);
                    }
                    else
                    {
                        if (receiptNo != modReceipt.GetSetreceiptno)
                        {
                            receiptNo = modReceipt.GetSetreceiptno;
                            countReceipt = countReceipt + 1;
                            lsReceiptNo.Add(receiptNo);
                        }
                    }

                }
                objPurchaseReceiptList = new { countreceipt = countReceipt, arrayreceiptno = lsReceiptNo };
            }
        }

        jsonResponse = convertJson(objPurchaseReceiptList);
        //jsonResponse = new JavaScriptSerializer().Serialize(objPurchaseReceiptList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PurchaseReceiptItem(String comp, String orderno, String receiptno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsReceiptHeaderDetailsList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsReceiptHeaderDetailsList = oMainCon.getReceiptHeaderDetailsList(comp, receiptno, "", "", "", orderno, "", "", "");

        }

        jsonResponse = convertJson(lsReceiptHeaderDetailsList);
        //jsonResponse = new JavaScriptSerializer().Serialize(lsReceiptHeaderDetailsList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_PurchaseOrder(String comp, String bpid, String ordercat, String ordertype, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            //get comp bp if exist
            MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
            if (oModBP.GetSetbpid.Trim().Length > 0)
            {
                MainModel oModOrder = new MainModel();
                oModOrder.GetSetcomp = comp;
                oModOrder.GetSetorderdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModOrder.GetSetordercat = ordercat;
                String sOrderNo = oMainCon.getNextRunningNo(comp, oModOrder.GetSetordercat, "ACTIVE");
                oModOrder.GetSetorderno = sOrderNo;
                oModOrder.GetSetorderactivity = "ACTIVITY00";
                oModOrder.GetSetordertype = ordertype;
                oModOrder.GetSetpaytype = "NOT_APPLICABLE";
                oModOrder.GetSetplandeliverydate = DateTime.Now.ToString("dd-MM-yyyy");
                oModOrder.GetSetbpid = oModBP.GetSetbpid;
                oModOrder.GetSetbpdesc = oModBP.GetSetbpdesc;
                oModOrder.GetSetbpaddress = oModBP.GetSetbpaddress;
                oModOrder.GetSetbpcontact = oModBP.GetSetbpcontact;
                oModOrder.GetSetorderremarks = "";
                oModOrder.GetSetorderstatus = "NEW";
                oModOrder.GetSetordercreated = userid;

                if (oMainCon.insertPurchaseOrderHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModOrder.GetSetcomp, oModOrder.GetSetordercat, "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModOrder.GetSetorderno;
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_PurchaseOrderStatus(String comp, String orderno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        if (TokenNumber.Equals(TokenNumberConfig))
        {
            MainModel oModOrder = oMainCon.getPurchaseOrderHeaderDetails(comp, orderno);
            if (oModOrder.GetSetorderno.Trim().Length > 0)
            {
                oModOrder.GetSetorderstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModOrder.GetSetorderapproved = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModOrder.GetSetordercancelled = userid;
                }
                if (oMainCon.updatePurchaseOrderHeader(oModOrder).Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_PurchaseItemOrder(String comp, String orderno, String ordercat, String itemno, String itemcat, String itemtype, String ordertype)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            bool proceed = true;

            //get item details if exist
            MainModel oModItem = oMainCon.getItemDetails(comp, itemno);
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                //get item discount if exist
                ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                if (lsItemDisc.Count == 0)
                {
                    //create item discount for ordercat & ordertype
                    oModItem.GetSetordercat = ordercat;
                    oModItem.GetSetdiscounttype = ordertype;
                    oModItem.GetSetdisccat = "PERCENTAGE";
                    oModItem.GetSetdiscvalue = 0;
                    oModItem.GetSetstatus = "STATUS";

                    if (oMainCon.insertItemDiscount(oModItem).Equals("Y"))
                    {
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;
                    }
                }
                else
                {
                    proceed = true;
                }
            }
            else
            {
                proceed = false;
            }

            if (proceed)
            {
                ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                MainModel oModItemDisc = new MainModel();
                for (int x = 0; x < lsItemDisc.Count; x++)
                {
                    oModItemDisc = (MainModel)lsItemDisc[x];
                }

                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetorderno = orderno;
                oModLineItem.GetSetlineno = oMainCon.getPurchaseOrderDetailsList(comp, orderno, 0, "").Count + 1;
                oModLineItem.GetSetitemno = oModItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
                oModLineItem.GetSetunitprice = oModItem.GetSetpurchaseprice;
                if (oModItemDisc.GetSetdisccat.Equals("PERCENTAGE"))
                {
                    oModLineItem.GetSetdiscamount = oModItem.GetSetpurchaseprice * oModItemDisc.GetSetdiscvalue / 100;
                }
                else
                {
                    oModLineItem.GetSetdiscamount = oModItemDisc.GetSetdiscamount;
                }
                oModLineItem.GetSetquantity = 1;
                oModLineItem.GetSetorderprice = (oModLineItem.GetSetunitprice - oModLineItem.GetSetdiscamount) * oModLineItem.GetSetquantity;
                oModLineItem.GetSettaxcode = "NA";
                oModLineItem.GetSettaxrate = 0;
                oModLineItem.GetSettaxamount = 0;
                oModLineItem.GetSettotalprice = oModLineItem.GetSetorderprice + oModLineItem.GetSettaxamount;

                MainModel modExistent = oMainCon.getPurchaseOrderDetailsDetails(oModLineItem.GetSetcomp, oModLineItem.GetSetorderno, 0, oModLineItem.GetSetitemno);
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPurchaseOrderDetails(oModLineItem).Equals("Y"))
                    {
                        //update purchase order header information
                        sStatus = oMainCon.updatePurchaseOrderHeaderInfo(comp, orderno);
                        sMessage = "";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Internal Server Error, Please contact System Admin!";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Internal Server Error, Please contact System Admin!";
            }
        }
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string editMobile_PurchaseItemOrder(String comp, String orderno, String ordercat, String ordertype, int lineno, String itemno, int qty, double price, double discount, String taxcode)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            //get item details if exist
            MainModel oModItem = oMainCon.getPurchaseOrderDetailsDetails(comp, orderno, lineno, itemno);
            oModItem.GetSetunitprice = price;
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (discount > 0)
                {
                    oModItem.GetSetdiscamount = discount;
                }
                else
                {
                    ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                    MainModel oModItemDisc = new MainModel();
                    for (int x = 0; x < lsItemDisc.Count; x++)
                    {
                        oModItemDisc = (MainModel)lsItemDisc[x];
                    }

                    if (oModItemDisc.GetSetdisccat.Equals("PERCENTAGE"))
                    {
                        oModItem.GetSetdiscamount = oModItem.GetSetunitprice * oModItemDisc.GetSetdiscvalue / 100;
                    }
                    else
                    {
                        oModItem.GetSetdiscamount = oModItemDisc.GetSetdiscamount;
                    }
                }
                oModItem.GetSetquantity = qty;
                oModItem.GetSetorderprice = (oModItem.GetSetunitprice - oModItem.GetSetdiscamount) * oModItem.GetSetquantity;

                if (taxcode.Trim().Length > 0)
                {
                    oModItem.GetSettaxcode = taxcode;
                }
                else
                {
                    oModItem.GetSettaxcode = "NA";
                }
                MainModel modTaxCode = oMainCon.getTaxDetails(comp, oModItem.GetSettaxcode);
                oModItem.GetSettaxrate = modTaxCode.GetSettaxrate;
                oModItem.GetSettaxamount = oModItem.GetSetorderprice * modTaxCode.GetSettaxrate / 100;

                oModItem.GetSettotalprice = oModItem.GetSetorderprice + oModItem.GetSettaxamount;

                if (oMainCon.updatePurchaseOrderDetails(oModItem).Equals("Y"))
                {
                    //update purchase order header information
                    String result = oMainCon.updatePurchaseOrderHeaderInfo(comp, orderno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string deleteMobile_PurchaseItemOrder(String comp, String orderno, int lineno, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            //get item details if exist
            MainModel oModItem = oMainCon.getPurchaseOrderDetailsDetails(comp, orderno, lineno, itemno);
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (oMainCon.deletePurchaseOrderDetails(oModItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getPurchaseOrderDetailsList(comp, orderno, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deletePurchaseOrderDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertPurchaseOrderDetails(oLineItem);
                    }
                    //update purchase order header information
                    String result = oMainCon.updatePurchaseOrderHeaderInfo(comp, orderno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingReceiptList(String comp, String bpid, String orderno, String ordercat)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countpendreceipt = 0;
        ArrayList lsPendReceipt = new ArrayList();

        object objPendReceiptList = new { countpendreceipt = countpendreceipt, arraypendreceiptno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            ArrayList lsPendReceiptList = oMainCon.getLineItemPendingReceipt(comp, bpid, "", orderno, ordercat);
            if (lsPendReceiptList.Count > 0)
            {
                countpendreceipt = lsPendReceiptList.Count;
                objPendReceiptList = new { countpendreceipt = countpendreceipt, arraypendreceiptno = lsPendReceiptList };
            }
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(objPendReceiptList);
        jsonResponse = convertJson(objPendReceiptList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ReceiptOrder(String comp, String orderno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            //get order header if exist
            MainModel oModOrder = oMainCon.getPurchaseOrderHeaderDetails(comp, orderno);
            if (oModOrder.GetSetorderno.Trim().Length > 0)
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetreceiptdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModHeader.GetSetreceiptcat = oModOrder.GetSetordercat;
                String sReceiptNo = oMainCon.getNextRunningNo(comp, "RECEIPT", "ACTIVE");
                oModHeader.GetSetreceiptno = sReceiptNo;
                oModHeader.GetSetbpid = oModOrder.GetSetbpid;
                oModHeader.GetSetbpdesc = oModOrder.GetSetbpdesc;
                oModHeader.GetSetbpaddress = oModOrder.GetSetbpaddress;
                oModHeader.GetSetbpcontact = oModOrder.GetSetbpcontact;
                oModHeader.GetSetremarks = oModOrder.GetSetordertype;
                oModHeader.GetSetstatus = "NEW";
                oModHeader.GetSetcreatedby = userid;

                if (oMainCon.insertReceiptHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(comp, "RECEIPT", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModHeader.GetSetreceiptno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ReceiptItemOrder(String comp, String receiptno, int receiptlineno, String orderno, int lineno, String itemno, int qty, String location)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            MainModel oModItem = oMainCon.getPurchaseOrderDetailsDetails(comp, orderno, lineno, itemno);

            if (oModItem.GetSetorderno.Trim().Length > 0)
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetreceiptno = receiptno;
                oModLineItem.GetSetlineno = receiptlineno;
                oModLineItem.GetSetorderno = orderno;
                oModLineItem.GetSetorder_lineno = lineno;
                oModLineItem.GetSetitemno = oModItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
                oModLineItem.GetSetorder_quantity = oModItem.GetSetquantity;
                oModLineItem.GetSetreceipt_quantity = qty;
                oModLineItem.GetSetlocation = location;
                oModLineItem.GetSetremarks = "";
                oModLineItem.GetSethasbilling = "N";

                if (oMainCon.insertReceiptDetails(oModLineItem).Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ReceiptOrderStatus(String comp, String receiptno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            MainModel oModOrder = oMainCon.getReceiptHeaderDetails(comp, receiptno);
            if (oModOrder.GetSetreceiptno.Trim().Length > 0)
            {
                oModOrder.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModOrder.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModOrder.GetSetcancelledby = userid;
                }
                if (oMainCon.updateReceiptHeader(oModOrder).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        //get latest information about Receipt Header ie. Confirmed Date which is needed for storing item stock transactions
                        MainModel oModHeader = oMainCon.getReceiptHeaderDetails(comp, receiptno);
                        ArrayList lsReceiptLineItem = oMainCon.getReceiptDetailsList(oModHeader.GetSetcomp, oModHeader.GetSetreceiptno, 0, "");
                        for (int i = 0; i < lsReceiptLineItem.Count; i++)
                        {
                            MainModel modReceiptDet = (MainModel)lsReceiptLineItem[i];

                            //update Receipt Details Date SOH
                            modReceiptDet.GetSetdatesoh = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                            String result0 = oMainCon.updateReceiptDetails(modReceiptDet);

                            MainModel oModOrderLineItem = new MainModel();
                            //to update shipped quantity order item @ transfer item
                            if (oModHeader.GetSetreceiptcat.Equals("PURCHASE_ORDER") || oModHeader.GetSetreceiptcat.Equals("RECEIVE_ORDER"))
                            {
                                //to update received quantity order item
                                oModOrderLineItem = oMainCon.getPurchaseOrderDetailsDetails(modReceiptDet.GetSetcomp, modReceiptDet.GetSetorderno, modReceiptDet.GetSetorder_lineno, modReceiptDet.GetSetitemno);
                                oModOrderLineItem.GetSetreceiptqty = oModOrderLineItem.GetSetreceiptqty + modReceiptDet.GetSetreceipt_quantity;
                                String result1 = oMainCon.updatePurchaseOrderDetails(oModOrderLineItem);
                            }
                            else if (oModHeader.GetSetreceiptcat.Equals("TRANSFER_ORDER"))
                            {
                                MainModel oModTranferHeader = oMainCon.getTransferOrderHeaderDetails("", "", modReceiptDet.GetSetcomp, modReceiptDet.GetSetorderno);
                                oModOrderLineItem = oMainCon.getTransferOrderDetailsDetails(oModTranferHeader.GetSetCompFromDetails.GetSetcomp, modReceiptDet.GetSetorderno, modReceiptDet.GetSetorder_lineno, modReceiptDet.GetSetitemno);
                                oModOrderLineItem.GetSetreceiptqty = oModOrderLineItem.GetSetreceiptqty + modReceiptDet.GetSetreceipt_quantity;
                                String result = oMainCon.updateTransferOrderDetails(oModOrderLineItem);
                            }


                            //to update item stock & stock transaction
                            MainModel oModItemDet = oMainCon.getItemDetails(modReceiptDet.GetSetcomp, modReceiptDet.GetSetitemno);
                            if (oModItemDet.GetSetitemcat.Equals("INVENTORY") || oModItemDet.GetSetitemcat.Equals("ASSET"))
                            {
                                MainModel oModItemStock = new MainModel();
                                oModItemStock.GetSetcomp = modReceiptDet.GetSetcomp;
                                oModItemStock.GetSetitemno = modReceiptDet.GetSetitemno;
                                oModItemStock.GetSetitemdesc = modReceiptDet.GetSetitemdesc;
                                oModItemStock.GetSetlocation = modReceiptDet.GetSetlocation;
                                oModItemStock.GetSetdatesoh = modReceiptDet.GetSetdatesoh;
                                oModItemStock.GetSetqtysoh = modReceiptDet.GetSetreceipt_quantity;
                                if (oModOrderLineItem.GetSetordercat.Equals("RECEIVE_ORDER"))
                                {
                                    oModItemStock.GetSetcostsoh = Math.Round(oModItemDet.GetSetcostprice * modReceiptDet.GetSetreceipt_quantity, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    oModItemStock.GetSetcostsoh = Math.Round(Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero) * (modReceiptDet.GetSetreceipt_quantity), 2, MidpointRounding.AwayFromZero);
                                }
                                String result2 = oMainCon.insertItemStock(oModItemStock);

                                MainModel oModItemStockTrans = new MainModel();
                                oModItemStockTrans.GetSetcomp = modReceiptDet.GetSetcomp;
                                oModItemStockTrans.GetSetitemno = modReceiptDet.GetSetitemno;
                                oModItemStockTrans.GetSetitemdesc = modReceiptDet.GetSetitemdesc;
                                oModItemStockTrans.GetSetlocation = modReceiptDet.GetSetlocation;
                                oModItemStockTrans.GetSetdatesoh = modReceiptDet.GetSetdatesoh;
                                oModItemStockTrans.GetSettranstype = "RECEIPT";
                                oModItemStockTrans.GetSettransdate = oModHeader.GetSetconfirmeddate;
                                oModItemStockTrans.GetSettransno = modReceiptDet.GetSetreceiptno;
                                oModItemStockTrans.GetSettrans_lineno = modReceiptDet.GetSetlineno;
                                oModItemStockTrans.GetSetorderno = modReceiptDet.GetSetorderno;
                                oModItemStockTrans.GetSetorder_lineno = modReceiptDet.GetSetorder_lineno;
                                oModItemStockTrans.GetSettransqty = modReceiptDet.GetSetreceipt_quantity;
                                if (oModOrderLineItem.GetSetordercat.Equals("RECEIVE_ORDER"))
                                {
                                    oModItemStockTrans.GetSettransprice = Math.Round(oModItemDet.GetSetcostprice, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    oModItemStockTrans.GetSettransprice = Math.Round(oModOrderLineItem.GetSettotalprice / oModOrderLineItem.GetSetquantity, 2, MidpointRounding.AwayFromZero);
                                }
                                oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                                oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                                String result3 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                            }

                        }
                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //for selling - matzul 03/05/2019

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_OrderList(String comp, String searchitem, String datefrom, String dateto, String ordercat)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsOrderHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsOrderHeader = oMainCon.getOrderHeaderListSearching(comp, searchitem, ordercat, datefrom, dateto, "");
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsOrderHeader);
        jsonResponse = convertJson(lsOrderHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_OrderDetails(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modOrderHeaderDetails = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modOrderHeaderDetails = oMainCon.getOrderHeaderDetails(comp, orderno);
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modOrderHeaderDetails);
        jsonResponse = convertJson(modOrderHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_OrderItems(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsOrderHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsOrderHeader = oMainCon.getOrderDetailsList(comp, orderno, 0, "");
        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsOrderHeader);
        jsonResponse = convertJson(lsOrderHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_OrderOrder(String comp, String bpid, String ordercat, String ordertype, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get comp bp if exist
            MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
            if (oModBP.GetSetbpid.Trim().Length > 0)
            {
                MainModel oModOrder = new MainModel();
                oModOrder.GetSetcomp = comp;
                oModOrder.GetSetorderdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModOrder.GetSetordercat = ordercat;
                String sOrderNo = oMainCon.getNextRunningNo(comp, oModOrder.GetSetordercat, "ACTIVE");
                oModOrder.GetSetorderno = sOrderNo;
                oModOrder.GetSetorderactivity = "ACTIVITY00";
                oModOrder.GetSetordertype = ordertype;
                oModOrder.GetSetpaytype = "NOT_APPLICABLE";
                oModOrder.GetSetplandeliverydate = DateTime.Now.ToString("dd-MM-yyyy");
                oModOrder.GetSetbpid = oModBP.GetSetbpid;
                oModOrder.GetSetbpdesc = oModBP.GetSetbpdesc;
                oModOrder.GetSetbpaddress = oModBP.GetSetbpaddress;
                oModOrder.GetSetbpcontact = oModBP.GetSetbpcontact;
                oModOrder.GetSetorderremarks = "";
                oModOrder.GetSetorderstatus = "NEW";
                oModOrder.GetSetordercreated = userid;

                if (oMainCon.insertOrderHeader(oModOrder).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModOrder.GetSetcomp, oModOrder.GetSetordercat, "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModOrder.GetSetorderno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_OrderOrderStatus(String comp, String orderno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModOrder = oMainCon.getOrderHeaderDetails(comp, orderno);
            if (oModOrder.GetSetorderno.Trim().Length > 0)
            {
                oModOrder.GetSetorderstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModOrder.GetSetorderapproved = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModOrder.GetSetordercancelled = userid;
                }
                if (oMainCon.updateOrderHeader(oModOrder).Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_OrderItemOrder(String comp, String orderno, String ordercat, String itemno, String itemcat, String itemtype, String ordertype)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            bool proceed = true;

            //get item details if exist
            MainModel oModItem = oMainCon.getItemDetails(comp, itemno);
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                //get item discount if exist
                ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                if (lsItemDisc.Count == 0)
                {
                    //create item discount for ordercat & ordertype
                    oModItem.GetSetordercat = ordercat;
                    oModItem.GetSetdiscounttype = ordertype;
                    oModItem.GetSetdisccat = "PERCENTAGE";
                    oModItem.GetSetdiscvalue = 0;
                    oModItem.GetSetstatus = "STATUS";

                    if (oMainCon.insertItemDiscount(oModItem).Equals("Y"))
                    {
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;
                    }
                }
                else
                {
                    proceed = true;
                }
            }
            else
            {
                proceed = false;

            }

            if (proceed)
            {
                ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                MainModel oModItemDisc = new MainModel();
                for (int x = 0; x < lsItemDisc.Count; x++)
                {
                    oModItemDisc = (MainModel)lsItemDisc[x];
                }

                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetorderno = orderno;
                oModLineItem.GetSetlineno = oMainCon.getOrderDetailsList(comp, orderno, 0, "").Count + 1;
                oModLineItem.GetSetitemno = oModItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
                oModLineItem.GetSetunitprice = oModItem.GetSetsalesprice;
                if (oModItemDisc.GetSetdisccat.Equals("PERCENTAGE"))
                {
                    oModLineItem.GetSetdiscamount = oModItem.GetSetsalesprice * oModItemDisc.GetSetdiscvalue / 100;
                }
                else
                {
                    oModLineItem.GetSetdiscamount = oModItemDisc.GetSetdiscamount;
                }
                oModLineItem.GetSetquantity = 1;
                oModLineItem.GetSetorderprice = (oModLineItem.GetSetunitprice - oModLineItem.GetSetdiscamount) * oModLineItem.GetSetquantity;
                oModLineItem.GetSettaxcode = "NA";
                oModLineItem.GetSettaxrate = 0;
                oModLineItem.GetSettaxamount = 0;
                oModLineItem.GetSettotalprice = oModLineItem.GetSetorderprice + oModLineItem.GetSettaxamount;

                MainModel modExistent = oMainCon.getOrderDetailsDetails(oModLineItem.GetSetcomp, oModLineItem.GetSetorderno, 0, oModLineItem.GetSetitemno);
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertOrderDetails(oModLineItem).Equals("Y"))
                    {
                        //update order header information
                        sStatus = oMainCon.updateOrderHeaderInfo(comp, orderno);
                        sMessage = "";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Internal Server Error, Please contact System Admin!";
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Internal Server Error, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string editMobile_OrderItemOrder(String comp, String orderno, String ordercat, String ordertype, int lineno, String itemno, int qty, double price, double discount, String taxcode)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getOrderDetailsDetails(comp, orderno, lineno, itemno);
            oModItem.GetSetunitprice = price;
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (discount > 0)
                {
                    oModItem.GetSetdiscamount = discount;
                }
                else
                {
                    ArrayList lsItemDisc = oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno);
                    MainModel oModItemDisc = new MainModel();
                    for (int x = 0; x < lsItemDisc.Count; x++)
                    {
                        oModItemDisc = (MainModel)lsItemDisc[x];
                    }

                    if (oModItemDisc.GetSetdisccat.Equals("PERCENTAGE"))
                    {
                        oModItem.GetSetdiscamount = oModItem.GetSetunitprice * oModItemDisc.GetSetdiscvalue / 100;
                    }
                    else
                    {
                        oModItem.GetSetdiscamount = oModItemDisc.GetSetdiscamount;
                    }
                }
                oModItem.GetSetquantity = qty;
                oModItem.GetSetorderprice = (oModItem.GetSetunitprice - oModItem.GetSetdiscamount) * oModItem.GetSetquantity;

                if (taxcode.Trim().Length > 0)
                {
                    oModItem.GetSettaxcode = taxcode;
                }
                else
                {
                    oModItem.GetSettaxcode = "NA";
                }
                MainModel modTaxCode = oMainCon.getTaxDetails(comp, oModItem.GetSettaxcode);
                oModItem.GetSettaxrate = modTaxCode.GetSettaxrate;
                oModItem.GetSettaxamount = oModItem.GetSetorderprice * modTaxCode.GetSettaxrate / 100;

                oModItem.GetSettotalprice = oModItem.GetSetorderprice + oModItem.GetSettaxamount;

                if (oMainCon.updateOrderDetails(oModItem).Equals("Y"))
                {
                    //update purchase order header information
                    String result = oMainCon.updateOrderHeaderInfo(comp, orderno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string deleteMobile_OrderItemOrder(String comp, String orderno, int lineno, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getOrderDetailsDetails(comp, orderno, lineno, itemno);
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (oMainCon.deleteOrderDetails(oModItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getOrderDetailsList(comp, orderno, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteOrderDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertOrderDetails(oLineItem);
                    }
                    //update order header information
                    String result = oMainCon.updateOrderHeaderInfo(comp, orderno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_OrderShipmentList(String comp, String orderno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countShipment = 0;
        String shipmentNo = "";
        ArrayList lsShipmentNo = new ArrayList();

        object objOrderShipmentList = new { countshipment = countShipment, arrayshipmentno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsShipmentHeaderDetailsList = oMainCon.getShipmentDetailsList(comp, "", "", "", "", orderno, "", "", "");
            if (lsShipmentHeaderDetailsList.Count > 0)
            {
                for (int i = 0; i < lsShipmentHeaderDetailsList.Count; i++)
                {
                    MainModel modShipment = (MainModel)lsShipmentHeaderDetailsList[i];
                    if (i.Equals(0))
                    {
                        shipmentNo = modShipment.GetSetshipmentno;
                        countShipment = countShipment + 1;
                        lsShipmentNo.Add(shipmentNo);
                    }
                    else
                    {
                        if (shipmentNo != modShipment.GetSetshipmentno)
                        {
                            shipmentNo = modShipment.GetSetshipmentno;
                            countShipment = countShipment + 1;
                            lsShipmentNo.Add(shipmentNo);
                        }
                    }

                }
                objOrderShipmentList = new { countshipment = countShipment, arrayshipmentno = lsShipmentNo };
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objOrderShipmentList);
        jsonResponse = convertJson(objOrderShipmentList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_OrderShipmentItem(String comp, String orderno, String shipmentno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsShipmentHeaderDetailsList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsShipmentHeaderDetailsList = oMainCon.getShipmentDetailsList(comp, shipmentno, "", "", "", orderno, "", "", "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsShipmentHeaderDetailsList);
        jsonResponse = convertJson(lsShipmentHeaderDetailsList);


        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingShipmentList(String comp, String bpid, String orderno, String ordercat)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countpendshipment = 0;
        ArrayList lsPendShipment = new ArrayList();

        object objPendShipmentList = new { countpendshipment = countpendshipment, arraypendshipmentno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendShipmentList = oMainCon.getLineItemPendingShipment(comp, bpid, "", orderno, ordercat);
            if (lsPendShipmentList.Count > 0)
            {
                countpendshipment = lsPendShipmentList.Count;
                objPendShipmentList = new { countpendshipment = countpendshipment, arraypendshipmentno = lsPendShipmentList };
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objPendShipmentList);
        jsonResponse = convertJson(objPendShipmentList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ShipmentOrder(String comp, String orderno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get order header if exist
            MainModel oModOrder = oMainCon.getOrderHeaderDetails(comp, orderno);
            if (oModOrder.GetSetorderno.Trim().Length > 0)
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetshipmentdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModHeader.GetSetshipmentcat = oModOrder.GetSetordercat;
                String sShipmentNo = oMainCon.getNextRunningNo(comp, "SHIPMENT", "ACTIVE");
                oModHeader.GetSetshipmentno = sShipmentNo;
                oModHeader.GetSetbpid = oModOrder.GetSetbpid;
                oModHeader.GetSetbpdesc = oModOrder.GetSetbpdesc;
                oModHeader.GetSetbpaddress = oModOrder.GetSetbpaddress;
                oModHeader.GetSetbpcontact = oModOrder.GetSetbpcontact;
                oModHeader.GetSetremarks = oModOrder.GetSetordertype;
                oModHeader.GetSetstatus = "NEW";
                oModHeader.GetSetcreatedby = userid;

                if (oMainCon.insertShipmentHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(comp, "SHIPMENT", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModHeader.GetSetshipmentno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ShipmentItemOrder(String comp, String shipmentno, int shipmentlineno, String orderno, int lineno, String itemno, int qty, String location, String datesoh, int qtyavailable)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModItem = oMainCon.getOrderDetailsDetails(comp, orderno, lineno, itemno);

            if (oModItem.GetSetorderno.Trim().Length > 0)
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetshipmentno = shipmentno;
                oModLineItem.GetSetlineno = shipmentlineno;
                oModLineItem.GetSetorderno = orderno;
                oModLineItem.GetSetorder_lineno = lineno;
                oModLineItem.GetSetitemno = oModItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModItem.GetSetitemdesc;
                oModLineItem.GetSetorder_quantity = oModItem.GetSetquantity;
                oModLineItem.GetSetshipment_quantity = qty;
                oModLineItem.GetSetlocation = location;
                oModLineItem.GetSetdatesoh = datesoh;
                oModLineItem.GetSetqtysoh = 0;
                oModLineItem.GetSetqtyavailable = qtyavailable;
                oModLineItem.GetSetremarks = "";
                oModLineItem.GetSethasinvoice = "N";

                //insert new line item
                if (oMainCon.insertShipmentDetails(oModLineItem).Equals("Y"))
                {
                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ShipmentOrderStatus(String comp, String shipmentno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModOrder = oMainCon.getShipmentHeaderDetails(comp, shipmentno);
            if (oModOrder.GetSetshipmentno.Trim().Length > 0)
            {
                oModOrder.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModOrder.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModOrder.GetSetcancelledby = userid;
                }
                if (oMainCon.updateShipmentHeader(oModOrder).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        //get latest information about Shipment Header ie. Confirmed Date which is needed for storing item stock transactions
                        MainModel oModHeader = oMainCon.getShipmentHeaderDetails(comp, shipmentno);
                        ArrayList lsShipmentLineItem = oMainCon.getShipmentDetailsList(oModHeader.GetSetcomp, oModHeader.GetSetshipmentno, 0, "");
                        for (int i = 0; i < lsShipmentLineItem.Count; i++)
                        {
                            MainModel modShipmentDet = (MainModel)lsShipmentLineItem[i];

                            //to update shipped quantity order item @ transfer item
                            if (oModHeader.GetSetshipmentcat.Equals("SALES_ORDER") || oModHeader.GetSetshipmentcat.Equals("GIVE_ORDER"))
                            {
                                MainModel oModOrderLineItem = oMainCon.getOrderDetailsDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetorderno, modShipmentDet.GetSetorder_lineno, modShipmentDet.GetSetitemno);
                                oModOrderLineItem.GetSetdeliverqty = oModOrderLineItem.GetSetdeliverqty + modShipmentDet.GetSetshipment_quantity;
                                String result = oMainCon.updateOrderDetails(oModOrderLineItem);
                            }
                            else if (oModHeader.GetSetshipmentcat.Equals("TRANSFER_ORDER"))
                            {
                                MainModel oModOrderLineItem = oMainCon.getTransferOrderDetailsDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetorderno, modShipmentDet.GetSetorder_lineno, modShipmentDet.GetSetitemno);
                                oModOrderLineItem.GetSetdeliverqty = oModOrderLineItem.GetSetdeliverqty + modShipmentDet.GetSetshipment_quantity;
                                String result = oMainCon.updateTransferOrderDetails(oModOrderLineItem);
                            }

                            //to update item stock & stock transaction
                            MainModel oModLatestItemStock = oMainCon.getItemStockDetails(modShipmentDet.GetSetcomp, modShipmentDet.GetSetitemno, modShipmentDet.GetSetlocation, modShipmentDet.GetSetdatesoh);
                            if (oModLatestItemStock.GetSetitemcat.Equals("INVENTORY") || oModLatestItemStock.GetSetitemcat.Equals("ASSET"))
                            {
                                MainModel oModItemStock = new MainModel();
                                oModItemStock.GetSetcomp = modShipmentDet.GetSetcomp;
                                oModItemStock.GetSetitemno = modShipmentDet.GetSetitemno;
                                oModItemStock.GetSetitemdesc = modShipmentDet.GetSetitemdesc;
                                oModItemStock.GetSetlocation = modShipmentDet.GetSetlocation;
                                oModItemStock.GetSetdatesoh = modShipmentDet.GetSetdatesoh;
                                oModItemStock.GetSetqtysoh = oModLatestItemStock.GetSetqtysoh - modShipmentDet.GetSetshipment_quantity;
                                oModItemStock.GetSetcostsoh = Math.Round(Math.Round(oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh, 2, MidpointRounding.AwayFromZero) * (oModLatestItemStock.GetSetqtysoh - modShipmentDet.GetSetshipment_quantity), 2, MidpointRounding.AwayFromZero);
                                if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                                {
                                    String result1 = oMainCon.updateItemStock(oModItemStock);
                                }

                                MainModel oModItemStockTrans = new MainModel();
                                oModItemStockTrans.GetSetcomp = modShipmentDet.GetSetcomp;
                                oModItemStockTrans.GetSetitemno = modShipmentDet.GetSetitemno;
                                oModItemStockTrans.GetSetitemdesc = modShipmentDet.GetSetitemdesc;
                                oModItemStockTrans.GetSetlocation = modShipmentDet.GetSetlocation;
                                oModItemStockTrans.GetSetdatesoh = modShipmentDet.GetSetdatesoh;
                                oModItemStockTrans.GetSettranstype = "SHIPMENT";
                                oModItemStockTrans.GetSettransdate = oModHeader.GetSetconfirmeddate;
                                oModItemStockTrans.GetSettransno = modShipmentDet.GetSetshipmentno;
                                oModItemStockTrans.GetSettrans_lineno = modShipmentDet.GetSetlineno;
                                oModItemStockTrans.GetSetorderno = modShipmentDet.GetSetorderno;
                                oModItemStockTrans.GetSetorder_lineno = modShipmentDet.GetSetorder_lineno;
                                oModItemStockTrans.GetSettransqty = modShipmentDet.GetSetshipment_quantity * -1;
                                oModItemStockTrans.GetSettransprice = Math.Round(oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh, 2, MidpointRounding.AwayFromZero);
                                oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                                oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                                String result2 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                            }

                        }

                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //for invoicing - matzul 15/06/2019

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_RevenueList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String additionalquery = "";

        ArrayList lsRevenuDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            ArrayList lsParamType = oMainCon.getParametertype("INCOME");
            ArrayList revenuetype = new ArrayList();
            for (int i = 0; i < lsParamType.Count; i++)
            {
                MainModel modParam = (MainModel)lsParamType[i];
                revenuetype.Add(modParam.GetSetparamttype);
            }
            additionalquery = " and  (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat in ('RECEIPT_VOUCHER','JOURNAL_VOUCHER') ";
            String revtyp = "";
            for (int i = 0; i < revenuetype.Count; i++)
            {
                String str = (String)revenuetype[i];
                if (i.Equals(0))
                {
                    revtyp = "'" + str + "'";
                }
                else
                {
                    revtyp = revtyp + ",'" + str + "'";
                }
            }
            additionalquery = additionalquery + " and  invoice_header.invoicetype in (" + revtyp + "))) ";
            lsRevenuDetails = oMainCon.getRevenueListDetails(comp, selyear, selmonth, selday, status, additionalquery);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsRevenuDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoiceList(String comp, String searchtype, String searchitem, String datefrom, String dateto, String invoicecat, String invoicetype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsInvoiceHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsInvoiceHeader = oMainCon.getInvoiceHeaderListSearching(comp, searchtype, searchitem, datefrom, dateto, invoicecat, invoicetype, "");

        }

        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsInvoiceHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoiceHeaderList(String comp, String invoiceno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsInvoiceHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsInvoiceHeader = oMainCon.getInvoiceHeaderList(comp, invoiceno, "", "", "", "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsInvoiceHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoiceDetails(String comp, String invoiceno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modInvoiceHeaderDetails = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modInvoiceHeaderDetails = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(modInvoiceHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoiceItems(String comp, String invoiceno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsInvoiceDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsInvoiceDetails = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceDetails);
        jsonResponse = convertJson(lsInvoiceDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_InvoiceInvoice(String comp, String bpid, String invoicecat, String invoicetype, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get comp bp if exist
            MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
            if (oModBP.GetSetbpid.Trim().Length > 0)
            {
                MainModel oModInvoice = new MainModel();
                oModInvoice.GetSetcomp = comp;
                oModInvoice.GetSetinvoicedate = DateTime.Now.ToString("dd-MM-yyyy");
                oModInvoice.GetSetinvoicecat = invoicecat;
                oModInvoice.GetSetinvoicetype = invoicetype;
                oModInvoice.GetSetinvoiceterm = "NOT_APPLICABLE";
                String sInvoiceNo = oMainCon.getNextRunningNo(comp, "INVOICE", "ACTIVE");
                oModInvoice.GetSetinvoiceno = sInvoiceNo;
                oModInvoice.GetSetbpid = oModBP.GetSetbpid;
                oModInvoice.GetSetbpdesc = oModBP.GetSetbpdesc;
                oModInvoice.GetSetbpaddress = oModBP.GetSetbpaddress;
                oModInvoice.GetSetbpcontact = oModBP.GetSetbpcontact;
                oModInvoice.GetSetremarks = "";
                oModInvoice.GetSetstatus = "NEW";
                oModInvoice.GetSetcreatedby = userid;

                if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModInvoice.GetSetcomp, "INVOICE", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModInvoice.GetSetinvoiceno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_InvoiceHeaderStatus(String comp, String invoiceno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
            if (oModInvoice.GetSetinvoiceno.Trim().Length > 0)
            {
                oModInvoice.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModInvoice.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModInvoice.GetSetcancelledby = userid;
                }
                if (oMainCon.updateInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        if (oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE"))
                        {
                            ArrayList lsInvLineItem = oMainCon.getInvoiceDetailsList(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno, 0, "");
                            for (int i = 0; i < lsInvLineItem.Count; i++)
                            {
                                MainModel modInvDet = (MainModel)lsInvLineItem[i];

                                //to update Sales Order Invoice Amount
                                MainModel oModOrder = oMainCon.getOrderDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetorderno, modInvDet.GetSetorder_lineno, "");
                                oModOrder.GetSetinvoiceamount = oModOrder.GetSetinvoiceamount + modInvDet.GetSettotalinvoice;
                                String result = oMainCon.updateOrderDetails(oModOrder);

                                //update status for shipment has invoice
                                MainModel oModShipment = oMainCon.getShipmentDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetshipmentno, modInvDet.GetSetshipment_lineno, "");
                                oModShipment.GetSethasinvoice = "Y";
                                result = oMainCon.updateShipmentDetails(oModShipment);
                            }
                        }
                        else if (oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE"))
                        {
                            ArrayList lsInvLineItem = oMainCon.getInvoiceDetailsList(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno, 0, "");
                            for (int i = 0; i < lsInvLineItem.Count; i++)
                            {
                                MainModel modInvDet = (MainModel)lsInvLineItem[i];

                                //to update transfer Order Invoice Amount
                                MainModel oModOrder = oMainCon.getTransferOrderDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetorderno, modInvDet.GetSetorder_lineno, "");
                                oModOrder.GetSetinvoiceamount = oModOrder.GetSetinvoiceamount + modInvDet.GetSettotalinvoice;
                                String result = oMainCon.updateTransferOrderDetails(oModOrder);

                                //update status for shipment has invoice
                                MainModel oModShipment = oMainCon.getShipmentDetailsDetails(modInvDet.GetSetcomp, modInvDet.GetSetshipmentno, modInvDet.GetSetshipment_lineno, "");
                                oModShipment.GetSethasinvoice = "Y";
                                result = oMainCon.updateShipmentDetails(oModShipment);
                            }

                        }
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingInvoiceList(String comp, String bpid, String invoicecat, String invoicetype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendInvoiceList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPendInvoiceList = oMainCon.getLineItemPendingInvoice(comp, bpid, "", invoicecat, invoicetype, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendInvoiceList);
        jsonResponse = convertJson(lsPendInvoiceList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingInvoiceShipmentList(String comp, String bpid, String ordertype, String invoicecat, String shipmentno, String orderno, String itemno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendInvoiceList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPendInvoiceList = oMainCon.getLineItemPendingInvoice(comp, bpid, ordertype, invoicecat, shipmentno, orderno, itemno);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendInvoiceList);
        jsonResponse = convertJson(lsPendInvoiceList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_InvoiceShipmentItemHeader(String comp, String invoicecat, String shipmentno, int shipmentlineno, String orderno, int orderlineno, String itemno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String invoiceno = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendInvoiceList = oMainCon.getLineItemPendingInvoice(comp, "", "", invoicecat, shipmentno, shipmentlineno, orderno, orderlineno, itemno);
            if (lsPendInvoiceList.Count > 0)
            {
                //create new invoice header
                MainModel oModItem = (MainModel)lsPendInvoiceList[0];

                MainModel modExistent = oMainCon.getInvoiceDetailsDetails(comp, "", 0, shipmentno, shipmentlineno, itemno, "NEW");
                if (modExistent.GetSetorderno.Length > 0)
                {
                    sMessage = "Invoice item already assigned, Please contact System Admin!";
                }
                else
                {
                    //get comp bp if exist
                    MainModel oModBP = oMainCon.getBPDetails(comp, oModItem.GetSetbpid);
                    if (oModBP.GetSetbpid.Trim().Length > 0)
                    {
                        MainModel oModInvoice = new MainModel();
                        oModInvoice.GetSetcomp = comp;
                        oModInvoice.GetSetinvoicedate = DateTime.Now.ToString("dd-MM-yyyy");
                        oModInvoice.GetSetinvoicecat = invoicecat;
                        oModInvoice.GetSetinvoicetype = "NOT_APPLICABLE";
                        oModInvoice.GetSetinvoiceterm = "NOT_APPLICABLE";
                        invoiceno = oMainCon.getNextRunningNo(comp, "INVOICE", "ACTIVE");
                        oModInvoice.GetSetinvoiceno = invoiceno;
                        oModInvoice.GetSetbpid = oModBP.GetSetbpid;
                        oModInvoice.GetSetbpdesc = oModBP.GetSetbpdesc;
                        oModInvoice.GetSetbpaddress = oModBP.GetSetbpaddress;
                        oModInvoice.GetSetbpcontact = oModBP.GetSetbpcontact;
                        oModInvoice.GetSetremarks = "";
                        oModInvoice.GetSetstatus = "NEW";
                        oModInvoice.GetSetcreatedby = userid;

                        if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))
                        {
                            oMainCon.updateNextRunningNo(oModInvoice.GetSetcomp, "INVOICE", "ACTIVE");
                            sStatus = "Y";
                            sMessage = invoiceno;
                        }
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Internal Server Error, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_InvoiceShipmentItemDetails(String comp, String invoiceno, String invoicecat, String shipmentno, int shipmentlineno, String orderno, int orderlineno, String itemno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendInvoiceList = oMainCon.getLineItemPendingInvoice(comp, "", "", invoicecat, shipmentno, shipmentlineno, orderno, orderlineno, itemno);
            //insert item for invoice details
            for (int i = 0; i < lsPendInvoiceList.Count; i++)
            {
                MainModel oModLineItem = (MainModel)lsPendInvoiceList[i];
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetinvoiceno = invoiceno;
                oModLineItem.GetSetlineno = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "").Count + 1;
                oModLineItem.GetSetshipmentno = oModLineItem.GetSetshipmentno;
                oModLineItem.GetSetshipment_lineno = oModLineItem.GetSetlineno;
                oModLineItem.GetSetorderno = oModLineItem.GetSetorderno;
                oModLineItem.GetSetorder_lineno = oModLineItem.GetSetorder_lineno;
                oModLineItem.GetSetitemno = oModLineItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModLineItem.GetSetitemdesc;
                oModLineItem.GetSetunitcost = oModLineItem.GetSetunitcost;
                oModLineItem.GetSetunitprice = oModLineItem.GetSetunitprice;
                oModLineItem.GetSetdiscamount = oModLineItem.GetSetdiscamount;
                oModLineItem.GetSetquantity = oModLineItem.GetSetquantity;
                oModLineItem.GetSetcostprice = oModLineItem.GetSetcostprice;
                oModLineItem.GetSetinvoiceprice = oModLineItem.GetSetinvoiceprice;
                oModLineItem.GetSettaxcode = oModLineItem.GetSettaxcode;
                oModLineItem.GetSettaxrate = oModLineItem.GetSettaxrate;
                oModLineItem.GetSettaxamount = oModLineItem.GetSettaxamount;
                oModLineItem.GetSettotalinvoice = oModLineItem.GetSettotalinvoice;

                MainModel modExistent = oMainCon.getInvoiceDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetshipmentno, oModLineItem.GetSetshipment_lineno, itemno, "NEW");
                if (modExistent.GetSetinvoiceno.Length > 0)
                {
                    sStatus = "N";
                    sMessage = "Invoice item already assigned, Please contact System Admin!";
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertInvoiceDetails(oModLineItem).Equals("Y"))
                    {
                        //update invoice header information
                        sStatus = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);
                        sMessage = "";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Unable to add invoice item, Please contact System Admin!";
                    }
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_InvoiceItemDetails(String comp, String invoiceno, String shipmentno, int shipmentlineno, String orderno, int lineno, String itemno, String itemdesc, double unitcost, double unitprice, double discamount, int quantity, double costprice, double invoiceprice, String taxcode, int taxrate, double taxamount, double totalinvoice)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetinvoiceno = invoiceno;
            oModLineItem.GetSetlineno = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "").Count + 1;
            oModLineItem.GetSetshipmentno = shipmentno;
            oModLineItem.GetSetshipment_lineno = shipmentlineno;
            oModLineItem.GetSetorderno = orderno;
            oModLineItem.GetSetorder_lineno = lineno;
            oModLineItem.GetSetitemno = itemno;
            oModLineItem.GetSetitemdesc = itemdesc;
            oModLineItem.GetSetunitcost = unitcost;
            oModLineItem.GetSetunitprice = unitprice;
            oModLineItem.GetSetdiscamount = discamount;
            oModLineItem.GetSetquantity = quantity;
            oModLineItem.GetSetcostprice = costprice;
            oModLineItem.GetSetinvoiceprice = invoiceprice;
            oModLineItem.GetSettaxcode = taxcode;
            oModLineItem.GetSettaxrate = taxrate;
            oModLineItem.GetSettaxamount = taxamount;
            oModLineItem.GetSettotalinvoice = totalinvoice;

            MainModel modExistent = oMainCon.getInvoiceDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetshipmentno, oModLineItem.GetSetshipment_lineno, itemno, "NEW");
            if (modExistent.GetSetinvoiceno.Length > 0)
            {
                sStatus = "N";
                sMessage = "Invoice item already assigned, Please contact System Admin!";
            }
            else
            {
                //insert new line item
                if (oMainCon.insertInvoiceDetails(oModLineItem).Equals("Y"))
                {
                    //update invoice header information
                    sStatus = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Unable to add invoice item, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string editMobile_InvoiceItemDetails(String comp, String invoiceno, int lineno, String shipmentno, int shipmentlineno, String itemno, String itemdesc, int qty, double unitprice, String taxcode)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getInvoiceDetailsDetails(comp, invoiceno, lineno, shipmentno, shipmentlineno, itemno, "");
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                oModItem.GetSetitemdesc = itemdesc;
                oModItem.GetSetquantity = qty;
                oModItem.GetSetunitprice = unitprice;
                oModItem.GetSetinvoiceprice = (oModItem.GetSetunitprice - oModItem.GetSetdiscamount) * oModItem.GetSetquantity;

                if (taxcode.Trim().Length > 0)
                {
                    oModItem.GetSettaxcode = taxcode;
                }
                else
                {
                    oModItem.GetSettaxcode = "NA";
                }
                MainModel modTaxCode = oMainCon.getTaxDetails(comp, oModItem.GetSettaxcode);
                oModItem.GetSettaxrate = modTaxCode.GetSettaxrate;
                oModItem.GetSettaxamount = oModItem.GetSetinvoiceprice * modTaxCode.GetSettaxrate / 100;

                oModItem.GetSettotalinvoice = oModItem.GetSetinvoiceprice + oModItem.GetSettaxamount;

                if (oMainCon.updateInvoiceDetails(oModItem).Equals("Y"))
                {
                    //update invoice header information
                    sStatus = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string deleteMobile_InvoiceItemDetails(String comp, String invoiceno, int lineno, String shipmentno, int shipmentlineno, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getInvoiceDetailsDetails(comp, invoiceno, lineno, shipmentno, shipmentlineno, itemno, "");
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (oMainCon.deleteInvoiceDetails(oModItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteInvoiceDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertInvoiceDetails(oLineItem);
                    }
                    //update invoice header information
                    String result = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoicePayRcptItem(String comp, String invoiceno, String payrcptno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPayRcptHeaderDetailsList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPayRcptHeaderDetailsList = oMainCon.getPaymentReceiptHeaderDetailsList(comp, payrcptno, "", 0, invoiceno, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPayRcptHeaderDetailsList);
        jsonResponse = convertJson(lsPayRcptHeaderDetailsList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_InvoicePayRcptList(String comp, String invoiceno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countPayRcpt = 0;
        String payrcptNo = "";
        ArrayList lsPayRcptNo = new ArrayList();

        object objOrderPayRcptList = new { countpayrcpt = countPayRcpt, arraypayrcptno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPayRcptHeaderDetailsList = oMainCon.getPaymentReceiptDetailsList(comp, "", 0, invoiceno);
            if (lsPayRcptHeaderDetailsList.Count > 0)
            {
                for (int i = 0; i < lsPayRcptHeaderDetailsList.Count; i++)
                {
                    MainModel modPayRcpt = (MainModel)lsPayRcptHeaderDetailsList[i];
                    if (i.Equals(0))
                    {
                        payrcptNo = modPayRcpt.GetSetpayrcptno;
                        countPayRcpt = countPayRcpt + 1;
                        lsPayRcptNo.Add(payrcptNo);
                    }
                    else
                    {
                        if (payrcptNo != modPayRcpt.GetSetpayrcptno)
                        {
                            payrcptNo = modPayRcpt.GetSetpayrcptno;
                            countPayRcpt = countPayRcpt + 1;
                            lsPayRcptNo.Add(payrcptNo);
                        }
                    }

                }
                objOrderPayRcptList = new { countpayrcpt = countPayRcpt, arraypayrcptno = lsPayRcptNo };
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objOrderPayRcptList);
        jsonResponse = convertJson(objOrderPayRcptList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingPayRcptList(String comp, String bpid, String invoiceno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countpendpayrcpt = 0;

        object objPendPayRcptList = new { countpendpayrcpt = countpendpayrcpt, arraypendpayrcptno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendPayRcptList = oMainCon.getLineItemPendingPaymentReceipt(comp, bpid, "", invoiceno);
            if (lsPendPayRcptList.Count > 0)
            {
                countpendpayrcpt = lsPendPayRcptList.Count;
                objPendPayRcptList = new { countpendpayrcpt = countpendpayrcpt, arraypendpayrcptno = lsPendPayRcptList };
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objPendPayRcptList);
        jsonResponse = convertJson(objPendPayRcptList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_PayRcptInvoice(String comp, String invoiceno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get invoice header if exist
            MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
            if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetpayrcptdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModHeader.GetSetpayrcpttype = "INVOICE";
                String sPayRcptNo = oMainCon.getNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
                oModHeader.GetSetpayrcptno = sPayRcptNo;
                oModHeader.GetSetbpid = oModInvoice.GetSetbpid;
                oModHeader.GetSetbpdesc = oModInvoice.GetSetbpdesc;
                oModHeader.GetSetbpaddress = oModInvoice.GetSetbpaddress;
                oModHeader.GetSetbpcontact = oModInvoice.GetSetbpcontact;
                oModHeader.GetSetremarks = oModInvoice.GetSetremarks;
                oModHeader.GetSetstatus = "NEW";
                oModHeader.GetSetcreatedby = userid;

                if (oMainCon.insertPaymentReceiptHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModHeader.GetSetpayrcptno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_PayRcptItemInvoice(String comp, String payrcptno, int payrcptlineno, String invoiceno, String invoicedate, double invoiceprice, String paytype, String payrefno, double payrcptprice, String payremarks)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get invoice header if exist
            MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
            if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetpayrcptno = payrcptno;
                oModLineItem.GetSetlineno = payrcptlineno;
                oModLineItem.GetSetinvoiceno = invoiceno;
                oModLineItem.GetSetinvoicedate = invoicedate;
                oModLineItem.GetSetinvoiceprice = invoiceprice;
                oModLineItem.GetSetpaytype = paytype;
                oModLineItem.GetSetpayrefno = payrefno;
                oModLineItem.GetSetpayrcptprice = payrcptprice;
                oModLineItem.GetSetpayremarks = payremarks;

                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPaymentReceiptDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetinvoiceno, "NEW");
                if (modExistent.GetSetpayrcptno.Length > 0)
                {
                    sStatus = "Y";
                    sMessage = "Item tersebut telah ditambah pada Bayaran Terima: " + modExistent.GetSetpayrcptno;
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPaymentReceiptDetails(oModLineItem).Equals("Y"))
                    {
                        //update payment receipt header information
                        sStatus = oMainCon.updatePaymentReceiptHeaderInfo(comp, payrcptno);
                        sMessage = "";
                    }
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_PayRcptInvoiceStatus(String comp, String payrcptno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(comp, payrcptno);
            if (oModPayRcpt.GetSetpayrcptno.Trim().Length > 0)
            {
                oModPayRcpt.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModPayRcpt.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModPayRcpt.GetSetcancelledby = userid;
                }
                if (oMainCon.updatePaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        ArrayList lsPayRcptLineItem = oMainCon.getPaymentReceiptDetailsList(oModPayRcpt.GetSetcomp, oModPayRcpt.GetSetpayrcptno, 0, "");
                        for (int i = 0; i < lsPayRcptLineItem.Count; i++)
                        {
                            MainModel modPayRcptDet = (MainModel)lsPayRcptLineItem[i];

                            //to update Invoice & receipt Amount
                            MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(modPayRcptDet.GetSetcomp, modPayRcptDet.GetSetinvoiceno);
                            oModInvoice.GetSetpayrcptamount = oModInvoice.GetSetpayrcptamount + modPayRcptDet.GetSetpayrcptprice;
                            String result = oMainCon.updateInvoiceHeader(oModInvoice);
                        }

                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //for expenses - matzul 24/06/2019
    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_SpendingList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String additionalquery = "";

        ArrayList lsExpensesDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            ArrayList lsParamType2 = oMainCon.getParametertype("EXPENSES");
            ArrayList expensestype = new ArrayList();
            for (int i = 0; i < lsParamType2.Count; i++)
            {
                MainModel modParam = (MainModel)lsParamType2[i];
                expensestype.Add(modParam.GetSetparamttype);
            }

            additionalquery = " and  (expenses_header.expensescat = 'PURCHASE_INVOICE' or expenses_header.expensescat = 'TRANSFER_INVOICE' or (expenses_header.expensescat in ('PAYMENT_VOUCHER','JOURNAL_VOUCHER') ";
            String exptyp = "";
            for (int i = 0; i < expensestype.Count; i++)
            {
                String str = (String)expensestype[i];
                if (i.Equals(0))
                {
                    exptyp = "'" + str + "'";
                }
                else
                {
                    exptyp = exptyp + ",'" + str + "'";
                }
            }
            additionalquery = additionalquery + " and  expenses_header.expensestype in (" + exptyp + "))) ";
            additionalquery = additionalquery + @"  and  NOT EXISTS (select item.itemno from item, expenses_details 
                                                        where expenses_header.expensesno = expenses_details.expensesno and expenses_header.comp = expenses_details.comp and expenses_header.expensescat = 'PURCHASE_INVOICE'
                                                        and expenses_details.itemno = item.itemno and expenses_details.comp = item.comp
                                                        and item.itemcat in ('INVENTORY','ASSET'))";
            lsExpensesDetails = oMainCon.getExpensesListDetails(comp, selyear, selmonth, selday, status, additionalquery);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsExpensesDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesList(String comp, String searchtype, String searchitem, String datefrom, String dateto, String expensescat, String expensestype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsExpensesHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsExpensesHeader = oMainCon.getExpensesHeaderListSearching(comp, searchtype, searchitem, datefrom, dateto, expensescat, expensestype, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsExpensesHeader);
        jsonResponse = convertJson(lsExpensesHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesHeaderList(String comp, String expensesno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsExpensesHeader = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsExpensesHeader = oMainCon.getExpensesHeaderList(comp, expensesno, "", "", "", "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsExpensesHeader);
        jsonResponse = convertJson(lsExpensesHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesDetails(String comp, String expensesno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modInvoiceHeaderDetails = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modInvoiceHeaderDetails = oMainCon.getExpensesHeaderDetails(comp, expensesno);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(modInvoiceHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesItems(String comp, String expensesno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsExpensesDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsExpensesDetails = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "");

        }

        //jsonResponse = new JavaScriptSerializer().Serialize(lsExpensesDetails);
        jsonResponse = convertJson(lsExpensesDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ExpensesHeader(String comp, String bpid, String expensescat, String expensestype, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get comp bp if exist
            MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
            if (oModBP.GetSetbpid.Trim().Length > 0)
            {
                MainModel oModExpenses = new MainModel();
                oModExpenses.GetSetcomp = comp;
                oModExpenses.GetSetexpensesdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModExpenses.GetSetexpensescat = expensescat;
                oModExpenses.GetSetexpensestype = expensestype;
                String sExpensesNo = oMainCon.getNextRunningNo(comp, "EXPENSES", "ACTIVE");
                oModExpenses.GetSetexpensesno = sExpensesNo;
                oModExpenses.GetSetbpid = oModBP.GetSetbpid;
                oModExpenses.GetSetbpdesc = oModBP.GetSetbpdesc;
                oModExpenses.GetSetbpaddress = oModBP.GetSetbpaddress;
                oModExpenses.GetSetbpcontact = oModBP.GetSetbpcontact;
                oModExpenses.GetSetremarks = "";
                oModExpenses.GetSetstatus = "NEW";
                oModExpenses.GetSetcreatedby = userid;

                if (oMainCon.insertExpensesHeader(oModExpenses).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModExpenses.GetSetcomp, "EXPENSES", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModExpenses.GetSetexpensesno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ExpensesHeaderStatus(String comp, String expensesno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
            if (oModExpenses.GetSetexpensesno.Trim().Length > 0)
            {
                oModExpenses.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModExpenses.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModExpenses.GetSetcancelledby = userid;
                }
                if (oMainCon.updateExpensesHeader(oModExpenses).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        if (oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE"))
                        {
                            ArrayList lsExpLineItem = oMainCon.getExpensesDetailsList(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno, 0, "");
                            for (int i = 0; i < lsExpLineItem.Count; i++)
                            {
                                MainModel modExpDet = (MainModel)lsExpLineItem[i];

                                //to update Purchase Order Expenses Amount
                                MainModel oModOrder = oMainCon.getPurchaseOrderDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetorderno, modExpDet.GetSetorder_lineno, "");
                                oModOrder.GetSetbillingamount = oModOrder.GetSetbillingamount + modExpDet.GetSettotalexpenses;
                                String result = oMainCon.updatePurchaseOrderDetails(oModOrder);

                                //update status for purchase has invoice (Receipt)
                                MainModel oModReceipt = oMainCon.getReceiptDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetreceiptno, modExpDet.GetSetreceipt_lineno, "");
                                oModReceipt.GetSethasbilling = "Y";
                                result = oMainCon.updateReceiptDetails(oModReceipt);
                            }
                        }
                        else if (oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE"))
                        {
                            ArrayList lsExpLineItem = oMainCon.getExpensesDetailsList(oModExpenses.GetSetcomp, oModExpenses.GetSetexpensesno, 0, "");
                            for (int i = 0; i < lsExpLineItem.Count; i++)
                            {
                                MainModel modExpDet = (MainModel)lsExpLineItem[i];

                                //to update Transfer Order Expenses Amount
                                MainModel oModTranferHeader = oMainCon.getTransferOrderHeaderDetails("", "", modExpDet.GetSetcomp, modExpDet.GetSetorderno);
                                MainModel oModOrder = oMainCon.getTransferOrderDetailsDetails(oModTranferHeader.GetSetCompFromDetails.GetSetcomp, modExpDet.GetSetorderno, modExpDet.GetSetorder_lineno, "");
                                oModOrder.GetSetbillingamount = oModOrder.GetSetbillingamount + modExpDet.GetSettotalexpenses;
                                String result = oMainCon.updateTransferOrderDetails(oModOrder);

                                //update status for Transfer has invoice (Receipt)
                                MainModel oModReceipt = oMainCon.getReceiptDetailsDetails(modExpDet.GetSetcomp, modExpDet.GetSetreceiptno, modExpDet.GetSetreceipt_lineno, "");
                                oModReceipt.GetSethasbilling = "Y";
                                result = oMainCon.updateReceiptDetails(oModReceipt);

                            }
                        }
                    }
                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingExpensesList(String comp, String bpid, String expensescat, String expensestype)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendExpensesList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPendExpensesList = oMainCon.getLineItemPendingExpenses(comp, bpid, "", expensescat, expensestype, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendExpensesList);
        jsonResponse = convertJson(lsPendExpensesList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingExpensesReceiptList(String comp, String bpid, String ordertype, String expensescat, String receiptno, String orderno, String itemno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendExpensesList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPendExpensesList = oMainCon.getLineItemPendingExpenses(comp, bpid, ordertype, expensescat, receiptno, orderno, itemno);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendExpensesList);
        jsonResponse = convertJson(lsPendExpensesList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ExpensesReceiptItemHeader(String comp, String expensescat, String receiptno, int receiptlineno, String orderno, int orderlineno, String itemno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String expensesno = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendExpensesList = oMainCon.getLineItemPendingExpenses(comp, "", "", expensescat, receiptno, receiptlineno, orderno, orderlineno, itemno);
            if (lsPendExpensesList.Count > 0)
            {
                //create new expenses header
                MainModel oModItem = (MainModel)lsPendExpensesList[0];

                MainModel modExistent = oMainCon.getExpensesDetailsDetails(comp, "", 0, receiptno, receiptlineno, itemno, "NEW");
                if (modExistent.GetSetexpensesno.Length > 0)
                {
                    sMessage = "Expenses item already assigned, Please contact System Admin!";
                }
                else
                {
                    //get comp bp if exist
                    MainModel oModBP = oMainCon.getBPDetails(comp, oModItem.GetSetbpid);
                    if (oModBP.GetSetbpid.Trim().Length > 0)
                    {
                        MainModel oModExpenses = new MainModel();
                        oModExpenses.GetSetcomp = comp;
                        oModExpenses.GetSetexpensesdate = DateTime.Now.ToString("dd-MM-yyyy");
                        oModExpenses.GetSetexpensescat = expensescat;
                        oModExpenses.GetSetexpensestype = "NOT_APPLICABLE";
                        expensesno = oMainCon.getNextRunningNo(comp, "EXPENSES", "ACTIVE");
                        oModExpenses.GetSetexpensesno = expensesno;
                        oModExpenses.GetSetbpid = oModBP.GetSetbpid;
                        oModExpenses.GetSetbpdesc = oModBP.GetSetbpdesc;
                        oModExpenses.GetSetbpaddress = oModBP.GetSetbpaddress;
                        oModExpenses.GetSetbpcontact = oModBP.GetSetbpcontact;
                        oModExpenses.GetSetremarks = "";
                        oModExpenses.GetSetstatus = "NEW";
                        oModExpenses.GetSetcreatedby = userid;

                        if (oMainCon.insertExpensesHeader(oModExpenses).Equals("Y"))
                        {
                            oMainCon.updateNextRunningNo(oModExpenses.GetSetcomp, "EXPENSES", "ACTIVE");
                            sStatus = "Y";
                            sMessage = expensesno;
                        }
                    }
                }
            }
            else
            {
                sStatus = "N";
                sMessage = "Internal Server Error, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ExpensesReceiptItemDetails(String comp, String expensesno, String expensescat, String receiptno, int receiptlineno, String orderno, int orderlineno, String itemno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendExpensesList = oMainCon.getLineItemPendingExpenses(comp, "", "", expensescat, receiptno, receiptlineno, orderno, orderlineno, itemno);
            //insert item for expenses details
            for (int i = 0; i < lsPendExpensesList.Count; i++)
            {
                MainModel oModLineItem = (MainModel)lsPendExpensesList[i];
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetexpensesno = expensesno;
                oModLineItem.GetSetlineno = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "").Count + 1;
                oModLineItem.GetSetreceiptno = oModLineItem.GetSetreceiptno;
                oModLineItem.GetSetreceipt_lineno = oModLineItem.GetSetlineno;
                oModLineItem.GetSetorderno = oModLineItem.GetSetorderno;
                oModLineItem.GetSetorder_lineno = oModLineItem.GetSetorder_lineno;
                oModLineItem.GetSetitemno = oModLineItem.GetSetitemno;
                oModLineItem.GetSetitemdesc = oModLineItem.GetSetitemdesc;
                oModLineItem.GetSetunitcost = oModLineItem.GetSetunitcost;
                oModLineItem.GetSetunitprice = oModLineItem.GetSetunitprice;
                oModLineItem.GetSetdiscamount = oModLineItem.GetSetdiscamount;
                oModLineItem.GetSetquantity = oModLineItem.GetSetquantity;
                oModLineItem.GetSetcostprice = oModLineItem.GetSetcostprice;
                oModLineItem.GetSetexpensesprice = oModLineItem.GetSetexpensesprice;
                oModLineItem.GetSettaxcode = oModLineItem.GetSettaxcode;
                oModLineItem.GetSettaxrate = oModLineItem.GetSettaxrate;
                oModLineItem.GetSettaxamount = oModLineItem.GetSettaxamount;
                oModLineItem.GetSettotalexpenses = oModLineItem.GetSettotalexpenses;

                MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, itemno, "NEW");
                if (modExistent.GetSetinvoiceno.Length > 0)
                {
                    sStatus = "N";
                    sMessage = "Expenses item already assigned, Please contact System Admin!";
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
                    {
                        //update expenses header information
                        sStatus = oMainCon.updateExpensesHeaderInfo(comp, expensesno);
                        sMessage = "";
                    }
                    else
                    {
                        sStatus = "N";
                        sMessage = "Unable to add expenses item, Please contact System Admin!";
                    }
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ExpensesItemDetails(String comp, String expensesno, String receiptno, int receiptlineno, String orderno, int lineno, String itemno, String itemdesc, double unitcost, double unitprice, double discamount, int quantity, double costprice, double expensesprice, String taxcode, int taxrate, double taxamount, double totalexpenses)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetexpensesno = expensesno;
            oModLineItem.GetSetlineno = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "").Count + 1;
            oModLineItem.GetSetreceiptno = receiptno;
            oModLineItem.GetSetreceipt_lineno = receiptlineno;
            oModLineItem.GetSetorderno = orderno;
            oModLineItem.GetSetorder_lineno = lineno;
            oModLineItem.GetSetitemno = itemno;
            oModLineItem.GetSetitemdesc = itemdesc;
            oModLineItem.GetSetunitcost = unitcost;
            oModLineItem.GetSetunitprice = unitprice;
            oModLineItem.GetSetdiscamount = discamount;
            oModLineItem.GetSetquantity = quantity;
            oModLineItem.GetSetcostprice = costprice;
            oModLineItem.GetSetexpensesprice = expensesprice;
            oModLineItem.GetSettaxcode = taxcode;
            oModLineItem.GetSettaxrate = taxrate;
            oModLineItem.GetSettaxamount = taxamount;
            oModLineItem.GetSettotalexpenses = totalexpenses;

            MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, itemno, "NEW");
            if (modExistent.GetSetexpensesno.Length > 0)
            {
                sStatus = "N";
                sMessage = "Expenses item already assigned, Please contact System Admin!";
            }
            else
            {
                //insert new line item
                if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
                {
                    //update invoice header information
                    sStatus = oMainCon.updateExpensesHeaderInfo(comp, expensesno);
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Unable to add expenses item, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string editMobile_ExpensesItemDetails(String comp, String expensesno, int lineno, String receiptno, int receiptlineno, String itemno, String itemdesc, int qty, double unitprice, String taxcode)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getExpensesDetailsDetails(comp, expensesno, lineno, receiptno, receiptlineno, itemno, "");
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                oModItem.GetSetitemdesc = itemdesc;
                oModItem.GetSetquantity = qty;
                oModItem.GetSetunitprice = unitprice;
                oModItem.GetSetexpensesprice = (oModItem.GetSetunitprice - oModItem.GetSetdiscamount) * oModItem.GetSetquantity;

                if (taxcode.Trim().Length > 0)
                {
                    oModItem.GetSettaxcode = taxcode;
                }
                else
                {
                    oModItem.GetSettaxcode = "NA";
                }
                MainModel modTaxCode = oMainCon.getTaxDetails(comp, oModItem.GetSettaxcode);
                oModItem.GetSettaxrate = modTaxCode.GetSettaxrate;
                oModItem.GetSettaxamount = oModItem.GetSetexpensesprice * modTaxCode.GetSettaxrate / 100;

                oModItem.GetSettotalexpenses = oModItem.GetSetexpensesprice + oModItem.GetSettaxamount;

                if (oMainCon.updateExpensesDetails(oModItem).Equals("Y"))
                {
                    //update expenses header information
                    sStatus = oMainCon.updateExpensesHeaderInfo(comp, expensesno);
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string deleteMobile_ExpensesItemDetails(String comp, String expensesno, int lineno, String receiptno, int receiptlineno, String itemno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get item details if exist
            MainModel oModItem = oMainCon.getExpensesDetailsDetails(comp, expensesno, lineno, receiptno, receiptlineno, itemno, "");
            if (oModItem.GetSetitemno.Trim().Length > 0)
            {
                if (oMainCon.deleteExpensesDetails(oModItem).Equals("Y"))
                {
                    //rearrange the line no
                    ArrayList lsLineItemNew = new ArrayList();
                    ArrayList lsLineItem = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "");
                    for (int x = 0; x < lsLineItem.Count; x++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItem[x];
                        if (!oLineItem.GetSetlineno.Equals(oModItem.GetSetlineno))
                        {
                            lsLineItemNew.Add(oLineItem);
                        }
                        String flag = oMainCon.deleteExpensesDetails(oLineItem);
                    }
                    for (int y = 0; y < lsLineItemNew.Count; y++)
                    {
                        MainModel oLineItem = (MainModel)lsLineItemNew[y];
                        oLineItem.GetSetlineno = y + 1;
                        String flag2 = oMainCon.insertExpensesDetails(oLineItem);
                    }
                    //update expenses header information
                    String result = oMainCon.updateExpensesHeaderInfo(comp, expensesno);

                    sStatus = "Y";
                    sMessage = "";
                }
            }

        }
        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesPayPaidItem(String comp, String expensesno, String paypaidno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPayPaidHeaderDetailsList = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsPayPaidHeaderDetailsList = oMainCon.getPaymentPaidHeaderDetailsList(comp, paypaidno, "", 0, expensesno, "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsPayPaidHeaderDetailsList);
        jsonResponse = convertJson(lsPayPaidHeaderDetailsList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesPayPaidList(String comp, String expensesno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countPayPaid = 0;
        String paypaidNo = "";
        ArrayList lsPayPaidNo = new ArrayList();

        object objOrderPayPaidList = new { countpaypaid = countPayPaid, arraypaypaidno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPayPaidHeaderDetailsList = oMainCon.getPaymentPaidDetailsList(comp, "", 0, expensesno);
            if (lsPayPaidHeaderDetailsList.Count > 0)
            {
                for (int i = 0; i < lsPayPaidHeaderDetailsList.Count; i++)
                {
                    MainModel modPayPaid = (MainModel)lsPayPaidHeaderDetailsList[i];
                    if (i.Equals(0))
                    {
                        paypaidNo = modPayPaid.GetSetpaypaidno;
                        countPayPaid = countPayPaid + 1;
                        lsPayPaidNo.Add(paypaidNo);
                    }
                    else
                    {
                        if (paypaidNo != modPayPaid.GetSetpaypaidno)
                        {
                            paypaidNo = modPayPaid.GetSetpaypaidno;
                            countPayPaid = countPayPaid + 1;
                            lsPayPaidNo.Add(paypaidNo);
                        }
                    }

                }
                objOrderPayPaidList = new { countpaypaid = countPayPaid, arraypaypaidno = lsPayPaidNo };
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objOrderPayPaidList);
        jsonResponse = convertJson(objOrderPayPaidList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PendingPayPaidList(String comp, String bpid, String expensesno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        int countpendpaypaid = 0;

        object objPendPayPaidList = new { countpendpaypaid = countpendpaypaid, arraypendpaypaidno = new { } };

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsPendPayPaidList = oMainCon.getLineItemPendingPaymentPaid(comp, bpid, "", expensesno);
            if (lsPendPayPaidList.Count > 0)
            {
                countpendpaypaid = lsPendPayPaidList.Count;
                objPendPayPaidList = new { countpendpaypaid = countpendpaypaid, arraypendpaypaidno = lsPendPayPaidList };
            }

        }

        //jsonResponse = new JavaScriptSerializer().Serialize(objPendPayPaidList);
        jsonResponse = convertJson(objPendPayPaidList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_PayPaidExpenses(String comp, String expensesno, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get expenses header if exist
            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
            if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
            {
                MainModel oModHeader = new MainModel();
                oModHeader.GetSetcomp = comp;
                oModHeader.GetSetexpensesdate = DateTime.Now.ToString("dd-MM-yyyy");
                oModHeader.GetSetpaypaidtype = "EXPENSES";
                String sPayPaidNo = oMainCon.getNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
                oModHeader.GetSetpaypaidno = sPayPaidNo;
                oModHeader.GetSetbpid = oModExpenses.GetSetbpid;
                oModHeader.GetSetbpdesc = oModExpenses.GetSetbpdesc;
                oModHeader.GetSetbpaddress = oModExpenses.GetSetbpaddress;
                oModHeader.GetSetbpcontact = oModExpenses.GetSetbpcontact;
                oModHeader.GetSetremarks = oModExpenses.GetSetremarks;
                oModHeader.GetSetstatus = "NEW";
                oModHeader.GetSetcreatedby = userid;

                if (oMainCon.insertPaymentPaidHeader(oModHeader).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
                    sStatus = "Y";
                    sMessage = oModHeader.GetSetpaypaidno;
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_PayPaidItemExpenses(String comp, String paypaidno, int paypaidlineno, String expensesno, String expensesdate, double expensesprice, String paytype, String payrefno, double paypaidprice, String payremarks)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            //get expenses header if exist
            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
            if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
            {
                MainModel oModLineItem = new MainModel();
                oModLineItem.GetSetcomp = comp;
                oModLineItem.GetSetpaypaidno = paypaidno;
                oModLineItem.GetSetlineno = paypaidlineno;
                oModLineItem.GetSetexpensesno = expensesno;
                oModLineItem.GetSetexpensesdate = expensesdate;
                oModLineItem.GetSetexpensesprice = expensesprice;
                oModLineItem.GetSetpaytype = paytype;
                oModLineItem.GetSetpayrefno = payrefno;
                oModLineItem.GetSetpaypaidprice = paypaidprice;
                oModLineItem.GetSetpayremarks = payremarks;

                //check whether already exist in Other Line Item that is not confirm yet or not
                MainModel modExistent = oMainCon.getPaymentPaidDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetexpensesno, "NEW");
                if (modExistent.GetSetpaypaidno.Length > 0)
                {
                    sStatus = "Y";
                    sMessage = "Item tersebut telah ditambah pada Bayaran Belanja: " + modExistent.GetSetpayrcptno;
                }
                else
                {
                    //insert new line item
                    if (oMainCon.insertPaymentPaidDetails(oModLineItem).Equals("Y"))
                    {
                        //update payment paid header information
                        sStatus = oMainCon.updatePaymentPaidHeaderInfo(comp, paypaidno);
                        sMessage = "";
                    }
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_PayPaidExpensesStatus(String comp, String paypaidno, String status, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModPayPaid = oMainCon.getPaymentPaidHeaderDetails(comp, paypaidno);
            if (oModPayPaid.GetSetpaypaidno.Trim().Length > 0)
            {
                oModPayPaid.GetSetstatus = status;
                if (status.Equals("CONFIRMED"))
                {
                    oModPayPaid.GetSetconfirmedby = userid;
                }
                if (status.Equals("CANCELLED"))
                {
                    oModPayPaid.GetSetcancelledby = userid;
                }
                if (oMainCon.updatePaymentPaidHeader(oModPayPaid).Equals("Y"))
                {
                    if (status.Equals("CONFIRMED"))
                    {
                        ArrayList lsPayPaidLineItem = oMainCon.getPaymentPaidDetailsList(oModPayPaid.GetSetcomp, oModPayPaid.GetSetpaypaidno, 0, "");
                        for (int i = 0; i < lsPayPaidLineItem.Count; i++)
                        {
                            MainModel modPayPaidDet = (MainModel)lsPayPaidLineItem[i];

                            //to update Expenses & paid Amount
                            MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(modPayPaidDet.GetSetcomp, modPayPaidDet.GetSetexpensesno);
                            oModExpenses.GetSetpaypaidamount = oModExpenses.GetSetpaypaidamount + modPayPaidDet.GetSetpaypaidprice;
                            String result = oMainCon.updateExpensesHeader(oModExpenses);
                        }

                    }

                    sStatus = "Y";
                    sMessage = "";
                }
                else
                {
                    sStatus = "N";
                    sMessage = "Internal Server Error, Please contact System Admin!";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //for inventory - matzul 11/06/2019

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_COGSList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsCOGSDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsCOGSDetails = oMainCon.getItemStockTransactionsList(comp, "", "", "", selyear, selmonth, selday, "OUT");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsCOGSDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockInList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsCOGSDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsCOGSDetails = oMainCon.getItemStockTransactionsList(comp, "", "", "", selyear, selmonth, selday, "IN");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsCOGSDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockOutList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsCOGSDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsCOGSDetails = oMainCon.getItemStockTransactionsList(comp, "", "", "", selyear, selmonth, selday, "OUT");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsCOGSDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockList(String comp, String searchitem)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsStockListing = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsStockListing = oMainCon.getItemStockListSummary3(comp, searchitem);
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(lsStockListing);
        jsonResponse = convertJson(lsStockListing);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockDetails(String comp, String itemno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modStockDetails = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            modStockDetails = oMainCon.getItemStockSummary2(comp, itemno);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modStockDetails);
        jsonResponse = convertJson(modStockDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockDetailsList(String comp, String itemno, bool existonly)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsStockListing = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsStockListing = oMainCon.getItemStockList(comp, itemno, "", "", existonly);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsStockListing);
        jsonResponse = convertJson(lsStockListing);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockTransactionList(String comp, String itemno, String itemlocation, String datesoh)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsStockTransListing = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsStockTransListing = oMainCon.getItemStockTransactionsList(comp, itemno, itemlocation, datesoh);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(lsStockTransListing);
        jsonResponse = convertJson(lsStockTransListing);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string updateMobile_ReportFYRYearMonth(String comp, String financeyear, String financemonth, String actualyear, String actualmonth)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            MainModel oModFYR = oMainCon.getReportFYRYearMonth(comp, "", "", actualyear, actualmonth);

            //update report fyr data for selected year & month
            //1. For Stock Value IN
            Double dStockTransIN = oMainCon.getReportStockTrans(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "IN");
            int resultStockIn = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "STOCK_IN", "dashboard_stockin", oModFYR.GetSetfinancemonth, dStockTransIN);

            //1. For Stock Value IN QTY
            int iStockTransIN = oMainCon.getReportStockTransQty(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "IN");
            resultStockIn = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "STOCK_IN_QTY", "dashboard_stockin", oModFYR.GetSetfinancemonth, iStockTransIN);

            //2. For Stock Value OUT
            Double dStockTransOUT = oMainCon.getReportStockTrans(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "OUT");
            int resultStockOut = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "STOCK_OUT", "dashboard_stockout", oModFYR.GetSetfinancemonth, dStockTransOUT);

            //2. For Stock Value OUT QTY
            int iStockTransOUT = oMainCon.getReportStockTransQty(comp, oModFYR.GetSetactualyear, oModFYR.GetSetactualmonth, "OUT");
            resultStockOut = oMainCon.updateReportFYRDetails(comp, oModFYR.GetSetfinanceyear, "STOCK_OUT_QTY", "dashboard_stockout", oModFYR.GetSetfinancemonth, iStockTransOUT);

            if (resultStockIn == 1 && resultStockOut == 1)
            {
                sStatus = "Y";
                sMessage = "";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ReportStockTrans(String comp, String fyr, String type, String tablename)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel oModStockTrans = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            oModStockTrans = oMainCon.getReportFYRDetails(comp, fyr, type, tablename);
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockTrans);
        jsonResponse = convertJson(oModStockTrans);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockSummary(String comp, String itemno, String itemlocation, String datesoh)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel oModStockPosition = new MainModel();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            oModStockPosition = oMainCon.getItemStockSummary(comp, "", "", "");

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(oModStockPosition);
        jsonResponse = convertJson(oModStockPosition);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockReportSummary(String comp, String itemno, String selectyear, String selectmonth, String selectday)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";
        double totalopeningstockamount = 0;
        double totaladditionstockamount = 0;
        double totalsubtractionstockamount = 0;
        double totalclosingstockamount = 0;

        int totalopeningstockqty = 0;
        int totaladditionstockqty = 0;
        int totalsubtractionstockqty = 0;
        int totalclosingstockqty = 0;

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
            {
                sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
                DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
                /*
                DateTime firstOfNextMonth = new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
                */
                int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
                sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
            }
            else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
            {
                sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
                sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
            }

            MainModel modLastStockState = oMainCon.getStockStateLastHeaderDetails(comp, sDateFrom, "CLOSED");

            var lsStockLastSOHList = oMainCon.getStockStateLastSOHList(comp, "", (modLastStockState.GetSetclosingdate.Trim().Length > 0 ? modLastStockState.GetSetclosingdate : sDateFrom), "", "", "", "CLOSED");

            var lsStockInitialSOHList = oMainCon.getItemStockTransactionsListing(comp, "", "", "", modLastStockState.GetSetclosingdate, sDateFrom, "");

            List<MainModel> lsOpeningStockTransListing = new List<MainModel>();

            foreach (var item in lsStockLastSOHList)
            {
                MainModel modItem = new MainModel();
                modItem.GetSetcomp = item.GetSetcomp;
                modItem.GetSetstockstateno = item.GetSetstockstateno;
                modItem.GetSetopeningdate = item.GetSetopeningdate;
                modItem.GetSetopeningtype = item.GetSetopeningtype;
                modItem.GetSetstockopeningamount = item.GetSetstockopeningamount;
                modItem.GetSetstockinamount = item.GetSetstockinamount;
                modItem.GetSetstockoutamount = item.GetSetstockoutamount;
                modItem.GetSetstockclosingamount = item.GetSetstockclosingamount;
                modItem.GetSetclosingdate = item.GetSetclosingdate;
                modItem.GetSetclosingtype = item.GetSetclosingtype;
                modItem.GetSetremarks = item.GetSetremarks;
                modItem.GetSetstatus = item.GetSetstatus;
                modItem.GetSetitemno = item.GetSetitemno;
                modItem.GetSetitemdesc = item.GetSetitemdesc;
                modItem.GetSetlocation = item.GetSetlocation;
                modItem.GetSetdatesoh = item.GetSetdatesoh;
                modItem.GetSetqtysoh = item.GetSetqtysoh;
                modItem.GetSetcostsoh = item.GetSetcostsoh;
                foreach (var itemtrans in lsStockInitialSOHList)
                {
                    if (itemtrans.GetSetcomp.Equals(item.GetSetcomp) &&
                       itemtrans.GetSetitemno.Equals(item.GetSetitemno) &&
                       itemtrans.GetSetlocation.Equals(item.GetSetlocation) &&
                       itemtrans.GetSetdatesoh.Equals(item.GetSetdatesoh))
                    {
                        modItem.GetSetqtysoh = itemtrans.GetSetqtysoh;
                        modItem.GetSetcostsoh = itemtrans.GetSetcostsoh;
                    }

                }

                lsOpeningStockTransListing.Add(modItem);

            }

            var lsStockTransSOHList = oMainCon.getItemStockTransactionsListing(comp, "", "", "", sDateFrom, sDateTo, "");

            List<MainModel> lsClosingStockTransListing = new List<MainModel>();
            List<MainModel> lsAdditionStockTransListing = new List<MainModel>();
            List<MainModel> lsSubtractionStockTransListing = new List<MainModel>();

            foreach (var item in lsOpeningStockTransListing)
            {
                MainModel modItem = new MainModel();
                modItem.GetSetcomp = item.GetSetcomp;
                modItem.GetSetstockstateno = item.GetSetstockstateno;
                modItem.GetSetopeningdate = item.GetSetopeningdate;
                modItem.GetSetopeningtype = item.GetSetopeningtype;
                modItem.GetSetstockopeningamount = item.GetSetstockopeningamount;
                modItem.GetSetstockinamount = item.GetSetstockinamount;
                modItem.GetSetstockoutamount = item.GetSetstockoutamount;
                modItem.GetSetstockclosingamount = item.GetSetstockclosingamount;
                modItem.GetSetclosingdate = item.GetSetclosingdate;
                modItem.GetSetclosingtype = item.GetSetclosingtype;
                modItem.GetSetremarks = item.GetSetremarks;
                modItem.GetSetstatus = item.GetSetstatus;
                modItem.GetSetitemno = item.GetSetitemno;
                modItem.GetSetitemdesc = item.GetSetitemdesc;
                modItem.GetSetlocation = item.GetSetlocation;
                modItem.GetSetdatesoh = item.GetSetdatesoh;
                modItem.GetSetqtysoh = 0;
                modItem.GetSetcostsoh = 0;

                MainModel modItemSubtraction = new MainModel();
                modItemSubtraction.GetSetcomp = item.GetSetcomp;
                modItemSubtraction.GetSetstockstateno = item.GetSetstockstateno;
                modItemSubtraction.GetSetopeningdate = item.GetSetopeningdate;
                modItemSubtraction.GetSetopeningtype = item.GetSetopeningtype;
                modItemSubtraction.GetSetstockopeningamount = item.GetSetstockopeningamount;
                modItemSubtraction.GetSetstockinamount = item.GetSetstockinamount;
                modItemSubtraction.GetSetstockoutamount = item.GetSetstockoutamount;
                modItemSubtraction.GetSetstockclosingamount = item.GetSetstockclosingamount;
                modItemSubtraction.GetSetclosingdate = item.GetSetclosingdate;
                modItemSubtraction.GetSetclosingtype = item.GetSetclosingtype;
                modItemSubtraction.GetSetremarks = item.GetSetremarks;
                modItemSubtraction.GetSetstatus = item.GetSetstatus;
                modItemSubtraction.GetSetitemno = item.GetSetitemno;
                modItemSubtraction.GetSetitemdesc = item.GetSetitemdesc;
                modItemSubtraction.GetSetlocation = item.GetSetlocation;
                modItemSubtraction.GetSetdatesoh = item.GetSetdatesoh;
                modItemSubtraction.GetSetqtysoh = 0;
                modItemSubtraction.GetSetcostsoh = 0;

                MainModel modItemClose = new MainModel();
                modItemClose.GetSetcomp = item.GetSetcomp;
                modItemClose.GetSetstockstateno = item.GetSetstockstateno;
                modItemClose.GetSetopeningdate = item.GetSetopeningdate;
                modItemClose.GetSetopeningtype = item.GetSetopeningtype;
                modItemClose.GetSetstockopeningamount = item.GetSetstockopeningamount;
                modItemClose.GetSetstockinamount = item.GetSetstockinamount;
                modItemClose.GetSetstockoutamount = item.GetSetstockoutamount;
                modItemClose.GetSetstockclosingamount = item.GetSetstockclosingamount;
                modItemClose.GetSetclosingdate = item.GetSetclosingdate;
                modItemClose.GetSetclosingtype = item.GetSetclosingtype;
                modItemClose.GetSetremarks = item.GetSetremarks;
                modItemClose.GetSetstatus = item.GetSetstatus;
                modItemClose.GetSetitemno = item.GetSetitemno;
                modItemClose.GetSetitemdesc = item.GetSetitemdesc;
                modItemClose.GetSetlocation = item.GetSetlocation;
                modItemClose.GetSetdatesoh = item.GetSetdatesoh;
                modItemClose.GetSetqtysoh = item.GetSetqtysoh;
                modItemClose.GetSetcostsoh = item.GetSetcostsoh;

                foreach (var itemtrans in lsStockTransSOHList)
                {
                    if (itemtrans.GetSetcomp.Equals(item.GetSetcomp) &&
                       itemtrans.GetSetitemno.Equals(item.GetSetitemno) &&
                       itemtrans.GetSetlocation.Equals(item.GetSetlocation) &&
                       itemtrans.GetSetdatesoh.Equals(item.GetSetdatesoh))
                    {
                        if (itemtrans.GetSettransqty > 0)
                        {
                            modItem.GetSetqtysoh = modItem.GetSetqtysoh + itemtrans.GetSettransqty;
                            //modItem.GetSetcostsoh = modItem.GetSetcostsoh + ((itemtrans.GetSetcostsoh / itemtrans.GetSetqtysoh) * itemtrans.GetSettransqty);
                            modItem.GetSetcostsoh = modItem.GetSetcostsoh + (itemtrans.GetSettransprice * itemtrans.GetSettransqty);
                        }
                        else
                        {
                            modItemSubtraction.GetSetqtysoh = modItemSubtraction.GetSetqtysoh + itemtrans.GetSettransqty;
                            //modItemSubtraction.GetSetcostsoh = modItemSubtraction.GetSetcostsoh + ((itemtrans.GetSetcostsoh / itemtrans.GetSetqtysoh) * itemtrans.GetSettransqty);
                            modItemSubtraction.GetSetcostsoh = modItemSubtraction.GetSetcostsoh + (itemtrans.GetSettransprice * itemtrans.GetSettransqty);
                        }
                        modItemClose.GetSetqtysoh = itemtrans.GetSetqtysoh;
                        modItemClose.GetSetcostsoh = itemtrans.GetSetcostsoh;

                    }

                }
                lsAdditionStockTransListing.Add(modItem);
                lsSubtractionStockTransListing.Add(modItemSubtraction);
                lsClosingStockTransListing.Add(modItemClose);

            }

            ArrayList lsStockOpeningSOH = new ArrayList(lsOpeningStockTransListing);
            ArrayList lsStockAdditionSOH = new ArrayList(lsAdditionStockTransListing);
            ArrayList lsStockSubstractionSOH = new ArrayList(lsSubtractionStockTransListing);
            ArrayList lsStockClosingSOH = new ArrayList(lsClosingStockTransListing);

            foreach (var item in lsOpeningStockTransListing)
            {
                //oMainCon.WriteToLogFile("Opening Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalopeningstockqty = totalopeningstockqty + item.GetSetqtysoh;
                totalopeningstockamount = totalopeningstockamount + item.GetSetcostsoh;
            }

            foreach (var item in lsAdditionStockTransListing)
            {
                //oMainCon.WriteToLogFile("Addition Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totaladditionstockqty = totaladditionstockqty + item.GetSetqtysoh;
                totaladditionstockamount = totaladditionstockamount + item.GetSetcostsoh;
            }

            foreach (var item in lsSubtractionStockTransListing)
            {
                //oMainCon.WriteToLogFile("Subtraction Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalsubtractionstockqty = totalsubtractionstockqty + item.GetSetqtysoh;
                totalsubtractionstockamount = totalsubtractionstockamount + item.GetSetcostsoh;
            }

            foreach (var item in lsClosingStockTransListing)
            {
                //oMainCon.WriteToLogFile("Closing Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                totalclosingstockqty = totalclosingstockqty + item.GetSetqtysoh;
                totalclosingstockamount = totalclosingstockamount + item.GetSetcostsoh;
            }

            object objStockReportSummary = new
            {
                datefrom = sDateFrom,
                dateto = sDateTo,
                totalopeningstockqty = totalopeningstockqty,
                totaladditionstockqty = totaladditionstockqty,
                totalsubtractionstockqty = totalsubtractionstockqty,
                totalclosingstockqty = totalclosingstockqty,
                totalopeningstockamount = totalopeningstockamount,
                totaladditionstockamount = totaladditionstockamount,
                totalsubtractionstockamount = totalsubtractionstockamount,
                totalclosingstockamount = totalclosingstockamount,
                arrayopeningstock = lsStockOpeningSOH,
                arrayadditionstock = lsStockAdditionSOH,
                arraysubtractionstock = lsStockSubstractionSOH,
                arrayclosingstock = lsStockClosingSOH
            };
            //jsonResponse = new JavaScriptSerializer().Serialize(oModStockPosition);
            jsonResponse = convertJson(objStockReportSummary);

        }
        else
        {
            object objStockReportSummary = new
            {
                datefrom = sDateFrom,
                dateto = sDateTo,
                totalopeningstockqty = totalopeningstockqty,
                totaladditionstockqty = totaladditionstockqty,
                totalsubtractionstockqty = totalsubtractionstockqty,
                totalclosingstockqty = totalclosingstockqty,
                totalopeningstockamount = totalopeningstockamount,
                totaladditionstockamount = totaladditionstockamount,
                totalsubtractionstockamount = totalsubtractionstockamount,
                totalclosingstockamount = totalclosingstockamount,
                arrayopeningstock = new ArrayList(),
                arrayadditionstock = new ArrayList(),
                arraysubtractionstock = new ArrayList(),
                arrayclosingstock = new ArrayList()
            };
            //jsonResponse = new JavaScriptSerializer().Serialize(oModStockPosition);
            jsonResponse = convertJson(objStockReportSummary);
        }

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_StockStateHeaderList(String comp, String stockstateno, String openingdate, String openingtype, String closingdate, String closingtype, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsClosingStockValue = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            ArrayList lsStockState = oMainCon.getStockStateHeaderList(comp, "", "", "", "", "", "");
            for (int i = 0; i < lsStockState.Count; i++)
            {
                MainModel modStockState = (MainModel)lsStockState[i];

                if (modStockState.GetSetstatus.Equals("IN-PROGRESS"))
                {
                    String sClosingDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                    ArrayList lsStockInDetails = oMainCon.getItemStockTransactionsList(modStockState.GetSetcomp, "", "", "", modStockState.GetSetopeningdate, sClosingDate, "IN");
                    if (lsStockInDetails.Count > 0)
                    {
                        for (int x = 0; x < lsStockInDetails.Count; x++)
                        {
                            MainModel oStockInDet = (MainModel)lsStockInDetails[x];
                            modStockState.GetSetstockinamount = modStockState.GetSetstockinamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    ArrayList lsStockOutDetails = oMainCon.getItemStockTransactionsList(modStockState.GetSetcomp, "", "", "", modStockState.GetSetopeningdate, sClosingDate, "OUT");
                    if (lsStockOutDetails.Count > 0)
                    {
                        for (int x = 0; x < lsStockOutDetails.Count; x++)
                        {
                            MainModel oStockOutDet = (MainModel)lsStockOutDetails[x];
                            modStockState.GetSetstockoutamount = modStockState.GetSetstockoutamount + Math.Round(oStockOutDet.GetSettransqty * oStockOutDet.GetSettransprice, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    modStockState.GetSetstockclosingamount = Math.Round(modStockState.GetSetstockopeningamount + modStockState.GetSetstockinamount - modStockState.GetSetstockoutamount);

                }

                lsClosingStockValue.Add(modStockState);
            }
        }

        //jsonResponse = new JavaScriptSerializer().Serialize(lsClosingStockValue);
        jsonResponse = convertJson(lsClosingStockValue);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    //For Buku Tunai

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_PaymentReportSummary2(String comp, String selectyear, String selectmonth, String selectday)
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";        

        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        double totalcashopeningamount = 0;
        double totalcashpayrcptamount = 0;
        double totalcashpaypaidamount = 0;
        double totalcashclosingamount = 0;


        if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
        {
            sDateFrom = selectday + "-" + selectmonth + "-" + selectyear + " 00:00:00";
            sDateTo = selectday + "-" + selectmonth + "-" + selectyear + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
        {
            sDateFrom = "01" + "-" + selectmonth + "-" + selectyear + " 00:00:00";
            DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
            int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
            sDateTo = maxdt + "-" + selectmonth + "-" + selectyear + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
        {
            sDateFrom = "01" + "-" + "01" + "-" + selectyear + " 00:00:00";
            sDateTo = "31" + "-" + "12" + "-" + selectyear + " 23:59:59";
        }

        MainModel modLastCashFlow = oMainCon.getCashFlowLastHeaderDetails(comp, sDateFrom, "CLOSED");

        MainModel modCashFlow = new MainModel();
        modCashFlow.GetSetcomp = comp;
        modCashFlow.GetSetopeningdate = sDateFrom;
        modCashFlow.GetSetbankopeningamount = modLastCashFlow.GetSetbankclosingamount;
        modCashFlow.GetSetcashopeningamount = modLastCashFlow.GetSetcashclosingamount;

        ArrayList lsPayRcptHeaderDetails = oMainCon.getPaymentReceiptCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
        for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++)
        {
            MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];

            modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
            modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
        }

        ArrayList lsPayPaidHeaderDetails = oMainCon.getPaymentPaidCashFlowList(comp, modLastCashFlow.GetSetclosingdate, sDateFrom, "CONFIRMED");
        for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++)
        {
            MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];

            modCashFlow.GetSetbankopeningamount = modCashFlow.GetSetbankopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
            modCashFlow.GetSetcashopeningamount = modCashFlow.GetSetcashopeningamount - (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
        }

        ArrayList lsPaymentReceipt = oMainCon.getPaymentReceiptCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
        for (int i = 0; i < lsPaymentReceipt.Count; i++)
        {
            MainModel oPayRcptDet = (MainModel)lsPaymentReceipt[i];

            modCashFlow.GetSetbankpaymentreceiptamount = modCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
            modCashFlow.GetSetcashpaymentreceiptamount = modCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
        }

        ArrayList lsPaymentPaid = oMainCon.getPaymentPaidCashFlowList(comp, sDateFrom, sDateTo, "CONFIRMED");
        for (int i = 0; i < lsPaymentPaid.Count; i++)
        {
            MainModel oPayPaidDet = (MainModel)lsPaymentPaid[i];

            modCashFlow.GetSetbankpaymentpaidamount = modCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
            modCashFlow.GetSetcashpaymentpaidamount = modCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
        }

        modCashFlow.GetSetbankclosingamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetbankpaymentreceiptamount - modCashFlow.GetSetbankpaymentpaidamount;
        modCashFlow.GetSetcashclosingamount = modCashFlow.GetSetcashopeningamount + modCashFlow.GetSetcashpaymentreceiptamount - modCashFlow.GetSetcashpaymentpaidamount;
        modCashFlow.GetSetclosingdate = sDateTo;

        totalcashopeningamount = modCashFlow.GetSetbankopeningamount + modCashFlow.GetSetcashopeningamount;
        totalcashpayrcptamount = modCashFlow.GetSetbankpaymentreceiptamount + modCashFlow.GetSetcashpaymentreceiptamount;
        totalcashpaypaidamount = modCashFlow.GetSetbankpaymentpaidamount + modCashFlow.GetSetcashpaymentpaidamount;
        totalcashclosingamount = modCashFlow.GetSetbankclosingamount + modCashFlow.GetSetcashclosingamount;

        object objStockReportSummary = new { datefrom = sDateFrom, dateto = sDateTo, totalcashopeningamount = totalcashopeningamount, totalcashpayrcptamount = totalcashpayrcptamount, totalcashpaypaidamount = totalcashpaypaidamount * -1, totalcashclosingamount = totalcashclosingamount };
        //jsonResponse = convertJson(objStockReportSummary);

        jsonResponse = convertJson(objStockReportSummary);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ListCashInOut2(String comp, String selectyear, String selectmonth, String selectday)
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sDateFrom = "", sDateTo = "";

        ArrayList lsInvoiceHeader = new ArrayList();

        if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length > 0)
        {
            sDateFrom = selectyear + "-" + selectmonth + "-" + selectday + " 00:00:00";
            sDateTo = selectyear + "-" + selectmonth + "-" + selectday + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length > 0 && selectday.Length == 0)
        {
            sDateFrom = selectyear + "-" + selectmonth + "-" + "01" + " 00:00:00";

            DateTime datetimeFrom = Convert.ToDateTime(sDateFrom, oMainCon.ukDtfi);
            int maxdt = (new DateTime(datetimeFrom.Year, datetimeFrom.Month, 1).AddMonths(1).AddDays(-1)).Day;
            sDateTo = selectyear + "-" + selectmonth + "-" + maxdt + " 23:59:59";
        }
        else if (selectyear.Length > 0 && selectmonth.Length == 0 && selectday.Length == 0)
        {
            sDateFrom = selectyear + "-" + "01" + "-" + "01" + " 00:00:00";
            sDateTo = selectyear + "-" + "12" + "-" + "31" + " 23:59:59";
        }

        lsInvoiceHeader = oMainCon.getCashInOut2(comp, sDateFrom, sDateTo);

        //jsonResponse = new JavaScriptSerializer().Serialize(lsInvoiceHeader);
        jsonResponse = convertJson(lsInvoiceHeader);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_parametertype(String paramtcategory)
    {
        HttpContext.Current.Response.ContentType = "text/json";
        //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        ArrayList lsPendInvoiceList = new ArrayList();


        lsPendInvoiceList = oMainCon.getParametertype(paramtcategory, "ACTIVE");

        //jsonResponse = new JavaScriptSerializer().Serialize(lsPendInvoiceList);
        jsonResponse = convertJson(lsPendInvoiceList);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_ExpensesDetails2(String comp, String paypaidno, String receiptno)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modExpensesHeaderDetails = new MainModel();


        modExpensesHeaderDetails = oMainCon.getExpensesHeaderDetails2(comp, paypaidno, receiptno);


        //jsonResponse = new JavaScriptSerializer().Serialize(modExpensesHeaderDetails);
        jsonResponse = convertJson(modExpensesHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_IncomeDetails(String comp, String paypaidno, String receiptno)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        MainModel modInvoiceHeaderDetails = new MainModel();

        modInvoiceHeaderDetails = oMainCon.getIncomeDetails(comp, paypaidno, receiptno);

        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(modInvoiceHeaderDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_InvoiceInvoice2(String comp, String bpid, double inc_amount, String invoicecat, String inc_type, String  inc_remarks,String datecreation, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get comp bp if exist
        MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
        if (oModBP.GetSetbpid.Trim().Length > 0)
        {
            MainModel oModInvoice = new MainModel();
            oModInvoice.GetSetcomp = comp;
            oModInvoice.GetSetinvoicedate = datecreation;
            oModInvoice.GetSetinvoicecat = invoicecat;
            oModInvoice.GetSetinvoicetype = inc_type;
            oModInvoice.GetSetinvoiceterm = "NOT_APPLICABLE";
            String sInvoiceNo = oMainCon.getNextRunningNo(comp, "INVOICE", "ACTIVE");
            oModInvoice.GetSetinvoiceno = sInvoiceNo;
            oModInvoice.GetSetbpid = oModBP.GetSetbpid;
            oModInvoice.GetSetbpdesc = oModBP.GetSetbpdesc;
            oModInvoice.GetSetbpaddress = oModBP.GetSetbpaddress;
            oModInvoice.GetSetbpcontact = oModBP.GetSetbpcontact;
            oModInvoice.GetSetsalesamount = inc_amount;
            oModInvoice.GetSetinvoiceamount = inc_amount;
            oModInvoice.GetSettotalamount = inc_amount;
            oModInvoice.GetSetpayrcptamount = inc_amount;
            oModInvoice.GetSetremarks = inc_remarks;
            oModInvoice.GetSetstatus = "CONFIRMED";
            oModInvoice.GetSetcreatedby = userid;
            oModInvoice.GetSetcreateddate = datecreation;
            oModInvoice.GetSetconfirmedby = userid;
            oModInvoice.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertInvoiceHeader2(oModInvoice).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(oModInvoice.GetSetcomp, "INVOICE", "ACTIVE");
                sStatus = "Y";
                sMessage = oModInvoice.GetSetinvoiceno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_InvoiceItemDetails2(String comp, String invoiceno, String inc_item, String inc_itemdesc, double inc_amount)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        MainModel oModLineItem = new MainModel();
        oModLineItem.GetSetcomp = comp;
        oModLineItem.GetSetinvoiceno = invoiceno;
        oModLineItem.GetSetlineno = oMainCon.getInvoiceDetailsList(comp, invoiceno, 0, "").Count + 1;
        oModLineItem.GetSetitemno = inc_item;
        oModLineItem.GetSetitemdesc = inc_itemdesc;
        oModLineItem.GetSetunitprice = inc_amount;
        oModLineItem.GetSetquantity = 1;
        oModLineItem.GetSetinvoiceprice = inc_amount;
        oModLineItem.GetSettaxcode = "N/A";
        oModLineItem.GetSettotalinvoice = inc_amount;

        MainModel modExistent = oMainCon.getInvoiceDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetshipmentno, oModLineItem.GetSetshipment_lineno, inc_item, "NEW");
        if (modExistent.GetSetinvoiceno.Length > 0)
        {
            sStatus = "N";
            sMessage = "Invoice item already assigned, Please contact System Admin!";
        }
        else
        {
            //insert new line item
            if (oMainCon.insertInvoiceDetails(oModLineItem).Equals("Y"))
            {
                //update invoice header information
                sStatus = oMainCon.updateInvoiceHeaderInfo(comp, invoiceno);
                sMessage = "";
            }
            else
            {
                sStatus = "N";
                sMessage = "Unable to add invoice item, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_PayRcptInvoice2(String comp, String invoiceno, String datecreation, double inc_amount, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get invoice header if exist
        MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
        if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetpayrcptdate = datecreation;
            oModHeader.GetSetpayrcpttype = "INVOICE";
            String sPayRcptNo = oMainCon.getNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
            oModHeader.GetSetpayrcptno = sPayRcptNo;
            oModHeader.GetSetbpid = oModInvoice.GetSetbpid;
            oModHeader.GetSetbpdesc = oModInvoice.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModInvoice.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModInvoice.GetSetbpcontact;
            oModHeader.GetSetinvoiceamount = inc_amount;
            oModHeader.GetSetpayrcptamount = inc_amount;
            oModHeader.GetSetremarks = oModInvoice.GetSetremarks;
            oModHeader.GetSetstatus = "CONFIRMED";
            oModHeader.GetSetcreatedby = userid;
            oModHeader.GetSetcreateddate = datecreation;
            oModHeader.GetSetconfirmedby = userid;
            oModHeader.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertPaymentReceiptHeader2(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "PAYMENT_RECEIPT", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetpayrcptno;
            }
        }


        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_PayRcptItemInvoice2(String comp, String payrcptno, String invoiceno, String datecreation, double inc_amount, String inc_paytype)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get invoice header if exist
        MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(comp, invoiceno);
        if (oModInvoice.GetSetinvoiceno.Trim().Length > 0 && oModInvoice.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetpayrcptno = payrcptno;
            oModLineItem.GetSetlineno = 1;
            oModLineItem.GetSetinvoiceno = invoiceno;
            oModLineItem.GetSetinvoicedate = datecreation;
            oModLineItem.GetSetinvoiceprice = inc_amount;
            oModLineItem.GetSetpaytype = inc_paytype;
            oModLineItem.GetSetpayrcptprice = inc_amount;

            //check whether already exist in Other Line Item that is not confirm yet or not
            MainModel modExistent = oMainCon.getPaymentReceiptDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetinvoiceno, "NEW");
            if (modExistent.GetSetpayrcptno.Length > 0)
            {
                sStatus = "Y";
                sMessage = "Item tersebut telah ditambah pada Bayaran Terima: " + modExistent.GetSetpayrcptno;
            }
            else
            {
                //insert new line item
                if (oMainCon.insertPaymentReceiptDetails(oModLineItem).Equals("Y"))
                {
                    //update payment receipt header information
                    sStatus = oMainCon.updatePaymentReceiptHeaderInfo(comp, payrcptno);
                    sMessage = "";
                }
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_ExpensesHeader2(String comp, String bpid, String expensescat, String expensestype, Double exp_amount, String exp_remarks, String datecreation, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";

        //get comp bp if exist
        MainModel oModBP = oMainCon.getBPDetails(comp, bpid);
        if (oModBP.GetSetbpid.Trim().Length > 0)
        {
            MainModel oModExpenses = new MainModel();
            oModExpenses.GetSetcomp = comp;
            oModExpenses.GetSetexpensesdate = datecreation;
            oModExpenses.GetSetexpensescat = expensescat;
            oModExpenses.GetSetexpensestype = expensestype;
            String sExpensesNo = oMainCon.getNextRunningNo(comp, "EXPENSES", "ACTIVE");
            oModExpenses.GetSetexpensesno = sExpensesNo;
            oModExpenses.GetSetbpid = oModBP.GetSetbpid;
            oModExpenses.GetSetbpdesc = oModBP.GetSetbpdesc;
            oModExpenses.GetSetbpaddress = oModBP.GetSetbpaddress;
            oModExpenses.GetSetbpcontact = oModBP.GetSetbpcontact;
            oModExpenses.GetSetremarks = exp_remarks;
            oModExpenses.GetSetstatus = "CONFIRMED";
            oModExpenses.GetSetexpensesamount = exp_amount;
            oModExpenses.GetSettotalamount = exp_amount;
            oModExpenses.GetSetpaypaidamount = exp_amount;
            oModExpenses.GetSetpurchaseamount = exp_amount;
            oModExpenses.GetSetcreatedby = userid;
            oModExpenses.GetSetcreateddate = datecreation;
            oModExpenses.GetSetconfirmedby = userid;
            oModExpenses.GetSetconfirmeddate = datecreation;

            //oMainCon.WriteToLogFile("WebService-createMobile_ExpensesHeader2: Remarks [" + oModExpenses.GetSetremarks + "]");

            if (oMainCon.insertExpensesHeader2(oModExpenses).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(oModExpenses.GetSetcomp, "EXPENSES", "ACTIVE");
                sStatus = "Y";
                sMessage = oModExpenses.GetSetexpensesno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_ExpensesReceiptItemDetails2(String comp, String expensesno, String expensescat, String exp_item, String exp_itemdesc, Double exp_amount, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        MainModel oModLineItem = new MainModel();
        oModLineItem.GetSetcomp = comp;
        oModLineItem.GetSetexpensesno = expensesno;
        oModLineItem.GetSetlineno = oMainCon.getExpensesDetailsList(comp, expensesno, 0, "").Count + 1;
        oModLineItem.GetSetitemno = exp_item;
        oModLineItem.GetSetitemdesc = exp_itemdesc;
        oModLineItem.GetSetunitprice = exp_amount;
        oModLineItem.GetSetdiscamount = 0;
        oModLineItem.GetSetquantity = 1;
        oModLineItem.GetSettaxcode = "N/A";
        oModLineItem.GetSetexpensesprice = exp_amount;
        oModLineItem.GetSettotalexpenses = exp_amount;

        MainModel modExistent = oMainCon.getExpensesDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetreceiptno, oModLineItem.GetSetreceipt_lineno, exp_item, "NEW");
        if (modExistent.GetSetinvoiceno.Length > 0)
        {
            sStatus = "N";
            sMessage = "Expenses item already assigned, Please contact System Admin!";
        }
        else
        {
            //insert new line item
            if (oMainCon.insertExpensesDetails(oModLineItem).Equals("Y"))
            {
                //update expenses header information
                sStatus = oMainCon.updateExpensesHeaderInfo(comp, expensesno);
                sMessage = "";
            }
            else
            {
                sStatus = "N";
                sMessage = "Unable to add expenses item, Please contact System Admin!";
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string createMobile_PayPaidExpenses2(String comp, String expensesno, Double exp_amount, String datecreation, String userid)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        //get expenses header if exist
        MainModel oModExpenses = oMainCon.getExpensesHeaderDetails(comp, expensesno);
        if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModHeader = new MainModel();
            oModHeader.GetSetcomp = comp;
            oModHeader.GetSetpaypaiddate = datecreation;
            oModHeader.GetSetpaypaidtype = "EXPENSES";
            String sPayPaidNo = oMainCon.getNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
            oModHeader.GetSetpaypaidno = sPayPaidNo;
            oModHeader.GetSetbpid = oModExpenses.GetSetbpid;
            oModHeader.GetSetbpdesc = oModExpenses.GetSetbpdesc;
            oModHeader.GetSetbpaddress = oModExpenses.GetSetbpaddress;
            oModHeader.GetSetbpcontact = oModExpenses.GetSetbpcontact;
            oModHeader.GetSetremarks = oModExpenses.GetSetremarks;
            oModHeader.GetSetstatus = "CONFIRMED";
            oModHeader.GetSetexpensesamount = exp_amount;
            oModHeader.GetSetpaypaidamount = exp_amount;
            oModHeader.GetSetcreatedby = userid;
            oModHeader.GetSetcreateddate = datecreation;
            oModHeader.GetSetconfirmedby = userid;
            oModHeader.GetSetconfirmeddate = datecreation;

            if (oMainCon.insertPaymentPaidHeader2(oModHeader).Equals("Y"))
            {
                oMainCon.updateNextRunningNo(comp, "PAYMENT_PAID", "ACTIVE");
                sStatus = "Y";
                sMessage = oModHeader.GetSetpaypaidno;
            }
        }

        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string addMobile_PayPaidItemExpenses2(String comp, String paypaidno, String expensesno, Double exp_amount, String exp_paytype, String datecreation)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";


        //get expenses header if exist
        MainModel oModExpenses = (MainModel)oMainCon.getExpensesHeaderDetails(comp, expensesno);
        if (oModExpenses.GetSetexpensesno.Trim().Length > 0 && oModExpenses.GetSetstatus.Equals("CONFIRMED"))
        {
            MainModel oModLineItem = new MainModel();
            oModLineItem.GetSetcomp = comp;
            oModLineItem.GetSetpaypaidno = paypaidno;
            oModLineItem.GetSetlineno = 1;
            oModLineItem.GetSetexpensesno = expensesno;
            oModLineItem.GetSetexpensesdate = datecreation;
            oModLineItem.GetSetexpensesprice = exp_amount;
            oModLineItem.GetSetpaytype = exp_paytype;
            oModLineItem.GetSetpayrefno = "";
            oModLineItem.GetSetpaypaidprice = exp_amount;
            oModLineItem.GetSetpayremarks = "";

            //check whether already exist in Other Line Item that is not confirm yet or not
            MainModel modExistent = oMainCon.getPaymentPaidDetailsDetails(oModLineItem.GetSetcomp, "", 0, oModLineItem.GetSetexpensesno, "NEW");
            if (modExistent.GetSetpaypaidno.Length > 0)
            {
                sStatus = "Y";
                sMessage = "Item tersebut telah ditambah pada Bayaran Belanja: " + modExistent.GetSetpayrcptno;
            }
            else
            {
                //insert new line item
                if (oMainCon.insertPaymentPaidDetails(oModLineItem).Equals("Y"))
                {
                    //update payment paid header information
                    sStatus = oMainCon.updatePaymentPaidHeaderInfo(comp, paypaidno);
                    sMessage = "";
                }
            }
        }


        object objData = new { status = sStatus, message = sMessage };
        //jsonResponse = new JavaScriptSerializer().Serialize(objData);
        jsonResponse = convertJson(objData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_CashInList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsCashDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsCashDetails = oMainCon.getPaymentReceiptCashInList(comp, selyear, selmonth, selday, status);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsCashDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getMobile_CashOutList(String comp, String selyear, String selmonth, String selday, String status)
    {

        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        ArrayList lsCashDetails = new ArrayList();

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            lsCashDetails = oMainCon.getPaymentPaidCashOutList(comp, selyear, selmonth, selday, status);

        }
        //jsonResponse = new JavaScriptSerializer().Serialize(modInvoiceHeaderDetails);
        jsonResponse = convertJson(lsCashDetails);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    private string convertJson(ArrayList lsItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(lsItem);

        return jsonResponse;
    }

    private string convertJson(MainModel modItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(modItem);

        return jsonResponse;
    }

    private string convertJson(object objItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(objItem);

        return jsonResponse;
    }

}
