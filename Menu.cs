using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;

namespace Game2d
{
    public class Menu
    {
        //Background
        private Texture2D _background;
        //Botão de play
        private Texture2D _playButtonWhite;
        private Texture2D _playButtonRed;
        private Vector2 _playButtonPosition;
        bool isMouseOverPlayButton = false;
        //Botão de exit
        private Texture2D _exitButtonWhite;
        private Texture2D _exitButtonRed;
        private Vector2 _exitButtonPosition;
        bool isMouseOverExitButton = false;

        //Detector da área de colisão dos botões
        public Rectangle playButtonBounds => new Rectangle((int)_playButtonPosition.X, (int)_playButtonPosition.Y, _playButtonWhite.Width, _playButtonWhite.Height);
        public Rectangle exitButtonBounds => new Rectangle((int)_exitButtonPosition.X, (int)_exitButtonPosition.Y, _exitButtonWhite.Width, _exitButtonWhite.Height);
        public void Initialize()
        {
            //Centralizar os botões
            _playButtonPosition = new Vector2(1000 / 2.0f - _playButtonWhite.Width / 2.0f, 400);
            _exitButtonPosition = new Vector2(1000 / 2.0f - _exitButtonWhite.Width / 2.0f, 550);
        }
        public void LoadContent(ContentManager content)
        {
            //Background
            _background = content.Load<Texture2D>("asset/background");
            //Botões
            _playButtonWhite = content.Load<Texture2D>("asset/Playwhite");
            _playButtonRed = content.Load<Texture2D>("asset/Playred");
            _exitButtonWhite = content.Load<Texture2D>("asset/Exitwhite");
            _exitButtonRed = content.Load<Texture2D>("asset/Exitred");
        }
  
        public void Update(float totalSeconds, GameTime gameTime)
        {  
            MouseState mouseState = Mouse.GetState();

            //Se o mouse tiver em cima do botão altera a cor
            if (playButtonBounds.Contains(mouseState.Position))
            {
                isMouseOverPlayButton = true;
            }
            else
            {
                isMouseOverPlayButton = false;
            }
            if (exitButtonBounds.Contains(mouseState.Position))
            {
                isMouseOverExitButton = true;
            }
            else
            {
                isMouseOverExitButton = false;
            } 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Background do menu
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            //Se true muda a cor
            if (isMouseOverPlayButton)
            {
                spriteBatch.Draw(_playButtonRed, _playButtonPosition, Color.White);
            }
            else
            {
                spriteBatch.Draw(_playButtonWhite, _playButtonPosition, Color.White);
            }
            if (isMouseOverExitButton)
            {
                spriteBatch.Draw(_exitButtonRed, _exitButtonPosition, Color.White);
            }
            else
            {
                spriteBatch.Draw(_exitButtonWhite, _exitButtonPosition, Color.White);
            }
        }
        
    }
}
