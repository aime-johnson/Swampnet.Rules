using System;
using System.Collections.Generic;
using System.Text;

namespace Swampnet.Rules
{
	/// <summary>
	/// Simple name/value construct. We use this sort of structure everywhere so it's useful to have an interface defined that we can write extensions against.
	/// </summary>
    public interface INameValue
    {
		string Name { get; set; }
		string Value { get; set; }
    }
}
