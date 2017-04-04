using PoeHUD.Models;
using PoeHUD.Framework;
using PoeHUD.Poe.Components;
using System;
using System.Numerics;
using SharpDX;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = System.Numerics.Vector4;

using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe;

namespace TotemHelper.Classes
{
    public class CustomCamera : RemoteMemoryObject
    {
        public int Width => M.ReadInt(Address + 0x4);
        public int Height => M.ReadInt(Address + 0x8);
        public float ZFar => M.ReadFloat(Address + 0x25C);
        public Vector3 Position => new Vector3(M.ReadFloat(Address + 0x200), M.ReadFloat(Address + 0x204), M.ReadFloat(Address + 0x208));

        public unsafe Vector2 MouseWorldPosition
        {
            get {
                Vector2 result = new Vector2();
                long addrX = Address + 0x3B0;
                long addrY = Address + 0x3B4;
                fixed (byte* numRef = M.ReadBytes(addrX, 0x8))
                {
                    result.X = *(float*)numRef;
                }
                fixed (byte* numRef = M.ReadBytes(addrY, 0x8))
                {
                    result.Y = *(float*)numRef;
                }
                return result;
            }
        }

        public Matrix4x4 Matrix { get { return getMatrix(); } }

        //cameraarray 0x17c

        private static Vector2 oldplayerCord;

        public unsafe CustomCamera()
        {
        }

        private unsafe Matrix4x4 getMatrix()
        {
            Matrix4x4 result = new Matrix4x4();
            float a = new float();
            float playerX = PoeHUD.Plugins.BasePlugin.API.GameController.Player.Pos.X;
            long addr = Address + 0x184;
            fixed (byte* numRef = M.ReadBytes(addr, 0x40))
            {
                result = *(Matrix4x4*)numRef;
            }
            addr = Address + 0x3B0;
            fixed (byte* numRef = M.ReadBytes(addr, 0x8))
            {
                a = *(float*)numRef;
            }

            return result;
        }

        public unsafe Vector2 WorldToScreen(Vector3 vec3)
        {
            float x, y;
            Matrix4x4 matrix = Matrix;
            Vector4 cord = *(Vector4*)&vec3;
            cord.W = 1;
            cord = Vector4.Transform(cord, matrix);
            cord = Vector4.Divide(cord, cord.W);
            x = (cord.X + 1.0f) * 0.5f * Width;
            y = (1.0f - cord.Y) * 0.5f * Height;
            return new Vector2(x, y);
        }
    }
}