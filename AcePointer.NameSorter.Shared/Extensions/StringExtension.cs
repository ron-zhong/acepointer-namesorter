using AcePointer.NameSorter.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AcePointer.NameSorter
{
    public static class Extension
    {
        public static Person AsPerson(this string fullname)
        {
            if(string.IsNullOrEmpty(fullname))
            {
                return null;
            }

            // fullname = "Adonis Julius Archer"
            // lastName = "Archer"
            // firstName = "Adonis Julius"

            var names = fullname.Trim().Split(" ");
            var lastName = names.Last();
            var firstName = fullname.Replace(lastName, string.Empty).Trim();

            return new Person(firstName, lastName);
        }
    }
}