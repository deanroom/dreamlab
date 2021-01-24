#include <stdio.h>
#include <stdlib.h>

int main(int argc, char* argv[])
{
    printf("size of double is %d\n",(int) sizeof(double));   

    int *x = malloc(10* sizeof(int));
    printf("%d\n",(int) sizeof(x));
}