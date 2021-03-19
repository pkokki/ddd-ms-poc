### Install Envoy using Docker
````
docker pull envoyproxy/envoy-dev:8ff7787a84225b5859e2397dabbf4a586ed916c4
````

### Check your Envoy version
````
docker run --rm envoyproxy/envoy-dev:8ff7787a84225b5859e2397dabbf4a586ed916c4 --version
````

### View the Envoy command line options
````
docker run --rm envoyproxy/envoy-dev:8ff7787a84225b5859e2397dabbf4a586ed916c4 --help
````

### Run Envoy with the demo configuration

You can start the Envoy Docker image without specifying a configuration file, and it will use the demo config by default.

````
docker run --rm -it -p 9901:9901 -p 10000:10000 envoyproxy/envoy-dev:8ff7787a84225b5859e2397dabbf4a586ed916c4
````

To specify a custom configuration you can mount the config into the container, and specify the path with ```-c```.

Assuming you have a custom configuration in the current directory named ```envoy-custom.yaml```:

````
docker run --rm -it -v $(pwd)/envoy-custom.yaml:/envoy-custom.yaml -p 9901:9901 -p 10000:10000 envoyproxy/envoy-dev:8ff7787a84225b5859e2397dabbf4a586ed916c4 -c /envoy-custom.yaml
````

Check Envoy is proxying on http://localhost:10000.

The Envoy admin endpoint should also be available at http://localhost:9901.

You can exit the server with Ctrl-c.

See the [admin quick start guide](https://www.envoyproxy.io/docs/envoy/latest/start/quick-start/admin#start-quick-start-admin) for more information about the Envoy admin interface.