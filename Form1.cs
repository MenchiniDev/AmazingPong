namespace VirtualPong
{
    public partial class Form1 : Form
    {
        static int Regolator=0; //needed to slow down the process
        private bool AutomaticPilot = false; //necessario per la seconda implementazione con intelligenza
        private List<Circle> SXbar = new List<Circle>();
        private List<Circle> DXbar = new List<Circle>();
        bool onlystart=false; //da porre a false per iniziare con l'"""animazione"""


        private Circle Ball = new Circle(0,0); 
        public Form1()
        {
            
            InitializeComponent();

            new VirtualPong.Settings(); // linking the Settings Class to this Form

            Timer1.Interval = 70;
            Timer1.Tick += updateSreen; // linking a updateScreen function to the timer
            Timer1.Start(); // starting the timer

            startGame();

        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            // the key down event will trigger the change state from the Input class
            Input.changeState(e.KeyCode, true);

            if (Input.KeyPress(Keys.A))
                AutomaticPilot = true;

            if (Input.KeyPress(Keys.W))
            {
                Input.changeState(Keys.S, false);
                AutomaticPilot = false;
            }
            else if (Input.KeyPress(Keys.S))
            {
                Input.changeState(Keys.W, false);
                AutomaticPilot = false;
            }

            /*lato player DX*/
            if (Input.KeyPress(Keys.Up))
            {
                Input.changeState(Keys.Down, false);
                
            }
            else if (Input.KeyPress(Keys.Down))
            {

                Input.changeState(Keys.Up, false);
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false); 
        }

        private void startGame()
        {
            // this is the start game function
            new Settings(); // create a new instance of settings
            
            Put(); // load the bar sizes
            generateBall();
            SXbar[0].Score--;

        }

        private void Put() 
        {
            
            for (int i = 0; i < 5; i++)
            {

                Circle Plus = new Circle
                {
                    X = 3,
                    Y = i + 5
                 };
                SXbar.Add(Plus);
                Circle PlusDx = new Circle
                {
                     X = 53,
                     Y = i + 7
                 };
                DXbar.Add(PlusDx);

            }
            

        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            // this is where we will see the snake and its parts moving

            Graphics canvas = e.Graphics; // create a new graphics class called canvas

            if (Settings.GameOver == false)
            {
                Brush Ballcolor; // create a new brush called snake colour

                Ballcolor = Brushes.White;

              

                Brush barColourSx; // create a new brush 
                Brush barColourDx; // create a new brush

                barColourSx = Brushes.Purple;
                barColourDx = Brushes.Yellow;

                for (int i = 0; i < 5; i++)
                {

                    //draw SX barrier
                    canvas.FillRectangle(barColourSx,
                                        new Rectangle(
                                            (SXbar[i].X) * Settings.Width,
                                            (SXbar[i].Y) * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));



                    //draw DX barrier
                    canvas.FillRectangle(barColourDx,
                                        new Rectangle(
                                            (DXbar[i].X ) * Settings.Width,
                                            (DXbar[i].Y) * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));
                }
                if (!onlystart)
                {
                    System.Media.SoundPlayer splayer = new System.Media.SoundPlayer(@"C:\Users\loren\Desktop\FMVe\PongCs\Intro.wav"); //aggiungi suono 8-bit
                    splayer.Play();
                    System.Threading.Thread.Sleep(4000);
                    onlystart = true;
                }

                canvas.FillEllipse(Ballcolor,
                                        new Rectangle(
                                            (Ball.X + 25) * Settings.Width,
                                            (Ball.Y) * Settings.Height,
                                            Settings.Width - 5, Settings.Height - 5
                                            ));
            }
            else
            {
                string gameOver = "Game Over \n";
                System.Media.SoundPlayer Outro = new System.Media.SoundPlayer(@"C:\Users\loren\Desktop\FMVe\PongCs\Outro.wav"); //aggiungi suono 8-bit
                Outro.Play();
                MessageBox.Show("the game is over: go touch some grass bruh");

            }
        }
        
        private void generateBall()
        {
            Random rndY = new Random();
            Random rndX = new Random();

            if (rndX.Next(1,10)%2 == 0)
                Ball.lastmodXinc= true;
                    else
                Ball.lastmodXdec = true;
            if (rndY.Next(1,10) % 2 == 0)
                Ball.lastmodYinc = true;
            else
                Ball.lastmodYdec = true;
        }

        private void modSXDX()
        {
            /*funzionamento:*/

            /*la funzione agisce calcolando il triangolo formato da PnewposX+X e PnewposY+Y, 
            trovandone l'angolo e cosi risalendo alla posizione Y dove deve sistemarsi SX */

            /*lo schema predittivo può esser allargato calcolando il triangolo che genera nel caso in cui urti un lato orizzontale*/

            double prod = (Ball.X) * (Ball.X) + (Ball.Y) * (Ball.Y);

            double ipo = Math.Sqrt(prod); //trovo l'ipotenusa
            double cos = (Ball.X) / ipo;

            double squad = 1 - (cos * cos);
            double sin = (Math.Sqrt(squad));


            if (Ball.X < 3)
            {
                if (((sin) * ipo) != SXbar[0].Y + 1 || ((sin) * ipo) != SXbar[0].Y - 1)
                {
                    if (((sin) * ipo) > SXbar[0].Y)
                    {
                        ++SXbar[0].Y;
                        ++SXbar[1].Y;
                        ++SXbar[2].Y;
                        ++SXbar[3].Y;
                        ++SXbar[4].Y;
                    }
                    else
                    {
                        --SXbar[0].Y;
                        --SXbar[1].Y;
                        --SXbar[2].Y;
                        --SXbar[3].Y;
                        --SXbar[4].Y;
                    }

                    if (((sin) * ipo) > SXbar[0].Y)
                    {
                        ++SXbar[0].Y;
                        ++SXbar[1].Y;
                        ++SXbar[2].Y;
                        ++SXbar[3].Y;
                        ++SXbar[4].Y;
                    }
                    else
                    {
                        --SXbar[0].Y;
                        --SXbar[1].Y;
                        --SXbar[2].Y;
                        --SXbar[3].Y;
                        --SXbar[4].Y;
                    }
                }
            }

        }

        private void movePlayer()
        {
            if (AutomaticPilot)
                modSXDX();
                for (int i = 0; i < 5; i++)
                {
                    if (Settings.direction == Directions.Up)
                    {
                        DXbar[i].Y--;
                    }
                    else if (Settings.direction == Directions.Down)
                        DXbar[i].Y++;

                    if (Settings.direction == Directions.W)
                    {
                        SXbar[i].Y--;
                    }
                    else if (Settings.direction == Directions.S)
                        SXbar[i].Y++;
                }
            for (int i = 0; i < 5; i++)
            {
                if (Regolator == 1)//regolatore di velocità della pallina
                {
                        Regolator = 0;
                        if (Ball.X < -23)
                        {
                            Ball.X++;
                            Ball.lastmodXinc = true;
                            Ball.lastmodXdec = false;
                            SXbar[0].Score++;
                            if (SXbar[0].Score == 10)
                                Settings.GameOver = true; 
                            textBox2.Text = (' ' + Convert.ToString(SXbar[0].Score));
                        }
                        if (Ball.X == -22)
                        {
                            if ((Ball.Y == SXbar[0].Y || Ball.Y == SXbar[1].Y || Ball.Y == SXbar[2].Y || Ball.Y == SXbar[3].Y || Ball.Y == SXbar[4].Y))
                            {
                                Ball.lastmodXinc = true;
                                Ball.lastmodXdec = false;

                            }

                        } //rimbalzo su barriera di SX
                        if (Ball.X > 29)
                        {
                            Ball.X--;
                            Ball.lastmodXdec = true;
                            Ball.lastmodXinc = false;
                            DXbar[0].Score++;
                            if (DXbar[0].Score == 10)
                                Settings.GameOver = true;
                            textBox1.Text = (' ' + Convert.ToString(DXbar[0].Score));
                        }
                        if (Ball.X == 27)
                        {
                            if ((Ball.Y == DXbar[0].Y || Ball.Y == DXbar[1].Y || Ball.Y == DXbar[2].Y || Ball.Y == DXbar[3].Y || Ball.Y == DXbar[4].Y))
                            {
                                Ball.lastmodXinc = false;
                                Ball.lastmodXdec = true;

                            }
                        } //rimbalzo su barriera di DX
                        if (Ball.Y > 32)
                        {
                            Ball.lastmodYdec = true;
                            Ball.lastmodYinc = false;
                        }


                        if (Ball.Y < 3)
                        {
                            Ball.lastmodYdec = false;
                            Ball.lastmodYinc = true;
                        }

                        //if necessari per poter costantemente muovere la pallina in una direzione e per pilotarla in caso di rimbalzo
                        if (Ball.lastmodYinc)
                        {
                            Ball.Y++;
                        }
                        if (Ball.lastmodYdec)
                        {
                            Ball.Y--;
                        }
                        if (Ball.lastmodXinc)
                        {
                            Ball.X++;
                        }
                        if (Ball.lastmodXdec)
                        {
                            Ball.X--;
                        }
                }
                    //per evitare di sforare i margini

                    if (SXbar[0].Y < 3) //margine alto
                    {
                        SXbar[0].Y++;
                        SXbar[1].Y++;
                        SXbar[2].Y++;
                        SXbar[3].Y++;
                        SXbar[4].Y++;
                    }
                    if (SXbar[i].Y > 33)   //margine basso
                    {
                        SXbar[0].Y--;
                        SXbar[1].Y--;
                        SXbar[2].Y--;
                        SXbar[3].Y--;
                        SXbar[4].Y--;
                    }


                    if (DXbar[i].Y < 3) //margine alto
                    {
                        DXbar[0].Y++;
                        DXbar[1].Y++;
                        DXbar[2].Y++;
                        DXbar[3].Y++;
                        DXbar[4].Y++;
                    }
                    if (DXbar[i].Y > 33)   //margine basso
                    {
                        DXbar[0].Y--;
                        DXbar[1].Y--;
                        DXbar[2].Y--;
                        DXbar[3].Y--;
                        DXbar[4].Y--;
                    }
            }
    }

        private void updateSreen(object sender, EventArgs e)
        {

            if (Settings.GameOver == true)
            {

                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
               
                //if the game is not over then the following commands will be executed

                // below the actions will probe the keys being presse by the player
                // and move the accordingly

                if (Input.KeyPress(Keys.Up))
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down))
                {
                    Settings.direction = Directions.Down;
                }else if (Input.KeyPress(Keys.W))
                {
                    Settings.direction = Directions.W;
                }
                else if (Input.KeyPress(Keys.S))
                {
                    Settings.direction = Directions.S;
                }
                Regolator++;
         
                movePlayer();
                pbCanvas.Invalidate(); // refresh the picture box and update the graphics on it
            }

            

        }
    }
}