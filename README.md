Sistema de Gestión de Citas Médicas 

<img width="1919" height="919" alt="image" src="https://github.com/user-attachments/assets/5ab95fe8-ecfa-42dd-a73d-b8b9f4e96aad" />
1. Análisis del problema
Qué problema tiene la clínica: Actualmente, la clínica sufre de desorganización debido a un proceso manual basado en llamadas y hojas de cálculo. Esto provoca citas duplicadas, dificultad para consultar la disponibilidad real de los médicos, mala gestión de las cancelaciones, pérdida de tiempo repitiendo datos y una falta total de comunicación automática (recordatorios) con los pacientes.

Qué módulos identifica: 1.  Módulo de Entidades Básicas: Gestión de Pacientes, Especialidades y Médicos.
2.  Módulo de Agenda: Gestión de Citas (agendar, consultar, reprogramar y cancelar).
3.  Módulo de Utilidades: Validaciones globales y Notificaciones.

Qué funcionalidades pertenecen a la primera versión: Registrar pacientes, médicos y especialidades; asignar especialidades a los médicos; agendar, consultar, cancelar y reprogramar citas; y enviar notificaciones de confirmación o cambios por correo.

Qué funcionalidades deberían dejarse para versiones futuras: Aplicando el principio YAGNI, se descartan para esta versión: facturación, recetas médicas, historial clínico, seguros médicos, telemedicina, chat médico e inteligencia artificial. Estas requieren un análisis más profundo y no solucionan el problema urgente de la agenda.

2. Diseño orientado a objetos
Basado en el principio de Separación de Responsabilidades (SoC) y el de Responsabilidad Única (SRP), el sistema se divide en entidades (modelos) y servicios lógicos.

Clases principales y sus atributos:

Paciente:

Atributos: Id (entero), Nombre (texto), Email (texto).

Especialidad:

Atributos: Id (entero), Nombre (texto).

Medico:

Atributos: Id (entero), Nombre (texto), Especialidad (Objeto tipo Especialidad).

Cita:

Atributos: Id (entero), Paciente (Objeto tipo Paciente), Medico (Objeto tipo Medico), FechaHora (Fecha y hora), EstaCancelada (booleano).

Clases de Servicio y Métodos principales:

ClinicaService (Gestión de registros):

Métodos: RegistrarPaciente(), RegistrarEspecialidad(), RegistrarMedico(), ObtenerPacientes(), ObtenerMedicos().

CitaService (Gestión de la agenda):

Métodos: AgendarCita(), ConsultarPorPaciente(), ConsultarPorMedico(), CancelarCita(), ReprogramarCita().

Validador (Clase estática de utilidades):

Método: ValidarTexto().

Relaciones entre clases:

Asociación / Agregación: Un Medico tiene una Especialidad. La clase Medico contiene una propiedad que hace referencia a la clase Especialidad.

Asociación Múltiple: Una Cita relaciona a un Paciente con un Medico en un momento específico. La clase Cita contiene instancias de ambas clases.

Inyección de Dependencias (Realización): CitaService no crea notificaciones directamente, sino que depende de la interfaz INotificador y de la interfaz IClinicaRepositorio. Cualquier clase que implemente INotificador (como EmailNotificador) se relaciona con el servicio a través de su constructor.
Este proyecto es una aplicación de consola desarrollada en C# y .NET 8 que permite a una clínica médica gestionar su agenda de manera eficiente, abandonando los procesos manuales propensos a errores. El sistema permite registrar pacientes, médicos y especialidades, así como agendar, reprogramar y cancelar citas médicas evitando conflictos de horarios. Todo el código está estructurado bajo una arquitectura modular estricta que aplica los principios arquitectónicos modernos: SOLID, SoC, DRY, KISS y YAGNI.

Comenzando 
Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas.

Mira Despliegue para conocer cómo desplegar el proyecto.

Pre-requisitos 📋
Qué cosas necesitas para instalar el software y cómo instalarlas:

.NET 8.0 SDK instalado en tu equipo.

Un IDE compatible con C# como Visual Studio 2022 o Visual Studio Code.

Git para clonar el repositorio.

Ejemplo de verificación de requisitos en la terminal:

Bash
dotnet --version
# Debería devolver algo como: 8.0.x
Instalación 🔧
Una serie de ejemplos paso a paso que te dice lo que debes ejecutar para tener un entorno de desarrollo ejecutándose.

Clona el repositorio en tu máquina local:

Bash
git clone https://github.com/tu-usuario/SistemaCitas.git
Navega hasta el directorio raíz del proyecto:

Bash
cd SistemaCitas
Restaura las dependencias y compila el proyecto:

Bash
dotnet build
Ejecuta la aplicación de consola:

Bash
dotnet run
Para obtener datos del sistema o usarlo para una pequeña demo, al iniciar la aplicación verás un menú interactivo. Simplemente escribe el número 1 para registrar un paciente (ej. Nombre: Juan, Email: juan@correo.com), luego usa el número 2 para una especialidad, y finalmente explora las opciones 4 y 5 para agendar y consultar las citas guardadas en memoria.

Ejecutando las pruebas ⚙️
Debido a la inyección de dependencias (DIP) y la segregación de interfaces (ISP) aplicadas en la arquitectura, este sistema está preparado para la integración de pruebas unitarias automatizadas utilizando frameworks como xUnit o NUnit haciendo mocking de la interfaz IClinicaRepositorio.

Actualmente, las pruebas se realizan de manera funcional a través de la interfaz de consola interactiva.

Analice las pruebas end-to-end 🔩
Estas pruebas verifican que las reglas de negocio del sistema no permitan inconsistencias en la memoria de la clínica.

Prueba de Conflicto de Horario: Intenta agendar dos citas con el mismo médico a la misma fecha y hora exacta. El sistema arrojará la excepción: "El médico está ocupado en ese horario".

Prueba de Restricción Temporal: Intenta agendar o reprogramar una cita para el día de ayer. El sistema arrojará la excepción: "No puedes agendar en el pasado".

Y las pruebas de estilo de codificación ⌨️
Estas pruebas verifican que se cumpla la validación de integridad de los datos de entrada (Principio DRY).

Prueba de Campos Vacíos: Al intentar registrar un paciente, presiona Enter sin escribir un nombre. La clase estática Validador.cs capturará el error y mostrará: "El campo 'Nombre del Paciente' no puede estar vacío".

Despliegue 📦
Para desplegar la aplicación y generar un ejecutable .exe que pueda correr en cualquier máquina Windows sin necesidad de tener .NET instalado (Self-Contained), ejecuta el siguiente comando en la raíz del proyecto:

Bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
El archivo ejecutable se generará en la ruta: bin\Release\net8.0\win-x64\publish\.

Construido con 🛠️
C# 12 - Lenguaje de programación.

.NET 8 - El framework principal.

System.Console - Utilizado para la interfaz interactiva.

LINQ - Utilizado para las consultas eficientes de datos en memoria.



Versionado 📌
Usamos SemVer para el versionado. Para todas las versiones disponibles, mira los tags en este repositorio.

Autores ✒️
Raymel Ramirez - Desarrollo, Diseño y Arquitectura de Software - Estudiante de Desarrollo en Software

