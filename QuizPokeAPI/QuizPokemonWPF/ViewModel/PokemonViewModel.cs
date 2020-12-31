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
        public ICommand InciarQuizCommand { get; set; }
        public ICommand SiguienteCommand { get; set; }
        public ICommand ReiniciarQuizCommand { get; set; }
        public ICommand ValidarRespuestaCommand { get; set; }

        public int Puntaje { get; set; } = 0;

        public string TextoPregunta { get; set; } = "";
        public string TextoPregunta2 { get; set; } = "";

        void Lanzar(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PokemonViewModel()
        {
            LlenarLista();

            InciarQuizCommand = new RelayCommand(IniciarQuiz);
            SiguienteCommand = new RelayCommand(Siguiente);
            ValidarRespuestaCommand = new RelayCommand(Validar);
            ReiniciarQuizCommand = new RelayCommand(ReiniciarQuiz);
        }

        private void Validar()
        {
            
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
            // 1. HACE VISIBLE LA PANTALLA PRINCIPAL
            ModalVisible = Modal.Inicio;
            Lanzar();
            listaPokes.Clear();
            LlenarLista();
            // 2. REINICIAR LOS PUNTAJES
            Puntaje = 0;
        }

        int contador = 1;

        private void Siguiente()
        {
            if (contador <= 9)
            {
                ModalVisible = (Modal)contador;
                TextoPregunta = "";
                TextoPregunta2 = "";

                switch (ModalVisible)
                {
                    case Modal.p1:
                        TextoPregunta = "BINDING PREGUNTA 1";
                        break;
                    case Modal.p2:
                        TextoPregunta = "BINDING PREGUNTA 2";
                        break;
                    case Modal.p3:
                        TextoPregunta = "BINDING PREGUNTA 3";
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

        public void IniciarQuiz()
        {
            // BINDING A LA PREGUNTA 1
            ModalVisible = Modal.p1;
            Lanzar();

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