using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for CompModel
/// </summary>
public class CompModel
{
    public CompModel()
    {
        //
        // TODO: Add constructor logic here
        //
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

    private string comp_name = "";
    [DataMember]
    public string GetSetcomp_name
    {
        get
        {
            string text = comp_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_name = value;
        }
    }

    private string comp_id = "";
    [DataMember]
    public string GetSetcomp_id
    {
        get
        {
            string text = comp_id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_id = value;
        }
    }

    private string comp_accountbank = "";
    [DataMember]
    public string GetSetcomp_accountbank
    {
        get
        {
            string text = comp_accountbank;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_accountbank = value;
        }
    }

    private string comp_accountno = "";
    [DataMember]
    public string GetSetcomp_accountno
    {
        get
        {
            string text = comp_accountno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_accountno = value;
        }
    }

    private string comp_address = "";
    [DataMember]
    public string GetSetcomp_address
    {
        get
        {
            string text = comp_address;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_address = value;
        }
    }

    private string comp_contact = "";
    [DataMember]
    public string GetSetcomp_contact
    {
        get
        {
            string text = comp_contact;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_contact = value;
        }
    }

    private string comp_contactno = "";
    [DataMember]
    public string GetSetcomp_contactno
    {
        get
        {
            string text = comp_contactno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_contactno = value;
        }
    }

    private string comp_website = "";
    [DataMember]
    public string GetSetcomp_website
    {
        get
        {
            string text = comp_website;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_website = value;
        }
    }

    private string comp_email = "";
    [DataMember]
    public string GetSetcomp_email
    {
        get
        {
            string text = comp_email;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_email = value;
        }
    }

    private string comp_icon = "";
    [DataMember]
    public string GetSetcomp_icon
    {
        get
        {
            string text = comp_icon;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_icon = value;
        }
    }

    private string comp_logo1 = "";
    [DataMember]
    public string GetSetcomp_logo1
    {
        get
        {
            string text = comp_logo1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_logo1 = value;
        }
    }

    private string comp_logo2 = "";
    [DataMember]
    public string GetSetcomp_logo2
    {
        get
        {
            string text = comp_logo2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_logo2 = value;
        }
    }
}