using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAIFMVC.Models;
using EAIFMVC.EAIFAPI;
using Newtonsoft.Json;

namespace EAIFMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AlertList()
        {
            string error = "";
            var alertList = EAIFAPI.EAIFAPI.LoadAlertList(out error);

            foreach (var alert in alertList)
            {
                var company = EAIFAPI.EAIFAPI.LoadCompanyWrapperById(alert.CompanyID, out error);
                if (company != null)
                {
                    alert.CompanyName = company.Name;
                    alert.Phone = company.Phone;
                }
            }

            return View(alertList);
        }

        public ActionResult Report2()
        {
            return View();
        }

        public ActionResult AlertHistory(string start = null, string end = null)
        {
            DateTime? startDate = null;
            if (!string.IsNullOrEmpty(start))
            {
                startDate = DateTime.ParseExact(start, "MM/dd/yyyy", null);
            }

            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(end))
            {
                endDate = DateTime.ParseExact(end, "MM/dd/yyyy", null);
            }

            string error = "";
            var alertList = EAIFAPI.EAIFAPI.SearchAlerts(startDate, endDate, out error);
            foreach (var alert in alertList)
            {
                var company = EAIFAPI.EAIFAPI.LoadCompanyWrapperById(alert.CompanyID, out error);
                if (company != null)
                {
                    alert.CompanyName = company.Name;
                    alert.Phone = company.Phone;
                }
            }

            ViewBag.startTime = start;
            ViewBag.endTime = end;
            return View(alertList);
        }

        public ActionResult Users()
        {
            string error = "";
            var contactorList = EAIFAPI.EAIFAPI.LoadContactorList(out error);

            return View(contactorList);
        }

        public ActionResult Notifications()
        {
            string error = "";
            List<NotificationWrapper> list = EAIFAPI.EAIFAPI.LoadNotificationList(out error);
            string listJson = JsonConvert.SerializeObject(list);
            ViewBag.listJson = listJson;
            return View();
        }

        public ActionResult Message(Guid alertId)
        {
            string error = "";
            AlertWrapper alert = EAIFAPI.EAIFAPI.LoadAlertWrapper(alertId, out error);
            CompanyWrapper company = null;
            if (alert != null)
            {
                company = EAIFAPI.EAIFAPI.LoadCompanyWrapperById(alert.CompanyID, out error);

            }
            ViewBag.errorMessage = error;
            if (company != null)
                ViewBag.phone = company.Phone;

            return View();
        }

        public ActionResult Detail(Guid alertId)
        {
            string error = "";
            AlertWrapper alert = EAIFAPI.EAIFAPI.LoadAlertWrapper(alertId, out error);
            CompanyWrapper company = null;
            if (alert != null)
            {
                company = EAIFAPI.EAIFAPI.LoadCompanyWrapperById(alert.CompanyID, out error);

            }

            ViewBag.errorMessage = error;

            if (alert != null && company != null)
            {
                string alertJson = JsonConvert.SerializeObject(alert);
                string companyJson = JsonConvert.SerializeObject(company);

                ViewBag.AlertJson = alertJson;
                ViewBag.CompanyJson = companyJson;
            }

            return View();
        }

        public ActionResult Reports()
        {
            return View(1);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}