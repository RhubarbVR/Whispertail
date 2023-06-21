using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localisation
{
	public static class EnumerableHelper
	{
		public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) {
			return source.Select((item, index) => (item, index));
		}
	}
}
