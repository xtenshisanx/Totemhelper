using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PoeHUD.Framework.InputHooks;
using PoeHUD.Framework;
using PoeHUD.Models;
using PoeHUD.Plugins;

using SharpDX;

using TotemHelper.Classes;
namespace TotemHelper.Core
{
    public class Core : BaseSettingsPlugin<Settings>
    {
        private HashSet<Totem> ActiveTotems;
        private Vector2 MousePosition;
        private CustomCamera customCamera;
        private Boolean KeyDown;
        public override void Initialise()
        {
            PluginName = "TotemHelper";
            KeyDown = false;
            ActiveTotems = new HashSet<Totem>();
            MouseHook.MouseMove += MouseHook_MouseMove;
            customCamera = GameController.Game.IngameState.Camera.AsObject<CustomCamera>();
        }

        private void MouseHook_MouseMove(MouseInfo obj)
        {
            MousePosition.X = obj.Position.X;
            MousePosition.Y = obj.Position.Y;
        }

        public override void Render()
        {
            List<Vector2> pointlist = new List<Vector2>();
            Int32 range = Settings.TotemRadius * 6;
            if(GameController.InGame)
            {
                var playerWorldPos = GameController.Player.Pos;
                foreach(Totem totem in ActiveTotems.ToList())
                {
                    pointlist = Visuals.CirclePoints(totem.Position, range, Settings.RadiusDots);
                    foreach(Vector2 vector in pointlist)
                    {
                        var point = customCamera.WorldToScreen(new Vector3(vector,totem.Position.Z));
                        Graphics.DrawBox(new RectangleF(point.X, point.Y, 5, 5), Color.Green);
                    }
                }
                if ((WinApi.IsKeyDown(Keys.ShiftKey) && KeyDown && Settings.HoldKey) || !Settings.HoldKey)
                {
                    pointlist = Visuals.CirclePoints(new Vector3(customCamera.MouseWorldPosition, playerWorldPos.Z), range, Settings.RadiusDots);
                    foreach (Vector2 vector in pointlist)
                    {
                        var point = customCamera.WorldToScreen(new Vector3(vector, playerWorldPos.Z));
                        Graphics.DrawBox(new RectangleF(point.X, point.Y, 5, 5), Color.Green);
                    }
                }
                if(WinApi.IsKeyDown(Keys.ShiftKey) && !KeyDown && Settings.HoldKey)
                {
                    KeyDown = true;
                }
                if(!WinApi.IsKeyDown(Keys.ShiftKey) && KeyDown && Settings.HoldKey)
                {
                    KeyDown = false;
                }
            }
        }
        public override void EntityAdded(EntityWrapper entityWrapper)
        {
            if (entityWrapper.Path.ToLower().Contains("totem") && !entityWrapper.IsHostile)
            {
                ActiveTotems.Add(new Totem(entityWrapper));
            }
        }
        public override void EntityRemoved(EntityWrapper entityWrapper)
        {
            ActiveTotems.Remove(ActiveTotems.FirstOrDefault(totem => totem.Position == entityWrapper.Pos));
        }
    }
}
