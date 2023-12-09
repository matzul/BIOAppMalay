SELECT *
FROM `bioapp`.`payrcpt_header`;

update payrcpt_header
set	status = 'NEW'
where payrcptno = 'CD-PRC201600004';

update payrcpt_header
set	   payrcptdate = STR_TO_DATE('10-08-2016','%d-%m-%Y');

delete from payrcpt_header
where payrcptno = 'CD-PRC201600003';

select *
from	payrcpt_details;

update payrcpt_details
set	invoicedate = STR_TO_DATE('21-07-2016','%d-%m-%Y')
where	payrcptno = 'CD-PRC201600001';

delete from payrcpt_details
where payrcptno = 'CD-PRC201600003';

SELECT invoice_header.comp, invoice_header.invoiceno, date_format(invoice_header.invoicedate,'%d-%m-%Y') str_invoicedate, invoice_header.invoicetype, invoice_header.invoiceterm, invoice_header.bpid,         
		invoice_header.bpdesc, invoice_header.bpaddress, invoice_header.bpcontact, invoice_header.salesamount, invoice_header.discamount, invoice_header.invoiceamount,         
		invoice_header.taxamount, invoice_header.totalamount, invoice_header.payrcptamount, invoice_header.remarks, invoice_header.status, invoice_header.createdby, invoice_header.createddate,         
        invoice_header.confirmedby, invoice_header.confirmeddate, invoice_header.cancelledby, invoice_header.cancelleddate   
FROM   invoice_header  
WHERE  invoice_header.comp is not NULL  
AND    invoice_header.status = 'CONFIRMED'  
/*AND    invoice_header.totalamount - invoice_header.payrcptamount > 0  */
and  invoice_header.comp = 'CDG'  
and  invoice_header.bpid = 'CD-BP201600004'  
order by invoice_header.comp, invoice_header.invoiceno, invoice_header.invoicedate;

SELECT SUM(payrcpt_header.payrcptamount) collection  
from   payrcpt_header  
WHERE  payrcpt_header.comp is not NULL  
and  payrcpt_header.comp = 'CDG'  and  date_format(payrcptdate,'%m-%Y') = '09-2016';

SELECT payrcpt_header.comp, payrcpt_header.bpid, payrcpt_header.bpdesc, payrcpt_header.bpaddress, payrcpt_header.bpcontact, SUM(payrcpt_header.invoiceamount), SUM(payrcpt_header.payrcptamount)  
from   payrcpt_header  
WHERE  payrcpt_header.comp is not NULL  and  payrcpt_header.comp = 'CDG'  
and  date_format(payrcpt_header.payrcptdate,'%m-%Y') IN (SELECT CONCAT(actualmonth, '-', actualyear) FROM fiscalperiod WHERE financeyear = '2016')  and  payrcpt_header.status = 'CONFIRMED'  
group by payrcpt_header.comp, payrcpt_header.bpid, payrcpt_header.bpdesc, payrcpt_header.bpaddress, payrcpt_header.bpcontact  
order by payrcpt_header.comp, payrcpt_header.bpid, payrcpt_header.bpdesc, payrcpt_header.bpaddress, payrcpt_header.bpcontact