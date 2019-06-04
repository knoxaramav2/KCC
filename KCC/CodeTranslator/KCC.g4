grammar KCC;

options{
    language= CSharp;
}


rules               : assembly_decl +;

//constructs
assembly_decl       : ASSEMBLY symbol_id asm_block;
class_decl          : 'class' symbol_id class_block;
function_decl        : symbol_id symbol_id fnc_header function_block;

//blocks
asm_block           : L_BRACE ((class_decl|function_decl|~R_BRACE)+)? R_BRACE;
class_block         : L_BRACE ((function_decl|var_decl|~R_BRACE)+)? R_BRACE;
function_block      : L_BRACE ((var_decl|keyword_call|function_call|~R_BRACE)+)? R_BRACE  ;


//groups
fnc_header          :   L_PARANTH ((var_decl)(','(var_decl)+)?|~R_PARANTH)? R_PARANTH;

call_group          :   L_PARANTH (((value_id|expression)(','(value_id|expression))+)?|~R_PARANTH)? R_PARANTH;
expr_group          :   ;


//keywords
keywords            : 'return'
                    | 'print'
                    | 'exit'
                    ;


//operations
var_decl            : symbol_id symbol_id ('=' (expression|value_id))?;
keyword_call        : keywords (call_group|value_id)?;
function_call       : symbol_id call_group;

unary_expr          : lv_unary_ops
                    | rv_unary_ops
                    | unary_symb symbol_id
                    ;

//operations
expression          : (unary_expr|value_id) binary_ops (unary_expr|value_id|expression)
                    | expression binary_ops (expression|value_id)
                    | unary_expr;

//resolution
rv_unary_ops        : symbol_id increment
                    | symbol_id decrement;

lv_unary_ops        : increment symbol_id
                    | decrement symbol_id;

increment           : INCREMENT;
decrement           : DECRIMENT;

binary_ops          : ADD
                    | SUBTRACT
                    | MULTIPLY
                    | DIVIDE
                    | EXPONENT
                    | MODULO
                    ;

unary_symb          : NOT;

assign_ops          : SET
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    ;



symbol_id           : IDENTIFIER (('.'IDENTIFIER)+)?;
restric_id          : IDENTIFIER;
value_id            : STRINGLIT | DECIMAL | IDENTIFIER;

string              : STRINGLIT;
char                : '\'' ALPHA '\'';



/*
 * Lexer Rules
 */

//keywords
RETURN              : 'return';

//arithmetic operators
SET             : '=';
ADD             : '+';
SUBTRACT        : '-';
MULTIPLY        : '*';
DIVIDE          : '/';
EXPONENT        : '**';
MODULO          : '%' ;
SET_SUM         : '+=';
SET_DIFFERENCE  : '-=';
SET_PRODUCT     : '*=';
SET_QUOTIENT    : '/=';
INCREMENT       : '++';
DECRIMENT       : '--';

//logic operators
NOT             : '!';


fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

fragment ALPHA  : [a-zA-Z_];

fragment NEWLINE : '\r'?'\n';


//enclosures
L_BRACKET       : '[';
R_BRACKET       : ']';
L_PARANTH       : '(';
R_PARANTH       : ')';
L_BRACE         : '{';
R_BRACE         : '}';

ASSEMBLY            : 'asm' | 'assembly';

//general identifiers
STRINGLIT       : STRINGLITPART '"';
STRINGLITPART   : '"' (~["\\\n] | '\\' (. | EOF))*;

DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

