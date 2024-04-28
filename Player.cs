using Game2d;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Game2d
{
    public class Player
    {
        //Player
        private Texture2D _player;
        private Vector2 _playerPosition;
        private float _playerSpeed = 300.0f;

        //Animação do player
        private Rectangle[] _playeridle;
        private int _index;
        private double _time;

        //Detector a área dee colisão do player
        public Rectangle Bounds => new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, 50, 50);

        public void LoadContent(ContentManager content)
        {
            //textura do player
            _player = content.Load<Texture2D>("asset/playerIdle");
        }
        public void Initialize()
        {
            //posição do player
            _playerPosition = new Vector2(45, 65);

            //Animação
            _playeridle = new Rectangle[]
            {
               new Rectangle(0, 0, 50, 50), new Rectangle(50, 0, 50, 50), new Rectangle(100, 0, 50, 50),
               new Rectangle(150, 0, 50, 50), new Rectangle(200, 0, 50, 50), new Rectangle(250, 0, 50, 50),
               new Rectangle(300, 0, 50, 50), new Rectangle(350, 0, 50, 50), new Rectangle(400, 0, 50, 50),
               new Rectangle(450, 0, 50, 50), new Rectangle(500, 0, 50, 50)
            };
            _index = 0;
            _time = 0.0;

        }
        public void Update(float deltaTime, GameTime gameTime)
        {

            Vector2 direction = Vector2.Zero;

            if (Input.GetKey(Keys.W))
            {
                direction.Y = -1.0f;
            }
            if (Input.GetKey(Keys.S))
            {
                direction.Y = 1.0f;
            }
            if (Input.GetKey(Keys.A))
            {
                direction.X = -1.0f;
            }
            if (Input.GetKey(Keys.D))
            {
                direction.X = 1.0f;
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 newPosition = _playerPosition + direction * _playerSpeed * deltaTime;

                // Verificar colisões com as bordas da janela
                if (newPosition.X < 0)
                {
                    newPosition.X = 0;
                }
                if (newPosition.X + Bounds.Width > 1000)
                {
                    newPosition.X = 1000 - Bounds.Width;
                }
                if (newPosition.Y < 0)
                {
                    newPosition.Y = 0;
                }
                if (newPosition.Y + Bounds.Height > 800)
                {
                    newPosition.Y = 800 - Bounds.Height;
                }
                _playerPosition = newPosition;
            }

            //Tempo da animação
            _time += gameTime.ElapsedGameTime.TotalSeconds;
            if (_time > 0.1)
            {
                _time = 0.0;
                _index++;
                if (_index > 10)
                {
                    _index = 0;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_player, _playerPosition, _playeridle[_index], Color.White);

        }

    }
}

