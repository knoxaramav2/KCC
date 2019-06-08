grammar KCC3;

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
function_decl        : fnc_type=symbol_id fnc_name=symbol_id fnc_header function_block;

//blocks
asm_block           : L_BRACE ((class_decl|function_decl|~R_BRACE)+)? R_BRACE;
class_block         : L_BRACE ((function_decl|var_decl|~R_BRACE)+)? R_BRACE;
function_block      : L_BRACE ((var_decl|keyword_call|function_call|expression|~R_BRACE)+)? R_BRACE  ;

//groups
fnc_header          :   L_PARANTH ((var_decl)(','(var_decl)+)?|~R_PARANTH)? R_PARANTH;

call_group          :   L_PARANTH ((value_id|expression)((','(value_id|expression))+)?|~R_PARANTH)? R_PARANTH;

index_anyvalue      : index_string|index_integer|index_variable;
index_string        : '['string|char']';
index_integer       : '['integer']';
index_variable      : '[' ']';


function_call       : symbol_id call_group
                    ;
keyword_call        : keywords (call_group|expression)?
                    ;
var_decl            : var_type=symbol_id index_anyvalue? var_name=symbol_id ('=' expression)?
                    ;

//keywords
keywords            : 'return'
                    | 'print'
                    | 'exit'
                    ;

//operations
/*
expression          : (unary_expr|value_id) binary_ops (unary_expr|value_id|expression)
                    | expression binary_ops (expression|value_id)
                    | unary_expr;*/

accessor            : symbol_id ('.' symbol_id)*
                    ;

val_var             : (accessor|value_id);

lval_op             : accessor INCREMENT        #post_inc
                    | accessor DECRIMENT        #post_dec
                    | accessor index_anyvalue   #index
                    | accessor call_group       #fnc_call
                    ;

rval_op             : INCREMENT accessor        #pre_inc
                    | DECRIMENT accessor        #pre_dec
                    | NOT val_var               #not
                    | BIT_INVERT val_var        #invert
                    | (symbol_id) val_var       #type_cast
                    ;

expression          : symbol_id
                    | value_id
                    | accessor expression
                    | lval_op
                    | rval_op
                    | expression prod_op=('*'|'/'|'%'|'**') expression
                    | expression sum_op=('+'|'-') expression
                    | expression shift_op=('<<'|'>>') expression
                    | expression gl_compare=('<'|'<='|'>'|'>=') expression
                    | expression eq_compare=('=='|'!=') expression
                    | expression bw_and='&' expression
                    | expression bw_xor='^' expression
                    | expression bw_or='|' expression
                    | expression lg_and=('&&'|'!&') expression
                    | expression lg_or=('||'|'!|'|'|||'|'!||') expression
                    | expression op_sum=('='|'+='|'-='|'*='|'/='|'%='|'&='|'^='|'|=') expression index_anyvalue?
                    | expression list=',' expression
                    | '(' paran=expression ')'
                    ;

comparison_outer    : LESS_THAN
                    | LESS_OR_EQUAL
                    | GREATER_THAN
                    | GREATER_OR_EQUAL
                    ;

comparison_inner    : EQUAL_TO
                    | NOT_EQUAL
                    ;

symbol_id           : IDENTIFIER;
value_id            : bool |string | char | integer | decimal | symbol_id;

bool                : 'true' | 'false';
decimal             : DECIMAL;
integer             : INTEGER;
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

INTEGER         : '-'?[0-9];
DECIMAL         : '-'?[0-9]+('.'([0-9])+)?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;
