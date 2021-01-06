using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PokeQuizWeb.Models;

namespace PokeQuizWeb.Models.ViewModels
{
    public class QuizViewModel
    {


        //public ObservableCollection<Pregunta> ListaPreguntas { get; set; }
        public List<Pokemon> ListaPokemones { get; set; }

        public Pregunta P1 { get; set; }
        public Pregunta P2 { get; set; }
        public Pregunta P3 { get; set; }
        public Pregunta P4 { get; set; }
        public Pregunta P5 { get; set; }
        public Pregunta P6 { get; set; }
        public Pregunta P7 { get; set; }
        public Pregunta P8 { get; set; }
        public Pregunta P9 { get; set; }
        public Pregunta P10 { get; set; }
        public int Puntaje { get; set; }


    }
}
