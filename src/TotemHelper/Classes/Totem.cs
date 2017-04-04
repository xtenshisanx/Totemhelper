using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoeHUD.Models;
using SharpDX;
namespace TotemHelper.Classes
{
    class Totem
    {
        //Variables
        public EntityWrapper Entity;
        
        //Indexer
        public Vector3 Position { get { return Entity.Pos; } }

        //Constructor
        public Totem(EntityWrapper totemEntity)
        {
            Entity = totemEntity;
        }

        //Functions
    }
}
