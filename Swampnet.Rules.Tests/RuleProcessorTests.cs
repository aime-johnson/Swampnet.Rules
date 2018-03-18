using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swampnet.Rules.Tests
{
	[TestClass]
	public class RuleProcessorTests
	{
		[TestMethod]
		public void RuleProcessor_Simple()
		{
			var rule = Mock.Rule;
			var prosessor = Mock.RuleProcessor;
			var context = Mock.Context;


			// Assert: "test-01-parameter-01" doesn't exists
			var x = context.Properties.Get("test-01-parameter-01");
			Assert.IsNull(x);


			prosessor.Run(context, rule);


			// Assert: "test-01-parameter-01" now exists and equals "value-01"
			x = context.Properties.Get("test-01-parameter-01");
			Assert.IsNotNull(x);
			Assert.AreEqual("value-01", x.Value);
		}



		[TestMethod]
		public void RuleProcessor_History()
		{
			var rule = Mock.Rule;
			var processor = Mock.RuleProcessor;
			var context = Mock.Context;

			context.Properties.Add(new ContextClassProperty("count", "0"));

			// Fill up history
			for(int i = 0; i < rule.MaxHistoryRequired; i++)
			{
				context.Properties.Get("count").Value = i.ToString();
				processor.Run(context, rule);
				Assert.AreEqual(i + 1, processor.GetHistory(rule).Count());
			}

			for(int i = 0; i < 5; i++)
			{
				// Should truncate
				processor.Run(context, rule);

				Assert.AreEqual(rule.MaxHistoryRequired, processor.GetHistory(rule).Count());
			}
		}
	}
}