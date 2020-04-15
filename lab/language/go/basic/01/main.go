package main

import "fmt"

func main() {
	fmt.Printf("Test Value : %b\n", ^uint(2))
	fmt.Printf("Test Value : %b\n", ^uint8(2))
	fmt.Printf("Test Value : %b\n", ^uint16(2))
	fmt.Printf("Test Value : %b\n", ^uint32(2))
	fmt.Printf("Test Value : %b\n", ^uintptr(2))
	fmt.Printf("Test Value : %b\n", ^uint64(2))
	fmt.Printf("Test Value : %b\n", int(2))
}
