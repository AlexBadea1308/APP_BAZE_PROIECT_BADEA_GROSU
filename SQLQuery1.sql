create database HotelReservations

CREATE TABLE room_type (
	room_type_id INT IDENTITY(1,1) PRIMARY KEY,
	room_type_name VARCHAR(255) NOT NULL UNIQUE,
	room_type_is_active BIT NOT NULL
);

CREATE TABLE room (
	room_id INT IDENTITY(1,1) PRIMARY KEY,
	room_number VARCHAR(25) NOT NULL UNIQUE,
	has_TV BIT NOT NULL,
	has_mini_bar BIT NOT NULL,
	room_is_active BIT NOT NULL,
	room_type_id INT NOT NULL,
	CONSTRAINT FK_ROOM_ROOM_TYPE
	FOREIGN KEY (room_type_id) REFERENCES dbo.room_type (room_type_id)
);

CREATE TABLE "user" (
	"user_id" INT IDENTITY(1,1) PRIMARY KEY,
	"first_name" VARCHAR(40) NOT NULL,
	"last_name" VARCHAR(50) NOT NULL,
	"JMBG" VARCHAR(13) NOT NULL,
	"username" VARCHAR(20) NOT NULL UNIQUE,
	"password" VARCHAR(50) NOT NULL,
	"user_type" VARCHAR(15) NOT NULL,
	"user_is_active" BIT NOT NULL
);

CREATE TABLE price (
    price_id INT IDENTITY(1,1) PRIMARY KEY,
    price_value FLOAT,
    price_is_active BIT NOT NULL,
    price_reservation_type VARCHAR(20) NOT NULL,
    room_type_id INT NOT NULL,
    CONSTRAINT FK_PRICE_ROOM_TYPE
    FOREIGN KEY (room_type_id) REFERENCES room_type (room_type_id)
);

CREATE TABLE reservation (
    reservation_id INT IDENTITY(1,1) PRIMARY KEY,
    reservation_room_number VARCHAR(25) NOT NULL,
    reservation_type VARCHAR(255) NOT NULL,
    start_date_time DATETIME NOT NULL,
    end_date_time DATETIME NOT NULL,
    total_price FLOAT NOT NULL,
    reservation_is_active BIT NOT NULL,
);

CREATE TABLE guest (
    guest_id INT IDENTITY(1,1) PRIMARY KEY,
    guest_name VARCHAR(255) NOT NULL,
    guest_surname VARCHAR(255) NOT NULL,
    guest_jmbg VARCHAR(13) NOT NULL,
    guest_is_active BIT NOT NULL,
	reservationID INT,
    CONSTRAINT FK_Guest_Reservation
    FOREIGN KEY (reservationID) REFERENCES reservation (reservation_id)
);