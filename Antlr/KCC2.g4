grammar KCC2;

options{
    language= CSharp;
}

/*
high                : '*'|'/';
low                 : '+'|'-';

test                : test high test
                    | test low test
                    | '(' test ')'
                    | integer
                    ;*/

rules               : assembly_decl +;

//constructs
assembly_decl       : ASSEMBLY symbol_id asm_block;
class_decl          : 'class' symbol_id class_block;
function_decl        : symbol_id symbol_id fnc_header function_block;

//blocks
asm_block           : L_BRACE ((class_decl|function_decl|~R_BRACE)+)? R_BRACE;
class_block         : L_BRACE ((function_decl|var_decl|~R_BRACE)+)? R_BRACE;
function_block      : L_BRACE ((var_decl|keyword_call|function_call|expression|~R_BRACE)+)? R_BRACE  ;


//groups
fnc_header          :   L_PARANTH ((var_decl)(','(var_decl)+)?|~R_PARANTH)? R_PARANTH;

call_group          :   L_PARANTH ((value_id|expression)((','(value_id|expression))+)?|~R_PARANTH)? R_PARANTH;
expr_group          :   ;


index_anyvalue      : index_string|index_integer|index_variable;
index_string        : '['string']';
index_integer       : '['integer']';
index_variable      : '[' ']';

//keywords
keywords            : 'return'
                    | 'print'
                    | 'exit'
                    ;


//operations
var_decl            : symbol_id (index_anyvalue)? symbol_id ('=' (expression|value_id))?;
keyword_call        : keywords (call_group|value_id)?;
function_call       : call_group;

unary_expr          : rv_unary_ops #rv
                    | lv_unary_ops #lv
                    ;

//operations
/*
expression          : (unary_expr|value_id) binary_ops (unary_expr|value_id|expression)
                    | expression binary_ops (expression|value_id)
                    | unary_expr;*/

expression          : '-' expression
                    | expression oop_1 expression
                    | expression oop_2 expression
                    | expression oop_3 expression
                    | expression oop_4 expression
                    | expression oop_5 expression
                    | expression oop_6 expression
                    | expression oop_7 expression
                    | expression oop_8 expression
                    | expression oop_9 expression
                    | expression oop_10 expression
                    | expression oop_11 expression
                    | expression oop_12 expression
                    | expression oop_13 expression
                    | expression oop_14 expression
                    | expression oop_15 expression
                    | expression oop_16 expression
                    | value_id

                    ;

oop_1               :  ('.'|index_anyvalue)
                    ;

oop_2               : //index_anyvalue#index
                     function_call#fnc_call
                    ;

oop_3               : INCREMENT#post_inc
                    | DECRIMENT#post_dec
                    ;

oop_4               : //INCREMENT #pre_inc
                     DECRIMENT symbol_id#pre_dec
                    | NOT#not
                    | BIT_INVERT#invert
                    | '(' restric_id ')'#cast
                    ;

oop_5               :  MULTIPLY#product
                    |  DIVIDE#quotient
                    |  MODULO#remainder
                    |  EXPONENT#power
                    ;

oop_6               : ADD#sum
                    | SUBTRACT#difference
                    ;

oop_7               : BIT_LSHIFT#left_shift
                    | BIT_RSHIFT#right_shift
                    ;

oop_8               : LESS_THAN#less
                    | expression LESS_OR_EQUAL#less_equal
                    | GREATER_THAN#greater
                    | GREATER_OR_EQUAL#greater_equal
                    ;

oop_9               : EQUAL_TO#equal
                    | NOT_EQUAL#not_equal
                    ;

oop_10              : BIT_AND;
oop_11              : BIT_XOR;
oop_12              : BIT_OR;

oop_13              : AND#and
                    | NAND#nand;

oop_14              : OR#or
                    | NOR#nor
                    | XOR#xor
                    | XNOR#xnor
                    ;

oop_15              : SET#set
                    | SET_SUM#set_sum
                    | SET_DIFFERENCE#set_diff
                    | SET_PRODUCT#set_product
                    | SET_QUOTIENT#set_quotient
                    | SET_REMAINDER#set_remainder
                    | SET_AND#set_and
                    | SET_XOR#set_xor
                    | expression SET_OR expression #set_or
                    ;

oop_16              : LIST#list;

//resolution
rv_unary_ops        : symbol_id increment
                    | symbol_id decrement;

lv_unary_ops        : increment symbol_id
                    | decrement symbol_id
                    | unary_symb symbol_id;

increment           : INCREMENT;
decrement           : DECRIMENT;

binary_ops          : math_ops
                    | assign_ops
                    | logic_ops
                    | bitwise_ops
                    ;

unary_symb          : NOT | BIT_INVERT;

assign_ops          : SET
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    | SET_REMAINDER
                    | SET_AND
                    | SET_XOR
                    | SET_OR
                    ;

math_ops            : ADD
                    | SUBTRACT
                    | MULTIPLY
                    | DIVIDE
                    | EXPONENT
                    | MODULO
                    ;

logic_ops           : AND
                    | NAND
                    | OR
                    | NOR
                    | XOR
                    | XNOR
                    ;

bitwise_ops         : BIT_AND
                    | BIT_OR
                    | BIT_XOR
                    | BIT_LSHIFT
                    | BIT_RSHIFT
                    ;



symbol_id           : IDENTIFIER;
restric_id          : IDENTIFIER;
value_id            : bool |string | char | decimal | integer | symbol_id;

bool                : 'true' | 'false';
decimal             : '-'?DECIMAL;
integer             : '-'?INTEGER;
string              : STRINGLIT;
char                : '\'' ALPHA '\'';

/*
 * Lexer Rules
 */

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
SET_REMAINDER   : '%=';
INCREMENT       : '++';
DECRIMENT       : '--';
SET_AND         : '&=';
SET_OR          : '|=';
SET_XOR         : '^=';

//logic operators
AND             : '&&';
NAND            : '!&';
OR              : '||';
NOR             : '!|';
XOR             : '|||';
XNOR            : '!||';
NOT             : '!';

//bitwise operators

BIT_AND         : '&';
BIT_OR          : '|';
BIT_INVERT      : '~';
BIT_LSHIFT      : '<<';
BIT_RSHIFT      : '>>';
BIT_XOR         : '^';

//comparison operators
LESS_THAN       : '<';
GREATER_THAN    : '>';
LESS_OR_EQUAL   : '<=';
GREATER_OR_EQUAL: '>=';
EQUAL_TO        : '==';
NOT_EQUAL       : '!=';

//other
LIST            : ',';

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

INTEGER         : [0-9];
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

