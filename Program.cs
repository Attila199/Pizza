using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Pizza
{
    class Program
    {
        static void Main(string[] args)
        {

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "pizza";

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT fnev, COUNT(rendeles.fazon) AS Ennyi_alakolommal_szállítottak_házhoz_egyes_futárok FROM futar, rendeles WHERE rendeles.fazon = futar.fazon GROUP BY fnev; ";//23.feladat
                command.CommandText = "SELECT pnev,SUM(db)from tetel, pizza WHERE tetel.pazon = pizza.pazon GROUP by pnevO;RDER BY SUM(db) DESC; ";//24.feladat
                command.CommandText = "SELECT vnev,SUM(db) from vevo, rendeles, tetel, pizza WHERE tetel.razon = rendeles.razon AND rendeles.vazon = vevo.vazon AND tetel.pazon = pizza.pazon GROUP by vnev ORDER BY SUM(par* db) DESC;"; //25.feladat
                command.CommandText = "SELECT par AS Ára,pnev AS Legdrágább_pizza FROM pizza ORDER BY par DESC LIMIT 1; ";//26.feladat
                command.CommandText = "SELECT fnev A_legtöbb_pizzát_szállította,SUM(db) AS Darab FROM futar, rendeles, tetel WHERE rendeles.razon = tetel.razon AND rendeles.fazon = futar.fazon GROUP BY fnev ORDER BY SUM(db) DESC limit 1; ";//27.feladat
                command.CommandText = "SELECT vnev AS A_legtöbb_pizzát_ette,SUM(db) AS db   from vevo, rendeles, tetel WHERE rendeles.razon = tetel.razon AND rendeles.vazon = vevo.vazon GROUP BY vnev ORDER BY SUM(DB) DESC LIMIT 1;";//28.feladat

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string fnev = dr.GetString("fnev");
                        int countrfazon = dr.GetInt32("Count(rendeles.fazon)");
                        Console.WriteLine($"23.feladat:Hány házhoz szállítása volt az egyes futároknak?\t{fnev},{ countrfazon}");
                        Console.WriteLine($"24.feladat:A fogyasztás alapján mi a pizzák népszerűségi sorrendje?");
                        Console.WriteLine($"25.feladat:A rendelések összértéke alapján mi a vevők sorrendje?");
                        Console.WriteLine($"26.feldat:Melyik a legdrágább pizza?");
                        Console.WriteLine($"27.feladat:Ki szállította házhoz a legtöbb pizzát?");
                        Console.WriteLine($"28.feladat:Ki ette a legtöbb pizzát?");
                    }
                }
                connection.Close();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);

            }
            Console.ReadKey();
        }
    }
}
