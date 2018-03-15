using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swampnet.Rules.Tests
{
	static class Mock
	{
		public static Evaluator<ContextClass> Evaluator => new Evaluator<ContextClass>((context, propertyName) => {
			object value = "";

			switch (propertyName.ToLowerInvariant())
			{
				case "id":
					value = context.Id;
					break;

				case "value":
					value = context.Value;
					break;

				default:
					if(context.Properties != null)
					{
						var prp = context.Properties.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
						if(prp != null)
						{
							value = prp.Value;
						}
					}
					break;
			}

			return value;
		});


		public static ContextClass Context => new ContextClass()
		{
			Id = 1,
			Value = "Test",
			Properties = new[]
				{
					new ContextClassProperty("property-one", "value-one"),
					new ContextClassProperty("property-two", "value-two"),
					new ContextClassProperty("property-three", "value-three")
				}
		};
	}
}
