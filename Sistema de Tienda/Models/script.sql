CREATE DATABASE SistemaTienda
GO

USE SistemaTienda;

-- SI
CREATE TABLE Tiendas (
    id INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(90) NOT NULL,
    -- Logo VARCHAR(2000) NULL,
    Email VARCHAR(100) NULL,
    Direccion VARCHAR(200) NOT NULL
);
GO
-- SI
CREATE TABLE Categorias (
    id INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(15) NOT NULL,
    Descripcion TEXT NULL
);
GO
-- SI
CREATE TABLE Productos (
    id INT NOT NULL PRIMARY KEY,
    IdCategoria INT NOT NULL,
    IdTienda INT NOT NULL,
    Nombre VARCHAR(20) NOT NULL,
    Image VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(200) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    activo TINYINT NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(id),
    FOREIGN KEY (IdTienda) REFERENCES Tiendas(id)
);
GO
-- SI
CREATE TABLE Clientes (
    id INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Correo VARCHAR(200) NOT NULL,
    Contrasena CHAR(64) NOT NULL,
    DireccionPrincipal TEXT,
    DUI CHAR(9),
    FechaNacimiento DATE,
    Telefono CHAR(8),
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);
GO
CREATE TABLE Pedidos (
    id INT NOT NULL PRIMARY KEY,
    IdCliente INT NOT NULL,
    FechaPedido DATETIME DEFAULT CURRENT_TIMESTAMP,
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(10) NOT NULL CHECK (Estado IN ('pendiente', 'pagado', 'cancelado')) DEFAULT 'pendiente'
    FOREIGN KEY (IdCliente) REFERENCES Clientes(id)
);
GO
CREATE TABLE DetallesPedidos (
    id INT NOT NULL PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdProducto INT NOT NULL,
    Item INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(id),
    FOREIGN KEY (IdProducto) REFERENCES Productos(id)
);
GO
CREATE TABLE Pagos (
    id INT NOT NULL PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdCliente INT NOT NULL,
    FechaPago DATETIME DEFAULT CURRENT_TIMESTAMP,
    MetodoPago VARCHAR(15) NOT NULL CHECK (MetodoPago IN ('efectivo', 'tarjeta', 'transferencia')),
    Estado VARCHAR(10) NOT NULL CHECK (Estado IN ('exitoso', 'fallido', 'pendiente')) DEFAULT 'pendiente',
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(id),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(id)
);
GO
CREATE TABLE Facturas (
    id INT NOT NULL PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdPago INT NOT NULL,
    IdCliente INT NOT NULL,
    FechaFacturacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    TipoFacturacion VARCHAR(10) NOT NULL CHECK (TipoFacturacion IN ('digital', 'fisica')),
    MetodoEnvio VARCHAR(10) NOT NULL CHECK (MetodoEnvio IN ('email', 'descarga')),
    Total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(id),
    FOREIGN KEY (IdPago) REFERENCES Pagos(id),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(id)
);
GO