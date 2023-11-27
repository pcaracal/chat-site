# Le Chat - A communication website

## Folder Structure

- `./backend/` - ASP.NET Core Backend
- `./frontend/` - Angular Frontend
- `./docs/` - Project Documentation

## Installation and Usage

Note:

- The project has only been tested on Linux. Support for other operating systems is not guaranteed.

### Prerequisites

- Docker
- Docker Compose
- Linux (Recommended)

### Installation

1. Clone the repository `git clone https://github.com/pcaracal/chat-site.git`
2. cd into the repository `cd chat-site`
3. Start the *docker service* if it is not already running `sudo systemctl start docker`
4. Run `docker-compose up -d` to start the containers
5. Depending on the speed of your machine, you may have to restart the backend container to ensure the database is
   ready. `docker-compose restart`
6. The website should now be available at `http://localhost:8000`

### Removal

1. To stop the containers, run `docker-compose down`
2. To remove the containers, run `docker-compose down --rmi all`
3. To remove the containers, volumes, and images, run `docker-compose down --rmi all -v --remove-orphans`