alter table verifyaccounts alter column userid varchar(100)
alter table verifyaccounts drop constraint FK__VerifyAcc__useri__398D8EEE

alter table tbl_users set isverified=true;

truncate table tbl_users
truncate table VerifyAccounts;
select* from Tbl_Users
