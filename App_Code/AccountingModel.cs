using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for AccountingModel
/// </summary>
/// 
[Serializable]
public class AccountingModel
{
    public AccountingModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region/*** BEGIN MODEL FOR COA***/

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


    private string accid = "";
    [DataMember]
    public string GetSetaccid
    {
        get
        {
            string text = accid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accid = value;
        }
    }


    private string accdesc = "";
    [DataMember]
    public string GetSetaccdesc
    {
        get
        {
            string text = accdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accdesc = value;
        }
    }


    private string parentid = "";
    [DataMember]
    public string GetSetparentid
    {
        get
        {
            string text = parentid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            parentid = value;
        }
    }

    private string parentdesc = "";
    [DataMember]
    public string GetSetparentdesc
    {
        get
        {
            string text = parentdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            parentdesc = value;
        }
    }

    private string accgroup = "";
    [DataMember]
    public string GetSetaccgroup
    {
        get
        {
            string text = accgroup;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accgroup = value;
        }
    }

    private int acclevel = 0;
    [DataMember]
    public int GetSetacclevel
    {
        get
        {
            int num = acclevel;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            acclevel = value;
        }
    }
    private string endlevel = "";
    [DataMember]
    public string GetSetendlevel
    {
        get
        {
            string text = endlevel;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            endlevel = value;
        }
    }

    private string acctype = "";
    [DataMember]
    public string GetSetacctype
    {
        get
        {
            string text = acctype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            acctype = value;
        }
    }

    private string acccategory = "";
    [DataMember]
    public string GetSetacccategory
    {
        get
        {
            string text = acccategory;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            acccategory = value;
        }
    }

    private string acccode = "";
    [DataMember]
    public string GetSetacccode
    {
        get
        {
            string text = acccode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            acccode = value;
        }
    }


    private string accnumber = "";
    [DataMember]
    public string GetSetaccnumber
    {
        get
        {
            string text = accnumber;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            accnumber = value;
        }
    }

    private double debit = 0;
    [DataMember]
    public double GetSetdebit
    {
        get
        {
            double doubleno = debit;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            debit = value;
        }
    }

    private double credit = 0;
    [DataMember]
    public double GetSetcredit
    {
        get
        {
            double doubleno = credit;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            credit = value;
        }
    }

    private double currdebit = 0;
    [DataMember]
    public double GetSetcurrdebit
    {
        get
        {
            double doubleno = currdebit;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            currdebit = value;
        }
    }

    private double currcredit = 0;
    [DataMember]
    public double GetSetcurrcredit
    {
        get
        {
            double doubleno = currcredit;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            currcredit = value;
        }
    }

    private int lasttranno = 0;
    [DataMember]
    public int GetSetlasttranno
    {
        get
        {
            int num = lasttranno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            lasttranno = value;
        }
    }

    private string lasttrancode = "";
    [DataMember]
    public string GetSetlasttrancode
    {
        get
        {
            string text = lasttrancode;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            lasttrancode = value;
        }
    }

    private string haschecked = "";
    [DataMember]
    public string GetSethaschecked
    {
        get
        {
            string text = haschecked;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            haschecked = value;
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

    #endregion
    #region/*** BEGIN MODEL FOR BANK***/

    private string bankid = "";
    [DataMember]
    public string GetSetbankid
    {
        get
        {
            string text = bankid;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            bankid = value;
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

    private string contact = "";
    [DataMember]
    public string GetSetcontact
    {
        get
        {
            string text = contact;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            contact = value;
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

    private string currency = "";
    [DataMember]
    public string GetSetcurrency
    {
        get
        {
            string text = currency;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            currency = value;
        }
    }

    private double exrate = 0;
    [DataMember]
    public double GetSetexrate
    {
        get
        {
            double doubleno = exrate;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            exrate = value;
        }
    }

    #endregion
    #region/*** BEGIN MODEL FOR CUSTOMER & SUPPLIER***/
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

    #endregion

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
    #endregion

    #region/*** BEGIN MODEL FOR ITEM***/
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
    private string assetcatdesc = "";
    [DataMember]
    public string GetSetassetcatdesc
    {
        get
        {
            string text = assetcatdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assetcatdesc = value;
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
    private string assettypdesc = "";
    [DataMember]
    public string GetSetassettypdesc
    {
        get
        {
            string text = assettypdesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            assettypdesc = value;
        }
    }
    #endregion

    #region/*** BEGIN MODEL FOR OPENING & CLOSING BALANCE***/

    private string baltype = "";
    [DataMember]
    public string GetSetbaltype
    {
        get
        {
            string text = baltype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baltype = value;
        }
    }

    private string baldatetime = "";
    [DataMember]
    public string GetSetbaldatetime
    {
        get
        {
            string text = baldatetime;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baldatetime = value;
        }
    }

    private string baldesc = "";
    [DataMember]
    public string GetSetbaldesc
    {
        get
        {
            string text = baldesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            baldesc = value;
        }
    }

    private Int64 balid = 0;
    [DataMember]
    public Int64 GetSetbalid
    {
        get
        {
            Int64 num = balid;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            balid = value;
        }
    }

    private int tranno = 0;
    [DataMember]
    public int GetSettranno
    {
        get
        {
            int num = tranno;
            if (num != 0)
                return num;
            else
                return 0;
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

    private string trantype = "";
    [DataMember]
    public string GetSettrantype
    {
        get
        {
            string text = trantype;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            trantype = value;
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

    private string datefrom = "";
    [DataMember]
    public string GetSetdatefrom
    {
        get
        {
            string text = datefrom;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            datefrom = value;
        }
    }

    private string dateto = "";
    [DataMember]
    public string GetSetdateto
    {
        get
        {
            string text = dateto;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            dateto = value;
        }
    }

    private double tranamount = 0;
    [DataMember]
    public double GetSettranamount
    {
        get
        {
            double doubleno = tranamount;
            if (doubleno != 0)
                return doubleno;
            else
                return 0;
        }
        set
        {
            tranamount = value;
        }
    }

    private string trandesc = "";
    [DataMember]
    public string GetSettrandesc
    {
        get
        {
            string text = trandesc;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            trandesc = value;
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

    private string refno = "";
    [DataMember]
    public string GetSetrefno
    {
        get
        {
            string text = refno;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            refno = value;
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

    #endregion

    #region/*** BEGIN MODEL FOR LEDGER***/

    private string ledgerdate = "";
    [DataMember]
    public string GetSetledgerdate
    {
        get
        {
            string text = ledgerdate;
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ledgerdate = value;
        }
    }

    private int ledgerno = 0;
    [DataMember]
    public int GetSetledgerno
    {
        get
        {
            int num = ledgerno;
            if (num != 0)
                return num;
            else
                return 0;
        }
        set
        {
            ledgerno = value;
        }
    }

    #endregion

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

    private Int64 runno = 0;
    [DataMember]
    public Int64 GetSetrunno
    {
        get
        {
            Int64 num = runno;
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

}