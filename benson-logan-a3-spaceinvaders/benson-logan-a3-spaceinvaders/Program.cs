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

    // Ball or "Bullet" variables
    static int radius;
    static Raylib_cs.Color color;
    static Vector2 position;
    static Vector2 velocity;
    static Vector2 gravity;


    // Player ship location and speed
    static int shipX = 360;
    static int shipY = 400;
    static int shipSpeed = 5;

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

            //Creating filler rectangle for ship
            DrawShip();
            DrawEnemyShips();
            DrawText();

            //Draw enemy ships



            Update();
            // Stop drawing to the canvas, begin displaying the frame
            Raylib.EndDrawing();
        }
        // Close the window
        Raylib.CloseWindow();
    }

    static void Setup()
    {
        // Your one-time setup code here
        radius = 25;
        position = new Vector2(400, radius);
        color = Raylib_cs.Color.Gold;
        gravity = new Vector2(0,+10);
        
    }

    static void Update() 
    {

        // Your game code run each frame here

        //Update Velocity
        velocity += gravity * Raylib.GetFrameTime();
        position += velocity;

        //Ball / laser / bullet
        Raylib.DrawCircleV(position, radius, color);
        

        // Ship controls
        if (Raylib.IsKeyDown(KeyboardKey.Left) && shipX > 0)
        {
            shipX -= shipSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right) && shipX < screenWidth - 80)
        {
            shipX += shipSpeed;
        }

    }

    static void DrawShip()
    {
        
        Raylib.DrawRectangleGradientH(shipX, shipY, 80, 170, Raylib_cs.Color.Green, Raylib_cs.Color.Blue);
    }

    static void DrawEnemyShips()
    {
        Raylib.DrawRectangleGradientH(550, 60, 60, 60, Raylib_cs.Color.Gold, Raylib_cs.Color.Red);
        Raylib.DrawRectangleGradientH(450, 60, 60, 60, Raylib_cs.Color.Gold, Raylib_cs.Color.Red);
        Raylib.DrawRectangleGradientH(350, 60, 60, 60, Raylib_cs.Color.Gold, Raylib_cs.Color.Red);
        Raylib.DrawRectangleGradientH(250, 60, 60, 60, Raylib_cs.Color.Gold, Raylib_cs.Color.Red);
        Raylib.DrawRectangleGradientH(150, 60, 60, 60, Raylib_cs.Color.Gold, Raylib_cs.Color.Red);
    }

    static void DrawText()
    {
        Raylib.DrawText("Score:", 15, 200, 30, Raylib_cs.Color.White);
        Raylib.DrawText("Time:", 15, 250, 30, Raylib_cs.Color.White);
    }
}

