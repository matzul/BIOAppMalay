using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for UserProfileModel
/// </summary>
[DataContract]
public class UserProfileModel
{
	public UserProfileModel()
	{
	}

    private string userid = "";
    [DataMember]
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

    private string comp = "";
    [DataMember]
    public string GetSetcomp
    {
        get
        {
            string text = comp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp = value;
        }
    }

    private string screenid = "";
    [DataMember]
    public string GetSetscreenid
    {
        get
        {
            string text = screenid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            screenid = value;
        }
    }

    private string userpwd = "";
    [DataMember]
    public string GetSetuserpwd
    {
        get
        {
            string text = userpwd;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            userpwd = value;
        }
    }

    private string username = "";
    [DataMember]
    public string GetSetusername
    {
        get
        {
            string text = username;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            username = value;
        }
    }

    private string useradd = "";
    [DataMember]
    public string GetSetuseradd
    {
        get
        {
            string text = useradd;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            useradd = value;
        }
    }

    private string usertelno = "";
    [DataMember]
    public string GetSetusertelno
    {
        get
        {
            string text = usertelno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            usertelno = value;
        }
    }

    private string usertype = "";
    [DataMember]
    public string GetSetusertype
    {
        get
        {
            string text = usertype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            usertype = value;
        }
    }

    private string userroleid = "";
    [DataMember]
    public string GetSetuserroleid
    {
        get
        {
            string text = userroleid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            userroleid = value;
        }
    }

    private string userstatus = "";
    [DataMember]
    public string GetSetuserstatus
    {
        get
        {
            string text = userstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            userstatus = value;
        }
    }

}