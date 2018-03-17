using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Swampnet.Rules
{
    public class Rule
    {
		public Expression Expression { get; set; }

		/// <summary>
		/// Actions taken when expression is true
		/// </summary>
		public ActionDefinition[] TrueActions { get; set; }

		/// <summary>
		/// Actions taken when expression is false
		/// </summary>
		public ActionDefinition[] FalseActions { get; set; }
	}


	public class ActionDefinition
	{
		public string Name { get; set; }
		public ActionParameter[] Parameters { get; set; }
	}


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
