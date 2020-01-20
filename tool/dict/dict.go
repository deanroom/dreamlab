package dict

import (
	"fmt"
	"github.com/PuerkitoBio/goquery"
	"github.com/hajimehoshi/oto"
	"github.com/tosone/minimp3"
	"io"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"strings"
)



func QueryWords(keyword string) {
	// Request the HTML page.
	res, err := http.Get(fmt.Sprintf("http://youdao.com/w/eng/%s/#keyfrom=dict2.index", keyword))
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
		trans = strings.ReplaceAll(trans, "%", "")
		fmt.Printf("\n解释:%s", trans)
	})
}

func DownloadFile(url string, filepath string) error {
	// Create the file
	out, err := os.Create(filepath)
	if err != nil {
		return err
	}
	defer out.Close()

	// Get the data
	resp, err := http.Get(url)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	// Write the body to file
	_, err = io.Copy(out, resp.Body)
	if err != nil {
		return err
	}

	return nil
}

func play() {
	var file, _ = ioutil.ReadFile("/users/jerry/wordic/download.mp3")
	dec, data, _ := minimp3.DecodeFull(file)

	player, _ := oto.NewPlayer(dec.SampleRate, dec.Channels, 2, 1024)
	player.Write(data)
}
func QueryAndPlay(keyWord string) error {
	QueryWords(keyWord)
	if len(keyWord) > 0 {
		DownloadFile(fmt.Sprintf( "https://dict.youdao.com/dictvoice?audio=%s&type=2",keyWord),"/users/jerry/wordic/download.mp3")
	}
	play()
	return nil
}
