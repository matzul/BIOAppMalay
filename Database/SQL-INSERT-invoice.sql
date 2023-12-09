SELECT *
FROM bioapp.invoice_header;

SELECT invoice_header.comp, invoice_header.bpid, invoice_header.bpdesc, invoice_header.bpaddress, invoice_header.bpcontact, SUM(invoice_header.totalamount), SUM(invoice_header.payrcptamount)  
from   invoice_header  
WHERE  invoice_header.comp is not NULL  and  invoice_header.comp = 'CDG'  
and  date_format(invoice_header.invoicedate,'%m-%Y') IN (SELECT CONCAT(actualmonth, '-', actualyear) FROM fiscalperiod WHERE financeyear = '2016')  
and  invoice_header.status = 'CONFIRMED'  
group by invoice_header.comp, invoice_header.bpid, invoice_header.bpdesc, invoice_header.bpaddress, invoice_header.bpcontact  
order by invoice_header.comp, invoice_header.bpid, invoice_header.bpdesc, invoice_header.bpaddress, invoice_header.bpcontact;

update invoice_header
set	   remarks = 'RENTAL PERIOD : 19.09.2016 - 18.11.2016 FOR RERN THAI'
where	invoiceno = 'CD-INV201600008';

update invoice_header
set	   bpaddress = '42, G-FLOOR, PERSIARAN BUNGA RAYA, MAHSURI MALL, 07000 LANGKAWI, KEDAH',
	   bpcontact = 'ROBYN NORMAN CHOO 018-204 4139',
       bpid = 'CD-BP201600005'
where  bpdesc = 'NEW SIGNAG';

update invoice_header
set	   shipmentdate = STR_TO_DATE('02-09-2016','%d-%m-%Y')
where	invoiceno = 'CD-INV201600007';

delete from invoice_header
where	invoiceno = 'CD-INV201600005';



update invoice_header
set	invoicedate = STR_TO_DATE('01-09-2016','%d-%m-%Y')
where	invoiceno = 'CD-INV201600007';


SELECT *
FROM bioapp.invoice_details
where invoiceno = '';

delete from invoice_details
where  invoiceno = '';

update invoice_details
set	shipmentno = 'CD-DO201600007',
	orderno = 'CD-SOO201600007'
where invoiceno = 'CD-INV201600007';

delete from invoice_details
where	invoiceno = 'CD-INV201600005';

/*PENDING INVOICE*/
SELECT shipment_header.comp, date_format(shipment_header.shipmentdate,'%d-%m-%Y') str_shipmentdate, shipment_details.shipmentno, shipment_details.lineno,         
	shipment_header.bpid, shipment_header.bpdesc, shipment_header.bpaddress, shipment_header.bpcontact, shipment_header.remarks,         
    date_format(order_header.orderdate,'%d-%m-%Y') str_orderdate, order_header.ordertype, shipment_details.orderno, shipment_details.order_lineno, shipment_details.itemno, shipment_details.itemdesc,         order_details.unitprice, order_details.discamount, shipment_details.shipment_quantity quantity,         order_details.taxcode, order_details.taxrate  FROM   shipment_header, shipment_details, order_header, order_details  
WHERE  shipment_details.comp is not NULL  AND    shipment_details.comp = order_details.comp  
AND    shipment_details.orderno = order_details.orderno  AND    shipment_details.order_lineno = order_details.lineno      
AND    shipment_details.itemno = order_details.itemno  AND    shipment_details.hasinvoice = 'N'  
AND    shipment_details.comp = shipment_header.comp AND    shipment_details.shipmentno = shipment_header.shipmentno  
AND    shipment_header.status = 'CONFIRMED'  AND    order_details.comp = order_header.comp  
AND    order_details.orderno = order_header.orderno  and  shipment_details.comp = 'CDG'  
/*and  shipment_header.bpid = 'CD-BP201600005'  */
order by shipment_details.comp, shipment_details.shipmentno, shipment_details.lineno, shipment_details.orderno, shipment_details.order_lineno