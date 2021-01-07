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
            vm.P2.Opcion1 = vm.ListaPokemones[9].name;
            vm.P2.Opcion2 = vm.ListaPokemones[1].name;
            vm.P2.Opcion3 = vm.ListaPokemones[0].name;
            vm.P2.OpcionRespuesta = vm.ListaPokemones[6].name;

            //Pregunta 3 //PENDIENTE
            vm.P3.TextoPregunta = vm.ListaPokemones[0].name;
            vm.P3.ImagenP3 = vm.ListaPokemones[0].sprites.front_default;
            vm.P3.Opcion1 = vm.ListaPokemones[7].name;
            vm.P3.Opcion2 = vm.ListaPokemones[1].name;
            vm.P3.Opcion3 = vm.ListaPokemones[9].name;
            vm.P3.OpcionRespuesta = vm.ListaPokemones[0].name;

            //pregunta 4
            vm.P4.TextoPregunta = vm.ListaPokemones[15].moves[0].move.name;
            vm.P4.Opcion1 = vm.ListaPokemones[17].name;
            vm.P4.Opcion2 = vm.ListaPokemones[4].name;
            vm.P4.Opcion3 = vm.ListaPokemones[9].name;
            vm.P4.OpcionRespuesta = vm.ListaPokemones[15].name;
            //pregunta 5
            vm.P5.TextoPregunta = vm.ListaPokemones[10].name;
            vm.P5.TextoPregunta2 = vm.ListaPokemones[6].moves[10].move.name;
            vm.P5.Opcion1 = vm.ListaPokemones[19].name;
            vm.P5.Opcion2 = vm.ListaPokemones[11].name;
            vm.P5.Opcion3 = vm.ListaPokemones[5].name;
            vm.P5.OpcionRespuesta = vm.ListaPokemones[10].name;

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
            var p17 = ListaPoke[9];
            var p27 = ListaPoke[1];
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
            vm.P8.Opcion3 = vm.ListaPokemones[9].types[0].type.name;
            vm.P8.OpcionRespuesta = vm.ListaPokemones[14].types[0].type.name;

            //pregunta 9
            vm.P9.TextoPregunta = vm.ListaPokemones[7].name;
            vm.P9.TextoPregunta2 = vm.ListaPokemones[7].types[0].type.name;
            vm.P9.Opcion1 = vm.ListaPokemones[2].name;
            vm.P9.OpcionRespuesta = vm.ListaPokemones[7].name;

            //pregunta 10
            vm.P10.TextoPregunta = vm.ListaPokemones[3].name;
            vm.P10.Opcion1 = vm.ListaPokemones[7].name;
            vm.P10.Opcion2 = vm.ListaPokemones[18].name;
            vm.P10.Opcion3 = vm.ListaPokemones[4].name;
            vm.P10.OpcionRespuesta = vm.ListaPokemones[3].name;
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
