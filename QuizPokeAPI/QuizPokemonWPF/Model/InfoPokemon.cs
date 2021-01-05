using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static QuizPokemonWPF.Model.InfoPokemon;
using Type = QuizPokemonWPF.Model.InfoPokemon.Type;

namespace QuizPokemonWPF.Model
{
    public class InfoPokemon
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Ability2
        {
            public string name { get; set; }
        }

        public class Ability
        {
            public Ability2 ability { get; set; }
        }

        public class Move2
        {
            public string name { get; set; }
        }

        public class Move
        {
            public Move2 move { get; set; }
        }

        public class Sprites
        {
            public string back_default { get; set; }
            public object back_female { get; set; }
            public string back_shiny { get; set; }
            public object back_shiny_female { get; set; }
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }


        public class Type2
        {
            public string name { get; set; }
        }

        public class Type
        {
            public Type2 type { get; set; }
        }

    }


}
