
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;


// namespace OnlineYournal.Code
namespace Open.Infrastructure.Web.DomainMatcher
{
	/// <summary>
	/// Match hostname
	/// </summary>
	internal class HostMatcher
	{
		public HostMatcher(RouteTemplate template)
		{
			if (template == null)
				throw new System.ArgumentNullException(nameof(template) + " is NULL !");

			this.Template = template;
		}
		/// <summary>
		/// Routing template
		/// </summary>
		public RouteTemplate Template { get; }

		/// <summary>
		/// Match hostname
		/// </summary>
		/// <param name="host"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public bool TryMatch(HostString host, RouteValueDictionary values)
		{
			if(values == null)
				throw new System.ArgumentNullException(nameof(values) + " is NULL !");

			//The first paragraph defaults to host {projectcode}.ixiaoben.com.cn
			var firstSegment = this.Template.GetSegment(0);
			if (firstSegment.IsSimple)
			{
				return false;
			}
			if (!MatchComplexSegmentCore(firstSegment, host.Host, values, firstSegment.Parts.Count - 1))
			{
				return false;
			}

			return true;
		}


		//Match complex segments a{project}b...
		private bool MatchComplexSegmentCore(
			TemplateSegment routeSegment,
			string requestSegment,
			RouteValueDictionary values,
			int indexOfLastSegmentUsed)
		{
			// Find last literal segment and get its last index in the string
			var lastIndex = requestSegment.Length;
			TemplatePart parameterNeedsValue = null; // Keeps track of a parameter segment that is pending a value
			TemplatePart lastLiteral = null; // Keeps track of the left-most literal we've encountered
			var outValues = new RouteValueDictionary();
			while (indexOfLastSegmentUsed >= 0)
			{
				var newLastIndex = lastIndex;
				var part = routeSegment.Parts[indexOfLastSegmentUsed];
				if (part.IsParameter)
				{
					// Hold on to the parameter so that we can fill it in when we locate the next literal
					parameterNeedsValue = part;
				}
				else
				{
					lastLiteral = part;
					var startIndex = lastIndex - 1;
					// If we have a pending parameter subsegment, we must leave at least one character for that
					if (parameterNeedsValue != null)
					{
						startIndex--;
					}
					if (startIndex < 0)
					{
						return false;
					}
					var indexOfLiteral = requestSegment.LastIndexOf(
						part.Text,
						startIndex,
						System.StringComparison.OrdinalIgnoreCase);
					if (indexOfLiteral == -1)
					{
						// If we couldn't find this literal index, this segment cannot match
						return false;
					}
					// If the first subsegment is a literal, it must match at the right-most extent of the request URI.
					// Without this check if your route had "/Foo/" we'd match the request URI "/somethingFoo/".
					// This check is related to the check we do at the very end of this function.
					if (indexOfLastSegmentUsed == (routeSegment.Parts.Count - 1))
					{
						if ((indexOfLiteral + part.Text.Length) != requestSegment.Length)
						{
							return false;
						}
					}
					newLastIndex = indexOfLiteral;
				}
				if ((parameterNeedsValue != null) &&
					(((lastLiteral != null) && (part.IsLiteral)) || (indexOfLastSegmentUsed == 0)))
				{
					// If we have a pending parameter that needs a value, grab that value
					int parameterStartIndex;
					int parameterTextLength;
					if (lastLiteral == null)
					{
						if (indexOfLastSegmentUsed == 0)
						{
							parameterStartIndex = 0;
						}
						else
						{
							parameterStartIndex = newLastIndex;
							//Debug.Assert(false, "indexOfLastSegementUsed should always be 0 from the check above");
						}
						parameterTextLength = lastIndex;
					}
					else
					{
						// If we're getting a value for a parameter that is somewhere in the middle of the segment
						if ((indexOfLastSegmentUsed == 0) && (part.IsParameter))
						{
							parameterStartIndex = 0;
							parameterTextLength = lastIndex;
						}
						else
						{
							parameterStartIndex = newLastIndex + lastLiteral.Text.Length;
							parameterTextLength = lastIndex - parameterStartIndex;
						}
					}
					var parameterValueString = requestSegment.Substring(parameterStartIndex, parameterTextLength);
					if (string.IsNullOrEmpty(parameterValueString))
					{
						// If we're here that means we have a segment that contains multiple sub-segments.
						// For these segments all parameters must have non-empty values. If the parameter
						// has an empty value it's not a match.                        
						return false;
					}
					else
					{
						// If there's a value in the segment for this parameter, use the subsegment value
						outValues.Add(parameterNeedsValue.Name, parameterValueString);
					}
					parameterNeedsValue = null;
					lastLiteral = null;
				}
				lastIndex = newLastIndex;
				indexOfLastSegmentUsed--;
			}
			// If the last subsegment is a parameter, it's OK that we didn't parse all the way to the left extent of
			// the string since the parameter will have consumed all the remaining text anyway. If the last subsegment
			// is a literal then we *must* have consumed the entire text in that literal. Otherwise we end up matching
			// the route "Foo" to the request URI "somethingFoo". Thus we have to check that we parsed the *entire*
			// request URI in order for it to be a match.
			// This check is related to the check we do earlier in this function for LiteralSubsegments.
			if (lastIndex == 0 || routeSegment.Parts[0].IsParameter)
			{
				foreach (var item in outValues)
				{
					values.Add(item.Key, item.Value);
				}
				return true;
			}
			return false;
		}
	}
}
