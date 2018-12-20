#pragma once

#include <string>
#include <vector>

//Define token type
enum tokenCode{

};

//Identifies token data
struct token{
    tokenCode code;
    std::string tknString;
    token(tokenCode, std::string);
};

//Manages tokenization process
class Tokenizer{



public:

};