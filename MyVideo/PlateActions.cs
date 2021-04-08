using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyVideo
{
    class PlateActions
    {
        public static string getPlateNumber(string file)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string regions = "vn";
                string token = ConfigurationManager.AppSettings["apiToken"];
                Console.WriteLine("Uploading Plate(s)...");
                string postUrl = "https://api.platerecognizer.com/v1/plate-reader/";

                try
                {
                    PlateReaderResult plateReaderResult = PlateReader.Read(postUrl, file, null, regions, token);
                    Console.WriteLine("-------------------");
                    Result result = plateReaderResult.results[0];
                    return result.plate.ToString();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("Error occured when uploading [{0}].", file));
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Small";
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.Red;
                return "Big";
            }
            Console.ResetColor();
        }
    }
}
