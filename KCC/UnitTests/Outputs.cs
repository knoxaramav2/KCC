using CommonLangLib;
using KCC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Outputs
    {
        string helloPath = "src/prints/helloworld";

        public Outputs()
        {
            KCCEnv.Init();
        }

        [TestMethod]
        public void FilesExist()
        {
            CliOptions.GetInstance().Refresh(new string[] { "--src="+ helloPath + ".kcc" });
            Utils.CallSystem("kcc", "--src="+ helloPath);

            Assert.IsTrue(File.Exists(helloPath + ".kcc"), $"{helloPath}.kcc not found");
            Assert.IsTrue(File.Exists(helloPath + ".s"), $"{helloPath}.s not found");
            Assert.IsTrue(File.Exists(helloPath + ".exe") || File.Exists(helloPath), $"{helloPath}(.exe) not found");
        }

        [TestMethod]
        public void HelloWorld()
        {
            CliOptions cli = CliOptions.GetInstance();
            cli.Refresh(new string[] { "--src=" +KCCEnv.BaseUri+"/"+helloPath + ".kcc" });
            var exec = CliOptions.Arch.OS == CommonLangLib.OS.Windows ? ".exe" : "";
            string actual = Utils.CallSystem($"{KCCEnv.BaseUri}/src/prints/helloworld{exec}","");
            //string actual = Utils.CallSystem($"echo", "meh");
            string expected = "Hello, world \r\nGoodbye\r\n";
            Assert.AreEqual(expected, actual, "Hello world output not as expected");
        }
    }
}
