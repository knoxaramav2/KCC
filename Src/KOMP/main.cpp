#include <stdio.h>
#include <string>
#include <vector>

#include "options.hxx"

using namespace OPTIONS;
using namespace std;

void cliArgProc(int argc, char ** argv){

    //Parse CLI options, retrieve extrenous commands and unrecognized options
    CliOptions cliOptions(argc, argv);
    SubCommandOptions subOptions = cliOptions.getSubCommandOptions();
    vector <CliWrnErr> cliWrnErr = cliOptions.getCliWrnErr();
}

int main(int argc, char ** argv, char ** argx){

    cliArgProc(argc, argv);

    printf("\r\nDONE\r\n");

    return 0;
}