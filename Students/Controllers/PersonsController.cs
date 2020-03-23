using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;
using Domain.Enum;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Services;

namespace Students.Controllers
{
    public class PersonsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Add(PersonRequest request)
        {
            try
            {
                switch (request.PersonType)
                {
                    case PersonType.Student:
                        var student = new Student
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            PhoneNumbers = request.PhoneNumbers,
                            Faculties = request.Faculties,
                            Group = request.Group
                        };

                        PersonsService<Student>.Instance.AddNewPerson(student);
                        return new HttpResponseMessage() { Content = new StringContent("Success") };

                    case PersonType.Entrant:
                        var entrant = new Entrant
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            PhoneNumbers = request.PhoneNumbers,
                            Faculties = request.Faculties
                        };
                        PersonsService<Entrant>.Instance.AddNewPerson(entrant);
                        return new HttpResponseMessage() { Content = new StringContent("Success") };

                    case PersonType.Employee:
                        var employee = new Employee
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            PhoneNumbers = request.PhoneNumbers,
                            Сathedra = request.Сathedra
                        };

                        PersonsService<Employee>.Instance.AddNewPerson(employee);
                        return new HttpResponseMessage() { Content = new StringContent("Success") };

                    case PersonType.Guest:
                        var guest = new Guest
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            PhoneNumbers = request.PhoneNumbers,
                            Reason = request.Reason
                        };

                        PersonsService<Guest>.Instance.AddNewPerson(guest);
                        return new HttpResponseMessage { Content = new StringContent("Success") };

                    default:

                        return new HttpResponseMessage() { Content = new StringContent("such person type is not supported") };
                }
                
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(){ Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost]
        public HttpResponseMessage Remove(RemoveRequest request)
        {
            bool result;
            try
            {
                if (request.Id > 0)
                {
                    result = PersonsService<BasePerson>.Instance.RemovePerson(request.Id);
                }
                else if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    result = PersonsService<BasePerson>.Instance.RemovePerson(request.PhoneNumber);
                }
                else
                {
                    return new HttpResponseMessage() { Content = new StringContent("Invalid request") };
                }

                return new HttpResponseMessage() { Content = new StringContent(result.ToString()) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetAll(GetPersonRequest request)
        {
            List<BasePerson> result;
            try
            {
                if (request?.PersonType > 0)
                {
                    switch (request.PersonType)
                    {
                        case PersonType.Student:
                            
                            var students = PersonsService<Student>.Instance.GetPersons(PersonType.Student);
                            var studentsJson = JsonConvert.SerializeObject(students);
                            return new HttpResponseMessage() { Content = new StringContent(studentsJson) };

                        case PersonType.Entrant:

                            var entrants = PersonsService<Entrant>.Instance.GetPersons(PersonType.Entrant);
                            var entrantsJson = JsonConvert.SerializeObject(entrants);
                            return new HttpResponseMessage { Content = new StringContent(entrantsJson) };

                        case PersonType.Employee:

                            var employee = PersonsService<Employee>.Instance.GetPersons(PersonType.Employee);
                            var employeeJson = JsonConvert.SerializeObject(employee);
                            return new HttpResponseMessage { Content = new StringContent(employeeJson) };

                        case PersonType.Guest:

                            var guest = PersonsService<Guest>.Instance.GetPersons(PersonType.Guest);
                            var guestJson = JsonConvert.SerializeObject(guest);
                            return new HttpResponseMessage { Content = new StringContent(guestJson) };

                        default:

                            return new HttpResponseMessage() { Content = new StringContent("such person type is not supported") };
                    }
                }
                else
                {
                    result = PersonsService<BasePerson>.Instance.GetPersons();
                    var resultJson = JsonConvert.SerializeObject(result);
                    return new HttpResponseMessage() { Content = new StringContent(resultJson) };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage Get(GetPersonRequest request)
        {
            try
            {
                dynamic search = request.Id > 0 ? (dynamic) request.Id : request.PhoneNumber;
                switch (request.PersonType)
                {
                    case PersonType.Student:

                        var student = PersonsService<Student>.Instance.GetPerson(search);
                        var studentJson = JsonConvert.SerializeObject(student);
                        return new HttpResponseMessage() { Content = new StringContent(studentJson) };

                    case PersonType.Entrant:

                        var entrants = PersonsService<Entrant>.Instance.GetPerson(search);
                        var entrantsJson = JsonConvert.SerializeObject(entrants);
                        return new HttpResponseMessage { Content = new StringContent(entrantsJson) };

                    case PersonType.Employee:

                        var employee = PersonsService<Employee>.Instance.GetPerson(search);
                        var employeeJson = JsonConvert.SerializeObject(employee);
                        return new HttpResponseMessage { Content = new StringContent(employeeJson) };

                    case PersonType.Guest:

                        var guest = PersonsService<Guest>.Instance.GetPerson(search);
                        var guestJson = JsonConvert.SerializeObject(guest);
                        return new HttpResponseMessage { Content = new StringContent(guestJson) };

                    default:

                        return new HttpResponseMessage { Content = new StringContent("such person type is not supported") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new StringContent(ex.Message) };
            }
        }
    }
}
