using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using UContact.Web.HttpClients;
using UContact.Web.Models;
using UContact.Web.Models.Persons;

namespace UContact.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonHttpClient _personHttpClient;

        public PersonController(PersonHttpClient personHttpClient)
        {
            _personHttpClient = personHttpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List([FromQuery] int take = 10, [FromQuery] int skip = 0)
        {
            var persons = await _personHttpClient.GetPersons(take, skip);
            return View(persons);
        }
        public async Task<IActionResult> InsertOrUpdate([FromQuery] Guid? Id)
        {
            var model = new PersonViewModel();

            if (Id.HasValue)
                model = await _personHttpClient.GetPersonById(Id.Value);
            else
                model = new PersonViewModel();

            PrepareCommonModel(model);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdate(PersonViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var insert = await _personHttpClient.InsertPerson(model);
                return Ok("INSERTED");
            }

            else
            {
                var update = await _personHttpClient.UpdatePerson(model);
                return Ok("UPDATED");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return Ok("Id is not be empty for delete.");
            }

            else
            {
                await _personHttpClient.DeletePerson(Id.Value);
                return Content("DELETED");
            };
        }
        private void PrepareCommonModel(PersonViewModel model)
        {
            model.InfoTypes.Clear();
            model.InfoTypes.Add(new SelectListItem("Choose Type", string.Empty));
            model.InfoTypes.Add(new SelectListItem("PhoneNumber", "PhoneNumber"));
            model.InfoTypes.Add(new SelectListItem("EmailAddress", "EmailAddress"));
            model.InfoTypes.Add(new SelectListItem("Location", "Location"));

            model.InfoTypes.Clear();
            model.InfoTypes.Add(new SelectListItem("Choose Location", string.Empty));
            model.InfoTypes.Add(new SelectListItem("Izmir", "Izmir"));
            model.InfoTypes.Add(new SelectListItem("Ankara", "Ankara"));
            model.InfoTypes.Add(new SelectListItem("Istanbul", "Istanbul"));
            model.InfoTypes.Add(new SelectListItem("Eskisehir", "Eskisehir"));
            model.InfoTypes.Add(new SelectListItem("Bursa", "Bursa"));
        }

    }
}
