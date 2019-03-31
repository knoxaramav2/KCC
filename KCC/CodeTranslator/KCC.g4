grammar KCC;

options{
    language= CSharp;
}


rules               : assembly +;

assembly            : ASSEMBLY symbol_id block_struct;
class               : 'class' symbol_id block_struct;

//enclosures
group               : L_PARANTH ((instruction|~R_PARANTH)+)? R_PARANTH;

//defines properties of a body
block_struct        : L_BRACE ((inst_body|~R_BRACE)+)? R_BRACE
                    ;
//contains executable instructions
block_exec          : L_BRACE ((inst_exec|~R_BRACE)+)? R_BRACE
                    ;

block               : L_BRACE ((.|~R_BRACE)+)? R_BRACE;

//allowed in definition
inst_body           : var_decl
                    | fnc_proto
                    | fnc_decl
                    | class;
//allowed in function body
inst_exec           : var_decl
                    | keywords;

instruction         : var_decl SEMI?
                    ;

//keywords
keywords            : return;
return              : RETURN expression? SEMI;

//declarations
var_decl            :symbol_id symbol_id (assignment)?
                    ;
fnc_decl            : fnc_proto block_exec;
fnc_proto           : symbol_id restric_id group SEMI?;

//basic
assignment          : assign_ops value_id;

unary_expr          : unary_ops symbol_id
                    | symbol_id unary_ops
                    ;

//resolution
unary_ops           :INCREMENT
                    |DECRIMENT
                    ;

binary_ops          : ADD
                    | SUBTRACT
                    | MULTIPLY
                    | DIVIDE
                    | EXPONENT
                    | MODULO
                    ;


assign_ops          : SET
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    ;

symbol_id           : IDENTIFIER (('.'IDENTIFIER)+)?;
restric_id          : IDENTIFIER;
value_id            : DECIMAL | IDENTIFIER;

expression          : unary_expr
                    | IDENTIFIER;

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

fragment A          : ('A'|'a') ;
fragment S          : ('S'|'s') ;
fragment Y          : ('Y'|'y') ;

fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

fragment ALPHA  : [a-zA-Z_];


//enclosures
L_BRACKET       : '[';
R_BRACKET       : ']';
L_PARANTH       : '(';
R_PARANTH       : ')';
L_BRACE         : '{';
R_BRACE         : '}';

ASSEMBLY            : 'asm' | 'assembly';

//general identifies
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

