using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Digiturno1
{
    public enum EstadoTurno 
    {
        EnEspera,
        Llamado,
        Finalizado,
        Cancelado,
        Bloqueado
    }

    public class Turno
    {
        public int id {get; set;}
        public int numero {get; set;}
        public char letra {get; set;}
        public EstadoTurno estado {get; set;}
        public DateTime fechaHoraSolicitud {get; set;}
        public DateTime? fechaHoraAtencion { get; set;}
        public DateTime? fechaHoraFinalizacion {get; set;}
        public TimeSpan tiempoTranscurrido {get; set;}

        //Relación con clase servicio
        public int servicioAsignadoId {get; set;}
        public Servicio servicioAsignado {get; set;}

        public bool bloqueado {get; set;}

        public string codigo => $"{letra}{numero:D3}";//D3 para que tenga al menos 3 digitos
    
    }
}
