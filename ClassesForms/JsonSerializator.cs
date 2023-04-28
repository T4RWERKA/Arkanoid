using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using SFML.System;

namespace ClassesForms
{
    internal static class JsonSerializator
    {
        public static async Task SerializeJson(string fileName, List<DisplayObject>? displayObjects)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var serializeOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };
                serializeOptions.Converters.Add(new DisplayObjectConverterWithTypeDiscriminator());

                await JsonSerializer.SerializeAsync<List<DisplayObject>>(fs, displayObjects, serializeOptions);
            }
        }
        public static async Task<List<DisplayObject>?> DeserializeJson(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var serializeOptions = new JsonSerializerOptions();
                serializeOptions.Converters.Add(new DisplayObjectConverterWithTypeDiscriminator());

                return await JsonSerializer.DeserializeAsync<List<DisplayObject>>(fs, serializeOptions);
            }
        }
    }
}
