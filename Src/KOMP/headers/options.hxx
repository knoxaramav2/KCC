#pragma once

#include <vector>
#include <string>

namespace OPTIONS{

    const std::string _helpPath = "/data/help.txt";

    //Not important for overall operation
    enum CliWrnErrEnum{
        CLI_ERR_OK,//Probably not used
        CLI_ERR_MISFORMED,//Illegally formed options
        CLI_ERR_UNKNOWN,//
        CLI_ERR_EMPTY_VALUE
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
    void printVerbose();

    //Switches
    bool pedantic;
    bool ignoreWarning;
    bool verbose;
    bool noLink;
    bool outputAsm;

    //Values
    int optimizationLevel;

    //Input/Output
    std::string outputFileName;

    //Paths
    std::vector <std::string> sourceFiles;

    };

    extern CliOptions _cliOptions;
}