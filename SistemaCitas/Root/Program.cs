using SistemaCitas.Interfaces;
using SistemaCitas.Menus;
using SistemaCitas.Repositories;
using SistemaCitas.Services;

IClinicaRepositorio repositorio = new ClinicaRepositorioMemoria();
INotificador notificador = new EmailNotificador();

ClinicaService clinicaService = new ClinicaService(repositorio);
CitaService citaService = new CitaService(repositorio, notificador);

MenuPrincipal menu = new MenuPrincipal(clinicaService, citaService);
menu.Mostrar();