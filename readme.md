# TanksApi

## How to Run

To run the TanksApi project, please follow the steps below:

1. Build the Docker image by running the following command in the project's root directory:

```bash
docker build -t tanks-api .
```


2. After the Docker image is built, start the containers using Docker Compose:

```bash
docker compose up -d
```


3. Once the containers are up and running, you need to find the name of the network spawned by Docker Compose. In most cases, it will be named `tanks_mongo-network`. Use the following command to check the network names:

```bash
docker network ls
```

4. After finding the name of the network, you need to determine the IP address of the MongoDB server running on this network. Run the following command, replacing `tanks_mongo-network` with the actual network name you found in the previous step:

```bash
docker network inspect tanks_mongo-network
```

5. Finally, to start the TanksApi container and connect it to the MongoDB server, run the following command, replacing `mongodb://172.19.0.2:27017` with the appropriate connection string for your MongoDB server:

```bash
docker run -p 5000:80 --network=tanks_mongo-network -e MongoDbSettings__ConnectionString="mongodb://172.19.0.2:27017" -e ASPNETCORE_ENVIRONMENT=Development tanks-api
```

The TanksApi server will now be running on http://localhost:5000. To access Swagger and explore the API documentation, open your web browser and go to http://localhost:5000/swagger.