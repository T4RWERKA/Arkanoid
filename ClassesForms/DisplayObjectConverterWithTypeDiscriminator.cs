using Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassesForms
{
    internal class DisplayObjectConverterWithTypeDiscriminator: JsonConverter<DisplayObject>
    {
        enum TypeDiscriminator
        {
            Ball = 1,
            PlayerTile = 2,
            FieldTile = 3
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(DisplayObject).IsAssignableFrom(typeToConvert);

        public override DisplayObject Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            DisplayObject obj = typeDiscriminator switch
            {
                TypeDiscriminator.Ball => new Ball(),
                TypeDiscriminator.PlayerTile => new PlayerTile(),
                TypeDiscriminator.FieldTile => new FieldTile(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    obj.shape.Position = new Vector2f((float)obj.left, (float)obj.top);
                    if (obj is Tile)
                        ((RectangleShape)(obj.shape)).Size = new Vector2f((float)(obj.right - obj.left), (float)(obj.bottom - obj.top));
                    return obj;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "movable":
                            bool movable = reader.GetBoolean();
                            obj.movable = movable;
                            break;
                        case "breakable":
                            bool breakable = reader.GetBoolean();
                            obj.breakable = breakable;
                            break;
                        case "broken":
                            bool broken = reader.GetBoolean();
                            obj.broken = broken;
                            break;
                        case "breaking":
                            bool breaking = reader.GetBoolean();
                            obj.breaking = breaking;
                            break;
                        case "left":
                            int left = reader.GetInt32();
                            obj.left = left;
                            break;
                        case "right":
                            int right = reader.GetInt32();
                            obj.right = right;
                            break;
                        case "top":
                            int top = reader.GetInt32();
                            obj.top = top;
                            break;
                        case "bottom":
                            int bottom = reader.GetInt32();
                            obj.bottom = bottom;
                            break;
                        case "speed":
                            List<int> speed = JsonSerializer.Deserialize<List<int>>(ref reader);
                            obj.speed = new Vector2i(speed[0], speed[1]);
                            break;
                        case "damage":
                            uint damage = reader.GetUInt32();
                            ((Ball)obj).damage = damage;
                            break;
                        case "durability":
                            int durability = reader.GetInt32();
                            ((FieldTile)obj).durability = durability;
                            break;
                        case "color":
                            MyColor color = (MyColor)reader.GetInt32();
                            ((FieldTile)obj).color = color;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer, DisplayObject obj, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            switch (obj)
            {
                case Ball ball:
                    writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Ball);
                    writer.WriteNumber("damage", ball.damage);
                    break;
                case PlayerTile playerTile:
                    writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.PlayerTile);
                    // Write any fields exclusive to PlayerTile here
                    break;
                case FieldTile fieldTile:
                    writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.FieldTile);
                    writer.WriteNumber("durability", fieldTile.durability);
                    writer.WriteNumber("color", (int)fieldTile.color);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected object type.");
            }

            // Write shared fields for all objects
            writer.WriteBoolean("movable", obj.movable);
            writer.WriteBoolean("breakable", obj.breakable);
            writer.WriteBoolean("broken", obj.broken);
            writer.WriteBoolean("breaking", obj.breaking);
            writer.WriteNumber("left", obj.left);
            writer.WriteNumber("right", obj.right);
            writer.WriteNumber("top", obj.top);
            writer.WriteNumber("bottom", obj.bottom);

            writer.WriteStartArray("speed");
            writer.WriteNumberValue(obj.speed.X);
            writer.WriteNumberValue(obj.speed.Y);
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
