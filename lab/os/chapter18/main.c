#include <stdlib.h>
#include <stdio.h>

int main(int argc, char* argv[])
{
    int array[10000];
    for (size_t i = 0; i < 1000; i++)
    {
        array[i] = 0;
    }
    printf("length is %d\n",(int) sizeof(array));
    
}