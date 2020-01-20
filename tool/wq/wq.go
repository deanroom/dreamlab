package main

import (
	"github.com/deanroom/dreamtool/tool/dict"
)

func main() {
	// app := &cli.App{
	// 	Action: func(c *cli.Context) error {
	// 		var keyWord = c.Args().Get(0)
	// 		dict.QueryAndPlay(keyWord)
	// 		return nil
	// 	},
	// }

	// err := app.Run(os.Args)
	// if err != nil {
	// 	log.Fatal(err)
	// }
	dict.QueryAndPlay("name")
}
