DROP TABLE IF EXISTS `Authorization`, `Class`, `Employee`, 
                    `Operation`, `Person`, `Person_sex`, 
                    `Product`, `Store_city`,`Store_country`,  
                    `Week_day`, `Schedule_open`, `Store`, `Store_address`,
                    `Store_phone`, `Store_Schedule_open`, `Operation_type`, 
                    `Work_position`, `Operation_Product`;

CREATE TABLE `Week_day`(
	`id_week_day` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `value` VARCHAR(100) NOT NULL
);

CREATE TABLE `Schedule_open`(
    `id_schedule_open` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_week_day` BIGINT NOT NULL,
    `open_at` TIME NOT NULL,
    `close_at` TIME NOT NULL,
    FOREIGN KEY (`id_week_day`) REFERENCES `Week_day`(`id_week_day`) ON DELETE CASCADE
);

CREATE TABLE `Store_country`(
	`id_store_country` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `country_name` VARCHAR(100) NOT NULL
);

CREATE TABLE `Store_city`(
	`id_store_city` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_store_country` BIGINT NOT NULL,
    `city_name` VARCHAR(100) NOT NULL,
    FOREIGN KEY (`id_store_country`) REFERENCES `Store_country`(`id_store_country`) ON DELETE CASCADE
);

CREATE TABLE `Store_address`(
    `id_store_address` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_store_city` BIGINT NOT NULL,
    `street_name` VARCHAR(100) NOT NULL,
    `street_number_code` VARCHAR(20) NOT NULL,
    FOREIGN KEY (`id_store_city`) REFERENCES `Store_city`(`id_store_city`) ON DELETE CASCADE
);

CREATE TABLE `Store_phone`(
    `id_store_phone` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `phone_value` VARCHAR(100)
);

CREATE TABLE `Store`(
    `id_store` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_schedule_open` BIGINT NOT NULL,
    `store_name` VARCHAR(100) NOT NULL,
    `id_store_address` BIGINT NOT NULL,
    `id_store_phone` BIGINT,
    FOREIGN KEY (`id_store_address`) REFERENCES `Store_address`(`id_store_address`)ON DELETE CASCADE,
    FOREIGN KEY (`id_store_phone`) REFERENCES `Store_phone`(`id_store_phone`)ON DELETE CASCADE
);

CREATE TABLE `Store_Schedule_open`(
    `id_schedule_open` BIGINT NOT NULL,
    `id_store` BIGINT NOT NULL,
    FOREIGN KEY (`id_schedule_open`) REFERENCES  `Schedule_open`(`id_schedule_open`),
    FOREIGN KEY (`id_store`) REFERENCES `Store`(`id_store`),
    PRIMARY KEY (`id_schedule_open`, `id_store`)
);

CREATE TABLE `Person_sex`(
    `id_sex` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `sex_value` VARCHAR(100) NOT NULL
);


CREATE TABLE `Person`(
    `id_person` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `firstName` VARCHAR(100) NOT NULL,
    `lastName` VARCHAR(100),
    `middleName` VARCHAR(100),
    `id_sex` BIGINT,
    `birthdate` DATE NOT NULL,
    FOREIGN KEY (`id_sex`) REFERENCES `Person_sex`(`id_sex`) ON DELETE CASCADE
);

CREATE TABLE `Authorization`(
    `id_person` BIGINT PRIMARY KEY,
    `login` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    FOREIGN KEY (`id_person`) REFERENCES `Person`(`id_person`) ON DELETE CASCADE
);

CREATE TABLE `Work_position`(
    `id_work_position` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `start_date` DATE NOT NULL,
    `position_name` VARCHAR(100) NOT NULL 
);

CREATE TABLE `Employee`(
    `id_employee` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_person` BIGINT NOT NULL,
    `id_store` BIGINT,
    `id_work_position` UNIQUE BIGINT,
    FOREIGN KEY (`id_person`) REFERENCES `Person`(`id_person`),
    FOREIGN KEY (`id_store`) REFERENCES `Store`(`id_store`),
    FOREIGN KEY (`id_work_position`) REFERENCES `Work_position`(`id_work_position`) ON DELETE CASCADE
);


CREATE TABLE `Class`(
    `id_class` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `class_name` VARCHAR(100) NOT NULL
);

CREATE TABLE `Product`(
    `id_product` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_store` BIGINT,
    `id_class` BIGINT,
    `usd_price` DECIMAL(8, 2) UNSIGNED NOT NULL,
    `define_code` VARCHAR(255),
    `isAvailable` TINYINT(1),
    `name` VARCHAR(100) NOT NULL,
    `count` INT UNSIGNED,
    FOREIGN KEY (`id_class`) REFERENCES `Class`(`id_class`),
    FOREIGN KEY (`id_store`) REFERENCES `Store`(`id_store`)
);

CREATE TABLE `Operation_type`(
    `id_operation_type` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `type_name` VARCHAR(100) NOT NULL
);

CREATE TABLE `Operation`(
    `id_operation` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `id_operation_type` BIGINT NOT NULL,
    `id_employee` BIGINT,
    `id_product` BIGINT NOT NULL,
    `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    FOREIGN KEY (`id_operation_type`) REFERENCES `Operation_type`(`id_operation_type`),
    FOREIGN KEY (`id_employee`) REFERENCES `Employee`(`id_employee`)
);

CREATE TABLE `Operation_Product`(
    `id_operation` BIGINT NOT NULL,
    `id_product` BIGINT NOT NULL,
    `operation_product_price` INT UNSIGNED NOT NULL,
    `operation_product_count`  INT UNSIGNED NOT NULL, # Count of the product that we working with right now (add/sell), not that we have at all
    FOREIGN KEY (`id_operation`) REFERENCES `Operation`(`id_operation`),
    FOREIGN KEY (`id_product`) REFERENCES `Product`(`id_product`),
    PRIMARY KEY (`id_operation`, `id_product`)
);
