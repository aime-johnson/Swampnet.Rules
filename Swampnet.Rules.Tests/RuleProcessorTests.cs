using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

			Assert.AreEqual(1, context.Properties.IntValue("definition-Test-01-fired"));
		}


		[TestMethod]
		public void RuleProcessor_ConsecutiveHits()
		{
			var rule = Mock.Rule;
			var processor = Mock.RuleProcessor;
			var context = Mock.Context;


			processor.Run(context, rule);			
			Assert.AreEqual(1, context.Properties.IntValue("definition-Test-01-fired")); // Test-01 fires every hit
			Assert.AreEqual(0, context.Properties.IntValue("definition-Test-02-fired"));

			processor.Run(context, rule);
			Assert.AreEqual(2, context.Properties.IntValue("definition-Test-01-fired"));
			Assert.AreEqual(0, context.Properties.IntValue("definition-Test-02-fired"));

			processor.Run(context, rule);
			Assert.AreEqual(3, context.Properties.IntValue("definition-Test-01-fired"));
			Assert.AreEqual(1, context.Properties.IntValue("definition-Test-02-fired")); // Test-02 fires after 3 consecutive hits

			processor.Run(context, rule);
			Assert.AreEqual(4, context.Properties.IntValue("definition-Test-01-fired"));
			Assert.AreEqual(2, context.Properties.IntValue("definition-Test-02-fired"));
			
			context.Properties.Get("isTrue").Value = "false";							 // Expression now evaluates to false

			processor.Run(context, rule);
			Assert.AreEqual(4, context.Properties.IntValue("definition-Test-01-fired")); // make sure test-01/02 didn't fire
			Assert.AreEqual(2, context.Properties.IntValue("definition-Test-02-fired"));
			
			context.Properties.Get("isTrue").Value = "true";							 // Expression now evaluates to true again

			processor.Run(context, rule);
			Assert.AreEqual(5, context.Properties.IntValue("definition-Test-01-fired")); // test-01 should fire
			Assert.AreEqual(2, context.Properties.IntValue("definition-Test-02-fired")); // test-02 shouldn't fire (need 3 consecutive hits)

			processor.Run(context, rule);
			Assert.AreEqual(6, context.Properties.IntValue("definition-Test-01-fired"));
			Assert.AreEqual(2, context.Properties.IntValue("definition-Test-02-fired"));

			processor.Run(context, rule);
			Assert.AreEqual(7, context.Properties.IntValue("definition-Test-01-fired"));
			Assert.AreEqual(3, context.Properties.IntValue("definition-Test-02-fired")); // test-02 should fire
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