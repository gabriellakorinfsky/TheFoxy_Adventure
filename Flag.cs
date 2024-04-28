using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2d
{
    public class Flag
    {
        //bandeira
        private Texture2D _flag;
        private Vector2 _flagPosition;

        //animação
        private Rectangle[] _flagAnimation;
        private int _index;
        private double _time;

        //Detector da área de colisão da bandeira
        public Rectangle flagBounds => new Rectangle((int)_flagPosition.X, (int)_flagPosition.Y, 48, 48);

        public void LoadContent(ContentManager content)
        {
            //Textura da bandeira
            _flag = content.Load<Texture2D>("asset/bandeira");
        }
        public void Initialize()
        {

            _index = 0;
            _time = 0.0;

            //Bandeira Animação
            _flagAnimation = new Rectangle[]
            {
               new Rectangle(0, 0, 48, 48), new Rectangle(48, 0, 48, 48), new Rectangle(96, 0, 48, 48),
               new Rectangle(144, 0, 48, 48), new Rectangle(192, 0, 48, 48), new Rectangle(240, 0, 48, 48),
               new Rectangle(288, 0, 48, 48)
            };
            //Posição da bandeira
            _flagPosition = new Vector2(867, 636);

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
            spriteBatch.Draw(_flag, _flagPosition, _flagAnimation[_index], Color.White);
        }
    }
}
