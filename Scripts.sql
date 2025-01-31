### Indice para realização de busca para o campo email
create database usermanagement;

use usermanagement;

drop schema usermanagement;

CREATE INDEX IX_users_email ON users (email);

### Procedure para realizar limpeza de registros antigos
DELIMITER //

CREATE PROCEDURE CleanOldRecords(IN cutoff_date DATE)
BEGIN
    DELETE FROM users WHERE created_at < cutoff_date;
END //

DELIMITER ;
