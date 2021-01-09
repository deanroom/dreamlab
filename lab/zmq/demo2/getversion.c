#include <zmq.h>
#include <stdio.h>

int main(void)
{
    int major = 0;
    int minor = 0;
    int patch = 0;

    zmq_version(&major, &minor, &patch);

    printf("ZMQ_VERSION == %d.%d.%d\n", major, minor, patch);

    return 0;
}