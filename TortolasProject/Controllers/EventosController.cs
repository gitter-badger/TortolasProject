﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TortolasProject.Models.Repositorios;
using TortolasProject.Models;

namespace TortolasProject.Controllers
{
    public class EventosController : Controller
    {
        // GET: /Eventos/
        mtbMalagaDataContext bd = new mtbMalagaDataContext();
        EventosRepositorio EventosRepo = new EventosRepositorio();
        UsuariosRepositorio UsuariosRepo = new UsuariosRepositorio();

          //Index
        public ActionResult Index()
        {
            return View();
        }

         
        [HttpPost]
        public ActionResult cargarVistaCrearEvento()
        {
            return PartialView("VistaCrearEvento");
        }

        [HttpPost]
        public ActionResult LeerTodos()
        {
            var eventos = from ob in EventosRepo.listarEventos()
                          select new
                          {
                              idEvento = ob.idEvento,
                              Titulo = ob.Titulo,
                              Lugar = ob.Lugar,
                              Actividad = ob.Actividad,
                              FechaAperturaInscripcion = ob.FechaAperturaInscripcion.ToShortDateString(),
                              FechaLimiteInscripcion = ob.FechaLimiteInscripcion.ToShortDateString(),
                              FechaRealizacion = ob.FechaRealizacion.ToShortDateString(),
                              PrioridadSocios = ob.PrioridadSocios,
                              Plazas = ob.Plazas,
                              NumAcompa = ob.NumAcompa
                          };
            return Json(eventos);
        }

        [HttpPost]
        public void InscripcionEvento(FormCollection data)
        {
            Guid idDocumentoInscripcion = Guid.NewGuid();
            Guid idEvento = Guid.Parse(data["idEvento"]);
            int NumAcompa = int.Parse(data["numacompa"]);
            Guid FKUsuario = UsuariosRepo.obtenerUsuarioByUser(HomeController.obtenerUserIdActual());
            tbDocInscripcion DocInscrip = new tbDocInscripcion
            {
                idDocumentoInscripcion = idDocumentoInscripcion,
                Pagado = false,
                NumAcom = NumAcompa,
                FKCursillo = null,
                FKEvento = idEvento,
                FKUsuario = FKUsuario
            };
            EventosRepo.inscripcionEvento(DocInscrip);
            
        }

        [HttpPost]

        public void UpdateEvento(FormCollection data)
        {
            Guid idEvento = Guid.Parse(data["idEvento"]);
            String Titulo = data["TituloUpdate"];
            String Lugar = data["LugarUpdate"];
            DateTime FechaRealizacion = DateTime.Parse(data["FechaRealizacionUpdate"]);
            DateTime FechaAperturaIncripcion = DateTime.Parse(data["FechaAperturaInscripUpdate"]);
            DateTime FechaLimiteIncripcion = DateTime.Parse(data["FechaLimiteInscripUpdate"]);
            int Plazas = int.Parse(data["PlazasUpdate"]);
            int NumAcompa = int.Parse(data["NumAcompaUpdate"]);
            bool PrioridadSocios = bool.Parse(data["PrioridadSociosUpdate"]);
            String Actividad = data["ActividadUpdate"];

            tbEvento Evento = new tbEvento
            {
                Titulo = Titulo,
                Lugar = Lugar,
                FechaRealizacion = FechaRealizacion,
                FechaAperturaInscripcion = FechaAperturaIncripcion,
                FechaLimiteInscripcion = FechaLimiteIncripcion,
                Plazas = Plazas,
                NumAcompa = NumAcompa,
                PrioridadSocios = PrioridadSocios,
                Actividad = Actividad
            };

            EventosRepo.editarEvento(idEvento,Evento);
        }
        [HttpPost]
        public void CreateEvento(FormCollection data)
        {
            Guid idEvento = Guid.NewGuid();
            String Titulo = data["TituloUpdate"];
            String Lugar = data["LugarUpdate"];
            DateTime FechaRealizacion = DateTime.Parse(data["FechaRealizacionUpdate"]);
            DateTime FechaAperturaIncripcion = DateTime.Parse(data["FechaAperturaInscripUpdate"]);
            DateTime FechaLimiteIncripcion = DateTime.Parse(data["FechaLimiteInscripUpdate"]);
            int Plazas = int.Parse(data["PlazasUpdate"]);
            int NumAcompa = int.Parse(data["NumAcompaUpdate"]);
            bool PrioridadSocios = bool.Parse(data["PrioridadSociosUpdate"]);
            String Actividad = data["ActividadUpdate"];
            Guid FKUsuario =  UsuariosRepo.obtenerUsuarioByUser(HomeController.obtenerUserIdActual());

            tbEvento Evento = new tbEvento
            {
                idEvento = idEvento,
                Titulo = Titulo,
                Lugar = Lugar,
                FechaRealizacion = FechaRealizacion,
                FechaAperturaInscripcion = FechaAperturaIncripcion,
                FechaLimiteInscripcion = FechaLimiteIncripcion,
                Plazas = Plazas,
                NumAcompa = NumAcompa,
                PrioridadSocios = PrioridadSocios,
                Actividad = Actividad,
                FKUsuarioCreador = FKUsuario
            };

            EventosRepo.crearEvento(Evento);
        }
        [HttpPost]
        public void eliminarEvento(FormCollection data)
        {
            Guid idEvento = Guid.Parse(data["idEvento"]);

            EventosRepo.eliminarEvento(idEvento);
        }
    }

}

