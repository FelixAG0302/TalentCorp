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
    puesto_id     int         not null,
    foreign key (puesto_id) references Puestos (id)
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
    fecha_entrevista datetime not null,
    foreign key (candidato_id) references Candidatos (id),
);


create table Empleados
(
    id            int         not null primary key identity,
    cedula        varchar(11) not null,
    nombre        varchar(50) not null,
    apellido      varchar(50) not null,
    fecha_ingreso datetime    not null,
    estado        varchar(20) not null check (estado in ('ACTIVO', 'INACTIVO')),
    puesto_id     int         not null,
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
    id          int         not null primary key identity,
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

INSERT INTO puestos (Nombre, Descripcion, nivel_riesgo, salario_min, salario_max, Estado) VALUES 
('Analista de Sistemas', 'Responsable de analizar y diseñar sistemas informáticos', 'Bajo', 30000, 45000, 'Disponible'),
('Gerente de Ventas', 'Dirige el departamento de ventas y estrategia comercial', 'Medio', 50000, 80000, 'Disponible'),
('Operador de Maquinaria', 'Maneja y mantiene la maquinaria de producción', 'Alto', 20000, 35000, 'Ocupado'),
('Asistente Administrativo', 'Realiza tareas de apoyo administrativo en la oficina', 'Bajo', 15000, 25000, 'Disponible'),
('Desarrollador Web', 'Desarrolla y mantiene aplicaciones web', 'Bajo', 35000, 55000, 'Disponible'),
('Graphic Designer', 'Crea diseños visuales para materiales impresos y digitales', 'Bajo', 25000, 40000, 'Disponible'),
('Contador', 'Gestiona las finanzas y la contabilidad de la empresa', 'Medio', 30000, 50000, 'Disponible'),
('Gerente de Proyectos', 'Lidera proyectos y coordina equipos de trabajo', 'Medio', 60000, 90000, 'Disponible'),
('Soporte', 'Brinda soporte y solución de problemas técnicos', 'Bajo', 20000, 35000, 'Disponible'),
('Marketing Digital', 'Desarrolla estrategias de marketing en medios digitales', 'Bajo', 30000, 50000, 'Disponible'),
('Recursos Humanos', 'Gestiona la contratación y el bienestar de los empleados', 'Bajo', 25000, 45000, 'Disponible'),
('Ingeniero de Software', 'Desarrolla y mantiene software de aplicación', 'Bajo', 40000, 70000, 'Disponible'),
('Vendedor', 'Vende productos y servicios a los clientes', 'Bajo', 18000, 30000, 'Disponible'),
('Cajero', 'Maneja transacciones y atención al cliente en caja', 'Bajo', 15000, 25000, 'Disponible'),
('Gerente de Operaciones', 'Supervisa las operaciones diarias de la empresa', 'Medio', 50000, 80000, 'Disponible'),
('Electronics Technician', 'Repara y mantiene equipos electrónicos', 'Medio', 25000, 40000, 'Disponible'),
('Software Architect', 'Diseña la arquitectura de sistemas y aplicaciones', 'Bajo', 50000, 80000, 'Disponible'),
('Internal Auditor', 'Revisa y evalúa los procesos internos de la empresa', 'Medio', 35000, 55000, 'Disponible'),
('Secretaria', 'Realiza tareas administrativas y de apoyo a la gerencia', 'Bajo', 15000, 25000, 'Disponible'),
('Jefe de Productos', 'Supervisa el proceso de producción y el personal', 'Medio', 40000, 60000, 'Disponible'),
('Community Manager', 'Gestiona la presencia de la empresa en redes sociales', 'Bajo', 25000, 40000, 'Disponible'),
('Logistica', 'Gestiona el almacenamiento y distribución de productos', 'Medio', 30000, 50000, 'Disponible'),
('Industrial Engineer', 'Optimiza procesos productivos y de manufactura', 'Medio', 40000, 65000, 'Disponible'),
('Recepcionista', 'Atiende y direcciona a visitantes y llamadas', 'Bajo', 15000, 25000, 'Disponible'),
('App Developer', 'Crea y mantiene aplicaciones móviles', 'Bajo', 35000, 55000, 'Disponible'),
('Consultor', 'Brinda asesoramiento en áreas especializadas', 'Bajo', 40000, 70000, 'Disponible'),
('Network Engineer', 'Diseña y mantiene redes de comunicación', 'Bajo', 35000, 60000, 'Disponible'),
('Supervisor de Ventas', 'Supervisa y coordina al equipo de ventas', 'Medio', 30000, 50000, 'Disponible'),
('Analista Financiero', 'Analiza datos financieros para la toma de decisiones', 'Medio', 35000, 55000, 'Disponible'),
('Backend Developer', 'Desarrolla la parte del servidor de aplicaciones web', 'Bajo', 40000, 65000, 'Disponible'),
('Analista de Datos', 'Analiza y reporta datos para la toma de decisiones', 'Bajo', 30000, 50000, 'Disponible'),
('Especialista de Mantenimiento', 'Realiza mantenimiento preventivo y correctivo', 'Medio', 20000, 35000, 'Disponible'),
('UI/UX Designer', 'Crea interfaces y experiencias de usuario', 'Bajo', 35000, 55000, 'Disponible'),
('Gerente de Marketing', 'Lidera el equipo de marketing y estrategias publicitarias', 'Medio', 50000, 85000, 'Disponible'),
('Operador de Call Center', 'Atiende llamadas y brinda soporte a clientes', 'Bajo', 15000, 25000, 'Disponible'),
('Investigador de Mercado', 'Realiza estudios de mercado y análisis de consumidores', 'Bajo', 25000, 40000, 'Disponible'),
('Ingeniero Civil', 'Diseña y supervisa obras de construcción', 'Alto', 45000, 70000, 'Disponible'),
('Programador', 'Escribe y mantiene código para aplicaciones', 'Bajo', 30000, 50000, 'Disponible'),
('Jefe de Almacenes', 'Gestiona el almacenamiento y control de inventarios', 'Medio', 30000, 50000, 'Disponible'),
('Abogado', 'Asesora legalmente a la empresa y maneja litigios', 'Medio', 50000, 80000, 'Disponible'),
('SEO Specialist', 'Optimiza sitios web para motores de búsqueda', 'Bajo', 30000, 50000, 'Disponible'),
('IT Manager', 'Supervisa y gestiona la infraestructura tecnológica', 'Medio', 60000, 90000, 'Disponible'),
('Jefe de Compras', 'Gestiona la adquisición de materiales y servicios', 'Medio', 40000, 65000, 'Disponible'),
('Jefe de Seguridad', 'Implementa y mantiene medidas de seguridad', 'Alto', 25000, 45000, 'Disponible'),
('Analista de Negocios', 'Analiza y mejora procesos de negocio', 'Bajo', 35000, 55000, 'Disponible'),
('Cybersecurity Specialist', 'Protege la infraestructura digital de la empresa', 'Alto', 50000, 80000, 'Disponible'),
('Jefe de Recursos Humanos', 'Lidera el departamento de recursos humanos', 'Medio', 50000, 80000, 'Disponible'),
('Quality Engineer', 'Asegura la calidad de productos y procesos', 'Medio', 40000, 65000, 'Disponible');
