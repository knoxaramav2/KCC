#include <stdio.h>
#include <stdlib.h>

void test(int a, int b, int c, int d, int e){


}

int main(){

	float a = 5;
	float b = 4.6;
	float c = 33.33;
	float d = 55.55;
	float e = a*b;
	a+=b+d-e;

	printf("%f %f %f %f %e",a, b, c, d, e);

    return 0;
}