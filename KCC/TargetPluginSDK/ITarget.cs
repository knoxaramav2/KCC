using CodeTranslator;
using KCC;
using System;

namespace TargetPluginSDK
{
    public interface ITarget
    {
        string TargetName { get; }

        void Init(CliOptions cli, InstDeclController controller);

        string Build();

    }
}
