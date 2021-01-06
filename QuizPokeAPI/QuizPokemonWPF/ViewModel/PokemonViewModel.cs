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

        //public int numRandomLista()
        //{
        //    Random r = new Random();
        //    int num = r.Next(1, 20);
        //    return num;
        //}

        public void generarRespuesta1()
        {
            var poke = listaPokes[3];

            respuestaCorrecta = poke.name;
            TextoPregunta = poke.types[0].type.name;

            var pokeExtra1 = listaPokes[19];
            var pokeExtra2 = listaPokes[5];
            var pokeExtra3 = listaPokes[4];

            if (pokeExtra1.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra1 = listaPokes[12];
            }
            if (pokeExtra2.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra2 = listaPokes[9];
            }
            if (pokeExtra3.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra3 = listaPokes[16];
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = respuestaCorrecta;
            RP4 = pokeExtra3.name;
            Lanzar();
        }
        public void generarRespuesta2()
        {
            var poke = listaPokes[14];

            respuestaCorrecta = poke.name;

            TextoPregunta = poke.abilities[0].ability.name;

            var pokeExtra1 = listaPokes[2];
            var pokeExtra2 = listaPokes[8];
            var pokeExtra3 = listaPokes[1];

            if (pokeExtra1.abilities[0].ability.name == poke.abilities[0].ability.name)
            {
                pokeExtra1 = listaPokes[17];
            }
            if (pokeExtra1.abilities[0].ability.name == poke.abilities[0].ability.name)
            {
                pokeExtra2 = listaPokes[6];
            }
            if (pokeExtra1.abilities[0].ability.name == poke.abilities[0].ability.name)
            {
                pokeExtra3 = listaPokes[7];
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = pokeExtra3.name;
            RP4 = respuestaCorrecta;
            Lanzar();
        }
        public void generarRespuesta3()
        {
            var poke = listaPokes[8];
            
            Imagen = poke.sprites.front_default;

            respuestaCorrecta = poke.name;

            var pokeExtra1 = listaPokes[13];
            var pokeExtra2 = listaPokes[11];
            var pokeExtra3 = listaPokes[10];

            if (pokeExtra1.name == poke.name)
            {
                pokeExtra1 = listaPokes[4];
            }
            if (pokeExtra2.name == poke.name)
            {
                pokeExtra2 = listaPokes[18];
            }
            if (pokeExtra3.name == poke.name)
            {
                pokeExtra3 = listaPokes[3];
            }

            RP1 = respuestaCorrecta;
            RP2 = pokeExtra1.name;
            RP3 = pokeExtra2.name;
            RP4 = pokeExtra3.name;
            Lanzar();

        }
        public void generarRespuesta4()
        {
            var poke = listaPokes[10];
            respuestaCorrecta = poke.name;
            TextoPregunta = poke.moves[0].move.name;


            var pokeExtra1 = listaPokes[8];
            var pokeExtra2 = listaPokes[7];
            var pokeExtra3 = listaPokes[6];

            if (pokeExtra1.moves.Any(x=>x.move.name == respuestaCorrecta))
            {
                pokeExtra1 = listaPokes[11];
            }
            if (pokeExtra2.moves.Any(x => x.move.name == respuestaCorrecta))
            {
                pokeExtra2 = listaPokes[19];
            }
            if (pokeExtra3.moves.Any(x => x.move.name == respuestaCorrecta))
            {
                pokeExtra3 = listaPokes[15];
            }

            RP1 = pokeExtra2.name;
            RP2 = pokeExtra1.name;
            RP3 = respuestaCorrecta;
            RP4 = pokeExtra3.name;
            Lanzar();
        }
        public void generarRespuesta5()
        {
            var poke = listaPokes[8];

            var cont = 0;

            TextoPregunta = poke.name;
            TextoPregunta2 = listaPokes[2].moves[9].move.name;

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
            var p1 = listaPokes[7];
            var p2 = listaPokes[11];
            var p3= listaPokes[16];
            var p4= listaPokes[2];

            if(p1.height> p2.height & p1.height > p3.height & p1.height > p4.height)
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
            var p1 = listaPokes[19];
            var p2 = listaPokes[1];
            var p3 = listaPokes[2];
            var p4 = listaPokes[9];

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
            var poke = listaPokes[17];

            Imagen = poke.sprites.front_default;

            respuestaCorrecta = poke.types[0].type.name;

            var pokeExtra1 = listaPokes[10];
            var pokeExtra2 = listaPokes[3];
            var pokeExtra3 = listaPokes[18];

            if (pokeExtra1.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra1 = listaPokes[6];
            }
            if (pokeExtra2.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra2 = listaPokes[2];
            }
            if (pokeExtra3.types[0].type.name == poke.types[0].type.name)
            {
                pokeExtra3 = listaPokes[11];
            }

            RP1 = pokeExtra3.types[0].type.name;
            RP2 = pokeExtra2.types[0].type.name;
            RP3 = pokeExtra1.types[0].type.name;
            RP4 = respuestaCorrecta;
            Lanzar();
        }
        public void generarRespuesta9()
        {
            var poke = listaPokes[5];

            var cont = 0;

            TextoPregunta = poke.name;
            TextoPregunta2 = listaPokes[9].types[0].type.name;

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
            var p1 = listaPokes[8];
            var p2 = listaPokes[2];
            var p3 = listaPokes[1];
            var p4 = listaPokes[17];

            if (p1.types.Count() > 1)
            {
                p1 = listaPokes[5];
            }
            else if (p2.types.Count() > 1)
            {
                p1 = listaPokes[15];
            }
            else if(p3.types.Count() > 1)
            {
                p1 = listaPokes[20];
            }

            if (p1.types.Count() == 2)
            {
                respuestaCorrecta = p1.name;
            } 
            else if (p2.types.Count() == 2)
            {
                respuestaCorrecta = p2.name;
            }
            else if(p3.types.Count() == 2)
            {
                respuestaCorrecta = p3.name;
            }
            else
            {
                respuestaCorrecta = p4.name;
            }

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