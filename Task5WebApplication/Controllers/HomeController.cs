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
using System;
using System.Numerics;

namespace Task5WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private int _countryId = 1;
        public HomeController(ILogger<HomeController> logger, 
            IRepositoryWrapper repository, 
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _countryId = Extensions.CountryId;
        }

        public IActionResult Index(int cursor, int page = 1)
        {
            Extensions.CurrentPage = page;
            Extensions.r = new Random(Extensions.CurrentPage + Extensions.PersonSeed);
            Extensions.randomForUsers = new Random(Extensions.CurrentPage + Extensions.PersonSeed);
            var count = cursor == 0 ? 20 : 10;
            cursor = cursor == 0 ? 11 : cursor;
            for (int i = 0; i < count; i++)
            {
                AddPerson();
            }
            var model = new IndexViewModel()
            {
                Cursor = cursor,
                Countries = _repository.Country.GetAllCounties(),
                PeopleInformation = Extensions.Persons,
            };
            return Request.IsHtmx()
                ? PartialView("_Cards", model)
                : View(model);
        }
        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            
            Extensions.CountryId = model.CountryId;
            _countryId = Extensions.CountryId;
            Extensions.Errors = Convert.ToDouble(model.Errors, new CultureInfo("en-US"));
            Extensions.Persons = new List<PersonInformationModel>();
            Extensions.PersonSeed = model.UserSeed;
            Extensions.CurrentPage = 1;
            Extensions.r = new Random(Extensions.CurrentPage + Extensions.PersonSeed);
            Extensions.randomForUsers = new Random(Extensions.CurrentPage + Extensions.PersonSeed);
            
            for (int i = 0; i < 20; i++)
            {
                AddPerson();
            }
            model.Countries = _repository.Country.GetAllCounties();
            model.PeopleInformation = Extensions.Persons;
            return View(model);
        }

        public List<StringBuilder> AddErrors(List<StringBuilder> personRow, double errors)
        {
            var error = Extensions.r.Next(0, 2) == 0 && errors % 1 == 0.5 ? (int)errors : (int)errors + 1;
                for (int j = 0; j < error; j++)
                {
                    int minSymbols = 5;
                    var typeError = Extensions.r.Next(1, 20);
                    var randomPerson = personRow[Extensions.r.Next(personRow.Count)];
                    if (typeError == 1 && randomPerson.Length < minSymbols)
                    {
                        typeError = 2;
                    }
                    switch (typeError)
                    {
                        case 1:
                            randomPerson.DeleteSymbol();
                            break;
                        case 2:
                            randomPerson.InsertSymbol(_countryId);
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
            var personInformation = new PersonInformationModel()
            {
                Passport = person[0].ToString(),
                Name = person[1].ToString(),
                City = person[2].ToString(),
                Street = person[3].ToString(),
                Phone = person[4].ToString()
            };
            Extensions.Persons.Add(personInformation);
            return person;
        }

        private StringBuilder AddPhoneNumber()
        {
            StringBuilder phoneNumber = new StringBuilder(" "+_repository.Country.FindById(c => c.Id == _countryId).CodePhoneNumber);
            switch (_countryId)
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
            var surNameUser = _repository.Surname.GetRandomSurname(_countryId, isMale).SurnamePerson;
            var nameUser = _repository.Name.GetRandomName(_countryId, isMale).NamePerson;
            
            name.Append($"{surNameUser} " +
                        $"{nameUser} ");
            if (_countryId != 2)
            {
                var middleNameUser = _repository.MiddleName.GetRandomMiddleName(_countryId, isMale)
                    .MiddleNamePerson;
                name.Append($"{middleNameUser} ");
            }
            return name;
        }

        private StringBuilder AddCity()
        {
            StringBuilder city = new StringBuilder();
            var randomRegion = _repository.Region.GetRandomRegion(_countryId);
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
            Street street = _repository.Street.GetRandomStreet(_countryId);
            var house = _countryId == 2 ? " " : "д. ";
            var appartment = _countryId == 2 ? "-" : "кв.";
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
            switch (_countryId)
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
            var prefixName = Guid.NewGuid().ToString();
            using (var writer = new StreamWriter($"{_webHostEnvironment.WebRootPath}/files/{prefixName}personsData.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(Extensions.Persons);
                }
            }
            DownloadFile(prefixName);
        }

        public void DownloadFile(string prefix)
        {
            Response.Headers.ContentDisposition = $"attachment; filename={prefix}personsData.csv";
            Response.SendFileAsync($"{_webHostEnvironment.WebRootPath}/files/{prefix}personsData.csv").Wait();
        }
    }
}