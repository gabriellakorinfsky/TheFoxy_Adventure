using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;


namespace Game2d
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Classes
        private Menu _menu;
        private Player _player;
        private Flag _flag;
        private Credits _credits;

        //Moedas
        private Texture2D _coinTexture;
        //Lista de Moedas
        private List<Coin> _coins;
        //Coletar Moedas
        private int _coinsCollected;   

        //Bandeira
        private bool _interactFlag;

        //Fontes
        private SpriteFont _font;
        private SpriteFont _fontWin;

        //Fundo Credits
        private Texture2D _backCredits;
        //Fundo do jogo
        private Texture2D _backgroundGame;

        //GameStart e GameOver
        private bool gameStarted = false;
        private bool _gameOver;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            //Tamanho da tela
            _graphics.PreferredBackBufferWidth = 1000; 
            _graphics.PreferredBackBufferHeight = 800; 
            _graphics.ApplyChanges();

            //Contador da coleta de moedas
            _coinsCollected = 0;

            //Creditos
            _credits = new Credits(this);
            _credits.Initialize();
        }   

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Menu
            _menu = new Menu();
            _menu.LoadContent(Content);
            _menu.Initialize();

            //GAME
            //Backgound do jogo
            _backgroundGame = Content.Load<Texture2D>("asset/floor");

            //Fonte
            _font = Content.Load<SpriteFont>("asset/fonte");
            _fontWin = Content.Load<SpriteFont>("asset/fonteWin");

            //Bandeira
            _flag = new Flag();
            _flag.LoadContent(Content);
            _flag.Initialize();

            //Player;
            _player = new Player();
            _player.LoadContent(Content);
            _player.Initialize();

            //Moedas
            _coinTexture = Content.Load<Texture2D>("asset/coin");

            //Lista de posições da moeda
            _coins = new List<Coin>();
            // Adiciona as moedas ao jogo com diferentes posições
            _coins.Add(new Coin(_coinTexture, new Vector2(286, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(373, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(460, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(547, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(634, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(721, 99)));
            _coins.Add(new Coin(_coinTexture, new Vector2(187, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(274, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(361, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(448, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(535, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(622, 483)));
            _coins.Add(new Coin(_coinTexture, new Vector2(230, 630)));
            _coins.Add(new Coin(_coinTexture, new Vector2(317, 630)));
            _coins.Add(new Coin(_coinTexture, new Vector2(404, 630)));
            _coins.Add(new Coin(_coinTexture, new Vector2(491, 630)));
            _coins.Add(new Coin(_coinTexture, new Vector2(578, 630)));
            
            foreach (Coin coin in _coins)
            {
                coin.Initialize();
            }

            //Back Dos Creditos
            _backCredits = Content.Load<Texture2D>("asset/black");
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
          
           //Detector de colisão do player chamado da classe
            Rectangle playerBounds = _player.Bounds;

            //Chama o Update do Player
            _player.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

            foreach (Coin coin in _coins)
            {
                coin.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

                if (!_gameOver && !coin._isCollected && coin.CheckCollision(playerBounds))
                {
                    coin._isCollected = true;
                    _coinsCollected++;
                }
            }

            //Chama o Update da bandeira (Flag)
            _flag.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

            //Coletar todas as moedas pode interagir com a bandeira
            if (!_gameOver && _coinsCollected == _coins.Count)
            {
                _interactFlag = true;

            }

            //Ao interagir com a bandeira acaba o jogo
            if (_interactFlag && playerBounds.Intersects(_flag.flagBounds))
            {
                _gameOver = true;
            }

            //Se GameOver for true, chama os créditos
            if (_gameOver)
            {
                // Chama o Update dos creditos (Credits)
                _credits.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);
                return;
            }

            MouseState mouseState = Mouse.GetState();

            //Chama o Update do menu
            _menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

            //Representa a area de colisão do botão Play e Exit chamando da classe menu (Menu)
            Rectangle playButtonBounds = _menu.playButtonBounds;
            Rectangle exitButtonBounds = _menu.exitButtonBounds;

            //Ao clicar no botão play do menu começa o jogo
            if (mouseState.LeftButton == ButtonState.Pressed && playButtonBounds.Contains(Mouse.GetState().Position))
            {
                StartGame();

            }

            // Ao clicar no botão exit, fecha o jogo
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && exitButtonBounds.Contains(Mouse.GetState().Position))
            {

                Exit(); // Fecha o jogo
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //Chama o Draw do menu (Menu)
            _menu.Draw(_spriteBatch);

            //Inicio de jogo
            if (gameStarted)
            {
                //Chama o fundo do jogo
                _spriteBatch.Draw(_backgroundGame, Vector2.Zero, Color.White);

                //Chama a lista de moedas
                foreach (Coin coin in _coins)
                {
                    if (!coin._isCollected)
                    {
                        coin.Draw(_spriteBatch);
                    }
                }

                //Chama o Draw da classe bandeira (Flag)
                _flag.Draw(_spriteBatch);

                //Desenha a contagem da coleta de moedas
                _spriteBatch.DrawString(_font, "Moedas coletadas: " + _coinsCollected, new Vector2(35, 35), Color.White);

                //Chama o Draw da classe player (Player)
                _player.Draw(_spriteBatch);

                //GameOver
                if (_gameOver)
                {
                    //Apresenta o fundo dos creditos + texto de finalização
                    _spriteBatch.Draw(_backCredits, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(_fontWin, "Voce venceu!", new Vector2(310, 200), Color.White);
                    //Chama o Draw da classe creditos (Credits)
                    _credits.Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        // Método chamado quando o jogo inicia
        private void StartGame()
        {
            // Inicializa a tela do jogo se ainda não estiver inicializada
            _player.Initialize(); 
            // Indicar que o jogo começou
            gameStarted = true; 
        }
        // Método chamado quando o jogo termina
        public void EndGame()
        {
            _gameOver = true;
        }

        // Método para voltar para o menu
        public void ShowMenu()
        {

            _gameOver = false;
            gameStarted = false;
            _coinsCollected = 0;
            _player.Initialize();
            _menu.Initialize();
  
        }

    }
}
