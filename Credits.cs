using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game2d
{
    public class Credits
    {
        private Game1 _game;

        private SpriteFont _font;
        private string[] _creditsText; 
        private float _creditsTimer = 0f;
        private float _creditsDuration = 5f;

        public Credits(Game1 game)
        {
            _game = game;
        }
        public void Initialize()
        {
            _font = _game.Content.Load<SpriteFont>("asset/Fonte");

            // Definir o texto dos créditos
            _creditsText = new string[]
            {
            "Desenvolvedores do jogo: ",
            "Amanda L Santos Alves - 01535268",
            "Gabriella Lacerda Chaves Korinfsky - 01511663",
            "Joao Arthur Duarte de Faria - 01558846",
            "Jose Hugo Chaves Filho - 01512676",
            "Kaua Albuquerque Xavier de Farias - 01516413"
            };

        }
        public void Update(float TotalSeconds, GameTime gameTime)
        {
            _creditsTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_creditsTimer >= _creditsDuration)
            {
                // Voltar para o menu após os créditos
                _game.ShowMenu();
                _creditsTimer = 0f;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            float yPos = 400;
            foreach (string line in _creditsText)
            {
                spriteBatch.DrawString(_font, line, new Vector2(350, yPos), Color.White);
                // Ajuste o espaçamento entre as linhas
                yPos += _font.LineSpacing + 5; 
            }
        }
    }
}
