﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeQuizWeb.Models;
using static PokeQuizWeb.Models.InfoPokemon;
using static QuizPokemonWPF.Model.InfoPokemon.Ability;
using static QuizPokemonWPF.Model.InfoPokemon.Type;
using static QuizPokemonWPF.Model.InfoPokemon.Move;
using Type = PokeQuizWeb.Models.InfoPokemon.Type;

namespace PokeQuizWeb.Models
{
    public class Pokemon
    {
        public List<Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public List<Form> forms { get; set; }
        public List<GameIndice> game_indices { get; set; }
        public int height { get; set; }
        public List<object> held_items { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public List<Move> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public Species species { get; set; }
        public Sprites sprites { get; set; }
        public List<Stat> stats { get; set; }
        public List<Type> types { get; set; }
        public int weight { get; set; }
    }
}
