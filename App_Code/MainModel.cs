using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Collections;
using System.Device.Location;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for MainModel
/// </summary>
[DataContract]
public class MainModel
{
    public MainModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region/*** BEGIN MODEL FOR GENERAL***/

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

    private bool rowinclude = false;
    [DataMember]
    public bool GetSetrowinclude
    {
        get
        {
            bool intno = rowinclude;
            if (intno != false)
                return intno;
            else
                return false;
        }
        set
        {
            rowinclude = value;
        }
    }

    private CompModel CompFromDetails = new CompModel();
    public CompModel GetSetCompFromDetails
    {
        get
        {
            CompModel mod = CompFromDetails;
            if (mod != null)
                return mod;
            else
                return new CompModel();
        }
        set
        {
            CompFromDetails = value;
        }
    }

    private CompModel CompToDetails = new CompModel();
    public CompModel GetSetCompToDetails
    {
        get
        {
            CompModel mod = CompToDetails;
            if (mod != null)
                return mod;
            else
                return new CompModel();
        }
        set
        {
            CompToDetails = value;
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

    private string comp_type = "";
    [DataMember]
    public string GetSetcomp_type
    {
        get
        {
            string text = comp_type;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_type = value;
        }
    }

    private string comp_cat = "";
    [DataMember]
    public string GetSetcomp_cat
    {
        get
        {
            string text = comp_cat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_cat = value;
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

    private string comp_daerah = "";
    [DataMember]
    public string GetSetcomp_daerah
    {
        get
        {
            string text = comp_daerah;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_daerah = value;
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

    private string comp_status = "";
    [DataMember]
    public string GetSetcomp_status
    {
        get
        {
            string text = comp_status;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_status = value;
        }
    }

    //update by fakhrul @ 20012020
    private string comp_longitud = "";
    [DataMember]
    public string GetSetcomp_longitud
    {
        get
        {
            string text = comp_longitud;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_longitud = value;
        }
    }

    private string comp_latitud = "";
    [DataMember]
    public string GetSetcomp_latitud
    {
        get
        {
            string text = comp_latitud;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_latitud = value;
        }
    }

    //update by fakhrul @ 08022020
    private string comp_target = "";
    [DataMember]
    public string GetSetcomp_target
    {
        get
        {
            string text = comp_target;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_target = value;
        }
    }

    //update by fakhrul @ 08022020
    private string comp_StartDate = "";
    [DataMember]
    public string GetSetcomp_StartDate
    {
        get
        {
            string text = comp_StartDate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_StartDate = value;
        }
    }

    //update by fakhrul @ 08022020
    private string comp_EndDate = "";
    [DataMember]
    public string GetSetcomp_EndDate
    {
        get
        {
            string text = comp_EndDate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_EndDate = value;
        }
    }

    private string comp_registerno = "";
    [DataMember]
    public string GetSetcomp_registerno
    {
        get
        {
            string text = comp_registerno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_registerno = value;

        }
    }

    private string comp_area = "";
    [DataMember]
    public string GetSetcomp_area
    {
        get
        {
            string text = comp_area;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_area = value;

        }
    }
    private string comp_landstatus = "";
    [DataMember]
    public string GetSetcomp_landstatus
    {
        get
        {
            string text = comp_landstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            comp_landstatus = value;

        }
    }

    private string type = "";
    [DataMember]
    public string GetSettype
    {
        get
        {
            string text = type;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            type = value;
        }
    }

    private string initial = "";
    [DataMember]
    public string GetSetinitial
    {
        get
        {
            string text = initial;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            initial = value;
        }
    }

    private string year = "";
    [DataMember]
    public string GetSetyear
    {
        get
        {
            string text = year;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            year = value;
        }
    }

    private int runno = 0;
    [DataMember]
    public int GetSetrunno
    {
        get
        {
            int num = runno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            runno = value;
        }
    }

    private string remarks = "";
    [DataMember]
    public string GetSetremarks
    {
        get
        {
            string text = remarks;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            remarks = value;
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

    private string alertstatus = "";
    [DataMember]
    public string GetSetalertstatus
    {
        get
        {
            string text = alertstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            alertstatus = value;
        }
    }

    private string alertmessage = "";
    [DataMember]
    public string GetSetalertmessage
    {
        get
        {
            string text = alertmessage;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            alertmessage = value;
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
    private string modifiedby = "";
    [DataMember]
    public string GetSetmodifiedby
    {
        get
        {
            string text = modifiedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            modifiedby = value;
        }
    }
    private string modifieddate = "";
    [DataMember]
    public string GetSetmodifieddate
    {
        get
        {
            string text = modifieddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            modifieddate = value;
        }
    }
    private string confirmedby = "";
    [DataMember]
    public string GetSetconfirmedby
    {
        get
        {
            string text = confirmedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            confirmedby = value;
        }
    }
    private string confirmeddate = "";
    [DataMember]
    public string GetSetconfirmeddate
    {
        get
        {
            string text = confirmeddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            confirmeddate = value;
        }
    }
    private string cancelledby = "";
    [DataMember]
    public string GetSetcancelledby
    {
        get
        {
            string text = cancelledby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cancelledby = value;
        }
    }
    private string cancelleddate = "";
    [DataMember]
    public string GetSetcancelleddate
    {
        get
        {
            string text = cancelleddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cancelleddate = value;
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

    private int lineno = 0;
    [DataMember]
    public int GetSetlineno
    {
        get
        {
            int num = lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            lineno = value;
        }
    }

    private int saleslineno = 0;
    [DataMember]
    public int GetSetsaleslineno
    {
        get
        {
            int num = saleslineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            saleslineno = value;
        }
    }

    private int quantity = 0;
    [DataMember]
    public int GetSetquantity
    {
        get
        {
            int num = quantity;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            quantity = value;
        }
    }

    private string financeyear = "";
    [DataMember]
    public string GetSetfinanceyear
    {
        get
        {
            string text = financeyear;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            financeyear = value;
        }
    }
    private string financemonth = "";
    [DataMember]
    public string GetSetfinancemonth
    {
        get
        {
            string text = financemonth;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            financemonth = value;
        }
    }
    private string actualyear = "";
    [DataMember]
    public string GetSetactualyear
    {
        get
        {
            string text = actualyear;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            actualyear = value;
        }
    }
    private string actualmonth = "";
    [DataMember]
    public string GetSetactualmonth
    {
        get
        {
            string text = actualmonth;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            actualmonth = value;
        }
    }

    private string fyr = "";
    [DataMember]
    public string GetSetfyr
    {
        get
        {
            string text = fyr;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            fyr = value;
        }
    }
    private double MON01 = 0;
    [DataMember]
    public double GetSetMON01
    {
        get
        {
            double doubleno = MON01;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON01 = value;
        }
    }
    private double MON02 = 0;
    [DataMember]
    public double GetSetMON02
    {
        get
        {
            double doubleno = MON02;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON02 = value;
        }
    }
    private double MON03 = 0;
    [DataMember]
    public double GetSetMON03
    {
        get
        {
            double doubleno = MON03;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON03 = value;
        }
    }
    private double MON04 = 0;
    [DataMember]
    public double GetSetMON04
    {
        get
        {
            double doubleno = MON04;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON04 = value;
        }
    }
    private double MON05 = 0;
    [DataMember]
    public double GetSetMON05
    {
        get
        {
            double doubleno = MON05;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON05 = value;
        }
    }
    private double MON06 = 0;
    [DataMember]
    public double GetSetMON06
    {
        get
        {
            double doubleno = MON06;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON06 = value;
        }
    }
    private double MON07 = 0;
    [DataMember]
    public double GetSetMON07
    {
        get
        {
            double doubleno = MON07;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON07 = value;
        }
    }
    private double MON08 = 0;
    [DataMember]
    public double GetSetMON08
    {
        get
        {
            double doubleno = MON08;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON08 = value;
        }
    }
    private double MON09 = 0;
    [DataMember]
    public double GetSetMON09
    {
        get
        {
            double doubleno = MON09;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON09 = value;
        }
    }
    private double MON10 = 0;
    [DataMember]
    public double GetSetMON10
    {
        get
        {
            double doubleno = MON10;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON10 = value;
        }
    }
    private double MON11 = 0;
    [DataMember]
    public double GetSetMON11
    {
        get
        {
            double doubleno = MON11;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON11 = value;
        }
    }
    private double MON12 = 0;
    [DataMember]
    public double GetSetMON12
    {
        get
        {
            double doubleno = MON12;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            MON12 = value;
        }
    }

    private string MON01desc = "";
    [DataMember]
    public string GetSetMON01desc
    {
        get
        {
            string text = MON01desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON01desc = value;
        }
    }
    private string MON02desc = "";
    [DataMember]
    public string GetSetMON02desc
    {
        get
        {
            string text = MON02desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON02desc = value;
        }
    }
    private string MON03desc = "";
    [DataMember]
    public string GetSetMON03desc
    {
        get
        {
            string text = MON03desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON03desc = value;
        }
    }
    private string MON04desc = "";
    [DataMember]
    public string GetSetMON04desc
    {
        get
        {
            string text = MON04desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON04desc = value;
        }
    }
    private string MON05desc = "";
    [DataMember]
    public string GetSetMON05desc
    {
        get
        {
            string text = MON05desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON05desc = value;
        }
    }
    private string MON06desc = "";
    [DataMember]
    public string GetSetMON06desc
    {
        get
        {
            string text = MON06desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON06desc = value;
        }
    }
    private string MON07desc = "";
    [DataMember]
    public string GetSetMON07desc
    {
        get
        {
            string text = MON07desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON07desc = value;
        }
    }
    private string MON08desc = "";
    [DataMember]
    public string GetSetMON08desc
    {
        get
        {
            string text = MON08desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON08desc = value;
        }
    }
    private string MON09desc = "";
    [DataMember]
    public string GetSetMON09desc
    {
        get
        {
            string text = MON09desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON09desc = value;
        }
    }
    private string MON10desc = "";
    [DataMember]
    public string GetSetMON10desc
    {
        get
        {
            string text = MON10desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON10desc = value;
        }
    }
    private string MON11desc = "";
    [DataMember]
    public string GetSetMON11desc
    {
        get
        {
            string text = MON11desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON11desc = value;
        }
    }
    private string MON12desc = "";
    [DataMember]
    public string GetSetMON12desc
    {
        get
        {
            string text = MON12desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            MON12desc = value;
        }
    }

    private double TODATE = 0;
    [DataMember]
    public double GetSetTODATE
    {
        get
        {
            double doubleno = TODATE;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            TODATE = value;
        }
    }

    private string paramid = "";
    [DataMember]
    public string GetSetparamid
    {
        get
        {
            string text = paramid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramid = value;
        }
    }
    private string paramtype = "";
    [DataMember]
    public string GetSetparamtype
    {
        get
        {
            string text = paramtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramtype = value;
        }
    }
    private string paramcode = "";
    [DataMember]
    public string GetSetparamcode
    {
        get
        {
            string text = paramcode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramcode = value;
        }
    }
    private string paramdesc = "";
    [DataMember]
    public string GetSetparamdesc
    {
        get
        {
            string text = paramdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramdesc = value;
        }
    }
    private string paramstatus = "";
    [DataMember]
    public string GetSetparamstatus
    {
        get
        {
            string text = paramstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramstatus = value;
        }
    }

    #endregion/*** END MODEL FOR GENERAL***/

    #region/*** BEGIN JAWATANKUASA COMP ***/

    private string committee_id = "";
    [DataMember]
    public string GetSetcommittee_id
    {
        get
        {
            string text = committee_id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_id = value;
        }
    }

    private string committee_name = "";
    [DataMember]
    public string GetSetcommittee_name
    {
        get
        {
            string text = committee_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_name = value;
        }
    }

    private string committee_address = "";
    [DataMember]
    public string GetSetcommittee_address
    {
        get
        {
            string text = committee_address;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_address = value;
        }
    }

    private string committee_contact = "";
    [DataMember]
    public string GetSetcommittee_contact
    {
        get
        {
            string text = committee_contact;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_contact = value;
        }
    }

    private string committee_role = "";
    [DataMember]
    public string GetSetcommittee_role
    {
        get
        {
            string text = committee_role;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_role = value;
        }
    }

    private string committee_prevrole = "";
    [DataMember]
    public string GetSetcommittee_prevrole
    {
        get
        {
            string text = committee_prevrole;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_prevrole = value;
        }
    }

    private string committee_appointmentby = "";
    [DataMember]
    public string GetSetcommittee_appointmentby
    {
        get
        {
            string text = committee_appointmentby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_appointmentby = value;
        }
    }

    private string committee_certno = "";
    [DataMember]
    public string GetSetcommittee_certno
    {
        get
        {
            string text = committee_certno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_certno = value;
        }
    }

    private string exchangeid = "";
    [DataMember]
    public string GetSetexchangeid
    {
        get
        {
            string text = exchangeid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            exchangeid = value;
        }
    }

    private string committee_dob = "";
    [DataMember]
    public string GetSetcommittee_dob
    {
        get
        {
            string text = committee_dob;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_dob = value;
        }
    }

    private string committee_doa = "";
    [DataMember]
    public string GetSetcommittee_doa
    {
        get
        {
            string text = committee_doa;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_doa = value;
        }
    }

    private string committee_age = "";
    [DataMember]
    public string GetSetcommittee_age
    {
        get
        {
            string text = committee_age;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_age = value;
        }
    }

    private string committee_job = "";
    [DataMember]
    public string GetSetcommittee_job
    {
        get
        {
            string text = committee_job;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_job = value;
        }
    }

    private string committee_status = "";
    [DataMember]
    public string GetSetcommittee_status
    {
        get
        {
            string text = committee_status;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            committee_status = value;
        }
    }
    #endregion/*** END JAWATANKUASA COMP*/


    #region/*** BEGIN MODEL FOR PEOPLE ***/

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
    private string name = "";
    [DataMember]
    public string GetSetname
    {
        get
        {
            string text = name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            name = value;
        }
    }
    private string nokp = "";
    [DataMember]
    public string GetSetnokp
    {
        get
        {
            string text = nokp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            nokp = value;
        }
    }
    private string address = "";
    [DataMember]
    public string GetSetaddress
    {
        get
        {
            string text = address;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            address = value;
        }
    }
    private string gender = "";
    [DataMember]
    public string GetSetgender
    {
        get
        {
            string text = gender;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            gender = value;
        }
    }
    private string telno = "";
    [DataMember]
    public string GetSettelno
    {
        get
        {
            string text = telno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            telno = value;
        }
    }
    private string email = "";
    [DataMember]
    public string GetSetemail
    {
        get
        {
            string text = email;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            email = value;
        }
    }

    #endregion/*** END MODEL FOR PEOPLE ***/

    #region/*** BEGIN MODEL FOR STITCH ***/

    private string stitchno = "";
    [DataMember]
    public string GetSetstitchno
    {
        get
        {
            string text = stitchno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            stitchno = value;
        }
    }
    private string stitchdate = "";
    [DataMember]
    public string GetSetstitchdate
    {
        get
        {
            string text = stitchdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            stitchdate = value;
        }
    }
    private string peopleid = "";
    [DataMember]
    public string GetSetpeopleid
    {
        get
        {
            string text = peopleid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            peopleid = value;
        }
    }
    private string measurement = "";
    [DataMember]
    public string GetSetmeasurement
    {
        get
        {
            string text = measurement;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            measurement = value;
        }
    }
    private string baju_bahu = "";
    [DataMember]
    public string GetSetbaju_bahu
    {
        get
        {
            string text = baju_bahu;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_bahu = value;
        }
    }
    private string baju_labuh_lengan = "";
    [DataMember]
    public string GetSetbaju_labuh_lengan
    {
        get
        {
            string text = baju_labuh_lengan;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_labuh_lengan = value;
        }
    }
    private string baju_labuh_baju = "";
    [DataMember]
    public string GetSetbaju_labuh_baju
    {
        get
        {
            string text = baju_labuh_baju;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_labuh_baju = value;
        }
    }
    private string baju_dada = "";
    [DataMember]
    public string GetSetbaju_dada
    {
        get
        {
            string text = baju_dada;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_dada = value;
        }
    }
    private string baju_pinggang = "";
    [DataMember]
    public string GetSetbaju_pinggang
    {
        get
        {
            string text = baju_pinggang;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_pinggang = value;
        }
    }
    private string baju_punggung = "";
    [DataMember]
    public string GetSetbaju_punggung
    {
        get
        {
            string text = baju_punggung;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_punggung = value;
        }
    }
    private string baju_labuh_kain = "";
    [DataMember]
    public string GetSetbaju_labuh_kain
    {
        get
        {
            string text = baju_labuh_kain;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_labuh_kain = value;
        }
    }
    private string baju_leher = "";
    [DataMember]
    public string GetSetbaju_leher
    {
        get
        {
            string text = baju_leher;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_leher = value;
        }
    }
    private string baju_span = "";
    [DataMember]
    public string GetSetbaju_span
    {
        get
        {
            string text = baju_span;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_span = value;
        }
    }
    private string baju_bahu_dada = "";
    [DataMember]
    public string GetSetbaju_bahu_dada
    {
        get
        {
            string text = baju_bahu_dada;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_bahu_dada = value;
        }
    }
    private string baju_bahu_pinggang = "";
    [DataMember]
    public string GetSetbaju_bahu_pinggang
    {
        get
        {
            string text = baju_bahu_pinggang;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baju_bahu_pinggang = value;
        }
    }
    private string seluar_pinggang = "";
    [DataMember]
    public string GetSetseluar_pinggang
    {
        get
        {
            string text = seluar_pinggang;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_pinggang = value;
        }
    }
    private string seluar_punggung = "";
    [DataMember]
    public string GetSetseluar_punggung
    {
        get
        {
            string text = seluar_punggung;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_punggung = value;
        }
    }
    private string seluar_cawat = "";
    [DataMember]
    public string GetSetseluar_cawat
    {
        get
        {
            string text = seluar_cawat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_cawat = value;
        }
    }
    private string seluar_paha = "";
    [DataMember]
    public string GetSetseluar_paha
    {
        get
        {
            string text = seluar_paha;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_paha = value;
        }
    }
    private string seluar_lutut = "";
    [DataMember]
    public string GetSetseluar_lutut
    {
        get
        {
            string text = seluar_lutut;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_lutut = value;
        }
    }
    private string seluar_bukaan_kaki = "";
    [DataMember]
    public string GetSetseluar_bukaan_kaki
    {
        get
        {
            string text = seluar_bukaan_kaki;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_bukaan_kaki = value;
        }
    }
    private string seluar_labuh_seluar = "";
    [DataMember]
    public string GetSetseluar_labuh_seluar
    {
        get
        {
            string text = seluar_labuh_seluar;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            seluar_labuh_seluar = value;
        }
    }

    #endregion/*** END MODEL FOR STITCH ***/

    #region/*** BEGIN MODEL FOR ITEM***/

    private string itemno = "";
    [DataMember]
    public string GetSetitemno
    {
        get
        {
            string text = itemno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemno = value;
        }
    }
    private string itemdesc = "";
    [DataMember]
    public string GetSetitemdesc
    {
        get
        {
            string text = itemdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemdesc = value;
        }
    }
    private string itemcat = "";
    [DataMember]
    public string GetSetitemcat
    {
        get
        {
            string text = itemcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemcat = value;
        }
    }
    private string itemcatdesc = "";
    [DataMember]
    public string GetSetitemcatdesc
    {
        get
        {
            string text = itemcatdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemcatdesc = value;
        }
    }
    private string itemtype = "";
    [DataMember]
    public string GetSetitemtype
    {
        get
        {
            string text = itemtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemtype = value;
        }
    }
    private string itemtypedesc = "";
    [DataMember]
    public string GetSetitemtypedesc
    {
        get
        {
            string text = itemtypedesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemtypedesc = value;
        }
    }
    private string filename = "";
    [DataMember]
    public string GetSetfilename
    {
        get
        {
            string text = filename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            filename = value;
        }
    }
    private int imgwidth = 0;
    [DataMember]
    public int GetSetimgwidth
    {
        get
        {
            int num = imgwidth;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            imgwidth = value;
        }
    }
    private int imgheight = 0;
    [DataMember]
    public int GetSetimgheight
    {
        get
        {
            int num = imgheight;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            imgheight = value;
        }
    }
    private string location = "";
    [DataMember]
    public string GetSetlocation
    {
        get
        {
            string text = location;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            location = value;
        }
    }
    private string itemstatus = "";
    [DataMember]
    public string GetSetitemstatus
    {
        get
        {
            string text = itemstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemstatus = value;
        }
    }
    private string itemstatusdesc = "";
    [DataMember]
    public string GetSetitemstatusdesc
    {
        get
        {
            string text = itemstatusdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemstatusdesc = value;
        }
    }

    #endregion/*** END MODEL FOR ITEM***/

    #region/*** BEGIN MODEL FOR STOCK***/
    private int qtyorder = 0;
    [DataMember]
    public int GetSetqtyorder
    {
        get
        {
            int num = qtyorder;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyorder = value;
        }
    }
    private int qtydemand = 0;
    [DataMember]
    public int GetSetqtydemand
    {
        get
        {
            int num = qtydemand;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtydemand = value;
        }
    }
    private int qtysoh = 0;
    [DataMember]
    public int GetSetqtysoh
    {
        get
        {
            int num = qtysoh;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtysoh = value;
        }
    }
    private double costsoh = 0;
    [DataMember]
    public double GetSetcostsoh
    {
        get
        {
            double doubleno = costsoh;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            costsoh = value;
        }
    }
    private int qtyallocated = 0;
    [DataMember]
    public int GetSetqtyallocated
    {
        get
        {
            int num = qtyallocated;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyallocated = value;
        }
    }
    private int qtyavailable = 0;
    [DataMember]
    public int GetSetqtyavailable
    {
        get
        {
            int num = qtyavailable;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyavailable = value;
        }
    }
    private int qtysafetystock = 0;
    [DataMember]
    public int GetSetqtysafetystock
    {
        get
        {
            int num = qtysafetystock;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtysafetystock = value;
        }
    }

    #endregion/*** END MODEL FOR STOCK***/

    #region/*** BEGIN MODEL FOR ORDER***/
    private string orderno = "";
    [DataMember]
    public string GetSetorderno
    {
        get
        {
            string text = orderno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderno = value;
        }
    }
    private string salesorderno = "";
    [DataMember]
    public string GetSetsalesorderno
    {
        get
        {
            string text = salesorderno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            salesorderno = value;
        }
    }
    private string orderdate = "";
    [DataMember]
    public string GetSetorderdate
    {
        get
        {
            string text = orderdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderdate = value;
        }
    }
    private string ordercat = "";
    [DataMember]
    public string GetSetordercat
    {
        get
        {
            string text = ordercat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercat = value;
        }
    }
    private string ordercatdesc = "";
    [DataMember]
    public string GetSetordercatdesc
    {
        get
        {
            string text = ordercatdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercatdesc = value;
        }
    }
    private string orderactivity = "";
    [DataMember]
    public string GetSetorderactivity
    {
        get
        {
            string text = orderactivity;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderactivity = value;
        }
    }
    private string ordertype = "";
    [DataMember]
    public string GetSetordertype
    {
        get
        {
            string text = ordertype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordertype = value;
        }
    }
    private string ordertypedesc = "";
    public string GetSetordertypedesc
    {
        get
        {
            string text = ordertypedesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordertypedesc = value;
        }
    }
    private string paytype = "";
    [DataMember]
    public string GetSetpaytype
    {
        get
        {
            string text = paytype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paytype = value;
        }
    }
    private string paytypedesc = "";
    [DataMember]
    public string GetSetpaytypedesc
    {
        get
        {
            string text = paytypedesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paytypedesc = value;
        }
    }
    private string plandeliverydate = "";
    [DataMember]
    public string GetSetplandeliverydate
    {
        get
        {
            string text = plandeliverydate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            plandeliverydate = value;
        }
    }
    private string expirydate = "";
    [DataMember]
    public string GetSetexpirydate
    {
        get
        {
            string text = expirydate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            expirydate = value;
        }
    }
    private string orderremarks = "";
    [DataMember]
    public string GetSetorderremarks
    {
        get
        {
            string text = orderremarks;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderremarks = value;
        }
    }
    private string orderstatus = "";
    [DataMember]
    public string GetSetorderstatus
    {
        get
        {
            string text = orderstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderstatus = value;
        }
    }
    private string ordercreated = "";
    [DataMember]
    public string GetSetordercreated
    {
        get
        {
            string text = ordercreated;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercreated = value;
        }
    }
    private string ordercreateddate = "";
    [DataMember]
    public string GetSetordercreateddate
    {
        get
        {
            string text = ordercreateddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercreateddate = value;
        }
    }
    private string orderapproved = "";
    [DataMember]
    public string GetSetorderapproved
    {
        get
        {
            string text = orderapproved;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderapproved = value;
        }
    }
    private string orderapproveddate = "";
    [DataMember]
    public string GetSetorderapproveddate
    {
        get
        {
            string text = orderapproveddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            orderapproveddate = value;
        }
    }

    private string ordercancelled = "";
    [DataMember]
    public string GetSetordercancelled
    {
        get
        {
            string text = ordercancelled;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercancelled = value;
        }
    }
    private string ordercancelleddate = "";
    [DataMember]
    public string GetSetordercancelleddate
    {
        get
        {
            string text = ordercancelleddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ordercancelleddate = value;
        }
    }

    private int order_lineno = 0;
    [DataMember]
    public int GetSetorder_lineno
    {
        get
        {
            int num = order_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            order_lineno = value;
        }
    }

    private int order_quantity = 0;
    [DataMember]
    public int GetSetorder_quantity
    {
        get
        {
            int num = order_quantity;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            order_quantity = value;
        }
    }

    #endregion/*** END MODEL FOR ORDER***/

    #region/*** BEGIN MODEL FOR SHIPMENT***/
    private string shipmentno = "";
    [DataMember]
    public string GetSetshipmentno
    {
        get
        {
            string text = shipmentno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            shipmentno = value;
        }
    }
    private int shipment_lineno = 0;
    [DataMember]
    public int GetSetshipment_lineno
    {
        get
        {
            int num = shipment_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            shipment_lineno = value;
        }
    }
    private string shipmentdate = "";
    [DataMember]
    public string GetSetshipmentdate
    {
        get
        {
            string text = shipmentdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            shipmentdate = value;
        }
    }
    private string shipmentcat = "";
    [DataMember]
    public string GetSetshipmentcat
    {
        get
        {
            string text = shipmentcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            shipmentcat = value;
        }
    }

    private int shipment_quantity = 0;
    [DataMember]
    public int GetSetshipment_quantity
    {
        get
        {
            int num = shipment_quantity;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            shipment_quantity = value;
        }
    }

    private string shipmentstatus = "";
    [DataMember]
    public string GetSetshipmentstatus
    {
        get
        {
            string text = shipmentstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            shipmentstatus = value;
        }
    }

    private string hasinvoice = "";
    [DataMember]
    public string GetSethasinvoice
    {
        get
        {
            string text = hasinvoice;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            hasinvoice = value;
        }
    }

    #endregion/*** END MODEL FOR SHIPMENT***/

    #region /*** BEGIN CASH IN & OUT***/

    private string CashInOutcomp = "";
    [DataMember]
    public string GetSetCashInOutcomp
    {
        get
        {
            string text = CashInOutcomp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutcomp = value;
        }
    }

    private string CashInOutno = "";
    [DataMember]
    public string GetSetCashInOutno
    {
        get
        {
            string text = CashInOutno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutno = value;
        }
    }


    private string CashInOutAmount = "";
    [DataMember]
    public string GetSetCashInOutAmount
    {
        get
        {
            string text = CashInOutAmount;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutAmount = value;
        }
    }

    private string CashInOutType = "";
    [DataMember]
    public string GetSetCashInOutType
    {
        get
        {
            string text = CashInOutType;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutType = value;
        }
    }
    private string paramttype = "";
    [DataMember]
    public string GetSetparamttype
    {
        get
        {
            string text = paramttype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramttype = value;
        }
    }
    private string paramtdesc = "";
    [DataMember]
    public string GetSetparamtdesc
    {
        get
        {
            string text = paramtdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramtdesc = value;
        }
    }

    private string paramtcode = "";
    [DataMember]
    public string GetSetparamtcode
    {
        get
        {
            string text = paramtcode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramtcode = value;
        }
    }
    private string paramtcategory = "";
    [DataMember]
    public string GetSetparamtcategory
    {
        get
        {
            string text = paramtcategory;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paramtcategory = value;
        }
    }
    private string TotalCashIn = "";
    [DataMember]
    public string GetSetTotalCashIn
    {
        get
        {
            string text = TotalCashIn;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            TotalCashIn = value;
        }
    }

    private string TotalCashout = "";
    [DataMember]
    public string GetSetTotalCashout
    {
        get
        {
            string text = TotalCashout;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            TotalCashout = value;
        }
    }


    private string CashInOutpayno = "";
    [DataMember]
    public string GetSetCashInOutpayno
    {
        get
        {
            string text = CashInOutpayno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutpayno = value;
        }
    }
    private string CashInOutresno = "";
    [DataMember]
    public string GetSetCashInOutresno
    {
        get
        {
            string text = CashInOutresno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutresno = value;
        }
    }

    private string CashInOutdate = "";
    [DataMember]
    public string GetSetCashInOutdate
    {
        get
        {
            string text = CashInOutdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutdate = value;
        }
    }



    private string CashInOutDesc = "";
    [DataMember]
    public string GetSetCashInOutDesc
    {
        get
        {
            string text = CashInOutDesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutDesc = value;
        }
    }

    private string CashInOutStatus = "";
    [DataMember]
    public string GetSetCashInOutStatus
    {
        get
        {
            string text = CashInOutStatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            CashInOutStatus = value;
        }
    }

    #endregion/*** END CASH IN & OUT***/

    #region/*** BEGIN MODEL FOR INVOICE***/

    private string invoiceno = "";
    [DataMember]
    public string GetSetinvoiceno
    {
        get
        {
            string text = invoiceno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoiceno = value;
        }
    }
    private int invoice_lineno = 0;
    [DataMember]
    public int GetSetinvoice_lineno
    {
        get
        {
            int num = invoice_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            invoice_lineno = value;
        }
    }
    private string invoicedate = "";
    [DataMember]
    public string GetSetinvoicedate
    {
        get
        {
            string text = invoicedate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoicedate = value;
        }
    }
    private string invoicecat = "";
    [DataMember]
    public string GetSetinvoicecat
    {
        get
        {
            string text = invoicecat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoicecat = value;
        }
    }
    private string invoicetype = "";
    [DataMember]
    public string GetSetinvoicetype
    {
        get
        {
            string text = invoicetype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoicetype = value;
        }
    }
    private string invoiceterm = "";
    [DataMember]
    public string GetSetinvoiceterm
    {
        get
        {
            string text = invoiceterm;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoiceterm = value;
        }
    }
    private double totalinvoice = 0;
    [DataMember]
    public double GetSettotalinvoice
    {
        get
        {
            double doubleno = totalinvoice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalinvoice = value;
        }
    }

    private string invoicestatus = "";
    [DataMember]
    public string GetSetinvoicestatus
    {
        get
        {
            string text = invoicestatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            invoicestatus = value;
        }
    }

    #endregion/*** END MODEL FOR INVOICE***/

    #region/*** BEGIN MODEL FOR PAYMENT RECEIPT ***/

    private string payrcptno = "";
    [DataMember]
    public string GetSetpayrcptno
    {
        get
        {
            string text = payrcptno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payrcptno = value;
        }
    }
    private string payrcptdate = "";
    [DataMember]
    public string GetSetpayrcptdate
    {
        get
        {
            string text = payrcptdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payrcptdate = value;
        }
    }
    private string payrcpttype = "";
    [DataMember]
    public string GetSetpayrcpttype
    {
        get
        {
            string text = payrcpttype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payrcpttype = value;
        }
    }
    private double totalpayrcpt = 0;
    [DataMember]
    public double GetSettotalpayrcpt
    {
        get
        {
            double doubleno = totalpayrcpt;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalpayrcpt = value;
        }
    }
    private string payrefno = "";
    [DataMember]
    public string GetSetpayrefno
    {
        get
        {
            string text = payrefno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payrefno = value;
        }
    }
    private string payremarks = "";
    [DataMember]
    public string GetSetpayremarks
    {
        get
        {
            string text = payremarks;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payremarks = value;
        }
    }

    private string payrcptstatus = "";
    [DataMember]
    public string GetSetpayrcptstatus
    {
        get
        {
            string text = payrcptstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            payrcptstatus = value;
        }
    }

    private double paidamount = 0;
    [DataMember]
    public double GetSetpaidamount
    {
        get
        {
            double doubleno = paidamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            paidamount = value;
        }
    }

    private double balanceamount = 0;
    [DataMember]
    public double GetSetbalanceamount
    {
        get
        {
            double doubleno = balanceamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            balanceamount = value;
        }
    }
    #endregion/*** END MODEL FOR PAYMENT RECEIPT ***/

    #region/*** BEGIN MODEL FOR COSTING ***/
    private double purchaseprice = 0;
    [DataMember]
    public double GetSetpurchaseprice
    {
        get
        {
            double doubleno = purchaseprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            purchaseprice = value;
        }
    }
    private double costprice = 0;
    [DataMember]
    public double GetSetcostprice
    {
        get
        {
            double doubleno = costprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            costprice = value;
        }
    }
    private double salesprice = 0;
    [DataMember]
    public double GetSetsalesprice
    {
        get
        {
            double doubleno = salesprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            salesprice = value;
        }
    }
    private double salesamount = 0;
    [DataMember]
    public double GetSetsalesamount
    {
        get
        {
            double doubleno = salesamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            salesamount = value;
        }
    }
    private double purchaseamount = 0;
    [DataMember]
    public double GetSetpurchaseamount
    {
        get
        {
            double doubleno = purchaseamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            purchaseamount = value;
        }
    }
    private double transferamount = 0;
    [DataMember]
    public double GetSettransferamount
    {
        get
        {
            double doubleno = transferamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            transferamount = value;
        }
    }
    private double invoiceprice = 0;
    [DataMember]
    public double GetSetinvoiceprice
    {
        get
        {
            double doubleno = invoiceprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            invoiceprice = value;
        }
    }
    private double payrcptprice = 0;
    [DataMember]
    public double GetSetpayrcptprice
    {
        get
        {
            double doubleno = payrcptprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            payrcptprice = value;
        }
    }
    private double unitcost = 0;
    [DataMember]
    public double GetSetunitcost
    {
        get
        {
            double doubleno = unitcost;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            unitcost = value;
        }
    }
    private double unitprice = 0;
    [DataMember]
    public double GetSetunitprice
    {
        get
        {
            double doubleno = unitprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            unitprice = value;
        }
    }
    private string disccat = "";
    [DataMember]
    public string GetSetdisccat
    {
        get
        {
            string text = disccat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            disccat = value;
        }
    }
    private double discvalue = 0;
    [DataMember]
    public double GetSetdiscvalue
    {
        get
        {
            double doubleno = discvalue;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            discvalue = value;
        }
    }
    private double discamount = 0;
    [DataMember]
    public double GetSetdiscamount
    {
        get
        {
            double doubleno = discamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            discamount = value;
        }
    }
    private int deliverqty = 0;
    [DataMember]
    public int GetSetdeliverqty
    {
        get
        {
            int num = deliverqty;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            deliverqty = value;
        }
    }
    private int receiptqty = 0;
    [DataMember]
    public int GetSetreceiptqty
    {
        get
        {
            int num = receiptqty;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            receiptqty = value;
        }
    }
    private double orderprice = 0;
    [DataMember]
    public double GetSetorderprice
    {
        get
        {
            double doubleno = orderprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            orderprice = value;
        }
    }
    private double orderamount = 0;
    [DataMember]
    public double GetSetorderamount
    {
        get
        {
            double doubleno = orderamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            orderamount = value;
        }
    }
    private string taxcode = "";
    [DataMember]
    public string GetSettaxcode
    {
        get
        {
            string text = taxcode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            taxcode = value;
        }
    }
    private double taxrate = 0;
    [DataMember]
    public double GetSettaxrate
    {
        get
        {
            double doubleno = taxrate;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            taxrate = value;
        }
    }
    private double taxamount = 0;
    [DataMember]
    public double GetSettaxamount
    {
        get
        {
            double doubleno = taxamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            taxamount = value;
        }
    }
    private double totalprice = 0;
    [DataMember]
    public double GetSettotalprice
    {
        get
        {
            double doubleno = totalprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalprice = value;
        }
    }
    private double totalamount = 0;
    [DataMember]
    public double GetSettotalamount
    {
        get
        {
            double doubleno = totalamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalamount = value;
        }
    }
    private double invoiceamount = 0;
    [DataMember]
    public double GetSetinvoiceamount
    {
        get
        {
            double doubleno = invoiceamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            invoiceamount = value;
        }
    }
    private double billingamount = 0;
    [DataMember]
    public double GetSetbillingamount
    {
        get
        {
            double doubleno = billingamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            billingamount = value;
        }
    }
    private double payrcptamount = 0;
    [DataMember]
    public double GetSetpayrcptamount
    {
        get
        {
            double doubleno = payrcptamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            payrcptamount = value;
        }
    }
    private double paypaidamount = 0;
    [DataMember]
    public double GetSetpaypaidamount
    {
        get
        {
            double doubleno = paypaidamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            paypaidamount = value;
        }
    }

    #endregion/*** END MODEL FOR COSTING ***/

    #region/*** BEGIN MODEL FOR BP ***/

    private string bpid = "";
    [DataMember]
    public string GetSetbpid
    {
        get
        {
            string text = bpid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpid = value;
        }
    }
    private string bpdesc = "";
    [DataMember]
    public string GetSetbpdesc
    {
        get
        {
            string text = bpdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpdesc = value;
        }
    }
    private string bpaddress = "";
    [DataMember]
    public string GetSetbpaddress
    {
        get
        {
            string text = bpaddress;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpaddress = value;
        }
    }
    private string bpcontact = "";
    [DataMember]
    public string GetSetbpcontact
    {
        get
        {
            string text = bpcontact;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpcontact = value;
        }
    }
    private string bpreference = "";
    [DataMember]
    public string GetSetbpreference
    {
        get
        {
            string text = bpreference;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpreference = value;
        }
    }
    private string bpcat = "";
    [DataMember]
    public string GetSetbpcat
    {
        get
        {
            string text = bpcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpcat = value;
        }
    }
    private string discounttype = "";
    [DataMember]
    public string GetSetdiscounttype
    {
        get
        {
            string text = discounttype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            discounttype = value;
        }
    }
    private double cashguarantee = 0;
    [DataMember]
    public double GetSetcashguarantee
    {
        get
        {
            double doubleno = cashguarantee;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            cashguarantee = value;
        }
    }
    private double bankguarantee = 0;
    [DataMember]
    public double GetSetbankguarantee
    {
        get
        {
            double doubleno = bankguarantee;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            bankguarantee = value;
        }
    }
    private double creditlimit = 0;
    [DataMember]
    public double GetSetcreditlimit
    {
        get
        {
            double doubleno = creditlimit;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            creditlimit = value;
        }
    }
    private string bpstatus = "";
    [DataMember]
    public string GetSetbpstatus
    {
        get
        {
            string text = bpstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bpstatus = value;
        }
    }

    #endregion/*** END MODEL FOR BP ***/


    #region/*** BEGIN MODEL FOR ASSET ***/

    private string assetno = "";
    [DataMember]
    public string GetSetassetno
    {
        get
        {
            string text = assetno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetno = value;
        }
    }
    private string assetdesc = "";
    [DataMember]
    public string GetSetassetdesc
    {
        get
        {
            string text = assetdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetdesc = value;
        }
    }
    private string assettyp = "";
    [DataMember]
    public string GetSetassettyp
    {
        get
        {
            string text = assettyp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assettyp = value;
        }
    }
    private string assetcat = "";
    [DataMember]
    public string GetSetassetcat
    {
        get
        {
            string text = assetcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetcat = value;
        }
    }
    private string assetowner = "";
    [DataMember]
    public string GetSetassetowner
    {
        get
        {
            string text = assetowner;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetowner = value;
        }
    }
    private string assetrefno = "";
    [DataMember]
    public string GetSetassetrefno
    {
        get
        {
            string text = assetrefno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetrefno = value;
        }
    }
    private string datemfg = "";
    [DataMember]
    public string GetSetdatemfg
    {
        get
        {
            string text = datemfg;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datemfg = value;
        }
    }
    private string warranty = "";
    [DataMember]
    public string GetSetwarranty
    {
        get
        {
            string text = warranty;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            warranty = value;
        }
    }
    private string datewarend = "";
    [DataMember]
    public string GetSetdatewarend
    {
        get
        {
            string text = datewarend;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datewarend = value;
        }
    }
    private string datereg = "";
    [DataMember]
    public string GetSetdatereg
    {
        get
        {
            string text = datereg;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datereg = value;
        }
    }
    private string deprtyp = "";
    [DataMember]
    public string GetSetdeprtyp
    {
        get
        {
            string text = deprtyp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            deprtyp = value;
        }
    }

    private double costreg = 0;
    [DataMember]
    public double GetSetcostreg
    {
        get
        {
            double doubleno = costreg;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            costreg = value;
        }
    }

    private int qtyreg = 0;
    [DataMember]
    public int GetSetqtyreg
    {
        get
        {
            int num = qtyreg;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyreg = value;
        }
    }

    private double deprrate = 0;
    [DataMember]
    public double GetSetdeprrate
    {
        get
        {
            double doubleno = deprrate;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            deprrate = value;
        }
    }
    private double depraccum = 0;
    [DataMember]
    public double GetSetdepraccum
    {
        get
        {
            double doubleno = depraccum;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            depraccum = value;
        }
    }
    private double assetnbv = 0;
    [DataMember]
    public double GetSetassetnbv
    {
        get
        {
            double doubleno = assetnbv;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            assetnbv = value;
        }
    }

    private string tranno = "";
    [DataMember]
    public string GetSettranno
    {
        get
        {
            string text = tranno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            tranno = value;
        }
    }

    private string trancode = "";
    [DataMember]
    public string GetSettrancode
    {
        get
        {
            string text = trancode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            trancode = value;
        }
    }

    private string trancat = "";
    [DataMember]
    public string GetSettrancat
    {
        get
        {
            string text = trancat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            trancat = value;
        }
    }

    private string trandate = "";
    [DataMember]
    public string GetSettrandate
    {
        get
        {
            string text = trandate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            trandate = value;
        }
    }
    private int tranqty = 0;
    [DataMember]
    public int GetSettranqty
    {
        get
        {
            int num = tranqty;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            tranqty = value;
        }
    }
    private double tranvalue = 0;
    [DataMember]
    public double GetSettranvalue
    {
        get
        {
            double doubleno = tranvalue;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            tranvalue = value;
        }
    }

    private string country = "";
    [DataMember]
    public string GetSetcountry
    {
        get
        {
            string text = country;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            country = value;
        }
    }

    private string country_desc = "";
    [DataMember]
    public string GetSetcountry_desc
    {
        get
        {
            string text = country_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            country_desc = value;
        }
    }

    private string state = "";
    [DataMember]
    public string GetSetstate
    {
        get
        {
            string text = state;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            state = value;
        }
    }

    private string state_desc = "";
    [DataMember]
    public string GetSetstate_desc
    {
        get
        {
            string text = state_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            state_desc = value;
        }
    }

    private string district = "";
    [DataMember]
    public string GetSetdistrict
    {
        get
        {
            string text = district;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            district = value;
        }
    }

    private string district_desc = "";
    [DataMember]
    public string GetSetdistrict_desc
    {
        get
        {
            string text = district_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            district_desc = value;
        }
    }

    private string placedate = "";
    [DataMember]
    public string GetSetplacedate
    {
        get
        {
            string text = placedate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            placedate = value;
        }
    }

    private string purpose = "";
    [DataMember]
    public string GetSetpurpose
    {
        get
        {
            string text = purpose;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            purpose = value;
        }
    }

    private string officerid = "";
    [DataMember]
    public string GetSetofficerid
    {
        get
        {
            string text = officerid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            officerid = value;
        }
    }

    private string officername = "";
    [DataMember]
    public string GetSetofficername
    {
        get
        {
            string text = officername;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            officername = value;
        }
    }

    private string contactno = "";
    [DataMember]
    public string GetSetcontactno
    {
        get
        {
            string text = contactno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            contactno = value;
        }
    }

    #endregion/*** END MODEL FOR ASSET ***/

    #region/*** BEGIN MODEL FOR OTHER BP ***/

    private string obpid = "";
    [DataMember]
    public string GetSetobpid
    {
        get
        {
            string text = obpid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpid = value;
        }
    }
    private string obpdesc = "";
    [DataMember]
    public string GetSetobpdesc
    {
        get
        {
            string text = obpdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpdesc = value;
        }
    }
    private string obpaddress = "";
    [DataMember]
    public string GetSetobpaddress
    {
        get
        {
            string text = obpaddress;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpaddress = value;
        }
    }
    private string obpcontact = "";
    [DataMember]
    public string GetSetobpcontact
    {
        get
        {
            string text = obpcontact;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpcontact = value;
        }
    }
    private string obpreference = "";
    [DataMember]
    public string GetSetobpreference
    {
        get
        {
            string text = obpreference;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpreference = value;
        }
    }
    private string obpcat = "";
    [DataMember]
    public string GetSetobpcat
    {
        get
        {
            string text = obpcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpcat = value;
        }
    }
    private string obpstatus = "";
    [DataMember]
    public string GetSetobpstatus
    {
        get
        {
            string text = obpstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            obpstatus = value;
        }
    }

    #endregion/*** END MODEL FOR OTHER BP ***/

    #region/*** BEGIN MODEL FOR RECEIPT***/
    private string receiptno = "";
    [DataMember]
    public string GetSetreceiptno
    {
        get
        {
            string text = receiptno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            receiptno = value;
        }
    }
    private int receipt_lineno = 0;
    [DataMember]
    public int GetSetreceipt_lineno
    {
        get
        {
            int num = receipt_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            receipt_lineno = value;
        }
    }
    private string receiptdate = "";
    [DataMember]
    public string GetSetreceiptdate
    {
        get
        {
            string text = receiptdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            receiptdate = value;
        }
    }
    private string receiptcat = "";
    [DataMember]
    public string GetSetreceiptcat
    {
        get
        {
            string text = receiptcat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            receiptcat = value;
        }
    }

    private int receipt_quantity = 0;
    [DataMember]
    public int GetSetreceipt_quantity
    {
        get
        {
            int num = receipt_quantity;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            receipt_quantity = value;
        }
    }

    private string hasbilling = "";
    [DataMember]
    public string GetSethasbilling
    {
        get
        {
            string text = hasbilling;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            hasbilling = value;
        }
    }

    #endregion/*** END MODEL FOR RECEIPT***/

    #region/*** BEGIN MODEL FOR ADJUSTMENT ORDER ***/

    private string adjustmentno = "";
    [DataMember]
    public string GetSetadjustmentno
    {
        get
        {
            string text = adjustmentno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            adjustmentno = value;
        }
    }
    private string adjustmentdate = "";
    [DataMember]
    public string GetSetadjustmentdate
    {
        get
        {
            string text = adjustmentdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            adjustmentdate = value;
        }
    }
    private string adjustmenttype = "";
    [DataMember]
    public string GetSetadjustmenttype
    {
        get
        {
            string text = adjustmenttype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            adjustmenttype = value;
        }
    }
    private string datesoh = "";
    [DataMember]
    public string GetSetdatesoh
    {
        get
        {
            string text = datesoh;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datesoh = value;
        }
    }
    private int qtyvariance = 0;
    [DataMember]
    public int GetSetqtyvariance
    {
        get
        {
            int num = qtyvariance;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyvariance = value;
        }
    }
    private double pricevariance = 0;
    [DataMember]
    public double GetSetpricevariance
    {
        get
        {
            double doubleno = pricevariance;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            pricevariance = value;
        }
    }
    private int qtyadjusted = 0;
    [DataMember]
    public int GetSetqtyadjusted
    {
        get
        {
            int num = qtyadjusted;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            qtyadjusted = value;
        }
    }
    private double costadjusted = 0;
    [DataMember]
    public double GetSetcostadjusted
    {
        get
        {
            double doubleno = costadjusted;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            costadjusted = value;
        }
    }
    private string transtype = "";
    [DataMember]
    public string GetSettranstype
    {
        get
        {
            string text = transtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            transtype = value;
        }
    }
    private string transflow = "";
    [DataMember]
    public string GetSettransflow
    {
        get
        {
            string text = transflow;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            transflow = value;
        }
    }
    private string transdate = "";
    [DataMember]
    public string GetSettransdate
    {
        get
        {
            string text = transdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            transdate = value;
        }
    }
    private string transno = "";
    [DataMember]
    public string GetSettransno
    {
        get
        {
            string text = transno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            transno = value;
        }
    }
    private int trans_lineno = 0;
    [DataMember]
    public int GetSettrans_lineno
    {
        get
        {
            int num = trans_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            trans_lineno = value;
        }
    }
    private int transqty = 0;
    [DataMember]
    public int GetSettransqty
    {
        get
        {
            int num = transqty;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            transqty = value;
        }
    }
    private double transprice = 0;
    [DataMember]
    public double GetSettransprice
    {
        get
        {
            double doubleno = transprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            transprice = value;
        }
    }

    #endregion/*** END MODEL FOR ADJUSTMENT ORDER ***/

    #region/*** BEGIN MODEL FOR EXPENSES***/

    private string expensesno = "";
    [DataMember]
    public string GetSetexpensesno
    {
        get
        {
            string text = expensesno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            expensesno = value;
        }
    }
    private int expenses_lineno = 0;
    [DataMember]
    public int GetSetexpenses_lineno
    {
        get
        {
            int num = expenses_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            expenses_lineno = value;
        }
    }
    private string expensesdate = "";
    [DataMember]
    public string GetSetexpensesdate
    {
        get
        {
            string text = expensesdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            expensesdate = value;
        }
    }
    private string expensestype = "";
    [DataMember]
    public string GetSetexpensestype
    {
        get
        {
            string text = expensestype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            expensestype = value;
        }
    }
    private string expensescat = "";
    [DataMember]
    public string GetSetexpensescat
    {
        get
        {
            string text = expensescat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            expensescat = value;
        }
    }
    private double expensesamount = 0;
    [DataMember]
    public double GetSetexpensesamount
    {
        get
        {
            double doubleno = expensesamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            expensesamount = value;
        }
    }
    private double expensesprice = 0;
    [DataMember]
    public double GetSetexpensesprice
    {
        get
        {
            double doubleno = expensesprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            expensesprice = value;
        }
    }
    private double totalexpenses = 0;
    [DataMember]
    public double GetSettotalexpenses
    {
        get
        {
            double doubleno = totalexpenses;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalexpenses = value;
        }
    }

    #endregion/*** END MODEL FOR EXPENSES***/

    #region/*** BEGIN MODEL FOR INCOME ***/

    private string incomeno = "";
    [DataMember]
    public string GetSetincomeno
    {
        get
        {
            string text = incomeno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeno = value;
        }
    }
    private int income_lineno = 0;
    [DataMember]
    public int GetSetincome_lineno
    {
        get
        {
            int num = income_lineno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            income_lineno = value;
        }
    }
    private string incomedate = "";
    [DataMember]
    public string GetSetincomedate
    {
        get
        {
            string text = incomedate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomedate = value;
        }
    }
    private string incometype = "";
    [DataMember]
    public string GetSetincometype
    {
        get
        {
            string text = incometype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incometype = value;
        }
    }
    private string incomecat = "";
    [DataMember]
    public string GetSetincomecat
    {
        get
        {
            string text = incomecat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomecat = value;
        }
    }
    private string incomeyear = "";
    [DataMember]
    public string GetSetincomeyear
    {
        get
        {
            string text = incomeyear;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeyear = value;
        }
    }
    private double incomeamount = 0;
    [DataMember]
    public double GetSetincomeamount
    {
        get
        {
            double doubleno = incomeamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            incomeamount = value;
        }
    }
    private double incomeprice = 0;
    [DataMember]
    public double GetSetincomeprice
    {
        get
        {
            double doubleno = incomeprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            incomeprice = value;
        }
    }
    private double totalincome = 0;
    [DataMember]
    public double GetSettotalincome
    {
        get
        {
            double doubleno = totalincome;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalincome = value;
        }
    }
    private string incomestatus = "";
    [DataMember]
    public string GetSetincomestatus
    {
        get
        {
            string text = incomestatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomestatus = value;
        }
    }

    private float incometotal = 0;
    [DataMember]
    public float GetSetincometotal
    {
        get
        {
            float number = incometotal;
            if (number != null)
                return number;
            else
                return 0;
        }
        set
        {
            incometotal = value;
        }
    }

    private string incomeself = "";
    [DataMember]
    public string GetSetincomeself
    {
        get
        {
            string text = incomeself;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeself = value;
        }
    }

    private string incomespouse = "";
    [DataMember]
    public string GetSetincomespouse
    {
        get
        {
            string text = incomespouse;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomespouse = value;
        }
    }

    private string incomedonation = "";
    [DataMember]
    public string GetSetincomedonation
    {
        get
        {
            string text = incomedonation;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomedonation = value;
        }
    }

    private string incomejkm = "";
    [DataMember]
    public string GetSetincomejkm
    {
        get
        {
            string text = incomejkm;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomejkm = value;
        }
    }

    private string incomezakat = "";
    [DataMember]
    public string GetSetincomezakat
    {
        get
        {
            string text = incomezakat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomezakat = value;
        }
    }

    private string incomepencen = "";
    [DataMember]
    public string GetSetincomepencen
    {
        get
        {
            string text = incomepencen;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomepencen = value;
        }
    }

    private string incomebr1m = "";
    [DataMember]
    public string GetSetincomebr1m
    {
        get
        {
            string text = incomebr1m;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomebr1m = value;
        }
    }

    private string incomeperkeso = "";
    [DataMember]
    public string GetSetincomeperkeso
    {
        get
        {
            string text = incomeperkeso;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeperkeso = value;
        }
    }

    private string incomerentbendang = "";
    [DataMember]
    public string GetSetincomerentbendang
    {
        get
        {
            string text = incomerentbendang;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomerentbendang = value;
        }
    }

    private string incomerenthouse = "";
    [DataMember]
    public string GetSetincomerenthouse
    {
        get
        {
            string text = incomerenthouse;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomerenthouse = value;
        }
    }

    private string incomekwapm = "";
    [DataMember]
    public string GetSetincomekwapm
    {
        get
        {
            string text = incomekwapm;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomekwapm = value;
        }
    }

    private string incomeasb = "";
    [DataMember]
    public string GetSetincomeasb
    {
        get
        {
            string text = incomeasb;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeasb = value;
        }
    }

    private string incomeothers = "";
    [DataMember]
    public string GetSetincomeothers
    {
        get
        {
            string text = incomeothers;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            incomeothers = value;
        }
    }

    #endregion/*** END MODEL FOR INCOME***/

    #region/*** BEGIN MODEL FOR PAYMENT PAID ***/

    private string paypaidno = "";
    [DataMember]
    public string GetSetpaypaidno
    {
        get
        {
            string text = paypaidno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paypaidno = value;
        }
    }
    private string paypaiddate = "";
    [DataMember]
    public string GetSetpaypaiddate
    {
        get
        {
            string text = paypaiddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paypaiddate = value;
        }
    }
    private string paypaidtype = "";
    [DataMember]
    public string GetSetpaypaidtype
    {
        get
        {
            string text = paypaidtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paypaidtype = value;
        }
    }
    private double paypaidprice = 0;
    [DataMember]
    public double GetSetpaypaidprice
    {
        get
        {
            double doubleno = paypaidprice;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            paypaidprice = value;
        }
    }
    private double totalpaypaid = 0;
    [DataMember]
    public double GetSettotalpaypaid
    {
        get
        {
            double doubleno = totalpaypaid;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalpaypaid = value;
        }
    }

    #endregion/*** END MODEL FOR PAYMENT PAID ***/

    #region/*** BEGIN MODEL FOR CASH FLOW & STOCK STATEMENT ***/

    private string cashflowno = "";
    [DataMember]
    public string GetSetcashflowno
    {
        get
        {
            string text = cashflowno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cashflowno = value;
        }
    }
    private string stockstateno = "";
    [DataMember]
    public string GetSetstockstateno
    {
        get
        {
            string text = stockstateno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            stockstateno = value;
        }
    }
    private string zakatcalculationno = "";
    [DataMember]
    public string GetSetzakatcalculationno
    {
        get
        {
            string text = zakatcalculationno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            zakatcalculationno = value;
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
    private string openingtype = "";
    [DataMember]
    public string GetSetopeningtype
    {
        get
        {
            string text = openingtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            openingtype = value;
        }
    }
    private double bankopeningamount = 0;
    [DataMember]
    public double GetSetbankopeningamount
    {
        get
        {
            double doubleno = bankopeningamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            bankopeningamount = value;
        }
    }
    private double cashopeningamount = 0;
    [DataMember]
    public double GetSetcashopeningamount
    {
        get
        {
            double doubleno = cashopeningamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            cashopeningamount = value;
        }
    }
    private double stockopeningamount = 0;
    [DataMember]
    public double GetSetstockopeningamount
    {
        get
        {
            double doubleno = stockopeningamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            stockopeningamount = value;
        }
    }
    private double bankpaymentreceiptamount = 0;
    [DataMember]
    public double GetSetbankpaymentreceiptamount
    {
        get
        {
            double doubleno = bankpaymentreceiptamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            bankpaymentreceiptamount = value;
        }
    }
    private double cashpaymentreceiptamount = 0;
    [DataMember]
    public double GetSetcashpaymentreceiptamount
    {
        get
        {
            double doubleno = cashpaymentreceiptamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            cashpaymentreceiptamount = value;
        }
    }
    private double pendingreceiptamount = 0;
    [DataMember]
    public double GetSetpendingreceiptamount
    {
        get
        {
            double doubleno = pendingreceiptamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            pendingreceiptamount = value;
        }
    }
    private double stockinamount = 0;
    [DataMember]
    public double GetSetstockinamount
    {
        get
        {
            double doubleno = stockinamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            stockinamount = value;
        }
    }
    private double bankpaymentpaidamount = 0;
    [DataMember]
    public double GetSetbankpaymentpaidamount
    {
        get
        {
            double doubleno = bankpaymentpaidamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            bankpaymentpaidamount = value;
        }
    }
    private double cashpaymentpaidamount = 0;
    [DataMember]
    public double GetSetcashpaymentpaidamount
    {
        get
        {
            double doubleno = cashpaymentpaidamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            cashpaymentpaidamount = value;
        }
    }
    private double pendingpaidamount = 0;
    [DataMember]
    public double GetSetpendingpaidamount
    {
        get
        {
            double doubleno = pendingpaidamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            pendingpaidamount = value;
        }
    }
    private double stockoutamount = 0;
    [DataMember]
    public double GetSetstockoutamount
    {
        get
        {
            double doubleno = stockoutamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            stockoutamount = value;
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
    private string closingtype = "";
    [DataMember]
    public string GetSetclosingtype
    {
        get
        {
            string text = closingtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            closingtype = value;
        }
    }
    private double bankclosingamount = 0;
    [DataMember]
    public double GetSetbankclosingamount
    {
        get
        {
            double doubleno = bankclosingamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            bankclosingamount = value;
        }
    }
    private double cashclosingamount = 0;
    [DataMember]
    public double GetSetcashclosingamount
    {
        get
        {
            double doubleno = cashclosingamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            cashclosingamount = value;
        }
    }
    private double stockclosingamount = 0;
    [DataMember]
    public double GetSetstockclosingamount
    {
        get
        {
            double doubleno = stockclosingamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            stockclosingamount = value;
        }
    }
    private double adjustmentadditionamount = 0;
    [DataMember]
    public double GetSetadjustmentadditionamount
    {
        get
        {
            double doubleno = adjustmentadditionamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            adjustmentadditionamount = value;
        }
    }
    private double adjustmentsubtractionamount = 0;
    [DataMember]
    public double GetSetadjustmentsubtractionamount
    {
        get
        {
            double doubleno = adjustmentsubtractionamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            adjustmentsubtractionamount = value;
        }
    }
    private double zakatnisabamount = 0;
    [DataMember]
    public double GetSetzakatnisabamount
    {
        get
        {
            double doubleno = zakatnisabamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            zakatnisabamount = value;
        }
    }
    private double zakatrate = 0;
    [DataMember]
    public double GetSetzakatrate
    {
        get
        {
            double doubleno = zakatrate;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            zakatrate = value;
        }
    }
    private double sharepercentage = 0;
    [DataMember]
    public double GetSetsharepercentage
    {
        get
        {
            double doubleno = sharepercentage;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            sharepercentage = value;
        }
    }

    private double totalamountforzakat1 = 0;
    [DataMember]
    public double GetSettotalamountforzakat1
    {
        get
        {
            double doubleno = totalamountforzakat1;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalamountforzakat1 = value;
        }
    }
    private double totalamountforzakat2 = 0;
    [DataMember]
    public double GetSettotalamountforzakat2
    {
        get
        {
            double doubleno = totalamountforzakat2;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalamountforzakat2 = value;
        }
    }
    private double totalamountforzakat3 = 0;
    [DataMember]
    public double GetSettotalamountforzakat3
    {
        get
        {
            double doubleno = totalamountforzakat3;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalamountforzakat3 = value;
        }
    }
    private double totalamountpayzakat = 0;
    [DataMember]
    public double GetSettotalamountpayzakat
    {
        get
        {
            double doubleno = totalamountpayzakat;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            totalamountpayzakat = value;
        }
    }

    private string cashflowtype = "";
    [DataMember]
    public string GetSetcashflowtype
    {
        get
        {
            string text = cashflowtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cashflowtype = value;
        }
    }

    private string stockstatetype = "";
    [DataMember]
    public string GetSetstockstatetype
    {
        get
        {
            string text = stockstatetype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            stockstatetype = value;
        }
    }

    private string paymentno = "";
    [DataMember]
    public string GetSetpaymentno
    {
        get
        {
            string text = paymentno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paymentno = value;
        }
    }
    private string paymentdate = "";
    [DataMember]
    public string GetSetpaymentdate
    {
        get
        {
            string text = paymentdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paymentdate = value;
        }
    }
    private string paymentconfirmeddate = "";
    [DataMember]
    public string GetSetpaymentconfirmeddate
    {
        get
        {
            string text = paymentconfirmeddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paymentconfirmeddate = value;
        }
    }
    private string paymenttype = "";
    [DataMember]
    public string GetSetpaymenttype
    {
        get
        {
            string text = paymenttype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paymenttype = value;
        }
    }
    private string paydetno = "";
    [DataMember]
    public string GetSetpaydetno
    {
        get
        {
            string text = paydetno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paydetno = value;
        }
    }
    private double payamount = 0;
    [DataMember]
    public double GetSetpayamount
    {
        get
        {
            double doubleno = payamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            payamount = value;
        }
    }

    #endregion/*** END MODEL FOR CASH FLOW ***/

    #region /*** BEGIN USER ROLE ***/

    private string moduleid = "";
    [DataMember]
    public string GetSetmoduleid
    {
        get
        {
            string text = moduleid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            moduleid = value;
        }
    }

    private string modulename = "";
    [DataMember]
    public string GetSetmodulename
    {
        get
        {
            string text = modulename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            modulename = value;
        }
    }

    private string moduledesc = "";
    [DataMember]
    public string GetSetmoduledesc
    {
        get
        {
            string text = moduledesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            moduledesc = value;
        }
    }

    private string modulestatus = "";
    [DataMember]
    public string GetSetmodulestatus
    {
        get
        {
            string text = modulestatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            modulestatus = value;
        }
    }

    private string moduleicon = "";
    [DataMember]
    public string GetSetmoduleicon
    {
        get
        {
            string text = moduleicon;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            moduleicon = value;
        }
    }

    private string submoduleid = "";
    [DataMember]
    public string GetSetsubmoduleid
    {
        get
        {
            string text = submoduleid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            submoduleid = value;
        }
    }

    private string submodulename = "";
    [DataMember]
    public string GetSetsubmodulename
    {
        get
        {
            string text = submodulename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            submodulename = value;
        }
    }

    private string submoduledesc = "";
    [DataMember]
    public string GetSetsubmoduledesc
    {
        get
        {
            string text = submoduledesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            submoduledesc = value;
        }
    }

    private string submodulestatus = "";
    [DataMember]
    public string GetSetsubmodulestatus
    {
        get
        {
            string text = submodulestatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            submodulestatus = value;
        }
    }

    private string roleid = "";
    [DataMember]
    public string GetSetroleid
    {
        get
        {
            string text = roleid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            roleid = value;
        }
    }

    private string rolename = "";
    [DataMember]
    public string GetSetrolename
    {
        get
        {
            string text = rolename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rolename = value;
        }
    }

    private string roledesc = "";
    [DataMember]
    public string GetSetroledesc
    {
        get
        {
            string text = roledesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            roledesc = value;
        }
    }

    private string rolestatus = "";
    [DataMember]
    public string GetSetrolestatus
    {
        get
        {
            string text = rolestatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rolestatus = value;
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

    private string screenfilename = "";
    [DataMember]
    public string GetSetscreenfilename
    {
        get
        {
            string text = screenfilename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            screenfilename = value;
        }
    }

    private string screendesc = "";
    [DataMember]
    public string GetSetscreendesc
    {
        get
        {
            string text = screendesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            screendesc = value;
        }
    }

    private string screenstatus = "";
    [DataMember]
    public string GetSetscreenstatus
    {
        get
        {
            string text = screenstatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            screenstatus = value;
        }
    }

    private string useraccess = "";
    [DataMember]
    public string GetSetuseraccess
    {
        get
        {
            string text = useraccess;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            useraccess = value;
        }
    }

    #endregion

    #region/*** BEGIN INFO COMP ***/

    private string info_no = "";
    [DataMember]
    public string GetSetinfo_no
    {
        get
        {
            string text = info_no;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            info_no = value;
        }
    }

    private string info_type = "";
    [DataMember]
    public string GetSetinfo_type
    {
        get
        {
            string text = info_type;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            info_type = value;
        }
    }

    private string info_desc = "";
    [DataMember]
    public string GetSetinfo_desc
    {
        get
        {
            string text = info_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            info_desc = value;
        }
    }

    private string info_status = "";
    [DataMember]
    public string GetSetinfo_status
    {
        get
        {
            string text = info_status;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            info_status = value;
        }
    }
    #endregion/*** END INFO COMP ***/

    #region LOCATION
    private string endlocation = "";
    [DataMember]
    public string GetSetendlocation
    {
        get
        {
            string text = endlocation;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            endlocation = value;
        }
    }

    private int distance = 0;
    [DataMember]
    public int GetSetdistance
    {
        get
        {
            int integer = distance;
            if (integer != 0)
                return integer;
            else
                return 0;
        }
        set
        {
            distance = value;
        }
    }

    private string locationid = "";
    [DataMember]
    public string GetSetlocationid
    {
        get
        {
            string text = locationid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            locationid = value;
        }
    }

    private string locationname = "";
    [DataMember]
    public string GetSetlocationname
    {
        get
        {
            string text = locationname;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            locationname = value;
        }
    }

    private double locationlatitude = 0;
    [DataMember]
    public double GetSetlocationlatitude
    {
        get
        {
            double doubleno = locationlatitude;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            locationlatitude = value;
        }
    }

    private double locationlongitude = 0;
    [DataMember]
    public double GetSetlocationlongitude
    {
        get
        {
            double doubleno = locationlongitude;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            locationlongitude = value;
        }
    }

    private int routelocationseq = 0;
    [DataMember]
    public int GetSetroutelocationseq
    {
        get
        {
            int integer = routelocationseq;
            if (integer != 0)
                return integer;
            else
                return 0;
        }
        set
        {
            routelocationseq = value;
        }
    }

    private string priceid = "";
    [DataMember]
    public string GetSetpriceid
    {
        get
        {
            string text = priceid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            priceid = value;
        }
    }

    private string pricetype = "";
    [DataMember]
    public string GetSetpricetype
    {
        get
        {
            string text = pricetype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pricetype = value;
        }
    }

    private double priceamt = 0;
    [DataMember]
    public double GetSetpriceamt
    {
        get
        {
            double decimalno = priceamt;
            if (decimalno != 0)
                return decimalno;
            else
                return 0;
        }
        set
        {
            priceamt = value;
        }
    }

    //model for bus trip information
    private string tripid = "";
    [DataMember]
    public string GetSettripid
    {
        get
        {
            string text = tripid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            tripid = value;
        }
    }

    private string driverid = "";
    [DataMember]
    public string GetSetdriverid
    {
        get
        {
            string text = driverid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            driverid = value;
        }
    }

    private string begindatetime = "";
    [DataMember]
    public string GetSetbegindatetime
    {
        get
        {
            string text = begindatetime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            begindatetime = value;
        }
    }

    private string enddatetime = "";
    [DataMember]
    public string GetSetenddatetime
    {
        get
        {
            string text = enddatetime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            enddatetime = value;
        }
    }

    private int sessionno = 0;
    [DataMember]
    public int GetSetsessionno
    {
        get
        {
            int num = sessionno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            sessionno = value;
        }
    }

    private double collection = 0;
    [DataMember]
    public double GetSetcollection
    {
        get
        {
            double dou = collection;
            if (dou != 0)
                return dou;
            else
                return 0;
        }
        set
        {
            collection = value;
        }
    }

    private string ticketid = "";
    [DataMember]
    public string GetSetticketid
    {
        get
        {
            string text = ticketid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ticketid = value;
        }
    }

    private int ticketno = 0;
    [DataMember]
    public int GetSetticketno
    {
        get
        {
            int num = ticketno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            ticketno = value;
        }
    }

    private string tickettype = "";
    [DataMember]
    public string GetSettickettype
    {
        get
        {
            string text = tickettype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            tickettype = value;
        }
    }

    private double ticketprice = 0;
    [DataMember]
    public double GetSetticketprice
    {
        get
        {
            double dou = ticketprice;
            if (dou != 0)
                return dou;
            else
                return 0;
        }
        set
        {
            ticketprice = value;
        }
    }

    private string ticketdatetime = "";
    [DataMember]
    public string GetSetticketdatetime
    {
        get
        {
            string text = ticketdatetime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ticketdatetime = value;
        }
    }

    private GeoCoordinate GEO_LOC_FROM = null;
    [DataMember]
    public GeoCoordinate GetSetGOE_LOC_FROM
    {
        get
        {
            GeoCoordinate geo_loc = GEO_LOC_FROM;
            if (geo_loc != null)
                return geo_loc;
            else
                return null;
        }
        set
        {
            GEO_LOC_FROM = value;
        }
    }

    private GeoCoordinate GEO_LOC_TO = null;
    [DataMember]
    public GeoCoordinate GetSetGOE_LOC_TO
    {
        get
        {
            GeoCoordinate geo_loc = GEO_LOC_TO;
            if (geo_loc != null)
                return geo_loc;
            else
                return null;
        }
        set
        {
            GEO_LOC_TO = value;
        }
    }
    #endregion

    #region/*Nested Array*/

    private ArrayList InvoiceDetails = new ArrayList();
    public ArrayList GetSetInvoiceDetails
    {
        get
        {
            ArrayList text = InvoiceDetails;
            if (text != null)
                return text;
            else
                return new ArrayList();
        }
        set
        {
            InvoiceDetails = value;
        }
    }

    private ArrayList ExpensesDetails = new ArrayList();
    public ArrayList GetSetExpensesDetails
    {
        get
        {
            ArrayList text = ExpensesDetails;
            if (text != null)
                return text;
            else
                return new ArrayList();
        }
        set
        {
            ExpensesDetails = value;
        }
    }

    private ArrayList PaymentDetails = new ArrayList();
    public ArrayList GetSetPaymentDetails
    {
        get
        {
            ArrayList text = PaymentDetails;
            if (text != null)
                return text;
            else
                return new ArrayList();
        }
        set
        {
            PaymentDetails = value;
        }
    }

    #endregion
    /*
    private ArrayList LINK_TO = new ArrayList();
    [DataMember]
    public ArrayList GetSetLINK_TO
    {
        get
        {
            ArrayList text = LINK_TO;
            if (text != null)
                return text;
            else
                return new ArrayList();
        }
        set
        {
            LINK_TO = value;
        }
    }
    */
    
}