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

    private String TokenNumber = "00000000";
    private String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

    private MainController oMainCon = new MainController();

    public WebServiceAdmin()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
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
}