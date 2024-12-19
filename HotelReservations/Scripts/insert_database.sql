-- Inserări pentru tabelul room_type
use HotelReservations
GO

INSERT INTO room_type (room_type_name) 
VALUES 
('Single Room'),
('Double Room'),
('Suite'),
('Deluxe Room'),
('Family Room');

-- Inserări pentru tabelul room
INSERT INTO room (room_number, has_TV, has_mini_bar, room_type_id) 
VALUES 
('101', 1, 0,1),
('102', 1, 1,2),
('201', 1, 1,3),
('202', 1, 0,4),
('301', 1, 1,5);

-- Inserări pentru tabelul user
INSERT INTO "user" (first_name, last_name, CNP, username, password, user_type) 
VALUES 
('Alex', 'Badea', '1234567890123', 'alex', 'alex', 'Receptionist'),
('Razvan', 'Grosu', '1234567890125', 'grosu', 'grosu', 'Receptionist'),
('admin', 'admin', '1234567390127', 'admin', 'pass','Admin');

-- Inserări pentru tabelul price
INSERT INTO price (price_value, price_reservation_type, room_type_id) 
VALUES 
(50.0,'Night', 1),
(75.0,'Night', 2),
(100.0,'Night', 3),
(150.0,'Night', 4),
(200.0,'Night', 5),
(30.0,'Day', 1),
(45.0,'Day', 2),
(80.0,'Day', 3),
(120.0,'Day', 4),
(175.0,'Day', 5);

-- Inserări pentru tabelul reservation
INSERT INTO reservation (reservation_room_number, reservation_type, start_date_time, end_date_time, total_price) 
VALUES 
('101', 'Night', '2024-11-20 14:00:00', '2024-11-21 12:00:00', 50.0),
('102', 'Night', '2024-11-21 14:00:00', '2024-11-22 12:00:00', 75.0),
('201', 'Night', '2024-11-22 14:00:00', '2024-11-23 12:00:00', 100.0),
('202', 'Night', '2024-11-23 14:00:00', '2024-11-24 12:00:00', 150.0),
('301', 'Night', '2024-11-24 14:00:00', '2024-11-25 12:00:00', 200.0);

-- Inserări pentru tabelul guest
INSERT INTO guest (guest_name, guest_surname, guest_cnp, reservationID) 
VALUES 
('Ana', 'Maria', '1234567890123', 1),
('Oprea', 'Laur', '1234567890124',2),
('Bogdan', 'Gruia', '1234567890125', 3),
('Valentin', 'Axi', '1234567890126',4),
('Pila', 'Alexandru', '1234567890127',5);
