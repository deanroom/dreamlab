package main

import (
	"github.com/gin-gonic/gin"
	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"

	"github.com/deanroom/lab/go/gin/webdemo/pkg/api"

	_ "github.com/deanroom/lab/go/gin/webdemo/docs"
)

// @title DreamWork Swagger Demo API
// @version 1.0

// @contact.name API Support
// @contact.url http://www.swagger.io/support
// @contact.email support@swagger.io

// @host localhost:8080
// @BasePath /v1
func main() {
	r := gin.New()

	r.GET("/v1/api/get-string-by-int/:some_id", api.GetStringByInt)
	r.GET("/v1/api/get-struct-array-by-string/:some_id", api.GetStructArrayByString)

	url := ginSwagger.URL("http://localhost:8080/swagger/doc.json") // The url pointing to API definition
	r.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler, url))

	r.Run()
}
