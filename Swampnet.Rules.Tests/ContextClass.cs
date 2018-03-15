using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swampnet.Rules.Tests
{
	class ContextClass
	{
		public int Id { get; set; }
		public string Value { get; set; }
		public ContextClassProperty[] Properties { get; set; }
	}


	class ContextClassProperty
	{
		public ContextClassProperty()
		{
		}

		public ContextClassProperty(string name, string value)
			: this()
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public string Value { get; set; }
	}
}
