select *
from	userprofile;

INSERT INTO `bioapp`.`userprofile` (`userid`, `comp`, `userpwd`, `username`, `usertype`, `userstatus`)
VALUES ('badrudi@capitaldigital.com.my', 'CDG', 'badrudi@123', 'Badrudi Bin Haji Hamid', '01', 'A');

update userprofile
set	userpwd = 'anas@123'
where	userid = 'anas@capitaldigital.com.my';

select *
from role;

update role
set	rolename = 'COMPADMIN',
	roledesc = 'Company Admin'
where	roleid = '02';

select * from user_role;

insert into user_role (userid, roleid, comp)
values ('badrudi@capitaldigital.com.my', '02', 'CDG');

SELECT * FROM module;

select * from submodule;

SELECT * FROM bioapp.role_module where comp = 'CDG';

insert into role_module (roleid, moduleid, comp)
values ('02', '130', 'CDG');

update role_module set roleid = '01' where moduleid = '130';


SELECT * FROM bioapp.role_submodule
where  comp = 'CDG';

INSERT INTO `bioapp`.`role_submodule` (`roleid`, `moduleid`, `submoduleid`, `comp`) VALUES ('02','130', '130020', 'CDG');

update role_submodule set roleid = '01' where moduleid = '130';

