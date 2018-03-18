using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Swampnet.Rules
{
    public static class NameValueExtensions
    {
		public static INameValue Get(this IEnumerable<INameValue> source, string name)
		{
			return source.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
		}

		public static IEnumerable<INameValue> All(this IEnumerable<INameValue> source, string name)
		{
			return source.Where(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
		}


		public static string StringValue(this IEnumerable<INameValue> source, string name, string defaultValue = "")
		{
			var nameValue = source.Get(name);
			return nameValue == null
				? defaultValue
				: nameValue.Value;
		}

		public static int IntValue(this IEnumerable<INameValue> source, string name, int defaultValue = default(int))
		{
			return int.Parse(source.StringValue(name, defaultValue.ToString()));
		}

		public static IEnumerable<string> StringValues(this IEnumerable<INameValue> source, string name)
		{
			return source.All(name).Select(s => s.Value);
		}
	}
}
