using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class GUI_Inventory 
    {
        Texture2D buttonImage;
        Texture2D panel;
        SpriteFont spriteFont;

        private bool showGUI;
        public bool ShowGUI { get { return showGUI; } set { showGUI = value; } }

        public List<GUI_Component> gui_inventory = new List<GUI_Component>();

        public  void LoadContent(ContentManager content)
        {
            // Load in Image
            buttonImage = content.Load<Texture2D>("Texture/UI/Button");
            panel = content.Load<Texture2D>("Texture/UI/MenuBackground");
            // Load in Font
            spriteFont = content.Load<SpriteFont>("Font/NormalFont");
        }

        public void GUI_Setup()
        {

            var panel01 = new GUI_Panel(panel)
            {
                Position = new Vector2(100, 150),
                Origin = GUI_OriginPosition.TopLeft,
                Scale = 0.5f
            };
            panel01.SetOrigin();
            gui_inventory.Add(panel01);

            var panelTitle01 = new GUI_Font(spriteFont)
            {
                Position = new Vector2(panel01.Position.X + 150, panel01.Position.Y + 20),
                Text = "Character",
                FontScale = 1f
            };
            gui_inventory.Add(panelTitle01);

            var randomButton = new GUI_Button(buttonImage, spriteFont)
            {
                Position = new Vector2(panel01.Position.X + 370, panel01.Position.Y + 120),
                Text = "Random",
                FontScale = 0.5f,
                ButtonScale = 1f
            };
            randomButton.Click += RandomButton_Click;
            gui_inventory.Add(randomButton);

            var randomButton01 = new GUI_Button(buttonImage, spriteFont)
            {
                Position = new Vector2(panel01.Position.X + 370, panel01.Position.Y + 160),
                Text = "Jamen",
                FontScale = 0.5f,
                ButtonScale = 1f
            };
            randomButton01.Click += RandomButton_Click;
            gui_inventory.Add(randomButton01);



            // at the end.
            foreach (GUI_Component x in gui_inventory)
            {
                x.ShowGUI = true;
                Gameworld.AddGUI(x);
            }
        }




        public void Test()
        {
            var randomButton = new GUI_Button(buttonImage, spriteFont)
            {
                Position = new Vector2(350, 200),
                Text = "Random",
            };

            randomButton.Click += RandomButton_Click;
        }

        

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();

            Gameworld.backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }


    }
}
