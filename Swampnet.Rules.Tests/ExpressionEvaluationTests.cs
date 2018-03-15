using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swampnet.Rules.Tests
{
	[TestClass]
	public class ExpressionEvaluationTests
	{
		[TestMethod]
		public void Evaluate_SimpleExpression_01()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.MATCH_ALL)
			{
				Children = new[]
				{
					new Expression(ExpressionOperatorType.EQ, "{id}", 1),						// true
					new Expression(ExpressionOperatorType.EQ, "{value}", "test"),				// true
					new Expression(ExpressionOperatorType.EQ, "{property-two}", "value-two"),	// true
				}
			};


			bool expected = true;
			bool actual = eval.Evaluate(Mock.Context, expression);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Evaluate_SimpleExpression_02()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.MATCH_ALL)
			{
				Children = new[]
				{
					new Expression(ExpressionOperatorType.EQ, "{id}", 1),							// true
					new Expression(ExpressionOperatorType.EQ, "{property-two}", "madeup value"),	// false
				}
			};


			bool expected = false;
			bool actual = eval.Evaluate(Mock.Context, expression);

			Assert.AreEqual(expected, actual);
		}
	}
}
