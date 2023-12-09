using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for HRModel
/// </summary>
public class HRModel
{
    public HRModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region/*** BEGIN MODEL FOR GENERAL***/

    private Int64 id = 0;
    [DataMember]
    public Int64 GetSetid
    {
        get
        {
            Int64 num = id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            id = value;
        }
    }

    private Int64 wg_id = 0;
    [DataMember]
    public Int64 GetSetwg_id
    {
        get
        {
            Int64 num = wg_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            wg_id = value;
        }
    }

    private Int64 wd_id = 0;
    [DataMember]
    public Int64 GetSetwd_id
    {
        get
        {
            Int64 num = wd_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            wd_id = value;
        }
    }

    private Int64 exc_id = 0;
    [DataMember]
    public Int64 GetSetexc_id
    {
        get
        {
            Int64 num = exc_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            exc_id = value;
        }
    }

    private Int64 lg_id = 0;
    [DataMember]
    public Int64 GetSetlg_id
    {
        get
        {
            Int64 num = lg_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            lg_id = value;
        }
    }

    private int count = 0;
    [DataMember]
    public int GetSetcount
    {
        get
        {
            int num = count;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            count = value;
        }
    }

    private int brought = 0;
    [DataMember]
    public int GetSetbrought
    {
        get
        {
            int num = brought;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            brought = value;
        }
    }

    private int taken = 0;
    [DataMember]
    public int GetSettaken
    {
        get
        {
            int num = taken;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            taken = value;
        }
    }

