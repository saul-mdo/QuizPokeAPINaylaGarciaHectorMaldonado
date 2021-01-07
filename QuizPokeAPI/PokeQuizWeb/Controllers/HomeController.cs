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

        public async Task<IActionResult> Index()
        {
            try
            {
             // await LlenarListaPokemon();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

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
            vm.P1.OpcionRespuesta = vm.ListaPokemones[8].name; //res
            //Pregunta 2
            vm.P2.TextoPregunta = vm.ListaPokemones[6].abilities[0].ability.name;
            vm.P2.Opcion1 = vm.ListaPokemones[9].name;
            vm.P2.Opcion2 = vm.ListaPokemones[1].name;
            vm.P2.Opcion3 = vm.ListaPokemones[0].name;
            vm.P2.OpcionRespuesta = vm.ListaPokemones[6].name;

            //Pregunta 3 //PENDIENTE
            vm.P3.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P3.Imagen = vm.ListaPokemones[0].sprites.front_default;
            vm.P3.Opcion1 = vm.ListaPokemones[7].name;
            vm.P3.Opcion2 = vm.ListaPokemones[1].name;
            vm.P3.Opcion3 = vm.ListaPokemones[9].name;
            vm.P3.OpcionRespuesta = vm.ListaPokemones[6].name;

            //pregunta 4
            vm.P4.TextoPregunta = vm.ListaPokemones[15].moves[0].move.name;
            vm.P4.Opcion1 = vm.ListaPokemones[17].name;
            vm.P4.Opcion2 = vm.ListaPokemones[4].name;
            vm.P4.Opcion3 = vm.ListaPokemones[9].name;
            vm.P4.OpcionRespuesta = vm.ListaPokemones[15].name;
            //pregunta 5 //PENDIENTE
            vm.P5.TextoPregunta = vm.ListaPokemones[10].name;
            vm.P5.TextoPregunta2 = vm.ListaPokemones[10].name;
            vm.P5.Opcion1 = vm.ListaPokemones[19].name;
            vm.P5.Opcion2 = vm.ListaPokemones[11].name;
            vm.P5.Opcion3 = vm.ListaPokemones[5].name;
            vm.P5.OpcionRespuesta = vm.ListaPokemones[10].name;

            //pregunta 6
         
            vm.P6.Opcion1 = vm.ListaPokemones[14].name;
            vm.P6.Opcion2 = vm.ListaPokemones[9].name;
            vm.P6.Opcion3 = vm.ListaPokemones[7].name;
            vm.P6.OpcionRespuesta = vm.ListaPokemones[3].name;
            //pregunta 7
            vm.P7.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P7.Opcion1 = vm.ListaPokemones[7].name;
            vm.P7.Opcion2 = vm.ListaPokemones[1].name;
            vm.P7.Opcion3 = vm.ListaPokemones[9].name;
            vm.P7.OpcionRespuesta = vm.ListaPokemones[6].name;

            //pregunta 8
            vm.P8.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P8.Opcion1 = vm.ListaPokemones[7].name;
            vm.P8.Opcion2 = vm.ListaPokemones[1].name;
            vm.P8.Opcion3 = vm.ListaPokemones[9].name;
            vm.P8.OpcionRespuesta = vm.ListaPokemones[6].name;

            //pregunta 9
            vm.P9.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P9.Opcion1 = vm.ListaPokemones[7].name;
            vm.P9.Opcion2 = vm.ListaPokemones[1].name;
            vm.P9.Opcion3 = vm.ListaPokemones[9].name;
            vm.P9.OpcionRespuesta = vm.ListaPokemones[6].name;

            //pregunta 10
            vm.P10.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P10.Opcion1 = vm.ListaPokemones[7].name;
            vm.P10.Opcion2 = vm.ListaPokemones[1].name;
            vm.P10.Opcion3 = vm.ListaPokemones[9].name;
            vm.P10.OpcionRespuesta = vm.ListaPokemones[6].name;
            return View(vm);
        }


        public IActionResult Puntaje()
        {
            return View();
        }

        public async Task<IActionResult> Reiniciar()
        {
            QuizViewModel vm = new QuizViewModel();
            vm.ListaPokemones.Clear();
            await LlenarListaPokemon();
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
