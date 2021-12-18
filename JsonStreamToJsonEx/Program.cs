using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JsonStreamToJsonEx
{
    class Program
    {
        private static IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static Stack<string> _context = new Stack<string>();
        private static string _currentPath;
        static void Main(string[] args)
        {
			var path = @"D:\2019VSProject\NetCoreFrameworkStudy\FileConfigurationEx\profile.json";
			var file = File.ReadAllText(path);
			var fileStream = new FileStream(path, FileMode.Open);
			var d = ParseStream(fileStream);

			Console.WriteLine("Hello World!");
        }


        private static IDictionary<string,string> ParseStream(Stream input)
        {
            _data.Clear();
            JsonDocumentOptions options = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            using(StreamReader sr= new StreamReader(input))
            {
                using JsonDocument jd = JsonDocument.Parse(sr.ReadToEnd(), options);
                if (jd.RootElement.ValueKind != JsonValueKind.Object)
                {
                    throw new FormatException("Error_UnsupportedJSONToken");
                }
				VisitElement(jd.RootElement);
			}
			return _data;
        }
		private static void VisitElement(JsonElement element)
		{
			foreach (JsonProperty jsonProperty in element.EnumerateObject())
			{
				EnterContext(jsonProperty.Name);
				VisitValue(jsonProperty.Value);
				ExitContext();
			}
		}
		private static void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse<string>());
        }
		private static void ExitContext()
		{
			_context.Pop();
			_currentPath = ConfigurationPath.Combine(_context.Reverse<string>());
		}
		private static void VisitValue(JsonElement value)
		{
			switch (value.ValueKind)
			{
				case JsonValueKind.Object:
					VisitElement(value);
					return;
				case JsonValueKind.Array:
					{
						int num = 0;
						using (JsonElement.ArrayEnumerator enumerator = value.EnumerateArray().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								JsonElement value2 = enumerator.Current;
								EnterContext(num.ToString());
								VisitValue(value2);
								ExitContext();
								num++;
							}
							return;
						}
						break;
					}
				case JsonValueKind.String:
				case JsonValueKind.Number:
				case JsonValueKind.True:
				case JsonValueKind.False:
				case JsonValueKind.Null:
					break;
				default:
					throw new FormatException("Error_UnsupportedJSONToken");
			}
			string currentPath = _currentPath;
			if (_data.ContainsKey(currentPath))
			{
				throw new FormatException("Error_KeyIsDuplicated");
			}
			_data[currentPath] = value.ToString();
		}
	}
}
