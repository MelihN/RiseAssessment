using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RaModels;
using RAUI.Models;
using System.Text.Json;

namespace RAUI.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppSettings AppConfig;

        public PersonController(Microsoft.Extensions.Options.IOptions<AppSettings> config)
        {
            AppConfig= config.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PersonDtoData(DtoModel gridRequest)
        {
            //var bll = new AccountTypeBll();
            //var reqMsg = new RequestMsg<DtoModel>(string.Empty, _DbSchemaName, ServiceUrlList.AccountSrvUrl, gridRequest, null, null, null);
            //var data = bll.GetListVM(reqMsg);
            var response = new DtoResponseModel<Person>();
            //response.all_data = null;
            response.data = new List<Person>()
            {
                new Person(){ UUID = "01", 
                    Name = "Name1", 
                    Surname="Surname1", 
                    CompanyName = "CompanyName1", 
                    ContactInfos = new List<ContactInfo>() { 
                     new ContactInfo(){ Location="Ankara", Email="name1@rise.com", PhoneNumber="903123212121"}
                    }},
                new Person(){ UUID = "02",
                    Name = "Name2",
                    Surname="Surname2",
                    CompanyName = "CompanyName3",
                    ContactInfos = new List<ContactInfo>() {
                     new ContactInfo(){ Location="Ankara", Email="name2@rise.com", PhoneNumber="903123222323"}
                    }},
                new Person(){ UUID = "03",
                    Name = "Name3",
                    Surname="Surname3",
                    CompanyName = "CompanyName3",
                    ContactInfos = new List<ContactInfo>() {
                     new ContactInfo(){ Location="İstanbul", Email="name3@rise.com", PhoneNumber="902163563636"}
                    }},
                new Person(){ UUID = "04",
                    Name = "Name4",
                    Surname="Surname4",
                    CompanyName = "CompanyName4",
                    ContactInfos = new List<ContactInfo>() {
                     new ContactInfo(){ Location="İzmir", Email="name4@rise.com", PhoneNumber="902328568989"}
                    }}
            };
            //response.total_rows = data.TotalCount;
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }

        //public IActionResult AccountTypeCreate(string Id)
        //{
        //    ViewData["StateList"] = GlobalState.StateList;
        //    SetSchemaName(HttpContext);
        //    if (string.IsNullOrWhiteSpace(Id))
        //        return View();

        //    var filter = new List<FilterParameter>();
        //    filter.Add(new FilterParameter() { Key = "Id", Value = Id, NextFilterOperator = "and", Operator = "Eq" });

        //    var accountTypeBll = new AccountTypeBll();
        //    var reqMsg = new RequestMsg<AccountType>(string.Empty, _DbSchemaName, ServiceUrlList.AccountSrvUrl, null, filter, null);
        //    var data = accountTypeBll.GetOne(reqMsg);

        //    if (data.RequestState)
        //        return View(data.ResponseObj);
        //    else
        //        return View();
        //}

        //[HttpPost]
        //public IActionResult AccountTypeCreate(AccountType model)
        //{
        //    SetSchemaName(HttpContext);
        //    var accountTypeBll = new AccountTypeBll();
        //    var requestObj = new RequestMsg<AccountType>(string.Empty, _DbSchemaName, ServiceUrlList.AccountSrvUrl, model, null, null, null);
        //    var response = new ResponseMsg<AccountType>();
        //    if (string.IsNullOrWhiteSpace(model.Id))
        //        response = accountTypeBll.Create(requestObj);
        //    else
        //    {
        //        var filter = new List<FilterParameter>();
        //        filter.Add(new FilterParameter() { Key = "Id", Value = model.Id, NextFilterOperator = "and", Operator = "Eq" });
        //        requestObj.FilterParameters = filter;
        //        response = accountTypeBll.Update(requestObj);
        //    }
        //    return Json(response, new JsonSerializerOptions { WriteIndented = true });
        //}

        //[HttpPost]
        //public IActionResult AccountTypeDelete(string Id)
        //{
        //    SetSchemaName(HttpContext);
        //    if (string.IsNullOrWhiteSpace(Id))
        //        return Json(new ResponseMsg<string>() { ErrorCode = "X0001", ErrorMessage = "Kayıt seçmelisiniz." }, new JsonSerializerOptions { WriteIndented = true });

        //    var accountTypeBll = new AccountTypeBll();
        //    var filter = new List<FilterParameter>();
        //    filter.Add(new FilterParameter() { Key = "Id", Value = Id, NextFilterOperator = "and", Operator = "Eq" });
        //    var requestObj = new RequestMsg<AccountType>(string.Empty, _DbSchemaName, ServiceUrlList.AccountSrvUrl, null, filter, null);

        //    var response = accountTypeBll.Delete(requestObj);
        //    return Json(response, new JsonSerializerOptions { WriteIndented = true });
        //}

    }
}
