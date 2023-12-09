SELECT * FROM asnafcare.invoice_header
where  comp = 'LZNK-ASNAF-CARE' and invoiceno = 'INV20200602';

update asnafcare.invoice_header
#set    payrcptamount = 53.1
set    status = 'CONFIRMED'
where  comp = 'LZNK-ASNAF-CARE' and invoiceno in ('INV20200077',
'INV20200053',
'INV20200028',
'INV20200668',
'INV20205797',
'INV20204871',
'INV20203725',
'INV20201816',
'INV20201581',
'INV20201349'
);

select * from asnafcare.payrcpt_header
where  comp = 'LZNK-ASNAF-CARE' and payrcptno = 'PRC20201128';

update asnafcare.payrcpt_header
set    invoiceamount = 53.1,
	   payrcptamount = 53.1
where  comp = 'LZNK-ASNAF-CARE' and payrcptno = 'PRC20201128';	

SELECT invoice_header.bpid, invoice_header.bpdesc, date_format(invoice_header.invoicedate,'%d-%m-%Y') str_invoicedate, invoice_header.status,        
		invoice_details.comp, invoice_details.invoiceno, invoice_details.lineno, invoice_details.shipmentno, invoice_details.shipment_lineno, invoice_details.orderno, invoice_details.order_lineno,  	     
        invoice_details.itemno, invoice_details.itemdesc, invoice_details.unitprice, invoice_details.discamount, invoice_details.quantity, invoice_details.invoiceprice, invoice_details.taxcode,         
        invoice_details.taxrate, invoice_details.taxamount, invoice_details.totalinvoice, 
        (invoice_header.payrcptamount / invoice_header.totalamount) * invoice_details.totalinvoice as fundcollected  
from   invoice_details, invoice_header  
WHERE  invoice_details.comp is not NULL  
AND  invoice_details.comp = invoice_header.comp  
AND  invoice_details.invoiceno = invoice_header.invoiceno  
and  invoice_details.comp = 'LZNK-ASNAF-CARE'
and  (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat = 'RECEIPT_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME'))    
#and  invoice_details.itemno = 'PAR202003004'  
and  invoice_header.status = 'CONFIRMED'  
order by invoice_details.comp, invoice_header.invoicedate, invoice_details.invoiceno desc;

SELECT SUM(payrcpt_details.payrcptprice)
            from   payrcpt_header, payrcpt_details, invoice_header
			WHERE  payrcpt_header.comp is not NULL  
			AND    payrcpt_header.comp = payrcpt_details.comp  
			AND    payrcpt_header.payrcptno = payrcpt_details.payrcptno  
			AND    payrcpt_details.comp = 'LZNK-ASNAF-CARE'  
			AND    payrcpt_details.comp = invoice_header.comp  
			AND    payrcpt_details.invoiceno = invoice_header.invoiceno  
			and    payrcpt_header.status = 'CONFIRMED';

SELECT invoice_header.bpid, invoice_header.bpdesc, date_format(invoice_header.invoicedate,'%d-%m-%Y') str_invoicedate, invoice_header.status,        
		invoice_header.comp, invoice_header.invoiceno, invoice_header.invoiceamount, invoice_header.taxamount,         
        invoice_header.totalamount, invoice_header.payrcptamount, 
        (   SELECT SUM(payrcpt_details.payrcptprice)
            from   payrcpt_header, payrcpt_details
			WHERE  payrcpt_header.comp is not NULL  
			AND    payrcpt_header.comp = payrcpt_details.comp  
			AND    payrcpt_header.payrcptno = payrcpt_details.payrcptno  
			AND    payrcpt_details.comp = invoice_header.comp  
			AND    payrcpt_details.invoiceno = invoice_header.invoiceno  
			and    payrcpt_header.status = 'CONFIRMED'
        ) collection
from   invoice_header  
WHERE  invoice_header.comp is not NULL  
and    invoice_header.comp = 'LZNK-ASNAF-CARE'
and    (invoice_header.invoicecat = 'SALES_INVOICE' or invoice_header.invoicecat = 'TRANSFER_INVOICE' or (invoice_header.invoicecat = 'RECEIPT_VOUCHER' and invoice_header.invoicetype = 'OTHER_INCOME'))    
and    invoice_header.status = 'CONFIRMED'  
order by invoice_header.comp, invoice_header.invoicedate, invoice_header.invoiceno desc;  

SELECT invoice_header.bpid, invoice_header.bpdesc, date_format(invoice_header.invoicedate,'%d-%m-%Y') str_invoicedate, invoice_header.remarks,   
      invoice_details.comp, invoice_details.invoiceno, invoice_details.lineno, invoice_details.shipmentno, invoice_details.shipment_lineno, invoice_details.orderno, invoice_details.order_lineno,  
      invoice_details.itemno, invoice_details.itemdesc, invoice_details.unitprice, invoice_details.discamount, invoice_details.quantity, invoice_details.invoiceprice, invoice_details.taxcode,  
      invoice_details.taxrate, invoice_details.taxamount, invoice_details.totalinvoice, (invoice_header.payrcptamount / invoice_header.totalamount) * invoice_details.totalinvoice as fundcollected  
