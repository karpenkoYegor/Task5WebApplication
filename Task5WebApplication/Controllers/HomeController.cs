using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Models;
using Task5WebApplication.Views.Home;
using Microsoft.AspNetCore.Hosting;

namespace Task5WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, 
            IRepositoryWrapper repository, 
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int cursor, int page = 1)
        {
            Extensions.CurrentPage++;
            Extensions.r = new Random(Extensions.PersonSeed + Extensions.CurrentPage + (int)Extensions.Errors);
            if (cursor == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Extensions.People.Add(AddPerson());
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Extensions.People.Add(AddPerson());
                }
            }
            cursor = cursor == 0 ? 11 : cursor;
            var model = new IndexViewModel()
            {
                Cursor = cursor,
                Countries = _repository.Country.GetAllCounties(),
                PeopleInformation = Extensions.People,
                
            };
            return Request.IsHtmx()
                ? PartialView("_Cards", model)
                : View(model);
        }
        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            Extensions.People = new List<List<StringBuilder>>();
            Extensions.PersonInformationModels = new List<PersonInformationModel>();
            Extensions.CountryId = model.CountryId;
            Extensions.Errors = Extensions.r.Next(0, 1) == 0 ? model.Errors : model.Errors + 1;
            Extensions.PersonSeed = model.UserSeed;
            Extensions.CurrentPage = 1;
            Extensions.r = new Random(Extensions.PersonSeed + Extensions.CurrentPage + (int)Extensions.Errors);
            for (int i = 0; i < 20; i++)
            {
                Extensions.People.Add(AddPerson());
            }
            model.Countries = _repository.Country.GetAllCounties();
            model.PeopleInformation = Extensions.People;
            return View(model);
        }

        public List<StringBuilder> AddErrors(List<StringBuilder> personRow, double errors)
        {
            
            for (int j = 0; j < errors; j++) 
            {
                var typeError = Extensions.r.Next(1, 20);
                var randomPerson = personRow.PickRandom();
                if (typeError == 1 && randomPerson.Length < 5)
                {
                    typeError = 2;
                }
                switch (typeError)
                { 
                    case 1:
                        randomPerson.DeleteSymbol();
                        break;
                    case 2:
                        randomPerson.InsertSymbol(Extensions.CountryId);
                        break;
                    default:
                        randomPerson.SwapSymbol();
                        break;
                }
            }
            return personRow;
        }
        public List<StringBuilder> AddPerson()
        {
            List<StringBuilder> person = new List<StringBuilder>();
            person.Add(AddPassport());
            person.Add(AddPersonName());
            person.Add(AddCity());
            person.Add(AddStreet());
            person.Add(AddPhoneNumber());
            if (Extensions.Errors > 0)
                person = AddErrors(person, Extensions.Errors);
            Extensions.PersonInformationModels.Add(new PersonInformationModel()
            {
                Passport = person[0].ToString(),
                Name = person[1].ToString(),
                City = person[2].ToString(),
                Street = person[3].ToString(),
                Phone = person[4].ToString()
            });
            return person;
        }

        private StringBuilder AddPhoneNumber()
        {
            StringBuilder phoneNumber = new StringBuilder(" "+_repository.Country.FindById(c => c.Id == Extensions.CountryId).CodePhoneNumber);
            switch (Extensions.CountryId)
            {
                case 1:
                    phoneNumber.Append($"{Extensions.r.Next(100000000, 999999999)}");
                    break;
                case 2:
                    phoneNumber.Append($"{Extensions.r.NextInt64(9100000000, 9199999999)}");
                    break;
                case 3:
                    phoneNumber.Append($"33{Extensions.r.Next(1000000, 9999999)}");
                    break;
            }
            return phoneNumber;
        }
        private StringBuilder AddPersonName()
        {
            StringBuilder name = new StringBuilder();
            bool isMale = Extensions.r.Next(0, 2) == 1;
            var surNameUser = _repository.Surname.GetRandomSurname(Extensions.CountryId, isMale).SurnamePerson;
            var nameUser = _repository.Name.GetRandomName(Extensions.CountryId, isMale).NamePerson;
            
            name.Append($"{surNameUser} " +
                        $"{nameUser} ");
            if (Extensions.CountryId != 2)
            {
                var middleNameUser = _repository.MiddleName.GetRandomMiddleName(Extensions.CountryId, isMale)
                    .MiddleNamePerson;
                name.Append($"{middleNameUser} ");
            }
            return name;
        }

        private StringBuilder AddCity()
        {
            StringBuilder city = new StringBuilder();
            var randomRegion = _repository.Region.GetRandomRegion(Extensions.CountryId);
            var randomCity = randomRegion.Cities.PickRandom();
            city.Append($"{randomRegion.RegionName} " +
                        $"{randomCity.TypeCity.Name} " +
                        $"{randomCity.CityName} " +
                        $"{randomCity.IndexCity} ");
            return city;
        }

        private StringBuilder AddStreet()
        {
            StringBuilder adress = new StringBuilder();
            Street street = _repository.Street.GetRandomStreet(Extensions.CountryId);
            var house = Extensions.CountryId == 2 ? " " : "д. ";
            var appartment = Extensions.CountryId == 2 ? "-" : "кв.";
            var houseNum = Extensions.r.Next(1, 100);
            adress.Append($"{street.StreetName} " +
                          $"{house} " +
                          $"{houseNum} ");
            if (Extensions.r.Next(0, 2) < 1)
            {
                var appartmentNum = Extensions.r.Next(1, 100);
                adress.Append($"{appartment} " +
                              $"{appartmentNum}");
            }
            return adress;
        }

        private StringBuilder AddPassport()
        {
            List<string> seriesPassportRB = new List<string>()
            {
                "АВ", "ВМ", "НВ", "КН", "МР", "МС", "КВ"
            };
            StringBuilder idPassport = new StringBuilder();
            switch (Extensions.CountryId)
            {
                case 1:
                    idPassport.Append($"{Extensions.r.Next(10, 99)}№" +
                                      $"{Extensions.r.Next(1000000, 9999999)}");
                    break;
                case 2:
                    idPassport.Append($"{Extensions.r.Next(100000000, 999999999)}");
                    break;
                case 3:
                    idPassport.Append($"{seriesPassportRB.PickRandom()}" +
                                      $"{Extensions.r.Next(1000000, 9999999)}");
                    break;
            };
            
            return idPassport;
        }

        public void SaveData()
        {
            using (var writer = new StreamWriter($"{_webHostEnvironment.WebRootPath}/files/personsData.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(Extensions.PersonInformationModels);
                }
            }
            DownloadFile();
        }

        public async void DownloadFile()
        {
            Response.Headers.ContentDisposition = $"attachment; filename=personsData.csv";
            await Response.SendFileAsync($"{_webHostEnvironment.WebRootPath}/files/personsData.csv");
        }
    }
}