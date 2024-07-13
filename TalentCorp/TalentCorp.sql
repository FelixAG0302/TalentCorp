create table Puestos
(
    id           int            not null primary key identity,
    nombre       varchar(100)   not null,
    descripción  text           not null,
    nivel_riesgo varchar(20) check (nivel_riesgo in ('ALTO', 'MEDIO', 'BAJO')),
    salarioMin   decimal(10, 2) not null,
    salarioMax   decimal(10, 2) not null,
    Estado       varchar(20)    not null
);


create table Candidatos
(
    id           int         not null primary key identity,
    cédula       varchar(11) not null,
    nombre       varchar(50) not null,
    apellido     varchar(50) not null,
    fechaIngreso datetime    not null,
    departamento varchar(50) not null,
    estado       varchar(20) not null
);


create table ExperienciaLaboral
(
    id            int            not null primary key,
    candidatoID   int            not null,
    empresa       varchar(100)   not null,
    puestoOcupado varchar(50)    not null,
    fechaDesde    datetime       not null,
    fechaHasta    datetime       not null,
    salario       decimal(10, 2) not null,
    foreign key (candidatoID) references Candidatos (id)
);


create table Educacion
(
    id          int          not null primary key,
    candidatoID int          not null,
    nivel       varchar(50)  not null,
    institucion varchar(100) not null,
    idiomas     varchar(50),
    fechaDesde  datetime     not null,
    fechaHasta  datetime     not null,
    foreign key (candidatoID) references Candidatos (id)
);


create table Entrevistas
(
    id              int      not null primary key,
    candidatoID     int      not null,
    puestoID        int      not null,
    fechaEntrevista datetime not null,
    foreign key (candidatoID) references Candidatos (id),
    foreign key (puestoID) references Puestos (id)
);


create table Empleados
(
    id           int         not null primary key identity,
    cedula       varchar(11) not null,
    nombre       varchar(50) not null,
    apellido     varchar(50) not null,
    fechaIngreso datetime    not null,
    departamento varchar(50) not null,
    estado       varchar(20) not null check (estado in ('ACTIVO', 'INACTIVO')),
    puestoID     int         not null,
    candidatoID  int         not null,
    foreign key (candidatoID) references Candidatos (id),
    foreign key (puestoID) references Puestos (id)
);

create table Usuarios
(
    id       int not null primary key identity,
    username varchar(50),
    email    varchar(50),
    password varchar(30)
)

create table Roles
(
    id          int not null primary key,
    nombre      varchar(50),
    descripcion text
)

create table UsuariosRoles
(
    id      int not null primary key identity,
    user_id int not null,
    role_id int not null foreign key (user_id) references Usuarios(id),
    foreign key (role_id) references Roles (id)
)