drop DATABASE DA_SmartMedCab;
drop user 'smc_user';

CREATE DATABASE DA_SmartMedCab;
create user 'smc_user'@'%' identified by 'smc';
grant all on DA_SmartMedCab.* to 'smc_user'@'%';

