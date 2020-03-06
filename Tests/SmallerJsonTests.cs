using System.Collections.Generic;
using NUnit.Framework;
using System.Text;
using JsonDict = System.Collections.Generic.Dictionary<string, object>;
using JsonArray = System.Collections.Generic.List<object>;
using SmallerJSON;

[TestFixture]
public class SmallerJsonTests 
{

   private static object ParseString(string s)
   {
      return Json.Deserialize(Encoding.UTF8.GetBytes(s));
   }

   [Test]
   public void TestParseNull() {
      var result = ParseString("null");
      Assert.IsNull(result);
   }

   [Test]
   public void TestParseFalse() {
      var result = ParseString("false");
      Assert.AreEqual(false, result);
   }

   [Test]
   public void TestParseTrue() {
      var result = ParseString("true");
      Assert.AreEqual(true, result);
   }

   [Test]
   public void TestParseString() {
      var result = ParseString("\"hello\\\"a\"");
      Assert.AreEqual("hello\"a", result);
   }

   [Test]
   public void TestParseStringWithUnicodeEscape() {
      var result = ParseString("\"hello\\u270Aworld\"");
      Assert.AreEqual("hello\x270Aworld", result);
   }

   [Test]
   public void TestParseStringWithUtf8Characters() {
      var result = ParseString("\"Τη γλώσσα μου έδωσαν ελληνική\"");
      Assert.AreEqual("Τη γλώσσα μου έδωσαν ελληνική", result);
   }

   [Test]
   public void TestParseInt() {
      var result = ParseString("563");
      Assert.AreEqual(563, result);
   }

   [Test]
   public void TestParseDouble() {
      var result = ParseString("1.25");
      Assert.AreEqual(1.25, result);
   }

   [Test]
   public void TestParseEmptyArray() {
      var result = ParseString("[]");
      Assert.AreEqual(new JsonArray(), result);
   }

   [Test]
   public void TestParseSingleElemArray() {
      Assert.AreEqual(new JsonArray {"hello"}, ParseString("[\"hello\"]"));
      Assert.AreEqual(new JsonArray {1}, ParseString("[1]"));
   }

   [Test]
   public void TestParseArray() {
      var result = ParseString("[1, 2, 3]");
      Assert.AreEqual(new JsonArray {1, 2, 3}, result);
   }

   [Test]
   public void TestParseObject() {
      var result = ParseString("{\"foo\":1}") as IDictionary<string, object>;
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result["foo"]);
   }
}