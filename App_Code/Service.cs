using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    private static Dictionary<Guid, ToDoItem> _todos = new Dictionary<Guid, ToDoItem>();
    private MainController oMainCon = new MainController();

    public List<ToDoItem> GetToDos()
    {
        return _todos.Values.ToList();
    }

    public void AddOrUpdateToDo(ToDoItem toDoItem)
    {
        _todos[toDoItem.Id] = toDoItem;
    }

    public void DeleteToDo(ToDoItem toDoItem)
    {
        if (_todos.ContainsKey(toDoItem.Id))
            _todos.Remove(toDoItem.Id);
        else
            throw new FaultException("ID not found: " + toDoItem.Id);
    }

    public string getCurrentDateFormatDDMMYYYY()
    {
        return DateTime.Now.ToString("dd-MM-yyyy");
    }

    public List<MainModel> getOrderItemDiscountList(String comp, String ordercat, String ordertype, String itemno)
    {
        return oMainCon.getItemDiscountList(comp, ordercat, ordertype, itemno).Cast<MainModel>().ToList<MainModel>();
    }

    public List<CounterModel> getCounterMasterList(String comp, String status)
    {
        return oMainCon.getCounterMasterList(comp, status).Cast<CounterModel>().ToList<CounterModel>();
    }

    public CounterModel getCounterMasterDetails(String comp, String status, String counterno, String countertranid)
    {
        return oMainCon.getCounterMasterDetails(comp, status, counterno, countertranid);
    }

    public String insertCounterMaster(CounterModel oModCounterMaster)
    {
        return oMainCon.insertCounterMaster(oModCounterMaster);
    }

    public String updateCounterMaster(CounterModel oModCounterMaster)
    {
        return oMainCon.updateCounterMaster(oModCounterMaster);
    }

    public List<CounterModel> getCounterTransList(String comp, String counterno, String id, String userid, String status)
    {
        return oMainCon.getCounterTransList(comp, counterno, id, userid, status).Cast<CounterModel>().ToList<CounterModel>();
    }

    public CounterModel getCounterTrans(String comp, String counterno, String id, String userid, String status)
    {
        return oMainCon.getCounterTrans(comp, counterno, id, userid, status);
    }

    public String insertCounterTrans(CounterModel oModCounterTrans)
    {
        return oMainCon.insertCounterTrans(oModCounterTrans);
    }

    public String updateCounterTrans(CounterModel oModCounterTrans)
    {
        return oMainCon.updateCounterTrans(oModCounterTrans);
    }

    public List<MainModel> getCounterTransDetailsList(String sComp, String sCounterNo, String sCounterTransId, String sOrderNo, String sInvoiceNo, String sRowInclude)
    {
        return oMainCon.getCounterTransDetailsList(sComp, sCounterNo, sCounterTransId, sOrderNo, sInvoiceNo, sRowInclude).Cast<MainModel>().ToList<MainModel>();
    }

    public MainModel getCounterTransDetails(String sComp, String sCounterNo, String sCounterTransId, String sOrderNo)
    {
        return oMainCon.getCounterTransDetails(sComp, sCounterNo, sCounterTransId, sOrderNo);
    }

    public UserProfileModel getUserProfile(String comp, String userid)
    {
        return oMainCon.getUserProfile(comp, userid, "", "");
    }

    public String getNextRunningNo(String comp, String type, String status)
    {
        return oMainCon.getNextRunningNo(comp, type, status);
    }

    public void updateNextRunningNo(String comp, String type, String status)
    {
        oMainCon.updateNextRunningNo(comp, type, status);
    }

    public List<MainModel> getOrderHeaderList(String comp, String orderno, String bpid, String startdate, String enddate, String status)
    {
        return oMainCon.getOrderHeaderList(comp, orderno, "", bpid, startdate, enddate, status).Cast<MainModel>().ToList<MainModel>();
    }

    public MainModel getOrderHeaderDetails(String comp, String orderno)
    {
        return oMainCon.getOrderHeaderDetails(comp, orderno);
    }

    //public String insertOrderHeader(MainModel oModOrder)
    public String insertOrderHeader(MainModel oModOrder, string counterno, string countertranid)
    {
        String strResult = "N";
        strResult = oMainCon.insertOrderHeader(oModOrder);
        //next insert into counter_transaction_details
        if (strResult.Equals("Y"))
        {
            MainModel oModOrderTemp = oMainCon.getOrderHeaderDetails(oModOrder.GetSetcomp, oModOrder.GetSetorderno);
            MainModel oModCounTransDet = new MainModel();

            oModCounTransDet.GetSetcomp = oModOrderTemp.GetSetcomp;
            oModCounTransDet.GetSetcounterno = counterno;
            oModCounTransDet.GetSetcountertranid = countertranid;
            oModCounTransDet.GetSetbpid = oModOrderTemp.GetSetbpid;
            oModCounTransDet.GetSetbpdesc = oModOrderTemp.GetSetbpdesc;
            oModCounTransDet.GetSetorderno = oModOrderTemp.GetSetorderno;
            oModCounTransDet.GetSetorderdate = oModOrderTemp.GetSetorderdate;

            oModCounTransDet.GetSetorderamount = oModOrderTemp.GetSetsalesamount;
            oModCounTransDet.GetSetdiscamount = oModOrderTemp.GetSetdiscamount;
            oModCounTransDet.GetSettotalamount = oModOrderTemp.GetSetorderamount;
            oModCounTransDet.GetSettaxamount = oModOrderTemp.GetSettaxamount;
            oModCounTransDet.GetSetsalesamount = oModOrderTemp.GetSettotalamount;
            oModCounTransDet.GetSetorderstatus = oModOrderTemp.GetSetorderstatus;

            strResult = oMainCon.insertCounterTransDetails(oModCounTransDet);
        }
        return strResult;
    }

    public String updateOrderHeader(MainModel oModOrder, string counterno, string id)
    {
        String strResult = "";
        strResult = oMainCon.updateOrderHeader(oModOrder);
        if (strResult.Equals("Y"))
        {
            CounterModel oModCounTrans = oMainCon.getCounterTrans(oModOrder.GetSetcomp, counterno, id, "", "");
            if (oModOrder.GetSetorderstatus.Equals("CONFIRMED"))
            {
                oModCounTrans.GetSettotalorderamount = oModCounTrans.GetSettotalorderamount + oModOrder.GetSettotalamount;

                MainModel oModCounTransDet = oMainCon.getCounterTransDetails(oModOrder.GetSetcomp, counterno, id, oModOrder.GetSetorderno);
                oModCounTransDet.GetSetorderstatus = oModOrder.GetSetorderstatus;
                oModCounTransDet.GetSetrowinclude = true;
                strResult = oMainCon.updateCounterTransDetails(oModCounTransDet);
            }
            strResult = oMainCon.updateCounterTrans(oModCounTrans);
        }
        return strResult;
    }

    public String updateOrderHeaderInfo(String comp, String orderno, string counterno, string countertranid)
    {
        String strResult = "";
        strResult = oMainCon.updateOrderHeaderInfo(comp, orderno);
        //next update counter_transaction_details
        if (strResult.Equals("Y"))
        {
            MainModel oModOrderTemp = oMainCon.getOrderHeaderDetails(comp, orderno);
            MainModel oModCounTransDet = oMainCon.getCounterTransDetails(comp, counterno, countertranid, orderno);

            oModCounTransDet.GetSetorderamount = oModOrderTemp.GetSetsalesamount;
            oModCounTransDet.GetSetdiscamount = oModOrderTemp.GetSetdiscamount;
            oModCounTransDet.GetSettotalamount = oModOrderTemp.GetSetorderamount;
            oModCounTransDet.GetSettaxamount = oModOrderTemp.GetSettaxamount;
            oModCounTransDet.GetSetsalesamount = oModOrderTemp.GetSettotalamount;
            oModCounTransDet.GetSetorderstatus = oModOrderTemp.GetSetorderstatus;

            strResult = oMainCon.updateCounterTransDetails(oModCounTransDet);
        }
        return strResult;
    }

    public List<MainModel> getOrderDetailsList(String comp, String orderno, int lineno, String itemno)
    {
        return oMainCon.getOrderDetailsList(comp, orderno, lineno, itemno).Cast<MainModel>().ToList<MainModel>();
    }

    public MainModel getOrderDetailsDetails(String comp, String orderno, int lineno, String itemno)
    {
        return oMainCon.getOrderDetailsDetails(comp, orderno, lineno, itemno);
    }

    public String insertOrderDetails(MainModel oModOrderDet)
    {
        oModOrderDet.GetSetorderprice = Math.Round(oModOrderDet.GetSetorderprice, 2, MidpointRounding.AwayFromZero);
        oModOrderDet.GetSettaxamount = Math.Round(oModOrderDet.GetSettaxamount, 2, MidpointRounding.AwayFromZero);
        //oModOrderDet.GetSettotalprice = Math.Round(oModOrderDet.GetSettotalprice, 2, MidpointRounding.AwayFromZero);
        oModOrderDet.GetSettotalprice = Math.Round(oModOrderDet.GetSetorderprice + oModOrderDet.GetSettaxamount, 2, MidpointRounding.AwayFromZero);
        return oMainCon.insertOrderDetails(oModOrderDet);
    }

    public String updateOrderDetails(MainModel oModOrderDet)
    {
        oModOrderDet.GetSetorderprice = Math.Round(oModOrderDet.GetSetorderprice, 2, MidpointRounding.AwayFromZero);
        oModOrderDet.GetSettaxamount = Math.Round(oModOrderDet.GetSettaxamount, 2, MidpointRounding.AwayFromZero);
        //oModOrderDet.GetSettotalprice = Math.Round(oModOrderDet.GetSettotalprice, 2, MidpointRounding.AwayFromZero);
        oModOrderDet.GetSettotalprice = Math.Round(oModOrderDet.GetSetorderprice + oModOrderDet.GetSettaxamount, 2, MidpointRounding.AwayFromZero);
        return oMainCon.updateOrderDetails(oModOrderDet);
    }

    public String deleteOrderDetails(MainModel oModOrderDet)
    {
        return oMainCon.deleteOrderDetails(oModOrderDet);
    }

    public MainModel getItemStockSummary2(String comp, String itemno)
    {
        return oMainCon.getItemStockSummary2(comp, itemno);
    }

    public String insertShipmentHeaderAndDetails(MainModel oModOrdHdr, string counterno, string countertranid)
    {
        String strResult = "";
        if (oModOrdHdr != null ? (oModOrdHdr.GetSetorderno != null ? (oModOrdHdr.GetSetorderno.Length > 0 && oModOrdHdr.GetSetorderstatus.Equals("NEW") ? true : false) : false) : false)
        {
            //insert data to shipment table
            MainModel oModShipment = new MainModel();
            oModShipment.GetSetcomp = oModOrdHdr.GetSetcomp;
            oModShipment.GetSetshipmentno = oMainCon.getNextRunningNo(oModOrdHdr.GetSetcomp, "SHIPMENT", "ACTIVE");
            oModShipment.GetSetshipmentcat = "NORMAL";
            oModShipment.GetSetbpid = oModOrdHdr.GetSetbpid;
            oModShipment.GetSetbpdesc = oModOrdHdr.GetSetbpdesc;
            oModShipment.GetSetbpaddress = oModOrdHdr.GetSetbpaddress;
            oModShipment.GetSetbpcontact = oModOrdHdr.GetSetbpcontact;
            oModShipment.GetSetremarks = "SHIPMENT CREATED BY POS COUNTER";
            oModShipment.GetSetstatus = "NEW";
            oModShipment.GetSetcreatedby = oModOrdHdr.GetSetordercreated;

            //oMainCon.WriteToLogFile("ShipmentNo1: " + oModShipment.GetSetshipmentno);
            if (oMainCon.insertShipmentHeader(oModShipment).Equals("Y"))
            {
                //oMainCon.WriteToLogFile("ShipmentNo2: " + oModShipment.GetSetshipmentno);
                oMainCon.updateNextRunningNo(oModOrdHdr.GetSetcomp, "SHIPMENT", "ACTIVE");

                //get latest information about Shipment Header ie. Confirmed Date which is needed for storing item stock transactions
                oModShipment = oMainCon.getShipmentHeaderDetails(oModShipment.GetSetcomp, oModShipment.GetSetshipmentno);

                MainModel oModCounTransDet = oMainCon.getCounterTransDetails(oModOrdHdr.GetSetcomp, counterno, countertranid, oModOrdHdr.GetSetorderno);
                oModCounTransDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                oModCounTransDet.GetSetshipmentdate = oModShipment.GetSetshipmentdate;
                oModCounTransDet.GetSetshipmentstatus = oModShipment.GetSetstatus;
                strResult = oMainCon.updateCounterTransDetails(oModCounTransDet);

                ArrayList lsOrderLineItem = oMainCon.getOrderDetailsList(oModOrdHdr.GetSetcomp, oModOrdHdr.GetSetorderno, 0, "");
                int shipment_lino = 1;
                for (int i = 0; i < lsOrderLineItem.Count; i++)
                {
                    MainModel oModOrderDet = (MainModel)lsOrderLineItem[i];

                    if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))
                    {
                        int stockRemaining = oModOrderDet.GetSetquantity;
                        ArrayList lsStockAvailable = oMainCon.getItemStockList(oModOrderDet.GetSetcomp, oModOrderDet.GetSetitemno, "", "", true);
                        for (int x = 0; x < lsStockAvailable.Count; x++)
                        {
                            MainModel stockAssigned = (MainModel)lsStockAvailable[x];

                            if (stockRemaining > 0)
                            {
                                int stockforthisloop = 0;
                                if (stockAssigned.GetSetqtyavailable >= stockRemaining)
                                {
                                    stockforthisloop = stockRemaining;
                                }
                                else
                                {
                                    stockforthisloop = stockAssigned.GetSetqtyavailable;
                                }

                                MainModel oModShipmentDet = new MainModel();

                                oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                                oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                                oModShipmentDet.GetSetlineno = shipment_lino;
                                oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                                oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                                oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                                oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                                oModShipmentDet.GetSetorder_quantity = stockRemaining;
                                oModShipmentDet.GetSetshipment_quantity = stockforthisloop;
                                oModShipmentDet.GetSetlocation = stockAssigned.GetSetlocation;
                                oModShipmentDet.GetSetdatesoh = stockAssigned.GetSetdatesoh;
                                oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                                oModShipmentDet.GetSethasinvoice = "N";

                                String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                                shipment_lino = shipment_lino + 1;

                                stockRemaining = stockRemaining - stockforthisloop;

                            }
                            else
                            {
                                x = lsStockAvailable.Count;
                            }
                        }//for (int x = 0; x < lsStockAvailable.Count; x++)
                    }
                    else
                    {
                        MainModel oModShipmentDet = new MainModel();

                        oModShipmentDet.GetSetcomp = oModOrderDet.GetSetcomp;
                        oModShipmentDet.GetSetshipmentno = oModShipment.GetSetshipmentno;
                        oModShipmentDet.GetSetlineno = shipment_lino;
                        oModShipmentDet.GetSetorderno = oModOrderDet.GetSetorderno;
                        oModShipmentDet.GetSetorder_lineno = oModOrderDet.GetSetlineno;
                        oModShipmentDet.GetSetitemno = oModOrderDet.GetSetitemno;
                        oModShipmentDet.GetSetitemdesc = oModOrderDet.GetSetitemdesc;
                        oModShipmentDet.GetSetorder_quantity = oModOrderDet.GetSetquantity;
                        oModShipmentDet.GetSetshipment_quantity = oModOrderDet.GetSetquantity;
                        oModShipmentDet.GetSetlocation = "";
                        oModShipmentDet.GetSetdatesoh = "";
                        oModShipmentDet.GetSetremarks = oModOrderDet.GetSetremarks;
                        oModShipmentDet.GetSethasinvoice = "N";

                        String result0 = oMainCon.insertShipmentDetails(oModShipmentDet);
                        shipment_lino = shipment_lino + 1;

                    }//if (oModOrderDet.GetSetitemcat.Equals("INVENTORY"))

                }
                strResult = "Y";
            }
            else
            {
                strResult = "N";
            }
        }
        else
        {
            strResult = "N";
        }
        return strResult;
    }

    public String insertInvoiceHeaderAndDetails(MainModel oModOrdHdr, string counterno, string countertranid)
    {
        String strResult = "";
        if (oModOrdHdr != null ? (oModOrdHdr.GetSetorderno != null ? (oModOrdHdr.GetSetorderno.Length > 0 && oModOrdHdr.GetSetorderstatus.Equals("NEW") ? true : false) : false) : false)
        {
            if(insertShipmentHeaderAndDetails(oModOrdHdr, counterno, countertranid).Equals("Y"))
            {
                oModOrdHdr.GetSetorderstatus = "CONFIRMED";
                oModOrdHdr.GetSetorderapproved = oModOrdHdr.GetSetordercreated;
                String str1 = updateOrderHeader(oModOrdHdr, counterno, countertranid);
            }
        }
        if (oModOrdHdr != null ? (oModOrdHdr.GetSetorderno != null ? (oModOrdHdr.GetSetorderno.Length > 0 && oModOrdHdr.GetSetorderstatus.Equals("CONFIRMED") ? true : false) : false) : false)
        {
            MainModel oModCounTransDet = oMainCon.getCounterTransDetails(oModOrdHdr.GetSetcomp, counterno, countertranid, oModOrdHdr.GetSetorderno);
            if (oModCounTransDet != null ? oModCounTransDet.GetSetshipmentno != null ? oModCounTransDet.GetSetshipmentno.Length > 0 && oModCounTransDet.GetSetshipmentstatus.Equals("NEW") ? true : false : false : false)
            {
                //get latest information about Shipment Header
                MainModel oModShipment = oMainCon.getShipmentHeaderDetails(oModCounTransDet.GetSetcomp, oModCounTransDet.GetSetshipmentno);

                //proceed create invoice automatically
                //insert invoice header - CONFIRM
                MainModel oModInvoice = new MainModel();
                oModInvoice.GetSetcomp = oModShipment.GetSetcomp;
                //let system define the invoice date
                //oModInvoice.GetSetinvoicedate = oModShipment.GetSetshipmentdate;
                oModInvoice.GetSetinvoicetype = "SALES_INVOICE";
                oModInvoice.GetSetinvoiceterm = "COD";
                oModInvoice.GetSetinvoiceno = oMainCon.getNextRunningNo(oModCounTransDet.GetSetcomp, "INVOICE", "ACTIVE");
                oModInvoice.GetSetbpid = oModShipment.GetSetbpid;
                oModInvoice.GetSetbpdesc = oModShipment.GetSetbpdesc;
                oModInvoice.GetSetbpaddress = oModShipment.GetSetbpaddress;
                oModInvoice.GetSetbpcontact = oModShipment.GetSetbpcontact;
                oModInvoice.GetSetremarks = "INVOICE CREATED BY SYSTEM";
                oModInvoice.GetSetstatus = "CONFIRMED";
                oModInvoice.GetSetcreatedby = oModShipment.GetSetcreatedby;
                oModInvoice.GetSetconfirmedby = oModShipment.GetSetcreatedby;

                if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))
                {
                    oMainCon.updateNextRunningNo(oModShipment.GetSetcomp, "INVOICE", "ACTIVE");

                    //update shipment header as CONFIRMED
                    oModShipment.GetSetstatus = "CONFIRMED";
                    oModShipment.GetSetconfirmedby = oModShipment.GetSetcreatedby;
                    String result6 = oMainCon.updateShipmentHeader(oModShipment);

                    //get latest information about Invoice Header
                    oModInvoice = oMainCon.getInvoiceHeaderDetails(oModShipment.GetSetcomp, oModInvoice.GetSetinvoiceno);

                    ArrayList lsPendInvLineItem = oMainCon.getLineItemPendingInvoice(oModInvoice.GetSetcomp, oModInvoice.GetSetbpid, "", oModInvoice.GetSetinvoicecat, oModInvoice.GetSetinvoicetype, oModShipment.GetSetshipmentno);

                    for (int y = 0; y < lsPendInvLineItem.Count; y++)
                    {
                        MainModel modPendInv = (MainModel)lsPendInvLineItem[y];

                        MainModel oModLineItem = new MainModel();
                        oModLineItem.GetSetcomp = modPendInv.GetSetcomp;
                        oModLineItem.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                        oModLineItem.GetSetlineno = y + 1;
                        oModLineItem.GetSetshipmentno = modPendInv.GetSetshipmentno;
                        oModLineItem.GetSetshipment_lineno = modPendInv.GetSetlineno;
                        oModLineItem.GetSetorderno = modPendInv.GetSetorderno;
                        oModLineItem.GetSetorder_lineno = modPendInv.GetSetorder_lineno;
                        oModLineItem.GetSetitemno = modPendInv.GetSetitemno;
                        oModLineItem.GetSetitemdesc = modPendInv.GetSetitemdesc;

                        oModLineItem.GetSetunitcost = modPendInv.GetSetunitcost;
                        oModLineItem.GetSetcostprice = modPendInv.GetSetcostprice;
                        oModLineItem.GetSetunitprice = modPendInv.GetSetunitprice;
                        oModLineItem.GetSetdiscamount = modPendInv.GetSetdiscamount;
                        oModLineItem.GetSetquantity = modPendInv.GetSetquantity;
                        oModLineItem.GetSetinvoiceprice = modPendInv.GetSetinvoiceprice;

                        oModLineItem.GetSettaxcode = modPendInv.GetSettaxcode;
                        oModLineItem.GetSettaxrate = modPendInv.GetSettaxrate;
                        oModLineItem.GetSettaxamount = modPendInv.GetSettaxamount;
                        oModLineItem.GetSettotalinvoice = modPendInv.GetSettotalinvoice;

                        String result0 = oMainCon.insertInvoiceDetails(oModLineItem);

                        //to update Sales Order Invoice Amount
                        MainModel oModOrderDet = oMainCon.getOrderDetailsDetails(modPendInv.GetSetcomp, modPendInv.GetSetorderno, modPendInv.GetSetorder_lineno, "");
                        oModOrderDet.GetSetinvoiceamount = oModOrderDet.GetSetinvoiceamount + modPendInv.GetSettotalinvoice;
                        oModOrderDet.GetSetdeliverqty = oModOrderDet.GetSetdeliverqty + modPendInv.GetSetquantity;
                        String result1 = oMainCon.updateOrderDetails(oModOrderDet);

                        //update status for shipment has invoice
                        MainModel oModShipmentDet = oMainCon.getShipmentDetailsDetails(modPendInv.GetSetcomp, modPendInv.GetSetshipmentno, modPendInv.GetSetlineno, "");

                        //to update item stock & stock transaction
                        MainModel oModLatestItemStock = oMainCon.getItemStockDetails(oModShipmentDet.GetSetcomp, oModShipmentDet.GetSetitemno, oModShipmentDet.GetSetlocation, oModShipmentDet.GetSetdatesoh);
                        if (oModLatestItemStock.GetSetitemcat.Equals("INVENTORY"))
                        {
                            MainModel oModItemStock = new MainModel();
                            oModItemStock.GetSetcomp = oModShipmentDet.GetSetcomp;
                            oModItemStock.GetSetitemno = oModShipmentDet.GetSetitemno;
                            oModItemStock.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                            oModItemStock.GetSetlocation = oModShipmentDet.GetSetlocation;
                            oModItemStock.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                            oModItemStock.GetSetqtysoh = oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity;
                            oModItemStock.GetSetcostsoh = (oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh) * (oModLatestItemStock.GetSetqtysoh - oModShipmentDet.GetSetshipment_quantity);
                            if (oMainCon.getItemStockList(oModItemStock.GetSetcomp, oModItemStock.GetSetitemno, oModItemStock.GetSetlocation, oModItemStock.GetSetdatesoh, true).Count > 0)
                            {
                                String result2 = oMainCon.updateItemStock(oModItemStock);
                            }

                            MainModel oModItemStockTrans = new MainModel();
                            oModItemStockTrans.GetSetcomp = oModShipmentDet.GetSetcomp;
                            oModItemStockTrans.GetSetitemno = oModShipmentDet.GetSetitemno;
                            oModItemStockTrans.GetSetitemdesc = oModShipmentDet.GetSetitemdesc;
                            oModItemStockTrans.GetSetlocation = oModShipmentDet.GetSetlocation;
                            oModItemStockTrans.GetSetdatesoh = oModShipmentDet.GetSetdatesoh;
                            oModItemStockTrans.GetSettranstype = "SHIPMENT";
                            oModItemStockTrans.GetSettransdate = oModShipment.GetSetconfirmeddate;
                            oModItemStockTrans.GetSettransno = oModShipmentDet.GetSetshipmentno;
                            oModItemStockTrans.GetSettrans_lineno = oModShipmentDet.GetSetlineno;
                            oModItemStockTrans.GetSetorderno = oModShipmentDet.GetSetorderno;
                            oModItemStockTrans.GetSetorder_lineno = oModShipmentDet.GetSetorder_lineno;
                            oModItemStockTrans.GetSettransqty = oModShipmentDet.GetSetshipment_quantity * -1;
                            oModItemStockTrans.GetSettransprice = oModLatestItemStock.GetSetcostsoh / oModLatestItemStock.GetSetqtysoh;
                            oModItemStockTrans.GetSetqtysoh = oModItemStock.GetSetqtysoh;
                            oModItemStockTrans.GetSetcostsoh = oModItemStock.GetSetcostsoh;
                            String result3 = oMainCon.insertItemStockTransactions(oModItemStockTrans);
                        }

                        oModShipmentDet.GetSethasinvoice = "Y";
                        String result4 = oMainCon.updateShipmentDetails(oModShipmentDet);

                    }//for(int y=0; y <lsShipmentLineItem.Count; y++){                                
                     //update order header information
                    String result5 = oMainCon.updateInvoiceHeaderInfo(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno);

                    //update CounterTransDetails
                    //get latest info invoice header
                    oModInvoice = oMainCon.getInvoiceHeaderDetails(oModInvoice.GetSetcomp, oModInvoice.GetSetinvoiceno);
                    oModCounTransDet.GetSetshipmentstatus = oModShipment.GetSetstatus;
                    oModCounTransDet.GetSetinvoiceno = oModInvoice.GetSetinvoiceno;
                    oModCounTransDet.GetSetinvoicedate = oModInvoice.GetSetinvoicedate;
                    oModCounTransDet.GetSetinvoiceamount = oModInvoice.GetSettotalamount;
                    oModCounTransDet.GetSetinvoicestatus = oModInvoice.GetSetstatus;
                    String result7 = oMainCon.updateCounterTransDetails(oModCounTransDet);

                }//if (oMainCon.insertInvoiceHeader(oModInvoice).Equals("Y"))

                CounterModel oModCounTrans = oMainCon.getCounterTrans(oModInvoice.GetSetcomp, counterno, countertranid, "", "");
                if (oModInvoice.GetSetstatus.Equals("CONFIRMED"))
                {
                    oModCounTrans.GetSettotalinvoiceamount = oModCounTrans.GetSettotalinvoiceamount + oModInvoice.GetSettotalamount;
                    strResult = oMainCon.updateCounterTrans(oModCounTrans);
                }
                else
                {
                    strResult = "Y";
                }
            }
            else if (oModCounTransDet != null ? oModCounTransDet.GetSetshipmentno != null ? oModCounTransDet.GetSetshipmentno.Length > 0 && oModCounTransDet.GetSetshipmentstatus.Equals("CONFIRMED") ? (oModCounTransDet.GetSetinvoiceno != null ? oModCounTransDet.GetSetinvoiceno.Length > 0 && oModCounTransDet.GetSetinvoicestatus.Equals("CONFIRMED") ? true : false : false) : false : false : false)
            {
                //already confirm shipment, create invoice & confirm invoice. can proceed to make a payment receipt
                strResult = "Y";
            }
            else
            {
                strResult = "N";
            }
        }
        else
        {
            strResult = "N";
        }
        return strResult;
    }

    public String insertPaymentReceiptHeaderAndDetails(MainModel oModOrdHdr, MainModel oModCounTransDet)
    {
        String strResult = "";
        if (oModOrdHdr != null ? (oModOrdHdr.GetSetorderno != null ? (oModOrdHdr.GetSetorderno.Length > 0 && oModOrdHdr.GetSetorderstatus.Equals("CONFIRMED") ? true : false) : false) : false)
        {
            //MainModel oModCounTransDet = oMainCon.getCounterTransDetails(oModOrdHdr.GetSetcomp, counterno, countertranid, oModOrdHdr.GetSetorderno);
            if (oModCounTransDet != null ? oModCounTransDet.GetSetinvoiceno != null ? oModCounTransDet.GetSetinvoiceno.Length > 0 && oModCounTransDet.GetSetinvoicestatus.Equals("CONFIRMED") ? true : false : false : false)
            {
                if (oModCounTransDet != null ? oModCounTransDet.GetSetpayrcptno != null ? oModCounTransDet.GetSetpayrcptno.Length > 0 && oModCounTransDet.GetSetpayrcptstatus.Equals("CONFIRMED") ? true : false : false : false)
                {
                    strResult = "Y";
                }
                else
                {
                    //get latest information about Invoice Header
                    MainModel oModInvoice = oMainCon.getInvoiceHeaderDetails(oModOrdHdr.GetSetcomp, oModCounTransDet.GetSetinvoiceno);

                    MainModel oModPayRcpt = new MainModel();
                    oModPayRcpt.GetSetcomp = oModInvoice.GetSetcomp;
                    //let system define the invoice date
                    //oModPayRcpt.GetSetpayrcptdate = oMainCon.replaceNull(Request.Params.Get("payrcptdate"));
                    oModPayRcpt.GetSetpayrcpttype = "INVOICE";
                    oModPayRcpt.GetSetpayrcptno = oMainCon.getNextRunningNo(oModPayRcpt.GetSetcomp, "PAYMENT_RECEIPT", "ACTIVE");
                    oModPayRcpt.GetSetbpid = oModInvoice.GetSetbpid;
                    oModPayRcpt.GetSetbpdesc = oModInvoice.GetSetbpdesc;
                    oModPayRcpt.GetSetbpaddress = oModInvoice.GetSetbpaddress;
                    oModPayRcpt.GetSetbpcontact = oModInvoice.GetSetbpcontact;
                    oModPayRcpt.GetSetremarks = "PAYMENT RECEIPT CREATED BY SYSTEM"; ;
                    oModPayRcpt.GetSetstatus = "CONFIRMED";
                    oModPayRcpt.GetSetcreatedby = oModInvoice.GetSetcreatedby;
                    oModPayRcpt.GetSetconfirmedby = oModInvoice.GetSetcreatedby;

                    if (oMainCon.insertPaymentReceiptHeader(oModPayRcpt).Equals("Y"))
                    {
                        oMainCon.updateNextRunningNo(oModPayRcpt.GetSetcomp, "PAYMENT_RECEIPT", "ACTIVE");

                        MainModel oModLineItem = new MainModel();
                        oModLineItem.GetSetcomp = oModPayRcpt.GetSetcomp;
                        oModLineItem.GetSetpayrcptno = oModPayRcpt.GetSetpayrcptno;
                        oModLineItem.GetSetlineno = 1;
                        oModLineItem.GetSetinvoiceno = oModCounTransDet.GetSetinvoiceno;
                        oModLineItem.GetSetinvoicedate = oModCounTransDet.GetSetinvoicedate;
                        oModLineItem.GetSetinvoiceprice = oModCounTransDet.GetSetinvoiceamount;
                        oModLineItem.GetSetpaytype = oModOrdHdr.GetSetpaytype;
                        oModLineItem.GetSetpayrefno = oModCounTransDet.GetSetcounterno;
                        oModLineItem.GetSetpayrcptprice = oModCounTransDet.GetSetpayrcptamount;
                        oModLineItem.GetSetpayremarks = oModPayRcpt.GetSetremarks;

                        //insert new line item
                        String result2 = oMainCon.insertPaymentReceiptDetails(oModLineItem);

                        /*
                        ArrayList lsPendPayRcptMod = oMainCon.getLineItemPendingPaymentReceipt(oModInvoice.GetSetcomp, oModInvoice.GetSetbpid, oModInvoice.GetSetbpdesc, oModInvoice.GetSetinvoiceno);
                        for (int y = 0; y < lsPendPayRcptMod.Count; y++)
                        {
                            MainModel modPendPayment = (MainModel)lsPendPayRcptMod[y];

                            MainModel oModLineItem = new MainModel();
                            oModLineItem.GetSetcomp = oModPayRcpt.GetSetcomp;
                            oModLineItem.GetSetpayrcptno = oModPayRcpt.GetSetpayrcptno;
                            oModLineItem.GetSetlineno = y + 1;
                            oModLineItem.GetSetinvoiceno = modPendPayment.GetSetinvoiceno;
                            oModLineItem.GetSetinvoicedate = modPendPayment.GetSetinvoicedate;
                            oModLineItem.GetSetinvoiceprice = modPendPayment.GetSettotalamount - modPendPayment.GetSetpayrcptamount;
                            oModLineItem.GetSetpaytype = oModPayRcpt.GetSetpayrcpttype;
                            oModLineItem.GetSetpayrefno = oModCounTransDet.GetSetcounterno;
                            oModLineItem.GetSetpayrcptprice = oModCounTransDet.GetSetpayrcptamount;
                            oModLineItem.GetSetpayremarks = oModPayRcpt.GetSetremarks;

                            //insert new line item
                            String result2 = oMainCon.insertPaymentReceiptDetails(oModLineItem);

                        }
                        */

                        //update payment receipt header information
                        String result3 = oMainCon.updatePaymentReceiptHeaderInfo(oModPayRcpt.GetSetcomp, oModPayRcpt.GetSetpayrcptno);

                        //update CounterTransDetails
                        //get latest info invoice header
                        oModPayRcpt = oMainCon.getPaymentReceiptHeaderDetails(oModPayRcpt.GetSetcomp, oModPayRcpt.GetSetpayrcptno);
                        oModCounTransDet.GetSetpayrcptno = oModPayRcpt.GetSetpayrcptno;
                        oModCounTransDet.GetSetpayrcptdate = oModPayRcpt.GetSetpayrcptdate;
                        //oModCounTransDet.GetSetpayrcptamount = oModPayRcpt.GetSetpayrcptamount;
                        oModCounTransDet.GetSetpayrcptstatus = oModPayRcpt.GetSetstatus;
                        String result4 = oMainCon.updateCounterTransDetails(oModCounTransDet);

                    }

                    CounterModel oModCounTrans = oMainCon.getCounterTrans(oModCounTransDet.GetSetcomp, oModCounTransDet.GetSetcounterno, oModCounTransDet.GetSetcountertranid, "", "");
                    if (oModPayRcpt.GetSetstatus.Equals("CONFIRMED"))
                    {
                        oModCounTrans.GetSettotalpayrcptamount = oModCounTrans.GetSettotalpayrcptamount + oModPayRcpt.GetSetpayrcptamount;
                        oModCounTrans.GetSetclosingbalance = oModCounTrans.GetSetopeningbalance + oModCounTrans.GetSettotalpayrcptamount;
                        strResult = oMainCon.updateCounterTrans(oModCounTrans);
                    }
                    else
                    {
                        strResult = "Y";
                    }
                }
            }
            else
            {
                strResult = "N";
            }
        }
        else
        {
            strResult = "N";
        }

        return strResult;
    }

    public String updateCounterTransDetails(MainModel oModCounTransDet)
    {
        String strResult = "";

        if (oModCounTransDet.GetSetcountertranid.Trim().Length > 0)
        {
            strResult = oMainCon.updateCounterTransDetails(oModCounTransDet);
            if (strResult.Equals("Y"))
            {
                strResult = oMainCon.updateCounterTransInfo(oModCounTransDet);
            }          
        }

        return strResult;
    }

    public String getMathRound(double dValue)
    {        
        return Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString();
    }

}