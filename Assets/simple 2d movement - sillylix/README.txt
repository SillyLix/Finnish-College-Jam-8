PlayerMovement2D Script + CameraMovement - Version 1.2 (march 2025)

Created by: Sillylix (https://www.sillylix.com/)

Overview:
----------
This script gives your player character simple 2D movement and Camera Movement in Unity.
It also optionally supports:
 � Jumping (or even double jumping)
 � Dashing

Requirements:
----------
 � Rigidbody2D (Automatically added).
 � Collider2D of anytype.
 � Set a Ground Layer in the inspector for the jump feature (if jump is used).
 � Unity Engine 6000+ (Tested with version 6000.0.35f1)
 � Basic knowledge of Unity scripting and unity engine

How to Use:
----------

 PlayerMovement:
 ---------------
  1. Attach the Script:
    - Add the PlayerMovement2D script to your player object in Unity.
    - you can also use the ready made player that is stored in prefabs folder.

  2. Adjust Settings in the Unity Inspector:
    - **Movement:**
      � Enable horizontal and/or vertical movement.
      � Set the PlayerMovement speed to what feels good for your game.
    - **Jump Settings:**
      � Enable jump (or double jump) as needed.
      � Adjust the jump force, gravity, and the length of the ground detector.
      � Set the Ground Layer so the script knows what counts as ground.
      � Optionally, enable "draw ground detector" to see the raycast in the editor.
      � Choose the key (default is space)
    - **Dash Settings:**
      � Check the option to enable dash.
      � Set the dash speed, dash duration, and choose the key (default is Left Shift).

  3. Control Your Character:
    - Use the arrow keys or WASD to move.
    - Press Space to jump (if enabled).
    - Press Left Shift to dash (if enabled).

 Camera Movement:
 -----------------
  1. Attach the Script:
    - Add the CameraMovement script to your Camera object in Unity.
    - you can also use the ready made camera that is stored in prefabs folder.

  2. Adjust Settings in the Unity Inspector:
    - Add you player in the Target.
    - if there is a offset on camera add it on offset section.
    - cameraSpeed is the speed at which the camera will move at.

Configuration Details:
----------
 � jumpForce: Force applied when the player jumps.
 � jumpDetectorLength: Length of the raycast to check if the player is on the ground.
 � playerSpeed: Movement speed of the player.
 � dashSpeed: Speed during the dash.
 � dashDuration: How long the dash lasts.

Example Use Cases:
----------
 � Top-Down Game: Enable both horizontal and vertical movement (jump is off).
 � Platformer Game: Enable horizontal movement and jumping for a classic platformer game.

Notes:
----------
 � This script is designed to be simple and flexible; you can enable or disable features as needed.
 � The jump raycast is visible in the Unity Editor when debug mode ("draw jump detector") is enabled.

License:
----------
MIT License, Copyright (c) 2025 Sillylix
This software is provided "as is", without any warranty. You are free to use, modify, and distribute it under the terms of the MIT License.

AI Assistance:
----------
This description was helped by AI to improve clarity.
some of the code and comments was made with AI (github co-pilot)