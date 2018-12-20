#include "tokenizer.hxx"

using namespace std;

token::token(tokenCode code, std::string tknString){
    this->code = code;
    this->tknString = tknString;
}

