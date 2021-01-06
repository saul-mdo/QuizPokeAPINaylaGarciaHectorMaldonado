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
              await LlenarListaPokemon();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();

        }

        
        public IActionResult Quiz()
        {
            QuizViewModel qvm = new QuizViewModel();

            return View(qvm);
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

        private async Task<Pokemon> LlenarListaPokemon()
        {
            QuizViewModel vm = new QuizViewModel();
            vm.ListaPokemones = new List<Pokemon>();
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
                    vm.ListaPokemones.Add(p);
                    i++;
                }
            }

            return null;

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

    }
}
