using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using QuizPokemonWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace QuizPokemonWPF.ViewModel
{
    public enum Modal { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, puntajes, Inicio }

    public class PokemonViewModel: INotifyPropertyChanged
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("https://pokeapi.co/") };

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Pokemon> listaPokes { get; set; } = new ObservableCollection<Pokemon>();

        public Modal ModalVisible { get; set; } = Modal.Inicio;
        public bool botonSiguienteActivo { get; set; } = false;
        public bool botonRespuestasActivo { get; set; } = true;
        public ICommand SiguienteCommand { get; set; }
        public ICommand ReiniciarQuizCommand { get; set; }
        public ICommand ValidarRespuestaCommand { get; set; }
        public ICommand IniciarJuegoCommand { get; set; }
        public int Puntaje { get; set; } = 0;
        public string TextoPregunta { get; set; } = "";
        public string TextoPregunta2 { get; set; } = "";
        public string respuestaCorrecta { get; set; } = "";
        public string Imagen { get; set; } = "";

        public string RP1 { get; set; } = "";
        public string RP2 { get; set; } = "";
        public string RP3 { get; set; } = "";
        public string RP4 { get; set; } = "";

        void Lanzar(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PokemonViewModel()
        {
            LlenarLista();
            SiguienteCommand = new RelayCommand(Siguiente);
            ValidarRespuestaCommand = new RelayCommand<string>(Validar);
            ReiniciarQuizCommand = new RelayCommand(ReiniciarQuiz);
            IniciarJuegoCommand = new RelayCommand(InciarQuiz);
        }

        private void InciarQuiz()
        {
            ModalVisible = Modal.p1;
            generarRespuesta1();
            Lanzar();
            Puntaje = 0;
        }

        private void Validar(string respuesta)
        {
            botonRespuestasActivo = false;
            botonSiguienteActivo = true;

            if (respuesta.ToUpper() == respuestaCorrecta.ToUpper())
            {
                Puntaje = Puntaje + 10;
            }
            else
            {
                Puntaje = Puntaje;
            }
            Lanzar();
        }

        public void LlenarLista()
        {
            var i = 0;
            while (i < 20)
            {
                var id = IdPokemonRandom();
                GetPokemon(id);
                i++;
            }
        }

        private void ReiniciarQuiz()
        {
            ModalVisible = Modal.Inicio;
            Lanzar();
            listaPokes.Clear();
            LlenarLista();
            Puntaje = 0;
            reiniciarValores();
        }

        int contador = 1;

        private void Siguiente()
        {
            botonSiguienteActivo = false;
            botonRespuestasActivo = true;
            reiniciarValores();
            if (contador <= 9)
            {
                ModalVisible = (Modal)contador;
                switch (ModalVisible)
                {
                    case Modal.p2:
                        generarRespuesta2();
                        break;
                    case Modal.p3:
                        generarRespuesta3();
                        break;
                    case Modal.p4:
                        generarRespuesta4();
                        break;
                    case Modal.p5:
                        generarRespuesta5();
                        break;
                    case Modal.p6:
                        generarRespuesta6();
                        break;
                    case Modal.p7:
                        generarRespuesta7();
                        break;
                    case Modal.p8:
                        generarRespuesta8();
                        break;
                    case Modal.p9:
                        generarRespuesta9();
                        break;
                    case Modal.p10:
                        generarRespuesta10();
                        break;
                }
                contador++;
            }
            else
            {
                ModalVisible = Modal.puntajes;
                contador = 1;
            }
            Lanzar();
        }

        public int IdPokemonRandom()
        {
            Random r = new Random();
           int idRandom = r.Next(1, 898);
            return idRandom;
        }

        async void GetPokemon(int idPokemon)
        {
            var result = await client.GetAsync($"api/v2/pokemon/{idPokemon}");
            if (result.IsSuccessStatusCode)
            {
                string datos = await result.Content.ReadAsStringAsync();
                var Pokemon = JsonConvert.DeserializeObject<Pokemon>(datos);
                listaPokes.Add(Pokemon);
                Lanzar();
            }
        }

        public int numRandomLista()
        {
            Random r = new Random();
            int num = r.Next(1, 20);
            return num;
        }

        public void generarRespuesta1()
        {
            var poke = listaPokes[numRandomLista()];

            respuestaCorrecta = poke.name;
            TextoPregunta = poke.types[0].type.name;

            var pokeExtra1 = listaPokes[numRandomLista()];
            var pokeExtra2 = listaPokes[numRandomLista()];
            var pokeExtra3 = listaPokes[numRandomLista()];

            if (pokeExtra1.types.Any(x=>x.type.name==TextoPregunta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name|| pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes.FirstOrDefault(x => x.types.Any(x => x.type.name != TextoPregunta) == true && x.name!=poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra2.types.Any(x => x.type.name == TextoPregunta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra2 = listaPokes.FirstOrDefault(x => x.types.Any(x => x.type.name != TextoPregunta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra3.types.Any(x => x.type.name == TextoPregunta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra3 = listaPokes.FirstOrDefault(x => x.types.Any(x => x.type.name != TextoPregunta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = respuestaCorrecta;
            RP4 = pokeExtra3.name;
            Lanzar();
        }
        public void generarRespuesta2()
        {
            var poke = listaPokes[numRandomLista()];

            respuestaCorrecta = poke.name;

            TextoPregunta = poke.abilities[0].ability.name;

            var pokeExtra1 = listaPokes[numRandomLista()];
            var pokeExtra2 = listaPokes[numRandomLista()];
            var pokeExtra3 = listaPokes[numRandomLista()];

            if (pokeExtra1.abilities.Any(x=>x.ability.name==TextoPregunta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes.FirstOrDefault(x => x.abilities.Any(x => x.ability.name != TextoPregunta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra2.abilities.Any(x => x.ability.name == TextoPregunta) || pokeExtra2.name == pokeExtra1.name || pokeExtra2.name == pokeExtra3.name || pokeExtra2.name == poke.name)
            {
                pokeExtra2 = listaPokes.FirstOrDefault(x => x.abilities.Any(x => x.ability.name != TextoPregunta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra3.abilities.Any(x => x.ability.name == TextoPregunta) || pokeExtra3.name == pokeExtra2.name || pokeExtra3.name == pokeExtra1.name || pokeExtra3.name == poke.name)
            {
                pokeExtra3 = listaPokes.FirstOrDefault(x => x.abilities.Any(x => x.ability.name != TextoPregunta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = pokeExtra3.name;
            RP4 = respuestaCorrecta;
            Lanzar();
        }
        public void generarRespuesta3()
        {
            var poke = listaPokes[numRandomLista()];
            
            Imagen = poke.sprites.front_default;

            respuestaCorrecta = poke.name;

            var pokeExtra1 = listaPokes[numRandomLista()];
            var pokeExtra2 = listaPokes[numRandomLista()];
            var pokeExtra3 = listaPokes[numRandomLista()];

            if (pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes.FirstOrDefault(x => x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra2.name == pokeExtra1.name || pokeExtra2.name == pokeExtra3.name || pokeExtra2.name == poke.name)
            {
                pokeExtra2 = listaPokes.FirstOrDefault(x => x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if  (pokeExtra3.name == pokeExtra1.name || pokeExtra2.name == pokeExtra3.name || pokeExtra3.name == poke.name)
            {
                pokeExtra3 = listaPokes.FirstOrDefault(x => x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }

            RP1 = respuestaCorrecta;
            RP2 = pokeExtra1.name;
            RP3 = pokeExtra2.name;
            RP4 = pokeExtra3.name;
            Lanzar();

        }
        public void generarRespuesta4()
        {
            var poke = listaPokes[numRandomLista()];
            respuestaCorrecta = poke.name;
            
            if (poke.moves.Count() > 0)
            {
                TextoPregunta = poke.moves[0].move.name;
            }
            else
            {
                poke = listaPokes[numRandomLista()];
                respuestaCorrecta = poke.name;
                TextoPregunta = poke.moves[0].move.name;
            }


            var pokeExtra1 = listaPokes[numRandomLista()];
            var pokeExtra2 = listaPokes[numRandomLista()];
            var pokeExtra3 = listaPokes[numRandomLista()];

            if (pokeExtra1.moves.Any(x=>x.move.name == TextoPregunta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes.FirstOrDefault(x => x.moves.Any(x => x.move.name != respuestaCorrecta)==true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra2.moves.Any(x => x.move.name == TextoPregunta) || pokeExtra2.name == pokeExtra1.name || pokeExtra2.name == pokeExtra3.name || pokeExtra2.name == poke.name)
            {
                pokeExtra2 = listaPokes.FirstOrDefault(x => x.moves.Any(x => x.move.name != respuestaCorrecta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }
            if (pokeExtra3.moves.Any(x => x.move.name == TextoPregunta) || pokeExtra3.name == pokeExtra1.name || pokeExtra3.name == pokeExtra2.name || pokeExtra3.name == poke.name)
            {
                pokeExtra3 = listaPokes.FirstOrDefault(x => x.moves.Any(x => x.move.name != respuestaCorrecta) == true && x.name != poke.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name);
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = respuestaCorrecta;
            RP4 = pokeExtra3.name;
            Lanzar();
        }
        public void generarRespuesta5()
        {
            var poke = listaPokes[numRandomLista()];

            var cont = 0;

            if (poke.moves.Count() > 0)
            {
                TextoPregunta = poke.name;
                TextoPregunta2 = poke.moves[0].move.name;
            }
            else
            {
                poke = listaPokes[numRandomLista()];
                respuestaCorrecta = poke.name;
                TextoPregunta = poke.name;
                TextoPregunta2 = poke.moves[0].move.name;
            }


            foreach (var item in poke.moves)
            {
                if(item.move.name == TextoPregunta2)
                {
                    cont++;
                }
            }

            if (cont>0)
            {
                respuestaCorrecta = "Cierto";
            }
            else
            {
                respuestaCorrecta = "Falso";
            }
            Lanzar();
        }
        public void generarRespuesta6()
        {
            var p1 = listaPokes[numRandomLista()];
            var p2 = listaPokes[numRandomLista()];
            var p3= listaPokes[numRandomLista()];
            var p4= listaPokes[numRandomLista()];

            if (p1.name == p2.name || p1.name == p3.name || p1.name == p4.name)
            {
                p1 = listaPokes.FirstOrDefault(x => x.name != p2.name && x.name != p3.name && x.name != p4.name && x.name!= p1.name);
            }
            if (p2.name == p1.name || p2.name == p3.name || p2.name == p4.name)
            {
                p2 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p3.name && x.name != p4.name && x.name != p2.name);
            }
            if (p3.name == p1.name || p3.name == p2.name || p3.name == p4.name)
            {
                p3 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p2.name && x.name != p4.name && x.name != p3.name);
            }
            if (p4.name == p1.name || p4.name == p3.name || p4.name == p2.name)
            {
                p4 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p2.name && x.name != p3.name && x.name!=p4.name);
            }

            if (p1.height> p2.height & p1.height > p3.height & p1.height > p4.height)
            {
                respuestaCorrecta = p1.name;
            }
            else if (p2.height > p1.height & p2.height > p3.height & p1.height > p4.height)
            {
                respuestaCorrecta = p2.name;
            }
            else if (p3.height > p1.height & p3.height > p2.height & p3.height > p4.height)
            {
                respuestaCorrecta = p3.name;
            }
            else
            {
                respuestaCorrecta = p4.name;
            }

            RP1 = p1.name;
            RP2 = p4.name;
            RP3 = p2.name;
            RP4 = p3.name;

            Lanzar();
        }
        public void generarRespuesta7()
        {
            var p1 = listaPokes[numRandomLista()];
            var p2 = listaPokes[numRandomLista()];
            var p3 = listaPokes[numRandomLista()];
            var p4 = listaPokes[numRandomLista()];

            if(p1.name == p2.name || p1.name == p3.name || p1.name == p4.name)
            {
                p1 = listaPokes.FirstOrDefault(x => x.name != p2.name && x.name != p3.name && x.name != p4.name);
            }
            if (p2.name == p1.name || p2.name == p3.name || p2.name == p4.name)
            {
                p2 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p3.name && x.name != p4.name);
            }
            if (p3.name == p1.name || p3.name == p2.name || p3.name == p4.name)
            {
                p3 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p2.name && x.name != p4.name);
            }
            if (p4.name == p1.name || p4.name == p3.name || p4.name == p2.name)
            {
                p4 = listaPokes.FirstOrDefault(x => x.name != p1.name && x.name != p2.name && x.name != p3.name);
            }



            if (p1.weight > p2.weight & p1.weight > p3.weight & p1.weight > p4.weight)
            {
                respuestaCorrecta = p1.name;
            }
            else if (p2.weight > p1.weight & p2.weight > p3.weight & p1.weight > p4.weight)
            {
                respuestaCorrecta = p2.name;
            }
            else if (p3.weight > p1.weight & p3.weight > p2.weight & p3.weight > p4.weight)
            {
                respuestaCorrecta = p3.name;
            }
            else
            {
                respuestaCorrecta = p4.name;
            }

            RP1 = p3.name;
            RP2 = p2.name;
            RP3 = p4.name;
            RP4 = p1.name;

            Lanzar();
        }
        public void generarRespuesta8()
        {
            var poke = listaPokes[numRandomLista()];

            Imagen = poke.sprites.front_default;

            respuestaCorrecta = poke.types[0].type.name;

            var pokeExtra1 = listaPokes[numRandomLista()];
            var pokeExtra2 = listaPokes[numRandomLista()];
            var pokeExtra3 = listaPokes[numRandomLista()];

            if (pokeExtra1.types.Any(x=>x.type.name==respuestaCorrecta) || pokeExtra1.name == pokeExtra2.name || pokeExtra1.name == pokeExtra3.name || pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes.FirstOrDefault(x => x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != pokeExtra3.name && x.name != poke.name && x.types[0].type.name != poke.types[0].type.name);
            }
            if (pokeExtra2.types.Any(x=>x.type.name==respuestaCorrecta) || pokeExtra2.name == pokeExtra1.name || pokeExtra2.name == pokeExtra3.name || pokeExtra2.name == poke.name)
            {
                pokeExtra2 = listaPokes.FirstOrDefault(x => x.name != pokeExtra2.name && x.name != pokeExtra1.name && x.name != pokeExtra3.name && x.name != poke.name && x.types[0].type.name != poke.types[0].type.name);
            }
            if (pokeExtra3.types.Any(x => x.type.name == respuestaCorrecta) || pokeExtra3.name == pokeExtra1.name || pokeExtra3.name == pokeExtra2.name || pokeExtra3.name == poke.name)
            {
                pokeExtra3 = listaPokes.FirstOrDefault(x =>x.name != pokeExtra3.name && x.name != pokeExtra1.name && x.name != pokeExtra2.name && x.name != poke.name && x.types[0].type.name != poke.types[0].type.name);
            }

            RP1 = pokeExtra3.types[0].type.name;
            RP2 = pokeExtra2.types[0].type.name;
            RP3 = pokeExtra1.types[0].type.name;
            RP4 = respuestaCorrecta;
            Lanzar();
        }
        public void generarRespuesta9()
        {
            var poke = listaPokes[numRandomLista()];

            var cont = 0;

            TextoPregunta = poke.name;
            TextoPregunta2 = listaPokes[numRandomLista()].types[0].type.name;

            foreach (var item in poke.types)
            {
                if (item.type.name == TextoPregunta2)
                {
                    cont++;
                }
            }

            if (cont > 0)
            {
                respuestaCorrecta = "Cierto";
            }
            else
            {
                respuestaCorrecta = "Falso";
            }
            Lanzar();
        }
        public void generarRespuesta10()
        {
            var p1 = listaPokes[numRandomLista()];
            var p2 = listaPokes[numRandomLista()];
            var p3 = listaPokes[numRandomLista()];
            var p4 = listaPokes[numRandomLista()];

            if (p1.types.Count() <= 1)
            {
                p1 = listaPokes.FirstOrDefault(x => x.types.Count() == 2);
            }
            if (p2.types.Count() > 1 || p2.name == p1.name || p2.name == p3.name || p2.name == p4.name)
            {
                p2 = listaPokes.FirstOrDefault(x => x.types.Count() <2 && x.name != p1.name && x.name != p2.name && x.name != p3.name && x.name != p4.name);
            }
            if (p3.types.Count() > 1 || p3.name == p1.name || p3.name == p2.name || p3.name == p4.name)
            {
                p3 = listaPokes.FirstOrDefault(x => x.types.Count() < 2 && x.name != p1.name && x.name != p2.name && x.name != p3.name &&  x.name != p4.name);
            }
            if (p4.types.Count() > 1 || p4.name == p1.name || p4.name == p3.name || p4.name == p2.name)
            {
                p4 = listaPokes.FirstOrDefault(x => x.types.Count() < 2 && x.name != p1.name && x.name != p2.name && x.name != p3.name && x.name != p4.name);
            }

            respuestaCorrecta = p1.name;

            RP1 = p1.name;
            RP2 = p4.name;
            RP3 = p3.name;
            RP4 = p2.name;

            Lanzar();
        }
        public void reiniciarValores()
        {
            RP1 = "";
            RP2 = "";
            RP3 = "";
            RP4 = "";
            respuestaCorrecta = "";
            TextoPregunta = "";
            TextoPregunta2 = "";
            Lanzar();
        }
    }
}


// PREGUNTAS
/*
1. ¿Qué pokemon es tipo __?

2. ¿Que pokemon posee la habilidad __?

3. ¿Cual es el nombre de este pokemon? (IMAGEN)

4. ¿Que pokemon tiene el movimiento ___?

5. El pokemon ___ puede aprender el movimiento ___. ¿Cierto o falso?

6. ¿Que pokemon es más alto?

7. ¿Que pokemon es más pesado?

8. ¿Que tipo es este pokemon? (IMAGEN)

9.El pokemon ____ es tipo ____. ¿Cierto o falso?

10. ¿Qué pokemon tiene dos tipos?
*/