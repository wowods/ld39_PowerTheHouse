using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace ld39
{
    static class Art
    {
        // Texture2D
        public static Texture2D PowerPlant { get; private set; }
        public static Texture2D Pole { get; private set; }
        public static Texture2D House { get; private set; }
        public static Texture2D Grass { get; private set; }
        public static Texture2D Blocked { get; private set; }
        public static Texture2D Wall { get; private set; }
        public static Texture2D Water { get; private set; }
        public static Texture2D Indicator { get; private set; }
        public static Texture2D Cursor { get; private set; }

        // Font
        public static SpriteFont FontSmall { get; private set; }
        public static SpriteFont FontMedium { get; private set; }
        public static SpriteFont FontLarge { get; private set; }

        // SoundEffect
        public static SoundEffect Cannot { get; private set; }
        public static SoundEffect Pick { get; private set; }
        public static SoundEffect Drop { get; private set; }
        public static SoundEffect Done { get; private set; }

        // Color
        public static Color NoTint= Color.White;
        public static Color SelectedTint = Color.LightSalmon;
        public static Color RangeTint = Color.LightSeaGreen;

        public static void Load(ContentManager contentManager)
        {
            // Load Texture2D
            PowerPlant = contentManager.Load<Texture2D>("power");
            Pole = contentManager.Load<Texture2D>("pole");
            House = contentManager.Load<Texture2D>("house");
            Grass = contentManager.Load<Texture2D>("grass");
            Blocked = contentManager.Load<Texture2D>("blocked");
            Wall = contentManager.Load<Texture2D>("wall");
            Water = contentManager.Load<Texture2D>("water");
            Indicator = contentManager.Load<Texture2D>("indicator");
            Cursor = contentManager.Load<Texture2D>("cursor");

            // Load SoundEffect
            Cannot = contentManager.Load<SoundEffect>("cannot");
            Pick = contentManager.Load<SoundEffect>("pick");
            Drop = contentManager.Load<SoundEffect>("drop");
            Done = contentManager.Load<SoundEffect>("done");

            // Load SpriteFont
            FontSmall = contentManager.Load<SpriteFont>("Font_small");
            FontMedium = contentManager.Load<SpriteFont>("Font_medium");
            FontLarge = contentManager.Load<SpriteFont>("Font_large");
        }
    }
}
