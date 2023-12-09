SELECT item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue
FROM bioapp.item_discount
order by ordertype, itemno;

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-LED RENTAL-PACKAGE-01','PERCENTAGE',100);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-LED RENTAL-PACKAGE-02','PERCENTAGE',100);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-LED RENTAL-PACKAGE-03','PERCENTAGE',100);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-LED RENTAL-PACKAGE-04','PERCENTAGE',100);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-ADVERT DESIGNING-TYPE-01','AMOUNT',0);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-ADVERT DESIGNING-TYPE-02','AMOUNT',0);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-ADVERT DESIGNING-TYPE-03','AMOUNT',0);

insert into item_discount(item_discount.comp, item_discount.ordertype, item_discount.itemno, item_discount.disccat, item_discount.discvalue)
values ('CDG','WAIVED','CD-ADVERT SALES AGENT-COM-01','AMOUNT',0);