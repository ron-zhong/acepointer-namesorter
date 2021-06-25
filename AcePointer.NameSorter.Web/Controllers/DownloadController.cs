using AcePointer.NameSorter.DTO;
using AcePointer.NameSorter.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcePointer.NameSorter.Web.Controllers
{
    [ApiController]
    public class DownloadController : ControllerBase
    {
        public DownloadController(IPersonRepo repo) => Repo = repo;
        public IPersonRepo Repo { get; }

        [HttpGet]
        [Route("download/sorted-names-list.txt")]
        public IActionResult Get()
        {
            var persons = Repo.GetSortedPersons();
            var bytes = convertPersonstoBytes(persons);

            return File(bytes, "application/text");
        }

        private static byte[] convertPersonstoBytes(List<Person> persons)
        {
            var text = string.Empty;
            persons.ForEach(person =>
            {
                text += $"{person.FullName}\n";
            });
            var bytes = Encoding.ASCII.GetBytes(text);
            return bytes;
        }
    }
}
