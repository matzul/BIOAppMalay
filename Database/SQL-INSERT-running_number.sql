SELECT * FROM bioapp.running_number;

INSERT INTO `bioapp`.`running_number` (`comp`, `type`, `initial`, `year`, `runno`, `status`)
VALUES ('CDG', 'PAYMENT_RECEIPT', 'CD-PRC', '2016', 0, 'ACTIVE');

update running_number
set	   runno = 7
where  comp = 'CDG'
and	   type = 'BUSINESS_PARTNER';

update running_number
set	   runno = 4
where  comp = 'CDG'
and	   type = 'SALES_ORDER';

update running_number
set	   runno = 0
where  comp = 'CDG'
and	   type = 'WORK_ORDER';

update running_number
set	   runno = 3
where  comp = 'CDG'
and	   type = 'SHIPMENT';

update running_number
set	   runno = 6
where  comp = 'CDG'
and	   type = 'EXPENSES';

update running_number
set	   runno = 0
where  comp = 'CDG'
and	   type = 'PAYMENT_RECEIPT';

INSERT INTO `bioapp`.`running_number` (`comp`, `type`, `initial`, `year`, `runno`, `status`)
VALUES ('CDG', 'SHIPMENT', 'CD-DO', '2016', 0, 'ACTIVE');

INSERT INTO `bioapp`.`running_number` (`comp`, `type`, `initial`, `year`, `runno`, `status`)
VALUES ('CDG', 'INVOICE', 'CD-INV', '2016', 0, 'ACTIVE');

INSERT INTO `bioapp`.`running_number` (`comp`, `type`, `initial`, `year`, `runno`, `status`)
VALUES ('CDG', 'EXPENSES', 'CD-EXP', '2016', 0, 'ACTIVE');

INSERT INTO `bioapp`.`running_number` (`comp`, `type`, `initial`, `year`, `runno`, `status`)
VALUES ('CDG', 'OTHER_BUSINESS_PARTNER', 'CD-OBP', '2016', 0, 'ACTIVE');
