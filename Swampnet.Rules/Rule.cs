using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Swampnet.Rules
{
    public class Rule
    {
		public Expression Expression { get; set; }

		/// <summary>
		/// Actions taken when expression is true
		/// </summary>
		public ActionDefinition[] TrueActions { get; set; }

		/// <summary>
		/// Actions taken when expression is false
		/// </summary>
		public ActionDefinition[] FalseActions { get; set; }

		// @todo: Calculated from Actions I guess. Think I'd prefer this to be in the processor rather than here?
		public int MaxHistoryRequired =>
			(TrueActions ?? Enumerable.Empty<ActionDefinition>().ToArray())
				.Concat((FalseActions ?? Enumerable.Empty<ActionDefinition>().ToArray()))
				.Max(x => x.CosecutiveHits);
	}
}
