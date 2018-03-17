using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swampnet.Rules.Tests
{
	[TestClass]
	public class RuleProcessorTests
	{
		[TestMethod]
		public void TestMethod1()
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
	}
}
