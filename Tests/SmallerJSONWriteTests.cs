using NUnit.Framework;
using JsonDict = System.Collections.Generic.Dictionary<string, object>;
using JsonArray = System.Collections.Generic.List<object>;
using SmallerJSON;

[TestFixture]
public class SmallerJsonWriteTests 
{
   private static string WriteObject(object o)
   {
      return Json.Serialize(o, null);
   }

   [Test]
   public void TestWriteArray() {
      Assert.AreEqual("[1,\"My toe whistles\"]", WriteObject(new JsonArray {1, "My toe whistles"}));
   }
   
   [Test]
   public void TestWriteStringWithUtf8Characters() {
      Assert.AreEqual("\"Τη γλώσσα μου έδωσαν ελληνική\"", WriteObject("Τη γλώσσα μου έδωσαν ελληνική"));
   }
   
   [Test]
   public void TestWriteString() {
      Assert.AreEqual("\"hello\\\"a\"", WriteObject("hello\"a"));
   }
}