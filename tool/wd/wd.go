package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"strings"

	"github.com/PuerkitoBio/goquery"
	"github.com/urfave/cli/v2"
)

func main() {
	app := &cli.App{
		Action: func(c *cli.Context) error {
      //fmt.Printf("Hello %q", c.Args().Get(0))
	    QueryWords(c.Args().Get(0))
			return nil
		},
	}

	err := app.Run(os.Args)
	if err != nil {
		log.Fatal(err)
	}
}
func QueryWords(keyword string) {
	// Request the HTML page.
	res, err := http.Get(fmt.Sprintf("http://youdao.com/w/eng/%s/#keyfrom=dict2.index",keyword))
	if err != nil {
		log.Fatal(err)
	}
	defer res.Body.Close()
	if res.StatusCode != 200 {
		log.Fatalf("status code error: %d %s", res.StatusCode, res.Status)
	}

	// Load the HTML document
	doc, err := goquery.NewDocumentFromReader(res.Body)
	if err != nil {
		log.Fatal(err)
	}

	// Find the review items
	doc.Find("#phrsListTab").Each(func(i int, s *goquery.Selection) {
		keyword := s.Find(".keyword").Text()
		fmt.Printf("单词:『%s』", keyword)

		pronounce := s.Find(".baav").Text()
		pronounce = strings.ReplaceAll(pronounce, " ", "")
		pronounce = strings.ReplaceAll(pronounce, "\n\n", "    ")
		pronounce = strings.ReplaceAll(pronounce, "\n", "")
		fmt.Printf("\n发音:%s", pronounce)

		trans := s.Find(".trans-container").Text()
		trans = strings.ReplaceAll(trans, " ", "")
		trans = strings.ReplaceAll(trans, "\n\n\n", "")
    trans = strings.ReplaceAll(trans, "\n\n", "")
    //trans = strings.ReplaceAll(trans, "%", "")
		fmt.Printf("\n解释:%s", trans)
	})
}
