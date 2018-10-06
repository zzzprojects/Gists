// Description: HAP - Load (From File)
// Website: https://html-agility-pack.net/
// Run: https://dotnetfiddle.net/EsvZyg

// @nuget: HtmlAgilityPack

using System;
using System.Xml;
using HtmlAgilityPack;
					
public class Program
{
	public static void Main()
	{
		SaveHtmlFile();
		
		#region example
			
		var path = @"test.html";
		
		var doc = new HtmlDocument();
		doc.Load(path);
		
		var node = doc.DocumentNode.SelectSingleNode("//body");

        Console.WriteLine(node.OuterHtml);	
		
		#endregion
	}
	
	private static void SaveHtmlFile()
	{
		var html =
        @"<!DOCTYPE html>
<html>
<body>
	<h1>This is <b>bold</b> heading</h1>
	<p>This is <u>underlined</u> paragraph</p>
	<h2>This is <i>italic</i> heading</h2>
</body>
</html> ";

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
		
		htmlDoc.Save("test.html");
	}
}