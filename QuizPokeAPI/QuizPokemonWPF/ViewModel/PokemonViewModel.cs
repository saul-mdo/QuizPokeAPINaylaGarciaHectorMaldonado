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

        public int Puntaje { get; set; } = 0;

        void Lanzar(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PokemonViewModel()
        {
            LlenarLista();

            InciarQuizCommand = new RelayCommand(IniciarQuiz);
            SiguienteCommand = new RelayCommand(Siguiente);
            ReiniciarQuizCommand = new RelayCommand(ReiniciarQuiz);
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
