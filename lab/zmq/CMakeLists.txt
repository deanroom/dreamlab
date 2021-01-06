cmake_minimum_required(VERSION 3.17)
project(zmq C)

set(CMAKE_C_STANDARD 11)

add_executable(zmq
        demo1/client.c
        demo1/server.c)
