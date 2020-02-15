package main

import (
	"github.com/pdfcpu/pdfcpu/pkg/api"
	"github.com/pdfcpu/pdfcpu/pkg/pdfcpu"
)

func main() {
	inFile := "in2.pdf"
	outFire := "compressed.tracemonkey-pldi-09.pdf"
	conf := pdfcpu.NewAESConfiguration("upw", "opw", 256)
	api.EncryptFile(inFile, outFire, conf)
}
