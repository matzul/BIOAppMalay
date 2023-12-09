CREATE TABLE dashboard_revenue (
  comp varchar(3) DEFAULT NULL,
  fyr varchar(10) DEFAULT NULL,
  type varchar(30) DEFAULT NULL,
  MON01 double DEFAULT NULL,
  MON01desc varchar(10) DEFAULT NULL,
  MON02 double DEFAULT NULL,
  MON02desc varchar(10) DEFAULT NULL,
  MON03 double DEFAULT NULL,
  MON03desc varchar(10) DEFAULT NULL,
  MON04 double DEFAULT NULL,
  MON04desc varchar(10) DEFAULT NULL,
  MON05 double DEFAULT NULL,
  MON05desc varchar(10) DEFAULT NULL,
  MON06 double DEFAULT NULL,
  MON06desc varchar(10) DEFAULT NULL,
  MON07 double DEFAULT NULL,
  MON07desc varchar(10) DEFAULT NULL,
  MON08 double DEFAULT NULL,
  MON08desc varchar(10) DEFAULT NULL,
  MON09 double DEFAULT NULL,
  MON09desc varchar(10) DEFAULT NULL,
  MON10 double DEFAULT NULL,
  MON10desc varchar(10) DEFAULT NULL,
  MON11 double DEFAULT NULL,
  MON11desc varchar(10) DEFAULT NULL,
  MON12 double DEFAULT NULL,
  MON12desc varchar(10) DEFAULT NULL,
  status varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_DASHBOARD_REV01 (comp,fyr,type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

select *
from dashboard_revenue;

SELECT date_format(invoicedate,'%m-%Y'), SUM(invoice_header.totalamount) revenue  
from   invoice_header  
WHERE  invoice_header.comp is not NULL  
and    invoice_header.comp = 'CDG'
and    date_format(invoicedate,'%m-%Y') = '09-2016';

select comp, fyr, type, MON01, MON02, MON03, MON04, MON05, MON06, MON07, MON08, MON09, MON10, MON11, MON12,         
		MON01desc, MON02desc, MON03desc, MON04desc, MON05desc, MON06desc, MON07desc, MON08desc, MON09desc, MON10desc, MON11desc, MON12desc,         
        (MON01 + MON02 + MON03 + MON04 + MON05 + MON06 + MON07 + MON08 + MON09 + MON10 + MON11 + MON12) TODATE  
from   dashboard_revenue where  comp = 'CDG'  AND    fyr = '2016'  and  type = 'REVENUE_PLAN';

INSERT INTO bioapp.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'REVENUE_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_revenue (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'REVENUE_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

update dashboard_revenue
set	   MON10 = 8000
where  comp = 'CDG'
and	   fyr = '2016'
and	   type = 'REVENUE_PLAN';
	   

CREATE TABLE dashboard_expenses (
  comp varchar(3) DEFAULT NULL,
  fyr varchar(10) DEFAULT NULL,
  type varchar(30) DEFAULT NULL,
  MON01 double DEFAULT NULL,
  MON01desc varchar(10) DEFAULT NULL,
  MON02 double DEFAULT NULL,
  MON02desc varchar(10) DEFAULT NULL,
  MON03 double DEFAULT NULL,
  MON03desc varchar(10) DEFAULT NULL,
  MON04 double DEFAULT NULL,
  MON04desc varchar(10) DEFAULT NULL,
  MON05 double DEFAULT NULL,
  MON05desc varchar(10) DEFAULT NULL,
  MON06 double DEFAULT NULL,
  MON06desc varchar(10) DEFAULT NULL,
  MON07 double DEFAULT NULL,
  MON07desc varchar(10) DEFAULT NULL,
  MON08 double DEFAULT NULL,
  MON08desc varchar(10) DEFAULT NULL,
  MON09 double DEFAULT NULL,
  MON09desc varchar(10) DEFAULT NULL,
  MON10 double DEFAULT NULL,
  MON10desc varchar(10) DEFAULT NULL,
  MON11 double DEFAULT NULL,
  MON11desc varchar(10) DEFAULT NULL,
  MON12 double DEFAULT NULL,
  MON12desc varchar(10) DEFAULT NULL,
  status varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_DASHBOARD_EXP01 (comp,fyr,type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO bioapp.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'EXPENSES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 3800, 'Jul', 3800, 'Aug', 3800, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_expenses (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'EXPENSES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 3800, 'Jul', 4550, 'Aug', 5350, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

select *
from dashboard_expenses;

update dashboard_expenses
set	   MON10 = 7000
where  comp = 'CDG'
and	   fyr = '2016'
and	   type = 'EXPENSES_PLAN';

update dashboard_expenses
set	   MON10 = 6177
where  comp = 'CDG'
and	   fyr = '2016'
and	   type = 'EXPENSES_ACTUAL';

delete from dashboard_expenses;

CREATE TABLE dashboard_sales (
  comp varchar(3) DEFAULT NULL,
  fyr varchar(10) DEFAULT NULL,
  type varchar(30) DEFAULT NULL,
  MON01 double DEFAULT NULL,
  MON01desc varchar(10) DEFAULT NULL,
  MON02 double DEFAULT NULL,
  MON02desc varchar(10) DEFAULT NULL,
  MON03 double DEFAULT NULL,
  MON03desc varchar(10) DEFAULT NULL,
  MON04 double DEFAULT NULL,
  MON04desc varchar(10) DEFAULT NULL,
  MON05 double DEFAULT NULL,
  MON05desc varchar(10) DEFAULT NULL,
  MON06 double DEFAULT NULL,
  MON06desc varchar(10) DEFAULT NULL,
  MON07 double DEFAULT NULL,
  MON07desc varchar(10) DEFAULT NULL,
  MON08 double DEFAULT NULL,
  MON08desc varchar(10) DEFAULT NULL,
  MON09 double DEFAULT NULL,
  MON09desc varchar(10) DEFAULT NULL,
  MON10 double DEFAULT NULL,
  MON10desc varchar(10) DEFAULT NULL,
  MON11 double DEFAULT NULL,
  MON11desc varchar(10) DEFAULT NULL,
  MON12 double DEFAULT NULL,
  MON12desc varchar(10) DEFAULT NULL,
  status varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_DASHBOARD_SALES01 (comp,fyr,type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO bioapp.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SALES_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_sales (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SALES_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

select *
from	dashboard_sales;

update dashboard_sales
set    mon07 = 976,
       mon08 = 5206,
       mon09 = 876
where  comp = 'CDG'
and    fyr = '2016'
and    type = 'SALES_ACTUAL';

CREATE TABLE dashboard_collection (
  comp varchar(3) DEFAULT NULL,
  fyr varchar(10) DEFAULT NULL,
  type varchar(30) DEFAULT NULL,
  MON01 double DEFAULT NULL,
  MON01desc varchar(10) DEFAULT NULL,
  MON02 double DEFAULT NULL,
  MON02desc varchar(10) DEFAULT NULL,
  MON03 double DEFAULT NULL,
  MON03desc varchar(10) DEFAULT NULL,
  MON04 double DEFAULT NULL,
  MON04desc varchar(10) DEFAULT NULL,
  MON05 double DEFAULT NULL,
  MON05desc varchar(10) DEFAULT NULL,
  MON06 double DEFAULT NULL,
  MON06desc varchar(10) DEFAULT NULL,
  MON07 double DEFAULT NULL,
  MON07desc varchar(10) DEFAULT NULL,
  MON08 double DEFAULT NULL,
  MON08desc varchar(10) DEFAULT NULL,
  MON09 double DEFAULT NULL,
  MON09desc varchar(10) DEFAULT NULL,
  MON10 double DEFAULT NULL,
  MON10desc varchar(10) DEFAULT NULL,
  MON11 double DEFAULT NULL,
  MON11desc varchar(10) DEFAULT NULL,
  MON12 double DEFAULT NULL,
  MON12desc varchar(10) DEFAULT NULL,
  status varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_DASHBOARD_COL01 (comp,fyr,type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

select *
from	dashboard_collection;

INSERT INTO bioapp.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'COLLECTION_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_collection (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'COLLECTION_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

CREATE TABLE dashboard_slot (
  comp varchar(3) DEFAULT NULL,
  fyr varchar(10) DEFAULT NULL,
  type varchar(30) DEFAULT NULL,
  MON01 double DEFAULT NULL,
  MON01desc varchar(10) DEFAULT NULL,
  MON02 double DEFAULT NULL,
  MON02desc varchar(10) DEFAULT NULL,
  MON03 double DEFAULT NULL,
  MON03desc varchar(10) DEFAULT NULL,
  MON04 double DEFAULT NULL,
  MON04desc varchar(10) DEFAULT NULL,
  MON05 double DEFAULT NULL,
  MON05desc varchar(10) DEFAULT NULL,
  MON06 double DEFAULT NULL,
  MON06desc varchar(10) DEFAULT NULL,
  MON07 double DEFAULT NULL,
  MON07desc varchar(10) DEFAULT NULL,
  MON08 double DEFAULT NULL,
  MON08desc varchar(10) DEFAULT NULL,
  MON09 double DEFAULT NULL,
  MON09desc varchar(10) DEFAULT NULL,
  MON10 double DEFAULT NULL,
  MON10desc varchar(10) DEFAULT NULL,
  MON11 double DEFAULT NULL,
  MON11desc varchar(10) DEFAULT NULL,
  MON12 double DEFAULT NULL,
  MON12desc varchar(10) DEFAULT NULL,
  status varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_DASHBOARD_SLOT01 (comp,fyr,type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

select *
from	dashboard_slot;

INSERT INTO bioapp.dashboard_slot (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SLOT_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_slot (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SLOT_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 0, 'Jul', 0, 'Aug', 0, 'Sep', 0, 'Oct', 0, 'Nov', 0, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_slot (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SLOT_PLAN', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 2790, 'Jul', 2790, 'Aug', 2700, 'Sep', 2790, 'Oct', 2700, 'Nov', 2790, 'Dec', 'ACTIVE');

INSERT INTO bioapp.dashboard_slot (comp, fyr, type, MON01, MON01desc, MON02, MON02desc, MON03, MON03desc, MON04, MON04desc, MON05, MON05desc, MON06, MON06desc, MON07, MON07desc, MON08, MON08desc, MON09, MON09desc, MON10, MON10desc, MON11, MON11desc, MON12, MON12desc, status)
VALUES ('CDG', '2016', 'SLOT_ACTUAL', 0, 'Jan', 0, 'Feb', 0, 'Mar', 0, 'Apr', 0, 'May', 0, 'Jun', 952, 'Jul', 938, 'Aug', 1047, 'Sep', 812, 'Oct', 732, 'Nov', 760, 'Dec', 'ACTIVE');

update dashboard_slot
set	   MON10 = 978
where  comp = 'CDG'
and	   fyr = '2016'
and    type = 'SLOT_ACTUAL';

delete from dashboard_slot;