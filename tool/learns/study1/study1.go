package main

import "fmt"

func main() {
	primes := [6]int{2, 3, 5, 7, 11, 13}

	var slice []int = primes[1:3]
	var s []int = primes[2:5]
	longs := append(slice, s...)
	fmt.Println(len(slice))
	fmt.Println(len(s))
	fmt.Println(len(longs))

}
