// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
/*
var file = File.ReadAllText("./savedrecs.txt");

var head = 0;

while (true)
{
  var start = file.IndexOf("PT", head, StringComparison.OrdinalIgnoreCase);
  if (start == -1)
    return;
  var end = file.IndexOf("ER", start, StringComparison.OrdinalIgnoreCase);
  if (end == -1)
    return;
  
  Console.WriteLine(file[start..end]);

  head = end;
  Console.ReadKey();

}*/

var file = File.ReadLines("./savedrecs.txt");
var enumerable = file as string[] ?? file.ToArray();
var head = 0;

while (true)
{
  var start = head;
  while (start < enumerable.Length && !enumerable[start].StartsWith("PT", StringComparison.Ordinal))
    start++;
  if (start == enumerable.Length)
    return;
  var end = start + 1;
  while (end < enumerable.Length && !enumerable[end].StartsWith("ER", StringComparison.Ordinal))
    end++;
  if (end == enumerable.Length)
    return;
  
  head = end + 1;

  var indexStart = start + 1;
  while (indexStart < end && !enumerable[indexStart].StartsWith("TI", StringComparison.Ordinal))
    indexStart++;
  if (indexStart == end)
    return;
  var indexEnd = indexStart + 1;
  while (indexEnd < end && enumerable[indexEnd].StartsWith(" ", StringComparison.Ordinal))
    indexEnd++;
  if (indexEnd == end)
    return;

  var title = "";
  for (var i = indexStart; i < indexEnd; i++)
  {
    //Console.WriteLine(enumerable[i]);
    title += enumerable[i];
  }

  title = EditPath(RemoveBlanks(title[3..]));
  
  File.WriteAllLines(Path.Combine("./Papers", $"{title}.txt"), enumerable[start..end]);
  
  Console.WriteLine(title);
}


string RemoveBlanks(string strWords)
{
 
  Regex replaceSpace = MyRegex();
 
  return replaceSpace.Replace(strWords, " ").Trim();
 
}

string EditPath(string path)
{
  return Path.GetInvalidFileNameChars().Aggregate(path, (current, ch) => current.Replace(ch, '.'));
}

partial class Program
{
  [GeneratedRegex("\\s{1,}", RegexOptions.IgnoreCase, "zh-CN")]
  private static partial Regex MyRegex();
}