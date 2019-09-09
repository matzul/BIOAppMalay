using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
[XmlSerializerFormat]
public interface IService
{
    [OperationContract]
    List<ToDoItem> GetToDos();

    [OperationContract]
    void AddOrUpdateToDo(ToDoItem toDoItem);

    [OperationContract]
    void DeleteToDo(ToDoItem toDoItem);

    [OperationContract]
    string getCurrentDateFormatDDMMYYYY();

    [OperationContract]
    List<MainModel> getOrderItemDiscountList(String comp, String ordercat, String ordertype, String itemno);

    [OperationContract]
    List<CounterModel> getCounterMasterList(String comp, String status);

    [OperationContract]
    CounterModel getCounterMasterDetails(String comp, String status, String counterno, String countertranid);

    [OperationContract]
    String insertCounterMaster(CounterModel oModCounterMaster);

    [OperationContract]
    String updateCounterMaster(CounterModel oModCounterMaster);

    [OperationContract]
    List<CounterModel> getCounterTransList(String comp, String counterno, String id, String userid, String status);

    [OperationContract]
    CounterModel getCounterTrans(String comp, String counterno, String id, String userid, String status);

    [OperationContract]
    String insertCounterTrans(CounterModel oModCounterTrans);

    [OperationContract]
    String updateCounterTrans(CounterModel oModCounterTrans);

    [OperationContract]
    List<MainModel> getCounterTransDetailsList(String sComp, String sCounterNo, String sCounterTransId, String sOrderNo, String sInvoiceNo, String sRowInclude);

    [OperationContract]
    MainModel getCounterTransDetails(String sComp, String sCounterNo, String sCounterTransId, String sOrderNo);

    [OperationContract]
    UserProfileModel getUserProfile(String comp, String userid);

    [OperationContract]
    String getNextRunningNo(String comp, String type, String status);

    [OperationContract]
    void updateNextRunningNo(String comp, String type, String status);

    [OperationContract]
    List<MainModel> getOrderHeaderList(String comp, String orderno, String bpid, String startdate, String enddate, String status);

    [OperationContract]
    MainModel getOrderHeaderDetails(String comp, String orderno);

    [OperationContract]
    String insertOrderHeader(MainModel oModOrder, string counterno, string countertranid);
    //String insertOrderHeader(MainModel oModOrder);

    [OperationContract]
    String updateOrderHeader(MainModel oModOrder, string counterno, string id);

    [OperationContract]
    String updateOrderHeaderInfo(String sComp, String sOrderNo, string counterno, string countertranid);

    [OperationContract]
    List<MainModel> getOrderDetailsList(String comp, String orderno, int lineno, String itemno);

    [OperationContract]
    MainModel getOrderDetailsDetails(String comp, String orderno, int lineno, String itemno);

    [OperationContract]
    String insertOrderDetails(MainModel oModOrderDet);

    [OperationContract]
    String updateOrderDetails(MainModel oModOrderDet);
    
    [OperationContract]
    String deleteOrderDetails(MainModel oModOrderDet);

    [OperationContract]
    MainModel getItemStockSummary2(String comp, String itemno);

    [OperationContract]
    String insertShipmentHeaderAndDetails(MainModel oModOrdHdr, string counterno, string countertranid);

    [OperationContract]
    String insertInvoiceHeaderAndDetails(MainModel oModOrdHdr, string counterno, string countertranid);

    [OperationContract]
    String insertPaymentReceiptHeaderAndDetails(MainModel oModOrdHdr, MainModel oModCounTransDet);

    [OperationContract]
    String updateCounterTransDetails(MainModel oModCounTransDet);
        
    [OperationContract]
    String getMathRound(double dValue);
}
