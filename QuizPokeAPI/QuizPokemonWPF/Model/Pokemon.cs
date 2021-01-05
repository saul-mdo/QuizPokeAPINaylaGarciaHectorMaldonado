using System;
using System.Collections.Generic;
using System.Text;
using static QuizPokemonWPF.Model.InfoPokemon;
using Type = QuizPokemonWPF.Model.InfoPokemon.Type;

namespace QuizPokemonWPF.Model
{
    public class Pokemon
    {
        public List<Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public List<Move> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public Sprites sprites { get; set; }
        public List<Type> types { get; set; }
        public int weight { get; set; }
    }











}
