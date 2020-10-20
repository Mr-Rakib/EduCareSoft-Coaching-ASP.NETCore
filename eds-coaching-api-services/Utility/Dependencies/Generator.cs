using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Dependencies
{
    public static class Generator
    {
        private static readonly StudentRepository studentRepository = new StudentRepository();
        private static readonly FeesCollectionRepository feesCollectionRepository = new FeesCollectionRepository();

        public static string StudentID()
        {
            string studentID = "";
            int demo = (DateTime.Now.Year * 1000000) + 1;
            List<int> studentIDs = studentRepository.FindAll().Select(student => student.Id).ToList();
            demo = (studentIDs == null) ? demo : (demo + studentIDs.Count());
            studentID = string.Concat(demo, "s");
            return studentID;
        }

        public static string StaffID()
        {
            string studentID = "";
            int demo = (DateTime.Now.Year * 1000000) + 1;
            List<int> studentIDs = studentRepository.FindAll().Select(student => student.Id).ToList();
            demo = (studentIDs == null) ? demo : (demo + studentIDs.Count());
            studentID = string.Concat(demo, "s");
            return studentID;
        }

        public static string InvoiceNumber()
        {
            string Invoice = "INV";
            //int Number = (DateTime.Now.Year)*10000000;
            int Number = 1000000001;
            List<FeesCollection> feesCollections = feesCollectionRepository.FindAll().OrderBy(o => o.Id).ToList();

            Number += (feesCollections.Count == 0)? 0 : feesCollections.LastOrDefault().Id;
            Invoice = String.Concat(Invoice, Number).Remove(3,1);
            return Invoice; 
        }

        // Generate a random 8 digit password    
        public static string GeneratePassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(00, 9));
            builder.Append(RandomString(1, false));
            builder.Append(RandomString(2, true));
            builder.Append(RandomString(1, true));
            return builder.ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static int GetMonth(string month)
        {
            int monthValue = 0;
            switch (month.ToLower())
            {
                case "january":
                    monthValue = 1;
                    break;
                case "february":
                    monthValue = 2;
                    break;
                case "march":
                    monthValue = 3;
                    break;
                case "april":
                    monthValue = 4;
                    break;
                case "may":
                    monthValue = 5;
                    break;
                case "june":
                    monthValue = 6;
                    break;
                case "july":
                    monthValue = 7;
                    break;
                case "august":
                    monthValue = 8;
                    break;
                case "september":
                    monthValue = 9;
                    break;
                case "october":
                    monthValue = 10;
                    break;
                case "november":
                    monthValue = 11;
                    break;
                case "december":
                    monthValue = 12;
                    break;
            }
            return monthValue;
        }

    }
}
