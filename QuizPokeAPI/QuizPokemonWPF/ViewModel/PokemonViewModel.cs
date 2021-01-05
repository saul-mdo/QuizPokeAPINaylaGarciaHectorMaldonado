using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using QuizPokemonWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            respuestaCorrecta = "";
            TextoPregunta = "";
            TextoPregunta2 = "";
        }

        int contador = 0;

        private void Siguiente()
        {
            botonSiguienteActivo = false;
            botonRespuestasActivo = true;
            if (contador <= 9)
            {
                ModalVisible = (Modal)contador;
                switch (ModalVisible)
                {
                    case Modal.p1:
                        generarRespuesta1();
                        break;
                    case Modal.p2:
                        generarRespuesta2();
                        break;
                    case Modal.p3:
                        generarRespuestas3();
                        break;
                    case Modal.p4:
                        break;
                    case Modal.p5:
                        TextoPregunta = "BINDING PREGUNTA 5";
                        TextoPregunta2 = "SEGUNDO BINDING PREGUNTA 5";
                        break;
                    case Modal.p6:
                       
                        break;
                    case Modal.p7:
                      
                        break;
                    case Modal.p8:
                        
                        break;
                    case Modal.p9:
                        TextoPregunta = "BINDING PREGUNTA 9";
                        TextoPregunta2 = "SEGUNDO BINDING PREGUNTA 9";
                        break;
                    case Modal.p10:
                       
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

            RP1 = listaPokes[numRandomLista()].name;
            RP2 = respuestaCorrecta;
            RP3 = listaPokes[numRandomLista()].name;
            RP4 = listaPokes[numRandomLista()].name; 

        }
        public void generarRespuesta2()
        {
            var poke = listaPokes[numRandomLista()];
            respuestaCorrecta = poke.name;
            TextoPregunta = poke.abilities[0].ability.name;

            RP1 = listaPokes[numRandomLista()].name;
            RP2 = listaPokes[numRandomLista()].name;
            RP3 = listaPokes[numRandomLista()].name;
            RP4 = respuestaCorrecta;
        }

        public void generarRespuestas3()
        {
            var poke = listaPokes[numRandomLista()];
            Imagen = poke.sprites.front_default;

            respuestaCorrecta = poke.name;

            RP1 = respuestaCorrecta;
            RP2 = listaPokes[numRandomLista()].name;
            RP3 = listaPokes[numRandomLista()].name;
            RP4 = listaPokes[numRandomLista()].name;


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