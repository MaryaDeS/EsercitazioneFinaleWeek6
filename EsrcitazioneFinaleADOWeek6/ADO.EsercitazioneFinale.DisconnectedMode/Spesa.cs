using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.EsercitazioneFinale.DisconnectedMode
{
    public class Spesa
    {
        public int ID { get; set; }
   
        public DateTime Date { get; set; }
        public string Descrizione { get; set; } 
        public string Utente { get; set; }
        public int Importo { get; set; }
        public bool Approvata { get; set; }
        public int Description { get; internal set; }
    }
}
