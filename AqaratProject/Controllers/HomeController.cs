using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AqaratProject.Models;
using Domains;
using BL;
using Newtonsoft.Json;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace AqaratProject.Controllers
{
    public class MySearch
    {
        public bool OnlyActive { get; set; } = true;
        public bounds Ids { get; set; }

        public string Id { get; set; }
    }
    public class bounds
    {
       
        public decimal neLat { get; set; }

        public decimal neLng { get; set; }
        public decimal swLat { get; set; }
        public decimal swLng { get; set; }
        
            
    }
    public class Feature
    {
        public string type { get; set; }

    }
    public class Position
    {
        public string type { get; set; }

        public string typee { get; set; }

    }
    public class HomeController : Controller
    {
        UnitService unitService;
        Al3QaratContext ctx;
        private readonly ILogger<HomeController> _logger;
      
        public HomeController(UnitService UnitService, Al3QaratContext Ctx, ILogger<HomeController> logger)
        {
            _logger = logger;
            ctx = Ctx;
            unitService = UnitService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Map()
        {
            return View();
        }
        public IActionResult Map2()
        {
            return View();
        }
        public IActionResult Map3()
        {
            return View();
        }


        [Authorize(Roles = "Admin,خريطة العروض")]
        public IActionResult Map4()
        {

            ViewBag.Units = unitService.getAll();
            return View();
        }
        [Authorize(Roles = "Admin,خريطة الطلبات")]
        public IActionResult Map40()
        {
            ViewBag.Units = unitService.getAll();
            return View();
        }
        public IActionResult Map6()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Map8(MySearch arr)
        {



            decimal swLat = arr.Ids.swLat - 1;
            decimal neLng = arr.Ids.neLng - 1;
            decimal neLat = arr.Ids.neLat - 1;
            decimal swLng = arr.Ids.swLng - 1;

            List<TbOffer> map1 = new List<TbOffer>();
            List<TbMap> map2 = new List<TbMap>();
            List<TbMap> map3 = new List<TbMap>();
            List<TbMap> map4 = new List<TbMap>();
            //map4 = ctx.TbMaps.ToList();
            map1 = ctx.TbOffers.Where(a => a.lat >= arr.Ids.swLat && a.lat <= arr.Ids.neLat && a.lng >= arr.Ids.swLng && a.lng <= arr.Ids.neLng).Where(a => a.UnitId == Guid.Parse(arr.Id)).Where(a => a.OfferStatus == "موافقة").Where(a=> a.contract_type != "محجوز").ToList();
            //if (neLat < swLat)
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > neLat && a.lat < swLat).ToList();
            //}
            //else
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > swLat && a.lat < neLat).ToList();

            //}



            //if (neLng < swLng)
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > neLng && a.lat < swLng).ToList();
            //}
            //else
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > swLng && a.lat < neLat).ToList();

            //}
            //map3 = map1.Concat(map2).ToList();
            //foreach (var i in map3)
            //{

            //        map4.Add(i);

            //}
            var json = Json(map1);

            return json;
        }



        [HttpPost]
        public IActionResult Map9(MySearch arr)
        {



            decimal swLat = arr.Ids.swLat - 1;
            decimal neLng = arr.Ids.neLng - 1;
            decimal neLat = arr.Ids.neLat - 1;
            decimal swLng = arr.Ids.swLng - 1;

            List<TbOffer> map1 = new List<TbOffer>();
            List<TbMap> map2 = new List<TbMap>();
            List<TbMap> map3 = new List<TbMap>();
            List<TbMap> map4 = new List<TbMap>();
            //map4 = ctx.TbMaps.ToList();
            map1 = ctx.TbOffers.Where(a => a.lat >= arr.Ids.swLat && a.lat <= arr.Ids.neLat && a.lng >= arr.Ids.swLng && a.lng <= arr.Ids.neLng).Where(a=> a.UnitId ==  Guid.Parse(arr.Id)).Where(a=> a.OfferStatus == "موافقة").Where(a => a.contract_type != "محجوز").ToList();
            //if (neLat < swLat)
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > neLat && a.lat < swLat).ToList();
            //}
            //else
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > swLat && a.lat < neLat).ToList();

            //}



            //if (neLng < swLng)
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > neLng && a.lat < swLng).ToList();
            //}
            //else
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > swLng && a.lat < neLat).ToList();

            //}
            //map3 = map1.Concat(map2).ToList();
            //foreach (var i in map3)
            //{

            //        map4.Add(i);

            //}
            var json = Json(map1);

            return json;
        }




        [HttpPost]
        public IActionResult Map10(MySearch arr)
        {



            decimal swLat = arr.Ids.swLat - 1;
            decimal neLng = arr.Ids.neLng - 1;
            decimal neLat = arr.Ids.neLat - 1;
            decimal swLng = arr.Ids.swLng - 1;

            List<TbRequest> map1 = new List<TbRequest>();
            List<TbMap> map2 = new List<TbMap>();
            List<TbMap> map3 = new List<TbMap>();
            List<TbMap> map4 = new List<TbMap>();
            //map4 = ctx.TbMaps.ToList();
            map1 = ctx.TbRequests.Where(a => a.lat >= arr.Ids.swLat && a.lat <= arr.Ids.neLat && a.lng >= arr.Ids.swLng && a.lng <= arr.Ids.neLng).Where(a => a.UnitId == Guid.Parse(arr.Id)).Where(a => a.Notes == "موافقة").ToList();
            //if (neLat < swLat)
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > neLat && a.lat < swLat).ToList();
            //}
            //else
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > swLat && a.lat < neLat).ToList();

            //}



            //if (neLng < swLng)
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > neLng && a.lat < swLng).ToList();
            //}
            //else
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > swLng && a.lat < neLat).ToList();

            //}
            //map3 = map1.Concat(map2).ToList();
            //foreach (var i in map3)
            //{

            //        map4.Add(i);

            //}
            var json = Json(map1);

            return json;
        }




        [HttpPost]
        public IActionResult Map80(MySearch arr)
        {



            decimal swLat = arr.Ids.swLat - 1;
            decimal neLng = arr.Ids.neLng - 1;
            decimal neLat = arr.Ids.neLat - 1;
            decimal swLng = arr.Ids.swLng - 1;

            List<TbRequest> map1 = new List<TbRequest>();
            //List<TbMap> map2 = new List<TbMap>();
            //List<TbMap> map3 = new List<TbMap>();
            //List<TbMap> map4 = new List<TbMap>();
            //map4 = ctx.TbMaps.ToList();
            map1 = ctx.TbRequests.Where(a => a.lat >= arr.Ids.swLat && a.lat <= arr.Ids.neLat && a.lng >= arr.Ids.swLng && a.lng <= arr.Ids.neLng).Where(a => a.UnitId == Guid.Parse(arr.Id)).Where(a => a.Notes == "موافقة").ToList();
            //if (neLat < swLat)
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > neLat && a.lat < swLat).ToList();
            //}
            //else
            //{
            //    map1 = ctx.TbMaps.Where(a => a.lat > swLat && a.lat < neLat).ToList();

            //}



            //if (neLng < swLng)
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > neLng && a.lat < swLng).ToList();
            //}
            //else
            //{
            //    map2 = ctx.TbMaps.Where(a => a.lat > swLng && a.lat < neLat).ToList();

            //}
            //map3 = map1.Concat(map2).ToList();
            //foreach (var i in map3)
            //{

            //        map4.Add(i);

            //}
            var json = Json(map1);

            return json;
        }
        public IActionResult Map5()
        {
            return View();
        }
        public IActionResult land()
        {
            return View();
        }
        public IActionResult OpenWindow()
        {
            return View();
        }
        public IActionResult ShowMarker()
        {
            return View();
        }
        public IActionResult ShowMarkerWithCustomIcons()
        {
            return View();
        }
        public IActionResult ShowMultipleMarker()
        {
            return View();
        }
        public IActionResult ShowMultipleMarkerWithCustomIcon()
        {
            List<TbMap> LST = ctx.TbMaps.ToList();
            foreach(var i in LST) 
            {

            }
            return View();
        }
        public IActionResult ShowMultipleMarkerWithMultipleInfo()
        {
            return View();
        }



        
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
