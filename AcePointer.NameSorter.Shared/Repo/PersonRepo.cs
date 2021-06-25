using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AcePointer.NameSorter.DTO;


namespace AcePointer.NameSorter.Repo
{
    public interface IPersonRepo
    {
        public void Load(string dataSource);
        public void Load(MemoryStream stream);
        List<Person> GetPersons();
        List<Person> GetSortedPersons();
        Person Add(string fullname);
    }

    public class PersonRepo : IPersonRepo
    {
        public List<Person> GetPersons() => _Persons;
        public List<Person> GetSortedPersons() => _Persons.OrderBy(x => x.LastName).ToList();

        public PersonRepo() { }

        public void Load(string dataSource)
        {
            _Persons = loadPersons(dataSource);
        }

        public void Load(MemoryStream stream)
        {
            _Persons = loadPersons(stream);
        }

        private List<Person> _Persons = new();

        public Person Add(string fullname)
        {
            Person person = fullname.AsPerson();

            if (person is not null)
            {
                _Persons.Add(person);
            }

            return person;
        }

        /// <summary>
        /// To read a list of names fro text file and serialize it into List<Person>
        /// </summary>
        /// <returns></returns>
        private List<Person> loadPersons(string filePath)
        {
            //1. Arrange
            List<Person> persons = new();

            //2. Act
            File.ReadAllLines(filePath).ToList()
                .ForEach(fullname =>
                {
                    //Utilizing string extension method
                    Person person = fullname.AsPerson();
                    persons.Add(person);
                });

            return persons;
        }

        private List<Person> loadPersons(MemoryStream stream)
        {
            //1. Arrange
            List<Person> persons = new();
            var context = Encoding.ASCII.GetString(stream.ToArray())
                .Replace("?", string.Empty)
                .Replace("\r",string.Empty);

            //2. Act
            context.Split("\n").ToList().ForEach(line =>
            {
                var person = line.AsPerson();

                if (person is not null)
                {
                    persons.Add(person);
                }
            });

            return persons;
        }
    }
}
