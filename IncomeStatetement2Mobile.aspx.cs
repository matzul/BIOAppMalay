using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IncomeStatetement2Mobile : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public UserProfileModel modUserProfile = new UserProfileModel();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sAction = "";
    public String sStockStateNo = "";
    public String sAlertMessage = "";
    
    public ArrayList lsRevenueHeader = new ArrayList();
    public double totalrevenueamount = 0;
    public ArrayList lsExpensesHeader = new ArrayList();
    public double totalexpensesamount = 0;
    public ArrayList lsStockTransHeader = new ArrayList();
    public double totalinventoryamount = 0;
    public double totalopeningstockamount = 0;
    public double totaladditionstockamount = 0;
    public double totalclosingstockamount = 0;
    public ArrayList lsStockSOHHeader = new ArrayList();

    public ArrayList lsStockOpeningSOH = new ArrayList();
    public ArrayList lsStockAdditionSOH = new ArrayList();
    public ArrayList lsStockClosingSOH = new ArrayList();

    public double totalstatementamount = 0;

    public String sDateFrom = "";
    public String sDateTo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }

    private DateTime FirstDayOfMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }

    private void initialValues()
    {
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
        }

        if (sUserId.Trim().Length == 0)
        {
            Response.Redirect("ExpiredPage.aspx");
        }

        if (Request.QueryString["action"] != null)
        {
            sAction = Request.QueryString["action"].ToString();
        }
        if (Request.QueryString["stockstateno"] != null)
        {
            sStockStateNo = Request.QueryString["stockstateno"].ToString();
        }
        if (Request.QueryString["alertmessage"] != null)
        {
            sAlertMessage = Request.QueryString["alertmessage"].ToString();
        }
        sDateFrom = FirstDayOfMonth().ToString("dd-MM-yyyy 00:00:00");
        sDateTo = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }

    private void getValues()
    {
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (sUserId.Trim().Length > 0)
        {
            modUserProfile = oMainCon.getUserProfile("", sUserId, "", "");
            sCurrComp = modUserProfile.GetSetcomp;
            sUserId = modUserProfile.GetSetuserid;
        }

        if (sUserId.Trim().Length == 0)
        {
            Response.Redirect("ExpiredPage.aspx");
        }

        if (Request.Params.Get("hidAction") != null)
        {
            sAction = oMainCon.replaceNull(Request.Params.Get("hidAction"));
        }
        if (Request.Params.Get("hidStockStateNo") != null)
        {
            sStockStateNo = oMainCon.replaceNull(Request.Params.Get("hidStockStateNo"));
        }
        if (Request.Params.Get("datefrom") != null)
        {
            sDateFrom = oMainCon.replaceNull(Request.Params.Get("datefrom"));
        }
        if (Request.Params.Get("dateto") != null)
        {
            sDateTo = oMainCon.replaceNull(Request.Params.Get("dateto"));
        }

        if (sDateFrom.Trim().Length == 0)
            sDateFrom = FirstDayOfMonth().ToString("dd-MM-yyyy 00:00:00");

        if (sDateTo.Trim().Length == 0)
            sDateTo = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

    }

    private void processValues()
    {
        if (oMainCon.compareTwoDateTime(sDateFrom, sDateTo) > 0)
        {
            if (sAction.Equals("OPEN") || sAction.Equals(""))
            {
                String additionalquery = "";

                //ArrayList invoicecat = new ArrayList();
                //invoicecat.Add("SALES_INVOICE");
                //invoicecat.Add("RECEIPT_VOUCHER");
                //ArrayList invoicetype = new ArrayList();
                //invoicetype.Add("OTHER_INCOME");
                additionalquery = " and  (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat = 'RECEIPT_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME') or (invoice_header.invoicecat = 'JOURNAL_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME')) ";
                lsRevenueHeader = oMainCon.getInvoiceHeaderListSum(sCurrComp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");

                //ArrayList expensescat = new ArrayList();
                //expensescat.Add("PURCHASE_INVOICE");
                //expensescat.Add("PAYMENT_VOUCHER");
                ArrayList expensestype = new ArrayList();
                expensestype.Add("SUPPLY_EXPENSES");
                expensestype.Add("SALARIES_WAGES");
                expensestype.Add("TRAVEL_EXPENSES");
                expensestype.Add("ENTERTAINMENT_EXPENSES");
                expensestype.Add("MARKETING_ADVERTISING");
                expensestype.Add("RENTAL_LEASING");
                expensestype.Add("REPAIR_MAINTENANCE");
                expensestype.Add("DEPRECIATION_EXPENSES");
                expensestype.Add("BAD_DEBT_EXPENSES");
                expensestype.Add("SUBSCRIPTION_REGISTRATION");
                expensestype.Add("INSURANCE_SECURITY");
                expensestype.Add("PROFESSIONAL_STATUTORY");
                expensestype.Add("BILL_UTILITIES");
                expensestype.Add("TAXATION");
                expensestype.Add("SELLING_SERVICES");
                expensestype.Add("OTHER_EXPENSES");
                additionalquery = " and  (expenses_header.expensescat = 'PURCHASE_INVOICE' or expenses_header.expensescat = 'TRANSFER_INVOICE' or (expenses_header.expensescat in ('PAYMENT_VOUCHER','JOURNAL_VOUCHER') ";
                String exptyp = "";
                for (int i = 0; i < expensestype.Count; i++)
                {
                    String str = (String)expensestype[i];
                    if (i.Equals(0))
                    {
                        exptyp = "'" + str + "'";
                    }
                    else
                    {
                        exptyp = exptyp + ",'" + str + "'";
                    }
                }
                additionalquery = additionalquery + " and  expenses_header.expensestype in (" + exptyp + "))) ";
                additionalquery = additionalquery + @"  and  NOT EXISTS (select item.itemno from item, expenses_details 
                                                        where expenses_header.expensesno = expenses_details.expensesno and expenses_header.comp = expenses_details.comp and expenses_header.expensescat = 'PURCHASE_INVOICE'
                                                        and expenses_details.itemno = item.itemno and expenses_details.comp = item.comp
                                                        and item.itemcat = 'INVENTORY')";
                lsExpensesHeader = oMainCon.getExpensesHeaderListSum(sCurrComp, "", "", sDateFrom, sDateTo, additionalquery, "CONFIRMED");

                MainModel modLastStockState = oMainCon.getStockStateLastHeaderDetails(sCurrComp, sDateFrom, "CLOSED");

                var lsStockLastSOHList = oMainCon.getStockStateLastSOHList(sCurrComp, "", (modLastStockState.GetSetclosingdate.Trim().Length > 0 ? modLastStockState.GetSetclosingdate : sDateFrom), "", "", "", "CLOSED");

                var lsStockInitialSOHList = oMainCon.getItemStockTransactionsListing(sCurrComp, "", "", "", modLastStockState.GetSetclosingdate, sDateFrom, "");

                List<MainModel> lsOpeningStockTransListing = new List<MainModel>();

                foreach (var item in lsStockLastSOHList)
                {
                    MainModel modItem = new MainModel();
                    modItem.GetSetcomp = item.GetSetcomp;
                    modItem.GetSetstockstateno = item.GetSetstockstateno;
                    modItem.GetSetopeningdate = item.GetSetopeningdate;
                    modItem.GetSetopeningtype = item.GetSetopeningtype;
                    modItem.GetSetstockopeningamount = item.GetSetstockopeningamount;
                    modItem.GetSetstockinamount = item.GetSetstockinamount;
                    modItem.GetSetstockoutamount = item.GetSetstockoutamount;
                    modItem.GetSetstockclosingamount = item.GetSetstockclosingamount;
                    modItem.GetSetclosingdate = item.GetSetclosingdate;
                    modItem.GetSetclosingtype = item.GetSetclosingtype;
                    modItem.GetSetremarks = item.GetSetremarks;
                    modItem.GetSetstatus = item.GetSetstatus;
                    modItem.GetSetitemno = item.GetSetitemno;
                    modItem.GetSetitemdesc = item.GetSetitemdesc;
                    modItem.GetSetlocation = item.GetSetlocation;
                    modItem.GetSetdatesoh = item.GetSetdatesoh;
                    modItem.GetSetqtysoh = item.GetSetqtysoh;
                    modItem.GetSetcostsoh = item.GetSetcostsoh;
                    foreach (var itemtrans in lsStockInitialSOHList)
                    {
                        if(itemtrans.GetSetcomp.Equals(item.GetSetcomp) &&
                           itemtrans.GetSetitemno.Equals(item.GetSetitemno) &&
                           itemtrans.GetSetlocation.Equals(item.GetSetlocation) &&
                           itemtrans.GetSetdatesoh.Equals(item.GetSetdatesoh))
                        {
                            modItem.GetSetqtysoh = itemtrans.GetSetqtysoh;
                            modItem.GetSetcostsoh = itemtrans.GetSetcostsoh;
                        }

                    }

                    lsOpeningStockTransListing.Add(modItem);
                    
                }
                
                var lsStockTransSOHList = oMainCon.getItemStockTransactionsListing(sCurrComp, "", "", "", sDateFrom, sDateTo, "");

                List<MainModel> lsClosingStockTransListing = new List<MainModel>();
                List<MainModel> lsAdditionStockTransListing = new List<MainModel>();

                foreach (var item in lsOpeningStockTransListing)
                {
                    MainModel modItem = new MainModel();
                    modItem.GetSetcomp = item.GetSetcomp;
                    modItem.GetSetstockstateno = item.GetSetstockstateno;
                    modItem.GetSetopeningdate = item.GetSetopeningdate;
                    modItem.GetSetopeningtype = item.GetSetopeningtype;
                    modItem.GetSetstockopeningamount = item.GetSetstockopeningamount;
                    modItem.GetSetstockinamount = item.GetSetstockinamount;
                    modItem.GetSetstockoutamount = item.GetSetstockoutamount;
                    modItem.GetSetstockclosingamount = item.GetSetstockclosingamount;
                    modItem.GetSetclosingdate = item.GetSetclosingdate;
                    modItem.GetSetclosingtype = item.GetSetclosingtype;
                    modItem.GetSetremarks = item.GetSetremarks;
                    modItem.GetSetstatus = item.GetSetstatus;
                    modItem.GetSetitemno = item.GetSetitemno;
                    modItem.GetSetitemdesc = item.GetSetitemdesc;
                    modItem.GetSetlocation = item.GetSetlocation;
                    modItem.GetSetdatesoh = item.GetSetdatesoh;
                    modItem.GetSetqtysoh = 0;
                    modItem.GetSetcostsoh = 0;

                    MainModel modItemClose = new MainModel();
                    modItemClose.GetSetcomp = item.GetSetcomp;
                    modItemClose.GetSetstockstateno = item.GetSetstockstateno;
                    modItemClose.GetSetopeningdate = item.GetSetopeningdate;
                    modItemClose.GetSetopeningtype = item.GetSetopeningtype;
                    modItemClose.GetSetstockopeningamount = item.GetSetstockopeningamount;
                    modItemClose.GetSetstockinamount = item.GetSetstockinamount;
                    modItemClose.GetSetstockoutamount = item.GetSetstockoutamount;
                    modItemClose.GetSetstockclosingamount = item.GetSetstockclosingamount;
                    modItemClose.GetSetclosingdate = item.GetSetclosingdate;
                    modItemClose.GetSetclosingtype = item.GetSetclosingtype;
                    modItemClose.GetSetremarks = item.GetSetremarks;
                    modItemClose.GetSetstatus = item.GetSetstatus;
                    modItemClose.GetSetitemno = item.GetSetitemno;
                    modItemClose.GetSetitemdesc = item.GetSetitemdesc;
                    modItemClose.GetSetlocation = item.GetSetlocation;
                    modItemClose.GetSetdatesoh = item.GetSetdatesoh;
                    modItemClose.GetSetqtysoh = item.GetSetqtysoh;
                    modItemClose.GetSetcostsoh = item.GetSetcostsoh;

                    foreach (var itemtrans in lsStockTransSOHList)
                    {
                        if (itemtrans.GetSetcomp.Equals(item.GetSetcomp) &&
                           itemtrans.GetSetitemno.Equals(item.GetSetitemno) &&
                           itemtrans.GetSetlocation.Equals(item.GetSetlocation) &&
                           itemtrans.GetSetdatesoh.Equals(item.GetSetdatesoh))
                        {
                            if(itemtrans.GetSettransqty > 0)
                            {
                                modItem.GetSetqtysoh = modItem.GetSetqtysoh + itemtrans.GetSettransqty;
                                //modItem.GetSetcostsoh = modItem.GetSetcostsoh + ((itemtrans.GetSetcostsoh/ itemtrans.GetSetqtysoh) * itemtrans.GetSettransqty);
                                modItem.GetSetcostsoh = modItem.GetSetcostsoh + (itemtrans.GetSettransprice * itemtrans.GetSettransqty);
                            }
                            modItemClose.GetSetqtysoh = itemtrans.GetSetqtysoh;
                            modItemClose.GetSetcostsoh = itemtrans.GetSetcostsoh;

                        }

                    }
                    lsAdditionStockTransListing.Add(modItem);
                    lsClosingStockTransListing.Add(modItemClose);

                }
                lsStockOpeningSOH = new ArrayList(lsOpeningStockTransListing);
                lsStockAdditionSOH = new ArrayList(lsAdditionStockTransListing);
                lsStockClosingSOH = new ArrayList(lsClosingStockTransListing);

                
                foreach (var item in lsOpeningStockTransListing)
                {
                    //oMainCon.WriteToLogFile("Opening Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                    totalopeningstockamount = totalopeningstockamount + item.GetSetcostsoh;
                }

                foreach (var item in lsAdditionStockTransListing)
                {
                    //oMainCon.WriteToLogFile("Addition Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                    totaladditionstockamount = totaladditionstockamount + item.GetSetcostsoh;
                }

                foreach (var item in lsClosingStockTransListing)
                {
                    //oMainCon.WriteToLogFile("Closing Stock: " + item.GetSetcomp + "~" + item.GetSetitemno + "~" + item.GetSetlocation + "~" + item.GetSetdatesoh + "~" + item.GetSetqtysoh + "~" + item.GetSetcostsoh);
                    totalclosingstockamount = totalclosingstockamount + item.GetSetcostsoh;
                }

                /*
                var lsStockTransListing = oMainCon.getItemStockTransactionsListing(sCurrComp, "", "", "", sDateFrom, sDateTo, "IN");

                var lsInStockTransListing = lsStockTransListing.GroupBy(item => new
                {
                    item.GetSetcomp,
                    item.GetSettransdate,
                    item.GetSettranstype,
                    item.GetSettransno,
                    item.GetSetorderno,
                    item.GetSetadjustmenttype
                }).Select(newItem => new MainModel
                {
                    GetSetcomp = newItem.Key.GetSetcomp,
                    GetSettransdate = newItem.Key.GetSettransdate,
                    GetSettranstype = newItem.Key.GetSettranstype,
                    GetSettransno = newItem.Key.GetSettransno,
                    GetSetorderno = newItem.Key.GetSetorderno,
                    GetSetadjustmenttype = newItem.Key.GetSetadjustmenttype,
                    GetSettotalamount = newItem.Sum(x => x.GetSettransprice * x.GetSettransqty)
                }).ToArray();
                lsStockTransHeader = new ArrayList(lsInStockTransListing);

                lsStockTransListing = oMainCon.getItemStockTransactionsListing(sCurrComp, "", "", "", sDateFrom, sDateTo, "OUT");
                var lsOutStockTransListing = lsStockTransListing.GroupBy(item => new
                {
                    item.GetSetcomp,
                    item.GetSettransdate,
                    item.GetSettranstype,
                    item.GetSettransno,
                    item.GetSetorderno,
                    item.GetSetadjustmenttype
                }).Select(newItem => new MainModel
                {
                    GetSetcomp = newItem.Key.GetSetcomp,
                    GetSettransdate = newItem.Key.GetSettransdate,
                    GetSettranstype = newItem.Key.GetSettranstype,
                    GetSettransno = newItem.Key.GetSettransno,
                    GetSetorderno = newItem.Key.GetSetorderno,
                    GetSetadjustmenttype = newItem.Key.GetSetadjustmenttype,
                    GetSettotalamount = newItem.Sum(x => x.GetSettransprice * x.GetSettransqty)
                }).ToArray();
                lsStockTransHeader = new ArrayList(lsOutStockTransListing);
                */
            }
        }
        else
        {
            lsRevenueHeader = new ArrayList();
            lsStockTransHeader = new ArrayList();
            lsExpensesHeader = new ArrayList();
            sAlertMessage = "ERROR|The Date To must later than Date From...";
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