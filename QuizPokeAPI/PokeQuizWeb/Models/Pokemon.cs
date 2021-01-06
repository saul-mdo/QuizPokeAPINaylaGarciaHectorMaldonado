using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeQuizWeb.Models;

namespace PokeQuizWeb.Models
{
    public class Pokemon
    {
        public List<InfoPokemon.Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public List<InfoPokemon.Move> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public InfoPokemon.Sprites sprites { get; set; }
        public List<Type> types { get; set; }
        public int weight { get; set; }
    }
}
