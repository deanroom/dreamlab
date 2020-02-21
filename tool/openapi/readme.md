#### Public Pre-built Docker images

 - https://hub.docker.com/r/swaggerapi/swagger-generator/ (official web service)
 - https://hub.docker.com/r/swaggerapi/swagger-codegen-cli/ (official CLI)


##### Swagger Generator Docker Image

The Swagger Generator image can act as a self-hosted web application and API for generating code. This container can be  incorporated into a CI pipeline, and requires at least two HTTP requests and some docker orchestration to access generated code.

Example usage (note this assumes `jq` is installed for command line processing of JSON):

```sh
# Start container and save the container id
CID=$(docker run -d swaggerapi/swagger-generator)
# allow for startup
sleep 5
# Get the IP of the running container
GEN_IP=$(docker inspect --format '{{.NetworkSettings.IPAddress}}'  $CID)
# Execute an HTTP request and store the download link
RESULT=$(curl -X POST --header 'Content-Type: application/json' --header 'Accept: application/json' -d '{
  "swaggerUrl": "http://petstore.swagger.io/v2/swagger.json"
}' 'http://localhost:8188/api/gen/clients/javascript' | jq '.link' | tr -d '"')
# Download the generated zip and redirect to a file
curl $RESULT > result.zip
# Shutdown the swagger generator image
docker stop $CID && docker rm $CID
```

In the example above, `result.zip` will contain the generated client.

##### Swagger Codegen CLI Docker Image

The Swagger Codegen image acts as a standalone executable. It can be used as an alternative to installing via homebrew, or for developers who are unable to install Java or upgrade the installed version.

To generate code with this image, you'll need to mount a local location as a volume.

Example:

```sh
docker run --rm -v ${PWD}:/local swaggerapi/swagger-codegen-cli generate \
    -i http://petstore.swagger.io/v2/swagger.json \
    -l go \
    -o /local/out/go
```

(On Windows replace `${PWD}` with `%CD%`)

The generated code will be located under `./out/go` in the current directory.