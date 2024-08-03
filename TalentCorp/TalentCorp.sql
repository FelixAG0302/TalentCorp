create table Puestos
(
    id           int            not null primary key identity,
    nombre       varchar(100)   not null,
    descripcion  text           not null,
    nivel_riesgo varchar(20)    not null check (nivel_riesgo in ('ALTO', 'MEDIO', 'BAJO')),
    salario_min  decimal(10, 2) not null,
    salario_max  decimal(10, 2) not null,
    estado       varchar(20)    not null check (estado in ('DISPONIBLE', 'OCUPADO'))
);


create table Candidatos
(
    id            int         not null primary key identity,
    cédula        varchar(11) not null,
    nombre        varchar(50) not null,
    apellido      varchar(50) not null,
    fecha_ingreso datetime    not null,
    departamento  varchar(50) not null
);


create table ExperienciaLaboral
(
    id             int            not null primary key identity,
    candidato_id   int            not null,
    empresa        varchar(100)   not null,
    puesto_ocupado varchar(50)    not null,
    fecha_desde    datetime       not null,
    fecha_hasta    datetime       not null,
    salario        decimal(10, 2) not null,
    foreign key (candidato_id) references Candidatos (id)
);


create table Educacion
(
    id           int          not null primary key identity,
    candidato_id int          not null,
    nivel        varchar(50)  not null,
    institucion  varchar(100) not null,
    idiomas      varchar(255) not null,
    fecha_desde  datetime     not null,
    fecha_hasta  datetime     not null,
    foreign key (candidato_id) references Candidatos (id)
);


create table Entrevistas
(
    id               int      not null primary key identity,
    candidato_id     int      not null,
    puesto_id        int      not null,
    fecha_entrevista datetime not null,
    foreign key (candidato_id) references Candidatos (id),
    foreign key (puesto_id) references Puestos (id)
);


create table Empleados
(
    id            int         not null primary key identity,
    cedula        varchar(11) not null,
    nombre        varchar(50) not null,
    apellido      varchar(50) not null,
    fecha_ingreso datetime    not null,
    departamento  varchar(50) not null,
    estado        varchar(20) not null check (estado in ('ACTIVO', 'INACTIVO')),
    puesto_id     int         not null,
    candidato_id  int         not null,
    foreign key (candidato_id) references Candidatos (id),
    foreign key (puesto_id) references Puestos (id)
);

create table Usuarios
(
    id         int          not null primary key identity,
    nombre     varchar(50)  not null,
    correo     varchar(50)  not null,
    contrasena varchar(255) not null
)

create table Roles
(
    id          int         not null primary key,
    nombre      varchar(50) not null,
    descripcion text        not null
)

create table UsuariosRoles
(
    id         int not null primary key identity,
    usuario_id int not null,
    rol_id     int not null,
    foreign key (usuario_id) references Usuarios (id),
    foreign key (rol_id) references Roles (id)
)