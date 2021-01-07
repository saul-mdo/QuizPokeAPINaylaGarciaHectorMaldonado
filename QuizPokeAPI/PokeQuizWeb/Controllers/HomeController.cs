using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeQuizWeb.Models;
using PokeQuizWeb.Models.ViewModels;

namespace PokeQuizWeb.Controllers
{
    public class HomeController : Controller
    {
        public IHttpClientFactory Factory { get; set; }
        HttpClient c;
        public HomeController(IHttpClientFactory clientFactory)
        {
            Factory = clientFactory;
        }

        public List<Pokemon> ListaPoke { get; set; } = new List<Pokemon>();

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Quiz()
        {
            QuizViewModel vm = new QuizViewModel();
            vm.ListaPokemones = await LlenarListaPokemon();

            //LLENAR LAS PREGUNTAS
            //Pregunta 1
            vm.P1.TextoPregunta = vm.ListaPokemones[8].types[0].type.name;
            vm.P1.Opcion1 = vm.ListaPokemones[7].name;
            vm.P1.Opcion2 = vm.ListaPokemones[1].name;
            vm.P1.Opcion3 = vm.ListaPokemones[9].name;
            vm.P1.OpcionRespuesta = vm.ListaPokemones[8].name;
            //Pregunta 2
            vm.P2.TextoPregunta = vm.ListaPokemones[6].abilities[0].ability.name;
            vm.P2.Opcion1 = vm.ListaPokemones[10].name;
            vm.P2.Opcion2 = vm.ListaPokemones[2].name;
            vm.P2.Opcion3 = vm.ListaPokemones[0].name;
            vm.P2.OpcionRespuesta = vm.ListaPokemones[6].name;

            //Pregunta 3
            vm.P3.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P3.ImagenP3 = vm.ListaPokemones[0].sprites.front_default;
            vm.P3.Opcion1 = vm.ListaPokemones[7].name;
            vm.P3.Opcion2 = vm.ListaPokemones[13].name;
            vm.P3.Opcion3 = vm.ListaPokemones[18].name;
            vm.P3.OpcionRespuesta = vm.ListaPokemones[0].name;

            //pregunta 4
            var pk1 = vm.ListaPokemones[0];
            var pk2 = vm.ListaPokemones[4];
            var pk3 = vm.ListaPokemones[3];
            var pk4 = vm.ListaPokemones[11];

            if (pk1.moves.Count() <= 0)
            {
                pk1 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name!=pk1.name && x.name != pk2.name && x.name != pk3.name && x.name != pk4.name);
            }
            if (pk2.moves.Count() <= 0)
            {
                pk2 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != pk2.name && x.name != pk1.name && x.name != pk3.name && x.name != pk4.name);
            }
            if (pk3.moves.Count() <= 0)
            {
                pk3 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != pk3.name && x.name != pk2.name && x.name != pk1.name && x.name != pk4.name);
            }
            if (pk4.moves.Count() <= 0)
            {
                pk4 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != pk4.name && x.name != pk2.name && x.name != pk3.name && x.name != pk1.name);
            }

            vm.P4.TextoPregunta = pk1.moves[0].move.name;
            vm.P4.Opcion1 = pk3.name;
            vm.P4.Opcion2 = pk2.name;
            vm.P4.Opcion3 = pk4.name;
            vm.P4.OpcionRespuesta = pk1.name;
            //pregunta 5
            var poke1 = vm.ListaPokemones[10];
            var poke2 = vm.ListaPokemones[14];
            var poke3 = vm.ListaPokemones[13];
            var poke4 = vm.ListaPokemones[1];

            if (poke1.moves.Count() <= 0)
            {
                poke1 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != poke1.name && x.name != poke2.name && x.name != poke3.name && x.name != poke4.name);
            }
            if (poke2.moves.Count() <= 0)
            {
                poke2 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != poke2.name && x.name != poke1.name && x.name != poke3.name && x.name != poke4.name);
            }
            if (pk3.moves.Count() <= 0)
            {
                pk3 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != poke3.name && x.name != poke2.name && x.name != poke1.name && x.name != poke4.name);
            }
            if (pk4.moves.Count() <= 0)
            {
                pk4 = vm.ListaPokemones.FirstOrDefault(x => x.moves.Count() > 0 && x.name != poke4.name && x.name != poke2.name && x.name != poke3.name && x.name != poke1.name);
            }

            vm.P5.TextoPregunta = poke1.name;
            vm.P5.TextoPregunta2 = poke4.moves[0].move.name;
            vm.P5.Opcion1 = poke2.name;
            vm.P5.Opcion2 = poke4.name;
            vm.P5.Opcion3 = poke3.name;
            vm.P5.OpcionRespuesta = poke1.name;

            //pregunta 6
            var p1 = ListaPoke[5];
            var p2 = ListaPoke[18];
            var p3 = ListaPoke[2];
            var p4 = ListaPoke.FirstOrDefault(x => (x.height > p1.height) && (x.height > p2.height) && (x.height > p3.height));

            vm.P6.Opcion1 = p1.name;
            vm.P6.Opcion2 = p2.name;
            vm.P6.Opcion3 = p3.name;
            vm.P6.OpcionRespuesta = p4.name;
            //pregunta 7
            var p17 = ListaPoke[11];
            var p27 = ListaPoke[0];
            var p37 = ListaPoke.FirstOrDefault(x => (x.weight > p1.weight) && (x.weight > p2.weight) && (x.weight > p3.weight));
            var p47 = ListaPoke[3];
            vm.P7.Opcion1 = p17.name;
            vm.P7.Opcion2 = p27.name;
            vm.P7.Opcion3 = p47.name;
            vm.P7.OpcionRespuesta =p37.name;

            //pregunta 8
            vm.P8.TextoPregunta = vm.ListaPokemones[14].name;
            vm.P8.ImagenP8 = vm.ListaPokemones[14].sprites.front_default;
            vm.P8.Opcion1 = vm.ListaPokemones[7].types[0].type.name;
            vm.P8.Opcion2 = vm.ListaPokemones[16].types[0].type.name;
            vm.P8.Opcion3 = vm.ListaPokemones[12].types[0].type.name;
            vm.P8.OpcionRespuesta = vm.ListaPokemones[14].types[0].type.name;

            //pregunta 9
            vm.P9.TextoPregunta = vm.ListaPokemones[7].name;
            vm.P9.TextoPregunta2 = vm.ListaPokemones[7].types[0].type.name;
            vm.P9.Opcion1 = vm.ListaPokemones[2].name;
            vm.P9.OpcionRespuesta = vm.ListaPokemones[7].name;

            //pregunta 10
            var pkmn1 = vm.ListaPokemones[0];
            var pkmn2 = vm.ListaPokemones[19];
            var pkmn3 = vm.ListaPokemones[2];
            var pkmn4 = vm.ListaPokemones[5];

            if (pkmn1.types.Count() > 1)
            {
                pkmn1 = vm.ListaPokemones.FirstOrDefault(x => x.types.Count() < 2 && x.name!=pkmn1.name && x.name != pkmn2.name && x.name != pkmn3.name && x.name != pkmn4.name);
            }
            if (pkmn2.types.Count() > 1)
            {
                pkmn2 = vm.ListaPokemones.FirstOrDefault(x => x.types.Count() < 2 && x.name != pkmn1.name && x.name != pkmn2.name && x.name != pkmn3.name && x.name != pkmn4.name);
            }
            if (pkmn3.types.Count() < 2)
            {
                pkmn3 = vm.ListaPokemones.FirstOrDefault(x => x.types.Count() >= 2 && x.name != pkmn1.name && x.name != pkmn2.name && x.name != pkmn3.name && x.name != pkmn4.name);
            }
            if (pkmn4.types.Count() > 1)
            {
                pkmn4 = vm.ListaPokemones.FirstOrDefault(x => x.types.Count() < 2 && x.name != pkmn1.name && x.name != pkmn2.name && x.name != pkmn3.name && x.name != pkmn4.name);
            }


            vm.P10.TextoPregunta = pkmn3.name;
            vm.P10.Opcion1 = pkmn2.name;
            vm.P10.Opcion2 = pkmn4.name;
            vm.P10.Opcion3 = pkmn1.name;
            vm.P10.OpcionRespuesta = pkmn3.name;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Quiz( string res1, string res2, string res3, string res4, string res5, string res6, string res7, string res8, string res9, string res10, string pregunta1, string pregunta2, string pregunta3,
            string pregunta4, string pregunta5, string pregunta6, string pregunta7, string pregunta8, string pregunta9, string pregunta10)
        {

            int Puntaje = 0;

            if (res1 == pregunta1)
                Puntaje = Puntaje + 10;
            if(res2==pregunta2)
                Puntaje = Puntaje + 10;
            if (res3==pregunta3)
                Puntaje = Puntaje + 10;
            if (res4==pregunta4)
                Puntaje = Puntaje + 10;
            if (res5==pregunta5)
                Puntaje = Puntaje + 10;
            if (res6==pregunta6)
                Puntaje = Puntaje + 10;
            if (res7==pregunta7)
                Puntaje = Puntaje + 10;
            if (res8==pregunta8)
                Puntaje = Puntaje + 10;
            if (res9==pregunta9)
                Puntaje = Puntaje + 10;
            if (res10==pregunta10)
                Puntaje = Puntaje + 10;


            return RedirectToAction("Puntaje", new { Puntajes= Puntaje });
        }

        public IActionResult Puntaje(int Puntajes)
        {
            return View(Puntajes);
        }

        public IActionResult Reiniciar()
        {
           
            return RedirectToAction("Index");
        }


        public int IdPokemonRandom()
        {
            Random r = new Random();
            int idRandom = r.Next(1, 898);
            return idRandom;
        }

        private async Task<List<Pokemon>> LlenarListaPokemon()
        {

            var i = 0;
            while (i < 20)
            {
                var id = IdPokemonRandom();
                c = Factory.CreateClient("pokemones");
                var response = await c.GetAsync($"api/v2/pokemon/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var p = JsonConvert.DeserializeObject<Pokemon>(json);
                    ListaPoke.Add(p);
                   
                    i++;
                }
            }


            return ListaPoke;

        }

     

    }
}