from   invoice_details, invoice_header  
WHERE  invoice_details.comp is not NULL  
AND  invoice_details.comp = invoice_header.comp  
AND  invoice_details.invoiceno = invoice_header.invoiceno  
AND  invoice_header.payrcptamount > 0  
and  invoice_details.comp = 'LZNK-ASNAF-CARE'  
#and  invoice_details.itemno = 'PAR202003014'  
and  invoice_header.status = 'CONFIRMED'  
order by invoice_details.comp, invoice_header.invoicedate desc, invoice_details.invoiceno desc;

SELECT a.invoiceno ,sum(a.invoiceprice), sum(a.payrcptprice), sum(b.payrcptamount), sum(c.totalamount), sum(c.payrcptamount), c.status
FROM asnafcare.payrcpt_details a, asnafcare.payrcpt_header b, asnafcare.invoice_header c
where a.comp = b.comp
and   a.payrcptno = b.payrcptno
and   b.status = 'CONFIRMED'
and   a.comp = c.comp
and   a.invoiceno = c.invoiceno
and   a.payrcptprice > 0
group by a.invoiceno, c.status;

######compare invoice details and payment details
select a.invoiceno, sum(a.totalamount), sum(a.payrcptamount), sum(b.invoiceprice), sum(b.payrcptprice)
from   asnafcare.invoice_header a
left join asnafcare.payrcpt_details b on a.comp = b.comp and a.invoiceno = b.invoiceno and b.payrcptprice > 0
#where   a.status = 'CONFIRMED'
#and     a.invoicedate >= '2020-04-01' and a.invoicedate <= '2020-04-30'
where   a.status = 'CANCELLED'
group  by a.invoiceno
order  by a.invoiceno;

select a.invoiceno, sum(a.totalamount), sum(a.payrcptamount), sum(b.invoiceprice), sum(b.payrcptprice)
from   bioappdb.invoice_header a
left join (
			select x.comp, x.payrcptprice, x.invoiceprice, x.invoiceno from bioappdb.payrcpt_details x, bioappdb.payrcpt_header y 
            where x.comp = y.comp and x.payrcptno = y.payrcptno and x.comp = 'T01' and y.status = 'CONFIRMED'
		  ) b on a.comp = b.comp and a.invoiceno = b.invoiceno and b.payrcptprice > 0
#where   a.status = 'CONFIRMED'
#and     a.invoicedate >= '2020-04-01' and a.invoicedate <= '2020-04-30'
where a.comp = 'T01'  
and   a.status = 'CONFIRMED'
and   date_format(a.confirmeddate,'%Y') = '2020'
group  by a.invoiceno
order  by a.invoiceno;

select a.invoiceno, sum(a.totalamount), sum(a.payrcptamount), sum(b.invoiceprice), sum(b.payrcptprice)
from   bioappdb.invoice_header a
left join (
			select x.comp, x.payrcptprice, x.invoiceprice, x.invoiceno from bioappdb.payrcpt_details x, bioappdb.payrcpt_header y 
            where x.comp = y.comp and x.payrcptno = y.payrcptno and x.comp = 'T01' and y.status = 'CANCELLED'
		  ) b on a.comp = b.comp and a.invoiceno = b.invoiceno and b.payrcptprice > 0
#where   a.status = 'CONFIRMED'
#and     a.invoicedate >= '2020-04-01' and a.invoicedate <= '2020-04-30'
where a.comp = 'T01'  
and   a.status = 'CONFIRMED'
and   date_format(a.confirmeddate,'%Y') = '2020'
group  by a.invoiceno
order  by a.invoiceno;

SELECT sum(a.invoiceprice), sum(a.payrcptprice) 
FROM asnafcare.payrcpt_details a, asnafcare.payrcpt_header b
where a.comp = b.comp
and   a.payrcptno = b.payrcptno
and   b.status = 'CONFIRMED';

select sum(totalamount), sum(payrcptamount)
from   asnafcare.invoice_header
where  comp = 'LZNK-ASNAF-CARE' and status = 'CONFIRMED';

SELECT * FROM asnafcare.invoice_header
where  comp = 'LZNK-ASNAF-CARE'
and    invoiceno = 'INV20200004826';

update asnafcare.invoice_header
set    payrcptamount = 3, status = 'CONFIRMED', cancelledby = null, cancelleddate = null
where  comp = 'LZNK-ASNAF-CARE'
and    invoiceno = 'INV20200004826';

select *
from   asnafcare.payrcpt_header
where  comp = 'LZNK-ASNAF-CARE'
and    payrcptno = 'PRC20200002950';

update asnafcare.payrcpt_header
set    status = 'ÇANCELLED', confirmedby = null, confirmeddate = null, cancelledby = 'asnafcare_support', cancelleddate = curdate()
where  comp = 'LZNK-ASNAF-CARE'
and    payrcptno = 'PRC20200002950';
