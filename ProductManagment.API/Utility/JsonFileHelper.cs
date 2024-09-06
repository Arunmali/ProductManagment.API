
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProductManagment.API.Helpers;
using ProductManagment.API.Repository;

namespace ProductManagment.API.Utility
{
    public class JsonFileHelper<T>(IWebHostEnvironment fileContextPath) : IJsonFileHelper<T> where T : class
    {
        private readonly string _fileContextPath = Path.Combine(fileContextPath.ContentRootPath, "products.json");

        public List<T> ReadFromJsonFile<T>()
        {
            if (!File.Exists(_fileContextPath))
            {
                return [];
            }

            try
            {
                using var fileStream = new FileStream(_fileContextPath, FileMode.Open, FileAccess.Read);
                using var streamReader = new StreamReader(fileStream);
                using var jsonReader = new JsonTextReader(streamReader);
                var serializer = new Newtonsoft.Json.JsonSerializer();
                return serializer.Deserialize<List<T>>(jsonReader) ?? new List<T>();

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ApplicationException("An error occurred while reading the JSON file.", ex);
            }
            
        }

        public async Task WriteToJsonFile<T>(List<T> data)
        {
            //using StreamWriter file = File.CreateText(_fileContextPath);
            //JsonSerializer serializer = new();
            //serializer.Serialize(file, data);

            try
            {
                var directory = Path.GetDirectoryName(_fileContextPath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using var fileStream = new FileStream(_fileContextPath, FileMode.Create, FileAccess.Write);
                using var streamWriter = new StreamWriter(fileStream);
                using var jsonWriter = new JsonTextWriter(streamWriter);
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, data);
               await  jsonWriter.FlushAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ApplicationException("An error occurred while writing to the JSON file.", ex);
            }
        }
    }
}
