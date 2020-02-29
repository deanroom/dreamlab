package main

import (
	"fmt"
	"log"
	"sync"
	"time"

	"github.com/pdfcpu/pdfcpu/pkg/api"
	"github.com/pdfcpu/pdfcpu/pkg/pdfcpu"
)

func main() {
	start := time.Now()

	inFile := "in.pdf"
	outDir := "out"
	var wg sync.WaitGroup

	onTop := true
	for i := 0; i < 10; i++ {
		wg.Add(1)
		go addWaterMark(i, inFile, outDir, &wg, onTop)
	}
	wg.Wait()
	elapsed := time.Since(start)
	log.Printf("Sucessed and cost %s", elapsed)
}

func addWaterMark(i int, inFile string, outDir string, wg *sync.WaitGroup, onTop bool) {
	defer wg.Done()
	log.Println(fmt.Sprintf("File %d.pdf starting", i))
	wm, _ := pdfcpu.ParseImageWatermarkDetails("logo.jpg", "s:.5 a, rot:45", onTop)
	err := api.AddWatermarksFile(inFile, fmt.Sprintf("%s/out_%d.pdf", outDir, i), nil, wm, nil)
	if err != nil {
		log.Println(fmt.Sprintf("Error %s", err.Error()))
	} else {
		log.Println(fmt.Sprintf("File %d.pdf done", i))
	}
}
