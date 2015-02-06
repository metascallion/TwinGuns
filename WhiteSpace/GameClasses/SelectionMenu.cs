using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using WhiteSpace.Composite;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Drawables;
using WhiteSpace.Content;

namespace WhiteSpace.GameClasses
{
    public class SelectionMenu
    {
        ComponentsSector<lobbystate> sector;
        GameObject go;

        public SelectionMenu()
        {
            this.sector = new ComponentsSector<lobbystate>(lobbystate.selection);
            start();
        }

        public void start()
        {
            buildUI();
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.selection);
        }

        private void buildUI()
        {
            GameObjectFactory.createTexture(sector, Vector2.Zero, new Vector2(1280, 720), ContentLoader.getContent<Texture2D>("UIBackground"));
            Transform playTrans = Transform.createTransformWithSizeOnPosition(new Vector2(639.5f - 85, 375 - 52), new Vector2(169, 104));
            GameObject play = GameObjectFactory.createButton(this.sector, playTrans);
            Button b = play.getComponent<Button>();
            b.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("playnormal")));
            b.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("playhover")));
            b.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("playactive")));

            Transform createTrans = Transform.createTransformWithSizeOnPosition(new Vector2(406.5f - 60, 382.5f - 42), new Vector2(121, 85));
            GameObject create = GameObjectFactory.createButton(this.sector, createTrans);
            Button b2 = create.getComponent<Button>();
            b2.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("createnormal")));
            b2.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("createhover")));
            b2.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("createactive")));

            Transform exitTrans = Transform.createTransformWithSizeOnPosition(new Vector2(1008f - 39, 378.5f - 30), new Vector2(78, 61));
            GameObject exit = GameObjectFactory.createButton(this.sector, exitTrans);
            Button b3 = exit.getComponent<Button>();
            b3.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("exitnormal")));
            b3.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("exithover")));
            b3.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("exitactive")));


            Transform optionsTrans = Transform.createTransformWithSizeOnPosition(new Vector2(874 - 61, 383 - 42), new Vector2(123, 85));
            GameObject options = GameObjectFactory.createButton(this.sector, optionsTrans);
            Button b4 = options.getComponent<Button>();
            b4.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("optionsnormal")));
            b4.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("optionshover")));
            b4.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("optionsactive")));


            Transform creditsTrans = Transform.createTransformWithSizeOnPosition(new Vector2(273 - 40, 380 - 33), new Vector2(80, 67));
            GameObject credits = GameObjectFactory.createButton(this.sector, creditsTrans);
            Button b5 = credits.getComponent<Button>();
            b5.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("creditsnormal")));
            b5.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("creditshover")));
            b5.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("creditsactive")));
            //buildNameTextField();
            //buildOkButton();
        }

        //private void buildNameTextField()
        //{
        //    Transform textTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 10), new Vector2(100, 20));
        //    Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(100, 10), new Vector2(200, 30));
        //    go = GameObjectFactory.createTextField(this.sector, transform);
        //    GameObjectFactory.createLabel(this.sector, textTransform, "Name:");
        //}

        //private void buildOkButton()
        //{
        //    Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(310, 10), new Vector2(40, 30));
        //    GameObjectFactory.createButton(this.sector, transform, "Ok", nextState);
        //}

        private void nextState(Clickable sender)
        {
            //Client.name = go.getComponent<EditableText>().textDrawer.text;
            new MatchmakingMenu();

        }
    }
}
