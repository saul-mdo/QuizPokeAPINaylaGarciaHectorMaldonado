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

        public Pokemon Pokemon { get; set; } = new Pokemon();

        void Lanzar(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      


        public PokemonViewModel()
        {

            var i = 0;

            while (i<20)
            {
                var id = IdPokemonRandom();
                GetPokemon(id);
                i++;

            }

            InciarQuizCommand = new RelayCommand(IniciarQuiz);
            SiguienteCommand = new RelayCommand(Siguiente);
            SiguienteCommand2 = new RelayCommand(Siguiente2);
        }

        private void Siguiente2()
        {
            ModalVisible = Modal.p3;
            //contador++;
            Lanzar();
        }

        private void Siguiente()
        {
            //int contador = 1;
            //if (contador <= 10)
            //{
            //    //string ventana = $"p{contador}";
                ModalVisible = Modal.p2;
                //contador++;
                Lanzar();

            //}

        }

        public int IdPokemonRandom()
        {
            Random r = new Random();
           int idRandom = r.Next(1, 898);
            return idRandom;
        }

      
        public void IniciarQuiz()
        {
            //var id = IdPokemonRandom();
            //GetPokemon(id);
            // CAMBIAR MODAL A LA PRGUNTA 1
           // contador++;
            ModalVisible = Modal.p1;
            Lanzar();

        }

        async void GetPokemon(int idPokemon)
        {
            var result = await client.GetAsync($"api/v2/pokemon/{idPokemon}");
            if (result.IsSuccessStatusCode)
            {
                string datos = await result.Content.ReadAsStringAsync();
                Pokemon = JsonConvert.DeserializeObject<Pokemon>(datos);
                listaPokes.Add(Pokemon);
                Lanzar();
            }
        }

        public ICommand InciarQuizCommand { get; set; }
        public ICommand SiguienteCommand { get; set; }
        public ICommand SiguienteCommand2 { get; set; }
    }
}
