using Newtonsoft.Json;
using QuizPokemonWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace QuizPokemonWPF.ViewModel
{
    public class PokemonViewModel: INotifyPropertyChanged
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("https://pokeapi.co/") };

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<object> Pokemones { get; set; } = new ObservableCollection<object>();

        public PokemonViewModel()
        {
            //GetPokemon();
        }

        //async void GetPokemon()
        //{
        //    var result = await client.GetAsync("/api/v2/characteristic");
        //    if (result.IsSuccessStatusCode)
        //    {
        //        string datos = await result.Content.ReadAsStringAsync();
        //        var lista = JsonConvert.DeserializeObject<List<Pokemon>>(datos);

        //        Pokemones.Clear();

        //        lista.ForEach(x => Pokemones.Add(x));
        //    }
        //}
    }
}
