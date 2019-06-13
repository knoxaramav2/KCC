#include <stdio.h>

int main(){

    char * s = "hello world";
    char * g = "goodbye";
    int a = 500;
    int b = 2;
    int c = a * b << 7;

    printf("%d\r\n", c);

    return c;

}