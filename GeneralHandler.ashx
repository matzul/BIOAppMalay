<%@ WebHandler Language="C#" Class="GeneralHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Configuration;

public class GeneralHandler : IHttpHandler {

    private String TokenNumber = "00000000";
    private String TokenNumberConfig = ConfigurationSettings.AppSettings["TokenNumber"];

    public MainController oMainCon = new MainController();
    public String sAction = "";
    public MainModel oItemStock = new MainModel();
    public MainModel oItemMod = new MainModel();
    public ArrayList lsItemMod = new ArrayList();

    public void ProcessRequest (HttpContext context) {
        sAction = context.Request.QueryString["action"];
        String json = "";
        context.Response.ContentType = "text/json";

        if (TokenNumber.Equals(TokenNumberConfig))
        {

            if (sAction.Equals("GET_STOCKLOCATION"))
            {
                oItemStock.GetSetcomp = context.Request.QueryString["comp"];
                json = getStockLocation(oItemStock);
                context.Response.Write(json);
            }
            else if (sAction.Equals("GET_STOCKAVAILABLE"))
            {
                oItemStock.GetSetcomp = context.Request.QueryString["comp"];
                oItemStock.GetSetitemno = context.Request.QueryString["itemno"];
                json = getStockAvailableList(oItemStock);
                context.Response.Write(json);
            }
            else if (sAction.Equals("GET_DATESOH"))
            {
                oItemStock.GetSetcomp = context.Request.QueryString["comp"];
                oItemStock.GetSetitemno = context.Request.QueryString["itemno"];
                oItemStock.GetSetlocation = context.Request.QueryString["location"];
                json = getDateSOHList(oItemStock);
                context.Response.Write(json);
            }
            else if (sAction.Equals("GET_QTYSOH"))
            {
                oItemStock.GetSetcomp = context.Request.QueryString["comp"];
                oItemStock.GetSetitemno = context.Request.QueryString["itemno"];
                oItemStock.GetSetlocation = context.Request.QueryString["location"];
                oItemStock.GetSetdatesoh = context.Request.QueryString["datesoh"];
                json = getQtySOHDetails(oItemStock);
                context.Response.Write(json);
            }
        }
        else
        {
                context.Response.Write(json);
        }

    }

    private String getStockLocation(MainModel oItemStock)
    {
        List<object> lsStockLocation = new List<object>();

        lsItemMod = oMainCon.getParamList(oItemStock.GetSetcomp, "", "STOCK_LOCATION", "");
        foreach (MainModel item in lsItemMod)
        {
            lsStockLocation.Add(new { location = item.GetSetparamdesc, locationdesc = item.GetSetparamdesc });
        }
        return new JavaScriptSerializer().Serialize(lsStockLocation);
    }

    private String getStockAvailableList(MainModel oItemStock)
    {
        List<object> lsStockAvailable = new List<object>();

        lsItemMod = oMainCon.getItemStockList(oItemStock.GetSetcomp, oItemStock.GetSetitemno, "", "", true);
        foreach (MainModel item in lsItemMod)
        {
            if (item.GetSetqtyavailable > 0)
            {
                lsStockAvailable.Add(new { item = item.GetSetitemno, location = item.GetSetlocation, datesoh = item.GetSetdatesoh, qtysoh = item.GetSetqtysoh, qtyavailable = item.GetSetqtyavailable, qtyallocated = item.GetSetqtyallocated, costsoh = item.GetSetcostsoh, itemdesc = item.GetSetitemdesc });
            }
        }
        return new JavaScriptSerializer().Serialize(lsStockAvailable);
    }

    private String getDateSOHList(MainModel oItemStock)
    {
        List<String> lsDateSOH = new List<String>();

        lsItemMod = oMainCon.getItemStockList(oItemStock.GetSetcomp, oItemStock.GetSetitemno, oItemStock.GetSetlocation, "", true);
        foreach (MainModel item in lsItemMod)
        {
            lsDateSOH.Add(item.GetSetdatesoh);
        }
        lsDateSOH.Add("");
        return new JavaScriptSerializer().Serialize(lsDateSOH);
    }

    private String getQtySOHDetails(MainModel oItemStock)
    {
        object objQtySOH = new object();
        if (oItemStock.GetSetcomp.Length > 0 && oItemStock.GetSetitemno.Length > 0 && oItemStock.GetSetlocation.Length > 0 && oItemStock.GetSetdatesoh.Length > 0)
        {
            oItemMod = oMainCon.getItemStockDetails(oItemStock.GetSetcomp, oItemStock.GetSetitemno, oItemStock.GetSetlocation, oItemStock.GetSetdatesoh);
            objQtySOH = new { qtysoh = oItemMod.GetSetqtysoh, costsoh = oItemMod.GetSetcostsoh };
        }
        else
        {
            objQtySOH = new { qtysoh = 0, costsoh = 0 };
        }
        return new JavaScriptSerializer().Serialize(objQtySOH);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}