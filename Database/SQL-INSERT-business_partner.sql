select *
from	businesspartner;

INSERT INTO bioapp.businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
VALUES ('CDG', 'CD-BP201600002', 'RAAMS HOME DÉCOR', 'PEKAN RABU, 07000 LANGKAWI, KEDAH', '0125056255', ' ', 'SHOP', 'NORMAL', 0, 0, 0, 'ACTIVE');

INSERT INTO bioapp.businesspartner (comp, bpid, bpdesc, bpaddress, bpcontact, bpreference, bpcat, discounttype, cashguarantee, bankguarantee, creditlimit, bpstatus)
VALUES ('CDG', 'CD-BP999999999', 'OTHER', '', '', '', 'OTHER', 'NORMAL', 0, 0, 0, 'ACTIVE');


update businesspartner
set	   bpid = 'CD-BP201600002'
where  bpid = 'CD-SOO201600002';

delete from businesspartner where bpid = 'CD-BP201600009';