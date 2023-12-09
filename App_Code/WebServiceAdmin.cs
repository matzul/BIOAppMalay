using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Configuration;

/// <summary>
/// Summary description for WebServiceAdmin
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebServiceAdmin : System.Web.Services.WebService
{

    private String TokenNumber = "M05kit0@1";
    private String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

    private MainController oMainCon = new MainController();
    private HRController oHRCon = new HRController();

    public WebServiceAdmin()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
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

    private string convertJson(HRModel modItem)
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Xml)]
    public String getUserListing(String comp, String userid, String usertype)
    {
        HttpContext.Current.Response.ContentType = "application/xml; charset=utf-8";
        String response = "";

        ArrayList lsUserProfile = new ArrayList();
        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsUserProfile = oMainCon.getUserProfileList(comp, userid, "", usertype);
        }
        response = new JavaScriptSerializer().Serialize(lsUserProfile);
        return response;
    }

    [WebMethod]
    [System.Xml.Serialization.XmlInclude(typeof(UserProfileModel))]
    public ArrayList getUserArrayListing(String comp, String userid, String usertype)
    {
        ArrayList lsUserProfile = new ArrayList();
        if (TokenNumber.Equals(TokenNumberConfig))
        {

            lsUserProfile = oMainCon.getUserProfileList(comp, userid, "", usertype);
        }
        return lsUserProfile;
    }

    [WebMethod]
    [System.Xml.Serialization.XmlInclude(typeof(ResponseMessage))]
    public ResponseMessage createUser(String comp, String userid, String userpwd, String username, String useradd, String usertelno, String usertype, String userstatus, String screenid, String roleid, String createdby)
    {
        ResponseMessage result = new ResponseMessage();
        result.GetSetstatus = "N";
        result.GetSetmessage = "internal server error";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            int i = oMainCon.createUser(comp, userid, userpwd, username, useradd, usertelno, usertype, userstatus, screenid, roleid, createdby);
            if (i == 1)
            {
                result.GetSetstatus = "Y";
                result.GetSetmessage = "user created successfully";
            }
            else
            {
                result.GetSetstatus = "N";
                result.GetSetmessage = "internal server error";
            }
        }
        return result;
    }

    [WebMethod]
    [System.Xml.Serialization.XmlInclude(typeof(ResponseMessage))]
    public ResponseMessage createCompany(String comp, String comp_name, String comp_id, String comp_accountbank, String comp_accountno, String comp_address, String comp_contact, String comp_contactno, String comp_website, String comp_email, String comp_icon, String comp_logo1, String comp_logo2, String status, String createdby, String confirmedby, String year)
    {
        ResponseMessage result = new ResponseMessage();
        result.GetSetstatus = "N";
        result.GetSetmessage = "internal server error";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            int i = oMainCon.createCompany(comp, comp_name, comp_id, comp_accountbank, comp_accountno, comp_address, comp_contact, comp_contactno, comp_website, comp_email, comp_icon, comp_logo1, comp_logo2, status, createdby, confirmedby, year);
            if (i == 1)
            {
                result.GetSetstatus = "Y";
                result.GetSetmessage = "company created successfully";
            }
            else
            {
                result.GetSetstatus = "N";
                result.GetSetmessage = "internal server error";
            }
        }

        return result;
    }

    [WebMethod]
    [System.Xml.Serialization.XmlInclude(typeof(ResponseMessage))]
    public ResponseMessage createfiscalyeardashboard(String comp, String year)
    {
        ResponseMessage result = new ResponseMessage();
        result.GetSetstatus = "N";
        result.GetSetmessage = "internal server error";

        if (TokenNumber.Equals(TokenNumberConfig))
        {
            int i = oMainCon.createfiscalyeardashboard(comp, year);
            if (i == 1)
            {
                result.GetSetstatus = "Y";
                result.GetSetmessage = "fiscalyear & dashboard created successfully";
            }
            else
            {
                result.GetSetstatus = "N";
                result.GetSetmessage = "internal server error";
            }
        }

        return result;
    }

    [DataContract]
    public class ResponseMessage
    {
        private string status = "";
        [DataMember]
        public string GetSetstatus
        {
            get
            {
                string text = status;
                if (text != null)
                    return text;
                else
                    return string.Empty;
            }
            set
            {
                status = value;
            }
        }

        private string message = "";
        [DataMember]
        public string GetSetmessage
        {
            get
            {
                string text = message;
                if (text != null)
                    return text;
                else
                    return string.Empty;
            }
            set
            {
                message = value;
            }
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string Authenticate_HRUSer(String userid, String password, String comp)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        HRModel oModUser = oHRCon.getStaffDetails(comp, "", userid, password);
        if (oModUser.GetSetuserid.Trim().Length > 0)
        {
            sStatus = "Y";
            sMessage = "Successfully Log Into System!";
        }
        else
        {
            oModUser.GetSetstaffno = "";
            sMessage = "Wrong password or user id!";
        }
        if (sStatus.Equals("Y"))
        {
            if (oModUser.GetSetstatus.Trim() == "ACTIVE")
            {
                sMessage = "Successfully Log Into System!";
            }
            else if (oModUser.GetSetstatus.Trim() == "CHANGED")
            {
                oModUser.GetSetstaffno = "";
                sMessage = "Your password needs to be updated. To update your password, please log in using the System BIOAppHR site.";
            }
            else if (oModUser.GetSetstatus.Trim() == "BLOCKED")
            {
                oModUser.GetSetstaffno = "";
                sMessage = "Your account has been blocked! Please contact the administrator.";
            }
            else if (oModUser.GetSetstatus.Trim() == "IN-ACTIVE")
            {
                oModUser.GetSetstaffno = "";
                sMessage = "Your account is not active! Please contact the administrator.";
            }
            else if (oModUser.GetSetstatus.Trim() == "DISABLED")
            {
                oModUser.GetSetstaffno = "";
                sMessage = "Your account has been disabled! Please contact the administrator.";
            }
        }

        object retData = new { result = sStatus, message = sMessage, itemdetail = oModUser };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getCompInfoDetails(String comp)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";

        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        MainModel oModComp = oMainCon.getCompInfoDetails(comp);
        if (oModComp.GetSetcomp.Trim().Length > 0)
        {
            sStatus = "Y";
            sMessage = "";
        }

        object retData = new { result = sStatus, message = sMessage, itemdetail = oModComp };

        jsonResponse = new JavaScriptSerializer().Serialize(retData);

        return jsonResponse;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getHRMS_StaffInfoEhr(String comp, String staffno, String nicno, String staffname, String deptid, String nickname)
    {
        //HttpContext.Current.Response.ContentType = "text/json";
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        HRModel oModUser = oHRCon.getStaffDetails(comp, staffno);
        if (oModUser.GetSetuserid.Trim().Length > 0)
        {
            sStatus = "Y";
            sMessage = "";
        }
        object objParam = new
        {
            GetSetstaffID = oModUser.GetSetid,
            GetSetstaffNickname = oModUser.GetSetnickname,
            GetSetstaffNo = oModUser.GetSetstaffno,
            GetSetstaffIC = oModUser.GetSetnicno,
            GetSetstaffName = oModUser.GetSetname,
            GetSetstaffPosition = oModUser.GetSetpos_name,
            GetSetstaffDesignation = oModUser.GetSetgred_name,
            GetSetdeptID = oModUser.GetSetdept_id,
            GetSetdeptName = oModUser.GetSetdept_name,
            GetSetsupervisorID = oModUser.GetSetreportto,
            GetSetjobGrade = oModUser.GetSetgred_id,
            GetSetGradeDesc = oModUser.GetSetgred_name,
            GetSetappadd1 = oModUser.GetSetpaddress1,
            GetSetappadd2 = oModUser.GetSetpaddress2,
            GetSetappadd3 = oModUser.GetSetpaddress3,
            GetSetappadd4 = oModUser.GetSetpaddress4,
            GetSetappadd5 = "",
            GetSetstaffPhone = oModUser.GetSetptelephone,
            GetSetstaffEmail = oModUser.GetSetemail1,
            GetSetbankno = oModUser.GetSetaccountno,
            GetSetepf = oModUser.GetSetepfno,
            GetSetincomeTax = oModUser.GetSettaxno,
            GetSetsocsoNo = oModUser.GetSetsocsono,
            GetSetbankcode = oModUser.GetSetaccountype,
            GetSetbankname = oModUser.GetSetbankname,
            GetSetSEX = oModUser.GetSetgender,
            GetSetRELIGION = oModUser.GetSetreligion,
            GetSetDATE_JOINED = oModUser.GetSetstr_datejoined,
            GetSetPHOTO = "",
            GetSetContentType = oModUser.GetSetusertype
        };

        //jsonResponse = new JavaScriptSerializer().Serialize(modStaff);
        object retData = new { result = sStatus, message = sMessage, itemdetail = objParam };
        jsonResponse = convertJson(retData);

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string getInfo_StaffLeaveToday(String comp, String fyr, String staffno, String fromdate, String todate)
    {
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        String jsonResponse = "";
        String sStatus = "N";
        String sMessage = "Internal Server Error, Please contact System Admin!";
        ArrayList lsStaffLeaveToday = new ArrayList();
        ArrayList rStaffLeaveToday = new ArrayList();

        rStaffLeaveToday = oHRCon.getStaffLeaveList(comp, fyr, staffno, "", "", fromdate, todate, "", "");
        for (int i = 0; i < rStaffLeaveToday.Count; i++)
        {
            HRModel modComp = (HRModel)rStaffLeaveToday[i];
            object objParamList = new
            {
                GetSetcomp = modComp.GetSetcomp,
                GetSetfyr = modComp.GetSetfyr,
                GetSetstaffno = modComp.GetSetstaffno,
                GetSetHolidayCode = modComp.GetSettype,
                GetSetfromdate = modComp.GetSetfromdate,
                GetSettodate = modComp.GetSettodate,
                GetSetreason = modComp.GetSetreason,
                GetSetstatus = modComp.GetSetstatus,
                GetSetLeaveNo = modComp.GetSetid,
                GetSetPhoneNo = "",
                GetSetstaffName = modComp.GetSetname,
            };
            lsStaffLeaveToday.Add(objParamList);
        }
        object retData = new { result = sStatus, message = sMessage, itemlist = lsStaffLeaveToday };
        jsonResponse = new JavaScriptSerializer().Serialize(retData);
        /*jsonResponse = convertJson(modApplicant);*/

        return jsonResponse;
        //HttpContext.Current.Response.Write(jsonResponse);
    }

}