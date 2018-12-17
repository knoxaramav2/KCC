#include "options.hxx"

#include <string>
#include <vector>
#include <fstream>
#include <sstream>

using namespace std;
using namespace OPTIONS;


CliOptions::CliOptions(int argc, char ** argv){
    
    setDefault();

    workingDirectory = argv[0];
    size_t execSlash = workingDirectory.find_last_of('/');
    workingDirectory.erase(workingDirectory.begin()+execSlash, workingDirectory.end());

    for (int i = 1; i < argc; ++i){
        string cmd = string(argv[i]);
        size_t length = cmd.length();

        //Check for -[-] validity
        int dashMode = 1;
        
        if (cmd[0] != '-') {
            dashMode = -1;
            }
        else if (cmd[1] == '-') {
            dashMode = 2;
            }
        
        if (cmd.length() < 2 || dashMode==-1) {
            if (_subCommandOptions == nullptr){
                _subCommandOptions = new SubCommandOptions();
            }
            _subCommandOptions->unusedParams.push_back(cmd);
            _cliWrnErr.push_back(CliWrnErr(cmd, CliWrnErrEnum::CLI_ERR_MISFORMED));
            continue;
        }

        if (dashMode == 1){
            for (size_t x = 1; x < length; ++x){
                char c = cmd[x];

                switch(c){
                    case 'h': loadDisplayHelp(); break;

                    default:
                    _cliWrnErr.push_back(CliWrnErr(string(1, c), CliWrnErrEnum::CLI_ERR_UNKNOWN));
                }
            }
        } else {
            string values;
            size_t set = cmd.find_first_of('=');
            if (set != string::npos){
                values = cmd.substr(set+1, cmd.length());
                cmd.erase(cmd.begin()+set, cmd.end());
            }

            //Act on options
            if (cmd == "--help"){
                loadDisplayHelp();
            }
        }
    }
}

SubCommandOptions CliOptions::getSubCommandOptions(){
    if (_subCommandOptions == nullptr){
        return SubCommandOptions();
    }

    return *_subCommandOptions;
}

vector <CliWrnErr> CliOptions::getCliWrnErr(){
    return _cliWrnErr;
}

CliWrnErr::CliWrnErr(string msg, CliWrnErrEnum code){
    msg = msg;
    code = code;
}


//Assist

void CliOptions::loadDisplayHelp(){
    
    string path = workingDirectory + _helpPath;
    ifstream helpFile(path);

    if (!helpFile){
        printf("Help file not found\r\n");
        return;
    }

    string line;
    while(getline(helpFile, line)){
        printf("%-20s\r\n", line.c_str());
    }

    fflush(stdout);

    helpFile.close();
}

void CliOptions::setDefault(){
    _subCommandOptions = nullptr;

    //Switches
    pedantic = false;
    ignoreWarning = false;
    elevateWarning = false;

    //Values
    optimizationLevel = 0;
}