# PongUnity
Pong clone made in Unity

Game development has always interested me, and I've had some experience with Godot, Unity, Android, and writing some CLI games from scratch. I wanted to expand upon my knowledge of game development by taking on the "20 game challenge" (https://20_games_challenge.gitlab.io/), the first of which is Pong. I'm more familiar with Unity's 3D workspace, so the game was built in 3D but with an orthographic camera.

## Architecture

Being one of the first games, there's not too much here. That being said, I still tried to adhere to standard coding practices, with clear separations between each file and its intended purpose.

### Ball Physics

The most complex of the classes. This handles how the ball moves and interacts with the 3 types of possible collisions; players, walls, and nets. 

When colliding with a wall, the Z axis is flipped so the ball continues heading to the opposing player.

When colliding with a player's net, the ball is reset and a Unity Action is emitted which alerts the PongGame (sort of a game manager class) that a goal was scored.

Player collision is the most complex. When colliding with a player, the location of where the collision happened determines the angle at which the ball is sent in the other direction. I first normalize the collision location to a known value between 2.5 and -2.5 (representing the total length, 5 units, of the player). From here I calculate the angle at which I want to send the ball. For my version of Pong, I chose values between 45 and -45 degrees. This line converts the collision location to an angle I can use: 

`var angle = normalizedCollisionLocation * 18.0f;` 

This gives me an angle, but Unity's Mathf.Tan method requires radians so in the following line I do the conversion to create my new direction for the ball: 

`direction = new Vector3(-direction.x, 0,Mathf.Tan((float) angle * Mathf.Deg2Rad)).normalized;` 

All directions in the code are normalized to ensure the vector does not affect the speed of the ball at any point and I can control that manually via a separate parameter.  

### Player Movement

Using Unity's CharacterController class as well as InputManager, setting up 2 players with WASD and arrow keys was fairly simple. The only bit of complexity is ensuring the players cannot move outside the bounds of the game screen. In my case I identified that the bounds would be at Z = -12 to Z = 12. 

### Game Manager 

This class is responsible for handling the running of the game itself. It coordinates between the players, ball, and UI to ensure everything is synced. 

### UIManager

A simple class just as a wrapper for interacting with the UI text on screen and handling tasks such as showing the player's score and when someone wins. 

The only interesting thing here is the fact that the dividing line between the 2 halves of the field is rendered in camera rather than on-top like a UI. I wanted the ball to pass over top of this line and this was the easiest way to accomplish that. 

### Screenshot
![image](https://github.com/user-attachments/assets/e4928f34-5c8e-4d1a-9e47-47c79acad494)


