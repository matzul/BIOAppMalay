using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for AccountingController
/// </summary>
public class AccountingController
{
    public DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
    public DateTimeFormatInfo ukDtfi = new CultureInfo("en-GB", false).DateTimeFormat;

    private String sErrorLog = "";
    public AccountingController()
    {
        sErrorLog = "";
    }

    public AccountingController(String _sErrorLog)
    {
        sErrorLog = _sErrorLog;
    }

    #region/*** BEGIN FOR COA***/

    public ArrayList getFisCOAMasterAccGroupList(String comp, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT DISTINCT a.accgroup 
                           FROM   fis_coa_master a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOAMasterAccGroupList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOAMasterList(String comp, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_master a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOAMasterList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public AccountingModel getFisCOAMasterDetail(String comp, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_master a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOAMasterDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public AccountingModel getFisCOAMasterDetail(String comp, Int64 id, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_master a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOAMasterDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public int insertFisCOAMaster(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_coa_master(comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, 
                                                    status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?accid, ?accdesc, ?parentid, ?accgroup, ?acclevel, ?endlevel, ?acctype, ?acccat, ?acccode, ?accnumber, 
                                  ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = modItem.GetSetparentid;
                cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = modItem.GetSetaccgroup;
                cmd.Parameters.Add("?acclevel", MySqlDbType.Int16).Value = modItem.GetSetacclevel;
                cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = modItem.GetSetendlevel;
                cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = modItem.GetSetacctype;
                cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = modItem.GetSetacccategory;
                cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = modItem.GetSetacccode;
                cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = modItem.GetSetaccnumber;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisCOAMaster: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisCOAMaster(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_coa_master 
                           SET  accid = ?accid,
                                accdesc = ?accdesc, parentid = ?parentid, 
                                accgroup = ?accgroup, acclevel = ?acclevel, 
                                endlevel = ?endlevel, acctype = ?acctype, 
                                acccat = ?acccat, acccode = ?acccode, 
                                accnumber = ?accnumber, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = modItem.GetSetparentid;
                cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = modItem.GetSetaccgroup;
                cmd.Parameters.Add("?acclevel", MySqlDbType.Int16).Value = modItem.GetSetacclevel;
                cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = modItem.GetSetendlevel;
                cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = modItem.GetSetacctype;
                cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = modItem.GetSetacccategory;
                cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = modItem.GetSetacccode;
                cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = modItem.GetSetaccnumber;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -updateFisCOAMaster: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisCOAMaster(String comp, String fyr, String accid)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_coa_master WHERE comp = ?comp AND accid = ?accid ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisCOAMaster: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public ArrayList getFisCOATranListForClosing(String comp, String fyr, String accid, String parentid, String accgroup, String endlevel, String acctype, String acccat, String accnumber, String starttrandate, String endtrandate, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, sum(b.debit) debit, sum(b.credit) credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a left join fis_ledger b on b.comp = a.comp and b.fyr = a.fyr and b.accid = a.accid and b.ledgerdate >= ?starttrandate and b.ledgerdate <= ?endtrandate and b.status <> 'CANCELLED'
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (endlevel.Trim().Length > 0)
                {
                    query = query + " and  a.endlevel = ?endlevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" group by a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate";
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [SQL] " + query);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [starttrandate] " + starttrandate);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [endtrandate] " + endtrandate);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (endlevel.Trim().Length > 0) cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = endlevel;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                //if (starttrandate.Trim().Length > 0) cmd.Parameters.Add("?starttrandate", MySqlDbType.VarChar).Value = starttrandate;
                if (starttrandate.Length > 0)
                {
                    if (starttrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(starttrandate);
                        DateTime datetime = Convert.ToDateTime(starttrandate, ukDtfi);
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = null;
                }

                //if (endtrandate.Trim().Length > 0) cmd.Parameters.Add("?endtrandate", MySqlDbType.VarChar).Value = endtrandate;
                if (endtrandate.Length > 0)
                {
                    if (endtrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(endtrandate);
                        DateTime datetime = Convert.ToDateTime(endtrandate, ukDtfi);
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = null;
                }

                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranListForClosing: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOATranListWithoutClosing(String comp, String fyr, String accid, String parentid, String accgroup, String endlevel, String acctype, String acccat, String accnumber, String starttrandate, String endtrandate, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, sum(b.debit) debit, sum(b.credit) credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a left join fis_ledger b on b.comp = a.comp and b.fyr = a.fyr and b.accid = a.accid and b.ledgerdate >= ?starttrandate and b.ledgerdate <= ?endtrandate and b.status <> 'CANCELLED' and b.trancode <> 'CLOSING_BALANCE'
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (endlevel.Trim().Length > 0)
                {
                    query = query + " and  a.endlevel = ?endlevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" group by a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate";
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [SQL] " + query);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [starttrandate] " + starttrandate);
                //WriteToLogFile("AccountingController-getFisCOATranListForClosing: [endtrandate] " + endtrandate);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (endlevel.Trim().Length > 0) cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = endlevel;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                //if (starttrandate.Trim().Length > 0) cmd.Parameters.Add("?starttrandate", MySqlDbType.VarChar).Value = starttrandate;
                if (starttrandate.Length > 0)
                {
                    if (starttrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(starttrandate);
                        DateTime datetime = Convert.ToDateTime(starttrandate, ukDtfi);
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = null;
                }

                //if (endtrandate.Trim().Length > 0) cmd.Parameters.Add("?endtrandate", MySqlDbType.VarChar).Value = endtrandate;
                if (endtrandate.Length > 0)
                {
                    if (endtrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(endtrandate);
                        DateTime datetime = Convert.ToDateTime(endtrandate, ukDtfi);
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = null;
                }

                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranListForClosing: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOATranListWithoutClosingDualReport(String comp, String fyr, String accid, String parentid, String accgroup, String endlevel, String acctype, String acccat, String accnumber, String starttrandate, String endtrandate, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, sum(b.debit) debit, sum(b.credit) credit, 
                                  (select sum(c.debit) from fis_ledger c where c.comp = a.comp and c.fyr = a.fyr and c.accid = a.accid and c.ledgerdate >= ?starttrandate and c.ledgerdate <= ?endtrandate and c.status <> 'CANCELLED' and c.trancode <> 'CLOSING_BALANCE' and c.trancode <> 'OPENING_BALANCE') currdebit, 
                                  (select sum(c.credit) from fis_ledger c where c.comp = a.comp and c.fyr = a.fyr and c.accid = a.accid and c.ledgerdate >= ?starttrandate and c.ledgerdate <= ?endtrandate and c.status <> 'CANCELLED' and c.trancode <> 'CLOSING_BALANCE' and c.trancode <> 'OPENING_BALANCE') currcredit, 
                                  a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a left join fis_ledger b on b.comp = a.comp and b.fyr = a.fyr and b.accid = a.accid and b.ledgerdate >= ?starttrandate and b.ledgerdate <= ?endtrandate and b.status <> 'CANCELLED' and b.trancode <> 'CLOSING_BALANCE'
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (endlevel.Trim().Length > 0)
                {
                    query = query + " and  a.endlevel = ?endlevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" group by a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate";
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                //WriteToLogFile("AccountingController-getFisCOATranListWithoutClosingDualReport: [SQL] " + query);
                //WriteToLogFile("AccountingController-getFisCOATranListWithoutClosingDualReport: [starttrandate] " + starttrandate);
                //WriteToLogFile("AccountingController-getFisCOATranListWithoutClosingDualReport: [endtrandate] " + endtrandate);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (endlevel.Trim().Length > 0) cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = endlevel;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                //if (starttrandate.Trim().Length > 0) cmd.Parameters.Add("?starttrandate", MySqlDbType.VarChar).Value = starttrandate;
                if (starttrandate.Length > 0)
                {
                    if (starttrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(starttrandate);
                        DateTime datetime = Convert.ToDateTime(starttrandate, ukDtfi);
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = null;
                }

                //if (endtrandate.Trim().Length > 0) cmd.Parameters.Add("?endtrandate", MySqlDbType.VarChar).Value = endtrandate;
                if (endtrandate.Length > 0)
                {
                    if (endtrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(endtrandate);
                        DateTime datetime = Convert.ToDateTime(endtrandate, ukDtfi);
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = null;
                }

                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetcurrdebit = replaceDoubleZero(dataReader, "currdebit");
                    modItem.GetSetcurrcredit = replaceDoubleZero(dataReader, "currcredit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranListWithoutClosingDualReport: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOATranList(String comp, String fyr, String accid, String parentid, String accgroup, String endlevel, String acctype, String acccat, String accnumber, String lasttrancode, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (endlevel.Trim().Length > 0)
                {
                    query = query + " and  a.endlevel = ?endlevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (endlevel.Trim().Length > 0) cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = endlevel;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOATranList(String comp, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String lasttrancode, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisCOATranList(String comp, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String acccode, String accnumber, String lasttrancode, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (acccode.Trim().Length > 0)
                {
                    query = query + " and  a.acccode = ?acccode ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (acccode.Trim().Length > 0) cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = acccode;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList getFisTOPCOATranList(String comp, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String acccode, String accnumber, String lasttrancode, String status)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > -1)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (acccode.Trim().Length > 0)
                {
                    query = query + " and  a.acccode = ?acccode ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (acccode.Trim().Length > 0) cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = acccode;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                cmd.CommandTimeout = 60;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public AccountingModel getFisCOATranDetail(String comp, String fyr, String accid)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid like '%" + accid + "%' ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranDetail: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modItem;
    }

    public AccountingModel getFisCOATranDetail(String comp, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String lasttrancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranDetail: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modItem;
    }

    public AccountingModel getFisCOATranDetail(String comp, Int64 id, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String lasttrancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, a.debit, a.credit, a.lasttranno, a.lasttrancode, 
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  a.lasttrancode = ?lasttrancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, a.acclevel ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                cmd.CommandTimeout = 60;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOATranDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public ArrayList getFisCOAMasterTranList(String comp, String fyr, String accid, String parentid, String accgroup, int acclevel, String acctype, String acccat, String accnumber, String lasttrancode, String status, String option)
    {
        ArrayList lsFisCOATran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, b.fyr, a.accid, a.accdesc, a.parentid, a.accgroup, a.acclevel, a.endlevel, a.acctype,
                                  a.acccat, a.acccode, a.accnumber, b.debit, b.credit, b.lasttranno, b.lasttrancode, CASE WHEN b.accdesc IS NOT NULL THEN 1 ELSE 0 END as haschecked,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_coa_master a " + (option.Equals("INNER") ? "INNER JOIN" : option.Equals("LEFT") ? "LEFT OUTER JOIN" : option.Equals("RIGHT") ? "RIGHT OUTER JOIN" : option.Equals("ONLY") ? "LEFT OUTER JOIN" : "LEFT OUTER JOIN") + @" fis_coa_tran b on a.comp = b.comp and a.accid = b.accid " + (fyr.Trim().Length > 0 ? "and b.fyr = ?fyr" : "") + @"
                           WHERE  a.comp is not null " + (option.Equals("ONLY") ? "and b.accdesc is null " : "");
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  (b.fyr = ?fyr OR b.fyr is null)";
                }
                if (parentid.Trim().Length > 0)
                {
                    query = query + " and  a.parentid = ?parentid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accgroup.Trim().Length > 0)
                {
                    query = query + " and  a.accgroup = ?accgroup ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = " + acclevel + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (acccat.Trim().Length > 0)
                {
                    query = query + " and  a.acccat = ?acccat ";
                }
                if (accnumber.Trim().Length > 0)
                {
                    query = query + " and  a.accnumber = ?accnumber ";
                }
                if (lasttrancode.Trim().Length > 0)
                {
                    query = query + " and  (b.lasttrancode = ?lasttrancode OR b.lasttrancode is null) ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.accid, a.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOAMasterList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (parentid.Trim().Length > 0) cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = parentid;
                if (accgroup.Trim().Length > 0) cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = accgroup;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (acccat.Trim().Length > 0) cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = acccat;
                if (accnumber.Trim().Length > 0) cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = accnumber;
                if (lasttrancode.Trim().Length > 0) cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = lasttrancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSethaschecked = replaceNull(dataReader, "haschecked");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisCOATran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOAMasterList: " + e.Message.ToString());
        }
        return lsFisCOATran;
    }

    public ArrayList searchFisCOATranList(ArrayList lsFisCOATranAccId, String comp, String fyr, String accid, String acctype, String acccat, String accnumber, String status)
    {
        try
        {
            ArrayList lsFisCOATran = getFisCOATranList(comp, fyr, accid, "", "", 0, acctype, acccat, accnumber, "", status);
            for (int i = 0; i < lsFisCOATran.Count; i++)
            {
                AccountingModel modItem = (AccountingModel)lsFisCOATran[i];
                if (isBoolContains(lsFisCOATranAccId, modItem.GetSetaccid) == false)
                {
                    lsFisCOATranAccId.Add(modItem.GetSetaccid);
                    for (int x = modItem.GetSetacclevel; x > 0; x--)
                    {
                        lsFisCOATranAccId = searchFisCOATranList(lsFisCOATranAccId, modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, "", "", "", "");
                    }
                }
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-searchFisCOATranList: " + e.Message.ToString());
        }
        return lsFisCOATranAccId;
    }

    public int insertFisCOATran(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_coa_tran(comp, fyr, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, 
                                                    debit, credit, lasttranno, lasttrancode, status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?fyr, ?accid, ?accdesc, ?parentid, ?accgroup, ?acclevel, ?endlevel, ?acctype, ?acccat, ?acccode, ?accnumber, 
                                  ?debit, ?credit, ?lasttranno, ?lasttrancode, ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = modItem.GetSetparentid;
                cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = modItem.GetSetaccgroup;
                cmd.Parameters.Add("?acclevel", MySqlDbType.Int16).Value = modItem.GetSetacclevel;
                cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = modItem.GetSetendlevel;
                cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = modItem.GetSetacctype;
                cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = modItem.GetSetacccategory;
                cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = modItem.GetSetacccode;
                cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = modItem.GetSetaccnumber;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisCOATran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateInitialAmountFisCOATranEndLevel(String comp, String fyr, double debit, double credit)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_coa_tran 
                           SET  debit = ?debit, 
                                credit = ?credit
                           WHERE comp = ?comp and fyr = ?fyr and endlevel = 'Y' ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = debit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = credit;
                cmd.CommandTimeout = 60;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -updateInitialAmountFisCOATranEndLevel: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisCOATran(String comp, String fyr)
    {
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        int success = 0;

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @"
                        SELECT updatecoabalance(?comp, ?fyr) result;
                        ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    success = replaceZero(dataReader, "result");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-updateFisCOATran: " + e.Message.ToString());
        }
        return success;
    }

    public int updateFisCOATran(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_coa_tran 
                           SET  accid = ?accid, accdesc = ?accdesc, parentid = ?parentid, 
                                accgroup = ?accgroup, acclevel = ?acclevel, 
                                endlevel = ?endlevel, acctype = ?acctype, 
                                acccat = ?acccat, acccode = ?acccode, 
                                accnumber = ?accnumber, debit = ?debit, 
                                credit = ?credit, lasttranno = ?lasttranno, 
                                lasttrancode = ?lasttrancode, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id and fyr = ?fyr ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?parentid", MySqlDbType.VarChar).Value = modItem.GetSetparentid;
                cmd.Parameters.Add("?accgroup", MySqlDbType.VarChar).Value = modItem.GetSetaccgroup;
                cmd.Parameters.Add("?acclevel", MySqlDbType.Int16).Value = modItem.GetSetacclevel;
                cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = modItem.GetSetendlevel;
                cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = modItem.GetSetacctype;
                cmd.Parameters.Add("?acccat", MySqlDbType.VarChar).Value = modItem.GetSetacccategory;
                cmd.Parameters.Add("?acccode", MySqlDbType.VarChar).Value = modItem.GetSetacccode;
                cmd.Parameters.Add("?accnumber", MySqlDbType.VarChar).Value = modItem.GetSetaccnumber;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.CommandTimeout = 60;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -updateFisCOATran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisCOATran(String comp, String fyr, String accid)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_coa_tran WHERE comp = ?comp AND fyr = ?fyr AND accid = ?accid ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisCOATran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int getMaxLevelCOA(String comp, String fyr, String acctype)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT MAX(a.acclevel) as maxlevel FROM fis_coa_tran a WHERE a.comp is not null  ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                cmd.CommandTimeout = 60;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result = replaceZero(dataReader, "maxlevel");
                }
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getMaxLevelCOA: " + e.Message.ToString());
        }

        return result;
    }

    public ArrayList getFisCOALedgerTranDebitCredit(String comp, String fyr, String acctype, String endlevel)
    {
        ArrayList lsFisCOALegderTranDebitCredit = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT b.id, b.comp, b.fyr, b.accid, SUM(a.debit) as jumdebit, SUM(a.credit) as jumcredit
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                           AND    a.status <> 'CANCELLED'
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  b.comp = ?comp ";
                }
                else
                {
                    query = query + " and  b.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  b.fyr = ?fyr ";
                }
                if (endlevel.Trim().Length > 0)
                {
                    query = query + " and  b.endlevel = ?endlevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  b.acctype = ?acctype ";
                }
                query = query + " group by b.id, b.comp, b.fyr, b.accid ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (endlevel.Trim().Length > 0) cmd.Parameters.Add("?endlevel", MySqlDbType.VarChar).Value = endlevel;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                cmd.CommandTimeout = 60;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "jumdebit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "jumcredit");
                    lsFisCOALegderTranDebitCredit.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOALedgerTranDebitCredit: " + e.Message.ToString());
        }
        return lsFisCOALegderTranDebitCredit;
    }

    public ArrayList getFisCOALevelDebitCredit(String comp, String fyr, String acctype, int acclevel)
    {
        ArrayList lsFisCOALevelDebitCredit = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.accid, 
                                  (SELECT SUM(b.debit) FROM fis_coa_tran b WHERE b.comp = a.comp AND b.fyr = a.fyr AND b.parentid = a.accid) as jumdebit , 
                                  (SELECT SUM(b.credit) FROM fis_coa_tran b WHERE b.comp = a.comp AND b.fyr = a.fyr AND b.parentid = a.accid) as jumcredit
                           FROM   fis_coa_tran a
                           WHERE  a.comp is not null
                           and a.endlevel = 'N'
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (acclevel > 0)
                {
                    query = query + " and  a.acclevel = ?acclevel ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                query = query + " group by a.id, a.comp, a.fyr, a.accid ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (acclevel > 0) cmd.Parameters.Add("?acclevel", MySqlDbType.Int16).Value = acclevel;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                cmd.CommandTimeout = 60;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "jumdebit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "jumcredit");
                    lsFisCOALevelDebitCredit.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisCOALevelDebitCredit: " + e.Message.ToString());
        }
        return lsFisCOALevelDebitCredit;
    }

    public String getFISCOATranParentDesc(String comp, String fyr, String accid, int acclevel)
    {
        String parentDesc = "";
        String query = "";

        try
        {
            for (int i=acclevel; i>0; i--)
            {
                AccountingModel modItem = getFisCOATranDetail(comp, fyr, accid, "", "", 0, "", "", "", "", "");
                parentDesc = parentDesc + ">" + modItem.GetSetaccdesc;
                accid = modItem.GetSetparentid;
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFISCOATranParentDesc: " + e.Message.ToString());
        }
        return parentDesc;
    }

    public String getFISCOATranChildAccid(String comp, String fyr, String accid, String accumulatedaccid)
    {
        String childAccid = "";

        try
        {
            AccountingModel modItem = getFisCOATranDetail(comp, fyr, accid, "", "", 0, "", "", "", "", "");
            if (modItem.GetSetaccid.Length > 0) {
                if (accumulatedaccid.Length > 0)
                {
                    childAccid = accumulatedaccid + ",'" + modItem.GetSetaccid + "'";
                }
                else
                {
                    childAccid = "'" + modItem.GetSetaccid + "'";
                }
                ArrayList lsAccid = getFisCOATranList(comp, fyr, "", accid, "", 0, "", "", "", "", "");
                for(int i=0; i<lsAccid.Count; i++)
                {
                    AccountingModel modItem2 = (AccountingModel)lsAccid[i];
                    childAccid = getFISCOATranChildAccid(modItem2.GetSetcomp, modItem2.GetSetfyr, modItem2.GetSetaccid, childAccid);
                }
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFISCOATranChildAccid: " + e.Message.ToString());
        }
        return childAccid;
    }

    public String getFisLegderTranSatus(String comp, String fyr, String accid, String starttrandate, String endtrandate, String Status)
    {
        String ledgertranstatus = "";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.status
                           FROM   fis_ledger a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (starttrandate.Trim().Length > 0)
                {
                    query = query + " and a.ledgerdate >= ?starttrandate";
                }
                if (endtrandate.Trim().Length > 0)
                {
                    query = query + " and a.ledgerdate <= ?endtrandate";
                }
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (starttrandate.Length > 0)
                {
                    if (starttrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(starttrandate, ukDtfi);
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?starttrandate", MySqlDbType.DateTime).Value = null;
                }

                //if (endtrandate.Trim().Length > 0) cmd.Parameters.Add("?endtrandate", MySqlDbType.VarChar).Value = endtrandate;
                if (endtrandate.Length > 0)
                {
                    if (endtrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(endtrandate, ukDtfi);
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?endtrandate", MySqlDbType.DateTime).Value = null;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    ledgertranstatus = replaceNull(dataReader, "status");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisLegderTranSatus: " + e.Message.ToString());
        }
        return ledgertranstatus;
    }

    #endregion

    #region/*** BEGIN FOR BANK***/

    public ArrayList getFisBankList(String comp, Int64 id, String bankid, String bankname, String accountno, String status)
    {
        ArrayList lsFisBank = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.bankid, a.bankname, a.accountno, a.address, a.contact, a.contactno, a.currency,
                                  a.exrate, a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_bank a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (bankid.Trim().Length > 0)
                {
                    query = query + " and  a.bankid = ?bankid ";
                }
                if (bankname.Trim().Length > 0)
                {
                    query = query + " and  a.bankname like ?bankname ";
                }
                if (accountno.Trim().Length > 0)
                {
                    query = query + " and  a.accountno = ?accountno ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.bankid, a.accountno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (bankid.Trim().Length > 0) cmd.Parameters.Add("?bankid", MySqlDbType.VarChar).Value = bankid;
                if (bankname.Trim().Length > 0) cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = bankname;
                if (accountno.Trim().Length > 0) cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = accountno;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetbankid = replaceNull(dataReader, "bankid");
                    modItem.GetSetbankname = replaceNull(dataReader, "bankname");
                    modItem.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modItem.GetSetaddress = replaceNull(dataReader, "address");
                    modItem.GetSetcontact = replaceNull(dataReader, "contact");
                    modItem.GetSetcontactno = replaceNull(dataReader, "contactno");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisBank.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBankList: " + e.Message.ToString());
        }
        return lsFisBank;
    }

    public AccountingModel getFisBankDetail(String comp, Int64 id, String bankid, String bankname, String accountno, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.bankid, a.bankname, a.accountno, a.address, a.contact, a.contactno, a.currency,
                                  a.exrate, a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_bank a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (bankid.Trim().Length > 0)
                {
                    query = query + " and  a.bankid = ?bankid ";
                }
                if (bankname.Trim().Length > 0)
                {
                    query = query + " and  a.bankname like ?bankname ";
                }
                if (accountno.Trim().Length > 0)
                {
                    query = query + " and  a.accountno = ?accountno ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.bankid, a.accountno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (bankid.Trim().Length > 0) cmd.Parameters.Add("?bankid", MySqlDbType.VarChar).Value = bankid;
                if (bankname.Trim().Length > 0) cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = bankname;
                if (accountno.Trim().Length > 0) cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = accountno;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetbankid = replaceNull(dataReader, "bankid");
                    modItem.GetSetbankname = replaceNull(dataReader, "bankname");
                    modItem.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modItem.GetSetaddress = replaceNull(dataReader, "address");
                    modItem.GetSetcontact = replaceNull(dataReader, "contact");
                    modItem.GetSetcontactno = replaceNull(dataReader, "contactno");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();

            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBankDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public int insertFisBank(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_bank(comp, bankid, bankname, accountno, address, contact, contactno, currency, exrate, debit, credit, 
                                                    lasttranno, lasttrancode, status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?bankid, ?bankname, ?accountno, ?address, ?contact, ?contactno, ?currency, ?exrate, ?debit, ?credit, 
                                  ?lasttranno, ?lasttrancode, ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?bankid", MySqlDbType.VarChar).Value = modItem.GetSetbankid;
                cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = modItem.GetSetbankname;
                cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = modItem.GetSetaccountno;
                cmd.Parameters.Add("?address", MySqlDbType.VarChar).Value = modItem.GetSetaddress;
                cmd.Parameters.Add("?contact", MySqlDbType.VarChar).Value = modItem.GetSetcontact;
                cmd.Parameters.Add("?contactno", MySqlDbType.VarChar).Value = modItem.GetSetcontactno;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisBank: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisBank(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_bank 
                           SET  bankid = ?bankid,
                                bankname = ?bankname, accountno = ?accountno, 
                                address = ?address, contact = ?contact, 
                                contactno = ?contactno, currency = ?currency, 
                                exrate = ?exrate, debit = ?debit, 
                                credit = ?credit, lasttranno = ?lasttranno, 
                                lasttrancode = ?lasttrancode, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?bankid", MySqlDbType.VarChar).Value = modItem.GetSetbankid;
                cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = modItem.GetSetbankname;
                cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = modItem.GetSetaccountno;
                cmd.Parameters.Add("?address", MySqlDbType.VarChar).Value = modItem.GetSetaddress;
                cmd.Parameters.Add("?contact", MySqlDbType.VarChar).Value = modItem.GetSetcontact;
                cmd.Parameters.Add("?contactno", MySqlDbType.VarChar).Value = modItem.GetSetcontactno;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -updateFisCOAMaster: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisBank(String comp, String id)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_bank WHERE comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisBank: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    #endregion

    #region/*** BEGIN FOR BP***/

    public ArrayList getFisBpList(String comp, Int64 id, String bpid, String bpdesc, String bpref, String status)
    {
        ArrayList lsFisBp = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.bpid, a.bpdesc, a.bpaddress, a.bpcontact, a.bpreference,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_bpid a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (bpid.Trim().Length > 0)
                {
                    query = query + " and  a.bpid = ?bpid ";
                }
                if (bpdesc.Trim().Length > 0)
                {
                    query = query + " and  a.bpdesc like ?bpdesc ";
                }
                if (bpref.Trim().Length > 0)
                {
                    query = query + " and  a.bpreference = ?bpreference ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.bpdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (bpid.Trim().Length > 0) cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = bpid;
                if (bpdesc.Trim().Length > 0) cmd.Parameters.Add("?bpdesc", MySqlDbType.VarChar).Value = bpdesc;
                if (bpref.Trim().Length > 0) cmd.Parameters.Add("?bpreference", MySqlDbType.VarChar).Value = bpref;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetbpid = replaceNull(dataReader, "bpid");
                    modItem.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modItem.GetSetbpaddress = replaceNull(dataReader, "bpaddress");
                    modItem.GetSetbpcontact = replaceNull(dataReader, "bpcontact");
                    modItem.GetSetbpreference = replaceNull(dataReader, "bpreference");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisBp.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBpList: " + e.Message.ToString());
        }
        return lsFisBp;
    }

    public AccountingModel getFisBpDetail(String comp, Int64 id, String bpid, String bpdesc, String bpref, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.bpid, a.bpdesc, a.bpaddress, a.bpcontact, a.bpreference,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_bpid a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (bpid.Trim().Length > 0)
                {
                    query = query + " and  a.bpid = ?bpid ";
                }
                if (bpdesc.Trim().Length > 0)
                {
                    query = query + " and  a.bpdesc like ?bpdesc ";
                }
                if (bpref.Trim().Length > 0)
                {
                    query = query + " and  a.bpreference = ?bpreference ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.bpdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (bpid.Trim().Length > 0) cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = bpid;
                if (bpdesc.Trim().Length > 0) cmd.Parameters.Add("?bpdesc", MySqlDbType.VarChar).Value = bpdesc;
                if (bpref.Trim().Length > 0) cmd.Parameters.Add("?bpreference", MySqlDbType.VarChar).Value = bpref;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetbpid = replaceNull(dataReader, "bpid");
                    modItem.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modItem.GetSetbpaddress = replaceNull(dataReader, "bpaddress");
                    modItem.GetSetbpcontact = replaceNull(dataReader, "bpcontact");
                    modItem.GetSetbpreference = replaceNull(dataReader, "bpreference");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();

            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBpDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public int insertFisBp(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_bpid(comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, debit, credit, 
                                                    lasttranno, lasttrancode, status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?bpid, ?bpdesc, ?bpaddress, ?bpcontact, ?bpreference, ?debit, ?credit, 
                                  ?lasttranno, ?lasttrancode, ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = modItem.GetSetbpid;
                cmd.Parameters.Add("?bpdesc", MySqlDbType.VarChar).Value = modItem.GetSetbpdesc;
                cmd.Parameters.Add("?bpaddress", MySqlDbType.VarChar).Value = modItem.GetSetbpaddress;
                cmd.Parameters.Add("?bpcontact", MySqlDbType.VarChar).Value = modItem.GetSetbpcontact;
                cmd.Parameters.Add("?bpreference", MySqlDbType.VarChar).Value = modItem.GetSetbpreference;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - insertFisBp: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisBp(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_bpid 
                           SET  bpid = ?bpid,
                                bpdesc = ?bpdesc, bpaddress = ?bpaddress, 
                                bpcontact = ?bpcontact, bpreference = ?bpreference, 
                                debit = ?debit, 
                                credit = ?credit, lasttranno = ?lasttranno, 
                                lasttrancode = ?lasttrancode, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = modItem.GetSetbpid;
                cmd.Parameters.Add("?bpdesc", MySqlDbType.VarChar).Value = modItem.GetSetbpdesc;
                cmd.Parameters.Add("?bpaddress", MySqlDbType.VarChar).Value = modItem.GetSetbpaddress;
                cmd.Parameters.Add("?bpcontact", MySqlDbType.VarChar).Value = modItem.GetSetbpcontact;
                cmd.Parameters.Add("?bpreference", MySqlDbType.VarChar).Value = modItem.GetSetbpreference;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - updateFisBp: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisBp(String comp, String bpid)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_bpid WHERE comp = ?comp AND bpid = ?bpid ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = bpid;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisBp: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public ArrayList getBPMasterTranList(String comp, String bpid, String bpdesc, String option)
    {
        ArrayList lsBPMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.bpid, a.bpdesc, a.bpaddress, a.bpcontact, a.bpreference, a.bpstatus, CASE WHEN b.bpdesc IS NOT NULL THEN 1 ELSE 0 END as haschecked ";
                query = query + " from   businesspartner a " + (option.Equals("INNER") ? "INNER JOIN" : option.Equals("LEFT") ? "LEFT OUTER JOIN" : option.Equals("RIGHT") ? "RIGHT OUTER JOIN" : option.Equals("ONLY") ? "LEFT OUTER JOIN" : "LEFT OUTER JOIN") + @" fis_bpid b on a.comp = b.comp and a.bpid = b.bpid ";
                query = query + " WHERE  a.comp is not null " + (option.Equals("ONLY") ? " and b.bpid is null " : "");
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (bpid.Trim().Length > 0)
                {
                    query = query + " and  a.bpid = '" + bpid + "' ";
                }
                if (bpdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.bpdesc) like '%" + bpdesc + "%' ";
                }
                query = query + " order by a.comp, a.bpid ";
                //WriteToLogFile("AccountingController-getBPMasterTranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modBP = new AccountingModel();
                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetbpid = replaceNull(dataReader, "bpid");
                    modBP.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modBP.GetSetbpaddress = replaceNull(dataReader, "bpaddress");
                    modBP.GetSetbpcontact = replaceNull(dataReader, "bpcontact");
                    modBP.GetSetbpreference = replaceNull(dataReader, "bpreference");
                    modBP.GetSetstatus = replaceNull(dataReader, "bpstatus");
                    modBP.GetSethaschecked = replaceNull(dataReader, "haschecked");
                    lsBPMod.Add(modBP);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getBPMasterTranList: " + e.Message.ToString());
        }
        return lsBPMod;
    }

    public ArrayList getBPMasterList(String comp, String bpid, String bpdesc, String bpreference)
    {
        ArrayList lsBPMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.bpid, a.bpdesc, a.bpaddress, a.bpcontact, a.bpreference, a.bpstatus ";
                query = query + " from   businesspartner a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (bpid.Trim().Length > 0)
                {
                    query = query + " and  a.bpid = '" + bpid + "' ";
                }
                if (bpdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.bpdesc) like '%" + bpdesc + "%' ";
                }
                if (bpreference.Trim().Length > 0)
                {
                    query = query + " and  a.bpreference = '" + bpreference + "' ";
                }
                query = query + " order by a.comp, a.bpdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modBP = new AccountingModel();
                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetbpid = replaceNull(dataReader, "bpid");
                    modBP.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modBP.GetSetbpaddress = replaceNull(dataReader, "bpaddress");
                    modBP.GetSetbpcontact = replaceNull(dataReader, "bpcontact");
                    modBP.GetSetbpreference = replaceNull(dataReader, "bpreference");
                    modBP.GetSetstatus = replaceNull(dataReader, "bpstatus");
                    lsBPMod.Add(modBP);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getBPMasterList: " + e.Message.ToString());
        }
        return lsBPMod;
    }

    public AccountingModel getBPMasterDetail(String comp, String bpid, String bpdesc, String bpreference)
    {
        AccountingModel modBP = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.bpid, a.bpdesc, a.bpaddress, a.bpcontact, a.bpreference, a.bpstatus ";
                query = query + " from   businesspartner a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (bpid.Trim().Length > 0)
                {
                    query = query + " and  a.bpid = '" + bpid + "' ";
                }
                if (bpdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.bpdesc) like '%" + bpdesc + "%' ";
                }
                if (bpreference.Trim().Length > 0)
                {
                    query = query + " and  a.bpreference = '" + bpreference + "' ";
                }
                query = query + " order by a.comp, a.bpdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetbpid = replaceNull(dataReader, "bpid");
                    modBP.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modBP.GetSetbpaddress = replaceNull(dataReader, "bpaddress");
                    modBP.GetSetbpcontact = replaceNull(dataReader, "bpcontact");
                    modBP.GetSetbpreference = replaceNull(dataReader, "bpreference");
                    modBP.GetSetstatus = replaceNull(dataReader, "bpstatus");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getBPMasterList: " + e.Message.ToString());
        }
        return modBP;
    }

    #endregion

    #region/*** BEGIN FOR ITEM***/

    public ArrayList getFisItemList(String comp, Int64 id, String itemno, String itemdesc, String itemcat, String status)
    {
        ArrayList lsFisItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.itemno, a.itemdesc, a.itemcat, a.itemtype,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_item a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (itemno.Trim().Length > 0)
                {
                    query = query + " and  a.itemno = ?itemno ";
                }
                if (itemdesc.Trim().Length > 0)
                {
                    query = query + " and  a.itemdesc like ?itemdesc ";
                }
                if (itemcat.Trim().Length > 0)
                {
                    query = query + " and  a.itemcat = ?itemcat ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.itemdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (itemno.Trim().Length > 0) cmd.Parameters.Add("?itemno", MySqlDbType.VarChar).Value = itemno;
                if (itemdesc.Trim().Length > 0) cmd.Parameters.Add("?itemdesc", MySqlDbType.VarChar).Value = itemdesc;
                if (itemcat.Trim().Length > 0) cmd.Parameters.Add("?itemcat", MySqlDbType.VarChar).Value = itemcat;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetitemno = replaceNull(dataReader, "itemno");
                    modItem.GetSetitemdesc = replaceNull(dataReader, "itemdesc");
                    modItem.GetSetitemcat = replaceNull(dataReader, "itemcat");
                    modItem.GetSetitemtype = replaceNull(dataReader, "itemtype");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisItemList: " + e.Message.ToString());
        }
        return lsFisItem;
    }

    public AccountingModel getFisItemDetail(String comp, Int64 id, String itemno, String itemdesc, String itemcat, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.itemno, a.itemdesc, a.itemcat, a.itemtype,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_item a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (itemno.Trim().Length > 0)
                {
                    query = query + " and  a.itemno = ?itemno ";
                }
                if (itemdesc.Trim().Length > 0)
                {
                    query = query + " and  a.itemdesc like ?bpdesc ";
                }
                if (itemcat.Trim().Length > 0)
                {
                    query = query + " and  a.itemcat = ?itemcat ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.itemdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (itemno.Trim().Length > 0) cmd.Parameters.Add("?itemno", MySqlDbType.VarChar).Value = itemno;
                if (itemdesc.Trim().Length > 0) cmd.Parameters.Add("?itemdesc", MySqlDbType.VarChar).Value = itemdesc;
                if (itemcat.Trim().Length > 0) cmd.Parameters.Add("?itemcat", MySqlDbType.VarChar).Value = itemcat;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetitemno = replaceNull(dataReader, "itemno");
                    modItem.GetSetitemdesc = replaceNull(dataReader, "itemdesc");
                    modItem.GetSetitemcat = replaceNull(dataReader, "itemcat");
                    modItem.GetSetitemtype = replaceNull(dataReader, "itemtype");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modItem.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();

            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisItemDetail: " + e.Message.ToString());
        }
        return modItem;
    }

    public int insertFisItem(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_item(comp, itemno, itemdesc, itemcat, itemtype, debit, credit, lasttranno, lasttrancode, status,
                                                    createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?itemno, ?itemdesc, ?itemcat, ?itemtype, ?debit, ?credit, ?lasttranno, ?lasttrancode, ?status, 
                                  ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?itemno", MySqlDbType.VarChar).Value = modItem.GetSetitemno;
                cmd.Parameters.Add("?itemdesc", MySqlDbType.VarChar).Value = modItem.GetSetitemdesc;
                cmd.Parameters.Add("?itemcat", MySqlDbType.VarChar).Value = modItem.GetSetitemcat;
                cmd.Parameters.Add("?itemtype", MySqlDbType.VarChar).Value = modItem.GetSetitemtype;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - insertFisItem: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisItem(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_item 
                           SET  itemno = ?itemno,
                                itemdesc = ?itemdesc, itemcat = ?itemcat, 
                                itemtype = ?itemtype, debit = ?debit, 
                                credit = ?credit, lasttranno = ?lasttranno, 
                                lasttrancode = ?lasttrancode, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?itemno", MySqlDbType.VarChar).Value = modItem.GetSetitemno;
                cmd.Parameters.Add("?itemdesc", MySqlDbType.VarChar).Value = modItem.GetSetitemdesc;
                cmd.Parameters.Add("?itemcat", MySqlDbType.VarChar).Value = modItem.GetSetitemcat;
                cmd.Parameters.Add("?itemtype", MySqlDbType.VarChar).Value = modItem.GetSetitemtype;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modItem.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modItem.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - updateFisItem: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisItem(String comp, String itemno)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_item WHERE comp = ?comp AND itemno = ?itemno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?itemno", MySqlDbType.VarChar).Value = itemno;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisBp: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public ArrayList getItemMasterTranList(String comp, String itemno, String itemdesc, String itemcat, String option)
    {
        ArrayList lsItemMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.itemno, a.itemdesc, a.itemcat, a.itemtype, a.itemstatus, CASE WHEN b.itemdesc IS NOT NULL THEN 1 ELSE 0 END as haschecked ";
                query = query + " from   item a " + (option.Equals("INNER") ? "INNER JOIN" : option.Equals("LEFT") ? "LEFT OUTER JOIN" : option.Equals("RIGHT") ? "RIGHT OUTER JOIN" : option.Equals("ONLY") ? "LEFT OUTER JOIN" : "LEFT OUTER JOIN") + @" fis_item b on a.comp = b.comp and a.itemno = b.itemno ";
                query = query + " WHERE  a.comp is not null " + (option.Equals("ONLY") ? " and b.itemno is null " : "");
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (itemno.Trim().Length > 0)
                {
                    query = query + " and  a.itemno = '" + itemno + "' ";
                }
                if (itemdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.itemdesc) like '%" + itemdesc + "%' ";
                }
                if (itemcat.Trim().Length > 0)
                {
                    query = query + " and  a.itemcat = '" + itemcat + "' ";
                }
                query = query + " order by a.comp, a.itemdesc ";
                //WriteToLogFile("AccountingController-getBPMasterTranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modBP = new AccountingModel();
                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetitemno = replaceNull(dataReader, "itemno");
                    modBP.GetSetitemdesc = replaceNull(dataReader, "itemdesc");
                    modBP.GetSetitemcat = replaceNull(dataReader, "itemcat");
                    modBP.GetSetitemtype = replaceNull(dataReader, "itemtype");
                    modBP.GetSetstatus = replaceNull(dataReader, "itemstatus");
                    modBP.GetSethaschecked = replaceNull(dataReader, "haschecked");
                    lsItemMod.Add(modBP);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getItemMasterTranList: " + e.Message.ToString());
        }
        return lsItemMod;
    }

    public ArrayList getItemMasterList(String comp, String itemno, String itemdesc, String itemcat)
    {
        ArrayList lsItemMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.itemno, a.itemdesc, a.itemcat, a.itemtype, a.itemstatus ";
                query = query + " from   item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (itemno.Trim().Length > 0)
                {
                    query = query + " and  a.itemno = '" + itemno + "' ";
                }
                if (itemdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.itemdesc) like '%" + itemdesc + "%' ";
                }
                if (itemcat.Trim().Length > 0)
                {
                    query = query + " and  a.itemcat = '" + itemcat + "' ";
                }
                query = query + " order by a.comp, a.itemdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modBP = new AccountingModel();
                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetitemno = replaceNull(dataReader, "itemno");
                    modBP.GetSetitemdesc = replaceNull(dataReader, "itemdesc");
                    modBP.GetSetitemcat = replaceNull(dataReader, "itemcat");
                    modBP.GetSetitemtype = replaceNull(dataReader, "itemtype");
                    modBP.GetSetstatus = replaceNull(dataReader, "itemstatus");
                    lsItemMod.Add(modBP);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getItemMasterList: " + e.Message.ToString());
        }
        return lsItemMod;
    }

    public AccountingModel getItemMasterDetail(String comp, String itemno, String itemdesc, String itemcat)
    {
        AccountingModel modBP = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.itemno, a.itemdesc, a.itemcat, a.itemtype, a.itemstatus ";
                query = query + " from   item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (itemno.Trim().Length > 0)
                {
                    query = query + " and  a.itemno = '" + itemno + "' ";
                }
                if (itemdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.itemdesc) like '%" + itemdesc + "%' ";
                }
                if (itemcat.Trim().Length > 0)
                {
                    query = query + " and  a.itemcat = '" + itemcat + "' ";
                }
                query = query + " order by a.comp, a.itemdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modBP.GetSetcomp = replaceNull(dataReader, "comp");
                    modBP.GetSetitemno = replaceNull(dataReader, "itemno");
                    modBP.GetSetitemdesc = replaceNull(dataReader, "itemdesc");
                    modBP.GetSetitemcat = replaceNull(dataReader, "itemcat");
                    modBP.GetSetitemtype = replaceNull(dataReader, "itemtype");
                    modBP.GetSetstatus = replaceNull(dataReader, "itemstatus");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getItemMasterDetail: " + e.Message.ToString());
        }
        return modBP;
    }

    #endregion

    #region/*** BEGIN FOR ASSET***/

    public ArrayList getFisAssetList(String comp, Int64 id, String assetno, String assetdesc, String assetcat, String status)
    {
        ArrayList lsFisAsset = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_asset a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = ?assetno ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  a.assetdesc like ?assetdesc ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = ?assetcat ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (assetno.Trim().Length > 0) cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = assetno;
                if (assetdesc.Trim().Length > 0) cmd.Parameters.Add("?assetdesc", MySqlDbType.VarChar).Value = assetdesc;
                if (assetcat.Trim().Length > 0) cmd.Parameters.Add("?assetcat", MySqlDbType.VarChar).Value = assetcat;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modAsset = new AccountingModel();
                    modAsset.GetSetid = replaceZero(dataReader, "id");
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "assettyp");
                    modAsset.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modAsset.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modAsset.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modAsset.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                    modAsset.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modAsset.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modAsset.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modAsset.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modAsset.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modAsset.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisAsset.Add(modAsset);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisAssetList: " + e.Message.ToString());
        }
        return lsFisAsset;
    }

    public ArrayList getFisAssetList(String comp, Int64 id, String assetno, String assetdesc, String assetcat, String assettyp, String status)
    {
        ArrayList lsFisAsset = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_asset a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = ?assetno ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  a.assetdesc like ?assetdesc ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = ?assetcat ";
                }
                if (assettyp.Trim().Length > 0)
                {
                    query = query + " and  a.assettyp = ?assettyp ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (assetno.Trim().Length > 0) cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = assetno;
                if (assetdesc.Trim().Length > 0) cmd.Parameters.Add("?assetdesc", MySqlDbType.VarChar).Value = assetdesc;
                if (assetcat.Trim().Length > 0) cmd.Parameters.Add("?assetcat", MySqlDbType.VarChar).Value = assetcat;
                if (assettyp.Trim().Length > 0) cmd.Parameters.Add("?assettyp", MySqlDbType.VarChar).Value = assettyp;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modAsset = new AccountingModel();
                    modAsset.GetSetid = replaceZero(dataReader, "id");
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "assettyp");
                    modAsset.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modAsset.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modAsset.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modAsset.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                    modAsset.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modAsset.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modAsset.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modAsset.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modAsset.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modAsset.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisAsset.Add(modAsset);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisAssetList: " + e.Message.ToString());
        }
        return lsFisAsset;
    }

    public AccountingModel getFisAssetDetail(String comp, Int64 id, String assetno, String assetdesc, String assetcat, String assettyp, String status)
    {
        AccountingModel modAsset = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp,
                                  a.debit, a.credit, a.lasttranno, a.lasttrancode,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_asset a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = ?assetno ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  a.assetdesc like ?assetdesc ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = ?assetcat ";
                }
                if (assettyp.Trim().Length > 0)
                {
                    query = query + " and  a.assettyp = ?assettyp ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (assetno.Trim().Length > 0) cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = assetno;
                if (assetdesc.Trim().Length > 0) cmd.Parameters.Add("?assetdesc", MySqlDbType.VarChar).Value = assetdesc;
                if (assetcat.Trim().Length > 0) cmd.Parameters.Add("?assetcat", MySqlDbType.VarChar).Value = assetcat;
                if (assettyp.Trim().Length > 0) cmd.Parameters.Add("?assettyp", MySqlDbType.VarChar).Value = assettyp;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modAsset.GetSetid = replaceZero(dataReader, "id");
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "assettyp");
                    modAsset.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modAsset.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modAsset.GetSetlasttranno = replaceZero(dataReader, "lasttranno");
                    modAsset.GetSetlasttrancode = replaceNull(dataReader, "lasttrancode");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                    modAsset.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modAsset.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modAsset.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modAsset.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modAsset.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modAsset.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();

            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisAssetDetail: " + e.Message.ToString());
        }
        return modAsset;
    }

    public int insertFisAsset(AccountingModel modAsset)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_asset(comp, assetno, assetdesc, assetcat, assettyp, debit, credit, lasttranno, lasttrancode, status,
                                                    createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?assetno, ?assetdesc, ?assetcat, ?assettyp, ?debit, ?credit, ?lasttranno, ?lasttrancode, ?status, 
                                  ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modAsset.GetSetcomp;
                cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = modAsset.GetSetassetno;
                cmd.Parameters.Add("?assetdesc", MySqlDbType.VarChar).Value = modAsset.GetSetassetdesc;
                cmd.Parameters.Add("?assetcat", MySqlDbType.VarChar).Value = modAsset.GetSetassetcat;
                cmd.Parameters.Add("?assettyp", MySqlDbType.VarChar).Value = modAsset.GetSetassettyp;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modAsset.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modAsset.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modAsset.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modAsset.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modAsset.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modAsset.GetSetcreatedby;
                if (modAsset.GetSetcreatedby.Length > 0)
                {
                    if (modAsset.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modAsset.GetSetconfirmedby;
                if (modAsset.GetSetconfirmedby.Length > 0)
                {
                    if (modAsset.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modAsset.GetSetcancelledby;
                if (modAsset.GetSetcancelledby.Length > 0)
                {
                    if (modAsset.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - insertFisAsset: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisAsset(AccountingModel modAsset)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_asset 
                           SET  assetno = ?assetno,
                                assetdesc = ?assetdesc, assetcat = ?assetcat, 
                                assettyp = ?assettyp, debit = ?debit, 
                                credit = ?credit, lasttranno = ?lasttranno, 
                                lasttrancode = ?lasttrancode, status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modAsset.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modAsset.GetSetid;
                cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = modAsset.GetSetassetno;
                cmd.Parameters.Add("?assetdesc", MySqlDbType.VarChar).Value = modAsset.GetSetassetdesc;
                cmd.Parameters.Add("?assetcat", MySqlDbType.VarChar).Value = modAsset.GetSetassetcat;
                cmd.Parameters.Add("?assettyp", MySqlDbType.VarChar).Value = modAsset.GetSetassettyp;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modAsset.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modAsset.GetSetcredit;
                cmd.Parameters.Add("?lasttranno", MySqlDbType.Int16).Value = modAsset.GetSetlasttranno;
                cmd.Parameters.Add("?lasttrancode", MySqlDbType.VarChar).Value = modAsset.GetSetlasttrancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modAsset.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modAsset.GetSetcreatedby;
                if (modAsset.GetSetcreatedby.Length > 0)
                {
                    if (modAsset.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modAsset.GetSetconfirmedby;
                if (modAsset.GetSetconfirmedby.Length > 0)
                {
                    if (modAsset.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modAsset.GetSetcancelledby;
                if (modAsset.GetSetcancelledby.Length > 0)
                {
                    if (modAsset.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modAsset.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController - updateFisAsset: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisAsset(String comp, String assetno, String assettyp)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_asset WHERE comp = ?comp AND assetno = ?assetno AND assettyp = ?assettyp ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?assetno", MySqlDbType.VarChar).Value = assetno;
                cmd.Parameters.Add("?assettyp", MySqlDbType.VarChar).Value = assettyp;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisAsset: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public ArrayList getAssetMasterTranList(String comp, String assetno, String assetdesc, String assetcat, String option)
    {
        ArrayList lsAssetMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT distinct a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp, d.trancode, a.status, CASE WHEN b.assetdesc IS NOT NULL THEN 1 ELSE 0 END as haschecked ";
                query = query + " from   asset a, asset_tran_details d " + (option.Equals("INNER") ? "INNER JOIN" : option.Equals("LEFT") ? "LEFT OUTER JOIN" : option.Equals("RIGHT") ? "RIGHT OUTER JOIN" : option.Equals("ONLY") ? "LEFT OUTER JOIN" : "LEFT OUTER JOIN") + @" fis_asset b on d.comp = b.comp and d.assetno = b.assetno and b.assettyp = d.trancode ";
                query = query + " WHERE  a.comp is not null " + (option.Equals("ONLY") ? " and b.assetno is null " : "");
                query = query + " AND    a.comp = d.comp AND a.assetno = d.assetno AND d.trancode IN ('REGCOST','DEPCOST') ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = '" + assetno + "' ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.assetdesc) like '%" + assetdesc + "%' ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = '" + assetcat + "' ";
                }
                query = query + " order by a.comp, a.assetdesc ";
                //WriteToLogFile("AccountingController-getBPMasterTranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modAsset = new AccountingModel();
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "trancode");
                    //modAsset.GetSettrancode = replaceNull(dataReader, "trancode");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                    modAsset.GetSethaschecked = replaceNull(dataReader, "haschecked");
                    lsAssetMod.Add(modAsset);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getAssetMasterTranList: " + e.Message.ToString());
        }
        return lsAssetMod;
    }

    public ArrayList getAssetMasterList(String comp, String assetno, String assetdesc, String assetcat)
    {
        ArrayList lsAssetMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp, a.status ";
                query = query + " from   asset a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = '" + assetno + "' ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.assetdesc) like '%" + assetdesc + "%' ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = '" + assetcat + "' ";
                }
                query = query + " order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modAsset = new AccountingModel();
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "assettyp");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                    lsAssetMod.Add(modAsset);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getAssetMasterList: " + e.Message.ToString());
        }
        return lsAssetMod;
    }

    public AccountingModel getAssetMasterDetail(String comp, String assetno, String assetdesc, String assetcat)
    {
        AccountingModel modAsset = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.assetno, a.assetdesc, a.assetcat, a.assettyp, a.status ";
                query = query + " from   asset a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = '" + assetno + "' ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.assetdesc) like '%" + assetdesc + "%' ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = '" + assetcat + "' ";
                }
                query = query + " order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "assettyp");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getAssetMasterDetail: " + e.Message.ToString());
        }
        return modAsset;
    }

    public AccountingModel getAssetMasterTranDetail(String comp, String assetno, String assetdesc, String assetcat, String assettyp)
    {
        AccountingModel modAsset = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.assetno, a.assetdesc, a.assetcat, d.trancode, a.status ";
                query = query + " from   asset a, asset_tran_details d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = d.comp AND a.assetno = d.assetno AND d.trancode IN ('REGCOST','DEPCOST') ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (assetno.Trim().Length > 0)
                {
                    query = query + " and  a.assetno = '" + assetno + "' ";
                }
                if (assetdesc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.assetdesc) like '%" + assetdesc + "%' ";
                }
                if (assetcat.Trim().Length > 0)
                {
                    query = query + " and  a.assetcat = '" + assetcat + "' ";
                }
                if (assettyp.Trim().Length > 0)
                {
                    query = query + " and  d.trancode = '" + assettyp + "' ";
                }
                query = query + " order by a.comp, a.assetdesc ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modAsset.GetSetcomp = replaceNull(dataReader, "comp");
                    modAsset.GetSetassetno = replaceNull(dataReader, "assetno");
                    modAsset.GetSetassetdesc = replaceNull(dataReader, "assetdesc");
                    modAsset.GetSetassetcat = replaceNull(dataReader, "assetcat");
                    modAsset.GetSetassettyp = replaceNull(dataReader, "trancode");
                    modAsset.GetSetstatus = replaceNull(dataReader, "status");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getAssetMasterDetail: " + e.Message.ToString());
        }
        return modAsset;
    }

    #endregion

    #region/*** BEGIN FOR OPENING & CLOSING BALANCE***/

    public ArrayList getFisAccountBalanceList(String comp, String fyr, String accid, String acctype, int tranno, String trancode, String status)
    {
        ArrayList lsFisAccountBalance = new ArrayList();

        lsFisAccountBalance = getFisLedgerTranList(comp, fyr, 0, accid, 0, "", tranno, trancode, "");

        if (lsFisAccountBalance.Count == 0)
        {
            if (trancode.Equals("OPENING_BALANCE"))
            {
                ArrayList lsFisCOATran = getFisCOATranList(comp, fyr, accid, "", "", "Y", acctype, "", "", "", "");
                for (int i = 0; i < lsFisCOATran.Count; i++)
                {
                    AccountingModel modFisCOA = (AccountingModel)lsFisCOATran[i];
                    if (modFisCOA.GetSetacctype.Equals("H") || modFisCOA.GetSetacctype.Equals("B"))
                    {
                        //do nothing for Revenue & Expenses
                    }
                    else
                    {
                        AccountingModel modItem = new AccountingModel();
                        modItem.GetSetid = 0;
                        modItem.GetSetcomp = modFisCOA.GetSetcomp;
                        modItem.GetSetfyr = modFisCOA.GetSetfyr;
                        modItem.GetSettranno = 0;
                        modItem.GetSettrancode = trancode;
                        modItem.GetSettrandate = "";
                        modItem.GetSetaccid = modFisCOA.GetSetaccid;
                        modItem.GetSetaccdesc = modFisCOA.GetSetaccdesc;
                        modItem.GetSetparentid = modFisCOA.GetSetparentid;
                        modItem.GetSetaccgroup = modFisCOA.GetSetaccgroup;
                        modItem.GetSetacclevel = modFisCOA.GetSetacclevel;
                        modItem.GetSetendlevel = modFisCOA.GetSetendlevel;
                        modItem.GetSetacctype = modFisCOA.GetSetacctype;
                        modItem.GetSetacccategory = modFisCOA.GetSetacccategory;
                        modItem.GetSetacccode = modFisCOA.GetSetacccode;
                        modItem.GetSetaccnumber = modFisCOA.GetSetaccnumber;
                        modItem.GetSetdebit = 0;// modFisCOA.GetSetdebit;
                        modItem.GetSetcredit = 0;// modFisCOA.GetSetcredit;
                        modItem.GetSetrefno = "";
                        modItem.GetSetstatus = "";
                        modItem.GetSetcreatedby = "";
                        modItem.GetSetcreateddate = "";
                        modItem.GetSetconfirmedby = "";
                        modItem.GetSetconfirmeddate = "";
                        modItem.GetSetcancelledby = "";
                        modItem.GetSetcancelleddate = "";
                        modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                        lsFisAccountBalance.Add(modItem);
                    }
                }
            }
            else if (trancode.Equals("CLOSING_BALANCE"))
            {
                int ClosingPnLLedgerNo = 1;
                Double ClosingPnLDebit = 0;
                Double ClosingPnLCredit = 0;
                String COAPnL = replaceNull(ConfigurationSettings.AppSettings["COAPnL"]);
                AccountingModel modFisBalance = getFisBalance(comp, fyr, tranno, trancode, "");
                AccountingModel modFisOpeningBalance = getLastFisBalance(comp, fyr, "OPENING_BALANCE", modFisBalance.GetSettrandate, "");
                ArrayList lsFisCOATran = getFisCOATranListForClosing(comp, fyr, accid, "", "", "Y", acctype, "", "", modFisOpeningBalance.GetSettrandate, modFisBalance.GetSettrandate, "");
                for (int i = 0; i < lsFisCOATran.Count; i++)
                {
                    AccountingModel modFisCOA = (AccountingModel)lsFisCOATran[i];
                    if (modFisCOA.GetSetacctype.Equals("H") || modFisCOA.GetSetacctype.Equals("B"))
                    {
                        ClosingPnLDebit = ClosingPnLDebit + modFisCOA.GetSetdebit;
                        ClosingPnLCredit = ClosingPnLCredit + modFisCOA.GetSetcredit;
                        if (modFisCOA.GetSetaccid.Equals(COAPnL))
                        {
                            AccountingModel modItem = new AccountingModel();
                            modItem.GetSetid = 0;
                            modItem.GetSetcomp = modFisCOA.GetSetcomp;
                            modItem.GetSetfyr = modFisCOA.GetSetfyr;
                            modItem.GetSetdatefrom = modFisOpeningBalance.GetSettrandate;
                            modItem.GetSetdateto = modFisBalance.GetSettrandate;
                            modItem.GetSettranno = 0;
                            modItem.GetSettrancode = trancode;
                            modItem.GetSettrandate = "";
                            modItem.GetSetaccid = modFisCOA.GetSetaccid;
                            modItem.GetSetaccdesc = modFisCOA.GetSetaccdesc;
                            modItem.GetSetparentid = modFisCOA.GetSetparentid;
                            modItem.GetSetaccgroup = modFisCOA.GetSetaccgroup;
                            modItem.GetSetacclevel = modFisCOA.GetSetacclevel;
                            modItem.GetSetendlevel = modFisCOA.GetSetendlevel;
                            modItem.GetSetacctype = modFisCOA.GetSetacctype;
                            modItem.GetSetacccategory = modFisCOA.GetSetacccategory;
                            modItem.GetSetacccode = modFisCOA.GetSetacccode;
                            modItem.GetSetaccnumber = modFisCOA.GetSetaccnumber;
                            //Begin Method 1
                            if (modItem.GetSetacctype.Equals("A"))
                            {
                                modItem.GetSetledgerno = 1;
                                modItem.GetSetdebit = (modFisCOA.GetSetcredit - modFisCOA.GetSetdebit > 0 ? modFisCOA.GetSetcredit - modFisCOA.GetSetdebit : 0);
                                modItem.GetSetcredit = (modFisCOA.GetSetdebit - modFisCOA.GetSetcredit >= 0 ? modFisCOA.GetSetdebit - modFisCOA.GetSetcredit : 0);
                            }
                            else
                            {
                                modItem.GetSetledgerno = 2;
                                modItem.GetSetcredit = (modFisCOA.GetSetdebit - modFisCOA.GetSetcredit > 0 ? modFisCOA.GetSetdebit - modFisCOA.GetSetcredit : 0);
                                modItem.GetSetdebit = (modFisCOA.GetSetcredit - modFisCOA.GetSetdebit >= 0 ? modFisCOA.GetSetcredit - modFisCOA.GetSetdebit : 0);
                            }
                            //End Method 1
                            //Begin Comment Method 2
                            //modItem.GetSetdebit = modFisCOA.GetSetdebit;
                            //modItem.GetSetcredit = modFisCOA.GetSetcredit;
                            //End Comment Method 2
                            modItem.GetSetrefno = "";
                            modItem.GetSetstatus = getFisLegderTranSatus(modFisCOA.GetSetcomp, modFisCOA.GetSetfyr, modFisCOA.GetSetaccid, modFisOpeningBalance.GetSettrandate, modFisBalance.GetSettrandate, "NEW").Equals("NEW") ? "NEW" : "CONFIRMED";
                            modItem.GetSetcreatedby = "";
                            modItem.GetSetcreateddate = "";
                            modItem.GetSetconfirmedby = "";
                            modItem.GetSetconfirmeddate = "";
                            modItem.GetSetcancelledby = "";
                            modItem.GetSetcancelleddate = "";
                            modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                            lsFisAccountBalance.Add(modItem);
                        }
                    }
                    else 
                    { 
                        AccountingModel modItem = new AccountingModel();
                        modItem.GetSetid = 0;
                        modItem.GetSetcomp = modFisCOA.GetSetcomp;
                        modItem.GetSetfyr = modFisCOA.GetSetfyr;
                        modItem.GetSetdatefrom = modFisOpeningBalance.GetSettrandate;
                        modItem.GetSetdateto = modFisBalance.GetSettrandate;
                        modItem.GetSettranno = 0;
                        modItem.GetSettrancode = trancode;
                        modItem.GetSettrandate = "";
                        modItem.GetSetaccid = modFisCOA.GetSetaccid;
                        modItem.GetSetaccdesc = modFisCOA.GetSetaccdesc;
                        modItem.GetSetparentid = modFisCOA.GetSetparentid;
                        modItem.GetSetaccgroup = modFisCOA.GetSetaccgroup;
                        modItem.GetSetacclevel = modFisCOA.GetSetacclevel;
                        modItem.GetSetendlevel = modFisCOA.GetSetendlevel;
                        modItem.GetSetacctype = modFisCOA.GetSetacctype;
                        modItem.GetSetacccategory = modFisCOA.GetSetacccategory;
                        modItem.GetSetacccode = modFisCOA.GetSetacccode;
                        modItem.GetSetaccnumber = modFisCOA.GetSetaccnumber;
                        //Begin Method 1
                        if (modItem.GetSetacctype.Equals("A"))
                        {
                            modItem.GetSetledgerno = 1;
                            modItem.GetSetdebit = (modFisCOA.GetSetcredit - modFisCOA.GetSetdebit > 0 ? modFisCOA.GetSetcredit - modFisCOA.GetSetdebit : 0);
                            modItem.GetSetcredit = (modFisCOA.GetSetdebit - modFisCOA.GetSetcredit >= 0 ? modFisCOA.GetSetdebit - modFisCOA.GetSetcredit : 0);
                        }
                        else
                        {
                            modItem.GetSetledgerno = 2;
                            modItem.GetSetcredit = (modFisCOA.GetSetdebit - modFisCOA.GetSetcredit > 0 ? modFisCOA.GetSetdebit - modFisCOA.GetSetcredit : 0);
                            modItem.GetSetdebit = (modFisCOA.GetSetcredit - modFisCOA.GetSetdebit >= 0 ? modFisCOA.GetSetcredit - modFisCOA.GetSetdebit : 0);
                        }
                        //End Method 1
                        //Begin Comment Method 2
                        //modItem.GetSetdebit = modFisCOA.GetSetdebit;
                        //modItem.GetSetcredit = modFisCOA.GetSetcredit;
                        //End Comment Method 2
                        modItem.GetSetrefno = "";
                        modItem.GetSetstatus = getFisLegderTranSatus(modFisCOA.GetSetcomp, modFisCOA.GetSetfyr, modFisCOA.GetSetaccid, modFisOpeningBalance.GetSettrandate, modFisBalance.GetSettrandate, "NEW").Equals("NEW") ? "NEW" : "CONFIRMED";
                        modItem.GetSetcreatedby = "";
                        modItem.GetSetcreateddate = "";
                        modItem.GetSetconfirmedby = "";
                        modItem.GetSetconfirmeddate = "";
                        modItem.GetSetcancelledby = "";
                        modItem.GetSetcancelleddate = "";
                        modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                        lsFisAccountBalance.Add(modItem);
                    }
                }
                //to update Coding Profit & Loss again in the ArrayList
                if (ClosingPnLDebit - ClosingPnLCredit > 0)
                {
                    ClosingPnLLedgerNo = 1;
                    ClosingPnLCredit = ClosingPnLDebit - ClosingPnLCredit;
                    ClosingPnLDebit = 0;
                }
                else if (ClosingPnLCredit - ClosingPnLDebit >= 0)
                {
                    ClosingPnLLedgerNo = 2;
                    ClosingPnLDebit = ClosingPnLCredit - ClosingPnLDebit;
                    ClosingPnLCredit = 0;
                }
                var query = from AccountingModel modItem in lsFisAccountBalance
                            where modItem.GetSetaccid.Equals(COAPnL)
                            select modItem;

                foreach (AccountingModel modItem in query)
                {
                    modItem.GetSetledgerno = ClosingPnLLedgerNo;
                    modItem.GetSetdebit = modItem.GetSetdebit + ClosingPnLDebit;
                    modItem.GetSetcredit = modItem.GetSetcredit + ClosingPnLCredit;
                }

            }
        }

        return lsFisAccountBalance;
    }

    public ArrayList getFisBalanceList(String comp, String fyr, String trancode, String status)
    {
        ArrayList lsFisBalance = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisBalance.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalanceList: " + e.Message.ToString());
        }
        return lsFisBalance;
    }

    public ArrayList getFisBalanceList(String comp, String fyr, String trancode, String trandate, String status)
    {
        ArrayList lsFisBalance = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (trandate.Trim().Length > 0)
                {
                    query = query + " and  a.trandate = ?trandate ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (trandate.Length > 0)
                {
                    if (trandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(trandate);
                        DateTime datetime = Convert.ToDateTime(trandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisBalance.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalanceList: " + e.Message.ToString());
        }
        return lsFisBalance;
    }

    public AccountingModel getFisBalance(String comp, String fyr, Int64 id, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalance: " + e.Message.ToString());
        }
        return modItem;
    }
    public AccountingModel getFisBalance(String comp, String fyr, int tranno, String trancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = ?tranno ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (tranno > 0) cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = tranno;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalance: " + e.Message.ToString());
        }
        return modItem;
    }
    public AccountingModel getFisBalance(String comp, String fyr, String trancode, String trandate, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (trandate.Trim().Length > 0)
                {
                    query = query + " and  a.trandate = ?trandate ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (trandate.Length > 0)
                {
                    if (trandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(trandate);
                        DateTime datetime = Convert.ToDateTime(trandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalance: " + e.Message.ToString());
        }
        return modItem;
    }
    public AccountingModel getLastFisBalance(String comp, String fyr, String trancode, String trandate, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, a.trandesc, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_balance a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (trandate.Trim().Length > 0)
                {
                    query = query + " and  a.trandate < ?trandate ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (trandate.Length > 0)
                {
                    if (trandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(trandate);
                        DateTime datetime = Convert.ToDateTime(trandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getLastFisBalance: " + e.Message.ToString());
        }
        return modItem;
    }
    public int insertFisBalance(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_balance(comp, fyr, tranno, trancode, trandate, trandesc, currency, exrate, debit, credit, refno, remarks,
                                                    status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?fyr, ?tranno, ?trancode, ?trandate, ?trandesc, ?currency, ?exrate, ?debit, ?credit, ?refno, ?remarks, 
                                  ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate);
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisBalance: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisBalance(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_balance 
                           SET  fyr = ?fyr, tranno = ?tranno, 
                                trancode = ?trancode, trandate = ?trandate, 
                                trandesc = ?trandesc, currency = ?currency, 
                                exrate = ?exrate,
                                refno = ?refno, remarks = ?remarks, 
                                debit = ?debit, credit = ?credit, 
                                status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        //DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate);
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-updateFisBalance: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisBalanceInfo(String comp, String fyr, Int64 id)
    {
        int result = 0;

        AccountingModel oModFisBalance = new AccountingModel();
        ArrayList lsFisLedgerTran = new ArrayList();

        try
        {
            oModFisBalance = getFisBalance(comp, fyr, id, "");
            if (oModFisBalance.GetSetid > 0)
            {
                double debit = 0, credit = 0;
                lsFisLedgerTran = getFisLedgerTranList(comp, fyr, 0, "", 0, "", oModFisBalance.GetSettranno, oModFisBalance.GetSettrancode, "");
                for (int i = 0; i < lsFisLedgerTran.Count; i++)
                {
                    AccountingModel oModLineItem = (AccountingModel)lsFisLedgerTran[i];
                    debit = debit + oModLineItem.GetSetdebit;
                    credit = credit + oModLineItem.GetSetcredit;
                }
                oModFisBalance.GetSetdebit = debit;
                oModFisBalance.GetSetcredit = credit;
                //update order header
                result = updateFisBalance(oModFisBalance);
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-updateFisBalanceInfo: " + e.Message.ToString());
        }
        return result;
    }

    public int updateFisBalanceInfo(String comp, String fyr, int tranno, String trancode)
    {
        int result = 0;

        AccountingModel oModFisBalance = new AccountingModel();
        ArrayList lsFisLedgerTran = new ArrayList();

        try
        {
            oModFisBalance = getFisBalance(comp, fyr, tranno, trancode, "");
            if (oModFisBalance.GetSetid > 0)
            {
                double debit = 0, credit = 0;
                lsFisLedgerTran = getFisLedgerTranList(comp, fyr, 0, "", 0, "", oModFisBalance.GetSettranno, oModFisBalance.GetSettrancode, "");
                for (int i = 0; i < lsFisLedgerTran.Count; i++)
                {
                    AccountingModel oModLineItem = (AccountingModel)lsFisLedgerTran[i];
                    debit = debit + oModLineItem.GetSetdebit;
                    credit = credit + oModLineItem.GetSetcredit;
                }
                oModFisBalance.GetSetdebit = debit;
                oModFisBalance.GetSetcredit = credit;
                //update order header
                result = updateFisBalance(oModFisBalance);
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-updateFisBalanceInfo: " + e.Message.ToString());
        }
        return result;
    }

    public int deleteFisBalance(String comp, String id)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_balance WHERE comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisBalance: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    /*
    public ArrayList getFisBalanceDetailsList(String comp, String fyr, Int64 balid, String accid, String acctype, int tranno, String trancode, String status)
    {
        ArrayList lsFisBalance = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.balanceid, a.tranno, a.trancode, a.trandate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.debit, a.credit, a.refno, a.remarks
                           FROM   fis_balance_details a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (balid > 0)
                {
                    query = query + " and  a.balanceid = ?balid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.balanceid, a.accid, b.acclevel ";
                //WriteToLogFile("AccountingController-getFisBalanceDetailsList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (balid > 0) cmd.Parameters.Add("?balid", MySqlDbType.Int64).Value = balid;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetbalid = replaceZero(dataReader, "balanceid");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "trandate");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    lsFisBalance.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalanceList: " + e.Message.ToString());
        }
        return lsFisBalance;
    }

    public AccountingModel getFisBalanceDetails(String comp, String fyr, Int64 id, Int64 balid, String accid, String acctype, int tranno, String trancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.balanceid, a.tranno, a.trancode, a.trandate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.debit, a.credit, a.refno, a.remarks
                           FROM   fis_balance_details a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (balid > 0)
                {
                    query = query + " and  a.balanceid = ?balid ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (acctype.Trim().Length > 0)
                {
                    query = query + " and  a.acctype = ?acctype ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.balanceid, a.accid, b.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (balid > 0) cmd.Parameters.Add("?balid", MySqlDbType.Int64).Value = balid;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (acctype.Trim().Length > 0) cmd.Parameters.Add("?acctype", MySqlDbType.VarChar).Value = acctype;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetbalid = replaceZero(dataReader, "balanceid");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "trandate");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisBalanceDetails: " + e.Message.ToString());
        }
        return modItem;
    }

    public int insertFisBalanceDetails(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_balance_details(comp, fyr, balanceid, tranno, trancode, trandate, accid, accdesc, currency, exrate, debit, credit, refno, remarks)
                           VALUES(?comp, ?fyr, ?balanceid, ?tranno, ?trancode, ?trandate, ?accid, ?accdesc, ?currency, ?exrate, ?debit, ?credit, ?refno, ?remarks)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?balanceid", MySqlDbType.Int64).Value = modItem.GetSetbalid;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int64).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisBalanceDetails: " + e.Message.ToString());
        }

        return result;
    }

    public int updateFisBalanceDetails(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_balance_details 
                           SET  fyr = ?fyr,
                                balanceid = ?balanceid, tranno = ?tranno, 
                                trancode = ?trancode, trandate = ?trandate, 
                                accid = ?accid, accdesc = ?accdesc, 
                                exrate = ?exrate, currency = ?currency, 
                                debit = ?debit, credit = ?credit, 
                                refno = ?refno, remarks = ?remarks
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?balanceid", MySqlDbType.Int64).Value = modItem.GetSetbalid;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int64).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-updateFisBalanceDetails: " + e.Message.ToString());
        }

        return result;
    }

    public int deleteFisBalanceDetails(String comp, Int64 id)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_balance_details WHERE comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisBalanceDetails: " + e.Message.ToString());
        }

        return result;
    }
    */

    #endregion

    #region/*** BEGIN FOR LEDGER***/

    public ArrayList getFisLedgerTranList(String comp, String fyr, Int64 id, String accid, int ledgerno, String ledgerdate, int tranno, String trancode, String status)
    {
        ArrayList lsFisLedgerTran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.ledgerno, date_format(a.ledgerdate,'%d-%m-%Y %H:%i:%s') str_ledgerdate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (ledgerdate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate = ?ledgerdate ";
                }
                if (ledgerno > 0)
                {
                    query = query + " and  a.ledgerno = " + ledgerno + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.ledgerno, a.accid, b.acclevel ";
                //if (tranno.Equals(202000005) && trancode.Equals("PAYMENT_VOUCHER"))
                //{
                //    WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                //}
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (ledgerdate.Length > 0)
                {
                    if (ledgerdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(ledgerdate, ukDtfi);
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSetledgerdate = replaceNull(dataReader, "str_ledgerdate");
                    modItem.GetSetledgerno = replaceZero(dataReader, "ledgerno");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                    lsFisLedgerTran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisLedgerTranList: " + e.Message.ToString());
        }
        return lsFisLedgerTran;
    }

    public ArrayList getFisLedgerTranList(String comp, String fyr, Int64 id, String accid, int ledgerno, String startdate, String enddate, int tranno, String trancode, String status)
    {
        ArrayList lsFisLedgerTran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.ledgerno, date_format(a.ledgerdate,'%d-%m-%Y %H:%i:%s') str_ledgerdate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (startdate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate >= ?startdate ";
                }
                if (enddate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate <= ?enddate ";
                }
                if (ledgerno > 0)
                {
                    query = query + " and  a.ledgerno = " + ledgerno + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.ledgerno, a.accid, b.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (startdate.Length > 0)
                {
                    if (startdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(startdate, ukDtfi);
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (enddate.Length > 0)
                {
                    if (enddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(enddate, ukDtfi);
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSetledgerdate = replaceNull(dataReader, "str_ledgerdate");
                    modItem.GetSetledgerno = replaceZero(dataReader, "ledgerno");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                    lsFisLedgerTran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisLedgerTranList: " + e.Message.ToString());
        }
        return lsFisLedgerTran;
    }

    public ArrayList getFisLedgerTranList(String comp, String fyr, Int64 id, String accid, String accumulatedaccid, int ledgerno, String startdate, String enddate, int tranno, String trancode, String status)
    {
        ArrayList lsFisLedgerTran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.ledgerno, date_format(a.ledgerdate,'%d-%m-%Y %H:%i:%s') str_ledgerdate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accumulatedaccid.Trim().Length > 0)
                {
                    query = query + " and  a.accid in ("+ accumulatedaccid + ")";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (startdate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate >= ?startdate ";
                }
                if (enddate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate <= ?enddate ";
                }
                if (ledgerno > 0)
                {
                    query = query + " and  a.ledgerno = " + ledgerno + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, b.acclevel, a.ledgerdate ";
                //WriteToLogFile("AccountingController-getFisLedgerTranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (startdate.Length > 0)
                {
                    if (startdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(startdate, ukDtfi);
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (enddate.Length > 0)
                {
                    if (enddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(enddate, ukDtfi);
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSetledgerdate = replaceNull(dataReader, "str_ledgerdate");
                    modItem.GetSetledgerno = replaceZero(dataReader, "ledgerno");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                    lsFisLedgerTran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisLedgerTranList: " + e.Message.ToString());
        }
        return lsFisLedgerTran;
    }

    public ArrayList getFisLedgerTranList(String comp, String fyr, Int64 id, String accid, String accumulatedaccid, int ledgerno, String startdate, String enddate, int tranno, String trancode, String excludeclosing, String status)
    {
        ArrayList lsFisLedgerTran = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.ledgerno, date_format(a.ledgerdate,'%d-%m-%Y %H:%i:%s') str_ledgerdate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (accumulatedaccid.Trim().Length > 0)
                {
                    query = query + " and  a.accid in (" + accumulatedaccid + ")";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (startdate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate >= ?startdate ";
                }
                if (enddate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate <= ?enddate ";
                }
                if (ledgerno > 0)
                {
                    query = query + " and  a.ledgerno = " + ledgerno + " ";
                }
                if (excludeclosing.Trim().Length > 0)
                {
                    if (excludeclosing.Equals("Y"))
                    {
                        query = query + " and  a.trancode <> 'CLOSING_BALANCE' ";
                    }
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, b.acclevel, a.ledgerdate, a.id ";
                //WriteToLogFile("AccountingController-getFisLedgerTranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (startdate.Length > 0)
                {
                    if (startdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(startdate, ukDtfi);
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?startdate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (enddate.Length > 0)
                {
                    if (enddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(enddate, ukDtfi);
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?enddate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSetledgerdate = replaceNull(dataReader, "str_ledgerdate");
                    modItem.GetSetledgerno = replaceZero(dataReader, "ledgerno");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetparentdesc = getFISCOATranParentDesc(modItem.GetSetcomp, modItem.GetSetfyr, modItem.GetSetparentid, modItem.GetSetacclevel);
                    lsFisLedgerTran.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisLedgerTranList: " + e.Message.ToString());
        }
        return lsFisLedgerTran;
    }

    public AccountingModel getFisLedgerTran(String comp, String fyr, Int64 id, String accid, int ledgerno, String ledgerdate, int tranno, String trancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.ledgerno, date_format(a.ledgerdate,'%d-%m-%Y %H:%i:%s') str_ledgerdate, a.accid, a.accdesc, b.parentid, b.accgroup, b.acclevel, b.endlevel, b.acctype,
                                  b.acccat, b.acccode, b.accnumber, a.currency, a.exrate, a.debit, a.credit, a.refno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate
                           FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = ?id ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  a.accid = ?accid ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = " + tranno + " ";
                }
                if (ledgerdate.Trim().Length > 0)
                {
                    query = query + " and  a.ledgerdate = ?ledgerdate ";
                }
                if (ledgerno > 0)
                {
                    query = query + " and  a.ledgerno = " + ledgerno + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.accid, b.acclevel ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (id > 0) cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (ledgerdate.Length > 0)
                {
                    if (ledgerdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(ledgerdate, ukDtfi);
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSetledgerdate = replaceNull(dataReader, "str_ledgerdate");
                    modItem.GetSetledgerno = replaceZero(dataReader, "ledgerno");
                    modItem.GetSetaccid = replaceNull(dataReader, "accid");
                    modItem.GetSetaccdesc = replaceNull(dataReader, "accdesc");
                    modItem.GetSetparentid = replaceNull(dataReader, "parentid");
                    modItem.GetSetaccgroup = replaceNull(dataReader, "accgroup");
                    modItem.GetSetacclevel = replaceZero(dataReader, "acclevel");
                    modItem.GetSetendlevel = replaceNull(dataReader, "endlevel");
                    modItem.GetSetacctype = replaceNull(dataReader, "acctype");
                    modItem.GetSetacccategory = replaceNull(dataReader, "acccat");
                    modItem.GetSetacccode = replaceNull(dataReader, "acccode");
                    modItem.GetSetaccnumber = replaceNull(dataReader, "accnumber");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getFisFisLedgerTran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return modItem;
    }

    public int insertFisLedgerTran(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_ledger(comp, fyr, tranno, trancode, ledgerdate, ledgerno, accid, accdesc, currency, exrate, debit, credit, refno, remarks,
                                                  status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?fyr, ?tranno, ?trancode, ?ledgerdate, ?ledgerno, ?accid, ?accdesc, ?currency, ?exrate, ?debit, ?credit, ?refno, ?remarks,
                                  ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int64).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSetledgerdate.Length > 0)
                {
                    if (modItem.GetSetledgerdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetledgerdate, ukDtfi);
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?ledgerno", MySqlDbType.Int16).Value = modItem.GetSetledgerno;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController -insertFisLedgerTran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateFisLedgerTran(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_ledger
                           SET fyr = ?fyr, tranno = ?tranno, trancode = ?trancode, ledgerdate = ?ledgerdate, 
                               ledgerno = ?ledgerno, accid = ?accid, accdesc = ?accdesc, currency = ?currency, 
                               exrate = ?exrate, debit = ?debit, credit = ?credit, refno = ?refno, remarks = ?remarks,
                               status = ?status, createdby = ?createdby, createddate = ?createddate, 
                               confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int64).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSetledgerdate.Length > 0)
                {
                    if (modItem.GetSetledgerdate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetledgerdate, ukDtfi);
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?ledgerdate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?ledgerno", MySqlDbType.Int16).Value = modItem.GetSetledgerno;
                cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = modItem.GetSetaccid;
                cmd.Parameters.Add("?accdesc", MySqlDbType.VarChar).Value = modItem.GetSetaccdesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-updateFisLedgerTran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int deleteFisLedgerTran(String comp, Int64 id)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" DELETE FROM fis_ledger WHERE comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-deleteFisLedgerTran: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }


    #endregion

    #region/*** BEGIN FOR POSTING***/

    public ArrayList getPostingDataList(String comp, String fyr, String datefrom, String dateto, int tranno, String trancode, String refno, Int64 id, String status)
    {
        ArrayList lsFisBalance = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @"  select listing.comp, listing.fyr, listing.refno, listing.lineno, listing.trancode, listing.trantype, listing.bpid, listing.bpdesc, listing.trandate, date_format(listing.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, listing.tranamount, 
	                               post.id, post.tranno, post.trandesc, post.currency, post.exrate, post.debit, post.credit, post.status,
                                   post.createdby, post.createddate, post.confirmedby, post.confirmeddate, post.cancelledby, post.cancelleddate
                            from (
                            select a.comp, date_format(a.confirmeddate,'%Y') fyr, a.invoiceno refno, b.lineno, a.invoicecat trancode, a.invoicetype trantype, a.bpid, a.bpdesc, a.confirmeddate trandate, b.totalinvoice tranamount
                            from   invoice_header a, invoice_details b
                            where  a.comp is not null
                            and    a.comp = b.comp
                            and    a.invoiceno = b.invoiceno
                            and    a.status = 'CONFIRMED'
                            and    b.totalinvoice > 0
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(a.confirmeddate,'%Y') fyr, a.expensesno refno, b.lineno, a.expensescat trancode, a.expensestype trantype, a.bpid, a.bpdesc, a.confirmeddate trandate, b.totalexpenses tranamount
                            from   expenses_header a, expenses_details b
                            where  a.comp is not null
                            and    a.comp = b.comp
                            and    a.expensesno = b.expensesno
                            and    a.status = 'CONFIRMED'
                            and    b.totalexpenses > 0
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(confirmeddate,'%Y') fyr, a.payrcptno refno, b.lineno lineno, CONCAT(a.payrcpttype,'_',b.paytype) trancode, (select invoicetype from invoice_header c where c.comp = b.comp and c.invoiceno = b.invoiceno) trantype, a.bpid bpid, a.bpdesc bpdesc, a.confirmeddate trandate, b.payrcptprice tranamount
                            from   payrcpt_header a, payrcpt_details b
                            where  a.comp is not null
                            and    a.comp = b.comp
                            and    a.payrcptno = b.payrcptno
                            and    a.status = 'CONFIRMED'
                            and    b.payrcptprice > 0
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(confirmeddate,'%Y') fyr, a.paypaidno refno, b.lineno lineno, CONCAT(a.paypaidtype,'_',b.paytype) trancode, (select expensestype from expenses_header c where c.comp = b.comp and c.expensesno = b.expensesno) trantype, a.bpid bpid, a.bpdesc bpdesc, a.confirmeddate trandate, b.paypaidprice tranamount
                            from   paypaid_header a, paypaid_details b
                            where  a.comp is not null
                            and    a.comp = b.comp
                            and    a.paypaidno = b.paypaidno
                            and    a.status = 'CONFIRMED'
                            and    b.paypaidprice > 0
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(a.confirmeddate,'%Y') fyr, a.shipmentno tranno, b.lineno lineno, c.transtype trancode, '' trantype, b.itemno bpid, b.itemdesc bpdesc, a.confirmeddate trandate, c.transprice * b.shipment_quantity tranamount
                            from   shipment_header a, item i, shipment_details b inner join item_stock_transactions c on b.comp = c.comp and b.shipmentno = c.transno and b.itemno = c.itemno and b.lineno = c.trans_lineno
                            where  a.comp is not null 
                            and    a.status = 'CONFIRMED'
                            and    a.comp = b.comp
                            and    a.shipmentno = b.shipmentno
                            and    i.comp = b.comp
                            and    i.itemno = b.itemno
                            and    i.itemcat = 'INVENTORY'
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(a.confirmeddate,'%Y') fyr, a.receiptno tranno, b.lineno lineno, c.transtype trancode, '' trantype, b.itemno bpid, b.itemdesc bpdesc, a.confirmeddate trandate, c.transprice * b.receipt_quantity tranamount
                            from   receipt_header a, item i, receipt_details b inner join item_stock_transactions c on b.comp = c.comp and b.receiptno = c.transno and b.itemno = c.itemno and b.lineno = c.trans_lineno
                            where  a.comp is not null 
                            and    a.status = 'CONFIRMED'
                            and    a.comp = b.comp
                            and    a.receiptno = b.receiptno
                            and    i.comp = b.comp
                            and    i.itemno = b.itemno
                            and    i.itemcat = 'INVENTORY'
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(a.confirmeddate,'%Y') fyr, a.adjustmentno tranno, b.lineno lineno, c.transtype trancode, '' trantype, b.itemno bpid, b.itemdesc bpdesc, a.confirmeddate trandate, c.transprice * b.qtyvariance tranamount
                            from   adjustment_header a, item i, adjustment_details b inner join item_stock_transactions c on b.comp = c.comp and b.adjustmentno = c.transno and b.itemno = c.itemno and b.lineno = c.trans_lineno
                            where  a.comp is not null 
                            and    a.status = 'CONFIRMED'
                            and    a.comp = b.comp
                            and    a.adjustmentno = b.adjustmentno
                            and    i.comp = b.comp
                            and    i.itemno = b.itemno
                            and    i.itemcat = 'INVENTORY'
                            and    a.comp = ?comp
                            UNION ALL
                            select a.comp, date_format(a.trandate,'%Y') fyr, a.tranno tranno, b.id lineno, a.trancode trancode, '' trantype, i.assetno bpid, i.assetdesc bpdesc, a.trandate trandate, b.tranvalue tranamount
                            from   asset_tran_header a, asset i, asset_tran_details b 
                            where  a.comp is not null 
                            and    a.status = 'CONFIRMED'
                            and    a.comp = b.comp
                            and    a.tranno = b.tranno
                            and    a.trancode = b.trancode
                            and    i.comp = b.comp
                            and    i.assetno = b.assetno
                            and    a.trancode in ('REGCOST','DEPCOST')
                            and    a.comp = ?comp
                            ) as listing left join fis_posting as post on listing.comp = post.comp and listing.fyr = post.fyr and listing.trancode = post.trancode and listing.refno = post.refno and listing.lineno = post.lineno
                            where listing.comp is not null
                            and   listing.trandate >= (select min(trandate) from fis_balance where comp = ?comp and fyr = ?fyr and trancode = 'OPENING_BALANCE' and status <> 'CANCELLED')
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  listing.comp = ?comp ";
                }
                else
                {
                    query = query + " and  listing.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  listing.fyr = ?fyr ";
                }
                if (datefrom.Trim().Length > 0)
                {
                    query = query + " and  listing.trandate >= ?datefrom ";
                }
                if (dateto.Trim().Length > 0)
                {
                    query = query + " and  listing.trandate <= ?dateto ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  listing.trancode = ?trancode ";
                }
                if (tranno > 0)
                {
                    query = query + " and  listing.tranno = " + tranno + " ";
                }
                if (refno.Trim().Length > 0)
                {
                    query = query + " and  listing.refno = ?refno ";
                }
                if (id > 0)
                {
                    query = query + " and  post.id = " + id + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  post.status = ?status ";
                }
                query = query + @" order by listing.comp, listing.trandate, listing.refno, listing.lineno ";
                //WriteToLogFile("AccountingController-getFisBalanceDetailsList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (datefrom.Length > 0)
                {
                    if (datefrom.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(datefrom);
                        cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (dateto.Length > 0)
                {
                    if (dateto.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(dateto);
                        cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSetlineno = replaceZero(dataReader, "lineno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrantype = replaceNull(dataReader, "trantype");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSettranamount = replaceDoubleZero(dataReader, "tranamount");
                    modItem.GetSetbpid = replaceNull(dataReader, "bpid");
                    modItem.GetSetbpdesc = replaceNull(dataReader, "bpdesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsFisBalance.Add(modItem);
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getPostingDataList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsFisBalance;
    }

    public AccountingModel getPostingData(String comp, String fyr, int tranno, String trancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.trandate, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, 
                                  a.trandesc, a.currency, a.exrate, a.tranamount, a.debit, a.credit, a.refno, a.lineno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_posting a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = ?tranno ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (tranno > 0) cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = tranno;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSettranamount = replaceDoubleZero(dataReader, "tranamount");
                    modItem.GetSetdebit = replaceDoubleZero(dataReader, "debit");
                    modItem.GetSetcredit = replaceDoubleZero(dataReader, "credit");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetlineno = replaceZero(dataReader, "lineno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getPostingData: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modItem;
    }

    public int insertPostingData(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_posting(comp, fyr, tranno, trancode, trandate, trandesc, currency, exrate, debit, credit, refno, lineno, remarks,
                                                    status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?fyr, ?tranno, ?trancode, ?trandate, ?trandesc, ?currency, ?exrate, ?debit, ?credit, ?refno, ?lineno, ?remarks, 
                                  ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?lineno", MySqlDbType.Int16).Value = modItem.GetSetlineno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-insertPostingData: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updatePostingData(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_posting 
                           SET  fyr = ?fyr, tranno = ?tranno, 
                                trancode = ?trancode, trandate = ?trandate, 
                                trandesc = ?trandesc, currency = ?currency, 
                                exrate = ?exrate,
                                refno = ?refno, lineno = ?lineno, remarks = ?remarks, 
                                debit = ?debit, credit = ?credit, 
                                status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?debit", MySqlDbType.Double).Value = modItem.GetSetdebit;
                cmd.Parameters.Add("?credit", MySqlDbType.Double).Value = modItem.GetSetcredit;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?lineno", MySqlDbType.Int16).Value = modItem.GetSetlineno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-updatePostingData: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    #endregion

    #region/*** BEGIN FOR JOURNAL***/

    public ArrayList getJournalEntryList(String comp, String fyr, String datefrom, String dateto, int tranno, String trancode, String refno, Int64 id, String status)
    {
        ArrayList lsItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.trandate, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, 
                                  a.trandesc, a.currency, a.exrate, a.tranamount, a.refno, a.lineno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_journal a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = ?tranno ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (tranno > 0) cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = tranno;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSettranamount = replaceDoubleZero(dataReader, "tranamount");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetlineno = replaceZero(dataReader, "lineno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItem.Add(modItem);
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getJournalEntryList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsItem;
    }

    public ArrayList getJournalEntryList(String comp, String fyr, String datefrom, String dateto, int tranno, String trancode, String accid, String refno, Int64 id, String status)
    {
        ArrayList lsItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.trancat, a.trantype, a.bpid, a.trandate, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, 
                                  a.trandesc, a.currency, a.exrate, a.tranamount, a.refno, a.lineno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_journal a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (datefrom.Trim().Length > 0)
                {
                    query = query + " and  a.trandate >= ?datefrom ";
                }
                if (dateto.Trim().Length > 0)
                {
                    query = query + " and  a.trandate <= ?dateto ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = ?tranno ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (accid.Trim().Length > 0)
                {
                    query = query + " and  EXISTS (SELECT id FROM fis_ledger b WHERE a.comp = b.comp AND a.fyr = b.fyr AND a.tranno = b.tranno AND a.trancode = b.trancode AND b.accid = ?accid) ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (datefrom.Length > 0)
                {
                    if (datefrom.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(datefrom, ukDtfi);
                        cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (dateto.Length > 0)
                {
                    if (dateto.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(dateto, ukDtfi);
                        cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = DateTime.Now;
                }
                if (tranno > 0) cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = tranno;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (accid.Trim().Length > 0) cmd.Parameters.Add("?accid", MySqlDbType.VarChar).Value = accid;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modItem = new AccountingModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrancat = replaceNull(dataReader, "trancat");
                    modItem.GetSettrantype = replaceNull(dataReader, "trantype");
                    modItem.GetSetbpid = replaceNull(dataReader, "bpid");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSettranamount = replaceDoubleZero(dataReader, "tranamount");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetlineno = replaceZero(dataReader, "lineno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItem.Add(modItem);
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getJournalEntryList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsItem;
    }

    public AccountingModel getJournalEntryDetails(String comp, String fyr, int tranno, String trancode, String status)
    {
        AccountingModel modItem = new AccountingModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" SELECT a.id, a.comp, a.fyr, a.tranno, a.trancode, a.trancat, a.trantype, a.bpid, a.trandate, date_format(a.trandate,'%d-%m-%Y %H:%i:%s') str_trandate, 
                                  a.trandesc, a.currency, a.exrate, a.tranamount, a.refno, a.lineno, a.remarks,
                                  a.status, a.createdby, a.createddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate 
                           FROM   fis_journal a
                           WHERE  a.comp is not null
                         ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = ?comp ";
                }
                else
                {
                    query = query + " and  a.comp = '000' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = ?fyr ";
                }
                if (tranno > 0)
                {
                    query = query + " and  a.tranno = ?tranno ";
                }
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = ?trancode ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = ?status ";
                }
                query = query + @" order by a.comp, a.fyr, a.trandate ";
                //WriteToLogFile("AccountingController-getFisCOATranList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (comp.Trim().Length > 0) cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                if (fyr.Trim().Length > 0) cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                if (tranno > 0) cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = tranno;
                if (trancode.Trim().Length > 0) cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                if (status.Trim().Length > 0) cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSettranno = replaceZero(dataReader, "tranno");
                    modItem.GetSettrancode = replaceNull(dataReader, "trancode");
                    modItem.GetSettrancat = replaceNull(dataReader, "trancat");
                    modItem.GetSettrantype = replaceNull(dataReader, "trantype");
                    modItem.GetSetbpid = replaceNull(dataReader, "bpid");
                    modItem.GetSettrandate = replaceNull(dataReader, "str_trandate");
                    modItem.GetSettrandesc = replaceNull(dataReader, "trandesc");
                    modItem.GetSetcurrency = replaceNull(dataReader, "currency");
                    modItem.GetSetexrate = replaceDoubleZero(dataReader, "exrate");
                    modItem.GetSettranamount = replaceDoubleZero(dataReader, "tranamount");
                    modItem.GetSetrefno = replaceNull(dataReader, "refno");
                    modItem.GetSetlineno = replaceZero(dataReader, "lineno");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-getJournalEntryDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modItem;
    }

    public int insertJournalEntryDetails(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" INSERT INTO fis_journal(comp, fyr, tranno, trancode, trandate, trandesc, trancat, trantype, bpid, currency, exrate, tranamount, refno, lineno, remarks,
                                                    status, createdby, createddate, confirmedby, confirmeddate, cancelledby, cancelleddate)
                           VALUES(?comp, ?fyr, ?tranno, ?trancode, ?trandate, ?trandesc, ?trancat, ?trantype, ?bpid, ?currency, ?exrate, ?tranamount, ?refno, ?lineno, ?remarks, 
                                  ?status, ?createdby, ?createddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?trancat", MySqlDbType.VarChar).Value = modItem.GetSettrancat;
                cmd.Parameters.Add("?trantype", MySqlDbType.VarChar).Value = modItem.GetSettrantype;
                cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = modItem.GetSetbpid;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?tranamount", MySqlDbType.Double).Value = modItem.GetSettranamount;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?lineno", MySqlDbType.Int16).Value = modItem.GetSetlineno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-insertJournalEntryDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    public int updateJournalEntryDetails(AccountingModel modItem)
    {
        int result = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @" UPDATE fis_journal 
                           SET  fyr = ?fyr, tranno = ?tranno, 
                                trancode = ?trancode, trandate = ?trandate, 
                                trandesc = ?trandesc, currency = ?currency,
                                trancat = ?trancat, trantype = ?trantype, bpid = ?bpid, 
                                exrate = ?exrate,
                                refno = ?refno, lineno = ?lineno, remarks = ?remarks, 
                                tranamount = ?tranamount, 
                                status = ?status, 
                                createdby = ?createdby, createddate = ?createddate, 
                                confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, 
                                cancelledby = ?cancelledby, cancelleddate = ?cancelleddate
                           WHERE comp = ?comp and id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?tranno", MySqlDbType.Int16).Value = modItem.GetSettranno;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = modItem.GetSettrancode;
                if (modItem.GetSettrandate.Length > 0)
                {
                    if (modItem.GetSettrandate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSettrandate, ukDtfi);
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?trandate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?trandesc", MySqlDbType.VarChar).Value = modItem.GetSettrandesc;
                cmd.Parameters.Add("?trancat", MySqlDbType.VarChar).Value = modItem.GetSettrancat;
                cmd.Parameters.Add("?trantype", MySqlDbType.VarChar).Value = modItem.GetSettrantype;
                cmd.Parameters.Add("?bpid", MySqlDbType.VarChar).Value = modItem.GetSetbpid;
                cmd.Parameters.Add("?currency", MySqlDbType.VarChar).Value = modItem.GetSetcurrency;
                cmd.Parameters.Add("?exrate", MySqlDbType.Double).Value = modItem.GetSetexrate;
                cmd.Parameters.Add("?tranamount", MySqlDbType.Double).Value = modItem.GetSettranamount;
                cmd.Parameters.Add("?refno", MySqlDbType.VarChar).Value = modItem.GetSetrefno;
                cmd.Parameters.Add("?lineno", MySqlDbType.Int16).Value = modItem.GetSetlineno;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = modItem.GetSetcreatedby;
                if (modItem.GetSetcreatedby.Length > 0)
                {
                    if (modItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcreateddate);
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?createddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = modItem.GetSetconfirmedby;
                if (modItem.GetSetconfirmedby.Length > 0)
                {
                    if (modItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetconfirmeddate);
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?confirmeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = modItem.GetSetcancelledby;
                if (modItem.GetSetcancelledby.Length > 0)
                {
                    if (modItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetcancelleddate);
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?cancelleddate", MySqlDbType.DateTime).Value = null;
                }

                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("AccountingController-updateJournalEntryDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }

        return result;
    }

    #endregion

    #region/*** BEGIN FOR GENERAL***/

    public ArrayList getFisFYRList(string currcomp)
    {
        ArrayList lsFisFYR = new ArrayList();
        if (currcomp.Length > 0)
        {
            lsFisFYR.Add(new { GetSetfyrid = "2019", GetSetfyrdesc = "2019" });
            lsFisFYR.Add(new { GetSetfyrid = "2020", GetSetfyrdesc = "2020" });
            lsFisFYR.Add(new { GetSetfyrid = "2021", GetSetfyrdesc = "2021" });
            lsFisFYR.Add(new { GetSetfyrid = "2022", GetSetfyrdesc = "2022" });
        }

        return lsFisFYR;
    }

    public ArrayList getRunningNoList(String comp, String fyr, String trancode, String status)
    {
        ArrayList lsRunningNoList = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.trancode, a.initial, a.runno, a.status ";
                query = query + " from   fis_running_number a ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " and  a.comp = '" + comp + "' ";
                query = query + " and  a.fyr = '" + fyr + "' ";
                if (trancode.Trim().Length > 0)
                {
                    query = query + " and  a.trancode = '" + trancode + "' ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.trancode ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modRunNo = new AccountingModel();
                    modRunNo.GetSetid = replaceZero(dataReader, "id");
                    modRunNo.GetSetcomp = replaceNull(dataReader, "comp");
                    modRunNo.GetSetfyr = replaceNull(dataReader, "fyr");
                    modRunNo.GetSettrancode = replaceNull(dataReader, "trancode");
                    modRunNo.GetSetinitial = replaceNull(dataReader, "initial");
                    modRunNo.GetSetrunno = replaceZero(dataReader, "runno");
                    modRunNo.GetSetstatus = replaceNull(dataReader, "status");
                    lsRunningNoList.Add(modRunNo);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-getRunningNoList: " + e.Message.ToString());
        }

        return lsRunningNoList;
    }

    public int createNewRunningNoList(String comp, String fyr)
    {
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        int success = 0;

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = @"
                        SELECT createfiscalyearaccounting(?comp, ?fyr) result;
                        ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    success = replaceZero(dataReader, "result");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-createNewRunningNoList: " + e.Message.ToString());
        }
        return success;
    }

    public int getNextRunningNo(String comp, String fyr, String trancode, String status)
    {
        int nextrunno = 0;
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.trancode, a.initial, a.runno, a.status ";
                query = query + " from   fis_running_number a ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " and  a.comp = '" + comp + "' ";
                query = query + " and  a.fyr = '" + fyr + "' ";
                query = query + " and  a.trancode = '" + trancode + "' ";
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.trancode ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountingModel modRunNo = new AccountingModel();
                    modRunNo.GetSetid = replaceZero(dataReader, "id");
                    modRunNo.GetSetcomp = replaceNull(dataReader, "comp");
                    modRunNo.GetSetfyr = replaceNull(dataReader, "fyr");
                    modRunNo.GetSettrancode = replaceNull(dataReader, "trancode");
                    modRunNo.GetSetinitial = replaceNull(dataReader, "initial");
                    modRunNo.GetSetrunno = replaceZero(dataReader, "runno") + 1;
                    modRunNo.GetSetstatus = replaceNull(dataReader, "status");
                    nextrunno = int.Parse(modRunNo.GetSetfyr + modRunNo.GetSetrunno.ToString().PadLeft(5, '0'));
                }
                dataReader.Close();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-getNextRunningNo: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return nextrunno;
    }

    public void updateNextRunningNo(String comp, String fyr, String trancode, String status)
    {
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE fis_running_number ";
                query = query + " SET runno = runno + 1 ";
                query = query + " WHERE comp = ?comp ";
                query = query + " AND fyr = ?fyr ";
                query = query + " AND trancode = ?trancode ";
                query = query + " AND status = ?status ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?trancode", MySqlDbType.VarChar).Value = trancode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-updateNextRunningNo: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
    }

    public String getNextSecond(String sDateTimeIn, Double dNext)
    {

        String sDateTimeOut = "";

        try
        {
            DateTime datetime = Convert.ToDateTime(sDateTimeIn, ukDtfi);
            //DateTime datetime = Convert.ToDateTime(sDateTimeIn);
            sDateTimeOut = datetime.AddSeconds(dNext).ToString("dd-MM-yyyy HH:mm:ss");

        }
        catch (Exception e)
        {
            WriteToLogFile("MainController-getNextSecond: " + e.Message.ToString());
        }
        return sDateTimeOut;
    }

    public ArrayList tokenString(String sStr, String sParse)
    {
        ArrayList lsToken = new ArrayList();
        String s;
        int i, j;
        int nLen;
        nLen = sParse.Length;

        i = 0;
        while (true)
        {
            j = sStr.IndexOf(sParse, i);
            if (j < 0)
            {
                // last
                s = replaceNull(sStr.Substring(i));

                if (s.Length > 0)
                    lsToken.Add(s);

                break;
            }
            s = replaceNull(sStr.Substring(i, j - i));
            lsToken.Add(s);
            i = j + nLen;
        }
        return lsToken;
    }
    public String replaceNull(String sString)
    {
        String _String = "";
        if ((sString == null) || (sString.Trim().Equals("null")))
            _String = "";
        else
            _String = sString.Trim();

        return _String;
    } // replaceNull
    public int replaceIntZero(String sString)
    {
        int _result = 0;
        if ((sString == null) || (sString.Trim().Equals("null")))
            _result = 0;
        else
            _result = int.Parse(sString.Trim());

        return _result;
    } // replaceNull
    public Double replaceDoubleZero(String sString)
    {
        Double _result = 0;
        if ((sString == null) || (sString.Trim().Equals("null")))
            _result = 0;
        else
            _result = Double.Parse(sString.Trim());

        return _result;
    } // replaceNull

    public String RegExReplace(String inputstring, String replaceValue)
    {

        String pattern = @"[\r|\n|\t]";
        //String pattern = @"\t\n\r";

        // Specify your replace string value here.

        String outString = Regex.Replace(inputstring, pattern, replaceValue);

        return outString;

    }

    public String replaceStr(String sStrValue, String sStrOld, String sStrNew)
    {
        String sStrNewValue = sStrValue;
        if (sStrValue.Length > 0 && sStrOld.Length > 0)
        {
            sStrNewValue = sStrValue.Replace(sStrOld, sStrNew);
        }
        return sStrNewValue;
    }

    public String isContains(ArrayList arrayString, String submoduleid)
    {
        String display = "none";
        bool contains = false;
        if (arrayString.Count > 0)
        {
            contains = arrayString.Contains(submoduleid);
        }
        if (contains)
        {
            display = "";
        }
        return display;
    }

    public bool isBoolContains(ArrayList arrayString, String submoduleid)
    {
        bool contains = false;
        if (arrayString.Count > 0)
        {
            contains = arrayString.Contains(submoduleid);
        }
        return contains;
    }

    public String replaceNull(MySqlDataReader oDataReader, String sField)
    {
        if (oDataReader[sField] == DBNull.Value)
        {
            return "";
        }
        else
        {
            if (oDataReader[sField].ToString().Trim().ToUpper().Equals("NULL"))
            {
                return "";
            }
            else
            {
                return oDataReader[sField].ToString();
            }
        }
    }

    public String replaceNull(MySqlDataReader oDataReader, int iField)
    {
        if (oDataReader.IsDBNull(iField))
        {
            return "";
        }
        else
        {
            if (oDataReader[iField].ToString().Trim().ToUpper().Equals("NULL"))
            {
                return "";
            }
            else
            {
                return oDataReader[iField].ToString();
            }
        }
    }

    public int replaceZero(MySqlDataReader oDataReader, String sField)
    {
        if (oDataReader[sField] == DBNull.Value)
            return 0;
        else
            return int.Parse(oDataReader[sField].ToString());
    }
    public int replaceZero(MySqlDataReader oDataReader, int iField)
    {
        if (oDataReader.IsDBNull(iField))
            return 0;
        else
            return int.Parse(oDataReader[iField].ToString());
    }

    public double replaceDoubleZero(MySqlDataReader oDataReader, String sField)
    {
        if (oDataReader[sField] == DBNull.Value)
            return 0;
        else
            return double.Parse(oDataReader[sField].ToString());
    }
    public double replaceDoubleZero(MySqlDataReader oDataReader, int iField)
    {
        if (oDataReader.IsDBNull(iField))
            return 0;
        else
            return double.Parse(oDataReader[iField].ToString());
    }
    public decimal replaceDecimalZero(MySqlDataReader oDataReader, String sField)
    {
        if (oDataReader[sField] == DBNull.Value)
            return 0;
        else
            return decimal.Parse(oDataReader[sField].ToString());
    }
    public decimal replaceDecimalZero(MySqlDataReader oDataReader, int iField)
    {
        if (oDataReader.IsDBNull(iField))
            return 0;
        else
            return decimal.Parse(oDataReader[iField].ToString());
    }

    public double RoundCorrect(double d, int decimals)
    {
        double multiplier = Math.Pow(10, decimals);
        if (d < 0)
            multiplier *= -1;
        return Math.Floor((d * multiplier) + 0.5) / multiplier;
    }
    
    public AccountingModel getAlertMessage(String message)
    {
        AccountingModel oAlertMssg = new AccountingModel();
        if (message.Length > 0)
        {
            ArrayList lsAlertMssg = tokenString(message, "|");
            if (lsAlertMssg.Count > 0)
            {
                oAlertMssg.GetSetalertstatus = lsAlertMssg[0].ToString();
                oAlertMssg.GetSetalertmessage = lsAlertMssg[1].ToString();
            }
        }
        return oAlertMssg;
    }

    public void WriteToLogFile(string strMessage)
    {
        //Open a file for writing
        //Get a StreamWriter class that can be used to write to the file
        if (strMessage.Length > 0)
        {
            string strlogFile = ConfigurationSettings.AppSettings["LogFile"];
            strlogFile = MyHttpApplication.GetAppDataPath(strlogFile);
            //string strlogFile = sErrorLog;
            if (strlogFile.Trim().Length > 0)
            {
                System.IO.StreamWriter objStreamWriter;
                objStreamWriter = System.IO.File.AppendText(strlogFile);

                objStreamWriter.WriteLine(DateTime.Now.ToString() + ": " + strMessage);

                //Close the stream
                objStreamWriter.Close();
            }
        }
    }

    public T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException("The type must be serializable.", "source");
        }

        if (Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    class MyHttpApplication : HttpApplication
    {
        // of course you can fetch&store the value at Application_Start
        public static string GetAppDataPath(String sLogFile)
        {
            try
            {
                return HttpContext.Current.Server.MapPath(sLogFile);
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    #endregion
}