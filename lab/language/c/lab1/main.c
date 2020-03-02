#include <stdio.h>

int main()
{
    float fahr, celsius;
    int lower, upper, step;

    lower = 0 ;
    upper = 300;
    step = 20;

    fahr = lower;
    while (fahr <= upper) {
        celsius = (fahr-32) *(5.0/9.0);
        printf("%3.0f\t%6.1f\n", fahr, celsius);
        fahr = fahr + step;
    }
}