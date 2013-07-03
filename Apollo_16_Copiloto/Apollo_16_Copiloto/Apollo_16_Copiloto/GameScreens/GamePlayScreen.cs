using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;

namespace Apollo_16_Copiloto
{

    
    public class GamePlayScreen : BaseGameState
    {
        Texture2D backgroundTexture;
        Texture2D itemBackground;
        Texture2D shipOverview;
        Texture2D buttonTexture;
        Texture2D buttonPressedTexture;

        Int32 selectedItem = -1;

        ItemClass draggingIcon;
        Boolean dragging = false;

        ItemClass primaryWeaponSlotOne;
        ItemClass primaryWeaponSlotTwo;

        ItemClass secondaryWeaponSlotOne;
        ItemClass secondaryWeaponSlotTwo;

        ItemClass shieldSlot;
        ItemClass hullSlot;

        ItemClass engineSlot;

        Int32 weaponStatus;
        Int32 hullStatus;
        Int32 controlStatus;
        Int32 engineStatus;

        Texture2D mouseTexture;
        Vector2 mousePos;

        SpriteFont font;
        SpriteFont invFont;
        List<ItemClass> inventory = new List<ItemClass>(24);

        String status;

        Boolean leftMouseOnButton = false;

        public Texture2D repair;
        public Texture2D fuel;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            systemRef = (SystemClass)game;
        }

        /* XNA Methods */
        public override void Initialize()
        {
            weaponStatus = 10;
            hullStatus = 30;
            controlStatus = 60;
            engineStatus = 90;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            buttonTexture = content.Load<Texture2D>(@"Copiloto\Button");
            buttonPressedTexture = content.Load<Texture2D>(@"Copiloto\Button_pressed");
            mouseTexture = content.Load<Texture2D>(@"Copiloto\Cursor");

            font = content.Load<SpriteFont>(@"Copiloto\OverviewFont");
            invFont = content.Load<SpriteFont>(@"Copiloto\inventoryFont");

            backgroundTexture = content.Load<Texture2D>(@"Copiloto\Geral");
            itemBackground = content.Load<Texture2D>(@"Copiloto\itemBackground");
            shipOverview = content.Load<Texture2D>(@"Copiloto\Nave");
            repair = content.Load<Texture2D>(@"Copiloto\Icons\Concentrate Boming");
            fuel = content.Load<Texture2D>(@"Copiloto\Icons\Guild");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadPackets(this);
            MouseState mouseState = Mouse.GetState();
            mousePos.X = mouseState.X - 4;
            mousePos.Y = mouseState.Y - 4;
            int num;
            if ((num = MouseAtButton(mouseState)) >= 0)
            {
                if (mouseState.LeftButton == ButtonState.Released && leftMouseOnButton == true)
                {
                    PressButton(num);
                }
                else if (mouseState.LeftButton == ButtonState.Pressed && leftMouseOnButton == false)
                {
                    leftMouseOnButton = true;
                }
            }
            else
            {
                leftMouseOnButton = false;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && dragging == false)
            {
                if (selectedItem != -1)
                {
                    dragging = true;
                    draggingIcon = inventory.ElementAt(selectedItem);
                    inventory.RemoveAt(selectedItem);
                    selectedItem = -1;
                }
            } else if (mouseState.LeftButton == ButtonState.Released && dragging == true)
            {
                selectedItem = -1;
                MouseCheckRelease(mouseState);
                dragging = false;
            } else
                selectedItem = MouseAtInventory(mouseState);

            base.Update(gameTime);
        }

        private void PressButton(int num)
        {
            status = "Botao " + num;
        }

        private Int32 MouseAtInventory(MouseState mouseState)
        {
            Vector2 pos = new Vector2(90, 543);
            Rectangle r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);

