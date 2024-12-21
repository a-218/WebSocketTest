# WebSocketTest

This project is a WebSocket server that I built by following a YouTube tutorial 
(https://www.youtube.com/watch?v=G5b1Zd367sA&t=130s&ab_channel=Alex%27sDevDen%F0%9F%91%A8%E2%80%8D%F0%9F%92%BB).
The goal of the project was to learn how to create a WebSocket server in C#, test it using Postman, and automate testing with GitHub Actions.

## Description

- **WebSocket Server**: A simple WebSocket server created in C# using a WebSocket library (like Fleck).
- **Postman Testing**: Used Postman to test WebSocket connections and messages.
- **GitHub Actions**: Set up CI/CD with GitHub Actions to run tests automatically on every push.

## Technical Requirements
- .NET Core SDK
- Git
- Postman (for testing)

## Installation

1. Clone the repository:
```bash
git clone https://github.com/a-218/WebSocketTest.git
cd WebSocketTest
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Start the development server:
```bash
cd api
dotnet watch
```

## WebSocket Event Protocol

### Event Structure
All events follow a standard JSON format with an `eventType` field that determines how the message is processed.

### Connection Flow
The WebSocket server requires a specific sequence of events to establish a proper connection and enable messaging:

1. Sign in (required first)
2. Enter a room
3. Send messages to the room

### Supported Events

#### 1. Client Sign In (First Step)
First, authenticate the user and establish their session.

**Payload:**
```json
{
    "eventType": "ClientSignIn",
    "Username": "bob"
}
```

**Fields:**
- `eventType`: Must be "ClientSignIn"
- `Username`: String containing the user's username

#### 2. Client Enter Room (Second Step)
After signing in, users can join a specific room.

**Payload:**
```json
{
    "eventType": "ClientEnterRoom",
    "roomId": 1
}
```

**Fields:**
- `eventType`: Must be "ClientEnterRoom"
- `roomId`: Integer identifying the room to join

#### 3. Client Broadcast Room (Final Step)
Once in a room, users can broadcast messages to all clients in that room.

**Payload:**
```json
{
    "eventType": "ClientBroadcastRoom",
    "roomId": 1,
    "messageContent": "Hello everyone!"
}
```

**Fields:**
- `eventType`: Must be "ClientBroadcastRoom"
- `roomId`: Integer identifying the target room
- `messageContent`: String containing the message to broadcast

## Testing with Postman

1. Open Postman and create a new WebSocket request
2. Set the WebSocket URL (e.g., `ws://localhost:5000`), default `ws://0.0.0.0:8181`
3. Follow this sequence for testing:

### Test Sequence

1. **Step 1: User Authentication**
   ```json
   {
       "eventType": "ClientSignIn",
       "Username": "bob"
   }
   ```

2. **Step 2: Room Entry**
   ```json
   {
       "eventType": "ClientEnterRoom",
       "roomId": 1
   }
   ```


3. **Step 3: Message Broadcasting**
   ```json
   {
       "eventType": "ClientBroadcastRoom",
       "roomId": 1,
       "messageContent": "Hello everyone!"
   }
   ```


## CI/CD with GitHub Actions

The project includes automated testing through GitHub Actions. On every push:

1. Code is built and tested
2. WebSocket server integration tests are run
3. Test results are reported in the GitHub Actions interface

