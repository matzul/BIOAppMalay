using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for HRController
/// </summary>
public class HRController
{
    public DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
    public DateTimeFormatInfo ukDtfi = new CultureInfo("en-GB", false).DateTimeFormat;

    private String sErrorLog = "";

    public HRController()
    {
        sErrorLog = "";
    }

    public HRController(String _sErrorLog)
    {
        sErrorLog = _sErrorLog;
    }

    #region /*** BEGIN FOR HR ***/

    public ArrayList getCompDeptList(String comp, String deptid, String deptname)
    {
        ArrayList lsCompDept = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.dept_id, a.dept_name, a.dept_level, a.dept_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   department_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (deptid.Trim().Length > 0)
                {
                    query = query + " and  a.dept_id = '" + deptid + "' ";
                }
                if (deptname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.dept_name) like '%" + deptname + "%' ";
                }
                query = query + " order by a.comp, a.dept_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetname = replaceNull(dataReader, "dept_name");                    
                    modStaff.GetSetlevel = replaceZero(dataReader, "dept_level");                    
                    modStaff.GetSetreportto = replaceNull(dataReader, "dept_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompDept.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompDeptList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompDept;
    }
    
    public HRModel getCompDeptDetails(String comp, String deptid, String deptname)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.dept_id, a.dept_name, a.dept_level, a.dept_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   department_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (deptid.Trim().Length > 0)
                {
                    query = query + " and  a.dept_id = '" + deptid + "' ";
                }
                if (deptname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.dept_name) like '%" + deptname + "%' ";
                }
                query = query + " order by a.comp, a.dept_name ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetname = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "dept_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "dept_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompDeptDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public ArrayList getCompDeptReportTo(String comp, String deptid, String deptname, int deptlevel)
    {
        ArrayList lsCompDept = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.dept_id, a.dept_name, a.dept_level, a.dept_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   department_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (deptid.Trim().Length > 0)
                {
                    query = query + " and  a.dept_id = '" + deptid + "' ";
                }
                if (deptname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.dept_name) like '%" + deptname + "%' ";
                }
                if(deptlevel > 0)
                {
                    query = query + " and  a.dept_level < " + deptlevel + " ";
                }
                query = query + " order by a.comp, a.dept_name ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetname = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "dept_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "dept_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompDept.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompDeptList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompDept;
    }

    public String insertCompDept(HRModel oModDept)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO department_comp (comp, dept_id, dept_name, dept_level, dept_reportto, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?dept_id, ?dept_name, ?dept_level, ?dept_reportto, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModDept.GetSetcomp;
                cmd.Parameters.Add("?dept_id", MySqlDbType.VarChar).Value = oModDept.GetSetsid;
                cmd.Parameters.Add("?dept_name", MySqlDbType.VarChar).Value = oModDept.GetSetname;
                cmd.Parameters.Add("?dept_level", MySqlDbType.Int16).Value = oModDept.GetSetlevel;
                cmd.Parameters.Add("?dept_reportto", MySqlDbType.VarChar).Value = oModDept.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModDept.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModDept.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModDept.GetSetcreatedby;
                if (oModDept.GetSetcreatedby.Length > 0)
                {
                    if (oModDept.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModDept.GetSetmodifiedby;
                if (oModDept.GetSetmodifiedby.Length > 0)
                {
                    if (oModDept.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModDept.GetSetconfirmedby;
                if (oModDept.GetSetconfirmedby.Length > 0)
                {
                    if (oModDept.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModDept.GetSetcancelledby;
                if (oModDept.GetSetcancelledby.Length > 0)
                {
                    if (oModDept.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompDept: " + e.Message.ToString());
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

    public String updateCompDept(HRModel oModDept)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE department_comp ";
                query = query + " SET    dept_name = ?dept_name, dept_level = ?dept_level, dept_reportto = ?dept_reportto, remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND dept_id = ?dept_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModDept.GetSetcomp;
                cmd.Parameters.Add("?dept_id", MySqlDbType.VarChar).Value = oModDept.GetSetsid;
                cmd.Parameters.Add("?dept_name", MySqlDbType.VarChar).Value = oModDept.GetSetname;
                cmd.Parameters.Add("?dept_level", MySqlDbType.Int16).Value = oModDept.GetSetlevel;
                cmd.Parameters.Add("?dept_reportto", MySqlDbType.VarChar).Value = oModDept.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModDept.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModDept.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModDept.GetSetcreatedby;
                if (oModDept.GetSetcreatedby.Length > 0)
                {
                    if (oModDept.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModDept.GetSetmodifiedby;
                if (oModDept.GetSetmodifiedby.Length > 0)
                {
                    if (oModDept.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModDept.GetSetconfirmedby;
                if (oModDept.GetSetconfirmedby.Length > 0)
                {
                    if (oModDept.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModDept.GetSetcancelledby;
                if (oModDept.GetSetcancelledby.Length > 0)
                {
                    if (oModDept.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModDept.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompDept: " + e.Message.ToString());
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

    public String deleteCompDept(HRModel oModDept)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM department_comp ";
                query = query + " WHERE  comp = ?comp AND dept_id = ?dept_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModDept.GetSetcomp;
                cmd.Parameters.Add("?dept_id", MySqlDbType.VarChar).Value = oModDept.GetSetsid;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompDept: " + e.Message.ToString());
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

    public ArrayList getStaffEmployList(String comp, String staffno, String status)
    {
        ArrayList lsStaffEmploy = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.emp_type, a.emp_cat, a.emp_probation, a.emp_fromdate, date_format(a.emp_fromdate,'%d-%m-%Y') str_emp_fromdate, a.emp_todate, date_format(a.emp_todate,'%d-%m-%Y') str_emp_todate, a.emp_reportto, ";
                query = query + "        b.salute, b.name, b.nicno, c.dept_id, c.dept_name, d.gred_id, d.gred_name, e.pos_id, e.pos_name, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_employment a, staff_info b, department_comp c, grade_comp d, position_comp e ";
                query = query + " WHERE  a.comp is not NULL AND a.comp = b.comp AND a.comp = c.comp AND a.comp = d.comp AND a.comp = e.comp ";
                query = query + " AND  a.staffno = b.staffno ";
                query = query + " AND  a.emp_deptid = c.dept_id ";
                query = query + " AND  a.emp_gredid = d.gred_id ";
                query = query + " AND  a.emp_posid = e.pos_id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.staffno, a.emp_fromdate ";
                //WriteToLogFile("HRController-getStaffEmployList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSettype = replaceNull(dataReader, "emp_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "emp_cat");
                    modStaff.GetSetprobation = replaceZero(dataReader, "emp_probation");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "emp_fromdate");
                    modStaff.GetSetstr_fromdate = replaceNull(dataReader, "str_emp_fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "emp_todate");
                    modStaff.GetSetstr_todate = replaceNull(dataReader, "str_emp_todate");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsStaffEmploy.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffEmployList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffEmploy;
    }

    public ArrayList getStaffEmployList(String comp, String staffno, String staffname, String deptid, String gredid, String posid)
    {
        ArrayList lsStaffEmploy = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.emp_type, a.emp_cat, a.emp_probation, a.emp_fromdate, date_format(a.emp_fromdate,'%d-%m-%Y') str_emp_fromdate, a.emp_todate, date_format(a.emp_todate,'%d-%m-%Y') str_emp_todate, a.emp_reportto, ";
                query = query + "        b.salute, b.name, b.nicno, c.dept_id, c.dept_name, d.gred_id, d.gred_name, e.pos_id, e.pos_name, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_employment a, staff_info b, department_comp c, grade_comp d, position_comp e ";
                query = query + " WHERE  a.comp is not NULL AND a.comp = b.comp AND a.comp = c.comp AND a.comp = d.comp AND a.comp = e.comp ";
                query = query + " AND  a.staffno = b.staffno "; 
                query = query + " AND  a.emp_deptid = c.dept_id ";
                query = query + " AND  a.emp_gredid = d.gred_id ";
                query = query + " AND  a.emp_posid = e.pos_id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (staffname.Trim().Length > 0)
                {
                    query = query + " and  b.name = '" + staffname + "' ";
                }
                if (deptid.Trim().Length > 0)
                {
                    query = query + " and  c.dept_id = '" + deptid + "' ";
                }
                if (gredid.Trim().Length > 0)
                {
                    query = query + " and  d.gred_id = '" + gredid + "' ";
                }
                if (posid.Trim().Length > 0)
                {
                    query = query + " and  e.pos_id = '" + posid + "' ";
                }
                query = query + " order by a.comp, a.staffno, a.emp_fromdate ";
                //WriteToLogFile("HRController-getStaffEmployList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSettype = replaceNull(dataReader, "emp_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "emp_cat");
                    modStaff.GetSetprobation = replaceZero(dataReader, "emp_probation");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "emp_fromdate");
                    modStaff.GetSetstr_fromdate = replaceNull(dataReader, "str_emp_fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "emp_todate");
                    modStaff.GetSetstr_todate = replaceNull(dataReader, "str_emp_todate");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsStaffEmploy.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffEmployList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffEmploy;
    }

    public HRModel getStaffEmployDetails(String comp, String staffno, String staffname, String deptid, String gredid, String posid)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.emp_type, a.emp_cat, a.emp_probation, a.emp_fromdate, date_format(a.emp_fromdate,'%d-%m-%Y') str_emp_fromdate, a.emp_todate, date_format(a.emp_todate,'%d-%m-%Y') str_emp_todate, a.emp_reportto, ";
                query = query + "        b.salute, b.name, b.nicno, c.dept_id, c.dept_name, d.gred_id, d.gred_name, e.pos_id, e.pos_name, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_employment a, staff_info b, department_comp c, grade_comp d, position_comp e ";
                query = query + " WHERE  a.comp is not NULL AND a.comp = b.comp AND a.comp = c.comp AND a.comp = d.comp AND a.comp = e.comp ";
                query = query + " AND  a.staffno = b.staffno ";
                query = query + " AND  a.emp_deptid = c.dept_id ";
                query = query + " AND  a.emp_gredid = d.gred_id ";
                query = query + " AND  a.emp_posid = e.pos_id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (staffname.Trim().Length > 0)
                {
                    query = query + " and  b.name = '" + staffname + "' ";
                }
                if (deptid.Trim().Length > 0)
                {
                    query = query + " and  c.dept_id = '" + deptid + "' ";
                }
                if (gredid.Trim().Length > 0)
                {
                    query = query + " and  d.gred_id = '" + gredid + "' ";
                }
                if (posid.Trim().Length > 0)
                {
                    query = query + " and  e.pos_id = '" + posid + "' ";
                }
                query = query + " order by a.comp, a.staffno, a.emp_fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSettype = replaceNull(dataReader, "emp_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "emp_cat");
                    modStaff.GetSetprobation = replaceZero(dataReader, "emp_probation");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "emp_fromdate");
                    modStaff.GetSetstr_fromdate = replaceNull(dataReader, "str_emp_fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "emp_todate");
                    modStaff.GetSetstr_todate = replaceNull(dataReader, "str_emp_todate");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffEmployDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public HRModel getStaffEmployDetails(String comp, String staffno, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.emp_type, a.emp_cat, a.emp_probation, a.emp_fromdate, date_format(a.emp_fromdate,'%d-%m-%Y') str_emp_fromdate, a.emp_todate, date_format(a.emp_todate,'%d-%m-%Y') str_emp_todate, a.emp_reportto, ";
                query = query + "        b.salute, b.name, b.nicno, c.dept_id, c.dept_name, d.gred_id, d.gred_name, e.pos_id, e.pos_name, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_employment a, staff_info b, department_comp c, grade_comp d, position_comp e ";
                query = query + " WHERE  a.comp is not NULL AND a.comp = b.comp AND a.comp = c.comp AND a.comp = d.comp AND a.comp = e.comp ";
                query = query + " AND  a.staffno = b.staffno ";
                query = query + " AND  a.emp_deptid = c.dept_id ";
                query = query + " AND  a.emp_gredid = d.gred_id ";
                query = query + " AND  a.emp_posid = e.pos_id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.staffno, a.emp_fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSettype = replaceNull(dataReader, "emp_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "emp_cat");
                    modStaff.GetSetprobation = replaceZero(dataReader, "emp_probation");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "emp_fromdate");
                    modStaff.GetSetstr_fromdate = replaceNull(dataReader, "str_emp_fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "emp_todate");
                    modStaff.GetSetstr_todate = replaceNull(dataReader, "str_emp_todate");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffEmployDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public HRModel getStaffEmployDetails(String comp, String staffno)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.emp_type, a.emp_cat, a.emp_probation, a.emp_fromdate, date_format(a.emp_fromdate,'%d-%m-%Y') str_emp_fromdate, a.emp_todate, date_format(a.emp_todate,'%d-%m-%Y') str_emp_todate, a.emp_reportto, ";
                query = query + "        b.salute, b.name, b.nicno, c.dept_id, c.dept_name, d.gred_id, d.gred_name, e.pos_id, e.pos_name, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_employment a, staff_info b, department_comp c, grade_comp d, position_comp e ";
                query = query + " WHERE  a.comp is not NULL AND a.comp = b.comp AND a.comp = c.comp AND a.comp = d.comp AND a.comp = e.comp ";
                query = query + " AND  a.staffno = b.staffno ";
                query = query + " AND  a.emp_deptid = c.dept_id ";
                query = query + " AND  a.emp_gredid = d.gred_id ";
                query = query + " AND  a.emp_posid = e.pos_id ";
                query = query + " AND  a.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                query = query + " order by a.comp, a.staffno, a.emp_fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSettype = replaceNull(dataReader, "emp_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "emp_cat");
                    modStaff.GetSetprobation = replaceZero(dataReader, "emp_probation");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "emp_fromdate");
                    modStaff.GetSetstr_fromdate = replaceNull(dataReader, "str_emp_fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "emp_todate");
                    modStaff.GetSetstr_todate = replaceNull(dataReader, "str_emp_todate");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffEmployDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertStaffEmploy(HRModel oModStaff)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_employment (comp, staffno, emp_deptid, emp_gredid, emp_posid, emp_type, emp_cat, emp_probation, emp_fromdate, emp_todate, emp_reportto, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?staffno, ?emp_deptid, ?emp_gredid, ?emp_posid, ?emp_type, ?emp_cat, ?emp_probation, ?emp_fromdate, ?emp_todate, ?emp_reportto, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModStaff.GetSetcomp;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModStaff.GetSetstaffno;
                cmd.Parameters.Add("?emp_deptid", MySqlDbType.VarChar).Value = oModStaff.GetSetdept_id;
                cmd.Parameters.Add("?emp_gredid", MySqlDbType.VarChar).Value = oModStaff.GetSetgred_id;
                cmd.Parameters.Add("?emp_posid", MySqlDbType.VarChar).Value = oModStaff.GetSetpos_id;
                cmd.Parameters.Add("?emp_type", MySqlDbType.VarChar).Value = oModStaff.GetSettype;
                cmd.Parameters.Add("?emp_cat", MySqlDbType.VarChar).Value = oModStaff.GetSetcat;
                cmd.Parameters.Add("?emp_probation", MySqlDbType.Int16).Value = oModStaff.GetSetprobation;
                if (oModStaff.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?emp_fromdate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?emp_fromdate", MySqlDbType.Date).Value = null;
                }
                if (oModStaff.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?emp_todate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?emp_todate", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?emp_reportto", MySqlDbType.VarChar).Value = oModStaff.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModStaff.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModStaff.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModStaff.GetSetcreatedby;
                if (oModStaff.GetSetcreatedby.Length > 0)
                {
                    if (oModStaff.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModStaff.GetSetmodifiedby;
                if (oModStaff.GetSetmodifiedby.Length > 0)
                {
                    if (oModStaff.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModStaff.GetSetconfirmedby;
                if (oModStaff.GetSetconfirmedby.Length > 0)
                {
                    if (oModStaff.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModStaff.GetSetcancelledby;
                if (oModStaff.GetSetcancelledby.Length > 0)
                {
                    if (oModStaff.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffEmploy: " + e.Message.ToString());
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

    public String updateStaffEmploy(HRModel oModStaff)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_employment ";
                query = query + " SET    emp_deptid = ?emp_deptid, emp_gredid = ?emp_gredid, emp_posid = ?emp_posid, emp_type = ?emp_type, emp_cat = ?emp_cat, emp_probation = ?emp_probation, emp_fromdate = ?emp_fromdate, emp_todate = ?emp_todate, emp_reportto = ?emp_reportto, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND staffno = ?staffno AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModStaff.GetSetcomp;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModStaff.GetSetstaffno;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModStaff.GetSetid;                
                cmd.Parameters.Add("?emp_deptid", MySqlDbType.VarChar).Value = oModStaff.GetSetdept_id;
                cmd.Parameters.Add("?emp_gredid", MySqlDbType.VarChar).Value = oModStaff.GetSetgred_id;
                cmd.Parameters.Add("?emp_posid", MySqlDbType.VarChar).Value = oModStaff.GetSetpos_id;
                cmd.Parameters.Add("?emp_type", MySqlDbType.VarChar).Value = oModStaff.GetSettype;
                cmd.Parameters.Add("?emp_cat", MySqlDbType.VarChar).Value = oModStaff.GetSetcat;
                cmd.Parameters.Add("?emp_probation", MySqlDbType.Int16).Value = oModStaff.GetSetprobation;
                if (oModStaff.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?emp_fromdate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?emp_fromdate", MySqlDbType.Date).Value = null;
                }
                if (oModStaff.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?emp_todate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?emp_todate", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?emp_reportto", MySqlDbType.VarChar).Value = oModStaff.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModStaff.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModStaff.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModStaff.GetSetcreatedby;
                if (oModStaff.GetSetcreatedby.Length > 0)
                {
                    if (oModStaff.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModStaff.GetSetmodifiedby;
                if (oModStaff.GetSetmodifiedby.Length > 0)
                {
                    if (oModStaff.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModStaff.GetSetconfirmedby;
                if (oModStaff.GetSetconfirmedby.Length > 0)
                {
                    if (oModStaff.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModStaff.GetSetcancelledby;
                if (oModStaff.GetSetcancelledby.Length > 0)
                {
                    if (oModStaff.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompDept: " + e.Message.ToString());
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

    public String deleteStaffEmploy(HRModel oModStaff)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_employment ";
                query = query + " WHERE  comp = ?comp AND staffno = ?staffno AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModStaff.GetSetcomp;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModStaff.GetSetstaffno;
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModStaff.GetSetid;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffEmploy: " + e.Message.ToString());
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

    public ArrayList getCompGredList(String comp, String gredid, String gredname)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.gred_id, a.gred_name, a.gred_level, a.gred_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   grade_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (gredid.Trim().Length > 0)
                {
                    query = query + " and  a.gred_id = '" + gredid + "' ";
                }
                if (gredname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.gred_name) like '%" + gredname + "%' ";
                }
                query = query + " order by a.comp, a.gred_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetname = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "gred_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "gred_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompGredList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getCompGredDetails(String comp, String gredid, String gredname)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.gred_id, a.gred_name, a.gred_level, a.gred_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   grade_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (gredid.Trim().Length > 0)
                {
                    query = query + " and  a.gred_id = '" + gredid + "' ";
                }
                if (gredname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.gred_name) like '%" + gredname + "%' ";
                }
                query = query + " order by a.comp, a.gred_name ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetname = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "gred_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "gred_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompGredDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public ArrayList getCompGredReportTo(String comp, String gredid, String gredname, int gredlevel)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.gred_id, a.gred_name, a.gred_level, a.gred_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   grade_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (gredid.Trim().Length > 0)
                {
                    query = query + " and  a.gred_id = '" + gredid + "' ";
                }
                if (gredname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.gred_name) like '%" + gredname + "%' ";
                }
                if (gredlevel > 0)
                {
                    query = query + " and  a.gred_level <= " + gredlevel + " ";
                }
                query = query + " order by a.comp, a.gred_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetname = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "gred_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "gred_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompGredReportTo: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public String insertCompGred(HRModel oModGred)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO grade_comp (comp, gred_id, gred_name, gred_level, gred_reportto, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?gred_id, ?gred_name, ?gred_level, ?gred_reportto, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModGred.GetSetcomp;
                cmd.Parameters.Add("?gred_id", MySqlDbType.VarChar).Value = oModGred.GetSetsid;
                cmd.Parameters.Add("?gred_name", MySqlDbType.VarChar).Value = oModGred.GetSetname;
                cmd.Parameters.Add("?gred_level", MySqlDbType.Int16).Value = oModGred.GetSetlevel;
                cmd.Parameters.Add("?gred_reportto", MySqlDbType.VarChar).Value = oModGred.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModGred.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModGred.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModGred.GetSetcreatedby;
                if (oModGred.GetSetcreatedby.Length > 0)
                {
                    if (oModGred.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModGred.GetSetmodifiedby;
                if (oModGred.GetSetmodifiedby.Length > 0)
                {
                    if (oModGred.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModGred.GetSetconfirmedby;
                if (oModGred.GetSetconfirmedby.Length > 0)
                {
                    if (oModGred.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModGred.GetSetcancelledby;
                if (oModGred.GetSetcancelledby.Length > 0)
                {
                    if (oModGred.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompGred: " + e.Message.ToString());
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

    public String updateCompGred(HRModel oModGred)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE grade_comp ";
                query = query + " SET    gred_name = ?gred_name, gred_level = ?gred_level, gred_reportto = ?gred_reportto, remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND gred_id = ?gred_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModGred.GetSetcomp;
                cmd.Parameters.Add("?gred_id", MySqlDbType.VarChar).Value = oModGred.GetSetsid;
                cmd.Parameters.Add("?gred_name", MySqlDbType.VarChar).Value = oModGred.GetSetname;
                cmd.Parameters.Add("?gred_level", MySqlDbType.Int16).Value = oModGred.GetSetlevel;
                cmd.Parameters.Add("?gred_reportto", MySqlDbType.VarChar).Value = oModGred.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModGred.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModGred.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModGred.GetSetcreatedby;
                if (oModGred.GetSetcreatedby.Length > 0)
                {
                    if (oModGred.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModGred.GetSetmodifiedby;
                if (oModGred.GetSetmodifiedby.Length > 0)
                {
                    if (oModGred.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModGred.GetSetconfirmedby;
                if (oModGred.GetSetconfirmedby.Length > 0)
                {
                    if (oModGred.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModGred.GetSetcancelledby;
                if (oModGred.GetSetcancelledby.Length > 0)
                {
                    if (oModGred.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModGred.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompGred: " + e.Message.ToString());
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

    public String deleteCompGred(HRModel oModGred)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM grade_comp ";
                query = query + " WHERE  comp = ?comp AND gred_id = ?gred_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModGred.GetSetcomp;
                cmd.Parameters.Add("?gred_id", MySqlDbType.VarChar).Value = oModGred.GetSetsid;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompGred: " + e.Message.ToString());
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

    public ArrayList getCompPosList(String comp, String posid, String posname)
    {
        ArrayList lsCompPos = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.pos_id, a.pos_name, a.pos_level, a.pos_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   position_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (posid.Trim().Length > 0)
                {
                    query = query + " and  a.pos_id = '" + posid + "' ";
                }
                if (posname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.pos_name) like '%" + posname + "%' ";
                }
                query = query + " order by a.comp, a.pos_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetname = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "pos_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "pos_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompPos.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPosList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompPos;
    }

    public HRModel getCompPosDetails(String comp, String posid, String posname)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.pos_id, a.pos_name, a.pos_level, a.pos_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   position_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (posid.Trim().Length > 0)
                {
                    query = query + " and  a.pos_id = '" + posid + "' ";
                }
                if (posname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.pos_name) like '%" + posname + "%' ";
                }
                query = query + " order by a.comp, a.pos_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetname = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "pos_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "pos_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPosDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public ArrayList getCompPosReportTo(String comp, String posid, String posname, int poslevel)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.pos_id, a.pos_name, a.pos_level, a.pos_reportto, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   position_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (posid.Trim().Length > 0)
                {
                    query = query + " and  a.pos_id = '" + posid + "' ";
                }
                if (posname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.pos_name) like '%" + posname + "%' ";
                }
                if (poslevel > 0)
                {
                    query = query + " and  a.pos_level <= " + poslevel + " ";
                }
                query = query + " order by a.comp, a.pos_level ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetsid = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetname = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetlevel = replaceZero(dataReader, "pos_level");
                    modStaff.GetSetreportto = replaceNull(dataReader, "pos_reportto");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPosReportTo: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public String insertCompPos(HRModel oModPos)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO position_comp (comp, pos_id, pos_name, pos_level, pos_reportto, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?pos_id, ?pos_name, ?pos_level, ?pos_reportto, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPos.GetSetcomp;
                cmd.Parameters.Add("?pos_id", MySqlDbType.VarChar).Value = oModPos.GetSetsid;
                cmd.Parameters.Add("?pos_name", MySqlDbType.VarChar).Value = oModPos.GetSetname;
                cmd.Parameters.Add("?pos_level", MySqlDbType.Int16).Value = oModPos.GetSetlevel;
                cmd.Parameters.Add("?pos_reportto", MySqlDbType.VarChar).Value = oModPos.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModPos.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModPos.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModPos.GetSetcreatedby;
                if (oModPos.GetSetcreatedby.Length > 0)
                {
                    if (oModPos.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModPos.GetSetmodifiedby;
                if (oModPos.GetSetmodifiedby.Length > 0)
                {
                    if (oModPos.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModPos.GetSetconfirmedby;
                if (oModPos.GetSetconfirmedby.Length > 0)
                {
                    if (oModPos.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModPos.GetSetcancelledby;
                if (oModPos.GetSetcancelledby.Length > 0)
                {
                    if (oModPos.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompPos: " + e.Message.ToString());
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

    public String updateCompPos(HRModel oModPos)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE position_comp ";
                query = query + " SET    pos_name = ?pos_name, pos_level = ?pos_level, pos_reportto = ?pos_reportto, remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND pos_id = ?pos_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPos.GetSetcomp;
                cmd.Parameters.Add("?pos_id", MySqlDbType.VarChar).Value = oModPos.GetSetsid;
                cmd.Parameters.Add("?pos_name", MySqlDbType.VarChar).Value = oModPos.GetSetname;
                cmd.Parameters.Add("?pos_level", MySqlDbType.Int16).Value = oModPos.GetSetlevel;
                cmd.Parameters.Add("?pos_reportto", MySqlDbType.VarChar).Value = oModPos.GetSetreportto;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModPos.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModPos.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModPos.GetSetcreatedby;
                if (oModPos.GetSetcreatedby.Length > 0)
                {
                    if (oModPos.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModPos.GetSetmodifiedby;
                if (oModPos.GetSetmodifiedby.Length > 0)
                {
                    if (oModPos.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModPos.GetSetconfirmedby;
                if (oModPos.GetSetconfirmedby.Length > 0)
                {
                    if (oModPos.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModPos.GetSetcancelledby;
                if (oModPos.GetSetcancelledby.Length > 0)
                {
                    if (oModPos.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPos.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompPos: " + e.Message.ToString());
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

    public String deleteCompPos(HRModel oModPos)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM position_comp ";
                query = query + " WHERE  comp = ?comp AND pos_id = ?pos_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPos.GetSetcomp;
                cmd.Parameters.Add("?pos_id", MySqlDbType.VarChar).Value = oModPos.GetSetsid;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompPos: " + e.Message.ToString());
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

    public ArrayList getSaluteList(String comp, String id, String desc)
    {
        ArrayList lsObj = new ArrayList();
        HRModel objMod = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            objMod = new HRModel();
            objMod.GetSetcode = "DATUK";
            objMod.GetSetdesc = "DATUK";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "DATO'";
            objMod.GetSetdesc = "DATO'";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "TUAN";
            objMod.GetSetdesc = "TUAN";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "PUAN";
            objMod.GetSetdesc = "PUAN";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "ENCIK";
            objMod.GetSetdesc = "ENCIK";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "CIK";
            objMod.GetSetdesc = "CIK";
            lsObj.Add(objMod);
            /*
            if (dbConnect.OpenConnection() == true)
            {
                dataReader.Close();
                dbConnect.CloseConnection();
            }
            */
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getSaluteList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsObj;
    }

    public ArrayList getRaceList(String comp, String id, String desc)
    {
        ArrayList lsObj = new ArrayList();
        HRModel objMod = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            objMod = new HRModel();
            objMod.GetSetcode = "MELAYU";
            objMod.GetSetdesc = "MELAYU";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "CINA";
            objMod.GetSetdesc = "CINA";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "INDIA";
            objMod.GetSetdesc = "INDIA";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "SIAM";
            objMod.GetSetdesc = "SIAM";
            lsObj.Add(objMod);
            /*
            if (dbConnect.OpenConnection() == true)
            {
                dataReader.Close();
                dbConnect.CloseConnection();
            }
            */
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRaceList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsObj;
    }

    public ArrayList getReligionList(String comp, String id, String desc)
    {
        ArrayList lsObj = new ArrayList();
        HRModel objMod = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            objMod = new HRModel();
            objMod.GetSetcode = "ISLAM";
            objMod.GetSetdesc = "ISLAM";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "KRISTIAN";
            objMod.GetSetdesc = "KRISTIAN";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "HINDU";
            objMod.GetSetdesc = "HINDU";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "BUDDHA";
            objMod.GetSetdesc = "BUDDHA";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "SIKHISME";
            objMod.GetSetdesc = "SIKHISME";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "TAOISME";
            objMod.GetSetdesc = "TAOISME";
            lsObj.Add(objMod);

            /*
            if (dbConnect.OpenConnection() == true)
            {
                dataReader.Close();
                dbConnect.CloseConnection();
            }
            */
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getReligionList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsObj;
    }

    public ArrayList getCountryList(String comp, String id, String desc)
    {
        ArrayList lsObj = new ArrayList();
        HRModel objMod = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            objMod = new HRModel();
            objMod.GetSetcode = "MALAYSIA";
            objMod.GetSetdesc = "MALAYSIA";
            lsObj.Add(objMod);

            /*
            if (dbConnect.OpenConnection() == true)
            {
                dataReader.Close();
                dbConnect.CloseConnection();
            }
            */
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCountryList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsObj;
    }

    public ArrayList getStateList(String comp, String id, String desc)
    {
        ArrayList lsObj = new ArrayList();
        HRModel objMod = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            objMod = new HRModel();
            objMod.GetSetcode = "KEDAH";
            objMod.GetSetdesc = "KEDAH";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "PERLIS";
            objMod.GetSetdesc = "PERLIS";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "PULAU PINANG";
            objMod.GetSetdesc = "PULAU PINANG";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "PERAK";
            objMod.GetSetdesc = "PERAK";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "KELANTAN";
            objMod.GetSetdesc = "KELANTAN";
            lsObj.Add(objMod);
            objMod = new HRModel();
            objMod.GetSetcode = "TERENGGANU";
            objMod.GetSetdesc = "TERENGGANU";
            lsObj.Add(objMod);

            /*
            if (dbConnect.OpenConnection() == true)
            {
                dataReader.Close();
                dbConnect.CloseConnection();
            }
            */
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStateList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsObj;
    }

    public ArrayList getStaffList(String comp, String staffno, String staffname, String staffdept)
    {
        ArrayList lsStaffMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.salute, a.name, a.nickname, a.nicno, a.oicno, a.iccolor, a.passport, a.ppexpiry, date_format(a.ppexpiry,'%d-%m-%Y %H:%i:%s') str_ppexpiry, a.wkpermit, a.wkexpiry, date_format(a.wkexpiry,'%d-%m-%Y %H:%i:%s') str_wkexpiry, ";
                query = query + "        a.dob, date_format(a.dob,'%d-%m-%Y') str_dob, a.birthplace, a.marital, a.dtmarried, date_format(a.dtmarried,'%d-%m-%Y') str_dtmarried, a.bloodtype, a.race, a.religion, a.gender, a.nationality, a.bumistatus, ";
                query = query + "        b.emp_deptid dept_id, (select dept_name from department_comp c where b.comp = c.comp and b.emp_deptid = c.dept_id) dept_name, ";
                query = query + "        b.emp_gredid gred_id, (select gred_name from grade_comp c where b.comp = c.comp and b.emp_gredid = c.gred_id) gred_name, ";
                query = query + "        b.emp_posid pos_id, (select pos_name from position_comp c where b.comp = c.comp and b.emp_posid = c.pos_id) pos_name, ";
                query = query + "        a.paddress1, a.paddress2, a.paddress3, a.paddress4, a.ppostcode, a.pcity, a.pstate, a.pcountry, a.ptelephone, ";
                query = query + "        a.caddress1, a.caddress2, a.caddress3, a.caddress4, a.cpostcode, a.ccity, a.cstate, a.ccountry, a.ctelephone, ";
                query = query + "        a.mobile1, a.mobile2, a.datejoined, date_format(a.datejoined,'%d-%m-%Y') str_datejoined, a.retireage, a.retiredate, date_format(a.retiredate,'%d-%m-%Y') str_retiredate, a.email1, a.email2, a.facebook, a.instagram, a.whatsapp, ";
                query = query + "        a.epfno, a.socsono, a.taxno, a.bankname, a.accountype, a.accountno, a.userid, a.password, a.usertype, a.lastaccess, date_format(a.lastaccess,'%d-%m-%Y %H:%i:%s') str_lastaccess, a.statuslogon, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " FROM   staff_info a left join staff_employment b on a.comp = b.comp and a.staffno = b.staffno and b.status = 'ACTIVE' ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (staffname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.name) like '%" + staffname + "%' ";
                }
                query = query + " order by a.comp, a.staffno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnickname = replaceNull(dataReader, "nickname");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetoicno = replaceNull(dataReader, "oicno");
                    modStaff.GetSeticcolor = replaceNull(dataReader, "iccolor");
                    modStaff.GetSetpassport = replaceNull(dataReader, "passport");
                    modStaff.GetSetppexpiry = replaceNull(dataReader, "ppexpiry");
                    modStaff.GetSetstr_ppexpiry = replaceNull(dataReader, "str_ppexpiry");
                    modStaff.GetSetwkpermit = replaceNull(dataReader, "wkpermit");
                    modStaff.GetSetwkexpiry = replaceNull(dataReader, "wkexpiry");
                    modStaff.GetSetstr_wkexpiry = replaceNull(dataReader, "str_wkexpiry");
                    modStaff.GetSetdob = replaceNull(dataReader, "dob");
                    modStaff.GetSetstr_dob = replaceNull(dataReader, "str_dob");
                    modStaff.GetSetbirthplace = replaceNull(dataReader, "birthplace");
                    modStaff.GetSetmarital = replaceNull(dataReader, "marital");
                    modStaff.GetSetdtmarried = replaceNull(dataReader, "dtmarried");
                    modStaff.GetSetstr_dtmarried = replaceNull(dataReader, "str_dtmarried");
                    modStaff.GetSetbloodtype = replaceNull(dataReader, "bloodtype");
                    modStaff.GetSetrace = replaceNull(dataReader, "race");
                    modStaff.GetSetreligion = replaceNull(dataReader, "religion");
                    modStaff.GetSetgender = replaceNull(dataReader, "gender");
                    modStaff.GetSetnationality = replaceNull(dataReader, "nationality");
                    modStaff.GetSetbumistatus = replaceNull(dataReader, "bumistatus");

                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");

                    modStaff.GetSetpaddress1 = replaceNull(dataReader, "paddress1");
                    modStaff.GetSetpaddress2 = replaceNull(dataReader, "paddress2");
                    modStaff.GetSetpaddress3 = replaceNull(dataReader, "paddress3");
                    modStaff.GetSetpaddress4 = replaceNull(dataReader, "paddress4");
                    modStaff.GetSetppostcode = replaceNull(dataReader, "ppostcode");
                    modStaff.GetSetpcity = replaceNull(dataReader, "pcity");
                    modStaff.GetSetpstate = replaceNull(dataReader, "pstate");
                    modStaff.GetSetpcountry = replaceNull(dataReader, "pcountry");
                    modStaff.GetSetptelephone = replaceNull(dataReader, "ptelephone");
                    modStaff.GetSetcaddress1 = replaceNull(dataReader, "caddress1");
                    modStaff.GetSetcaddress2 = replaceNull(dataReader, "caddress2");
                    modStaff.GetSetcaddress3 = replaceNull(dataReader, "caddress3");
                    modStaff.GetSetcaddress4 = replaceNull(dataReader, "caddress4");
                    modStaff.GetSetcpostcode = replaceNull(dataReader, "cpostcode");
                    modStaff.GetSetccity = replaceNull(dataReader, "ccity");
                    modStaff.GetSetcstate = replaceNull(dataReader, "cstate");
                    modStaff.GetSetccountry = replaceNull(dataReader, "ccountry");
                    modStaff.GetSetctelephone = replaceNull(dataReader, "ctelephone");
                    modStaff.GetSetmobile1 = replaceNull(dataReader, "mobile1");
                    modStaff.GetSetmobile2 = replaceNull(dataReader, "mobile2");
                    modStaff.GetSetdatejoined = replaceNull(dataReader, "datejoined");
                    modStaff.GetSetstr_datejoined = replaceNull(dataReader, "str_datejoined");
                    modStaff.GetSetretireage = replaceZero(dataReader, "retireage");
                    modStaff.GetSetretiredate = replaceNull(dataReader, "retiredate");
                    modStaff.GetSetstr_retiredate = replaceNull(dataReader, "str_retiredate");
                    modStaff.GetSetemail1 = replaceNull(dataReader, "email1");
                    modStaff.GetSetemail2 = replaceNull(dataReader, "email2");
                    modStaff.GetSetfacebook = replaceNull(dataReader, "facebook");
                    modStaff.GetSetinstagram = replaceNull(dataReader, "instagram");
                    modStaff.GetSetwhatsapp = replaceNull(dataReader, "whatsapp");
                    modStaff.GetSetepfno = replaceNull(dataReader, "epfno");
                    modStaff.GetSetsocsono = replaceNull(dataReader, "socsono");
                    modStaff.GetSettaxno = replaceNull(dataReader, "taxno");
                    modStaff.GetSetbankname = replaceNull(dataReader, "bankname");
                    modStaff.GetSetaccountype = replaceNull(dataReader, "accountype");
                    modStaff.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modStaff.GetSetuserid = replaceNull(dataReader, "userid");
                    modStaff.GetSetpassword = replaceNull(dataReader, "password");
                    modStaff.GetSetusertype = replaceNull(dataReader, "usertype");
                    modStaff.GetSetlastaccess = replaceNull(dataReader, "lastaccess");
                    modStaff.GetSetstr_lastaccess = replaceNull(dataReader, "str_lastaccess");
                    modStaff.GetSetstatuslogon = replaceNull(dataReader, "statuslogon");

                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsStaffMod.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffMod;
    }

    public ArrayList getStaffListReportTo(String comp, String staffno, String gredid)
    {
        ArrayList lsStaffMod = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.salute, a.name, a.nickname, a.nicno, a.oicno, a.iccolor, a.passport, a.ppexpiry, date_format(a.ppexpiry,'%d-%m-%Y %H:%i:%s') str_ppexpiry, a.wkpermit, a.wkexpiry, date_format(a.wkexpiry,'%d-%m-%Y %H:%i:%s') str_wkexpiry, ";
                query = query + "        a.dob, date_format(a.dob,'%d-%m-%Y') str_dob, a.birthplace, a.marital, a.dtmarried, date_format(a.dtmarried,'%d-%m-%Y') str_dtmarried, a.bloodtype, a.race, a.religion, a.gender, a.nationality, a.bumistatus, ";
                query = query + "        b.emp_deptid dept_id, (select dept_name from department_comp c where b.comp = c.comp and b.emp_deptid = c.dept_id) dept_name, ";
                query = query + "        b.emp_gredid gred_id, (select gred_name from grade_comp c where b.comp = c.comp and b.emp_gredid = c.gred_id) gred_name, ";
                query = query + "        b.emp_posid pos_id, (select pos_name from position_comp c where b.comp = c.comp and b.emp_posid = c.pos_id) pos_name, ";
                query = query + "        a.paddress1, a.paddress2, a.paddress3, a.paddress4, a.ppostcode, a.pcity, a.pstate, a.pcountry, a.ptelephone, ";
                query = query + "        a.caddress1, a.caddress2, a.caddress3, a.caddress4, a.cpostcode, a.ccity, a.cstate, a.ccountry, a.ctelephone, ";
                query = query + "        a.mobile1, a.mobile2, a.datejoined, date_format(a.datejoined,'%d-%m-%Y') str_datejoined, a.retireage, a.retiredate, date_format(a.retiredate,'%d-%m-%Y') str_retiredate, a.email1, a.email2, a.facebook, a.instagram, a.whatsapp, ";
                query = query + "        a.epfno, a.socsono, a.taxno, a.bankname, a.accountype, a.accountno, a.userid, a.password, a.usertype, a.lastaccess, date_format(a.lastaccess,'%d-%m-%Y %H:%i:%s') str_lastaccess, a.statuslogon, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " FROM   staff_info a, staff_employment b, grade_comp c ";
                query = query + " WHERE  a.comp = b.comp AND a.staffno = b.staffno AND b.status = 'ACTIVE' ";
                query = query + " AND    b.comp = c.comp AND b.emp_gredid = c.gred_id AND c.gred_level <= (select x.gred_level from grade_comp x where x.comp = c.comp and x.gred_id = '" + gredid + "') ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    //comment out: supaya boleh select diri sendiri sebagai penyelia
                    //query = query + " and  a.staffno not in ('" + staffno + "') ";
                }
                query = query + " order by a.comp, a.staffno ";
                //WriteToLogFile("HRController-getStaffList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnickname = replaceNull(dataReader, "nickname");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetoicno = replaceNull(dataReader, "oicno");
                    modStaff.GetSeticcolor = replaceNull(dataReader, "iccolor");
                    modStaff.GetSetpassport = replaceNull(dataReader, "passport");
                    modStaff.GetSetppexpiry = replaceNull(dataReader, "ppexpiry");
                    modStaff.GetSetstr_ppexpiry = replaceNull(dataReader, "str_ppexpiry");
                    modStaff.GetSetwkpermit = replaceNull(dataReader, "wkpermit");
                    modStaff.GetSetwkexpiry = replaceNull(dataReader, "wkexpiry");
                    modStaff.GetSetstr_wkexpiry = replaceNull(dataReader, "str_wkexpiry");
                    modStaff.GetSetdob = replaceNull(dataReader, "dob");
                    modStaff.GetSetstr_dob = replaceNull(dataReader, "str_dob");
                    modStaff.GetSetbirthplace = replaceNull(dataReader, "birthplace");
                    modStaff.GetSetmarital = replaceNull(dataReader, "marital");
                    modStaff.GetSetdtmarried = replaceNull(dataReader, "dtmarried");
                    modStaff.GetSetstr_dtmarried = replaceNull(dataReader, "str_dtmarried");
                    modStaff.GetSetbloodtype = replaceNull(dataReader, "bloodtype");
                    modStaff.GetSetrace = replaceNull(dataReader, "race");
                    modStaff.GetSetreligion = replaceNull(dataReader, "religion");
                    modStaff.GetSetgender = replaceNull(dataReader, "gender");
                    modStaff.GetSetnationality = replaceNull(dataReader, "nationality");
                    modStaff.GetSetbumistatus = replaceNull(dataReader, "bumistatus");

                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");

                    modStaff.GetSetpaddress1 = replaceNull(dataReader, "paddress1");
                    modStaff.GetSetpaddress2 = replaceNull(dataReader, "paddress2");
                    modStaff.GetSetpaddress3 = replaceNull(dataReader, "paddress3");
                    modStaff.GetSetpaddress4 = replaceNull(dataReader, "paddress4");
                    modStaff.GetSetppostcode = replaceNull(dataReader, "ppostcode");
                    modStaff.GetSetpcity = replaceNull(dataReader, "pcity");
                    modStaff.GetSetpstate = replaceNull(dataReader, "pstate");
                    modStaff.GetSetpcountry = replaceNull(dataReader, "pcountry");
                    modStaff.GetSetptelephone = replaceNull(dataReader, "ptelephone");
                    modStaff.GetSetcaddress1 = replaceNull(dataReader, "caddress1");
                    modStaff.GetSetcaddress2 = replaceNull(dataReader, "caddress2");
                    modStaff.GetSetcaddress3 = replaceNull(dataReader, "caddress3");
                    modStaff.GetSetcaddress4 = replaceNull(dataReader, "caddress4");
                    modStaff.GetSetcpostcode = replaceNull(dataReader, "cpostcode");
                    modStaff.GetSetccity = replaceNull(dataReader, "ccity");
                    modStaff.GetSetcstate = replaceNull(dataReader, "cstate");
                    modStaff.GetSetccountry = replaceNull(dataReader, "ccountry");
                    modStaff.GetSetctelephone = replaceNull(dataReader, "ctelephone");
                    modStaff.GetSetmobile1 = replaceNull(dataReader, "mobile1");
                    modStaff.GetSetmobile2 = replaceNull(dataReader, "mobile2");
                    modStaff.GetSetdatejoined = replaceNull(dataReader, "datejoined");
                    modStaff.GetSetstr_datejoined = replaceNull(dataReader, "str_datejoined");
                    modStaff.GetSetretireage = replaceZero(dataReader, "retireage");
                    modStaff.GetSetretiredate = replaceNull(dataReader, "retiredate");
                    modStaff.GetSetstr_retiredate = replaceNull(dataReader, "str_retiredate");
                    modStaff.GetSetemail1 = replaceNull(dataReader, "email1");
                    modStaff.GetSetemail2 = replaceNull(dataReader, "email2");
                    modStaff.GetSetfacebook = replaceNull(dataReader, "facebook");
                    modStaff.GetSetinstagram = replaceNull(dataReader, "instagram");
                    modStaff.GetSetwhatsapp = replaceNull(dataReader, "whatsapp");
                    modStaff.GetSetepfno = replaceNull(dataReader, "epfno");
                    modStaff.GetSetsocsono = replaceNull(dataReader, "socsono");
                    modStaff.GetSettaxno = replaceNull(dataReader, "taxno");
                    modStaff.GetSetbankname = replaceNull(dataReader, "bankname");
                    modStaff.GetSetaccountype = replaceNull(dataReader, "accountype");
                    modStaff.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modStaff.GetSetuserid = replaceNull(dataReader, "userid");
                    modStaff.GetSetpassword = replaceNull(dataReader, "password");
                    modStaff.GetSetusertype = replaceNull(dataReader, "usertype");
                    modStaff.GetSetlastaccess = replaceNull(dataReader, "lastaccess");
                    modStaff.GetSetstr_lastaccess = replaceNull(dataReader, "str_lastaccess");
                    modStaff.GetSetstatuslogon = replaceNull(dataReader, "statuslogon");

                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsStaffMod.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffMod;
    }

    public HRModel getStaffDetails(String comp, String staffno)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.salute, a.name, a.nickname, a.nicno, a.oicno, a.iccolor, a.passport, a.ppexpiry, date_format(a.ppexpiry,'%d-%m-%Y %H:%i:%s') str_ppexpiry, a.wkpermit, a.wkexpiry, date_format(a.wkexpiry,'%d-%m-%Y %H:%i:%s') str_wkexpiry, ";
                query = query + "        a.dob, date_format(a.dob,'%d-%m-%Y') str_dob, a.birthplace, a.marital, a.dtmarried, date_format(a.dtmarried,'%d-%m-%Y') str_dtmarried, a.bloodtype, a.race, a.religion, a.gender, a.nationality, a.bumistatus, ";
                query = query + "        b.emp_deptid dept_id, (select dept_name from department_comp c where b.comp = c.comp and b.emp_deptid = c.dept_id) dept_name, ";
                query = query + "        b.emp_gredid gred_id, (select gred_name from grade_comp c where b.comp = c.comp and b.emp_gredid = c.gred_id) gred_name, ";
                query = query + "        b.emp_posid pos_id, (select pos_name from position_comp c where b.comp = c.comp and b.emp_posid = c.pos_id) pos_name, ";
                query = query + "        b.emp_reportto, ";
                query = query + "        a.paddress1, a.paddress2, a.paddress3, a.paddress4, a.ppostcode, a.pcity, a.pstate, a.pcountry, a.ptelephone, ";
                query = query + "        a.caddress1, a.caddress2, a.caddress3, a.caddress4, a.cpostcode, a.ccity, a.cstate, a.ccountry, a.ctelephone, ";
                query = query + "        a.mobile1, a.mobile2, a.datejoined, date_format(a.datejoined,'%d-%m-%Y') str_datejoined, a.retireage, a.retiredate, date_format(a.retiredate,'%d-%m-%Y') str_retiredate, a.email1, a.email2, a.facebook, a.instagram, a.whatsapp, ";
                query = query + "        a.epfno, a.socsono, a.taxno, a.bankname, a.accountype, a.accountno, a.userid, a.password, a.usertype, a.lastaccess, date_format(a.lastaccess,'%d-%m-%Y %H:%i:%s') str_lastaccess, a.statuslogon, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " FROM   staff_info a left join staff_employment b on a.comp = b.comp and a.staffno = b.staffno and b.status = 'ACTIVE' ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                query = query + " order by a.comp, a.staffno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnickname = replaceNull(dataReader, "nickname");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetoicno = replaceNull(dataReader, "oicno");
                    modStaff.GetSeticcolor = replaceNull(dataReader, "iccolor");
                    modStaff.GetSetpassport = replaceNull(dataReader, "passport");
                    modStaff.GetSetppexpiry = replaceNull(dataReader, "ppexpiry");
                    modStaff.GetSetstr_ppexpiry = replaceNull(dataReader, "str_ppexpiry");
                    modStaff.GetSetwkpermit = replaceNull(dataReader, "wkpermit");
                    modStaff.GetSetwkexpiry = replaceNull(dataReader, "wkexpiry");
                    modStaff.GetSetstr_wkexpiry = replaceNull(dataReader, "str_wkexpiry");
                    modStaff.GetSetdob = replaceNull(dataReader, "dob");
                    modStaff.GetSetstr_dob = replaceNull(dataReader, "str_dob");
                    modStaff.GetSetbirthplace = replaceNull(dataReader, "birthplace");
                    modStaff.GetSetmarital = replaceNull(dataReader, "marital");
                    modStaff.GetSetdtmarried = replaceNull(dataReader, "dtmarried");
                    modStaff.GetSetstr_dtmarried = replaceNull(dataReader, "str_dtmarried");
                    modStaff.GetSetbloodtype = replaceNull(dataReader, "bloodtype");
                    modStaff.GetSetrace = replaceNull(dataReader, "race");
                    modStaff.GetSetreligion = replaceNull(dataReader, "religion");
                    modStaff.GetSetgender = replaceNull(dataReader, "gender");
                    modStaff.GetSetnationality = replaceNull(dataReader, "nationality");
                    modStaff.GetSetbumistatus = replaceNull(dataReader, "bumistatus");

                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");

                    modStaff.GetSetpaddress1 = replaceNull(dataReader, "paddress1");
                    modStaff.GetSetpaddress2 = replaceNull(dataReader, "paddress2");
                    modStaff.GetSetpaddress3 = replaceNull(dataReader, "paddress3");
                    modStaff.GetSetpaddress4 = replaceNull(dataReader, "paddress4");
                    modStaff.GetSetppostcode = replaceNull(dataReader, "ppostcode");
                    modStaff.GetSetpcity = replaceNull(dataReader, "pcity");
                    modStaff.GetSetpstate = replaceNull(dataReader, "pstate");
                    modStaff.GetSetpcountry = replaceNull(dataReader, "pcountry");
                    modStaff.GetSetptelephone = replaceNull(dataReader, "ptelephone");
                    modStaff.GetSetcaddress1 = replaceNull(dataReader, "caddress1");
                    modStaff.GetSetcaddress2 = replaceNull(dataReader, "caddress2");
                    modStaff.GetSetcaddress3 = replaceNull(dataReader, "caddress3");
                    modStaff.GetSetcaddress4 = replaceNull(dataReader, "caddress4");
                    modStaff.GetSetcpostcode = replaceNull(dataReader, "cpostcode");
                    modStaff.GetSetccity = replaceNull(dataReader, "ccity");
                    modStaff.GetSetcstate = replaceNull(dataReader, "cstate");
                    modStaff.GetSetccountry = replaceNull(dataReader, "ccountry");
                    modStaff.GetSetctelephone = replaceNull(dataReader, "ctelephone");
                    modStaff.GetSetmobile1 = replaceNull(dataReader, "mobile1");
                    modStaff.GetSetmobile2 = replaceNull(dataReader, "mobile2");
                    modStaff.GetSetdatejoined = replaceNull(dataReader, "datejoined");
                    modStaff.GetSetstr_datejoined = replaceNull(dataReader, "str_datejoined");
                    modStaff.GetSetretireage = replaceZero(dataReader, "retireage");
                    modStaff.GetSetretiredate = replaceNull(dataReader, "retiredate");
                    modStaff.GetSetstr_retiredate = replaceNull(dataReader, "str_retiredate");
                    modStaff.GetSetemail1 = replaceNull(dataReader, "email1");
                    modStaff.GetSetemail2 = replaceNull(dataReader, "email2");
                    modStaff.GetSetfacebook = replaceNull(dataReader, "facebook");
                    modStaff.GetSetinstagram = replaceNull(dataReader, "instagram");
                    modStaff.GetSetwhatsapp = replaceNull(dataReader, "whatsapp");
                    modStaff.GetSetepfno = replaceNull(dataReader, "epfno");
                    modStaff.GetSetsocsono = replaceNull(dataReader, "socsono");
                    modStaff.GetSettaxno = replaceNull(dataReader, "taxno");
                    modStaff.GetSetbankname = replaceNull(dataReader, "bankname");
                    modStaff.GetSetaccountype = replaceNull(dataReader, "accountype");
                    modStaff.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modStaff.GetSetuserid = replaceNull(dataReader, "userid");
                    modStaff.GetSetpassword = replaceNull(dataReader, "password");
                    modStaff.GetSetusertype = replaceNull(dataReader, "usertype");
                    modStaff.GetSetlastaccess = replaceNull(dataReader, "lastaccess");
                    modStaff.GetSetstr_lastaccess = replaceNull(dataReader, "str_lastaccess");
                    modStaff.GetSetstatuslogon = replaceNull(dataReader, "statuslogon");

                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffDetails: " + e.Message.ToString());
        }
        return modStaff;
    }

    public HRModel getStaffDetails(String comp, String staffno, String staffname)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.salute, a.name, a.nickname, a.nicno, a.oicno, a.iccolor, a.passport, a.ppexpiry, date_format(a.ppexpiry,'%d-%m-%Y %H:%i:%s') str_ppexpiry, a.wkpermit, a.wkexpiry, date_format(a.wkexpiry,'%d-%m-%Y %H:%i:%s') str_wkexpiry, ";
                query = query + "        a.dob, date_format(a.dob,'%d-%m-%Y') str_dob, a.birthplace, a.marital, a.dtmarried, date_format(a.dtmarried,'%d-%m-%Y') str_dtmarried, a.bloodtype, a.race, a.religion, a.gender, a.nationality, a.bumistatus, ";
                query = query + "        a.paddress1, a.paddress2, a.paddress3, a.paddress4, a.ppostcode, a.pcity, a.pstate, a.pcountry, a.ptelephone, ";
                query = query + "        a.caddress1, a.caddress2, a.caddress3, a.caddress4, a.cpostcode, a.ccity, a.cstate, a.ccountry, a.ctelephone, ";
                query = query + "        a.mobile1, a.mobile2, a.datejoined, date_format(a.datejoined,'%d-%m-%Y') str_datejoined, a.retireage, a.retiredate, date_format(a.retiredate,'%d-%m-%Y') str_retiredate, a.email1, a.email2, a.facebook, a.instagram, a.whatsapp, ";
                query = query + "        a.epfno, a.socsono, a.taxno, a.bankname, a.accountype, a.accountno, a.userid, a.password, a.usertype, a.lastaccess, date_format(a.lastaccess,'%d-%m-%Y %H:%i:%s') str_lastaccess, a.statuslogon, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_info a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (staffname.Trim().Length > 0)
                {
                    query = query + " and  upper(a.staffname) like upper('%" + staffname + "%') ";
                }
                query = query + " order by a.comp, a.staffname ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnickname = replaceNull(dataReader, "nickname");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetoicno = replaceNull(dataReader, "oicno");
                    modStaff.GetSeticcolor = replaceNull(dataReader, "iccolor");
                    modStaff.GetSetpassport = replaceNull(dataReader, "passport");
                    modStaff.GetSetppexpiry = replaceNull(dataReader, "ppexpiry");
                    modStaff.GetSetstr_ppexpiry = replaceNull(dataReader, "str_ppexpiry");
                    modStaff.GetSetwkpermit = replaceNull(dataReader, "wkpermit");
                    modStaff.GetSetwkexpiry = replaceNull(dataReader, "wkexpiry");
                    modStaff.GetSetstr_wkexpiry = replaceNull(dataReader, "str_wkexpiry");
                    modStaff.GetSetdob = replaceNull(dataReader, "dob");
                    modStaff.GetSetstr_dob = replaceNull(dataReader, "str_dob");
                    modStaff.GetSetbirthplace = replaceNull(dataReader, "birthplace");
                    modStaff.GetSetmarital = replaceNull(dataReader, "marital");
                    modStaff.GetSetdtmarried = replaceNull(dataReader, "dtmarried");
                    modStaff.GetSetstr_dtmarried = replaceNull(dataReader, "str_dtmarried");
                    modStaff.GetSetbloodtype = replaceNull(dataReader, "bloodtype");
                    modStaff.GetSetrace = replaceNull(dataReader, "race");
                    modStaff.GetSetreligion = replaceNull(dataReader, "religion");
                    modStaff.GetSetgender = replaceNull(dataReader, "gender");
                    modStaff.GetSetnationality = replaceNull(dataReader, "nationality");
                    modStaff.GetSetbumistatus = replaceNull(dataReader, "bumistatus");
                    modStaff.GetSetpaddress1 = replaceNull(dataReader, "paddress1");
                    modStaff.GetSetpaddress2 = replaceNull(dataReader, "paddress2");
                    modStaff.GetSetpaddress3 = replaceNull(dataReader, "paddress3");
                    modStaff.GetSetpaddress4 = replaceNull(dataReader, "paddress4");
                    modStaff.GetSetppostcode = replaceNull(dataReader, "ppostcode");
                    modStaff.GetSetpcity = replaceNull(dataReader, "pcity");
                    modStaff.GetSetpstate = replaceNull(dataReader, "pstate");
                    modStaff.GetSetpcountry = replaceNull(dataReader, "pcountry");
                    modStaff.GetSetptelephone = replaceNull(dataReader, "ptelephone");
                    modStaff.GetSetcaddress1 = replaceNull(dataReader, "caddress1");
                    modStaff.GetSetcaddress2 = replaceNull(dataReader, "caddress2");
                    modStaff.GetSetcaddress3 = replaceNull(dataReader, "caddress3");
                    modStaff.GetSetcaddress4 = replaceNull(dataReader, "caddress4");
                    modStaff.GetSetcpostcode = replaceNull(dataReader, "cpostcode");
                    modStaff.GetSetccity = replaceNull(dataReader, "ccity");
                    modStaff.GetSetcstate = replaceNull(dataReader, "cstate");
                    modStaff.GetSetccountry = replaceNull(dataReader, "ccountry");
                    modStaff.GetSetctelephone = replaceNull(dataReader, "ctelephone");
                    modStaff.GetSetmobile1 = replaceNull(dataReader, "mobile1");
                    modStaff.GetSetmobile2 = replaceNull(dataReader, "mobile2");
                    modStaff.GetSetdatejoined = replaceNull(dataReader, "datejoined");
                    modStaff.GetSetstr_datejoined = replaceNull(dataReader, "str_datejoined");
                    modStaff.GetSetretireage = replaceZero(dataReader, "retireage");
                    modStaff.GetSetretiredate = replaceNull(dataReader, "retiredate");
                    modStaff.GetSetstr_retiredate = replaceNull(dataReader, "str_retiredate");
                    modStaff.GetSetemail1 = replaceNull(dataReader, "email1");
                    modStaff.GetSetemail2 = replaceNull(dataReader, "email2");
                    modStaff.GetSetfacebook = replaceNull(dataReader, "facebook");
                    modStaff.GetSetinstagram = replaceNull(dataReader, "instagram");
                    modStaff.GetSetwhatsapp = replaceNull(dataReader, "whatsapp");
                    modStaff.GetSetepfno = replaceNull(dataReader, "epfno");
                    modStaff.GetSetsocsono = replaceNull(dataReader, "socsono");
                    modStaff.GetSettaxno = replaceNull(dataReader, "taxno");
                    modStaff.GetSetbankname = replaceNull(dataReader, "bankname");
                    modStaff.GetSetaccountype = replaceNull(dataReader, "accountype");
                    modStaff.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modStaff.GetSetuserid = replaceNull(dataReader, "userid");
                    modStaff.GetSetpassword = replaceNull(dataReader, "password");
                    modStaff.GetSetusertype = replaceNull(dataReader, "usertype");
                    modStaff.GetSetlastaccess = replaceNull(dataReader, "lastaccess");
                    modStaff.GetSetstr_lastaccess = replaceNull(dataReader, "str_lastaccess");
                    modStaff.GetSetstatuslogon = replaceNull(dataReader, "statuslogon");

                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffDetails: " + e.Message.ToString());
        }
        return modStaff;
    }

    public HRModel getStaffDetails(String comp, String staffno, String userid, String password)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.staffno, a.salute, a.name, a.nickname, a.nicno, a.oicno, a.iccolor, a.passport, a.ppexpiry, date_format(a.ppexpiry,'%d-%m-%Y %H:%i:%s') str_ppexpiry, a.wkpermit, a.wkexpiry, date_format(a.wkexpiry,'%d-%m-%Y %H:%i:%s') str_wkexpiry, ";
                query = query + "        a.dob, date_format(a.dob,'%d-%m-%Y') str_dob, a.birthplace, a.marital, a.dtmarried, date_format(a.dtmarried,'%d-%m-%Y') str_dtmarried, a.bloodtype, a.race, a.religion, a.gender, a.nationality, a.bumistatus, ";
                query = query + "        b.emp_deptid dept_id, (select dept_name from department_comp c where b.comp = c.comp and b.emp_deptid = c.dept_id) dept_name, ";
                query = query + "        b.emp_gredid gred_id, (select gred_name from grade_comp c where b.comp = c.comp and b.emp_gredid = c.gred_id) gred_name, ";
                query = query + "        b.emp_posid pos_id, (select pos_name from position_comp c where b.comp = c.comp and b.emp_posid = c.pos_id) pos_name, ";
                query = query + "        b.emp_reportto, ";
                query = query + "        a.paddress1, a.paddress2, a.paddress3, a.paddress4, a.ppostcode, a.pcity, a.pstate, a.pcountry, a.ptelephone, ";
                query = query + "        a.caddress1, a.caddress2, a.caddress3, a.caddress4, a.cpostcode, a.ccity, a.cstate, a.ccountry, a.ctelephone, ";
                query = query + "        a.mobile1, a.mobile2, a.datejoined, date_format(a.datejoined,'%d-%m-%Y') str_datejoined, a.retireage, a.retiredate, date_format(a.retiredate,'%d-%m-%Y') str_retiredate, a.email1, a.email2, a.facebook, a.instagram, a.whatsapp, ";
                query = query + "        a.epfno, a.socsono, a.taxno, a.bankname, a.accountype, a.accountno, a.userid, a.password, a.usertype, a.lastaccess, date_format(a.lastaccess,'%d-%m-%Y %H:%i:%s') str_lastaccess, a.statuslogon, ";
                query = query + "        a.remarks, a.status, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " FROM   staff_info a left join staff_employment b on a.comp = b.comp and a.staffno = b.staffno and b.status = 'ACTIVE' ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (userid.Trim().Length > 0)
                {
                    query = query + " and  a.userid = '" + userid + "' ";
                }
                if (password.Trim().Length > 0)
                {
                    query = query + " and  a.password = '" + password + "' ";
                }
                query = query + " order by a.comp, a.staffno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetsalute = replaceNull(dataReader, "salute");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetnickname = replaceNull(dataReader, "nickname");
                    modStaff.GetSetnicno = replaceNull(dataReader, "nicno");
                    modStaff.GetSetoicno = replaceNull(dataReader, "oicno");
                    modStaff.GetSeticcolor = replaceNull(dataReader, "iccolor");
                    modStaff.GetSetpassport = replaceNull(dataReader, "passport");
                    modStaff.GetSetppexpiry = replaceNull(dataReader, "ppexpiry");
                    modStaff.GetSetstr_ppexpiry = replaceNull(dataReader, "str_ppexpiry");
                    modStaff.GetSetwkpermit = replaceNull(dataReader, "wkpermit");
                    modStaff.GetSetwkexpiry = replaceNull(dataReader, "wkexpiry");
                    modStaff.GetSetstr_wkexpiry = replaceNull(dataReader, "str_wkexpiry");
                    modStaff.GetSetdob = replaceNull(dataReader, "dob");
                    modStaff.GetSetstr_dob = replaceNull(dataReader, "str_dob");
                    modStaff.GetSetbirthplace = replaceNull(dataReader, "birthplace");
                    modStaff.GetSetmarital = replaceNull(dataReader, "marital");
                    modStaff.GetSetdtmarried = replaceNull(dataReader, "dtmarried");
                    modStaff.GetSetstr_dtmarried = replaceNull(dataReader, "str_dtmarried");
                    modStaff.GetSetbloodtype = replaceNull(dataReader, "bloodtype");
                    modStaff.GetSetrace = replaceNull(dataReader, "race");
                    modStaff.GetSetreligion = replaceNull(dataReader, "religion");
                    modStaff.GetSetgender = replaceNull(dataReader, "gender");
                    modStaff.GetSetnationality = replaceNull(dataReader, "nationality");
                    modStaff.GetSetbumistatus = replaceNull(dataReader, "bumistatus");

                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetreportto = replaceNull(dataReader, "emp_reportto");

                    modStaff.GetSetpaddress1 = replaceNull(dataReader, "paddress1");
                    modStaff.GetSetpaddress2 = replaceNull(dataReader, "paddress2");
                    modStaff.GetSetpaddress3 = replaceNull(dataReader, "paddress3");
                    modStaff.GetSetpaddress4 = replaceNull(dataReader, "paddress4");
                    modStaff.GetSetppostcode = replaceNull(dataReader, "ppostcode");
                    modStaff.GetSetpcity = replaceNull(dataReader, "pcity");
                    modStaff.GetSetpstate = replaceNull(dataReader, "pstate");
                    modStaff.GetSetpcountry = replaceNull(dataReader, "pcountry");
                    modStaff.GetSetptelephone = replaceNull(dataReader, "ptelephone");
                    modStaff.GetSetcaddress1 = replaceNull(dataReader, "caddress1");
                    modStaff.GetSetcaddress2 = replaceNull(dataReader, "caddress2");
                    modStaff.GetSetcaddress3 = replaceNull(dataReader, "caddress3");
                    modStaff.GetSetcaddress4 = replaceNull(dataReader, "caddress4");
                    modStaff.GetSetcpostcode = replaceNull(dataReader, "cpostcode");
                    modStaff.GetSetccity = replaceNull(dataReader, "ccity");
                    modStaff.GetSetcstate = replaceNull(dataReader, "cstate");
                    modStaff.GetSetccountry = replaceNull(dataReader, "ccountry");
                    modStaff.GetSetctelephone = replaceNull(dataReader, "ctelephone");
                    modStaff.GetSetmobile1 = replaceNull(dataReader, "mobile1");
                    modStaff.GetSetmobile2 = replaceNull(dataReader, "mobile2");
                    modStaff.GetSetdatejoined = replaceNull(dataReader, "datejoined");
                    modStaff.GetSetstr_datejoined = replaceNull(dataReader, "str_datejoined");
                    modStaff.GetSetretireage = replaceZero(dataReader, "retireage");
                    modStaff.GetSetretiredate = replaceNull(dataReader, "retiredate");
                    modStaff.GetSetstr_retiredate = replaceNull(dataReader, "str_retiredate");
                    modStaff.GetSetemail1 = replaceNull(dataReader, "email1");
                    modStaff.GetSetemail2 = replaceNull(dataReader, "email2");
                    modStaff.GetSetfacebook = replaceNull(dataReader, "facebook");
                    modStaff.GetSetinstagram = replaceNull(dataReader, "instagram");
                    modStaff.GetSetwhatsapp = replaceNull(dataReader, "whatsapp");
                    modStaff.GetSetepfno = replaceNull(dataReader, "epfno");
                    modStaff.GetSetsocsono = replaceNull(dataReader, "socsono");
                    modStaff.GetSettaxno = replaceNull(dataReader, "taxno");
                    modStaff.GetSetbankname = replaceNull(dataReader, "bankname");
                    modStaff.GetSetaccountype = replaceNull(dataReader, "accountype");
                    modStaff.GetSetaccountno = replaceNull(dataReader, "accountno");
                    modStaff.GetSetuserid = replaceNull(dataReader, "userid");
                    modStaff.GetSetpassword = replaceNull(dataReader, "password");
                    modStaff.GetSetusertype = replaceNull(dataReader, "usertype");
                    modStaff.GetSetlastaccess = replaceNull(dataReader, "lastaccess");
                    modStaff.GetSetstr_lastaccess = replaceNull(dataReader, "str_lastaccess");
                    modStaff.GetSetstatuslogon = replaceNull(dataReader, "statuslogon");

                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffDetails: " + e.Message.ToString());
        }
        return modStaff;
    }

    public String insertStaffInfo(HRModel oModStaff)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_info (comp, staffno, salute, name, nickname, nicno, oicno, iccolor, passport, ppexpiry, wkpermit, wkexpiry, dob, birthplace, ";
                query = query + " marital, dtmarried, bloodtype, race, religion, gender, nationality, bumistatus, paddress1, paddress2, paddress3, paddress4, ppostcode, pcity, ";
                query = query + " pstate, pcountry, ptelephone, caddress1, caddress2, caddress3, caddress4, cpostcode, ccity, cstate, ccountry, ctelephone, mobile1, mobile2, ";
                query = query + " datejoined, retireage, retiredate, email1, email2, facebook, instagram, whatsapp, epfno, socsono, taxno, bankname, accountype, accountno, userid, password, ";
                query = query + " usertype, lastaccess, statuslogon, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";

                query = query + " VALUES (?comp, ?staffno, ?salute, ?name, ?nickname, ?nicno, ?oicno, ?iccolor, ?passport, ?ppexpiry, ?wkpermit, ?wkexpiry, ?dob, ?birthplace, ";
                query = query + " ?marital, ?dtmarried, ?bloodtype, ?race, ?religion, ?gender, ?nationality, ?bumistatus, ?paddress1, ?paddress2, ?paddress3, ?paddress4, ?ppostcode, ?pcity, ";
                query = query + " ?pstate, ?pcountry, ?ptelephone, ?caddress1, ?caddress2, ?caddress3, ?caddress4, ?cpostcode, ?ccity, ?cstate, ?ccountry, ?ctelephone, ?mobile1, ?mobile2, ";
                query = query + " ?datejoined, ?retireage, ?retiredate, ?email1, ?email2, ?facebook, ?instagram, ?whatsapp, ?epfno, ?socsono, ?taxno, ?bankname, ?accountype, ?accountno, ?userid, ?password, ";
                query = query + " ?usertype, ?lastaccess, ?statuslogon, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModStaff.GetSetcomp;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModStaff.GetSetstaffno;
                cmd.Parameters.Add("?salute", MySqlDbType.VarChar).Value = oModStaff.GetSetsalute;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = oModStaff.GetSetname;
                cmd.Parameters.Add("?nickname", MySqlDbType.VarChar).Value = oModStaff.GetSetnickname;
                cmd.Parameters.Add("?nicno", MySqlDbType.VarChar).Value = oModStaff.GetSetnicno;
                cmd.Parameters.Add("?oicno", MySqlDbType.VarChar).Value = oModStaff.GetSetoicno;
                cmd.Parameters.Add("?iccolor", MySqlDbType.VarChar).Value = oModStaff.GetSeticcolor;
                cmd.Parameters.Add("?passport", MySqlDbType.VarChar).Value = oModStaff.GetSetpassport;
                if (oModStaff.GetSetppexpiry.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetppexpiry, ukDtfi);
                    cmd.Parameters.Add("?ppexpiry", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?ppexpiry", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?wkpermit", MySqlDbType.VarChar).Value = oModStaff.GetSetwkpermit;
                if (oModStaff.GetSetwkexpiry.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetwkexpiry, ukDtfi);
                    cmd.Parameters.Add("?wkexpiry", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?wkexpiry", MySqlDbType.Date).Value = null;
                }
                if (oModStaff.GetSetdob.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdob, ukDtfi);
                    cmd.Parameters.Add("?dob", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?dob", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?birthplace", MySqlDbType.VarChar).Value = oModStaff.GetSetbirthplace;

                cmd.Parameters.Add("?marital", MySqlDbType.VarChar).Value = oModStaff.GetSetmarital;
                if (oModStaff.GetSetdtmarried.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdtmarried, ukDtfi);
                    cmd.Parameters.Add("?dtmarried", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?dtmarried", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?bloodtype", MySqlDbType.VarChar).Value = oModStaff.GetSetbloodtype;
                cmd.Parameters.Add("?race", MySqlDbType.VarChar).Value = oModStaff.GetSetrace;
                cmd.Parameters.Add("?religion", MySqlDbType.VarChar).Value = oModStaff.GetSetreligion;
                cmd.Parameters.Add("?gender", MySqlDbType.VarChar).Value = oModStaff.GetSetgender;
                cmd.Parameters.Add("?nationality", MySqlDbType.VarChar).Value = oModStaff.GetSetnationality;
                cmd.Parameters.Add("?bumistatus", MySqlDbType.VarChar).Value = oModStaff.GetSetbumistatus;
                cmd.Parameters.Add("?paddress1", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress1;
                cmd.Parameters.Add("?paddress2", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress2;
                cmd.Parameters.Add("?paddress3", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress3;
                cmd.Parameters.Add("?paddress4", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress4;
                cmd.Parameters.Add("?ppostcode", MySqlDbType.VarChar).Value = oModStaff.GetSetppostcode;
                cmd.Parameters.Add("?pcity", MySqlDbType.VarChar).Value = oModStaff.GetSetpcity;
                cmd.Parameters.Add("?pstate", MySqlDbType.VarChar).Value = oModStaff.GetSetpstate;
                cmd.Parameters.Add("?pcountry", MySqlDbType.VarChar).Value = oModStaff.GetSetpcountry;
                cmd.Parameters.Add("?ptelephone", MySqlDbType.VarChar).Value = oModStaff.GetSetptelephone;
                cmd.Parameters.Add("?caddress1", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress1;
                cmd.Parameters.Add("?caddress2", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress2;
                cmd.Parameters.Add("?caddress3", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress3;
                cmd.Parameters.Add("?caddress4", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress4;
                cmd.Parameters.Add("?cpostcode", MySqlDbType.VarChar).Value = oModStaff.GetSetcpostcode;
                cmd.Parameters.Add("?ccity", MySqlDbType.VarChar).Value = oModStaff.GetSetccity;

                cmd.Parameters.Add("?cstate", MySqlDbType.VarChar).Value = oModStaff.GetSetcstate;
                cmd.Parameters.Add("?ccountry", MySqlDbType.VarChar).Value = oModStaff.GetSetccountry;
                cmd.Parameters.Add("?ctelephone", MySqlDbType.VarChar).Value = oModStaff.GetSetctelephone;
                cmd.Parameters.Add("?mobile1", MySqlDbType.VarChar).Value = oModStaff.GetSetmobile1;
                cmd.Parameters.Add("?mobile2", MySqlDbType.VarChar).Value = oModStaff.GetSetmobile2;
                if (oModStaff.GetSetdatejoined.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdatejoined, ukDtfi);
                    cmd.Parameters.Add("?datejoined", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?datejoined", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?retireage", MySqlDbType.Int16).Value = oModStaff.GetSetretireage;
                if (oModStaff.GetSetretiredate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetretiredate, ukDtfi);
                    cmd.Parameters.Add("?retiredate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?retiredate", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?email1", MySqlDbType.VarChar).Value = oModStaff.GetSetemail1;
                cmd.Parameters.Add("?email2", MySqlDbType.VarChar).Value = oModStaff.GetSetemail2;
                cmd.Parameters.Add("?facebook", MySqlDbType.VarChar).Value = oModStaff.GetSetfacebook;
                cmd.Parameters.Add("?instagram", MySqlDbType.VarChar).Value = oModStaff.GetSetinstagram;
                cmd.Parameters.Add("?whatsapp", MySqlDbType.VarChar).Value = oModStaff.GetSetwhatsapp;
                cmd.Parameters.Add("?epfno", MySqlDbType.VarChar).Value = oModStaff.GetSetepfno;
                cmd.Parameters.Add("?socsono", MySqlDbType.VarChar).Value = oModStaff.GetSetsocsono;
                cmd.Parameters.Add("?taxno", MySqlDbType.VarChar).Value = oModStaff.GetSettaxno;
                cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = oModStaff.GetSetbankname;
                cmd.Parameters.Add("?accountype", MySqlDbType.VarChar).Value = oModStaff.GetSetaccountype;
                cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = oModStaff.GetSetaccountno;
                cmd.Parameters.Add("?userid", MySqlDbType.VarChar).Value = oModStaff.GetSetuserid;
                cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = oModStaff.GetSetpassword;

                cmd.Parameters.Add("?usertype", MySqlDbType.VarChar).Value = oModStaff.GetSetusertype;
                if (oModStaff.GetSetlastaccess.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetlastaccess, ukDtfi);
                    cmd.Parameters.Add("?lastaccess", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?lastaccess", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?statuslogon", MySqlDbType.VarChar).Value = oModStaff.GetSetstatuslogon;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModStaff.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModStaff.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModStaff.GetSetcreatedby;
                if (oModStaff.GetSetcreatedby.Length > 0)
                {
                    if (oModStaff.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModStaff.GetSetmodifiedby;
                if (oModStaff.GetSetmodifiedby.Length > 0)
                {
                    if (oModStaff.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModStaff.GetSetconfirmedby;
                if (oModStaff.GetSetconfirmedby.Length > 0)
                {
                    if (oModStaff.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModStaff.GetSetcancelledby;
                if (oModStaff.GetSetcancelledby.Length > 0)
                {
                    if (oModStaff.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffInfo: " + e.Message.ToString());
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

    public String updateStaffInfo(HRModel oModStaff)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_info ";
                query = query + " SET    salute = ?salute, name = ?name, nickname = ?nickname, nicno = ?nicno, oicno = ?oicno, iccolor = ?iccolor, passport = ?passport, ppexpiry = ?ppexpiry, wkpermit = ?wkpermit, wkexpiry = ?wkexpiry, dob = ?dob, birthplace = ?birthplace, ";
                query = query + " marital = ?marital, dtmarried = ?dtmarried, bloodtype = ?bloodtype, race = ?race, religion = ?religion, gender = ?gender, nationality = ?nationality, bumistatus = ?bumistatus, paddress1 = ?paddress1, paddress2 = ?paddress2, paddress3 = ?paddress3, paddress4 = ?paddress4, ppostcode = ?ppostcode, pcity = ?pcity, ";
                query = query + " pstate = ?pstate, pcountry = ?pcountry, ptelephone = ?ptelephone, caddress1 = ?caddress1, caddress2 = ?caddress2, caddress3 = ?caddress3, caddress4 = ?caddress4, cpostcode = ?cpostcode, ccity = ?ccity, cstate = ?cstate, ccountry = ?ccountry, ctelephone = ?ctelephone, mobile1 = ?mobile1, mobile2 = ?mobile2, ";
                query = query + " datejoined = ?datejoined, retireage = ?retireage, retiredate = ?retiredate, email1 = ?email1, email2 = ?email2, facebook = ?facebook, instagram = ?instagram, whatsapp = ?whatsapp, epfno = ?epfno, socsono = ?socsono, taxno = ?taxno, bankname = ?bankname, accountype = ?accountype, accountno = ?accountno, userid = ?userid, password = ?password, ";
                query = query + " usertype = ?usertype, lastaccess = ?lastaccess, statuslogon = ?statuslogon, remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND staffno = ?staffno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModStaff.GetSetcomp;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModStaff.GetSetstaffno;

                cmd.Parameters.Add("?salute", MySqlDbType.VarChar).Value = oModStaff.GetSetsalute;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = oModStaff.GetSetname;
                cmd.Parameters.Add("?nickname", MySqlDbType.VarChar).Value = oModStaff.GetSetnickname;
                cmd.Parameters.Add("?nicno", MySqlDbType.VarChar).Value = oModStaff.GetSetnicno;
                cmd.Parameters.Add("?oicno", MySqlDbType.VarChar).Value = oModStaff.GetSetoicno;
                cmd.Parameters.Add("?iccolor", MySqlDbType.VarChar).Value = oModStaff.GetSeticcolor;
                cmd.Parameters.Add("?passport", MySqlDbType.VarChar).Value = oModStaff.GetSetpassport;
                if (oModStaff.GetSetppexpiry.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetppexpiry, ukDtfi);
                    cmd.Parameters.Add("?ppexpiry", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?ppexpiry", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?wkpermit", MySqlDbType.VarChar).Value = oModStaff.GetSetwkpermit;
                if (oModStaff.GetSetwkexpiry.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetwkexpiry, ukDtfi);
                    cmd.Parameters.Add("?wkexpiry", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?wkexpiry", MySqlDbType.Date).Value = null;
                }
                if (oModStaff.GetSetdob.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdob, ukDtfi);
                    cmd.Parameters.Add("?dob", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?dob", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?birthplace", MySqlDbType.VarChar).Value = oModStaff.GetSetbirthplace;

                cmd.Parameters.Add("?marital", MySqlDbType.VarChar).Value = oModStaff.GetSetmarital;
                if (oModStaff.GetSetdtmarried.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdtmarried, ukDtfi);
                    cmd.Parameters.Add("?dtmarried", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?dtmarried", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?bloodtype", MySqlDbType.VarChar).Value = oModStaff.GetSetbloodtype;
                cmd.Parameters.Add("?race", MySqlDbType.VarChar).Value = oModStaff.GetSetrace;
                cmd.Parameters.Add("?religion", MySqlDbType.VarChar).Value = oModStaff.GetSetreligion;
                cmd.Parameters.Add("?gender", MySqlDbType.VarChar).Value = oModStaff.GetSetgender;
                cmd.Parameters.Add("?nationality", MySqlDbType.VarChar).Value = oModStaff.GetSetnationality;
                cmd.Parameters.Add("?bumistatus", MySqlDbType.VarChar).Value = oModStaff.GetSetbumistatus;
                cmd.Parameters.Add("?paddress1", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress1;
                cmd.Parameters.Add("?paddress2", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress2;
                cmd.Parameters.Add("?paddress3", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress3;
                cmd.Parameters.Add("?paddress4", MySqlDbType.VarChar).Value = oModStaff.GetSetpaddress4;
                cmd.Parameters.Add("?ppostcode", MySqlDbType.VarChar).Value = oModStaff.GetSetppostcode;
                cmd.Parameters.Add("?pcity", MySqlDbType.VarChar).Value = oModStaff.GetSetpcity;
                cmd.Parameters.Add("?pstate", MySqlDbType.VarChar).Value = oModStaff.GetSetpstate;
                cmd.Parameters.Add("?pcountry", MySqlDbType.VarChar).Value = oModStaff.GetSetpcountry;
                cmd.Parameters.Add("?ptelephone", MySqlDbType.VarChar).Value = oModStaff.GetSetptelephone;
                cmd.Parameters.Add("?caddress1", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress1;
                cmd.Parameters.Add("?caddress2", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress2;
                cmd.Parameters.Add("?caddress3", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress3;
                cmd.Parameters.Add("?caddress4", MySqlDbType.VarChar).Value = oModStaff.GetSetcaddress4;
                cmd.Parameters.Add("?cpostcode", MySqlDbType.VarChar).Value = oModStaff.GetSetcpostcode;
                cmd.Parameters.Add("?ccity", MySqlDbType.VarChar).Value = oModStaff.GetSetccity;

                cmd.Parameters.Add("?cstate", MySqlDbType.VarChar).Value = oModStaff.GetSetcstate;
                cmd.Parameters.Add("?ccountry", MySqlDbType.VarChar).Value = oModStaff.GetSetccountry;
                cmd.Parameters.Add("?ctelephone", MySqlDbType.VarChar).Value = oModStaff.GetSetctelephone;
                cmd.Parameters.Add("?mobile1", MySqlDbType.VarChar).Value = oModStaff.GetSetmobile1;
                cmd.Parameters.Add("?mobile2", MySqlDbType.VarChar).Value = oModStaff.GetSetmobile2;

                if (oModStaff.GetSetdatejoined.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetdatejoined, ukDtfi);
                    cmd.Parameters.Add("?datejoined", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?datejoined", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?retireage", MySqlDbType.Int16).Value = oModStaff.GetSetretireage;
                if (oModStaff.GetSetretiredate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetretiredate, ukDtfi);
                    cmd.Parameters.Add("?retiredate", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?retiredate", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?email1", MySqlDbType.VarChar).Value = oModStaff.GetSetemail1;
                cmd.Parameters.Add("?email2", MySqlDbType.VarChar).Value = oModStaff.GetSetemail2;
                cmd.Parameters.Add("?facebook", MySqlDbType.VarChar).Value = oModStaff.GetSetfacebook;
                cmd.Parameters.Add("?instagram", MySqlDbType.VarChar).Value = oModStaff.GetSetinstagram;
                cmd.Parameters.Add("?whatsapp", MySqlDbType.VarChar).Value = oModStaff.GetSetwhatsapp;
                cmd.Parameters.Add("?epfno", MySqlDbType.VarChar).Value = oModStaff.GetSetepfno;
                cmd.Parameters.Add("?socsono", MySqlDbType.VarChar).Value = oModStaff.GetSetsocsono;
                cmd.Parameters.Add("?taxno", MySqlDbType.VarChar).Value = oModStaff.GetSettaxno;
                cmd.Parameters.Add("?bankname", MySqlDbType.VarChar).Value = oModStaff.GetSetbankname;
                cmd.Parameters.Add("?accountype", MySqlDbType.VarChar).Value = oModStaff.GetSetaccountype;
                cmd.Parameters.Add("?accountno", MySqlDbType.VarChar).Value = oModStaff.GetSetaccountno;
                cmd.Parameters.Add("?userid", MySqlDbType.VarChar).Value = oModStaff.GetSetuserid;
                cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = oModStaff.GetSetpassword;

                cmd.Parameters.Add("?usertype", MySqlDbType.VarChar).Value = oModStaff.GetSetusertype;
                if (oModStaff.GetSetlastaccess.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModStaff.GetSetlastaccess, ukDtfi);
                    cmd.Parameters.Add("?lastaccess", MySqlDbType.Date).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?lastaccess", MySqlDbType.Date).Value = null;
                }
                cmd.Parameters.Add("?statuslogon", MySqlDbType.VarChar).Value = oModStaff.GetSetstatuslogon;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModStaff.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModStaff.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModStaff.GetSetcreatedby;
                if (oModStaff.GetSetcreatedby.Length > 0)
                {
                    if (oModStaff.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModStaff.GetSetmodifiedby;
                if (oModStaff.GetSetmodifiedby.Length > 0)
                {
                    if (oModStaff.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModStaff.GetSetconfirmedby;
                if (oModStaff.GetSetconfirmedby.Length > 0)
                {
                    if (oModStaff.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModStaff.GetSetcancelledby;
                if (oModStaff.GetSetcancelledby.Length > 0)
                {
                    if (oModStaff.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModStaff.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffInfo: " + e.Message.ToString());
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

    public ArrayList getCompPublicHolidayList(String comp, String fyr, String phdate, String code, String desc)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.ph_date,'%d-%m-%Y') ph_date, a.fromdate, a.todate, a.ph_type, a.ph_cat, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   publicholiday_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (phdate.Trim().Length > 0)
                {
                    query = query + " and  a.ph_date = ?ph_date ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.fyr, a.ph_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (phdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(phdate, ukDtfi);
                    cmd.Parameters.Add("?ph_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetph_date = replaceNull(dataReader, "ph_date");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSettype = replaceNull(dataReader, "ph_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "ph_cat");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPublicHolidayList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public ArrayList getCompPublicHolidayStringList(String comp, String fyr, String phdate, String code, String desc)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.ph_date,'%d-%m-%Y') ph_date, a.fromdate, a.todate, a.ph_type, a.ph_cat, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   publicholiday_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (phdate.Trim().Length > 0)
                {
                    query = query + " and  a.ph_date = ?ph_date ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.fyr, a.ph_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (phdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(phdate, ukDtfi);
                    cmd.Parameters.Add("?ph_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    String ph_date = replaceNull(dataReader, "ph_date");                    
                    lsCompGred.Add(ph_date);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPublicHolidayStringList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getCompPublicHolidayDetails(String comp, String fyr, String phdate, String code, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.ph_date,'%d-%m-%Y') ph_date, a.fromdate, a.todate, a.ph_type, a.ph_cat, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   publicholiday_comp a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (phdate.Trim().Length > 0)
                {
                    query = query + " and  a.ph_date = ?ph_date ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if(id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.ph_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (phdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(phdate, ukDtfi);
                    cmd.Parameters.Add("?ph_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetph_date = replaceNull(dataReader, "ph_date");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSettype = replaceNull(dataReader, "ph_type");
                    modStaff.GetSetcat = replaceNull(dataReader, "ph_cat");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompPublicHolidayDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertCompPublicHoliday(HRModel oModPH)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO publicholiday_comp (comp, fyr, code, description, ph_date, fromdate, todate, ph_type, ph_cat, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?code, ?description, ?ph_date, ?fromdate, ?todate, ?ph_type, ?ph_cat, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPH.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModPH.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModPH.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModPH.GetSetdesc;
                if (oModPH.GetSetph_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSetph_date, ukDtfi);
                    cmd.Parameters.Add("?ph_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?ph_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModPH.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModPH.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?ph_type", MySqlDbType.VarChar).Value = oModPH.GetSettype;
                cmd.Parameters.Add("?ph_cat", MySqlDbType.VarChar).Value = oModPH.GetSetcat;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModPH.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModPH.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModPH.GetSetcreatedby;
                if (oModPH.GetSetcreatedby.Length > 0)
                {
                    if (oModPH.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModPH.GetSetmodifiedby;
                if (oModPH.GetSetmodifiedby.Length > 0)
                {
                    if (oModPH.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModPH.GetSetconfirmedby;
                if (oModPH.GetSetconfirmedby.Length > 0)
                {
                    if (oModPH.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModPH.GetSetcancelledby;
                if (oModPH.GetSetcancelledby.Length > 0)
                {
                    if (oModPH.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompPublicHoliday: " + e.Message.ToString());
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

    public String updateCompPublicHoliday(HRModel oModPH)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE publicholiday_comp ";
                query = query + " SET    fyr = ?fyr, code = ?code, description = ?description, ph_date = ?ph_date, fromdate = ?fromdate, todate = ?todate, ph_type = ?ph_type, ph_cat = ?ph_cat, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModPH.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPH.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModPH.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModPH.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModPH.GetSetdesc;
                if (oModPH.GetSetph_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSetph_date, ukDtfi);
                    cmd.Parameters.Add("?ph_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?ph_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModPH.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModPH.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModPH.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?ph_type", MySqlDbType.VarChar).Value = oModPH.GetSettype;
                cmd.Parameters.Add("?ph_cat", MySqlDbType.VarChar).Value = oModPH.GetSetcat;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModPH.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModPH.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModPH.GetSetcreatedby;
                if (oModPH.GetSetcreatedby.Length > 0)
                {
                    if (oModPH.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModPH.GetSetmodifiedby;
                if (oModPH.GetSetmodifiedby.Length > 0)
                {
                    if (oModPH.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModPH.GetSetconfirmedby;
                if (oModPH.GetSetconfirmedby.Length > 0)
                {
                    if (oModPH.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModPH.GetSetcancelledby;
                if (oModPH.GetSetcancelledby.Length > 0)
                {
                    if (oModPH.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModPH.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompPublicHoliday: " + e.Message.ToString());
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

    public String deleteCompPublicHoliday(HRModel oModPH)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM publicholiday_comp ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModPH.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModPH.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompPublicHoliday: " + e.Message.ToString());
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

    public ArrayList getCompWorkingGroupList(String comp, String fyr, String code, String desc, String fromdate, String todate)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, rest_day, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   attendance_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and  a.fromdate = ?fromdate ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and  a.todate = ?todate ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.fyr, a.fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (fromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(fromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.Date).Value = datetime;
                }
                if (todate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(todate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompWorkingGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getCompWorkingGroupDetails(String comp, String fyr, String code, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, rest_day, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   attendance_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompWorkingGroupDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertCompWorkingGroup(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO attendance_group (comp, fyr, code, description, fromdate, todate, rest_day, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?code, ?description, ?fromdate, ?todate, ?rest_day, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWG.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModWG.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModWG.GetSetdesc;
                if (oModWG.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModWG.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?rest_day", MySqlDbType.VarChar).Value = oModWG.GetSetrest_day;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModWG.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModWG.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModWG.GetSetcreatedby;
                if (oModWG.GetSetcreatedby.Length > 0)
                {
                    if (oModWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModWG.GetSetmodifiedby;
                if (oModWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModWG.GetSetconfirmedby;
                if (oModWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModWG.GetSetcancelledby;
                if (oModWG.GetSetcancelledby.Length > 0)
                {
                    if (oModWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompWorkingGroup: " + e.Message.ToString());
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

    public String updateCompWorkingGroup(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE attendance_group ";
                query = query + " SET    fyr = ?fyr, code = ?code, description = ?description, fromdate = ?fromdate, todate = ?todate, rest_day = ?rest_day, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWG.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModWG.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModWG.GetSetdesc;
                if (oModWG.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModWG.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?rest_day", MySqlDbType.VarChar).Value = oModWG.GetSetrest_day;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModWG.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModWG.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModWG.GetSetcreatedby;
                if (oModWG.GetSetcreatedby.Length > 0)
                {
                    if (oModWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModWG.GetSetmodifiedby;
                if (oModWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModWG.GetSetconfirmedby;
                if (oModWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModWG.GetSetcancelledby;
                if (oModWG.GetSetcancelledby.Length > 0)
                {
                    if (oModWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompWorkingGroup: " + e.Message.ToString());
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

    public String deleteCompWorkingGroup(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM attendance_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompWorkingGroup: " + e.Message.ToString());
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

    public ArrayList getCompWorkingDayList(String comp, String fyr, String code, String daydate)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, next_day, follow_previous ";
                query = query + " from   attendance_group_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (daydate.Trim().Length > 0)
                {
                    query = query + " and  c.day_date = ?day_date ";
                }
                query = query + " order by a.comp, a.fyr, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (daydate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(daydate, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetday_date = replaceNull(dataReader, "day_date");
                    modStaff.GetSetday_name = replaceNull(dataReader, "day_name");
                    modStaff.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modStaff.GetSettotime = replaceNull(dataReader, "totime");
                    modStaff.GetSetnext_day = replaceNull(dataReader, "next_day");
                    modStaff.GetSetfollow_previous = replaceNull(dataReader, "follow_previous");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompWorkingDayList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getCompWorkingDayDetails(String comp, String fyr, String code, string daydate, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, next_day, follow_previous ";
                query = query + " from   attendance_group_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (daydate.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (daydate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(daydate, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetday_name = replaceNull(dataReader, "day_name");
                    modStaff.GetSetday_date = replaceNull(dataReader, "day_date");
                    modStaff.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modStaff.GetSettotime = replaceNull(dataReader, "totime");
                    modStaff.GetSetnext_day = replaceNull(dataReader, "next_day");
                    modStaff.GetSetfollow_previous = replaceNull(dataReader, "follow_previous");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompWorkingDayDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertCompWorkingDay(HRModel oModWD)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO attendance_group_day (comp, fyr, code, day_date, fromtime, totime, next_day, follow_previous) ";
                query = query + " VALUES (?comp, ?fyr, ?code, ?day_date, ?fromtime, ?totime, ?next_day, ?follow_previous) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWD.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWD.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModWD.GetSetcode;
                if (oModWD.GetSetday_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSetday_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModWD.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModWD.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?next_day", MySqlDbType.VarChar).Value = oModWD.GetSetnext_day;
                cmd.Parameters.Add("?follow_previous", MySqlDbType.VarChar).Value = oModWD.GetSetfollow_previous;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompWorkingDay: " + e.Message.ToString());
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

    public String insertCompWorkingDayBulk(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO attendance_group_day (comp, fyr, wg_id, code, day_name, day_date, fromtime, totime, next_day, follow_previous) ";
                query = query + " SELECT ?comp, ?fyr, ?wg_id, ?code, ";
                query = query + "        (case dayname(selected_date) when 'Sunday' then 'AHAD' when 'Monday' then 'ISNIN' when 'Tuesday' then 'SELASA' when 'Wednesday' then 'RABU' when 'Thursday' then 'KHAMIS' when 'Friday' then 'JUMAAT' when 'Saturday' then 'SABTU' else null end) as day_name, ";
                query = query + "        selected_date as day_date, '08:00:00', '17:00:00', '0', ";
                query = query + "        (case when selected_date = STR_TO_DATE('"+ oModWG.GetSetfromdate + "','%d-%m-%Y') then '0' else '1' end) as follow_previous FROM ";
                query = query + @"       (select adddate('1970-01-01',t4*10000 + t3*1000 + t2*100 + t1*10 + t0) selected_date from
                                             (select 0 t0 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t0,
                                             (select 0 t1 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t1,
                                             (select 0 t2 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t2,
                                             (select 0 t3 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t3,
                                             (select 0 t4 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t4) v  ";
                query = query + " WHERE selected_date between STR_TO_DATE('" + oModWG.GetSetfromdate + "','%d-%m-%Y') and STR_TO_DATE('" + oModWG.GetSettodate + "','%d-%m-%Y') ";
                query = query + " AND selected_date not in (select day_date from attendance_group_day where comp = ?comp and fyr = ?fyr and wg_id = ?wg_id) ";
                //WriteToLogFile("HRController-insertCompWorkingDayBulk: [SQL] -> " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWG.GetSetfyr;
                cmd.Parameters.Add("?wg_id", MySqlDbType.Int64).Value = oModWG.GetSetid;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModWG.GetSetcode;
                /*
                if (oModWG.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    //cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = null;
                }
                if (oModWG.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    //cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = null;
                }
                */
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompWorkingDayBulk: " + e.Message.ToString());
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

    public String updateCompWorkingDay(HRModel oModWD)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE attendance_group_day ";
                query = query + " SET    fyr = ?fyr, code = ?code, day_date = ?day_date, fromtime = ?fromtime, totime = ?totime, next_day = ?next_day, follow_previous = ?follow_previous ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModWD.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWD.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWD.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModWD.GetSetcode;
                if (oModWD.GetSetday_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSetday_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModWD.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModWD.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWD.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?next_day", MySqlDbType.VarChar).Value = oModWD.GetSetnext_day;
                cmd.Parameters.Add("?follow_previous", MySqlDbType.VarChar).Value = oModWD.GetSetfollow_previous;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompWorkingDay: " + e.Message.ToString());
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

    public String updateCompWorkingDayBulk(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM attendance_group_day ";
                query = query + " WHERE comp = ?comp AND fyr = ?fyr AND wg_id = ?wg_id ";
                query = query + " AND (day_date < STR_TO_DATE('" + oModWG.GetSetfromdate + "','%d-%m-%Y') OR day_date > STR_TO_DATE('" + oModWG.GetSettodate + "','%d-%m-%Y')) ";
                //WriteToLogFile("HRController-updateCompWorkingDayBulk: [SQL] -> " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWG.GetSetfyr;
                cmd.Parameters.Add("?wg_id", MySqlDbType.Int64).Value = oModWG.GetSetid;
                /*
                if (oModWG.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?datefrom", MySqlDbType.DateTime).Value = null;
                }
                if (oModWG.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModWG.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?dateto", MySqlDbType.DateTime).Value = null;
                }
                */
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompWorkingDayBulk: " + e.Message.ToString());
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

    public String deleteCompWorkingDay(HRModel oModWD)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM attendance_group_day ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModWD.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWD.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompWorkingDay: " + e.Message.ToString());
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

    public String deleteCompWorkingDayBulk(HRModel oModWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM attendance_group_day ";
                query = query + " WHERE comp = ?comp AND fyr = ?fyr AND wg_id = ?wg_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModWG.GetSetfyr;
                cmd.Parameters.Add("?wg_id", MySqlDbType.Int64).Value = oModWG.GetSetid;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompWorkingDayBulk: " + e.Message.ToString());
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

    public ArrayList getStaffAttendanceGroupList(String comp, String fyr, String staffno, Int64 grpid, String grpcode, String status)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.wg_id, d.code, d.description, date_format(d.fromdate,'%d-%m-%Y') fromdate, date_format(d.todate,'%d-%m-%Y') todate, d.rest_day, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_attendance_group a, staff_info b, staff_employment c, attendance_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.wg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '"+staffno+"' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.wg_id = " + grpid;
                }
                if (grpcode.Trim().Length > 0)
                {
                    query = query + " and  d.code = '" + grpcode + "' ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, d.fromdate ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetwg_id = replaceZero(dataReader, "wg_id");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffAttendanceGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getStaffAttendanceGroupDetails(String comp, String fyr, String staffno, Int64 grpid, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.wg_id, d.code, d.description, date_format(d.fromdate,'%d-%m-%Y') fromdate, date_format(d.todate,'%d-%m-%Y') todate, d.rest_day, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_attendance_group a, staff_info b, staff_employment c, attendance_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.wg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.wg_id = " + grpid;
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetwg_id = replaceZero(dataReader, "wg_id");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffAttendanceGroupDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertStaffAttendanceGroup(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_attendance_group (comp, fyr, staffno, wg_id, code, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?wg_id, ?code, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModSWG.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModSWG.GetSetstaffno;
                cmd.Parameters.Add("?wg_id", MySqlDbType.Int64).Value = oModSWG.GetSetwg_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModSWG.GetSetcode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModSWG.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModSWG.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModSWG.GetSetcreatedby;
                if (oModSWG.GetSetcreatedby.Length > 0)
                {
                    if (oModSWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModSWG.GetSetmodifiedby;
                if (oModSWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModSWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModSWG.GetSetconfirmedby;
                if (oModSWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModSWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModSWG.GetSetcancelledby;
                if (oModSWG.GetSetcancelledby.Length > 0)
                {
                    if (oModSWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffAttendanceGroup: " + e.Message.ToString());
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

    public String updateStaffAttendanceGroup(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_attendance_group ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, wg_id = ?wg_id, code = ?code, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModSWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModSWG.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModSWG.GetSetstaffno;
                cmd.Parameters.Add("?wg_id", MySqlDbType.Int64).Value = oModSWG.GetSetwg_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModSWG.GetSetcode;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModSWG.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModSWG.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModSWG.GetSetcreatedby;
                if (oModSWG.GetSetcreatedby.Length > 0)
                {
                    if (oModSWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModSWG.GetSetmodifiedby;
                if (oModSWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModSWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModSWG.GetSetconfirmedby;
                if (oModSWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModSWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModSWG.GetSetcancelledby;
                if (oModSWG.GetSetcancelledby.Length > 0)
                {
                    if (oModSWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffAttendanceGroup: " + e.Message.ToString());
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

    public String deleteStaffAttendanceGroup(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_attendance_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModSWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffAttendanceGroup: " + e.Message.ToString());
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

    public ArrayList getStaffAttendanceGroupDayList(String comp, String fyr, String staffno, Int64 id, Int64 wg_id, String status, String daydate)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.wg_id, b.code, b.description, date_format(b.fromdate,'%d-%m-%Y') fromdate, date_format(b.todate,'%d-%m-%Y') todate, b.rest_day, ";
                query = query + "        c.id wd_id, c.day_name, date_format(c.day_date,'%d-%m-%Y') day_date, date_format(c.fromtime,'%H:%i:%s') fromtime, date_format(c.totime,'%H:%i:%s') totime, c.next_day, c.follow_previous, ";
                query = query + "        IFNULL(date_format(d.fromtime,'%H:%i:%s'),'00:00:00') timein, IFNULL(date_format(d.totime,'%H:%i:%s'),'00:00:00') timeout, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_attendance_group a, attendance_group b, attendance_group_day c left join staff_attendance_day d on c.comp = d.comp and c.fyr = d.fyr and c.day_date = d.day_date ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.wg_id = b.id ";
                query = query + " AND    a.comp = c.comp AND a.wg_id = c.wg_id ";
                query = query + " AND    (a.staffno = d.staffno or d.staffno is null) ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                if (wg_id > 0)
                {
                    query = query + " and  a.wg_id = " + wg_id;
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                if (daydate.Trim().Length > 0)
                {
                    query = query + " and  c.day_date = ?day_date ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, b.fromdate, c.day_date ";
                //WriteToLogFile("HRController-getStaffAttendanceGroupDayList [SQL]: " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (daydate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(daydate, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetwg_id = replaceZero(dataReader, "wg_id");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetwd_id = replaceZero(dataReader, "wd_id");
                    modStaff.GetSetday_date = replaceNull(dataReader, "day_date");
                    modStaff.GetSetday_name = replaceNull(dataReader, "day_name");
                    modStaff.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modStaff.GetSettotime = replaceNull(dataReader, "totime");
                    modStaff.GetSetnext_day = replaceNull(dataReader, "next_day");
                    modStaff.GetSetfollow_previous = replaceNull(dataReader, "follow_previous");
                    modStaff.GetSettimein = replaceNull(dataReader, "timein");
                    modStaff.GetSettimeout = replaceNull(dataReader, "timeout");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffAttendanceGroupDayList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public ArrayList getAttendanceWorkingGroupTableList(String comp, String fyr, Int64 id, String staffno, String fromdate, String todate)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, rest_day, ";
                query = query + "        b.id wg_id, b.staffno, c.name, d.emp_deptid dept_id, (select dept_name from department_comp x where d.comp = x.comp and d.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        d.emp_gredid gred_id, (select gred_name from grade_comp y where d.comp = y.comp and d.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        d.emp_posid pos_id, (select pos_name from position_comp z where d.comp = z.comp and d.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   attendance_group a, staff_attendance_group b, staff_info c, staff_employment d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.fyr = b.fyr AND a.id = b.wg_id ";
                query = query + " AND    b.comp = c.comp AND b.staffno = c.staffno ";
                query = query + " AND    c.comp = d.comp and c.staffno = d.staffno and d.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and  a.fromdate = ?fromdate ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and  a.todate = ?todate ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  b.staffno = '" + staffno + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.fromdate, c.name ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (fromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(fromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.Date).Value = datetime;
                }
                if (todate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(todate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetcode = replaceNull(dataReader, "code");
                    modStaff.GetSetdesc = replaceNull(dataReader, "description");
                    modStaff.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modStaff.GetSettodate = replaceNull(dataReader, "todate");
                    modStaff.GetSetrest_day = replaceNull(dataReader, "rest_day");
                    modStaff.GetSetwg_id = replaceZero(dataReader, "wg_id");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getAttendanceWorkingGroupTableList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public ArrayList getStaffAttendanceDayList(String comp, String fyr, String staffno, String day_date, String status)
    {
        ArrayList lsCompGred = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.deviceid, a.devicename, a.ipno, a.location, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_attendance_day a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modStaff = new HRModel();
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetday_name = replaceNull(dataReader, "day_name");
                    modStaff.GetSetday_date = replaceNull(dataReader, "day_date");
                    modStaff.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modStaff.GetSettotime = replaceNull(dataReader, "totime");
                    modStaff.GetSetdeviceid = replaceNull(dataReader, "deviceid");
                    modStaff.GetSetdevicename = replaceNull(dataReader, "devicename");
                    modStaff.GetSetipno = replaceNull(dataReader, "ipno");
                    modStaff.GetSetlocation = replaceNull(dataReader, "location");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompGred.Add(modStaff);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffAttendanceDayList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompGred;
    }

    public HRModel getStaffAttendanceDayDetails(String comp, String fyr, String staffno, String day_date, Int64 id)
    {
        HRModel modStaff = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.deviceid, a.devicename, a.ipno, a.location, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_attendance_day a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (day_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(day_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = null;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modStaff.GetSetid = replaceZero(dataReader, "id");
                    modStaff.GetSetcomp = replaceNull(dataReader, "comp");
                    modStaff.GetSetfyr = replaceNull(dataReader, "fyr");
                    modStaff.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modStaff.GetSetname = replaceNull(dataReader, "name");
                    modStaff.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modStaff.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modStaff.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modStaff.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modStaff.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modStaff.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modStaff.GetSetday_name = replaceNull(dataReader, "day_name");
                    modStaff.GetSetday_date = replaceNull(dataReader, "day_date");
                    modStaff.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modStaff.GetSettotime = replaceNull(dataReader, "totime");
                    modStaff.GetSetdeviceid = replaceNull(dataReader, "deviceid");
                    modStaff.GetSetdevicename = replaceNull(dataReader, "devicename");
                    modStaff.GetSetipno = replaceNull(dataReader, "ipno");
                    modStaff.GetSetlocation = replaceNull(dataReader, "location");
                    modStaff.GetSetstatus = replaceNull(dataReader, "status");
                    modStaff.GetSetremarks = replaceNull(dataReader, "remarks");
                    modStaff.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modStaff.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modStaff.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modStaff.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modStaff.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modStaff.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modStaff.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modStaff.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffAttendanceDayDetails: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return modStaff;
    }

    public String insertStaffAttendanceDay(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_attendance_day (comp, fyr, staffno, day_name, day_date, fromtime, totime, deviceid, devicename, ipno, location, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?day_name, ?day_date, ?fromtime, ?totime, ?deviceid, ?devicename, ?ipno, ?location, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModSWG.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModSWG.GetSetstaffno;
                cmd.Parameters.Add("?day_name", MySqlDbType.VarChar).Value = oModSWG.GetSetday_name;
                if (oModSWG.GetSetday_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSetday_date,ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModSWG.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModSWG.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?deviceid", MySqlDbType.VarChar).Value = oModSWG.GetSetdeviceid;
                cmd.Parameters.Add("?devicename", MySqlDbType.VarChar).Value = oModSWG.GetSetdevicename;
                cmd.Parameters.Add("?ipno", MySqlDbType.VarChar).Value = oModSWG.GetSetipno;
                cmd.Parameters.Add("?location", MySqlDbType.VarChar).Value = oModSWG.GetSetlocation;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModSWG.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModSWG.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModSWG.GetSetcreatedby;
                if (oModSWG.GetSetcreatedby.Length > 0)
                {
                    if (oModSWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModSWG.GetSetmodifiedby;
                if (oModSWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModSWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModSWG.GetSetconfirmedby;
                if (oModSWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModSWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModSWG.GetSetcancelledby;
                if (oModSWG.GetSetcancelledby.Length > 0)
                {
                    if (oModSWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffAttendanceDay: " + e.Message.ToString());
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

    public String updateStaffAttendanceDay(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_attendance_day ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, day_name = ?day_name, day_date = ?day_date, fromtime = ?fromtime, totime = ?totime, deviceid = ?deviceid, devicename = ?devicename, ipno = ?ipno, location = ?location, ";
                query = query + "        status = ?status, remarks = ?remarks, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModSWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModSWG.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModSWG.GetSetstaffno;
                cmd.Parameters.Add("?day_name", MySqlDbType.VarChar).Value = oModSWG.GetSetday_name;
                if (oModSWG.GetSetday_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSetday_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?day_date", MySqlDbType.DateTime).Value = null;
                }
                if (oModSWG.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModSWG.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModSWG.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?deviceid", MySqlDbType.VarChar).Value = oModSWG.GetSetdeviceid;
                cmd.Parameters.Add("?devicename", MySqlDbType.VarChar).Value = oModSWG.GetSetdevicename;
                cmd.Parameters.Add("?ipno", MySqlDbType.VarChar).Value = oModSWG.GetSetipno;
                cmd.Parameters.Add("?location", MySqlDbType.VarChar).Value = oModSWG.GetSetlocation;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModSWG.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModSWG.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModSWG.GetSetcreatedby;
                if (oModSWG.GetSetcreatedby.Length > 0)
                {
                    if (oModSWG.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModSWG.GetSetmodifiedby;
                if (oModSWG.GetSetmodifiedby.Length > 0)
                {
                    if (oModSWG.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModSWG.GetSetconfirmedby;
                if (oModSWG.GetSetconfirmedby.Length > 0)
                {
                    if (oModSWG.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModSWG.GetSetcancelledby;
                if (oModSWG.GetSetcancelledby.Length > 0)
                {
                    if (oModSWG.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModSWG.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffAttendanceDay: " + e.Message.ToString());
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

    public String deleteStaffAttendanceDay(HRModel oModSWG)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_attendance_day ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModSWG.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSWG.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffAttendanceDay: " + e.Message.ToString());
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

    public ArrayList getExcuseTypeList(String comp, String fyr, String exc_cat, String exc_typ, String status)
    {
        ArrayList lsExcType = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT distinct a.comp, a.fyr, a.exc_cat, a.exc_type ";
                query = query + " from   staff_excuse a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "' ";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "'";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.exc_cat, a.exc_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modExc = new HRModel();
                    modExc.GetSetcomp = replaceNull(dataReader, "comp");
                    modExc.GetSetfyr = replaceNull(dataReader, "fyr");
                    modExc.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modExc.GetSettype = replaceNull(dataReader, "exc_type");
                    lsExcType.Add(modExc);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getExcuseTypeList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsExcType;
    }

    public ArrayList getStaffExcuseList(String comp, String fyr, String staffno, String exc_cat, String exc_typ, String fromdate, String todate, String status, String filefolder)
    {
        ArrayList lsStaffExcuse = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.exc_cat, a.exc_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.exc_reason, ";
                query = query + "        date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.reliefedby, a.reliefeddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_excuse a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "' ";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and fromdate >= STR_TO_DATE('" + fromdate + "','%d-%m-%Y') ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and todate <= STR_TO_DATE('" + todate + "','%d-%m-%Y') ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.fromdate, a.exc_cat, a.exc_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modItem.GetSettype = replaceNull(dataReader, "exc_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetreason = replaceNull(dataReader, "exc_reason");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");

                    modItem.GetSetreliefedby = replaceNull(dataReader, "reliefedby");
                    modItem.GetSetreliefeddate = replaceNull(dataReader, "reliefeddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsStaffExcuse.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffExcuseList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffExcuse;
    }
    
    public HRModel getStaffExcuseDetails(String comp, String fyr, String staffno, String exc_cat, String exc_typ, String fromdate, String todate, Int64 id, String filefolder)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.exc_cat, a.exc_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.exc_reason, ";
                query = query + "        date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.filename1, a.fileblob1, a.filename2, a.fileblob2, a.filename3, a.fileblob3, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.reliefedby, a.reliefeddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_excuse a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "' ";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and fromdate >= STR_TO_DATE('" + fromdate + "','%d-%m-%Y') ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and todate <= STR_TO_DATE('" + todate + "','%d-%m-%Y') ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.fromdate, a.exc_cat, a.exc_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modItem.GetSettype = replaceNull(dataReader, "exc_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetreason = replaceNull(dataReader, "exc_reason");
                    modItem.GetSetfilename1 = replaceNull(dataReader, "filename1");
                    String sUrl = filefolder + modItem.GetSetfilename1;
                    if (dataReader["fileblob1"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob1"]);
                    }
                    modItem.GetSetfilename2 = replaceNull(dataReader, "filename2");
                    sUrl = filefolder + modItem.GetSetfilename2;
                    if (dataReader["fileblob2"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob2"]);
                    }
                    modItem.GetSetfilename3 = replaceNull(dataReader, "filename3");
                    sUrl = filefolder + modItem.GetSetfilename3;
                    if (dataReader["fileblob3"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob3"]);
                    }
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");

                    modItem.GetSetreliefedby = replaceNull(dataReader, "reliefedby");
                    modItem.GetSetreliefeddate = replaceNull(dataReader, "reliefeddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffExcuseDetails: " + e.Message.ToString());
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
    
    public String insertStaffExcuseDetails(HRModel oModExcDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_excuse (comp, fyr, staffno, exc_cat, exc_type, fromdate, todate, fromtime, totime, exc_reason, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?exc_cat, ?exc_type, ?fromdate, ?todate, ?fromtime, ?totime, ?exc_reason, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                cmd.Parameters.Add("?exc_cat", MySqlDbType.VarChar).Value = oModExcDet.GetSetcat;
                cmd.Parameters.Add("?exc_type", MySqlDbType.VarChar).Value = oModExcDet.GetSettype;
                if (oModExcDet.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?exc_reason", MySqlDbType.VarChar).Value = oModExcDet.GetSetreason;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                if (oModExcDet.GetSetcreatedby.Length > 0)
                {
                    if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                if (oModExcDet.GetSetmodifiedby.Length > 0)
                {
                    if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                if (oModExcDet.GetSetconfirmedby.Length > 0)
                {
                    if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                if (oModExcDet.GetSetcancelledby.Length > 0)
                {
                    if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffExcuseDetails: " + e.Message.ToString());
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
    
    public String updateStaffExcuseDetails(HRModel oModExcDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_excuse ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, exc_cat = ?exc_cat, exc_type = ?exc_type, fromdate = ?fromdate, todate = ?todate, fromtime = ?fromtime, totime = ?totime, exc_reason = ?exc_reason, ";
                query = query + "        status = ?status, remarks = ?remarks, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate, ";
                query = query + "        reliefedby = ?reliefedby, reliefeddate = ?reliefeddate, approvedby = ?approvedby, approveddate = ?approveddate, rejectedby = ?rejectedby, rejecteddate= ?rejecteddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                cmd.Parameters.Add("?exc_cat", MySqlDbType.VarChar).Value = oModExcDet.GetSetcat;
                cmd.Parameters.Add("?exc_type", MySqlDbType.VarChar).Value = oModExcDet.GetSettype;
                if (oModExcDet.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModExcDet.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?exc_reason", MySqlDbType.VarChar).Value = oModExcDet.GetSetreason;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                if (oModExcDet.GetSetcreatedby.Length > 0)
                {
                    if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                if (oModExcDet.GetSetmodifiedby.Length > 0)
                {
                    if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                if (oModExcDet.GetSetconfirmedby.Length > 0)
                {
                    if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                if (oModExcDet.GetSetcancelledby.Length > 0)
                {
                    if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                cmd.Parameters.Add("?reliefedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetreliefedby;
                if (oModExcDet.GetSetreliefedby.Length > 0)
                {
                    if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetreliefeddate);
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetapprovedby;
                if (oModExcDet.GetSetapprovedby.Length > 0)
                {
                    if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetapproveddate);
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetrejectedby;
                if (oModExcDet.GetSetrejectedby.Length > 0)
                {
                    if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetrejecteddate);
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffExcuseDetails: " + e.Message.ToString());
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

    public String updateStaffExcuseAttachment(HRModel oModExcDet, String filefolder, String filename1, String filename2, String filename3)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";
        //String filefolder = Server.MapPath("./Attachment/HumanResource/");

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_excuse ";
                query = query + " SET    fyr = ?fyr ";
                if (filename1.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename1.Equals(filename1)) 
                    {
                        query = query + " ,filename1 = ?filename1, fileblob1 = ?fileblob1 ";
                    }
                }
                else
                {
                    query = query + " ,filename1 = null, fileblob1 = null ";
                }
                if (filename2.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename2.Equals(filename2))
                    {
                        query = query + " ,filename2 = ?filename2, fileblob2 = ?fileblob2 ";
                    }
                }
                else
                {
                    query = query + " ,filename2 = null, fileblob2 = null ";
                }
                if (filename3.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename3.Equals(filename3))
                    {
                        query = query + " ,filename3 = ?filename3, fileblob1 = ?fileblob3 ";
                    }
                }
                else
                {
                    query = query + " ,filename3 = null, fileblob3 = null ";
                }
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                if (filename1.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename1.Equals(filename1))
                    {
                        byte[] blobData1;
                        FileStream fs1 = new FileStream(filefolder + filename1, FileMode.Open, FileAccess.Read);
                        BinaryReader br1 = new BinaryReader(fs1);
                        blobData1 = br1.ReadBytes((int)fs1.Length);
                        br1.Close();
                        fs1.Close();

                        cmd.Parameters.Add("?filename1", MySqlDbType.VarChar).Value = filename1;
                        cmd.Parameters.Add("?fileblob1", MySqlDbType.Blob).Value = blobData1;
                    }
                }
                if (filename2.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename2.Equals(filename2))
                    {
                        byte[] blobData2;
                        FileStream fs2 = new FileStream(filefolder + filename2, FileMode.Open, FileAccess.Read);
                        BinaryReader br2 = new BinaryReader(fs2);
                        blobData2 = br2.ReadBytes((int)fs2.Length);
                        br2.Close();
                        fs2.Close();

                        cmd.Parameters.Add("?filename2", MySqlDbType.VarChar).Value = filename2;
                        cmd.Parameters.Add("?fileblob2", MySqlDbType.Blob).Value = blobData2;
                    }
                }
                if (filename3.Length > 0)
                {
                    if (!oModExcDet.GetSetfilename3.Equals(filename3))
                    {
                        byte[] blobData3;
                        FileStream fs3 = new FileStream(filefolder + filename3, FileMode.Open, FileAccess.Read);
                        BinaryReader br3 = new BinaryReader(fs3);
                        blobData3 = br3.ReadBytes((int)fs3.Length);
                        br3.Close();
                        fs3.Close();

                        cmd.Parameters.Add("?filename3", MySqlDbType.VarChar).Value = filename3;
                        cmd.Parameters.Add("?fileblob3", MySqlDbType.Blob).Value = blobData3;
                    }
                }

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffExcuseAttachment: " + e.Message.ToString());
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
    
    public String deleteStaffExcuseDetails(HRModel oModExcDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_excuse ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffExcuseDetails: " + e.Message.ToString());
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

    public ArrayList getStaffExceptionList(String comp, String fyr, String staffno, String day_date, String exc_cat, String exc_typ, Int64 exc_id)
    {
        ArrayList lsItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.exc_type, a.exc_cat, a.exc_id, a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime,";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_exception_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "'";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "'";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "'";
                }
                if (exc_id > 0)
                {
                    query = query + " and  a.exc_id = " + exc_id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (day_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(day_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetday_name = replaceNull(dataReader, "day_name");
                    modItem.GetSetday_date = replaceNull(dataReader, "day_date");
                    modItem.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modItem.GetSettype = replaceNull(dataReader, "exc_type");
                    modItem.GetSetexc_id = replaceZero(dataReader, "exc_id");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffExceptionList: " + e.Message.ToString());
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

    public List<HRModel> getStaffExceptionStringList(String comp, String fyr, String staffno, String day_date, String exc_cat, String exc_typ, Int64 exc_id)
    {
        List<HRModel> lsItem = new List<HRModel>();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.exc_type, a.exc_cat, a.exc_id, a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime,";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_exception_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "'";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "'";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "'";
                }
                if (exc_id > 0)
                {
                    query = query + " and  a.exc_id = " + exc_id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (day_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(day_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    //String strItem = replaceNull(dataReader, "day_date") + "|" + replaceNull(dataReader, "exc_cat") + "|" + replaceNull(dataReader, "exc_type") + "|" + replaceZero(dataReader, "id");
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetday_name = replaceNull(dataReader, "day_name");
                    modItem.GetSetday_date = replaceNull(dataReader, "day_date");
                    modItem.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modItem.GetSettype = replaceNull(dataReader, "exc_type");
                    modItem.GetSetexc_id = replaceZero(dataReader, "exc_id");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItem.Add(modItem);
                }

                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffExceptionStringList: " + e.Message.ToString());
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

    public HRModel getStaffExceptionDetails(String comp, String fyr, String staffno, String day_date, String exc_cat, String exc_typ, Int64 exc_id, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.exc_type, a.exc_cat, a.exc_id, a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime,";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_exception_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno +"'";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (exc_cat.Trim().Length > 0)
                {
                    query = query + " and  a.exc_cat = '" + exc_cat + "'";
                }
                if (exc_typ.Trim().Length > 0)
                {
                    query = query + " and  a.exc_type = '" + exc_typ + "'";
                }
                if (exc_id > 0)
                {
                    query = query + " and  a.exc_id = " + exc_id;
                }
                if (id > 0)
                {
                    query = query + " and  a.exc_id = " + id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (day_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(day_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetday_name = replaceNull(dataReader, "day_name");
                    modItem.GetSetday_date = replaceNull(dataReader, "day_date");
                    modItem.GetSetcat = replaceNull(dataReader, "exc_cat");
                    modItem.GetSettype = replaceNull(dataReader, "exc_type");
                    modItem.GetSetexc_id = replaceZero(dataReader, "exc_id");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getStaffExceptionDetails: " + e.Message.ToString());
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

    public String insertStaffExceptionDetails(HRModel oModExcDet, String sType)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        String restday = "";

        try
        {
            ArrayList lsAttGrp = getStaffAttendanceGroupList(oModExcDet.GetSetcomp, oModExcDet.GetSetfyr, oModExcDet.GetSetstaffno, 0, "", "ACTIVE");
            for(int i=0; i< lsAttGrp.Count; i++)
            {
                HRModel oAttGrp = (HRModel)lsAttGrp[i];
                restday = restday + oAttGrp.GetSetrest_day + ",";
            }
            if (dbConnect.OpenConnection() == true)
            {
                if (sType.Equals("EXCUSE"))
                {
                    query = "";
                    query = query + " INSERT INTO staff_exception_day (comp, fyr, staffno, exc_id, exc_cat, exc_type, fromtime, totime, day_name, day_date, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate, reliefedby, reliefeddate, approvedby, approveddate, rejectedby, rejecteddate) ";
                    query = query + " SELECT ?comp, ?fyr, ?staffno, ?exc_id, ?exc_cat, ?exc_type, ?fromtime, ?totime, ";
                    query = query + "        (case dayname(selected_date) when 'Sunday' then 'AHAD' when 'Monday' then 'ISNIN' when 'Tuesday' then 'SELASA' when 'Wednesday' then 'RABU' when 'Thursday' then 'KHAMIS' when 'Friday' then 'JUMAAT' when 'Saturday' then 'SABTU' else null end) as day_name, ";
                    query = query + "        selected_date as day_date, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate, ?reliefedby, ?reliefeddate, ?approvedby, ?approveddate, ?rejectedby, ?rejecteddate ";
                    query = query + "        FROM ";
                    query = query + @"       (select adddate('1970-01-01',t4*10000 + t3*1000 + t2*100 + t1*10 + t0) selected_date from
                                             (select 0 t0 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t0,
                                             (select 0 t1 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t1,
                                             (select 0 t2 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t2,
                                             (select 0 t3 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t3,
                                             (select 0 t4 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t4) v  ";
                    query = query + " WHERE selected_date between STR_TO_DATE('" + oModExcDet.GetSetfromdate + "','%d-%m-%Y') and STR_TO_DATE('" + oModExcDet.GetSettodate + "','%d-%m-%Y') ";
                    query = query + " AND selected_date not in (select ph_date from publicholiday_comp where comp = ?comp and fyr = ?fyr) ";
                    //query = query + " AND LOCATE((case dayname(selected_date) when 'Sunday' then 'AHAD' when 'Monday' then 'ISNIN' when 'Tuesday' then 'SELASA' when 'Wednesday' then 'RABU' when 'Thursday' then 'KHAMIS' when 'Friday' then 'JUMAAT' when 'Saturday' then 'SABTU' else null end), '"+ restday + "') = 0 ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                    cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                    cmd.Parameters.Add("?exc_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?exc_cat", MySqlDbType.VarChar).Value = oModExcDet.GetSetcat;
                    cmd.Parameters.Add("?exc_type", MySqlDbType.VarChar).Value = oModExcDet.GetSettype;
                    if (oModExcDet.GetSetfromtime.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromtime, ukDtfi);
                        cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                    }
                    if (oModExcDet.GetSettotime.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettotime, ukDtfi);
                        cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                    cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                    cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                    if (oModExcDet.GetSetcreatedby.Length > 0)
                    {
                        if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                    cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                    if (oModExcDet.GetSetmodifiedby.Length > 0)
                    {
                        if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                    if (oModExcDet.GetSetconfirmedby.Length > 0)
                    {
                        if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                    cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                    if (oModExcDet.GetSetcancelledby.Length > 0)
                    {
                        if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                    cmd.Parameters.Add("?reliefedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetreliefedby;
                    if (oModExcDet.GetSetreliefedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetreliefeddate);
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetapprovedby;
                    if (oModExcDet.GetSetapprovedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetapproveddate);
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetrejectedby;
                    if (oModExcDet.GetSetrejectedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetrejecteddate);
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
                else if (sType.Equals("LEAVE"))
                {
                    query = "";
                    query = query + " INSERT INTO staff_leave_day (comp, fyr, staffno, leave_id, leave_cat, leave_type, fromtime, totime, day_name, day_date, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate, reliefedby, reliefeddate, approvedby, approveddate, rejectedby, rejecteddate) ";
                    query = query + " SELECT ?comp, ?fyr, ?staffno, ?leave_id, ?leave_cat, ?leave_type, ?fromtime, ?totime, ";
                    query = query + "        (case dayname(selected_date) when 'Sunday' then 'AHAD' when 'Monday' then 'ISNIN' when 'Tuesday' then 'SELASA' when 'Wednesday' then 'RABU' when 'Thursday' then 'KHAMIS' when 'Friday' then 'JUMAAT' when 'Saturday' then 'SABTU' else null end) as day_name, ";
                    query = query + "        selected_date as day_date, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate, ?reliefedby, ?reliefeddate, ?approvedby, ?approveddate, ?rejectedby, ?rejecteddate ";
                    query = query + "        FROM ";
                    query = query + @"       (select adddate('1970-01-01',t4*10000 + t3*1000 + t2*100 + t1*10 + t0) selected_date from
                                             (select 0 t0 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t0,
                                             (select 0 t1 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t1,
                                             (select 0 t2 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t2,
                                             (select 0 t3 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t3,
                                             (select 0 t4 union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) t4) v  ";
                    query = query + " WHERE selected_date between STR_TO_DATE('" + oModExcDet.GetSetfromdate + "','%d-%m-%Y') and STR_TO_DATE('" + oModExcDet.GetSettodate + "','%d-%m-%Y') ";
                    query = query + " AND selected_date not in (select ph_date from publicholiday_comp where comp = ?comp and fyr = ?fyr) ";
                    query = query + " AND LOCATE((case dayname(selected_date) when 'Sunday' then 'AHAD' when 'Monday' then 'ISNIN' when 'Tuesday' then 'SELASA' when 'Wednesday' then 'RABU' when 'Thursday' then 'KHAMIS' when 'Friday' then 'JUMAAT' when 'Saturday' then 'SABTU' else null end), '"+ restday + "') = 0 ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                    cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                    cmd.Parameters.Add("?leave_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?leave_cat", MySqlDbType.VarChar).Value = oModExcDet.GetSetcat;
                    cmd.Parameters.Add("?leave_type", MySqlDbType.VarChar).Value = oModExcDet.GetSettype;
                    if (oModExcDet.GetSetfromtime.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetfromtime, ukDtfi);
                        cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                    }
                    if (oModExcDet.GetSettotime.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModExcDet.GetSettotime, ukDtfi);
                        cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                    cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                    cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                    if (oModExcDet.GetSetcreatedby.Length > 0)
                    {
                        if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                    cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                    if (oModExcDet.GetSetmodifiedby.Length > 0)
                    {
                        if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                    if (oModExcDet.GetSetconfirmedby.Length > 0)
                    {
                        if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                    cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                    if (oModExcDet.GetSetcancelledby.Length > 0)
                    {
                        if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                    cmd.Parameters.Add("?reliefedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetreliefedby;
                    if (oModExcDet.GetSetreliefedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetreliefeddate);
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetapprovedby;
                    if (oModExcDet.GetSetapprovedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetapproveddate);
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetrejectedby;
                    if (oModExcDet.GetSetrejectedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetrejecteddate);
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffExceptionDetails: " + e.Message.ToString());
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

    public String updateStaffExceptionDetails(HRModel oModExcDet, String sType)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                if (sType.Equals("EXCUSE"))
                {
                    query = "";
                    query = query + " UPDATE staff_exception_day ";
                    query = query + " SET    status = ?status, remarks = ?remarks, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate, ";
                    query = query + "        reliefedby = ?reliefedby, reliefeddate = ?reliefeddate, approvedby = ?approvedby, approveddate = ?approveddate, rejectedby = ?rejectedby, rejecteddate= ?rejecteddate ";
                    query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND exc_id = ?exc_id AND staffno = ?staffno ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?exc_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                    cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                    cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                    cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                    cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                    if (oModExcDet.GetSetcreatedby.Length > 0)
                    {
                        if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                    cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                    if (oModExcDet.GetSetmodifiedby.Length > 0)
                    {
                        if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                    if (oModExcDet.GetSetconfirmedby.Length > 0)
                    {
                        if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                    cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                    if (oModExcDet.GetSetcancelledby.Length > 0)
                    {
                        if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                    cmd.Parameters.Add("?reliefedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetreliefedby;
                    if (oModExcDet.GetSetreliefedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetreliefeddate);
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetapprovedby;
                    if (oModExcDet.GetSetapprovedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetapproveddate);
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetrejectedby;
                    if (oModExcDet.GetSetrejectedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetrejecteddate);
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
                else if (sType.Equals("LEAVE"))
                {
                    query = "";
                    query = query + " UPDATE staff_leave_day ";
                    query = query + " SET    status = ?status, remarks = ?remarks, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate, ";
                    query = query + "        reliefedby = ?reliefedby, reliefeddate = ?reliefeddate, approvedby = ?approvedby, approveddate = ?approveddate, rejectedby = ?rejectedby, rejecteddate= ?rejecteddate ";
                    query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND leave_id = ?leave_id AND staffno = ?staffno ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?leave_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModExcDet.GetSetfyr;
                    cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModExcDet.GetSetstaffno;
                    cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModExcDet.GetSetstatus;
                    cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModExcDet.GetSetremarks;
                    cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcreatedby;
                    if (oModExcDet.GetSetcreatedby.Length > 0)
                    {
                        if (oModExcDet.GetSetcreateddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcreateddate);
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
                    cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetmodifiedby;
                    if (oModExcDet.GetSetmodifiedby.Length > 0)
                    {
                        if (oModExcDet.GetSetmodifieddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetmodifieddate);
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetconfirmedby;
                    if (oModExcDet.GetSetconfirmedby.Length > 0)
                    {
                        if (oModExcDet.GetSetconfirmeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetconfirmeddate);
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
                    cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModExcDet.GetSetcancelledby;
                    if (oModExcDet.GetSetcancelledby.Length > 0)
                    {
                        if (oModExcDet.GetSetcancelleddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetcancelleddate);
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
                    cmd.Parameters.Add("?reliefedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetreliefedby;
                    if (oModExcDet.GetSetreliefedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetreliefeddate);
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?reliefeddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetapprovedby;
                    if (oModExcDet.GetSetapprovedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetapproveddate);
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModExcDet.GetSetrejectedby;
                    if (oModExcDet.GetSetrejectedby.Length > 0)
                    {
                        if (oModExcDet.GetSetreliefeddate.Trim().Length > 0)
                        {
                            DateTime datetime = Convert.ToDateTime(oModExcDet.GetSetrejecteddate);
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                        }
                        else
                        {
                            cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                    }
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffExcuseDetails: " + e.Message.ToString());
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

    public String deleteStaffExceptionDetails(HRModel oModExcDet, String sType)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                if (sType.Equals("EXCUSE"))
                {
                    query = "";
                    query = query + " DELETE FROM staff_exception_day ";
                    query = query + " WHERE  comp = ?comp AND exc_id = ?exc_id ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?exc_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
                else if (sType.Equals("LEAVE"))
                {
                    query = "";
                    query = query + " DELETE FROM staff_leave_day ";
                    query = query + " WHERE  comp = ?comp AND leave_id = ?leave_id ";
                    MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                    cmd.Parameters.Add("?leave_id", MySqlDbType.Int64).Value = oModExcDet.GetSetid;
                    cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModExcDet.GetSetcomp;
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = "Y";
                    }
                    else
                    {
                        result = "N";
                    }
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffExceptionDetails: " + e.Message.ToString());
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

    public ArrayList getCompLeaveGroupList(String comp, String fyr, String code, String desc, String leave_cat, String leave_type)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.leave_cat, a.leave_type, a.leave_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   leave_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "' ";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.fyr, a.leave_cat, a.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetcount = replaceZero(dataReader, "leave_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompLeaveGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public ArrayList getCompLeaveGroupList(String comp, String fyr, String code, String desc, String leave_cat, String leave_type, String staffno)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.leave_cat, a.leave_type, a.leave_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   leave_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "' ";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.id NOT IN (select b.lg_id from staff_leave_group b where a.comp = b.comp and a.fyr = b.fyr and b.staffno = '"+staffno+"') ";
                }
                query = query + " order by a.comp, a.fyr, a.leave_cat, a.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetcount = replaceZero(dataReader, "leave_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompLeaveGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public HRModel getCompLeaveGroupDetails(String comp, String fyr, String code, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.leave_cat, a.leave_type, a.leave_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   leave_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.leave_type, a.leave_count ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetcount = replaceZero(dataReader, "leave_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getCompLeaveGroupDetails: " + e.Message.ToString());
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

    public String insertCompLeaveGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO leave_group (comp, fyr, code, description, leave_cat, leave_type, leave_count, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?code, ?description, ?leave_cat, ?leave_type, ?leave_count, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?leave_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?leave_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?leave_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompLeaveGroup: " + e.Message.ToString());
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

    public String updateCompLeaveGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE leave_group ";
                query = query + " SET    fyr = ?fyr, code = ?code, description = ?description, leave_cat = ?leave_cat, leave_type = ?leave_type, leave_count = ?leave_count, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?leave_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?leave_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?leave_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompLeaveGroup: " + e.Message.ToString());
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

    public String deleteCompLeaveGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM leave_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompLeaveGroup: " + e.Message.ToString());
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

    public ArrayList getStaffLeaveGroupList(String comp, String fyr, String staffno, Int64 grpid, String grpcode, String grpcat, String grptype, String status)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.lg_id, d.code, d.description, d.leave_cat, d.leave_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.leave_count, a.leave_brought, a.leave_taken, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_leave_group a, staff_info b, staff_employment c, leave_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.lg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.lg_id = " + grpid;
                }
                if (grpcode.Trim().Length > 0)
                {
                    query = query + " and  d.code = '" + grpcode + "' ";
                }
                if (grpcat.Trim().Length > 0)
                {
                    query = query + " and  d.leave_cat = '" + grpcat + "' ";
                }
                if (grptype.Trim().Length > 0)
                {
                    query = query + " and  d.leave_type = '" + grptype + "' ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, d.leave_cat, d.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetlg_id = replaceZero(dataReader, "lg_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetcount = replaceZero(dataReader, "leave_count");
                    modItem.GetSetbrought = replaceZero(dataReader, "leave_brought");
                    modItem.GetSettaken = replaceZero(dataReader, "leave_taken");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffLeaveGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public HRModel getStaffLeaveGroupDetails(String comp, String fyr, String staffno, Int64 grpid, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.lg_id, d.code, d.description, d.leave_cat, d.leave_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.leave_count, a.leave_brought, a.leave_taken, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_leave_group a, staff_info b, staff_employment c, leave_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.lg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.lg_id = " + grpid;
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, d.leave_cat, d.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetlg_id = replaceZero(dataReader, "lg_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetcount = replaceZero(dataReader, "leave_count");
                    modItem.GetSetbrought = replaceZero(dataReader, "leave_brought");
                    modItem.GetSettaken = replaceZero(dataReader, "leave_taken");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getStaffLeaveGroupDetails: " + e.Message.ToString());
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

    public String insertStaffLeaveGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_leave_group (comp, fyr, staffno, lg_id, code, fromdate, todate, leave_count, leave_brought, leave_taken, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?lg_id, ?code, ?fromdate, ?todate, ?leave_count, ?leave_brought, ?leave_taken, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = modItem.GetSetstaffno;
                cmd.Parameters.Add("?lg_id", MySqlDbType.Int64).Value = modItem.GetSetlg_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = modItem.GetSetcode;
                if (modItem.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSetfromdate,ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (modItem.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?leave_count", MySqlDbType.Int16).Value = modItem.GetSetcount;
                cmd.Parameters.Add("?leave_brought", MySqlDbType.Int16).Value = modItem.GetSetbrought;
                cmd.Parameters.Add("?leave_taken", MySqlDbType.Int16).Value = modItem.GetSettaken;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = modItem.GetSetmodifiedby;
                if (modItem.GetSetmodifiedby.Length > 0)
                {
                    if (modItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffLeaveGroup: " + e.Message.ToString());
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

    public String updateStaffLeaveGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_leave_group ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, lg_id = ?lg_id, code = ?code, fromdate = ?fromdate, todate = ?todate, leave_count = ?leave_count, leave_brought = ?leave_brought, leave_taken = ?leave_taken, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = modItem.GetSetstaffno;
                cmd.Parameters.Add("?lg_id", MySqlDbType.Int64).Value = modItem.GetSetlg_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = modItem.GetSetcode;
                if (modItem.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (modItem.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?leave_count", MySqlDbType.Int16).Value = modItem.GetSetcount;
                cmd.Parameters.Add("?leave_brought", MySqlDbType.Int16).Value = modItem.GetSetbrought;
                cmd.Parameters.Add("?leave_taken", MySqlDbType.Int16).Value = modItem.GetSettaken;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = modItem.GetSetmodifiedby;
                if (modItem.GetSetmodifiedby.Length > 0)
                {
                    if (modItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffLeaveGroup: " + e.Message.ToString());
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

    public String deleteStaffLeaveGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_leave_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffLeaveGroup: " + e.Message.ToString());
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

    public ArrayList getLeaveTypeList(String comp, String fyr, String leave_cat, String leave_type, String status)
    {
        ArrayList lsItemType = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT distinct a.comp, a.fyr, a.leave_cat, a.leave_type ";
                query = query + " from   staff_leave a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "' ";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "'";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.leave_cat, a.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    lsItemType.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getLeaveTypeList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsItemType;
    }

    public ArrayList getStaffLeaveList(String comp, String fyr, String staffno, String leave_cat, String leave_type, String fromdate, String todate, String status, String filefolder)
    {
        ArrayList lsStaffItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.leave_cat, a.leave_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.leave_variety, a.leave_reason, ";
                query = query + "        date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.reliefedby, a.reliefeddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_leave a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "' ";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and fromdate >= STR_TO_DATE('" + fromdate + "','%d-%m-%Y') ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and todate <= STR_TO_DATE('" + todate + "','%d-%m-%Y') ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.fromdate, a.leave_cat, a.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetvariety = replaceZero(dataReader, "leave_variety");
                    modItem.GetSetreason = replaceNull(dataReader, "leave_reason");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");

                    modItem.GetSetreliefedby = replaceNull(dataReader, "reliefedby");
                    modItem.GetSetreliefeddate = replaceNull(dataReader, "reliefeddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsStaffItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffLeaveList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsStaffItem;
    }

    public List<HRModel> getStaffLeaveStringList(String comp, String fyr, String staffno, String day_date, String leave_cat, String leave_type, Int64 exc_id)
    {
        List<HRModel> lsItem = new List<HRModel>();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.leave_type, a.leave_cat, a.leave_id, a.day_name, date_format(a.day_date,'%d-%m-%Y') day_date, date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime,";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_leave_day a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "'";
                }
                if (day_date.Trim().Length > 0)
                {
                    query = query + " and  a.day_date = ?day_date ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "'";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "'";
                }
                if (exc_id > 0)
                {
                    query = query + " and  a.exc_id = " + exc_id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.day_date ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                if (day_date.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(day_date, ukDtfi);
                    cmd.Parameters.Add("?day_date", MySqlDbType.Date).Value = datetime;
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    //String strItem = replaceNull(dataReader, "day_date") + "|" + replaceNull(dataReader, "exc_cat") + "|" + replaceNull(dataReader, "exc_type") + "|" + replaceZero(dataReader, "id");
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetday_name = replaceNull(dataReader, "day_name");
                    modItem.GetSetday_date = replaceNull(dataReader, "day_date");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetlg_id = replaceZero(dataReader, "leave_id");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItem.Add(modItem);
                }

                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffLeaveStringList: " + e.Message.ToString());
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

    public HRModel getStaffLeaveDetails(String comp, String fyr, String staffno, String leave_cat, String leave_type, String fromdate, String todate, Int64 id, String filefolder)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.leave_cat, a.leave_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, a.leave_variety, a.leave_reason, ";
                query = query + "        date_format(a.fromtime,'%H:%i:%s') fromtime, date_format(a.totime,'%H:%i:%s') totime, ";
                query = query + "        a.filename1, a.fileblob1, a.filename2, a.fileblob2, a.filename3, a.fileblob3, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.reliefedby, a.reliefeddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_leave a, staff_info b, staff_employment c ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (leave_cat.Trim().Length > 0)
                {
                    query = query + " and  a.leave_cat = '" + leave_cat + "' ";
                }
                if (leave_type.Trim().Length > 0)
                {
                    query = query + " and  a.leave_type = '" + leave_type + "' ";
                }
                if (fromdate.Trim().Length > 0)
                {
                    query = query + " and fromdate >= STR_TO_DATE('" + fromdate + "','%d-%m-%Y') ";
                }
                if (todate.Trim().Length > 0)
                {
                    query = query + " and todate <= STR_TO_DATE('" + todate + "','%d-%m-%Y') ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.fromdate, a.leave_cat, a.leave_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetcat = replaceNull(dataReader, "leave_cat");
                    modItem.GetSettype = replaceNull(dataReader, "leave_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetfromtime = replaceNull(dataReader, "fromtime");
                    modItem.GetSettotime = replaceNull(dataReader, "totime");
                    modItem.GetSetvariety = replaceZero(dataReader, "leave_variety");
                    modItem.GetSetreason = replaceNull(dataReader, "leave_reason");
                    modItem.GetSetfilename1 = replaceNull(dataReader, "filename1");
                    String sUrl = filefolder + modItem.GetSetfilename1;
                    if (dataReader["fileblob1"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob1"]);
                    }
                    modItem.GetSetfilename2 = replaceNull(dataReader, "filename2");
                    sUrl = filefolder + modItem.GetSetfilename2;
                    if (dataReader["fileblob2"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob2"]);
                    }
                    modItem.GetSetfilename3 = replaceNull(dataReader, "filename3");
                    sUrl = filefolder + modItem.GetSetfilename3;
                    if (dataReader["fileblob3"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob3"]);
                    }
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");

                    modItem.GetSetreliefedby = replaceNull(dataReader, "reliefedby");
                    modItem.GetSetreliefeddate = replaceNull(dataReader, "reliefeddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffLeaveDetails: " + e.Message.ToString());
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

    public String insertStaffLeaveDetails(HRModel oModItemDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_leave (comp, fyr, staffno, leave_cat, leave_type, fromdate, todate, fromtime, totime, leave_variety, leave_reason, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?leave_cat, ?leave_type, ?fromdate, ?todate, ?fromtime, ?totime, ?leave_variety, ?leave_reason, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItemDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItemDet.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModItemDet.GetSetstaffno;
                cmd.Parameters.Add("?leave_cat", MySqlDbType.VarChar).Value = oModItemDet.GetSetcat;
                cmd.Parameters.Add("?leave_type", MySqlDbType.VarChar).Value = oModItemDet.GetSettype;
                if (oModItemDet.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?leave_variety", MySqlDbType.Int16).Value = oModItemDet.GetSetvariety;
                cmd.Parameters.Add("?leave_reason", MySqlDbType.VarChar).Value = oModItemDet.GetSetreason;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItemDet.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItemDet.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItemDet.GetSetcreatedby;
                if (oModItemDet.GetSetcreatedby.Length > 0)
                {
                    if (oModItemDet.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItemDet.GetSetmodifiedby;
                if (oModItemDet.GetSetmodifiedby.Length > 0)
                {
                    if (oModItemDet.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItemDet.GetSetconfirmedby;
                if (oModItemDet.GetSetconfirmedby.Length > 0)
                {
                    if (oModItemDet.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItemDet.GetSetcancelledby;
                if (oModItemDet.GetSetcancelledby.Length > 0)
                {
                    if (oModItemDet.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffLeaveDetails: " + e.Message.ToString());
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

    public String updateStaffLeaveDetails(HRModel oModItemDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_leave ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, leave_cat = ?leave_cat, leave_type = ?leave_type, fromdate = ?fromdate, todate = ?todate, fromtime = ?fromtime, totime = ?totime, leave_variety = ?leave_variety, leave_reason = ?leave_reason, ";
                query = query + "        status = ?status, remarks = ?remarks, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate, ";
                query = query + "        reliefedby = ?reliefedby, reliefeddate = ?reliefeddate, approvedby = ?approvedby, approveddate = ?approveddate, rejectedby = ?rejectedby, rejecteddate= ?rejecteddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItemDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItemDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItemDet.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModItemDet.GetSetstaffno;
                cmd.Parameters.Add("?leave_cat", MySqlDbType.VarChar).Value = oModItemDet.GetSetcat;
                cmd.Parameters.Add("?leave_type", MySqlDbType.VarChar).Value = oModItemDet.GetSettype;
                if (oModItemDet.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSetfromtime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetfromtime, ukDtfi);
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromtime", MySqlDbType.DateTime).Value = null;
                }
                if (oModItemDet.GetSettotime.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(oModItemDet.GetSettotime, ukDtfi);
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?totime", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?leave_variety", MySqlDbType.Int16).Value = oModItemDet.GetSetvariety;
                cmd.Parameters.Add("?leave_reason", MySqlDbType.VarChar).Value = oModItemDet.GetSetreason;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItemDet.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItemDet.GetSetremarks;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItemDet.GetSetcreatedby;
                if (oModItemDet.GetSetcreatedby.Length > 0)
                {
                    if (oModItemDet.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItemDet.GetSetmodifiedby;
                if (oModItemDet.GetSetmodifiedby.Length > 0)
                {
                    if (oModItemDet.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItemDet.GetSetconfirmedby;
                if (oModItemDet.GetSetconfirmedby.Length > 0)
                {
                    if (oModItemDet.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItemDet.GetSetcancelledby;
                if (oModItemDet.GetSetcancelledby.Length > 0)
                {
                    if (oModItemDet.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItemDet.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffLeaveDetails: " + e.Message.ToString());
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

    public String updateStaffLeaveAttachment(HRModel oModItemDet, String filefolder, String filename1, String filename2, String filename3)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";
        //String filefolder = Server.MapPath("./Attachment/HumanResource/");

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_leave ";
                query = query + " SET    fyr = ?fyr ";
                if (filename1.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename1.Equals(filename1))
                    {
                        query = query + " ,filename1 = ?filename1, fileblob1 = ?fileblob1 ";
                    }
                }
                else
                {
                    query = query + " ,filename1 = null, fileblob1 = null ";
                }
                if (filename2.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename2.Equals(filename2))
                    {
                        query = query + " ,filename2 = ?filename2, fileblob2 = ?fileblob2 ";
                    }
                }
                else
                {
                    query = query + " ,filename2 = null, fileblob2 = null ";
                }
                if (filename3.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename3.Equals(filename3))
                    {
                        query = query + " ,filename3 = ?filename3, fileblob1 = ?fileblob3 ";
                    }
                }
                else
                {
                    query = query + " ,filename3 = null, fileblob3 = null ";
                }
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItemDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItemDet.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItemDet.GetSetfyr;
                if (filename1.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename1.Equals(filename1))
                    {
                        byte[] blobData1;
                        FileStream fs1 = new FileStream(filefolder + filename1, FileMode.Open, FileAccess.Read);
                        BinaryReader br1 = new BinaryReader(fs1);
                        blobData1 = br1.ReadBytes((int)fs1.Length);
                        br1.Close();
                        fs1.Close();

                        cmd.Parameters.Add("?filename1", MySqlDbType.VarChar).Value = filename1;
                        cmd.Parameters.Add("?fileblob1", MySqlDbType.Blob).Value = blobData1;
                    }
                }
                if (filename2.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename2.Equals(filename2))
                    {
                        byte[] blobData2;
                        FileStream fs2 = new FileStream(filefolder + filename2, FileMode.Open, FileAccess.Read);
                        BinaryReader br2 = new BinaryReader(fs2);
                        blobData2 = br2.ReadBytes((int)fs2.Length);
                        br2.Close();
                        fs2.Close();

                        cmd.Parameters.Add("?filename2", MySqlDbType.VarChar).Value = filename2;
                        cmd.Parameters.Add("?fileblob2", MySqlDbType.Blob).Value = blobData2;
                    }
                }
                if (filename3.Length > 0)
                {
                    if (!oModItemDet.GetSetfilename3.Equals(filename3))
                    {
                        byte[] blobData3;
                        FileStream fs3 = new FileStream(filefolder + filename3, FileMode.Open, FileAccess.Read);
                        BinaryReader br3 = new BinaryReader(fs3);
                        blobData3 = br3.ReadBytes((int)fs3.Length);
                        br3.Close();
                        fs3.Close();

                        cmd.Parameters.Add("?filename3", MySqlDbType.VarChar).Value = filename3;
                        cmd.Parameters.Add("?fileblob3", MySqlDbType.Blob).Value = blobData3;
                    }
                }

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffLeaveAttachment: " + e.Message.ToString());
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

    public String deleteStaffLeaveDetails(HRModel oModItemDet)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_leave ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItemDet.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItemDet.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffLeaveDetails: " + e.Message.ToString());
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

    public ArrayList getCompSalaryItemList(String comp, String item_cat, String item_type, String code, String desc)
    {
        ArrayList lsCompItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.description, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group"); 
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompSalaryItemList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompItem;
    }

    public HRModel getCompSalaryItemDetails(String comp, String item_cat, String item_type, String code, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.item_type, a.item_cat ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getCompSalaryItemDetails: " + e.Message.ToString());
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

    public String insertCompSalaryItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO salary_item (comp, code, description, item_type, item_cat, item_value, item_group, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?code, ?description, ?item_type, ?item_cat, ?item_value, ?item_group, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompSalaryItem: " + e.Message.ToString());
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

    public String updateCompSalaryItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE salary_item ";
                query = query + " SET    code = ?code, description = ?description, item_type = ?item_type, item_cat = ?item_cat, item_value = ?item_value, item_group = ?item_group, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompSalaryItem: " + e.Message.ToString());
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

    public String deleteCompSalaryItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM salary_item ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompSalaryItem: " + e.Message.ToString());
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

    public ArrayList getCompSalaryItemGroupList(String comp, String status)
    {
        ArrayList lsItemGroup = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                query = query + " and  a.item_cat in ('SALARY','ADDITION') ";
                query = query + " and  a.status = '"+ status + "' ";
                query = query + " order by a.comp, a.description, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsItemGroup.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompSalaryItemGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsItemGroup;
    }

    public ArrayList getCompSalaryGroupList(String comp, String fyr, String code, String desc, String salary_cat, String salary_type)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.salary_cat, a.salary_type, a.salary_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (salary_cat.Trim().Length > 0)
                {
                    query = query + " and  a.salary_cat = '" + salary_cat + "' ";
                }
                if (salary_type.Trim().Length > 0)
                {
                    query = query + " and  a.salary_type = '" + salary_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                query = query + " order by a.comp, a.fyr, a.salary_cat, a.salary_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "salary_cat");
                    modItem.GetSettype = replaceNull(dataReader, "salary_type");
                    modItem.GetSetcount = replaceZero(dataReader, "salary_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompSalaryGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public ArrayList getCompSalaryGroupList(String comp, String fyr, String code, String desc, String salary_cat, String salary_type, String staffno)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.salary_cat, a.salary_type, a.salary_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (salary_cat.Trim().Length > 0)
                {
                    query = query + " and  a.salary_cat = '" + salary_cat + "' ";
                }
                if (salary_type.Trim().Length > 0)
                {
                    query = query + " and  a.salary_type = '" + salary_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (desc.Trim().Length > 0)
                {
                    query = query + " and  upper(a.description) like '%" + desc + "%' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.id NOT IN (select b.lg_id from staff_salary_group b where a.comp = b.comp and a.fyr = b.fyr and b.staffno = '" + staffno + "') ";
                }
                query = query + " order by a.comp, a.fyr, a.salary_cat, a.salary_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "salary_cat");
                    modItem.GetSettype = replaceNull(dataReader, "salary_type");
                    modItem.GetSetcount = replaceZero(dataReader, "salary_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompSalaryGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public HRModel getCompSalaryGroupDetails(String comp, String fyr, String code, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.code, a.description, a.salary_cat, a.salary_type, a.salary_count, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   salary_group a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.salary_cat, a.salary_type, a.salary_count ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "salary_cat");
                    modItem.GetSettype = replaceNull(dataReader, "salary_type");
                    modItem.GetSetcount = replaceZero(dataReader, "salary_count");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getCompSalaryGroupDetails: " + e.Message.ToString());
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

    public String insertCompSalaryGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO salary_group (comp, fyr, code, description, salary_cat, salary_type, salary_count, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?code, ?description, ?salary_cat, ?salary_type, ?salary_count, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?salary_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?salary_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?salary_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompSalaryGroup: " + e.Message.ToString());
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

    public String updateCompSalaryGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE salary_group ";
                query = query + " SET    fyr = ?fyr, code = ?code, description = ?description, salary_cat = ?salary_cat, salary_type = ?salary_type, salary_count = ?salary_count, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?salary_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?salary_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?salary_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateCompSalaryGroup: " + e.Message.ToString());
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

    public String deleteCompSalaryGroup(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM salary_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompSalaryGroup: " + e.Message.ToString());
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

    public ArrayList getCompSalaryGroupItemList(String comp, String fyr, Int64 sg_id, Int64 si_id, String code)
    {
        ArrayList lsCompSGItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.sg_id, a.si_id, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group ";
                query = query + " from   salary_group_item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (si_id > 0)
                {
                    query = query + " and  a.si_id = " + si_id;
                }
                if (sg_id > 0)
                {
                    query = query + " and  a.sg_id = " + sg_id;
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  d.code = '" + code + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.sg_id, a.code ";
                //WriteToLogFile("HRController-getCompSalaryGroupItemList: [SQL] >> " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetsi_id = replaceZero(dataReader, "si_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    lsCompSGItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getCompSalaryGroupItemList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompSGItem;
    }

    public String insertCompSalaryGroupItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO salary_group_item (comp, fyr, sg_id, si_id, code, description, item_type, item_cat, item_value, item_group) ";
                query = query + " VALUES (?comp, ?fyr, ?sg_id, ?si_id, ?code, ?description, ?item_type, ?item_cat, ?item_value, ?item_group) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = oModItem.GetSetsg_id;
                cmd.Parameters.Add("?si_id", MySqlDbType.Int64).Value = oModItem.GetSetsi_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertCompSalaryGroupItem: " + e.Message.ToString());
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

    public String deleteCompSalaryGroupItem(String comp, String fyr, Int64 sg_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM salary_group_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND sg_id = ?sg_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = sg_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteCompSalaryGroupItem: " + e.Message.ToString());
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

    public ArrayList getStaffSalaryGroupList(String comp, String fyr, String staffno, Int64 grpid, String grpcode, String grpcat, String grptype, String status)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.sg_id, d.code, d.description, d.salary_cat, d.salary_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_salary_group a, staff_info b, staff_employment c, salary_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.sg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.sg_id = " + grpid;
                }
                if (grpcode.Trim().Length > 0)
                {
                    query = query + " and  d.code = '" + grpcode + "' ";
                }
                if (grpcat.Trim().Length > 0)
                {
                    query = query + " and  d.salary_cat = '" + grpcat + "' ";
                }
                if (grptype.Trim().Length > 0)
                {
                    query = query + " and  d.salary_type = '" + grptype + "' ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, d.salary_cat, d.salary_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "salary_cat");
                    modItem.GetSettype = replaceNull(dataReader, "salary_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffSalaryGroupList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public HRModel getStaffSalaryGroupDetails(String comp, String fyr, String staffno, Int64 grpid, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        a.sg_id, d.code, d.description, d.salary_cat, d.salary_type, date_format(a.fromdate,'%d-%m-%Y') fromdate, date_format(a.todate,'%d-%m-%Y') todate, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate ";
                query = query + " from   staff_salary_group a, staff_info b, staff_employment c, salary_group d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp AND a.staffno = b.staffno ";
                query = query + " AND    a.comp = c.comp and a.staffno = c.staffno and c.status = 'ACTIVE' ";
                query = query + " AND    a.comp = d.comp AND a.sg_id = d.id AND a.fyr = d.fyr ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (grpid > 0)
                {
                    query = query + " and  a.sg_id = " + grpid;
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, d.salary_cat, d.salary_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSetcat = replaceNull(dataReader, "salary_cat");
                    modItem.GetSettype = replaceNull(dataReader, "salary_type");
                    modItem.GetSetfromdate = replaceNull(dataReader, "fromdate");
                    modItem.GetSettodate = replaceNull(dataReader, "todate");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
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
            WriteToLogFile("HRController-getStaffSalaryGroupDetails: " + e.Message.ToString());
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

    public String insertStaffSalaryGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_group (comp, fyr, staffno, sg_id, fromdate, todate, status, remarks, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?sg_id, ?fromdate, ?todate, ?status, ?remarks, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = modItem.GetSetstaffno;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = modItem.GetSetsg_id;
                if (modItem.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (modItem.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = modItem.GetSetmodifiedby;
                if (modItem.GetSetmodifiedby.Length > 0)
                {
                    if (modItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffSalaryGroup: " + e.Message.ToString());
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

    public String updateStaffSalaryGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_salary_group ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, sg_id = ?sg_id, fromdate = ?fromdate, todate = ?todate, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = modItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = modItem.GetSetstaffno;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = modItem.GetSetsg_id;
                if (modItem.GetSetfromdate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSetfromdate, ukDtfi);
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?fromdate", MySqlDbType.DateTime).Value = null;
                }
                if (modItem.GetSettodate.Trim().Length > 0)
                {
                    DateTime datetime = Convert.ToDateTime(modItem.GetSettodate, ukDtfi);
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = datetime;
                }
                else
                {
                    cmd.Parameters.Add("?todate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = modItem.GetSetstatus;
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = modItem.GetSetremarks;
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = modItem.GetSetmodifiedby;
                if (modItem.GetSetmodifiedby.Length > 0)
                {
                    if (modItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(modItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
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
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateStaffSalaryGroup: " + e.Message.ToString());
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

    public String deleteStaffSalaryGroup(HRModel modItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_group ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = modItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = modItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffSalaryGroup: " + e.Message.ToString());
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

    public ArrayList getStaffSalaryItemList(String comp, String fyr, String staffno, Int64 sg_id, String item_cat, String item_type, String code, Int64 ssg_id)
    {
        ArrayList lsSalaryItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.staffno, a.ssg_id, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, a.item_amount ";
                query = query + " from   staff_salary_group_item a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  a.staffno = '" + staffno + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  a.sg_id = " + sg_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  a.ssg_id = " + ssg_id;
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.staffno, a.code, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetsg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetitemamount = replaceDoubleZero(dataReader, "item_amount");
                    lsSalaryItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getStaffSalaryItemList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsSalaryItem;
    }

    public String deleteStaffSalaryItemList(String comp, String fyr, String staffno, Int64 sg_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_group_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND staffno = ?staffno AND sg_id = ?sg_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = staffno;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = sg_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffSalaryItemList: " + e.Message.ToString());
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

    public String deleteStaffSalaryItemList(String comp, String fyr, String staffno, Int64 sg_id, Int64 ssg_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_group_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND staffno = ?staffno AND sg_id = ?sg_id AND ssg_id = ?ssg_id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = staffno;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = sg_id;
                cmd.Parameters.Add("?ssg_id", MySqlDbType.Int64).Value = ssg_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteStaffSalaryItemList: " + e.Message.ToString());
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

    public String insertStaffSalaryItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_group_item (comp, fyr, sg_id, ssg_id, staffno, code, description, item_type, item_cat, item_value, item_group, item_amount) ";
                query = query + " VALUES (?comp, ?fyr, ?sg_id, ?ssg_id, ?staffno, ?code, ?description, ?item_type, ?item_cat, ?item_value, ?item_group, ?item_amount) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = oModItem.GetSetsg_id;
                cmd.Parameters.Add("?ssg_id", MySqlDbType.Int64).Value = oModItem.GetSetssg_id;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModItem.GetSetstaffno;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.Parameters.Add("?item_amount", MySqlDbType.Double).Value = oModItem.GetSetitemamount;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertStaffSalaryItem: " + e.Message.ToString());
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
    public ArrayList getRunSalaryList(String comp, String fyr, String run_cat, String run_type)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }
    public HRModel getRunSalaryDetails(String comp, String fyr, String run_cat, String run_type, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");

                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryDetails: " + e.Message.ToString());
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
    public HRModel getRunSalaryDetails(String comp, String fyr, String run_cat, String run_type, Int64 id, int run_month, int run_year, String status)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a ";
                query = query + " WHERE  a.comp is not NULL ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id + " ";
                }
                if (run_month > 0)
                {
                    query = query + " and  a.run_month = " + run_month + " ";
                }
                if (run_year > 0)
                {
                    query = query + " and  a.run_year = " + run_year + " ";
                }
                if (status.Trim().Length > 0)
                {
                    query = query + " and  a.status = '" + status + "' ";
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");

                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryDetails: " + e.Message.ToString());
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

    public String insertRunSalary(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary (comp, fyr, run_cat, run_type, run_count, run_month, run_year, run_by, run_date, remarks, status, createdby, createddate, modifiedby, modifieddate, confirmedby, confirmeddate, cancelledby, cancelleddate, verifiedby, verifieddate, approvedby, approveddate, rejectedby, rejecteddate) ";
                query = query + " VALUES (?comp, ?fyr, ?run_cat, ?run_type, ?run_count, ?run_month, ?run_year, ?run_by, ?run_date, ?remarks, ?status, ?createdby, ?createddate, ?modifiedby, ?modifieddate, ?confirmedby, ?confirmeddate, ?cancelledby, ?cancelleddate, ?verifiedby, ?verifieddate, ?approvedby, ?approveddate, ?rejectedby, ?rejecteddate) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?run_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?run_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?run_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?run_month", MySqlDbType.Int16).Value = oModItem.GetSetmonth;
                cmd.Parameters.Add("?run_year", MySqlDbType.Int16).Value = oModItem.GetSetyear;
                cmd.Parameters.Add("?run_by", MySqlDbType.VarChar).Value = oModItem.GetSetrunby;
                if (oModItem.GetSetrunby.Length > 0)
                {
                    if (oModItem.GetSetrundate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetrundate);
                        cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = DateTime.Now;

                    }
                }
                else
                {
                    cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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

                cmd.Parameters.Add("?verifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetverifiedby;
                if (oModItem.GetSetverifiedby.Length > 0)
                {
                    if (oModItem.GetSetverifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetverifieddate);
                        cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModItem.GetSetapprovedby;
                if (oModItem.GetSetapprovedby.Length > 0)
                {
                    if (oModItem.GetSetapproveddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetapproveddate);
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                }

                cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModItem.GetSetrejectedby;
                if (oModItem.GetSetrejectedby.Length > 0)
                {
                    if (oModItem.GetSetrejecteddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetrejecteddate);
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalary: " + e.Message.ToString());
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

    public String updateRunSalary(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_salary ";
                query = query + " SET    fyr = ?fyr, run_cat = ?run_cat, run_type = ?run_type, run_count = ?run_count, run_month = ?run_month, run_year = ?run_year, run_by = ?run_by, run_date = ?run_date, ";
                query = query + "        remarks = ?remarks, status = ?status, createdby = ?createdby, createddate = ?createddate, modifiedby = ?modifiedby, modifieddate = ?modifieddate, confirmedby = ?confirmedby, confirmeddate = ?confirmeddate, cancelledby = ?cancelledby, cancelleddate = ?cancelleddate, ";
                query = query + "        verifiedby = ?verifiedby, verifieddate = ?verifieddate, approvedby = ?approvedby, approveddate = ?approveddate, rejectedby = ?rejectedby, rejecteddate = ?rejecteddate ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?run_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?run_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?run_count", MySqlDbType.Int16).Value = oModItem.GetSetcount;
                cmd.Parameters.Add("?run_month", MySqlDbType.Int16).Value = oModItem.GetSetmonth;
                cmd.Parameters.Add("?run_year", MySqlDbType.Int16).Value = oModItem.GetSetyear;
                cmd.Parameters.Add("?run_by", MySqlDbType.VarChar).Value = oModItem.GetSetrunby;
                if (oModItem.GetSetrunby.Length > 0)
                {
                    if (oModItem.GetSetrundate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetrundate);
                        cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = DateTime.Now;

                    }
                }
                else
                {
                    cmd.Parameters.Add("?run_date", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?remarks", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.Parameters.Add("?status", MySqlDbType.VarChar).Value = oModItem.GetSetstatus;
                cmd.Parameters.Add("?createdby", MySqlDbType.VarChar).Value = oModItem.GetSetcreatedby;
                if (oModItem.GetSetcreatedby.Length > 0)
                {
                    if (oModItem.GetSetcreateddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcreateddate);
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
                cmd.Parameters.Add("?modifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetmodifiedby;
                if (oModItem.GetSetmodifiedby.Length > 0)
                {
                    if (oModItem.GetSetmodifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetmodifieddate);
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?modifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?confirmedby", MySqlDbType.VarChar).Value = oModItem.GetSetconfirmedby;
                if (oModItem.GetSetconfirmedby.Length > 0)
                {
                    if (oModItem.GetSetconfirmeddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetconfirmeddate);
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
                cmd.Parameters.Add("?cancelledby", MySqlDbType.VarChar).Value = oModItem.GetSetcancelledby;
                if (oModItem.GetSetcancelledby.Length > 0)
                {
                    if (oModItem.GetSetcancelleddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetcancelleddate);
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
                cmd.Parameters.Add("?verifiedby", MySqlDbType.VarChar).Value = oModItem.GetSetverifiedby;
                if (oModItem.GetSetverifiedby.Length > 0)
                {
                    if (oModItem.GetSetverifieddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetverifieddate);
                        cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?verifieddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.Parameters.Add("?approvedby", MySqlDbType.VarChar).Value = oModItem.GetSetapprovedby;
                if (oModItem.GetSetapprovedby.Length > 0)
                {
                    if (oModItem.GetSetapproveddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetapproveddate);
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?approveddate", MySqlDbType.DateTime).Value = null;
                }

                cmd.Parameters.Add("?rejectedby", MySqlDbType.VarChar).Value = oModItem.GetSetrejectedby;
                if (oModItem.GetSetrejectedby.Length > 0)
                {
                    if (oModItem.GetSetrejecteddate.Trim().Length > 0)
                    {
                        DateTime datetime = Convert.ToDateTime(oModItem.GetSetrejecteddate);
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = datetime;
                    }
                    else
                    {
                        cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = DateTime.Now;
                    }
                }
                else
                {
                    cmd.Parameters.Add("?rejecteddate", MySqlDbType.DateTime).Value = null;
                }
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateRunSalary: " + e.Message.ToString());
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

    public String deleteRunSalary(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteRunSalary: " + e.Message.ToString());
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

    public ArrayList getRunSalaryStaffList(String comp, String fyr, String staffno, String run_cat, String run_type, Int64 sg_id, Int64 ss_id, Int64 ssg_id)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.fyr, d.sg_id, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        b.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        d.id, d.ss_id, d.ssg_id, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a, staff_info b, staff_employment c, staff_salary_details d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = d.comp AND a.fyr = d.fyr AND a.id = d.ss_id ";
                query = query + " AND    d.comp = b.comp AND d.staffno = b.staffno ";
                query = query + " AND    d.comp = c.comp and d.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  d.staffno = '" + staffno + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  d.sg_id = " + sg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  d.ss_id = " + ss_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  d.ssg_id = " + ssg_id;
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryStaffList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public ArrayList getRunSalaryStaffList(String comp, String fyr, String staffno, String run_cat, String run_type, Int64 sg_id, Int64 ss_id, Int64 ssg_id, String notin)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.fyr, d.sg_id, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        b.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        d.id, d.ss_id, d.ssg_id, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a, staff_info b, staff_employment c, staff_salary_details d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = d.comp AND a.fyr = d.fyr AND a.id = d.ss_id ";
                query = query + " AND    d.comp = b.comp AND d.staffno = b.staffno ";
                query = query + " AND    d.comp = c.comp and d.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  d.staffno = '" + staffno + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  d.sg_id = " + sg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  d.ss_id = " + ss_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  d.ssg_id = " + ssg_id;
                }
                if (notin.Trim().Length > 0)
                {
                    query = query + " and  d.staffno NOT IN (" + notin + ") ";
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryStaffList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }

    public ArrayList getRunSalaryStaffListWithAttachment(String comp, String fyr, String staffno, String run_cat, String run_type, Int64 sg_id, Int64 ss_id, Int64 ssg_id, String filefolder)
    {
        ArrayList lsCompLeave = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.comp, a.fyr, d.sg_id, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        b.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        d.id, d.ss_id, d.ssg_id, d.filename1, d.fileblob1, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a, staff_info b, staff_employment c, staff_salary_details d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = d.comp AND a.fyr = d.fyr AND a.id = d.ss_id ";
                query = query + " AND    d.comp = b.comp AND d.staffno = b.staffno ";
                query = query + " AND    d.comp = c.comp and d.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  d.staffno = '" + staffno + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  d.sg_id = " + sg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  d.ss_id = " + ss_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  d.ssg_id = " + ssg_id;
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetfilename1 = replaceNull(dataReader, "filename1");
                    String sUrl = filefolder + modItem.GetSetfilename1;
                    if (dataReader["fileblob1"] != DBNull.Value)
                    {
                        File.WriteAllBytes(sUrl, (byte[])dataReader["fileblob1"]);
                    }
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");
                    lsCompLeave.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryStaffList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsCompLeave;
    }
    public HRModel getRunSalaryStaffDetails(String comp, String fyr, String staffno, String run_cat, String run_type, Int64 sg_id, Int64 ss_id, Int64 ssg_id, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = "";
                query = query + " SELECT a.comp, a.fyr, d.sg_id, a.run_cat, a.run_type, a.run_count, a.run_month, a.run_year, a.run_date, a.run_by, ";
                query = query + "        b.staffno, b.name, c.emp_deptid dept_id, (select dept_name from department_comp x where c.comp = x.comp and c.emp_deptid = x.dept_id) dept_name, ";
                query = query + "        c.emp_gredid gred_id, (select gred_name from grade_comp y where c.comp = y.comp and c.emp_gredid = y.gred_id) gred_name, ";
                query = query + "        c.emp_posid pos_id, (select pos_name from position_comp z where c.comp = z.comp and c.emp_posid = z.pos_id) pos_name, ";
                query = query + "        d.id, d.ss_id, d.ssg_id, ";
                query = query + "        a.status, a.remarks, a.createdby, a.createddate, a.modifiedby, a.modifieddate, a.confirmedby, a.confirmeddate, a.cancelledby, a.cancelleddate, ";
                query = query + "        a.verifiedby, a.verifieddate, a.approvedby, a.approveddate, a.rejectedby, a.rejecteddate ";
                query = query + " from   staff_salary a, staff_info b, staff_employment c, staff_salary_details d ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = d.comp AND a.fyr = d.fyr AND a.id = d.ss_id ";
                query = query + " AND    d.comp = b.comp AND d.staffno = b.staffno ";
                query = query + " AND    d.comp = c.comp and d.staffno = c.staffno and c.status = 'ACTIVE' ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  d.staffno = '" + staffno + "' ";
                }
                if (run_cat.Trim().Length > 0)
                {
                    query = query + " and  a.run_cat = '" + run_cat + "' ";
                }
                if (run_type.Trim().Length > 0)
                {
                    query = query + " and  a.run_type = '" + run_type + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  d.sg_id = " + sg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  d.ss_id = " + ss_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  d.ssg_id = " + ssg_id;
                }
                if (id > 0)
                {
                    query = query + " and  d.id = " + id + " ";
                }
                query = query + " order by a.comp, a.fyr, a.run_cat, a.run_type ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetname = replaceNull(dataReader, "name");
                    modItem.GetSetdept_id = replaceNull(dataReader, "dept_id");
                    modItem.GetSetdept_name = replaceNull(dataReader, "dept_name");
                    modItem.GetSetgred_id = replaceNull(dataReader, "gred_id");
                    modItem.GetSetgred_name = replaceNull(dataReader, "gred_name");
                    modItem.GetSetpos_id = replaceNull(dataReader, "pos_id");
                    modItem.GetSetpos_name = replaceNull(dataReader, "pos_name");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetcat = replaceNull(dataReader, "run_cat");
                    modItem.GetSettype = replaceNull(dataReader, "run_type");
                    modItem.GetSetcount = replaceZero(dataReader, "run_count");
                    modItem.GetSetmonth = replaceZero(dataReader, "run_month");
                    modItem.GetSetyear = replaceZero(dataReader, "run_year");
                    modItem.GetSetrundate = replaceNull(dataReader, "run_date");
                    modItem.GetSetrunby = replaceNull(dataReader, "run_by");
                    modItem.GetSetstatus = replaceNull(dataReader, "status");
                    modItem.GetSetremarks = replaceNull(dataReader, "remarks");
                    modItem.GetSetcreatedby = replaceNull(dataReader, "createdby");
                    modItem.GetSetcreateddate = replaceNull(dataReader, "createddate");
                    modItem.GetSetmodifiedby = replaceNull(dataReader, "modifiedby");
                    modItem.GetSetmodifieddate = replaceNull(dataReader, "modifieddate");
                    modItem.GetSetconfirmedby = replaceNull(dataReader, "confirmedby");
                    modItem.GetSetconfirmeddate = replaceNull(dataReader, "confirmeddate");
                    modItem.GetSetcancelledby = replaceNull(dataReader, "cancelledby");
                    modItem.GetSetcancelleddate = replaceNull(dataReader, "cancelleddate");
                    modItem.GetSetverifiedby = replaceNull(dataReader, "verifiedby");
                    modItem.GetSetverifieddate = replaceNull(dataReader, "verifieddate");
                    modItem.GetSetapprovedby = replaceNull(dataReader, "approvedby");
                    modItem.GetSetapproveddate = replaceNull(dataReader, "approveddate");
                    modItem.GetSetrejectedby = replaceNull(dataReader, "rejectedby");
                    modItem.GetSetrejecteddate = replaceNull(dataReader, "rejecteddate");

                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunSalaryStaffDetails: " + e.Message.ToString());
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
    public String insertRunSalaryStaff(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_details (comp, fyr, staffno, ss_id, sg_id, ssg_id) ";
                query = query + " VALUES (?comp, ?fyr, ?staffno, ?ss_id, ?sg_id, ?ssg_id) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModItem.GetSetstaffno;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = oModItem.GetSetsg_id;
                cmd.Parameters.Add("?ssg_id", MySqlDbType.Int64).Value = oModItem.GetSetssg_id;

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalaryStaff: " + e.Message.ToString());
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

    public String updateRunSalaryStaff(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_salary_details ";
                query = query + " SET    fyr = ?fyr, staffno = ?staffno, ss_id = ?ss_id, sg_id = ?sg_id, ssg_id = ?ssg_id ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = oModItem.GetSetstaffno;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.Parameters.Add("?sg_id", MySqlDbType.Int64).Value = oModItem.GetSetsg_id;
                cmd.Parameters.Add("?ssg_id", MySqlDbType.Int64).Value = oModItem.GetSetssg_id;

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateRunSalaryStaff: " + e.Message.ToString());
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

    public String updateRunSalaryStaffAttachment(HRModel oModSalaryEmp, String filefolder, String filename1)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";
        //String filefolder = "./Attachment/HumanResource/";
        //String filefolder = Server.MapPath("./Attachment/HumanResource/");

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_salary_details ";
                query = query + " SET    fyr = ?fyr ";
                if (filename1.Length > 0)
                {
                    //if (!oModSalaryEmp.GetSetfilename1.Equals(filename1))
                    {
                        query = query + " ,filename1 = ?filename1, fileblob1 = ?fileblob1 ";
                    }
                }
                else
                {
                    query = query + " ,filename1 = null, fileblob1 = null ";
                }
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModSalaryEmp.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModSalaryEmp.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModSalaryEmp.GetSetfyr;
                if (filename1.Length > 0)
                {
                    //if (!oModSalaryEmp.GetSetfilename1.Equals(filename1))
                    {
                        byte[] blobData1;
                        FileStream fs1 = new FileStream(filefolder, FileMode.Open, FileAccess.Read);
                        BinaryReader br1 = new BinaryReader(fs1);
                        blobData1 = br1.ReadBytes((int)fs1.Length);
                        br1.Close();
                        fs1.Close();

                        cmd.Parameters.Add("?filename1", MySqlDbType.VarChar).Value = filename1;
                        cmd.Parameters.Add("?fileblob1", MySqlDbType.Blob).Value = blobData1;
                    }
                }

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-updateRunSalaryStaffAttachment: " + e.Message.ToString());
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


    public String deleteRunSalaryStaff(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_details ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteRunSalaryStaff: " + e.Message.ToString());
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
    public String deleteRunSalaryStaff(String comp, String fyr, String staffno, Int64 ss_id, Int64 ssg_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_details ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND ss_id = ?ss_id ";
                if (staffno.Trim().Length > 0)
                {
                    query = query + " AND  staffno = '" + staffno + "' ";
                }
                if (ssg_id > 0)
                {
                    query = query + " AND  ssg_id = " + ssg_id;
                }
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = ss_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteRunSalaryStaff: " + e.Message.ToString());
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

    public String insertRunSalaryStaffItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_details_item (comp, fyr, ss_id, ssd_id, code, description, item_cat, item_type, item_value, item_group, item_amount) ";
                query = query + " VALUES (?comp, ?fyr, ?ss_id, ?ssd_id, ?code, ?description, ?item_cat, ?item_type, ?item_value, ?item_group, ?item_amount) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.Parameters.Add("?ssd_id", MySqlDbType.Int64).Value = oModItem.GetSetssd_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.Parameters.Add("?item_amount", MySqlDbType.Double).Value = oModItem.GetSetitemamount;

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalaryStaffItem: " + e.Message.ToString());
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
    public String insertRunSalaryStaffItem(String comp, String fyr, String staffno, Int64 ssg_id, Int64 ss_id, Int64 ssd_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_details_item (comp, fyr, ss_id, ssd_id, code, description, item_cat, item_type, item_value, item_group, item_amount) ";
                query = query + " SELECT comp, fyr, ?ss_id, ?ssd_id, code, description, item_cat, item_type, item_value, item_group, item_amount ";
                query = query + " FROM   staff_salary_group_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND staffno = ?staffno AND ssg_id = ?ssg_id ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?staffno", MySqlDbType.VarChar).Value = staffno;
                cmd.Parameters.Add("?ssg_id", MySqlDbType.Int64).Value = ssg_id;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = ss_id;
                cmd.Parameters.Add("?ssd_id", MySqlDbType.Int64).Value = ssd_id;

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalaryStaffItem: " + e.Message.ToString());
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
    public String updateRunSalaryStaffItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " UPDATE staff_salary_details_item ";
                query = query + " SET    fyr = ?fyr, ss_id = ?ss_id, ssd_id = ?ssd_id, code = ?code, description = ?description, item_cat = ?item_cat, item_type = ?item_type, item_value = ?item_value, item_group = ?item_group, item_amount = ?item_amount ";
                query = query + " WHERE  comp = ?comp AND id = ?id ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = oModItem.GetSetid;
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.Parameters.Add("?ssd_id", MySqlDbType.Int64).Value = oModItem.GetSetssd_id;
                cmd.Parameters.Add("?code", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?description", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?item_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?item_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?item_value", MySqlDbType.Double).Value = oModItem.GetSetitemvalue;
                cmd.Parameters.Add("?item_group", MySqlDbType.VarChar).Value = oModItem.GetSetitemgroup;
                cmd.Parameters.Add("?item_amount", MySqlDbType.Double).Value = oModItem.GetSetitemamount;

                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalaryStaffItem: " + e.Message.ToString());
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
    public ArrayList getRunStaffSalaryItemList(String comp, String fyr, String staffno, Int64 sg_id, Int64 ssg_id, Int64 ss_id, Int64 ssd_id, String item_cat, String item_type, String code)
    {
        ArrayList lsSalaryItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, b.staffno, b.sg_id, b.ssg_id, a.ss_id, a.ssd_id, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, a.item_amount ";
                query = query + " from   staff_salary_details_item a, staff_salary_details b ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp ";
                query = query + " AND    a.fyr = b.fyr ";
                query = query + " AND    a.ssd_id = b.id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  b.staffno = '" + staffno + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  b.sg_id = " + sg_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  b.ssg_id = " + ssg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  a.ss_id = " + ss_id;
                }
                if (ssd_id > 0)
                {
                    query = query + " and  a.ssd_id = " + ssd_id;
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                query = query + " order by a.comp, a.fyr, b.staffno, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssd_id = replaceZero(dataReader, "ssd_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetitemamount = replaceDoubleZero(dataReader, "item_amount");
                    lsSalaryItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunStaffSalaryItemList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsSalaryItem;
    }
    public HRModel getRunStaffSalaryItemDetails(String comp, String fyr, String staffno, Int64 sg_id, Int64 ssg_id, Int64 ss_id, Int64 ssd_id, String item_cat, String item_type, String code, Int64 id)
    {
        HRModel modItem = new HRModel();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, b.staffno, b.sg_id, b.ssg_id, a.ss_id, a.ssd_id, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, a.item_amount ";
                query = query + " from   staff_salary_details_item a, staff_salary_details b ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp ";
                query = query + " AND    a.fyr = b.fyr ";
                query = query + " AND    a.ssd_id = b.id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  b.staffno = '" + staffno + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  b.sg_id = " + sg_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  b.ssg_id = " + ssg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  a.ss_id = " + ss_id;
                }
                if (ssd_id > 0)
                {
                    query = query + " and  a.ssd_id = " + ssd_id;
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (id > 0)
                {
                    query = query + " and  a.id = " + id;
                }
                query = query + " order by a.comp, a.fyr, b.staffno, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssd_id = replaceZero(dataReader, "ssd_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetitemamount = replaceDoubleZero(dataReader, "item_amount");
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunStaffSalaryItemDetails: " + e.Message.ToString());
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
    public ArrayList getRunStaffSalaryItemList(String comp, String fyr, String staffno, Int64 sg_id, Int64 ssg_id, Int64 ss_id, Int64 ssd_id, String item_cat, String item_type, String code, String notin)
    {
        ArrayList lsSalaryItem = new ArrayList();
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " SELECT a.id, a.comp, a.fyr, b.staffno, b.sg_id, b.ssg_id, a.ss_id, a.ssd_id, a.code, a.description, a.item_type, a.item_cat, a.item_value, a.item_group, a.item_amount ";
                query = query + " from   staff_salary_details_item a, staff_salary_details b ";
                query = query + " WHERE  a.comp is not NULL ";
                query = query + " AND    a.comp = b.comp ";
                query = query + " AND    a.fyr = b.fyr ";
                query = query + " AND    a.ssd_id = b.id ";
                if (comp.Trim().Length > 0)
                {
                    query = query + " and  a.comp = '" + comp + "' ";
                }
                if (fyr.Trim().Length > 0)
                {
                    query = query + " and  a.fyr = '" + fyr + "' ";
                }
                if (staffno.Trim().Length > 0)
                {
                    query = query + " and  b.staffno = '" + staffno + "' ";
                }
                if (sg_id > 0)
                {
                    query = query + " and  b.sg_id = " + sg_id;
                }
                if (ssg_id > 0)
                {
                    query = query + " and  b.ssg_id = " + ssg_id;
                }
                if (ss_id > 0)
                {
                    query = query + " and  a.ss_id = " + ss_id;
                }
                if (ssd_id > 0)
                {
                    query = query + " and  a.ssd_id = " + ssd_id;
                }
                if (item_cat.Trim().Length > 0)
                {
                    query = query + " and  a.item_cat = '" + item_cat + "' ";
                }
                if (item_type.Trim().Length > 0)
                {
                    query = query + " and  a.item_type = '" + item_type + "' ";
                }
                if (code.Trim().Length > 0)
                {
                    query = query + " and  a.code = '" + code + "' ";
                }
                if (notin.Trim().Length > 0)
                {
                    query = query + " and  a.code NOT IN (" + notin + ") ";
                }
                query = query + " order by a.comp, a.fyr, b.staffno, a.item_type, a.item_cat ";
                //WriteToLogFile("HRController-getCompSalaryItemList: [SQL] > " + query);
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    HRModel modItem = new HRModel();
                    modItem.GetSetid = replaceZero(dataReader, "id");
                    modItem.GetSetcomp = replaceNull(dataReader, "comp");
                    modItem.GetSetfyr = replaceNull(dataReader, "fyr");
                    modItem.GetSetstaffno = replaceNull(dataReader, "staffno");
                    modItem.GetSetsg_id = replaceZero(dataReader, "sg_id");
                    modItem.GetSetssg_id = replaceZero(dataReader, "ssg_id");
                    modItem.GetSetss_id = replaceZero(dataReader, "ss_id");
                    modItem.GetSetssd_id = replaceZero(dataReader, "ssd_id");
                    modItem.GetSetcode = replaceNull(dataReader, "code");
                    modItem.GetSetdesc = replaceNull(dataReader, "description");
                    modItem.GetSettype = replaceNull(dataReader, "item_type");
                    modItem.GetSetcat = replaceNull(dataReader, "item_cat");
                    modItem.GetSetitemvalue = replaceDoubleZero(dataReader, "item_value");
                    modItem.GetSetitemgroup = replaceNull(dataReader, "item_group");
                    modItem.GetSetitemamount = replaceDoubleZero(dataReader, "item_amount");
                    lsSalaryItem.Add(modItem);
                }
                dataReader.Close();
                dbConnect.CloseConnection();
            }
        }
        catch (Exception e)
        {
            WriteToLogFile("HRController-getRunStaffSalaryItemList: " + e.Message.ToString());
        }
        finally
        {
            if (dbConnect.connection.State.HasFlag(ConnectionState.Open))
            {
                dbConnect.CloseConnection();
            }
        }
        return lsSalaryItem;
    }

    public String deleteRunSalaryStaffItem(String comp, String fyr, Int64 ss_id, Int64 ssd_id)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_details_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND ss_id = ?ss_id ";
                if (ssd_id > 0)
                {
                    query = query + " AND  ssd_id = " + ssd_id;
                }
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = comp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = fyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = ss_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteRunSalaryStaff: " + e.Message.ToString());
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

    public String deleteRunSalaryStaffItem(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " DELETE FROM staff_salary_details_item ";
                query = query + " WHERE  comp = ?comp AND fyr = ?fyr AND ss_id = ?ss_id ";
                if (oModItem.GetSetssd_id > 0)
                {
                    query = query + " AND  ssd_id = " + oModItem.GetSetssd_id;
                }
                if (oModItem.GetSetid > 0)
                {
                    query = query + " AND  id = " + oModItem.GetSetid;
                }
                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-deleteRunSalaryStaffItem: " + e.Message.ToString());
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

    public String insertRunSalaryExpenses(HRModel oModItem)
    {
        String result = "Y";
        DBConnect dbConnect = new DBConnect(sErrorLog);
        String query = "";

        try
        {
            if (dbConnect.OpenConnection() == true)
            {
                query = "";
                query = query + " INSERT INTO staff_salary_expenses (comp, fyr, ss_id, run_cat, run_type, run_month, run_year, exp_item, exp_desc, exp_amount, expensesno) ";
                query = query + " VALUES (?comp, ?fyr, ?ss_id, ?run_cat, ?run_type, ?run_month, ?run_year, ?exp_item, ?exp_desc, ?exp_amount, ?expensesno) ";

                MySqlCommand cmd = new MySqlCommand(query, dbConnect.connection);
                cmd.Parameters.Add("?comp", MySqlDbType.VarChar).Value = oModItem.GetSetcomp;
                cmd.Parameters.Add("?fyr", MySqlDbType.VarChar).Value = oModItem.GetSetfyr;
                cmd.Parameters.Add("?ss_id", MySqlDbType.Int64).Value = oModItem.GetSetss_id;
                cmd.Parameters.Add("?run_cat", MySqlDbType.VarChar).Value = oModItem.GetSetcat;
                cmd.Parameters.Add("?run_type", MySqlDbType.VarChar).Value = oModItem.GetSettype;
                cmd.Parameters.Add("?run_month", MySqlDbType.Int16).Value = oModItem.GetSetmonth;
                cmd.Parameters.Add("?run_year", MySqlDbType.Int16).Value = oModItem.GetSetyear;
                cmd.Parameters.Add("?exp_item", MySqlDbType.VarChar).Value = oModItem.GetSetcode;
                cmd.Parameters.Add("?exp_desc", MySqlDbType.VarChar).Value = oModItem.GetSetdesc;
                cmd.Parameters.Add("?exp_amount", MySqlDbType.Double).Value = oModItem.GetSetitemamount;
                cmd.Parameters.Add("?expensesno", MySqlDbType.VarChar).Value = oModItem.GetSetremarks;
                cmd.CommandTimeout = 60;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
            dbConnect.CloseConnection();
        }
        catch (Exception e)
        {
            result = "N";
            WriteToLogFile("HRController-insertRunSalaryExpenses: " + e.Message.ToString());
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


    #endregion/*** END FOR HR ***/

    #region/*** BEGIN FOR GENERAL***/

    public ArrayList getFYRList(string currcomp)
    {
        ArrayList lsFYR = new ArrayList();
        if (currcomp.Length > 0)
        {
            lsFYR.Add(new { GetSetfyrid = "2019", GetSetfyrdesc = "2019" });
            lsFYR.Add(new { GetSetfyrid = "2020", GetSetfyrdesc = "2020" });
            lsFYR.Add(new { GetSetfyrid = "2021", GetSetfyrdesc = "2021" });
            lsFYR.Add(new { GetSetfyrid = "2022", GetSetfyrdesc = "2022" });
        }

        return lsFYR;
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
            WriteToLogFile("HRController-getRunningNoList: " + e.Message.ToString());
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
            WriteToLogFile("HRController-createNewRunningNoList: " + e.Message.ToString());
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
            WriteToLogFile("HRController-getNextRunningNo: " + e.Message.ToString());
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
            WriteToLogFile("HRController-updateNextRunningNo: " + e.Message.ToString());
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
            WriteToLogFile("HRController-getNextSecond: " + e.Message.ToString());
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
        {
            String strDouble = oDataReader[sField].ToString();
            return double.Parse(strDouble);
        }
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

    public HRModel getAlertMessage(String message)
    {
        HRModel oAlertMssg = new HRModel();
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
            WriteToLogFile(DateTime.Now.ToString() + ": [HRController.cs:getFileAttached()] " + e.Message.ToString());
        }
        return lsFileAttached;
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