            for (int i = 0; i < 24; i++)
            {
                r.X = (int)pos.X + ((i % 12) * 70);
                r.Y = (int)pos.Y + ((i / 12) * 70);
                if (inventory.ElementAtOrDefault(i) != null)
                {
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private Int32 MouseAtButton(MouseState mouseState)
        {
            Vector2 pos;
            
            Rectangle r;

            pos = new Vector2(678, 97);
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            for (int i = 0; i < 5; i++)
            {
                r.Y = (int)pos.Y + (i * 40);
                if (r.Contains(mouseState.X, mouseState.Y))
                {
                    return i;
                }
            }
            return -1;
        }

        private void MouseCheckRelease(MouseState mouseState)
        {
            Vector2 pos;

            Rectangle r;
            if (dragging == true)
            {
                if (draggingIcon.type == ItemType.PRIMARY_WEAPON)
                {
                    pos = new Vector2(138, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (primaryWeaponSlotOne != null)
                            inventory.Add(primaryWeaponSlotOne);

                        primaryWeaponSlotOne = draggingIcon;
                        return;
                    }

                    pos = new Vector2(208, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (primaryWeaponSlotTwo != null)
                            inventory.Add(primaryWeaponSlotTwo);

                        primaryWeaponSlotTwo = draggingIcon;
                        return;
                    }
                }
                else if (draggingIcon.type == ItemType.SECONDARY_WEAPON)
                {
                    pos = new Vector2(302, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (secondaryWeaponSlotOne != null)
                            inventory.Add(secondaryWeaponSlotOne);

                        secondaryWeaponSlotOne = draggingIcon;
                        return;
                    }

                    pos = new Vector2(372, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (secondaryWeaponSlotTwo != null)
                            inventory.Add(secondaryWeaponSlotTwo);

                        secondaryWeaponSlotTwo = draggingIcon;
                        return;
                    }
                }

                if (draggingIcon.type == ItemType.SHIELD)
                {
                    pos = new Vector2(535, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (shieldSlot != null)
                            inventory.Add(shieldSlot);

                        shieldSlot = draggingIcon;
                        return;
                    }
                }

                if (draggingIcon.type == ItemType.HULL)
                {
                    pos = new Vector2(636, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (hullSlot != null)
                            inventory.Add(hullSlot);

                        hullSlot = draggingIcon;
                        return;
                    }
                }
                if (draggingIcon.type == ItemType.ENGINE)
                {
                    pos = new Vector2(799, 429);
                    r = new Rectangle((int)pos.X, (int)pos.Y, 69, 69);
                    if (r.Contains(mouseState.X, mouseState.Y))
                    {
                        if (engineSlot != null)
                            inventory.Add(engineSlot);

                        engineSlot = draggingIcon;
                        return;
                    }
                }
                inventory.Add(draggingIcon);
            }
        }

        private Color GetColor(Int32 value)
        {
            if (value > 70)
                return Color.Green;
            if (value > 30)
                return Color.Yellow;
            return Color.Red;
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            systemRef.spriteBatch.Begin();
            //systemRef.spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            DrawInventory(systemRef.spriteBatch);
            DrawWeapon(systemRef.spriteBatch);
            DrawShield(systemRef.spriteBatch);
            DrawStatus(systemRef.spriteBatch);
            DrawOverview(systemRef.spriteBatch);
            DrawControlPanel(systemRef.spriteBatch);
            DrawLabel(systemRef.spriteBatch);

            DrawCursor(systemRef.spriteBatch);
            
            systemRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawCursor(SpriteBatch spriteBatch)
        {
            if (dragging)
            {
                systemRef.spriteBatch.Draw(draggingIcon.texture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            else
            {
                systemRef.spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
        }
        private void DrawLabel(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Estado da Nave", new Vector2(180, 39), Color.White);
            spriteBatch.DrawString(font, "Armas", new Vector2(34, 100), Color.Blue);
            spriteBatch.DrawString(font, weaponStatus + "%", new Vector2(34, 120), GetColor(weaponStatus));
            spriteBatch.DrawString(font, "Controle", new Vector2(467, 100), Color.Blue);
            spriteBatch.DrawString(font, controlStatus + "%", new Vector2(467, 120), GetColor(controlStatus));
            spriteBatch.DrawString(font, "Casco", new Vector2(34, 182), Color.Blue);
            spriteBatch.DrawString(font, hullStatus + "%", new Vector2(34, 202), GetColor(hullStatus));
            spriteBatch.DrawString(font, "Motor", new Vector2(467, 182), Color.Blue);
            spriteBatch.DrawString(font, engineStatus + "%", new Vector2(467, 202), GetColor(engineStatus));

            spriteBatch.DrawString(font, "Armamento", new Vector2(210, 370), Color.White);
            spriteBatch.DrawString(font, "Primario", new Vector2(152, 400), Color.Blue);
            spriteBatch.DrawString(font, "Secundario", new Vector2(300, 400), Color.Blue);

            spriteBatch.DrawString(font, "Defesa", new Vector2(570, 370), Color.White);
            spriteBatch.DrawString(font, "Escudo", new Vector2(525, 400), Color.Blue);
            spriteBatch.DrawString(font, "Casco", new Vector2(631, 400), Color.Blue);

            spriteBatch.DrawString(font, "Nave", new Vector2(798, 370), Color.White);
            spriteBatch.DrawString(font, "Motor", new Vector2(793, 400), Color.Blue);

            spriteBatch.DrawString(font, "Inventario", new Vector2(453, 514), Color.White);
            if(selectedItem != -1)
                spriteBatch.DrawString(font, "Item: " + inventory.ElementAt(selectedItem).name, new Vector2(98, 712), Color.Blue);
            else 
                spriteBatch.DrawString(font, "Item:", new Vector2(98, 712), Color.Blue);

            spriteBatch.DrawString(font, "Painel de Controle", new Vector2(670, 39), Color.White);
        }
        private void DrawInventory(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(90, 543);
            for (int i = 0; i < 24; i++)
            {
                spriteBatch.Draw(itemBackground, pos + new Vector2(((i % 12) * 70), (i / 12) * 70), Color.White);
                if (inventory.ElementAtOrDefault(i) != null)
                {
                    Vector2 relative = new Vector2(((i % 12) * 70), ((i / 12) * 70));
                    spriteBatch.Draw(inventory.ElementAt(i).texture, pos + relative + new Vector2(2,2), Color.Wheat);
                    if(inventory.ElementAt(i).countable == true)
                        spriteBatch.DrawString(invFont, "x" + inventory.ElementAt(i).amount, pos + relative + new Vector2(4, 69 - 16), Color.White);
                }
            }
        }

        private void DrawWeapon(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(138, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (primaryWeaponSlotOne != null)
            {
                spriteBatch.Draw(primaryWeaponSlotOne.texture, pos + new Vector2(2, 2), Color.Wheat);
                spriteBatch.DrawString(invFont, "x" + primaryWeaponSlotOne.amount,pos + new Vector2(4, 69 - 16), Color.White);
            }

            pos = new Vector2(208, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (primaryWeaponSlotTwo != null)
            {
                spriteBatch.Draw(primaryWeaponSlotTwo.texture, pos + new Vector2(2, 2), Color.Wheat);
                spriteBatch.DrawString(invFont, "x" + primaryWeaponSlotTwo.amount, pos + new Vector2(4, 69 - 16), Color.White);
            }

            pos = new Vector2(302, 429);

            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (secondaryWeaponSlotOne != null)
            {
                spriteBatch.Draw(secondaryWeaponSlotOne.texture, pos + new Vector2(2, 2), Color.Wheat);
                spriteBatch.DrawString(invFont, "x" + secondaryWeaponSlotOne.amount, pos + new Vector2(4, 69 - 16), Color.White);
            }

            pos = new Vector2(372, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (secondaryWeaponSlotTwo != null)
            {
                spriteBatch.Draw(secondaryWeaponSlotTwo.texture, pos + new Vector2(2, 2), Color.Wheat);
                spriteBatch.DrawString(invFont, "x" + secondaryWeaponSlotTwo.amount, pos + new Vector2(4, 69 - 16), Color.White);
            }
        }

        private void DrawShield(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(535, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (shieldSlot != null)
            {
                spriteBatch.Draw(shieldSlot.texture, pos + new Vector2(2, 2), Color.Wheat);
            }

            pos = new Vector2(636, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (hullSlot != null)
            {
                spriteBatch.Draw(hullSlot.texture, pos + new Vector2(2, 2), Color.Wheat);
            }
        }

        private void DrawStatus(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(799, 429);
            spriteBatch.Draw(itemBackground, pos, Color.White);
            if (engineSlot != null)
            {
                spriteBatch.Draw(engineSlot.texture, pos + new Vector2(2, 2), Color.Wheat);
            }
        }

        private void DrawOverview(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipOverview, new Vector2(158, 104), Color.White);
        }

        private void DrawControlPanel(SpriteBatch spriteBatch)
        {
            Vector2 pos;
            pos = new Vector2(678, 97);
            Rectangle r;
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            if (r.Contains((int)mousePos.X, (int)mousePos.Y))
            {
                spriteBatch.Draw(buttonPressedTexture, pos, Color.White);
            } else
                spriteBatch.Draw(buttonTexture, pos, Color.White);
            spriteBatch.DrawString(font, "Ativar Escudo", pos + new Vector2(8, 2), Color.Yellow);

            pos = new Vector2(678, 137);
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            if (r.Contains((int)mousePos.X, (int)mousePos.Y))
            {
                spriteBatch.Draw(buttonPressedTexture, pos, Color.White);
            }
            else
                spriteBatch.Draw(buttonTexture, pos, Color.White);
            spriteBatch.DrawString(font, "Rep. Armas", pos + new Vector2(8, 2), Color.Yellow);

            pos = new Vector2(678, 177);
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            if (r.Contains((int)mousePos.X, (int)mousePos.Y))
            {
                spriteBatch.Draw(buttonPressedTexture, pos, Color.White);
            }
            else
                spriteBatch.Draw(buttonTexture, pos, Color.White);
            spriteBatch.DrawString(font, "Rep. Casco", pos + new Vector2(8, 2), Color.Yellow);

            pos = new Vector2(678, 217);
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            if (r.Contains((int)mousePos.X, (int)mousePos.Y))
            {
                spriteBatch.Draw(buttonPressedTexture, pos, Color.White);
            }
            else
                spriteBatch.Draw(buttonTexture, pos, Color.White);
            spriteBatch.DrawString(font, "Rep. Motor", pos + new Vector2(8, 2), Color.Yellow);

            pos = new Vector2(678, 257);
            r = new Rectangle((int)pos.X, (int)pos.Y, 212, 34);
            if (r.Contains((int)mousePos.X, (int)mousePos.Y))
            {
                spriteBatch.Draw(buttonPressedTexture, pos, Color.White);
            }
            else
                spriteBatch.Draw(buttonTexture, pos, Color.White);
            spriteBatch.DrawString(font, "Rep. Controle", pos + new Vector2(8, 2), Color.Yellow);

            pos = new Vector2(678, 300);
            spriteBatch.DrawString(font, "Acao: " + status, pos + new Vector2(8, 2), Color.Blue);
        }

        public void ParseInput(CopilotDataClass input)
        {
            inventory = input.inventory;
        }
    }
}

