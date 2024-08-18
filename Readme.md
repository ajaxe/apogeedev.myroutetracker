# My Route Tracker

Simple application to tracker a user route by collecting GPS coordinates.

## Building docker image

Use the following command

```bash
docker build . -f build/Dockerfile --network=host --tag apogee-dev/my-route-tracker:local
```
