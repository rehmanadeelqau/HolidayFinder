using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.Gds.Converters
{
    public static class FileUtils
    {
        public static string ReadJsonFileAsString(string fileToRead)
        {
            var path = Path.Combine(AppContext.BaseDirectory, fileToRead);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to find the flighs data file.");
            }

            return File.ReadAllText(path);
        }
    }
}
