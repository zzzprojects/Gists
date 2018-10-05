// Description: C# Eval - linq-dynamic-combine-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/sqa5uV

using System;
using System.Linq;
using System.Collections.Generic;

					
public class Program
{
	public static void Main()
	{
		int[] vectorA = {0, 2, 4, 5, 6};
		int[] vectorB = {1, 3, 5, 7, 8};

		var dotProduct = vectorA.Combine(vectorB, (a, b) => a * b).Sum();

		Console.WriteLine("Dot product: {0}", dotProduct);


	}
	
	
}

public static class CustomSequenceOperators
    {
        public static IEnumerable<S> Combine<S>(this IEnumerable<S> first, IEnumerable<S> second, Func<S, S, S> func)
        {
            using (IEnumerator<S> e1 = first.GetEnumerator(), e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }
    }