#pragma once

#include <vector>
#include <string>

namespace OPTIONS{

    const std::string _helpPath = "/data/help.txt";

    //Not important for overall operation
    enum CliWrnErrEnum{
        CLI_ERR_OK,
        CLI_ERR_MISFORMED,
        CLI_ERR_UNKNOWN
    };

    struct CliWrnErr{
        std::string msg;
        CliWrnErrEnum code;

        CliWrnErr(std::string, CliWrnErrEnum);
    };

    struct SubCommandOptions{
        std::vector<std::string> unusedParams;
    };

    //Stores CLI options for reference during compilation
    class CliOptions{

    SubCommandOptions * _subCommandOptions;
    std::vector <CliWrnErr> _cliWrnErr;

    //Assist methods
    void loadDisplayHelp();
    void setDefault();

    //Basic environment info
    std::string workingDirectory;

    public:
    CliOptions(int argc, char ** argv);

    SubCommandOptions getSubCommandOptions();
    std::vector <CliWrnErr> getCliWrnErr();

    //Switches
    bool pedantic;
    bool ignoreWarning;
    bool elevateWarning;


    //Values
    int optimizationLevel;

    //Paths
    std::vector <std::string> sourceFiles;

    };

    extern CliOptions _cliOptions;
}