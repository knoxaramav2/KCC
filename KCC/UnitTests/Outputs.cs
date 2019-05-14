using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Outputs
    {
        [TestMethod]
        public void HelloWorld()
        {
            Utils.CallSystem("kcc", "--src=src/prints/helloworld.kcc");
        }
    }
}
