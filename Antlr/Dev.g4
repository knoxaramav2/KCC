grammar Dev;

options{
    language= CSharp;
}


rules               : assembly +;

assembly            : ASSEMBLY symbol_id block_struct;
class               : 'class' symbol_id block_struct;

//defines properties of a body
block_struct        : L_BRACE ((instruction| class |~R_BRACE)+)? R_BRACE
                    ;
//contains executable instructions
block_exec          :
                    ;

instruction         :
                    var_decl SEMI
                    ;

//declarations
var_decl            :symbol_id symbol_id (assign_ops value_id)?
                    ;

//resolution

assign_ops          : '='
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    ;

symbol_id           : IDENTIFIER;
value_id            : DECIMAL | IDENTIFIER;


/*
 * Lexer Rules
 */

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

ASSEMBLY            : 'asm';

//general identifies
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

