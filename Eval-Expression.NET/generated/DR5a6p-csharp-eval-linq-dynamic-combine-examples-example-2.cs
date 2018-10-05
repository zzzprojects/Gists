// Description: C# Eval - linq-dynamic-combine-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/DR5a6p

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using Z.Expressions;

					
public class Program
{
	public static void Main()
	{
		int[] vectorA = {0, 2, 4, 5, 6};
		int[] vectorB = {1, 3, 5, 7, 8};
		
		EvalManager.DefaultContext.RegisterExtensionMethod(typeof(CustomSequenceOperators));
		var dotProduct = vectorA.Execute<int>("Combine(vectorB, (a, b) => a * b).Sum()", new {vectorB});


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