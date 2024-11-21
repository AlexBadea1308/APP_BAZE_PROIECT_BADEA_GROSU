-- Inserări pentru tabelul room_type
use HotelReservations
GO

INSERT INTO room_type (room_type_name, room_type_is_active) 
VALUES 
('Single Room', 1),
('Double Room', 1),
('Suite', 1),
('Deluxe Room', 1),
('Family Room', 1);

-- Inserări pentru tabelul room
INSERT INTO room (room_number, has_TV, has_mini_bar, room_is_active, room_type_id) 
VALUES 
('101', 1, 0, 1, 1),
('102', 1, 1, 1, 2),
('201', 1, 1, 1, 3),
('202', 1, 0, 1, 4),
('301', 1, 1, 1, 5);

-- Inserări pentru tabelul user
INSERT INTO "user" (first_name, last_name, JMBG, username, password, user_type, user_is_active) 
VALUES 
('John', 'Doe', '1234567890123', 'jdoe', 'password123', 'Admin', 1),
('Jane', 'Smith', '1234567890124', 'jsmith', 'password123', 'Receptionist', 1),
('Bob', 'Brown', '1234567890125', 'bbrown', 'password123', 'Guest', 1),
('Alice', 'Johnson', '1234567890126', 'ajohnson', 'password123', 'Manager', 1),
('Charlie', 'Davis', '1234567890127', 'cdavis', 'password123', 'Guest', 1);

-- Inserări pentru tabelul price
INSERT INTO price (price_value, price_is_active, price_reservation_type, room_type_id) 
VALUES 
(50.0, 1, 'Standard', 1),
(75.0, 1, 'Standard', 2),
(100.0, 1, 'Standard', 3),
(150.0, 1, 'Deluxe', 4),
(200.0, 1, 'Family', 5);

-- Inserări pentru tabelul reservation
INSERT INTO reservation (reservation_room_number, reservation_type, start_date_time, end_date_time, total_price, reservation_is_active) 
VALUES 
('101', 'Standard', '2024-11-20 14:00:00', '2024-11-21 12:00:00', 50.0, 1),
('102', 'Standard', '2024-11-21 14:00:00', '2024-11-22 12:00:00', 75.0, 1),
('201', 'Standard', '2024-11-22 14:00:00', '2024-11-23 12:00:00', 100.0, 1),
('202', 'Deluxe', '2024-11-23 14:00:00', '2024-11-24 12:00:00', 150.0, 1),
('301', 'Family', '2024-11-24 14:00:00', '2024-11-25 12:00:00', 200.0, 1);

-- Inserări pentru tabelul guest
INSERT INTO guest (guest_name, guest_surname, guest_jmbg, guest_is_active, reservationID) 
VALUES 
('John', 'Doe', '1234567890123', 1, 1),
('Jane', 'Smith', '1234567890124', 1, 2),
('Bob', 'Brown', '1234567890125', 1, 3),
('Alice', 'Johnson', '1234567890126', 1, 4),
('Charlie', 'Davis', '1234567890127', 1, 5);
