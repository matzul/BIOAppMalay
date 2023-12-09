SELECT *
FROM bioapp.order_header
order by orderno;

update order_header
set	   orderdate = STR_TO_DATE('31-08-2016 13:40:30','%d-%m-%Y %H:%i:%s')
where  orderno = 'CD-SOO201600007';

update order_header
set	   orderstatus = 'CONFIRMED'
where  orderno = 'CD-SOO201600007';

update order_header
set	   bpid = 'CD-BP201600005'
where  bpid = 'CD-BP201600009';

delete from order_header
where  orderno = 'CD-SOO201600005';

select STR_TO_DATE('18-07-2016 10:40:30','%d-%m-%Y %H:%i:%s'),
	   date_format(orderdate,'%d-%m-%Y %H:%i:%s')
from   order_header;

SELECT *, date_format(orderdate,'%m-%Y')
FROM   bioapp.order_header
where  date_format(orderapproveddate,'%m-%Y') = '10-2016'
and	   orderstatus = 'CONFIRMED';

update order_header
set	   orderremarks = 'LED RENTAL PACKAGE FOR OCTOBER 2016'
where  orderno = 'CD-SOO201600012';

update order_header
set	   orderstatus = 'CANCELLED', orderapproved = null, ordercancelled = 'sysadmin'
where  orderno = 'CD-SOO201600001';

select *
from   order_details;

delete from order_details
where  orderno = 'CD-SOO201600005';

INSERT INTO bioapp.order_details
(comp,
orderno,
lineno,
itemno,
itemdesc,
unitprice,
discamount,
quantity,
orderprice,
taxcode,
taxrate,
taxamount,
totalprice,
deliverqty,
invoiceamount)
VALUES
('CDG',
'CD-SOO201600004',
1,
'CD-LED RENTAL-PACKAGE-01',
'LED Digital Billboard: Rental Package 1 (Start Up)
<br/>Monthly Commitment: 3 Months + 1 Free',
699,
211,
1,
488,
'ZRL',
0,
0,
488,
0,
0);

update order_details
set	   itemdesc = 'LED Digital Billboard: Rental Package 1 (Start Up) [Monthly Commitment: 1 Month + 1 Free]'
where  orderno = 'CD-SOO201600004'; 

SELECT * FROM bioapp.order_details
order  by orderno;


SELECT order_details.comp, order_details.itemno, order_details.itemdesc, SUM(order_details.totalprice) order_amount, SUM(order_details.deliverqty) order_qty, SUM(order_details.invoiceamount) invoice_amount  
from   order_details, order_header  
WHERE  order_details.comp is not NULL  
AND    order_details.orderno = order_header.orderno  
and  order_details.comp = 'CDG'  
and  order_details.itemno = 'CD-LED RENTAL-PACKAGE-01'  
and  date_format(order_header.orderdate,'%m-%Y') IN (SELECT CONCAT(actualmonth, '-', actualyear) FROM fiscalperiod WHERE financeyear = '2016')  
and  date_format(order_header.orderdate,'%m') = '09' 
and  order_header.orderstatus = 'CONFIRMED'  
group by order_details.comp, order_details.itemno, order_details.itemdesc  
order by order_details.comp, order_details.itemno, order_details.itemdesc