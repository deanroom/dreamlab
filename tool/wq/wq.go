package main

import (
	"log"
	"os"

	"github.com/deanroom/dreamtool/tool/wq/dict"
	"github.com/urfave/cli"
)

func main() {
	app := &cli.App{
		Action: func(c *cli.Context) error {
			dict.QueryAndPlay(c.Args().Get(0))
			return nil
		},
	}

	err := app.Run(os.Args)
	if err != nil {
		log.Fatal(err)
	}
}
