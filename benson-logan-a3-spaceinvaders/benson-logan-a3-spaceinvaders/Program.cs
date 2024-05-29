using Raylib_cs;
using System.Drawing;
using System.Numerics;

public class Program
{
    // If you need variables in the Program class (outside functions), you must mark them as static
    static string title = "Space Invaders"; // Window title
    static int screenWidth = 800; // Screen width
    static int screenHeight = 600; // Screen height
    static int targetFps = 60; // Target frames-per-second

    // Bullet properties
    static Vector2 bulletPosition;
    static Vector2 bulletSpeed = new Vector2(0, -5);
    static int bulletRadius = 5;
    static Raylib_cs.Color bulletColor = Raylib_cs.Color.Gold;
    static bool bulletActive = false;

    // Enemy properties
    static Raylib_cs.Rectangle[] enemyShips = new Raylib_cs.Rectangle[5];
    static bool[] enemyAlive = new bool[5];


    // Player ship properties
    static int shipX = 360;
    static int shipY = 400;
    static int shipSpeed = 5;

    // Score
    static int score = 0;

    static void Main()
    {
        // Create a window to draw to. The arguments define width and height 
        Raylib.InitWindow(screenWidth, screenHeight, title);
        // Set the target frames-per-second (FPS)
        Raylib.SetTargetFPS(targetFps);
        // Setup your game. This is a function YOU define.
        Setup();
        // Loop so long as window should not close
        while (!Raylib.WindowShouldClose())
        {
            // Enable drawing to the canvas (window)
            Raylib.BeginDrawing();
            // Clear the canvas with one color
            Raylib.ClearBackground(Raylib_cs.Color.Black);

            // Your game code here. This is a function YOU define.

            Update();
            Draw();

            // Stop drawing to the canvas, begin displaying the frame
            Raylib.EndDrawing();
        }
        // Close the window
        Raylib.CloseWindow();
    }

    static void Setup()
    {
        // Your one-time setup code here


        //enemy ships
        int startX = 150;
        int startY = 90;
        int enemyWidth = 60;
        int enemyHeight = 60;
        int padding = 20;

        for (int i = 0; i < enemyShips.Length; i++)
        {
            enemyShips[i] = new Raylib_cs.Rectangle(startX + i * (enemyWidth + padding), startY, enemyWidth, enemyHeight);
            enemyAlive[i] = true;
        }

    }
    static void Update()
    {

        // Your game code run each frame here


        // Ship controls
        if (Raylib.IsKeyDown(KeyboardKey.Left) && shipX > 0)
        {
            shipX -= shipSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right) && shipX < screenWidth - 80)
        {
            shipX += shipSpeed;
        }

        // Shooting bullets
        if (Raylib.IsKeyPressed(KeyboardKey.Space) && !bulletActive)
        {
            bulletPosition = new Vector2(shipX + 40, shipY);
            bulletActive = true;
        }

        // Update bullet position
        if (bulletActive)
        {
            bulletPosition += bulletSpeed;

            // Check if bullet is off screen
            if (bulletPosition.Y < 0)
            {
                bulletActive = false;
            }
        }

        // COLLISION
        for (int i = 0; i < enemyShips.Length; i++)
        {
            if (enemyAlive[i] && Raylib.CheckCollisionCircleRec(bulletPosition, bulletRadius, enemyShips[i]))
            {
                enemyAlive[i] = false;
                bulletActive = false;
                score += 100;
                break;
            }
        }
    }


    static void Draw()
    {
        // Player ship
        Raylib.DrawRectangle(shipX, shipY, 80, 20, Raylib_cs.Color.Green);


        // Draw bullet
        if (bulletActive)
        {
            Raylib.DrawCircleV(bulletPosition, bulletRadius, bulletColor);
        }

        // Draw enemy ships
        for (int i = 0; i < enemyShips.Length; i++)
        {
            if (enemyAlive[i])
            {
                Raylib.DrawRectangleRec(enemyShips[i], Raylib_cs.Color.Yellow);
            }
        }
        // Draw score
        Raylib.DrawText($"Score: {score}", 15, 15, 30, Raylib_cs.Color.White);
    }
}