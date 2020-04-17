package main

import (
	"log"

	"github.com/aliyun/aliyun-oss-go-sdk/oss"
)

func main() {
	client, err := oss.New("oss-cn-hangzhou.aliyuncs.com", "", "")
	if err != nil {
		// HandleError(err)
		log.Println(err)
	}

	lsRes, err := client.ListBuckets()
	if err != nil {
		log.Println(err)
		// HandleError(err)
	}

	for _, bucket := range lsRes.Buckets {
		log.Println("Buckets:", bucket.Name)
	}
}