    private int variety = 0;
    [DataMember]
    public int GetSetvariety
    {
        get
        {
            int num = variety;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            variety = value;
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

    private string code = "";
    [DataMember]
    public string GetSetcode
    {
        get
        {
            string text = code;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            code = value;
        }
    }

    private string desc = "";
    [DataMember]
    public string GetSetdesc
    {
        get
        {
            string text = desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            desc = value;
        }
    }

    private string sid = "";
    [DataMember]
    public string GetSetsid
    {
        get
        {
            string text = sid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            sid = value;
        }
    }

    /*
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
    */

    private int level = 0;
    [DataMember]
    public int GetSetlevel
    {
        get
        {
            int num = level;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            level = value;
        }
    }

    private string reportto = "";
    [DataMember]
    public string GetSetreportto
    {
        get
        {
            string text = reportto;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            reportto = value;
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

    private string reason = "";
    [DataMember]
    public string GetSetreason
    {
        get
        {
            string text = reason;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            reason = value;
        }
    }

    private string filename1 = "";
    [DataMember]
    public string GetSetfilename1
    {
        get
        {
            string text = filename1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            filename1 = value;
        }
    }

    private string filename2 = "";
    [DataMember]
    public string GetSetfilename2
    {
        get
        {
            string text = filename2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            filename2 = value;
        }
    }

    private string filename3 = "";
    [DataMember]
    public string GetSetfilename3
    {
        get
        {
            string text = filename3;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            filename3 = value;
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

    private string reliefedby = "";
    [DataMember]
    public string GetSetreliefedby
    {
        get
        {
            string text = reliefedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            reliefedby = value;
        }
    }
    private string reliefeddate = "";
    [DataMember]
    public string GetSetreliefeddate
    {
        get
        {
            string text = reliefeddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            reliefeddate = value;
        }
    }

    private string verifiedby = "";
    [DataMember]
    public string GetSetverifiedby
    {
        get
        {
            string text = verifiedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            verifiedby = value;
        }
    }
    private string verifieddate = "";
    [DataMember]
    public string GetSetverifieddate
    {
        get
        {
            string text = verifieddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            verifieddate = value;
        }
    }

    private string approvedby = "";
    [DataMember]
    public string GetSetapprovedby
    {
        get
        {
            string text = approvedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            approvedby = value;
        }
    }
    private string approveddate = "";
    [DataMember]
    public string GetSetapproveddate
    {
        get
        {
            string text = approveddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            approveddate = value;
        }
    }
    private string rejectedby = "";
    [DataMember]
    public string GetSetrejectedby
    {
        get
        {
            string text = rejectedby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rejectedby = value;
        }
    }

    private string rejecteddate = "";
    [DataMember]
    public string GetSetrejecteddate
    {
        get
        {
            string text = rejecteddate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rejecteddate = value;
        }
    }

    private string rundate = "";
    [DataMember]
    public string GetSetrundate
    {
        get
        {
            string text = rundate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rundate = value;
        }
    }

    private string runby = "";
    [DataMember]
    public string GetSetrunby
    {
        get
        {
            string text = runby;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            runby = value;
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

    #endregion/*** END MODEL FOR GENERAL***/

    #region/*** BEGIN INFO STAFF ***/

    private string staffno = "";
    [DataMember]
    public string GetSetstaffno
    {
        get
        {
            string text = staffno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            staffno = value;
        }
    }

    private string salute = "";
    [DataMember]
    public string GetSetsalute
    {
        get
        {
            string text = salute;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            salute = value;
        }
    }

    private string salute_desc = "";
    [DataMember]
    public string GetSetsalute_desc
    {
        get
        {
            string text = salute_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            salute_desc = value;
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

    private string nickname = "";
    [DataMember]
    public string GetSetnickname
    {
        get
        {
            string text = nickname;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            nickname = value;
        }
    }

    private string nicno = "";
    [DataMember]
    public string GetSetnicno
    {
        get
        {
            string text = nicno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            nicno = value;
        }
    }

    private string oicno = "";
    [DataMember]
    public string GetSetoicno
    {
        get
        {
            string text = oicno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            oicno = value;
        }
    }

    private string iccolor = "";
    [DataMember]
    public string GetSeticcolor
    {
        get
        {
            string text = iccolor;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            iccolor = value;
        }
    }

    private string passport = "";
    [DataMember]
    public string GetSetpassport
    {
        get
        {
            string text = passport;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            passport = value;
        }
    }

    private string ppexpiry = "";
    [DataMember]
    public string GetSetppexpiry
    {
        get
        {
            string text = ppexpiry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ppexpiry = value;
        }
    }

    private string str_ppexpiry = "";
    [DataMember]
    public string GetSetstr_ppexpiry
    {
        get
        {
            string text = str_ppexpiry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_ppexpiry = value;
        }
    }

    private string wkpermit = "";
    [DataMember]
    public string GetSetwkpermit
    {
        get
        {
            string text = wkpermit;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            wkpermit = value;
        }
    }

    private string wkexpiry = "";
    [DataMember]
    public string GetSetwkexpiry
    {
        get
        {
            string text = wkexpiry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            wkexpiry = value;
        }
    }

    private string str_wkexpiry = "";
    [DataMember]
    public string GetSetstr_wkexpiry
    {
        get
        {
            string text = str_wkexpiry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_wkexpiry = value;
        }
    }

    private string dob = "";
    [DataMember]
    public string GetSetdob
    {
        get
        {
            string text = dob;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            dob = value;
        }
    }

    private string str_dob = "";
    [DataMember]
    public string GetSetstr_dob
    {
        get
        {
            string text = str_dob;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_dob = value;
        }
    }

    private string birthplace = "";
    [DataMember]
    public string GetSetbirthplace
    {
        get
        {
            string text = birthplace;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            birthplace = value;
        }
    }

    private string marital = "";
    [DataMember]
    public string GetSetmarital
    {
        get
        {
            string text = marital;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            marital = value;
        }
    }

    private string marital_desc = "";
    [DataMember]
    public string GetSetmarital_desc
    {
        get
        {
            string text = marital_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            marital_desc = value;
        }
    }

    private string dtmarried = "";
    [DataMember]
    public string GetSetdtmarried
    {
        get
        {
            string text = dtmarried;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            dtmarried = value;
        }
    }

    private string str_dtmarried = "";
    [DataMember]
    public string GetSetstr_dtmarried
    {
        get
        {
            string text = str_dtmarried;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_dtmarried = value;
        }
    }

    private string bloodtype = "";
    [DataMember]
    public string GetSetbloodtype
    {
        get
        {
            string text = bloodtype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bloodtype = value;
        }
    }

    private string bloodtype_desc = "";
    [DataMember]
    public string GetSetbloodtype_desc
    {
        get
        {
            string text = bloodtype_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bloodtype_desc = value;
        }
    }

    private string race = "";
    [DataMember]
    public string GetSetrace
    {
        get
        {
            string text = race;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            race = value;
        }
    }

    private string race_desc = "";
    [DataMember]
    public string GetSetrace_desc
    {
        get
        {
            string text = race_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            race_desc = value;
        }
    }

    private string religion = "";
    [DataMember]
    public string GetSetreligion
    {
        get
        {
            string text = religion;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            religion = value;
        }
    }

    private string religion_desc = "";
    [DataMember]
    public string GetSetreligion_desc
    {
        get
        {
            string text = religion_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            religion_desc = value;
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

    private string nationality = "";
    [DataMember]
    public string GetSetnationality
    {
        get
        {
            string text = nationality;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            nationality = value;
        }
    }

    private string nationality_desc = "";
    [DataMember]
    public string GetSetnationality_desc
    {
        get
        {
            string text = nationality_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            nationality_desc = value;
        }
    }

    private string bumistatus = "";
    [DataMember]
    public string GetSetbumistatus
    {
        get
        {
            string text = bumistatus;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bumistatus = value;
        }
    }

    private string paddress1 = "";
    [DataMember]
    public string GetSetpaddress1
    {
        get
        {
            string text = paddress1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paddress1 = value;
        }
    }

    private string paddress2 = "";
    [DataMember]
    public string GetSetpaddress2
    {
        get
        {
            string text = paddress2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paddress2 = value;
        }
    }

    private string paddress3 = "";
    [DataMember]
    public string GetSetpaddress3
    {
        get
        {
            string text = paddress3;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paddress3 = value;
        }
    }

    private string paddress4 = "";
    [DataMember]
    public string GetSetpaddress4
    {
        get
        {
            string text = paddress4;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            paddress4 = value;
        }
    }

    private string ppostcode = "";
    [DataMember]
    public string GetSetppostcode
    {
        get
        {
            string text = ppostcode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ppostcode = value;
        }
    }

    private string pcity = "";
    [DataMember]
    public string GetSetpcity
    {
        get
        {
            string text = pcity;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pcity = value;
        }
    }

    private string pcity_desc = "";
    [DataMember]
    public string GetSetpcity_desc
    {
        get
        {
            string text = pcity_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pcity_desc = value;
        }
    }

    private string pstate = "";
    [DataMember]
    public string GetSetpstate
    {
        get
        {
            string text = pstate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pstate = value;
        }
    }

    private string pstate_desc = "";
    [DataMember]
    public string GetSetpstate_desc
    {
        get
        {
            string text = pstate_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pstate_desc = value;
        }
    }

    private string pcountry = "";
    [DataMember]
    public string GetSetpcountry
    {
        get
        {
            string text = pcountry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pcountry = value;
        }
    }

    private string pcountry_desc = "";
    [DataMember]
    public string GetSetpcountry_desc
    {
        get
        {
            string text = pcountry_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pcountry_desc = value;
        }
    }

    private string ptelephone = "";
    [DataMember]
    public string GetSetptelephone
    {
        get
        {
            string text = ptelephone;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ptelephone = value;
        }
    }

    private string caddress1 = "";
    [DataMember]
    public string GetSetcaddress1
    {
        get
        {
            string text = caddress1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            caddress1 = value;
        }
    }

    private string caddress2 = "";
    [DataMember]
    public string GetSetcaddress2
    {
        get
        {
            string text = caddress2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            caddress2 = value;
        }
    }

    private string caddress3 = "";
    [DataMember]
    public string GetSetcaddress3
    {
        get
        {
            string text = caddress3;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            caddress3 = value;
        }
    }

    private string caddress4 = "";
    [DataMember]
    public string GetSetcaddress4
    {
        get
        {
            string text = caddress4;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            caddress4 = value;
        }
    }

    private string cpostcode = "";
    [DataMember]
    public string GetSetcpostcode
    {
        get
        {
            string text = cpostcode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cpostcode = value;
        }
    }

    private string ccity = "";
    [DataMember]
    public string GetSetccity
    {
        get
        {
            string text = ccity;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ccity = value;
        }
    }

    private string ccity_desc = "";
    [DataMember]
    public string GetSetccity_desc
    {
        get
        {
            string text = ccity_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ccity_desc = value;
        }
    }

    private string cstate = "";
    [DataMember]
    public string GetSetcstate
    {
        get
        {
            string text = cstate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cstate = value;
        }
    }

    private string cstate_desc = "";
    [DataMember]
    public string GetSetcstate_desc
    {
        get
        {
            string text = cstate_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cstate_desc = value;
        }
    }

    private string ccountry = "";
    [DataMember]
    public string GetSetccountry
    {
        get
        {
            string text = ccountry;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ccountry = value;
        }
    }

    private string ccountry_desc = "";
    [DataMember]
    public string GetSetccountry_desc
    {
        get
        {
            string text = ccountry_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ccountry_desc = value;
        }
    }

    private string ctelephone = "";
    [DataMember]
    public string GetSetctelephone
    {
        get
        {
            string text = ctelephone;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ctelephone = value;
        }
    }

    private string mobile1 = "";
    [DataMember]
    public string GetSetmobile1
    {
        get
        {
            string text = mobile1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            mobile1 = value;
        }
    }

    private string mobile2 = "";
    [DataMember]
    public string GetSetmobile2
    {
        get
        {
            string text = mobile2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            mobile2 = value;
        }
    }

    private string datejoined = "";
    [DataMember]
    public string GetSetdatejoined
    {
        get
        {
            string text = datejoined;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datejoined = value;
        }
    }

    private string str_datejoined = "";
    [DataMember]
    public string GetSetstr_datejoined
    {
        get
        {
            string text = str_datejoined;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_datejoined = value;
        }
    }

    private int retireage = 0;
    [DataMember]
    public int GetSetretireage
    {
        get
        {
            int num = retireage;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            retireage = value;
        }
    }

    private string retiredate = "";
    [DataMember]
    public string GetSetretiredate
    {
        get
        {
            string text = retiredate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            retiredate = value;
        }
    }

    private string str_retiredate = "";
    [DataMember]
    public string GetSetstr_retiredate
    {
        get
        {
            string text = str_retiredate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_retiredate = value;
        }
    }

    private string email1 = "";
    [DataMember]
    public string GetSetemail1
    {
        get
        {
            string text = email1;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            email1 = value;
        }
    }

    private string email2 = "";
    [DataMember]
    public string GetSetemail2
    {
        get
        {
            string text = email2;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            email2 = value;
        }
    }

    private string facebook = "";
    [DataMember]
    public string GetSetfacebook
    {
        get
        {
            string text = facebook;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            facebook = value;
        }
    }

    private string instagram = "";
    [DataMember]
    public string GetSetinstagram
    {
        get
        {
            string text = instagram;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            instagram = value;
        }
    }

    private string whatsapp = "";
    [DataMember]
    public string GetSetwhatsapp
    {
        get
        {
            string text = whatsapp;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            whatsapp = value;
        }
    }

    private string epfno = "";
    [DataMember]
    public string GetSetepfno
    {
        get
        {
            string text = epfno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            epfno = value;
        }
    }

    private string socsono = "";
    [DataMember]
    public string GetSetsocsono
    {
        get
        {
            string text = socsono;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            socsono = value;
        }
    }

    private string taxno = "";
    [DataMember]
    public string GetSettaxno
    {
        get
        {
            string text = taxno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            taxno = value;
        }
    }

    private string bankname = "";
    [DataMember]
    public string GetSetbankname
    {
        get
        {
            string text = bankname;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bankname = value;
        }
    }

    private string accountype = "";
    [DataMember]
    public string GetSetaccountype
    {
        get
        {
            string text = accountype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accountype = value;
        }
    }

    private string accountno = "";
    [DataMember]
    public string GetSetaccountno
    {
        get
        {
            string text = accountno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accountno = value;
        }
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

    private string password = "";
    [DataMember]
    public string GetSetpassword
    {
        get
        {
            string text = password;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            password = value;
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

    private string usertype_desc = "";
    [DataMember]
    public string GetSetusertype_desc
    {
        get
        {
            string text = usertype_desc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            usertype_desc = value;
        }
    }

    private string lastaccess = "";
    [DataMember]
    public string GetSetlastaccess
    {
        get
        {
            string text = lastaccess;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            lastaccess = value;
        }
    }

    private string str_lastaccess = "";
    [DataMember]
    public string GetSetstr_lastaccess
    {
        get
        {
            string text = str_lastaccess;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_lastaccess = value;
        }
    }

    private string statuslogon = "";
    [DataMember]
    public string GetSetstatuslogon
    {
        get
        {
            string text = statuslogon;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            statuslogon = value;
        }
    }

    #endregion/*** END INFO STAFF ***/

    #region/*** BEGIN STAFF EMPLOYMENT ***/

    private string dept_id = "";
    [DataMember]
    public string GetSetdept_id
    {
        get
        {
            string text = dept_id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            dept_id = value;
        }
    }

    private string dept_name = "";
    [DataMember]
    public string GetSetdept_name
    {
        get
        {
            string text = dept_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            dept_name = value;
        }
    }

    private string gred_id = "";
    [DataMember]
    public string GetSetgred_id
    {
        get
        {
            string text = gred_id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            gred_id = value;
        }
    }

    private string gred_name = "";
    [DataMember]
    public string GetSetgred_name
    {
        get
        {
            string text = gred_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            gred_name = value;
        }
    }

    private string pos_id = "";
    [DataMember]
    public string GetSetpos_id
    {
        get
        {
            string text = pos_id;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_id = value;
        }
    }

    private string pos_name = "";
    [DataMember]
    public string GetSetpos_name
    {
        get
        {
            string text = pos_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            pos_name = value;
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

    private string cat = "";
    [DataMember]
    public string GetSetcat
    {
        get
        {
            string text = cat;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            cat = value;
        }
    }

    private double itemvalue = 0;
    [DataMember]
    public double GetSetitemvalue
    {
        get
        {
            double doubleno = itemvalue;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            itemvalue = value;
        }
    }

    private double itemamount = 0;
    [DataMember]
    public double GetSetitemamount
    {
        get
        {
            double doubleno = itemamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            itemamount = value;
        }
    }

    private int probation = 0;
    [DataMember]
    public int GetSetprobation
    {
        get
        {
            int num = probation;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            probation = value;
        }
    }

    private string fromdate = "";
    [DataMember]
    public string GetSetfromdate
    {
        get
        {
            string text = fromdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            fromdate = value;
        }
    }

    private string str_fromdate = "";
    [DataMember]
    public string GetSetstr_fromdate
    {
        get
        {
            string text = str_fromdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_fromdate = value;
        }
    }

    private string todate = "";
    [DataMember]
    public string GetSettodate
    {
        get
        {
            string text = todate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            todate = value;
        }
    }

    private string str_todate = "";
    [DataMember]
    public string GetSetstr_todate
    {
        get
        {
            string text = str_todate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            str_todate = value;
        }
    }
    #endregion/*** END STAFF EMPLOYMENT***/

    #region/*** BEGIN ATTENDANCE ***/

    private string ph_date = "";
    [DataMember]
    public string GetSetph_date
    {
        get
        {
            string text = ph_date;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ph_date = value;
        }
    }

    private string day_date = "";
    [DataMember]
    public string GetSetday_date
    {
        get
        {
            string text = day_date;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            day_date = value;
        }
    }

    private string day_name = "";
    [DataMember]
    public string GetSetday_name
    {
        get
        {
            string text = day_name;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            day_name = value;
        }
    }

    private string rest_day = "";
    [DataMember]
    public string GetSetrest_day
    {
        get
        {
            string text = rest_day;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            rest_day = value;
        }
    }

    private string fromtime = "";
    [DataMember]
    public string GetSetfromtime
    {
        get
        {
            string text = fromtime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            fromtime = value;
        }
    }

    private string totime = "";
    [DataMember]
    public string GetSettotime
    {
        get
        {
            string text = totime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            totime = value;
        }
    }

    private string timein = "";
    [DataMember]
    public string GetSettimein
    {
        get
        {
            string text = timein;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            timein = value;
        }
    }

    private string timeout = "";
    [DataMember]
    public string GetSettimeout
    {
        get
        {
            string text = timeout;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            timeout = value;
        }
    }

    private string next_day = "";
    [DataMember]
    public string GetSetnext_day
    {
        get
        {
            string text = next_day;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            next_day = value;
        }
    }

    private string follow_previous = "";
    [DataMember]
    public string GetSetfollow_previous
    {
        get
        {
            string text = follow_previous;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            follow_previous = value;
        }
    }

    private string deviceid = "";
    [DataMember]
    public string GetSetdeviceid
    {
        get
        {
            string text = deviceid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            deviceid = value;
        }
    }

    private string devicename = "";
    [DataMember]
    public string GetSetdevicename
    {
        get
        {
            string text = devicename;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            devicename = value;
        }
    }

    private string ipno = "";
    [DataMember]
    public string GetSetipno
    {
        get
        {
            string text = ipno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ipno = value;
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

    #endregion/*** END ATTENDANCE ***/

    #region/*** BEGIN LEAVE ***/

    #endregion/*** END LEAVE ***/

    #region/*** BEGIN SALARY ***/

    private string itemgroup = "";
    [DataMember]
    public string GetSetitemgroup
    {
        get
        {
            string text = itemgroup;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            itemgroup = value;
        }
    }

    private Int64 sg_id = 0;
    [DataMember]
    public Int64 GetSetsg_id
    {
        get
        {
            Int64 num = sg_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            sg_id = value;
        }
    }

    private Int64 ssg_id = 0;
    [DataMember]
    public Int64 GetSetssg_id
    {
        get
        {
            Int64 num = ssg_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            ssg_id = value;
        }
    }

    private Int64 si_id = 0;
    [DataMember]
    public Int64 GetSetsi_id
    {
        get
        {
            Int64 num = si_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            si_id = value;
        }
    }

    private Int64 ss_id = 0;
    [DataMember]
    public Int64 GetSetss_id
    {
        get
        {
            Int64 num = ss_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            ss_id = value;
        }
    }

    private Int64 ssd_id = 0;
    [DataMember]
    public Int64 GetSetssd_id
    {
        get
        {
            Int64 num = ssd_id;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            ssd_id = value;
        }
    }

    private int month = 0;
    [DataMember]
    public int GetSetmonth
    {
        get
        {
            int num = month;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            month = value;
        }
    }

    private int year = 0;
    [DataMember]
    public int GetSetyear
    {
        get
        {
            int num = year;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            year = value;
        }
    }


    #endregion/*** END SALARY ***/
}