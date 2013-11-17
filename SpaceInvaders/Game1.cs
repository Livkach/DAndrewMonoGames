//#define AtHome
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using System.Text;
#endregion

namespace SpaceInvaders
{
    public class GameObjectBase
    {
        public Texture2D Picture { get; set; }
        public Rectangle Box;
        public float Speed { get; set; }
        public uint MaxHealth { get; set; }
        public uint CurrentHealth { get; set; }
        public double Angle { get; set; }

        public GameObjectBase(Texture2D picture, uint MaxHealth, Rectangle box)
        {
            Picture = picture;
            MaxHealth = 1000;
            Box = box;
            //  сохраните в соответстующие поля класса эти переменные
            // поставьте значение CurrentHealth максимальным
        }
    }
   
   
    public class Asteroid : GameObjectBase 
    {
        public Asteroid(Texture2D picture, uint MaxHealth, Rectangle box):base(picture,MaxHealth,box)
        {
         
        }
         
        
        // создайте конструктор и прокиньте все параметры базовому конструктору
    }
     public class Ship : GameObjectBase
    {
            public string Name {get;set;}
            public Ship(string name, Texture2D picture, uint MaxHealth, Rectangle box)
                : base(picture, MaxHealth, box)
           {
              Name = name;
            }
    }
    public class Laser : GameObjectBase
    {
        public uint damage {get;set;}
        public Laser(uint damage,Texture2D picture, uint MaxHealth, Rectangle box):base(picture,MaxHealth,box)
        {
            damage = 25;
        }
    }        

/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        Texture2D AsteroidPic;
        Texture2D GameBackground;
        Texture2D LaserPic;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ship player;
        List<Laser> lasers;
        List<Asteroid> asteroids;


        private  Texture2D ShipPic;
        Random r = new Random();

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
             AsteroidPic = Content.Load<Texture2D>("asteroid.png");
           // TODO: use this.Content to load your game content here
            ShipPic = Content.Load<Texture2D>("ship.png");
            LaserPic = Content.Load<Texture2D>("laser.png");
            GameBackground = Content.Load<Texture2D>("space.png");
            player = new Ship("player", ShipPic, 100, ShipPic.Bounds);
            lasers = new List<Laser>();
            asteroids = new List<Asteroid>();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void CreateLasers()
        {
            if (r.Next(50)==1)
            {
                var rect_laser = new Rectangle (r.Next(0,800), -50, LaserPic.)
            }
        }
        void  CreateAstetroids()
        {
             
             if (r.Next(50) == 1)
             {
                 var rect_asteroid = new Rectangle(r.Next(0, 800), -50, AsteroidPic.Bounds.Width, AsteroidPic.Bounds.Height); Rectangle rect = AsteroidPic.Bounds;
                   var ast = new Asteroid(AsteroidPic,100,rect_asteroid); asteroids.Add(ast); }
        }

        void  MoveLasers()
        {
            for (int y = 0; y < lasers.Count; y++)
            {
                Laser laser = lasers[y];

                if (laser.Box.Y > 1000)
                {
                    lasers.RemoveAt(y);
                    y--;
                }

            }
            //движение лазеров
        }
        void MoveAsteroids()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                Asteroid asteroid = asteroids[i];
                
                //asteroid.Box.X += r.Next(0,5);
                
                //asteroid.Box.X -= r.Next(0,5);
                asteroid.Box.Y+=1;

                if (asteroid.Box.Y > 1000)
                {
                    asteroids.RemoveAt(i);
                    i--;
                }

                if (asteroid.Box.Intersects(player.Box))
                {
                    player.MaxHealth -= 10;
                }

                for (int j = 0; j < lasers.Count; j++) 
                { Laser laser = lasers[j]; } 
            }
            
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CreateAstetroids();
            MoveLasers();
            MoveAsteroids();
           
          
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
                laser.Box.Y -= 1;
            if (state.IsKeyDown(Keys.Left))
                player.Box.X -= 1;
            if (state.IsKeyDown(Keys.Right))
                player.Box.X += 1;
            if (state.IsKeyDown(Keys.Up))
                player.Box.Y -= 1;
            if (state.IsKeyDown(Keys.Down))
                player.Box.Y += 1;
            // TODO: Add your update logic here

            //

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(GameBackground,  graphics.GraphicsDevice.PresentationParameters.Bounds, Color.White);
            spriteBatch.Draw(player.Picture, player.Box, Color.White);
            foreach (var asteroid in asteroids)
            {
                spriteBatch.Draw(asteroid.Picture,  asteroid.Box, Color.White);
            }
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}