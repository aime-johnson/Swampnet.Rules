using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swampnet.Rules.Tests
{
	[TestClass]
	public class ExpressionEvaluationTests
	{
		[TestMethod]
		public void Expression_Evaluate_EQ()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.EQ, "{id}", 1);              // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_EQ_DateTime()
		{
			var eval = Mock.Evaluator;
			var dt = DateTime.Parse("1815-12-10");

			var expression = new Expression(ExpressionOperatorType.EQ, "{ada}", dt);              // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_NOT_EQ()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.NOT_EQ, "{id}", 2);          // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_LT_DateTime()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.LT, "{ada}", DateTime.Now);              // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_GT_DateTime()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.GT, DateTime.Now, "{ada}");              // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_LT()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.LT, "{id}", 2);          // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_LTE()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.LTE, "{id}", 1);          // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_GT()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.GT, 2, "{id}");          // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_GTE()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.GTE, 1, "{id}");          // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}

		[TestMethod]
		public void Expression_Evaluate_REGEX()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.REGEX, "{value}", @"t..t");  // true

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_MATCH_ALL()
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


			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_MATCH_ANY()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.MATCH_ANY)
			{
				Children = new[]
				{
					new Expression(ExpressionOperatorType.EQ, "{id}", 2),						// false
					new Expression(ExpressionOperatorType.EQ, "{value}", "test"),				// true
					new Expression(ExpressionOperatorType.EQ, "{property-two}", "made up"),		// false
				}
			};


			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}



		[TestMethod]
		public void Expression_Evaluate_ComplexExpression_02()
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


			Assert.IsFalse(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_LookupsBothSides()
		{
			var eval = Mock.Evaluator;

			// {id} and {numeric-one} are both context lookups (ie, we're not checking against a constant value)
			var expression = new Expression(ExpressionOperatorType.EQ, "{id}", "{numeric-one}");

			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}


		[TestMethod]
		public void Expression_Evaluate_ComplexExpression()
		{
			var eval = Mock.Evaluator;

			var expression = new Expression(ExpressionOperatorType.MATCH_ALL)
			{
				Children = new[]
				{
					new Expression(ExpressionOperatorType.EQ, "{id}", 1),									// true
					new Expression(ExpressionOperatorType.EQ, "{property-two}", "value-two"),				// true
					new Expression(ExpressionOperatorType.EQ, "{id}", "{numeric-one}"),						// true
					new Expression(ExpressionOperatorType.MATCH_ANY)
					{
						Children = new[]
						{
							new Expression(ExpressionOperatorType.EQ, "{property-one}", "madeup"),			// false
							new Expression(ExpressionOperatorType.EQ, "{property-three}", "value-three"),	// true
						}
					}
				}
			};


			Assert.IsTrue(eval.Evaluate(Mock.Context, expression));
		}
	}
}
