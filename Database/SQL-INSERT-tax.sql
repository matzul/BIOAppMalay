SELECT * FROM bioapp.tax;

INSERT INTO `bioapp`.`tax` (`comp`, `taxcode`, `taxrate`)
VALUES ('CDG', 'NA', 0);

UPDATE `bioapp`.`tax`
SET `comp` = 'CDG';
