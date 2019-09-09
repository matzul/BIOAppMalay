﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdjustmentListing : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sAdjustmentNo = "";
    public String sItemNo = "";
    public String sStartDate = "";
    public String sEndDate = "";
    public String sStatus = "";
    public ArrayList lsItem = new ArrayList();
    public ArrayList lsAdjustmentHeader = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }
    private void initialValues()
    {
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        lsItem = oMainCon.getItemList(sCurrComp,"","","INVENTORY");
    }

    private void getValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }

        lsItem = oMainCon.getItemList(sCurrComp, "", "", "INVENTORY");

        if (sAction.Equals("SEARCH"))
        {
            sAdjustmentNo = oMainCon.replaceNull(Request.Params.Get("adjustmentno"));
            sItemNo = oMainCon.replaceNull(Request.Params.Get("itemno"));
        }
        if (sAction.Equals("RESET"))
        {
            sAdjustmentNo = "";
            sItemNo = "";
        }
    }

    private void processValues()
    {
        if (sCurrComp.Length > 0 && sUserId.Length > 0)
        {
            lsAdjustmentHeader = oMainCon.getAdjustmentHeaderList(sCurrComp, sAdjustmentNo,sItemNo, sStatus);
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            getValues();
            processValues();
        }
    }

}