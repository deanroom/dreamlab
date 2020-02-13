package main

import (
	"fmt"
	"net"
	"strconv"
	"time"
)

func main() {
	var portsRange [256]string
	for i := 1; i < 255; i++ {
		portsRange[i] = strconv.Itoa(i)
	}
	var ports []string = portsRange[:]
	raw_connect("192.168.1.1", ports)
}

func raw_connect(host string, ports []string) {
	for _, port := range ports {
		timeout := time.Second
		conn, err := net.DialTimeout("tcp", net.JoinHostPort(host, port), timeout)
		if err != nil {
			fmt.Println("Connecting error:", err)
		}
		if conn != nil {
			defer conn.Close()
			fmt.Println("Opened", net.JoinHostPort(host, port))
		}
	}
}
