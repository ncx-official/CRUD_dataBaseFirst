using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataBaseManagerApplication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
            // CRUD
            // Create
            //    using (BuildingMaterialsStoreContext db = new BuildingMaterialsStoreContext())
            // {
            //     PersonSex sexType1 = new PersonSex { IdSex = 1, SexValue = "Male" };
            //     PersonSex sexType2 = new PersonSex { IdSex = 2, SexValue = "Female" };
            //     PersonSex sexType3 = new PersonSex { IdSex = 3, SexValue = "NonBinary" };
            //
            //     db.PersonSexes.Add(sexType1);
            //     db.PersonSexes.Add(sexType2);
            //     db.PersonSexes.Add(sexType3);
            //     db.PersonSexes.AddAsync();
            //
            //     db.SaveChanges();
            //     db.SaveChangesAsync();
            // }

            // Read
            // string[] data = new string[] { };
            // using (BuildingMaterialsStoreContext db = new BuildingMaterialsStoreContext())
            // {
            //     var sexTypes = db.PersonSexes.ToList();
            //              ToListAsync();
            //
            //     Console.WriteLine("______\nDATA: ");
            //     foreach (var obj in sexTypes)
            //     {
            //         Console.WriteLine($"{obj.IdSex}, {obj.SexValue}");
            //     }
            // }

            // Update
            //    using (BuildingMaterialsStoreContext db = new BuildingMaterialsStoreContext())
            // {
            //
            //     PersonSex sexType = db.PersonSexes.Find(2U); // search by primary key
            //     if (sexType != null)
            //     {
            //         //sexType.IdSex = 1;
            //         sexType.SexValue = "Female";
            //         db.SaveChanges();
            //     }
            //
            // }  

            // Delete
            //   using (BuildingMaterialsStoreContext db = new BuildingMaterialsStoreContext())
            // {
            //     PersonSex sexType = db.PersonSexes.Find(3U); // search by primary key
            //     if (sexType != null)
            //     {
            //         db.PersonSexes.Remove(sexType);
            //         db.SaveChanges();
            //     }
            // }
            // 
            // dotnet ef migrations add InitialMigration
            // (create migration)

            // dotnet ef database update
            // (update database)

        }
    }
}