Este proyecto es un panel administrativo para la gestión de eventos en *Sunset Restaurant*, desarrollado con ASP.NET MVC. Incluye funcionalidades como login de personal, visualización de tareas, gestión de eventos y un calendario interactivo.

## Características Principales

### 🗓️ Calendario de Eventos
- Usa la librería *FullCalendar*.
- Muestra eventos cargados desde el servidor (ViewBag.Eventos).
- Vista mensual, semanal y diaria.

### 📋 Dashboard Principal
- Panel lateral con accesos rápidos:
  - Tareas
  - Registro de usuario
  - Crear evento
  - Modificar evento
  - Ver registros
  - Ver calendario
- Bienvenida personalizada con nombre de usuario y avatar.

### 🔐 Acceso Personal
- Formulario de login para el personal.
- Validación de credenciales.
- Muestra mensajes de error si el acceso falla.

### 📁 Ver Registros
- Tabla con los eventos registrados.
- Incluye datos como cliente, tipo de evento, número de invitados, adelanto, total y fecha.
- Botones para actualizar o eliminar eventos.

### ✅ Lista de Tareas
- Muestra todas las tareas asociadas a eventos.
- Incluye datos del evento, responsable asignado y fecha.
- Visualización clara de la planificación y organización.

## Tecnologías Utilizadas

- ASP.NET MVC
- Razor
- C#
- HTML, CSS, Bootstrap
- FullCalendar.js
- SQL Server (en el backend)
