SELECT *
FROM bioapp.shipment_header;

delete from shipment_header
where  shipmentno = 'CD-DO201600004';

update shipment_header
set	   status = 'CANCELLED', cancelledby = 'sysadmin', cancelleddate = STR_TO_DATE('10-09-2016','%d-%m-%Y'), confirmedby = null, confirmeddate = null
where  shipmentno = 'CD-DO201600001';

update shipment_header
set	shipmentdate = STR_TO_DATE('31-08-2016','%d-%m-%Y')
where	shipmentno = 'CD-DO201600007';

update shipment_header
set	   bpid = 'CD-BP201600005'
where  bpid = 'CD-BP201600009';

update shipment_header
set	bpdesc = 'THE TREE CAFE'
where	shipmentno = 'CD-DO201600003';

SELECT shipment_header.*, shipment_details.*
FROM shipment_header, shipment_details
WHERE shipment_header.shipmentno = shipment_details.shipmentno
order by shipment_header.shipmentno;

delete from shipment_details
where  shipmentno = 'CD-DO201600004';

update shipment_details
set	   hasinvoice = "N"
where  shipmentno = 'CD-DO201600008';

insert into shipment_details (comp, shipmentno, lineno, orderno, order_lineno, itemno, itemdesc, order_quantity, shipment_quantity, hasinvoice)
values ('CDG', 'CD-DO201600007', 2, 'CD-SOO201600007', 2, 'CD-ADVERT SALES AGENT-COM-01', 'LED Digital Billboard: Advert Sales Agent Commission Type 1', 1, 1, 'Y');

insert into shipment_details (comp, shipmentno, lineno, orderno, order_lineno, itemno, itemdesc, order_quantity, shipment_quantity, hasinvoice)
values ('CDG', 'CD-DO201600008', 2, 'CD-SOO201600008', 2, 'CD-ADVERT SALES AGENT-COM-01', 'LED Digital Billboard: Advert Sales Agent Commission Type 1', 1, 1, 'N');
