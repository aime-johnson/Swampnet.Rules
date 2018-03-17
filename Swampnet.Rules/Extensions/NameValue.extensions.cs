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


		public static string StringValue(this IEnumerable<INameValue> source, string name, string defaultValue = null)
		{
			var nameValue = source.Get(name);
			return nameValue == null
				? defaultValue
				: nameValue.Value;
		}
	}
}
