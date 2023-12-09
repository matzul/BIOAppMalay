SELECT *
FROM `bioapp`.`expenses_header`;

SELECT `expenses_header`.`comp`,
    `expenses_header`.`expensesno`,
    `expenses_header`.`expensesdate`,
    `expenses_header`.`expensescat`,
    `expenses_header`.`expensestype`,
    `expenses_header`.`bpid`,
    `expenses_header`.`bpdesc`,
    `expenses_header`.`bpaddress`,
    `expenses_header`.`bpcontact`,
    `expenses_header`.`purchaseamount`,
    `expenses_header`.`discamount`,
    `expenses_header`.`expensesamount`,
    `expenses_header`.`taxamount`,
    `expenses_header`.`totalamount`,
    `expenses_header`.`paypaidamount`,
    `expenses_header`.`remarks`,
    `expenses_header`.`status`,
    `expenses_header`.`createdby`,
    `expenses_header`.`createddate`,
    `expenses_header`.`confirmedby`,
    `expenses_header`.`confirmeddate`,
    `expenses_header`.`cancelledby`,
    `expenses_header`.`cancelleddate`
FROM `bioapp`.`expenses_header`;

select sum(totalexpenses) from expenses_details;

delete from expenses_header where expensesno = 'CD-EXP201600007';

update expenses_header set status = 'NEW';

SELECT `expenses_details`.`comp`,
    `expenses_details`.`expensesno`,
    `expenses_details`.`lineno`,
    `expenses_details`.`receiptno`,
    `expenses_details`.`receipt_lineno`,
    `expenses_details`.`orderno`,
    `expenses_details`.`order_lineno`,
    `expenses_details`.`itemno`,
    `expenses_details`.`itemdesc`,
    `expenses_details`.`unitprice`,
    `expenses_details`.`discamount`,
    `expenses_details`.`quantity`,
    `expenses_details`.`expensesprice`,
    `expenses_details`.`taxcode`,
    `expenses_details`.`taxrate`,
    `expenses_details`.`taxamount`,
    `expenses_details`.`totalexpenses`
FROM `bioapp`.`expenses_details`;

delete from expenses_details;