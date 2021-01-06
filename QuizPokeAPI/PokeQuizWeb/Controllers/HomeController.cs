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

        public async Task<IActionResult> Index()
        {
            try
            {

                //traer lo pokemones
                //var i = 0;
                //while (i < 20)
                //{
                //    var id = IdPokemonRandom();
                //    vm.ListaPokemones.Add(await GetPokemon(id));
                //    vm.ListaPokemones = await GetPokemones();
                //    i++;
                //}
                //ViewBag.Texto = vm.ListaPokemones[0].name.ToString();
                //llenar las preguntas
                //LLenarPreguntas(vm);


              await GetPokemon();

               var i = 3;


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();

        }

        private void LLenarPreguntas(QuizViewModel vm)
        {
            //pregunta1
            //vm.P1.TextoPregunta = vm.ListaPokemones[6].types[6].Name;
            //vm.P1.Opcion1 = vm.ListaPokemones[7].name;
            //vm.P1.Opcion2 = vm.ListaPokemones[1].name;
            //vm.P1.Opcion3 = vm.ListaPokemones[9].name;
            //vm.P1.OpcionRespuesta = vm.ListaPokemones[6].name;
            ////pregunta2

        }

        public int IdPokemonRandom()
        {
            Random r = new Random();
            int idRandom = r.Next(1, 898);
            return idRandom;
        }

        [HttpPost]
        //public IActionResult Index(QuizViewModel vm)
        //{
            
        //    try
        //    {

        //        //se trae las respuestas
        //        //se comparan con las respuesta correcta
        //        //se muestran los resultados


        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);


        //    }

        //    return RedirectToAction("Puntaje");

        //}



        public async Task<IActionResult> Quiz()
        {
            QuizViewModel qvm = new QuizViewModel();

            

            return View(qvm);
        }


        public IActionResult Puntaje()
        {
            return View();
        }

       

        private async Task<Pokemon> GetPokemon()
        {
            QuizViewModel vm = new QuizViewModel();
            vm.ListaPokemones = new List<Pokemon>();
            var id = IdPokemonRandom();
            c = Factory.CreateClient("pokemones");
            var response = await c.GetAsync($"api/v2/pokemon/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var p = JsonConvert.DeserializeObject<Pokemon>(json);
                // var p = JsonConvert.DeserializeObject<Pokemon>(json);
                vm.ListaPokemones.Add(p);
                //return x;
                return p;
            }
            else
            {
                return null;
            }

        }

        //private async Task<List<Pokemon>> GetPokemones()
        //{

        //    c = Factory.CreateClient("pokemones");
        //    var response = await c.GetAsync("api/v2/pokemon/?limit=20&offset=20");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(json);
        //        return Pokemons;


        //    }

        //    return null;
        //}



    }
}
