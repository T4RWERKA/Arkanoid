using Classes;
using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassesForms
{
    internal static class TxtSerializator
    {
        private static readonly string beginObject = "{\n";
        private static readonly string endObject = "},\n";
        private static readonly char[] trimChars = { ' ', '\n', '\t', ',', '.', ';', ':', '!', '?', '-' };

        public static List<DisplayObject> DeserializeTxt(string fileName)
        {
            var result = new List<DisplayObject>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string txt = sr.ReadToEnd();
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == '{')
                    {
                        int close = TxtSerializator.CloseBracketInd(txt, i, "{");
                        string txtObj = txt.Substring(i + 1, close - i - 2).Trim(trimChars);
                        i = close;
                        DisplayObject obj = DeserializeTxtObject(txtObj) as DisplayObject;
                        obj.shape.Position = new Vector2f(obj.left, obj.top);
                        if (obj is Tile)
                            ((RectangleShape)obj.shape).Size = new Vector2f(obj.right - obj.left, obj.bottom - obj.top);
                        result.Add(obj);
                        i += 2;
                    }
                }
            }
            return result;
        }
        public static async Task SerializeTxt(string fileName, List<DisplayObject> displayObjects)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var obj in displayObjects)
                    await SerializeTxtObject(sw, obj);
            }
        }
        public static async Task SerializeTxtObject(StreamWriter sw, object? obj)
        {
            Type type = obj.GetType();
            if (type.IsClass || type.IsValueType && !type.IsPrimitive)
            {
                await sw.WriteAsync(beginObject);
                await sw.WriteAsync($"type: {type}, \n");
                if (obj is IEnumerable)
                {
                    foreach (var item in (IEnumerable<object>)obj)
                        await SerializeTxtObject(sw, item);
                }
                else
                {
                    foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        bool ignore = false;
                        object[] attributes = field.GetCustomAttributes(true);
                        foreach (Attribute attribute in attributes)
                            if (attribute is JsonIgnoreAttribute)
                                ignore = true;
                        if (!ignore)
                        {
                            await sw.WriteAsync($"{field.Name}: ");
                            await SerializeTxtObject(sw, field.GetValue(obj));
                        }
                    }
                    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        bool ignore = false;
                        object[] attributes = property.GetCustomAttributes(true);
                        foreach (Attribute attribute in attributes)
                            if (attribute is JsonIgnoreAttribute)
                                ignore = true;
                        if (!ignore)
                        {
                            await sw.WriteAsync($"{property.Name}: ");
                            await SerializeTxtObject(sw, property.GetValue(obj));
                        }
                    }
                }
                await sw.WriteAsync(endObject);
            }
            else if (type.IsValueType && type.IsPrimitive)
            {
                await sw.WriteAsync($"{obj},\n");
            }
        }
        public static object? DeserializeTxtObject(string txt)
        {
            int i = 0, j;
            object? resultObject = null;
            Type? objType = null;
            while (i < txt.Length)
            {
                j = i;
                while (j + 1 < txt.Length && txt[++j] != ':') ;
                if (j + 1 < txt.Length)
                {
                    string field = txt.Substring(i, j - i).Trim(trimChars);
                    i = j + 1;
                    if (txt[i + 1] == '{')
                    {
                        int close = CloseBracketInd(txt, i + 1, "{");
                        object? value = DeserializeTxtObject(txt.Substring(i + 3, close - i - 2));
                        FieldInfo fieldInfo = objType.GetField(field);
                        if (fieldInfo == null)
                        {
                            PropertyInfo propertyInfo = objType.GetProperty(field);
                            propertyInfo.SetValue(resultObject, value);
                        }
                        else
                        {
                            fieldInfo.SetValue(resultObject, value);
                        }
                        i = close + 1;
                    }
                    else
                    {
                        while (j + 1 < txt.Length && txt[++j] != ',') ;
                        if (j + 1 < txt.Length)
                        {
                            string value = txt.Substring(i, j - i).Trim(trimChars);
                            i = j + 1;
                            if (field.Equals("type"))
                            {
                                if ((objType = Type.GetType(value)) == null)
                                {
                                    if (value.StartsWith("SFML.System"))
                                    {
                                        value += ", SFML.System";
                                        objType = Type.GetType(value);
                                    }
                                }
                                resultObject = Activator.CreateInstance(objType);
                            }
                            else
                            {
                                FieldInfo fieldInfo = objType.GetField(field);
                                fieldInfo.SetValue(resultObject, Convert.ChangeType(value, fieldInfo.FieldType));
                            }
                        }
                        else
                        {
                            i = j + 1;
                        }
                    }
                }
                else
                {
                    i = j + 1;
                }
            }
            return resultObject;
        }
        public static int CloseBracketInd(string str, int begin, string open)
        {
            string close = "";
            switch (open)
            {
                case "{":
                    close = "}";
                    break;
                case "[":
                    close = "}";
                    break;
            }
            int nestLvl = 1;
            int i = begin;
            while (nestLvl != 0)
            {
                i++;
                if (str.Length - i >= open.Length && str.Substring(i, open.Length).Equals(open))
                    nestLvl++;
                else if (str.Length - i >= close.Length && str.Substring(i, close.Length).Equals(close))
                    nestLvl--;
                else if (str.Length <= i)
                {
                    return i - 1;
                }
            }
            return i;
        }
    }
}
