using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AcePointer.NameSorter.DTO;


namespace AcePointer.NameSorter.Repo
{
    public class PersonRepo
    {
        private List<Person> _Persons = new();

        public string DataSource { get; }

        public PersonRepo(string dataSource)
        {
            DataSource = dataSource;
        }

        public List<Person> Load()
        {
            _Persons = loadPersons(DataSource);

            return _Persons;
        }

        public List<Person> GetAll() => _Persons;

        public Person Add(string fullname)
        {
            Person person = fullname.AsPerson();
             
            if(person is not null)
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
    }
}
