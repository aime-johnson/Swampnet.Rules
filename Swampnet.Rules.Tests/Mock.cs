using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swampnet.Rules.Tests
{
	static class Mock
	{
		public static ContextClass Context => new ContextClass()
		{
			Id = 1,
			Value = "Test",
			Properties = new List<ContextClassProperty>
			{
				new ContextClassProperty("isTrue", "true"),
				new ContextClassProperty("property-one", "value-one"),
				new ContextClassProperty("property-two", "value-two"),
				new ContextClassProperty("property-three", "value-three"),
				new ContextClassProperty("numeric-one", "1"),
				new ContextClassProperty("numeric-five", "5"),
				new ContextClassProperty("ada", DateTime.Parse("1815-12-10").ToString())
			}
		};


		public static Rule Rule => new Rule()
		{
			Expression = new Expression(ExpressionOperatorType.EQ, "{isTrue}", true), // Always true
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
				},
				new ActionDefinition()
				{
					Name = "Test-02",
					CosecutiveHits = 3,
					Parameters = new[]
					{
						new ActionParameter("test-02-parameter-01", "value-01")
					}
				}

			}
		};


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
						value = context.Properties.StringValue(propertyName);
					}
					break;
			}

			return value;
		});


		public static RuleProcessor<ContextClass> RuleProcessor => new RuleProcessor<ContextClass>((actionDefinition) =>
		{
			// Normally you'd set up a way to map various action definitions to your own custom handlers. Just implement a generic solution here for tests.
			return (context, rule, definition) =>
			{
				var nv = context.Properties.Get($"definition-{definition.Name}-fired");
				if(nv == null)
				{
					nv = new ContextClassProperty($"definition-{definition.Name}-fired", "0");
					context.Properties.Add((ContextClassProperty)nv);
				}
				nv.Value = (int.Parse(nv.Value) + 1).ToString();
				
				// Copy all the definition arguments to the context properties
				foreach (var parameter in definition.Parameters)
				{
					context.Properties.Add(new ContextClassProperty(parameter.Name, parameter.Value));
				}
			};
		}, Mock.Evaluator);


	}
}
