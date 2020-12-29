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
    public class PokemonViewModel: INotifyPropertyChanged
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("https://pokeapi.co/") };

        public event PropertyChangedEventHandler PropertyChanged;

        public Pokemon Pokemon { get; set; } = new Pokemon();

        void Lanzar(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public PokemonViewModel()
        {
            //GetPokemon(7);
            InciarQuizCommand = new RelayCommand(IniciarQuiz);
        }

        public int IdPokemonRandom()
        {
            Random r = new Random();
           int idRandom = r.Next(1, 117);
            return idRandom;
        }

        private void IniciarQuiz()
        {
            GetPokemon(IdPokemonRandom());
            // CAMBIAR MODAL A LA PRGUNTA 1
        }

        async void GetPokemon(int idPokemon)
        {
            var result = await client.GetAsync($"api/v2/pokemon/{idPokemon}");
            if (result.IsSuccessStatusCode)
            {
                string datos = await result.Content.ReadAsStringAsync();
                Pokemon = JsonConvert.DeserializeObject<Pokemon>(datos);
                Lanzar();
            }
        }

        public ICommand InciarQuizCommand { get; set; }
    }
}
