SELECT parameters.comp,
    parameters.paramid,
    parameters.paramtype,
    parameters.paramcode,
    parameters.paramdesc,
    parameters.paramstatus,
    parameters.createdby,
    parameters.createddate
FROM bioapp.parameters;

SELECT parameters.comp, parameters.paramid, parameters.paramtype, parameters.paramcode, parameters.paramdesc, parameters.paramstatus,         
	   parameters.createdby, parameters.createddate  
FROM   parameters  
WHERE  parameters.comp = 'CDG'  
AND    parameters.paramtype = 'ADVERTISING_PROMOTIONAL'  
ORDER  BY parameters.paramcode;

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600001', 'SUPPLY_EXPENSES','EXPENSES0101', 'LAND AND BUILDING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600002', 'SUPPLY_EXPENSES','EXPENSES0102', 'FACTORY SUPPLIES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600003', 'SUPPLY_EXPENSES','EXPENSES0103', 'OFFICE SUPPLIES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600004', 'SUPPLY_EXPENSES','EXPENSES0104', 'MOTOR VEHICLES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600005', 'SUPPLY_EXPENSES','EXPENSES0199', 'OTHER SUPPLY EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600006', 'SALARIES_WAGES','EXPENSES0201', 'EMPLOYEE SALARIES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600007', 'SALARIES_WAGES','EXPENSES0202', 'EMPLOYEE WAGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600008', 'SALARIES_WAGES','EXPENSES0203', 'COMMISSIONS AND INCENTIVES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600009', 'SALARIES_WAGES','EXPENSES0204', 'OVERTIME', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600010', 'SALARIES_WAGES','EXPENSES0205', 'BONUS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600011', 'SALARIES_WAGES','EXPENSES0206', 'FESTIVAL GIFTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600012', 'SALARIES_WAGES','EXPENSES0207', 'ALLOWANCES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600013', 'SALARIES_WAGES','EXPENSES0299', 'OTHER SALARY AND WAGE EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600014', 'TRAVEL_EXPENSES','EXPENSES0301', 'LODGING AND ACCOMODATION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600015', 'TRAVEL_EXPENSES','EXPENSES0302', 'TRANSPORTATION AND TICKET FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600016', 'TRAVEL_EXPENSES','EXPENSES0303', 'MEAL ALLOWANCES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600017', 'TRAVEL_EXPENSES','EXPENSES0304', 'CLEANING AND LAUNDRY', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600018', 'TRAVEL_EXPENSES','EXPENSES0305', 'CASH ADVANCED', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600019', 'TRAVEL_EXPENSES','EXPENSES0399', 'OTHER TRAVEL EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600020', 'ENTERTAINMENT_EXPENSES','EXPENSES0401', 'FOOD AND BEVERAGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600021', 'ENTERTAINMENT_EXPENSES','EXPENSES0402', 'CHARITABLE EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600022', 'ENTERTAINMENT_EXPENSES','EXPENSES0403', 'MONITORY SPONSORSHIPS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600023', 'ENTERTAINMENT_EXPENSES','EXPENSES0404', 'CONFERENCES AND EVENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600024', 'ENTERTAINMENT_EXPENSES','EXPENSES0499', 'OTHER ENTERTAINMENT EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600025', 'EMPLOYEE_BENEFIT','EXPENSES0501', 'KWSP', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600026', 'EMPLOYEE_BENEFIT','EXPENSES0502', 'SOCSO', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600027', 'EMPLOYEE_BENEFIT','EXPENSES0503', 'ZAKAT', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600028', 'EMPLOYEE_BENEFIT','EXPENSES0504', 'INSURANCE FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600029', 'EMPLOYEE_BENEFIT','EXPENSES0505', 'MEDICAL FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600030', 'EMPLOYEE_BENEFIT','EXPENSES0506', 'EDUCATIONAL FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600031', 'EMPLOYEE_BENEFIT','EXPENSES0506', 'BENEFICIAL LOANS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600032', 'EMPLOYEE_BENEFIT','EXPENSES0507', 'RECREATIONAL FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600033', 'EMPLOYEE_BENEFIT','EXPENSES0599', 'OTHER EMPLOYEE BENEFITS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600034', 'MARKETING_ADVERTISING','EXPENSES0601', 'MEDIA PUBLICATIONS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600035', 'MARKETING_ADVERTISING','EXPENSES0602', 'ADVERT AND PROMOTIONS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600036', 'MARKETING_ADVERTISING','EXPENSES0603', 'EVENT SPONSORSHIPS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600037', 'MARKETING_ADVERTISING','EXPENSES0604', 'CONTEST AND GIFTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600038', 'MARKETING_ADVERTISING','EXPENSES0605', 'DONATIONS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600039', 'MARKETING_ADVERTISING','EXPENSES0699', 'OTHER ADVERTISING EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600040', 'RENTAL_LEASING','EXPENSES0701', 'LAND AND BUILDING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600041', 'RENTAL_LEASING','EXPENSES0702', 'FACTORY EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600042', 'RENTAL_LEASING','EXPENSES0703', 'OFFICE EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600043', 'RENTAL_LEASING','EXPENSES0704', 'MOTOR VEHICLES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600044', 'RENTAL_LEASING','EXPENSES0799', 'OTHER RENTAL AND LEASING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600045', 'REPAIR_MAINTENANCE','EXPENSES0801', 'LAND AND BUILDING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600046', 'REPAIR_MAINTENANCE','EXPENSES0802', 'FACTORY EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600047', 'REPAIR_MAINTENANCE','EXPENSES0803', 'OFFICE EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600048', 'REPAIR_MAINTENANCE','EXPENSES0804', 'MOTOR VEHICLES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600049', 'REPAIR_MAINTENANCE','EXPENSES0899', 'OTHER REPAIR AND MAINTENANCE', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600050', 'DEPRECIATION_EXPENSES','EXPENSES0901', 'LAND AND BUILDING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600051', 'DEPRECIATION_EXPENSES','EXPENSES0902', 'FACTORY EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600052', 'DEPRECIATION_EXPENSES','EXPENSES0903', 'OFFICE EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600053', 'DEPRECIATION_EXPENSES','EXPENSES0904', 'MOTOR VEHICLES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600054', 'DEPRECIATION_EXPENSES','EXPENSES0999', 'OTHER DEPRECIATION EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600055', 'BAD_DEBT_EXPENSES','EXPENSES1001', 'WRITE-OFF EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600056', 'BAD_DEBT_EXPENSES','EXPENSES1002', 'GIVE-ALLOWANCE EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600057', 'BAD_DEBT_EXPENSES','EXPENSES1099', 'OTHER BAD DEBT EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600058', 'SUBSCRIPTION_REGISTRATION','EXPENSES1101', 'BOOKS AND NEWSPAPER/MAGAZINE', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600059', 'SUBSCRIPTION_REGISTRATION','EXPENSES1102', 'PRODUCT LICENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600060', 'SUBSCRIPTION_REGISTRATION','EXPENSES1103', 'REGISTRATION AND STAMP DUTIES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600061', 'SUBSCRIPTION_REGISTRATION','EXPENSES1199', 'OTHER SUBSCRIPTION AND REGISTRATION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600062', 'INSURANCE_SECURITY','EXPENSES1201', 'LAND AND BUILDING', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600063', 'INSURANCE_SECURITY','EXPENSES1202', 'FACTORTY EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600064', 'INSURANCE_SECURITY','EXPENSES1203', 'OFFICE EQUIPMENTS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600065', 'INSURANCE_SECURITY','EXPENSES1204', 'MOTOR VEHICLES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600066', 'INSURANCE_SECURITY','EXPENSES1205', 'SECURITY SERVICES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600067', 'INSURANCE_SECURITY','EXPENSES1299', 'OTHER INSURANCE AND SECURITY', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600068', 'PROFESSIONAL_STATUTORY','EXPENSES1301', 'AUDIT FEES AND EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600069', 'PROFESSIONAL_STATUTORY','EXPENSES1302', 'LEGAL FEES AND EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600070', 'PROFESSIONAL_STATUTORY','EXPENSES1303', 'SECRETARIAL FEES AND EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600071', 'PROFESSIONAL_STATUTORY','EXPENSES1304', 'CONSULTANCY FEES AND EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600072', 'PROFESSIONAL_STATUTORY','EXPENSES1305', 'ENGINEERING FEES AND EXPENSES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600073', 'PROFESSIONAL_STATUTORY','EXPENSES1305', 'PENALTY FEES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600074', 'BILL_UTILITIES','EXPENSES1401', 'ELECTRICITY CONSUMPTION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600075', 'BILL_UTILITIES','EXPENSES1402', 'WATER CONSUMPTION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600076', 'BILL_UTILITIES','EXPENSES1403', 'TELEPHONE AND INTERNET', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600077', 'BILL_UTILITIES','EXPENSES1404', 'POSTAGE AND COURIER', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600078', 'BILL_UTILITIES','EXPENSES1499', 'OTHER BILLS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600079', 'TAXATION','EXPENSES1501', 'GST AND TAX', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600080', 'TAXATION','EXPENSES1599', 'OTHER TAXATION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600081', 'SELLING_SERVICES','EXPENSES1601', 'PACKING MATERIAL CHARGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600082', 'SELLING_SERVICES','EXPENSES1602', 'TRANSPORTATION CHARGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600083', 'SELLING_SERVICES','EXPENSES1603', 'INSURANCE CHARGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600084', 'SELLING_SERVICES','EXPENSES1604', 'DUTIES AND TAX CHARGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600085', 'SELLING_SERVICES','EXPENSES1605', 'PROTOTYPING CHARGES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600086', 'SELLING_SERVICES','EXPENSES1606', 'WARRANTY CLAIMS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600087', 'SELLING_SERVICES','EXPENSES1607', 'GOODWILL CLAIMS', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600088', 'SELLING_SERVICES','EXPENSES1608', 'SALES COMMISSIONS AND INCENTIVES', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600089', 'SELLING_SERVICES','EXPENSES1699', 'OTHER SELLING AND SERVICES COST', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600090', 'OTHER_EXPENSES','EXPENSES1701', 'BANK CHARGES AND COMMISION', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600091', 'OTHER_EXPENSES','EXPENSES1702', 'COMPANY TRIP AND TOUR', 'ACTIVE', 'sysadmin', now());

insert into parameters (comp, paramid, paramtype, paramcode, paramdesc, paramstatus, createdby, createddate)
values ('CDG', 'CDG-PAR201600092', 'OTHER_EXPENSES','EXPENSES1799', 'OTHER EXPENSES', 'ACTIVE', 'sysadmin', now());

/*
delete from parameters;
*/