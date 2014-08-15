using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Services.Concrete;
using System.Linq;

namespace WeeWorld.ADS.Controllers.Web
{
    public class AppController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}