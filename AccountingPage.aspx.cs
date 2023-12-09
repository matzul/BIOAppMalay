using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccountingPage : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public AccountingController oAccCon = new AccountingController();
    public String sCurrComp = "";
    public String sCurrFyr = "";
    public String sUserId = "";
    public String sAction = "";
    public double jumaset = 0, jumekuiti = 0, jumliabiliti = 0, jumhasil = 0, jumbelanja = 0;

    public ArrayList lsFisCOATran = new ArrayList();

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
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }
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
        if (Session["fyr"] != null)
        {
            sCurrFyr = Session["fyr"].ToString();
        }

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }

        if (sAction.Equals("UPDATE_DASHBOARD"))
        {
        }

    }
    private void processValues()
    {
        if (sAction.Equals("UPDATE_DASHBOARD"))
        {
            /* TO REPLACE USING STORED PROCEDURE IN DATABASE
            //update FisCOATranDetails
            int inital = oAccCon.updateInitialAmountFisCOATranEndLevel(sCurrComp, sCurrFyr, 0, 0);
            ArrayList lsFisCOALegderTranDebitCredit = oAccCon.getFisCOALedgerTranDebitCredit(sCurrComp, sCurrFyr, "", "Y");
            for(int i=0; i<lsFisCOALegderTranDebitCredit.Count; i++)
            {
                AccountingModel modItem = (AccountingModel)lsFisCOALegderTranDebitCredit[i];
                AccountingModel modFisCOATran = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetid, modItem.GetSetfyr, "", "", "", 0, "", "", "", "", "");
                modFisCOATran.GetSetdebit = modItem.GetSetdebit;
                modFisCOATran.GetSetcredit = modItem.GetSetcredit;
                int i2 = oAccCon.updateFisCOATran(modFisCOATran);
            }
            //update Parent FisCOATranDetails
            int maxLevel = oAccCon.getMaxLevelCOA(sCurrComp, sCurrFyr, "");
            for(int j=maxLevel; j>0; j--)
            {
                ArrayList lsFisCOALevelDebitCredit = oAccCon.getFisCOALevelDebitCredit(sCurrComp, sCurrFyr, "", j-1);
                for (int k = 0; k < lsFisCOALevelDebitCredit.Count; k++)
                {
                    AccountingModel modItem = (AccountingModel)lsFisCOALevelDebitCredit[k];
                    AccountingModel modFisCOATran = oAccCon.getFisCOATranDetail(modItem.GetSetcomp, modItem.GetSetid, modItem.GetSetfyr, "", "", "", 0, "", "", "", "", "");
                    modFisCOATran.GetSetdebit = modItem.GetSetdebit;
                    modFisCOATran.GetSetcredit = modItem.GetSetcredit;
                    int k2 = oAccCon.updateFisCOATran(modFisCOATran);
                }
            }
            */

            int i = oAccCon.updateFisCOATran(sCurrComp, sCurrFyr);
        }
        //get summary FIS COA Debit Credit
        lsFisCOATran = oAccCon.getFisTOPCOATranList(sCurrComp, sCurrFyr, "", "", "", 0, "", "", "", "", "", "");
        for(int x=0; x< lsFisCOATran.Count; x++)
        {
            AccountingModel modItem = (AccountingModel)lsFisCOATran[x];
            if (modItem.GetSetacctype.Equals("A"))
            {
                jumaset = modItem.GetSetdebit - modItem.GetSetcredit;
            }
            else if (modItem.GetSetacctype.Equals("E"))
            {
                jumekuiti = modItem.GetSetcredit - modItem.GetSetdebit;
            }
            else if (modItem.GetSetacctype.Equals("L"))
            {
                jumliabiliti = modItem.GetSetcredit - modItem.GetSetdebit;
            }
            else if (modItem.GetSetacctype.Equals("H"))
            {
                jumhasil = modItem.GetSetcredit - modItem.GetSetdebit;
            }
            else if (modItem.GetSetacctype.Equals("B"))
            {
                jumbelanja = modItem.GetSetdebit - modItem.GetSetcredit;
            }
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