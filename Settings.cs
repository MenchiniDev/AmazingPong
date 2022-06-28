using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace VirtualPong
{
    public enum Directions
    {
        // this is a enum class called Directions
        // we are using enum because its easier to classify the directions
        // for this game

        Up,
        Down,
        W,
        S
         
    };

    class Settings
    {
        public static int Width { get; set; } // set the width as int class
        public static int Height { get; set; } // set the height as int class
        public static int Speed { get; set; } // set the speed as int class
        public static int Score { get; set; } // set the score as int class
        public static int Points { get; set; } // set the points as int class
        public static bool GameOver { get; set; } // set the game over as Boolean class
        public static Directions direction { get; set; } // set the direction as the class we mentioned above

        public Settings()
        {
            // this is the default settings function
            Width = 16; // set the width to 16
            Height = 16; // set the height to 16
            Speed = 20; // set the speed to 20
            
            Points = 100; // set points to 100
            GameOver = false; // set game over to false
        }
    }
}
class Circle
{
    
    private int v1;
    private int v2;

    public int X { get; set; } // this is a public int class called X
    public int Y { get; set; } // this is a public int class called Y
    public int Score { get; set; }

    public Brush[] CircleColor = { Brushes.Gainsboro, Brushes.Maroon, Brushes.Magenta, Brushes.Silver, Brushes.Orange };
    public Brush CirclePaint;

    public bool lastmodXinc; 
    public bool lastmodYinc; 
    public bool lastmodXdec;
    public bool lastmodYdec; 

    public Circle()
    {
        // this function is resetting the X and Y to 0
        Score = 0; // set the score to 0
        X = 0;
        Y = 0;
        CirclePaint = CircleColor[0];
        lastmodXinc = false;
        lastmodXdec = false;
        lastmodYinc = false;
        lastmodYdec = false; 
        
    }

    public Circle(int v1, int v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }
}
