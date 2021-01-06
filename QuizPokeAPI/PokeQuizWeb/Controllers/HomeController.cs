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

        public List<Pokemon> ListaPokemones { get; set; }
        public async Task<IActionResult> Index()
        {
            QuizViewModel vm = new QuizViewModel();


            try
            {
               // var i = 0;
                //while (i < 20)
                //{
                    Random r = new Random();
                    int idRandom = r.Next(1, 898);
                    var p = await GetPokemon(idRandom);
                    ListaPokemones.Add(p);
              
                //  i++;
                //}

                //ListaPokemones = await GetPokemons();
                //PREGUNTA 1
                vm.P1.TextoPregunta = ListaPokemones[0].types[0].Name;
                vm.P1.OpcionRespuesta = ListaPokemones[0].name;
                vm.P1.Opcion1 = ListaPokemones[0].name;
                vm.P1.Opcion2 = ListaPokemones[0].name;
                vm.P1.Opcion3 = ListaPokemones[0].name;

                

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }


            return View(vm);

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



    }
}
