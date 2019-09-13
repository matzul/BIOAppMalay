-- ----------------------------------------------------------------------------
-- Created: Mon Sep 09 23:55:34 2019
-- Workbench Version: 6.3.6
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema bioappdb
-- ----------------------------------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `bioappdb` ;

-- ----------------------------------------------------------------------------
-- Table bioappdb.adjustment_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`adjustment_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `adjustmentno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `qtysoh` INT(11) NULL DEFAULT NULL,
  `costsoh` DOUBLE NULL DEFAULT NULL,
  `qtyvariance` INT(11) NULL DEFAULT NULL,
  `pricevariance` DOUBLE NULL DEFAULT NULL,
  `qtyadjusted` INT(11) NULL DEFAULT NULL,
  `costadjusted` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ADJUSTMENT_DET_01` (`comp` ASC, `adjustmentno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.adjustment_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`adjustment_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `adjustmentno` VARCHAR(20) NULL DEFAULT NULL,
  `adjustmentdate` DATETIME NULL DEFAULT NULL,
  `adjustmenttype` VARCHAR(15) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ADJUSTMENT_PRI_01` (`comp` ASC, `adjustmentno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.businesspartner
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`businesspartner` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(100) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(50) NULL DEFAULT NULL,
  `bpreference` VARCHAR(50) NULL DEFAULT NULL,
  `bpcat` VARCHAR(15) NULL DEFAULT NULL,
  `discounttype` VARCHAR(15) NULL DEFAULT NULL,
  `cashguarantee` DOUBLE NULL DEFAULT NULL,
  `bankguarantee` DOUBLE NULL DEFAULT NULL,
  `creditlimit` DOUBLE NULL DEFAULT NULL,
  `bpstatus` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_BP_01` (`comp` ASC, `bpid` ASC),
  UNIQUE INDEX `IDX_PRI_BP_02` (`comp` ASC, `bpdesc` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.cashflow_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`cashflow_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `cashflowno` VARCHAR(20) NULL DEFAULT NULL,
  `cashflowtype` VARCHAR(50) NULL DEFAULT NULL,
  `paymentno` VARCHAR(20) NULL DEFAULT NULL,
  `paymentdate` DATE NULL DEFAULT NULL,
  `paymentconfirmeddate` DATETIME NULL DEFAULT NULL,
  `paymenttype` VARCHAR(50) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `paydetno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `paytype` VARCHAR(15) NULL DEFAULT NULL,
  `payrefno` VARCHAR(50) NULL DEFAULT NULL,
  `payremarks` VARCHAR(100) NULL DEFAULT NULL,
  `payamount` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_CASHFLOWDET_01` (`comp` ASC, `cashflowno` ASC, `paymentno` ASC, `paydetno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.cashflow_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`cashflow_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `cashflowno` VARCHAR(20) NULL DEFAULT NULL,
  `openingdate` DATETIME NULL DEFAULT NULL,
  `openingtype` VARCHAR(50) NULL DEFAULT NULL,
  `bankopeningamount` DOUBLE NULL DEFAULT NULL,
  `cashopeningamount` DOUBLE NULL DEFAULT NULL,
  `bankpaymentreceiptamount` DOUBLE NULL DEFAULT NULL,
  `cashpaymentreceiptamount` DOUBLE NULL DEFAULT NULL,
  `bankpaymentpaidamount` DOUBLE NULL DEFAULT NULL,
  `cashpaymentpaidamount` DOUBLE NULL DEFAULT NULL,
  `closingdate` DATETIME NULL DEFAULT NULL,
  `closingtype` VARCHAR(50) NULL DEFAULT NULL,
  `bankclosingamount` DOUBLE NULL DEFAULT NULL,
  `cashclosingamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_CASHFLOW_01` (`comp` ASC, `cashflowno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.comp_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`comp_details` (
  `comp` VARCHAR(3) NOT NULL,
  `comp_name` VARCHAR(100) NULL DEFAULT NULL,
  `comp_id` VARCHAR(50) NULL DEFAULT NULL,
  `comp_accountbank` VARCHAR(100) NULL DEFAULT NULL,
  `comp_accountno` VARCHAR(50) NULL DEFAULT NULL,
  `comp_address` VARCHAR(100) NULL DEFAULT NULL,
  `comp_contact` VARCHAR(50) NULL DEFAULT NULL,
  `comp_contactno` VARCHAR(20) NULL DEFAULT NULL,
  `comp_website` VARCHAR(50) NULL DEFAULT NULL,
  `comp_email` VARCHAR(50) NULL DEFAULT NULL,
  `comp_icon` VARCHAR(100) NULL DEFAULT NULL,
  `comp_logo1` VARCHAR(100) NULL DEFAULT NULL,
  `comp_logo2` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`comp`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.counter_master
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`counter_master` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `counterno` VARCHAR(50) NULL DEFAULT NULL,
  `countertranid` VARCHAR(50) NULL DEFAULT NULL,
  `openingbalance` DOUBLE NULL DEFAULT NULL,
  `openingby` VARCHAR(50) NULL DEFAULT NULL,
  `openingdate` DATETIME NULL DEFAULT NULL,
  `pos_bpid` VARCHAR(20) NULL DEFAULT NULL,
  `pos_bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `pos_ordercat` VARCHAR(15) NULL DEFAULT NULL,
  `pos_ordertype` VARCHAR(15) NULL DEFAULT NULL,
  `pos_orderactivity` VARCHAR(10) NULL DEFAULT NULL,
  `pos_paytype` VARCHAR(15) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_COUNTERMASTER_01` (`comp` ASC, `counterno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.counter_transaction
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`counter_transaction` (
  `id` VARCHAR(50) NOT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `counterno` VARCHAR(50) NULL DEFAULT NULL,
  `openingbalance` DOUBLE NULL DEFAULT NULL,
  `openingby` VARCHAR(50) NULL DEFAULT NULL,
  `openingdate` DATETIME NULL DEFAULT NULL,
  `totalorderamount` DOUBLE NULL DEFAULT NULL,
  `totalinvoiceamount` DOUBLE NULL DEFAULT NULL,
  `totalpayrcptamount` DOUBLE NULL DEFAULT NULL,
  `closingbalance` DOUBLE NULL DEFAULT NULL,
  `closingby` VARCHAR(50) NULL DEFAULT NULL,
  `closingdate` DATETIME NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_COUNTERTRANS_01` (`comp` ASC, `counterno` ASC, `openingby` ASC, `openingdate` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.counter_transaction_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`counter_transaction_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `counterno` VARCHAR(50) NULL DEFAULT NULL,
  `countertranid` VARCHAR(50) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `orderdate` DATETIME NULL DEFAULT NULL,
  `orderamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `salesamount` DOUBLE NULL DEFAULT NULL,
  `orderstatus` VARCHAR(50) NULL DEFAULT NULL,
  `shipmentno` VARCHAR(20) NULL DEFAULT NULL,
  `shipmentdate` DATETIME NULL DEFAULT NULL,
  `shipmentstatus` VARCHAR(50) NULL DEFAULT NULL,
  `invoiceno` VARCHAR(20) NULL DEFAULT NULL,
  `invoicedate` DATETIME NULL DEFAULT NULL,
  `invoiceamount` DOUBLE NULL DEFAULT NULL,
  `invoicestatus` VARCHAR(50) NULL DEFAULT NULL,
  `payrcptno` VARCHAR(20) NULL DEFAULT NULL,
  `payrcptdate` DATETIME NULL DEFAULT NULL,
  `payrcptamount` DOUBLE NULL DEFAULT NULL,
  `payrcptstatus` VARCHAR(50) NULL DEFAULT NULL,
  `paidamount` DOUBLE NULL DEFAULT NULL,
  `balanceamount` DOUBLE NULL DEFAULT NULL,
  `rowinclude` INT(11) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_COUNTRANDET_01` (`comp` ASC, `counterno` ASC, `countertranid` ASC, `orderno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_collection
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_collection` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_COL01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_expenses
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_expenses` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_EXP01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_payment
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_payment` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PAYMENT_COL01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_revenue
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_revenue` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_REV01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_sales
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_sales` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_SALES01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_slot
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_slot` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_SLOT01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_stockin
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_stockin` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_STOCKIN01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.dashboard_stockout
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`dashboard_stockout` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `type` VARCHAR(30) NULL DEFAULT NULL,
  `MON01` DOUBLE NULL DEFAULT NULL,
  `MON01desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON02` DOUBLE NULL DEFAULT NULL,
  `MON02desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON03` DOUBLE NULL DEFAULT NULL,
  `MON03desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON04` DOUBLE NULL DEFAULT NULL,
  `MON04desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON05` DOUBLE NULL DEFAULT NULL,
  `MON05desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON06` DOUBLE NULL DEFAULT NULL,
  `MON06desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON07` DOUBLE NULL DEFAULT NULL,
  `MON07desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON08` DOUBLE NULL DEFAULT NULL,
  `MON08desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON09` DOUBLE NULL DEFAULT NULL,
  `MON09desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON10` DOUBLE NULL DEFAULT NULL,
  `MON10desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON11` DOUBLE NULL DEFAULT NULL,
  `MON11desc` VARCHAR(10) NULL DEFAULT NULL,
  `MON12` DOUBLE NULL DEFAULT NULL,
  `MON12desc` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_DASHBOARD_STOCKOUT01` (`comp` ASC, `fyr` ASC, `type` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.expenses_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`expenses_details` (
  `comp` VARCHAR(3) NOT NULL,
  `expensesno` VARCHAR(20) NOT NULL,
  `lineno` INT(11) NOT NULL,
  `receiptno` VARCHAR(20) NULL DEFAULT NULL,
  `receipt_lineno` INT(11) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `unitprice` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `quantity` INT(11) NULL DEFAULT NULL,
  `expensesprice` DOUBLE NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalexpenses` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_EXPENSESDET_01` (`comp` ASC, `expensesno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.expenses_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`expenses_header` (
  `comp` VARCHAR(3) NOT NULL,
  `expensesno` VARCHAR(20) NOT NULL,
  `expensesdate` DATETIME NULL DEFAULT NULL,
  `expensescat` VARCHAR(50) NULL DEFAULT NULL,
  `expensestype` VARCHAR(50) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `purchaseamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `expensesamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `paypaidamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_EXPENSES_01` (`comp` ASC, `expensesno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fiscalperiod
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fiscalperiod` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `financeyear` VARCHAR(10) NULL DEFAULT NULL,
  `financemonth` VARCHAR(10) NULL DEFAULT NULL,
  `actualyear` VARCHAR(10) NULL DEFAULT NULL,
  `actualmonth` VARCHAR(10) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_FISCAL_01` (`comp` ASC, `financeyear` ASC, `financemonth` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.invoice_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`invoice_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `invoiceno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `shipmentno` VARCHAR(20) NULL DEFAULT NULL,
  `shipment_lineno` INT(11) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `unitcost` DOUBLE NULL DEFAULT NULL,
  `unitprice` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `quantity` INT(11) NULL DEFAULT NULL,
  `costprice` DOUBLE NULL DEFAULT NULL,
  `invoiceprice` DOUBLE NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalinvoice` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_INVOICEDET_01` (`comp` ASC, `invoiceno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.invoice_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`invoice_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `invoiceno` VARCHAR(20) NULL DEFAULT NULL,
  `invoicedate` DATETIME NULL DEFAULT NULL,
  `invoicecat` VARCHAR(50) NULL DEFAULT NULL,
  `invoicetype` VARCHAR(50) NULL DEFAULT NULL,
  `invoiceterm` VARCHAR(50) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `salesamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `invoiceamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `payrcptamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_INVOICE_01` (`comp` ASC, `invoiceno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `itemcat` VARCHAR(15) NULL DEFAULT NULL,
  `itemtype` VARCHAR(15) NULL DEFAULT NULL,
  `purchaseprice` DOUBLE NULL DEFAULT NULL,
  `purchasetaxcode` VARCHAR(10) NULL DEFAULT NULL,
  `costprice` DOUBLE NULL DEFAULT NULL,
  `costtaxcode` VARCHAR(10) NULL DEFAULT NULL,
  `salesprice` DOUBLE NULL DEFAULT NULL,
  `salestaxcode` VARCHAR(10) NULL DEFAULT NULL,
  `qtyorder` INT(11) NULL DEFAULT NULL,
  `qtydemand` INT(11) NULL DEFAULT NULL,
  `qtysoh` INT(11) NULL DEFAULT NULL,
  `costsoh` DOUBLE NULL DEFAULT NULL,
  `qtysafetystock` INT(11) NULL DEFAULT NULL,
  `itemstatus` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ITEM_01` (`comp` ASC, `itemno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item_asset
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_asset` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `assetno` VARCHAR(50) NULL DEFAULT NULL,
  `assettyp` VARCHAR(15) NULL DEFAULT NULL,
  `assetcat` VARCHAR(15) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `costori` DOUBLE NULL DEFAULT NULL,
  `deprtyp` VARCHAR(15) NULL DEFAULT NULL,
  `deprrate` DOUBLE NULL DEFAULT NULL,
  `depraccum` DOUBLE NULL DEFAULT NULL,
  `nbv` DOUBLE NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ITEM_ASSET_01` (`comp` ASC, `itemno` ASC, `assetno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item_discount
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_discount` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `ordercat` VARCHAR(15) NULL DEFAULT NULL,
  `ordertype` VARCHAR(15) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `disccat` VARCHAR(10) NULL DEFAULT NULL,
  `discvalue` DOUBLE NULL DEFAULT NULL,
  `status` VARCHAR(15) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ITEMDISC_01` (`comp` ASC, `ordertype` ASC, `itemno` ASC, `ordercat` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item_image
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_image` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `filename` VARCHAR(100) NULL DEFAULT NULL,
  `fileblob` LONGBLOB NULL DEFAULT NULL,
  `imgwidth` INT(11) NULL DEFAULT NULL,
  `imgheight` INT(11) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ITEM_IMAGE` (`comp` ASC, `itemno` ASC, `filename` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item_stock
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_stock` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `qtysoh` INT(11) NULL DEFAULT NULL,
  `costsoh` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ITEM_STOCK_01` (`comp` ASC, `itemno` ASC, `location` ASC, `datesoh` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.item_stock_transactions
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_stock_transactions` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `transtype` VARCHAR(30) NULL DEFAULT NULL,
  `transdate` DATETIME NULL DEFAULT NULL,
  `transno` VARCHAR(20) NULL DEFAULT NULL,
  `trans_lineno` INT(11) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `transqty` INT(11) NULL DEFAULT NULL,
  `transprice` DOUBLE NULL DEFAULT NULL,
  `qtysoh` INT(11) NULL DEFAULT NULL,
  `costsoh` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ITEM_TRANS_01` (`comp` ASC, `itemno` ASC, `location` ASC, `datesoh` ASC, `transno` ASC, `trans_lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.module
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`module` (
  `moduleid` VARCHAR(3) NOT NULL,
  `modulename` VARCHAR(50) NULL DEFAULT NULL,
  `moduledesc` VARCHAR(100) NULL DEFAULT NULL,
  `modulestatus` VARCHAR(1) NULL DEFAULT NULL,
  `moduleicon` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`moduleid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.order_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`order_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `unitprice` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `quantity` INT(11) NULL DEFAULT NULL,
  `orderprice` DOUBLE NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalprice` DOUBLE NULL DEFAULT NULL,
  `deliverqty` INT(11) NULL DEFAULT NULL,
  `invoiceamount` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ORD_DET_01` (`comp` ASC, `orderno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.order_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`order_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `orderdate` DATETIME NULL DEFAULT NULL,
  `ordercat` VARCHAR(15) NULL DEFAULT NULL,
  `orderactivity` VARCHAR(10) NULL DEFAULT NULL,
  `ordertype` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `plandeliverydate` DATETIME NULL DEFAULT NULL,
  `paytype` VARCHAR(15) NULL DEFAULT NULL,
  `salesamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `orderamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `orderremarks` VARCHAR(100) NULL DEFAULT NULL,
  `orderstatus` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreated` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreateddate` DATETIME NULL DEFAULT NULL,
  `orderapproved` VARCHAR(50) NULL DEFAULT NULL,
  `orderapproveddate` DATETIME NULL DEFAULT NULL,
  `ordercancelled` VARCHAR(50) NULL DEFAULT NULL,
  `ordercancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ORD_HDR_01` (`comp` ASC, `orderno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.otherbusinesspartner
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`otherbusinesspartner` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `obpid` VARCHAR(20) NULL DEFAULT NULL,
  `obpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `obpaddress` VARCHAR(100) NULL DEFAULT NULL,
  `obpcontact` VARCHAR(50) NULL DEFAULT NULL,
  `obpreference` VARCHAR(50) NULL DEFAULT NULL,
  `obpcat` VARCHAR(15) NULL DEFAULT NULL,
  `discounttype` VARCHAR(15) NULL DEFAULT NULL,
  `cashguarantee` DOUBLE NULL DEFAULT NULL,
  `bankguarantee` DOUBLE NULL DEFAULT NULL,
  `creditlimit` DOUBLE NULL DEFAULT NULL,
  `obpstatus` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_OBP_01` (`comp` ASC, `obpid` ASC),
  UNIQUE INDEX `IDX_PRI_OBP_02` (`comp` ASC, `obpdesc` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.parameters
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`parameters` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `paramid` VARCHAR(20) NULL DEFAULT NULL,
  `paramtype` VARCHAR(50) NULL DEFAULT NULL,
  `paramcode` VARCHAR(50) NULL DEFAULT NULL,
  `paramdesc` VARCHAR(100) NULL DEFAULT NULL,
  `paramstatus` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PARAMID_KEY01` (`comp` ASC, `paramid` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.paypaid_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`paypaid_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `paypaidno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `expensesno` VARCHAR(20) NULL DEFAULT NULL,
  `expensesdate` DATETIME NULL DEFAULT NULL,
  `paytype` VARCHAR(15) NULL DEFAULT NULL,
  `payrefno` VARCHAR(50) NULL DEFAULT NULL,
  `payremarks` VARCHAR(100) NULL DEFAULT NULL,
  `expensesprice` DOUBLE NULL DEFAULT NULL,
  `paypaidprice` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PAYPAIDDET_01` (`comp` ASC, `paypaidno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.paypaid_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`paypaid_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `paypaidno` VARCHAR(20) NULL DEFAULT NULL,
  `paypaiddate` DATETIME NULL DEFAULT NULL,
  `paypaidtype` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `expensesamount` DOUBLE NULL DEFAULT NULL,
  `paypaidamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PAYPAID_01` (`comp` ASC, `paypaidno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.payrcpt_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`payrcpt_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `payrcptno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `invoiceno` VARCHAR(20) NULL DEFAULT NULL,
  `invoicedate` DATETIME NULL DEFAULT NULL,
  `paytype` VARCHAR(15) NULL DEFAULT NULL,
  `payrefno` VARCHAR(50) NULL DEFAULT NULL,
  `payremarks` VARCHAR(100) NULL DEFAULT NULL,
  `invoiceprice` DOUBLE NULL DEFAULT NULL,
  `payrcptprice` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PAYRCPTDET_01` (`comp` ASC, `payrcptno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.payrcpt_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`payrcpt_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `payrcptno` VARCHAR(20) NULL DEFAULT NULL,
  `payrcptdate` DATETIME NULL DEFAULT NULL,
  `payrcpttype` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `invoiceamount` DOUBLE NULL DEFAULT NULL,
  `payrcptamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PAYRCPT_01` (`comp` ASC, `payrcptno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.people
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`people` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `id` VARCHAR(20) NULL DEFAULT NULL,
  `name` VARCHAR(100) NULL DEFAULT NULL,
  `nokp` VARCHAR(20) NULL DEFAULT NULL,
  `address` VARCHAR(100) NULL DEFAULT NULL,
  `gender` VARCHAR(1) NULL DEFAULT NULL,
  `telno` VARCHAR(20) NULL DEFAULT NULL,
  `email` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PEOPLE` (`comp` ASC, `id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.purchase_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`purchase_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `unitprice` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `quantity` INT(11) NULL DEFAULT NULL,
  `orderprice` DOUBLE NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalprice` DOUBLE NULL DEFAULT NULL,
  `receiptqty` INT(11) NULL DEFAULT NULL,
  `billingamount` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PUR_DET_01` (`comp` ASC, `orderno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.purchase_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`purchase_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `orderdate` DATETIME NULL DEFAULT NULL,
  `ordercat` VARCHAR(15) NULL DEFAULT NULL,
  `orderactivity` VARCHAR(10) NULL DEFAULT NULL,
  `ordertype` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `plandeliverydate` DATETIME NULL DEFAULT NULL,
  `paytype` VARCHAR(15) NULL DEFAULT NULL,
  `purchaseamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `orderamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `orderremarks` VARCHAR(100) NULL DEFAULT NULL,
  `orderstatus` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreated` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreateddate` DATETIME NULL DEFAULT NULL,
  `orderapproved` VARCHAR(50) NULL DEFAULT NULL,
  `orderapproveddate` DATETIME NULL DEFAULT NULL,
  `ordercancelled` VARCHAR(50) NULL DEFAULT NULL,
  `ordercancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_PUR_HDR_01` (`comp` ASC, `orderno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.receipt_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`receipt_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `receiptno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `order_quantity` INT(11) NULL DEFAULT NULL,
  `receipt_quantity` INT(11) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `hasbilling` VARCHAR(1) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_RECEIPT_DET_01` (`comp` ASC, `receiptno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.receipt_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`receipt_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `receiptno` VARCHAR(20) NULL DEFAULT NULL,
  `receiptdate` DATETIME NULL DEFAULT NULL,
  `receiptcat` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_RECEIPT_01` (`comp` ASC, `receiptno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.role
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`role` (
  `roleid` VARCHAR(2) NOT NULL,
  `rolename` VARCHAR(50) NULL DEFAULT NULL,
  `roledesc` VARCHAR(100) NULL DEFAULT NULL,
  `rolestatus` VARCHAR(1) NULL DEFAULT NULL,
  PRIMARY KEY (`roleid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.role_module
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`role_module` (
  `roleid` VARCHAR(2) NULL DEFAULT NULL,
  `moduleid` VARCHAR(3) NULL DEFAULT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ROLE_MODULE_KEY` (`roleid` ASC, `moduleid` ASC, `comp` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.role_screen
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`role_screen` (
  `roleid` VARCHAR(2) NULL DEFAULT NULL,
  `screenid` VARCHAR(6) NULL DEFAULT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ROLE_SCREEN_KEY` (`roleid` ASC, `screenid` ASC, `comp` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.role_submodule
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`role_submodule` (
  `roleid` VARCHAR(2) NULL DEFAULT NULL,
  `moduleid` VARCHAR(3) NULL DEFAULT NULL,
  `submoduleid` VARCHAR(6) NULL DEFAULT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_ROLE_SUBMODULE_KEY` (`roleid` ASC, `moduleid` ASC, `submoduleid` ASC, `comp` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.running_number
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`running_number` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `type` VARCHAR(50) NULL DEFAULT NULL,
  `initial` VARCHAR(6) NULL DEFAULT NULL,
  `year` VARCHAR(4) NULL DEFAULT NULL,
  `runno` INT(11) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_RUNNO_01` (`comp` ASC, `type` ASC, `initial` ASC, `year` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.screen
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`screen` (
  `screenid` VARCHAR(6) NOT NULL,
  `screenfilename` VARCHAR(50) NULL DEFAULT NULL,
  `screendesc` VARCHAR(100) NULL DEFAULT NULL,
  `screenstatus` VARCHAR(1) NULL DEFAULT NULL,
  PRIMARY KEY (`screenid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.shipment_charge
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`shipment_charge` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `shipmentno` VARCHAR(20) NULL DEFAULT NULL,
  `chargetype` VARCHAR(15) NULL DEFAULT NULL,
  `chargeamount` DOUBLE NULL DEFAULT NULL,
  `chargeremarks` VARCHAR(100) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_SHIPMENTCHARGE_01` (`comp` ASC, `shipmentno` ASC, `chargetype` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.shipment_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`shipment_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `shipmentno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `order_quantity` INT(11) NULL DEFAULT NULL,
  `shipment_quantity` INT(11) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `hasinvoice` VARCHAR(1) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_SHIPMENTDET_01` (`comp` ASC, `shipmentno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.shipment_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`shipment_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `shipmentno` VARCHAR(20) NULL DEFAULT NULL,
  `shipmentdate` DATETIME NULL DEFAULT NULL,
  `shipmentcat` VARCHAR(15) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(200) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_SHIPMENT_01` (`comp` ASC, `shipmentno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.stitch_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`stitch_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `stitchno` VARCHAR(20) NULL DEFAULT NULL,
  `stitchdate` DATETIME NULL DEFAULT NULL,
  `peopleid` VARCHAR(20) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `measurement` VARCHAR(10) NULL DEFAULT NULL,
  `baju_bahu` VARCHAR(10) NULL DEFAULT NULL,
  `baju_labuh_lengan` VARCHAR(10) NULL DEFAULT NULL,
  `baju_labuh_baju` VARCHAR(10) NULL DEFAULT NULL,
  `baju_dada` VARCHAR(10) NULL DEFAULT NULL,
  `baju_pinggang` VARCHAR(10) NULL DEFAULT NULL,
  `baju_punggung` VARCHAR(10) NULL DEFAULT NULL,
  `baju_labuh_kain` VARCHAR(10) NULL DEFAULT NULL,
  `baju_leher` VARCHAR(10) NULL DEFAULT NULL,
  `baju_span` VARCHAR(10) NULL DEFAULT NULL,
  `baju_bahu_dada` VARCHAR(10) NULL DEFAULT NULL,
  `baju_bahu_pinggang` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_pinggang` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_punggung` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_cawat` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_paha` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_lutut` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_bukaan_kaki` VARCHAR(10) NULL DEFAULT NULL,
  `seluar_labuh_seluar` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_STITCH` (`comp` ASC, `stitchno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.stockstate_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`stockstate_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `stockstateno` VARCHAR(20) NULL DEFAULT NULL,
  `stockstatetype` VARCHAR(50) NULL DEFAULT NULL,
  `transno` VARCHAR(20) NULL DEFAULT NULL,
  `trans_lineno` INT(11) NULL DEFAULT NULL,
  `transdate` DATETIME NULL DEFAULT NULL,
  `transtype` VARCHAR(30) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `order_lineno` INT(11) NULL DEFAULT NULL,
  `transprice` DOUBLE NULL DEFAULT NULL,
  `transqty` INT(11) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_STOCKSTATE_DET_01` (`comp` ASC, `stockstateno` ASC, `transno` ASC, `trans_lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.stockstate_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`stockstate_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `stockstateno` VARCHAR(20) NULL DEFAULT NULL,
  `openingdate` DATETIME NULL DEFAULT NULL,
  `openingtype` VARCHAR(50) NULL DEFAULT NULL,
  `stockopeningamount` DOUBLE NULL DEFAULT NULL,
  `stockinamount` DOUBLE NULL DEFAULT NULL,
  `stockoutamount` DOUBLE NULL DEFAULT NULL,
  `closingdate` DATETIME NULL DEFAULT NULL,
  `closingtype` VARCHAR(50) NULL DEFAULT NULL,
  `stockclosingamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_STOCKSTATE_HDR_01` (`comp` ASC, `stockstateno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.stockstate_soh
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`stockstate_soh` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `stockstateno` VARCHAR(20) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `datesoh` DATETIME NULL DEFAULT NULL,
  `qtysoh` INT(11) NULL DEFAULT NULL,
  `costsoh` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_STOCKSTATE_SOH_01` (`comp` ASC, `stockstateno` ASC, `itemno` ASC, `location` ASC, `datesoh` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.submodule
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`submodule` (
  `submoduleid` VARCHAR(6) NOT NULL,
  `moduleid` VARCHAR(3) NULL DEFAULT NULL,
  `submodulename` VARCHAR(50) NULL DEFAULT NULL,
  `submoduledesc` VARCHAR(100) NULL DEFAULT NULL,
  `submodulestatus` VARCHAR(1) NULL DEFAULT NULL,
  PRIMARY KEY (`submoduleid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.tax
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`tax` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_TAX_01` (`comp` ASC, `taxcode` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.transfer_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`transfer_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `orderno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `itemno` VARCHAR(30) NULL DEFAULT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `unitprice` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `quantity` INT(11) NULL DEFAULT NULL,
  `orderprice` DOUBLE NULL DEFAULT NULL,
  `taxcode` VARCHAR(10) NULL DEFAULT NULL,
  `taxrate` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalprice` DOUBLE NULL DEFAULT NULL,
  `deliverqty` INT(11) NULL DEFAULT NULL,
  `invoiceamount` DOUBLE NULL DEFAULT NULL,
  `receiptqty` INT(11) NULL DEFAULT NULL,
  `billingamount` DOUBLE NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_TRA_DET_01` (`comp` ASC, `orderno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.transfer_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`transfer_header` (
  `comp` VARCHAR(3) NOT NULL,
  `orderno` VARCHAR(20) NOT NULL,
  `orderdate` DATETIME NOT NULL,
  `shipmentdate` DATETIME NULL DEFAULT NULL,
  `receiptdate` DATETIME NULL DEFAULT NULL,
  `ordercat` VARCHAR(15) NULL DEFAULT NULL,
  `orderactivity` VARCHAR(10) NULL DEFAULT NULL,
  `pricetype` VARCHAR(15) NULL DEFAULT NULL,
  `ordertype` VARCHAR(15) NOT NULL,
  `compfrom` VARCHAR(3) NULL DEFAULT NULL,
  `compto` VARCHAR(3) NULL DEFAULT NULL,
  `transferamount` DOUBLE NULL DEFAULT NULL,
  `discamount` DOUBLE NULL DEFAULT NULL,
  `orderamount` DOUBLE NULL DEFAULT NULL,
  `taxamount` DOUBLE NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `orderremarks` VARCHAR(100) NULL DEFAULT NULL,
  `orderstatus` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreated` VARCHAR(50) NULL DEFAULT NULL,
  `ordercreateddate` DATETIME NULL DEFAULT NULL,
  `orderapproved` VARCHAR(50) NULL DEFAULT NULL,
  `orderapproveddate` DATETIME NULL DEFAULT NULL,
  `ordercancelled` VARCHAR(50) NULL DEFAULT NULL,
  `ordercancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_TRANSFER_PRI_01` (`comp` ASC, `orderno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.user_role
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`user_role` (
  `userid` VARCHAR(50) NULL DEFAULT NULL,
  `roleid` VARCHAR(2) NULL DEFAULT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_USER_ROLE_KEY` (`userid` ASC, `roleid` ASC, `comp` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.userprofile
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`userprofile` (
  `userid` VARCHAR(50) NOT NULL,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `screenid` VARCHAR(6) NULL DEFAULT NULL,
  `userpwd` VARCHAR(50) NULL DEFAULT NULL,
  `username` VARCHAR(100) NULL DEFAULT NULL,
  `useradd` VARCHAR(200) NULL DEFAULT NULL,
  `usertelno` VARCHAR(15) NULL DEFAULT NULL,
  `usertype` VARCHAR(2) NULL DEFAULT NULL,
  `userstatus` VARCHAR(1) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`userid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.zakatcalculation_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`zakatcalculation_details` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `zakatcalculationno` VARCHAR(20) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `adjustmentno` VARCHAR(20) NULL DEFAULT NULL,
  `adjustmenttype` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `totalamount` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ZAKAT_DET_01` (`comp` ASC, `zakatcalculationno` ASC, `adjustmentno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.zakatcalculation_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`zakatcalculation_header` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `zakatcalculationno` VARCHAR(20) NULL DEFAULT NULL,
  `openingdate` DATETIME NULL DEFAULT NULL,
  `openingtype` VARCHAR(50) NULL DEFAULT NULL,
  `bankopeningamount` DOUBLE NULL DEFAULT NULL,
  `cashopeningamount` DOUBLE NULL DEFAULT NULL,
  `bankpaymentreceiptamount` DOUBLE NULL DEFAULT NULL,
  `cashpaymentreceiptamount` DOUBLE NULL DEFAULT NULL,
  `bankpaymentpaidamount` DOUBLE NULL DEFAULT NULL,
  `cashpaymentpaidamount` DOUBLE NULL DEFAULT NULL,
  `bankclosingamount` DOUBLE NULL DEFAULT NULL,
  `cashclosingamount` DOUBLE NULL DEFAULT NULL,
  `stockopeningamount` DOUBLE NULL DEFAULT NULL,
  `stockinamount` DOUBLE NULL DEFAULT NULL,
  `stockoutamount` DOUBLE NULL DEFAULT NULL,
  `stockclosingamount` DOUBLE NULL DEFAULT NULL,
  `pendingreceiptamount` DOUBLE NULL DEFAULT NULL,
  `pendingpaidamount` DOUBLE NULL DEFAULT NULL,
  `zakatnisabamount` DOUBLE NULL DEFAULT NULL,
  `zakatrate` DOUBLE NULL DEFAULT NULL,
  `sharepercentage` DOUBLE NULL DEFAULT NULL,
  `closingdate` DATETIME NULL DEFAULT NULL,
  `closingtype` VARCHAR(50) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ZAKATCALCULATION_01` (`comp` ASC, `zakatcalculationno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;



-- ----------------------------------------------------------------------------
-- Routine bioappdb.createcompany
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `createcompany`(param_comp varchar(20), param_comp_name varchar(100),  param_comp_id varchar(50), param_comp_accountbank varchar(100),
							  param_comp_accountno varchar(50), param_comp_address varchar(100), param_comp_contact varchar(50), param_comp_contactno varchar(20), 
                              param_comp_website varchar(50), param_comp_email varchar(50), param_comp_icon varchar(100), param_comp_logo1 varchar(100), 
                              param_comp_logo2 varchar(100), param_status varchar(50), param_createdby varchar(50), param_confirmedby varchar(50), param_year varchar(4)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer DEFAULT 0;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM comp_details WHERE upper(comp) = upper(param_comp) or upper(comp_name) = upper(param_comp_name) or upper(comp_id) = upper(param_comp_id));
    IF recordnotfound = 1 THEN
	BEGIN
		/*create comp details*/
		insert into comp_details (comp, comp_name, comp_id, comp_accountbank, comp_accountno, comp_address, comp_contact, comp_contactno, comp_website, comp_email, comp_icon, comp_logo1, comp_logo2, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, param_comp_name, param_comp_id, param_comp_accountbank, param_comp_accountno, param_comp_address, param_comp_contact, param_comp_contactno, param_comp_website, param_comp_email, param_comp_icon, param_comp_logo1, param_comp_logo2, param_status, param_createdby, now(), param_confirmedby, now());
        
        /*create running no*/
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ADJUSTMENT_ORDER','AD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'BUSINESS_PARTNER','BP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'CASH_FLOW','CF',param_year,1,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'EXPENSES','PV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'INVOICE','INV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'OTHER_BUSINESS_PARTNER','OBP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_PAID','PPD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_RECEIPT','PRC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PURCHASE_ORDER','PO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIPT','GR',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SALES_ORDER','SO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SHIPMENT','DO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STOCK_STATEMENT','ST',param_year,1,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'WORK_ORDER','WO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PEOPLE','PP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STITCH','ST',param_year,0,'ACTIVE');
		/***1)insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET','AST',param_year,0,'ACTIVE');*/
        /*
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIVE_ORDER','RC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'GIVE_ORDER','GV',param_year,0,'ACTIVE');
        */
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'TRANSFER_ORDER','TF',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ZAKAT_CALCULATION','ZK',param_year,1,'ACTIVE');

		/*create tax code */
		INSERT INTO tax (comp, taxcode, taxrate) VALUES (param_comp, 'NA', 0);
		INSERT INTO tax (comp, taxcode, taxrate) VALUES (param_comp, 'SST', 6);

		/*create default Business Partner*/
		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP000000', 'STAKEHOLDERS', param_comp_address, concat('PEJABAT',' ', param_comp_name), '', 'INTERNAL', 'NORMAL', 0, 0, 0, 'ACTIVE');

		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP999999', 'OTHER', '', '', '', 'OTHER', 'NORMAL', 0, 0, 0, 'ACTIVE');

		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP000001', 'TUNAI TANGAN', param_comp_address, concat('PEJABAT',' ', param_comp_name), 'TUNAI TANGAN', 'INTERNAL', 'NORMAL', 0, 0, 0, 'ACTIVE');

		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP000002', 'AKAUN BANK', param_comp_address, concat('PEJABAT',' ', param_comp_name), 'AKAUN BANK', 'INTERNAL', 'NORMAL', 0, 0, 0, 'ACTIVE');

		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP000003', 'PELANGGAN WALK-IN', 'N/A', 'N/A', '', 'OTHER', 'NORMAL', 0, 0, 0, 'ACTIVE');

		INSERT INTO businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
		VALUES (param_comp, 'BP000004', 'PELANGGAN ON-LINE', 'N/A', 'N/A', '', 'OTHER', 'NORMAL', 0, 0, 0, 'ACTIVE');

		/*create default location*/
        
		insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
		values (param_comp, concat('PAR',param_year,'00001'), 'STOCK_LOCATION',concat('STOCK_LOC_',param_comp,'001'), 'STOR', 'ACTIVE', 'sysadmin', now());
        
		insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
		values (param_comp, concat('PAR',param_year,'00002'), 'STOCK_LOCATION',concat('STOCK_LOC_',param_comp,'002'), 'SEMENTARA', 'ACTIVE', 'sysadmin', now());

		insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
		values (param_comp, concat('PAR',param_year,'00003'), 'STOCK_LOCATION',concat('STOCK_LOC_',param_comp,'003'), 'TRANSIT', 'ACTIVE', 'sysadmin', now());
        
        /*
		insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
		values (param_comp, concat('PAR',param_year,'00004'), 'STOCK_LOCATION',concat('STOCK_LOC_',param_comp,'004'), 'PUSAT BEKALAN', 'ACTIVE', 'sysadmin', now());

		insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
		values (param_comp, concat('PAR',param_year,'00005'), 'STOCK_LOCATION',concat('STOCK_LOC_',param_comp,'005'), 'JOM SADAQAH', 'ACTIVE', 'sysadmin', now());		
		*/
		
		insert into cashflow_header(comp, cashflowno, openingdate, openingtype, status, createdby, createddate)
		values (param_comp, CONCAT('CF',param_year,'0001'), sysdate(), 'BEGIN', 'IN-PROGRESS', param_createdby, sysdate());

		insert into stockstate_header(comp, stockstateno, openingdate, openingtype, status, createdby, createddate)
		values (param_comp, CONCAT('ST',param_year,'0001'), sysdate(), 'BEGIN', 'IN-PROGRESS', param_createdby, sysdate());
		
		insert into zakatcalculation_header(comp, zakatcalculationno, openingdate, openingtype, status, createdby, createddate)
		values (param_comp, CONCAT('ZK',param_year,'0001'), sysdate(), 'BEGIN', 'IN-PROGRESS', param_createdby, sysdate());

        /*create role module*/
		insert into role_module(roleid, moduleid, comp) values('01','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','040',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','060',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','070',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','080',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','090',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','100',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','110',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','120',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','130',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','140',param_comp);
        /*
		insert into role_module(roleid, moduleid, comp) values('01','160',param_comp);
		insert into role_module(roleid, moduleid, comp) values('01','170',param_comp);
		*/
		insert into role_module(roleid, moduleid, comp) values('01','180',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('02','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','040',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','060',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','070',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','080',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','090',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','100',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','110',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','120',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','130',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','140',param_comp);
        /*
		insert into role_module(roleid, moduleid, comp) values('02','160',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','170',param_comp);
		*/
		insert into role_module(roleid, moduleid, comp) values('02','180',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('03','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','040',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','060',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','070',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('04','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('04','070',param_comp);
		insert into role_module(roleid, moduleid, comp) values('04','080',param_comp);
		insert into role_module(roleid, moduleid, comp) values('04','120',param_comp);
		insert into role_module(roleid, moduleid, comp) values('04','130',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('05','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','060',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','100',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','110',param_comp);
        /*
		insert into role_module(roleid, moduleid, comp) values('05','160',param_comp);
		insert into role_module(roleid, moduleid, comp) values('05','170',param_comp);
        */
		insert into role_module(roleid, moduleid, comp) values('05','180',param_comp);
        
        /*create role sub module*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','010','010010',param_comp);
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','010','010020',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','010','010030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','020','020010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','020','020020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','030','030010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','030','030020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','040','040010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','040','040020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','040','040090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','060','060010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','060','060020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','060','060090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','070','070010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','070','070020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','070','070090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','080','080010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','080','080020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','090','090010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','090','090020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','090','090090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','100','100010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','100','100020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','100','100090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110050',param_comp);
		/***2)insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','110','110060',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','120','120010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','120','120020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','120','120090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','130','130010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','130','130020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140050',param_comp); 
        /*
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','160','160010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','160','160020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','160','160090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','170','170010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','170','170020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','170','170090',param_comp);
        */
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','180','180010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','180','180020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','180','180090',param_comp); 
        
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','010','010010',param_comp);
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','010','010020',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','010','010030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','020','020010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','020','020020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','030','030010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','030','030020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','040','040010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','040','040020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','040','040090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','060','060010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','060','060020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','060','060090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','070','070010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','070','070020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','070','070090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','080','080010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','080','080020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','090','090010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','090','090020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','090','090090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','100','100010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','100','100020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','100','100090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110050',param_comp);
		/***3)insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','110','110060',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','120','120010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','120','120020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','120','120090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','130','130010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','130','130020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140050',param_comp);
        /*
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','160','160010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','160','160020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','160','160090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','170','170010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','170','170020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','170','170090',param_comp);
        */
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','180','180010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','180','180020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','180','180090',param_comp);
        
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','010','010010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','020','020010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','020','020020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','030','030010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','030','030020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','060','060010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','060','060020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','060','060090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','070','070010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','070','070020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','070','070090',param_comp);
        
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','010','010010',param_comp);*/
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','010','010020',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','010','010030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','070','070010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','070','070020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','070','070090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','080','080010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','080','080020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','120','120010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','120','120020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','120','120090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','130','130010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('04','130','130020',param_comp);
        
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','010','010010',param_comp);*/
		/*insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','010','010020',param_comp);*/
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','010','010030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','020','020010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','020','020020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','030','030010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','030','030020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','060','060010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','060','060020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','060','060090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','100','100010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','100','100020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','100','100090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','110','110010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','110','110020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','110','110030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','110','110040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','110','110050',param_comp);
        /*
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','160','160010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','160','160020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','160','160090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','170','170010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','170','170020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','170','170090',param_comp);
        */
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','180','180010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','180','180020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('05','180','180090',param_comp);
        
		/*create role screen*/
		insert into role_screen(roleid, screenid, comp) values('01','010010',param_comp);
		/*insert into role_screen(roleid, screenid, comp) values('01','010020',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('01','010030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','020020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','030010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','030020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','040010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','040020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','040090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','060010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','060020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','060090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','070010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','070020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','070090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','080010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','080020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','090010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','090020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','090090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','100010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','100020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','100090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','110010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','110020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','110030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','110040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','110050',param_comp);
		/***4)insert into role_screen(roleid, screenid, comp) values('01','110060',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('01','120010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','120020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','120090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','130010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','130020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140050',param_comp);
        /*
		insert into role_screen(roleid, screenid, comp) values('01','160010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','160020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','160090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','170010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','170020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','170090',param_comp);
        */
		insert into role_screen(roleid, screenid, comp) values('01','180010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','180020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','180090',param_comp);
        
		insert into role_screen(roleid, screenid, comp) values('02','010010',param_comp);
		/*insert into role_screen(roleid, screenid, comp) values('02','010020',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('02','010030',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','020020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','030010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','030020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','040010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','040020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','040090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','060010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','060020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','060090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','070010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','070020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','070090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','080010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','080020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','090010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','090020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','090090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','100010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','100020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','100090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','110010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','110020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','110030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','110040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','110050',param_comp);
		/***5)insert into role_screen(roleid, screenid, comp) values('02','110060',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('02','120010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','120020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','120090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','130010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','130020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140050',param_comp);
        /*
		insert into role_screen(roleid, screenid, comp) values('02','160010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','160020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','160090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','170010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','170020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','170090',param_comp);
        */
		insert into role_screen(roleid, screenid, comp) values('02','180010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','180020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','180090',param_comp);
        
        insert into role_screen(roleid, screenid, comp) values('03','010010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','020020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','030010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','030020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','060010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','060020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','060090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','070010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','070020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','070090',param_comp);
        
        /*insert into role_screen(roleid, screenid, comp) values('04','010010',param_comp);)/
		/*insert into role_screen(roleid, screenid, comp) values('04','010020',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('04','010030',param_comp);
        /*
		insert into role_screen(roleid, screenid, comp) values('04','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','020020',param_comp);
        */
		insert into role_screen(roleid, screenid, comp) values('04','070010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','070020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','070090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','080010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','080020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','120010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','120020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','120090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','130010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('04','130020',param_comp);
        
        /*insert into role_screen(roleid, screenid, comp) values('05','010010',param_comp);)/
		/*insert into role_screen(roleid, screenid, comp) values('05','010020',param_comp);*/
		insert into role_screen(roleid, screenid, comp) values('05','010030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','020020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','030010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','030020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','060010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','060020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','060090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','100010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','100020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','100090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','110010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','110020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','110030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','110040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','110050',param_comp);
        /*
		insert into role_screen(roleid, screenid, comp) values('05','160010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','160020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','160090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','170010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','170020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','170090',param_comp);
        */
		insert into role_screen(roleid, screenid, comp) values('05','180010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','180020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('05','180090',param_comp);

		/*create fiscalperiod*/
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON01',param_year,'01');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON02',param_year,'02');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON03',param_year,'03');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON04',param_year,'04');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON05',param_year,'05');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON06',param_year,'06');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON07',param_year,'07');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON08',param_year,'08');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON09',param_year,'09');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON10',param_year,'10');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON11',param_year,'11');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON12',param_year,'12');

		/*create dashboard revenue*/
		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
		/*create dashboard expenses*/
		INSERT INTO bioappdb.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard sales*/
		INSERT INTO bioappdb.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard collection*/
		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'CASHCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'OTHERCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard stock*/
		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
        SET result = 1;

	END;
	ELSE
	BEGIN
		SET result = 0;
            
	END;
	END IF;    
    

RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.createfiscalyeardashboard
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `createfiscalyeardashboard`(param_comp varchar(3), param_year varchar(4)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer DEFAULT 0;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM comp_details WHERE upper(comp) = upper(param_comp));
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;
	END;
    ELSE
    BEGIN

		/*create fiscalperiod*/
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON01',param_year,'01');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON02',param_year,'02');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON03',param_year,'03');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON04',param_year,'04');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON05',param_year,'05');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON06',param_year,'06');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON07',param_year,'07');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON08',param_year,'08');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON09',param_year,'09');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON10',param_year,'10');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON11',param_year,'11');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON12',param_year,'12');

		/*create running no*/
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ADJUSTMENT_ORDER','AD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'BUSINESS_PARTNER','BP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'CASH_FLOW','CF',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'EXPENSES','PV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'INVOICE','INV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'OTHER_BUSINESS_PARTNER','OBP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_PAID','PPD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_RECEIPT','PRC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PURCHASE_ORDER','PO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIPT','GR',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SALES_ORDER','SO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SHIPMENT','DO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STOCK_STATEMENT','ST',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'WORK_ORDER','WO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PEOPLE','PP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STITCH','ST',param_year,0,'ACTIVE');
		/***1)insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET','AST',param_year,0,'ACTIVE');*/
        /*
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIVE_ORDER','RC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'GIVE_ORDER','GV',param_year,0,'ACTIVE');
        */
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'TRANSFER_ORDER','TF',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ZAKAT_CALCULATION','ZK',param_year,0,'ACTIVE');
		
        /*create dashboard revenue*/
		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
		/*create dashboard expenses*/
		INSERT INTO bioappdb.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard sales*/
		INSERT INTO bioappdb.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard collection*/
		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'CASHCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'OTHERCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard stock*/
		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		SET result = 1;
    END;
    END IF;
RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.createuser
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `createuser`(param_comp varchar(3), param_userid varchar(50), param_userpwd varchar(50), param_username varchar(100), param_useradd varchar(200), param_usertelno varchar(15), param_usertype varchar(2), param_userstatus varchar(1), param_screenid varchar(6), param_roleid varchar(2), param_createdby varchar(50)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM userprofile WHERE userid = param_userid);
    IF recordnotfound = 1 THEN
	BEGIN
		/*create user profile*/
		INSERT INTO userprofile (userid, comp, screenid, userpwd, username, useradd, usertelno, usertype, userstatus, createdby, createddate)
		VALUES (param_userid, param_comp, param_screenid, param_userpwd, param_username, param_useradd, param_usertelno, param_usertype, param_userstatus, param_createdby, now());
        
        /*create user role*/
		INSERT INTO user_role (comp, userid, roleid)
		VALUES(param_comp, param_userid, param_roleid); 
        
	END;
	ELSE
	BEGIN
		set recordnotfound = (SELECT isnull(max(1)) FROM user_role WHERE comp = param_comp AND userid = param_userid);
		IF recordnotfound = 1 THEN
        BEGIN
			update userprofile
            set	   comp = param_comp
            where  userid = param_userid;
        
			INSERT INTO user_role (comp, userid, roleid)
			VALUES(param_comp, param_userid, param_roleid); 
			
        END;
		ELSE
        BEGIN
			SET result = 0;

        END;
		END IF;
	END;
	END IF;    
    
    RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.deleteuser
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `deleteuser`(param_comp varchar(3), param_userid varchar(50)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM user_role WHERE comp = param_comp and  userid = param_userid);
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;        
        
	END;
	ELSE
	BEGIN
		delete from user_role WHERE comp = param_comp and  userid = param_userid;
        
		set recordnotfound = (SELECT isnull(max(1)) FROM user_role WHERE userid = param_userid);
		IF recordnotfound = 1 THEN
        BEGIN
			update userprofile
			set    comp = 'T01'
			where  userid = param_userid;
			
        END;
		ELSE
		BEGIN
			update userprofile
			set    comp = (select max(comp) from user_role WHERE userid = param_userid)
			where  userid = param_userid;
		END;
        END IF;

		SET result = 1;        

    END;
    END IF;

RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.insertuser
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `insertuser`(param_comp varchar(3), param_userid varchar(50), param_roleid varchar(2)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM user_role WHERE comp = param_comp AND userid = param_userid);
		IF recordnotfound = 1 THEN
        BEGIN
			INSERT INTO user_role (comp, userid, roleid)
			VALUES(param_comp, param_userid, param_roleid); 
			
        END;
		ELSE
        BEGIN
			SET result = 0;

        END;
		END IF;
	RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.removecompany
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `removecompany`(param_comp varchar(3)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer DEFAULT 0;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM comp_details WHERE upper(comp) = upper(param_comp));
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;
	END;
    ELSE
    BEGIN
		/*delete data in adjustment*/
        delete from adjustment_header where comp = param_comp;
        delete from adjustment_details where comp = param_comp;
        
        /*delete data in business partner*/
        delete from businesspartner where comp = param_comp;
        delete from otherbusinesspartner where comp = param_comp;
        
		/*delete data in cashflow*/
        delete from cashflow_header where comp = param_comp;
        delete from cashflow_details where comp = param_comp;
        
        /*delete data in counter*/
        delete from counter_master where comp = param_comp;
        delete from counter_transaction where comp = param_comp;
        delete from counter_transaction where comp = param_comp;
        
        /*delete data in expenses*/
        delete from expenses_header where comp = param_comp;
        delete from expenses_details where comp = param_comp;
        
        /*delete data in invoice*/
        delete from invoice_header where comp = param_comp;
        delete from invoice_details where comp = param_comp;
        
        /*delete data in item*/
        delete from item where comp = param_comp;
        delete from item_asset where comp = param_comp;
        delete from item_discount where comp = param_comp;
        delete from item_image where comp = param_comp;
        delete from item_stock where comp = param_comp;
        delete from item_stock_transactions where comp = param_comp;
        
        /*delete data in order*/
        delete from order_header where comp = param_comp;
        delete from order_details where comp = param_comp;
        
        /*delete data in payment receipt*/
        delete from payrcpt_header where comp = param_comp;
        delete from payrcpt_details where comp = param_comp;
        
        /*delete data in shipment*/
        delete from shipment_header where comp = param_comp;
        delete from shipment_details where comp = param_comp;
        delete from shipment_charge where comp = param_comp;

        /*delete data in purchase order*/
        delete from purchase_header where comp = param_comp;
        delete from purchase_details where comp = param_comp;
        
        /*delete data in payment paid*/
        delete from paypaid_header where comp = param_comp;
        delete from paypaid_details where comp = param_comp;
        
        /*delete data in receipt*/
        delete from receipt_header where comp = param_comp;
        delete from receipt_details where comp = param_comp;
        
        /*delete data in stock state*/
        delete from stockstate_header where comp = param_comp;
        delete from stockstate_details where comp = param_comp;
        delete from stockstate_soh where comp = param_comp;

        /*delete data in transfer*/
        delete from transfer_header where comp = param_comp;
        delete from transfer_details where comp = param_comp;
        
        /*delete data in zakat*/
        delete from zakatcalculation_header where comp = param_comp;
        delete from zakatcalculation_details where comp = param_comp;

        /*delete comp details*/
        delete from comp_details where comp = param_comp;
    
		/*delete running no*/
        delete from running_number where comp = param_comp;

		/*delete tax*/
        delete from tax where comp = param_comp;
    
		/*delete business partner*/
        delete from businesspartner where comp = param_comp;
    
		/*delete role parameter*/
        delete from parameters where comp = param_comp;
        
		/*delete role module*/
        delete from role_module  where comp = param_comp;

		/*delete role sub module*/
        delete from role_submodule where comp = param_comp;
    
		/*delete role screen*/
		delete from role_screen where comp = param_comp;
        
		/*delete fiscalyear*/
        delete from fiscalperiod where comp = param_comp;
    
		/*delete dashboard*/
        delete from dashboard_revenue where comp = param_comp;
        delete from dashboard_expenses where comp = param_comp;
        delete from dashboard_sales where comp = param_comp;
        delete from dashboard_collection where comp = param_comp;
        delete from dashboard_slot where comp = param_comp;
        delete from dashboard_stockin where comp = param_comp;
        delete from dashboard_stockout where comp = param_comp;
        
	END;
    END IF;
RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.removeuser
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `removeuser`(param_userid varchar(50)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM userprofile WHERE userid = param_userid);
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;        
        
	END;
	ELSE
	BEGIN
		delete from user_role where userid = param_userid;
        
        delete from userprofile WHERE userid = param_userid;

		SET result = 1;        

    END;
    END IF;

RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.resetcompany
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `resetcompany`(param_comp varchar(3), param_year varchar(4)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer DEFAULT 0;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM comp_details WHERE upper(comp) = upper(param_comp));
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;
	END;
    ELSE
    BEGIN
    
		/*delete data in adjustment*/
        delete from adjustment_details where comp = param_comp and adjustmentno in (select adjustmentno from adjustment_header where comp = param_comp and year(createddate) = param_year);
        delete from adjustment_header where comp = param_comp and year(createddate) = param_year;
               
		/*delete data in cashflow*/
        delete from cashflow_details where comp = param_comp and cashflowno in (select cashflowno from cashflow_header where comp = param_comp and year(createddate) = param_year);
        delete from cashflow_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in expenses*/
        delete from expenses_details where comp = param_comp and expensesno in (select expensesno from expenses_header where comp = param_comp and year(createddate) = param_year);
        delete from expenses_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in invoice*/
        delete from invoice_details where comp = param_comp and invoiceno in (select invoiceno from invoice_header where comp = param_comp and year(createddate) = param_year);
        delete from invoice_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in order*/
        delete from order_details where comp = param_comp and orderno in (select orderno from order_header where comp = param_comp and year(ordercreateddate) = param_year);
        delete from order_header where comp = param_comp and year(ordercreateddate) = param_year;
        
        /*delete data in payment receipt*/
        delete from payrcpt_details where comp = param_comp and payrcptno in (select payrcptno from payrcpt_header where comp = param_comp and year(createddate) = param_year);
        delete from payrcpt_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in shipment*/
        delete from shipment_details where comp = param_comp and shipmentno in (select shipmentno from shipment_header where comp = param_comp and year(createddate) = param_year);
        delete from shipment_charge where comp = param_comp and shipmentno in (select shipmentno from shipment_header where comp = param_comp and year(createddate) = param_year);
        delete from shipment_header where comp = param_comp and year(createddate) = param_year;

        /*delete data in purchase order*/
        delete from purchase_details where comp = param_comp and orderno in (select orderno from purchase_header where comp = param_comp and year(ordercreateddate) = param_year);
        delete from purchase_header where comp = param_comp and year(ordercreateddate) = param_year;
        
        /*delete data in payment paid*/
        delete from paypaid_details where comp = param_comp and paypaidno in (select paypaidno from paypaid_header where comp = param_comp and year(createddate) = param_year);
        delete from paypaid_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in receipt*/
        delete from receipt_details where comp = param_comp and receiptno in (select receiptno from receipt_header where comp = param_comp and year(createddate) = param_year);
        delete from receipt_header where comp = param_comp and year(createddate) = param_year;
        
        /*delete data in stock state*/
        delete from stockstate_details where comp = param_comp and stockstateno in (select stockstateno from stockstate_header where comp = param_comp and year(createddate) = param_year);
        delete from stockstate_soh where comp = param_comp and stockstateno in (select stockstateno from stockstate_header where comp = param_comp and year(createddate) = param_year);
        delete from stockstate_header where comp = param_comp and year(createddate) = param_year;
        
		/*delete dashboard*/
        delete from dashboard_revenue where comp = param_comp and fyr = param_year;
        delete from dashboard_expenses where comp = param_comp and fyr = param_year;
        delete from dashboard_sales where comp = param_comp and fyr = param_year;
        delete from dashboard_collection where comp = param_comp and fyr = param_year;
        delete from dashboard_slot where comp = param_comp and fyr = param_year;
        delete from dashboard_stockin where comp = param_comp and fyr = param_year;
        delete from dashboard_stockout where comp = param_comp and fyr = param_year;
        
        delete from running_number where comp = param_comp and year = param_year;
        
        delete from fiscalperiod where comp = param_comp and financeyear = param_year;
        
		/*create fiscalperiod*/
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON01',param_year,'01');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON02',param_year,'02');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON03',param_year,'03');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON04',param_year,'04');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON05',param_year,'05');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON06',param_year,'06');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON07',param_year,'07');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON08',param_year,'08');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON09',param_year,'09');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON10',param_year,'10');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON11',param_year,'11');
		insert into fiscalperiod (comp, financeyear, financemonth, actualyear, actualmonth) values(param_comp,param_year,'MON12',param_year,'12');        

        /*create running no*/
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ADJUSTMENT_ORDER','AD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'BUSINESS_PARTNER','BP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'CASH_FLOW','CF',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'EXPENSES','PV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'INVOICE','INV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'OTHER_BUSINESS_PARTNER','OBP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_PAID','PPD',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PAYMENT_RECEIPT','PRC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PURCHASE_ORDER','PO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIPT','GR',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SALES_ORDER','SO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'SHIPMENT','DO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STOCK_STATEMENT','ST',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'WORK_ORDER','WO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PEOPLE','PP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STITCH','ST',param_year,0,'ACTIVE');
		/***1)insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET','AST',param_year,0,'ACTIVE');*/
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECIEVE','RC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'GIVE','GV',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'TRANSFER','TF',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ZAKAT_CALCULATION','ZK',param_year,0,'ACTIVE');

		/*create dashboard revenue*/
		INSERT INTO dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
		/*create dashboard expenses*/
		INSERT INTO dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'EXPENSES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard sales*/
		INSERT INTO dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'SALES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard collection*/
		INSERT INTO dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'COLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'CASHCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'OTHERCOLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		/*create dashboard stock*/
		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockin (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_IN_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_stockout (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'STOCK_OUT_QTY', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		SET result = 1;
		
	END;
    END IF;
RETURN result;
END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine bioappdb.resetuser
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `resetuser`(param_userid varchar(50)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordnotfound integer;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM userprofile WHERE userid = param_userid);
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;        
        
	END;
	ELSE
	BEGIN
		delete from user_role where userid = param_userid;
        
        update userprofile
        set    comp = 'T01'
        where  userid = param_userid;

		SET result = 1;        

    END;
    END IF;

RETURN result;
END$$

DELIMITER ;
SET FOREIGN_KEY_CHECKS = 1;
