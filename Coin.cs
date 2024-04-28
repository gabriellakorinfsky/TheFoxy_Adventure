using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Game2d
{
    public class Coin
    {
        //Moeda
        private Texture2D _coin;
        private Vector2 _position;

        //Coleta de moeda
        public bool _isCollected { get; set; }

        //Lista das posições das moedas
        private List<Coin> _coins = new List<Coin>();

        //Animação
        private Rectangle[] _coinAnimation;
        private int _index;
        private double _time;

        //Detector da área de colisão da moeda
        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, 32, 32);
       
        public Coin(Texture2D coin, Vector2 position)
        {
            _coin = coin;
            _position = position;
            _isCollected = false;
        }
        
        public bool CheckCollision(Rectangle playerBounds)
        {
            return Bounds.Intersects(playerBounds);
        }

        public void Initialize()
        {
            //Animação
            _coinAnimation = new Rectangle[]
            {
               new Rectangle(0, 0, 32, 32), new Rectangle(32, 0, 32, 32), new Rectangle(64, 0, 32, 32),
               new Rectangle(96, 0, 32, 32), new Rectangle(128, 0, 32, 32), new Rectangle(160, 0, 32, 32),
               new Rectangle(192, 0, 32, 32)
            };
            _index = 0;
            _time = 0.0;
        }

        public void Update(float totalSeconds, GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;
            if (_time > 0.1)
            {
                _time = 0.0;
                _index++;
                if (_index > 6)
                {
                    _index = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_coin, _position, _coinAnimation[_index], Color.White);
        }

    }
}
