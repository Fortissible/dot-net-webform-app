PGSQL/SQL

1. Login with "psql --username postgres"
2. Create User if not available "CREATE USER <<nama user>> WITH ENCRYPTED PASSWORD '<<password>>';"
3. Create DB "CREATE DATABASE <<nama database>>;"
4. Grant access and DB owner 
	"GRANT ALL ON DATABASE <<nama database>> TO <<nama user>>;"
	"ALTER DATABASE <<nama database>> OWNER TO <<nama user>>;"
5. Access DB with non-root user "psql --username <<nama user>> --dbname <<nama database>>"
	- Check all userlist with "\du" and check all DB and owner with "\l"
6. Create table with 
	"CREATE TABLE table_name (
	   column1 datatype(length) column_constraint,
	   column2 datatype(length) column_constraint,
	   column3 datatype(length) column_constraint,
	);"
	
	Example:
	"CREATE TABLE karyawan (
	   id VARCHAR(8) PRIMARY KEY,
	   nama_lengkap VARCHAR(30) NOT NULL,
	   tgl_lahir INT2 NOT NULL,
	   email VARCHAR(50) UNIQUE NOT NULL,
	   alamat TEXT
	);"
7. Do CRUD with "INSERT, SELECT, UPDATE, & DELETE"
	Example:
	"INSERT INTO karyawan VALUES ('DCD002', 'Gilang Ramadhan', '1993', 'gilang@dicoding.com', 'Batik Kumeli No. 50 Bandung');"
	"SELECT id, nama_lengkap, email, alamat FROM karyawan;"
	"SELECT * FROM karyawan;"
	"SELECT * FROM karyawan WHERE nama_lengkap = 'Gilang Ramadhan';"
	"UPDATE karyawan
	SET nama_lengkap = 'Gilang Ramadan'
	WHERE id = 'DCD002';"
	"DELETE FROM karyawan WHERE id = 'DCD001';"
	"SELECT departmen.nama_departemen 
	FROM karyawan 
	INNER JOIN departemen ON karyawan.id_departemen=departemen.id_departemen
	WHERE karyawan.id_karyawan = ‘DCD0001’;"