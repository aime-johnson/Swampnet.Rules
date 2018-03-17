using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Swampnet.Rules
{
    public class RuleProcessor<T>
    {
		private readonly Func<ActionDefinition, Action<T, Rule, ActionDefinition>> _resolver;
		private readonly Evaluator<T> _evaluator;

		// Action<T, Rule<T>	
		// resolver:
		//	Func - Takes an ActionDefinition and returns an Action<T, Rule<T>>
		public RuleProcessor(
			Func<ActionDefinition, Action<T, Rule, ActionDefinition>> actionResolver,
			Evaluator<T> evaluator
			)
		{
			_resolver = actionResolver;
			_evaluator = evaluator;
		}


		public void Run(T context, Rule rule)
		{
			var result = _evaluator.Evaluate(context, rule.Expression);

			ProcessActions(context, rule, result ? rule.TrueActions : rule.FalseActions);
		}

		private void ProcessActions(T context, Rule rule, IEnumerable<ActionDefinition> actionDefinitions)
		{
			if (actionDefinitions != null)
			{
				foreach (var definition in actionDefinitions)
				{
					Trace.WriteLine($">> {definition.Name}");
					try
					{
						_resolver
							.Invoke(definition)                     // Get Action<T, Rule<T>, ActionDefinition>
							.Invoke(context, rule, definition);     // And execute it
					}
					catch (Exception ex)
					{
						Trace.TraceError($"{definition.Name} threw error: " + ex.Message);
					}
				}
			}
		}
	}
}
