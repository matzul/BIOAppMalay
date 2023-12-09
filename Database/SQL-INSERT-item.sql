select *
from	item
order   by itemno;

update item
set	itemno = 'CD-ADVERT SALES AGENT-COM-01'
where itemno = 'CD-ADVERT SALES AGENT-COMMISSI';

delete from item
where  itemno = 'CD-ADVERT DESIGNING-TYPE-02';

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-LED RENTAL-PACKAGE-ADHOC', 'LED Digital Billboard: Rental Package ADHOC (Special) [Weekly Commitment: 1 Week]', 
'SERVICE', 'SLOT', 0.00, 0.00, 199.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-LED RENTAL-PACKAGE-01', 'LED Digital Billboard: Rental Package 1 (Start Up) [Monthly Commitment: 1 Month + 1 Free]', 
'SERVICE', 'SLOT', 0.00, 0.00, 699.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

update item
set	itemno = 'CD-LED RENTAL-PACKAGE-01',
	itemdesc = 'LED Digital Billboard: Rental Package 1 (Start Up) [Monthly Commitment: 1 Month]'
where itemno = 'LED Digital Billboard: Rental';

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-LED RENTAL-PACKAGE-02', 'LED Digital Billboard: Rental Package 2 (Standard) [Monthly Commitment: 3 Months + 1 Free]', 
'SERVICE', 'SLOT', 0.00, 0.00, 2089.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

update item
set	itemdesc = 'LED Digital Billboard: Rental Package 2 (Standard) [Monthly Commitment: 3 Months]'
where itemno = 'CD-LED RENTAL-PACKAGE-02';

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-LED RENTAL-PACKAGE-03', 'LED Digital Billboard: Rental Package 3 (Premium) [Monthly Commitment: 6 Months + 1 Free]', 
'SERVICE', 'SLOT', 0.00, 0.00, 4189.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

update item
set	itemdesc = 'LED Digital Billboard: Rental Package 3 (Premium) [Monthly Commitment: 6 Months]'
where itemno = 'CD-LED RENTAL-PACKAGE-03';

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-LED RENTAL-PACKAGE-04', 'LED Digital Billboard: Rental Package 4 (Loyalty) [Monthly Commitment: 12 Months + 1 Free]', 
'SERVICE', 'SLOT', 0.00, 0.00, 8389.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

update item
set	itemdesc = 'LED Digital Billboard: Rental Package 4 (Loyalty) [Monthly Commitment: 12 Months]'
where itemno = 'CD-LED RENTAL-PACKAGE-04';

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-ADVERT DESIGNING-TYPE-01', 'LED Digital Billboard: Advert Designing Type 1', 
'SERVICE', 'PC', 0.00, 0.00, 50.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-YEAREND SALES AGENT-COM-01', 'LED Digital Billboard: Year End Sales Agent Commission Type 1', 
'SERVICE', 'PC', 0.00, 0.00, -38.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-YEAREND SALES AGENT-COM-02', 'LED Digital Billboard: Year End Sales Agent Commission Type 2', 
'SERVICE', 'PC', 0.00, 0.00, -58.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-YEAREND SALES AGENT-COM-03', 'LED Digital Billboard: Year End Sales Agent Commission Type 3', 
'SERVICE', 'PC', 0.00, 0.00, -88.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-YEAREND SALES AGENT-COM-04', 'LED Digital Billboard: Year End Sales Agent Commission Type 4', 
'SERVICE', 'PC', 0.00, 0.00, -128.00, 0, 0, 0, 0.00, 0, 'ACTIVE');

INSERT INTO bioapp.item (comp, itemno, itemdesc, itemcat, itemtype, purchaseprice, costprice, salesprice, qtyorder, qtydemand, qtysoh, costsoh, qtysafetystock, itemstatus)
VALUES ('CDG', 'CD-YEAREND SALES AGENT-COM-AD', 'LED Digital Billboard: Year End Sales Agent Commission Type ADHOC', 
'SERVICE', 'PC', 0.00, 0.00, -18.00, 0, 0, 0, 0.00, 0, 'ACTIVE');


update item
set	   itemdesc = 'LED Digital Billboard: Rental Package 1 (Start Up) [Monthly Commitment: 1 Month + 1 Free]'
where  itemno = 'CD-LED RENTAL-PACKAGE-01';

SELECT item.comp, item.itemno, item.itemdesc, item.itemcat, item.itemtype, item.purchaseprice, item.costprice, 
	   item.salesprice, item.qtyorder, item.qtydemand, item.qtysoh, item.costsoh, item.qtysafetystock, item.itemstatus
FROM   bioapp.item;

select item.comp, item.itemno, item.itemdesc, item.salesprice, item_discount.disccat, 
	   case item_discount.disccat 
       when 'AMOUNT' THEN item_discount.discvalue
       when 'PERCENTAGE' THEN round(item.salesprice * item_discount.discvalue / 100, 2)
       else 0 end discamount
from   item, item_discount
where  item.comp = item_discount.comp
and	   item.itemno = item_discount.itemno
and	   item_discount.ordertype = 'YEAREND'
order  by item.itemno;

select *
from	item_discount
order   by ordertype, itemno;

delete from item_discount
where  itemno = 'CD-ADVERT SALES AGENT-COM-01';

/*For Order Type = OPENING */
INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'OPENING', 'CD-ADVERT DESIGNING-TYPE-03', 'AMOUNT', 0);

/*For Order Type = NORMAL */
INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'NORMAL', 'CD-ADVERT DESIGNING-TYPE-03', 'AMOUNT', 0);

/*For Order Type = WAIVED */
update item_discount set disccat = 'PERCENTAGE', discvalue = 100 where ordertype = 'WAIVED';

/*For Order Type = PROMOTION */
INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'PROMOTION', 'CD-ADVERT DESIGNING-TYPE-03', 'AMOUNT', 0);

/*For Order Type = YEAREND */
INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-LED RENTAL-PACKAGE-01', 'AMOUNT', 311);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-LED RENTAL-PACKAGE-02', 'AMOUNT', 1001);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-LED RENTAL-PACKAGE-03', 'AMOUNT', 2101);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-LED RENTAL-PACKAGE-04', 'AMOUNT', 4201);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-LED RENTAL-PACKAGE-ADHOC', 'AMOUNT', 71);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-ADVERT DESIGNING-TYPE-01', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-ADVERT DESIGNING-TYPE-02', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-ADVERT DESIGNING-TYPE-03', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-YEAREND SALES AGENT-COM-AD', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-YEAREND SALES AGENT-COM-01', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-YEAREND SALES AGENT-COM-02', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-YEAREND SALES AGENT-COM-03', 'AMOUNT', 0);

INSERT INTO `bioapp`.`item_discount` (`comp`, `ordertype`, `itemno`, `disccat`, `discvalue`)
VALUES ('CDG', 'YEAREND', 'CD-YEAREND SALES AGENT-COM-04', 'AMOUNT', 0);
