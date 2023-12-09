-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: bioappdb
-- Source Schemata: bioappdb
-- Created: Thu Jun 16 20:15:24 2022
-- Workbench Version: 8.0.19
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
-- Table bioappdb.asset
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`asset` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `assetno` VARCHAR(50) NULL DEFAULT NULL,
  `assetdesc` VARCHAR(100) NULL DEFAULT NULL,
  `assettyp` VARCHAR(30) NULL DEFAULT NULL,
  `assetcat` VARCHAR(15) NULL DEFAULT NULL,
  `assetowner` VARCHAR(15) NULL DEFAULT NULL,
  `assetrefno` VARCHAR(100) NULL DEFAULT NULL,
  `datemfg` DATE NULL DEFAULT NULL,
  `warranty` VARCHAR(1) NULL DEFAULT 'N',
  `datewarend` DATE NULL DEFAULT NULL,
  `datereg` DATE NULL DEFAULT NULL,
  `costreg` DOUBLE NULL DEFAULT NULL,
  `deprtyp` VARCHAR(15) NULL DEFAULT NULL,
  `deprrate` DOUBLE NULL DEFAULT NULL,
  `depraccum` DOUBLE NULL DEFAULT NULL,
  `assetnbv` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(500) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_ASSET_01` (`comp` ASC, `assetno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.asset_image
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`asset_image` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `assetno` VARCHAR(30) NULL DEFAULT NULL,
  `filename` VARCHAR(100) NULL DEFAULT NULL,
  `fileblob` LONGBLOB NULL DEFAULT NULL,
  `imgwidth` INT(11) NULL DEFAULT NULL,
  `imgheight` INT(11) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_ASSET_IMAGE` (`comp` ASC, `assetno` ASC, `filename` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.asset_placement
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`asset_placement` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `assetno` VARCHAR(50) NULL DEFAULT NULL,
  `country` VARCHAR(20) NULL DEFAULT NULL,
  `state` VARCHAR(20) NULL DEFAULT NULL,
  `district` VARCHAR(20) NULL DEFAULT NULL,
  `location` VARCHAR(200) NULL DEFAULT NULL,
  `trandate` DATE NULL DEFAULT NULL,
  `tranqty` INT(11) NULL DEFAULT NULL,
  `purpose` VARCHAR(200) NULL DEFAULT NULL,
  `officerid` VARCHAR(20) NULL DEFAULT NULL,
  `officername` VARCHAR(50) NULL DEFAULT NULL,
  `contactno` VARCHAR(15) NULL DEFAULT NULL,
  `remarks` VARCHAR(500) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  INDEX `IDX_PLACE_ASSET_01` (`comp` ASC, `assetno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.asset_tran_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`asset_tran_details` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `tranno` VARCHAR(20) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `assetno` VARCHAR(50) NULL DEFAULT NULL,
  `tranqty` INT(11) NULL DEFAULT NULL,
  `tranvalue` DOUBLE NULL DEFAULT NULL,
  `remarks` VARCHAR(500) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_ASSET_TRAN_01` (`comp` ASC, `tranno` ASC, `trancode` ASC, `assetno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 20
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.asset_tran_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`asset_tran_header` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `tranno` VARCHAR(20) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `trancat` VARCHAR(15) NULL DEFAULT NULL,
  `trandate` DATE NULL DEFAULT NULL,
  `remarks` VARCHAR(500) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_ASSET_REG_01` (`comp` ASC, `tranno` ASC, `trancode` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 12
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.attendance_group
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`attendance_group` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `code` VARCHAR(50) NULL DEFAULT NULL,
  `description` VARCHAR(100) NULL DEFAULT NULL,
  `fromdate` DATETIME NULL DEFAULT NULL,
  `todate` DATETIME NULL DEFAULT NULL,
  `rest_day` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_ATT_GRP_01` (`comp` ASC, `fyr` ASC, `code` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.attendance_group_day
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`attendance_group_day` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `wg_id` BIGINT(11) NULL DEFAULT NULL,
  `code` VARCHAR(10) NULL DEFAULT NULL,
  `day_name` VARCHAR(10) NULL DEFAULT NULL,
  `day_date` DATE NULL DEFAULT NULL,
  `fromtime` TIME NULL DEFAULT NULL,
  `totime` TIME NULL DEFAULT NULL,
  `next_day` VARCHAR(1) NULL DEFAULT NULL,
  `follow_previous` VARCHAR(1) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_ATD_UNIQUE_01` (`comp` ASC, `fyr` ASC, `code` ASC, `day_date` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6367
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
  UNIQUE INDEX `IDX_PRI_BP_01` (`comp` ASC, `bpid` ASC) VISIBLE,
  UNIQUE INDEX `IDX_PRI_BP_02` (`comp` ASC, `bpdesc` ASC) VISIBLE)
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
-- Table bioappdb.committee_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`committee_comp` (
  `comp` VARCHAR(20) NULL DEFAULT NULL,
  `committee_id` VARCHAR(50) NULL DEFAULT NULL,
  `committee_name` VARCHAR(255) NULL DEFAULT NULL,
  `committee_address` VARCHAR(255) NULL DEFAULT NULL,
  `committee_tel` VARCHAR(15) NULL DEFAULT NULL,
  `committee_role` VARCHAR(20) NULL DEFAULT NULL,
  `committee_status` VARCHAR(10) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `committee_dob` VARCHAR(50) NULL DEFAULT NULL,
  `committee_age` VARCHAR(30) NULL DEFAULT NULL,
  `committee_prevrole` VARCHAR(30) NULL DEFAULT NULL,
  `committee_doa` VARCHAR(50) NULL DEFAULT NULL,
  `committee_appointmentby` VARCHAR(30) NULL DEFAULT NULL,
  `committee_certno` VARCHAR(30) NULL DEFAULT NULL,
  `committee_type` VARCHAR(10) NULL DEFAULT NULL,
  `committee_job` VARCHAR(100) NULL DEFAULT NULL,
  UNIQUE INDEX `idx_committee_comp` (`comp` ASC, `committee_id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.comp_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`comp_details` (
  `comp` VARCHAR(3) NOT NULL,
  `comp_name` VARCHAR(100) NULL DEFAULT NULL,
  `comp_id` VARCHAR(50) NULL DEFAULT NULL,
  `parent_comp` VARCHAR(50) NULL DEFAULT NULL,
  `comp_type` VARCHAR(50) NULL DEFAULT NULL,
  `comp_category` VARCHAR(50) NULL DEFAULT NULL,
  `comp_accountbank` VARCHAR(100) NULL DEFAULT NULL,
  `comp_accountno` VARCHAR(50) NULL DEFAULT NULL,
  `comp_registrationno` VARCHAR(50) NULL DEFAULT NULL,
  `comp_address` VARCHAR(100) NULL DEFAULT NULL,
  `comp_daerah` VARCHAR(50) NULL DEFAULT NULL,
  `comp_longitud` VARCHAR(25) NULL DEFAULT NULL,
  `comp_latitud` VARCHAR(25) NULL DEFAULT NULL,
  `comp_area` VARCHAR(5) NULL DEFAULT NULL,
  `comp_landstatus` VARCHAR(50) NULL DEFAULT NULL,
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
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_COUNTERMASTER_01` (`comp` ASC, `counterno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
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
  UNIQUE INDEX `IDX_PRI_COUNTERTRANS_01` (`comp` ASC, `counterno` ASC, `id` ASC) VISIBLE)
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
-- Table bioappdb.department_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`department_comp` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `dept_id` VARCHAR(50) NULL DEFAULT NULL,
  `dept_name` VARCHAR(100) NULL DEFAULT NULL,
  `dept_level` INT(11) NULL DEFAULT NULL,
  `dept_reportto` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_DEPT_UNIQUE01` (`comp` ASC, `dept_id` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 8
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
-- Table bioappdb.fis_asset
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_asset` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NOT NULL,
  `assetno` VARCHAR(50) NOT NULL,
  `assetdesc` VARCHAR(100) NOT NULL,
  `assetcat` VARCHAR(15) NULL DEFAULT NULL,
  `assettyp` VARCHAR(50) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_ASSET_PRI_01` (`comp` ASC, `assetno` ASC, `assettyp` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 26
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_balance
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_balance` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `tranno` INT(11) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `trandate` DATETIME NULL DEFAULT NULL,
  `trandesc` VARCHAR(200) NULL DEFAULT NULL,
  `currency` VARCHAR(10) NULL DEFAULT NULL,
  `exrate` DOUBLE NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `refno` VARCHAR(50) NULL DEFAULT NULL,
  `remarks` VARCHAR(200) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_BALANCE_PRI_01` (`comp` ASC, `fyr` ASC, `trancode` ASC, `tranno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 68
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_bank
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_bank` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NOT NULL,
  `bankid` VARCHAR(50) NOT NULL,
  `bankname` VARCHAR(50) NOT NULL,
  `accountno` VARCHAR(50) NOT NULL,
  `address` VARCHAR(100) NULL DEFAULT NULL,
  `contact` VARCHAR(50) NULL DEFAULT NULL,
  `contactno` VARCHAR(20) NULL DEFAULT NULL,
  `currency` VARCHAR(10) NULL DEFAULT NULL,
  `exrate` DOUBLE NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_BANK_PRI_01` (`comp` ASC, `bankid` ASC, `accountno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_bank_statement
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_bank_statement` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `bankid` VARCHAR(50) NULL DEFAULT NULL,
  `accountno` VARCHAR(50) NULL DEFAULT NULL,
  `statementno` INT(11) NULL DEFAULT NULL,
  `statementdate` DATETIME NULL DEFAULT NULL,
  `startdate` DATETIME NULL DEFAULT NULL,
  `enddate` DATETIME NULL DEFAULT NULL,
  `openingbalance` DOUBLE NULL DEFAULT NULL,
  `deposit` DOUBLE NULL DEFAULT NULL,
  `withdraw` DOUBLE NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `closingbalance` DOUBLE NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_BANK_STAT_PRI_01` (`comp` ASC, `fyr` ASC, `bankid` ASC, `accountno` ASC, `statementno` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_bank_statement_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_bank_statement_details` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `accid` VARCHAR(50) NULL DEFAULT NULL,
  `bankid` VARCHAR(50) NULL DEFAULT NULL,
  `accountno` VARCHAR(50) NULL DEFAULT NULL,
  `statementno` INT(11) NULL DEFAULT NULL,
  `tranno` INT(11) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `ledgerdate` DATETIME NULL DEFAULT NULL,
  `desc` VARCHAR(2000) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `refno` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_BANK_STAT_DET_PRI_01` (`comp` ASC, `fyr` ASC, `accid` ASC, `bankid` ASC, `accountno` ASC, `statementno` ASC, `tranno` ASC, `trancode` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_bpid
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_bpid` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(100) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(50) NULL DEFAULT NULL,
  `bpreference` VARCHAR(50) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_CUST_PRI_01` (`comp` ASC, `bpid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 65
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_coa_master
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_coa_master` (
  `id` BIGINT(32) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NOT NULL,
  `accid` VARCHAR(50) NOT NULL,
  `accdesc` VARCHAR(200) NULL DEFAULT NULL,
  `parentid` VARCHAR(50) NULL DEFAULT NULL,
  `accgroup` VARCHAR(50) NULL DEFAULT NULL,
  `acclevel` INT(11) NULL DEFAULT NULL,
  `endlevel` VARCHAR(1) NULL DEFAULT NULL,
  `acctype` VARCHAR(50) NULL DEFAULT NULL,
  `acccat` VARCHAR(50) NULL DEFAULT NULL,
  `acccode` VARCHAR(50) NULL DEFAULT NULL,
  `accnumber` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_COA_MASTER_PRI_01` (`comp` ASC, `accid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 433
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_coa_tran
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_coa_tran` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NOT NULL,
  `fyr` VARCHAR(10) NOT NULL,
  `accid` VARCHAR(50) NOT NULL,
  `accdesc` VARCHAR(200) NULL DEFAULT NULL,
  `parentid` VARCHAR(50) NULL DEFAULT NULL,
  `accgroup` VARCHAR(50) NULL DEFAULT NULL,
  `acclevel` INT(11) NULL DEFAULT NULL,
  `endlevel` VARCHAR(1) NULL DEFAULT NULL,
  `acctype` VARCHAR(50) NULL DEFAULT NULL,
  `acccat` VARCHAR(50) NULL DEFAULT NULL,
  `acccode` VARCHAR(50) NULL DEFAULT NULL,
  `accnumber` VARCHAR(50) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_COA_TRAN_PRI_01` (`comp` ASC, `fyr` ASC, `accid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 1240
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_item
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_item` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NOT NULL,
  `itemno` VARCHAR(30) NOT NULL,
  `itemdesc` VARCHAR(100) NOT NULL,
  `itemcat` VARCHAR(15) NULL DEFAULT NULL,
  `itemtype` VARCHAR(15) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_ITEM_PRI_01` (`comp` ASC, `itemno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 35
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_journal
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_journal` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `tranno` INT(11) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `trandate` DATETIME NULL DEFAULT NULL,
  `trandesc` VARCHAR(200) NULL DEFAULT NULL,
  `trancat` VARCHAR(10) NULL DEFAULT NULL COMMENT 'Payment Paid or Payment Receipt',
  `trantype` VARCHAR(10) NULL DEFAULT NULL COMMENT 'Accrual or direct cash',
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `currency` VARCHAR(10) NULL DEFAULT NULL,
  `exrate` DOUBLE NULL DEFAULT NULL,
  `tranamount` DOUBLE NULL DEFAULT NULL,
  `refno` VARCHAR(50) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT NULL,
  `remarks` VARCHAR(200) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_JOURNAL_PRI_01` (`comp` ASC, `trancode` ASC, `tranno` ASC, `fyr` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 21
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_ledger
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_ledger` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `tranno` INT(11) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `ledgerdate` DATETIME NULL DEFAULT NULL,
  `ledgerno` INT(11) NULL DEFAULT NULL,
  `accid` VARCHAR(50) NULL DEFAULT NULL,
  `accdesc` VARCHAR(2000) NULL DEFAULT NULL,
  `currency` VARCHAR(10) NULL DEFAULT NULL,
  `exrate` DOUBLE NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `refno` VARCHAR(50) NULL DEFAULT NULL,
  `reconstatus` VARCHAR(1) NULL DEFAULT NULL,
  `reconby` VARCHAR(50) NULL DEFAULT NULL,
  `recondate` DATETIME NULL DEFAULT NULL,
  `reconnote` VARCHAR(100) NULL DEFAULT NULL,
  `closestatus` VARCHAR(1) NULL DEFAULT NULL,
  `closeby` VARCHAR(50) NULL DEFAULT NULL,
  `closedate` DATETIME NULL DEFAULT NULL,
  `closenote` VARCHAR(100) NULL DEFAULT NULL,
  `remarks` VARCHAR(200) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_LEDGER_PRI_01` (`comp` ASC, `fyr` ASC, `trancode` ASC, `tranno` ASC, `ledgerno` ASC, `accid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 5345
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_posting
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_posting` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `tranno` INT(11) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `trandate` DATETIME NULL DEFAULT NULL,
  `trandesc` VARCHAR(200) NULL DEFAULT NULL,
  `currency` VARCHAR(10) NULL DEFAULT NULL,
  `exrate` DOUBLE NULL DEFAULT NULL,
  `tranamount` DOUBLE NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `refno` VARCHAR(50) NULL DEFAULT NULL,
  `lineno` INT(11) NULL DEFAULT '0',
  `remarks` VARCHAR(200) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_POST_PRI_01` (`comp` ASC, `fyr` ASC, `tranno` ASC, `trancode` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 739
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_running_number
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_running_number` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `trancode` VARCHAR(50) NULL DEFAULT NULL,
  `initial` VARCHAR(50) NULL DEFAULT NULL,
  `runno` INT(11) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_FIS_RUNNO_01` (`comp` ASC, `fyr` ASC, `trancode` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 145
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.fis_supplier
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`fis_supplier` (
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `accid` VARCHAR(50) NULL DEFAULT NULL,
  `bpid` VARCHAR(20) NULL DEFAULT NULL,
  `bpdesc` VARCHAR(100) NULL DEFAULT NULL,
  `bpaddress` VARCHAR(100) NULL DEFAULT NULL,
  `bpcontact` VARCHAR(50) NULL DEFAULT NULL,
  `bpreference` VARCHAR(50) NULL DEFAULT NULL,
  `debit` DOUBLE NULL DEFAULT NULL,
  `credit` DOUBLE NULL DEFAULT NULL,
  `lasttranno` INT(11) NULL DEFAULT NULL,
  `lasttrancode` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_FIS_CUST_PRI_01` (`comp` ASC, `accid` ASC, `bpid` ASC))
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
-- Table bioappdb.grade_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`grade_comp` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `gred_id` VARCHAR(50) NULL DEFAULT NULL,
  `gred_name` VARCHAR(100) NULL DEFAULT NULL,
  `gred_level` INT(11) NULL DEFAULT NULL,
  `gred_reportto` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_GRED_COMP_UNIQUE_01` (`comp` ASC, `gred_id` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 10
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.info_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`info_comp` (
  `info_no` VARCHAR(20) NULL DEFAULT NULL,
  `comp` VARCHAR(20) NULL DEFAULT NULL,
  `info_type` VARCHAR(20) NULL DEFAULT NULL,
  `info_description` LONGTEXT NULL DEFAULT NULL,
  `info_status` VARCHAR(20) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NOT NULL,
  `createddate` DATETIME NOT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_INFO_COMP` (`info_no` ASC, `comp` ASC))
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
-- Table bioappdb.item_slider
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`item_slider` (
  `comp` VARCHAR(20) NOT NULL,
  `itemno` VARCHAR(30) NOT NULL,
  `itemdesc` VARCHAR(100) NULL DEFAULT NULL,
  `itemcat` VARCHAR(15) NULL DEFAULT NULL,
  `itemtype` VARCHAR(15) NULL DEFAULT NULL,
  `itemstatus` VARCHAR(50) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_SLIDER_01` (`comp` ASC, `itemno` ASC))
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
  UNIQUE INDEX `IDX_PRI_OBP_01` (`comp` ASC, `obpid` ASC) VISIBLE,
  UNIQUE INDEX `IDX_PRI_OBP_02` (`comp` ASC, `obpdesc` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.parameters
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`parameters` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `paramid` VARCHAR(20) NULL DEFAULT NULL,
  `paramtype` VARCHAR(50) NULL DEFAULT NULL,
  `paramcode` VARCHAR(50) NULL DEFAULT NULL,
  `paramdesc` VARCHAR(100) NULL DEFAULT NULL,
  `paramstatus` VARCHAR(50) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PARAMID_KEY01` (`comp` ASC, `paramid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 208
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
-- Table bioappdb.position_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`position_comp` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `pos_id` VARCHAR(50) NULL DEFAULT NULL,
  `pos_name` VARCHAR(100) NULL DEFAULT NULL,
  `pos_level` INT(11) NULL DEFAULT NULL,
  `pos_reportto` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_POS_COMP_UNIQUE_01` (`comp` ASC, `pos_id` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.publicholiday_comp
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`publicholiday_comp` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `code` VARCHAR(50) NULL DEFAULT NULL,
  `description` VARCHAR(100) NULL DEFAULT NULL,
  `ph_date` DATE NULL DEFAULT NULL,
  `fromdate` DATETIME NULL DEFAULT NULL,
  `todate` DATETIME NULL DEFAULT NULL,
  `ph_type` VARCHAR(100) NULL DEFAULT NULL,
  `ph_cat` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PH_UNIQUE_01` (`comp` ASC, `fyr` ASC, `ph_date` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 9
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
-- Table bioappdb.quotation_details
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`quotation_details` (
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
  `salesorderno` VARCHAR(20) NULL DEFAULT NULL,
  `saleslineno` INT(11) NULL DEFAULT NULL,
  UNIQUE INDEX `IDX_PRI_QUO_DET_01` (`comp` ASC, `orderno` ASC, `lineno` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.quotation_header
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`quotation_header` (
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
  `expirydate` DATETIME NULL DEFAULT NULL,
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
  UNIQUE INDEX `IDX_PRI_QUO_HDR_01` (`comp` ASC, `orderno` ASC))
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
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `type` VARCHAR(50) NULL DEFAULT NULL,
  `initial` VARCHAR(6) NULL DEFAULT NULL,
  `year` VARCHAR(4) NULL DEFAULT NULL,
  `runno` INT(11) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_PRI_RUNNO_01` (`comp` ASC, `type` ASC, `initial` ASC, `year` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 782
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
-- Table bioappdb.staff_attendance_day
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_attendance_day` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `day_name` VARCHAR(10) NULL DEFAULT NULL,
  `day_date` DATE NULL DEFAULT NULL,
  `fromtime` TIME NULL DEFAULT NULL,
  `totime` TIME NULL DEFAULT NULL,
  `deviceid` VARCHAR(50) NULL DEFAULT NULL,
  `devicename` VARCHAR(100) NULL DEFAULT NULL,
  `ipno` VARCHAR(20) NULL DEFAULT NULL,
  `location` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_STAFF_ATT_DAY_01` (`comp` ASC, `fyr` ASC, `staffno` ASC, `day_date` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.staff_attendance_group
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_attendance_group` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `wg_id` BIGINT(11) NULL DEFAULT NULL,
  `code` VARCHAR(10) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_STAFF_ATT_GRP_01` (`comp` ASC, `staffno` ASC, `wg_id` ASC, `fyr` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.staff_employment
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_employment` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `emp_deptid` VARCHAR(50) NULL DEFAULT NULL,
  `emp_gredid` VARCHAR(50) NULL DEFAULT NULL,
  `emp_posid` VARCHAR(50) NULL DEFAULT NULL,
  `emp_type` VARCHAR(100) NULL DEFAULT NULL,
  `emp_cat` VARCHAR(100) NULL DEFAULT NULL,
  `emp_probation` INT(11) NULL DEFAULT NULL,
  `emp_fromdate` DATE NULL DEFAULT NULL,
  `emp_todate` DATE NULL DEFAULT NULL,
  `emp_reportto` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_EMP_UNIQUE_01` (`comp` ASC, `staffno` ASC, `emp_fromdate` ASC, `emp_deptid` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.staff_exception_day
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_exception_day` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `day_name` VARCHAR(10) NULL DEFAULT NULL,
  `day_date` DATE NULL DEFAULT NULL,
  `fromtime` TIME NULL DEFAULT NULL,
  `totime` TIME NULL DEFAULT NULL,
  `exc_type` VARCHAR(100) NULL DEFAULT NULL,
  `exc_cat` VARCHAR(100) NULL DEFAULT NULL,
  `exc_id` BIGINT(11) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  `reliefedby` VARCHAR(50) NULL DEFAULT NULL,
  `reliefeddate` DATETIME NULL DEFAULT NULL,
  `approvedby` VARCHAR(50) NULL DEFAULT NULL,
  `approveddate` DATETIME NULL DEFAULT NULL,
  `rejectedby` VARCHAR(50) NULL DEFAULT NULL,
  `rejecteddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_STAFF_EXC_DAY_01` (`comp` ASC, `fyr` ASC, `staffno` ASC, `day_date` ASC, `exc_id` ASC, `exc_type` ASC, `exc_cat` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.staff_excuse
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_excuse` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `fyr` VARCHAR(10) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `exc_cat` VARCHAR(100) NULL DEFAULT NULL,
  `exc_type` VARCHAR(100) NULL DEFAULT NULL,
  `fromdate` DATETIME NULL DEFAULT NULL,
  `todate` DATETIME NULL DEFAULT NULL,
  `exc_reason` VARCHAR(200) NULL DEFAULT NULL,
  `filename` VARCHAR(100) NULL DEFAULT NULL,
  `fileblob` LONGBLOB NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  `reliefedby` VARCHAR(50) NULL DEFAULT NULL,
  `reliefeddate` DATETIME NULL DEFAULT NULL,
  `approvedby` VARCHAR(50) NULL DEFAULT NULL,
  `approveddate` DATETIME NULL DEFAULT NULL,
  `rejectedby` VARCHAR(50) NULL DEFAULT NULL,
  `rejecteddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_EXC_STAFF_01` (`comp` ASC, `fyr` ASC, `exc_cat` ASC, `staffno` ASC, `exc_type` ASC, `fromdate` ASC, `todate` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table bioappdb.staff_info
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`staff_info` (
  `id` BIGINT(11) NOT NULL AUTO_INCREMENT,
  `comp` VARCHAR(3) NULL DEFAULT NULL,
  `staffno` VARCHAR(50) NULL DEFAULT NULL,
  `salute` VARCHAR(50) NULL DEFAULT NULL,
  `name` VARCHAR(200) NULL DEFAULT NULL,
  `nickname` VARCHAR(50) NULL DEFAULT NULL,
  `nicno` VARCHAR(15) NULL DEFAULT NULL,
  `oicno` VARCHAR(15) NULL DEFAULT NULL,
  `iccolor` VARCHAR(10) NULL DEFAULT NULL,
  `passport` VARCHAR(30) NULL DEFAULT NULL,
  `ppexpiry` DATETIME NULL DEFAULT NULL,
  `wkpermit` VARCHAR(30) NULL DEFAULT NULL,
  `wkexpiry` DATETIME NULL DEFAULT NULL,
  `dob` DATE NULL DEFAULT NULL,
  `birthplace` VARCHAR(100) NULL DEFAULT NULL,
  `marital` VARCHAR(50) NULL DEFAULT NULL,
  `dtmarried` DATETIME NULL DEFAULT NULL,
  `bloodtype` VARCHAR(10) NULL DEFAULT NULL,
  `race` VARCHAR(50) NULL DEFAULT NULL,
  `religion` VARCHAR(50) NULL DEFAULT NULL,
  `gender` VARCHAR(30) NULL DEFAULT NULL,
  `nationality` VARCHAR(50) NULL DEFAULT NULL,
  `bumistatus` VARCHAR(1) NULL DEFAULT NULL,
  `paddress1` VARCHAR(200) NULL DEFAULT NULL,
  `paddress2` VARCHAR(200) NULL DEFAULT NULL,
  `paddress3` VARCHAR(200) NULL DEFAULT NULL,
  `paddress4` VARCHAR(200) NULL DEFAULT NULL,
  `ppostcode` VARCHAR(10) NULL DEFAULT NULL,
  `pcity` VARCHAR(50) NULL DEFAULT NULL,
  `pstate` VARCHAR(50) NULL DEFAULT NULL,
  `pcountry` VARCHAR(50) NULL DEFAULT NULL,
  `ptelephone` VARCHAR(20) NULL DEFAULT NULL,
  `caddress1` VARCHAR(200) NULL DEFAULT NULL,
  `caddress2` VARCHAR(200) NULL DEFAULT NULL,
  `caddress3` VARCHAR(200) NULL DEFAULT NULL,
  `caddress4` VARCHAR(200) NULL DEFAULT NULL,
  `cpostcode` VARCHAR(10) NULL DEFAULT NULL,
  `ccity` VARCHAR(50) NULL DEFAULT NULL,
  `cstate` VARCHAR(50) NULL DEFAULT NULL,
  `ccountry` VARCHAR(50) NULL DEFAULT NULL,
  `ctelephone` VARCHAR(20) NULL DEFAULT NULL,
  `mobile1` VARCHAR(20) NULL DEFAULT NULL,
  `mobile2` VARCHAR(20) NULL DEFAULT NULL,
  `datejoined` DATE NULL DEFAULT NULL,
  `retireage` INT(11) NULL DEFAULT NULL,
  `retiredate` DATE NULL DEFAULT NULL,
  `email1` VARCHAR(50) NULL DEFAULT NULL,
  `email2` VARCHAR(50) NULL DEFAULT NULL,
  `facebook` VARCHAR(100) NULL DEFAULT NULL,
  `instagram` VARCHAR(100) NULL DEFAULT NULL,
  `whatsapp` VARCHAR(20) NULL DEFAULT NULL,
  `epfno` VARCHAR(30) NULL DEFAULT NULL,
  `socsono` VARCHAR(30) NULL DEFAULT NULL,
  `taxno` VARCHAR(30) NULL DEFAULT NULL,
  `bankname` VARCHAR(50) NULL DEFAULT NULL,
  `accountype` VARCHAR(50) NULL DEFAULT NULL,
  `accountno` VARCHAR(30) NULL DEFAULT NULL,
  `userid` VARCHAR(50) NULL DEFAULT NULL,
  `password` VARCHAR(50) NULL DEFAULT NULL,
  `usertype` VARCHAR(50) NULL DEFAULT NULL,
  `lastaccess` DATETIME NULL DEFAULT NULL,
  `statuslogon` VARCHAR(50) NULL DEFAULT NULL,
  `status` VARCHAR(10) NULL DEFAULT NULL,
  `remarks` VARCHAR(100) NULL DEFAULT NULL,
  `createdby` VARCHAR(50) NULL DEFAULT NULL,
  `createddate` DATETIME NULL DEFAULT NULL,
  `modifiedby` VARCHAR(50) NULL DEFAULT NULL,
  `modifieddate` DATETIME NULL DEFAULT NULL,
  `confirmedby` VARCHAR(50) NULL DEFAULT NULL,
  `confirmeddate` DATETIME NULL DEFAULT NULL,
  `cancelledby` VARCHAR(50) NULL DEFAULT NULL,
  `cancelleddate` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `IDX_STAFF_COMP_ID` (`comp` ASC, `staffno` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 10
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
-- Table bioappdb.tbl_parametertype
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `bioappdb`.`tbl_parametertype` (
  `paramttype` VARCHAR(100) NOT NULL,
  `paramtdesc` VARCHAR(100) NULL DEFAULT NULL,
  `paramtcategory` VARCHAR(20) NULL DEFAULT NULL,
  `paramstatus` VARCHAR(255) NULL DEFAULT NULL,
  `paramtcode` VARCHAR(20) NULL DEFAULT NULL,
  `comp` VARCHAR(3) NOT NULL,
  PRIMARY KEY (`paramttype`, `comp`))
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
-- Routine bioappdb.createaccounting
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `createaccounting`(param_comp varchar(3), param_fyr varchar(4), param_status varchar(20), param_by varchar(20)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 1;
    DECLARE recordfound integer DEFAULT 0;
    
    set recordfound = (SELECT count(comp) FROM comp_details WHERE upper(comp) = upper(param_comp) );
    IF recordfound = 1 THEN
	BEGIN

		insert into fis_bpid (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, status, createdby, createddate, confirmedby, confirmeddate)
        select comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, param_status, param_by, CURDATE(), param_by, CURDATE() from businesspartner where comp = param_comp and bpid in ('BP000000','BP000001','BP000002','BP000003','BP999999');
        
		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0000000', 'ASET', '0', '00', 0, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());
		
		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0110000', 'TUNAI DAN KESETARAAN TUNAI', 'A0000000', '01', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0112000', 'WANG TUNAI DI BANK', 'A0110000', '01', 2, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0111000', 'WANG TUNAI DI TANGAN', 'A0110000', '01', 2, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0111010', 'PANJAR WANG RUNCIT', 'A0111000', '01', 3, 'Y', 'A', 'CASH', null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0300000', 'AKAUN BELUM TERIMA', 'A0000000', '03', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0300001', 'STAKEHOLDERS', 'A0300000', '03', 2, 'Y', 'A', 'CUSTOMER', 'BP000000', 'BP000000', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0300002', 'TUNAI TANGAN', 'A0300000', '03', 2, 'Y', 'A', 'CUSTOMER', 'BP000001', 'BP000001', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0300003', 'AKAUN BANK', 'A0300000', '03', 2, 'Y', 'A', 'CUSTOMER', 'BP000002', 'BP000002', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0300004', 'PELANGGAN WALK-IN', 'A0300000', '03', 2, 'Y', 'A', 'CUSTOMER', 'BP000003', 'BP000003', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0399999', 'OTHER', 'A0300000', '03', 2, 'Y', 'A', 'CUSTOMER', 'BP999999', 'BP999999', param_status, param_by, CURDATE(), param_by, CURDATE());

		#insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		#values (param_comp, 'A0311101', 'TUNAI TANGAN', 'A0300000', '03', 2, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0600000', 'INVENTORI DAN STOK', 'A0000000', '06', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A0620000', 'BAHAN MENTAH DAN BEKALAN', 'A0600000', '06', 2, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1300000', 'ASET PEROLEHAN', 'A0000000', '13', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1310001', 'ASET DALAM PEMBELIAN', 'A1300000', '13', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1310002', 'ASET DALAM PEMBELIAN (SUSUT NILAI TERKUMPUL)', 'A1300000', '13', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1319997', 'ASET PEROLEHAN LAIN-LAIN', 'A1300000', '13', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1319998', 'ASET PEROLEHAN LAIN-LAIN (SUSUT NILAI TERKUMPUL)', 'A1300000', '13', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A2000000', 'ASET PEMBINAAN', 'A0000000', '20', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A2030001', 'ASET DALAM PEMBINAAN', 'A2000000', '20', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A2030002', 'ASET DALAM PEMBINAAN (SUSUT NILAI TERKUMPUL)', 'A2000000', '20', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A2039997', 'ASET PEMBINAAN LAIN-LAIN', 'A2000000', '20', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A2039998', 'ASET PEMBINAAN LAIN-LAIN (SUSUT NILAI TERKUMPUL)', 'A2000000', '20', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1400000', 'ASET ALIH', 'A0000000', '14', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1410001', 'ASET ALIH SEMENTARA', 'A1400000', '14', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1410002', 'ASET ALIH SEMENTARA (SUSUT NILAI TERKUMPUL)', 'A1400000', '14', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1419997', 'ASET ALIH LAIN-LAIN', 'A1400000', '14', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1419998', 'ASET ALIH LAIN-LAIN (SUSUT NILAI TERKUMPUL)', 'A1400000', '14', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1600000', 'ASET TAK ALIH', 'A0000000', '16', 1, 'N', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1610001', 'ASET TAK ALIH SEMENTARA', 'A1600000', '16', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1610002', 'ASET TAK ALIH SEMENTARA (SUSUT NILAI TERKUMPUL)', 'A1600000', '16', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1619997', 'ASET TAK ALIH LAIN-LAIN', 'A1600000', '16', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'A1619998', 'ASET TAK ALIH LAIN-LAIN (SUSUT NILAI TERKUMPUL)', 'A1600000', '16', 2, 'Y', 'A', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0000000', 'LIABILITI', '0', '00', 0, 'N', 'L', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0100000', 'AKAUN BELUM BAYAR', 'L0000000', '01', 1, 'N', 'L', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0100001', 'STAKEHOLDERS', 'L0100000', '01', 2, 'Y', 'L', 'SUPPLIER', 'BP000000', 'BP000000', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0100002', 'TUNAI TANGAN', 'L0100000', '01', 2, 'Y', 'L', 'SUPPLIER', 'BP000001', 'BP000001', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0100003', 'AKAUN BANK', 'L0100000', '01', 2, 'Y', 'L', 'SUPPLIER', 'BP000002', 'BP000002', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0100004', 'PELANGGAN WALK-IN', 'L0100000', '01', 2, 'Y', 'L', 'SUPPLIER', 'BP000003', 'BP000003', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'L0199999', 'OTHER', 'L0100000', '01', 2, 'Y', 'L', 'SUPPLIER', 'BP999999', 'BP999999', param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0000000', 'HASIL', '0', '00', 0, 'N', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0100000', 'HASIL TERIMAAN', 'H0000000', '01', 1, 'N', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0111101', 'URUSNIAGA JUALAN', 'H0100000', '01', 2, 'Y', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0121101', 'URUSNIAGA PERKHIDMATAN', 'H0100000', '01', 2, 'Y', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0191101', 'TERIMAAN LAIN-LAIN', 'H0100000', '01', 2, 'Y', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0900000', 'PENGHASILAN PERLARASAN', 'H0000000', '09', 1, 'N', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H0910001', 'HASIL PERLARASAN', 'H0900000', '09', 2, 'Y', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'H9999900', 'HASIL KEUNTUNGAN', 'H0000000', '99', 1, 'Y', 'H', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0000000', 'BELANJA', '0', '00', 0, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0110000', 'EMOLUMEN', 'B0000000', '01', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0111000', 'GAJI DAN UPAHAN', 'B0110000', '01', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0111101', 'GAJI KAKITANGAN', 'B0111000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0111102', 'PENDAHULUAN GAJI', 'B0111000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0111103', 'TUNGGAKAN GAJI', 'B0111000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0111999', 'GAJI LAIN-LAIN', 'B0111000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0112000', 'ELAUN DAN IMBUHAN', 'B0110000', '01', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0112101', 'ELAUN TETAP', 'B0112000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0112102', 'ELAUN KHAS', 'B0112000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0112999', 'ELAUN LAIN-LAIN', 'B0112000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0113000', 'FAEDAH DAN SUMBANGAN', 'B0110000', '01', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0113101', 'KUMPULAN WANG SIMPANAN PEKERJA (KWSP)', 'B0113000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0113102', 'PERTUBUHAN KEBAJIKAN PEKERJA (PERKESO)', 'B0113000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0113999', 'SUMBANGAN LAIN-LAIN', 'B0113000', '01', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0200000', 'PERKHIDMATAN DAN BEKALAN', 'B0000000', '02', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0221000', 'PERBELANJAAN PERJALANAN', 'B0200000', '02', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0221101', 'ELAUN PERJALANAN & BAYARAN TAMBANG', 'B0221000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0221102', 'ELAUN & BAYARAN PENGINAPAN', 'B0221000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0221199', 'BAYARAN PERJALANAN LAIN-LAIN', 'B0221000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0222000', 'PERBELANJAAN SARA HIDUP', 'B0200000', '02', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0222101', 'ELAUN & BAYARAN MAKANAN DAN MINUMAN', 'B0222000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0222199', 'BAYARAN SARA HIDUP LAIN-LAIN', 'B0222000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0223000', 'SEWAAN, UTILITI, PERHUBUNGAN DAN KERAIAN', 'B0200000', '02', 2, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0223101', 'PERKHIDAMTAN SEWAAN', 'B0223000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0223102', 'BIL DAN UTILITI', 'B0223000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0223103', 'PERHUBUNGAN DAN KOMUNIKASI', 'B0223000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0223104', 'KERAIAN & HOSPITALITI', 'B0223000', '02', 3, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0500000', 'PERBELANJAAN-PERBELANJAAN LAIN', 'B0000000', '05', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0551000', 'PULANGAN DAN HAPUS KIRA', 'B0500000', '05', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0552000', 'CUKAI DAN PERTUKARAN', 'B0500000', '05', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0557000', 'KERUGIAN DAN HUTANG LAPOK', 'B0500000', '05', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0591000', 'PELBAGAI DAN LAIN-LAIN', 'B0500000', '05', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0600000', 'KOS BARANG DIGUNAKAN', 'B0000000', '06', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0620000', 'BEKALAN DAN BAHAN', 'B0600000', '06', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0630000', 'KELENGKAPAN DAN PERALATAN', 'B0600000', '06', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0640000', 'PENYELENGGARAAN DAN PEMBAIKAN', 'B0600000', '06', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0900000', 'PERBELANJAAN PERLARASAN', 'B0000000', '09', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B0910001', 'BELANJA PERLARASAN', 'B0900000', '09', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B3100000', 'SUSUTNILAI HARTANAH, LOJI, PERALATAN DAN PELABURAN', 'B0000000', '31', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B3130000', 'HARTANAH, LOJI, PERALATAN DAN PELABURAN', 'B3100000', '31', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B3150000', 'NILAI ASET KEWANGAN', 'B3100000', '31', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B3170000', 'NILAI STOK DAN INVENTORI', 'B3100000', '31', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B4100000', 'PENJEJASAN NILAI HARTANAH, PERALATAN DAN PELABURAN', 'B0000000', '41', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B4130000', 'HARTANAH, LOJI, PERALATAN DAN PELABURAN', 'B4100000', '41', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B4150000', 'NILAI ASET KEWANGAN', 'B4100000', '41', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B4170000', 'NILAI STOK DAN INVENTORI', 'B4100000', '41', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B6100000', 'PELUNASAN DAN PELUPUSAN NILAI HARTANAH, PERALATAN DAN PELABURAN', 'B0000000', '61', 1, 'N', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B6130000', 'HARTANAH, LOJI, PERALATAN DAN PELABURAN', 'B6100000', '61', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B6150000', 'NILAI ASET KEWANGAN', 'B6100000', '61', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B6170000', 'NILAI STOK DAN INVENTORI', 'B6100000', '61', 2, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'B9999900', 'BAYARAN DIVIDEN', 'B0000000', '99', 1, 'Y', 'B', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0000000', 'EKUITI', '0', '00', 0, 'N', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0211000', 'LEBIHAN ATAU KURANGAN', 'E0000000', '02', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0311000', 'KUMPULAN WANG OPERASI', 'E0000000', '03', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0411000', 'KUMPULAN WANG AMANAH', 'E0000000', '04', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0511000', 'MODAL WANG PINJAMAN ', 'E0000000', '05', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0611000', 'MODAL WANG PENDAHULUAN', 'E0000000', '06', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		insert into fis_coa_master (comp, accid, accdesc, parentid, accgroup, acclevel, endlevel, acctype, acccat, acccode, accnumber, status, createdby, createddate, confirmedby, confirmeddate)
		values (param_comp, 'E0911000', 'EKUITI LAIN-LAIN', 'E0000000', '09', 1, 'Y', 'E', null, null, null, param_status, param_by, CURDATE(), param_by, CURDATE());

		#insert into fis_running_number (comp, fyr, trancode, initial, runno, status) values(param_comp, param_year, 'OPEBAL', 'OPEBAL', 0, 'ACTIVE');
        #insert into fis_running_number (comp, fyr, trancode, initial, runno, status) values(param_comp, param_year, 'CLOBAL', 'CLOBAL', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'OPENING_BALANCE', 'OPENING_BALANCE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'CLOSING_BALANCE', 'CLOSING_BALANCE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'SALES_INVOICE', 'SALES_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'RECEIPT_VOUCHER', 'RECEIPT_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CASH', 'INVOICE_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CHEQUE', 'INVOICE_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_BANKING', 'INVOICE_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CONTRA_PAYMENT', 'INVOICE_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PURCHASE_INVOICE', 'PURCHASE_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PAYMENT_VOUCHER', 'PAYMENT_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CASH', 'VOUCHER_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CHEQUE', 'VOUCHER_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_BANKING', 'VOUCHER_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CONTRA_PAYMENT', 'VOUCHER_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CASH', 'EXPENSES_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CHEQUE', 'EXPENSES_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_BANKING', 'EXPENSES_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CONTRA_PAYMENT', 'EXPENSES_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'TRANSFER_INVOICE', 'TRANSFER_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'JOURNAL_VOUCHER', 'JOURNAL_VOUCHER', 0, 'ACTIVE');
        
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'ADJUSTMENT_ORDER', 'ADJUSTMENT_ORDER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'RECEIPT', 'RECEIPT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'SHIPMENT', 'SHIPMENT', 0, 'ACTIVE');

		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'REGCOST', 'REGCOST', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'DEPCOST', 'DEPCOST', 0, 'ACTIVE');

		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'CASH_VOUCHER', 'CASH_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'BANK_VOUCHER', 'BANK_VOUCHER', 0, 'ACTIVE');
        
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PROFIT_LOSS', 'PROFIT_LOSS', 0, 'ACTIVE');

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
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'QUOTATION','WO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PEOPLE','PP',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STITCH','ST',param_year,0,'ACTIVE');
		/***1)insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET','AST',param_year,0,'ACTIVE');*/
        /*
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIVE_ORDER','RC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'GIVE_ORDER','GV',param_year,0,'ACTIVE');
        */
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'TRANSFER_ORDER','TF',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ZAKAT_CALCULATION','ZK',param_year,1,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_TRAN','AT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_ICT','AICT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_DAPUR','ADPR',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_MAKMAL','AMAK',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_PEJABAT','APJB',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_TELEKOMUNIKASI','ATEL',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_LAIN_LAIN','AOTH',param_year,0,'ACTIVE');

		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'INFO_COMP','IFC',param_year,0,'ACTIVE');
        
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
        insert into role_module(roleid, moduleid, comp) values('01','050',param_comp);
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
        insert into role_module(roleid, moduleid, comp) values('01','300',param_comp);
        insert into role_module(roleid, moduleid, comp) values('01','400',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('02','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('02','040',param_comp);
        insert into role_module(roleid, moduleid, comp) values('02','050',param_comp);
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
		insert into role_module(roleid, moduleid, comp) values('02','300',param_comp);
        insert into role_module(roleid, moduleid, comp) values('02','400',param_comp);
        
		insert into role_module(roleid, moduleid, comp) values('03','010',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','020',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','030',param_comp);
		insert into role_module(roleid, moduleid, comp) values('03','040',param_comp);
        insert into role_module(roleid, moduleid, comp) values('03','050',param_comp);
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
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','050','050010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','050','050020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','050','050090',param_comp);
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
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140050',param_comp);
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','140','140090',param_comp); 
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
        
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','300','300010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','300','300020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','300','300030',param_comp); 
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','300','300050',param_comp); 
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','300','300100',param_comp);
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('01','400','400010',param_comp);
        
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
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','050','050010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','050','050020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','050','050090',param_comp);
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
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140030',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140040',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140050',param_comp);
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','140','140090',param_comp);
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
        
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','300','300010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','300','300020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','300','300030',param_comp); 
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','300','300050',param_comp); 
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','300','300100',param_comp);
        insert into role_submodule(roleid, moduleid, submoduleid, comp) values('02','400','400010',param_comp);
        
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','010','010010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','020','020010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','020','020020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','030','030010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','030','030020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','040','040090',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','050','050010',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','050','050020',param_comp);
		insert into role_submodule(roleid, moduleid, submoduleid, comp) values('03','050','050090',param_comp);
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
		insert into role_screen(roleid, screenid, comp) values('01','050010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','050020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','050090',param_comp);
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
		insert into role_screen(roleid, screenid, comp) values('01','140030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('01','140050',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','140090',param_comp);
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
        
        insert into role_screen(roleid, screenid, comp) values('01','300010',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','300020',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','300030',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','300050',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','300100',param_comp);
        insert into role_screen(roleid, screenid, comp) values('01','400010',param_comp);
        
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
		insert into role_screen(roleid, screenid, comp) values('02','050010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','050020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','050090',param_comp);
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
		insert into role_screen(roleid, screenid, comp) values('02','140030',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140040',param_comp);
		insert into role_screen(roleid, screenid, comp) values('02','140050',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','140090',param_comp);
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
        
		insert into role_screen(roleid, screenid, comp) values('02','300010',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','300020',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','300030',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','300050',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','300100',param_comp);
        insert into role_screen(roleid, screenid, comp) values('02','400010',param_comp);

        insert into role_screen(roleid, screenid, comp) values('03','010010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','020010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','020020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','030010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','030020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','040090',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','050010',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','050020',param_comp);
		insert into role_screen(roleid, screenid, comp) values('03','050090',param_comp);
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

		/*create dashboard payment*/
		INSERT INTO bioappdb.dashboard_payment (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'PAYMENT_RECEIPT', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_payment (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'PAYMENT_PAID', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
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
-- Routine bioappdb.createfiscalyearaccounting
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `createfiscalyearaccounting`(param_comp varchar(3), param_fyr varchar(4)) RETURNS int(11)
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
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'OPENING_BALANCE', 'OPENING_BALANCE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'CLOSING_BALANCE', 'CLOSING_BALANCE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'SALES_INVOICE', 'SALES_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'RECEIPT_VOUCHER', 'RECEIPT_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CASH', 'INVOICE_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CHEQUE', 'INVOICE_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_BANKING', 'INVOICE_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'INVOICE_CONTRA_PAYMENT', 'INVOICE_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PURCHASE_INVOICE', 'PURCHASE_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PAYMENT_VOUCHER', 'PAYMENT_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CASH', 'VOUCHER_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CHEQUE', 'VOUCHER_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_BANKING', 'VOUCHER_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'VOUCHER_CONTRA_PAYMENT', 'VOUCHER_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CASH', 'EXPENSES_CASH', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CHEQUE', 'EXPENSES_CHEQUE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_BANKING', 'EXPENSES_BANKING', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'EXPENSES_CONTRA_PAYMENT', 'EXPENSES_CONTRA_PAYMENT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'TRANSFER_INVOICE', 'TRANSFER_INVOICE', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'JOURNAL_VOUCHER', 'JOURNAL_VOUCHER', 0, 'ACTIVE');
        
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'ADJUSTMENT_ORDER', 'ADJUSTMENT_ORDER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'RECEIPT', 'RECEIPT', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'SHIPMENT', 'SHIPMENT', 0, 'ACTIVE');
		
        insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'REGCOST', 'REGCOST', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'DEPCOST', 'DEPCOST', 0, 'ACTIVE');
        
        insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'CASH_VOUCHER', 'CASH_VOUCHER', 0, 'ACTIVE');
		insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'BANK_VOUCHER', 'BANK_VOUCHER', 0, 'ACTIVE');
        
        insert bioappdb.fis_running_number (comp, fyr, trancode, initial, runno, status)
		values (param_comp, param_fyr, 'PROFIT_LOSS', 'PROFIT_LOSS', 0, 'ACTIVE');
        
        SET result = 1;
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

		/*inactive old running no*/
		update running_number set status = 'IN-ACTIVE' where comp = param_comp;
        
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
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'QUOTATION','WO',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'PEOPLE','PP',param_year,0,'ACTIVE');
		/***insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'STITCH','ST',param_year,0,'ACTIVE');*/
		/***1)insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET','AST',param_year,0,'ACTIVE');*/
        /*
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'RECEIVE_ORDER','RC',param_year,0,'ACTIVE');
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'GIVE_ORDER','GV',param_year,0,'ACTIVE');
        */
		insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'TRANSFER_ORDER','TF',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ZAKAT_CALCULATION','ZK',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_TRAN','AT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_ICT','AICT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_DAPUR','ADPR',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_MAKMAL','AMAK',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_PEJABAT','APJB',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_TELEKOMUNIKASI','ATEL',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_LAIN_LAIN','AOTH',param_year,0,'ACTIVE');
		
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'INFO_COMP','IFC',param_year,0,'ACTIVE');
        
        /*create dashboard revenue*/
		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO bioappdb.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'REVENUE_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');
        
        /*create dashboard payment*/
		INSERT INTO dashboard_payment (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'PAYMENT_RECEIPT', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

		INSERT INTO dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
		VALUES (param_comp, param_year, 'PAYMENT_PAID', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

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
		delete from user_role
		where comp = param_comp
		and  userid = param_userid;
		
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
        #delete from global_businesspartner where bpid = param_comp;
        
		/*delete data in cashflow*/
        delete from cashflow_header where comp = param_comp;
        delete from cashflow_details where comp = param_comp;
        
		/*delete data in claimprocess*/
        #delete from claimprocess_header where comp = param_comp;
        #delete from claimprocess_details where comp = param_comp;

        /*delete data in counter*/
        delete from counter_master where comp = param_comp;
        delete from counter_transaction where comp = param_comp;
        delete from counter_transaction_details where comp = param_comp;
        
		/*delete dashboard*/
        delete from dashboard_revenue where comp = param_comp;
        delete from dashboard_expenses where comp = param_comp;
        delete from dashboard_payment where comp = param_comp;
        delete from dashboard_sales where comp = param_comp;
        delete from dashboard_collection where comp = param_comp;
        delete from dashboard_slot where comp = param_comp;
        delete from dashboard_stockin where comp = param_comp;
        delete from dashboard_stockout where comp = param_comp;

        /*delete data in expenses*/
        delete from expenses_header where comp = param_comp;
        delete from expenses_details where comp = param_comp;
        
		/*delete fiscalyear*/
        delete from fiscalperiod where comp = param_comp;
    
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
        
		/*delete role parameter*/
        delete from parameters where comp = param_comp;
        
        /*delete data in payment paid*/
        delete from paypaid_header where comp = param_comp;
        delete from paypaid_details where comp = param_comp;
        
        /*delete data in payment receipt*/
        delete from payrcpt_header where comp = param_comp;
        delete from payrcpt_details where comp = param_comp;
        
        /*delete data in people*/
        delete from people where comp = param_comp;

        /*delete data in purchase order*/
        delete from purchase_header where comp = param_comp;
        delete from purchase_details where comp = param_comp;
        
        /*delete data in receipt*/
        delete from receipt_header where comp = param_comp;
        delete from receipt_details where comp = param_comp;

		/*delete role module*/
        delete from role_module  where comp = param_comp;

		/*delete role sub module*/
        delete from role_submodule where comp = param_comp;
    
		/*delete role screen*/
		delete from role_screen where comp = param_comp;

		/*delete running no*/
        delete from running_number where comp = param_comp;

        /*delete data in shipment*/
        delete from shipment_header where comp = param_comp;
        delete from shipment_details where comp = param_comp;
        delete from shipment_charge where comp = param_comp;
        
        /*delete data in stitch_details*/
        delete from stitch_details where comp = param_comp;
        
        /*delete data in stock state*/
        delete from stockstate_header where comp = param_comp;
        delete from stockstate_details where comp = param_comp;
        delete from stockstate_soh where comp = param_comp;
        
		/*delete tax*/
        delete from tax where comp = param_comp;

        /*delete data in stock state*/
        delete from transfer_header where comp = param_comp;
        delete from transfer_details where comp = param_comp;
        
        /*delete data in user_role*/
        delete from user_role where comp = param_comp;

        /*delete data in stock state*/
        #delete from zakatpoint_header where comp = param_comp;
        #delete from zakatpoint_details where comp = param_comp;
        #delete from zakatpoint_transactions where comp = param_comp;

        /*delete comp details*/
        delete from comp_details where comp = param_comp;	
        
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

        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_TRAN','AT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_ICT','AICT',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_DAPUR','ADPR',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_MAKMAL','AMAK',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_PEJABAT','APJB',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_TELEKOMUNIKASI','ATEL',param_year,0,'ACTIVE');
        insert into running_number (comp, type, initial, year, runno, status) values(param_comp,'ASSET_ALATAN_LAIN_LAIN','AOTH',param_year,0,'ACTIVE');

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

-- ----------------------------------------------------------------------------
-- Routine bioappdb.updatecoabalance
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `bioappdb`$$
CREATE DEFINER=`bioappdb`@`%` FUNCTION `updatecoabalance`(param_comp varchar(3), param_year varchar(4)) RETURNS int(11)
BEGIN
	DECLARE result integer DEFAULT 0;
    DECLARE recordnotfound integer DEFAULT 0;
    
    declare v_id bigint;
	declare v_comp varchar(3);
	declare v_fyr varchar(4);
	declare v_accid varchar(50);
	declare v_jumdebit double;
    declare v_jumcredit double;
    declare v_maxlevel int default 0;
    
    set recordnotfound = (SELECT isnull(max(1)) FROM comp_details WHERE upper(comp) = upper(param_comp));
    IF recordnotfound = 1 THEN
	BEGIN
		SET result = 0;
	END;
    ELSE
    BEGIN
		#initial update as zero
		UPDATE fis_coa_tran 
        SET  debit = 0, credit = 0
		WHERE comp = param_comp AND fyr = param_year AND endlevel = 'Y';
        
        BEGIN
			DECLARE done INTEGER DEFAULT 0;
			DECLARE cur1 CURSOR FOR SELECT b.id, b.comp, b.fyr, b.accid, SUM(a.debit) as jumdebit, SUM(a.credit) as jumcredit
									FROM   fis_ledger a INNER JOIN fis_coa_tran b ON a.comp = b.comp AND a.fyr = b.fyr AND a.accid = b.accid
									WHERE  a.comp is not null
									AND    a.status <> 'CANCELLED'
									AND    b.comp = param_comp
									AND    b.fyr = param_year 
									AND    b.endlevel = 'Y'
                                    GROUP BY b.id, b.comp, b.fyr, b.accid;
			DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;        
			set done = 0;
			open cur1;
			igmLoop: loop
				fetch cur1 into v_id, v_comp, v_fyr, v_accid, v_jumdebit, v_jumcredit;
				if done = 1 then leave igmLoop; end if;
				
                UPDATE fis_coa_tran
                SET    debit = v_jumdebit,
					   credit = v_jumcredit
				WHERE  comp is not null
				AND    comp = v_comp
                AND    id = v_id
                AND    fyr = v_fyr;      
                
                set result = result + 1;
			end loop igmLoop;
			close cur1;        
        END;        
        
		SET v_maxlevel = (SELECT MAX(a.acclevel) FROM fis_coa_tran a WHERE a.comp is not null AND comp = param_comp AND fyr = param_year);        
        WHILE v_maxlevel > 0 DO
			BEGIN
				DECLARE done2 INTEGER DEFAULT 0;
				DECLARE cur2 CURSOR FOR SELECT a.id, a.comp, a.fyr, a.accid, 
											  (SELECT SUM(b.debit) FROM fis_coa_tran b WHERE b.comp = a.comp AND b.fyr = a.fyr AND b.parentid = a.accid) as jumdebit , 
											  (SELECT SUM(b.credit) FROM fis_coa_tran b WHERE b.comp = a.comp AND b.fyr = a.fyr AND b.parentid = a.accid) as jumcredit
									   FROM   fis_coa_tran a
									   WHERE  a.comp is not null
									   AND    a.comp = param_comp
								       AND    a.fyr = param_year 
									   AND    a.endlevel = 'N'
                                       AND    a.acclevel = v_maxlevel-1
                                       GROUP BY a.id, a.comp, a.fyr, a.accid;
				DECLARE CONTINUE HANDLER FOR NOT FOUND SET done2 = 1;
				SET done2 = 0;
				open cur2;
				igmLoop2: loop
					fetch cur2 into v_id, v_comp, v_fyr, v_accid, v_jumdebit, v_jumcredit;
					if done2 = 1 then leave igmLoop2; end if;
					
					UPDATE fis_coa_tran
					SET    debit = v_jumdebit,
						   credit = v_jumcredit
					WHERE  comp is not null
					AND    comp = v_comp
					AND    id = v_id
					AND    fyr = v_fyr;
					
                    set result = result + 1;
				end loop igmLoop2;
				close cur2;                                       
            END;
			SET v_maxlevel = v_maxlevel - 1;
		END WHILE;
        
        #SET result = 1;
	END;
    END IF; 
	RETURN result;
END$$

DELIMITER ;
SET FOREIGN_KEY_CHECKS = 1;
