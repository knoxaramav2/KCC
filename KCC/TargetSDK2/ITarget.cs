using CodeTranslator;
using KCC;

namespace TargetPluginSDK
{
    public interface ITarget
    {
        string TargetName { get; }

        void Init(CliOptions cli, InstDeclController controller);

        string Build();

        string DumpAssembly();

    }
}
