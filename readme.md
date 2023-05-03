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

## How to use

1. Create 2 tanks by posting on the `api/v1/tanks/add` endpoint. Here's an [example](Requests/AddTank.http) of how this request should be made. Note down the unique ids the server responds with. 

2. Create a map by posting on the `api/v1/maps/add` endpoint. Here's an [example](Requests/AddMap.http) of how this request should be made. Note down the 
resulting id. 

3. Simulate a battle between the 2 tanks by posting on the `api/v1/simulate` endpoint. Here's an [example](Requests/Simulate.http) of how this request should be made.

4. Getting on `api/v1/tanks/{tank_id}`, `api/v1/maps/{map_id}` and `api/v1/simulations/{simulate_id}` will fetch the created resources.

## Architecture

The TanksApi is implemented following the Clean architecture, splitting the project into multiple layers as follows:

1. `Domain`: provides the models of the entities in the problem domain (`Map`, `Tank`, `Simulation`) and all the interactions between them. 

    `Tank` objects are rather simple entities, they have `Health`, `Min/MaxAttack`, `Min/MaxDefense` and `Range`.

    `Map` objects are random maze-style 2D grids where 1 represents a wall and 0 represents a walkable cell.

    `Simulation` was implemented to follow 2 phases:
    - each tank seeks the enemy (assuming they have an infinite range Lidar - A* pathfinding)
    - when the enemy tank is in shooting range, start shooting

    The system was built with extensibility in mind, so more complicated [ITankAIs](Tanks.Domain/Simulation/TanksAIs/ITankAI.cs) could
    be implemented by adding more [ITankActions](Tanks.Domain/DomainModels/TankActions/ITankAction.cs) that alter the simulation state.
    (following the `Command` pattern)

    [ITankAIs](Tanks.Domain/Simulation/TanksAIs/ITankAI.cs) are currently selected for each tank at the beginning of the simulation using the 
    [InitialTankAIChooser](Tanks.Domain/Simulation/TanksAIs/InitialTankAIChooser.cs), but more complicated choosing strategies could be implemented
    and another possible improvement would be allowing to change the strategy after each simulation step. 

    Has no dependency on the other layers. 

2. `Application`: provides implementations for the required use cases (by calling into the `Domain` layer):
    - create/query a tank
    - create/query a map
    - simulate a battle
    - fetch a simulation result

    Implemnted according to CQRS principles, using [MediatR](https://github.com/jbogard/MediatR). (which is also useful for validation using [PipelineBehaviours](Tanks.Application/Validation/ValidationPipelineBehaviour.cs) and [FluentValidation](https://github.com/FluentValidation/FluentValidation))

    For data fetching purpuses, it relies on an [IRepository](Tanks.Application/Repositories/IRepository.cs) interface which is implemented in the
    `Infastructure` layer.

    This layer only depends the `Domain` layer. 

3. `Infrastructure`: is reponsible for implementing persistence, by allowing the `Application` layer to interact with a `MongoDB` database. 
    The point of having 2 separate layers is that the `Application` layer does not depend on a specific persistence solution, `EF Core` or others
    could be used just as well by simply implementing the [IRepository](Tanks.Application/Repositories/IRepository.cs) 
    and injecting the appropriate dependency.

4. `Api` (or `Presentation`): Is the layer with which the clients interact, simply routing `Http` requests to commands/queries in the 
    `Application` layer.