using System;
using System.Collections.Generic;
using System.Text;

namespace Swampnet.Rules
{
	public enum ExpressionOperatorType
	{
		//[Display(Name = "@TODO: Friendly name")]
		NULL,

		EQ,
		NOT_EQ,
		REGEX,
		GT,
		GTE,
		LT,
		LTE,

		MATCH_ALL,
		MATCH_ANY
	}
}
