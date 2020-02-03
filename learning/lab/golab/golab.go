package main

import "fmt"

import "time"

func main() {
	c := make(chan int)
	go func() {
		for i := 0; ; i++ {
			fmt.Println(i)
			c <- i
		}
	}()
	for {
		time.Sleep(time.Second)
		fmt.Println(time.Now())
	}
}
