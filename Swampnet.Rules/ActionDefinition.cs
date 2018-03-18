using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Swampnet.Rules
{
	public class ActionDefinition
	{
		public string Name { get; set; }
		/// <summary>
		/// 0 (default) 1 - Execute every hit
		/// </summary>
		public int CosecutiveHits { get; set; }
		public ActionParameter[] Parameters { get; set; }
	}
}
