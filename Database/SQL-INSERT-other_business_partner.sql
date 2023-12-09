CREATE TABLE otherbusinesspartner (
  comp varchar(3) DEFAULT NULL,
  obpid varchar(20) DEFAULT NULL,
  obpdesc varchar(100) DEFAULT NULL,
  obpaddress varchar(100) DEFAULT NULL,
  obpcontact varchar(50) DEFAULT NULL,
  obpreference varchar(50) DEFAULT NULL,
  obpcat varchar(15) DEFAULT NULL,
  discounttype varchar(15) DEFAULT NULL,
  cashguarantee double DEFAULT NULL,
  bankguarantee double DEFAULT NULL,
  creditlimit double DEFAULT NULL,
  obpstatus varchar(50) DEFAULT NULL,
  UNIQUE KEY IDX_PRI_OBP_01 (comp,obpid),
  UNIQUE KEY IDX_PRI_OBP_02 (comp,obpdesc)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

select * from otherbusinesspartner;

insert into otherbusinesspartner (comp, obpid, obpdesc, obpaddress, obpcontact)
values ('CDG','CDG-OBP201600001','EXORA KREATIF ENTERPRISE', 'SUNGAI LALANG, SG PETANI', 'SUHAIRE');

delete from otherbusinesspartner where obpid = 'CDG-OBP201600001';