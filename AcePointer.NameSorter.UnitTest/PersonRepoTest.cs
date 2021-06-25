using AcePointer.NameSorter.DTO;
using AcePointer.NameSorter.Repo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AcePointer.NameSorter.UnitTest
{
    public class PersonRepoTest
    {
        const string DataSource = "./Data/unsorted-names-list.txt";

        List<Person> Persons = new();
        PersonRepo PersonRepo = new(DataSource);

        [SetUp]
        public void Setup()
        {
            Persons = PersonRepo.Load();
        }

        [Test]
        public void LoadTest()
        {
            if (Persons.Count == 0)
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }


        [Test]
        public void SortByLastNameTest()
        {
            //1. Arrange 
            var lastNameToValidate = "Alvarez";

            //2. Act
            var person = Persons.OrderBy(x => x.LastName).FirstOrDefault();
            var isLastNameFound = person?
                .LastName.Equals(lastNameToValidate, StringComparison.InvariantCultureIgnoreCase) ?? false;

            //3. Assert
            if (isLastNameFound == false)
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }


        [Test]
        public void LookUpLastNameTest()
        {
            //1. Arrange 
            var lastNameToValidate = "Archer";

            //2. Act
            var isLastNameFound = Persons
                .Any(person =>
                person.LastName.Equals(lastNameToValidate, StringComparison.InvariantCultureIgnoreCase)
            );

            //3. Assert
            if (isLastNameFound == false)
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }
    }
}