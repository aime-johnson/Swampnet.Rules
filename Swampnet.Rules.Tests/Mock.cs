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
					if (context.Properties != null)
					{
						var prp = context.Properties.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
						if (prp != null)
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
			Properties = new List<ContextClassProperty>
			{
				new ContextClassProperty("property-one", "value-one"),
				new ContextClassProperty("property-two", "value-two"),
				new ContextClassProperty("property-three", "value-three"),
				new ContextClassProperty("numeric-one", "1"),
				new ContextClassProperty("numeric-five", "5"),
				new ContextClassProperty("ada", DateTime.Parse("1815-12-10").ToString())
			}
		};


		public static RuleProcessor<ContextClass> RuleProcessor => new RuleProcessor<ContextClass>((actionDefinition) =>
		{
			switch (actionDefinition.Name.ToLowerInvariant())
			{
				case "test-01":
					return (context, rule, definition) =>
					{
						// Copy all the definition arguments to the context properties
						foreach (var arg in definition.Parameters)
						{
							context.Properties.Add(new ContextClassProperty(arg.Name, arg.Value));
						}
					};


				default:
					throw new NotSupportedException(actionDefinition.Name);
			}
		}, Mock.Evaluator);


		public static Rule Rule => new Rule()
		{
			Expression = new Expression(ExpressionOperatorType.EQ, true, true),
			TrueActions = new[]
			{
				new ActionDefinition()
				{
					Name = "Test-01",
					Parameters = new[]
					{
						new ActionParameter("test-01-parameter-01", "value-01"),
						new ActionParameter("test-01-parameter-02", "value-02")
					}
				}
			}
		};
	}
}
