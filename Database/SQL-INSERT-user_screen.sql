SELECT * FROM bioapp.screen;

INSERT INTO `bioapp`.`screen` (`screenid`, `screenfilename`, `screendesc`, `screenstatus`) VALUES ('120020','ExpensesListing.aspx?action=OPEN','Expenses Order Listing','A');

update screen
set	   screendesc = 'Add New Payment Receipt'
where  screenid = '080010';

select * From role_screen where comp = 'CDG';

INSERT INTO `bioapp`.`role_screen` (`roleid`, `screenid`, `comp`) VALUES ('01', '120020', 'CDG');

select user_role.comp, user_role.userid, user_role.roleid, role_submodule.moduleid ,role_submodule.submoduleid, role_screen.screenid, screen.screenfilename
from   user_role, role_submodule, role_screen, screen
where  user_role.roleid = role_submodule.roleid
and	   user_role.comp = role_submodule.comp
and	   role_submodule.roleid = role_screen.roleid
and	   role_submodule.submoduleid = role_screen.screenid
and	   role_submodule.comp = role_screen.comp
and	   role_screen.screenid = screen.screenid

