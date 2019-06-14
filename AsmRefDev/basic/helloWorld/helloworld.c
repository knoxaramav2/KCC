#include <stdio.h>
#include <stdlib.h>

void function(int c){
	
	printf("Sup %c", c);
}

int main(){

	int c = 2;
	
	char * x = malloc(2);

	function(c+7);
    printf("%d\r\n", c);

    return c;

}