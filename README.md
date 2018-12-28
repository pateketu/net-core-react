## AddItUp
Live Url: http://additup.azurewebsites.net
The objective of this exercise is to create a simple website that offers games to train your brain.
The 1st game to implement is AddItUp - a simple addition online game.

#### Business requirements
----------------------------
1. When user connects to the web application, he is presented with a simple page with a question of type a + b = ?
2. User must submit answer within given timeframe (defined by server). User can submit one and only one answer for a given equation.
3. Timer should be displayed on screen for user to know how much time is left.
4. If answer is correct, next question is returned.
5. If user provides 3 good answers in a row he is promoted to the next rank and time allowance for each question is reduced by 1 second.
   Example of ranks: "Beginner", "Talented", "Intermediate", "Advanced", "Expert"
6. If answer is incorrect or time runs out game ends and user is offered the option to start again.
7. 2 users should not see the same equations at the same time.

#### Technical requirements
-----------------------------
- WebApi .net core project
- React app

Exercise creation and score management should be done on the server.

### High Level design

-- All of the Game logic (checking of time, checking of answer, moving levels etc) is in the back-end
-- Each gaming session is identified by Id (GUID), Calling GET API will start the game and create a new Guid
-- There is an InMemoery DB which holds the data
-- Answers are send to POST API, which returns with a Response (failed, time elapsed and the next exercise in case of valid answer)
-- Allowed time for a level is provided by server (starts with 20 sec for first level) but actual timer showing the countdown is in the React App, there will be some latency between client and server communication when user submits the answer hence user could lose few milliseconds theoretically.