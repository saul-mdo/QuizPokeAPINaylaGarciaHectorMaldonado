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
            QuizViewModel vm = new QuizViewModel();
            List<Pokemon> ListPoke = new List<Pokemon>();
            try
            {
               
                //traer lo pokemones
                var i = 0;
                while (i < 20)
                {
                    var id = IdPokemonRandom();
                    var nuevo = await GetPokemon(id);
                    // vm.ListaPokemones = await GetPokemones();
                    ListPoke.Add(nuevo);
                    i++;
                }
                //ViewBag.Texto = vm.ListaPokemones[0].name.ToString();
                vm.ListaPokemones = ListPoke;
                //llenar las preguntas
                //LLenarPreguntas(vm);



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(vm);

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
        public async Task<IActionResult> Index(QuizViewModel vm)
        {
            
            try
            {

                //se trae las respuestas
                //se comparan con las respuesta correcta
                //se muestran los resultados


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);


            }

            return RedirectToAction("Puntaje");

        }



        public IActionResult Quiz()
        {
            return View();
        }


        public IActionResult Puntaje()
        {
            return View();
        }

       

        private async Task<Pokemon> GetPokemon(int id)
        {
           
            c = Factory.CreateClient("pokemones");
            var response = await c.GetAsync($"api/v2/pokemon/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var Pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
                return Pokemon;

               
            }

            return null;
        }

        private async Task<List<Pokemon>> GetPokemones()
        {

            c = Factory.CreateClient("pokemones");
            var response = await c.GetAsync("api/v2/pokemon/?limit=20&offset=20");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(json);
                return Pokemons;


            }

            return null;
        }



    }
}
