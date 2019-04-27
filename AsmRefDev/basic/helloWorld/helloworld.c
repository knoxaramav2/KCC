#include <stdio.h>

int globdat = 5;

int testfnc(int a, int b){
	return a+b;
}

int main(){
	
	char * msg = "Hello World";
	int a=6;
	printf("%s %d\r\n", msg, testfnc(a, 8));
	
	return 0;
}