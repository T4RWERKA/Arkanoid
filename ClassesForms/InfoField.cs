using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SFML.System;
using System.Threading.Tasks;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;
using static System.Net.Mime.MediaTypeNames;
using Text = SFML.Graphics.Text;
using System.Collections;

namespace Classes
{
    internal class InfoField: Drawable
    {
        private static readonly string TitleFontPath = "fonts/Arka_solid.ttf";
        private static readonly string TextFontPath = "fonts/Space_Quest.ttf";
        private static readonly string LifeImagePath = "textures/live.png";

        private Text titleText;
        private Text scoreText;
        private Texture lifeTexture;
        private List<Sprite> lifes;

        public event EventHandler LoseEvent;

        public Vector2i Position
        {
            get 
            { 
                return (Vector2i)backgroundSprite.Position; 
            }
            set 
            {
                backgroundSprite.Position = (Vector2f)value;
            }
        }
        public int width, height;
        public Sprite backgroundSprite;
        public InfoField() { }
        public InfoField(int width, int height, Vector2i position) 
        { 
            this.width = width; 
            this.height = height;

            CreateBackground(position);

            // Заголовок
            var titleFont = new Font(TitleFontPath);
            titleText = new Text("Arkanoid", titleFont, 40);
            titleText.FillColor = Color.White;
            FloatRect titleTextBounds = titleText.GetLocalBounds();
            float titleTextWidth = titleTextBounds.Width;
            float titleTextHeight = titleTextBounds.Height;
            titleText.Position = new Vector2f(Position.X + (width - titleTextWidth) / 2, 20);

            // Счет
            var textFont = new Font(TextFontPath);
            scoreText = new Text("Score: 0", textFont, 30);
            scoreText.FillColor = Color.White;
            scoreText.Position = new Vector2f(Position.X + 10, titleText.Position.Y + titleTextHeight + 30);

            // Создание спрайтов для жизней
            lifeTexture = new Texture(LifeImagePath);
            lifes = new List<Sprite>();
            for (int i = 0; i < 4; i++)
            {
                AddLife();
            }
        }
        private void CreateBackground(Vector2i position)
        {
            var rect = new RectangleShape(new Vector2f(width, height));
            rect.FillColor = Color.Black;
            var renderTexture = new RenderTexture((uint)width, (uint)height);
            renderTexture.Clear(Color.Red);
            renderTexture.Draw(rect);
            var texture = renderTexture.Texture;
            backgroundSprite = new Sprite(texture);
            Position = position;
            backgroundSprite.Scale = new Vector2f((float)width / backgroundSprite.TextureRect.Width, (float)height / backgroundSprite.TextureRect.Height);
        }
        public void SetScore(int score)
        {
            scoreText.DisplayedString = $"Score: {score}";
        }
        private void AddLife()
        {
            Sprite lifeSprite = new Sprite(lifeTexture);
            lifeSprite.Scale = new Vector2f(width * 0.2f / lifeSprite.TextureRect.Width, width * 0.2f / lifeSprite.TextureRect.Width);

            FloatRect lifeSpriteBounds = lifeSprite.GetLocalBounds();
            float lifeSpriteWidth = lifeSpriteBounds.Width * lifeSprite.Scale.X;
            float lifeSpriteHeight = lifeSpriteBounds.Height * lifeSprite.Scale.Y;
            lifeSprite.Position = new Vector2f(Position.X + 10 + lifes.Count * (lifeSpriteWidth + 10), height - 30 - lifeSpriteHeight);

            lifes.Add(lifeSprite);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(backgroundSprite, states);
            target.Draw(titleText, states);
            target.Draw(scoreText, states);
            foreach (Sprite lifeSprite in lifes)
                target.Draw(lifeSprite, states);
        }
        public void OnBallFell(object? sender, EventArgs e)
        {
            lifes.RemoveAt(lifes.Count - 1);
            if (lifes.Count == 0)
                LoseEvent?.Invoke(this, EventArgs.Empty);
        }
        public void OnUpdateField(object? sender, UpdateFieldEventArgs e)
        {
            SetScore(0);
        }
    }
}