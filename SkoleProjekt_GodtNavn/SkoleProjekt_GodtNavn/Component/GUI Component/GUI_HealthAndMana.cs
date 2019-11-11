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
    public class GUI_HealthAndMana 
    {
        List<GUI_Component> ui = new List<GUI_Component>();

        GUI_Panel healthPanel;
        GUI_healthManaImage healthImage;

        GUI_Panel manaPanel;
        GUI_Panel manaImage;

        public void GUI_Setup()
        {
            Texture2D healthPanelImage = Gameworld.spriteContainer.soleSprite["HealthBar"];
            healthPanel = new GUI_Panel(healthPanelImage)
            {
                Position = new Vector2(0, Gameworld.ScreenSize.Y),
                Origin = GUI_OriginPosition.BottomLeft,
                Scale = new Vector2(2.0f, 2.0f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            healthPanel.SetOrigin();
            ui.Add(healthPanel);

            Texture2D _healthImage = Gameworld.spriteContainer.soleSprite["CircleWhite4"];
            healthImage = new GUI_healthManaImage(_healthImage)
            {
                Position = new Vector2(0 + _healthImage.Width / 2 + 70, Gameworld.ScreenSize.Y - _healthImage.Height + 107),
                Origin = GUI_OriginPosition.BottomLeft,
                Scale = new Vector2(0.70f, 0.70f),
                layerDepth = 0.7f,
                ShowGUI = true,
                defaultColor = Color.Red,
                trueHealth_falseMana = true
            };
            healthImage.SetOrigin();
            ui.Add(healthImage);




            Texture2D manaPanelImage = Gameworld.spriteContainer.soleSprite["ManaBar"];
            manaPanel = new GUI_Panel(manaPanelImage)
            {
                Position = new Vector2(Gameworld.ScreenSize.X, Gameworld.ScreenSize.Y),
                Origin = GUI_OriginPosition.BottomRigth,
                Scale = new Vector2(2.0f, 2.0f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            manaPanel.SetOrigin();
            ui.Add(manaPanel);

            Texture2D _manaImage = Gameworld.spriteContainer.soleSprite["CircleWhite4"];
            healthImage = new GUI_healthManaImage(_manaImage)
            {
                Position = new Vector2(Gameworld.ScreenSize.X - _manaImage.Width / 2 + 70, Gameworld.ScreenSize.Y - _manaImage.Height + 107),
                Origin = GUI_OriginPosition.BottomLeft,
                Scale = new Vector2(0.70f, 0.70f),
                layerDepth = 0.7f,
                ShowGUI = true,
                defaultColor = Color.Blue,
                trueHealth_falseMana = false
            };
            healthImage.SetOrigin();
            ui.Add(healthImage);

            // at the end.
            foreach (GUI_Component x in ui)
            {
                Gameworld.AddGUI(x);
            }
        }
    }
}
