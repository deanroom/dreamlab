#include <stdio.h> 

int bitcount(unsigned x)
{
    int b;
    for (b = 0; x !=0; x &= x-1)
    {
        b++;
        printf("A loop of %d,and the number is %d \n",b,x);
    }
    return b;
}
int main()
{
    printf("The number of 1 in x is %d.\n",bitcount(255));
} 
