CREATE TABLE users (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	type int,
	email VARCHAR NOT NULL,
	password VARCHAR not null,
	salt bytea NOT NULL,
	name VARCHAR NOT NULL,
	surname VARCHAR NOT NULL,
	phone_number VARCHAR NOT null,
	description VARCHAR,
	photo VARCHAR
);

CREATE TABLE procedures (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	name VARCHAR NOT NULL,
	description VARCHAR not null,
	price decimal not null
);

CREATE TABLE pets (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	type int NOT NULL,
	name VARCHAR NOT NULL,
	birth_date timestamptz not null,
	photo VARCHAR,
	owner_id uuid not null,
   	CONSTRAINT fk_owner FOREIGN KEY(owner_id) REFERENCES users(id) 
);
  
CREATE TABLE time_slots (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	beginning timestamptz not null,
	ending timestamptz not null,
	available bool not null,
	doctor_id uuid not null,
   	CONSTRAINT fk_doctor FOREIGN KEY(doctor_id) REFERENCES users(id) 
);  

CREATE TABLE appointments (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	status int not null,
	pet_id uuid not null,
	time_slot_id uuid not null,
	user_id uuid not null,
   	CONSTRAINT fk_pet FOREIGN KEY(pet_id) REFERENCES pets(id), 
   	CONSTRAINT fk_time_slot FOREIGN KEY(time_slot_id) REFERENCES time_slots(id),
   	CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id)
); 

CREATE TABLE notes (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	value varchar not null,
	appointment_id uuid not null,
	user_id uuid not null,
	CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id), 
   	CONSTRAINT fk_appointment FOREIGN KEY(appointment_id) REFERENCES appointments(id) 
); 

CREATE TABLE appointment_procedures (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	procedure_id uuid not null,
	appointment_id uuid not null,
	user_id uuid not null,
	CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id), 
   	CONSTRAINT fk_procedure FOREIGN KEY(procedure_id) REFERENCES procedures(id),
	CONSTRAINT fk_appointment FOREIGN KEY(appointment_id) REFERENCES appointments(id)
);

create table refresh_tokens(
id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
user_id uuid not null,
token string not null,
expires timestamptz not null,
CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id)
);