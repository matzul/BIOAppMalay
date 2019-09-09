using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for UserProfileModel
/// </summary>
[DataContract]
public class CounterModel
{
	public CounterModel()
	{
	}

    private string id = "";
    [DataMember]
    public string GetSetid
    {
        get
        {
            string text = id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            id = value;
        }
    }

    private int rowno = 0;
    [DataMember]
    public int GetSetrowno
    {
        get
        {
            int num = rowno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            rowno = value;
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

    private string counterno = "";
    [DataMember]
    public string GetSetcounterno
    {
        get
        {
            string text = counterno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            counterno = value;
        }
    }

    private string countertranid = "";
    [DataMember]
    public string GetSetcountertranid
    {
        get
        {
            string text = countertranid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            countertranid = value;
        }
    }

    private double openingbalance = 0;
    [DataMember]
    public double GetSetopeningbalance
    {
        get
        {
            double doubleno = openingbalance;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            openingbalance = value;
        }
    }

    private string openingby = "";
    [DataMember]
    public string GetSetopeningby
    {
        get
        {
            string text = openingby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            openingby = value;
        }
    }

    private string openingbyname = "";
    [DataMember]
    public string GetSetopeningbyname
    {
        get
        {
            string text = openingbyname;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            openingbyname = value;
        }
    }

    private string openingdate = "";
    [DataMember]
    public string GetSetopeningdate
    {
        get
        {
            string text = openingdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            openingdate = value;
        }
    }

    private double totalorderamount = 0;
    [DataMember]
    public double GetSettotalorderamount
    {
        get
        {
            double doubleno = totalorderamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalorderamount = value;
        }
    }

    private double totalinvoiceamount = 0;
    [DataMember]
    public double GetSettotalinvoiceamount
    {
        get
        {
            double doubleno = totalinvoiceamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalinvoiceamount = value;
        }
    }

    private double totalpayrcptamount = 0;
    [DataMember]
    public double GetSettotalpayrcptamount
    {
        get
        {
            double doubleno = totalpayrcptamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalpayrcptamount = value;
        }
    }

    private string pos_bpid = "";
    [DataMember]
    public string GetSetpos_bpid
    {
        get
        {
            string text = pos_bpid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_bpid = value;
        }
    }

    private string pos_bpdesc = "";
    [DataMember]
    public string GetSetpos_bpdesc
    {
        get
        {
            string text = pos_bpdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_bpdesc = value;
        }
    }

    private string pos_ordercat = "";
    [DataMember]
    public string GetSetpos_ordercat
    {
        get
        {
            string text = pos_ordercat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_ordercat = value;
        }
    }

    private string pos_ordertype = "";
    [DataMember]
    public string GetSetpos_ordertype
    {
        get
        {
            string text = pos_ordertype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_ordertype = value;
        }
    }

    private string pos_orderactivity = "";
    [DataMember]
    public string GetSetpos_orderactivity
    {
        get
        {
            string text = pos_orderactivity;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_orderactivity = value;
        }
    }

    private string pos_paytype = "";
    [DataMember]
    public string GetSetpos_paytype
    {
        get
        {
            string text = pos_paytype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_paytype = value;
        }
    }

    private double closingbalance = 0;
    [DataMember]
    public double GetSetclosingbalance
    {
        get
        {
            double doubleno = closingbalance;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            closingbalance = value;
        }
    }

    private string closingby = "";
    [DataMember]
    public string GetSetclosingby
    {
        get
        {
            string text = closingby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            closingby = value;
        }
    }

    private string closingbyname = "";
    [DataMember]
    public string GetSetclosingbyname
    {
        get
        {
            string text = closingbyname;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            closingbyname = value;
        }
    }

    private string closingdate = "";
    [DataMember]
    public string GetSetclosingdate
    {
        get
        {
            string text = closingdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            closingdate = value;
        }
    }

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

    private string createdby = "";
    [DataMember]
    public string GetSetcreatedby
    {
        get
        {
            string text = createdby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            createdby = value;
        }
    }
    private string createddate = "";
    [DataMember]
    public string GetSetcreateddate
    {
        get
        {
            string text = createddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            createddate = value;
        }
    }

}