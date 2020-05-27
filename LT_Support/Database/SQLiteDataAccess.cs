using System.Collections.Generic;
using System.Configuration;

namespace LT_Support
{
    public class SQLiteDataAccess
    {
        public static List<PersonModel> LoadPeople()
        {

        }
        public static void SavePerson(PersonModel person)
        {

        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
