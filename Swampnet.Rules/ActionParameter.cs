using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Swampnet.Rules
{
	public class ActionParameter : INameValue
	{
		public ActionParameter()
		{
		}

		public ActionParameter(string name, string value)
			: this()
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return $"[{Name}] {Value}";
		}
	}
}
